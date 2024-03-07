using Newtonsoft.Json;

namespace Meck.Shared.Accela
{
    public  class AIONAEDataResponse
    {
        [JsonProperty("Errors")]
        public string errors { get; set; }
         [JsonProperty("RecordSource")]
        public AccelaAIONAEData AionAERecord { get; set;  }
         [JsonProperty("TableRecord")]
        public AccelaAIONAEData CurrentRecord { get; set;  }

         public AIONAEDataResponse()
         {
             errors = string.Empty; 
         }

          
    }
}
