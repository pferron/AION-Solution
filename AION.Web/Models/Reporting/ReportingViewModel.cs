using AION.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.Models.Reporting
{
    public class ReportingViewModel:ViewModelBase
    {
        public ReportingViewModel()
        {

            FacilitatorWorkloadSummary = new List<Facilitator>();
        }
        public List<Facilitator> FacilitatorWorkloadSummary { get; set; }
        public string ReportUrl { get; set; }
        public string ManagementReportUrl { get; set; }
    }
}