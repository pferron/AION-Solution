using AION.BL;

namespace AION.Manager.Models
{
    public class ReserveExpressPlanReviewer : ModelBase
    {
        public int Id { get; set; }
        public int BusinessRefId { get; set; }
        public int PlanReviewerId { get; set; }
        public int RotationNbr { get; set; }
        public int DeptNameEnumId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}