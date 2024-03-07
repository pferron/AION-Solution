using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.BL.BusinessObjects
{
    public class FacilitatorMeetingApptModelBO : ModelBaseModelBO
    {
        private FacilitatorMeetingAppointmentBO _itemBO;
        private FacilitatorMeetingAppointmentBE _itemBE;
        public FacilitatorMeetingAppointment GetInstance(int id)
        {
            _itemBO = new FacilitatorMeetingAppointmentBO();
            _itemBE = _itemBO.GetById(id);
            return ConvertItemBEToModel(_itemBE);
        }

        public List<FacilitatorMeetingAppointment> Search(DateTime? startDate, DateTime? endDate, string fmaStatusIds)
        {
            _itemBO = new FacilitatorMeetingAppointmentBO();
            List<FacilitatorMeetingAppointment> retitems = new List<FacilitatorMeetingAppointment>();
            List<FacilitatorMeetingAppointmentBE> items = _itemBO.Search(startDate, endDate, fmaStatusIds);
            foreach (FacilitatorMeetingAppointmentBE i in items)
            {
                retitems.Add(ConvertItemBEToModel(i));
            }

            return retitems;
        }

        public List<FacilitatorMeetingAppointment> GetByProjectId(int projectId)
        {
            _itemBO = new FacilitatorMeetingAppointmentBO();
            List<FacilitatorMeetingAppointment> retitems = new List<FacilitatorMeetingAppointment>();
            List<FacilitatorMeetingAppointmentBE> items = _itemBO.GetByProjectId(projectId);
            foreach (FacilitatorMeetingAppointmentBE i in items)
            {
                retitems.Add(ConvertItemBEToModel(i));
            }

            return retitems;
        }

        public List<FacilitatorMeetingAppointment> GetByProjectIdAndMeetingType(string projectId, string meetingTypeDesc)
        {
            _itemBO = new FacilitatorMeetingAppointmentBO();
            List<FacilitatorMeetingAppointment> retitems = new List<FacilitatorMeetingAppointment>();
            List<FacilitatorMeetingAppointmentBE> items = _itemBO.GetListByProjectAndMeetingType(projectId, meetingTypeDesc);

            foreach (FacilitatorMeetingAppointmentBE i in items)
            {
                retitems.Add(ConvertItemBEToModel(i));
            }

            return retitems;
        }


        #region Private Methods
        /// <summary>
        /// converts BE object to view model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private FacilitatorMeetingAppointment ConvertItemBEToModel(FacilitatorMeetingAppointmentBE item)
        {
            FacilitatorMeetingAppointment fma = new FacilitatorMeetingAppointment
            {
                ApptResponseStatusEnum = item.ApptResponseStatusRefId.HasValue ? new AppointmentResponseStatusModelBO().GetInstance(item.ApptResponseStatusRefId.Value).ApptResponseStatusEnum : AppointmentResponseStatusEnum.Not_Scheduled,
                ApptResponseStatusRefId = item.ApptResponseStatusRefId,
                ApptCancellationEnum = item.ApptCancellationRefId.HasValue ? new AppointmentCancellationRefModelBO().GetInstance(item.ApptCancellationRefId.Value).ApptCancellationEnum : AppointmentCancellationEnum.NA,
                ApptCancellationRefId = item.ApptCancellationRefId,
                FromDt = item.FromDt,
                ToDt = item.ToDt,
                StartDate = item.FromDt,
                EndDate = item.ToDt,
                FacilitatorMeetingApptID = item.FacilitatorMeetingApptId,
                ProjectID = item.ProjectId,
                ID = item.FacilitatorMeetingApptId.HasValue ? item.FacilitatorMeetingApptId.Value : 0,
                CreatedDate = item.CreatedDate.HasValue ? item.CreatedDate.Value : DateTime.Now,
                UpdatedDate = item.UpdatedDate.HasValue ? item.UpdatedDate.Value : DateTime.Now,
                CreatedUser = (item.CreatedByWkrId != null) ? new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId)) : null,
                UpdatedUser = (item.UpdatedByWkrId != null) ? new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId)) : null,
                MeetingRoomRefId = item.MeetingRoomRefId,
                MeetingTypeRefId = item.MeetingTypRefId,
                MeetingTypeEnum = (MeetingTypeEnum)item.MeetingTypRefId,
                MeetingRoom = item.MeetingRoomRefId != null ? new MeetingRoomBO().GetById(item.MeetingRoomRefId.Value) : null,
                RequestedDate1 = item.RequestedDate1.HasValue ? item.RequestedDate1.Value : (DateTime?)null,
                RequestedDate2 = item.RequestedDate2.HasValue ? item.RequestedDate2.Value : (DateTime?)null,
                RequestedDate3 = item.RequestedDate3.HasValue ? item.RequestedDate3.Value : (DateTime?)null,
                ExternalAttendeesCnt = item.ExternalAttendeesCnt,
                UserId = !string.IsNullOrWhiteSpace(item.UpdatedByWkrId) ? item.UpdatedByWkrId : "1"
            };
            return fma;
        }
        #endregion Private Methods
    }
}