using System;

namespace Meck.Shared.PosseToAccela
{
    public class AccelaPosseLoging
    {
        public Int32 ACCELA_POSSE_LOG_ID { get; set; }
        public string ACCELA_REC_ID_NUM { get; set; }
        public string POSSE_ACTION_CAUSE_DESC { get; set; }
        public string POSSE_UPDATE_TXT { get; set; }
        public string POSSE_XML_TXT { get; set; }
        public DateTime PROCESS_DTTM { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }

        public AccelaPosseLoging()
        {

        }

    }
}
