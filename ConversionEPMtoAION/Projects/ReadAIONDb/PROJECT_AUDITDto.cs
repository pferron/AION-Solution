using System;
 

namespace ReadAIONDb
{
    internal class PROJECT_AUDITDto
    {
        public int PROJECT_AUDIT_ID { get; set; }
        public int? PROJECT_ID { get; set; }
        public string AUDIT_ACTION_DETAILS_TXT { get; set; }
        public string AUDIT_USER_NM { get; set; }
        public DateTime? AUDIT_DT { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime? CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime? UPDATED_DTTM { get; set; }
        public int? AUDIT_ACTION_REF_ID { get; set; }
        public int? PROJECT_CYCLE_ID { get; set; }

    }
    internal class PROJECT_AUDITtESTDto
    {
        //public decimal PROJECT_AUDIT_ID { get; set; }
        public int? PROJECT_ID { get; set; }
        //public string AUDIT_ACTION_DETAILS_TXT { get; set; }
        public int? AUDIT_USER_NM { get; set; }
        public DateTime? AUDIT_DT { get; set; }
        //public string WKR_ID_CREATED_TXT { get; set; }
        //public DateTime? CREATED_DTTM { get; set; }
        //public string WKR_ID_UPDATED_TXT { get; set; }
        //public DateTime? UPDATED_DTTM { get; set; }
        public int? AUDIT_ACTION_REF_ID { get; set; }
        //public int? PROJECT_CYCLE_ID { get; set; }

    }
}
