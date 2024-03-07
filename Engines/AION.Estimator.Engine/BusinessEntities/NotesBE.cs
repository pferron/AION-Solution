#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - NotesBE

    [DataContract]
    public class NotesBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? NotesId { get; set; }

        [DataMember]
        public string NotesComment { get; set; }

        [DataMember]
        public int? ProjectId { get; set; }

        [DataMember]
        public int? NotesTypeRefId { get; set; }

        [DataMember]
        public int ParentNoteID { get; set; }

        [DataMember]
        public int BusinessRefID { get; set; }

        #endregion

    }

    #endregion

}