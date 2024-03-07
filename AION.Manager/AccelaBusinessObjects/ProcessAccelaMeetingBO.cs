using AION.Accela.Engine.BusinessObjects;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AION.Manager.AccelaBusinessObjects
{
    public class ProcessAccelaMeetingBO : AccelaBusinessObjectBase
    {
        private Task _Logging;

        public async Task<bool> ProcessFacilitatorMeeting(AccelaAIONMeetingModel accelaMeeting)
        {
            FacilitatorMeeting meeting = await GenerateFacilitatorMeeting(accelaMeeting);

            if (meeting == null && string.IsNullOrWhiteSpace(meeting.MeetingType))
            {
                string errorMessage = "Error generating a facilitator meeting for record - " + accelaMeeting.AIONQueueRecordBE.REC_ID_NUM;
                _Logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                return false;
            }

            FacilitatorMeetingAppointmentBO fmaBO = new FacilitatorMeetingAppointmentBO();

            ProjectBO projectBO = new ProjectBO();
            ProjectBE projectBE = projectBO.GetByExternalRefInfo(meeting.ProjectId);

            if (!projectBE.ProjectId.HasValue)
            {
                string errorMessage = $"Error retrieving project from AION record - {accelaMeeting.AIONQueueRecordBE.REC_ID_NUM}";
                _Logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                return false;
            }

            MeetingType meetingType = new MeetingTypeModelBO().GetInstance(meeting.MeetingType);

            if (meetingType == null)
            {
                string errorMessage = $"Error mapping meeting type coming from Accela for record - {accelaMeeting.AIONQueueRecordBE.REC_ID_NUM} - meeting type: { meeting.MeetingType}";
                _Logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                return false;
            }

            AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Not_Scheduled);

            FacilitatorMeetingAppointmentBE fma = new FacilitatorMeetingAppointmentBE()
            {
                ProjectId = projectBE.ProjectId.Value,
                MeetingTypRefId = meetingType.MeetingTypeRefId,
                ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId,
                ExternalAttendeesCnt = meeting.ExternalAttendeesCnt,
                CreatedByWkrId = new UserIdentityModelBO().GetInstance(1).ID.ToString(),
                UpdatedByWkrId = new UserIdentityModelBO().GetInstance(1).ID.ToString(),
                UserId = "1"
            };

            fmaBO.Create(fma);

            return true;
        }

        public async Task<FacilitatorMeeting> GenerateFacilitatorMeeting(AccelaAIONMeetingModel meetingModel)
        {
            FacilitatorMeeting meeting = new FacilitatorMeeting();

            try
            {
                //internal meeting
                if (meetingModel.AIONQueueRecordBE.WORKFLOW_TASK_STATUS == "Meeting Requested")
                {
                    meeting.ProjectId = meetingModel.ProjectId;

                    WorkTaskCustForms workTaskCustForms = await GetRecordWorkFlowTaskCustForms(meetingModel.AIONQueueRecordBE.REC_ID_NUM, meeting.ProjectId);

                    if (workTaskCustForms == null || workTaskCustForms.stageMeetings.Count() == 0)
                    {
                        string errorMessage = "Error retrieving Accela work flow task custom forms - " + meetingModel.AIONQueueRecordBE.REC_ID_NUM;
                        _Logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);

                        return null;
                    }

                    WorkTaskCustForms.WorkTaskMeeting workTaskMeeting = workTaskCustForms.stageMeetings.FirstOrDefault();

                    if (workTaskMeeting == null)
                    {
                        string errorMessage = "Error retrieving Accela meeting from work flow task for record - " + meetingModel.AIONQueueRecordBE.REC_ID_NUM;
                        _Logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);

                        return null;
                    }

                    if (workTaskMeeting != null)
                    {
                        meeting.MeetingType = workTaskMeeting.MeetingType.Replace(" Meeting", "").Replace("-", "");
                    }
                }
                //external meeting
                else if (meetingModel.AIONQueueRecordBE.REC_TYP_DESC == "Meeting Request")
                {
                    meeting.ProjectId = meetingModel.ProjectId;
                    meeting.MeetingType = meetingModel.MeetingRequest.MeetingType.Replace(" Meeting", "");
                    meeting.RequestedMeetingDates.Add(new RequestedMeetingDateBE { RequestedMeetingDate = meetingModel.MeetingRequest.RequestedMeetingDate });
                    if (meetingModel.MeetingRequest.ProjectStatusCodeRef == "Cancelled" || meetingModel.AIONQueueRecordBE.STATUS_DESC == "Cancelled") meeting.IsCancelled = true;
                    meeting.ExternalAttendeesCnt = meetingModel.MeetingRequest.NumberOfAttendees;
                }
            }
            catch (Exception)
            {
                string errorMessage = "Error generating a facilitator meeting - " + meetingModel.AIONQueueRecordBE.REC_ID_NUM;
                _Logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }

            return meeting;
        }

        private async Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustForms(string recordId, string projectId)
        {
            AccelaApiBO acceladataconn = new AccelaApiBO();

            var workTaskCustForms = await acceladataconn.GetRecordWorkFlowTaskCustForms(recordId, projectId);

            return workTaskCustForms;
        }
    }
}