#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - StandardNoteBE

    [DataContract]
    public class StandardNoteBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? StandardNoteId { get; set; }

        [DataMember]
        public string StandardNoteGroupName { get; set; }

        [DataMember]
        public int? NoteTypeRefId { get; set; }

        [DataMember]
        public string StandardNoteText { get; set; }

        [DataMember]
        public string StandardNoteTitleText { get; set; }

        [DataMember]
        public int? ProjectTypRefId { get; set; }
        #endregion

    }

    #endregion

}