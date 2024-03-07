using AION.BL;

namespace AION.Manager.Models
{
    public class PermissionModule : ModelBase
    {
        public int? ModuleId { get; set; }

        public string ModuleName { get; set; }

        public int? EnumMappingNumber { get; set; }

        public PermissionModuleEnum ModuleEnum { get; set; }

        public string ModuleDisplayName { get; set; }

    }
}
