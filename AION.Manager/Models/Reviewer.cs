using System.Collections.Generic;

namespace AION.BL.Models
{
    public class Reviewer: UserIdentity
    {
        public double AssignedProjectsHours { get; set; }
        public int AssignedProjectCount { get; set; }
    }
}
