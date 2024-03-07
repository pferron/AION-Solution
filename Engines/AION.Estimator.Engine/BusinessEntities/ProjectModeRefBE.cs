#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectModeRefBE

    [DataContract]
    public class ProjectModeRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ProjectModeRefId { get; set; }

        [DataMember]
        public string ProjectModeRefNm { get; set; }

        [DataMember]
        public string ProjectModeRefDisplayNm { get; set; }

        [DataMember]
        public int? ExternalSystemRefId { get; set; }

        [DataMember]
        public string SrcSystemValueTxt { get; set; }

        #endregion

    }

    #endregion

}