using Newtonsoft.Json;

namespace Meck.Shared.Accela
{
    public class AIONRecordQueueResponse
    {
        [JsonProperty("Errors")]
        public string errors { get; set; }
        [JsonProperty("RecordSource")]
        public RecordNotification newRecordSource { get; set; }
        [JsonProperty("TableRecord")]
        public AIONQueueRecordBE CurrentRecord {get;set;}
        
         public AIONRecordQueueResponse()
         {

         }

    }
}
