using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class MeetingAllocationRequest
    {
        public MeetingAllocationRequest()
        {
            RequestedParticipantEmailList = new List<string>();
        }
        public List<string> RequestedParticipantEmailList { get; set; }
        public DateTime RequestedStartTime { get; set; }
        public DateTime RequestedEndTime { get; set; }
    }

    public class MeetingAllocationResponse
    {
        public MeetingAllocationResponse()
        {
            AllocatedParticipantList = new List<string>();
            AllocatedMeetings = new List<MeetingAllocations>();
        }

        public List<string> AllocatedParticipantList { get; set; }
        public List<MeetingAllocations> AllocatedMeetings { get; set; }
    }

    public class MeetingAllocations
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string LocationName { get; set; }
        public string TimeZone { get; set; }
        public string ParticipantEmail { get; set; }
    }
}