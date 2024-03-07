using AION.BL;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Engines
{


    public class AutoSchedulableReviewer
    {
        public AutoSchedulableReviewer()
        {
            WIPTimeSlots = new List<TimeSlot>();
            CurrentMeetings = new List<TimeSlot>();
            AllowedOccupancies = new List<ProjectOccupancyTypeNameModel>();
            AllocatedTimeSlots = new List<TimeSlot>();
        }
        public UserIdentity UserIdentity { get; set; }
        public List<TimeSlot> WIPTimeSlots { get; set; }
        public List<TimeSlot> AllocatedTimeSlots { get; set; }
        public List<TimeSlot> CurrentMeetings { get; set; }
        public decimal TotalHoursForCapacity { get; set; }
        public List<ProjectOccupancyTypeNameModel> AllowedOccupancies { get; set; }
        public decimal AllowedHoursPerDay { get; set; }
    }

    public class PlanReviewAutoSchedulableReviewer
    {
        public PlanReviewAutoSchedulableReviewer(AutoSchedulableReviewer baseData)
        {
            SelectedReviewer = baseData;
            WIPReviewerTimeSlots = new List<TimeSlot>();
        }
        public AutoSchedulableReviewer SelectedReviewer { get; private set; }
        public DateTime? AllotedStartDt { get; set; }
        public DateTime? AllotedEndDt { get; set; }

        /*  Same Reviewer can be allocated to multiple departments. 
         *  So in that case we need to consider the allocation for each dept seperatly and need 
         *  to calcualte hrs individualy. So this property is used to keep that list.
         */
        public List<TimeSlot> WIPReviewerTimeSlots { get; set; }

        public DepartmentNameEnums PlanReviewerDept { get; set; }
    }

    public class Holidays
    {
        public HolidayConfig Holiday { get; set; }

    }
}