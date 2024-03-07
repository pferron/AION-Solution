using Newtonsoft.Json;
using System;

namespace AION.Accela.Engine.Models
{
    public class AccelaRecordCustomTableTaskInsertBE
    {
        [JsonProperty("id")] public string AccelaCustomTableid { get; set; }
        [JsonProperty("rows")] public AccelaRecordCustomTableTaskInsertRow[] insertRows { get; set; }

        public AccelaRecordCustomTableTaskInsertBE()
        {
            AccelaRecordCustomTableTaskInsertRow taskbuster = new AccelaRecordCustomTableTaskInsertRow();
            insertRows = new AccelaRecordCustomTableTaskInsertRow[] {taskbuster};
        }
    }

    public class AccelaRecordCustomTableTaskInsertRow
    {
        public string action { get; set; }
        public AccelaRecordCustomTableTaskInsertField[] fields { get; set; }


        public AccelaRecordCustomTableTaskInsertRow()
        {
            AccelaRecordCustomTableTaskInsertField taskbuster = new AccelaRecordCustomTableTaskInsertField();

            fields = new AccelaRecordCustomTableTaskInsertField[] {taskbuster};
        }
    }

    public class AccelaRecordCustomTableTaskInsertField
    {
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public string serviceProviderCode { get; set; }
        public string lastModifiedDate { get; set; }
        public string isActive { get; set; }
        public string isCompleted { get; set; }
        public string processCode { get; set; }

        [JsonProperty("id")] public string fieldId { get; set; }
        public AccelaRecordCustomTableTaskInsertRecordid recordId { get; set; }
        public string currentTaskId { get; set; }
        public string nextTaskId { get; set; }
        public int daysDue { get; set; }
        public decimal hoursSpent { get; set; }
        public AccelaRecordCustomTableTaskInsertAssignedtodepartment assignedToDepartment { get; set; }
        public decimal estimatedHours { get; set; }
        public AccelaRecordCustomTableTaskInsertFieldStatus status { get; set; }

        public AccelaRecordCustomTableTaskInsertField()
        {
            recordId = new AccelaRecordCustomTableTaskInsertRecordid();
            assignedToDepartment = new AccelaRecordCustomTableTaskInsertAssignedtodepartment();
            status = new AccelaRecordCustomTableTaskInsertFieldStatus();
        }

    }

    public class AccelaRecordCustomTableTaskInsertRecordid
    {
        public string id { get; set; }
        public string customId { get; set; }
        public string serviceProviderCode { get; set; }
        public long trackingId { get; set; }
        public string capClass { get; set; }
        public string value { get; set; }
    }

    public class AccelaRecordCustomTableTaskInsertAssignedtodepartment
    {
        public string value { get; set; }
        public string text { get; set; }
    }


    public class AccelaRecordCustomTableTaskInsertFieldStatus
    {
        public string value { get; set; }
        public string text { get; set; }
    }

}

