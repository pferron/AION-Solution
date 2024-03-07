using AION.BL.Models;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IMeetingRoomAdapter
    {
        List<MeetingRoom> GetMeetingRooms(bool isactive, string meetingType ="");
        bool ReserveMeetingRoom(string subject, int meetingRoomRefId, MeetingAllocationRequest request);
    }
}
