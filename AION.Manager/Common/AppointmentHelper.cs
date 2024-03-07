using AION.BL;
using AION.BL.BusinessObjects;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Common
{
    public static class AppointmentHelper
    {
        public static Appointment GetAppointment(int meetingId, string meetingType)
        {
            switch (meetingType)
            {
                case "Preliminary Meeting":
                    return new PreliminaryMeetingApptModelBO().GetInstance(meetingId);
                default:
                    return new FacilitatorMeetingApptModelBO().GetInstance(meetingId);
            }
        }

        public static List<AttendeeInfo> ProcessAttendeesCSV(string attendeesCsv)
        {
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();
            if (string.IsNullOrWhiteSpace(attendeesCsv)) return attendees;

            if (attendeesCsv.Contains(";"))
            {
                //multiple attendees, ex 1,1,reviewer name;2,2, reviewer name
                foreach (string item in attendeesCsv.Split(';'))
                {
                    var attendee = item.Split(',');
                    AttendeeInfo attendeeInfo = new AttendeeInfo
                    {
                        AttendeeId = int.Parse(attendee[0]),
                        BusinessRefId = int.Parse(attendee[1]),
                        FirstName = attendee[2],
                        LastName = attendee[3]
                    };
                    attendees.Add(attendeeInfo);
                }
            }
            else
            {
                //single attendee ex 1,1, reviewer name
                var attendee = attendeesCsv.Split(',');
                AttendeeInfo attendeeInfo = new AttendeeInfo
                {
                    AttendeeId = int.Parse(attendee[0]),
                    BusinessRefId = int.Parse(attendee[1]),
                    FirstName = attendee[2],
                    LastName = attendee[3]
                };
                attendees.Add(attendeeInfo);
            }

            return attendees;
        }
    }
}