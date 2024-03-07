using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class ScheduleCapacitySearch
    {
        /// <summary>
        /// List of User Ids
        /// </summary>
        public List<string> ReviewerSearchList { get; set; }

        /// <summary>
        /// Search start date and time
        /// </summary>
        public DateTime BeginDateTime { get; set; }

        /// <summary>
        /// Search end date and time
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// User who has initiated the search
        /// </summary>
        public int WrkrId { get; set; }
    }
}