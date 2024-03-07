using System;

namespace ReadAIONDb.Dtos
{
    public class EstimationHistoryDto
    {
        public DateTime? DML_Datetime { get; set; }
        public string txt_performed_by { get; set; }
        public DateTime? perf_on { get; set; }
        public string nme_status { get; set; }
        public string nme_task { get; set; }
        public string task_desc { get; set; }
        public decimal id_task { get; set; }
        public System.Int64? id_tb_project_history { get; set; }
        public decimal assessment_cycle { get; set; }
        public int? AUDIT_ACTION_REF_ID { get; set; }
    }
}
