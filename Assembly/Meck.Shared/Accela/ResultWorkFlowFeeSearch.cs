using Newtonsoft.Json;
using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    public class ResultWorkFlowFeesSearch
    {
       public List<result> results { get; set; }
    }

    public partial class result
    {
        [JsonProperty("isCompleted")]
        public string isCompleted { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("isActive")]
        public string isActive { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }

        public result()
        {

        }
    }
}
