using System;

namespace ReadAIONDb.Dtos
{
    public class EstimationNotesDto
    {
        public int NOTES_TYP_REF_ID { get; set; }
        public int WKR_ID_CREATED_TXT { get; set; }
        public int BUSINESS_REF_ID { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string NOTES_COMMENT { get; set; }
        public string txt_trade_code { get; set; }
        public string txt_cust_notes { get; set; }
        public string txt_gate_notes { get; set; }
        public string txt_int_notes { get; set; }
        public decimal id_client { get; set; }
    }
}
