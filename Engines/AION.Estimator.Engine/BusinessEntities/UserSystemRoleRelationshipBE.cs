#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - UserSystemRoleRelationshipBE

    [DataContract]
    public class UserSystemRoleRelationshipBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? UserSystemRoleRelationshipId { get; set; }

        [DataMember]
        public int? UserID { get; set; }

        [DataMember]
        public int? SystemRoleId { get; set; }

        #endregion

        #region Added Properties for inner join 

        [DataMember]
        public int? SystemRoleEnumMappingNumber { get; set; }
        #endregion
    }

    #endregion

}