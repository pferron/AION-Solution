using Newtonsoft.Json;
using System;

namespace Meck.Shared.Accela
{
    public class AccelaAIONAEData
    {
        [JsonProperty("ACCELA_AE_DATA_ID")]
        public Int32 ACCELA_AE_DATA_ID { get; set; }
        [JsonProperty("SYSTEM_USER_NM")]
        public string SYSTEM_USER_NM { get; set; }
        [JsonProperty("REC_ID_NUM")]
        public string REC_ID_NUM { get; set; }
        [JsonProperty("PLAN_REVIEW_TYP_DESC")]
        public string PLAN_REVIEW_TYP_DESC { get; set; }
        [JsonProperty("CYCLE_NBR")]
        public Int32 CYCLE_NBR { get; set; }
        [JsonProperty("LICENSE_TYP_DESC")]
        public string LICENSE_TYP_DESC { get; set; }
        [JsonProperty("PROJECT_SCORE_DESC")]
        public string PROJECT_SCORE_DESC { get; set; }
        [JsonProperty("PROJECT_CREATED_DTTM")]
        public DateTime PROJECT_CREATED_DTTM { get; set; }
        [JsonProperty("PASS_FAIL_IND")]
        public bool PASS_FAIL_IND { get; set; }
        [JsonProperty("FAILURE_CAUSE_TXT")]
        public string FAILURE_CAUSE_TXT { get; set; }
        [JsonProperty("FAILURE_REASON_TXT")]
        public string FAILURE_REASON_TXT { get; set; }
        [JsonProperty(" WKR_ID_CREATED_TXT")]
        public string WKR_ID_CREATED_TXT { get; set; }
        [JsonProperty("CREATED_DTTM")]
        public DateTime CREATED_DTTM { get; set; }
        [JsonProperty("WKR_ID_UPDATED_TXT")]
        public string WKR_ID_UPDATED_TXT { get; set; }
        [JsonProperty("UPDATED_DTTM")]
        public DateTime UPDATED_DTTM { get; set; }
        

        public AccelaAIONAEData()
        { }
    }
}
