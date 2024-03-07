
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models.Base;
using AION.Engine.BusinessEntities;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Engines
{
    public class PlanReviewProjectSchedulingEngine : BaseSchedulingEngine
    {
        AutoScheduledPlanReviewParams RequestData;

        public bool IsPoolProjectType { get; set; }

        public PlanReviewAutoSchedulableReviewer SelectedBuildingReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedElectricReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedMechReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedPlumbReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedZoneReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedFireReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedFoodServiceReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedPoolReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedFacilityReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedDayCareReviewer;
        public PlanReviewAutoSchedulableReviewer SelectedBackFlowReviewer;

        /// <summary>
        /// 15 or 3 pm default
        /// </summary>
        public static int AllowedLastScheduleStartTime { get; set; } = 15; // 3 PM
        public DateTime AutoSchedulePeriodStart { get; set; } = DateTime.Now.AddDays(2); //look at least 2 days in the future for auto scheduling.

        //LES-1206
        //1) Plan Reviewer must meet the occupancy  for the project in order to be scheduled to the project. or plan reviewer has the matching  level.
        //2) ? = Schedule plan reviewer to their hours availably by day for role/project type
        /*
         * first look at MISC CONFIG ->
         * Plan Reviewer - Non MMF:
            (Commercial
            Special Projects Team
            Townhomes)

            Mega Multi Family:

            County Shop:

        LOOK FOR MAION TAB -> Project Type: -> Reviewer Specific Plan Review Hours Availability By Day:.
            */
        //3) ? = Schedule to reviewer specific plan review hour available by day if set to a different number
        //Ans:://Included in above #2
        //4) ? = Adjust to the scheduling multiplier. Is this the only
        //Ans:://Included in above #2
        //5) Project Types: Commercial, Mega Multi-Family, Special Projects Team, Townhomes, County Shop drawings (if auto schedule is chose instead of assigning as pool, In this case there is only Fire DEPT)
        //6) ? Is there time associated with meeting or is it just start date and end date?
        //Ans:://NO time its only 6 hrs or assume hrs in a day.

        //LES-177
        //7) ? = The Plan review time plus the meeting time in a day will be set to a maximum of 8 hours for systemic scheduling.
        //Ans:: // 8 hr max..
        //8) ? = How to manage this two together?
        // You cannot interrupt the plan review of 1 project with another project.
        // Multiple plan review projects can be scheduled to be completed in the same day as long as time permits.
        // Do we need to calculate meeting duration in a day in above case?
        // Does it comes across multiple days for one dept?
        //Ans::
        //The first slot cannot be overlapping with another project until done.
        //If yes then look for another guy. if not available anyone before he can have a slot fully then go back to first guy.
        public PlanReviewProjectSchedulingEngine(AutoScheduledPlanReviewParams data)
        {
            TimeSlotIntervalByMinutes = 15;
            RequestData = data;

            //TO REMOVE: SlotsInTheDay = (int)((decimal)(AllowedLastScheduleStartTime - AllowedStartTime) / ((decimal)TimeSlotIntervalByMinutes / (decimal)60));
            //These days must be excluded from scheduling
            if (HolidayList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
                HolidayList = GetHolidays();
            // if (WeekEndList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
            //   WeekEndList = GetAllWeekEnds(AutoSchedulePeriodStart, AutoSchedulePeriodStart.AddYears(5)); //there is an end date requirement of unlimited days. So nned to make this in multiple phases. TBD later.
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(data.AccelaProjectIDRef);
            SetCycleData(RequestData.Cycle);
            GetAutoScheduleStartDate();
            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.Commercial || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Townhomes || CurrentProject.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                IsPoolProjectType = true;

            AllEligibleReviewers = GetAllEligibleReviewers();
            SplitEligibleReviewersByDept();

            GetMultipliers();
            CalculateMeetingDurationWithReReviewHours();
        }

        public PlanReviewProjectSchedulingEngine(SchedulePlanReviewCapacityParams data)
        {
            RequestData = data;

            if (HolidayList == null)
                HolidayList = GetHolidays();
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(data.AccelaProjectIDRef);
            SetCycleData(RequestData.Cycle);
            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.Commercial || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Townhomes || CurrentProject.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                IsPoolProjectType = true;
            AllEligibleReviewers = GetAllPrelimEstimatorsForManualCapacity();
            SplitEligibleReviewersByDept();
            GetMultipliers();
            CalculateMeetingDurationWithReReviewHours();
        }

        /// <summary>
        /// Used to build scheduling lead time report LES-303
        /// Returns time slots
        /// </summary>
        /// <param name="data"></param>
        public PlanReviewProjectSchedulingEngine(AutoScheduleReportParams data)
        {
            TimeSlotIntervalByMinutes = 15;
            RequestData = new AutoScheduledPlanReviewParams
            {
                CurrentProject = data.CurrentProject

            };

            if (HolidayList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
                HolidayList = GetHolidays();

            CurrentProject = RequestData.CurrentProject;

            AutoSchedulePeriodStart = data.ManualStartDateTime.Value;

            if (CurrentProject.AccelaPropertyType == PropertyTypeEnums.Commercial || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team || CurrentProject.AccelaPropertyType == PropertyTypeEnums.Townhomes || CurrentProject.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                IsPoolProjectType = true;

            MeetingDuration = data.ReviewHours;
            MeetingDurationBackFlow = MeetingDuration;
            MeetingDurationBuild = MeetingDuration;
            MeetingDurationDayCare = MeetingDuration;
            MeetingDurationElectric = MeetingDuration;
            MeetingDurationFacility = MeetingDuration;
            MeetingDurationFire = MeetingDuration;
            MeetingDurationFood = MeetingDuration;
            MeetingDurationMech = MeetingDuration;
            MeetingDurationPlumb = MeetingDuration;
            MeetingDurationPool = MeetingDuration;
            MeetingDurationZone = MeetingDuration;

            AllEligibleReviewers = GetAllEligibleReviewers();
            SplitEligibleReviewersByDept();
            GetMultipliers();

        }
        public List<TimeSlot> GetPlanReviewAutoEstimatedValuesForReport()
        {
            DateTime? startDt = AutoSchedulePeriodStart;
            bool AllocationFound = false;
            //cap initial max search at 2 years out
            DateTime AllowedMaxEndDate = AutoSchedulePeriodStart.AddYears(2);
            PlanReviewAutoSchedulableReviewer Reviewer;

            DateRange dateRange = new DateRange();
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            AllocationFound = false;
            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

            foreach (ProjectAgency item in CurrentProject.Agencies.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    int returnReviewer = GetTradeReviewerID(GetTradeReviewersForReport(item.DepartmentInfo), item, out SelectedReviewer);
                    if (SelectedReviewer != null)
                        SelectedReviewer.PlanReviewerDept = item.DepartmentInfo;

                    Reviewer = SelectedReviewer;
                    if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDuration, start, end);
                        if (AllocationFound == true)
                        {
                            TimeSlot ts = new TimeSlot
                            {
                                DepartmentName = item.DepartmentInfo,
                                StartTime = Reviewer.AllotedStartDt.Value
                            };

                            timeSlots.Add(ts);

                        }
                    }
                }
            }

            foreach (ProjectTrade item in CurrentProject.Trades.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    int returnReviewer = GetTradeReviewerID(GetTradeReviewersForReport(item.DepartmentInfo), item, out SelectedReviewer);
                    if (SelectedReviewer != null)
                        SelectedReviewer.PlanReviewerDept = item.DepartmentInfo;

                    Reviewer = SelectedReviewer;
                    if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDuration, start, end);
                        if (AllocationFound == true)
                        {
                            TimeSlot ts = new TimeSlot
                            {
                                DepartmentName = item.DepartmentInfo,
                                StartTime = Reviewer.AllotedStartDt.Value
                            };

                            timeSlots.Add(ts);
                        }
                    }
                }
            }

            return timeSlots;
        }

        private void GetAutoScheduleStartDate()
        {
            if (RequestData.IsFutureCycle)
            {
                AutoSchedulePeriodStart = RequestData.ScheduleAfterDate;
                return;
            }

            if (RequestData.isSelfSchedule)
            {
                AutoSchedulePeriodStart = RequestData.selfScheduleDate;
                return;
            }

            if (RequestData.PlansReadyOnDate.HasValue)
            {
                AutoSchedulePeriodStart = RequestData.PlansReadyOnDate.Value;
                //Business rule - begin auto schedule two business days after PROD
                for (int i = 0; i < 2; i++)
                    AutoSchedulePeriodStart = NextWorkingDay(AutoSchedulePeriodStart);

                //JCL: check if this is in the past, then set to the next working day available in the future
                DateTime today = DateTime.Now.Date;
                if (AutoSchedulePeriodStart.Date <= today)
                {
                    AutoSchedulePeriodStart = NextWorkingDay(today);
                }

            }
            else throw new Exception("Plans Ready On Date missing - enter on the project details page for this cycle.");
        }

        private void CalculateMeetingDurationWithReReviewHours()
        {
            foreach (var item in CurrentProject.Trades)
            {
                ProjectCycleDetail sbr = ProjectCycleDetails.Where(x => x.BusinessRefId == (int)item.DepartmentInfo).FirstOrDefault();
                bool isSbr = sbr != null;
                decimal reReviewHours = isSbr ? sbr.RereviewHoursNbr.HasValue ? sbr.RereviewHoursNbr.Value : 0 : 0;
                decimal estimationHours = item.EstimationHours.HasValue ? item.EstimationHours.Value : 0;
                decimal hours = GetTheHours(isSbr, item.DepartmentInfo, reReviewHours, estimationHours);

                if (hours == 0) continue;

                switch (item.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:
                        MeetingDurationBuild = hours;
                        break;
                    case DepartmentNameEnums.Electrical:
                        MeetingDurationElectric = hours;
                        break;
                    case DepartmentNameEnums.Mechanical:
                        MeetingDurationMech = hours;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        MeetingDurationPlumb = hours;
                        break;
                }
            }

            foreach (var item in CurrentProject.Agencies)
            {
                ProjectCycleDetail sbr = ProjectCycleDetails.Where(x => x.BusinessRefId == (int)item.DepartmentInfo).FirstOrDefault();
                bool isSbr = sbr != null;
                decimal reReviewHours = isSbr ? sbr.RereviewHoursNbr.HasValue ? sbr.RereviewHoursNbr.Value : 0 : 0;
                decimal estimationHours = item.EstimationHours.HasValue ? item.EstimationHours.Value : 0;
                decimal hours = GetTheHours(isSbr, item.DepartmentInfo, reReviewHours, estimationHours);

                if (hours == 0) continue;

                switch (item.DepartmentInfo)
                {
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        MeetingDurationZone = hours;
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
                        MeetingDurationFire = hours;
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        MeetingDurationDayCare = hours;
                        break;
                    case DepartmentNameEnums.EH_Food:
                        MeetingDurationFood = hours;
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        MeetingDurationPool = hours;
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        MeetingDurationFacility = hours;
                        break;
                    case DepartmentNameEnums.Backflow:
                        MeetingDurationBackFlow = hours;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Get the hours for the trade/agency : 
        /// used by CalculateMeetingDuration
        /// </summary>
        /// <param name="isSbr"></param>
        /// <param name="itemDeptInfo"></param>
        /// <param name="proposedHours"></param>
        /// <param name="reReviewHours"></param>
        /// <param name="estimationHours"></param>
        /// <returns></returns>
        private decimal GetTheHours(bool isSbr, DepartmentNameEnums itemDeptInfo, decimal reReviewHours, decimal estimationHours)
        {
            decimal hours = 0;
            if (RequestData.IsFutureCycle)
            {
                PlanReviewScheduleDetail detail = PlanReviewScheduleDetails.FirstOrDefault(x => x.BusinessRefId == (int)itemDeptInfo);
                if (detail != null)
                {
                    hours = detail.AssignedHoursNbr.Value;
                }
            }
            else if (RequestData.isActivateNAReview)
            {
                switch (itemDeptInfo)
                {
                    case DepartmentNameEnums.Building:
                        hours = RequestData.UpdatedBuildingHours;
                        break;
                    case DepartmentNameEnums.Electrical:
                        hours = RequestData.UpdatedElectricHours;
                        break;
                    case DepartmentNameEnums.Mechanical:
                        hours = RequestData.UpdatedMechHours;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        hours = RequestData.UpdatedPlumbHours;
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
                        hours = RequestData.UpdatedZoneHours;
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
                        hours = RequestData.UpdatedFireHours;
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        hours = RequestData.UpdatedDayCareHours;
                        break;
                    case DepartmentNameEnums.EH_Food:
                        hours = RequestData.UpdatedFoodHours;
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        hours = RequestData.UpdatedPoolHours;
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        hours = RequestData.UpdatedLodgeHours;
                        break;
                    case DepartmentNameEnums.Backflow:
                        hours = RequestData.UpdatedBackflowHours;
                        break;
                    default:
                        break;
                }
            }
            else if (isSbr)
            {
                hours = reReviewHours;
            }
            else
            {
                hours = estimationHours;
            }

            return hours;
        }

        /*
             * The USER list at this point will NOT include any users that are
             * not planreviewer as per user role (system_role.ID = 2 FK)
             * and any plan reviewer who is set as 'N' in UI -> Admin.User.Schedulable
             * and 'N' in UI -> admin.user.Prelimunary Meeting setting.
             * Must have matching occupancy or level selected to the project
             *
            HIGH LEVEL ALGORITUM.
            * Get the current free allocations for each user from DB.
            *  for each user;
            *       remove the unallocatable parts(8am-5pm, holiday's weekends etc)
            *       split the allocations by 15 min slots.
            *
            *
            *  get the project's required departments.
            *  Create a new group of users by each dept.
            *
            *  remove excluded for this dept on proj from list..
            *
            *  If one person is there in multiple depts they can be considered as overlapping but when done
            *  do not send invite for each dept, but just one and he need to allocated only once in table..
            *
            *  primary -> secondary → requested are selected on order and they are selected only if they do satisfy all of the below conditions. the first person who satisfies all these will be picked .
            *
            *    Plan Reviewer must meet the occupancy or level setting for the project in order to be scheduled to the project.
            *
            *    Scheduleble = 'Y”
            *
            *    Preliminary Meeting = 'Y'
            *
            *    Role = plan reviewer
            *
            *    additional rules.
            *
            *    if none of the persons from above three passes then algorithm will pick the least allocated reviewer from list.
            *
            *    Once all the departments have a reviewer selected then algorithm will start looking for earliest slot where all of them are free. and allocate meeting at that point.
            *
            *   Must have matching occupancy or level selected to the project
            *
            *   Once scheduled it is removed from the scheduling dashboard.
            *
            *
            */

        public bool ManualScheduleCapacity(SchedulePlanReviewCapacityParams parms)
        {
            Dictionary<DepartmentNameEnums, double> orderedDepartments = new Dictionary<DepartmentNameEnums, double>();
            Helper helper = new Helper();

            if (parms.BuildingUserID > 0 && parms.BuildingHours > 0 && parms.BuildingIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleBuildingReviewersList, curtrade, out SelectedBuildingReviewer);
                if (SelectedBuildingReviewer != null)
                {
                    SelectedBuildingReviewer.PlanReviewerDept = DepartmentNameEnums.Building;
                    orderedDepartments.Add(DepartmentNameEnums.Building, parms.BuildingScheduleEnd.Value.Subtract(parms.BuildingScheduleStart.Value).TotalDays);
                }
            }
            if (parms.ElectricUserID > 0 && parms.ElectricHours > 0 && parms.ElectricIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleElectricReviewersList, curtrade, out SelectedElectricReviewer);
                if (SelectedElectricReviewer != null)
                {
                    SelectedElectricReviewer.PlanReviewerDept = DepartmentNameEnums.Electrical;
                    orderedDepartments.Add(DepartmentNameEnums.Electrical, parms.ElectricScheduleEnd.Value.Subtract(parms.ElectricScheduleStart.Value).TotalDays);
                }
            }
            if (parms.MechUserID > 0 && parms.MechHours > 0 && parms.MechIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleMechReviewersList, curtrade, out SelectedMechReviewer);
                if (SelectedMechReviewer != null)
                {
                    SelectedMechReviewer.PlanReviewerDept = DepartmentNameEnums.Mechanical;
                    orderedDepartments.Add(DepartmentNameEnums.Mechanical, parms.MechScheduleEnd.Value.Subtract(parms.MechScheduleStart.Value).TotalDays);
                }
            }
            if (parms.PlumbUserID > 0 && parms.PlumbHours > 0 && parms.PlumbIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligiblePlumbReviewersList, curtrade, out SelectedPlumbReviewer);
                if (SelectedPlumbReviewer != null)
                {
                    SelectedPlumbReviewer.PlanReviewerDept = DepartmentNameEnums.Plumbing;
                    orderedDepartments.Add(DepartmentNameEnums.Plumbing, parms.PlumbScheduleEnd.Value.Subtract(parms.PlumbScheduleStart.Value).TotalDays);
                }
            }
            if (parms.ZoneUserID > 0 && parms.ZoneHours > 0 && parms.ZoneIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleZoneReviewersList, curtrade, out SelectedZoneReviewer);
                if (SelectedZoneReviewer != null)
                {
                    SelectedZoneReviewer.PlanReviewerDept = curtrade.DepartmentInfo;
                    orderedDepartments.Add(curtrade.DepartmentInfo, parms.ZoneScheduleEnd.Value.Subtract(parms.ZoneScheduleStart.Value).TotalDays);
                }
            }
            if (parms.FireUserID > 0 && parms.FireHours > 0 && parms.FireIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleFireReviewersList, curtrade, out SelectedFireReviewer);
                if (SelectedFireReviewer != null)
                {
                    SelectedFireReviewer.PlanReviewerDept = curtrade.DepartmentInfo;
                    orderedDepartments.Add(curtrade.DepartmentInfo, parms.FireScheduleEnd.Value.Subtract(parms.FireScheduleStart.Value).TotalDays);
                }
            }
            if (parms.BackFlowUserID > 0 && parms.BackFlowHours > 0 && parms.BackFlowIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleBackFlowReviewersList, curtrade, out SelectedBackFlowReviewer);
                if (SelectedBackFlowReviewer != null)
                {
                    SelectedBackFlowReviewer.PlanReviewerDept = DepartmentNameEnums.Backflow;
                    orderedDepartments.Add(DepartmentNameEnums.Backflow, parms.BackFlowScheduleEnd.Value.Subtract(parms.BackFlowScheduleStart.Value).TotalDays);
                }
            }
            if (parms.FoodServiceUserID > 0 && parms.FoodServiceHours > 0 && parms.FoodServiceIsPool == false)
            {

                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleFoodServiceReviewersList, curtrade, out SelectedFoodServiceReviewer);
                if (SelectedFoodServiceReviewer != null)
                {
                    SelectedFoodServiceReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Food;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Food, parms.FoodScheduleEnd.Value.Subtract(parms.FoodScheduleStart.Value).TotalDays);
                }
            }
            if (parms.PoolUserID > 0 && parms.PoolHours > 0 && parms.PoolIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligiblePoolReviewersList, curtrade, out SelectedPoolReviewer);
                if (SelectedPoolReviewer != null)
                {
                    SelectedPoolReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Pool;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Pool, parms.PoolScheduleEnd.Value.Subtract(parms.PoolScheduleStart.Value).TotalDays);
                }
            }
            if (parms.FacilityUserID > 0 && parms.FacilityHours > 0 && parms.FacilityIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleFacilityReviewersList, curtrade, out SelectedFacilityReviewer);
                if (SelectedFacilityReviewer != null)
                {
                    SelectedFacilityReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Facilities;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Facilities, parms.FacilityScheduleEnd.Value.Subtract(parms.FacilityScheduleStart.Value).TotalDays);
                }
            }
            if (parms.DayCareUserID > 0 && parms.DayCareHours > 0 && parms.DayCareIsPool == false)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleDayCareReviewersList, curtrade, out SelectedDayCareReviewer);
                if (SelectedDayCareReviewer != null)
                {
                    SelectedDayCareReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Day_Care;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Day_Care, parms.DayCareScheduleEnd.Value.Subtract(parms.DayCareScheduleStart.Value).TotalDays);
                }
            }

            bool AllocationFound = false;
            PlanReviewAutoSchedulableReviewer Reviewer;

            AllocationFound = true;

            //order the reviewers by smallest timeframe to largest for proper allocation

            foreach (KeyValuePair<DepartmentNameEnums, double> item in orderedDepartments.OrderBy(x => x.Value))
            {
                switch (item.Key)
                {
                    case DepartmentNameEnums.Building:
                        Reviewer = SelectedBuildingReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.BuildingIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationBuild, parms.BuildingScheduleStart.Value, parms.BuildingScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.BuildingScheduleStart && Reviewer.AllotedEndDt <= parms.BuildingScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Building);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.Electrical:
                        Reviewer = SelectedElectricReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.ElectricIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationElectric, parms.ElectricScheduleStart.Value, parms.ElectricScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.ElectricScheduleStart && Reviewer.AllotedEndDt <= parms.ElectricScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Electrical);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.Mechanical:
                        Reviewer = SelectedMechReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.MechIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationMech, parms.MechScheduleStart.Value, parms.MechScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.MechScheduleStart && Reviewer.AllotedEndDt <= parms.MechScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Mechanical);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }

                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.Plumbing:
                        Reviewer = SelectedPlumbReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.PlumbIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationPlumb, parms.PlumbScheduleStart.Value, parms.PlumbScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.PlumbScheduleStart && Reviewer.AllotedEndDt <= parms.PlumbScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Plumbing);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }

                                else return false;
                            }
                        }
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
                        Reviewer = SelectedZoneReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.ZoneIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationZone, parms.ZoneScheduleStart.Value, parms.ZoneScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.ZoneScheduleStart && Reviewer.AllotedEndDt <= parms.ZoneScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = Reviewer.PlanReviewerDept);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
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
                        Reviewer = SelectedFireReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.FireIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFire, parms.FireScheduleStart.Value, parms.FireScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.FireScheduleStart && Reviewer.AllotedEndDt <= parms.FireScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = Reviewer.PlanReviewerDept);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.Backflow:
                        Reviewer = SelectedBackFlowReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.BackFlowIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationBackFlow, parms.BackFlowScheduleStart.Value, parms.BackFlowScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.BackFlowScheduleStart && Reviewer.AllotedEndDt <= parms.BackFlowScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Backflow);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.EH_Food:
                        Reviewer = SelectedFoodServiceReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.FoodServiceIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFood, parms.FoodScheduleStart.Value, parms.FoodScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.FoodScheduleStart && Reviewer.AllotedEndDt <= parms.FoodScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Food);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        Reviewer = SelectedPoolReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.PoolIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationPool, parms.PoolScheduleStart.Value, parms.PoolScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.PoolScheduleStart && Reviewer.AllotedEndDt <= parms.PoolScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Pool);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        Reviewer = SelectedFacilityReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.FacilityIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFacility, parms.FacilityScheduleStart.Value, parms.FacilityScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.FacilityScheduleStart && Reviewer.AllotedEndDt <= parms.FacilityScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Facilities);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        Reviewer = SelectedDayCareReviewer;
                        if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                        {
                            //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                            if (!(RequestData.DayCareIsPool == true && IsPoolProjectType == true))
                            {
                                AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationDayCare, parms.DayCareScheduleStart.Value, parms.DayCareScheduleEnd.Value);
                                if (AllocationFound == true)
                                {
                                    //if the dates found aren't valid, return false - these dates cause a conflict
                                    if (!(Reviewer.AllotedStartDt >= parms.DayCareScheduleStart && Reviewer.AllotedEndDt <= parms.DayCareScheduleEnd.Value.AddDays(1))) return false;
                                    Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Day_Care);
                                    /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                    Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                    /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                    Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                }
                                else return false;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }

            return AllocationFound;
        }

        public List<DateTime> SearchSelfScheduleCapacity(SchedulePlanReviewCapacityParams parms)
        {
            Dictionary<DepartmentNameEnums, double> orderedDepartments = new Dictionary<DepartmentNameEnums, double>();
            Helper helper = new Helper();
            if (parms.BuildingUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleBuildingReviewersList, curtrade, out SelectedBuildingReviewer);
                if (SelectedBuildingReviewer != null)
                {
                    SelectedBuildingReviewer.PlanReviewerDept = DepartmentNameEnums.Building;
                    orderedDepartments.Add(DepartmentNameEnums.Building, parms.BuildingScheduleEnd.Value.Subtract(parms.BuildingScheduleStart.Value).TotalDays);
                }
            }
            if (parms.ElectricUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleElectricReviewersList, curtrade, out SelectedElectricReviewer);
                if (SelectedElectricReviewer != null)
                {
                    SelectedElectricReviewer.PlanReviewerDept = DepartmentNameEnums.Electrical;
                    orderedDepartments.Add(DepartmentNameEnums.Electrical, parms.ElectricScheduleEnd.Value.Subtract(parms.ElectricScheduleStart.Value).TotalDays);
                }
            }
            if (parms.MechUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleMechReviewersList, curtrade, out SelectedMechReviewer);
                if (SelectedMechReviewer != null)
                {
                    SelectedMechReviewer.PlanReviewerDept = DepartmentNameEnums.Mechanical;
                    orderedDepartments.Add(DepartmentNameEnums.Mechanical, parms.MechScheduleEnd.Value.Subtract(parms.MechScheduleStart.Value).TotalDays);
                }
            }
            if (parms.PlumbUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligiblePlumbReviewersList, curtrade, out SelectedPlumbReviewer);
                if (SelectedPlumbReviewer != null)
                {
                    SelectedPlumbReviewer.PlanReviewerDept = DepartmentNameEnums.Plumbing;
                    orderedDepartments.Add(DepartmentNameEnums.Plumbing, parms.PlumbScheduleEnd.Value.Subtract(parms.PlumbScheduleStart.Value).TotalDays);
                }
            }
            if (parms.ZoneUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleZoneReviewersList, curtrade, out SelectedZoneReviewer);
                if (SelectedZoneReviewer != null)
                {
                    SelectedZoneReviewer.PlanReviewerDept = curtrade.DepartmentInfo;
                    orderedDepartments.Add(curtrade.DepartmentInfo, parms.ZoneScheduleEnd.Value.Subtract(parms.ZoneScheduleStart.Value).TotalDays);
                }
            }
            if (parms.FireUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleFireReviewersList, curtrade, out SelectedFireReviewer);
                if (SelectedFireReviewer != null)
                {
                    SelectedFireReviewer.PlanReviewerDept = curtrade.DepartmentInfo;
                    orderedDepartments.Add(curtrade.DepartmentInfo, parms.FireScheduleEnd.Value.Subtract(parms.FireScheduleStart.Value).TotalDays);
                }
            }
            if (parms.BackFlowUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleBackFlowReviewersList, curtrade, out SelectedBackFlowReviewer);
                if (SelectedBackFlowReviewer != null)
                {
                    SelectedBackFlowReviewer.PlanReviewerDept = DepartmentNameEnums.Backflow;
                    orderedDepartments.Add(DepartmentNameEnums.Backflow, parms.BackFlowScheduleEnd.Value.Subtract(parms.BackFlowScheduleStart.Value).TotalDays);
                }
            }
            if (parms.FoodServiceUserID > 0)
            {

                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleFoodServiceReviewersList, curtrade, out SelectedFoodServiceReviewer);
                if (SelectedFoodServiceReviewer != null)
                {
                    SelectedFoodServiceReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Food;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Food, parms.FoodScheduleEnd.Value.Subtract(parms.FoodScheduleStart.Value).TotalDays);
                }
            }
            if (parms.PoolUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligiblePoolReviewersList, curtrade, out SelectedPoolReviewer);
                if (SelectedPoolReviewer != null)
                {
                    SelectedPoolReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Pool;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Pool, parms.PoolScheduleEnd.Value.Subtract(parms.PoolScheduleStart.Value).TotalDays);
                }
            }
            if (parms.FacilityUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleFacilityReviewersList, curtrade, out SelectedFacilityReviewer);
                if (SelectedFacilityReviewer != null)
                {
                    SelectedFacilityReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Facilities;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Facilities, parms.FacilityScheduleEnd.Value.Subtract(parms.FacilityScheduleStart.Value).TotalDays);
                }
            }
            if (parms.DayCareUserID > 0)
            {
                ProjectDepartment curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
                GetTradeReviewerIDForManualCapacity(EligibleDayCareReviewersList, curtrade, out SelectedDayCareReviewer);
                if (SelectedDayCareReviewer != null)
                {
                    SelectedDayCareReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Day_Care;
                    orderedDepartments.Add(DepartmentNameEnums.EH_Day_Care, parms.DayCareScheduleEnd.Value.Subtract(parms.DayCareScheduleStart.Value).TotalDays);
                }
            }
            //is this a working day? change to next working day if not
            bool isHolidayOrWeekend = IsDateHolidayOrWeekEnd(parms.BuildingScheduleStart.Value);
            DateTime? startDt = (!isHolidayOrWeekend) ? parms.BuildingScheduleStart.Value : NextWorkingDay(parms.BuildingScheduleStart.Value);

            isHolidayOrWeekend = IsDateHolidayOrWeekEnd(parms.BuildingScheduleEnd.Value);
            DateTime endDt = (!isHolidayOrWeekend) ? parms.BuildingScheduleEnd.Value : NextWorkingDay(parms.BuildingScheduleEnd.Value);

            bool AllocationFound = false;

            DateTime AllowedMaxEndDate = endDt;
            PlanReviewAutoSchedulableReviewer Reviewer;
            bool datesInRange = false;
            AutoScheduledPlanReviewValues ret = new AutoScheduledPlanReviewValues();

            List<DateTime> foundDates = new List<DateTime>();
            //if the dates are out of range after working day adjustment, return
            if (startDt > endDt)
            {
                return foundDates;
            }

            do
            {
                AllowedMaxEndDate = endDt;
                AllocationFound = false;
                Reviewer = SelectedBuildingReviewer;

                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                    if (!(RequestData.BuildingIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationBuild, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.BuildingHours = MeetingDurationBuild;
                            ret.BuildingScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method. 
                            ret.BuildingScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Building);
                            /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.BuildingScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedMechReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.MechIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationMech, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.MechHours = MeetingDurationMech;
                            ret.MechScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.MechScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Mechanical);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.MechScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedElectricReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.ElectricIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationElectric, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.ElectricHours = MeetingDurationElectric;
                            ret.ElectricScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.ElectricScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Electrical);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.ElectricScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedPlumbReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.PlumbIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationPlumb, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.PlumbHours = MeetingDurationPlumb;
                            ret.PlumbScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.PlumbScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Plumbing);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.PlumbScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedFireReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.FireIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFire, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.FireHours = MeetingDurationFire;
                            ret.FireScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.FireScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = Reviewer.PlanReviewerDept);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.FireScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedZoneReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.ZoneIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationZone, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.ZoneHours = MeetingDurationZone;
                            ret.ZoneScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.ZoneScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = Reviewer.PlanReviewerDept);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.ZoneScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedPoolReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.PoolIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationPool, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.PoolHours = MeetingDurationPool;
                            ret.PoolScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.PoolScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Pool);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.PoolScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }


                Reviewer = SelectedFacilityReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.FacilityIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFacility, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.FacilityHours = MeetingDurationFacility;
                            ret.FacilityScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.FacilityScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Facilities);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.FacilityScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedDayCareReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.DayCareIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationDayCare, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.DayCareHours = MeetingDurationDayCare;
                            ret.DayCareScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.DayCareScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Day_Care);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.DayCareScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedFoodServiceReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.FoodServiceIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFood, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.FoodServiceHours = MeetingDurationFood;
                            ret.FoodScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.FoodScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Food);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.FoodScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                Reviewer = SelectedBackFlowReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.BackFlowIsPool == true && IsPoolProjectType == true))
                    {
                        AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationBackFlow, startDt.Value, AllowedMaxEndDate);
                        if (AllocationFound == true)
                        {
                            ret.BackFlowHours = MeetingDurationBackFlow;
                            ret.BackFlowScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                            ret.BackFlowScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                            Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Backflow);
                            Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                            AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.BackFlowScheduleStart.Value, AllowedMaxEndDate);
                        }
                    }
                }

                if (AllocationFound == false && //means the loop coudn't get into any of the dept
                                                //If none of the depts are selected then return defaults since ther is no reviewer or there is no hrs calculated.
                    ret.BackFlowScheduleEnd.HasValue == false && ret.BackFlowScheduleStart.HasValue == false
                    && ret.BuildingScheduleEnd.HasValue == false && ret.BuildingScheduleStart.HasValue == false
                    && ret.DayCareScheduleEnd.HasValue == false && ret.DayCareScheduleStart.HasValue == false
                    && ret.ElectricScheduleEnd.HasValue == false && ret.ElectricScheduleStart.HasValue == false
                    && ret.FacilityScheduleEnd.HasValue == false && ret.FacilityScheduleStart.HasValue == false
                    && ret.FireScheduleEnd.HasValue == false && ret.FireScheduleStart.HasValue == false
                    && ret.FoodScheduleEnd.HasValue == false && ret.FoodScheduleStart.HasValue == false
                    && ret.MechScheduleEnd.HasValue == false && ret.MechScheduleStart.HasValue == false
                    && ret.PlumbScheduleEnd.HasValue == false && ret.PlumbScheduleStart.HasValue == false
                    && ret.PoolScheduleEnd.HasValue == false && ret.PoolScheduleStart.HasValue == false
                    && ret.ZoneScheduleEnd.HasValue == false && ret.ZoneScheduleStart.HasValue == false)
                {
                    //add working day
                    startDt = NextWorkingDay(startDt.Value);

                    continue;
                }

                if (AllocationFound == true)
                    datesInRange = IsAllTradesAndAgenciesScheduled(ret, RequestData);
                else
                    datesInRange = false; //marks explicilty for next loop since one of the dept went past 7th day possibly resulting in ContinuetoNextDay = true 

                if (SelectedBuildingReviewer != null && SelectedBuildingReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedBuildingReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedBuildingReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedBuildingReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedMechReviewer != null && SelectedMechReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedMechReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedMechReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedMechReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedElectricReviewer != null && SelectedElectricReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedElectricReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedElectricReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedElectricReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedPlumbReviewer != null && SelectedPlumbReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedPlumbReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedPlumbReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedPlumbReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedFireReviewer != null && SelectedFireReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedFireReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedFireReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedFireReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedZoneReviewer != null && SelectedZoneReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedZoneReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedZoneReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedZoneReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedPoolReviewer != null && SelectedPoolReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedPoolReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedPoolReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedPoolReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedFacilityReviewer != null && SelectedFacilityReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedFacilityReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedFacilityReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedFacilityReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedDayCareReviewer != null && SelectedDayCareReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedDayCareReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedDayCareReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedDayCareReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedFoodServiceReviewer != null && SelectedFoodServiceReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedFoodServiceReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedFoodServiceReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedFoodServiceReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }
                if (SelectedBackFlowReviewer != null && SelectedBackFlowReviewer.WIPReviewerTimeSlots != null)
                {
                    SelectedBackFlowReviewer.WIPReviewerTimeSlots.Clear();
                    SelectedBackFlowReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                    SelectedBackFlowReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                }

                if (datesInRange)
                {
                    foundDates.Add(startDt.Value);
                }
                //add working day
                startDt = NextWorkingDay(startDt.Value);

            } while (startDt <= endDt);

            return foundDates;
        }

        public AutoScheduledPlanReviewValues GetAutoEstimatedValues()
        {
            AutoScheduledPlanReviewValues ret = new AutoScheduledPlanReviewValues();
            Helper helper = new Helper();
            if (AllEligibleReviewers.Count == 0) { ret.ErrorMessage = "No Eligible Reviewers Found! Check user settings for scheduling eligibility."; return ret; }

            //Select final reviewers
            ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            if (curtrade == null || EligibleBuildingReviewersList == null || MeetingDurationBuild <= 0)
            {
                ret.BuildingUserID = -1;
            }
            else
            {
                ret.BuildingUserID = GetTradeReviewerID(EligibleBuildingReviewersList, curtrade, out SelectedBuildingReviewer);
                if (SelectedBuildingReviewer != null)
                    SelectedBuildingReviewer.PlanReviewerDept = DepartmentNameEnums.Building;
            }

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
            if (curtrade == null || EligibleElectricReviewersList == null || MeetingDurationElectric <= 0)
            {
                ret.ElectricUserID = -1;
            }
            else
            {
                ret.ElectricUserID = GetTradeReviewerID(EligibleElectricReviewersList, curtrade, out SelectedElectricReviewer);
                if (SelectedElectricReviewer != null)
                    SelectedElectricReviewer.PlanReviewerDept = DepartmentNameEnums.Electrical;
            }

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
            if (curtrade == null || EligibleMechReviewersList == null || MeetingDurationMech <= 0)
            {
                ret.MechUserID = -1;
            }
            else
            {
                ret.MechUserID = GetTradeReviewerID(EligibleMechReviewersList, curtrade, out SelectedMechReviewer);
                if (SelectedMechReviewer != null)
                    SelectedMechReviewer.PlanReviewerDept = DepartmentNameEnums.Mechanical;
            }

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
            if (curtrade == null || EligiblePlumbReviewersList == null || MeetingDurationPlumb <= 0)
            {
                ret.PlumbUserID = -1;
            }
            else
            {
                ret.PlumbUserID = GetTradeReviewerID(EligiblePlumbReviewersList, curtrade, out SelectedPlumbReviewer);
                if (SelectedPlumbReviewer != null)
                    SelectedPlumbReviewer.PlanReviewerDept = DepartmentNameEnums.Plumbing;
            }

            curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || EligibleZoneReviewersList == null || MeetingDurationZone <= 0)
            {
                ret.ZoneUserID = -1;
            }
            else
            {
                ret.ZoneUserID = GetTradeReviewerID(EligibleZoneReviewersList, curtrade, out SelectedZoneReviewer);
                if (SelectedZoneReviewer != null)
                    SelectedZoneReviewer.PlanReviewerDept = curtrade.DepartmentInfo;
            }

            curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || EligibleFireReviewersList == null || MeetingDurationFire <= 0)
            {
                ret.FireUserID = -1;
            }
            else
            {
                ret.FireUserID = GetTradeReviewerID(EligibleFireReviewersList, curtrade, out SelectedFireReviewer);
                if (SelectedFireReviewer != null)
                    SelectedFireReviewer.PlanReviewerDept = curtrade.DepartmentInfo;
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
            if (curtrade == null || EligibleBackFlowReviewersList == null || MeetingDurationBackFlow <= 0)
            {
                ret.BackFlowUserID = -1;
            }
            else
            {
                ret.BackFlowUserID = GetTradeReviewerID(EligibleBackFlowReviewersList, curtrade, out SelectedBackFlowReviewer);
                if (SelectedBackFlowReviewer != null)
                    SelectedBackFlowReviewer.PlanReviewerDept = DepartmentNameEnums.Backflow;
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
            if (curtrade == null || EligibleFoodServiceReviewersList == null || MeetingDurationFood <= 0)
            {
                ret.FoodServiceUserID = -1;
            }
            else
            {
                ret.FoodServiceUserID = GetTradeReviewerID(EligibleFoodServiceReviewersList, curtrade, out SelectedFoodServiceReviewer);
                if (SelectedFoodServiceReviewer != null)
                    SelectedFoodServiceReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Food;
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
            if (curtrade == null || EligiblePoolReviewersList == null || MeetingDurationPool <= 0)
            {
                ret.PoolUserID = -1;
            }
            else
            {
                ret.PoolUserID = GetTradeReviewerID(EligiblePoolReviewersList, curtrade, out SelectedPoolReviewer);
                if (SelectedPoolReviewer != null)
                    SelectedPoolReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Pool;
            }


            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
            if (curtrade == null || EligibleFacilityReviewersList == null || MeetingDurationFacility <= 0)
            {
                ret.FacilityUserID = -1;
            }
            else
            {
                ret.FacilityUserID = GetTradeReviewerID(EligibleFacilityReviewersList, curtrade, out SelectedFacilityReviewer);
                if (SelectedFacilityReviewer != null)
                    SelectedFacilityReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Facilities;
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
            if (curtrade == null || EligibleDayCareReviewersList == null || MeetingDurationDayCare <= 0)
            {
                ret.DayCareUserID = -1;
            }
            else
            {
                ret.DayCareUserID = GetTradeReviewerID(EligibleDayCareReviewersList, curtrade, out SelectedDayCareReviewer);
                if (SelectedDayCareReviewer != null)
                    SelectedDayCareReviewer.PlanReviewerDept = DepartmentNameEnums.EH_Day_Care;
            }

            DateTime? startDt = AutoSchedulePeriodStart;
            bool AllocationFound = false;
            bool FailedAllocation = false;
            //cap initial max search at 2 years out
            DateTime AllowedMaxEndDate = AutoSchedulePeriodStart.AddYears(2);
            PlanReviewAutoSchedulableReviewer Reviewer;
            bool datesInRange = false;

            DateRange dateRange = new DateRange();

            do
            {
                AllocationFound = false;
                FailedAllocation = false;

                Reviewer = SelectedBuildingReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    //Rule: // I want to be able to mark a plan review for an individual as "pool" review so that this is just added to the list of plan reviews for them to complete and not scheduled.  This is applicable only to certain project types (IsPoolProjectType)
                    if (!(RequestData.BuildingIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationBuild > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationBuild, start, end);
                            if (AllocationFound == true)
                            {
                                ret.BuildingHours = MeetingDurationBuild;
                                ret.BuildingScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.BuildingScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Building);
                                /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }

                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedMechReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.MechIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationMech > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationMech, start, end);
                            if (AllocationFound == true)
                            {
                                ret.MechHours = MeetingDurationMech;
                                ret.MechScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.MechScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Mechanical);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedElectricReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.ElectricIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationElectric > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationElectric, start, end);
                            if (AllocationFound == true)
                            {
                                ret.ElectricHours = MeetingDurationElectric;
                                ret.ElectricScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.ElectricScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Electrical);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedPlumbReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.PlumbIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationPlumb > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationPlumb, start, end);
                            if (AllocationFound == true)
                            {
                                ret.PlumbHours = MeetingDurationPlumb;
                                ret.PlumbScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.PlumbScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Plumbing);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedFireReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.FireIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationFire > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFire, start, end);
                            if (AllocationFound == true)
                            {
                                ret.FireHours = MeetingDurationFire;
                                ret.FireScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.FireScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Fire_County);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedZoneReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.ZoneIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationZone > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationZone, start, end);
                            if (AllocationFound == true)
                            {
                                ret.ZoneHours = MeetingDurationZone;
                                ret.ZoneScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.ZoneScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Zone_County);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                                //LES-3407 jcl - if Huntersville or Mint Hill, mark Zone as pool
                                if (IsHuntersvilleOrMintHillZoning)
                                {
                                    ret.ZoneIsPool = true;
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedPoolReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.PoolIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationPool > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationPool, start, end);
                            if (AllocationFound == true)
                            {
                                ret.PoolHours = MeetingDurationPool;
                                ret.PoolScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.PoolScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Pool);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedFacilityReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.FacilityIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationFacility > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFacility, start, end);
                            if (AllocationFound == true)
                            {
                                ret.FacilityHours = MeetingDurationFacility;
                                ret.FacilityScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.FacilityScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Facilities);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedDayCareReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.DayCareIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationDayCare > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationDayCare, start, end);
                            if (AllocationFound == true)
                            {
                                ret.DayCareHours = MeetingDurationDayCare;
                                ret.DayCareScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.DayCareScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Day_Care);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedFoodServiceReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.FoodServiceIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationFood > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationFood, start, end);
                            if (AllocationFound == true)
                            {
                                ret.FoodServiceHours = MeetingDurationFood;
                                ret.FoodScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.FoodScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.EH_Food);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                Reviewer = SelectedBackFlowReviewer;
                if (Reviewer != null)//IF NA or reviewer not found then this will be null and ReviewerUserID will be -1
                {
                    if (!(RequestData.BackFlowIsPool == true && IsPoolProjectType == true))
                    {
                        if (MeetingDurationBackFlow > 0)
                        {
                            DateTime start = dateRange.Min.HasValue ? dateRange.Min.Value : startDt.Value;
                            DateTime end = dateRange.Max.HasValue ? dateRange.Max.Value : AllowedMaxEndDate;

                            AllocationFound = AllocateFirstMeetingSlotAvailable(Reviewer, MeetingDurationBackFlow, start, end);
                            if (AllocationFound == true)
                            {
                                ret.BackFlowHours = MeetingDurationBackFlow;
                                ret.BackFlowScheduleStart = Reviewer.AllotedStartDt; //assigns by ref value returned from above method.
                                ret.BackFlowScheduleEnd = Reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                                Reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Backflow);
                                Reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);
                                Reviewer.WIPReviewerTimeSlots.AddRange(Reviewer.SelectedReviewer.WIPTimeSlots);

                                //set the date range to within 5 working days of this found date
                                // ex   7/22/2022 start, then call date helper to add 5 working days
                                //this is the new date range 
                                //only do this if the daterange hasn't been set before
                                //otherwise, leave it alone
                                if (!dateRange.Min.HasValue)
                                {
                                    dateRange.Min = Reviewer.AllotedStartDt;
                                    dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);
                                }
                            }
                            else
                            {
                                FailedAllocation = true;
                            }
                        }
                    }
                }

                if (AllocationFound == false && //means the loop coudn't get into any of the dept
                //If none of the depts are selected then return defaults since ther is no reviewer or there is no hrs calculated.
                    ret.BackFlowScheduleEnd.HasValue == false && ret.BackFlowScheduleStart.HasValue == false
                    && ret.BuildingScheduleEnd.HasValue == false && ret.BuildingScheduleStart.HasValue == false
                    && ret.DayCareScheduleEnd.HasValue == false && ret.DayCareScheduleStart.HasValue == false
                    && ret.ElectricScheduleEnd.HasValue == false && ret.ElectricScheduleStart.HasValue == false
                    && ret.FacilityScheduleEnd.HasValue == false && ret.FacilityScheduleStart.HasValue == false
                    && ret.FireScheduleEnd.HasValue == false && ret.FireScheduleStart.HasValue == false
                    && ret.FoodScheduleEnd.HasValue == false && ret.FoodScheduleStart.HasValue == false
                    && ret.MechScheduleEnd.HasValue == false && ret.MechScheduleStart.HasValue == false
                    && ret.PlumbScheduleEnd.HasValue == false && ret.PlumbScheduleStart.HasValue == false
                    && ret.PoolScheduleEnd.HasValue == false && ret.PoolScheduleStart.HasValue == false
                    && ret.ZoneScheduleEnd.HasValue == false && ret.ZoneScheduleStart.HasValue == false)
                {
                    return ret;
                }

                if (AllocationFound == true && FailedAllocation == false)
                    //We don't need to do this since we are setting the range correctly in the beginning
                    //we really need to check that all the depts required were assigned hours
                    // if not, then we need to reiterate
                    //if meeting duration for trade or agency is greater than 0, then that trade or agency is required.
                    //if no reviewer for any trade or agency then false else true because all trades and agencies got scheduled within this week
                    datesInRange = IsAllTradesAndAgenciesScheduled(ret, RequestData);
                else
                    datesInRange = false; //marks explicilty for next loop since one of the dept went past 7th day possibly resulting in ContinuetoNextDay = true 
                if (datesInRange == false)
                {
                    if (SelectedBuildingReviewer != null && SelectedBuildingReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedBuildingReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedBuildingReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedBuildingReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedMechReviewer != null && SelectedMechReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedMechReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedMechReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedMechReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedElectricReviewer != null && SelectedElectricReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedElectricReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedElectricReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedElectricReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedPlumbReviewer != null && SelectedPlumbReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedPlumbReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedPlumbReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedPlumbReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedFireReviewer != null && SelectedFireReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedFireReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedFireReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedFireReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedZoneReviewer != null && SelectedZoneReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedZoneReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedZoneReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedZoneReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedPoolReviewer != null && SelectedPoolReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedPoolReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedPoolReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedPoolReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedFacilityReviewer != null && SelectedFacilityReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedFacilityReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedFacilityReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedFacilityReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedDayCareReviewer != null && SelectedDayCareReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedDayCareReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedDayCareReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedDayCareReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedFoodServiceReviewer != null && SelectedFoodServiceReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedFoodServiceReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedFoodServiceReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedFoodServiceReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                    if (SelectedBackFlowReviewer != null && SelectedBackFlowReviewer.WIPReviewerTimeSlots != null)
                    {
                        SelectedBackFlowReviewer.WIPReviewerTimeSlots.Clear();
                        SelectedBackFlowReviewer.SelectedReviewer.AllocatedTimeSlots.Clear();
                        SelectedBackFlowReviewer.SelectedReviewer.WIPTimeSlots.Clear();
                    }
                }

                //if no dates found in the current range, then start on the next working day after the daterange max
                //reset date range
                dateRange.Min = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Max.Value, 1);
                dateRange.Max = DateTimeHelper.DetermineWorkDateAfterDateSpecified(dateRange.Min.Value, 5);

            } while (datesInRange == false && dateRange.Min.Value < AutoSchedulePeriodStart.AddYears(2));//fail safe for 2 yr.
            //if it falls out of the whole loop, AutoScheduleStartDt -> +2years, return nothing
            if (datesInRange == false)
            {
                ret.BuildingScheduleStart = null;
                ret.BuildingScheduleEnd = null;
                ret.ElectricScheduleStart = null;
                ret.ElectricScheduleEnd = null;
                ret.MechScheduleStart = null;
                ret.MechScheduleEnd = null;
                ret.PlumbScheduleStart = null;
                ret.PlumbScheduleEnd = null;
                ret.FireScheduleStart = null;
                ret.FireScheduleEnd = null;
                ret.ZoneScheduleStart = null;
                ret.ZoneScheduleEnd = null;
                ret.PoolScheduleStart = null;
                ret.PoolScheduleEnd = null;
                ret.FoodScheduleStart = null;
                ret.FoodScheduleEnd = null;
                ret.FacilityScheduleStart = null;
                ret.FacilityScheduleEnd = null;
                ret.BackFlowScheduleStart = null;
                ret.BackFlowScheduleEnd = null;
                ret.DayCareScheduleStart = null;
                ret.DayCareScheduleEnd = null;
                return ret;
            }
            SaveAllocationsIntoStage();
            return ret;
        }

        private bool SaveAllocationsIntoStage()
        {
            List<int> savedUsers = new List<int>();
            List<UserScheduleStageBE> belst = new List<UserScheduleStageBE>();
            //Save the allocated meeting hours to DB temp table until the user comes back and save.
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedBuildingReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedElectricReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedMechReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedPlumbReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedFireReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedZoneReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedPoolReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedDayCareReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedFacilityReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedFoodServiceReviewer, savedUsers));
            belst.AddRange(SaveDeptAllocationsIntoStage(SelectedBackFlowReviewer, savedUsers));
            UserScheduleStageBO bo = new UserScheduleStageBO();
            bo.Delete(CurrentProject.ID); //delete all instances of auto estimation. allow
            foreach (var item in belst)
            {
                bo.Create(item);
            }
            return true;
        }

        List<UserScheduleStageBE> SaveDeptAllocationsIntoStage(PlanReviewAutoSchedulableReviewer cRvr, List<int> users)
        {
            List<UserScheduleStageBE> belst = new List<UserScheduleStageBE>();
            if (cRvr == null)
                return belst;
            users.Add(cRvr.SelectedReviewer.UserIdentity.ID);

            if (cRvr != null && cRvr.SelectedReviewer != null && cRvr.SelectedReviewer.UserIdentity != null)
            {
                foreach (var item in cRvr.WIPReviewerTimeSlots) //SelectedReviewer.AllocatedTimeSlots)
                {
                    UserScheduleStageBE be = new UserScheduleStageBE();
                    be.ProjectID = CurrentProject.ID;
                    be.StartDate = item.StartTime;
                    be.EndDate = item.EndTime;
                    be.BusinessRefID = (int)cRvr.PlanReviewerDept;
                    be.UserID = cRvr.SelectedReviewer.UserIdentity.ID;
                    belst.Add(be);
                }
            }
            return belst;
        }

        private int GetTradeReviewerIDForManualCapacity(List<AutoSchedulableReviewer> lst, ProjectDepartment dept, out PlanReviewAutoSchedulableReviewer prelimReviewer)
        {
            prelimReviewer = null;
            //if the call contains a special request from UI to consider a specific user then check he is eligible first.
            int manualSelectedDeptPlanReviewer = GetPlanReviewerFromRequest(dept.DepartmentInfo);
            if (manualSelectedDeptPlanReviewer != -1)
            {
                //If there is a requested plan reviewer from UI when auto schedule is run then check for that person first.
                AutoSchedulableReviewer manualSelctReviewer = lst.Where(x => x.UserIdentity.ID == manualSelectedDeptPlanReviewer).FirstOrDefault();
                if (manualSelctReviewer != null)
                {
                    prelimReviewer = new PlanReviewAutoSchedulableReviewer(manualSelctReviewer);
                    return manualSelctReviewer.UserIdentity.ID;
                }
                else //if the requested plan reviewer is not eligible then return not selected.
                    return 0;
            }
            return 0;
        }

        private int GetTradeReviewerID(List<AutoSchedulableReviewer> departmentReviewerList, ProjectDepartment dept, out PlanReviewAutoSchedulableReviewer prelimReviewer)
        {
            prelimReviewer = null;

            if (RequestData.IsFutureCycle)
            {
                PlanReviewScheduleDetail detail = PlanReviewScheduleDetails.Where(x => x.BusinessRefId == (int)dept.DepartmentInfo).FirstOrDefault();
                if (detail != null)
                {
                    if (detail.AssignedPlanReviewerId != null && detail.AssignedPlanReviewerId > 0)
                    {
                        AutoSchedulableReviewer previousReviewer = departmentReviewerList.Where(x => x.UserIdentity.ID == detail.AssignedPlanReviewerId).FirstOrDefault();
                        if (previousReviewer != null)
                        {
                            prelimReviewer = new PlanReviewAutoSchedulableReviewer(previousReviewer);
                            return previousReviewer.UserIdentity.ID;
                        }
                    }
                }
            }

            //if the call contains a special request from UI to consider a specific user then check he is eligible first.
            int manualSelectedDeptPlanReviewer = GetPlanReviewerFromRequest(dept.DepartmentInfo);
            if (manualSelectedDeptPlanReviewer != -1)
            {
                //If there is a requested plan reviewer from UI when auto schedule is run then check for that person first.
                AutoSchedulableReviewer manualSelctReviewer = departmentReviewerList.Where(x => x.UserIdentity.ID == manualSelectedDeptPlanReviewer).FirstOrDefault();
                if (manualSelctReviewer != null)
                {
                    prelimReviewer = new PlanReviewAutoSchedulableReviewer(manualSelctReviewer);
                    return manualSelctReviewer.UserIdentity.ID;
                }
                else //if the requested plan reviewer is not eligible then return not selected.
                    return 0;
            }

            //jcl LES-186 if this isn't an activate na review request
            if (dept.EstimationNotApplicable == true && RequestData.isActivateNAReview == false)
            {
                return -1;

            }
            //System will first try to schedule to the primary plan reviewer
            if (dept != null && dept.PrimaryPlanReviewer != null && dept.PrimaryPlanReviewer.ID > 0)
            {
                AutoSchedulableReviewer rvr = departmentReviewerList.Where(x => x.UserIdentity.ID == dept.PrimaryPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    prelimReviewer = new PlanReviewAutoSchedulableReviewer(rvr);
                    return dept.PrimaryPlanReviewer.ID;
                }
            }

            //If the primary plan reviewer is not available to be scheduled then schedule to the secondary plan reviewer.
            if (dept != null && dept.SecondaryPlanReviewer != null && dept.SecondaryPlanReviewer.ID > 0)
            {
                AutoSchedulableReviewer rvr = departmentReviewerList.Where(x => x.UserIdentity.ID == dept.SecondaryPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    prelimReviewer = new PlanReviewAutoSchedulableReviewer(rvr);
                    return dept.SecondaryPlanReviewer.ID;
                }
            }

            //If the secondary plan reviewer is not available to be scheduled then schedule to then schedule the requested plan reviewer.
            if (dept != null && dept.ProposedPlanReviewer != null && dept.ProposedPlanReviewer.ID > 0)
            {
                AutoSchedulableReviewer rvr = departmentReviewerList.Where(x => x.UserIdentity.ID == dept.ProposedPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    prelimReviewer = new PlanReviewAutoSchedulableReviewer(rvr);
                    return dept.ProposedPlanReviewer.ID;
                }
            }
            //If the requested plan reviewer is not available or was not indicated by the customer,

            //LES- 4526 - jcl - choose the plan reviewer with the soonest availability
            //for each reviewer, find the first one with an allocation for start and end
            // once we get allocation of reviewer, get out of loop and return that reviewer
            // order the reviewer list by last name then first name
            AutoSchedulableReviewer selectedReviewer = null;
            int selectedReviewerId =
                GetSoonestAvailableTradeReviewer(departmentReviewerList,
                GetMeetingHoursDurationByDept(dept.DepartmentInfo),
                AutoSchedulePeriodStart,
                AutoSchedulePeriodStart.AddYears(2),
               out selectedReviewer);

            if (selectedReviewer == null)
                return 0;
            else
            {
                prelimReviewer = new PlanReviewAutoSchedulableReviewer(selectedReviewer);
                return selectedReviewer.UserIdentity.ID;
            }
        }


        private int GetPlanReviewerFromRequest(DepartmentNameEnums departmentName)
        {
            switch (departmentName)
            {
                case DepartmentNameEnums.Building:
                    return RequestData.BuildingUserID <= 0 ? -1 : RequestData.BuildingUserID;
                case DepartmentNameEnums.Electrical:
                    return RequestData.ElectricUserID <= 0 ? -1 : RequestData.ElectricUserID;
                case DepartmentNameEnums.Mechanical:
                    return RequestData.MechUserID <= 0 ? -1 : RequestData.MechUserID;
                case DepartmentNameEnums.Plumbing:
                    return RequestData.PlumbUserID <= 0 ? -1 : RequestData.PlumbUserID;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return RequestData.ZoneUserID <= 0 ? -1 : RequestData.ZoneUserID;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return RequestData.FireUserID <= 0 ? -1 : RequestData.FireUserID;
                case DepartmentNameEnums.EH_Day_Care:
                    return RequestData.DayCareUserID <= 0 ? -1 : RequestData.DayCareUserID;
                case DepartmentNameEnums.EH_Food:
                    return RequestData.FoodServiceUserID <= 0 ? -1 : RequestData.FoodServiceUserID;
                case DepartmentNameEnums.EH_Pool:
                    return RequestData.PoolUserID <= 0 ? -1 : RequestData.PoolUserID;
                case DepartmentNameEnums.EH_Facilities:
                    return RequestData.FacilityUserID <= 0 ? -1 : RequestData.FacilityUserID;
                case DepartmentNameEnums.Backflow:
                    return RequestData.BackFlowUserID <= 0 ? -1 : RequestData.BackFlowUserID;
                case DepartmentNameEnums.NA:
                    return -1;
                default:
                    return -1;
            }
        }

        private List<AutoSchedulableReviewer> GetAllPrelimEstimatorsForManualCapacity()
        {
            UserAdapter useradapter = new UserAdapter();
            var mAllSqFtList = useradapter.GetAllSquareFootageList();

            UserIdentityModelBO bo = new UserIdentityModelBO();
            //filter roles.
            List<AutoSchedulableReviewer> users = bo.GetInstance("", SystemRoleEnum.Plan_Reviewer.ToString()).Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList();
            List<AutoSchedulableReviewer> existingReviewers = new List<AutoSchedulableReviewer>();
            if (RequestData.BuildingUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.BuildingUserID).FirstOrDefault());
            }
            if (RequestData.ElectricUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.ElectricUserID).FirstOrDefault());
            }
            if (RequestData.MechUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.MechUserID).FirstOrDefault());
            }
            if (RequestData.PlumbUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.PlumbUserID).FirstOrDefault());
            }
            if (RequestData.ZoneUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.ZoneUserID).FirstOrDefault());
            }
            if (RequestData.FireUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.FireUserID).FirstOrDefault());
            }
            if (RequestData.DayCareUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.DayCareUserID).FirstOrDefault());
            }
            if (RequestData.FoodServiceUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.FoodServiceUserID).FirstOrDefault());
            }
            if (RequestData.PoolUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.PoolUserID).FirstOrDefault());
            }
            if (RequestData.FacilityUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.FacilityUserID).FirstOrDefault());
            }
            if (RequestData.BackFlowUserID > 0)
            {
                existingReviewers.Add(users.Where(x => x.UserIdentity.ID == RequestData.BackFlowUserID).FirstOrDefault());
            }
            users = existingReviewers;

            users.ForEach(x => x.UserIdentity.FillDesignatedDepartments());
            for (int i = 0; i < users.Count; i++)
            {
                var mUserMgmtOccu = useradapter.GetSquareFootageListbyUserOccupancy(users[i].UserIdentity.ID);
                //gets all users with eligible for specified occupancy.
                users[i].AllowedOccupancies = useradapter.GetOccupancyTypeProjectMapListByUser(users[i].UserIdentity.ID)
                    .Where(x => x.ProjectOccupancyTypeName == CurrentProject.AccelaOccupancyType).ToList();
                foreach (var item in users[i].AllowedOccupancies)
                {
                    var occ = mUserMgmtOccu.Where(x => x.OccupancyName == item.OccupancyTypeName).FirstOrDefault();
                    if (occ != null && mAllSqFtList != null)
                    {
                        var sqft = mAllSqFtList.Where(x => x.ID == occ.SquareFootageId).FirstOrDefault();
                        if (sqft != null)
                        {
                            item.AllocatedSquareFootage = sqft.SquareFootage;
                        }
                    }
                }
                if (users[i].UserIdentity.PlanReviewOverrideHours != 0)
                    users[i].AllowedHoursPerDay = users[i].UserIdentity.PlanReviewOverrideHours;
                else
                    users[i].AllowedHoursPerDay = SchedulingHelper.GetGlobalAllowedPlanReviewHours(CurrentProject.AccelaPropertyType);
                if (users[i].AllowedHoursPerDay == 0)
                    users[i].AllowedHoursPerDay = 8;//set default as 8 hrs incase nothing found.
            }
            //filter matching occupancies and IS_SCHEDULABLE_IND
            List<AutoSchedulableReviewer> ret = users;
            for (int i = 0; i < ret.Count; i++)
            {
                ret[i].CurrentMeetings = useradapter.GetUsedTimeSlotsExtrasByUserID(ret[i].UserIdentity.ID);
                if (ret[i].CurrentMeetings != null)
                {
                    foreach (var item in ret[i].CurrentMeetings)
                    {
                        if (item.TotalTimeOfProject.Ticks == 0) //optimization
                        {
                            //calculate total timespan of the project. the calculation is only based on T-1 days since that is all retrived from DB SP. Days before that is ignore for now
                            var alldays = ret[i].CurrentMeetings.Where(x => x.ProjectID == item.ProjectID && x.DepartmentName == item.DepartmentName);
                            TimeSpan total = new TimeSpan(alldays.Sum(x => x.TotalTimeOfDay.Ticks));
                            alldays.ForEach(x => x.TotalTimeOfProject = total);
                        }
                    }
                }
            }
            return ret;
        }

    }

    public class DateRange
    {
        public DateTime? Min { get; set; }
        public DateTime? Max { get; set; }
    }
}