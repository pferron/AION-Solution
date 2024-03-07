namespace AION.Web.Models.ConfigurationHistory
{
    public class TableAuditLogViewModel
    {
        public string UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public string TypeName { get; set; }
        public string AuditCodeName { get; set; }
    }
}