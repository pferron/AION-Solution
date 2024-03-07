using AION.BL;

namespace AION.Manager.Models
{
    public class Permission : ModelBase
    {
        public int? PermissionId { get; set; }

        public string PermissionName { get; set; }

        public int? PermissionModuleId { get; set; }

        public int? EnumMappingNumber { get; set; }

        public PermissionEnum PermissionEnum { get; set; }

        public string PermissionDisplayName { get; set; }
        public PermissionModuleEnum PermissionModuleEnum { get; set; }
    }
}
