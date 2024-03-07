using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.BL.BusinessObjects
{
    public class PreliminaryMeetingApptModelBO : ModelBaseModelBO
    {
        private PreliminaryMeetingAppointmentBO _itemBO;
        private PreliminaryMeetingAppointmentBE _itemBE;
        public PreliminaryMeetingAppointment GetInstance(int id)
        {
            _itemBO = new PreliminaryMeetingAppointmentBO();
            _itemBE = _itemBO.GetById(id);
            return ConvertItemBEToModel(_itemBE);
        }

        /// <summary>
        /// Returns list of PreliminaryMeetingAppointment based on search criteria
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="apptResponseStatusRefId"></param>
        /// <returns></returns>
        public List<PreliminaryMeetingAppointment> Search(DateTime? startDate, DateTime? endDate, int? apptResponseStatusRefId)
        {
            _itemBO = new PreliminaryMeetingAppointmentBO();
            List<PreliminaryMeetingAppointment> retitems = new List<PreliminaryMeetingAppointment>();
            List<PreliminaryMeetingAppointmentBE> items = _itemBO.Search(startDate, endDate, apptResponseStatusRefId);
            foreach (PreliminaryMeetingAppointmentBE i in items)
            {
                retitems.Add(ConvertItemBEToModel(i));
            }

            return retitems;
        }
        public PreliminaryMeetingAppointment GetByProjectId(int id)
        {
            _itemBO = new PreliminaryMeetingAppointmentBO();
            _itemBE = _itemBO.GetByProjectId(id);
            return ConvertItemBEToModel(_itemBE);
        }
        #region Private Methods
        /// <summary>
        /// converts BE object to view model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private PreliminaryMeetingAppointment ConvertItemBEToModel(PreliminaryMeetingAppointmentBE item)
        {
            PreliminaryMeetingAppointment pma = new PreliminaryMeetingAppointment
            {
                AppendixAgendaDueDt = item.AppendixAgendaDueDt,
                ApptResponseStatusEnum = item.ApptResponseStatusRefId.HasValue ? new AppointmentResponseStatusModelBO().GetInstance(item.ApptResponseStatusRefId.Value).ApptResponseStatusEnum : AppointmentResponseStatusEnum.Not_Scheduled,
                ApptResponseStatusRefId = item.ApptResponseStatusRefId,
                ApptCancellationEnum = item.ApptCancellationRefId.HasValue ? new AppointmentCancellationRefModelBO().GetInstance(item.ApptCancellationRefId.Value).ApptCancellationEnum : AppointmentCancellationEnum.NA,
                ApptCancellationRefId = item.ApptCancellationRefId,
                FromDt = item.FromDT,
                ToDt = item.ToDT,
                PreliminaryMeetingApptID = item.PreliminaryMeetingApptID,
                ProjectID = item.ProjectID,
                ID = item.PreliminaryMeetingApptID.HasValue ? item.PreliminaryMeetingApptID.Value : 0,
                CreatedDate = item.CreatedDate.HasValue ? item.CreatedDate.Value : DateTime.Now,
                UpdatedDate = item.UpdatedDate.HasValue ? item.UpdatedDate.Value : DateTime.Now,
                CreatedUser = (item.CreatedByWkrId != null) ? new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId)) : null,
                UpdatedUser = (item.UpdatedByWkrId != null) ? new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId)) : null,
                MeetingRoomRefId = item.MeetingRoomRefID,
                MeetingRoom = item.MeetingRoomRefID != null ? new MeetingRoomBO().GetById(item.MeetingRoomRefID.Value) : null,
                ProposedDate1 = item.RequestedDate1.HasValue ? item.RequestedDate1.Value : (DateTime?)null,
                ProposedDate2 = item.RequestedDate2.HasValue ? item.RequestedDate2.Value : (DateTime?)null,
                ProposedDate3 = item.RequestedDate3.HasValue ? item.RequestedDate3.Value : (DateTime?)null,
                MeetingTypeEnum = MeetingTypeEnum.Preliminary,
                UserId = item.UserId,
                IsReschedule = item.IsReschedule.HasValue ? item.IsReschedule.Value : false
            };
            return pma;
        }
        #endregion Private Methods
    }
}