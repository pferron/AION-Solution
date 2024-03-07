using AION.BL;

namespace AION.Manager.Models
{
    public class InternalNoteManagerModel
    {
        public int? ProjectId { get; set; }
        public NoteTypeEnum? NoteTypeEnum { get; set; }

        /// <summary>
        /// ex. MMF-000030
        /// </summary>
        public string ProjectNumber { get; set; }
    }
}