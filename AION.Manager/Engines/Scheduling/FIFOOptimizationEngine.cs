using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models.Base;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Engines.Scheduling
{
    public class FIFOOptimizationEngine : BaseFIFOEngine
    {
        public PlanReview PlanReview { get; set; }
        private ProjectCycleSummary _ProjectCycleSummary { get; set; }

        bool _CanSwitchPlanReviewers = false;

        public FIFOOptimizationEngine(FIFOEngineParams data)
        {
            TimeSlotIntervalByMinutes = 15;
            RequestData = data;
            if (HolidayList == null)
                HolidayList = GetHolidays();
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            CurrentProject = data.CurrentProject;
            AllowedMaxEndDate = data.CurrentProject.FifoDueDt.Value;
            PlanReview = data.PlanReview;
            SetAssignedReviewers();

            _SameBuildingContractor = new SameBuildingContractor()
            {
                IsSameBuildingContractor = PlanReview.IsSameBuildingContractor
            };

            GetProjectCycleSummary();

            SetUpEligibleReviewers();

            _CanSwitchPlanReviewers = CanCurrentAssignmentSwitchPlanReviewers();

            AutoScheduledValues = OptimizeSchedule();

            while (!AutoScheduledValues.IsReadyForScheduling)
            {
                //reset date range
                AutoSchedulePeriodStart = NextWorkingDay(AllowedMaxEndDate);
                AllowedMaxEndDate = DateTimeHelper.DetermineWorkDateAfterDateSpecified(AutoSchedulePeriodStart, 7);

                //get update reviewer list
                SetUpEligibleReviewers();

                AutoScheduledValues = OptimizeSchedule();
            }

            if (AutoScheduledValues.IsReadyForScheduling)
            {
                CreateFIFOScheduleFromAutoScheduledValues();
                IsFifoScheduled = true;
            }
        }

        #region Private Methods
        private void GetProjectCycleSummary()
        {
            PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
            _ProjectCycleSummary = planReviewAdapter.GetProjectCycleSummary(CurrentProject.ID);
        }

        private void SetAssignedReviewers()
        {
            foreach (AttendeeInfo attendee in PlanReview.AssignedReviewers)
            {
                DepartmentNameEnums department = (DepartmentNameEnums)attendee.BusinessRefId;

                switch (department)
                {
                    case DepartmentNameEnums.Building:
                        PlanReview.BuildAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.Electrical:
                        PlanReview.ElectricAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.Mechanical:
                        PlanReview.MechAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        PlanReview.PlumbAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        PlanReview.ZoningAssigedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.Fire_Davidson:
                    case DepartmentNameEnums.Fire_Cornelius:
                    case DepartmentNameEnums.Fire_Pineville:
                    case DepartmentNameEnums.Fire_Matthews:
                    case DepartmentNameEnums.Fire_Mint_Hill:
                    case DepartmentNameEnums.Fire_Huntersville:
                    case DepartmentNameEnums.Fire_UMC:
                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                    case DepartmentNameEnums.Fire_County:
                        PlanReview.FireAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        PlanReview.DayCareAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.EH_Food:
                        PlanReview.FoodAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        PlanReview.PoolAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        PlanReview.FacilityAssignedReviewerId = attendee.AttendeeId;
                        break;
                    case DepartmentNameEnums.Backflow:
                        PlanReview.BackFlowAssignedReviewerId = attendee.AttendeeId;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool CanCurrentAssignmentSwitchPlanReviewers()
        {
            if (PlanReview.IsManualAssignment) return false;

            DateTime fourWorkingDaysAhead = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 4).Date;

            if (PlanReview.IsSameBuildingContractor && PlanReview.EarliestDate.Value.Date <= fourWorkingDaysAhead) return false;

            bool isInOmittedAccelaWorkflowStatus = CheckAccelaWorkflowTaskStatus();

            if (isInOmittedAccelaWorkflowStatus) return false;

            if (_ProjectCycleSummary.ProjectCycleCurrent != null
                && _ProjectCycleSummary.ProjectCycleCurrent.CycleNbr != null
                && _ProjectCycleSummary.ProjectCycleCurrent.CycleNbr > 1)
            {
                return false;
            }

            return true;
        }

        private bool CheckAccelaWorkflowTaskStatus()
        {
            List<string> omittedStatuses = new List<string>()
            {
                "Approved",
                "Approved as Noted",
                "In Progress",
                "Interactive Review",
                "Meeting Requested",
                "Revisions Required",
                "NA",
            };

            if (omittedStatuses.Contains(RequestData.AccelaWorkflowTaskStatus))
            {
                return true;
            }
            return false;
        }

        private void SetUpEligibleReviewers()
        {
            AllEligibleReviewers = GetAllEligibleReviewers();
            RemoveMeetingHoursForCurrentProject();
            SplitEligibleReviewersByDept();
        }

        private void RemoveMeetingHoursForCurrentProject()
        {
            foreach (AutoSchedulableReviewer reviewer in AllEligibleReviewers)
            {
                List<TimeSlot> currentFifoScheduleTimeSlots = reviewer.CurrentMeetings.Where(x => x.ProjectID == CurrentProject.ID).ToList();
                foreach (TimeSlot timeSlot in currentFifoScheduleTimeSlots)
                {
                    reviewer.CurrentMeetings.Remove(timeSlot);
                }
            }
        }

        private AutoScheduledFIFOValues OptimizeSchedule()
        {
            AutoScheduledFIFOValues ret = new AutoScheduledFIFOValues();
            Helper helper = new Helper();
            if (AllEligibleReviewers.Count == 0)
            {
                ret.ErrorMessage = "No Eligible Reviewers Found! Check user settings for scheduling eligibility.";
                return ret;
            }

            AllocationValues allocationValues = new AllocationValues();
            FIFOReviewer fifoReviewer = new FIFOReviewer();

            //Building
            ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBuildingReviewersList == null)
            {
                ret.BuildingUserID = -1;
            }
            else
            {
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleBuildingReviewersList);

                // check first date in list where remaining available hours > 0
                // if different reviewer than one assigned, make sure _CanSwitchPlanReviewers is set to true
                // if _CanSwitchPlanReviewers is true, send the matching eligible reviewer into base engine for scheduling
                // set the new proposed values

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.BuildAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleBuildingReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleBuildingReviewersList.Where(x => x.UserIdentity.ID == PlanReview.BuildAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursBuilding);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.BuildingUserID = fifoReviewer.ReviewerId;
                ret.BuildingHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleElectricReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.ElectricAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleElectricReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleElectricReviewersList.Where(x => x.UserIdentity.ID == PlanReview.ElectricAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursElectic);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.ElectricUserID = fifoReviewer.ReviewerId;
                ret.ElectricHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleMechReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.MechAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleMechReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleMechReviewersList.Where(x => x.UserIdentity.ID == PlanReview.MechAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursMech);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.MechUserID = fifoReviewer.ReviewerId;
                ret.MechHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligiblePlumbReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.PlumbAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligiblePlumbReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligiblePlumbReviewersList.Where(x => x.UserIdentity.ID == PlanReview.PlumbAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursPlumb);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.PlumbUserID = fifoReviewer.ReviewerId;
                ret.PlumbHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleZoneReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.ZoningAssigedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleZoneReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleZoneReviewersList.Where(x => x.UserIdentity.ID == PlanReview.ZoningAssigedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursZoning);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.ZoneUserID = fifoReviewer.ReviewerId;
                ret.ZoneHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleFireReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.FireAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleFireReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleFireReviewersList.Where(x => x.UserIdentity.ID == PlanReview.FireAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursFire);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.FireUserID = fifoReviewer.ReviewerId;
                ret.FireHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleBackFlowReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.BackFlowAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleBackFlowReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleBackFlowReviewersList.Where(x => x.UserIdentity.ID == PlanReview.BackFlowAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursBackFlow);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.BackFlowUserID = fifoReviewer.ReviewerId;
                ret.BackFlowHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleFoodServiceReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.FoodAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleFoodServiceReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleFoodServiceReviewersList.Where(x => x.UserIdentity.ID == PlanReview.FoodAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursFood);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.FoodServiceUserID = fifoReviewer.ReviewerId;
                ret.FoodServiceHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligiblePoolReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.PoolAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligiblePoolReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligiblePoolReviewersList.Where(x => x.UserIdentity.ID == PlanReview.PoolAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursPool);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.PoolUserID = fifoReviewer.ReviewerId;
                ret.PoolHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleFacilityReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.FacilityAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleFacilityReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleFacilityReviewersList.Where(x => x.UserIdentity.ID == PlanReview.FacilityAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursLodge);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.FacilityUserID = fifoReviewer.ReviewerId;
                ret.FacilityHours = allocationValues.Hours;
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
                // get reviewer with soonest available time
                List<ReviewerHoursByDate> reviewers = GetEligibleReviewersWithSoonestCapacity(EligibleDayCareReviewersList);

                foreach (ReviewerHoursByDate reviewer in reviewers)
                {
                    if (reviewer.RemainingAvailableHours > 0)
                    {
                        AutoSchedulableReviewer autoSchedulebleReviewer;

                        if (reviewer.UserIdentity.ID != PlanReview.DayCareAssignedReviewerId && _CanSwitchPlanReviewers)
                        {
                            autoSchedulebleReviewer = EligibleDayCareReviewersList.Where(x => x.UserIdentity.ID == reviewer.UserIdentity.ID).FirstOrDefault();
                        }
                        else
                        {
                            autoSchedulebleReviewer = EligibleDayCareReviewersList.Where(x => x.UserIdentity.ID == PlanReview.DayCareAssignedReviewerId).FirstOrDefault();
                        }

                        if (autoSchedulebleReviewer == null) continue;

                        fifoReviewer = CreateFifoReviewer(autoSchedulebleReviewer);

                        allocationValues = GetAllocation(fifoReviewer.Reviewer, PlanReview.HoursDayCare);

                        if (allocationValues.Success)
                        {
                            ret.IsReadyForScheduling = true;
                            break;
                        }
                    }
                }

                ret.DayCareUserID = fifoReviewer.ReviewerId;
                ret.DayCareHours = allocationValues.Hours;
                ret.DayCareScheduleStart = allocationValues.ScheduleStart;
                ret.DayCareScheduleEnd = allocationValues.ScheduleEnd;
            }

            return ret;
        }

        private List<DateTime> GetDatesToEvaluateSoonestCapacity()
        {
            List<DateTime> dates = new List<DateTime>();

            DateTime start = AutoSchedulePeriodStart;
            while (start.Date <= AllowedMaxEndDate.Date)
            {
                dates.Add(start.Date);
                start = NextWorkingDay(start.Date);
            }

            return dates;
        }

        private List<ReviewerHoursByDate> GetEligibleReviewersWithSoonestCapacity(
            List<AutoSchedulableReviewer> deptEligibleReviewers)
        {
            List<ReviewerHoursByDate> reviewerHoursByDates = new List<ReviewerHoursByDate>();

            List<DateTime> dates = GetDatesToEvaluateSoonestCapacity();

            foreach (AutoSchedulableReviewer reviewer in deptEligibleReviewers)
            {
                List<ReviewerHoursByDate> totalSlotsForDay = new List<ReviewerHoursByDate>();

                if (reviewer.CurrentMeetings.Count > 0)
                {
                    totalSlotsForDay = reviewer.CurrentMeetings.GroupBy(x => new { x.StartTime.Date })
                    .Select(rv => new ReviewerHoursByDate()
                    {
                        UserIdentity = reviewer.UserIdentity,
                        Date = rv.Key.Date,
                        TotalHours = rv.Sum(x => x.TotalTimeOfDay.Hours)
                    }).ToList();
                }

                foreach (var totalSlotForDay in totalSlotsForDay)
                {
                    if (totalSlotForDay.TotalHours < reviewer.AllowedHoursPerDay)
                    {
                        totalSlotForDay.RemainingAvailableHours = reviewer.AllowedHoursPerDay - totalSlotForDay.TotalHours;
                    }
                    else
                    {
                        totalSlotForDay.RemainingAvailableHours = 0;
                    }

                    reviewerHoursByDates.Add(totalSlotForDay);
                }

                // now go through each available day and see if there's an entry.
                // if not, add one with remaining available hours = reviwer.AllowedHoursPerDay

                foreach (DateTime date in dates)
                {
                    ReviewerHoursByDate reviewerHoursByDate = totalSlotsForDay.FirstOrDefault(x => x.Date.Date == date.Date);
                    if (reviewerHoursByDate == null)
                    {
                        reviewerHoursByDates.Add(new ReviewerHoursByDate()
                        {
                            UserIdentity = reviewer.UserIdentity,
                            Date = date,
                            TotalHours = 0,
                            RemainingAvailableHours = reviewer.AllowedHoursPerDay
                        });
                    }
                }
            }

            return reviewerHoursByDates.OrderBy(x => x.Date).ThenBy(x => x.TotalHours).ThenBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.FirstName).ToList();
        }

        public void CreateFIFOScheduleFromAutoScheduledValues()
        {
            // turn auto scheduled values into plan review object
            // pass plan review object to upsert fifo in fifo adapter

            AppointmentResponseStatusModelBO appointmentResponse = new AppointmentResponseStatusModelBO();

            FIFOAdapter fifoadapter = new FIFOAdapter();

            PlanReview planReview = new PlanReview();
            planReview.ProjectScheduleRefEnum = ProjectScheduleRefEnum.FIFO;
            planReview.IsReschedule = true;
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
            planReview.UpdateProjectStatus = true;

            fifoadapter.UpsertFIFO(planReview);
        }

        #endregion
    }
}