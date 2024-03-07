namespace ReadAIONDb.Dtos
{
    public class PBRELATIONSHIPDto
    {       
        public decimal? ESTIMATION_HOURS_NBR { get; set; }
        public int BUSINESS_REF_ID { get; set; }
        public string PROJECT_ID { get; set; }
        public int ASSIGNED_PLAN_REVIEWER_ID { get; set; }
        public int ESTIMATION_NOT_APPLICABLE_IND { get; set; }
        public string PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC { get; set; }
        public int PROJECT_STATUS_REF_ID { get; set; }
        public int IS_DEPT_REQUESTED_IND { get; set; }
    }
}
