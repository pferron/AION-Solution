using AION.Accela.Engine.Helpers;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using AION.Manager.Models.Estimation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class CalcAvgEstimationFactorAdapter : LoggingWrapper, ICalcAvgEstimationFactorAdapter
    {
        private IDataContextLegacyProjectDataBO _dataContextLegacyProjectData;
        private IDataContextAutoEstimationRefBO _dataContextAutoEstimation;
        private IDataContextProjectBusinessRelationshipBO _dataContextProjectBusinessRelationship;
        private IDataContextAverageEstimationHoursFactorBO _dataContextAverageEstimationHoursFactor;
        private IDataContextProjectBO _dataContextProjectBO;

		public bool UseLegacyData = false;

		private DateTime _GoLiveDate = DateTime.Parse(ConfigurationManager.AppSettings["GoLiveDate"]); // must be updated for actual go live date from web.config
		private List<ProjectBE> _MMFCompletedProjects;
        private List<OccupancyTypeRefBE> _OccupancyTypes;

        public CalcAvgEstimationFactorAdapter(
            IDataContextLegacyProjectDataBO dataContextLegacyProjectData,
            IDataContextAutoEstimationRefBO dataContextAutoEstimation,
            IDataContextProjectBusinessRelationshipBO dataContextProjectBusinessRelationship,
            IDataContextAverageEstimationHoursFactorBO dataContextAverageEstimationHoursFactor,
            IDataContextProjectBO dataContextProjectBO)
        {
            _dataContextLegacyProjectData = dataContextLegacyProjectData;
            _dataContextAutoEstimation = dataContextAutoEstimation;
            _dataContextProjectBusinessRelationship = dataContextProjectBusinessRelationship;
            _dataContextAverageEstimationHoursFactor = dataContextAverageEstimationHoursFactor;
            _dataContextProjectBO = dataContextProjectBO;

            _OccupancyTypes = new OccupancyTypeRefBO().GetList();
		}

        public bool ProcessData()
        {
            bool success = false;
            try
            {
                List<AveragedData> averagedData = new List<AveragedData>();

                List<Factor> factorData = new List<Factor>();

				UseLegacyData = DetermineIfUsingLegacyData(_GoLiveDate);

                AutoEstimationRefBE config = GetEstimationConfigurationParameters();

                averagedData = UseLegacyData ? GetLegacyAveragedData() : GetAveragedData();

                if (averagedData != null && averagedData.Count > 0) {
                    factorData = GetFactors(averagedData, config);
                    success = SaveFactors(factorData);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter ProcessData - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
            return success;
        }

        public AutoEstimationRefBE GetEstimationConfigurationParameters()
        {
            try
            {
                AutoEstimationRefBE config = _dataContextAutoEstimation.GetActive();

                if (config.Months == null || config.Months == 0)
                {
                    config.Months = 24;
                }

                int actualMonthsSinceGoLiveDate = ((DateTime.Now.Year - _GoLiveDate.Year) * 12) + DateTime.Now.Month - _GoLiveDate.Month;

                if (actualMonthsSinceGoLiveDate < config.Months)
                {
                    config.Months = actualMonthsSinceGoLiveDate;
                }

                return config;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter GetEstimationConfigurationParameters - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
        }
        
        public List<AveragedData> GetLegacyAveragedData()
        {
            try
            {
                List<AveragedData> averageHoursCollection = new List<AveragedData>();

                List<LegacyProjectEstimationHoursRefBE> legacyEstimationProjects = GetLegacyEstimationProjects();

                List<MMFHoursGroupingFlattened> flattenedProjects = GetMMFProjectsCompletedSummary();

                foreach (LegacyProjectEstimationHoursRefBE legacyEstimationProject in legacyEstimationProjects)
                {
                    MMFHoursGroupingFlattened flattened = flattenedProjects.FirstOrDefault(x => x.OccupancyTypeId == legacyEstimationProject.OccupancyTypRefId
                            && x.ConstructionTypeShortDesc == legacyEstimationProject.ConstrTypTxt);
                    
                    decimal flattenedTotalProjects = (flattened != null) ? flattened.TotalProjects : 0;

                    decimal flattenedBuilding = (flattened != null) ? flattened.BuildingHours : 0;
                    decimal flattenedElectrical = (flattened != null) ? flattened.ElectricalHours : 0;
                    decimal flattenedMechanical = (flattened != null) ? flattened.MechanicalHours : 0;
                    decimal flattenedPlumbing = (flattened != null) ? flattened.PlumbingHours : 0;
                    decimal flattenedCostOfConstruction = (flattened != null) ? flattened.CostOfConstruction : 0;
                    decimal flattenedSquareFootage = (flattened != null) ? flattened.SquareFootage : 0;
                    decimal flattenedSheetsCount = (flattened != null) ? flattened.SheetsCount : 0;

					decimal avgBuilding = (legacyEstimationProject.BuildHoursNbr.Value + flattenedBuilding) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);
                    decimal avgElectrical = (legacyEstimationProject.ElectHoursNbr.Value + flattenedElectrical) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);
                    decimal avgMechanical = (legacyEstimationProject.MechHoursNbr.Value + flattenedMechanical) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);
                    decimal avgPlumbing = (legacyEstimationProject.PlumbHoursNbr.Value + flattenedPlumbing) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);

                    decimal avgCostOfConstruction = (legacyEstimationProject.TotalConstrCostAmt.Value + flattenedCostOfConstruction) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);
					decimal avgSquareFeet = (legacyEstimationProject.TotalSquareFootageCnt.Value + flattenedSquareFootage) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);
					decimal avgSheetsCount = (legacyEstimationProject.TotalSheetsCnt.Value + flattenedSheetsCount) / (legacyEstimationProject.TotalProjectsCnt.Value + flattenedTotalProjects);

					//calculate new averages by occ type and construction type
					AveragedData averageHours = new AveragedData()
                    {
                        OccupancyTypeRefId = legacyEstimationProject.OccupancyTypRefId.Value,
                        ConstructionType = legacyEstimationProject.ConstrTypTxt,
                        AvgBuildingHours = RoundDecimal(avgBuilding, 1),
                        AvgElectricHours = RoundDecimal(avgElectrical, 1),
                        AvgMechanicalHours = RoundDecimal(avgMechanical, 1),
                        AvgPlumbingHours = RoundDecimal(avgPlumbing, 1),
                        AvgCostOfConstruction = RoundDecimal(avgCostOfConstruction, 1),
						AvgSheets = RoundDecimal(avgSheetsCount, 1),
                        AvgSquareFeet = RoundDecimal(avgSquareFeet, 1)
					};

                    averageHoursCollection.Add(averageHours);
                }

                return averageHoursCollection;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter GetLegacyAveragedData - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
        }

        public List<AveragedData> GetAveragedData()
        {
            try
            {
                List<AveragedData> averageHoursCollection = new List<AveragedData>();

                List<MMFHoursGroupingFlattened> flattenedProjects = GetMMFProjectsCompletedSummary();

                foreach (MMFHoursGroupingFlattened flattenedProject in flattenedProjects)
                {
                    decimal avgBuilding = flattenedProject.BuildingHours / flattenedProject.TotalProjects;
                    decimal avgElectrical = flattenedProject.ElectricalHours / flattenedProject.TotalProjects;
                    decimal avgMechanical = flattenedProject.MechanicalHours / flattenedProject.TotalProjects;
                    decimal avgPlumbing = flattenedProject.PlumbingHours / flattenedProject.TotalProjects;

					decimal avgCostOfConstruction = flattenedProject.CostOfConstruction / flattenedProject.TotalProjects;
					decimal avgSquareFeet = flattenedProject.SquareFootage / flattenedProject.TotalProjects;
					decimal avgSheetsCount = flattenedProject.SheetsCount / flattenedProject.TotalProjects;

					//calculate new averages by occ type and construction type
					AveragedData averageHours = new AveragedData()
                    {
                        OccupancyTypeRefId = flattenedProject.OccupancyTypeId,
                        ConstructionType = flattenedProject.ConstructionTypeShortDesc,
                        AvgBuildingHours = RoundDecimal(avgBuilding, 1),
                        AvgElectricHours = RoundDecimal(avgElectrical, 1),
                        AvgMechanicalHours = RoundDecimal(avgMechanical, 1),
                        AvgPlumbingHours = RoundDecimal(avgPlumbing, 1),
                        AvgCostOfConstruction = RoundDecimal(avgCostOfConstruction, 1),
						AvgSheets = RoundDecimal(avgSheetsCount, 1),
                        AvgSquareFeet = RoundDecimal(avgSquareFeet, 1)
					};

                    averageHoursCollection.Add(averageHours);
                }

                return averageHoursCollection;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter GetAveragedData - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
        }

        public List<LegacyProjectEstimationHoursRefBE> GetLegacyEstimationProjects()
        {
            return _dataContextLegacyProjectData.GetList();
        }

        public List<Factor> GetFactors(List<AveragedData> averagedData, AutoEstimationRefBE config)
        {
            try
            {
                List<Factor> factors = new List<Factor>();

                foreach (AveragedData average in averagedData)
                {
                    decimal bldgCOC = (average.AvgCostOfConstruction > 0) ? (average.AvgBuildingHours / average.AvgCostOfConstruction) * config.WeightCocNbr.Value : 0;
                    decimal elecCOC = (average.AvgCostOfConstruction > 0) ? (average.AvgElectricHours / average.AvgCostOfConstruction) * config.WeightCocNbr.Value : 0;
                    decimal mechCOC = (average.AvgCostOfConstruction > 0) ? (average.AvgMechanicalHours / average.AvgCostOfConstruction) * config.WeightCocNbr.Value : 0;
                    decimal plmgCOC = (average.AvgCostOfConstruction > 0) ? (average.AvgPlumbingHours / average.AvgCostOfConstruction) * config.WeightCocNbr.Value : 0;

                    decimal bldgSheets = (average.AvgSheets > 0) ? (average.AvgBuildingHours / average.AvgSheets) * config.WeightSheetsNbr.Value : 0;
                    decimal elecSheets = (average.AvgSheets > 0) ? (average.AvgElectricHours / average.AvgSheets) * config.WeightSheetsNbr.Value : 0;
                    decimal mechSheets = (average.AvgSheets > 0) ? (average.AvgMechanicalHours / average.AvgSheets) * config.WeightSheetsNbr.Value : 0;
                    decimal plmgSheets = (average.AvgSheets > 0) ? (average.AvgPlumbingHours / average.AvgSheets) * config.WeightSheetsNbr.Value : 0;

                    decimal bldgSqFeet = (average.AvgSquareFeet > 0) ? (average.AvgBuildingHours / average.AvgSquareFeet) * config.WeightSqftNbr.Value : 0;
                    decimal elecSqFeet = (average.AvgSquareFeet > 0) ? (average.AvgElectricHours / average.AvgSquareFeet) * config.WeightSqftNbr.Value : 0;
                    decimal mechSqFeet = (average.AvgSquareFeet > 0) ? (average.AvgMechanicalHours / average.AvgSquareFeet) * config.WeightSqftNbr.Value : 0;
                    decimal plmgSqFeet = (average.AvgSquareFeet > 0) ? (average.AvgPlumbingHours / average.AvgSquareFeet) * config.WeightSqftNbr.Value : 0;

                    factors.Add(new Factor
                    {
                        OccupancyType = average.OccupancyType,
                        OccupancyTypeRefId = average.OccupancyTypeRefId,
                        ConstructionType = average.ConstructionType,
                        BldgCOC = RoundDecimal(bldgCOC, 7),
                        ElecCOC = RoundDecimal(elecCOC, 7),
                        MechCOC = RoundDecimal(mechCOC, 7),
						PlmgCOC = RoundDecimal(plmgCOC, 7),
                        BldgSheets = RoundDecimal(bldgSheets, 7),
                        ElecSheets = RoundDecimal(elecSheets, 7),
                        MechSheets = RoundDecimal(mechSheets, 7),
                        PlmgSheets = RoundDecimal(plmgSheets, 7),
                        BldgSqFeet = RoundDecimal(bldgSqFeet, 7),
                        ElecSqFeet = RoundDecimal(elecSqFeet, 7),
                        MechSqFeet = RoundDecimal(mechSqFeet, 7),
                        PlmgSqFeet = RoundDecimal(plmgSqFeet, 7)
					});
                }

                return factors;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter GetFactors - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
        }

        public bool SaveFactors(List<Factor> factorData)
        {
            bool success = false;
            try
            {
                foreach (Factor factor in factorData)
                {
                    //deactivate previous row
                    int rows = _dataContextAverageEstimationHoursFactor.SetRowActive(factor.OccupancyTypeRefId, factor.ConstructionType, false, "1");

                    AverageEstimationHoursFactorBE factorBE = new AverageEstimationHoursFactorBE
                    {
                        ConstructionType = factor.ConstructionType,
                        OccupancyTypRefId = factor.OccupancyTypeRefId,
                        UserId = "1",
                        ActiveDate = DateTime.Now,
                        ActiveInd = true,
                        BuildingCocFactor = factor.BldgCOC,
                        BuildingSheetsFactor = factor.BldgSheets,
                        BuildingSqftFactor = factor.BldgSqFeet,
                        ElectricalCocFactor = factor.ElecCOC,
                        ElectricalSheetsFactor = factor.ElecSheets,
                        ElectricalSqftFactor = factor.ElecSqFeet,
                        MechanicalCocFactor = factor.MechCOC,
                        MechanicalSheetsFactor = factor.MechSheets,
                        MechanicalSqftFactor = factor.MechSqFeet,
                        PlumbingCocFactor = factor.PlmgCOC,
                        PlumbingSheetsFactor = factor.PlmgSheets,
                        PlumbingSqftFactor = factor.PlmgSqFeet
                    };

                    //save the row
                    int rownum = _dataContextAverageEstimationHoursFactor.Create(factorBE);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter SaveFactors - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
            return success;
        }

        public bool DetermineIfUsingLegacyData(DateTime goLiveDate)
        {
            bool useLegacyData = false;

            DateTime twoYearsAfterGoLive = goLiveDate.AddYears(2);
            if (DateTime.Now.Date < twoYearsAfterGoLive.Date)
            {
                useLegacyData = true;
            }

            return useLegacyData;
        }

        public List<MMFHoursGroupingFlattened> GetMMFProjectsCompletedSummary()
        {
            try
            {
                _MMFCompletedProjects = GetTotalCompletedMMFProjects();

                List<MMFHoursGroupingFlattened> flattenedProjects = GroupMMFCompletedProjects(_MMFCompletedProjects);

                List<int> bempTrades = new List<int> { 1, 2, 3, 4 };

                foreach (ProjectBE project in _MMFCompletedProjects)
                {
                    // find occ type and constr type 
                    // find associated flattened record
                    // get hours by business ref and update hours on flattened record

                    int projectOccupancyTypeId = _OccupancyTypes.FirstOrDefault(x => x.OccupancyTypName == project.ProjectOccupancyTypMapNm).OccupancyTypRefId.Value;

                    MMFHoursGroupingFlattened flattened = flattenedProjects.FirstOrDefault(x => x.OccupancyTypeId == projectOccupancyTypeId);

                    List<ProjectBusinessRelationshipBE> projectBusinessRelationships = _dataContextProjectBusinessRelationship.GetListByProjectId(project.ProjectId.Value);

                    foreach (ProjectBusinessRelationshipBE be in projectBusinessRelationships.Where(x => bempTrades.Contains(x.BusinessRefId.Value)))
                    {
                        switch (be.BusinessRefId)
                        {
                            case 1:
                                flattened.BuildingHours += be.ActualHoursNbr.Value;
                                break;
                            case 2:
                                flattened.ElectricalHours += be.ActualHoursNbr.Value;
                                break;
                            case 3:
                                flattened.MechanicalHours += be.ActualHoursNbr.Value;
                                break;
                            case 4:
                                flattened.PlumbingHours += be.ActualHoursNbr.Value;
                                break;
                        }
                    }
                }

                return flattenedProjects;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CalcAvgEstimationFactorAdapter GetMMFProjectsCompletedSummary - " + ex.Message;

                BLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);
                throw ex;
            }
        }

        public List<MMFHoursGroupingFlattened> GroupMMFCompletedProjects(List<ProjectBE> completedProjects)
        {
            return completedProjects.GroupBy(g => new { g.ProjectOccupancyTypMapNm, g.WorkTypDesc })
                .Select(g => new MMFHoursGroupingFlattened
                {
                    OccupancyTypeId = _OccupancyTypes.FirstOrDefault(x => x.OccupancyTypName == g.Key.ProjectOccupancyTypMapNm).OccupancyTypRefId.Value,
                    ConstructionTypeShortDesc = CreateConstructionTypeShortDesc(g.Key.WorkTypDesc),
                    CostOfConstruction = g.Sum(x => x.ConstrCostAmt.Value),
                    SheetsCount = g.Sum(x => int.Parse(x.SheetsCntDesc)),
                    SquareFootage = g.Sum(x => x.SquareFootageToBeReviewedNbr.Value),
                    TotalProjects = g.Count()
                }).ToList();
        }

        public string CreateConstructionTypeShortDesc(string constructionType)
        {
            return constructionType.ToUpper().Contains("UPFIT") ? "UPFITRTAP" : "NEWCONSTRUCTION";
        }

        public List<ProjectBE> GetTotalCompletedMMFProjects()
        {
            DateTime startDate = DetermineStartDate(_GoLiveDate);

			DateTime endDate = DateTime.Now.AddDays(-1).Date;

            return _dataContextProjectBO.GetMMFProjectsComplete(startDate, endDate);
        }

        public DateTime DetermineStartDate(DateTime dateTime)
        {
            UseLegacyData = DetermineIfUsingLegacyData(dateTime);
            
			DateTime startDate = UseLegacyData ? dateTime.Date: DateTime.Now.AddDays(-1).AddMonths(-24).Date;

            return startDate;
		}

        private decimal RoundDecimal(decimal amountToRound, int decimalPlaces)
        {
			return Math.Round(amountToRound, decimalPlaces, MidpointRounding.AwayFromZero);
		}
    }
}