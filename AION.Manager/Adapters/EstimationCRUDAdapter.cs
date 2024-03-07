using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Email.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.Engines;
using AION.Manager.Models;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;


namespace AION.BL.Adapters
{
    /// <summary>
    /// This class works as an adapter between multiple AION datbase ModelBO objects. If there is any calls which is made
    /// to database which consits of more than one database table, this class should be used for making the calls which
    /// eventually calls into methods in BusinessObject classes.
    /// </summary>
    public class EstimationCRUDAdapter : BaseManagerAdapter, IEstimationCRUDAdapter
    {
        public ProjectDetailModel GetProjectDetailModel(ProjectParms projectParms)
        {
            try
            {
                ProjectDetailModel model = new ProjectDetailModel();

                IPlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                IUserAdapter userAdapter = new UserAdapter();
                NoteAdapter noteAdapter = new NoteAdapter();
                IMeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
                IProjectAuditAdapter projectAuditAdapter = new ProjectAuditAdapter();
                IAdminAdapter adminAdapter = new AdminAdapter();
                ISchedulerAdapter schedulerAdapter = new SchedulerAdapter();
                IEmailAdapter emailAdapter = new EmailAdapter();

                Tuple<ProjectBE,ProjectEstimation> projectDetailsTuple = GetProjectDetailsTupleByProjectSrcSourceTxt(projectParms.ProjectId, false);

                ProjectBE projectBE = projectDetailsTuple.Item1;
                model.ProjectEstimation = projectDetailsTuple.Item2;

                model.Facilitators = userAdapter.GetAllFacilitators();

                InternalNoteManagerModel internalNoteManagerModel = new InternalNoteManagerModel
                {
                    ProjectId = model.ProjectEstimation.ID,
                };

                model.Notes = noteAdapter.GetAllInternalNotes(internalNoteManagerModel);

                if (model.ProjectEstimation.AccelaPropertyType == PropertyTypeEnums.Express)
                {
                    model.MeetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "EXPRESS_MEETING_ROOMS");
                }
                else
                {
                    model.MeetingRooms = meetingRoomAdapter.GetMeetingRooms(true, "PRELIM_MEETING_ROOMS");
                }

                model.ProjectAudits = projectAuditAdapter.GetProjectAudits(model.ProjectEstimation.ID);
                model.AuditActionRefs = adminAdapter.GetAuditActionRefs();
                model.ScheduledMeetings = schedulerAdapter.GetSchedMeetingListByProject(projectBE);
                model.ProjectNotificationEmails = emailAdapter.GetProjectNotificationEmails(model.ProjectEstimation.ID);

                model.ProjectCycleReviews = planReviewAdapter.GetProjectCycleReviews(model.ProjectEstimation.ID);

                model.Facilitator = userAdapter.GetUserIdentityByID(model.ProjectEstimation.AssignedFacilitator.Value);

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in ProjectDetailModel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public ProjectEstimation GetProjectDetailsForEstimation(Meck.Shared.MeckDataMapping.AccelaProjectModel accelaModel)
        {

            ProjectEstimation model = new ProjectEstimationModelBO().GetInstance(accelaModel);
            return model;
        }

        /// <summary>
        /// This returns the data from the AION db
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectEstimation GetProjectDetailsByProjectId(int projectId)
        {
            try
            {
                ProjectEstimation projectEstimation = new ProjectEstimationModelBO().GetInstanceFromAION(projectId);

                if (!projectEstimation.CycleNbr.HasValue || projectEstimation.CycleNbr.Value < 1)
                {
                    List<ProjectCycle> projectCycles = new PlanReviewAdapter().GetProjectCyclesByProjectId(projectEstimation.ID);
                    if (projectCycles.Count > 0)
                    {
                        projectEstimation.CycleNbr = projectCycles.FirstOrDefault(x => x.CurrentCycleInd == true).CycleNbr;
                    }
                }

                return projectEstimation;
            }
            catch (System.Exception ex)
            {
                string input = projectId.ToString();
                string errorMessage = input + " Error in GetProjectDetailsByProjectId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        /// <summary>
        /// This returns data form the AION db
        /// </summary>
        /// <param name="srcSystemValTxt"></param>
        /// <returns></returns>
        public ProjectEstimation GetProjectDetailsByProjectSrcSourceTxt(string srcSystemValTxt, bool includeReviewers = true)
        {
            try
            {
                string accelaProjectRefId = string.Empty;
                ProjectBO projectbo = new ProjectBO();
                ProjectBE projectbe = projectbo.GetByExternalRefInfo(srcSystemValTxt);
                return new ProjectEstimationModelBO().GetInstanceFromAION(projectbe, includeReviewers);
            }
            catch (System.Exception ex)
            {
                string input = !string.IsNullOrWhiteSpace(srcSystemValTxt) ? "input: " + srcSystemValTxt : "";
                string errorMessage = input + " Error in GetProjectDetailsByProjectSrcSourceTxt - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public Tuple<ProjectBE,ProjectEstimation> GetProjectDetailsTupleByProjectSrcSourceTxt(string srcSystemValTxt, bool includeReviewers = false)
        {
            try
            {
                string accelaProjectRefId = string.Empty;
                ProjectBO projectbo = new ProjectBO();
                ProjectBE projectbe = projectbo.GetByExternalRefInfo(srcSystemValTxt);

                ProjectEstimation projectEstimation;

                if (projectbe.ProjectTypRefId == (int)PropertyTypeEnums.Express)
                {
                    projectEstimation = new ProjectEstimationModelBO().GetInstanceFromAIONAllDepartmentReviewers(projectbe);
                }
                else
                {
                    projectEstimation = new ProjectEstimationModelBO().GetInstanceFromAION(projectbe, includeReviewers);
                }
                
                return new Tuple<ProjectBE, ProjectEstimation>(projectbe, projectEstimation);
            }
            catch (System.Exception ex)
            {
                string input = !string.IsNullOrWhiteSpace(srcSystemValTxt) ? "input: " + srcSystemValTxt : "";
                string errorMessage = input + " Error in GetProjectDetailsTupleByProjectSrcSourceTxt - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool SaveProjectEstimationDetails(ProjectEstimation model)
        {
            ProjectEstimationModelBO projectBO = new ProjectEstimationModelBO();
            if (projectBO.UpsertEstimationProjectDetails(model) == false) return false;
            return true;
        }

        public List<CatalogItem> GetAllPendingReasons()
        {
            return new CatalogItemModelBO().GetInstance("EstimationPendingReasons");
        }

        public List<Facilitator> GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate)
        {
            try
            {
                FacilitatorModelBO facilitatorModelBO = new FacilitatorModelBO();
                return facilitatorModelBO.GetFacilitatorWorkloadSummary(startdate, enddate);
            }
            catch (Exception ex)
            {

                string errorMessage = "Error in GetFacilitatorWorkloadSummary - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

        }
        public bool UpdateAccelaProjectPreliminaryMeetingStatus(Meck.Shared.MeckDataMapping.AccelaProjectModel project)
        {
            ProjectEstimation aionProjectModel = GetProjectDetailsForEstimation(project);

            if (aionProjectModel.IsPreliminaryMeetingRequested && aionProjectModel.IsPreliminaryMeetingCompleted)
            {
                if (UpdatePreliminaryMeetingStatus(aionProjectModel) == false) return false;
            }

            else if (aionProjectModel.IsPreliminaryMeetingRequested == true && aionProjectModel.IsPreliminaryMeetingCompleted == false && aionProjectModel.IsPreliminaryMeetingCancelled == true)
            {
                if (UpdateCancelledPreliminaryMeetingStatus(aionProjectModel) == false) return false;
            }
            return true;
        }

        private bool UpdatePreliminaryMeetingStatus(ProjectEstimation aionProj)
        {
            ISchedulerAdapter schedulerAdapter = new SchedulerAdapter();
            IPMAAccessor pmaAccessor = new PMAAccessor();

            try
            {
                var pma = pmaAccessor.GetByProjectId(aionProj.ID);

                if (pma != null)
                {
                    var savePrelimStatus = new SavePrelimStatus() { PrelimID = pma.ID, ResponseStatusEnumId = ((int)AppointmentResponseStatusEnum.Closed).ToString() };
                    schedulerAdapter.UpdatePrelimStatus(savePrelimStatus);
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error UpdatePreliminaryMeetingStatus - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw;
            }
        }

        private bool UpdateCancelledPreliminaryMeetingStatus(ProjectEstimation aionProj)
        {
            ISchedulerAdapter schedulerAdapter = new SchedulerAdapter();
            IPMAAccessor pmaAccessor = new PMAAccessor();

            try
            {
                var pma = pmaAccessor.GetByProjectId(aionProj.ID);

                if (pma != null)
                {
                    var savePrelimStatus = new SavePrelimStatus() { PrelimID = pma.ID, ResponseStatusEnumId = ((int)AppointmentResponseStatusEnum.Cancelled).ToString() };
                    schedulerAdapter.UpdatePrelimStatus(savePrelimStatus);
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error UpdatePreliminaryMeetingStatus - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw;
            }
        }

        public ProjectEstimation SwapEstimationExpressStatus(ProjectEstimation project, bool isExpress)
        {
            PlanReviewAdapter prAdapter = new PlanReviewAdapter();

            if (isExpress)
            {
                //swap from regular plan review to express

                List<PlanReview> planReviews = prAdapter.GetPlanReviewsByProjectId(project.ID);
                PlanReview pr = planReviews.FirstOrDefault(x => x.IsCurrentCycle == true);
                //check if any plan reviews currently exist
                if (pr.CreatedUser != null)
                {
                    //jcl 8-11-21 send the email
                    pr.SendEmail = true;
                    prAdapter.UpdatePlanReviewStatus(pr, AppointmentResponseStatusEnum.Cancelled);
                    if (pr.HasFutureCycle)
                    {
                        //cancel future cycle as well
                        PlanReview fpr = planReviews.FirstOrDefault(x => x.IsFutureCycle == true);
                        prAdapter.UpdatePlanReviewStatus(fpr, AppointmentResponseStatusEnum.Cancelled);
                    }
                }
                //update project to be express, set back to scheduling dashboard
                project.AionPropertyType = PropertyTypeEnums.Express;
            }

            if (project.AIONProjectStatus.ProjectStatusEnum != ProjectStatusEnum.Auto_Estimation_Pending)
                project.AIONProjectStatus = GetProjectStatusBaseList().Where(x => x.ProjectStatusEnum == ProjectStatusEnum.Not_Scheduled).FirstOrDefault();
            return project;
        }

        public bool SaveProjectTrade(ProjectTrade projectTrade)
        {
            try
            {
                ProjectTradeModelBO projectTradeModelBO = new ProjectTradeModelBO();
                int rowcount = projectTradeModelBO.UpdateProjectDepartment(projectTrade);
                return rowcount > 0;

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveProjectTrade - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        public bool SaveProjectAgency(ProjectAgency projectAgency)
        {
            try
            {
                ProjectTradeModelBO projectTradeModelBO = new ProjectTradeModelBO();
                int rowcount = projectTradeModelBO.UpdateProjectDepartment(projectAgency);
                return rowcount > 0;

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveProjectAgency - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }



        public List<ProjectStatus> GetProjectStatusBaseList()
        {
            try
            {
                ProjectStatusModelBO bo = new ProjectStatusModelBO();
                return bo.BaseList;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetProjectStatusBaseList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        /// <summary>
        /// Save Estimation from EstimationMain
        /// Utilizes Project SaveType SaveTypeEnum field
        /// Include express emails
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveEstimation(ProjectEstimation model)
        {
            bool success = false;
            bool fifoSuccess = true;

            try
            {
                EmailAdapter emailAdapter = new EmailAdapter();
                EmailMessageBO emailMessageBO = new EmailMessageBO();

                ProjectEstimationModelBO projectBO = new ProjectEstimationModelBO();
                if (model != null)
                {
                    success = projectBO.UpsertEstimationProjectDetails(model);

                    if (model.IsFifo 
                        && model.IsProjectPreliminary == false 
                        && model.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Scheduled) // no longer in estimation
                    {
                        fifoSuccess = new FIFOAdapter().ProcessFIFO(model);
                    }
                }
                else
                {
                    string errorMessage = "Project in SaveEstimation was null - " + DateTime.Now;
                    var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                        string.Empty, string.Empty, string.Empty);
                }

                if (success && fifoSuccess)
                {
                    switch (model.SaveType)
                    {
                        case SaveTypeEnum.Submittal:
                            if (model.AccelaPropertyType == PropertyTypeEnums.Express && model.AionPropertyType != PropertyTypeEnums.Express)
                            {
                                //get the last internal note
                                NoteTypeModelBO noteTypeModelBO = new NoteTypeModelBO();
                                NoteType internalNoteType = noteTypeModelBO.GetInstance(NoteTypeEnum.InternalNotes);
                                Note note = model.Notes.Where(x => x.ParentNoteID == 0 && x.NotesType == internalNoteType && x.UpdatedUser == model.UpdatedUser).OrderByDescending(x => x.UpdatedDate).FirstOrDefault();
                                string notecomments = note != null ? note.NotesComments : "";
                                //send the email to project manager
                                //get mail message defaults
                                MailMessage mailMessage = emailAdapter.GetMailMessage();

                                mailMessage.Subject = "Project Type Change for Project # " + model.AccelaProjectRefId + " ( " + model.ProjectName + ")";

                                MessageTemplateEngine mte = new MessageTemplateEngine();
                                mte.Project = model;
                                mte.ProjectNumber = model.AccelaProjectRefId;
                                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Express_Decision_By_Estimator;
                                mte.ProjectName = model.ProjectName;
                                mte.ProjectAddress = model.ProjectAddress;
                                mte.Notes = notecomments;
                                mte.EstimatorName = model.UpdatedUser.FirstName + " " + model.UpdatedUser.LastName;
                                mte.EstimatorEmail = model.UpdatedUser.Email;
                                mte.EstimatorPhone = model.UpdatedUser.Phone;
                                mte.ProjectType = model.AionPropertyType.ToStringValue();

                                mailMessage.Body = mte.BuildMessage();

                                //add the project manager
                                string pmname = model.PMName;
                                string pmemail = model.PMEmail;

                                if (!String.IsNullOrWhiteSpace(pmemail) && pmemail.Contains("@"))
                                    mailMessage.To.Add(new MailAddress(pmemail, pmname));

                                emailAdapter.SendEmailMessage(mailMessage);
                                //save this notification
                                SendProjectNotification sendProjectNotification = new SendProjectNotification
                                {
                                    ProjectId = model.ID,
                                    MailMessage = mailMessage,
                                    SendDate = DateTime.Now,
                                    WrkId = model.UpdatedUser.ID,
                                    EmailNotif = EmailNotifType.Express_Decision_By_Estimator,
                                    UserIds = new List<int>()

                                };
                                //add PM
                                sendProjectNotification.UserIds.Add(model.PMId.Value);
                                int notifId = emailAdapter.SaveNotificationEmail(sendProjectNotification);
                                if (notifId > 0)
                                {
                                    //save the email list
                                    sendProjectNotification.ProjectNotificationEmailId = notifId;
                                    emailAdapter.SaveNotificationEmailSendList(sendProjectNotification);
                                }
                            }
                            break;
                        case SaveTypeEnum.Save:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveProjectEstimationDetails - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }
    }
}