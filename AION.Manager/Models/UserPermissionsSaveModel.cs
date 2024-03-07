using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class UserPermissionsSaveModel
    {
        public int UserId { get; set; }
        public List<Permission> Permissions { get; set; }
        public string WrkrId { get; set; }
    }
}