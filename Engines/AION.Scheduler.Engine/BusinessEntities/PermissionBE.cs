#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - PermissionBE

    [DataContract]
    public class PermissionBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? PermissionId { get; set; }

        [DataMember]
        public string PermissionName { get; set; }

        [DataMember]
        public int? ModuleId { get; set; }

        [DataMember]
        public int? EnumMappingNumber { get; set; }

        [DataMember]
        public string PermissionDisplayName { get; set; }
        #endregion

        #region Properties not in table but added for refactor to speed up loop

        [DataMember]
        public int? ModuleEnumMappingNumber { get; set; }

        #endregion

    }

    #endregion

}