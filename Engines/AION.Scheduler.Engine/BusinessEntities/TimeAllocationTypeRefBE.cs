#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Scheduler.Engine.BusinessEntities
{

    #region BusinessEntitiy - TimeAllocationTypeRefBE

    [DataContract]
    public class TimeAllocationTypeRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? TimeAllocationTypeRefId { get; set; }

        [DataMember]
        public string TimeAllocationTypeRefDesc { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        [DataMember]
        public bool? ActiveInd { get; set; }

        #endregion

    }

    #endregion

}