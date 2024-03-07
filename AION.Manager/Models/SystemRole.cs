using AION.BL;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class SystemRole : ModelBase
    {

        public string RoleName { get; set; }

        public ExternalSystem ExternalSystem { get; set; }

        public string ExternalSystemRefInfo { get; set; }

        public SystemRoleEnum SystemRoleEnum { get; set; } //ENUM_MAPPING_VAL

        public string RoleOptions { get; set; }

        public bool Enabled { get; set; }

        public int? ParentSystemRoleId { get; set; }

        public List<Manager.Models.Permission> Permissions { get; set; }
        public int? EnumMappingValNbr { get; set; }

        /// <summary>
        /// String value to search for when role has no enum value
        /// </summary>
        public string SrcSystemValTxt { get; set; }


    }
}
