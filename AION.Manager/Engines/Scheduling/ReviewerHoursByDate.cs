using AION.BL;
using System;

namespace AION.Manager.Engines.Scheduling
{
    public class ReviewerHoursByDate
    {
        public UserIdentity UserIdentity { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalHours { get; set; }
        public decimal RemainingAvailableHours { get; set; }
    }
}