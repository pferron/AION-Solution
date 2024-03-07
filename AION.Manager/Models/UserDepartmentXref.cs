using System;
using System.Collections.Generic;

namespace AION.BL.Models
{
    public class UserDepartmentXref
    {
        public UserDepartmentXref()
        {
            UserDepartmentIDList = new List<int>();
        }
        public int UserID { get; set; }

        public List<int> UserDepartmentIDList { get; set; }

        public int UpdatedUserId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
