using System.Collections.Generic;

namespace AION.Manager.Models
{
    /// <summary>
    /// Provides a model for getting the lists of attendees for non project appointments
    /// </summary>
    public class NPAAttendeesModel : INPAAttendees
    {
        /// <summary>
        /// Int list of User Ids of attendees (reviewers)
        /// </summary>
        public List<AttendeeInfo> AttendeeIds { get; set; }

        /// <summary>
        /// NonProjectAppointmentID
        /// </summary>
        public int NpaId { get; set; }
        /// <summary>
        /// UserID of person who Updated/Created/Deleted
        /// </summary>
        public int WkrId { get; set; }
    }

}