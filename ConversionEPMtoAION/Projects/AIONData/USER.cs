using System;

namespace AIONData
{
    public partial class USER
    {
        public int USER_ID { get; set; }		
		public string FIRST_NM { get; set; }
        public string LAST_NM { get; set; }
        public int EXTERNAL_SYSTEM_REF_ID { get; set; }
        public string SRC_SYSTEM_VAL_TXT { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime? CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime? UPDATED_DTTM { get; set; }
        public int ACTIVE_IND { get; set; }
        public string USER_INTERFACE_SETTING_TXT { get; set; }
        public int? IS_EXPRESS_SCHEDULED_IND { get; set; }
        public string USER_NM { get; set; }
        public string LAN_ID_TXT { get; set; }
        public string PHONE_NUM { get; set; }
        public string EMAIL_ADDR_TXT { get; set; }
        public string NOTES_TXT { get; set; }
        public int IS_SCHEDULABLE_IND { get; set; }
        public decimal? PLAN_REVIEW_OVERRIDE_HOURS_NBR { get; set; }
        public string HOURS_ESTIMATED_DESC { get; set; }
        public string SCHEDULABLE_LVL_DESC { get; set; }
        public int JURISDICTION_TYP_ID { get; set; }
        public int IS_PRELIM_MEETING_ALLOWED_IND { get; set; }
        public string USER_PRINCIPAL_NM { get; set; }
        public string CALENDAR_ID { get; set; }
        public int? CITY_IND { get; set; }
    }
}
