#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - MeetingRoomRefBE

    [DataContract]
    public class MeetingRoomRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? MeetingRoomRefID { get; set; }

        [DataMember]
        public string MeetingRoomName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }


        [DataMember]
        public string MeetingRoomEmail { get; set; }

        [DataMember]
        public string UserPrincipalName { get; set; }

        [DataMember]
        public string CalendarId { get; set; }

        #endregion

    }

    #endregion

}