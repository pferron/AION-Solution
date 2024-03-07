using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Meck.Shared.PosseToAccela;
 using Meck.Shared; 

namespace Meck.Shared.Accela
{
    public class RecordWorkFlowTaskForPaymentBE
    {
        public List<ResultFull> Results { get; set; }
        public int status { get; set; }

        public List<AANHold> AANHolds { get; set; }
        
        public List<WorkTaskCustForms.WorkTaskMeetingDetail> MeetingTasks { get; set;  }

        public RecordWorkFlowTaskForPaymentBE()
        {
            Results = new List<ResultFull>();
          //   AANHolds = new List<AANHold>();
          MeetingTasks = new List<WorkTaskCustForms.WorkTaskMeetingDetail>();
        }
    }

    public partial class ResultFull  
    {
        [JsonProperty("isCompleted")]
        public string isCompleted { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("isActive")] 
        public string isActive { get; set; }
        [JsonProperty("id")] 
        public string id { get; set; }
         [JsonProperty("feeDetails")]
        public FeeDetail feeDetails { get; set; }
        public  string MeetingComment { get; set; }
        public DateTime MeetingDueDate { get; set; } 


        public ResultFull()
        {
            feeDetails = new FeeDetail();
        }
    }
    
    public partial class FeeDetail
     {
         [JsonProperty("WorkFlowTaskName")]
        public string WorkFlowTaskName { get; set; }
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("result")]
        public List<FieldsOfFeeDetail> FieldsOfFeeDetails { get; set; }
    }

    public class FieldsOfFeeDetail
    {
        [JsonProperty("StartDate")]
        public object StartDate { get; set; }
        [JsonProperty("SpecialInstructions")]
        public object SpecialInspections { get; set; }
        [JsonProperty("Cycle")]
        public string Cycle { get; set; }
        [JsonProperty("PoolReview")]
        public string PoolReview { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("SealHolderFailure")]
        public object SealHolderFailure { get; set; }
        [JsonProperty("EstimatedRereviewTime")]
        public string EstimatedRereviewTime { get; set; }
        [JsonProperty("PublicSecurityProject")]
        public object PublicSecurityProject { get; set; }
        [JsonProperty("EstimatedReviewTime")]
        public object EstimatedReviewTime { get; set; }
        [JsonProperty("FeeNotes")]
        public string FeeNotes { get; set; }
        [JsonProperty("FeeAmount")]
        public string FeeAmount { get; set; }
        [JsonProperty("MeetingType")]
        public object MeetingType { get; set; }
    }

    


}
