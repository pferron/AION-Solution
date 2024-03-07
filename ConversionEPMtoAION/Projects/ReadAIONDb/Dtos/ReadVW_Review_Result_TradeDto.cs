using System;

namespace ReadAIONDb.Dtos
{
    public class ReadVW_Review_Result_TradeDto
    {
        public int? BUSINESS_REF_ID { get; set; }
        public string txt_trade_code { get; set; }
        public string txt_trade_desc { get; set; }
        public DateTime? rvw_start_date { get; set; }
        public DateTime? rvw_end_date { get; set; }
        public string review_assigned_to { get; set; }
        public int UserReview_assigned_to { get; set; }
        public int assessment_cycle { get; set; }
        public int pool_trade_flg { get; set; }
        public string created_by { get; set; }
        public int UserCreated_by { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
        public decimal? estimated_time_hours { get; set; }
        public decimal?  re_review_time_hours { get; set; }
    }
}
