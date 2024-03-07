using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Web.Models
{
    /// <summary>
    /// Part of the Demo Controller set
    /// </summary>
    public class ProjectApplicationDetailViewModel
    {
        public ProjectEstimation project { get; set; }
        public List<string> loadprojects { get; set; }
        public string selectedloadproject { get; set; }
        public string projectid { get; set; }
    }
}