using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class RequestPrelimDatesManagerModel
    {
        public int PreliminaryMeetingApptId { get; set; }

       // public string ApptResponse { get; set; }

        public DateTime RequestDate1 { get; set; }

        public DateTime RequestDate2 { get; set; }

        public DateTime RequestDate3 { get; set; }
    }
}