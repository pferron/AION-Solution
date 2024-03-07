using AION.BL;
using AION.BL.Models;
using System;

namespace AION.Manager.Models
{
    public class ProjectsList : ModelBase
    {

        public Project Project { get; set; }

        public PreliminaryMeetingAppointment PreliminaryMeetingAppointment { get; set; }

        public ExpressMeetingAppointment ExpressMeetingAppointment { get; set; }

        public NonProjectAppointment NonProjectAppointment { get; set; }

        public int ProjectID { get; set; }

        public string AccelaProjectRefID { get; set; }

        public string ProjectName { get; set; }

        public PropertyTypeEnums ProjectType { get; set; }

        public ProjectStatusEnum ProjectStatus { get; set; }

        public DateTime? TentativeStartDate { get; set; }

        public DateTime? AcceptanceDeadLine { get; set; }
        public int AssignedFacilitatorId { get; set; }

        public string FacilitatorName { get; set; }

        public bool Payment { get; set; }

        public string RecIdTxt { get; set; }
        public bool? IsPaidStatus { get; set; }
        public bool HasOpenActions { get; set; }
    }
}