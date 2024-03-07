using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.BL.Models
{
    public class NPASearchResult
    {

        //Shijo 5-12-2020 Added to manage each recurracne date. Incase NPA is recurring this will conains multiple instance with seperate ProjectScheduleID.
        public int ProjectScheduleID { get; set; }

        public int? NPAId { get; set; }

        public string NPAType { get; set; }

        public string NPADate { get; set; }

        public string NPAName { get; set; }

        public string IsRecurring { get; set; }

        public string MeetingRoomName { get; set; }


        public string Day { get; set; }

        public string Time { get; set; }

        //public List<UserIdentity> Attendees { get; set; }
        public List<AttendeeInfo> Attendees { get; set; }

        public List<ProjectSchedule> Schedules { get; set; }

        /// <summary>
        /// Actual date this project schedule id falls on
        /// </summary>
        public System.DateTime Recurring_Date { get; set; }
    }
}
