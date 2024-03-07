using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.BL.Models
{
    public class ProjectEstimation : Project
    {

        public ProjectEstimation() : base()
        {
        }

        public MeetingTypeEnum? MeetingTypeEnum { get; set; }
        public AppointmentResponseStatusEnum? MeetingStatusEnum { get; set; }

        public bool IsAccelaUpdate { get; set; }

        public List<Reviewer> Reviewers { get; set; }

        public ProjectCycleSummary ProjectCycleSummary { get; set; }
    }
}
