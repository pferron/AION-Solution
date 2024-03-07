#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - UserJurisdictionXRefBE

    [DataContract]
    public class UserJurisdictionXRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? UserJurisdictionXRefId { get; set; }

        [DataMember]
        public int? UserID { get; set; }

        [DataMember]
        public int? JurisdictionRefId { get; set; }
        [DataMember]
        public int? EnumMappingValNbr { get; set; }
        #endregion

    }

    #endregion

}