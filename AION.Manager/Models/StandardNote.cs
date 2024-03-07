namespace AION.BL.Models
{
    public class StandardNote
    {
        public int StandardNoteId { get; set; }
        public NoteTypeEnum StandardNoteType { get; set; }
        public string StandardNoteGroupName { get; set; }
        public string StandardNoteText { get; set; }
        public StandardNoteGroupEnums StandardNoteGroupEnum { get; set; }
        public string StandardNoteTitle { get; set; }
        public string StandardNoteGroupDesc { get; set; }
        public PropertyTypeEnums PropertyTypeEnum { get; set; }
    }
}
