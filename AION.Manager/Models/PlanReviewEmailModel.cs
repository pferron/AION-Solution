using System;

namespace AION.Manager.Models
{
    public class PlanReviewEmailModel
    {
        public string AccelaProjectRefId { get; set; }

        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }

        public int FacilitatorId { get; set; }


        public string PlanReviewStartDate { get; set; }

        public string RecIdTxt { get; set; }

    }
}