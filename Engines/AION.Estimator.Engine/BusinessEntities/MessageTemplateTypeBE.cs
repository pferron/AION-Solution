#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - TemplateTypeBE

    [DataContract]
    public class MessageTemplateTypeBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? TemplateTypeId { get; set; }

        [DataMember]
        public string TemplateTypeName { get; set; }

        [DataMember]
        public string TemplateTypeDesc { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        [DataMember]
        public int? TemplateModuleId { get; set; }

        [DataMember]
        public bool? IsEditable { get; set; }

        #endregion

    }

    #endregion

}