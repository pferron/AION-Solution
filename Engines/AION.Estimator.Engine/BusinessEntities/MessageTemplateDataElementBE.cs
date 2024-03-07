#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - MessageTemplateDataElementBE

    [DataContract]
    public class MessageTemplateDataElementBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? DataElementId { get; set; }

        [DataMember]
        public string DataElementName { get; set; }

        [DataMember]
        public string DataElementDesc { get; set; }

        [DataMember]
        public string DataElementValTxt { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        #endregion

    }

    #endregion

}