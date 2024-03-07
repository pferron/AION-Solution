using System; 

namespace ReadAIONDb.Dtos
{
    public  class vw_appointmentsDto
    {
        public string txt_trade_code { get; set; }
        public decimal id_appointment_type { get; set; }
        public DateTime? start_date { get; set; }
        public string txt_conf_room { get; set; }        
        public DateTime? appt_start { get; set; }
        public DateTime? appt_end { get; set; }
    }
}
