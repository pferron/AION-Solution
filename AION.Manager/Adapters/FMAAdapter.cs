using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.BusinessObjects;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class FMAAdapter : AppointmentAdapter, IAppointmentAdapter, IFMAAdapter
    {
        private FacilitatorMeetingAppointment _fma;

        public FMAAdapter() { }

        public FMAAdapter(FacilitatorMeetingAppointment fma)
        {
            _fma = fma;

            SetData();
        }

        public int Upsert()
        {
            int itemId = 0;

            try
            {
                FacilitatorMeetingAppointmentBO facilitatorMeetingAppointmentBO = new FacilitatorMeetingAppointmentBO();

                FacilitatorMeetingAppointmentBE be = ConvertFMAToBE(_fma);

                if (_fma.FacilitatorMeetingApptID == 0)
                {
                    //insert fma
                    be.UserId = "1";
                    itemId = facilitatorMeetingAppointmentBO.Create(be);

                    _Appointment.ID = itemId;

                    if (itemId == 0) { throw new Exception("Insert Facilitator Meeting Appointment error"); }

                    //insert attendees into appointment and add user schedules
                    bool success = InsertAttendees(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID, 0);

                    //jcl 4/21/22 insert attendees for detail records
                    UpsertFMADetail(ActionType.Insert);

                }
                else
                {
                    if (_Appointment.IsReschedule && _Appointment.IsSubmit)
                    {
                        //get previous scheduled dates
                        FacilitatorMeetingAppointmentBE prevAppointment = facilitatorMeetingAppointmentBO.GetById(_fma.FacilitatorMeetingApptID.Value);

                        //set previous dates for cancellation email
                        _Appointment.PrevStartDate = prevAppointment.FromDt.Value;
                        _Appointment.PrevEndDate = prevAppointment.ToDt.Value;
                    }

                    //update fma
                    int ret = facilitatorMeetingAppointmentBO.Update(be);
                    if (ret == 0) { throw new Exception("Update Facilitator Meeting Appointment error"); }
                    itemId = _fma.FacilitatorMeetingApptID.Value;

                    int projectScheduleId = UpdateProjectSchedule();

                    SetAppointmentData();

                    //update attendees into appointment and update user schedules
                    bool success = UpdateAttendeeList(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID, projectScheduleId, false);

                    //jcl 4/21/22 update attendees for detail records
                    UpsertFMADetail(ActionType.Update);
                }

                int internalNoteId = SaveInternalNotes();

                //enter audit for auto schedule Y/N
                //This will be added later just for facilitator meetings jcl 3/15/2022
                //please leave stubbed in for later backlog ticket
                //ProjectAuditModelBO.InsertAutoScheduledAudit(_fma.ProjectID.Value, _fma.UpdatedUser.ID.ToString(), _fma.AutoScheduled ? "Y" : "N");

                //This will be added later just for facilitator meetings backlog item LES-4027
                //InsertProjectAudit();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in FMAAdapter Upsert - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return itemId;
        }

        public bool CancelMeetingById(CancelMeetingModel model)
        {
            // cancel the meeting
            FacilitatorMeetingAppointmentBO bo = new FacilitatorMeetingAppointmentBO();

            FacilitatorMeetingAppointmentBE be = bo.GetById(model.AppointmentId);

            be.UserId = model.UserId.ToString();
            be.ApptResponseStatusRefId = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Cancelled).ApptResponseStatusRefId;
            bo.Update(be);

            // get the appointment
            _fma = new FacilitatorMeetingApptModelBO().GetInstance(model.AppointmentId);

            int projectId = _fma.ProjectID.Value;

            SetData();

            CancelAppointment();

            // update project notes
            NotesBO notesBO = new NotesBO();
            NotesBE notesBE = new NotesBE();
            NoteTypeModelBO noteTypeModelBO = new NoteTypeModelBO();
            string userId = model.UserId.ToString();

            notesBE.ProjectId = projectId;
            notesBE.NotesComment = model.Notes;
            notesBE.NotesTypeRefId = noteTypeModelBO.GetInstance(NoteTypeEnum.SchedulingNotes).ID;
            notesBE.UserId = userId;
            notesBE.BusinessRefID = -1;
            notesBE.CreatedByWkrId = userId;
            notesBE.CreatedDate = DateTime.Now;
            notesBE.UpdatedByWkrId = userId;
            notesBE.UpdatedDate = DateTime.Now;
            notesBO.Create(notesBE);

            //LES-3915 jcl record cancellation
            new ProjectAuditModelBO().InsertProjectAudit(projectId, AuditActionEnum.Meeting_Cancelled.ToStringValue(), userId, AuditActionEnum.Meeting_Cancelled);

            return true;
        }

        #region Private Methods
        private void SetData()
        {
            if (_fma.ApptResponseStatusEnum > 0)
            {
                AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(_fma.ApptResponseStatusEnum);
                _fma.ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId;
            }

            _Appointment = _fma;

            SetAppointmentData();
        }

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
                CancelAfterDt = fma.FromDt.HasValue ? DateTimeHelper.DetermineWorkDateBeforeDateSpecified(fma.FromDt.Value, 2) : (DateTime?)null,
                VirtualMeetingInd = fma.VirtualMeetingInd.HasValue ? fma.VirtualMeetingInd.Value : false,
                ApptCancellationRefId = fma.ApptResponseStatusEnum != AppointmentResponseStatusEnum.Cancelled ? null : fma.ApptCancellationRefId
            };

            return be;
        }

        private enum ActionType { Insert, Update };

        private bool UpsertFMADetail(ActionType actionType)
        {
            FacilitatorMeetingApptDetailBO facilitatorMeetingApptDetailBO = new FacilitatorMeetingApptDetailBO();
            //_Appointment.ID
            //_Appointment.NewAttendees, _Appointment.UpdatedUser.ID
            if (actionType == ActionType.Update)
            {
                //delete the existing records
                facilitatorMeetingApptDetailBO.DeleteByFMAId(_Appointment.ID);

            }
            foreach (AttendeeInfo attendee in _Appointment.AssignedReviewers)
            {
                if (attendee.AttendeeId > 0)
                {
                    FacilitatorMeetingApptDetailBE be = new FacilitatorMeetingApptDetailBE
                    {
                        AssignedPlanReviewerId = attendee.AttendeeId,
                        BusinessRefId = attendee.BusinessRefId,
                        FacilitatorMeetingApptId = _Appointment.ID,
                        UserId = _Appointment.UpdatedUser.ID.ToString()
                    };
                    facilitatorMeetingApptDetailBO.Create(be);
                }
            }
            return true;
        }
        #endregion
    }
}