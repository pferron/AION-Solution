using AION.BL.Models;
using System;

namespace AION.BL
{
    public class ExpressMeetingAppointment : Appointment
    {
        private int? _expressMeetingApptID;
        public int? ExpressMeetingApptID
        {
            get { return _expressMeetingApptID; }
            set
            {
                _expressMeetingApptID = value;
                if (value != null)
                {
                    ID = _expressMeetingApptID.Value;
                }
            }
        }

        private DateTime? _fromDt;
        public DateTime? FromDT
        {
            get { return _fromDt; }
            set
            {
                _fromDt = value;
                StartDate = value;
            }
        }

        private DateTime? _toDt;
        public DateTime? ToDT
        {
            get { return _toDt; }
            set
            {
                _toDt = value;
                EndDate = value;
            }
        }

        public DateTime? ProdDate { get; set; }
        public DateTime? GateDate { get; set; }
        public DateTime? AppendixAgendaDueDt { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }
        public int Cycle { get; set; }
        public int PlanReviewProjectDetailsId { get; set; }
        public PlanReviewProjectDetails PlanReviewProjectDetails { get; set; }
        /// <summary>
        /// LES-3809 - add project audit for auto schedule
        /// </summary>
        public bool AutoScheduled { get; set; }

        public ExpressMeetingAppointment()
        {
            ProjectScheduleRefEnum = ProjectScheduleRefEnum.EMA;
        }
    }
}