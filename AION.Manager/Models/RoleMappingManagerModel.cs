using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class RoleMappingManagerModel
    {
        public int UserId { get; set; }
        public List<int> RoleMappings { get; set; }
        public int WrkrId { get; set; }
    }
}