namespace AION.Manager.Models
{
    public class InternalMeetings : CustmrMeetings
    {

        public bool IsProjectRTAP { get; set; } //Y/N

        public bool MinutesUploaded { get; set; } //Y/N

        public string FacilitatorName { get; set; }
        public string TeamGradeTxt { get; set; }

        public string PMName { get; set; }

        public string PMPhone { get; set; }
        public string PMEmail { get; set; }

        public string BuildingCodeVersion { get; set; }


    }
}