using Newtonsoft.Json;
using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    public class CycleWrapperBE
    {
        
        [JsonProperty("result")]
        public List<CycleBE> CycleList { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }


        public CycleWrapperBE()
        {
            CycleList = new List<CycleBE>();
        }
    }

    public class CycleBE
    {
        [JsonProperty("projectid")]
        public string ProjectId { get; set; }

        [JsonProperty("reReviews")]
        public List<ReReviewBE> ReReviews { get; set; }
    }

    public class ReReviewBE
    {
        [JsonProperty("dept")]
        public string Department { get; set; }
        [JsonProperty("estimatedReReviewTime")]
        public decimal? EstimatedReReviewTime { get; set; }
        [JsonProperty("proposedReviewerName")]
        public string ProposedReviewerName { get; set; }
    }
}
