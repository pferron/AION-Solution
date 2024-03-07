using Newtonsoft.Json;

namespace Meck.Shared.Accela
{
    public class RecordNotification
    {
        [JsonProperty("ReceivedQueueId")]
        public string recordID { get; set; }

        [JsonProperty("recordType")]
        public string recordtype { get; set; }

        [JsonProperty("recordStatus")]
        public string status { get; set; }

        [JsonProperty("WorkFlowTaskID")]
        public string WorkFlowStepId { get; set; }

        [JsonProperty("WorkFlowTaskName")]
        public string WorkFlowTaskName { get; set; }

        [JsonProperty("WorkFlowStatus")]
        public string WorkFlowStatus { get; set; }

        [JsonProperty("EstimatedRereviewHours")]
        public string EstimatedRereviewHours { get; set; }

        [JsonProperty("statusDesc")]
        public string statusdescription { get; set; }


        public RecordNotification()
        {

        }
    }
}
