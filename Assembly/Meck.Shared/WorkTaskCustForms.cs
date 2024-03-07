using Meck.Shared.PosseToAccela;
using System;
using System.Collections.Generic;

namespace Meck.Shared
{
    public class WorkTaskCustForms
    {
        public List<PlanReviewFee> stagePlanReviewFees { get; set; }

        public numTradesPermits stageTradePermits { get; set; }

        public List<WorkTaskMeeting> stageMeetings { get; set; }

        public List<WorkTaskMeetingDetail> stageMeetingsDetails { get; set; }
        //  public List<AANHold>  AANHolds { get; set;  }

        public List<TaskReview> TaskReviews { get; set; }



        public WorkTaskCustForms()
        {
            stagePlanReviewFees = new List<PlanReviewFee>();
            stageTradePermits = new numTradesPermits();
            stageMeetings = new List<WorkTaskMeeting>();
            stageMeetingsDetails = new List<WorkTaskMeetingDetail>();
            //   AANHolds = new List<AANHold>();
            TaskReviews = new List<TaskReview>();
        }

        public class WorkTaskMeeting
        {
            public string MeetingType { get; set; }

            public WorkTaskMeetingDetail MeetingDetail { get; set; }
        }

        public class numTradesPermits
        {
            public Int32 BuildingPermitsRequired { get; set; }
            public Int32 ElectricalPermitsRequired { get; set; }
            public Int32 MechanicalPermitsRequired { get; set; }
            public Int32 PlumbingPermitsRequired { get; set; }
        }

        public class WorkTaskMeetingDetail
        {
            public string meetingDate { get; set; }
            public string meetingTime { get; set; }
            public string meetingAttendeesList { get; set; }
            public string meetingNotes { get; set; }
            public string meetingStatus { get; set; }
            public string Requester { get; set; }
            public string CycleNumber { get; set; }
            public string Closed { get; set; }

        }
    }

    public class TaskReview
    {
        public string StartDate { get; set; }
        public string CycleNumber { get; set; }
        public bool PoolReview { get; set; }
        public string Id { get; set; }
        public decimal EstimatedRereviewTime { get; set; }
        public decimal EstimatedReviewTime { get; set; }
        public string TaskId { get; set; }
        public string Department { get; set; }

        public string HoursSpent { get; set; }

        public string IsCompleted { get; set; }
    }

    public class TaskInfo
    {
        public string Department { get; set; }
        public string TaskId { get; set; }
    }
}
