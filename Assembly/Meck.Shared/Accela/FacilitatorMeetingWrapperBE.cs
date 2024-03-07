using Newtonsoft.Json;
using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    public class FacilitatorMeetingWrapperBE
    {

        [JsonProperty("result")]
        public List<FacilitatorMeetingBE> MeetingList { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }


        public FacilitatorMeetingWrapperBE()
        {
            MeetingList = new List<FacilitatorMeetingBE>();
        }

        public class FacilitatorMeetingBE
        {
            public FacilitatorMeetingBE()
            {
                RequestedMeetingDates = new List<RequestedMeetingDateBE>();
            }

            [JsonProperty("projectid")]
            public string ProjectId { get; set; }

            [JsonProperty("meetingType")]
            public string MeetingType { get; set; }

            [JsonProperty("isCancelled")]
            public bool IsCancelled { get; set;}

            [JsonProperty("requestedMeetingDates")]
            public List<RequestedMeetingDateBE> RequestedMeetingDates { get; set; } = new List<RequestedMeetingDateBE>();
            public int? ExternalAttendeesCnt { get; set; }
        }

        public class MeetingAttendeeBE
        {
            [JsonProperty("accelaDepartmentDivisionRef")]
            public string AccelaDepartmentDivisionRef { get; set; }

            [JsonProperty("attendeeName")]
            public string Name { get; set; }
        }
    }
}
