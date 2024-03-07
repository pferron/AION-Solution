#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - TableAuditLogBE

    [DataContract]
    public class TableAuditLogBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? TableAuditLogId { get; set; }

        [DataMember]
        public string AuditTypeCd { get; set; }

        [DataMember]
        public string AuditTableName { get; set; }

        [DataMember]
        public int? AuditTablePkId { get; set; }

        [DataMember]
        public string AuditFieldNm { get; set; }

        [DataMember]
        public string OldValTxt { get; set; }

        [DataMember]
        public string NewValTxt { get; set; }

        #endregion

    }

    #endregion

}