using AION.BL;
using AION.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.Models
{
    public class BulkEstimationSaveViewModel : EstimationSaveViewModel
    {
        public BulkEstimationSaveViewModel()
        {
            IsSubmit = false;
        }

        public string ProjectIds { get; set; }
    }
}