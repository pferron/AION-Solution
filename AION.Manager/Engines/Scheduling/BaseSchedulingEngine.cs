using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AION.Manager.Engines
{
    public class BaseSchedulingEngine
    {
        public DateTime CountyTimeZoneDateTime //All scheduling is done per Mecklenburg county time.
        {
            get
            {
                return System.TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Eastern Standard Time");
            }
        }

        public List<AutoSchedulableReviewer> EligibleBuildingReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleElectricReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleMechReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligiblePlumbReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleZoneReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleZoneHuntersvilleReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleZoneMintHillReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleZoneCharlotteReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleZoneCountyReviewersList { get; set; } = new List<AutoSchedulableReviewer>();

        public List<AutoSchedulableReviewer> EligibleFireReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleFireCharlotteReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleFireCountyReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleFoodServiceReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligiblePoolReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleFacilityReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleDayCareReviewersList { get; set; } = new List<AutoSchedulableReviewer>();
        public List<AutoSchedulableReviewer> EligibleBackFlowReviewersList { get; set; } = new List<AutoSchedulableReviewer>();

        public ProjectEstimation CurrentProject { get; set; }
        public ProjectCycle ProjectCycle { get; set; }
        public List<ProjectCycleDetail> ProjectCycleDetails { get; set; }
        public PlanReviewSchedule PlanReviewSchedule { get; set; }
        public List<PlanReviewScheduleDetail> PlanReviewScheduleDetails { get; set; }

        public static List<DateTime> HolidayList { get; set; }
        public static List<DateTime> AvailableDateForRequestExpress { get; set; }
        /// <summary>
        /// Gets a list of reviewers who can actually do plan review.
        /// They will be (1) who is eligible for this project's occupancy(To be added in and square footage level later sprint)
        /// They will be set as Y in user.[IS_SCHEDULABLE_IND]
        /// They will be 'Y' in user.[IS_PRELIM_MEETING_ALLOWED_IND]
        /// They will be coming under role 2 in SYSTEM_ROLE table.(SYSTEM_ROLE_ID,SYSTEM_ROLE_NM= 2,Plan_Reviewer)
        /// </summary>
        public List<AutoSchedulableReviewer> AllEligibleReviewers { get; set; }

        /// <summary>
        /// 15 min Default
        /// </summary>
        public int TimeSlotIntervalByMinutes = 15;

        /// <summary>
        /// 8 am default
        /// </summary>
        public static int AllowedStartTime { get; set; } = 8;
        /// <summary>
        /// 17 or 5PM default
        /// </summary>
        public static int AllowedEndTime { get; set; } = 17; // 5 PM
        public bool IsHuntersvilleTownDefault { get; set; }
        public bool IsMintHillTownDefault { get; set; }
        public bool IsHuntersvilleOrMintHillZoning
        {
            get
            {
                if (CurrentProject == null) return false;
                return CurrentProject.Agencies.Any(x => x.DepartmentInfo == DepartmentNameEnums.Zone_Mint_Hill || x.DepartmentInfo == DepartmentNameEnums.Zone_Huntersville);
            }
        }
        public List<TimeSlot> BusinessTimeSlots { get; set; }
        public decimal MeetingDurationBuild { get; set; }
        public decimal MeetingDurationElectric { get; set; }
        public decimal MeetingDurationMech { get; set; }
        public decimal MeetingDurationPlumb { get; set; }
        public decimal MeetingDurationFire { get; set; }
        public decimal MeetingDurationZone { get; set; }
        public decimal MeetingDurationDayCare { get; set; }
        public decimal MeetingDurationPool { get; set; }
        public decimal MeetingDurationFood { get; set; }
        public decimal MeetingDurationFacility { get; set; }
        public decimal MeetingDurationBackFlow { get; set; }
        public decimal MeetingDuration { get; set; }

        public PlanReviewAutoSchedulableReviewer SelectedReviewer;
        public bool IsReviewerExcluded(AutoSchedulableReviewer reviewer, ProjectDepartment dept)
        {

            if (dept == null)
                return true;
            if (dept.ExcludedPlanReviewers.Any(x => reviewer.UserIdentity.ID == x) == true)
                return true;
            else
                return false;
        }

        public List<DateTime> GetHolidays()
        {
            HolidayConfigAdapter hd = new HolidayConfigAdapter();
            return hd.GetHolidayConfigDates();
        }

        public void SetCycleData(int cycleNumber)
        {
            PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();

            List<ProjectCycle> ProjectCycles = planReviewAdapter.GetProjectCyclesByProjectId(CurrentProject.ID);
            ProjectCycle = ProjectCycles.FirstOrDefault(x => x.CycleNbr == cycleNumber);
            ProjectCycleDetails = planReviewAdapter.GetProjectCycleDetailsByProjectCycleId(ProjectCycle.ID);
            List<PlanReviewSchedule> planReviewSchedules = planReviewAdapter.GetPlanReviewSchedulesByProjectCycle(ProjectCycle.ID);
            PlanReviewSchedule = planReviewSchedules.FirstOrDefault(x => x.IsRescheduleInd == false);
            if (PlanReviewSchedule != null)
            {
                PlanReviewScheduleDetails = planReviewAdapter.GetPlanReviewScheduleDetailsByPlanReviewSchedule(PlanReviewSchedule.ID);
            }

            if (ProjectCycle.CycleNbr > 1 && ProjectCycleDetails.Count() > 0)  // this is a CURRENT sub cycle
            {
                // adjust project trades for auto scheduling to be only those in the project cycle detail
                List<ProjectDepartment> projectDepartmentsToAutoSchedule = new List<ProjectDepartment>();

                foreach (ProjectCycleDetail detail in ProjectCycleDetails)
                {
                    ProjectDepartment dept = ProjectHelper.GetDepartment(CurrentProject, (DepartmentNameEnums)detail.BusinessRefId);
                    projectDepartmentsToAutoSchedule.Add(dept);
                }

                CurrentProject.Trades.RemoveAll(item => !projectDepartmentsToAutoSchedule.Contains(item));
                CurrentProject.Agencies.RemoveAll(item => !projectDepartmentsToAutoSchedule.Contains(item));
            }
        }

        public bool IsProjectLevelAllowed(List<ProjectOccupancyTypeNameModel> allowedOccupancies, string userlevel, string projLvl)
        {
            int usrLvl = 0;
            int prjLvl = 0;
            if (string.IsNullOrEmpty(projLvl) == true)//if none of them is assigned in admin then this user is not eligible.
                return false;
            if (string.IsNullOrEmpty(userlevel) == false)
                userlevel = userlevel.Trim();// avoid space
            if (string.IsNullOrEmpty(userlevel) == true && allowedOccupancies.Count == 0)//if none of them is assigned in admin then this user is not eligible.
                return false;

            
            if (projLvl.ToLower() == "level1")
                prjLvl = 1;
            else if (projLvl.ToLower() == "level2")
                prjLvl = 2;
            else if (projLvl.ToLower() == "level3")
                prjLvl = 3;
            else
                prjLvl = 0;

            if (string.IsNullOrEmpty(userlevel) == false)
            {
                if (userlevel.ToLower() == "level1")
                    usrLvl = 1;
                else if (userlevel.ToLower() == "level2")
                    usrLvl = 2;
                else if (userlevel.ToLower() == "level3")
                    usrLvl = 3;
                else
                    usrLvl = 0;
            }
            else
            {
                //double maxAllowedsqft = 0;
                if (allowedOccupancies.Any(x => x.AllocatedSquareFootage == "60000+"))
                    //maxAllowedsqft = 60001;
                    usrLvl = 3;
                else if (allowedOccupancies.Any(x => x.AllocatedSquareFootage == "20001-60000"))
                    //maxAllowedsqft = 60000;
                    usrLvl = 2;
                else if (allowedOccupancies.Any(x => x.AllocatedSquareFootage == "10001-20000"))
                    //maxAllowedsqft = 20000;
                    usrLvl = 1;
                else if (allowedOccupancies.Any(x => x.AllocatedSquareFootage == "0-10000"))
                    //maxAllowedsqft = 10000;
                    usrLvl = 0;
            }

            if (usrLvl < prjLvl)
                return false;
            else
                return true;
        }

        public List<TimeSlot> SplitTimeSlotByTimeSlotIntervalMinsHalfTime(TimeSlot timeslot)
        {
            return SchedulingHelper.SplitTimeSlotByTimeSlotIntervalMinsHalfTime(timeslot, TimeSlotIntervalByMinutes, AllowedStartTime, AllowedEndTime);
        }
        public List<TimeSlot> SplitTimeSlotByTimeSlotIntervalMinsQuarterTime(TimeSlot timeslot)
        {
            return SchedulingHelper.SplitTimeSlotByTimeSlotIntervalMinsQuarterTime(timeslot, TimeSlotIntervalByMinutes, AllowedStartTime, AllowedEndTime);
        }

        public virtual List<AutoSchedulableReviewer> GetAllEligibleReviewers()
        {
            UserAdapter useradapter = new UserAdapter();
            var mAllSqFtList = useradapter.GetAllSquareFootageList();

            UserIdentityModelBO bo = new UserIdentityModelBO();
            //filter roles.

            List<AutoSchedulableReviewer> users = useradapter.GetReviewers((int)CurrentProject.AionPropertyType, (int)DepartmentNameEnums.NA)
                .Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList();

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
            List<AutoSchedulableReviewer> ret = users.Where(x =>
                   (IsProjectLevelAllowed(x.AllowedOccupancies, x.UserIdentity.SchedulableLevel, CurrentProject.ProjectLvlTxt) == true)
                    && x.UserIdentity.IsSchedulable == true).ToList();


            for (int i = 0; i < ret.Count; i++)
            {
                decimal totalHoursForCapacity = 0M;
                ret[i].CurrentMeetings = useradapter.GetUsedTimeSlotsExtrasByUserID(ret[i].UserIdentity.ID);
                if (ret[i].CurrentMeetings != null)
                {
                    ret[i].CurrentMeetings = AdjustReservedTimeSlots(ret[i].CurrentMeetings);
                    totalHoursForCapacity = GetTotalHoursForCapacity(ret[i].CurrentMeetings);

                }
                ret[i].TotalHoursForCapacity = totalHoursForCapacity;
            }

            return ret;
        }

        public List<TimeSlot> AdjustReservedTimeSlots(List<TimeSlot> currentMeetings)
        {
            // NOTE: Adjust EXP timeslots by EMA timeslots allotted
            var reservedExpress = currentMeetings.Where(x => x.AllocationType == TimeAllocationType.Project_Express_Blocked).ToList();
            var scheduledExpress = currentMeetings.Where(x => x.AllocationType == TimeAllocationType.Project_Express_Reserved).ToList();

            foreach (TimeSlot scheduledSlot in scheduledExpress)
            {
                // find corresponding reservation
                TimeSlot reservedSlot = reservedExpress.FirstOrDefault(x => x.StartTime <= scheduledSlot.StartTime && x.EndTime >= scheduledSlot.EndTime);

                if (reservedSlot != null)
                {
                    // remove reserved slots from current meetings
                    currentMeetings.RemoveAll(r => r.AllocationType == reservedSlot.AllocationType
                        && r.DepartmentName == reservedSlot.DepartmentName
                        && r.StartTime == reservedSlot.StartTime
                        && r.EndTime == reservedSlot.EndTime);

                    //break up reserved slot and unscheduled portions get added back to current meetings

                    if (reservedSlot.StartTime.TimeOfDay == scheduledSlot.StartTime.TimeOfDay)
                    {
                        if (reservedSlot.EndTime.TimeOfDay < scheduledSlot.EndTime.TimeOfDay)
                        {
                            TimeSlot newReservedTimeSlot = reservedSlot;
                            newReservedTimeSlot.StartTime = scheduledSlot.StartTime;
                            newReservedTimeSlot.EndTime = reservedSlot.EndTime;

                            currentMeetings.Add(newReservedTimeSlot);
                        }
                    }

                    if (reservedSlot.StartTime.TimeOfDay < scheduledSlot.StartTime.TimeOfDay)
                    {
                        TimeSlot newReservedTimeSlot = reservedSlot;
                        newReservedTimeSlot.StartTime = reservedSlot.StartTime;
                        newReservedTimeSlot.EndTime = scheduledSlot.StartTime;

                        currentMeetings.Add(newReservedTimeSlot);
                    }

                    if (reservedSlot.EndTime.TimeOfDay > scheduledSlot.EndTime.TimeOfDay)
                    {
                        TimeSlot newReservedTimeSlot = reservedSlot;
                        newReservedTimeSlot.StartTime = scheduledSlot.EndTime;
                        newReservedTimeSlot.EndTime = reservedSlot.EndTime;

                        currentMeetings.Add(newReservedTimeSlot);
                    }
                }
            }

            return currentMeetings;
        }

        public decimal GetTotalHoursForCapacity(List<TimeSlot> currentMeetings)
        {
            decimal totalHoursForCapacity = 0M;

            foreach (var item in currentMeetings)
            {
                if (item.TotalTimeOfProject.Ticks == 0) //optimization
                {
                    //calculate total timespan of the project. the calculation is only based on T-1 days since that is all retrived from DB SP. Days before that is ignore for now
                    var alldays = currentMeetings.Where(x => x.ProjectID == item.ProjectID && x.DepartmentName == item.DepartmentName);
                    TimeSpan total = new TimeSpan(alldays.Sum(x => x.TotalTimeOfDay.Ticks));
                    alldays.ForEach(x => x.TotalTimeOfProject = total);
                }

                totalHoursForCapacity += item.TotalTimeOfDay.Hours;

                switch (item.TotalTimeOfDay.Minutes)
                {
                    case 15:
                        totalHoursForCapacity += 0.25M;
                        break;
                    case 30:
                        totalHoursForCapacity += 0.50M;
                        break;
                    case 45:
                        totalHoursForCapacity += 0.75M;
                        break;
                }
            }

            return totalHoursForCapacity;
        }

        public decimal GetTotalHoursForCapacityExpress(List<TimeSlot> currentMeetings)
        {
            decimal totalHoursForCapacity = 0M;

            foreach (var item in currentMeetings)
            {
                if (item.AllocationType != TimeAllocationType.Project_Express_Blocked)
                {
                    if (item.TotalTimeOfProject.Ticks == 0) //optimization
                    {
                        //calculate total timespan of the project. the calculation is only based on T-1 days since that is all retrived from DB SP. Days before that is ignore for now
                        var alldays = currentMeetings.Where(x => x.ProjectID == item.ProjectID && x.DepartmentName == item.DepartmentName);
                        TimeSpan total = new TimeSpan(alldays.Sum(x => x.TotalTimeOfDay.Ticks));
                        alldays.ForEach(x => x.TotalTimeOfProject = total);
                    }

                    totalHoursForCapacity += item.TotalTimeOfDay.Hours;

                    switch (item.TotalTimeOfDay.Minutes)
                    {
                        case 15:
                            totalHoursForCapacity += 0.25M;
                            break;
                        case 30:
                            totalHoursForCapacity += 0.50M;
                            break;
                        case 45:
                            totalHoursForCapacity += 0.75M;
                            break;
                    }
                }
            }

            return totalHoursForCapacity;
        }

        public void SplitEligibleReviewersByDept()
        {
            Helper helper = new Helper();

            foreach (var item in AllEligibleReviewers)
            {
                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Electrical)
                    && IsReviewerExcluded(item, CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault()) == false)
                    EligibleElectricReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Mechanical)
                    && IsReviewerExcluded(item, CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault()) == false)
                    EligibleMechReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Building)
                    && IsReviewerExcluded(item, CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault()) == false)
                    EligibleBuildingReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Plumbing)
                    && IsReviewerExcluded(item, CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault()) == false)
                    EligiblePlumbReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Zone_Huntersville)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Zone_Huntersville).FirstOrDefault()) == false)
                { 
                    EligibleZoneReviewersList.Add(item);
                    EligibleZoneHuntersvilleReviewersList.Add(item);
                }

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Zone_Mint_Hill)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Zone_Mint_Hill).FirstOrDefault()) == false)
                {
                    EligibleZoneReviewersList.Add(item);
                    EligibleZoneMintHillReviewersList.Add(item);
                }

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Zone_Cty_Chrlt)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Zone_Cty_Chrlt).FirstOrDefault()) == false)
                {
                    EligibleZoneReviewersList.Add(item);
                    EligibleZoneCharlotteReviewersList.Add(item);
                }

                if (item.UserIdentity.DesignatedDepartments.Any(y => helper.CountyZoneDepartmentNames.Contains(y.DepartmentEnum))
                      && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => helper.CountyZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault()) == false)
                {
                    EligibleZoneReviewersList.Add(item);
                    EligibleZoneCountyReviewersList.Add(item);
                }

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Fire_Cty_Chrlt)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Fire_Cty_Chrlt).FirstOrDefault()) == false)
                {
                    EligibleFireReviewersList.Add(item);
                    EligibleFireCharlotteReviewersList.Add(item);
                }

                if (item.UserIdentity.DesignatedDepartments.Any(y => helper.CountyFireDepartmentNames.Contains(y.DepartmentEnum))
                     && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => helper.CountyFireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault()) == false)
                {
                    EligibleFireReviewersList.Add(item);
                    EligibleFireCountyReviewersList.Add(item);
                }

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.EH_Food)
                     && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault()) == false)
                    EligibleFoodServiceReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.EH_Pool)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault()) == false)
                    EligiblePoolReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.EH_Facilities)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault()) == false)
                    EligibleFacilityReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.EH_Day_Care)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault()) == false)
                    EligibleDayCareReviewersList.Add(item);

                if (item.UserIdentity.DesignatedDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Backflow)
                    && IsReviewerExcluded(item, CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault()) == false)
                    EligibleBackFlowReviewersList.Add(item);
            }
        }

        public List<AutoSchedulableReviewer> GetTradeReviewersForReport(DepartmentNameEnums department)
        {
            switch (department)
            {
                case DepartmentNameEnums.Building:
                    return EligibleBuildingReviewersList;
                case DepartmentNameEnums.Electrical:
                    return EligibleElectricReviewersList;
                case DepartmentNameEnums.Mechanical:
                    return EligibleMechReviewersList;
                case DepartmentNameEnums.Plumbing:
                    return EligiblePlumbReviewersList;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_County:
                    return EligibleZoneCountyReviewersList;
                case DepartmentNameEnums.Zone_Mint_Hill:
                    return EligibleZoneMintHillReviewersList;
                case DepartmentNameEnums.Zone_Huntersville:
                    return EligibleZoneHuntersvilleReviewersList;
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                    return EligibleZoneCharlotteReviewersList;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_County:
                    return EligibleFireCountyReviewersList;
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                    return EligibleFireCharlotteReviewersList;
                case DepartmentNameEnums.EH_Day_Care:
                    return EligibleDayCareReviewersList;
                case DepartmentNameEnums.EH_Food:
                    return EligibleFoodServiceReviewersList;
                case DepartmentNameEnums.EH_Pool:
                    return EligiblePoolReviewersList;
                case DepartmentNameEnums.EH_Facilities:
                    return EligibleFacilityReviewersList;
                case DepartmentNameEnums.Backflow:
                    return EligibleBackFlowReviewersList;
                default:
                    return new List<AutoSchedulableReviewer>();
            }
        }

        protected bool FindMeetingSlotAvailableForTheDay(PlanReviewAutoSchedulableReviewer reviewer, DateTime day, decimal meetingDurationHrs
            , out decimal carryoverHrs, bool continueAllocationFlag, decimal originalMeetingDuration)
        {
            try
            {
                Dictionary<string, List<TimeSlot>> allSlotsForTheDay = CreateAllTimeSlotsForTheDay(day);
                List<TimeSlot> allocatedTimeSlotsBy15Min = new List<TimeSlot>();
                List<TimeSlot> pastWorkingDaySharedMeetings = new List<TimeSlot>(), futureWorkingDaySharedMeetings = new List<TimeSlot>();
                DateTime lastWorkDay = LastWorkingDay(day), nextWorkDay = NextWorkingDay(day);
                List<TimeSlot> AllCurrentMeetingsForReviewer = reviewer.SelectedReviewer.CurrentMeetings.Where(x => x != null
                && x.StartTime.Date == day.Date).ToList();

                //This is used only for include the hrs from same person from other dept if he is allocated in last loop.
                AllCurrentMeetingsForReviewer.AddRange(reviewer.SelectedReviewer.AllocatedTimeSlots.Where(x => x.StartTime.Date == day.Date).ToList());
                List<TimeSlot> todayMeetings = AllCurrentMeetingsForReviewer.Where(x => x.StartTime.Date == day.Date).ToList();

                if (todayMeetings.Count > 0)
                {
                    pastWorkingDaySharedMeetings =
                        (from x in AllCurrentMeetingsForReviewer
                         where x.ProjectID != 0 && x.StartTime.Date <= lastWorkDay.Date && todayMeetings.Any(y => y.ProjectID == x.ProjectID)
                         && IsAnyProject(x.AllocationType) == true
                         select x).ToList();

                    futureWorkingDaySharedMeetings =
                        (from x in AllCurrentMeetingsForReviewer
                         where x.ProjectID != 0 && x.StartTime.Date >= nextWorkDay.Date && todayMeetings.Any(y => y.ProjectID == x.ProjectID)
                         && IsAnyProject(x.AllocationType) == true
                         select x).ToList();

                    //NextWorkingDaySharedMeetings = reviewer.SelectedReviewer.CurrentMeetings
                    //    .Where(x => x.StartTime.Date == nextWorkDay.Date && todayMeetings.Any(y => y.ProjectScheduleID == x.ProjectScheduleID)).ToList();

                    allocatedTimeSlotsBy15Min = todayMeetings.SelectMany(x => SplitTimeSlotByTimeSlotIntervalMinsQuarterTime(x)).ToList();

                    //Rule: You cannot interrupt the plan review of 1 project with another project.
                    /* If any of the current day's project is spread across yesterday or any day 
                     * in the past AND also in future that means we cannot allocate a project for 
                     * today since that project is spread across > 3 days with today being in middle
                     */
                    if (futureWorkingDaySharedMeetings.Any(x => pastWorkingDaySharedMeetings.Any(y => x.ProjectID != CurrentProject.ID
                    && x.ProjectID != 0 && x.ProjectID == y.ProjectID)) == true)
                    {
                        carryoverHrs = meetingDurationHrs;
                        return false;
                    }
                    /* In case on continuing loop, if the same plan reviewer is assigned to 2 + depts then those cannot overlap
                     * with in the department but can only allocated only if one dept work allotment hrs are done.
                     * So if the dept work is goiing across 3 days then middle day cannot be allocated.
                     */
                    if (futureWorkingDaySharedMeetings.Any(x => pastWorkingDaySharedMeetings.Any(y => x.ProjectID == CurrentProject.ID
                    && x.DepartmentName == y.DepartmentName)) == true)
                    {
                        carryoverHrs = meetingDurationHrs;
                        return false;
                    }
                }
                //get the number of allocated slots by 15 minutes.
                int allocatedSlots = 0;
                //fixates the allocated slots including duplicated slots.
                foreach (var item in allocatedTimeSlotsBy15Min)
                {
                    if (allSlotsForTheDay.ContainsKey(GetTimeString(item.StartTime)))
                    {
                        List<TimeSlot> slot = allSlotsForTheDay[GetTimeString(item.StartTime)];
                        slot.Add(item);
                        /* When you count hours, used even if the hr is overlapped (eg: NPA:meeting 9.15 - 9.30, Express:9.15 -9.30 
                         * it still counts as two seperate hrs(30min toatl used for the day)  
                         * and will be counted as total number of hrs used up against day for 8 hrs.*/
                        allocatedSlots++;
                        slot.Remove(slot.Where(x => x.AllocationType == TimeAllocationType.NA).FirstOrDefault());
                    }
                }

                //this is the number of slots we can allocate in this day.
                int freeSlots = System.Convert.ToInt32(reviewer.SelectedReviewer.AllowedHoursPerDay * 4) - allocatedSlots; //this is the maximum slot we can allocate for this date. Allowed hours per day * 4 (15 minute intervals)
                //if (freeSlots <= 0)
                //{
                //    carryoverHrs = meetingDurationHrs;
                //    return false;
                //}

                /*Individual trade plan review time is 24 hours or more the MMF plan reviewers will not be extended to 8 hours of plan review per day. 
                 * They will use their project type setting or their individual plan reviewer profile setting. */
                if (freeSlots > 0 && CurrentProject.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family && originalMeetingDuration >= 24)
                {
                    //convert 100 based minute to 60 based minute
                    double hrs = (double)(freeSlots / 4) + Math.Round(((double)(((decimal)(freeSlots / (double)4) % 1))) * (double).60, 2);
                    if ((decimal)hrs > reviewer.SelectedReviewer.AllowedHoursPerDay)
                        freeSlots = System.Convert.ToInt32(reviewer.SelectedReviewer.AllowedHoursPerDay * 4);
                }
                //convert Hr into 15 min slots and take the count. 1.5 hr = 6 slots
                int totalMeetingSlots = (int)((decimal)(60 / TimeSlotIntervalByMinutes) * (decimal)meetingDurationHrs);

                //Rule: You cannot interrupt the plan review of 1 project with another project.
                /* Tag: *XRef25 :If the hours are more than allowedFreeSlots and is there is a project which started today 
                 * and goes to next day then we cannot allocate a project today since it need tomorrow and it will be overlapped */
                if (freeSlots < totalMeetingSlots && futureWorkingDaySharedMeetings.Any(x => todayMeetings.Any(y => x.ProjectID == y.ProjectID)) == true)
                {
                    carryoverHrs = meetingDurationHrs;
                    return false;
                }

                /* 
                * find how many slots are free for the meeting in this day. eg: meetingDurationHrs =  1.5 then 1.5/(30/60 = .5).5 = 3
                * Also need to find overlap. If the new project or current is goining over 1 days then look for morning hrs + evening hrs 
                * else just consder the total hrs of the project to find overlap.
                */

                int currentSlots = freeSlots;
                int leftoverMeetingSlots = totalMeetingSlots;
                DateTime currentHr = day.Date.AddHours(8); //start at 8 AM.
                bool allocationStarted = continueAllocationFlag;
                do
                {
                    //by default there will be only 1 item/NA. If there are more items then NA should not be there.
                    //As of now picking the first item and process it.
                    //TBD: consider multiple timeslots to handle overlaps logic.
                    List<TimeSlot> currentSlotlst = allSlotsForTheDay[GetTimeString(currentHr)];
                    //If NA then there is supposed to be only one records and that need to be an NA.
                    if (currentSlotlst.Count == 1 && currentSlotlst[0].AllocationType == TimeAllocationType.NA
                        && currentSlots > 0 && leftoverMeetingSlots > 0)
                    {
                        currentSlotlst[0].StartTime = currentHr;
                        currentSlotlst[0].EndTime = currentHr.AddMinutes(15);
                        currentSlotlst[0].AllocationType = TimeAllocationType.Project;
                        currentSlotlst[0].DepartmentName = reviewer.PlanReviewerDept;
                        reviewer.SelectedReviewer.WIPTimeSlots.Add(currentSlotlst[0]);
                        currentSlots--;
                        leftoverMeetingSlots--;
                        allocationStarted = true;
                    }
                    else
                    {
                        /* If allocation is started and the current day is fully allocated but then there is still 
                         * hrs left to allocate then need to verify  the rest of the day contains any projects so 
                         * that it will not cross over with that. This case is the one left over from multi day project
                         * which is already checked in 'Tag: *XRef25' above
                         */

                        //pending item, check this is needing any  (currentSlot.AllocationType == TimeAllocationType.NPA_PersonalTime checks

                        if (allocationStarted == true && IsAnyProject(currentSlotlst) == true)
                        {
                            //Plan review projects of 24 hours or more can be scheduled around Express review reserved/scheduled time. 
                            if (!((TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.Project_Express_Blocked) != null
                                  || TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.Project_Express_Reserved) != null)
                                  && originalMeetingDuration >= 24))
                            {
                                carryoverHrs = meetingDurationHrs;
                                return false;
                            }
                        }
                        if (allocationStarted == true && IsAnyNPA(currentSlotlst) == true)
                        {
                            //Skip this day since there is project allocated to this timeslot
                            //Or skip the slot since as per allocation type we can go over it.

                            /* Plan review projects of 24 hours or more can be scheduled around staff meeting NPA types. 
                             * (the plan review hours can be scheduled to begin before the staff meeting NPA and then continue 
                             * to be completed after the staff meeting NPA)
                             * 
                             * If the project is 24 hours or more and  personal time is 8 hours or less consecutively the project 
                             * can wrap around that personal time.
                             */

                            if ((TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.NPA_Staff_Meeting) != null && originalMeetingDuration < 24)
                                ||
                                (TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.NPA_PersonalTime) != null && originalMeetingDuration < 24
                                && TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.NPA_PersonalTime).TotalTimeOfProject.Hours > 8)
                                ||
                                (TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.NPA_PersonalTime) != null && originalMeetingDuration < 40
                                && TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.NPA_PersonalTime).TotalTimeOfProject.Hours <= 4
                                && TimeSlotContainsAllocation(currentSlotlst, TimeAllocationType.NPA_PersonalTime).TotalTimeOfProject.Hours > 16))
                            {
                                // No Wrap around. Satified conditions:
                                //1) For projects plan review less than 24 hours, Personal time is  4 hours ore more cannot have a project wrap around that.  
                                //2) Project is 40 hours or more and Personal time longer than 16 hours cannot have a project wrap around that. 
                                //3) + Any cases which does not fall around above if condition
                                carryoverHrs = meetingDurationHrs; //No Warp around. Cancel all allocation slots for the day and skip to next day.
                                return false;
                            }
                        }
                    }
                    currentHr = currentHr.AddMinutes(15);//adds time by 15 min
                } while (GetTimeString(currentHr) != "1700" && leftoverMeetingSlots > 0 && currentSlots > 0);

                //at this point it will be either all slots for the day is alloted or meeting is fully allocated.
                // Calculate balance hrs left. 
                carryoverHrs = (decimal)leftoverMeetingSlots / (decimal)4; //convert 15 minute slots into hr. VS show incorrect cast warning. need cast here or else decimal is gone.

                if (leftoverMeetingSlots == totalMeetingSlots) { return false; } // if nothing allocated for the day

                return true;
            }
            catch
            {
                carryoverHrs = meetingDurationHrs;
                return false;
            }
        }

        public bool AllocateFirstMeetingSlotAvailable(PlanReviewAutoSchedulableReviewer reviewer, decimal meetingDurationHrs, DateTime startDate, DateTime allowedMaxDate)
        {
            //If not able to use requested dates then Schedules to first available date and time frame based on plan reviewer availability.  
            //Find the first slot where all ther reviewers are available.

            DateTime masterStartDate = startDate;
            bool allocationSuccess = false;
            do
            {
                //Reset data on each loop.
                reviewer.SelectedReviewer.WIPTimeSlots.Clear(); //clear existing allocations and restart it again.
                allocationSuccess = SearchForFirstMeetingSlotAvailable(reviewer, meetingDurationHrs, masterStartDate, allowedMaxDate);
                //prepare for next loop.
                masterStartDate = NextWorkingDay(masterStartDate);  //increment day by 1. + performance optimization. If weekend or holiday then skip that date.
            } while (allocationSuccess == false && masterStartDate <= allowedMaxDate); //business rule says no project can run beyond 5 working days
            //at this point the allocation should be done and it should be there in reviewer object. So flatten it.
            if (allocationSuccess == true)
            {
                FlattenTimeSlots(reviewer);
                if (reviewer.SelectedReviewer.WIPTimeSlots.Count > 0)
                {
                    reviewer.SelectedReviewer.WIPTimeSlots.OrderBy(x => x.StartTime).ThenBy(x => x.EndTime);
                    reviewer.AllotedStartDt = reviewer.SelectedReviewer.WIPTimeSlots[0].StartTime;
                    reviewer.AllotedEndDt = reviewer.SelectedReviewer.WIPTimeSlots[reviewer.SelectedReviewer.WIPTimeSlots.Count - 1].EndTime;
                }
                return true;
            }
            else
                return false;
        }

        protected bool FlattenTimeSlots(PlanReviewAutoSchedulableReviewer reviewer)
        {
            if (reviewer == null) //NA or not picked up then just return
                return true;
            List<TimeSlot> ret = new List<TimeSlot>();
            //sort the list first based on start time. each time is supposed to be 15 minutes apart.
            List<TimeSlot> allocatedTimeSlots = reviewer.SelectedReviewer.WIPTimeSlots.OrderBy(x => x.StartTime).ToList();
            //merge all 15 minute timeslots based on continous projects.
            TimeSlot newSlot = new TimeSlot();
            TimeSlot lastSlot = new TimeSlot();
            if (allocatedTimeSlots != null && allocatedTimeSlots.Count > 0)
            {
                newSlot = allocatedTimeSlots[0];
                lastSlot = allocatedTimeSlots[0];
            }
            bool firstLeg = false;
            foreach (var item in allocatedTimeSlots)
            {
                if (firstLeg == true && lastSlot.EndTime != item.StartTime)
                {
                    newSlot.EndTime = lastSlot.EndTime;
                    newSlot.AllocationType = item.AllocationType;
                    newSlot.ProjectID = CurrentProject.ID;
                    newSlot.ProjectScheduleTypeName = "PMA";
                    newSlot.UserID = reviewer.SelectedReviewer.UserIdentity.ID;
                    //newSlot.ProjectScheduleID = to be filled after ProjectScheduleID is saved.
                    ret.Add(newSlot);
                    newSlot = item;
                }
                firstLeg = true;
                lastSlot = item;
            }

            newSlot.EndTime = lastSlot.EndTime;
            newSlot.ProjectID = CurrentProject.ID;
            newSlot.ProjectScheduleTypeName = "PMA";
            newSlot.UserID = reviewer.SelectedReviewer.UserIdentity.ID;
            ret.Add(newSlot);
            reviewer.SelectedReviewer.WIPTimeSlots = ret;
            return true;
        }

        protected bool SearchForFirstMeetingSlotAvailable(PlanReviewAutoSchedulableReviewer reviewer, decimal meetingDurationHrs, DateTime startDate, DateTime maxAllowedEndDate)
        {
            decimal currentMeetingHrs = meetingDurationHrs;
            DateTime currentDay = startDate;
            bool multiplierFlag = false;
            decimal multiplierHours = 0;

            /* All plan reviewers need to complete their plan review within 5 business days of each other. 
             * So one plan reviewer cannot have a start date more than 5th day of start since there 
             * could be anotehr dept which could be starting from day one.*/

            decimal hrBalance = currentMeetingHrs;
            bool continueAllocationFlag = false;
            //Continues until start date and end date is found and there is no more balance of hours left to allocate. 
            do
            {
                //check if current date falls within a multiplier daterange
                if (multiplierFlag == false)
                {
                    if (_multipliers != null && _multipliers.Count > 0 && _multipliers.Any(x => x.start <= currentDay && x.end >= currentDay))
                    {
                        multiplierHours = AddSchedulingMultiplier(meetingDurationHrs, currentDay) - meetingDurationHrs;
                        multiplierFlag = true;//set multiplier as already applied
                        currentMeetingHrs += multiplierHours;
                    }
                }
                else
                {
                    //if no hours have been allocated yet and the schedule window has passed the multiplier window, remove the hours
                    if (reviewer.SelectedReviewer.WIPTimeSlots.Count == 0 && !_multipliers.Any(x => x.start <= currentDay && x.end >= currentDay))
                    {
                        currentMeetingHrs -= multiplierHours;
                        multiplierFlag = false;
                    }
                }

                //if none of the hours is assigned until now then this day is considered as start day of meeting for this guy.
                if (IsDateHolidayOrWeekEnd(currentDay) == false)
                {
                    if (FindMeetingSlotAvailableForTheDay(reviewer, currentDay, currentMeetingHrs, out hrBalance, continueAllocationFlag, meetingDurationHrs) == false)
                    {
                        //there was an issue with allocation until today and so we need to start allocation from next working day.
                        reviewer.SelectedReviewer.WIPTimeSlots.Clear();
                        currentMeetingHrs = meetingDurationHrs;// original hr
                        multiplierHours = 0;
                        multiplierFlag = false;//reset multiplier flag
                        continueAllocationFlag = false;
                    }
                    else
                    {
                        currentMeetingHrs = hrBalance;
                        continueAllocationFlag = true;
                    }
                }
                currentDay = currentDay.AddDays(1);
            }
            while (currentMeetingHrs > 0 && currentDay <= maxAllowedEndDate);
            //if there is still hrs left then it means it is more than 1 week / 5 business days and so cannot allocate.
            if (hrBalance > 0)
                return false;
            else
                return true;
        }

        protected DateTime RevalidateNextCurrentLastAllowedWorkingDay(DateTime newDate, DateTime currentDate)
        {
            DateTime newLstDt = GetCurrentLastAllowedWorkingDay(newDate);
            if (newLstDt < currentDate)
                return newLstDt;
            else
                return currentDate;
        }

        protected DateTime GetCurrentLastAllowedWorkingDay(DateTime start)
        {
            DateTime lastAllowedDay = start;
            int i = 0;
            while (i < 5)
            {
                lastAllowedDay = NextWorkingDay(lastAllowedDay);
                i++;
            }
            return lastAllowedDay;
        }


        protected bool IsDatesIsInAllowedDateRange(AutoScheduledPlanReviewValues data)
        {
            DateTime start = data.ElectricScheduleStart.HasValue ? data.ElectricScheduleStart.Value : DateTime.MaxValue.Date;
            start = data.MechScheduleStart.HasValue && data.MechScheduleStart.Value < start ? data.MechScheduleStart.Value : start;
            start = data.BuildingScheduleStart.HasValue && data.BuildingScheduleStart.Value < start ? data.BuildingScheduleStart.Value : start;
            start = data.PlumbScheduleStart.HasValue && data.PlumbScheduleStart.Value < start ? data.PlumbScheduleStart.Value : start;
            start = data.FireScheduleStart.HasValue && data.FireScheduleStart.Value < start ? data.FireScheduleStart.Value : start;
            start = data.ZoneScheduleStart.HasValue && data.ZoneScheduleStart.Value < start ? data.ZoneScheduleStart.Value : start;
            start = data.FoodScheduleStart.HasValue && data.FoodScheduleStart.Value < start ? data.FoodScheduleStart.Value : start;
            start = data.PoolScheduleStart.HasValue && data.PoolScheduleStart.Value < start ? data.PoolScheduleStart.Value : start;
            start = data.FacilityScheduleStart.HasValue && data.FacilityScheduleStart.Value < start ? data.FacilityScheduleStart.Value : start;
            start = data.DayCareScheduleStart.HasValue && data.DayCareScheduleStart.Value < start ? data.DayCareScheduleStart.Value : start;
            start = data.BackFlowScheduleStart.HasValue && data.BackFlowScheduleStart.Value < start ? data.BackFlowScheduleStart.Value : start;

            DateTime end = data.ElectricScheduleEnd.HasValue ? data.ElectricScheduleEnd.Value : DateTime.MinValue;
            end = data.MechScheduleEnd.HasValue && data.MechScheduleEnd.Value > end ? data.MechScheduleEnd.Value : end;
            end = data.BuildingScheduleEnd.HasValue && data.BuildingScheduleEnd.Value > end ? data.BuildingScheduleEnd.Value : end;
            end = data.PlumbScheduleEnd.HasValue && data.PlumbScheduleEnd.Value > end ? data.PlumbScheduleEnd.Value : end;
            end = data.FireScheduleEnd.HasValue && data.FireScheduleEnd.Value > end ? data.FireScheduleEnd.Value : end;
            end = data.ZoneScheduleEnd.HasValue && data.ZoneScheduleEnd.Value > end ? data.ZoneScheduleEnd.Value : end;
            end = data.FoodScheduleEnd.HasValue && data.FoodScheduleEnd.Value > end ? data.FoodScheduleEnd.Value : end;
            end = data.PoolScheduleEnd.HasValue && data.PoolScheduleEnd.Value > end ? data.PoolScheduleEnd.Value : end;
            end = data.FacilityScheduleEnd.HasValue && data.FacilityScheduleEnd.Value > end ? data.FacilityScheduleEnd.Value : end;
            end = data.DayCareScheduleEnd.HasValue && data.DayCareScheduleEnd.Value > end ? data.DayCareScheduleEnd.Value : end;
            end = data.BackFlowScheduleEnd.HasValue && data.BackFlowScheduleEnd.Value > end ? data.BackFlowScheduleEnd.Value : end;

            //find 5 days ahead by considering holidays and weekends.
            DateTime lastAllowedDay = GetCurrentLastAllowedWorkingDay(start);
            //Schedule all plan reviewers to complete their plan review within 5 business days of each other.
            //If not then return false and then increment start date by 1.
            if (end.Date <= lastAllowedDay.Date)
                return true;
            else
                return false;
        }

        protected bool IsAllTradesAndAgenciesScheduled(AutoScheduledPlanReviewValues data, AutoScheduledPlanReviewParams parms)
        {
            //get the required meetingdurationhours, make a list of trades/agencies to compare to
            //get the scheduled and compare
            bool isSuccess = false;

            if ((MeetingDurationBackFlow == 0 || (MeetingDurationBackFlow > 0 && (data.BackFlowScheduleStart.HasValue || parms.BackFlowIsPool)))

                && (MeetingDurationBuild == 0 || (MeetingDurationBuild > 0 && (data.BuildingScheduleStart.HasValue || parms.BuildingIsPool)))

                && (MeetingDurationDayCare == 0 || (MeetingDurationDayCare > 0 && (data.DayCareScheduleStart.HasValue || parms.DayCareIsPool)))

                && (MeetingDurationElectric == 0 || (MeetingDurationElectric > 0 && (data.ElectricScheduleStart.HasValue || parms.ElectricIsPool)))

                && (MeetingDurationFacility == 0 || (MeetingDurationFacility > 0 && (data.FacilityScheduleStart.HasValue || parms.FacilityIsPool)))

                && (MeetingDurationFire == 0 || (MeetingDurationFire > 0 && (data.FireScheduleStart.HasValue || parms.FireIsPool)))

                && (MeetingDurationFood == 0 || (MeetingDurationFood > 0 && (data.FoodScheduleStart.HasValue || parms.FoodServiceIsPool)))

                && (MeetingDurationMech == 0 || (MeetingDurationMech > 0 && (data.MechScheduleStart.HasValue || parms.MechIsPool)))

                && (MeetingDurationPlumb == 0 || (MeetingDurationPlumb > 0 && (data.PlumbScheduleStart.HasValue || parms.PlumbIsPool)))

                && (MeetingDurationPool == 0 || (MeetingDurationPool > 0 && (data.PoolScheduleStart.HasValue || parms.PoolIsPool)))

                && (MeetingDurationZone == 0 || (MeetingDurationZone > 0 && (data.ZoneScheduleStart.HasValue || parms.ZoneIsPool))))

            {
                isSuccess = true;
            }


            //if isSuccess == true, then all required were done and this is complete
            return isSuccess;
        }

        //Checks if this is a working day
        protected static bool IsDateHolidayOrWeekEnd(DateTime date)
        {
            return HolidayList.Any(x => x.Date == date.Date)
               || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }


        protected DateTime LastWorkingDay(DateTime date)
        {
            DateTime ret = date;
            do
            {
                ret = ret.AddDays(-1);
            }
            while (IsDateHolidayOrWeekEnd(ret));

            return ret;
        }

        protected static DateTime NextWorkingDay(DateTime date)
        {
            DateTime ret = date;
            do
            {
                ret = ret.AddDays(1);
            }
            while (IsDateHolidayOrWeekEnd(ret));

            return ret;
        }

        protected bool IsAnyProject(List<TimeSlot> timeslots)
        {
            return timeslots.Any(x => x.ProjectID != CurrentProject.ID && IsAnyProject(x.AllocationType));
        }

        protected TimeSlot TimeSlotContainsAllocation(List<TimeSlot> timeslots, TimeAllocationType allotType)
        {
            return timeslots.Where(x => x.AllocationType == allotType).FirstOrDefault();
        }

        protected bool IsAnyProject(TimeAllocationType x)
        {
            if (x == TimeAllocationType.Project || x == TimeAllocationType.Project_Express_Blocked || x == TimeAllocationType.Project_Express_Reserved
                         || x == TimeAllocationType.Project_PlanReview || x == TimeAllocationType.Project_Prelim)
                return true;
            else
                return false;
        }

        protected bool IsAnyNPA(List<TimeSlot> timeslots)
        {
            return timeslots.Any(x => IsAnyNPA(x.AllocationType));
        }

        protected bool IsAnyNPA(TimeAllocationType x)
        {
            if (x == TimeAllocationType.NPA || x == TimeAllocationType.NPA_Staff_Meeting || x == TimeAllocationType.NPA_PersonalTime
                         || x == TimeAllocationType.Holiday || x == TimeAllocationType.Break ||
                         x == TimeAllocationType.WeekEnd)
                return true;
            else
                return false;
        }

        protected string GetTimeString(DateTime time)
        {
            return time.ToString("HHmm");
        }

        protected Dictionary<string, List<TimeSlot>> CreateAllTimeSlotsForTheDay(DateTime startDt)
        {
            Dictionary<string, List<TimeSlot>> ret = new Dictionary<string, List<TimeSlot>>();
            DateTime current = startDt.Date.AddHours(8);//starts ata 8 am
            do //8 AM to 5 PM 
            {
                List<TimeSlot> lst = new List<TimeSlot>();
                TimeSlot t = new TimeSlot();
                t.AllocationType = TimeAllocationType.NA;
                lst.Add(t);
                ret.Add(GetTimeString(current), lst);
                current = current.AddMinutes(15);
            } while (GetTimeString(current) != "1700");
            return ret;
        }

        protected class Multiplier
        {
            public DateTime start = DateTime.Now;
            public DateTime end = DateTime.Now;
            public decimal factor;
            public List<int> propertyTypes = new List<int>();
            public bool isPercentage;
            public bool isHours;
        }

        protected List<Multiplier> _multipliers;

        protected void GetMultipliers()
        {
            _multipliers = new List<Multiplier>();
            CultureInfo provider = CultureInfo.InvariantCulture;
            AdminAdapter thisengine = new AdminAdapter();
            List<CatalogItem> catalogs = thisengine.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER");
            if (catalogs.Count > 0)
            {
                Multiplier m = new Multiplier();
                var use = catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.USE").FirstOrDefault();
                if (use != null)
                {
                    if (use.Value == "-1")
                        return;
                    if (use.Value == "Percentage")
                        m.isPercentage = true;
                    else if (use.Value == "Hours")
                        m.isHours = true;
                }
                else
                    return;
                m.start = DateTime.ParseExact(catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.START_DATE").FirstOrDefault().Value, "MM/dd/yyyy", provider);
                m.end = DateTime.ParseExact(catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.END_DATE").FirstOrDefault().Value, "MM/dd/yyyy", provider);
                m.factor = decimal.Parse(double.Parse(catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR").FirstOrDefault().Value).ToString("0.##"));
                m.propertyTypes = GetselectedSMProjectTypeList(catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE").FirstOrDefault().Value);
                _multipliers.Add(m);
            }

        }

        protected decimal AddSchedulingMultiplier(decimal defaultHrs, DateTime date)
        {
            decimal ret = defaultHrs;
            Multiplier m = _multipliers.FirstOrDefault();
            if (m.propertyTypes.Any(x => x == (int)CurrentProject.AccelaPropertyType) == false)
                return defaultHrs;
            if (m == null) return ret;
            if (date >= m.start && date < m.end)
            {
                if (m.isPercentage == true)
                {
                    ret = (ret / (decimal)100) * m.factor;
                }
                else if (m.isHours == true)
                {
                    ret = ret + m.factor;
                }
            }
            return ret;
        }
        protected List<int> GetselectedSMProjectTypeList(string projectTypeList)
        {
            List<string> projectType = new List<string>(projectTypeList.Split(',').Select(s => s));
            return projectType.Select(x => int.Parse(x)).ToList();
        }
        public void CalculateMeetingDuration()
        {
            List<ProjectBusinessRelationshipBE> pbrList =
                new ProjectBusinessRelationshipBO().GetListByProjectId(CurrentProject.ID);

            foreach (var item in CurrentProject.Trades)
            {
                switch (item.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:

                        MeetingDurationBuild = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.Electrical:
                        MeetingDurationElectric = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.Mechanical:
                        MeetingDurationMech = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.Plumbing:
                        MeetingDurationPlumb = GetPBREstimationHours(pbrList, item);
                        break;
                }
            }

            foreach (var item in CurrentProject.Agencies)
            {
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
                        MeetingDurationZone = GetPBREstimationHours(pbrList, item);
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
                        MeetingDurationFire = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        MeetingDurationDayCare = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.EH_Food:
                        MeetingDurationFood = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        MeetingDurationPool = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        MeetingDurationFacility = GetPBREstimationHours(pbrList, item);
                        break;
                    case DepartmentNameEnums.Backflow:
                        MeetingDurationBackFlow = GetPBREstimationHours(pbrList, item);
                        break;
                    default:
                        break;
                }
            }

            List<decimal> allMeetingDurations = new List<decimal>()
            {
                MeetingDurationBuild,
                MeetingDurationElectric,
                MeetingDurationMech,
                MeetingDurationPlumb,
                MeetingDurationZone,
                MeetingDurationFire,
                MeetingDurationFood,
                MeetingDurationDayCare,
                MeetingDurationPool,
                MeetingDurationFacility,
                MeetingDurationBackFlow
            };

            MeetingDuration = allMeetingDurations.Max();
        }

        private decimal GetPBREstimationHours(List<ProjectBusinessRelationshipBE> pbrList, ProjectDepartment item)
        {
            decimal estimationHours;
            if (item.EstimationHours.HasValue == false || item.EstimationHours.Value == 0)
            {
                ProjectBusinessRelationshipBE pbr = pbrList.Where(x => x.BusinessRefId == (int)item.DepartmentInfo).FirstOrDefault();
                estimationHours = pbr.EstimationHoursNbr.Value;

            }
            else
            {
                estimationHours = item.EstimationHours.Value;
            }
            return estimationHours;
        }

        /// <summary>
        /// Takes the list and gets the reviewer with the soonest availability
        ///     ordered by last name, first name
        ///     
        /// Used in PlanReviewProjectSchedulingEngine and PrelimProjectSchedulingEngine
        /// 
        /// returns reviewer user id and the reviewer
        /// </summary>
        /// <param name="departmentReviewerList"></param>
        /// <param name="meetingDuration"></param>
        /// <param name="dateRangeStartDt"></param>
        /// <param name="dateRangeEndDt"></param>
        /// <param name="prelimReviewer"></param>
        /// <returns></returns>
        public int GetSoonestAvailableTradeReviewer(List<AutoSchedulableReviewer> departmentReviewerList, decimal meetingDuration, DateTime dateRangeStartDt, DateTime dateRangeEndDt, out AutoSchedulableReviewer prelimReviewer)
        {
            prelimReviewer = null;
            int reviewerNA = -1;

            //LES- 4526 - jcl - choose the plan reviewer with the soonest availability
            //for each reviewer, find the first one with an allocation for start and end
            // once we get allocation of reviewer, get out of loop and return that reviewer
            // order the reviewer list by last name then first name

            //get the alloted start date and end date for each reviewer
            List<PlanReviewAutoSchedulableReviewer> planReviewAutoSchedulableReviewers =
                departmentReviewerList.Select(x => new PlanReviewAutoSchedulableReviewer(x)).ToList();

            planReviewAutoSchedulableReviewers.Select(x =>
                AllocateFirstMeetingSlotAvailable(
                    x,
                    meetingDuration,
                    dateRangeStartDt,
                    dateRangeEndDt)).ToList();

            //select the reviewer with the soonest date by last name, first name
            PlanReviewAutoSchedulableReviewer planReviewAutoSchedulableReviewer =
                planReviewAutoSchedulableReviewers.OrderBy(x => x.AllotedStartDt)
               .ThenBy(x => x.SelectedReviewer.UserIdentity.LastName)
               .ThenBy(x => x.SelectedReviewer.UserIdentity.FirstName).ToList().FirstOrDefault();

            if (planReviewAutoSchedulableReviewer == null)
                return reviewerNA;
            else
            {
                AutoSchedulableReviewer selectedReviewer = departmentReviewerList.Where(x => x.UserIdentity.ID == planReviewAutoSchedulableReviewer.SelectedReviewer.UserIdentity.ID).FirstOrDefault();
                prelimReviewer = selectedReviewer;
                return selectedReviewer.UserIdentity.ID;
            }
        }

        public decimal GetMeetingHoursDurationByDept(DepartmentNameEnums department)
        {
            switch (department)
            {
                case DepartmentNameEnums.Building:
                    return MeetingDurationBuild;
                case DepartmentNameEnums.Electrical:
                    return MeetingDurationElectric;
                case DepartmentNameEnums.Mechanical:
                    return MeetingDurationMech;
                case DepartmentNameEnums.Plumbing:
                    return MeetingDurationPlumb;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return MeetingDurationZone;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return MeetingDurationFire;
                case DepartmentNameEnums.EH_Day_Care:
                    return MeetingDurationDayCare;
                case DepartmentNameEnums.EH_Food:
                    return MeetingDurationFood;
                case DepartmentNameEnums.EH_Pool:
                    return MeetingDurationPool;
                case DepartmentNameEnums.EH_Facilities:
                    return MeetingDurationFacility;
                case DepartmentNameEnums.Backflow:
                    return MeetingDurationBackFlow;
                default:
                    return 0;
            }
        }
    }
}