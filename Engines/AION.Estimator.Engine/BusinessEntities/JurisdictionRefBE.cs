#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - JurisdictionRefBE

    [DataContract]
    public class JurisdictionRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? JurisdictionRefId { get; set; }

        [DataMember]
        public string JurisdictionDesc { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        [DataMember]
        public bool? ActiveInd { get; set; }

        #endregion

    }

    #endregion

}