using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.BL.Models
{
    public class NonProjectAppointment : Appointment
    {
        #region Properties

        private int? _nonProjectAppointmentmentId;
        public int? NonProjectAppointmentID
        {
            get { return _nonProjectAppointmentmentId; }
            set
            {
                _nonProjectAppointmentmentId = value;
                ID = _nonProjectAppointmentmentId.HasValue ? _nonProjectAppointmentmentId.Value : 0;
            }
        }

        private DateTime? _appointmentFrom;
        public DateTime? AppointmentFrom
        {
            get { return _appointmentFrom; }
            set
            {
                _appointmentFrom = value;
                StartDate = value;
            }
        }

        private DateTime? _appointmentTo;
        public DateTime? AppointmentTo
        {
            get { return _appointmentTo; }
            set
            {
                _appointmentTo = value;
                EndDate = value;
            }
        }

        public string AppoinmentName { get; set; }

        public bool? IsAllPlanReviewers { get; set; }

        public bool? IsAllDay { get; set; }

        public int? NPATypeRefID { get; set; }

        public int? AppoinmentRecurrenceRefID { get; set; }

        public bool? IsAllBuild { get; set; }

        public bool? IsAllElectric { get; set; }

        public bool? IsAllMech { get; set; }

        public bool? IsAllPlumb { get; set; }

        public bool? IsAllZoning { get; set; }

        public bool? IsAllFire { get; set; }

        public bool? IsAllBackFlow { get; set; }

        public bool? IsAllEhsFood { get; set; }

        public bool? IsAllEhsPool { get; set; }

        public bool? IsAllEhsLodge { get; set; }

        public bool? IsAllEhsDayCare { get; set; }
        public AppointmentRecurrenceRefEnum AppointmentRecurrence { get; set; }
        public List<AttendeeInfo> AttendeeInfoList { get; set; }

        public NonProjectAppointment()
        {
            ProjectScheduleRefEnum = ProjectScheduleRefEnum.NPA;
        }
        #endregion
    }
}
