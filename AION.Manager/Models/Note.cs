using AION.BL.Models;
using System.Collections.Generic;

namespace AION.BL
{
    public class Note : ModelBase
    {
        public Note() : base()
        {
            NotesType = new NoteType();
            Attachments = new List<NotesAttachments>();
        }

        public NoteType NotesType { get; set; }

        public string NotesComments { get; set; }

        public List<NotesAttachments> Attachments { get; set; }

        public int ProjectID { get; set; }

        public DepartmentNameEnums DeptNameEnum { get; set; }

        public int ParentNoteID { get; set; }
        public string DeptNameDesc { get; set; }
    }


}

