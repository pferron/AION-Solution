using AION.Manager.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class FIFOReviewer
    {
        public int ReviewerId { get; set; }
        public PlanReviewAutoSchedulableReviewer Reviewer { get; set; }
        public decimal MeetingDuration { get; set; }
    }
}