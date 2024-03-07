namespace ReadAIONDb.Dtos
{
    public  class ReadEPMUsersDTO
    {
        public int USER_ID { get; set; }
        public string FIRST_NM { get; set; }
        public string LAST_NM { get; set; }
        public int EXTERNAL_SYSTEM_REF_ID { get; set; }
        public string SRC_SYSTEM_VAL_TXT { get; set; }
        public int WKR_ID_CREATED_TXT { get; set; }
        public int WKR_ID_UPDATED_TXT { get; set; }
        public int ACTIVE_IND { get; set; }
        public string USER_INTERFACE_SETTING_TXT { get; set; }
        public string IS_EXPRESS_SCHEDULED_IND { get; set; }
        public string USER_NM { get; set; }
        public string LAN_ID_TXT { get; set; }
        public string PHONE_NUM { get; set; }
        public string EMAIL_ADDR_TXT { get; set; }
        public int? CITY_IND { get; set; }
    }
}