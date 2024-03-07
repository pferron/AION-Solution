using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models.Base;
using AION.Manager.Adapters;
using AION.Manager.Engines.Scheduling;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Engines
{
    public class FIFOSchedulingEngine : BaseFIFOEngine
    {
        public List<ScheduleCapacitySearchResult> AllEligibleReviewerCapacity { get; set; }

        public List<AutoSchedulableReviewer> AllOtherEligibleFifoReviewers { get; set; } = new List<AutoSchedulableReviewer>();

        private ProjectCycleSummary _ProjectCycleSummary { get; set; }

        private List<PlanReviewScheduleDetail> _PreviousFifoSchedules { get; set; }

        private bool _IsSubCycle = false;
        private bool _NoConfiguredReviewer = false;

        public PlanReview PlanReview { get; set; }

        public UserIdentity LastAssignedCityZoningReviewer;

        public FIFOSchedulingEngine() { }

        public FIFOSchedulingEngine(FIFOEngineParams data)
        {
            TimeSlotIntervalByMinutes = 15;
            RequestData = data;
            _IsSubCycle = RequestData.Cycle > 1 ? true : false;
            if (HolidayList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
                HolidayList = GetHolidays();
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(data.AccelaProjectIDRef);

            AllowedMaxEndDate = CurrentProject.FifoDueDt == null ? DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 7) : CurrentProject.FifoDueDt.Value;
            AutoSchedulePeriodStart = NextWorkingDay(DateTime.Now);

            GetProjectCycleSummary();

            UpdateTradesAndAgenciesForCycle();

            _PreviousFifoSchedules = _ProjectCycleSummary.PlanReviewScheduleDetailsPrevious;

            _SameBuildingContractor = GetSameBuildingContractor();

            SetUpEligibleReviewers();

            CalculateMeetingDuration();

            CalculateMeetingDurationSubCycleOverride();

            AutoScheduledValues = GetAutoEstimatedValues();

            if (_NoConfiguredReviewer == false)
            {
                while (!AutoScheduledValues.IsReadyForScheduling)
                {
                    //reset date range
                    AutoSchedulePeriodStart = NextWorkingDay(AllowedMaxEndDate);
                    AllowedMaxEndDate = DateTimeHelper.DetermineWorkDateAfterDateSpecified(AutoSchedulePeriodStart, 7);

                    //get update reviewer list
                    SetUpEligibleReviewers();

                    AutoScheduledValues = GetAutoEstimatedValues();
                }

                if (AutoScheduledValues.IsReadyForScheduling)
                {
                    CreatePlanReviewFromAutoScheduledValues();
                    FIFOAdapter fifoAdapter = new FIFOAdapter();
                    IsFifoScheduled = fifoAdapter.UpsertFIFO(PlanReview);
                }
            }
        }
        public FIFOSchedulingEngine(AutoScheduleReportParams parms)
        {
            FIFOEngineParams data = new FIFOEngineParams
            {
                CurrentProject = parms.CurrentProject

            };
            TimeSlotIntervalByMinutes = 15;
            RequestData = data;
            _IsSubCycle = false;
            if (HolidayList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
                HolidayList = GetHolidays();

            CurrentProject = RequestData.CurrentProject;
            AllowedMaxEndDate = parms.ManualEndDateTime.Value;
            AutoSchedulePeriodStart = parms.ManualStartDateTime.Value;

            _ProjectCycleSummary = new ProjectCycleSummary();

            _PreviousFifoSchedules = new List<PlanReviewScheduleDetail>();

            _SameBuildingContractor = new SameBuildingContractor { IsSameBuildingContractor = false };

            SetUpEligibleReviewers();

            MeetingDuration = parms.ReviewHours;

            BusinessTimeSlots = GetFIFOAutoEstimatedValuesForReport();
        }
        private List<TimeSlot> GetFIFOAutoEstimatedValuesForReport()
        {

            AllocationValues allocationValues = new AllocationValues();
            FIFOReviewer fifoReviewer = new FIFOReviewer();
            List<AutoSchedulableReviewer> reviewersForSearch = new List<AutoSchedulableReviewer>();
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            foreach (ProjectAgency item in CurrentProject.Agencies.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == item.DepartmentInfo).FirstOrDefault();

                    reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, GetTradeReviewersForReport(curtrade.DepartmentInfo));

                    foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                    {
                        fifoReviewer = CreateFifoReviewer(reviewer);
                        if (fifoReviewer != null)
                        {
                            allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDuration);
                            if (allocationValues.Success)
                            {
                                TimeSlot ts = new TimeSlot
                                {
                                    DepartmentName = curtrade.DepartmentInfo,
                                    StartTime = allocationValues.ScheduleStart.Value,
                                    EndTime = allocationValues.ScheduleEnd.Value,
                                    TotalTimeOfProject = allocationValues.ScheduleEnd.Value - allocationValues.ScheduleStart.Value
                                };

                                timeSlots.Add(ts);
                                break;
                            }
                        }
                    }
                }
            }
            foreach (ProjectTrade item in CurrentProject.Trades.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == item.DepartmentInfo).FirstOrDefault();

                    reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, GetTradeReviewersForReport(curtrade.DepartmentInfo));

                    foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                    {
                        fifoReviewer = CreateFifoReviewer(reviewer);
                        if (fifoReviewer != null)
                        {
                            allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDuration);
                            if (allocationValues.Success)
                            {
                                TimeSlot ts = new TimeSlot
                                {
                                    DepartmentName = curtrade.DepartmentInfo,
                                    StartTime = allocationValues.ScheduleStart.Value,
                                    EndTime = allocationValues.ScheduleEnd.Value,
                                    TotalTimeOfProject = allocationValues.ScheduleEnd.Value - allocationValues.ScheduleStart.Value
                                };

                                timeSlots.Add(ts);
                                break;
                            }
                        }
                    }
                }
            }
            return timeSlots;
        }

        #region Private methods
        private void CreatePlanReviewFromAutoScheduledValues()
        {
            PlanReview planReview = new PlanReview();

            //needed to update project status
            planReview.IsFutureCycle =
                (_ProjectCycleSummary.ProjectCycleFuture != null) &&
                (_ProjectCycleSummary.ProjectCycleFuture.CycleNbr == RequestData.Cycle);

            planReview.UpdateProjectStatus = true;
            planReview.AutoScheduled = true;
            planReview.IsSubmit = true;

            planReview.ProjectScheduleRefEnum = ProjectScheduleRefEnum.FIFO;
            planReview.ProjectCycle = _ProjectCycleSummary.ProjectCycleCurrent;

            planReview.ProjectId = CurrentProject.ID;
            planReview.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Scheduled;
            planReview.IsSameBuildingContractor = _SameBuildingContractor.IsSameBuildingContractor;

            // Building
            planReview.BuildEndDate = AutoScheduledValues.BuildingScheduleEnd;
            planReview.BuildStartDate = AutoScheduledValues.BuildingScheduleStart;
            planReview.HoursBuilding = AutoScheduledValues.BuildingHours;

            // Backflow
            planReview.BackfEndDate = AutoScheduledValues.BackFlowScheduleEnd;
            planReview.BackfStartDate = AutoScheduledValues.BackFlowScheduleStart;
            planReview.HoursBackFlow = AutoScheduledValues.BackFlowHours;

            // Daycare
            planReview.DaycEndDate = AutoScheduledValues.DayCareScheduleEnd;
            planReview.DaycStartDate = AutoScheduledValues.DayCareScheduleStart;
            planReview.HoursDayCare = AutoScheduledValues.DayCareHours;

            // Electric
            planReview.ElectEndDate = AutoScheduledValues.ElectricScheduleEnd;
            planReview.ElectStartDate = AutoScheduledValues.ElectricScheduleStart;
            planReview.HoursElectic = AutoScheduledValues.ElectricHours;

            // Facility
            planReview.FacilEndDate = AutoScheduledValues.FacilityScheduleEnd;
            planReview.FacilStartDate = AutoScheduledValues.FacilityScheduleStart;
            planReview.HoursLodge = AutoScheduledValues.FacilityHours;

            // Fire
            planReview.FireEndDate = AutoScheduledValues.FireScheduleEnd;
            planReview.FireStartDate = AutoScheduledValues.FireScheduleStart;
            planReview.HoursFire = AutoScheduledValues.FireHours;

            // Food Service
            planReview.FoodEndDate = AutoScheduledValues.FoodScheduleEnd;
            planReview.FoodStartDate = AutoScheduledValues.FoodScheduleStart;
            planReview.HoursFood = AutoScheduledValues.FoodServiceHours;

            // Mechanical
            planReview.MechaEndDate = AutoScheduledValues.MechScheduleEnd;
            planReview.MechaStartDate = AutoScheduledValues.MechScheduleStart;
            planReview.HoursMech = AutoScheduledValues.MechHours;

            // Plumbing
            planReview.PlumbEndDate = AutoScheduledValues.PlumbScheduleEnd;
            planReview.PlumbStartDate = AutoScheduledValues.PlumbScheduleStart;
            planReview.HoursPlumb = AutoScheduledValues.PlumbHours;

            // Pool
            planReview.PoolEndDate = AutoScheduledValues.PoolScheduleEnd;
            planReview.PoolStartDate = AutoScheduledValues.PoolScheduleStart;
            planReview.HoursPool = AutoScheduledValues.PoolHours;

            // Zoning
            planReview.ZoneEndDate = AutoScheduledValues.ZoneScheduleEnd;
            planReview.ZoneStartDate = AutoScheduledValues.ZoneScheduleStart;
            planReview.HoursZoning = AutoScheduledValues.ZoneHours;

            //jcl get the correct depts for fire and zone
            Helper _helper = new Helper();
            ProjectAgency fireagency = CurrentProject.Agencies.Where(x => _helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            ProjectAgency zoneagency = CurrentProject.Agencies.Where(x => _helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();

            List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();

            if (AutoScheduledValues.BuildingUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.BuildingUserID, BusinessRefId = (int)DepartmentNameEnums.Building, DeptNameEnumId = (int)DepartmentNameEnums.Building });
            if (AutoScheduledValues.BackFlowUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.BackFlowUserID, BusinessRefId = (int)DepartmentNameEnums.Backflow, DeptNameEnumId = (int)DepartmentNameEnums.Backflow });
            if (AutoScheduledValues.DayCareUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.DayCareUserID, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care, DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care });
            if (AutoScheduledValues.ElectricUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.ElectricUserID, BusinessRefId = (int)DepartmentNameEnums.Electrical, DeptNameEnumId = (int)DepartmentNameEnums.Electrical });
            if (AutoScheduledValues.FacilityUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.FacilityUserID, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities, DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities });
            if (AutoScheduledValues.FireUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.FireUserID, BusinessRefId = (int)fireagency.DepartmentInfo, DeptNameEnumId = (int)fireagency.DepartmentInfo });
            if (AutoScheduledValues.FoodServiceUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.FoodServiceUserID, BusinessRefId = (int)DepartmentNameEnums.EH_Food, DeptNameEnumId = (int)DepartmentNameEnums.EH_Food });
            if (AutoScheduledValues.MechUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.MechUserID, BusinessRefId = (int)DepartmentNameEnums.Mechanical, DeptNameEnumId = (int)DepartmentNameEnums.Mechanical });
            if (AutoScheduledValues.PlumbUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.PlumbUserID, BusinessRefId = (int)DepartmentNameEnums.Plumbing, DeptNameEnumId = (int)DepartmentNameEnums.Plumbing });
            if (AutoScheduledValues.PoolUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.PoolUserID, BusinessRefId = (int)DepartmentNameEnums.EH_Pool, DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool });
            if (AutoScheduledValues.ZoneUserID != -1) attendeeIds.Add(new AttendeeInfo { AttendeeId = AutoScheduledValues.ZoneUserID, BusinessRefId = (int)zoneagency.DepartmentInfo, DeptNameEnumId = (int)zoneagency.DepartmentInfo });

            planReview.AssignedReviewers = attendeeIds;

            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

            planReview.CreatedUser = userIdentityModelBO.GetInstance(1);
            planReview.UpdatedUser = planReview.CreatedUser;

            PlanReview = planReview;
        }

        private void GetProjectCycleSummary()
        {
            PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
            _ProjectCycleSummary = planReviewAdapter.GetProjectCycleSummary(CurrentProject.ID);
        }

        private void UpdateTradesAndAgenciesForCycle()
        {
            if (_ProjectCycleSummary != null)
            {
                if (_ProjectCycleSummary.ProjectCycleCurrent.CycleNbr > 1 && _ProjectCycleSummary.ProjectCycleDetailsCurrent.Count() > 0)  // this is a CURRENT sub cycle
                {
                    // adjust project trades for auto scheduling to be only those in the project cycle detail
                    List<ProjectDepartment> projectDepartmentsToAutoSchedule = new List<ProjectDepartment>();

                    foreach (ProjectCycleDetail detail in _ProjectCycleSummary.ProjectCycleDetailsCurrent)
                    {
                        ProjectDepartment dept = ProjectHelper.GetDepartment(CurrentProject, (DepartmentNameEnums)detail.BusinessRefId);
                        projectDepartmentsToAutoSchedule.Add(dept);
                    }

                    CurrentProject.Trades.RemoveAll(item => !projectDepartmentsToAutoSchedule.Contains(item));
                    CurrentProject.Agencies.RemoveAll(item => !projectDepartmentsToAutoSchedule.Contains(item));
                }
            }
        }

        private void SetUpEligibleReviewers()
        {
            AllEligibleReviewers = GetAllEligibleReviewers();
            AllOtherEligibleFifoReviewers = GetOtherEligibleFifoReviewers();
            SplitEligibleReviewersByDept();

            LastAssignedCityZoningReviewer = GetLastAssignedCityZoningReviewer();
            if (LastAssignedCityZoningReviewer.ID == 0)
            {
                LastAssignedCityZoningReviewer = null;
            }
        }

        private UserIdentity GetLastAssignedCityZoningReviewer()
        {
            FIFOAdapter fifoAdapter = new FIFOAdapter();
            UserIdentity cityZoningUser = fifoAdapter.GetLastAssignedCityZoningReviewer();

            return cityZoningUser != null ? cityZoningUser : new UserIdentity();
        }

        public List<AutoSchedulableReviewer> GetOtherEligibleFifoReviewers()
        {
            UserAdapter userAdapter = new UserAdapter();

            List<AutoSchedulableReviewer> users = new List<AutoSchedulableReviewer>();

            users.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home, (int)DepartmentNameEnums.NA).Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList());
            users.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Master_Plans, (int)DepartmentNameEnums.NA).Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList());
            users.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Single_Family_Homes, (int)DepartmentNameEnums.NA).Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList());
            users.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Small_Commercial, (int)DepartmentNameEnums.NA).Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList());

            users = users.DistinctBy(x => x.UserIdentity.ID).ToList();

            List<AutoSchedulableReviewer> fifoUsers = SetUpEligibleUsers(users);

            List<AutoSchedulableReviewer> otherFifoUsers = new List<AutoSchedulableReviewer>();

            foreach (AutoSchedulableReviewer user in fifoUsers.OrderBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.FirstName))
            {
                int userId = user.UserIdentity.ID;

                if (!AllEligibleReviewers.Where(x => x.UserIdentity.ID == userId).Any())
                {
                    otherFifoUsers.Add(user);
                }
            }

            return otherFifoUsers;
        }

        private bool ProjectHasCityZoningDepartment()
        {
            ProjectDepartment cityZoningDepartment = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Zone_Cty_Chrlt).FirstOrDefault();

            if (cityZoningDepartment == null || cityZoningDepartment.EstimationNotApplicable == true)
            {
                return false;
            }

            return true;
        }

        private List<AutoSchedulableReviewer> GetEligibleReviewersForCurrentSearch(
            ProjectDepartment projectDepartment,
            List<AutoSchedulableReviewer> deptEligibleReviewers)
        {
            List<AutoSchedulableReviewer> reviewersForSearch = new List<AutoSchedulableReviewer>();

            if (_IsSubCycle)
            {
                if (_PreviousFifoSchedules != null && _PreviousFifoSchedules.Count > 0)
                {
                    foreach (PlanReviewScheduleDetail fifoSchedule in _PreviousFifoSchedules.Where(x => x.BusinessRefId == (int)projectDepartment.DepartmentInfo))
                    {
                        AutoSchedulableReviewer reviewer = AllEligibleReviewers.FirstOrDefault(x => x.UserIdentity.ID == fifoSchedule.AssignedPlanReviewerId);

                        if (reviewer != null)
                        {
                            reviewersForSearch.Add(reviewer);
                        }
                    }
                }

                if (reviewersForSearch.Count == 0)
                {
                    reviewersForSearch = deptEligibleReviewers;
                }
            }
            else if (_SameBuildingContractor.IsSameBuildingContractor)
            {
                foreach (PlanReviewScheduleDetail fifoSchedule in _SameBuildingContractor.FifoSchedules.Where(x => x.BusinessRefId == (int)projectDepartment.DepartmentInfo))
                {
                    AutoSchedulableReviewer reviewer = AllEligibleReviewers.FirstOrDefault(x => x.UserIdentity.ID == fifoSchedule.AssignedPlanReviewerId);

                    if (reviewer != null)
                    {
                        reviewersForSearch.Add(reviewer);
                    }
                }

                if (reviewersForSearch.Count == 0)
                {
                    reviewersForSearch = deptEligibleReviewers;
                }
            }
            else
            {
                reviewersForSearch = deptEligibleReviewers;
            }

            // sort reviewers first depending on department
            if (projectDepartment.DepartmentInfo == DepartmentNameEnums.Zone_Cty_Chrlt)
            {
                reviewersForSearch = reviewersForSearch.OrderBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.FirstName).ToList();

                if (LastAssignedCityZoningReviewer != null)
                {
                    reviewersForSearch = AdjustReviewerListOrderByLastReviewerAssigned(reviewersForSearch);
                }

                reviewersForSearch.AddRange(AllOtherEligibleFifoReviewers.OrderBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.FirstName).ToList());
            }
            else
            {
                reviewersForSearch = reviewersForSearch.OrderBy(x => x.TotalHoursForCapacity).ThenBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.FirstName).ToList();
            }

            // if the department had primary, secondary, or proposed reviewer requests,
            // add that reviewer to the front of the list

            if (projectDepartment.ProposedPlanReviewer != null && projectDepartment.ProposedPlanReviewer.ID > 0)
            {
                AutoSchedulableReviewer rvr = deptEligibleReviewers.Where(x => x.UserIdentity.ID == projectDepartment.ProposedPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    reviewersForSearch.Insert(0, rvr);
                }
            }

            if (projectDepartment.SecondaryPlanReviewer != null && projectDepartment.SecondaryPlanReviewer.ID > 0)
            {
                AutoSchedulableReviewer rvr = deptEligibleReviewers.Where(x => x.UserIdentity.ID == projectDepartment.SecondaryPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    reviewersForSearch.Insert(0, rvr);
                }
            }

            if (projectDepartment.PrimaryPlanReviewer != null && projectDepartment.PrimaryPlanReviewer.ID > 0)
            {
                AutoSchedulableReviewer rvr = deptEligibleReviewers.Where(x => x.UserIdentity.ID == projectDepartment.PrimaryPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    reviewersForSearch.Insert(0, rvr);
                }
            }

            return reviewersForSearch;
        }

        public List<AutoSchedulableReviewer> AdjustReviewerListOrderByLastReviewerAssigned(List<AutoSchedulableReviewer> reviewersForSearch)
        {
            // check if user id in list already, if not, then add and sort by last name, first name
            if (!reviewersForSearch.Any(x => x.UserIdentity.ID == LastAssignedCityZoningReviewer.ID))
            {
                reviewersForSearch.Add(new AutoSchedulableReviewer() { UserIdentity = LastAssignedCityZoningReviewer });
                reviewersForSearch = reviewersForSearch.OrderBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.FirstName).ToList();
            }

            // find the index of the reviewer in the list that is the last assigned city zoning reviewer
            AutoSchedulableReviewer previousReviewer = reviewersForSearch.FirstOrDefault(x => x.UserIdentity.ID == LastAssignedCityZoningReviewer.ID);

            if (previousReviewer != null)
            {
                int idx = reviewersForSearch.IndexOf(previousReviewer);
                int idxNextAlphaReviewer = idx + 1;

                // get all reviewers in front of last reviewer
                List<AutoSchedulableReviewer> pastReviewers = reviewersForSearch.GetRange(0, idx + 1);

                // remove past reviewers from front of list
                reviewersForSearch.RemoveRange(0, idx + 1);

                // add the past reviewers to the end so they can be cycled back through
                reviewersForSearch.AddRange(pastReviewers);
            }

            return reviewersForSearch;
        }

        public AutoScheduledFIFOValues GetAutoEstimatedValues()
        {
            AutoScheduledFIFOValues ret = new AutoScheduledFIFOValues();
            Helper helper = new Helper();
            if (AllEligibleReviewers.Count == 0
                && ProjectHasCityZoningDepartment() == false) // if has a city zoning department, needs to be able to look to other fifo project type reviewers
            {
                ret.ErrorMessage = "No Eligible Reviewers Found! Check user settings for scheduling eligibility.";
                return ret;
            }

            AllocationValues allocationValues = new AllocationValues();
            FIFOReviewer fifoReviewer = new FIFOReviewer();
            List<AutoSchedulableReviewer> reviewersForSearch = new List<AutoSchedulableReviewer>();

            //Building
            ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBuildingReviewersList == null)
            {
                ret.BuildingUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleBuildingReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationBuild);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.BuildingUserID = fifoReviewer.ReviewerId;
                ret.BuildingHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.BuildingScheduleStart = allocationValues.ScheduleStart;
                ret.BuildingScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Electrical
            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleElectricReviewersList == null)
            {
                ret.ElectricUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleElectricReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationElectric);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.ElectricUserID = fifoReviewer.ReviewerId;
                ret.ElectricHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.ElectricScheduleStart = allocationValues.ScheduleStart;
                ret.ElectricScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Mechanical
            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleMechReviewersList == null)
            {
                ret.MechUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleMechReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationMech);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.MechUserID = fifoReviewer.ReviewerId;
                ret.MechHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.MechScheduleStart = allocationValues.ScheduleStart;
                ret.MechScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Plumbing
            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePlumbReviewersList == null)
            {
                ret.PlumbUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligiblePlumbReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationPlumb);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.PlumbUserID = fifoReviewer.ReviewerId;
                ret.PlumbHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.PlumbScheduleStart = allocationValues.ScheduleStart;
                ret.PlumbScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Zoning
            curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleZoneReviewersList == null)
            {
                ret.ZoneUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleZoneReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationZone);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.ZoneUserID = fifoReviewer.ReviewerId;
                ret.ZoneHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.ZoneScheduleStart = allocationValues.ScheduleStart;
                ret.ZoneScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Fire
            curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFireReviewersList == null)
            {
                ret.FireUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleFireReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationFire);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.FireUserID = fifoReviewer.ReviewerId;
                ret.FireHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.FireScheduleStart = allocationValues.ScheduleStart;
                ret.FireScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Backflow
            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBackFlowReviewersList == null)
            {
                ret.BackFlowUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleBackFlowReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationBackFlow);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.BackFlowUserID = fifoReviewer.ReviewerId;
                ret.BackFlowHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.BackFlowScheduleStart = allocationValues.ScheduleStart;
                ret.BackFlowScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Food
            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFoodServiceReviewersList == null)
            {
                ret.FoodServiceUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleFoodServiceReviewersList);

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationFood);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.FoodServiceUserID = fifoReviewer.ReviewerId;
                ret.FoodServiceHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.FoodScheduleStart = allocationValues.ScheduleStart;
                ret.FoodScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Pool
            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePoolReviewersList == null)
            {
                ret.PoolUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligiblePoolReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationPool);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.PoolUserID = fifoReviewer.ReviewerId;
                ret.PoolHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.PoolScheduleStart = allocationValues.ScheduleStart;
                ret.PoolScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Facilities
            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFacilityReviewersList == null)
            {
                ret.FacilityUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleFacilityReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationFacility);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.FacilityUserID = fifoReviewer.ReviewerId;
                ret.FacilityHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.FacilityScheduleStart = allocationValues.ScheduleStart;
                ret.FacilityScheduleEnd = allocationValues.ScheduleEnd;
            }

            //Daycare
            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleDayCareReviewersList == null)
            {
                ret.DayCareUserID = -1;
            }
            else
            {
                reviewersForSearch = GetEligibleReviewersForCurrentSearch(curtrade, EligibleDayCareReviewersList);

                if (reviewersForSearch.Count == 0)
                {
                    _NoConfiguredReviewer = true;
                }

                foreach (AutoSchedulableReviewer reviewer in reviewersForSearch)
                {
                    fifoReviewer = CreateFifoReviewer(reviewer);
                    if (fifoReviewer != null)
                    {
                        allocationValues = GetAllocation(fifoReviewer.Reviewer, MeetingDurationDayCare);
                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.DayCareUserID = fifoReviewer.ReviewerId;
                ret.DayCareHours = _SameBuildingContractor.IsSameBuildingContractor ? AdjustMeetingDuration(fifoReviewer.ReviewerId, allocationValues.Hours) : allocationValues.Hours;
                ret.DayCareScheduleStart = allocationValues.ScheduleStart;
                ret.DayCareScheduleEnd = allocationValues.ScheduleEnd;
            }

            return ret;
        }

        private SameBuildingContractor GetSameBuildingContractor()
        {
            SameBuildingContractor sameBuildingContractor = new SameBuildingContractor();

            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes ||
                CurrentProject.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
            {
                sameBuildingContractor = new FIFOAdapter().GetSameBuildingContractor(CurrentProject);
            }

            return sameBuildingContractor;
        }

        private void CalculateMeetingDurationSubCycleOverride()
        {
            if (_IsSubCycle)
            {
                OverrideDurationForSubCycle();
            }
        }

        private void OverrideDurationForSubCycle()
        {
            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial)
            {
                MeetingDurationDayCare = 0.5M;
                MeetingDurationFood = 0.5M;
                MeetingDurationPool = 0.5M;
                MeetingDurationFacility = 0.5M;
                MeetingDurationBackFlow = 0.5M;
                MeetingDurationFire = 0.5M;
                MeetingDurationZone = 0.5M;
            }

            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes)
            {
                MeetingDurationBuild = 1.0M;
            }

            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
            {
                MeetingDurationBuild = 1.5M;
            }

            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home)
            {
                MeetingDurationBuild = 0.5M;
            }
        }

        private decimal AdjustMeetingDuration(int reviewerId, decimal allocatedHours)
        {
            if (_SameBuildingContractor.FifoSchedules.Any(x => x.AssignedPlanReviewerId == reviewerId))
            {
                return AdjustMeetingDurationHours(allocatedHours);
            }
            else
            {
                return allocatedHours;
            }
        }

        private decimal AdjustMeetingDurationHours(decimal hours)
        {
            hours = hours / 2;

            decimal wholeestimationhours = Math.Truncate(hours);
            decimal partestimationhours = hours - wholeestimationhours;
            if (hours % 0.5M != 0)
            {
                if (partestimationhours > 0.5M)
                {
                    //add one to truncated value
                    hours = wholeestimationhours + 1.0M;
                }
                if (partestimationhours < 0.5M)
                {
                    //add .5 to truncated value
                    hours = wholeestimationhours + 0.5M;
                }
            }

            return Math.Round(hours, 1);
        }
        #endregion
    }
}