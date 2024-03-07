using AION.BL;
using AION.BL.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class Meeting : ModelBase
    {
        /// <summary>
        /// Appointment ID
        /// NPA_ID
        /// PRELIMINARY_MEETING_APPOINTMENT_ID  
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Type of meeting, PMA, NPA, etc
        /// </summary>
        public MeetingTypeEnum MeetingType { get; set; }

        /// <summary>
        /// If this is PMA, this will contain the info
        /// </summary>
        public PreliminaryMeetingAppointment PreliminaryMeetingAppointment { get; set; }

        /// <summary>
        /// If this is NPA, this will contain the info
        /// </summary>
        public NonProjectAppointment NonProjectAppointment { get; set; }


        public int ProjectScheduleId { get; set; }
        public ProjectSchedule ProjectSchedule { get; set; }


        public List<AttendeeInfo> Attendees { get; set; }

        public MeetingRoom MeetingRoom { get; set; }
        public DateTime MeetingDate { get; set; }
        public DateTime MeetingStart { get; set; }
        public DateTime MeetingEnd { get; set; }

        public DateTime MeetingTime { get; set; }

        public PropertyTypeEnums ProjectType { get; set; }

        public string ProjectExternalRefID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public ProjectStatusEnum ProjectStatus { get; set; }
        public AppointmentResponseStatusEnum AppointmentResponseStatus { get; set; }

        public DateTime? AppendixAgendaDue { get; set; }

        public DateTime MinutesDue { get; set; }
        public bool RTAP { get; set; } //Y/N

        public bool MinutesUploaded { get; set; } //Y/N

        public string Facilitator { get; set; }
        public string TeamScore { get; set; }//Accela

        public string PMName { get; set; }//Accela

        public string PMPhone { get; set; }//Accela
        public string PMEmail { get; set; }//Accela

        public string BuildingCodeVersion { get; set; } //Accela
        public int MeetingRoomRefId { get; set; }

        /// <summary>
        /// NPA meeting name
        /// used in schedule capacity search result
        /// </summary>
        public string MeetingName { get; set; }
    }
}