using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AION.Manager.Models
{
    public class RequestExpressDatesManagerModel
    {
        public int PlanReviewScheduleId { get; set; }

        public DateTime RequestDate1 { get; set; }

        public DateTime RequestDate2 { get; set; }

        public DateTime RequestDate3 { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}