using System;
namespace ReadAIONDb.Dtos
{
    public class PROJECT_SCHEDULEDto
    {
        public int PROJECT_SCHEDULE_ID { get; set; }
        public string PROJECT_SCHEDULE_TYP_DESC { get; set; }
        public int APPT_ID { get; set; }
        public int RECURRING_APPT_DT { get; set; }        
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }
    }
}
