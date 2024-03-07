#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectTypeRefBE

    [DataContract]
    public class ProjectTypeRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ProjectTypRefId { get; set; }

        [DataMember]
        public string ProjectTypRefNm { get; set; }

        [DataMember]
        public string ProjectTypRefDisplayNm { get; set; }

        [DataMember]
        public int? ExternalSystemRefId { get; set; }

        [DataMember]
        public string SrcSystemValueTxt { get; set; }

        [DataMember]
        public bool? AutoAssignFacilitator { get; set; }
        #endregion

    }

    #endregion

}