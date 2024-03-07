using System;

namespace AION.Accela.Engine.Models
{
    public class AccelaUpDateRecordWorkFlowTask
    {

        public Actionbydepartment actionbyDepartment { get; set; }
        public Actionbyuser actionbyUser { get; set; }
        public string approval { get; set; }
        public string assignEmailDisplay { get; set; }
        public string billable { get; set; }
        public string comment { get; set; }
        public string commentDisplay { get; set; }
        public string[] commentPublicVisible { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime endTime { get; set; }
        public int hoursSpent { get; set; }
        public string overTime { get; set; }
        public DateTime startTime { get; set; }
        public RecUpdateStatus status { get; set; }
        public DateTime statusDate { get; set; }
    }

    public class Actionbydepartment
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class Actionbyuser
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecUpdateStatus
    {
        public string text { get; set; }
        public string value { get; set; }
    }

}
