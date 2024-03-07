using System;

namespace AION.Manager.Models
{
    public class SchedulingLeadTimeReport
    {
        public DateTime GeneratedOn { get; set; }

        public int ProjectTypeRefId { get; set; }

        public int BusinessDivisionRefId { get; set; }

        public int ProjectHours { get; set; }

        public int LeadTimeDays { get; set; }
        public string ProjectLevelTxt { get; set; }
    }
}