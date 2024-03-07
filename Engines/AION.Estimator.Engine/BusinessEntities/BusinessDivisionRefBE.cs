#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - BusinessDivisionRefBE

    [DataContract]
    public class BusinessDivisionRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? BusinessDivisionRefId { get; set; }

        [DataMember]
        public string BusinessDivisionName { get; set; }

        [DataMember]
        public string BusinessDivisionDesc { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        #endregion

    }

    #endregion
    #region BusinessEntitiy - BusinessDivisionXRefBE

    [DataContract]
    public class BusinessDivisionXRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? BusinessDivisionRefId { get; set; }

        [DataMember]
        public string BusinessDivisionName { get; set; }

        [DataMember]
        public string BusinessDivisionDesc { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        [DataMember]
        public int? BusinessRefId { get; set; }

        #endregion

    }

    #endregion

}