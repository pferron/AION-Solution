using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AION.Manager.Common;
using AION.Manager.Engines;
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
    public class SchedulerAdapter : BaseManagerAdapter, ISchedulerAdapter
    {
        public SchedulingModel GetSchedulingModel(ProjectParms projectParms)
        {
            try
            {
                SchedulingModel model = new SchedulingModel();

                IHolidayConfigAdapter holidayConfigAdapter = new HolidayConfigAdapter();
                IEstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                IPlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                IUserAdapter userAdapter = new UserAdapter();
                IExpressAdapter expressAdapter = new ExpressAdapter();
                NoteAdapter noteAdapter = new NoteAdapter();
                IMeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
                IAdminAdapter adminAdapter = new AdminAdapter();
                IPMAAccessor pmaAccessor = new PMAAccessor();
                IFMAAccessor fmaAccessor = new FMAAccessor();

                model.Holidays = holidayConfigAdapter.GetHolidayConfigDates();

                Tuple<ProjectBE, ProjectEstimation> projectDetailsTuple = estimationCRUDAdapter.GetProjectDetailsTupleByProjectSrcSourceTxt(projectParms.ProjectId, true);

                ProjectBE projectBE = projectDetailsTuple.Item1;

                model.ProjectEstimation = projectDetailsTuple.Item2;

                model.ProjectCycleSummary = planReviewAdapter.GetProjectCycleSummary(model.ProjectEstimation.ID, projectBE);
                model.Facilitators = userAdapter.GetAllFacilitators();

                InternalNoteManagerModel internalNoteManagerModel = new InternalNoteManagerModel
                {
                    ProjectId = model.ProjectEstimation.ID,
                };

                model.Notes = noteAdapter.GetAllInternalNotes(internalNoteManagerModel);
                model.MandatoryNotes = noteAdapter.GetStandardNotes(NoteTypeEnum.SchedulingMandatoryNotes, model.ProjectEstimation.AionPropertyType);
                model.StandardNotes = noteAdapter.GetStandardNotes(NoteTypeEnum.EstimationStandardNotes, PropertyTypeEnums.NA);
                model.StandardNoteGroupEnums = noteAdapter.GetStandardNoteGroupEnums();

                if (model.ProjectEstimation.AccelaPropertyType == PropertyTypeEnums.Express)
                {
                    model.ReserveExpressReservations = expressAdapter.GetExpressReservationList();
                    model.MeetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "EXPRESS_MEETING_ROOMS");
                }
                else
                {
                    model.MeetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "PRELIM_MEETING_ROOMS");
                }

                bool isExpress = model.ProjectEstimation.AccelaPropertyType == PropertyTypeEnums.Express;

                model.Reviewers = userAdapter.GetAllReviewers(isExpress);

                model.FireAgencyReviewers = userAdapter.GetReviewers(0, (int)DepartmentNameEnums.Fire_Cornelius, isExpress);

                model.ZoningJurisdictionReviewers.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home, (int)DepartmentNameEnums.Zone_County, isExpress));
                model.ZoningJurisdictionReviewers.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Master_Plans, (int)DepartmentNameEnums.Zone_County, isExpress));
                model.ZoningJurisdictionReviewers.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Single_Family_Homes, (int)DepartmentNameEnums.Zone_County, isExpress));
                model.ZoningJurisdictionReviewers.AddRange(userAdapter.GetReviewers((int)PropertyTypeEnums.FIFO_Small_Commercial, (int)DepartmentNameEnums.Zone_County, isExpress));

                model.CatalogItems = adminAdapter.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER");

                model.Users = userAdapter.Search(string.Empty, string.Empty);

                model.PreliminaryMeetingAppointment = pmaAccessor.GetByProjectId(model.ProjectEstimation.ID);

                if (!string.IsNullOrWhiteSpace(projectParms.MeetingTypeDesc))
                {
                    MeetingTypeEnum meetingType = (MeetingTypeEnum)Enum.Parse(typeof(MeetingTypeEnum), projectParms.MeetingTypeDesc);

                    model.FacilitatorMeetingAppointments = fmaAccessor.GetByProjectIDAndMeetingType(projectParms.ProjectId, meetingType.ToStringValue());
                }

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetSchedulingModel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<PlanReviewerAvailableHour> GetAllPlanReviewerHours()
        {
            List<PlanReviewerAvailableHour> ret = new List<PlanReviewerAvailableHour>();
            PlanReviewerAvailableHoursBO bo = new PlanReviewerAvailableHoursBO();
            List<PlanReviewerAvailableHoursBE> vals = bo.GetAllPlanReviewerAvailableHours();
            foreach (var item in vals)
            {
                PlanReviewerAvailableHour val = new PlanReviewerAvailableHour();
                val.AvailableHours = item.AvailableHours.Value;
                val.CreatedDate = item.CreatedDate.Value;
                val.CreatedUser = new UserIdentity() { ID = int.Parse(item.CreatedByWkrId) };
                val.UpdatedDate = item.UpdatedDate.Value;
                val.UpdatedUser = new UserIdentity() { ID = int.Parse(item.UpdatedByWkrId) };
                val.ID = item.ID.Value;
                val.PlanReviewTypeCd = item.PlanReviewTypeCd;
                val.EnumMappingValNbr = (PlanReviewHourTypes)item.EnumMappingValNbr.Value;
                val.ProjectTypeRefId = item.ProjectTypeRefId;
                ret.Add(val);
            }
            return ret;
        }

        public List<PlanReviewerAvailableTime> GetAllPlanReviewerTimes()
        {
            List<PlanReviewerAvailableTime> ret = new List<PlanReviewerAvailableTime>();
            PlanReviewerAvailableTimeBO bo = new PlanReviewerAvailableTimeBO();
            List<PlanReviewerAvailableTimeBE> vals = bo.GetAllPlanReviewerAvailableTimes();
            foreach (var item in vals)
            {
                PlanReviewerAvailableTime val = new PlanReviewerAvailableTime();
                val.AvailableStartTime = item.AvailableStartTime.Value;
                val.AvailableEndTime = item.AvailableEndTime.Value;
                val.CreatedDate = item.CreatedDate.Value;
                val.CreatedUser = new UserIdentity() { ID = int.Parse(item.CreatedByWkrId) };
                val.UpdatedDate = item.UpdatedDate.Value;
                val.UpdatedUser = new UserIdentity() { ID = int.Parse(item.UpdatedByWkrId) };
                val.ID = item.ProjectTypeRefID.Value;
                val.ProjectTypeDesc = item.ProjectTypeDesc;
                val.ProjectTypeRefID = (PropertyTypeEnums)item.ProjectTypeRefID.Value;
                ret.Add(val);
            }
            return ret;
        }

        public AutoScheduledPrelimValues GetPrelimAutoScheduledData(AutoScheduledPrelimParams data)
        {
            PrelimProjectSchedulingEngine prelim = new PrelimProjectSchedulingEngine(data);
            return prelim.GetAutoEstimatedValues();
        }

        public AutoScheduledPlanReviewValues GetAutoScheduledDataPlanReview(AutoScheduledPlanReviewParams data)
        {
            PlanReviewProjectSchedulingEngine prelim = new PlanReviewProjectSchedulingEngine(data);
            return prelim.GetAutoEstimatedValues();
        }

        public bool ManualScheduleCapacity(SchedulePlanReviewCapacityParams data)
        {
            PlanReviewProjectSchedulingEngine prelim = new PlanReviewProjectSchedulingEngine(data);
            return prelim.ManualScheduleCapacity(data);
        }

        public List<DateTime> SearchSelfScheduleCapacity(SchedulePlanReviewCapacityParams data)
        {
            PlanReviewProjectSchedulingEngine prelim = new PlanReviewProjectSchedulingEngine(data);
            return prelim.SearchSelfScheduleCapacity(data);
        }

        public bool RearrangeAutoScheduleIfRequired(DateTime startDt, DateTime endDt)
        {
            //idenitfy the length of personal time freed up.
            //identify the timeslots that got freed up.
            //
            //idenitify the project hrs after personal time
            //identify the slots which occured in last.
            //move each 15 minutes back and allocate them to freed up slots until the slots are filled.

            return true;
        }

        public AutoScheduledExpressValues GetAutoScheduledDataExpress(AutoScheduledExpressParams data)
        {
            ExpressProjectSchedulingEngine expressProjectSchedulingEngine = new ExpressProjectSchedulingEngine(data);
            return expressProjectSchedulingEngine.AutoScheduledValues;
        }

        public AutoScheduledFacilitatorMeetingValues GetAutoScheduledDataFacilitatorMeeting(AutoScheduledFacilitatorMeetingParams data)
        {
            FacilitatorMeetingSchedulingEngine facilitatorMeetingSchedulingEngine = new FacilitatorMeetingSchedulingEngine(data);
            return facilitatorMeetingSchedulingEngine.GetAutoEstimatedValues();
        }

        public List<CustmrMeetings> GetMeetingsByUserID(int userId)
        {
            List<CustmrMeetings> ret = new List<CustmrMeetings>();
            PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();

            List<CustmrMeetingsBE> bevals = bo.GetMeetingsListByUserID(userId).ToList();
            foreach (var item in bevals)
            {

                CustmrMeetings val = new CustmrMeetings();
                val.AppendixAgendaDue = item.AppendixAgendaDue.Value;
                val.ProjectStatus = (ProjectStatusEnum)item.ProjectStatus;
                val.MeetingDate = item.MeetingDate.Value;
                val.MeetingTime = item.MeetingTime.Value;
                val.MeetingType = (MeetingTypeEnum)item.MeetingType.Value;
                val.MeetingStatus = (AppointmentResponseStatusEnum)item.ApptResponseStatusRefId;
                val.MinutesDue = item.MinutesDue.Value;
                val.ProjectExternalRefID = item.ProjectExternalRefID;
                val.ProjectID = item.ProjectID;
                val.ProjectName = item.ProjectName;
                val.ProjectType = (PropertyTypeEnums)item.ProjectType.Value;
                val.RecIdTxt = item.RecIdTxt;
                ret.Add(val);
            }
            return ret;
        }

        public bool UpdatePlanReviewAvailableHours(PlanReviewerAvailableHour value)
        {
            PlanReviewerAvailableHoursBE be = new PlanReviewerAvailableHoursBE();
            be.AvailableHours = value.AvailableHours;
            be.UpdatedByWkrId = value.UpdatedUser.ID.ToString();
            be.UpdatedDate = value.UpdatedDate;
            be.ID = value.ID;
            be.ProjectTypeRefId = value.ProjectTypeRefId;
            PlanReviewerAvailableHoursBO bo = new PlanReviewerAvailableHoursBO();
            bo.Update(be);
            return true;
        }

        public bool UpdatePlanReviewAvailableTimes(PlanReviewerAvailableTime value)
        {
            PlanReviewerAvailableTimeBE be = new PlanReviewerAvailableTimeBE();
            be.AvailableStartTime = value.AvailableStartTime;
            be.AvailableEndTime = value.AvailableEndTime;
            be.UpdatedByWkrId = value.UpdatedUser.ID.ToString();
            be.UpdatedDate = value.UpdatedDate;
            be.PlanReviewerAvailableTimeID = value.ID;
            be.ProjectTypeRefID = (int)value.ProjectTypeRefID;
            PlanReviewerAvailableTimeBO bo = new PlanReviewerAvailableTimeBO();
            bo.Update(be);
            return true;
        }

        /// <summary>
        /// Get project list from Accela and return as ProjectEstimation list
        /// </summary>
        /// <returns></returns>
        public List<ProjectEstimation> GetSchedulingDashboardList()
        {
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

            List<ProjectEstimation> projectsToBeScheduled = estimationAccelaAdapter.GetProjectSchedulingList();

            List<ProjectEstimation> projectsNotFifo = projectsToBeScheduled.Where(x =>
                    x.IsFifo == false
            ).ToList();

            List<ProjectEstimation> completeprojects = new List<ProjectEstimation>();

            List<string> fmaStatuses = new List<string>();

            int apptResponseStatusRefNotScheduled = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Not_Scheduled).ApptResponseStatusRefId.Value;
            int apptResponseStatusRefRejected = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Reject).ApptResponseStatusRefId.Value;

            fmaStatuses.Add(apptResponseStatusRefNotScheduled.ToString());
            fmaStatuses.Add(apptResponseStatusRefRejected.ToString());

            string fmaStatusIds = string.Join(",", fmaStatuses);

            //TODO: set startdate and end date for the dashboard

            List<FacilitatorMeetingAppointment> fmas = new FacilitatorMeetingApptModelBO().Search(null, null, fmaStatusIds);

            //LES-3049 jcl - filter project types
            foreach (ProjectEstimation project in projectsNotFifo)
            {
                if (project.IsProjectPreliminary)
                {
                    project.MeetingTypeEnum = MeetingTypeEnum.Preliminary;
                }

                completeprojects.Add(project);
            }

            completeprojects = SortProjectsByAvailableTeamScore(completeprojects);

            List<ProjectEstimation> completeFMAprojects = new List<ProjectEstimation>();

            //if fmas then get the fma list since these projects can be in any status
            if (fmas != null && fmas.Count() > 0)
            {
                EstimationCRUDAdapter estimationCrudAdapter = new EstimationCRUDAdapter();
                List<int> projectids = fmas.Select(x => x.ProjectID.Value).ToList();

                List<ProjectEstimation> fmaprojects = estimationAccelaAdapter.GetFMAProjectScheduleList(projectids);

                foreach (FacilitatorMeetingAppointment fma in fmas)
                {
                    // Have to perform a mapping since we're adding the project estimation to the same collection 
                    // and do not want to change the IsFacilitatorMeeting values for the others with the same
                    // project ID if there are duplicates necessary for different UI actions.

                    if (fma.ProjectID != null)
                    {
                        ProjectEstimation fmaProject = new ProjectEstimation();

                        fmaProject = fmaprojects.FirstOrDefault(x => x.ID == (int)fma.ProjectID.Value);

                        if (fmaProject != null && fmaProject.ID > 0)
                        {
                            ProjectEstimation projectEstimation = new ProjectEstimation
                            {
                                ID = fmaProject.ID,
                                AccelaProjectRefId = fmaProject.AccelaProjectRefId,
                                AccelaPropertyType = fmaProject.AccelaPropertyType,
                                IsProjectRTAP = fmaProject.IsProjectRTAP,
                                AccelaCostOfConstruction = fmaProject.AccelaCostOfConstruction,
                                //LES-3813 - set the onTime field based on the meeting rather than the project
                                UpdatedDate = fma.UpdatedDate,
                                ProjectName = fmaProject.ProjectName,
                                IsProjectPreliminary = fmaProject.IsProjectPreliminary,
                                AssignedFacilitator = fmaProject.AssignedFacilitator,
                                DisplayOnlyInformation = fmaProject.DisplayOnlyInformation,
                                AccelaProjectCreatedDate = fmaProject.AccelaProjectCreatedDate,
                                PlansReadyOnDate = fmaProject.PlansReadyOnDate,
                                PMEmail = fmaProject.PMEmail,
                                PMName = fmaProject.PMName,
                                PMPhone = fmaProject.PMPhone,
                                AIONProjectStatus = fmaProject.AIONProjectStatus,
                                IsFacilitatorMeeting = true,
                                TeamGradeTxt = fmaProject.TeamGradeTxt
                            };

                            MeetingType meetingType = new MeetingTypeModelBO().GetInstance((int)fma.MeetingTypeRefId);

                            AppointmentResponseStatus meetingStatus = new AppointmentResponseStatusModelBO().GetInstance((int)fma.ApptResponseStatusRefId);

                            projectEstimation.MeetingTypeEnum = (MeetingTypeEnum)meetingType.MeetingTypeEnum;
                            projectEstimation.MeetingStatusEnum = (AppointmentResponseStatusEnum)meetingStatus.ApptResponseStatusEnum;

                            completeFMAprojects.Add(projectEstimation);
                        }
                    }
                }
            }

            completeFMAprojects = SortProjectsByAvailableTeamScore(completeFMAprojects);
            completeprojects.AddRange(completeFMAprojects);

            return completeprojects;
        }

        public List<CustSchedMeeting> GetSchedMeetingsByProjectId(string projectId)
        {
            CustomerScheduledMeetingBO bo = new CustomerScheduledMeetingBO();
            List<CustomerScheduledMeetingBE> be = bo.GetById(projectId);
            if (be.Count() == 0) return new List<CustSchedMeeting>();

            List<CustSchedMeeting> custSchedMeetings = new List<CustSchedMeeting>();

            foreach (CustomerScheduledMeetingBE item in be)
            {
                CustSchedMeeting meeting = new CustSchedMeeting()
                {
                    MeetingId = item.MeetingId,
                    MeetingTypeEnum = (MeetingTypeEnum)item.MeetingType.Value,
                    MeetingDate = item.FromDt,
                    MeetingTime = item.FromDt.ToString("hh:mm tt") + " to " + item.ToDt.ToString("hh:mm tt"),
                    AgendaDue = item.AppBAgendaDue,
                    ResponseDue = item.ResponseDue,
                    ApptResponseStatusId = item.ApptResponseStatusRefId,
                    AppointmentResponseStatusEnum = (AppointmentResponseStatusEnum)item.ApptResponseStatusRefId,
                    Attendees = AppointmentHelper.ProcessAttendeesCSV(item.Attendees)
                };

                if (item.ApptCancellationRefId != null && item.ApptCancellationRefId > 0)
                {
                    meeting.AppointmentCancellationEnum = (AppointmentCancellationEnum)item.ApptCancellationRefId;
                }

                custSchedMeetings.Add(meeting);
            }

            return custSchedMeetings;
        }

        public List<Meeting> GetSchedMeetingListbyProjectId(ProjectParms projectParms)
        {
            List<Meeting> ret = new List<Meeting>();
            try
            {
                PreliminaryMeetingAppointmentBO pmaBO = new PreliminaryMeetingAppointmentBO();
                FacilitatorMeetingAppointmentBO fmaBO = new FacilitatorMeetingAppointmentBO();

                ProjectBE projectBE = new ProjectBO().GetByExternalRefInfo(projectParms.ProjectId);
                ProjectEstimation project = new ProjectEstimationModelBO().ConvertFromBE(projectBE);

                int projectid = project.ID;

                //get a list of projects that exist in both Accela and DB
                List<MeetingBE> pmaMeetings = pmaBO.GetInternalMeetingsListByProjectID(projectid).OrderBy(x => x.DateTimeFrom).ToList();
                List<MeetingBE> fmaMeetings = fmaBO.GetInternalMeetingsListByProjectID(projectid).OrderBy(x => x.DateTimeFrom).ToList();

                ret = ConvertMeetings(project, pmaMeetings, fmaMeetings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetSchedMeetingListbyProjectId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return ret;
        }

        public List<Meeting> GetSchedMeetingListByProject(ProjectBE projectBE)
        {
            List<Meeting> ret = new List<Meeting>();
            try
            {
                PreliminaryMeetingAppointmentBO pmaBO = new PreliminaryMeetingAppointmentBO();
                FacilitatorMeetingAppointmentBO fmaBO = new FacilitatorMeetingAppointmentBO();

                ProjectEstimation project = new ProjectEstimationModelBO().ConvertFromBE(projectBE);

                int projectid = project.ID;
                bool hasAccelaProject = project != null && project.ID > 0;

                //get a list of projects that exist in both Accela and DB
                List<MeetingBE> pmaMeetings = pmaBO.GetInternalMeetingsListByProjectID(projectid).OrderBy(x => x.DateTimeFrom).ToList();
                List<MeetingBE> fmaMeetings = fmaBO.GetInternalMeetingsListByProjectID(projectid).OrderBy(x => x.DateTimeFrom).ToList();

                ret = ConvertMeetings(project, pmaMeetings, fmaMeetings);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetSchedMeetingListbyProjectId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// Scheduled meetings
        /// To return ALL meetings for facilitators, user wrkId == 0, 
        /// otherwise, use the user id of the person who should be in the schedule
        /// </summary>
        /// <param name="wrkId"></param>
        /// <returns></returns>
        public List<InternalMeetings> GetInternalMeetings(int wrkId)
        {
            List<InternalMeetings> ret = new List<InternalMeetings>();
            PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();

            List<InternalMeetingsBE> bevals = bo.GetInternalMeetingsList(wrkId);
            foreach (InternalMeetingsBE item in bevals)
            {

                Facilitator facilitator = new Facilitator();
                UserIdentity projectManager = new UserIdentity();

                if (item.FacilitatorId.HasValue && item.FacilitatorId.Value > 0)
                {
                    facilitator = new FacilitatorModelBO().GetInstance(item.FacilitatorId.Value);
                }
                if (item.ProjectManagerId.HasValue)
                {
                    projectManager = new UserIdentityModelBO().GetInstance(item.ProjectManagerId.Value);
                }
                InternalMeetings val = new InternalMeetings();
                val.AppendixAgendaDue = item.AppendixAgendaDue.HasValue ? item.AppendixAgendaDue.Value : (DateTime?)null;

                //TODO: jcl figure out what's supposed to go in this project status
                val.ProjectStatus = ProjectStatusEnum.Scheduled;

                val.MeetingDate = item.MeetingDate.Value;
                val.MeetingTime = item.MeetingTime.Value;
                val.MeetingType = (MeetingTypeEnum)item.MeetingType.Value;
                val.MeetingStatus = (AppointmentResponseStatusEnum)item.ApptResponseStatusRefId;
                val.MinutesDue = item.MinutesDue.Value;
                val.ProjectExternalRefID = item.ProjectExternalRefID;
                val.ProjectID = item.ProjectID;
                val.ProjectName = item.ProjectName;
                val.ProjectType = (PropertyTypeEnums)item.ProjectType.Value;

                val.PMEmail = projectManager.Email;
                val.PMName = projectManager.FirstName + " " + projectManager.LastName;
                val.PMPhone = projectManager.Phone;
                val.IsProjectRTAP = item.IsProjectRTAP.Value;
                val.BuildingCodeVersion = item.BuildingCodeVersion;
                val.FacilitatorName = "Unassigned";  // set to default
                if (facilitator != null)
                {
                    val.FacilitatorName = facilitator.FirstName + " " + facilitator.LastName;
                }
                val.TeamGradeTxt = item.TeamGradeTxt;
                val.RecIdTxt = item.RecIdTxt;
                ret.Add(val);
            }
            return ret;
        }

        #region Private Methods
        public List<int> GetExcludedPlanReviewers(List<string> excludedReviewers)
        {
            List<int> ret = new List<int>();
            if (excludedReviewers == null || excludedReviewers.Count == 0)
                return ret;
            List<Reviewer> allreviewers = new UserAdapter().GetAllReviewers();
            foreach (var item in excludedReviewers)
            {
                if (int.Parse(item) == -1)
                    continue;
                var t = (from x in allreviewers
                         where int.Parse(item) == x.ID
                         select x).FirstOrDefault();
                if (t != null)
                    ret.Add(t.ID);
            }
            return ret;
        }

        /// <summary>
        /// Get Project Schedule by PMA ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<ProjectScheduleBE> GetProjectScheduleByPMAId(int id, List<int> scheduleIds = null)
        {
            try
            {
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                //this method brings back an instance even if nothing is found
                return projectScheduleBO.GetByApptId(id, "PMA", scheduleIds);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetProjectScheduleByPMAId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        private List<Meeting> ConvertMeetings(ProjectEstimation project, List<MeetingBE> pmaMeetings, List<MeetingBE> fmaMeetings)
        {
            List<Meeting> meetings = new List<Meeting>();

            bool hasAccelaProject = project != null && project.ID > 0;

            if (pmaMeetings != null && pmaMeetings.Count > 0)
            {
                foreach (MeetingBE item in pmaMeetings)
                {
                    Meeting val = new Meeting();
                    val.AppointmentId = item.AppointmentId;
                    val.ProjectScheduleId = item.ProjectScheduleId.Value;
                    val.AppendixAgendaDue = item.AppendixAgendaDue;
                    //if this project doesn't exist in Accela, return NA project status
                    val.ProjectStatus = hasAccelaProject ? (ProjectStatusEnum)item.ProjectStatus : ProjectStatusEnum.NA;
                    val.MeetingDate = item.MeetingDate.Value;
                    val.MeetingTime = item.MeetingTime.Value;
                    val.MeetingType = (MeetingTypeEnum)item.MeetingType.Value;
                    val.MinutesDue = item.MinutesDue.Value;
                    val.ProjectExternalRefID = item.ProjectExternalRefID;
                    val.ProjectID = item.ProjectID;
                    val.ProjectName = item.ProjectName;
                    val.ProjectType = (PropertyTypeEnums)item.ProjectType.Value;
                    val.AppointmentResponseStatus = (AppointmentResponseStatusEnum)item.ApptResponseStatusRefId.Value;
                    val.PMEmail = project.PMEmail;
                    val.PMName = project.PMName;
                    val.PMPhone = project.PMPhone;
                    val.RTAP = project.IsProjectRTAP;
                    val.BuildingCodeVersion = project.BuildingCodeVersion;
                    val.MeetingStart = item.DateTimeFrom.Value;
                    val.MeetingEnd = item.DateTimeTo.Value;
                    val.MeetingRoomRefId = item.MeetingRoomRefId.HasValue ? item.MeetingRoomRefId.Value : 0;
                    if (item.MeetingRoomRefId.HasValue)
                        val.MeetingRoom = new MeetingRoomBO().GetById(item.MeetingRoomRefId.Value);
                    val.Attendees = AppointmentHelper.ProcessAttendeesCSV(item.Attendees);
                    meetings.Add(val);
                }
            }

            if (fmaMeetings != null && fmaMeetings.Count > 0)
            {
                foreach (MeetingBE item in fmaMeetings)
                {
                    Meeting val = new Meeting();
                    val.AppointmentId = item.AppointmentId;
                    val.ProjectScheduleId = item.ProjectScheduleId.Value;
                    val.AppendixAgendaDue = item.AppendixAgendaDue;
                    //if this project doesn't exist in Accela, return NA project status
                    val.ProjectStatus = hasAccelaProject ? (ProjectStatusEnum)item.ProjectStatus : ProjectStatusEnum.NA;
                    val.MeetingDate = item.MeetingDate.Value;
                    val.MeetingTime = item.MeetingTime.Value;
                    val.MeetingType = (MeetingTypeEnum)item.MeetingType.Value;
                    val.MinutesDue = item.MinutesDue.Value;
                    val.ProjectExternalRefID = item.ProjectExternalRefID;
                    val.ProjectID = item.ProjectID;
                    val.ProjectName = item.ProjectName;
                    val.ProjectType = (PropertyTypeEnums)item.ProjectType.Value;
                    val.AppointmentResponseStatus = (AppointmentResponseStatusEnum)item.ApptResponseStatusRefId.Value;
                    val.PMEmail = project.PMEmail;
                    val.PMName = project.PMName;
                    val.PMPhone = project.PMPhone;
                    val.RTAP = project.IsProjectRTAP;
                    val.BuildingCodeVersion = project.BuildingCodeVersion;
                    val.MeetingStart = item.DateTimeFrom.Value;
                    val.MeetingEnd = item.DateTimeTo.Value;
                    val.MeetingRoomRefId = item.MeetingRoomRefId.HasValue ? item.MeetingRoomRefId.Value : 0;
                    if (item.MeetingRoomRefId.HasValue)
                        val.MeetingRoom = new MeetingRoomBO().GetById(item.MeetingRoomRefId.Value);
                    val.Attendees = AppointmentHelper.ProcessAttendeesCSV(item.Attendees);
                    meetings.Add(val);
                }
            }

            return meetings;
        }
        #endregion Private Methods

        public bool UpdatePrelimDateRequest(RequestPrelimDatesManagerModel model)
        {
            PreliminaryMeetingAppointmentBE be = new PreliminaryMeetingAppointmentBE();
            be.PreliminaryMeetingApptID = model.PreliminaryMeetingApptId;
            be.RequestedDate1 = model.RequestDate1;
            be.RequestedDate2 = model.RequestDate2;
            be.RequestedDate3 = model.RequestDate3;


            PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();
            bo.UpdatePrelimDateRequest(be);
            return true;
        }

        public bool UpdateMeetingDateRequest(RequestMeetingDatesManagerModel model)
        {
            FacilitatorMeetingAppointmentBE be = new FacilitatorMeetingAppointmentBE();
            be.FacilitatorMeetingApptId = model.MeetingApptId;
            be.RequestedDate1 = model.RequestDate1;
            be.RequestedDate2 = model.RequestDate2;
            be.RequestedDate3 = model.RequestDate3;

            FacilitatorMeetingAppointmentBO bo = new FacilitatorMeetingAppointmentBO();
            bo.UpdateMeetingDateRequest(be);
            return true;
        }

        public bool UpdatePrelimStatus(SavePrelimStatus model)
        {
            PreliminaryMeetingAppointmentBE be = new PreliminaryMeetingAppointmentBE();
            be.PreliminaryMeetingApptID = model.PrelimID;
            be.ApptResponseStatusRefId = Convert.ToInt32(model.ResponseStatusEnumId);
            be.UpdatedByWkrId = model.WkrId;
            PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();
            bo.UpdatePrelimStatus(be);

            //Audit the Project Status since the called proc changes the status to NOT SCHEDULED if this is REJECT/ACCEPT
            if ((AppointmentResponseStatusEnum)Convert.ToInt32(model.ResponseStatusEnumId) == AppointmentResponseStatusEnum.Reject
                || (AppointmentResponseStatusEnum)Convert.ToInt32(model.ResponseStatusEnumId) == AppointmentResponseStatusEnum.Accept)
            {
                be = new PreliminaryMeetingAppointmentBO().GetById(model.PrelimID);
                new ProjectAuditModelBO().InsertProjectAudit(be.ProjectID.Value, ProjectStatusEnum.Not_Scheduled.ToStringValue(), model.WkrId, AuditActionEnum.Status_Changed);
            }
            return true;
        }

        public bool UpdatePrelimStatusFromCustomer(SavePrelimStatus model)
        {
            PreliminaryMeetingAppointmentBE be = new PreliminaryMeetingAppointmentBE();
            int statusenumid = Convert.ToInt32(model.ResponseStatusEnumId);
            be.PreliminaryMeetingApptID = model.PrelimID;
            //in this case, this is the enum int 
            be.ApptResponseStatusEnumId = statusenumid;
            be.UpdatedByWkrId = model.WkrId;
            new PreliminaryMeetingAppointmentBO().UpdatePrelimStatus(be);

            //Audit the Project Status since the called proc changes the status to NOT SCHEDULED if this is REJECT/ACCEPT
            if ((AppointmentResponseStatusEnum)statusenumid == AppointmentResponseStatusEnum.Reject
                 || (AppointmentResponseStatusEnum)Convert.ToInt32(model.ResponseStatusEnumId) == AppointmentResponseStatusEnum.Scheduled
                 || (AppointmentResponseStatusEnum)Convert.ToInt32(model.ResponseStatusEnumId) == AppointmentResponseStatusEnum.Accept)
            {
                be = new PreliminaryMeetingAppointmentBO().GetById(model.PrelimID);

                ProjectAuditModelBO auditModelBO = new ProjectAuditModelBO();

                auditModelBO.InsertProjectAudit(be.ProjectID.Value, ProjectStatusEnum.Not_Scheduled.ToStringValue(), model.WkrId, AuditActionEnum.Status_Changed);

                AuditActionEnum auditActionEnum = AuditActionEnum.NA;

                if ((AppointmentResponseStatusEnum)statusenumid == AppointmentResponseStatusEnum.Reject)
                {
                    auditActionEnum = AuditActionEnum.Review_Date_Rejected;
                }

                if ((AppointmentResponseStatusEnum)statusenumid == AppointmentResponseStatusEnum.Scheduled
                    || (AppointmentResponseStatusEnum)statusenumid == AppointmentResponseStatusEnum.Accept)
                {
                    auditActionEnum = AuditActionEnum.Review_Date_Accepted;
                }

                string auditText = $"by customer {be.FromDT.Value}";

                auditModelBO.InsertProjectAudit(be.ProjectID.Value, auditText, model.WkrId, auditActionEnum);

                auditModelBO.InsertProjectAudit(be.ProjectID.Value, ProjectStatusEnum.Scheduled.ToStringValue(), model.WkrId, AuditActionEnum.Status_Changed);
            }

            return true;
        }
        public bool UpdateMeetingStatus(SaveMeetingStatus model)
        {
            FacilitatorMeetingAppointmentBE be = new FacilitatorMeetingAppointmentBE();
            be.FacilitatorMeetingApptId = model.MeetingId;
            be.ApptResponseStatusRefId = Convert.ToInt32(model.Status);
            FacilitatorMeetingAppointmentBO bo = new FacilitatorMeetingAppointmentBO();
            bo.UpdateMeetingStatus(be);
            return true;
        }

        public List<ConfigureReserveExpressDays> GetConfigureReserveExpressList()
        {
            List<ConfigureReserveExpressDays> ret = new List<ConfigureReserveExpressDays>();
            ConfigureReserveExpressBO bo = new ConfigureReserveExpressBO();
            List<ConfigureReserveExpressBE> beList = bo.GetList();
            foreach (var item in beList)
            {
                ConfigureReserveExpressDays day = new ConfigureReserveExpressDays();
                day.Id = item.ConfigureReserveExpressId.Value;
                day.Day = item.ReserveExpressDay;
                day.ActiveInd = item.ActiveInd.Value;
                day.StartDate = item.StartDate == null ? (DateTime?)null : Convert.ToDateTime(item.StartDate);
                day.EndDate = item.EndDate == null ? (DateTime?)null : Convert.ToDateTime(item.EndDate);

                ret.Add(day);
            }
            return ret;
        }

        public bool SaveConfigureExpress(List<ConfigureReserveExpressDays> days)
        {
            try
            {
                ConfigureReserveExpressBO bo = new ConfigureReserveExpressBO();

                foreach (var item in days)
                {
                    ConfigureReserveExpressBE data = new ConfigureReserveExpressBE();

                    data.ConfigureReserveExpressId = item.Id;
                    data.ActiveInd = item.ActiveInd;
                    data.StartDate = item.StartDate;
                    data.EndDate = item.EndDate;

                    data.UpdatedDate = DateTime.Now;
                    bo.Update(data);
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error UpdateUserProjectTypeRef - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool DeleteExpressPlanReviewerRotation()
        {
            try
            {
                ReserveExpressPlanReviewerBO bo = new ReserveExpressPlanReviewerBO();

                return bo.DeleteExpressPlanReviewerRotation();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error DeleteExpressPlanReviewerRotation - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool SaveExpressPlanReviewerRotation(List<ReserveExpressPlanReviewer> reviewers)
        {
            try
            {
                ReserveExpressPlanReviewerBO bo = new ReserveExpressPlanReviewerBO();

                foreach (var item in reviewers)
                {
                    ReserveExpressPlanReviewerBE data = new ReserveExpressPlanReviewerBE();

                    data.BusinessRefId = item.BusinessRefId;
                    data.PlanReviewerId = item.PlanReviewerId;
                    data.RotationNbr = item.RotationNbr;
                    data.CreatedByWkrId = item.CreatedUser.ID.ToString();

                    bo.Create(data);
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SaveReserveExpressPlanReviewers - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool SaveExpressPlanReviewerRotation(List<ReserveExpressPlanReviewerBE> reviewers)
        {
            try
            {
                ReserveExpressPlanReviewerBO bo = new ReserveExpressPlanReviewerBO();

                foreach (var item in reviewers)
                {
                    item.UserId = "1";

                    bo.Create(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SaveReserveExpressPlanReviewers - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public List<BusinessRefBE> GetAllBusinessRefs()
        {
            var ret = new List<BusinessRefBE>();
            DepartmentModelBO bo = new DepartmentModelBO();
            ret = bo.GetAllBusinessRefs();

            return ret;
        }

        public bool SaveMeetingAction(ProjectDetailMeetingAction model)
        {
            PreliminaryMeetingAppointmentBE be = new PreliminaryMeetingAppointmentBE();
            be.ProjectID = model.ProjectId;

            be.ApptResponseStatusRefId = 7;
            PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();
            bo.UpdateMeetingAction(be);
            return true;
        }

        public bool AreAllCycleHoursUnderOneHour(List<ReReview> reReviews)
        {
            if (reReviews.Any(x => x.EstimatedReReviewTime > 1))
            {
                return false;
            }
            return true;
        }

        public List<DateTime> GetAvailDateForExpress(RequestExpressDatesManagerModel model) {
            ExpressProjectSchedulingEngine engine = new ExpressProjectSchedulingEngine(model.StartDate, model.EndDate, model.ProjectNumber);
            return ExpressProjectSchedulingEngine.AvailableDateForRequestExpress;
        }

        private List<ProjectEstimation> SortProjectsByAvailableTeamScore(List<ProjectEstimation> completeprojects)
        {
            List<ProjectEstimation> sortedProjects = new List<ProjectEstimation>();

            // sort the projects in ascending order by team score
            List<ProjectEstimation> superiorPerformers = completeprojects.Where(x => x.TeamGradeTxt != null && x.TeamGradeTxt.ToUpper() == "SUPERIOR").OrderBy(x => x.AccelaProjectCreatedDate).ToList();
            List<ProjectEstimation> successfulPerformers = completeprojects.Where(x => x.TeamGradeTxt != null && x.TeamGradeTxt.ToUpper() == "SUCCESSFUL").OrderBy(x => x.AccelaProjectCreatedDate).ToList();
            List<ProjectEstimation> poorPerformers = completeprojects.Where(x => x.TeamGradeTxt != null && x.TeamGradeTxt.ToUpper() == "POOR").OrderBy(x => x.AccelaProjectCreatedDate).ToList();

            List<ProjectEstimation> poorPerformersReadyForScheduling = new List<ProjectEstimation>();
            foreach (ProjectEstimation poorPerformer in poorPerformers)
            {
                if (IsPoorPerformerReadyForScheduling(poorPerformer))
                {
                    poorPerformersReadyForScheduling.Add(poorPerformer);
                }
            }
            List<ProjectEstimation> notRatedPerformers = completeprojects.Where(x => x.TeamGradeTxt != null && x.TeamGradeTxt.ToUpper() == "NOT YET RATED").OrderBy(x => x.AccelaProjectCreatedDate).ToList();
            List<ProjectEstimation> noTeamScore = completeprojects.Where(x => x.TeamGradeTxt == null || x.TeamGradeTxt.Trim() == "").OrderBy(x => x.AccelaProjectCreatedDate).ToList();

            sortedProjects.AddRange(superiorPerformers);
            sortedProjects.AddRange(successfulPerformers);
            sortedProjects.AddRange(poorPerformersReadyForScheduling);
            sortedProjects.AddRange(notRatedPerformers);
            sortedProjects.AddRange(noTeamScore);

            return sortedProjects;
        }

        private bool IsPoorPerformerReadyForScheduling(ProjectEstimation poorPerformer)
        {
            // Poor performers must have a delay in scheduling that is greater than 24 hours after estimation

            ProjectAuditAdapter adapter = new ProjectAuditAdapter();
            List<ProjectAudit> projectAudits = adapter.GetProjectAudits(poorPerformer.ID);

            ProjectAudit facilitatorAssigned = projectAudits.FirstOrDefault(x => x.AuditActionRefId == (int)AuditActionEnum.Facilitator_Assigned);

            if (facilitatorAssigned == null) return false;

            DateTime delayUntil = DateTime.Now.AddHours(-24);

            if (facilitatorAssigned.AuditDt > delayUntil) return false;

            return true;
        }
    }

    public interface ISchedulerAdapter
    {
        /// <summary>
        /// Part of Admin screen Misc tab and this will update the Plan reviewer default allocation hour.
        /// </summary>
        /// <param name="PlanReviewHoursTypeID">ID to plan reviewer</param>
        /// <param name="value">hour and minute value </param>
        /// <returns></returns>
        SchedulingModel GetSchedulingModel(ProjectParms parms);
        bool UpdatePlanReviewAvailableHours(PlanReviewerAvailableHour value);
        bool UpdatePlanReviewAvailableTimes(PlanReviewerAvailableTime value);

        List<PlanReviewerAvailableHour> GetAllPlanReviewerHours();
        List<PlanReviewerAvailableTime> GetAllPlanReviewerTimes();

        List<CustmrMeetings> GetMeetingsByUserID(int userId);

        /// <summary>
        /// Gets the list of unscheduled projects for the Scheduling Dashboard
        /// </summary>
        /// <returns></returns>
        List<ProjectEstimation> GetSchedulingDashboardList();
        List<CustSchedMeeting> GetSchedMeetingsByProjectId(string projectId);
        bool UpdatePrelimDateRequest(RequestPrelimDatesManagerModel model);

        bool UpdatePrelimStatus(SavePrelimStatus model);
        bool UpdatePrelimStatusFromCustomer(SavePrelimStatus model);

        bool UpdateMeetingDateRequest(RequestMeetingDatesManagerModel model);

        bool UpdateMeetingStatus(SaveMeetingStatus model);


        /// <summary>
        /// Gets all the scheduled meetings for a project
        /// Used on Internal Project Detail
        /// </summary>
        /// <param name="projectParms"></param>
        /// <returns></returns>
        List<Meeting> GetSchedMeetingListbyProjectId(ProjectParms projectParms);
        List<Meeting> GetSchedMeetingListByProject(ProjectBE projectBE);


        /// <summary>
        /// Scheduled meetings
        /// To return ALL meetings for facilitators, user wrkId == 0, 
        /// otherwise, use the user id of the person who should be in the schedule
        /// </summary>
        /// <param name="wrkId"></param>
        /// <returns></returns>
        List<InternalMeetings> GetInternalMeetings(int wrkId);
        List<ConfigureReserveExpressDays> GetConfigureReserveExpressList();
        AutoScheduledPrelimValues GetPrelimAutoScheduledData(AutoScheduledPrelimParams data);
        bool SaveConfigureExpress(List<ConfigureReserveExpressDays> days);
        bool SaveExpressPlanReviewerRotation(List<ReserveExpressPlanReviewer> reviewers);
        bool DeleteExpressPlanReviewerRotation();
        List<BusinessRefBE> GetAllBusinessRefs();
        bool SaveMeetingAction(ProjectDetailMeetingAction model);
        bool AreAllCycleHoursUnderOneHour(List<ReReview> reReviews);
    }
}
