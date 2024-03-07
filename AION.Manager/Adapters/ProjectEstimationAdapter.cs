using AION.BL.Controller;
using AION.BL.Helpers;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.BL.Adapters
{

	/// <summary>
	/// Performs the actual businesslogic for Estimation. This class contains all the actual auto estimation 
	/// and setting of default hours to apropriate project type and departments.
	/// </summary>
	public class ProjectEstimationAdapter : BaseManagerAdapter, IProjectEstimationAdapter
    {
        private IProjectEstimationAdapter _projectAdapter;
        public ProjectEstimationAdapter()
        {

        }

        public EstimationModel GetEstimationModel(ProjectParms projectParms)
        {
            try
            {
                EstimationModel model = new EstimationModel();

                IUserAdapter userAdapter = new UserAdapter();
                IPreliminaryProjectEstimationEngine preliminaryProjectEstimationEngine = new PreliminaryProjectEstimationEngine();
                IProjectAutoEstimationEngine projectAutoEstimationEngine = new ProjectAutoEstimationEngine();
                IEstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                NoteAdapter noteAdapter = new NoteAdapter();

                model.Facilitators = userAdapter.GetAllFacilitators();

                model.ProjectEstimation = estimationCRUDAdapter.GetProjectDetailsByProjectSrcSourceTxt(projectParms.ProjectId);

                //model.ProjectEstimation = 
                //    projectParms.IsPreliminary ? preliminaryProjectEstimationEngine.ExecutePreliminaryProjectEstimation(projectParms) 
                //    : projectAutoEstimationEngine.ExecuteProjectEstimation(projectParms, false);

                model.EstimatorUIModels = userAdapter.GetAllEstimators();
                model.PermissionMappingCatalogItems = estimationCRUDAdapter.GetAllPendingReasons();

                InternalNoteManagerModel internalNoteManagerModel = new InternalNoteManagerModel
                {
                    ProjectId = model.ProjectEstimation.ID,
                };

                model.Notes = noteAdapter.GetAllInternalNotes(internalNoteManagerModel);
                model.FacilitatorWorkloadSummary = estimationCRUDAdapter.GetFacilitatorWorkloadSummary(DateTime.Now.AddDays(-30), DateTime.Now);
                model.StandardNotes = noteAdapter.GetStandardNotes(NoteTypeEnum.EstimationStandardNotes, PropertyTypeEnums.NA);
                model.StandardNoteGroupEnums = noteAdapter.GetStandardNoteGroupEnums();

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetEstimationModel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public BulkEstimationModel GetBulkEstimationModel()
        {
            try
            {
                BulkEstimationModel model = new BulkEstimationModel();

                IUserAdapter userAdapter = new UserAdapter();

                model.Facilitators = userAdapter.GetAllFacilitators();
                model.Reviewers = userAdapter.GetReviewers((int)PropertyTypeEnums.NA, (int)DepartmentNameEnums.NA, false);
                model.EstimatorUIModels = userAdapter.GetAllEstimators();

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetBulkEstimationModel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public ProjectEstimationAdapter(IProjectEstimationAdapter projectAdapter)
        {
            _projectAdapter = projectAdapter;
        }

        /// <summary>
        /// This performs the autocalculation and returns the project
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ProjectEstimation model/ bool status</returns>
        public bool PerformAutoEstimation(ProjectEstimation model)
        {
            if (model.AccelaProjectRefId == null) return false;

            //skip autos incase of preliminary projects. Its not needed.
            if (model.IsProjectPreliminary == false)
            {
                //assign the default values to respective departments from database
                if (SetDefaultHours(model) == false) return false;

                // Auto estimation calculations and assign default values to respective departments.
                if (model.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family)
                {
                    AutoCalcEstimation(model);
                }

            }
            return true;
        }

        /// <summary>
        /// sets default estimation hours for project trades and agencies
        /// </summary>
        /// <returns>bool</returns>
        private bool SetDefaultHours(ProjectEstimation aionProj)
        {
            DefaultEstimationHour hour;
            try
            {
                foreach (var item in aionProj.Agencies)
                {
                    item.UpdatedUser = aionProj.UpdatedUser;
                    DepartmentNameEnums deptname = item.DepartmentInfo;
                    PropertyTypeEnums projtype = aionProj.AccelaPropertyType;
                    hour = new DefaultEstimationHourModelBO().GetInstance(deptname, projtype);
                    AssignDefaultHoursandNA(item, hour);
                }

                foreach (var item in aionProj.Trades)
                {
                    item.UpdatedUser = aionProj.UpdatedUser;
                    DepartmentNameEnums deptname = item.DepartmentInfo;
                    PropertyTypeEnums projtype = aionProj.AccelaPropertyType;
                    hour = new DefaultEstimationHourModelBO().GetInstance(deptname, projtype);
                    AssignDefaultHoursandNA(item, hour);
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SendingEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw;
            }
        }

        ProjectDepartment AssignDefaultHoursandNA(ProjectDepartment ret, DefaultEstimationHour hr)
        {
            if (hr == null)
            {
                ret.EstimationHours = 0;
                ret.EstimationNotApplicable = true;
                ret.DepartmentStatus = ProjectDisplayStatus.AutoEstimationCompleteNA;
            }
            else
            {
                if (hr.EstimationHoursMode == TradeSelectOptionConsts.Town)
                {
                    ret.EstimationHours = hr.DefaultHours;
                    ret.EstimationNotApplicable = false;
                    ret.DepartmentStatus = ProjectDisplayStatus.Default;
                }
                else if (hr.EstimationHoursMode == TradeSelectOptionConsts.Default)
                {
                    ret.EstimationHours = hr.DefaultHours;
                    ret.EstimationNotApplicable = false;
                    ret.DepartmentStatus = ProjectDisplayStatus.Default;
                }
                else if (hr.EstimationHoursMode == TradeSelectOptionConsts.NA)
                {
                    ret.EstimationHours = 0;
                    ret.EstimationNotApplicable = true;
                    ret.DepartmentStatus = ProjectDisplayStatus.AutoEstimationCompleteNA;
                }
                else if (hr.EstimationHoursMode == TradeSelectOptionConsts.Auto)
                {
                    //this value will be set in AutoCalcEstimation(ProjectEstimation aionProj)
                    ret.EstimationHours = 0;
                    ret.EstimationNotApplicable = false;
                }
                else if (hr.EstimationHoursMode == TradeSelectOptionConsts.Manual)
                {
                    ret.EstimationHours = 0;
                    ret.EstimationNotApplicable = false;
                }
                else if (hr.EstimationHoursMode == TradeSelectOptionConsts.County)
                {
                    ret.EstimationHours = hr.DefaultHours;
                    ret.EstimationNotApplicable = false;
                    ret.DepartmentStatus = ProjectDisplayStatus.Default;
                }
            }
            return ret;
        }

        public bool CheckIfAllDepartmentsComplete(int projectId)
        {
            bool isComplete = true;
            bool allMarkedAsNA = true;

            List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEs = new List<ProjectBusinessRelationshipBE>();
            ProjectBusinessRelationshipBO projectBusinessRelationshipBO = new ProjectBusinessRelationshipBO();

            projectBusinessRelationshipBEs = projectBusinessRelationshipBO.GetListByProjectId(projectId);

            foreach (var item in projectBusinessRelationshipBEs)
            {
                if (item.IsEstimationNotApplicable == false)
                {
                    allMarkedAsNA = false;
                }

                if (item.ProjectBusinessRelationshipStatusDesc != ProjectDisplayStatus.AutoEstimationComplete
                    && item.ProjectBusinessRelationshipStatusDesc != ProjectDisplayStatus.AutoEstimationCompleteNA
                    && item.ProjectBusinessRelationshipStatusDesc != ProjectDisplayStatus.Complete)
                {
                    isComplete = false;
                }
            }

            if (allMarkedAsNA)
            {
                isComplete = false;
            }

            return isComplete;
        }

        private bool AutoCalcEstimation(ProjectEstimation aionProj)
        {
            try
            {
                bool success = false;
                //send in project, set estimation hours for all trades if MMF
                if (aionProj.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family)
                {
                    //trades list

					success = EstimatorTool(aionProj.Trades,
                        aionProj.AccelaCostOfConstruction, aionProj.AccelaNumberofSheets, aionProj.AccelaSqrFtToBeReviewed, aionProj.ProjectOccupancyTypMapNm, aionProj.DisplayOnlyInformation.TypeOfWork);
                }
                return success;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SendingEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool EstimatorTool(List<ProjectTrade> trades, double coc, int sheets, int sqFt, string occupancyType, string typeOfWork)
        {
            if (string.IsNullOrWhiteSpace(occupancyType) || string.IsNullOrWhiteSpace(typeOfWork))
            {
                return false;
            }

            //get the factors
            AverageEstimationHoursFactorBE actual = GetAverageEstimationHoursFactors(occupancyType, typeOfWork);

            if (actual.AverageEstimationHoursFactorId == null)
            {
                return true;  // unsure since not really an error, just a non-existent combination
            }

            decimal cocFactor = 0;
            decimal sqftFactor = 0;
            decimal sheetsFactor = 0;

            bool isSum = IsSum(occupancyType);

            DefaultEstimationHourModelBO hrs = new DefaultEstimationHourModelBO();
            
            foreach (var item in trades)
            {
                //this method will be called only on PropertyTypeEnums.Mega_Multi_Family. So manually assigns it here.
                DefaultEstimationHour hr = hrs.GetInstance(item.DepartmentInfo, PropertyTypeEnums.Mega_Multi_Family);
                //if this mode is set to Auto (only valid for MMF and BEMP), then get the auto estimated values
                if (hr.EstimationHoursMode == TradeSelectOptionConsts.Auto)
                {
                    switch (item.DepartmentInfo)
                    {
                        case DepartmentNameEnums.Building:
                            cocFactor = (decimal)actual.BuildingCocFactor;
                            sqftFactor = (decimal)actual.BuildingSqftFactor;
                            sheetsFactor = (decimal)actual.BuildingSheetsFactor;
                            break;
                        case DepartmentNameEnums.Electrical:
                            cocFactor = (decimal)actual.ElectricalCocFactor;
                            sqftFactor = (decimal)actual.ElectricalSqftFactor;
                            sheetsFactor = (decimal)actual.ElectricalSheetsFactor;
                            break;
                        case DepartmentNameEnums.Mechanical:
                            cocFactor = (decimal)actual.MechanicalCocFactor;
                            sqftFactor = (decimal)actual.MechanicalSqftFactor;
                            sheetsFactor = (decimal)actual.MechanicalSheetsFactor;
                            break;
                        case DepartmentNameEnums.Plumbing:
                            cocFactor = (decimal)actual.PlumbingCocFactor;
                            sqftFactor = (decimal)actual.PlumbingSqftFactor;
                            sheetsFactor = (decimal)actual.PlumbingSheetsFactor;
                            break;
                    }

                    List<decimal> factors = new List<decimal>();
                    factors.Add(cocFactor * (decimal)coc);
                    factors.Add(sheetsFactor * sheets);
                    factors.Add(sqftFactor * sqFt);

                    if (isSum)
                    {
                        item.EstimationHours = Math.Round(factors.Sum(), 1, MidpointRounding.ToEven);
					}
                    else
                    {
                        item.EstimationHours = Math.Round(factors.Average(), 1, MidpointRounding.ToEven);
					}

                    item.EstimationHours = item.EstimationHours.AdjustToHalfHour();

                    //jcl LES-4710 - set auto calculated to Default
                    item.DepartmentStatus = ProjectDisplayStatus.Default;
                }
            }
            return true;
        }

        public AverageEstimationHoursFactorBE GetAverageEstimationHoursFactors(string occupancyType, string typeOfWork)
        {
            //get the correct type of work string
            AccelaMappingHelper accelaMappingHelper = new AccelaMappingHelper();
            string typeOfWorkEnumVal = accelaMappingHelper.GetTypeOfWorkValue(typeOfWork);
            //search for correct values
            AverageEstimationHoursFactorBO factorBO = new AverageEstimationHoursFactorBO();

            AverageEstimationHoursFactorBE actual = factorBO.GetByOccupancyTypConstructionTyp(occupancyType, typeOfWorkEnumVal);
            return actual;
        }

        public ProjectRtapMapping GetProjectRtapMapping(int projectId)
        {
            ProjectRtapMappingBO projectRtapMappingBO = new ProjectRtapMappingBO();
            ProjectRtapMappingBE projectRtapMappingBE = projectRtapMappingBO.GetByProjectId(projectId);

            if (projectRtapMappingBE != null)
            {
                ProjectRtapMapping projectRtapMapping = new ProjectRtapMapping()
                {
                    ProjectRtapMappingId = projectRtapMappingBE.ProjectRtapMappingId,
                    ProjectId = projectRtapMappingBE.ProjectId,
                    OriginalProjectId = projectRtapMappingBE.OriginalProjectId,
                    OriginalProjectNumber = projectRtapMappingBE.OriginalProjectNumber
                };
                return projectRtapMapping;
            }
            return null;
        }

        public int GetAssignedFacilitator(int projectId)
        {
            ProjectBO projectBO = new ProjectBO();
            ProjectBE projectBE = projectBO.GetById(projectId);

            if (projectBE.AssignedFacilitatorId != null)
            {
                return (int)projectBE.AssignedFacilitatorId;
            }
            return 0;
        }

        public int GetAIONProjectId(string accelaProjectId)
        {
            ProjectBO projectBO = new ProjectBO();
            ProjectBE projectBE = projectBO.GetByExternalRefInfo(accelaProjectId);
            if (projectBE != null)
            {
                return projectBE.ProjectId.Value;
            }
            return 0;
        }

        /// <summary>
        /// if Residential, sum otherwise average
        /// </summary>
        /// <param name="occupancyType"></param>
        /// <returns></returns>
        private bool IsSum(string occupancyType)
        {
            //get the data and find out what occ type
            OccupancyTypeRefBE refBE = GetOccupancyTypeReference(occupancyType);
            if (String.Equals(refBE.OccupancyTypName, "residential", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets base estimation hours from db 
        /// </summary>
        /// <param name="deptname"></param>
        /// <param name="projtype"></param>
        /// <returns>decimal?</returns>
        private decimal? GetEstHours(DepartmentNameEnums deptname, PropertyTypeEnums projtype)
        {
            //new DefaultEstimationHourModelBO().BaseList.GetDefaultEstimationHour(deptname, projtype).DefaultHours;
            //get the hours for the deptname and projtype ("building","townhomes")
            DefaultEstimationHour hour = new DefaultEstimationHourModelBO().GetInstance(deptname, projtype);
            if (hour == null)
            {
                return 0;
            }
            else
            {
                return hour.DefaultHours;
            }
        }

        private OccupancyTypeRefBE GetOccupancyTypeReference(string occupancyType)
        {
            var refBO = new OccupancyTypeRefBO();
            return refBO.GetByProjectOccupancyTyp(occupancyType);
        }
	}
}