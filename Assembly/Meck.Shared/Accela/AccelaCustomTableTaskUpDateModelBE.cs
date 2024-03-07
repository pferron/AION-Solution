using System;
using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    public class AccelaCustomTableTaskUpDateModelBE
    {
        public string recordId { get; set; }
        public List<AccelaTaskUpDateDetail> custTableTaskUpdateModel { get; set; }

        public AccelaCustomTableTaskUpDateModelBE()
        {
            custTableTaskUpdateModel = new List<AccelaTaskUpDateDetail>();
        }
        public AccelaCustomTableTaskUpDateModelBE( string RecordId)
        {
            recordId = RecordId; 
            custTableTaskUpdateModel = new List<AccelaTaskUpDateDetail>();
        }
    }
    public class AccelaTaskUpDateDetail
    {
        public string id { get; set; }
        public string TaskType { get; set; }
        public string TaskName { get; set; }
        public string PoolReview { get; set; }
        public string Assignee { get; set; }
        public string DueDate { get; set; }
        public string CycleCount { get; set; }
        public DateTime StartDate { get; set; }
        public Decimal EstimatedReviewTime { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string ProcessingStatus { get; set; }
        public string Comments { get; set; }

        public AccelaTaskUpDateDetail()
        {
        }

    }
}
