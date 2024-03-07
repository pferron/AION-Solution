using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.Models
{
    public class AssignedFacilitatorViewModel
    {
        public int ProjectId { get; set; }
        public int FacilitatorId { get; set; }
        public string FacilitatorName { get; set; }
    }
}