namespace ReadAIONDb.Dtos
{
    public class ReadPLAN_REVIEW_SCHEDULE_DETAILDto
    {
        public int PLAN_REVIEW_SCHEDULE_ID { get; set; }
        public int PROJECT_TYP_REF_ID { get; set; }
        public string Assignee { get; set; }
        public int? id_user { get; set; }
        public string StartDate { get; set; }
        public decimal? EstimatedReviewTime { get; set; }
        public char PoolReview { get; set; }
        public int POOL_REQUEST_IND { get; set; } 
    }
}
