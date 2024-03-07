using System;

namespace Meck.Shared.MeckDataMapping
{
    public class AccelaAIONMap
    {
        public Int32 ACCELA_AION_MAP_ID { get; set; }
        public string AION_CLS_NM { get; set; }
        public string AION_FIELD_NM { get; set; }
        public string AION_DATA_TYP_DESC { get; set; }
        public string AION_USAGE_DESC { get; set; }
        public string ACCELA_REC_TYP_NM { get; set; }
        public string ACCELA_OBJ_TYP_DESC { get; set; }
        public string ACCELA_LOC_DESC { get; set; }
        public string ACCELA_FIELD_NM { get; set; }
        public string ACCELA_DATA_TYP_DESC { get; set; }
        public string ACCELA_LOOKUP_FIELD_NM { get; set; }
        public string ACCELA_LOOKUP_VAL_DESC { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }


        public AccelaAIONMap()
        {

        }

    }


    public class LoadAccelaAIONMap
    {
        public string AION_CLS_NM { get; set; }
        public string AION_FIELD_NM { get; set; }
        public string AION_DATA_TYP_DESC { get; set; }
        public string AION_USAGE_DESC { get; set; }
        public string ACCELA_REC_TYP_NM { get; set; }
        public string ACCELA_OBJ_TYP_DESC { get; set; }
        public string ACCELA_LOC_DESC { get; set; }
        public string ACCELA_FIELD_NM { get; set; }
        public string ACCELA_DATA_TYP_DESC { get; set; }
        public string ACCELA_LOOKUP_FIELD_NM { get; set; }
        public string ACCELA_LOOKUP_VAL_DESC { get; set; }
       
        public LoadAccelaAIONMap()
        {

        }

    }

    public class LoadPosseAccelaMap
    {
        public string POSSE_FIELD_NM { get; set; }
        public string POSSE_DATA_TYP_DESC { get; set; }
        public string ACCELA_REC_TYP_NM { get; set; }
        public string ACCELA_OBJ_TYP_DESC { get; set; }
        public string ACCELA_LOC_DESC { get; set; }
        public string ACCELA_FIELD_NM { get; set; }
        public string ACCELA_DATA_TYP_DESC { get; set; }
        public string ACCELA_LOOKUP_FIELD_NM { get; set; }
        public string ACCELA_LOOKUP_VAL_DESC { get; set; }

        public LoadPosseAccelaMap()
        {
        }
    }
}
