using Newtonsoft.Json;
using System;

namespace Meck.Shared.PosseToAccela
{
    public class PosseAccelaDBMap
    {
        [JsonProperty("ACCELA_POSSE_MAP_ID")]
        public Int32 ACCELA_POSSE_MAP_ID { get; set; }
        [JsonProperty("ACCELA_REC_TYP_NM")]
        public string ACCELA_REC_TYP_NM { get; set; }
        [JsonProperty("ACCELA_OBJ_TYP_DESC")]
        public string ACCELA_OBJ_TYP_DESC { get; set; }
        [JsonProperty("ACCELA_LOC_DESC")]
        public string ACCELA_LOC_DESC { get; set; }
        [JsonProperty("ACCELA_FIELD_NM")]
        public string ACCELA_FIELD_NM { get; set; }
        [JsonProperty("ACCELA_DATA_TYP_DESC")]
        public string ACCELA_DATA_TYP_DESC { get; set; }
        [JsonProperty("ACCELA_LOOKUP_FIELD_NM")]
        public string ACCELA_LOOKUP_FIELD_NM { get; set; }
        [JsonProperty("ACCELA_LOOKUP_VAL_DESC")]
        public string ACCELA_LOOKUP_VAL_DESC { get; set; }
        [JsonProperty("WKR_ID_CREATED_TXT")]
        public string WKR_ID_CREATED_TXT { get; set; }
        [JsonProperty("CREATED_DTTM")]
        public DateTime CREATED_DTTM { get; set; }
        [JsonProperty("WKR_ID_UPDATED_TXT")]
        public string WKR_ID_UPDATED_TXT { get; set; }
        [JsonProperty("UPDATED_DTTM")]
        public DateTime UPDATED_DTTM { get; set; }
        [JsonProperty("POSSE_FIELD_NM")]
        public string POSSE_FIELD_NM { get; set; }
        [JsonProperty("POSSE_DATA_TYP_DESC")]
        public string POSSE_DATA_TYP_DESC { get; set; }



        public PosseAccelaDBMap()
        {

        }
    }


}
