using AION.BL;
using AION.Web.Models.ProjectDetail;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class ScheduledMeetingPartialViewModel
    {
        public string ProjectId { get; set; }

        public int LoggedInUserId { get; set; }

        public int MeetingId { get; set; }

        public DateTime MeetingDate { get; set; }

        public string MeetingTime { get; set; }

        public DateTime? AgendaDue { get; set; }

        public DateTime? ResponseDue { get; set; }

        public int ApptResponseStatusId { get; set; }

        public MeetingTypeEnum MeetingTypeEnum { get; set; }

        public AppointmentResponseStatusEnum AppointmentResponseStatusEnum { get; set; }

        public AppointmentCancellationEnum? AppointmentCancellationEnum { get; set; }
        public string StatusLabel { get; set; }

        private List<SelectListItem> _meetingApptResponseSelectList;
        public List<SelectListItem> MeetingApptResponseSelectList
        {
            get
            {
                if (_meetingApptResponseSelectList == null) BuildMeetingApptResponseSelectList();
                return _meetingApptResponseSelectList;
            }
        }
        /// <summary>
        /// LES-4028 - add attendees for each cycle for each plan review
        /// </summary>
        public List<AssignedReviewerListViewModel> Attendees { get; set; }

        private void BuildMeetingApptResponseSelectList()
        {
            _meetingApptResponseSelectList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = "Accept", Value = ((int)AppointmentResponseStatusEnum.Accept).ToString()},
                 new SelectListItem() { Text = "Reject", Value = ((int)AppointmentResponseStatusEnum.Reject).ToString()},
            };
        }
    }
}