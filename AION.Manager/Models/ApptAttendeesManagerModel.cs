using AION.BL;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    /// <summary>
    /// Provides a model for getting the lists of attendees for appointments
    /// </summary>
    public class ApptAttendeesManagerModel
    {
        /// <summary>
        /// list of User Ids of attendees (reviewers) and the department enum int selected
        /// </summary>
        public List<ApptAttendeeManagerModel> AttendeeIds { get; set; }
        /// <summary>
        /// Appointment Id 
        /// </summary>
        public int ApptId { get; set; }
        /// <summary>
        /// UserID of person who Updated/Created/Deleted
        /// </summary>
        public int WkrId { get; set; }

        /// <summary>
        /// Used only incase of Delete a Schedule. Set this to true incase of delete. If the value is true then Schedule id also need to be provided.
        /// </summary>
        public bool DeleteNPAProjectSchedule { get; set; }

        /// <summary>
        ///Specifies the Project Schedule ID for the attendee changes
        /// </summary>
        public int ProjectScheduleID { get; set; }

        public bool IsSchedule { get; set; }

        /// <summary>
        /// Used to indicate if emails should be sent, 
        /// Preliminary Meeting Appointment can be saved
        /// Email sent only on Submit of PMA
        /// </summary>
        public bool SendApptEmail { get; set; }

        /// <summary>
        /// Used on Project Detail to indicate meeting type being edited
        /// </summary>
        public MeetingTypeEnum MeetingType { get; set; }

        public bool ProcessInsertRemoveOnly { get; set; } = false;

    }
    public class ApptAttendeeManagerModel
    {
        public ApptAttendeeManagerModel()
        {
            RotationNbr = 0;
        }
        /// <summary>
        /// User ID
        /// </summary>
        public int AttendeeId { get; set; }
        /// <summary>
        /// DepartmentNameEnum ID
        /// </summary>
        public int DeptNameEnumId { get; set; }
        /// <summary>
        /// Business Ref Id
        /// </summary>
        public int BusinessRefId { get; set; }
        /// <summary>
        /// List ID for view 
        /// </summary>
        public string DeptNameListId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RotationNbr { get; set; }
    }

}