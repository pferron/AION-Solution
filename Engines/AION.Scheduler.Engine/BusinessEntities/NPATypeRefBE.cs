#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - NpaTypeRefBE

    [DataContract]
    public class NpaTypeRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? NpaTypeRefID { get; set; }

        [DataMember]
        public string AppointmentTypeName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public int? TimeAllocationTypeRefId { get; set; }

        #endregion

    }

    #endregion

}