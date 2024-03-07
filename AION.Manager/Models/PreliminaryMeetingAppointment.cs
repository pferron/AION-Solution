using System;

namespace AION.BL
{
    public class PreliminaryMeetingAppointment : Appointment
    {
        private int? _preliminaryMeetingApptID;
        public int? PreliminaryMeetingApptID
        {
            get { return _preliminaryMeetingApptID; }
            set
            {
                _preliminaryMeetingApptID = value;
                if (value != null)
                {
                    ID = _preliminaryMeetingApptID.Value;
                }
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

        public DateTime? AppendixAgendaDueDt { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }
        /// <summary>
        /// LES-3809 - add project audit for auto schedule
        /// </summary>
        public bool AutoScheduled { get; set; }

        public PreliminaryMeetingAppointment()
        {
            ProjectScheduleRefEnum = ProjectScheduleRefEnum.PMA;
        }
    }
}