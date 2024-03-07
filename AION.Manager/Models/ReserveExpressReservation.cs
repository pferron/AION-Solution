using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL.Models
{
    public class ReserveExpressReservation : Appointment
    {
        public int _reserveExpressReservationId;

        public int ReserveExpressReservationId
        {
            get { return _reserveExpressReservationId; }
            set
            {
                _reserveExpressReservationId = value;
                ID = _reserveExpressReservationId;
            }
        }

        private DateTime _reserveExpressDt;
        public DateTime ReserveExpressDt
        {
            get { return _reserveExpressDt; }
            set
            {
                _reserveExpressDt = value;
                //StartDate = _reserveExpressDt;
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                var startTime = _startTime.TimeOfDay;
                StartDate = _reserveExpressDt + startTime;
            }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                var endTime = _endTime.TimeOfDay;
                EndDate = _reserveExpressDt + endTime;
            }
        }

        public string Day { get; set; }
        public string StartTimeFormatted { get; set; }
        public string EndTimeFormatted { get; set; }
        public int BusinessRefId { get; set; }
        public int PlanReviewerId { get; set; }
        public string MeetingRoomName { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }

        public ReserveExpressReservation()
        {
            ProjectScheduleRefEnum = ProjectScheduleRefEnum.EXP;
        }
    }
}