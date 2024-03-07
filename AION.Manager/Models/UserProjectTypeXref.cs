using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Models
{
    public class UserProjectTypeXref
    {
        public UserProjectTypeXref()
        {
            ProjectTypeIDList = new List<int>();
        }
        public int UserID { get; set; }

        public List<int> ProjectTypeIDList { get; set; }

        public int UpdatedUserId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
