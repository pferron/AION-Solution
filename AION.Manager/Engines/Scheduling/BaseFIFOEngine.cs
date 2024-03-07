using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Helpers;
using AION.Manager.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Engines.Scheduling
{
    public class BaseFIFOEngine : BaseSchedulingEngine
    {
        protected FIFOEngineParams RequestData;
        protected AutoScheduledFIFOValues AutoScheduledValues;
        protected DateTime AutoSchedulePeriodStart { get; set; } = DateTime.Now.AddDays(1);
        protected DateTime AllowedMaxEndDate { get; set; }
        public bool IsFifoScheduled { get; set; } = false;
        protected SameBuildingContractor _SameBuildingContractor;

        protected new List<AutoSchedulableReviewer> GetAllEligibleReviewers()
        {
            UserAdapter useradapter = new UserAdapter();

            UserIdentityModelBO bo = new UserIdentityModelBO();

            List<AutoSchedulableReviewer> users = useradapter.GetReviewers((int)CurrentProject.AionPropertyType, (int)DepartmentNameEnums.NA)
                .Select(x => new AutoSchedulableReviewer() { UserIdentity = x }).ToList();

            List<AutoSchedulableReviewer> eligibleUsers = SetUpEligibleUsers(users);

            return eligibleUsers;
        }

        protected List<AutoSchedulableReviewer> SetUpEligibleUsers(List<AutoSchedulableReviewer> users)
        {
            UserAdapter useradapter = new UserAdapter();

            var mAllSqFtList = useradapter.GetAllSquareFootageList();

            users.ForEach(x => x.UserIdentity.FillDesignatedDepartments());

            for (int i = 0; i < users.Count; i++)
            {
                var mUserMgmtOccu = useradapter.GetSquareFootageListbyUserOccupancy(users[i].UserIdentity.ID);
                
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

            List<AutoSchedulableReviewer> ret = users.Where(x =>
                   (IsProjectLevelAllowed(x.AllowedOccupancies, x.UserIdentity.SchedulableLevel, CurrentProject.ProjectLvlTxt) == true)
                    && x.UserIdentity.IsSchedulable == true).ToList();

            for (int i = 0; i < ret.Count; i++)
            {
                decimal totalHoursForCapacity = 0M;
                ret[i].CurrentMeetings = useradapter.GetUsedTimeSlotsExtrasByUserID(ret[i].UserIdentity.ID, AutoSchedulePeriodStart, AllowedMaxEndDate);
                if (ret[i].CurrentMeetings != null)
                {
                    ret[i].CurrentMeetings = AdjustReservedTimeSlots(ret[i].CurrentMeetings);
                    totalHoursForCapacity = GetTotalHoursForCapacity(ret[i].CurrentMeetings);

                }
                ret[i].TotalHoursForCapacity = totalHoursForCapacity;
            }

            return ret.OrderBy(x => x.UserIdentity.LastName).ThenBy(x => x.UserIdentity.LastName).ToList();
        }

        protected AllocationValues GetAllocation(PlanReviewAutoSchedulableReviewer reviewer, decimal meetingDuration)
        {
            AllocationValues ret = new AllocationValues();

            bool allocationFound = false;

            allocationFound = AllocateFirstMeetingSlotAvailable(reviewer, meetingDuration, AutoSchedulePeriodStart, AllowedMaxEndDate);
            if (allocationFound == true)
            {
                ret.Success = true;
                ret.Hours = meetingDuration;
                ret.ScheduleStart = reviewer.AllotedStartDt; //assigns by ref value returned from above method. 
                ret.ScheduleEnd = reviewer.AllotedEndDt; //assigns by ref value returned from above method.
                reviewer.SelectedReviewer.WIPTimeSlots.ForEach(x => x.DepartmentName = DepartmentNameEnums.Building);
                /* Copy all the allocations for specific user to be considered if he is allocated into more than one dept.*/
                reviewer.SelectedReviewer.AllocatedTimeSlots.AddRange(reviewer.SelectedReviewer.WIPTimeSlots);
                /* Keeping track of the allocated hrs of the reviewer in each dept. This could be overwritten if same reviewer is going multiple depts*/
                reviewer.WIPReviewerTimeSlots.AddRange(reviewer.SelectedReviewer.WIPTimeSlots);
                AllowedMaxEndDate = RevalidateNextCurrentLastAllowedWorkingDay(ret.ScheduleStart.Value, AllowedMaxEndDate);
            }

            return ret;
        }

        protected FIFOReviewer CreateFifoReviewer(AutoSchedulableReviewer reviewer)
        {
            PlanReviewAutoSchedulableReviewer selectedReviewer = new PlanReviewAutoSchedulableReviewer(reviewer);

            FIFOReviewer fifoReviewer = new FIFOReviewer();
            fifoReviewer.Reviewer = selectedReviewer;
            fifoReviewer.ReviewerId = reviewer.UserIdentity.ID;

            return fifoReviewer;
        }
    }
}