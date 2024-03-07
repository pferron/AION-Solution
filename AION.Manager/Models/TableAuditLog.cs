namespace AION.BL.Models
{
    public class TableAuditLog : ModelBase
    {
        #region Properties

        public int? TableAuditLogId { get; set; }

        public string AuditTypeCd { get; set; }

        public string AuditTableName { get; set; }

        public int? AuditTablePkId { get; set; }

        public string AuditFieldNm { get; set; }

        public string OldValTxt { get; set; }

        public string NewValTxt { get; set; }
        /// <summary>
        /// Readable description of what was changed
        /// User Department Relationship, Default Hours, Holiday configuration
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Readable description of what was changed by field name
        /// First Name Last Name Default Hours 
        /// </summary>
        public string ValTxt { get; set; }
        /// <summary>
        /// Readable Update, Insert, Delete
        /// </summary>
        public string AuditCodeName { get; set; }
        #endregion   
    }
}
