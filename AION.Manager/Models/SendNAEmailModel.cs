namespace AION.Manager.Models
{
    public class SendNAEmailModel
    {
        public string AccelaProjectRefId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectStatusDesc { get; set; }
        public string PendingCommentsToCustomer { get; set; }
        public string UserName { get; set; }
        public string TimeStamp { get; set; }
    }
}