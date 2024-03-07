namespace AION.Manager.Models
{
    public class NotificationEmailList
    {
        #region Properties

        public int? NotificationEmailListId { get; set; }

        public int? ProjectEmailNotificationId { get; set; }

        public int? SendUserId { get; set; }

        public string EmailAddressTxt { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion
    }
}