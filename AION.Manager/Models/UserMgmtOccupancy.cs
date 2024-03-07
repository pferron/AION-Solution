using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class UserMgmtOccupancy
    {
        public int OccupancyId { get; set; }
        public string OccupancyName { get; set; }
        public int SquareFootageId { get; set; }

        public int UserId { get; set; }
        public bool OccuIdSelected { get; set; }
    }
}