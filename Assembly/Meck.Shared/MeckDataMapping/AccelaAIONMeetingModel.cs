using Meck.Shared.Accela;

namespace Meck.Shared.MeckDataMapping
{
    public class AccelaAIONMeetingModel
    {
        public AIONQueueRecordBE AIONQueueRecordBE { get; set; }
        public AccelaProjectModel MeetingRequest { get; set; }
        public string ProjectId { get; set; }
    }
}
