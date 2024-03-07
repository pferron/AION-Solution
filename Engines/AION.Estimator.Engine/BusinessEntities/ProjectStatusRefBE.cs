#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectStatusRefBE

    [DataContract]
    public class ProjectStatusRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public string ProjectStatusRefNm { get; set; }

        [DataMember]
        public int? ProjectStatusRefId { get; set; }

        [DataMember]
        public int? ExternalSystemRefId { get; set; }

        [DataMember]
        public string SrcSystemValueTxt { get; set; }

        [DataMember]
        public string ProjectStatusRefDesc { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        #endregion

    }

    #endregion

}