using System;
 
namespace ReadAIONDb.Dtos
{
    public class TB_PROJECT_CURRENT_STATUSDto
    {
        public DateTime? performed_on { get; set; }
        public decimal? assessment_cycle { get; set; }
        public DateTime? plans_ready_on { get; set; }
        public decimal? plans_review_fee { get; set; }
        public decimal? id_tb_project_history { get; set; }
        public DateTime? application_received_on { get; set; }
        public DateTime? review_cancel_by  { get; set; }
        public decimal? schedule_cycle { get; set; }
        public int? interactive_cycle { get; set; }
        public int? intake_cycle { get; set; }
    }
}
