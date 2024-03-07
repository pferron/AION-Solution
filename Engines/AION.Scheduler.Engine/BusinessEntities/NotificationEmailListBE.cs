#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - NotificationEmailListBE

    [DataContract]
    public class NotificationEmailListBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? NotificationEmailListId { get; set; }

        [DataMember]
        public int? ProjectEmailNotificationId { get; set; }

        [DataMember]
        public int? SendUserId { get; set; }

        [DataMember]
        public string EmailAddressTxt { get; set; }

        #endregion

    }

    #endregion

}