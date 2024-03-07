using System;

namespace AION.BL.Models
{
    public class ProjectSearchResult
    {
        public DateTime DateOfApplication { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string ProjectType { get; set; }
        public string CustomerName { get; set; }
        public string FacilitatorName { get; set; }
        public string ProjectStatus { get; set; }
        public string MeetingType { get; set; }

        public string RecIdTxt { get; set; }
    }
}