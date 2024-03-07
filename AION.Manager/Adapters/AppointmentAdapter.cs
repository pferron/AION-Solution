using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.BusinessObjects;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public abstract class AppointmentAdapter : BaseManagerAdapter, IAppointmentAdapter
    {
        protected Appointment _Appointment;

        private List<ScheduleCapacitySearch> _CapacitySearches = new List<ScheduleCapacitySearch>();

        public int SaveInternalNotes()
        {
            int id = 0;

            try
            {
                if (!String.IsNullOrWhiteSpace(_Appointment.InternalNotes))
                {
                    NoteAdapter noteAdapter = new NoteAdapter();

                    Note note = new Note
                    {
                        NotesType = new NoteTypeModelBO().GetInstance(NoteTypeEnum.InternalNotes),
                        NotesComments = _Appointment.InternalNotes,
                        CreatedUser = _Appointment.UpdatedUser,
                        UpdatedUser = _Appointment.UpdatedUser,
                        ProjectID = _Appointment.ProjectID.Value,
                        DeptNameEnum = DepartmentNameEnums.NA
                    };

                    id = noteAdapter.InsertProjectNote(note);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveInternalNotes AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return id;
        }

        public void InsertProjectAudit()
        {
            try
            {
                if (_Appointment.IsSubmit)
                {
                    _Appointment.PrevStartDate = null;
                    _Appointment.PrevEndDate = null;

                    new ProjectAuditModelBO().InsertProjectAudit(
                        _Appointment.NewAttendees,
                        _Appointment.ProjectID.Value,
                        _Appointment.StartDate.Value.ToShortDateString() + " " + _Appointment.StartDate.Value.ToShortTimeString() + " To " + _Appointment.EndDate.Value.ToShortTimeString(),
                        _Appointment.UpdatedUser.ID.ToString(),
                        _Appointment.ApptResponseStatusEnum);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertProjectAudit AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool InsertAttendees(List<AttendeeInfo> attendeeIds, int wkrId)
        {
            try
            {
                List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

                foreach (ScheduleTime scheduleTime in _Appointment.ScheduleTimes)
                {
                    int projectScheduleId = 0;

                    projectScheduleId = CreateProjectSchedule(wkrId, scheduleTime);

                    if (projectScheduleId > 0)
                    {
                        SetAppointmentProjectScheduleFromId(projectScheduleId);

                        attendeeDetails = CreateUserSchedulesOfAttendees(attendeeIds, projectScheduleId, scheduleTime, wkrId);

                        if (attendeeDetails.Count > 0)
                        {
                            SendAppointmentNotifications(attendeeDetails, false);

                            AdjustPlanReviewsForConflicts(attendeeIds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertAttendees AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool InsertAttendees(List<AttendeeInfo> attendeeIds, int wkrId, int projectScheduleId)
        {
            try
            {
                List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

                foreach (ScheduleTime scheduleTime in _Appointment.ScheduleTimes)
                {
                    if (projectScheduleId == 0)
                    {
                        projectScheduleId = CreateProjectSchedule(wkrId, scheduleTime);
                    }

                    if (projectScheduleId > 0)
                    {
                        attendeeDetails = CreateUserSchedulesOfAttendees(attendeeIds, projectScheduleId, scheduleTime, wkrId);

                        if (attendeeDetails.Count > 0)
                        {
                            SendAppointmentNotifications(attendeeDetails, false);

                            AdjustPlanReviewsForConflicts(attendeeIds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertAttendees AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool InsertAttendees(List<AttendeeDetails> attendeeDetails, int projectScheduleId)
        {
            try
            {
                if (projectScheduleId > 0)
                {
                    if (attendeeDetails.Count > 0)
                    {
                        SendAppointmentNotifications(attendeeDetails, false);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertAttendees AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool RemoveAttendees(List<AttendeeInfo> attendeeIds, int? projectScheduleId = null)
        {
            try
            {
                List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

                ProjectScheduleBE projectScheduleBE = GetProjectScheduleByScheduleDetails(projectScheduleId == null ? 0 : projectScheduleId.Value);

                if (projectScheduleBE.ProjectScheduleID == null)
                {
                    return true;
                }
                else
                {
                    projectScheduleId = projectScheduleBE.ProjectScheduleID.Value;
                }

                if (projectScheduleId != null && projectScheduleId > 0)
                {
                    SetAppointmentProjectScheduleFromId(projectScheduleId.Value);

                    attendeeDetails = GetAttendeesForRemovalByProjectSchedule((int)projectScheduleId, attendeeIds);

                    SendAppointmentNotifications(attendeeDetails, true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in RemoveAttendees AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return true;
        }

        public bool RemoveAttendees(int projectScheduleId)
        {
            try
            {
                SetAppointmentProjectScheduleFromId(projectScheduleId);

                List<AttendeeDetails> attendeeDetails = GetAttendeesForRemovalByProjectSchedule(projectScheduleId);

                //call even if 0 attendees, because calendar event still needs to be removed
                SendAppointmentNotifications(attendeeDetails, true);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in RemoveAttendees AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool UpdateAttendees(List<AttendeeInfo> attendeeIds, int wkrId, int projectScheduleId)
        {
            try
            {
                List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

                attendeeDetails = GetAttendeesForRemovalByProjectSchedule(projectScheduleId, attendeeIds);

                SendAppointmentNotifications(attendeeDetails, true);

                InsertAttendees(attendeeIds, wkrId, projectScheduleId);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateAttendees AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return true;
        }

        public bool UpdateAttendeeList(int wkrId)
        {
            bool success = false;

            try
            {
                success = UpdateAttendeeList(_Appointment.NewAttendees, wkrId, _Appointment.ProjectSchedule.ProjectScheduleID, false);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in AppointmentAdapter UpdateAttendeeList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool UpdateAttendeeList(List<AttendeeInfo> attendeeIds, int WkrId, int projectScheduleId, bool processInsertRemoveOnly)
        {
            bool success = false;
            try
            {
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //get the project schedule id
                //if none are returned, this is a new apptmnt
                if (projectScheduleId == 0)
                {
                    List<ProjectScheduleBE> projectScheduleBEs = new List<ProjectScheduleBE>();
                    projectScheduleBEs = projectScheduleBO.GetByApptId(_Appointment.ID, _Appointment.ProjectScheduleRefEnum.ToString(), null);
                    if (projectScheduleBEs != null && projectScheduleBEs.Count > 0)
                    {
                        projectScheduleId = projectScheduleBEs.FirstOrDefault().ProjectScheduleID.Value;
                    }
                }

                //get userschedulebe list by projectscheduleid
                List<UserScheduleBE> userschedules = new UserScheduleBO().GetListByScheduleID(projectScheduleId);

                //build the list of users who need to be inserted
                List<AttendeeInfo> inserted = new List<AttendeeInfo>();
                foreach (AttendeeInfo attendee in attendeeIds)
                {
                    int userid = attendee.AttendeeId;
                    attendee.BusinessRefId = attendee.BusinessRefId > 0 ? attendee.BusinessRefId : (int)DepartmentNameEnums.NA;

                    if (userschedules.Where(x => x.UserID == userid && x.BusinessRefID == attendee.BusinessRefId).Any() == false)
                    {
                        //insert the user because they aren't in the current users list
                        inserted.Add(attendee);
                    }
                }
                //build the list of users who need to be removed
                List<AttendeeInfo> removed = new List<AttendeeInfo>();
                foreach (UserScheduleBE user in userschedules)
                {
                    int userid = user.UserID.Value;
                    int businessrefid = user.BusinessRefID.HasValue && user.BusinessRefID.Value > 0 ? user.BusinessRefID.Value : (int)DepartmentNameEnums.NA;

                    if (attendeeIds.Where(x => x.AttendeeId == userid && x.BusinessRefId == businessrefid).Any() == false)
                    {
                        //remove user because they aren't in the attendee list
                        removed.Add(new AttendeeInfo { AttendeeId = userid, BusinessRefId = businessrefid, DeptNameEnumId = businessrefid });
                    }
                }
                //build the list of users who need to be updated
                List<AttendeeInfo> updated = new List<AttendeeInfo>();
                if (!processInsertRemoveOnly)
                {
                    foreach (UserScheduleBE user in userschedules)
                    {
                        int userid = user.UserID.Value;
                        int businessrefid = user.BusinessRefID.HasValue && user.BusinessRefID.Value > 0 ? user.BusinessRefID.Value : (int)DepartmentNameEnums.NA;

                        if (attendeeIds.Where(x => x.AttendeeId == userid && x.BusinessRefId == businessrefid).Any() == true)
                        {
                            //update user because they are being rescheduled to a different time
                            updated.Add(new AttendeeInfo { AttendeeId = userid, BusinessRefId = businessrefid, DeptNameEnumId = businessrefid });
                        }
                    }
                }

                //remove attendees - send appointments - must occur before the insert of the new
                bool removedsuccess = true;
                if (removed.Count() > 0)
                    removedsuccess = RemoveAttendees(removed, projectScheduleId);
                //insert attendees - send appointments
                bool insertedsuccess = true;
                if (inserted.Count() > 0)
                    insertedsuccess = InsertAttendees(inserted, WkrId, projectScheduleId);
                bool updatedsuccess = true;
                if (updated.Count() > 0)
                {
                    updatedsuccess = UpdateAttendees(updated, WkrId, projectScheduleId);
                }

                //these are initially set to true to capture failures
                if (!insertedsuccess || !removedsuccess || !updatedsuccess)
                {
                }
                else
                {
                    success = true;
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateAttendeeList AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool CancelAppointment()
        {
            try
            {
                List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

                SetAppointmentData();

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();

                List<ProjectScheduleBE> projectSchedules = projectScheduleBO.GetByApptId(_Appointment.ID, _Appointment.ProjectScheduleRefEnum.ToString());

                foreach (ProjectScheduleBE projectSchedule in projectSchedules)
                {
                    if (projectSchedule.ProjectScheduleID.HasValue)
                    {
                        attendeeDetails = GetAttendeesForRemovalByProjectSchedule(projectSchedule.ProjectScheduleID.Value);
                        SendAppointmentNotifications(attendeeDetails, true);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CancelAppointment AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public int UpdateProjectDept()
        {
            int ret = 0;
            try
            {
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                ExcludedPlanReviewersBO exbo = new ExcludedPlanReviewersBO();
                SchedulerAdapter schedulerAdapter = new SchedulerAdapter();

                ProjectEstimation pe = estimationCRUDAdapter.GetProjectDetailsByProjectId(_Appointment.ProjectID.Value);
                pe.UpdatedUser = _Appointment.UpdatedUser;

                //update project status if this PMA is scheduled
                bool updatestatus = _Appointment.ApptResponseStatusRefId == new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Scheduled).ApptResponseStatusRefId
                    || _Appointment.ApptResponseStatusRefId == new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Tentatively_Scheduled).ApptResponseStatusRefId;
                if (updatestatus)
                {
                    //set the project status to scheduled
                    pe.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Tentatively_Scheduled);
                }
                //update project facilitator
                bool updatefacilitator = pe.AssignedFacilitator.HasValue && pe.AssignedFacilitator.Value != int.Parse(_Appointment.AssignedFacilitator);
                if (updatefacilitator)
                {
                    //save the facilitator
                    pe.AssignedFacilitator = int.Parse(_Appointment.AssignedFacilitator);
                }
                //if any project updates, save the project
                if (updatestatus || updatefacilitator)
                    estimationCRUDAdapter.SaveProjectEstimationDetails(pe);

                //refresh Project data
                pe = estimationCRUDAdapter.GetProjectDetailsByProjectId(_Appointment.ProjectID.Value);

                bool savedtrades = false;
                foreach (ProjectTrade trade in pe.Trades)
                {
                    //assigned reviewer
                    AttendeeInfo scheduledReviewer = _Appointment.AssignedReviewers.Where(x => x.DeptNameEnumId == (int)trade.DepartmentInfo).FirstOrDefault();
                    UserIdentity assignedUser = userIdentityModelBO.GetInstance(scheduledReviewer.AttendeeId);
                    trade.AssignedPlanReviewer = assignedUser;
                    //

                    // primary, secondary reviewer //
                    AttendeeInfo primaryReviewer = _Appointment.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)trade.DepartmentInfo).FirstOrDefault();
                    if (primaryReviewer != null)
                    {
                        UserIdentity primaryUser = userIdentityModelBO.GetInstance(primaryReviewer.AttendeeId);
                        trade.PrimaryPlanReviewer = primaryUser;
                    }

                    AttendeeInfo secondaryReviewer = _Appointment.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)trade.DepartmentInfo).FirstOrDefault();
                    if (secondaryReviewer != null)
                    {
                        UserIdentity secondaryUser = userIdentityModelBO.GetInstance(secondaryReviewer.AttendeeId);
                        trade.SecondaryPlanReviewer = secondaryUser;
                    }

                    //
                    trade.UpdatedUser = _Appointment.UpdatedUser;
                    savedtrades = estimationCRUDAdapter.SaveProjectTrade(trade);

                    //excluded reviewers//
                    switch (trade.DepartmentInfo)
                    {
                        case DepartmentNameEnums.Building:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersBuild), _Appointment.UpdatedUser.ID);

                            break;
                        case DepartmentNameEnums.Electrical:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersElectric), _Appointment.UpdatedUser.ID);

                            break;
                        case DepartmentNameEnums.Mechanical:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersMech), _Appointment.UpdatedUser.ID);

                            break;
                        case DepartmentNameEnums.Plumbing:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersPlumb), _Appointment.UpdatedUser.ID);
                            break;
                        default:
                            break;
                    }
                }
                foreach (ProjectAgency agency in pe.Agencies)
                {
                    //for fire and zoning, look for the division
                    //for everything else just get the enum
                    switch (agency.DepartmentInfo)
                    {
                        case DepartmentNameEnums.EH_Day_Care:
                        case DepartmentNameEnums.EH_Food:
                        case DepartmentNameEnums.EH_Pool:
                        case DepartmentNameEnums.EH_Facilities:
                        case DepartmentNameEnums.Backflow:
                            //assigned reviewer
                            AttendeeInfo scheduledReviewer = _Appointment.AssignedReviewers.Where(x => x.DeptNameEnumId == (int)agency.DepartmentInfo).FirstOrDefault();
                            UserIdentity assignedUser = userIdentityModelBO.GetInstance(scheduledReviewer.AttendeeId);
                            agency.AssignedPlanReviewer = assignedUser;
                            //

                            // primary, secondary reviewer //
                            AttendeeInfo primaryReviewer = _Appointment.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)agency.DepartmentInfo).FirstOrDefault();
                            if (primaryReviewer != null)
                            {
                                UserIdentity primaryUser = userIdentityModelBO.GetInstance(primaryReviewer.AttendeeId);
                                agency.PrimaryPlanReviewer = primaryUser;
                            }

                            AttendeeInfo secondaryReviewer = _Appointment.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)agency.DepartmentInfo).FirstOrDefault();
                            if (secondaryReviewer != null)
                            {
                                UserIdentity secondaryUser = userIdentityModelBO.GetInstance(secondaryReviewer.AttendeeId);
                                agency.PrimaryPlanReviewer = secondaryUser;
                            }

                            //
                            agency.UpdatedUser = _Appointment.UpdatedUser;
                            savedtrades = estimationCRUDAdapter.SaveProjectAgency(agency);

                            break;
                        case DepartmentNameEnums.Zone_Davidson:
                        case DepartmentNameEnums.Zone_Cornelius:
                        case DepartmentNameEnums.Zone_Pineville:
                        case DepartmentNameEnums.Zone_Matthews:
                        case DepartmentNameEnums.Zone_Mint_Hill:
                        case DepartmentNameEnums.Zone_Huntersville:
                        case DepartmentNameEnums.Zone_UMC:
                        case DepartmentNameEnums.Zone_Cty_Chrlt:
                        case DepartmentNameEnums.Fire_Davidson:
                        case DepartmentNameEnums.Fire_Cornelius:
                        case DepartmentNameEnums.Fire_Pineville:
                        case DepartmentNameEnums.Fire_Matthews:
                        case DepartmentNameEnums.Fire_Mint_Hill:
                        case DepartmentNameEnums.Fire_Huntersville:
                        case DepartmentNameEnums.Fire_UMC:
                        case DepartmentNameEnums.Fire_Cty_Chrlt:
                        case DepartmentNameEnums.Fire_County:
                        case DepartmentNameEnums.Zone_County:
                            //need to work out fire and zoning 
                            //find out if this agency deparmtnet is in the same group
                            DepartmentDivisionEnum agencydepartmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)agency.DepartmentInfo);
                            //find the reviewer in the same division
                            AttendeeInfo attendeeInfo = _Appointment.AssignedReviewers.Where(x => new DepartmentDivisionEnum().CreateInstance(x.DeptNameEnumId) == agencydepartmentDivisionEnum).FirstOrDefault();
                            UserIdentity userIdentity = userIdentityModelBO.GetInstance(attendeeInfo.AttendeeId);
                            agency.AssignedPlanReviewer = userIdentity;

                            //primary, secondary//
                            //need to work out fire and zoning 
                            //find out if this agency deparmtnet is in the same group
                            DepartmentDivisionEnum primarydepartmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)agency.DepartmentInfo);
                            //find the reviewer in the same division
                            AttendeeInfo primary = _Appointment.PrimaryReviewers.Where(x => new DepartmentDivisionEnum().CreateInstance(x.DeptNameEnumId) == primarydepartmentDivisionEnum).FirstOrDefault();
                            if (primary != null)
                            {
                                UserIdentity primaryIdentity = userIdentityModelBO.GetInstance(primary.AttendeeId);
                                agency.PrimaryPlanReviewer = primaryIdentity;
                            }

                            //need to work out fire and zoning 
                            //find out if this agency deparmtnet is in the same group
                            DepartmentDivisionEnum secondarydepartmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)agency.DepartmentInfo);
                            //find the reviewer in the same division
                            AttendeeInfo secondary = _Appointment.SecondaryReviewers.Where(x => new DepartmentDivisionEnum().CreateInstance(x.DeptNameEnumId) == secondarydepartmentDivisionEnum).FirstOrDefault();
                            if (secondary != null)
                            {
                                UserIdentity secondaryIdentity = userIdentityModelBO.GetInstance(secondary.AttendeeId);
                                agency.SecondaryPlanReviewer = secondaryIdentity;
                            }

                            ////
                            agency.UpdatedUser = _Appointment.UpdatedUser;
                            savedtrades = estimationCRUDAdapter.SaveProjectAgency(agency);

                            break;
                        default:
                            break;
                    }

                    switch (agency.DepartmentInfo)
                    {
                        case DepartmentNameEnums.Zone_Davidson:
                        case DepartmentNameEnums.Zone_Cornelius:
                        case DepartmentNameEnums.Zone_Pineville:
                        case DepartmentNameEnums.Zone_Matthews:
                        case DepartmentNameEnums.Zone_Mint_Hill:
                        case DepartmentNameEnums.Zone_Huntersville:
                        case DepartmentNameEnums.Zone_UMC:
                        case DepartmentNameEnums.Zone_Cty_Chrlt:
                        case DepartmentNameEnums.Zone_County:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersZone), _Appointment.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.Fire_Davidson:
                        case DepartmentNameEnums.Fire_Cornelius:
                        case DepartmentNameEnums.Fire_Pineville:
                        case DepartmentNameEnums.Fire_Matthews:
                        case DepartmentNameEnums.Fire_Mint_Hill:
                        case DepartmentNameEnums.Fire_Huntersville:
                        case DepartmentNameEnums.Fire_UMC:
                        case DepartmentNameEnums.Fire_Cty_Chrlt:
                        case DepartmentNameEnums.Fire_County:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersFire), _Appointment.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Day_Care:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersDayCare), _Appointment.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Food:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersFood), _Appointment.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Pool:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersPool), _Appointment.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Facilities:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersLodge), _Appointment.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.Backflow:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(_Appointment.ExcludedPlanReviewersBackFlow), _Appointment.UpdatedUser.ID);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateProjectDeptAssignedReviewersPMA - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return ret;
        }

        public void SendAppointmentNotifications(bool isCancellation)
        {
            UpdateAppointment(new List<AttendeeDetails>(), isCancellation);

            SendCalendarNotifications();

            SendProjectManagerNotification();
        }

        public List<AttendeeInfo> GetAttendeesByApptId(int appointmentId)
        {
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();

            try
            {
                ProjectScheduleBE schedule = GetProjectScheduleByAppointmentId(appointmentId).First();

                List<UserScheduleBE> users = schedule.ProjectScheduleID.HasValue ? new UserScheduleBO().GetListByScheduleID(schedule.ProjectScheduleID.Value) : new List<UserScheduleBE>();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                foreach (UserScheduleBE user in users)
                {
                    UserIdentity userIdentity = userIdentityModelBO.GetInstance(user.UserID.Value);
                    AttendeeInfo attendeeInfo = new AttendeeInfo(user.UserID.Value, (int)user.BusinessRefID)
                    {
                        FirstName = userIdentity.FirstName,
                        LastName = userIdentity.LastName
                    };
                    attendees.Add(attendeeInfo);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetAttendeesByApptId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return attendees;
        }

        public List<AttendeeDetails> ConvertAttendeeInfoToAttendeeDetails()
        {
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            foreach (AttendeeInfo attendee in _Appointment.Attendees)
            {
                UserIdentity identity = userIdentityModelBO.GetInstance(attendee.AttendeeId);

                if (!attendeeDetails.Where(x => x.EmailId == identity.SrcSystemValueText).Any())
                {
                    attendeeDetails.Add(new AttendeeDetails
                    {
                        EmailId = identity.SrcSystemValueText,
                        IsRequired = true,
                        FirstName = identity.FirstName,
                        LastName = identity.LastName,
                        UserId = identity.ID
                    });
                }
            }

            return attendeeDetails;
        }

        public List<ProjectScheduleBE> GetProjectScheduleByAppointmentId(int id)
        {
            try
            {
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                //this method brings back an instance even if nothing is found
                return projectScheduleBO.GetByApptId(_Appointment.ID, _Appointment.ProjectScheduleRefEnum.ToString());

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetProjectScheduleById - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public void SendAppointmentNotifications(List<AttendeeDetails> attendeeDetails, bool isCancellation)
        {
            UpdateAppointment(attendeeDetails, isCancellation);

            SendCalendarNotifications();

            SendProjectManagerNotification();
        }

        public bool SendCalendarNotifications()
        {
            try
            {
                ICalendarAppointmentAdapter calendarAppointmentAdapter = new CalendarAppointmentAdapter(_Appointment);
                calendarAppointmentAdapter.ProcessAppointments();

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SendCalendarNotifications - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        #region private methods
        private void SetAppointmentProjectSchedule(ProjectScheduleBE projectScheduleBE)
        {
            _Appointment.ProjectSchedule = new ProjectScheduleModelBO().ConvertBEToModel(projectScheduleBE);
        }

        private void SetAppointmentProjectScheduleFromId(int projectScheduleId)
        {
            ProjectScheduleBE projectScheduleBE = new ProjectScheduleBO().GetById(projectScheduleId);
            SetAppointmentProjectSchedule(projectScheduleBE);
        }

        private int CreateProjectSchedule(int wkrId, ScheduleTime scheduleTime)
        {
            ProjectScheduleBE projectScheduleBE;
            ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();

            projectScheduleBE = new ProjectScheduleBE
            {
                AppoinmentID = _Appointment.ID,
                ProjectScheduleTypeRef = _Appointment.ProjectScheduleRefEnum.ToString(),
                UserId = wkrId.ToString(),
                RecurringApptDt = scheduleTime.StartDate
            };

            projectScheduleBE.ProjectScheduleID = projectScheduleBO.Create(projectScheduleBE);

            SetAppointmentProjectSchedule(projectScheduleBE);

            return projectScheduleBE.ProjectScheduleID.Value;
        }

        protected int UpdateProjectSchedule()
        {
            int projectScheduleId = 0;

            try
            {
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();

                List<ProjectScheduleBE> projectScheduleBEs = new List<ProjectScheduleBE>();

                projectScheduleBEs = projectScheduleBO.GetByApptId(_Appointment.ID, _Appointment.ProjectScheduleRefEnum.ToString(), null);
                if (projectScheduleBEs != null && projectScheduleBEs.Count > 0)
                {
                    projectScheduleBE = projectScheduleBEs.FirstOrDefault();
                    projectScheduleId = projectScheduleBE.ProjectScheduleID.Value;
                    projectScheduleBE.RecurringApptDt = _Appointment.StartDate;
                    projectScheduleBE.UserId = _Appointment.UpdatedUser.ID.ToString();

                    int rows = projectScheduleBO.Update(projectScheduleBE);
                }
                else
                {
                    projectScheduleBE.AppoinmentID = _Appointment.ID;
                    projectScheduleBE.ProjectScheduleTypeRef = _Appointment.ProjectScheduleRefEnum.ToString();
                    projectScheduleBE.RecurringApptDt = _Appointment.StartDate;
                    projectScheduleBE.UserId = _Appointment.UpdatedUser.ID.ToString();

                    projectScheduleId = projectScheduleBO.Create(projectScheduleBE);

                    projectScheduleBE.ProjectScheduleID = projectScheduleId;
                }

                SetAppointmentProjectSchedule(projectScheduleBE);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateProjectSchedule AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return projectScheduleId;
        }

        private List<AttendeeDetails> AddCapacitySearchesAndCreateAttendeeDetails(AttendeeInfo attendee)
        {
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
            UserIdentity identity = userIdentityModelBO.GetInstance(attendee.AttendeeId);

            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            //assemble list of conflicting meetings to adjust plan reviews accordingly
            _CapacitySearches.Add(new ScheduleCapacitySearch
            {
                BeginDateTime = _Appointment.StartDate.Value,
                EndDateTime = _Appointment.EndDate.Value,
                ReviewerSearchList = new List<string>(new string[] { attendee.AttendeeId.ToString() })
            });

            identity = userIdentityModelBO.GetInstance(attendee.AttendeeId);

            if (!attendeeDetails.Where(x => x.EmailId == identity.SrcSystemValueText).Any())
            {
                attendeeDetails.Add(new AttendeeDetails
                {
                    EmailId = identity.SrcSystemValueText,
                    IsRequired = true,
                    FirstName = identity.FirstName,
                    LastName = identity.LastName,
                    UserId = identity.ID
                });
            }

            return attendeeDetails;
        }

        protected void SetAppointmentData()
        {
            EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();

            //get business ref id for attendees, at this point they only have the enum id value
            foreach (AttendeeInfo user in _Appointment.NewAttendees)
            {
                user.BusinessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)user.DeptNameEnumId).ID;
            }

            _Appointment.MeetingRoom =
                _Appointment.MeetingRoomRefId.HasValue ? new MeetingRoomBO().GetById(_Appointment.MeetingRoomRefId.Value) : null;

            if (_Appointment.ProjectID.HasValue)
            {
                _Appointment.ProjectEstimation = estimationCRUDAdapter.GetProjectDetailsByProjectId(_Appointment.ProjectID.Value);
            }

            if (_Appointment.StartDate.HasValue && _Appointment.EndDate.HasValue)
            {
                _Appointment.ScheduleTimes = DetermineScheduleTimes(_Appointment.StartDate.Value, _Appointment.EndDate.Value);
            }

            ProjectScheduleBE projectScheduleBE = GetProjectScheduleByAppointmentId(_Appointment.ID).FirstOrDefault();

            if (projectScheduleBE != null && projectScheduleBE.RecurringApptDt.HasValue)
            {
                SetAppointmentProjectSchedule(projectScheduleBE);

                _Appointment.Attendees = GetAttendeesByApptId(projectScheduleBE.ProjectScheduleID.Value);
            }

            if (_Appointment.AttendeeDetails.Count == 0)
            {
                _Appointment.AttendeeDetails = ConvertAttendeeInfoToAttendeeDetails();
            }
        }

        private List<ScheduleTime> DetermineScheduleTimes(DateTime start, DateTime end)
        {
            DateTime startDate = _Appointment.IsCancellation && _Appointment.PrevStartDate.HasValue ? _Appointment.PrevStartDate.Value : start;
            DateTime endDate = _Appointment.IsCancellation && _Appointment.PrevEndDate.HasValue ? _Appointment.PrevEndDate.Value : end;

            List<ScheduleTime> scheduleTimes = new List<ScheduleTime>();

            if (_Appointment.RecurringDates.Any())
            {
                scheduleTimes.AddRange(_Appointment.RecurringDates);
            }
            else
            {
                scheduleTimes.Add(new ScheduleTime() { StartDate = startDate, EndDate = endDate });
            }

            return scheduleTimes;
        }

        private List<AttendeeDetails> CreateUserSchedulesOfAttendees(List<AttendeeInfo> attendeeIds, int projectScheduleId, ScheduleTime scheduleTime, int wkrId)
        {
            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            UserScheduleBO userScheduleBO = new UserScheduleBO();

            try
            {
                foreach (AttendeeInfo attendee in attendeeIds)
                {
                    int userScheduleId = 0;

                    UserScheduleBE userScheduleBE = new UserScheduleBE
                    {
                        ProjectScheduleID = projectScheduleId,
                        StartDateTime = scheduleTime.StartDate,
                        EndDateTime = scheduleTime.EndDate,
                        BusinessRefID = attendee.BusinessRefId,
                        UserID = attendee.AttendeeId,
                        UserId = wkrId.ToString()
                    };

                    userScheduleId = userScheduleBO.Create(userScheduleBE);

                    List<AttendeeDetails> updatedAttendeeDetails = AddCapacitySearchesAndCreateAttendeeDetails(attendee);

                    attendeeDetails.AddRange(updatedAttendeeDetails);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CreateUserSchedules AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return attendeeDetails;
        }

        private void AdjustPlanReviewsForConflicts(List<AttendeeInfo> attendeeIds)
        {
            //adjust plan reviews that conflict with this scheduling
            List<ScheduleCapacitySearchResult> conflicts = new ScheduleCapacityAdapter().SearchPlanReviewCapacity(_CapacitySearches);
            PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
            List<int> rescheduledPRs = new List<int>();
            foreach (ScheduleCapacitySearchResult item in conflicts)
            {
                foreach (Meeting pr in item.PlanReviews)
                {
                    ProjectScheduleBE be = new ProjectScheduleBO().GetByApptId(pr.AppointmentId, "PR", null).FirstOrDefault();
                    if (rescheduledPRs.Contains(be.ProjectScheduleID.Value)) continue;
                    //mark this one as already scheduled
                    rescheduledPRs.Add(be.ProjectScheduleID.Value);
                    AttendeeInfo attendee = attendeeIds.Where(x => x.AttendeeId == item.UserId).FirstOrDefault();
                    if (attendee != null)
                    {
                        planReviewAdapter.UpdateSinglePRAttendee(attendee, pr.AppointmentId, item.UserId.ToString(), be.ProjectScheduleID.Value);
                    }
                }
            }
        }

        private ProjectScheduleBE GetProjectScheduleByScheduleDetails(int projectScheduleId)
        {
            try
            {
                List<ProjectScheduleBE> retlst = GetProjectScheduleByAppointmentId(_Appointment.ID);
                if (retlst.Count == 0)
                    //this brings back a new instance even if nothing is found
                    return new ProjectScheduleBE();
                else
                    return retlst[0];
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetProjectScheduleByScheduleDetails - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        private List<AttendeeDetails> GetAttendeesForRemovalByProjectSchedule(int projectScheduleId, List<AttendeeInfo> attendeeIds = null)
        {
            List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

            UserScheduleBO userScheduleBO = new UserScheduleBO();
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

            List<UserScheduleBE> userSchedules = new UserScheduleBO().GetListByScheduleID(projectScheduleId);
            List<UserScheduleBE> attendeeSchedules = new List<UserScheduleBE>();

            if (attendeeIds == null)
            {
                attendeeSchedules = userSchedules;
            }
            else
            {
                foreach (AttendeeInfo attendee in attendeeIds)
                {
                    UserScheduleBE userSchedule = new UserScheduleBE();
                    if (attendee.BusinessRefId > 0)
                    {
                        userSchedule = userSchedules.Where(x => x.UserID == attendee.AttendeeId && x.BusinessRefID == attendee.BusinessRefId).FirstOrDefault();

                    }
                    else
                    {
                        userSchedule = userSchedules.Where(x => x.UserID == attendee.AttendeeId).FirstOrDefault();

                    }
                    if (userSchedule != null && userSchedule.UserScheduleID > 0)
                        attendeeSchedules.Add(userSchedule);
                }
            }

            foreach (UserScheduleBE schedule in attendeeSchedules)
            {
                userScheduleBO.Delete(schedule.UserScheduleID.Value);

                UserIdentity identity = userIdentityModelBO.GetInstance(schedule.UserID.Value);
                attendeesDetails.Add(new AttendeeDetails
                {
                    EmailId = identity.SrcSystemValueText,
                    IsRequired = true,
                    FirstName = identity.FirstName,
                    LastName = identity.LastName,
                    UserId = identity.ID,
                    UserPrincipalName = identity.UserPrincipalName,
                    CalendarId = identity.CalendarId

                });
            }

            return attendeesDetails;
        }

        private void UpdateAppointment(List<AttendeeDetails> attendeeDetails, bool isCancellation)
        {
            _Appointment.AttendeeDetails = attendeeDetails;
            _Appointment.IsCancellation = isCancellation;

            if (_Appointment.AttendeeDetails.Count() == 0)
            {
                _Appointment.AttendeeDetails = GetAllAttendeesByAppointment();
            }
        }

        private List<AttendeeDetails> GetAllAttendeesByAppointment()
        {
            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            if (_Appointment.ID > 0)
            {
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                if (_Appointment.ProjectSchedule != null)
                {
                    List<UserScheduleBE> userSchedules = userScheduleBO.GetListByScheduleID(_Appointment.ProjectSchedule.ProjectScheduleID);

                    foreach (UserScheduleBE userSchedule in userSchedules)
                    {
                        UserIdentity identity = userIdentityModelBO.GetInstance(userSchedule.UserID.Value);
                        attendeeDetails.Add(new AttendeeDetails()
                        {
                            EmailId = identity.SrcSystemValueText,
                            IsRequired = true,
                            FirstName = identity.FirstName,
                            LastName = identity.LastName,
                            UserId = identity.ID
                        });
                    }
                }
            }

            return attendeeDetails;
        }

        private bool SendProjectManagerNotification()
        {
            try
            {
                if (SendNotification())
                {
                    IProjectManagerEmailAdapter projectManagerEmailAdapter = new ProjectManagerEmailAdapter(_Appointment);
                    return projectManagerEmailAdapter.CreateAppointmentEmail();
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SendProjectManagerNotification AppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        private bool SendNotification()
        {
            if (!_Appointment.IsCancellation)
            {
                return true;
            }

            if (_Appointment.GetType() == typeof(ReserveExpressReservation))
            {
                return false;
            }

            if (_Appointment.ApptCancellationEnum == AppointmentCancellationEnum.No_Reply
                || _Appointment.ApptCancellationEnum == AppointmentCancellationEnum.Reject)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}