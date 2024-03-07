using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAIONDb
{
    public class ReadNotesDto
    {
        public int NOTES_TYP_REF_ID { get; set; }
        //public string PROJECT_ID { get; set; }
        public string NOTES_COMMENT { get; set; }
        public int? BUSINESS_REF_ID { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public DateTime UPDATED_DTTM { get; set; }
        public int? WKR_ID_CREATED_TXT { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        //
        //public string WKR_ID_UPDATED_TXT { get; set; }
        //

        //
    }
}
