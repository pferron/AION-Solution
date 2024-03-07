using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.Common;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.BL.Adapters
{
    public class ExpressAdapter : AppointmentAdapter, IAppointmentAdapter, IExpressAdapter
    {
        private ReserveExpressReservation _exp;

        private List<DateTime> _holidays = new List<DateTime>();
        public ExpressAdapter() { }

        public ExpressAdapter(int id)
        {
            _exp = new ReserveExpressReservationModelBO().GetInstance(id);

            SetData();
        }

        public ExpressAdapter(ReserveExpressReservation exp)
        {
            _exp = exp;

            SetData();
        }

        public ExpressAdapter(List<DateTime> configuredHolidays)
        {
            _holidays = configuredHolidays;
        }

        public ExpressModel GetExpressModel()
        {
            try
            {
                ExpressModel model = new ExpressModel();

                IUserAdapter userAdapter = new UserAdapter();
                IMeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
                ISchedulerAdapter schedulerAdapter = new SchedulerAdapter();

                model.Reviewers = userAdapter.GetReviewers((int)PropertyTypeEnums.Express, (int)DepartmentNameEnums.NA, true);
                model.MeetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "EXPRESS_MEETING_ROOMS");
                model.ConfigureReserveExpressDays = schedulerAdapter.GetConfigureReserveExpressList();
                model.ReserveExpressPlanReviewers = GetReserveExpressPlanReviewerListAll();
                model.ReserveExpressSearchResults = GetReservationByDate(DateTime.Now, DateTime.Now.AddMonths(1));
                model.ScheduledExpressSearchResults = GetScheduledByDate(DateTime.Now, DateTime.Now.AddMonths(1));

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetExpressModel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public int Upsert()
        {
            int itemId = 0;

            try
            {
                ReserveExpressReservationBO reserveExpressReserverationBO = new ReserveExpressReservationBO();
                ReserveExpressReservationBE reserveExpressReservationBE = new ReserveExpressReservationBE();

                string dt = _exp.ReserveExpressDt.ToShortDateString();
                string tmStart = _exp.StartTime.ToShortTimeString();
                string tmEnd = _exp.EndTime.ToShortTimeString();

                DateTime startTime = DateTime.Parse(dt + ' ' + tmStart);
                DateTime endTime = DateTime.Parse(dt + ' ' + tmEnd);

                _Appointment.StartDate = startTime;
                _Appointment.EndDate = endTime;

                reserveExpressReservationBE.ReserveExpressDt = _exp.ReserveExpressDt;
                reserveExpressReservationBE.StartTime = startTime;
                reserveExpressReservationBE.EndTime = endTime;
                reserveExpressReservationBE.MeetingRoomRefId = _exp.MeetingRoomRefId;
                reserveExpressReservationBE.ApptResponseStatusRefId = _exp.ApptResponseStatusRefId;
                reserveExpressReservationBE.CancelAfterDt = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(reserveExpressReservationBE.ReserveExpressDt, 5);

                if (_exp.ReserveExpressReservationId == 0)
                {
                    //insert reservation
                    reserveExpressReservationBE.UserId = "1";
                    itemId = reserveExpressReserverationBO.Create(reserveExpressReservationBE);

                    _Appointment.ID = itemId;

                    if (itemId == 0) { throw new Exception("Insert Facilitator Meeting Appointment error"); }

                    //insert attendees
                    bool success = InsertAttendees(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID);
                }
                else
                {
                    if (_Appointment.IsReschedule && _Appointment.IsSubmit)
                    {
                        //get previous scheduled dates
                        ReserveExpressReservationBE prevAppointment = reserveExpressReserverationBO.GetById(_exp.ReserveExpressReservationId);

                        string dtPrev = prevAppointment.ReserveExpressDt.ToShortDateString();
                        string tmStartPrev = prevAppointment.StartTime.ToShortTimeString();
                        string tmEndPrev = prevAppointment.EndTime.ToShortTimeString();

                        //set previous dates for cancellation email
                        _Appointment.PrevStartDate = DateTime.Parse(dtPrev + ' ' + tmStartPrev);
                        _Appointment.PrevEndDate = DateTime.Parse(dtPrev + ' ' + tmEndPrev);
                    }

                    //update fma
                    int ret = reserveExpressReserverationBO.Update(reserveExpressReservationBE);
                    if (ret == 0) { throw new Exception("Update Reserve Express Reservation error"); }
                    itemId = _exp.ReserveExpressReservationId;

                    int projectScheduleId = UpdateProjectSchedule();

                    SetAppointmentData();

                    bool success = UpdateAttendeeList(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID, projectScheduleId, false);
                }

                int internalNoteId = SaveInternalNotes();

                InsertProjectAudit();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in ExpressAdapter Upsert - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return itemId;
        }


        public bool ExpressReservations()
        {
            ReserveExpressReservationBO reservationBO = new ReserveExpressReservationBO();
            int expressReservationCount = reservationBO.GetExpressReservationCount();
            if (expressReservationCount > 0)
            {
                After6Months();
            }
            else
            {
                Initial6Months();
            }
            return true;
        }

        public List<ReserveExpressPlanReviewerBE> GetPlanReviewerList()
        {
            ReserveExpressPlanReviewerBO planReviewerBO = new ReserveExpressPlanReviewerBO();

            List<ReserveExpressPlanReviewerBE> planReviewerBEList = planReviewerBO.GetListAll().OrderBy(x => x.BusinessRefId).ThenBy(x => x.RotationNbr).ToList();

            return planReviewerBEList;
        }

        public bool UpdateReserveExpressReviewerRotation()
        {
            UserAdapter userAdapter = new UserAdapter();

            List<Reviewer> eligibleExpressReviewers = userAdapter.GetAllReviewers(true, true);

            List<ReserveExpressPlanReviewerBE> existingReserveExpressReviewers = GetPlanReviewerList();

            existingReserveExpressReviewers = UpdateExpressReviewers(eligibleExpressReviewers, existingReserveExpressReviewers);

            SchedulerAdapter schedulerAdapter = new SchedulerAdapter();
            schedulerAdapter.DeleteExpressPlanReviewerRotation();
            schedulerAdapter.SaveExpressPlanReviewerRotation(existingReserveExpressReviewers);

            return true;
        }

        public List<ReserveExpressPlanReviewerBE> UpdateExpressReviewers(
            List<Reviewer> eligibleExpressReviewers, 
            List<ReserveExpressPlanReviewerBE> existingReserveExpressReviewers)
        {
            for (int i = 0; i < existingReserveExpressReviewers.Count; i++)
            {
                int userId = existingReserveExpressReviewers[i].PlanReviewerId.Value;
                int businessRefId = existingReserveExpressReviewers[i].BusinessRefId.Value;

                Reviewer eligibleReviewer = eligibleExpressReviewers.FirstOrDefault(x => x.ID == userId);

                if (eligibleReviewer != null && eligibleReviewer.DesignatedDepartments.Any(x => x.ID == businessRefId))
                {
                    continue;
                }
                else
                {
                    existingReserveExpressReviewers.RemoveAt(i);
                }
            }

            //update remaining reviewer rotation

            int idx = 0;

            int prevBusinessRefId = 0;

            foreach (ReserveExpressPlanReviewerBE reviewer in existingReserveExpressReviewers.OrderBy(x => x.BusinessRefId).ThenBy(x => x.RotationNbr))
            {
                int businessRefId = reviewer.BusinessRefId.Value;

                if (businessRefId == prevBusinessRefId || prevBusinessRefId == 0)
                {
                    reviewer.RotationNbr = idx += 1;
                }
                else
                {
                    reviewer.RotationNbr = 1;
                    idx = 1;
                }

                prevBusinessRefId = reviewer.BusinessRefId.Value;
            }

            return existingReserveExpressReviewers;
        }

        public List<ExpressSearchResult> GetReservationByDate(DateTime fromdate, DateTime todate)
        {
            List<ExpressSearchResult> ret = new List<ExpressSearchResult>();
            try
            {
                ReserveExpressReservationBO bo = new ReserveExpressReservationBO();

                List<ReserveExpressPlanReviewerSearchResultBE> belst = bo.GetReservationByDateV2(fromdate, todate);
                foreach (var item in belst)
                {
                    var expressId = item.ReserveExpressReservationId.Value;

                    _exp = new ReserveExpressReservationModelBO().GetInstance(expressId);

                    SetData();

                    List<ProjectScheduleBE> schedulelst = GetProjectScheduleByAppointmentId(expressId);
                    List<ProjectSchedule> projectscheduleList = new List<ProjectSchedule>();

                    foreach (ProjectScheduleBE sche in schedulelst)
                    {
                        if (sche.ProjectScheduleID.HasValue)
                        {
                            projectscheduleList.Add(new ProjectSchedule
                            {
                                ProjectScheduleID = (sche.ProjectScheduleID.HasValue) ? sche.ProjectScheduleID.Value : 0,
                                ProjectScheduleTypeRef = sche.ProjectScheduleTypeRef,
                                AppointmentID = sche.AppoinmentID.Value,
                                RecurringApptDt = (sche.RecurringApptDt.HasValue) ? sche.RecurringApptDt.Value : (DateTime?)null
                            });
                        }
                    }

                    ret.Add(new ExpressSearchResult()
                    {
                        ExpressId = _Appointment.ID,
                        ExpressDate = _Appointment.StartDate.Value.ToString("MM/dd/yyyy"),
                        Schedules = projectscheduleList.Where(s => s.RecurringApptDt >= fromdate).Where(s => s.RecurringApptDt <= todate).ToList(),
                        Day = _exp.ReserveExpressDt.DayOfWeek.ToString(),
                        MeetingRoomRefId = _Appointment.MeetingRoomRefId.Value,
                        StartTime = _Appointment.StartDate.HasValue ? _Appointment.StartDate.Value.TimeOfDay : item.AppointmentStartTime.Value.TimeOfDay,
                        EndTime = _Appointment.EndDate.HasValue ? _Appointment.EndDate.Value.TimeOfDay : item.AppointmentEndTime.Value.TimeOfDay,
                        Time = _exp.StartTime.ToString("hh:mm tt") + " - " + _exp.EndTime.ToString("hh:mm tt"),
                        MeetingRoomName = _Appointment.MeetingRoom.MeetingRoomName,
                        Attendees = _Appointment.Attendees,
                        ProjectScheduleID = _Appointment.ProjectSchedule.ProjectScheduleID
                    });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetReservationByDate - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return ret;
        }

        public List<ExpressSearchResult> GetScheduledByDate(DateTime fromdate, DateTime todate)
        {
            List<ExpressSearchResult> ret = new List<ExpressSearchResult>();
            try
            {
                IEMAAccessor emaAccessor = new EMAAccessor();

                ret = emaAccessor.GetScheduledByDate(fromdate, todate);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error EMAAdapter GetScheduledByDate - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return ret;
        }

        public bool CheckIfDateIsHoliday(DateTime date)
        {
            if (_holidays == null || _holidays.Count() == 0)
            {
                GetAllHolidaysAndBlockedExpressDates();
            }
            bool isHoliday = _holidays.Any(d => d.Month == date.Month && d.Day == date.Day);
            return isHoliday;
        }

        public bool InsertUserSchedule(int projectScheduleId, ReserveExpressReservationBE reservation, ReserveExpressPlanReviewerBE planReviewerBE)
        {
            bool success = false;
            try
            {
                UserScheduleBO userScheduleBO = new UserScheduleBO();

                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                DateTime startDateTime = reservation.ReserveExpressDt.Date.Add(reservation.StartTime.TimeOfDay);
                DateTime endDateTime = reservation.ReserveExpressDt.Date.Add(reservation.EndTime.TimeOfDay);

                UserScheduleBE userScheduleBE = new UserScheduleBE
                {
                    ProjectScheduleID = projectScheduleId,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    BusinessRefID = planReviewerBE.BusinessRefId,
                    UserID = planReviewerBE.PlanReviewerId
                };

                userScheduleBO.Create(userScheduleBE);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertUserSchedule - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool UpdateAttendeeList(List<AttendeeInfo> attendeeIds, int expressId, int wkrId, int projectScheduleId, bool isSchedule, bool processInsertRemoveOnly)
        {
            bool success = false;
            try
            {
                UserIdentity user = new UserIdentityModelBO().GetInstance(wkrId);

                if (!isSchedule)
                {
                    _exp = new ReserveExpressReservationModelBO().GetInstance(expressId);

                    _exp.UpdatedUser = user;

                    SetData();

                    success = UpdateAttendeeList(attendeeIds, wkrId, projectScheduleId, processInsertRemoveOnly);
                }
                else
                {
                    PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();

                    success = planReviewAdapter.UpdateAttendeeSchedules(projectScheduleId, attendeeIds, wkrId);
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateAttendeeList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool CancelReservations(List<ApptAttendeesManagerModel> cancellations)
        {
            bool success = false;
            try
            {
                foreach (var cancellation in cancellations)
                {
                    var cancellationExpressId = cancellation.ApptId;
                    var cancellationProjectScheduleId = cancellation.ProjectScheduleID;
                    var cancellationWkrId = cancellation.WkrId;
                    bool isSchedule = cancellation.IsSchedule;

                    if (!isSchedule)
                    {
                        ReserveExpressReservationModelBO modelBO = new ReserveExpressReservationModelBO();
                        _exp = modelBO.GetInstance(cancellationExpressId);
                        _exp.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Cancelled;

                        SetData();

                        ReserveExpressReservationBO bo = new ReserveExpressReservationBO();
                        ReserveExpressReservationBE be = bo.GetById(cancellationExpressId);
                        be.ApptResponseStatusRefId = _exp.ApptResponseStatusRefId;
                        bo.UpdateStatus(be);
                    }

                    success = CancelAppointment();

                    return success;
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CancelReservationsForExpress - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool UpdateReservation(List<ReserveExpressReservation> reservations)
        {
            try
            {
                ReserveExpressReservationBE be = new ReserveExpressReservationBE();
                ReserveExpressReservationBO bo = new ReserveExpressReservationBO();

                foreach (var reservation in reservations)
                {
                    be.ReserveExpressReservationId = reservation.ReserveExpressReservationId;
                    be.MeetingRoomRefId = reservation.MeetingRoomRefId;
                    bo.Update(be);
                }
            }

            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateReservation - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return true;
        }

        public List<ReserveExpressReservation> GetExpressReservationList()
        {
            List<ReserveExpressReservation> ret = new List<ReserveExpressReservation>();
            ReserveExpressReservationBO bo = new ReserveExpressReservationBO();
            List<ReserveExpressReservationBE> beList = bo.GetList();
            foreach (var item in beList)
            {
                ReserveExpressReservation reser = new ReserveExpressReservation();
                reser.ReserveExpressReservationId = item.ReserveExpressReservationId;
                reser.ReserveExpressDt = item.ReserveExpressDt;
                reser.StartTime = item.StartTime;
                reser.EndTime = item.EndTime;
                reser.MeetingRoomRefId = item.MeetingRoomRefId;

                ret.Add(reser);
            }
            return ret;
        }

        public bool InsertExpressAttendees(List<AttendeeInfo> attendeeIds, int reserveId, int wkrId)
        {
            bool success = false;
            try
            {
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                ReserveExpressReservationBE rsr = new ReserveExpressReservationBO().GetById(reserveId);
                ReserveExpressReservation expressobj = new ReserveExpressReservationModelBO().GetInstance(reserveId);

                List<string> RequestedParticipantEmailList = new List<string>();

                foreach (AttendeeInfo userid in attendeeIds)
                {
                    UserIdentity identity = userIdentityModelBO.GetInstance(userid.AttendeeId);

                    if (RegexUtilities.IsValidEmail(identity.Email))
                        RequestedParticipantEmailList.Add(identity.Email);
                }

                MeetingAllocationRequest request = new MeetingAllocationRequest
                {
                    RequestedEndTime = rsr.ReserveExpressDt + rsr.EndTime.TimeOfDay,
                    RequestedStartTime = rsr.ReserveExpressDt + rsr.StartTime.TimeOfDay,
                    RequestedParticipantEmailList = RequestedParticipantEmailList
                };
                bool reserveMeetingRoom = new MeetingRoomAdapter().ReserveMeetingRoom("Manual Express Reservation", rsr.MeetingRoomRefId.Value, request);

                _exp = expressobj;

                _exp.UpdatedUser = userIdentityModelBO.GetInstance(wkrId);

                SetData();

                InsertAttendees(attendeeIds, wkrId);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertExpressAttendees - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public List<ReserveExpressPlanReviewer> GetReserveExpressPlanReviewerListAll()
        {
            var ret = new List<ReserveExpressPlanReviewer>();
            ReserveExpressPlanReviewerBO bo = new ReserveExpressPlanReviewerBO();
            List<ReserveExpressPlanReviewerBE> beList = bo.GetListAll();

            foreach (var item in beList)
            {
                ret.Add(ConvertBeToReserveExpressPlanReviewer(item));
            }
            return ret;
        }


        #region private methods
        private ReserveExpressPlanReviewer ConvertBeToReserveExpressPlanReviewer(ReserveExpressPlanReviewerBE item)
        {
            ReserveExpressPlanReviewer reviewerRotation = new ReserveExpressPlanReviewer();
            reviewerRotation.Id = item.ReserveExpressPlanReviewerId.Value;
            reviewerRotation.BusinessRefId = item.BusinessRefId.Value;
            reviewerRotation.PlanReviewerId = item.PlanReviewerId.Value;
            reviewerRotation.RotationNbr = item.RotationNbr.Value;
            reviewerRotation.DeptNameEnumId = item.BusinessRefId.Value;
            reviewerRotation.FirstName = item.FirstName;
            reviewerRotation.LastName = item.LastName;
            return reviewerRotation;
        }
        private void SetData()
        {
            if (_exp.ApptResponseStatusEnum > 0)
            {
                AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(_exp.ApptResponseStatusEnum);
                _exp.ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId;
            }

            _Appointment = _exp;

            SetAppointmentData();
        }

        private void Initial6Months()
        {
            var numberofMonths = 6;
            for (int i = 0; i < numberofMonths; i++)
            {
                var month = DateTime.Now;
                if (i == 0)
                {
                    month = DateTime.Now;

                }
                else
                {
                    month = DateTime.Now.AddMonths(i);

                }
                AddReservation(month);
            }
        }

        private void After6Months()
        {
            DateTime today = DateTime.Now;
            DateTime futureday = today.AddMonths(6);

            ProcessReservations(futureday);
        }

        private void AddReservation(DateTime month)
        {
            ProcessReservations(month);
        }

        private void ProcessReservations(DateTime month)
        {
            List<DateTime> allDatesInAMonth = new List<DateTime>();
            ConfigureReserveExpressBO bo = new ConfigureReserveExpressBO();
            List<ConfigureReserveExpressBE> beList = bo.GetList().Where(x => x.ActiveInd == true).ToList();

            DateTime dtStartDate = beList.Select(o => o.StartDate).FirstOrDefault().Value;
            DateTime dtEndDate = beList.Select(o => o.EndDate).FirstOrDefault().Value;

            foreach (var item in beList)
            {
                allDatesInAMonth.AddRange(GetAllDatesInAMonth(item, month));
            }

            List<DateTime> DatesInMonthOrder = allDatesInAMonth.OrderBy(n => n.Date).ToList();

            ReserveExpressReservationBO reservationBO = new ReserveExpressReservationBO();
            ReserveExpressReservationBE reservationBE = new ReserveExpressReservationBE();

            var noOfDates = DatesInMonthOrder.Count();

            var planReviewerList = GetPlanReviewerList();
            var businessRefIdList = planReviewerList.OrderBy(x => x.BusinessRefId).Select(x => x.BusinessRefId).Distinct();

            for (int dateIndex = 0; dateIndex < DatesInMonthOrder.Count(); dateIndex++)
            {
                DateTime date = DatesInMonthOrder[dateIndex];
                bool isInHolidayList = CheckIfDateIsHoliday(date);

                if (!isInHolidayList)
                {

                    MeetingRoom meetingRoom = GetExpressMeetingRoom(date, dtStartDate, dtEndDate);

                    reservationBE.MeetingRoomRefId = (meetingRoom != null) ? meetingRoom.MeetingRoomRefID.Value : (int?)null;
                    reservationBE.ReserveExpressDt = date;
                    reservationBE.StartTime = date.Add(dtStartDate.TimeOfDay);
                    reservationBE.EndTime = date.Add(dtEndDate.TimeOfDay);
                    reservationBE.CreatedByWkrId = "1";
                    AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Scheduled);
                    reservationBE.ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId;

                    reservationBE.CancelAfterDt = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(reservationBE.ReserveExpressDt, 5);

                    int reservationId = reservationBO.Create(reservationBE);
                    var reserveExpressReservation = new ReserveExpressReservationBO().GetById(reservationId);

                    ReserveExpressReservation expressReservation = new ReserveExpressReservationModelBO().GetInstance(reservationId);

                    _Appointment = expressReservation;

                    //create project schedule
                    ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();

                    var expressDate = DateTime.Parse(reserveExpressReservation.ReserveExpressDt.ToString());
                    var startTime = reserveExpressReservation.StartTime.TimeOfDay;
                    var endTime = reserveExpressReservation.EndTime.TimeOfDay;

                    ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE
                    {
                        AppoinmentID = reservationId,
                        ProjectScheduleTypeRef = _Appointment.ProjectScheduleRefEnum.ToString(),
                        UserId = reserveExpressReservation.UserId,
                        RecurringApptDt = expressDate + startTime
                    };

                    int projectScheduleId = projectScheduleBO.Create(projectScheduleBE);

                    int dateCountInList = dateIndex + 1;

                    List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

                    foreach (var businessRefId in businessRefIdList)
                    {
                        var planReviewerListByBusinessRefId = planReviewerList
                            .OrderBy(x => x.BusinessRefId).Where(x => x.BusinessRefId == businessRefId).ToList();

                        var noOfReviewers = planReviewerListByBusinessRefId.Count();

                        //**************************************************************************************
                        //
                        // Iterate through the correct rotation of reviewers for business unit
                        //
                        //**************************************************************************************
                        int idxRotationNumber = dateIndex;
                        if (dateCountInList > noOfReviewers)
                        {
                            idxRotationNumber = (dateCountInList % noOfReviewers);
                            if (idxRotationNumber == 0)
                            {
                                idxRotationNumber = noOfReviewers - 1;
                            }
                            else
                            {
                                idxRotationNumber--;
                            }
                        }

                        var planReviewer = planReviewerListByBusinessRefId[idxRotationNumber];

                        InsertUserSchedule(projectScheduleId, reserveExpressReservation, planReviewer);

                        UserIdentity user = new UserIdentityModelBO().GetInstance(planReviewer.PlanReviewerId.Value);

                        attendeeDetails.Add(new AttendeeDetails()
                        {
                            EmailId = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IsRequired = true,
                            UserId = user.ID
                        });
                    }

                    SetAppointmentData();

                    InsertAttendees(attendeeDetails, projectScheduleId);
                }
            }
        }

        private List<DateTime> GetAllDatesInAMonth(ConfigureReserveExpressBE item, DateTime month)
        {
            var dates = new List<DateTime>();

            if (item.ActiveInd == true)
            {
                dates = GetAllDayOfWeekPerMonth(month.Month, month.Year, (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.ReserveExpressDay));
            }

            return dates;
        }

        private static List<DateTime> GetAllDayOfWeekPerMonth(int month, int year, DayOfWeek dayOfWeek)
        {
            var date = new DateTime(year, month, 1);

            if (date.DayOfWeek != dayOfWeek)
            {
                int daysUntilDayOfWeek = ((int)dayOfWeek - (int)date.DayOfWeek + 7) % 7;
                date = date.AddDays(daysUntilDayOfWeek);
            }

            List<DateTime> days = new List<DateTime>();

            while (date.Month == month)
            {
                days.Add(date);
                date = date.AddDays(7);
            }

            return days;
        }

        private MeetingRoom GetExpressMeetingRoom(DateTime currentDate, DateTime startDate, DateTime endDate)
        {
            IMeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            List<MeetingRoom> meetingRooms = new List<MeetingRoom>();

            //NOTE: TEMPORARY FIX
            //var meetingRoomOrder = new List<string> { "Matthews", "Mint Hill", "Hoffman", "Woods" };
            var meetingRoomOrder = new List<string> { "Lue-Room 227", "Lue-Room 229B" };

            try
            {

                meetingRooms = outlookAdapter.GetAllAvailableMeetingRoomsForDateRange(currentDate, startDate, endDate, "EXPRESS_MEETING_ROOMS").OrderBy(item => meetingRoomOrder.IndexOf(item.MeetingRoomName)).ToList();
            }
            catch
            {
                meetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "EXPRESS_MEETING_ROOMS").OrderBy(item => meetingRoomOrder.IndexOf(item.MeetingRoomName)).ToList();
            }

            MeetingRoom meetingRoom = meetingRooms.First();
            return meetingRoom;
        }

        private UserScheduleBE CopyUserScheduleToNewObject(UserScheduleBE userSchedule)
        {
            return new UserScheduleBE()
            {
                BusinessRefID = userSchedule.BusinessRefID,
                CreatedByWkrId = userSchedule.CreatedByWkrId,
                CreatedDate = userSchedule.CreatedDate,
                IsActive = userSchedule.IsActive,
                ProjectScheduleID = userSchedule.ProjectScheduleID,
                UpdatedByWkrId = userSchedule.UpdatedByWkrId,
                UpdatedDate = userSchedule.UpdatedDate,
                UserID = userSchedule.UserID,
                UserId = userSchedule.UserId,
            };
        }

        private void GetAllHolidaysAndBlockedExpressDates()
        {
            IHolidayConfigAdapter holidayConfigAdapter = new HolidayConfigAdapter();

            _holidays = holidayConfigAdapter.GetHolidayConfigDates();
            _holidays.AddRange(DateTimeHelper.GetBlockedHolidayDatesForExpress(DateTime.Now.Year));
        }

        #endregion
    }
}