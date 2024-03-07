#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - TemplateBE

    [DataContract]
    public class MessageTemplateBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? TemplateId { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        [DataMember]
        public int? TemplateTypeId { get; set; }

        [DataMember]
        public string TemplateText { get; set; }

        [DataMember]
        public bool? ActiveInd { get; set; }

        [DataMember]
        public DateTime? ActiveDt { get; set; }

        #endregion

    }

    #endregion

}