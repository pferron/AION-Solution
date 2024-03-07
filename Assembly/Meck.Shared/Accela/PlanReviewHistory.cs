using Newtonsoft.Json;
using System;

namespace Meck.Shared.Accela
{
    public class PlanReviewHistory
    {
        public PlanReviewHistory()
        {
        }
        [JsonProperty("recordId")]
        public string REC_ID_NUM { get; set; }
        [JsonProperty("AccelaCreatedDateTime")]
        public DateTime ACCELA_CREATED_DT { get; set; }
        [JsonProperty("PlanReviewStartDateTime")]
         public DateTime PLAN_REVIEW_START_DT { get; set; }
        [JsonProperty("PlanReviewEndDateTime")]
       public DateTime PLAN_REVIEW_END_DT { get; set; }
    
    }
}
