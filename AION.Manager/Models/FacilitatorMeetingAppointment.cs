using System;

namespace AION.BL
{
    public class FacilitatorMeetingAppointment : Appointment
    {
        private int? _facilitatorMeetingApptID;
        public int? FacilitatorMeetingApptID
        {
            get { return _facilitatorMeetingApptID; }
            set
            {
                _facilitatorMeetingApptID = value;
                ID = _facilitatorMeetingApptID.Value;
            }
        }

        private DateTime? _fromDt;
        public DateTime? FromDt
        {
            get { return _fromDt; }
            set
            {
                _fromDt = value;
                StartDate = value;
            }
        }

        private DateTime? _toDt;
        public DateTime? ToDt
        {
            get { return _toDt; }
            set
            {
                _toDt = value;
                EndDate = value;
            }
        }

        public DateTime? RequestedDate1 { get; set; }
        public DateTime? RequestedDate2 { get; set; }
        public DateTime? RequestedDate3 { get; set; }
        public int? ExternalAttendeesCnt { get; set; }
        /// <summary>
        /// LES-3809 - add project audit for auto schedule
        /// </summary>
        public bool AutoScheduled { get; set; }

        public FacilitatorMeetingAppointment()
        {
            ProjectScheduleRefEnum = ProjectScheduleRefEnum.FMA;
        }
    }
}