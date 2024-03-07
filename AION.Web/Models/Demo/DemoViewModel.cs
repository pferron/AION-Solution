using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Web.Models
{
    /// <summary>
    /// Used in the Demo Controller set
    /// </summary>
    public class DemoViewModel
    {
        public List<ProjectEstimation> projects { get; set; }
        public string username { get; set; }
        public int tasks { get; set; }
        public string projectnumber { get; set; }
        public string projectstatus { get; set; }
        public string propertytypename { get; set; }
        public int propertytype { get; set; }
        public string departmentname { get; set; }
        public int department { get; set; }
        public double costofconstruction { get; set; }
        public int reviewsquarefootage { get; set; }
        public int numberofsheets { get; set; }
        public string occupancytype { get; set; }
        public decimal? estimationhours { get; set; }
        public List<AverageActualHoursFactor> actuals { get; set; }
        public AverageActualHoursFactor actualhours { get; set; }
        public string loaddata { get; set; }
        public decimal? buildinghours { get; set; }
        public decimal? electricalhours { get; set; }
        public decimal? mechanicalhours { get; set; }
        public decimal? plumbinghours { get; set; }
        public string constructiontype { get; set; }
        public List<Note> Notes { get; set; }
        public List<string> Folders { get; set; }
    }
}