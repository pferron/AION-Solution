using System;

namespace ReadAIONDb
{
    public class PROJECT_BUSINESS_RELATIONSHIPDto
    {
        public int ROJECT_BUSINESS_RELATIONSHIP_ID { get; set; }
        public decimal ESTIMATION_HOURS_NBR { get; set; }
        public int BUSINESS_REF_ID { get; set; }
        public int PROJECT_ID { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public string UPDATED_DTTM { get; set; }
        public int ASSIGNED_PLAN_REVIEWER_ID { get; set; }
        public int PROPOSED_PLAN_REVIEWER_ID { get; set; }
        public int SECONDARY_PLAN_REVIEWER_ID { get; set; }
        public int PRI_PLAN_REVIEWER_ID { get; set; }
        public int ESTIMATION_NOT_APPLICABLE_IND { get; set; }
        public string PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC { get; set; }
        public int PROJECT_STATUS_REF_ID { get; set; }
        public int IS_DEPT_REQUESTED_IND { get; set; }
    }
}
