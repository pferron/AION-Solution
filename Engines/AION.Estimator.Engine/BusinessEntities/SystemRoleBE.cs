#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - SystemRoleBE

    [DataContract]
    public class SystemRoleBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? SystemRoleId { get; set; }

        [DataMember]
        public string SystemRoleNm { get; set; }
        [DataMember]
        public int? ExternalSystemRefId { get; set; }

        [DataMember]
        public string SrcSystemValTxt { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        [DataMember]
        public string SystemRoleTxt { get; set; }
        [DataMember]
        public string RoleOptionsTxt { get; set; }

        [DataMember]
        public bool? EnabledInd { get; set; }

        [DataMember]
        public int? ParentSystemRoleId { get; set; }
        #endregion
    }

    #endregion

}