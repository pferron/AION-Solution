using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;

namespace AION.Base
{
    [DataContract]
    public class BaseBE
    {

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string CreatedByWkrId { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public string UpdatedByWkrId { get; set; }

        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

    }

}
