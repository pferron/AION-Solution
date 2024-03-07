namespace AION.Manager.Models
{
    public class AttendeeInfo
    {
        public AttendeeInfo()
        {

        }
        public AttendeeInfo(int attendeeID, int attendeeBusinessRefID)
        {
            BusinessRefId = attendeeBusinessRefID;
            AttendeeId = attendeeID;
            DeptNameEnumId = attendeeBusinessRefID;
        }
        public int AttendeeId { get; set; }

        public int BusinessRefId { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        /// <summary>
        /// DepartmentNameEnum ID
        /// </summary>
        public int DeptNameEnumId { get; set; }

        /// <summary>
        /// Date Timefrom To TimeTo
        /// Used in Project Audit
        /// </summary>
        public string MeetingInfo { get; set; }

    }
}