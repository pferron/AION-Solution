using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models.Base;
using AION.Manager.Helpers;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Engines
{
    public class PrelimProjectSchedulingEngine : BaseSchedulingEngine
    {

        public AutoSchedulableReviewer SelectedBuildingReviewer;
        public AutoSchedulableReviewer SelectedElectricReviewer;
        public AutoSchedulableReviewer SelectedMechReviewer;
        public AutoSchedulableReviewer SelectedPlumbReviewer;
        public AutoSchedulableReviewer SelectedZoneReviewer;
        public AutoSchedulableReviewer SelectedFireReviewer;
        public AutoSchedulableReviewer SelectedFoodServiceReviewer;
        public AutoSchedulableReviewer SelectedPoolReviewer;
        public AutoSchedulableReviewer SelectedFacilityReviewer;
        public AutoSchedulableReviewer SelectedDayCareReviewer;
        public AutoSchedulableReviewer SelectedBackFlowReviewer;

        public DateTime? SuggestedDate1 { get; set; } = null;
        public DateTime? SuggestedDate2 { get; set; } = null;
        public DateTime? SuggestedDate3 { get; set; } = null;

        //jcl LES-3353 - use requested from Accela
        public DateTime? RequestedDateRangeStart { get; set; } = null;
        public DateTime? RequestedDateRangeEnd { get; set; } = null;


        /// <summary>
        /// 15 or 3 pm default
        /// </summary>
        public static int AllowedLastScheduleStartTime { get; set; } = 15; // 3 PM
        public DateTime AutoSchedulePeriodStart { get; set; } = DateTime.Now.AddDays(2); //look at least 2 days in the future for auto scheduling.

        //System will not allow scheduling of a prelim meeting on a holiday.
        decimal MeetingDuration { get; set; }

        int SlotsInTheDay { get; set; }

        public PrelimProjectSchedulingEngine(AutoScheduledPrelimParams data)
        {
            TimeSlotIntervalByMinutes = 30;
            SlotsInTheDay = (int)((decimal)(AllowedLastScheduleStartTime - AllowedStartTime) / ((decimal)TimeSlotIntervalByMinutes / (decimal)60));
            //These days must be excluded from scheduling
            if (HolidayList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
                HolidayList = GetHolidays();
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            SuggestedDate1 = data.SuggestedDate1;

            SuggestedDate2 = data.SuggestedDate2;

            SuggestedDate3 = data.SuggestedDate3;

            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(data.AccelaProjectIDRef);
            RequestedDateRangeStart = CurrentProject.PrelimMeetingDetail.RequestedBeginDateRange;

            RequestedDateRangeEnd = CurrentProject.PrelimMeetingDetail.RequestedEndDateRange;

            AdjustRequestedDateRange();
            AllEligibleReviewers = GetAllPrelimEstimators();
            SplitEligibleReviewersByDept();
            MeetingDuration = CalculateMeetingDuration();
        }

        private decimal CalculateMeetingDuration()
        {
            decimal ret = (decimal).5;
            foreach (var item in CurrentProject.Trades)
            {
                if (item.EstimationHours.HasValue == true && item.EstimationHours.Value > ret)
                    ret = item.EstimationHours.Value;
            }
            foreach (var item in CurrentProject.Agencies)
            {
                if (item.EstimationHours.HasValue == true && item.EstimationHours.Value > ret)
                    ret = item.EstimationHours.Value;
            }
            return ret;
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
        public AutoScheduledPrelimValues GetAutoEstimatedValues()
        {
            AutoScheduledPrelimValues ret = new AutoScheduledPrelimValues();
            Helper helper = new Helper();
            //Select final reviewers
            ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBuildingReviewersList == null)
            {
                ret.BuildingUserID = -1;
            }
            else
            {
                ret.BuildingUserID = GetTradeReviewerID(EligibleBuildingReviewersList, curtrade, out SelectedBuildingReviewer);
            }

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleElectricReviewersList == null)
            {
                ret.ElectricUserID = -1;
            }
            else
            {
                ret.ElectricUserID = GetTradeReviewerID(EligibleElectricReviewersList, curtrade, out SelectedElectricReviewer);
            }

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleMechReviewersList == null)
            {
                ret.MechUserID = -1;
            }
            else
            {
                ret.MechUserID = GetTradeReviewerID(EligibleMechReviewersList, curtrade, out SelectedMechReviewer);
            }

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePlumbReviewersList == null)
            {
                ret.PlumbUserID = -1;
            }
            else
            {
                ret.PlumbUserID = GetTradeReviewerID(EligiblePlumbReviewersList, curtrade, out SelectedPlumbReviewer);
            }

            curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleZoneReviewersList == null)
            {
                ret.ZoneUserID = -1;
            }
            else
            {
                ret.ZoneUserID = GetTradeReviewerID(EligibleZoneReviewersList, curtrade, out SelectedZoneReviewer);
            }

            curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFireReviewersList == null)
            {
                ret.FireUserID = -1;
            }
            else
            {
                ret.FireUserID = GetTradeReviewerID(EligibleFireReviewersList, curtrade, out SelectedFireReviewer);
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBackFlowReviewersList == null)
            {
                ret.BackFlowUserID = -1;
            }
            else
            {
                ret.BackFlowUserID = GetTradeReviewerID(EligibleBackFlowReviewersList, curtrade, out SelectedBackFlowReviewer);
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFoodServiceReviewersList == null)
            {
                ret.FoodServiceUserID = -1;
            }
            else
            {
                ret.FoodServiceUserID = GetTradeReviewerID(EligibleFoodServiceReviewersList, curtrade, out SelectedFoodServiceReviewer);
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePoolReviewersList == null)
            {
                ret.PoolUserID = -1;
            }
            else
            {
                ret.PoolUserID = GetTradeReviewerID(EligiblePoolReviewersList, curtrade, out SelectedPoolReviewer);
            }


            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFacilityReviewersList == null)
            {
                ret.FacilityUserID = -1;
            }
            else
            {
                ret.FacilityUserID = GetTradeReviewerID(EligibleFacilityReviewersList, curtrade, out SelectedFacilityReviewer);
            }

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleDayCareReviewersList == null)
            {
                ret.DayCareUserID = -1;
            }
            else
            {
                ret.DayCareUserID = GetTradeReviewerID(EligibleDayCareReviewersList, curtrade, out SelectedDayCareReviewer);
            }

            DateTime? startDt = null;
            //Requested dates from customer need o be checked first.
            //Try to first schedule to one of the 3 requested dates by the customer if they submitted requested dates.  
            if (SuggestedDate1.HasValue == true && SuggestedDate1.Value != DateTime.MinValue)
            {
                startDt = FindMeetingSlotAvailableForTheDay(SuggestedDate1.Value, MeetingDuration);
                if (startDt.HasValue == true)
                {
                    ret.ScheduleStart = startDt.Value;
                    ret.ScheduleEnd = ret.ScheduleStart.Value.AddHours((double)MeetingDuration);

                    return ret;
                }
            }
            if (SuggestedDate2.HasValue == true && SuggestedDate2.Value != DateTime.MinValue)
            {
                startDt = FindMeetingSlotAvailableForTheDay(SuggestedDate2.Value, MeetingDuration);
                if (startDt.HasValue == true)
                {
                    ret.ScheduleStart = startDt.Value;
                    ret.ScheduleEnd = ret.ScheduleStart.Value.AddHours((double)MeetingDuration);

                    return ret;
                }
            }
            if (SuggestedDate3.HasValue == true && SuggestedDate3.Value != DateTime.MinValue)
            {
                startDt = FindMeetingSlotAvailableForTheDay(SuggestedDate3.Value, MeetingDuration);
                if (startDt.HasValue == true)
                {
                    ret.ScheduleStart = startDt.Value;
                    ret.ScheduleEnd = ret.ScheduleStart.Value.AddHours((double)MeetingDuration);

                    return ret;
                }
            }
            //If not able to use requested dates then Schedules to first available date and time frame based on plan reviewer availability.  
            //Find the first slot where all ther reviewers are available. 
            startDt = SearchForFirstMeetingSlotAvailable(MeetingDuration);

            if (startDt.HasValue)
            {

                ret.ScheduleStart = startDt.Value;
                ret.ScheduleEnd = ret.ScheduleStart.Value.AddHours((double)MeetingDuration);

                return ret;
            }
            else
            {
                //putting default if nothing found. Which should not be a case.
                ret.ScheduleStart = DateTime.MinValue;
                ret.ScheduleEnd = ret.ScheduleStart.Value.AddHours((double)MeetingDuration);

                return ret;
            }
        }

        private DateTime? SearchForFirstMeetingSlotAvailable(decimal meetingDurationHrs)
        {
            DateTime? startDt = null;
            if (IsInRejectionMode() == false && RequestedDateRangeStart.HasValue && RequestedDateRangeEnd.HasValue)
            {
                startDt = SearchForFirstMeetingSlotAvailableBetweenRequestedDates(meetingDurationHrs);
            }
            //if nothing comes back from requested dates then autoschedule as normal
            if (startDt.HasValue == false)
            {
                //look at least 2 days in the future for auto scheduling.
                DateTime currentDay = AutoSchedulePeriodStart.CurrentHalfTime();
                int maxyr = currentDay.Year + 10; //just incase for some reason to avoid endless loop.
                while (startDt.HasValue == false || currentDay.Year > maxyr)
                {
                    startDt = FindMeetingSlotAvailableForTheDay(currentDay, meetingDurationHrs);
                    currentDay = currentDay.AddDays(1);
                }
            }
            //if startDt is null after 10 years return null. else returns the first occurance of date which had free start time for all reviewers.
            return startDt;
        }

        private DateTime? SearchForFirstMeetingSlotAvailableBetweenRequestedDates(decimal meetingDurationHrs)
        {
            DateTime? startDt = null;
            //look at least 2 days in the future for auto scheduling.
            DateTime currentDay = RequestedDateRangeStart.Value.CurrentHalfTime();
            DateTime maxDay = RequestedDateRangeEnd.Value.AddDays(1);
            while (startDt.HasValue == false && currentDay < maxDay)
            {
                startDt = FindMeetingSlotAvailableForTheDay(currentDay, meetingDurationHrs);
                currentDay = currentDay.AddDays(1);
            }
            return startDt;
        }

        private void AdjustRequestedDateRange()
        {
            if (RequestedDateRangeStart.HasValue && RequestedDateRangeEnd.HasValue)
            {
                //reset to null if either are the minvalue
                if (RequestedDateRangeStart.Value == DateTime.MinValue || RequestedDateRangeEnd.Value == DateTime.MinValue)
                {
                    RequestedDateRangeStart = null;
                    RequestedDateRangeEnd = null;
                }
                else
                //decide if date range is in the past
                if (RequestedDateRangeEnd.Value < AutoSchedulePeriodStart)
                {
                    RequestedDateRangeStart = null;
                    RequestedDateRangeEnd = null;
                }
                else
                //decide if requested date range start is at least 2 days in future
                if (RequestedDateRangeStart.Value < AutoSchedulePeriodStart)
                {
                    RequestedDateRangeStart = AutoSchedulePeriodStart;
                }

            }
        }

        private bool IsInRejectionMode()
        {
            if (SuggestedDate1.HasValue == true || SuggestedDate2.HasValue == true || SuggestedDate3.HasValue == true)
            { return true; }
            return false;
        }
        /*
         *  Prelim can be scheduled as early as 8am.
            Do not schedule between 12pm to 1pm.
            The last start time for a meeting is 3pm and can end by 5pm.  
        */
        private DateTime? FindMeetingSlotAvailableForTheDay(DateTime day, decimal meetingDurationHrs)
        {
            if (day < AutoSchedulePeriodStart.CurrentHalfTime()) // less than 2 days from today then ignore.
                return null;
            if (IsDateHolidayOrWeekEnd(day) == true)
                return null;
            try
            {
                //find how many slots need to be free for this meeting. eg: meetingDurationHrs =  1.5 then 1.5/(30/60 = .5).5 = 3
                int meetingSlotCnt = (int)(meetingDurationHrs / (decimal)((decimal)TimeSlotIntervalByMinutes / (decimal)60));

                for (int i = 0; i <= SlotsInTheDay; i++)
                {

                    DateTime? CurrentStartTime = null;
                    List<DateTime> intervals = new List<DateTime>();
                    CurrentStartTime = day.Date.AddHours(AllowedStartTime).AddMinutes(i * TimeSlotIntervalByMinutes);//8am + add 30 minutes in every iteration.

                    intervals.AddRange(GetTimeIntervals(CurrentStartTime.Value, meetingSlotCnt));
                    if (CheckAllSlotsAreFree(intervals, CurrentStartTime.Value) == true)
                        return CurrentStartTime;
                }
                //if there are no free slot for this date then the function will reach here and will return null
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<DateTime> GetTimeIntervals(DateTime startTime, decimal meetingDurationHrs)
        {
            List<DateTime> ret = new List<DateTime>();
            for (int i = 0; i < meetingDurationHrs; i++)
            {
                DateTime tm = startTime.CurrentHalfTime().AddMinutes(i * TimeSlotIntervalByMinutes);
                if (tm.Hour >= AllowedStartTime && tm.Hour <= AllowedEndTime)
                    ret.Add(tm);
            }
            return ret;
        }

        private bool CheckAllSlotsAreFree(List<DateTime> hours, DateTime startHr)
        {
            try
            {
                foreach (DateTime currentHr in hours)
                {
                    DateTime time = currentHr.CurrentHalfTime();
                    if (IsMeetingAllowedAtThisTimeOftheDay(time, time == startHr.CurrentHalfTime()) == false)
                        return false;

                    //if any one of the reviewer is already allocated to this timeslot then the whole series is invalid.
                    if (SelectedBuildingReviewer != null && SelectedBuildingReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedElectricReviewer != null && SelectedElectricReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedMechReviewer != null && SelectedMechReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedPlumbReviewer != null && SelectedPlumbReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedZoneReviewer != null && SelectedZoneReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedFireReviewer != null && SelectedFireReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedBackFlowReviewer != null && SelectedBackFlowReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedFoodServiceReviewer != null && SelectedFoodServiceReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedPoolReviewer != null && SelectedPoolReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedFacilityReviewer != null && SelectedFacilityReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                    if (SelectedDayCareReviewer != null && SelectedDayCareReviewer.WIPTimeSlots.Any(x => x.StartTime.CurrentHalfTime() == time))
                        return false;
                }
                //if all of them are not allocated to all hour then it means all the slots are free for all of the reviewers.
                return true;//return null if no free timeslot found.
            }
            catch
            {
                return false;
            }
        }

        public bool IsMeetingAllowedAtThisTimeOftheDay(DateTime time, bool isMeetingStartTime)
        {
            if (time.Hour == 12)
                return false;
            if (isMeetingStartTime == true)
                return time.Hour >= AllowedStartTime && time.Hour <= AllowedLastScheduleStartTime;
            else
                return time.Hour >= AllowedStartTime && time.Hour <= AllowedEndTime;
        }

        private int GetTradeReviewerID(List<AutoSchedulableReviewer> departmentReviewerList, ProjectDepartment dept, out AutoSchedulableReviewer prelimReviewer)
        {
            prelimReviewer = null;
            int reviewerNA = -1;

            //System will first try to schedule to the primary plan reviewer
            if (dept != null && dept.PrimaryPlanReviewer != null)
            {
                AutoSchedulableReviewer rvr = departmentReviewerList.Where(x => x.UserIdentity.ID == dept.PrimaryPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    prelimReviewer = rvr;
                    return dept.PrimaryPlanReviewer.ID;
                }
            }

            //If the primary plan reviewer is not available to be scheduled then schedule to the secondary plan reviewer.
            if (dept != null && dept.SecondaryPlanReviewer != null)
            {
                AutoSchedulableReviewer rvr = departmentReviewerList.Where(x => x.UserIdentity.ID == dept.SecondaryPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    prelimReviewer = rvr;
                    return dept.SecondaryPlanReviewer.ID;
                }
            }


            //If the secondary plan reviewer is not available to be scheduled then schedule to then schedule the requested plan reviewer.  
            if (dept != null && dept.ProposedPlanReviewer != null)
            {
                AutoSchedulableReviewer rvr = departmentReviewerList.Where(x => x.UserIdentity.ID == dept.ProposedPlanReviewer.ID).FirstOrDefault();
                if (rvr != null)
                {
                    prelimReviewer = rvr;
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
                MeetingDuration,
                AutoSchedulePeriodStart,
                AutoSchedulePeriodStart.AddYears(2),
               out selectedReviewer);


            if (selectedReviewer == null)
                return reviewerNA;
            else
            {
                prelimReviewer = selectedReviewer;
                return selectedReviewer.UserIdentity.ID;
            }
        }


        /// <summary>
        /// Gets a list of reviewers who can actually do plan review.
        /// They will be (1) who is eligible for this project's occupancy(To be added in and square footage level later sprint)
        /// They will be set as Y in user.[IS_SCHEDULABLE_IND]
        /// They will be 'Y' in user.[IS_PRELIM_MEETING_ALLOWED_IND]
        /// They will be coming under role 2 in SYSTEM_ROLE table.(SYSTEM_ROLE_ID,SYSTEM_ROLE_NM= 2,Plan_Reviewer)
        /// /* only using occupancyType from below for now. rest will be added later.
        ///    "occupancyType": "R4",
        ///    "sqrFt": 13773,
        ///    "story": 2,
        ///    "sqrFtPerStory": 
        ///    {
        ///     "1": 13000,
        ///     "2": 773
        ///    },
        /// */
        /// </summary>
        private List<AutoSchedulableReviewer> GetAllPrelimEstimators()
        {
            UserIdentityModelBO bo = new UserIdentityModelBO();
            UserAdapter useradapter = new UserAdapter();

            //filter roles.
            List<AutoSchedulableReviewer> users = bo.GetInstance("", SystemRoleEnum.Plan_Reviewer.ToString()).Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList();
            users.ForEach(x => x.UserIdentity.FillDesignatedDepartments());
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserIdentity.PlanReviewOverrideHours != 0)
                    users[i].AllowedHoursPerDay = users[i].UserIdentity.PlanReviewOverrideHours;
                else
                    users[i].AllowedHoursPerDay = SchedulingHelper.GetGlobalAllowedPlanReviewHours(CurrentProject.AccelaPropertyType);
                if (users[i].AllowedHoursPerDay == 0)
                    users[i].AllowedHoursPerDay = 8;//set default as 8 hrs incase nothing found.
            }
            //filter matching occupancies and IS_SCHEDULABLE_IND and IS_PRELIM_MEETING_ALLOWED_IND
            List<AutoSchedulableReviewer> ret = users.Where(x =>
                   x.UserIdentity.IsSchedulable == true
                   && x.UserIdentity.IsPrelimMeetingAllowed == true).ToList();

            for (int i = 0; i < ret.Count; i++)
            {
                ret[i].CurrentMeetings = useradapter.GetUsedTimeSlotsExtrasByUserID(ret[i].UserIdentity.ID);
                if (ret[i].CurrentMeetings != null)
                {
                    ret[i].CurrentMeetings = AdjustReservedTimeSlots(ret[i].CurrentMeetings);

                }
            }

            for (int i = 0; i < ret.Count; i++)
            {
                ret[i].CurrentMeetings = useradapter.GetUsedTimeSlotsByUserID(ret[i].UserIdentity.ID);

                if (ret[i].CurrentMeetings != null)
                {
                    ret[i].CurrentMeetings = AdjustReservedTimeSlots(ret[i].CurrentMeetings);
                }

                ret[i].CurrentMeetings.OrderBy((x) => x.StartTime);
                ret[i].WIPTimeSlots = ret[i].CurrentMeetings.SelectMany(x => SplitTimeSlotByTimeSlotIntervalMinsHalfTime(x)).ToList();
                ret[i].WIPTimeSlots.OrderBy((x) => x.StartTime);
            }

            return ret;
        }

    }
}