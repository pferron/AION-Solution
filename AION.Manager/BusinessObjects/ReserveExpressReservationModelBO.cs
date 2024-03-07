using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL.BusinessObjects
{
    public class ReserveExpressReservationModelBO : IReserveExpressReservationBO
    {
        private ReserveExpressReservationBO _itemBO;
        private ReserveExpressReservationBE _itemBE;

        public ReserveExpressReservation GetInstance(int id)
        {
            _itemBO = new ReserveExpressReservationBO();
            _itemBE = _itemBO.GetById(id);
            return ConvertItemBEToModel(_itemBE);
        }

        private ReserveExpressReservation ConvertItemBEToModel(ReserveExpressReservationBE item)
        {
            ReserveExpressReservation express = new ReserveExpressReservation
            {
                ReserveExpressReservationId = item.ReserveExpressReservationId,
                ReserveExpressDt = item.ReserveExpressDt,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                MeetingRoomRefId = item.MeetingRoomRefId,
                MeetingRoom = item.MeetingRoomRefId > 0 ? new MeetingRoomBO().GetById(item.MeetingRoomRefId.Value) : null,
                CreatedDate = item.CreatedDate.HasValue ? item.CreatedDate.Value : DateTime.Now,
                UpdatedDate = item.UpdatedDate.HasValue ? item.UpdatedDate.Value : DateTime.Now,
                CreatedUser = (item.CreatedByWkrId != null) ? new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId)) : null,
                UpdatedUser = (item.UpdatedByWkrId != null) ? new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId)) : null,
                ProposedDate1 = item.RequestedDate1,
                ProposedDate2 = item.RequestedDate2,
                ProposedDate3 = item.RequestedDate3,
                UserId = item.UserId
            };
            return express;
        }
    }

    public interface IReserveExpressReservationBO
    {
        ReserveExpressReservation GetInstance(int id);
    }
}