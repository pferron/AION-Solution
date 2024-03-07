using System;

namespace Meck.Shared.Accela
{
    [Serializable]
    public class RecordWorkFlowHistoryBE
    {
        public class Rootobject
        {
            public Result[] result { get; set; }
            public int status { get; set; }
        }

        public class Result
        {
            public string processCode { get; set; }
            public string serviceProviderCode { get; set; }
            public string description { get; set; }
            public string assignedDate { get; set; }
            public string dueDate { get; set; }
            public string isActive { get; set; }
            public string isCompleted { get; set; }
            public string lastModifiedDate { get; set; }
            public string id { get; set; }
            public Recordid recordId { get; set; }
            public Actionbyuser actionbyUser { get; set; }
            public Status status { get; set; }
            public int daysDue { get; set; }
            public float hoursSpent { get; set; }
            public Actionbydepartment actionbyDepartment { get; set; }
            public Assignedtodepartment assignedToDepartment { get; set; }
            public float inPossessionTime { get; set; }
            public float estimatedHours { get; set; }
            public string action { get; set; }
            public string statusDate { get; set; }
            public string commentDisplay { get; set; }
            public string[] commentPublicVisible { get; set; }
            public string billable { get; set; }
            public string overTime { get; set; }
            public string assignEmailDisplay { get; set; }
            public string comment { get; set; }
        }

        public class Recordid
        {
            public string id { get; set; }
            public string customId { get; set; }
            public string capClass { get; set; }
            public long trackingId { get; set; }
            public string serviceProviderCode { get; set; }
            public string value { get; set; }
        }

        public class Actionbyuser
        {
            public string value { get; set; }
            public string text { get; set; }
        }

        public class Status
        {
            public string value { get; set; }
            public string text { get; set; }
        }

        public class Actionbydepartment
        {
            public string value { get; set; }
            public string text { get; set; }
        }

        public class Assignedtodepartment
        {
            public string value { get; set; }
            public string text { get; set; }
        }

    }
}
