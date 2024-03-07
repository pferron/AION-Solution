using System;

namespace ReadAIONDb.Dtos
{
    public  class PROJECT_EMAIL_NOTIFDto
    {
        public decimal? id_email_queue { get; set; }
        public string txt_email { get; set; }
        public int? assessment_cycle { get; set; }
        public decimal? id_email_template { get; set; }
        public string nme_template { get; set; }
        
        public string subject_template { get; set; }
        public string body_template { get; set; }
        public DateTime? sent_on { get; set; }
        public string trade_or_agency { get; set; }

    }
}
