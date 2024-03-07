using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Models
{
    public class NoteType : ModelBase
    {
        public NoteTypeEnum Type { get; set; }

        public string TypeName { get; set; }

        public string NotesExternalRef { get; set; }

        public ExternalSystem ExternalSystem { get; set; }

    }
}
