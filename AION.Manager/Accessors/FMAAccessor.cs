using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.Engine.BusinessEntities;
using AION.Manager.Adapters;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Accessors
{
    public class FMAAccessor : BaseAdapter, IFMAAccessor
    {
        public List<FacilitatorMeetingAppointment> GetListByProjectId(int projectId)
        {
            List<FacilitatorMeetingAppointment> items = new List<FacilitatorMeetingAppointment>();

            try
            {
                items = new FacilitatorMeetingApptModelBO().GetByProjectId(projectId);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetByProjectId FMAAccessor - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return items;
        }

        public List<FacilitatorMeetingAppointment> GetByProjectIDAndMeetingType(string projectId, string meetingTypeDesc)
        {
            List<FacilitatorMeetingAppointment> items = new List<FacilitatorMeetingAppointment>();
            try
            {
                items = new FacilitatorMeetingApptModelBO().GetByProjectIdAndMeetingType(projectId, meetingTypeDesc);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetByProjectIdAndMeetingType FMAAccessor - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return items;
        }

        public bool UpdateStatus(FacilitatorMeetingAppointment fma, int apptResponseStatusRefId, int apptCancellationRefId)
        {
            bool success = false;

            try
            {
                fma.ApptResponseStatusRefId = apptResponseStatusRefId;
                fma.ApptCancellationRefId = apptCancellationRefId;

                FacilitatorMeetingAppointmentBE fmaBE = ConvertFMAToBE(fma);
                int updatedId = new FacilitatorMeetingAppointmentBO().Update(fmaBE);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateStatus FMAAccessor - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool CancelByProjectId(int projectId)
        {
            FacilitatorMeetingApptModelBO bo = new FacilitatorMeetingApptModelBO();
            List<FacilitatorMeetingAppointment> items = bo.GetByProjectId(projectId);

            foreach (FacilitatorMeetingAppointment item in items)
            {
                if (item.FacilitatorMeetingApptID.HasValue)
                {
                    IAppointmentAdapter adapter = new FMAAdapter(item);

                    adapter.CancelAppointment();
                }
            }

            return true;
        }

        #region Private Methods
        private FacilitatorMeetingAppointmentBE ConvertFMAToBE(FacilitatorMeetingAppointment fma)
        {
            FacilitatorMeetingAppointmentBE be = new FacilitatorMeetingAppointmentBE
            {
                MeetingTypRefId = fma.MeetingTypeRefId,
                MeetingRoomRefId = fma.MeetingRoomRefId,
                FromDt = fma.FromDt,
                ToDt = fma.ToDt,
                FacilitatorMeetingApptId = fma.FacilitatorMeetingApptID,
                ApptResponseStatusRefId = fma.ApptResponseStatusRefId,
                ProjectId = fma.ProjectID,
                UpdatedDate = fma.UpdatedDate,
                CreatedByWkrId = fma.CreatedUser.ID.ToString(),
                CreatedDate = fma.CreatedDate,
                UpdatedByWkrId = fma.UpdatedUser.ID.ToString(),
                IsActive = true,
                UserId = fma.UpdatedUser.ID.ToString(),
                CancelAfterDt = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 2),
                VirtualMeetingInd = fma.VirtualMeetingInd.HasValue ? fma.VirtualMeetingInd.Value : false
            };

            return be;
        }
        #endregion
    }
}