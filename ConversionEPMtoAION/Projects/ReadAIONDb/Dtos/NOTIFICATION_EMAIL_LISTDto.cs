
using System;

namespace ReadAIONDb.Dtos
{
    public class NOTIFICATION_EMAIL_LISTDto
    {
        public int NOTIFICATION_EMAIL_LIST_ID { get; set; }
        public int PROJECT_EMAIL_NOTIFICATION_ID { get; set; }
        public int? USER_ID { get; set; }
        public string EMAIL_ADDR_TXT { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }
    }
}
