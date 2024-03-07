using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Common;
using AION.Manager.Engines;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.BusinessObjects
{
    public class SchedulingLeadTimeReportBO : BaseSchedulingEngine
    {
        /// <summary>
        /// List of PropertyTypeEnums
        /// </summary>
        List<PropertyTypeEnums> PropertyTypes { get; set; } = Enum<PropertyTypeEnums>.ToList();
        /// <summary>
        /// List of ProjectLevel enums
        /// </summary>
        List<ProjectLevel> ProjectLevels { get; set; } = Enum<ProjectLevel>.ToList();
        /// <summary>
        /// The start date for the scheduling engine
        /// </summary>
        DateTime AutoSchedulePeriodStart { get; set; } = DateTime.Now.AddDays(1);
        /// <summary>
        /// The end date for the scheduling engine
        /// </summary>
        DateTime AllowedMaxEndDate { get; set; } = DateTime.Now.AddDays(1).AddYears(2);
        /// <summary>
        /// Number of hours enumerated for the scheduling engine
        /// </summary>
        public int ReviewHours { get; set; }

        public string ProjectLevelTxt { get; set; }

        public List<ProjectEstimation> _projects = new List<ProjectEstimation>();

        List<TimeSlot> _timeSlots = new List<TimeSlot>();

        public List<BusinessDivisionXRefBE> BusinessDivisions = new List<BusinessDivisionXRefBE>();

        public List<SchedulingLeadTimeReport> SchedulingLeadTimeReportList = new List<SchedulingLeadTimeReport>();

        /// <summary>
        /// Build the projects list for the report
        /// These projecst will be enumerated for the scheduling engine
        /// </summary>
        /// <returns></returns>
        public bool GetReportProjects()
        {
            //get all except NA, Residential (which is a preliminary meeting)
            //LES-303 changes 6/9/2022 jcl - add levels
            /*Select level of project to generate the report:  Level 1, level 2 or level 3.  Required selection. 

            Based on level selection the drop for project types will be limited.  
            Townhomes, FIFO: Single Family Homes and FIFO: Master Plans and FIFO: Addition/Renovation Single Family Home are always level 1 work.  They will only display when level 1 is selected.  

            County Shop drawings - always level 3 work.  This will only display when level 3 is selected.  */

            foreach (PropertyTypeEnums propertyType in PropertyTypes.Where(x => (int)x > 0 && x != PropertyTypeEnums.Residential).ToList())
            {
                switch (propertyType)
                {
                    case PropertyTypeEnums.Express:
                    case PropertyTypeEnums.Commercial:
                    case PropertyTypeEnums.Mega_Multi_Family:
                    case PropertyTypeEnums.Special_Projects_Team:
                    case PropertyTypeEnums.FIFO_Small_Commercial:
                        //all levels
                        foreach (ProjectLevel projectLevel in ProjectLevels)
                        {
                            List<ProjectEstimation> projectsalllevels2 = new List<ProjectEstimation>();
                            List<ProjectEstimation> projectsalllevels4 = new List<ProjectEstimation>();
                            List<ProjectEstimation> projectsalllevels8 = new List<ProjectEstimation>();

                            projectsalllevels2.Add(CreatePE(propertyType, 2, projectLevel.ToString()));

                            projectsalllevels4.Add(CreatePE(propertyType, 4, projectLevel.ToString()));

                            projectsalllevels8.Add(CreatePE(propertyType, 8, projectLevel.ToString()));

                            _projects.AddRange(projectsalllevels2.ToList());
                            _projects.AddRange(projectsalllevels4.ToList());
                            _projects.AddRange(projectsalllevels8.ToList());

                        }

                        break;
                    case PropertyTypeEnums.Townhomes:
                    case PropertyTypeEnums.FIFO_Single_Family_Homes:
                    case PropertyTypeEnums.FIFO_Master_Plans:
                    case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                        //only level 1
                        List<ProjectEstimation> projectslevel12 = new List<ProjectEstimation>();
                        List<ProjectEstimation> projectslevel14 = new List<ProjectEstimation>();
                        List<ProjectEstimation> projectslevel18 = new List<ProjectEstimation>();

                        projectslevel12.Add(CreatePE(propertyType, 2, ProjectLevel.Level1.ToString()));

                        projectslevel14.Add(CreatePE(propertyType, 4, ProjectLevel.Level1.ToString()));

                        projectslevel18.Add(CreatePE(propertyType, 8, ProjectLevel.Level1.ToString()));

                        _projects.AddRange(projectslevel12.ToList());
                        _projects.AddRange(projectslevel14.ToList());
                        _projects.AddRange(projectslevel18.ToList());
                        break;
                    case PropertyTypeEnums.County_Fire_Shop_Drawings:
                        //only level 3
                        List<ProjectEstimation> projects2 = new List<ProjectEstimation>();
                        List<ProjectEstimation> projects4 = new List<ProjectEstimation>();
                        List<ProjectEstimation> projects8 = new List<ProjectEstimation>();

                        projects2.Add(CreatePE(propertyType, 2, ProjectLevel.Level3.ToString()));

                        projects4.Add(CreatePE(propertyType, 4, ProjectLevel.Level3.ToString()));

                        projects8.Add(CreatePE(propertyType, 8, ProjectLevel.Level3.ToString()));

                        _projects.AddRange(projects2.ToList());
                        _projects.AddRange(projects4.ToList());
                        _projects.AddRange(projects8.ToList());

                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Create the project 
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="projectHours"></param>
        /// <param name="projectLvlTxt"></param>
        /// <returns></returns>
        private ProjectEstimation CreatePE(PropertyTypeEnums propertyType, int projectHours, string projectLvlTxt)
        {
            ProjectEstimation project = new ProjectEstimation();

            //    Building
            ProjectTrade b = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Building,
                DepartmentDivision = DepartmentDivisionEnum.Building,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Electrical
            ProjectTrade e = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Electrical,
                DepartmentDivision = DepartmentDivisionEnum.Electrical,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Mechanical
            ProjectTrade m = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Mechanical,
                DepartmentDivision = DepartmentDivisionEnum.Mechanical,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Plumbing
            ProjectTrade p = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Plumbing,
                DepartmentDivision = DepartmentDivisionEnum.Plumbing,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Zoning County
            ProjectAgency z_county = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_County,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Zoning City Of Charlotte
            ProjectAgency z_charlotte = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Cty_Chrlt,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Zoning Mint Hill
            ProjectAgency z_minthill = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Mint_Hill,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Zoning Huntersville
            ProjectAgency z_hville = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Huntersville,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Fire City Of Charlotte
            ProjectAgency f_charlotte = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Fire_Cty_Chrlt,
                DepartmentDivision = DepartmentDivisionEnum.Fire,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Fire County
            ProjectAgency f_county = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Fire_County,
                DepartmentDivision = DepartmentDivisionEnum.Fire,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Backflow
            ProjectAgency backflow = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Backflow,
                DepartmentDivision = DepartmentDivisionEnum.Backflow,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Food Service
            ProjectAgency ehsfood = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Food,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Public Pool
            ProjectAgency ehspool = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Pool,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Facility Lodging
            ProjectAgency ehsfacility = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Facilities,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Day Care
            ProjectAgency ehsdaycare = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Day_Care,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            List<ProjectTrade> trades = new List<ProjectTrade>();
            trades.Add(b);
            trades.Add(e);
            trades.Add(m);
            trades.Add(p);

            List<ProjectAgency> agencies = new List<ProjectAgency>();
            agencies.Add(backflow);
            agencies.Add(ehsdaycare);
            agencies.Add(ehspool);
            agencies.Add(ehsfacility);
            agencies.Add(ehsfood);

            agencies.Add(z_charlotte);

            agencies.Add(z_county);

            agencies.Add(z_hville);

            agencies.Add(z_minthill);

            agencies.Add(f_charlotte);

            agencies.Add(f_county);

            project.Trades = trades;
            project.Agencies = agencies;
            project.AccelaPropertyType = propertyType;
            project.AionPropertyType = propertyType;
            project.ProjectLvlTxt = projectLvlTxt;

            return project;
        }
        /// <summary>
        /// Generates the data for the Scheduling Lead Time table
        /// Uses the scheduling engines for different project types
        /// Converts the time slots returned to report format
        /// Used in Reporting Main for Generate Data button
        /// </summary>
        /// <returns></returns>
        public bool GenerateSchedulingLeadTimeData()
        {
            try
            {
                BusinessDivisions = new BusinessDivisionRefBO().GetXRefList();
                SchedulingLeadTimeReportList = new List<SchedulingLeadTimeReport>();

                if (HolidayList == null)
                    HolidayList = GetHolidays();
                TimeSlotIntervalByMinutes = 15;

                //get next business day after today
                AutoSchedulePeriodStart = NextWorkingDay(DateTime.Now);

                GetReportProjects();

                int debugcountprojects = 0;

                foreach (ProjectEstimation project in _projects.ToList())
                {
                    //some methods need properties set on CurrentProject
                    CurrentProject = new ProjectEstimation();
                    CurrentProject = project;

                    //get the data for each review hrs, all are set the same for each trade/agency so just get the first
                    //also used to convert the time slot
                    ReviewHours = (int)CurrentProject.Trades.First().EstimationHours.Value;

                    //get the project level so we can use this to convert the time slot
                    //get the correct text so the report has readable text
                    ProjectLevel projectLeveltxt = (ProjectLevel)Enum.Parse(typeof(ProjectLevel), CurrentProject.ProjectLvlTxt);
                    ProjectLevelTxt = projectLeveltxt.ToStringValue();

                    switch (CurrentProject.AccelaPropertyType)
                    {
                        case PropertyTypeEnums.Express:
                            //45 rows = 3 projects, 15 trade agencies
                            _timeSlots = GetAutoScheduledDataExpress(new AutoScheduleReportParams
                            {
                                CurrentProject = CurrentProject,
                                ManualStartDateTime = AutoSchedulePeriodStart,
                                ReviewHours = ReviewHours
                            });

                            ConvertTimeSlot(_timeSlots, null);
                            debugcountprojects++;
                            break;
                        case PropertyTypeEnums.Commercial:
                        case PropertyTypeEnums.Mega_Multi_Family:
                        case PropertyTypeEnums.Special_Projects_Team:
                        case PropertyTypeEnums.Townhomes:
                        case PropertyTypeEnums.County_Fire_Shop_Drawings:
                            //225 rows = 15 projects, 15 trade agencies
                            _timeSlots = GetAutoScheduledDataPlanReview(new AutoScheduleReportParams
                            {
                                CurrentProject = CurrentProject,
                                ManualStartDateTime = AutoSchedulePeriodStart,
                                ManualEndDateTime = AllowedMaxEndDate,
                                ReviewHours = ReviewHours
                            });

                            ConvertTimeSlot(_timeSlots, null);
                            debugcountprojects++;

                            break;

                        case PropertyTypeEnums.FIFO_Small_Commercial:
                        case PropertyTypeEnums.FIFO_Single_Family_Homes:
                        case PropertyTypeEnums.FIFO_Master_Plans:
                        case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                            //180 rows = 12 projects, 15 trade agencies
                            _timeSlots = GetAutoScheduledDataFIFO(new AutoScheduleReportParams
                            {
                                CurrentProject = CurrentProject,
                                ManualStartDateTime = AutoSchedulePeriodStart,
                                ManualEndDateTime = AllowedMaxEndDate,
                                ReviewHours = ReviewHours
                            });

                            ConvertTimeSlot(_timeSlots, null);
                            debugcountprojects++;
                            break;
                        default:
                            break;
                    }
                }

                return InsertRowsForReport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TimeSlot> GetAutoScheduledDataExpress(AutoScheduleReportParams data)
        {
            ExpressProjectSchedulingEngine expressProjectSchedulingEngine = new ExpressProjectSchedulingEngine(data);
            return expressProjectSchedulingEngine.BusinessTimeSlots;
        }
        public List<TimeSlot> GetAutoScheduledDataFIFO(AutoScheduleReportParams data)
        {

            FIFOSchedulingEngine thisengine = new FIFOSchedulingEngine(data);
            return thisengine.BusinessTimeSlots;
        }
        public List<TimeSlot> GetAutoScheduledDataPlanReview(AutoScheduleReportParams data)
        {
            PlanReviewProjectSchedulingEngine thisengine = new PlanReviewProjectSchedulingEngine(data);
            return thisengine.GetPlanReviewAutoEstimatedValuesForReport();
        }
        /// <summary>
        /// Uses the BO to delete the current data in the report table, then insert the new data
        /// </summary>
        /// <returns></returns>
        private bool InsertRowsForReport()
        {
            Engine.BusinessObjects.SchedulingLeadTimeReportBO bo = new Engine.BusinessObjects.SchedulingLeadTimeReportBO();
            //delete all rows
            bo.DeleteAll();
            foreach (SchedulingLeadTimeReport item in SchedulingLeadTimeReportList)
            {
                bo.Create(new SchedulingLeadTimeReportBE
                {
                    BusinessDivisionRefId = item.BusinessDivisionRefId,
                    LeadTimeDays = item.LeadTimeDays,
                    ProjectTypeRefId = item.ProjectTypeRefId,
                    ReportGeneratedOn = item.GeneratedOn,
                    RequiredProjectHours = item.ProjectHours,
                    UserId = "1",
                    DateRangeStartDate = AutoSchedulePeriodStart,
                    DateRangeEndDate = AllowedMaxEndDate,
                    ProjectLevelTxt = item.ProjectLevelTxt
                });
            }
            return true;
        }
        /// <summary>
        /// Convert time slots to report data
        /// </summary>
        /// <param name="timeSlots"></param>
        /// <returns></returns>
        public bool ConvertTimeSlot(List<TimeSlot> timeSlots, DateTime? leadTimeStart)
        {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var timeUtc = DateTime.UtcNow;
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

            if (!leadTimeStart.HasValue)
            {
                //jcl add one day to start tomorrow
                var timeToCalcLeadFrom = DateTime.UtcNow.AddDays(1);
                leadTimeStart = TimeZoneInfo.ConvertTimeFromUtc(timeToCalcLeadFrom, easternZone);
            }

            foreach (TimeSlot timeslot in timeSlots)
            {
                int leadTimeInWorkDays = DateTimeHelper.GetWorkDayCountFromDateRange(leadTimeStart.Value, timeslot.StartTime);

                int businessDivisonRefId = BusinessDivisions.Where(x => x.BusinessRefId == (int)timeslot.DepartmentName).FirstOrDefault().BusinessDivisionRefId.Value;

                SchedulingLeadTimeReportList.Add(new SchedulingLeadTimeReport
                {
                    GeneratedOn = easternTime,
                    BusinessDivisionRefId = businessDivisonRefId,
                    ProjectTypeRefId = (int)CurrentProject.AccelaPropertyType,
                    ProjectHours = ReviewHours,
                    LeadTimeDays = leadTimeInWorkDays,
                    ProjectLevelTxt = ProjectLevelTxt
                });
            }

            //Get any missing departments, add a row with -1 for lead time
            foreach (ProjectAgency item in CurrentProject.Agencies.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    SchedulingLeadTimeReportList.Add(new SchedulingLeadTimeReport
                    {
                        GeneratedOn = easternTime,
                        BusinessDivisionRefId = BusinessDivisions.Where(x => x.BusinessRefId == (int)item.DepartmentInfo).FirstOrDefault().BusinessDivisionRefId.Value,
                        ProjectTypeRefId = (int)CurrentProject.AccelaPropertyType,
                        ProjectHours = ReviewHours,
                        LeadTimeDays = -1,
                        ProjectLevelTxt = ProjectLevelTxt
                    });

                }
            }
            foreach (ProjectTrade item in CurrentProject.Trades.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    SchedulingLeadTimeReportList.Add(new SchedulingLeadTimeReport
                    {
                        GeneratedOn = easternTime,
                        BusinessDivisionRefId = BusinessDivisions.Where(x => x.BusinessRefId == (int)item.DepartmentInfo).FirstOrDefault().BusinessDivisionRefId.Value,
                        ProjectTypeRefId = (int)CurrentProject.AccelaPropertyType,
                        ProjectHours = ReviewHours,
                        LeadTimeDays = -1,
                        ProjectLevelTxt = ProjectLevelTxt
                    });
                }
            }

            return true;
        }
    }
}