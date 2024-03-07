using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Manager.Models;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class MeetingRoomAdapter : BaseManagerAdapter, IMeetingRoomAdapter
    {
        public List<MeetingRoom> GetMeetingRooms(bool isactive, string meetingType = "")
        {
            return new MeetingRoomBO().GetListOfRooms(true, meetingType);
        }

        public bool ReserveMeetingRoom(string subject, int meetingRoomRefId, MeetingAllocationRequest request)
        {
            try
            {
                //Participant list can be empty 
                if (string.IsNullOrWhiteSpace(subject) || meetingRoomRefId == 0 || request.RequestedStartTime == DateTime.MinValue
                    || request.RequestedEndTime == DateTime.MinValue)
                    return false;

                bool success = false;
                //string subject = "Manual Express Reservation";
                OutlookAdapter outlookAdapter = new OutlookAdapter();
                MeetingRoom meetingRoom = new MeetingRoomBO().GetById(meetingRoomRefId);

                List<string> ParticipantEmail = new List<string>();
                ParticipantEmail.Add(meetingRoom.MeetingRoomEmail);

                //find out if this reservation already exists
                bool reservationExists = false;
                MeetingAllocationResponse response = new MeetingAllocationResponse();
                response = outlookAdapter.CheckForMeetingAllocationAvailability(request);
                if (response != null && response.AllocatedMeetings != null)
                {
                    foreach (MeetingAllocations item in response.AllocatedMeetings)
                    {
                        if (!string.IsNullOrWhiteSpace(item.Subject) && item.Subject == subject)
                        {
                            reservationExists = true;
                            break;
                        }
                    }
                }
                if (reservationExists == false)
                {
                    success = outlookAdapter.InjectMeetingToAllAttendeesDefaultCalendars(
                       subject, subject, ParticipantEmail,
                       meetingRoom.MeetingRoomName, meetingRoom.MeetingRoomEmail, request.RequestedStartTime, request.RequestedEndTime);
                }
                else
                {
                    success = true;
                }

                return success;
            }
            catch (Exception ex)
            {

                string errorMessage = "Error in ReserveMeetingRoom - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
    }
}