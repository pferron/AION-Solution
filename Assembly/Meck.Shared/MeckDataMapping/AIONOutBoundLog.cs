using System;

namespace Meck.Shared.MeckDataMapping
{
    public  class AIONOutBoundLogDetail
    {
        public string POSSE_ACCELA_OutBound_LOG_ID { get; set; }
        public string ACCELA_REC_ID_NUM { get; set; }
        public string ACTION_CAUSE_DESC { get; set; }
        public string ACTION_DETAIL_TXT { get; set; }
        public string PROCESS_DTTM { get; set; }
        public string WKR_ID_CREATED_TXT { get; set; }
        public DateTime CREATED_DTTM { get; set; }
        public string WKR_ID_UPDATED_TXT { get; set; }
        public DateTime UPDATED_DTTM { get; set; }
    }

  public class AIONOutBoundLog
  {
      public string ACCELA_REC_ID_NUM { get; set; }
      public string ACTION_CAUSE_DESC { get; set; }
      public string ACTION_DETAIL_TXT { get; set; }
      public string PROCESS_DTTM { get; set; }
     
      public AIONOutBoundLog(string recordId, string MethodCaller, string outdata)
      {
          ACCELA_REC_ID_NUM = recordId;
          ACTION_CAUSE_DESC = MethodCaller;
          ACTION_DETAIL_TXT = outdata; 
      }
    }

}
