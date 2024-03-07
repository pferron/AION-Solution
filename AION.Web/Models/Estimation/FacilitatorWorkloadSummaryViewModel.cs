using AION.BL;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Web.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class FacilitatorWorkloadSummaryViewModel : ViewModelBase
    {
       
        public FacilitatorWorkloadSummaryViewModel()
        {
            FacilitatorWorkloadSummary = new List<Facilitator>();
            StartDate= DateTime.Now.Date.AddDays(-30);
            EndDate= DateTime.Now.Date;
        }
        public List<Facilitator> FacilitatorWorkloadSummary { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}