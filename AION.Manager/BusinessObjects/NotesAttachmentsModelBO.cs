using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.BusinessObjects
{
    public class NotesAttachmentsModelBO : ModelBaseModelBO, INotesAttachmentsBO
    {
        public  NotesAttachments GetInstance(int notesID)
        {
            return new NotesAttachments();
        }
    }

    public interface INotesAttachmentsBO
    {
        NotesAttachments GetInstance(int notesID);
        
    }
}
