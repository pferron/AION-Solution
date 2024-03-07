using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.BL.Adapters
{
    public class NPAAdapter : AppointmentAdapter, IAppointmentAdapter, INPAAdapter
    {
        private NonProjectAppointment _npa;

        public NPAAdapter() { }

        public NPAAdapter(NonProjectAppointment npa)
        {
            _npa = npa;

            SetData();
        }

        public NPAModel GetNPAModel()
        {
            try
            {
                NPAModel model = new NPAModel();

                IUserAdapter userAdapter = new UserAdapter();
                INPATypeAdapter npaTypeAdapter = new NPATypeAdapter();
                INPAAccessor npaAccessor = new NPAAccessor();
                IMeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
                IHolidayConfigAdapter holidayConfigAdapter = new HolidayConfigAdapter();

                model.Reviewers = userAdapter.GetAllReviewers(false, true);
                model.NpaTypes = npaTypeAdapter.GetAll();
                var endingSoonResults = npaAccessor.SearchNPAs_v2(0, 0, string.Empty, DateTime.Now, DateTime.Now.AddMonths(1));
                //The ending soon results should only consist of NPAs that are recurring 
                model.EndingSoonResults = endingSoonResults.Where(x => x.IsRecurring == "Y").ToList();
                model.MeetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "PRELIM_MEETING_ROOMS");
                model.Holidays = holidayConfigAdapter.GetHolidayConfigList();

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetNPAModel - " + ex.Message;

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
                NonProjectAppoinmentBO nonProjectAppointmentBO = new NonProjectAppoinmentBO();

                NonProjectAppoinmentBE be = ConvertNPAToBE(_npa);

                if (_npa.NonProjectAppointmentID == 0)
                {
                    List<ScheduleTime> recurrdates =
                        DateTimeHelper.GetReccuringDates(_npa.AppointmentFrom.Value, _npa.AppointmentTo.Value, GetAppointmentRecurrenceRefEnum(be.AppoinmentRecurrenceRefID.Value));

                    if (recurrdates.Any())
                    {
                        itemId = nonProjectAppointmentBO.Create(be);

                        _Appointment.ID = itemId;
                        _Appointment.RecurringDates = recurrdates;

                        SetAppointmentData();

                        if (itemId == 0) { throw new Exception("Insert Non Project Appointment error"); }
                    }
                    else
                    {
                        itemId = -1;
                    }
                }
                else
                {
                    if (_Appointment.IsReschedule && _Appointment.IsSubmit)
                    {
                        //get previous scheduled dates
                        NonProjectAppoinmentBE prevAppointment = nonProjectAppointmentBO.GetById(_npa.NonProjectAppointmentID.Value);

                        //set previous dates for cancellation email
                        _Appointment.PrevStartDate = prevAppointment.AppointmentFrom.Value;
                        _Appointment.PrevEndDate = prevAppointment.AppointmentTo.Value;
                    }

                    //update npa
                    int ret = nonProjectAppointmentBO.Update(be);
                    if (ret == 0) { throw new Exception("Update Non Project Appointment error"); }
                    itemId = _npa.NonProjectAppointmentID.Value;

                    int projectScheduleId = UpdateProjectSchedule();

                    SetAppointmentData();

                    bool success = UpdateAttendeeList(_Appointment.NewAttendees, _Appointment.UpdatedUser.ID, projectScheduleId, false);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpsertNonProjectAppointment - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return itemId;
        }

        public bool RearrangeTimeslots(int userid, DateTime fromDt, DateTime toDt)
        {
            if (fromDt < DateTime.Now.AddDays(-720)) //no days before 2 yr
                return false;
            if (toDt > DateTime.Now.AddDays(3600)) //no days after 10 yrs.
                return false;
            UserScheduleBO userScheduleBO = new UserScheduleBO();
            double deletedMins = (toDt - fromDt).TotalMinutes;
            //look for any projects which went around the specifc time and then move them forward to the freed up slots.
            List<TimeSlot> toAdd = new List<TimeSlot>();
            UserAdapter uadpt = new UserAdapter();
            List<TimeSlot> timeSlots = uadpt.GetUsedTimeSlotsExtrasByUserID(userid, fromDt.AddDays(-1), toDt.AddDays(1));
            var before = timeSlots.Where(x => x.EndTime <= fromDt && x.ProjectScheduleTypeName == "PR");
            var after = timeSlots.Where(x => x.EndTime >= toDt && x.ProjectScheduleTypeName == "PR");
            //if a specific slot is found then it is an overlapping one
            var overlapped = before.Where(x => after.Any(y => y.ProjectID == x.ProjectID)).FirstOrDefault();
            if (overlapped != null)
            {
                //get all timeslots for the project found and then filter the timeslot to extract only the overlapped project.
                List<TimeSlot> curPrjTimeSlots = uadpt.GetUsedTimeSlotsExtrasByUserID(userid, fromDt.AddDays(-7), toDt.AddDays(14)).Where(x => x.ProjectID == overlapped.ProjectID).ToList();
                var orderedslots = curPrjTimeSlots.OrderByDescending(x => x.EndTime); //order so that the first slot taken out will be from the last day.
                //plan review schedule id is needed to save the FK ref to table. So get it now.
                ProjectScheduleBO prjScheduleBo = new ProjectScheduleBO();
                List<ProjectScheduleBE> prjSchdules = prjScheduleBo.GetByApptId(curPrjTimeSlots[0].ProjectScheduleID, "PR");
                int prjSchduleId = prjSchdules[0].ProjectScheduleID.Value;
                List<int> toDel = new List<int>();
                double leftOverMins = deletedMins;
                DateTime CurrentTime = fromDt;
                foreach (var item in orderedslots)
                {
                    if (leftOverMins > 0)
                    {
                        double curMins = (item.EndTime - item.StartTime).TotalMinutes;
                        if (curMins < leftOverMins) //whole slot can be taken off and moved to front.
                        {
                            toDel.Add(item.UserScheduleID);
                            TimeSlot slot = item;
                            slot.StartTime = CurrentTime;
                            slot.EndTime = CurrentTime.AddMinutes(curMins);
                            toAdd.Add(slot);
                            CurrentTime = slot.EndTime;
                        }
                        else //this timeslot need to be split into 2 and part of it need to be moved to front.
                        {
                            toDel.Add(item.UserScheduleID);
                            TimeSlot slot = new TimeSlot();
                            slot.AllocationType = item.AllocationType;
                            slot.DepartmentName = item.DepartmentName;
                            slot.ProjectCategory = item.ProjectCategory;
                            slot.ProjectID = item.ProjectID;
                            slot.ProjectScheduleID = item.ProjectScheduleID;
                            slot.ProjectScheduleTypeName = item.ProjectScheduleTypeName;
                            slot.TotalTimeOfDay = item.TotalTimeOfDay;
                            slot.TotalTimeOfProject = item.TotalTimeOfProject;
                            slot.UserID = item.UserID;
                            slot.StartTime = CurrentTime;
                            slot.EndTime = CurrentTime.AddMinutes(leftOverMins);
                            toAdd.Add(slot);
                            //the original item need to abe added back to table with reduced time.
                            item.EndTime = item.EndTime.AddMinutes(leftOverMins * -1);
                            toAdd.Add(item);
                            //since all the time is moved front exit loop.
                            leftOverMins = 0;
                            break;
                        }
                        leftOverMins -= curMins;
                    }
                }

                //convert to DB obj and then flatten
                List<UserScheduleBE> dbLst = new List<UserScheduleBE>();
                foreach (var item in toAdd)
                {
                    UserScheduleBE userScheduleBE = new UserScheduleBE
                    {
                        ProjectScheduleID = prjSchduleId,
                        StartDateTime = item.StartTime,
                        EndDateTime = item.EndTime,
                        BusinessRefID = (int)item.DepartmentName,
                        UserID = item.UserID,
                        UserId = item.UserID.ToString()
                    };
                    dbLst.Add(userScheduleBE);
                }
                foreach (var item in toDel)
                {
                    userScheduleBO.Delete(item);
                }
                dbLst = SchedulingHelper.FlattenTimeSlots(dbLst);
                foreach (var item in dbLst)
                {
                    userScheduleBO.Create(item);
                }
            }
            return true;
        }

        #region Private Methods

        private void SetData()
        {
            if (_npa.ApptResponseStatusEnum > 0)
            {
                AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(_npa.ApptResponseStatusEnum);
                _npa.ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId;
            }

            _Appointment = _npa;

            if (_npa.AppoinmentRecurrenceRefID.HasValue)
            {
                _Appointment.RecurringDates =
                        DateTimeHelper.GetReccuringDates(_npa.AppointmentFrom.Value, _npa.AppointmentTo.Value, GetAppointmentRecurrenceRefEnum(_npa.AppoinmentRecurrenceRefID.Value));
            }

            SetAppointmentData();
        }

        private NonProjectAppoinmentBE ConvertNPAToBE(NonProjectAppointment Npa)
        {
            int appointmentrecurrencerefid = (int)new AppoinmentReccuranceRefBO().GetByEnumId((int)Npa.AppointmentRecurrence).AppoinmentReccuranceID;
            NonProjectAppoinmentBE be = new NonProjectAppoinmentBE
            {
                AppoinmentName = Npa.AppoinmentName,
                AppoinmentRecurrenceRefID = appointmentrecurrencerefid,
                AppointmentFrom = Npa.AppointmentFrom,
                AppointmentTo = Npa.AppointmentTo,
                CreatedByWkrId = Npa.CreatedUser.ID.ToString(),
                CreatedDate = Npa.CreatedDate,
                UpdatedByWkrId = Npa.UpdatedUser.ID.ToString(),
                IsAllBackFlow = Npa.IsAllBackFlow,
                IsAllBuild = Npa.IsAllBuild,
                IsActive = true,
                IsAllDay = Npa.IsAllDay,
                IsAllEhsDayCare = Npa.IsAllEhsDayCare,
                IsAllEhsFood = Npa.IsAllEhsFood,
                IsAllEhsLodge = Npa.IsAllEhsLodge,
                IsAllEhsPool = Npa.IsAllEhsPool,
                IsAllElectric = Npa.IsAllElectric,
                IsAllFire = Npa.IsAllFire,
                IsAllMech = Npa.IsAllMech,
                IsAllPlanReviewers = Npa.IsAllPlanReviewers,
                IsAllPlumb = Npa.IsAllPlumb,
                IsAllZoning = Npa.IsAllZoning,
                MeetingRoomRefID = Npa.MeetingRoomRefId,
                NonProjectAppointmentID = Npa.NonProjectAppointmentID,
                NPATypeRefID = Npa.NPATypeRefID,
                UserId = Npa.UpdatedUser.ID.ToString()

            };

            return be;
        }

        private AppointmentRecurrenceRefEnum GetAppointmentRecurrenceRefEnum(int appoinmentRecurrenceRefID)
        {
            AppoinmentReccuranceRefBO bo = new AppoinmentReccuranceRefBO();
            List<AppoinmentReccuranceRefBE> be = bo.GetList(appoinmentRecurrenceRefID);
            int enumId = be.Where(x => x.AppoinmentReccuranceID.Value == appoinmentRecurrenceRefID).FirstOrDefault().EnumMappingValNbr.Value;
            //TBD - get values from DB.
            return (AppointmentRecurrenceRefEnum)enumId;
        }

        #endregion Private Methods
    }
}
