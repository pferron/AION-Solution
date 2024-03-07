using Newtonsoft.Json;

namespace Meck.Shared.Accela
{
    public  class AIONPlanReviewHistoryResponse
    {
        [JsonProperty("Errors")]
        public string errors { get; set; }
        [JsonProperty("RecordSource")]
        public PlanReviewHistory PlanReviewRecordSource { get; set; }
        [JsonProperty("TableRecord")]
        public PlanReviewHistoryAllFields CurrentRecord { get; set; }

        public AIONPlanReviewHistoryResponse()
        {
            errors = string.Empty; 
        }
    }
}
