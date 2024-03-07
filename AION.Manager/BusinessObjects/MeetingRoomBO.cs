using AION.Base;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.BL.BusinessObjects
{
    public class MeetingRoomBO : BaseAdapter, IMeetingRoomBO
    {
        MeetingRoomRefBO _bo;

        public MeetingRoom GetById(int id)
        {
            MeetingRoom meetingRoom = new MeetingRoom();
            try
            {
                _bo = new MeetingRoomRefBO();
                MeetingRoomRefBE beitem = _bo.GetById(id);
                meetingRoom = ConvertItemBEToModel(beitem);

            }
            catch (Exception ex)
            {

                string errorMessage = "An error in GetById " + id + "- " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return meetingRoom;
        }

        public List<MeetingRoom> GetListOfRooms(bool isactive, string meetingType = "")
        {
            List<MeetingRoom> items = new List<MeetingRoom>();
            try
            {
                _bo = new MeetingRoomRefBO();
                List<MeetingRoomRefBE> beitems = _bo.GetList(meetingType);
                foreach (MeetingRoomRefBE be in beitems)
                {
                    items.Add(ConvertItemBEToModel(be));
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "An error in GetListOfRooms " + isactive + "- " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return items;
        }

        #region Private Methods
        private MeetingRoom ConvertItemBEToModel(MeetingRoomRefBE item)
        {
            return new MeetingRoom
            {
                MeetingRoomName = item.MeetingRoomName,
                MeetingRoomEmail = item.MeetingRoomEmail,
                MeetingRoomRefID = item.MeetingRoomRefID,
                UserPrincipalName = item.UserPrincipalName,
                CalendarId = item.CalendarId,
                IsActive = item.IsActive,
                CreatedDate = item.CreatedDate.Value,
                CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId)),
                UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId)),
                ID = (int)item.MeetingRoomRefID,
                UpdatedDate = item.UpdatedDate.Value
            };
        }
        #endregion Private Methods
    }
    public interface IMeetingRoomBO
    {
        List<MeetingRoom> GetListOfRooms(bool isactive, string meetingType = "");
        MeetingRoom GetById(int id);

    }
}
