namespace AION.BL.Models
{
    public class ApplicationNotes : ModelBase
    {
        public ProjectStatusEnum PendingReason { get; set; }

        public string PendingNotesComments { get; set; }

        public string PendingGateNotesComments { get; set; }

        public string InternalNotesComments { get; set; }
    }
}
