using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.Engine.BusinessEntities;
using AION.Manager.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class PMAAdapter : AppointmentAdapter, IAppointmentAdapter, IPMAAdapter
    {
        private PreliminaryMeetingAppointment _pma;

        public PMAAdapter(PreliminaryMeetingAppointment pma)
        {
            _pma = pma;

            SetData();
        }

        public int Upsert()
        {
            int itemId = 0;

            try
            {
                PreliminaryMeetingAppointmentBO preliminaryMeetingAppointmentBO = new PreliminaryMeetingAppointmentBO();
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();

                PreliminaryMeetingAppointmentBE be = ConvertPMAToBE(_pma);

                if (_pma.PreliminaryMeetingApptID == 0)
                {
                    //insert pma
                    be.UserId = "1";
                    itemId = preliminaryMeetingAppointmentBO.Create(be);

                    _Appointment.ID = itemId;

                    if (itemId == 0) { throw new Exception("Insert Preliminary Meeting Appointment error"); }

                    //insert attendees
                    bool success = InsertAttendees(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID);
                }
                else
                {
                    if (_Appointment.IsReschedule && _Appointment.IsSubmit)
                    {
                        //get previous scheduled dates
                        PreliminaryMeetingAppointmentBE prevAppointment = preliminaryMeetingAppointmentBO.GetById(_pma.PreliminaryMeetingApptID.Value);

                        //set previous dates for cancellation email
                        _Appointment.PrevStartDate = prevAppointment.FromDT.Value;
                        _Appointment.PrevEndDate = prevAppointment.ToDT.Value;
                    }

                    //update ema
                    int ret = preliminaryMeetingAppointmentBO.Update(be);
                    if (ret == 0) { throw new Exception("Update Preliminary Meeting Appointment error"); }
                    itemId = _pma.PreliminaryMeetingApptID.Value;

                    int projectScheduleId = UpdateProjectSchedule();

                    SetAppointmentData();

                    bool success = UpdateAttendeeList(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID, projectScheduleId, false);
                }

                UpdateProjectDept();

                int internalNoteId = SaveInternalNotes();

                //enter audit for auto schedule Y/N
                ProjectAuditModelBO.InsertAutoScheduledAudit(_pma.ProjectID.Value, _pma.UpdatedUser.ID.ToString(), _pma.AutoScheduled ? "Y" : "N");

                InsertProjectAudit();

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PMAAdapter Upsert - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return itemId;
        }

        #region Private Methods

        private void SetData()
        {
            if (_pma.ApptResponseStatusEnum > 0)
            {
                AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(_pma.ApptResponseStatusEnum);
                _pma.ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId;
            }

            _Appointment = _pma;

            SetAppointmentData();
        }

        private PreliminaryMeetingAppointmentBE ConvertPMAToBE(PreliminaryMeetingAppointment pma)
        {
            PreliminaryMeetingAppointmentBE be = new PreliminaryMeetingAppointmentBE
            {
                MeetingRoomRefID = pma.MeetingRoomRefId,
                FromDT = pma.FromDt,
                ToDT = pma.ToDt,
                PreliminaryMeetingApptID = pma.PreliminaryMeetingApptID,
                AppendixAgendaDueDt = pma.AppendixAgendaDueDt,
                ApptResponseStatusRefId = pma.ApptResponseStatusRefId,
                ProjectID = pma.ProjectID,
                UpdatedDate = pma.UpdatedDate,
                CreatedByWkrId = pma.CreatedUser.ID.ToString(),
                CreatedDate = pma.CreatedDate,
                UpdatedByWkrId = pma.UpdatedUser.ID.ToString(),
                IsActive = true,
                UserId = pma.UpdatedUser.ID.ToString(),
                CancelAfterDt = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 2),
                IsReschedule = pma.IsReschedule
            };

            return be;
        }

        #endregion Private Methods
    }
}