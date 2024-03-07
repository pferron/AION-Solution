using System;

namespace Meck.Shared.PosseToAccela
{
    public class PosseOutputRecord
    {
        public Int32 QUEUEID { get; set; }  
        
        public string projectNum { get; set;  }
        public string  RecordId { get; set;  }
        
        public string TASKNAME { get; set; }
       
        public string recStatus { get; set; } 
        
        public string XmlData { get; set;  }

        public PosseOutputRecord()
        {

        }
    }
}
