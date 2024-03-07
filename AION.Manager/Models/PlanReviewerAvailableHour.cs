namespace AION.BL.Models
{
    public class PlanReviewerAvailableHour : ModelBase
    {
        public decimal AvailableHours { get; set; }

        public PlanReviewHourTypes EnumMappingValNbr { get; set; }

        public string PlanReviewTypeCd { get; set; }

        public int ProjectTypeRefId { get; set; }
    }
}
