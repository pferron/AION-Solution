using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class AIONQueueRecordBE
    {
        public AIONQueueRecordBE()
        {

        }

        [DataMember]
        public Int32 ACCELA_RECEIVED_REC_QUEUE_ID { get; set; }
        [DataMember]
        public string REC_ID_NUM { get; set; }
        [DataMember]
        public string REC_TYP_DESC { get; set; }
        [DataMember]
        public string STATUS_DESC { get; set; }
        [DataMember]
        public string WORKSTEP_ID_NUM { get; set; }

        [DataMember]
        public string WORKFLOW_TASK_NM { get; set; }

        [DataMember]
        public string WORKFLOW_TASK_STATUS { get; set; }

        [DataMember]
        public decimal? EstimatedRereviewHours { get; set; }
        [DataMember]
        public DateTime? RECEIVED_DT { get; set; }
        [DataMember]
        public DateTime? LAST_PROCESSING_DT { get; set; }
        [DataMember]
        public string PROCESS_STATUS_DESC { get; set; }
        [DataMember]
        public string WKR_ID_CREATED_TXT { get; set; }
        [DataMember]
        public DateTime? CREATED_DTTM { get; set; }
        [DataMember]
        public string WKR_ID_UPDATED_TXT { get; set; }
        [DataMember]
        public DateTime? UPDATED_DTTM { get; set; }
        [DataMember] 
        public List<AIONQueueRecordBE> PlanReviewStatusRecords { get; set; }
    }
}
