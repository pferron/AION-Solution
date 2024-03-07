using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL
{
    public class NotesAttachments : ModelBase
    {


        public Object Attachment { get; set; }

        public string AttachmentType { get; set; }

        public DateTime Created { get; set; }

        public UserIdentity CreatedBy { get; set; }

        public DateTime LastUpdated { get; set; }

        public UserIdentity LastUpdatedBy { get; set; }


    }
}
