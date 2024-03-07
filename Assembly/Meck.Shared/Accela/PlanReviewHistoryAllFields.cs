using System;

namespace Meck.Shared.Accela
{
    public class PlanReviewHistoryAllFields
    {
        public Int32 PLAN_REVIEW_HISTORY_ID { get; set; }
        public string REC_ID_NUM { get; set; }
        public DateTime ACCELA_CREATED_DT { get; set; }
        public DateTime PLAN_REVIEW_START_DT { get; set; }
        public DateTime PLAN_REVIEW_END_DT { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }
        public PlanReviewHistoryAllFields()
        {

        }
    }
}
