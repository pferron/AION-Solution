using System;

namespace ReadAIONDb.Dtos
{
    public class PROJECT_EMAIL_NOTIFICATIONDto
    {
        public int PROJECT_EMAIL_NOTIFICATION_ID { get; set; }
        public int PROJECT_ID { get; set; }
        public string EMAIL_TYP_DESC { get; set; }
        public string EMAIL_SUBJECT_TXT { get; set; }
        public string EMAIL_BODY_TXT { get; set; }
        public DateTime  EMAIL_SENT_DT { get; set; }
        public int SENDER_USER_ID { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }
    }
}
