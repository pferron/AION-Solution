using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Email.Engine.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.Engines.Scheduling;
using AION.Manager.Engines.SyncAccela;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Ical.Net;
using Meck.Logging;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace AION.BL.Adapters
{
    public class FunctionAdapter : BaseManagerAdapter, IFunctionAdapter
    {
        private List<AccelaFailure> _accelaFailures;
        private Failure _failure;

        private string _accelaEnvironment;
        private string _aionEnvironment;
        public FunctionAdapter()
        {
            _accelaEnvironment = ConfigurationManager.AppSettings["AccelaEnvironment"].ToString();
            _accelaFailures = new List<AccelaFailure>();
            _aionEnvironment = ConfigurationManager.AppSettings["Environment"].ToString();
            _failure = new Failure();
        }
        public bool CancelScheduledExpressPlanReview()
        {
            var errorMessage = $"Info: CancelScheduledExpressPlanReview in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);
            try
            {
                List<int> cancelledAppointmentIds = new List<int>();

                PlanReviewScheduleBO bo = new PlanReviewScheduleBO();

                PlanReviewAdapter adapter = new PlanReviewAdapter();

                cancelledAppointmentIds = bo.CancelScheduledExpressPlanReview();

                foreach (int id in cancelledAppointmentIds)
                {
                    PlanReview planReview = adapter.GetPlanReviewByPlanReviewScheduleId(id);

                    ExpressMeetingAppointment appt = new EMAAdapter().ConvertPlanReviewToEMA(planReview);

                    CancelAppointment(appt);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in CancelScheduledExpressPlanReview-FunctionAdapter - " + ex.Message;

                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool CancelPrelimMeeting()
        {
            var errorMessage = $"Info: CancelPrelimMeeting in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
    string.Empty, string.Empty, string.Empty);

            try
            {
                List<int> cancelledAppointmentIds = new List<int>();

                PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();
                cancelledAppointmentIds = bo.CancelPrelimMeeting();

                foreach (int id in cancelledAppointmentIds)
                {
                    PreliminaryMeetingAppointment appt = new PreliminaryMeetingApptModelBO().GetInstance(id);

                    CancelAppointment(appt);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in CancelPrelimMeeting-FunctionAdapter - " + ex.Message;

                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool CancelFacilitatorMeetingAppointment()
        {
            var errorMessage = $"Info: CancelFacilitatorMeetingAppointment in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
    string.Empty, string.Empty, string.Empty);

            try
            {
                List<int> cancelledAppointmentIds = new List<int>();

                FacilitatorMeetingAppointmentBO bo = new FacilitatorMeetingAppointmentBO();
                cancelledAppointmentIds = bo.CancelFacilitatorMeetingAppointment();

                bool cancelledSuccess = false;

                foreach (int id in cancelledAppointmentIds)
                {
                    cancelledSuccess = CancelFacilitatorMeetingAppointmentById(id);
                }

                return cancelledSuccess;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in CancelFacilitatorMeetingAppointment-FunctionAdapter - " + ex.Message;
                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool CancelFacilitatorMeetingAppointmentById(int id)
        {
            try
            {
                FacilitatorMeetingAppointment appt = new FacilitatorMeetingApptModelBO().GetInstance(id);

                CancelAppointment(appt);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelReserveExpressReservation()
        {
            var errorMessage = $"Info: CancelReserveExpressReservation in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
    string.Empty, string.Empty, string.Empty);

            try
            {
                List<int> cancelledAppointmentIds = new List<int>();

                ReserveExpressReservationBO bo = new ReserveExpressReservationBO();
                cancelledAppointmentIds = bo.CancelReserveExpressReservation();

                foreach (int id in cancelledAppointmentIds)
                {
                    ReserveExpressReservation appt = new ReserveExpressReservationModelBO().GetInstance(id);

                    CancelAppointment(appt);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in CancelReserveExpressReservation-FunctionAdapter - " + ex.Message;
                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool CancelMeetingSavedUserSchedules()
        {
            var errorMessage = $"Info: CancelMeetingSavedUserSchedules in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
    string.Empty, string.Empty, string.Empty);

            try
            {
                FacilitatorMeetingAppointmentBO bo = new FacilitatorMeetingAppointmentBO();
                bo.CancelMeetingSavedUserSchedules();

                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in CancelMeetingSavedUserSchedules-FunctionAdapter - " + ex.Message;
                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool CancelSchedulePlanReview()
        {
            var errorMessage = $"Info: CancelSchedulePlanReview in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
    string.Empty, string.Empty, string.Empty);

            try
            {
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                PlanReviewScheduleBOOLD bo = new PlanReviewScheduleBOOLD();
                List<int> projectIds = bo.CancelSchedulePlanReview();

                foreach (int projectID in projectIds)
                {
                    ProjectEstimation pe = estimationCRUDAdapter.GetProjectDetailsByProjectId(projectID);
                    int facilitatorId = pe.AssignedFacilitator.HasValue ? pe.AssignedFacilitator.Value : 0;
                    Facilitator facilitator = new FacilitatorModelBO().GetInstance(facilitatorId);
                    PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();
                    DateTime planReviewStartDate;

                    ProjectCycleSummary projectCycleSummary = new PlanReviewAdapter().GetProjectCycleSummary(projectID);

                    List<PlanReviewScheduleDetail> planReviewSchedules = projectCycleSummary.PlanReviewScheduleDetailsCurrent;

                    planReviewStartDate = planReviewSchedules[0].StartDt.Value;

                    EmailAdapter emailAdapter = new EmailAdapter();
                    EmailMessageBO emailMessageBO = new EmailMessageBO();

                    string projectnumber = pe.AccelaProjectRefId;
                    string projectname = pe.ProjectName;
                    string projectaddress = pe.ProjectAddress;
                    string planreviewstartdate = planReviewStartDate.ToShortDateString();

                    string projectcoordname = facilitator.FirstName + " " + facilitator.LastName;
                    string projectcoordphone = facilitator.Phone;
                    string projectcoordemail = facilitator.Email;
                    string subject = string.Format("Plan review tentatively scheduled date cancelled for Project # {0} ({1})", projectnumber, projectname);
                    //message body
                    string htmlMessageBody = emailMessageBO.CancelPlanReviewScheduledMessageBody(projectnumber, projectname, projectaddress, planreviewstartdate, projectcoordname, projectcoordphone, projectcoordemail);
                    //get mail message defaults
                    MailMessage mailMessage = emailAdapter.GetMailMessage();

                    //send email to customer
                    if (!String.IsNullOrWhiteSpace(pe.PMEmail) && pe.PMEmail.Contains("@"))
                        mailMessage.To.Add(new MailAddress(pe.PMEmail));

                    mailMessage.Subject = subject;
                    mailMessage.Body = htmlMessageBody;

                    emailAdapter.SendEmailMessage(mailMessage);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in CancelSchedulePlanReview-FunctionAdapter - " + ex.Message;
                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        //public bool ValidateSyncAccelaAION(ProjectEstimation projectEstimation)
        //{
        //    bool hasValidDepartments = true;

        //    foreach (var trade in projectEstimation.Trades)
        //    {
        //        if (trade.ID == -1)
        //        {
        //            hasValidDepartments = false;
        //        }
        //    }

        //    foreach (var agency in projectEstimation.Agencies)
        //    {
        //        if (agency.ID == -1)
        //        {
        //            hasValidDepartments = false;
        //        }
        //    }

        //    if (!hasValidDepartments)
        //    {
        //        string errorMessage = "Error processing departments coming from Accela - " + projectEstimation.RecIdTxt;
        //        var logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
        //            string.Empty, string.Empty, string.Empty);
        //    }

        //    bool hasCycleInformation = true;

        //    if (!projectEstimation.IsFifo) // do not validate for FIFO since cycle is generated during Plans Received
        //    {
        //        ProjectCycleBO projectCycleBO = new ProjectCycleBO();
        //        List<ProjectCycleBE> projectCycleBEs = projectCycleBO.GetListByProject(projectEstimation.ID);

        //        if (projectCycleBEs.Count == 0)
        //        {
        //            hasCycleInformation = false;

        //            string errorMessage = "Error in AION due to no cycle record processing - " + projectEstimation.RecIdTxt;
        //            var logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
        //                string.Empty, string.Empty, string.Empty);
        //        }
        //    }

        //    if (hasValidDepartments && hasCycleInformation)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool SyncAccelaAION()
        {
            _accelaFailures = new List<AccelaFailure>();
            Task logging;
            List<AIONQueueRecordBE> queueRecords = new List<AIONQueueRecordBE>();
            List<AIONQueueRecordBE> projectRecords = new List<AIONQueueRecordBE>();
            List<AIONQueueRecordBE> meetingRecords = new List<AIONQueueRecordBE>();
            List<AIONQueueRecordBE> approvedRecords = new List<AIONQueueRecordBE>();
            List<AIONQueueRecordBE> receivedQueueRecords = new List<AIONQueueRecordBE>();

            bool successProjects = false;
            bool successMeetings = false;
            AIONEngineCrudApiBO aIONEngineCrudApiBO = new AIONEngineCrudApiBO();

            try
            {
                //get the project records with status "Received"
                queueRecords = aIONEngineCrudApiBO.GetNewAIONRecordsToProcess();

                receivedQueueRecords = aIONEngineCrudApiBO.GetNewAIONRecordsToProcess(AccelaAionQueueStatus.ReceivedQueue.ToStringValue(), 15);

                queueRecords.AddRange(receivedQueueRecords);

                projectRecords = GetProjectRecordsToProcess(queueRecords);

                //get the approve records to process hours
                approvedRecords = GetApprovedRecordsToProcess(projectRecords);
                bool successPlanReviewUpdates = SyncPlanReviewHours(approvedRecords);

                successProjects = SyncAccelaAIONProjects(projectRecords);

                //get the project records with status "Not Found in Accela"
                queueRecords = aIONEngineCrudApiBO.GetNewAIONRecordsToProcess(AccelaAionQueueStatus.NotFound.ToStringValue(), 15);

                projectRecords = GetProjectRecordsToProcess(queueRecords);

                //get the approve records to process hours
                approvedRecords = GetApprovedRecordsToProcess(projectRecords);
                successPlanReviewUpdates = SyncPlanReviewHours(approvedRecords);

                successProjects = SyncAccelaAIONProjects(projectRecords);

                //get the meetings records with status "Received"
                queueRecords = aIONEngineCrudApiBO.GetNewAIONRecordsToProcess();
                receivedQueueRecords = aIONEngineCrudApiBO.GetNewAIONRecordsToProcess(AccelaAionQueueStatus.ReceivedQueue.ToStringValue(), 15);
                queueRecords.AddRange(receivedQueueRecords);

                meetingRecords = GetMeetingRecordsToProcess(queueRecords);

                successMeetings = SyncAccelaAIONMeetings(meetingRecords);

                //get the meetings records with status "Not Found In Accela"
                queueRecords = aIONEngineCrudApiBO.GetNewAIONRecordsToProcess(AccelaAionQueueStatus.NotFound.ToStringValue(), 15);

                meetingRecords = GetMeetingRecordsToProcess(queueRecords);

                successMeetings = SyncAccelaAIONMeetings(meetingRecords);

                if (_accelaFailures.Count() > 0)
                {
                    new EmailAdapter().SendAccelaIntegrationFailure(_accelaFailures);
                }
            }
            catch (Exception ex)
            {
                //If there is an error status, change Received to error status
                string errorMessage = "Error in SyncAccelaAION - " + ex.Message;
                logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                //send failure email
                new EmailAdapter().SendFunctionAdapterSyncFailure(
                    new Failure
                    {
                        Environment = _aionEnvironment,
                        FailureType = "Error in SyncAccelAION",
                        Message = errorMessage,
                        TimeStamp = DateTime.Now.ToString()
                    });

                return false;
            }

            if (successProjects && successMeetings)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the status incoming from the queue record and checks if it equals checkstatus, if it does, then it returns the new status
        /// otherwise the oldstatus is returned
        /// 
        /// Used to determine if this record has been processed before
        /// </summary>
        /// <param name="checkstatus"></param>
        /// <param name="oldstatus"></param>
        /// <param name="newstatus"></param>
        /// <returns></returns>
        private string GetAccelaAionQueueStatusString(AccelaAionQueueStatus checkstatus, string oldstatus, AccelaAionQueueStatus newstatus)
        {
            if (oldstatus.Equals(checkstatus.ToStringValue()))
            {
                return newstatus.ToStringValue();
            }
            return oldstatus;
        }
        public bool SyncAccelaAIONProjects(List<AIONQueueRecordBE> projectRecords)
        {
            AIONEngineCrudApiBO aIONEngineCrudApiBO = new AIONEngineCrudApiBO();
            EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

            int queueID = 0;
            try
            {
                foreach (AIONQueueRecordBE be in projectRecords)
                {
                    queueID = be.ACCELA_RECEIVED_REC_QUEUE_ID;
                    string oldstatus = be.PROCESS_STATUS_DESC;
                    string statusDesc = be.STATUS_DESC;
                    string queueStatus = GetAccelaAionQueueStatusString(AccelaAionQueueStatus.ReceivedQueue, oldstatus, AccelaAionQueueStatus.SyncErrorProjectError);

                    var inProcessStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.InProcess.ToStringValue());
                    int revertStatus;

                    string projectId = be.REC_ID_NUM;
                    string recIdTxt = be.REC_ID_NUM;

                    AccelaProjectModel project = new AccelaProjectModel();

                    project = estimationAccelaAdapter.GetProjectDetailsLoad(new ProjectParms { ProjectId = projectId, RecIdTxt = recIdTxt });

                    if (project == null)
                    {
                        string errorMessage = "Error processing incoming queue record from Accela - " + be.REC_ID_NUM;
                        var logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);
                        //jcl send email to Accela - build list
                        _accelaFailures.Add(AccelaSyncHelper.GetAccelaFailure(_accelaEnvironment
                            , ""
                            , "Record is not found",
                            "", be.REC_ID_NUM));

                        //jcl if this is the second pass for processing not found records, set it to a different status
                        string notfoundstatus = AccelaAionQueueStatus.NotFound.ToStringValue();

                        notfoundstatus = GetAccelaAionQueueStatusString(AccelaAionQueueStatus.NotFound, oldstatus, AccelaAionQueueStatus.NullProject);

                        revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, notfoundstatus);

                        continue;
                    }

                    //if rtap get the related record from the aion db
                    if (project.IsProjectRTAP)
                    {
                        RecordRelatedModelBE relatedRecords = new EstimationAccelaAdapter().GetRelatedRecord(projectId, RecordRelatedModelBE.RelationshipEnum.Parent);

                        if (relatedRecords == null || string.IsNullOrWhiteSpace(relatedRecords.CustomId))
                        {
                            //jcl send email to Accela - build list
                            _accelaFailures.Add(AccelaSyncHelper.GetAccelaFailure(_accelaEnvironment
                                        , ""
                                        , "RTAP Related Record is not found",
                                        project.ProjectNumber, be.REC_ID_NUM));

                            string errorMessage = "Error processing incoming RTAP from Accela - " + be.REC_ID_NUM;
                            var logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                                string.Empty, string.Empty, string.Empty);

                            revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                            continue;

                        }
                        else
                        {
                            ProjectBE projectBE = new ProjectBO().GetByExternalRefInfo(relatedRecords.CustomId);
                            if (projectBE == null || projectBE.ProjectId == null || projectBE.ProjectId == 0)
                            {
                                string errorMessage = "Error processing incoming RTAP from Accela - " + be.REC_ID_NUM;
                                var logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                                    string.Empty, string.Empty, string.Empty);

                                revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                                continue;
                            }
                            project.AIONOriginalProjectTypeId = projectBE.ProjectTypRefId.Value;
                            project.SquareFootageOfOverallBuilding = projectBE.SquareFootageOfOverallBuildNbr.HasValue ? projectBE.SquareFootageOfOverallBuildNbr.Value.ToString() : "0";
                            project.SquareFootageToBeReviewed = projectBE.SquareFootageToBeReviewedNbr.HasValue ? projectBE.SquareFootageToBeReviewedNbr.Value.ToString() : "0";

                        }
                    }

                    if (project != null)
                    {
                        // determine engine to use based on project - to start FIFO vs. non-FIFO

                        IAccelaSyncEngine engine;

                        if (project.IsFifo)
                        {
                            engine = new FIFOAccelaSyncEngine();
                        }
                        else
                        {
                            engine = new AccelaSyncEngine();
                        }

                        var insert = Task.Run(() => { engine.SaveAccelaProject(project, be); });

                        insert.Wait();

                        if (insert.IsCompleted && !insert.IsCanceled)
                        {
                            ProjectEstimation projectEstimation = estimationCRUDAdapter.GetProjectDetailsForEstimation(project);

                            bool updateProjectStatusSuccess = false;

                            if (be.WORKFLOW_TASK_STATUS == "Awaiting Plans")
                            { 
                                updateProjectStatusSuccess = engine.ProcessAwaitingPlans(projectEstimation);

                                if (!updateProjectStatusSuccess)
                                {
                                    string status = $"Error processing awaiting plans record from Accela - {be.REC_ID_NUM}";
                                    ReportQueueStatus(status, Enums.LoggingType.Information);
                                    revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                                    continue;
                                }

                                var syncValidationAwaitingPlans = ValidateSyncAccelaAION(engine, projectEstimation);

                                if (!syncValidationAwaitingPlans.Item1 && !syncValidationAwaitingPlans.Item2)
                                {
                                    updateProjectStatusSuccess = engine.UpdateProjectStatus(projectEstimation, ProjectStatusEnum.NA);
                                    aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.SyncErrorAIONProjectInvalid.ToStringValue());
                                    continue;
                                }
                            }

                            if (be.STATUS_DESC == "Plans Received")
                            {
                                var syncValidationPlansReceived = ValidateSyncAccelaAION(engine, projectEstimation);

                                if (!syncValidationPlansReceived.Item1 && !syncValidationPlansReceived.Item2)
                                {
                                    updateProjectStatusSuccess = engine.UpdateProjectStatus(projectEstimation, ProjectStatusEnum.NA);
                                    aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.SyncErrorAIONProjectInvalid.ToStringValue());
                                    continue;
                                }

                                if (be.WORKFLOW_TASK_NM == "Application Submittal" && be.WORKFLOW_TASK_STATUS == "Plans Received")
                                {
                                    bool plansReceivedSuccess = false;

                                    plansReceivedSuccess = engine.ProcessPlansReceived(projectEstimation);

                                    if (!plansReceivedSuccess)
                                    {
                                        string status = $"Error processing plans received record from Accela - {be.REC_ID_NUM}";
                                        ReportQueueStatus(status, Enums.LoggingType.Information);
                                        revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (statusDesc.Equals(AccelaAionQueueStatus.PlansReceived.ToStringValue()))
                                    {
                                        aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.PlansReceived.ToStringValue());
                                        continue;
                                    }
                                }
                            }

                            if (be.STATUS_DESC == "In Review" && be.WORKFLOW_TASK_NM == "Gate" && project.IsFifo)
                            {
                                var syncValidationInReview = ValidateSyncAccelaAION(engine, projectEstimation);

                                if (!syncValidationInReview.Item1 && !syncValidationInReview.Item2)
                                {
                                    aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.SyncErrorAIONProjectInvalid.ToStringValue());
                                    continue;
                                }

                                var inReviewSuccess = engine.ProcessInReview(projectEstimation);

                                if (!inReviewSuccess)
                                {
                                    revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.Received.ToStringValue());
                                }
                            }

                            if (be.STATUS_DESC == "Revisions Required")
                            // LES-4403 ONLY process revisions required when the STATUS_DESC field is the one marked as 'Revisions Required'. 
                            // Per Raj, this is when Accela is communicating that all trades are either approved or marked as requiring revisions 
                            // and AION is okay to process at this point. *** SUBJECT TO CHANGE ***
                            {
                                var syncValidationRevisionsRequired = ValidateSyncAccelaAION(engine, projectEstimation);

                                if (!syncValidationRevisionsRequired.Item1 && !syncValidationRevisionsRequired.Item2)
                                {
                                    aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.SyncErrorAIONProjectInvalid.ToStringValue());
                                    continue;
                                }

                                bool revisionsRequiredSuccess = engine.ProcessRevisionsRequired(projectEstimation);

                                if (!revisionsRequiredSuccess)
                                {
                                    string status = $"Error processing revisions required record from Accela - {be.REC_ID_NUM}";
                                    ReportQueueStatus(status, Enums.LoggingType.Information);
                                    revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                                    continue;
                                }
                            }

                            var processedStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.Processed.ToStringValue());
                        }
                        else
                        {
                            revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                        }
                    }
                    else
                    {
                        //If there is an error status, change Received to error status/record not found
                        revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                    }
                    continue;
                }
            }
            catch (Exception ex)
            {
                //If there is an error status, change Received to error status
                if (queueID > 0)
                {
                    var errorStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.Received.ToStringValue());
                }

                string errorMessage = "";
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message + " - " + ex.Message;
                }
                else
                {
                    errorMessage = "Error in SyncAccelaAIONProjects - " + ex.Message;
                }
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, ex);

                //send failure email
                new EmailAdapter().SendFunctionAdapterSyncFailure(
                    new Failure
                    {
                        Environment = _aionEnvironment,
                        FailureType = "Error in SyncAccelaAIONProjects",
                        Message = errorMessage,
                        TimeStamp = DateTime.Now.ToString()
                    });

                return false;
            }

            return true;
        }

        public bool SyncAccelaAIONMeetings(List<AIONQueueRecordBE> meetingRecords)
        {
            Task logging;
            List<AIONQueueRecordBE> queueRecords = new List<AIONQueueRecordBE>();
            AIONEngineCrudApiBO aIONEngineCrudApiBO = new AIONEngineCrudApiBO();

            ProcessAccelaMeetingBO processAccelaMeeting = new ProcessAccelaMeetingBO();
            int queueID = 0;
            try
            {

                foreach (AIONQueueRecordBE be in meetingRecords)
                {
                    int revertStatus;
                    queueID = be.ACCELA_RECEIVED_REC_QUEUE_ID;
                    string oldstatus = be.PROCESS_STATUS_DESC;
                    string queueStatus = GetAccelaAionQueueStatusString(AccelaAionQueueStatus.ReceivedQueue, oldstatus, AccelaAionQueueStatus.SyncErrorProjectError);

                    var inProcessStatus = new AIONEngineCrudApiBO().UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.InProcess.ToStringValue());

                    AccelaAIONMeetingModel meetingModel = ProcessMeetingModel(be);

                    if (meetingModel.MeetingRequest == null || string.IsNullOrWhiteSpace(meetingModel.ProjectId))
                    {
                        //jcl if this is the second pass for processing not found records, set it to a different status
                        string notfoundstatus = AccelaAionQueueStatus.NotFound.ToStringValue();

                        if (oldstatus.Equals(notfoundstatus))
                        {
                            notfoundstatus = AccelaAionQueueStatus.NullProject.ToStringValue();
                        }

                        revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, notfoundstatus);
                        continue;
                    }

                    var meetingProcess = Task.Run(() => processAccelaMeeting.ProcessFacilitatorMeeting(meetingModel));

                    meetingProcess.Wait();

                    if (!meetingProcess.IsCompleted || meetingProcess.IsCanceled || meetingProcess.IsFaulted)
                    {
                        string errorMessage = "Error processing incoming meeting from Accela - " + be.REC_ID_NUM;
                        logging = Logger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);

                        revertStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, queueStatus);
                        continue;
                    }

                    var processedStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.Processed.ToStringValue());
                }
            }
            catch (Exception ex)
            {
                //If there is an error status, change Received to error status
                if (queueID > 0)
                {
                    var errorStatus = aIONEngineCrudApiBO.UpDateQueueRecordStatus(queueID, AccelaAionQueueStatus.Received.ToStringValue());
                }
                string errorMessage = "Error in SyncAccelaAIONMeetings - " + ex.Message;
                logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                //send failure email
                new EmailAdapter().SendFunctionAdapterSyncFailure(
                    new Failure
                    {
                        Environment = _aionEnvironment,
                        FailureType = "Error in SyncAccelaAIONMeetings",
                        Message = errorMessage,
                        TimeStamp = DateTime.Now.ToString()
                    });

                return false;
            }

            return true;
        }

        /// <summary>
        /// LES-187 For these records, since the status sent
        /// indicates that this was completed today
        /// any future user schedules can be removed.
        /// Queue Records for statuses
        /// "Approved" "Approved"
        /// "Approved as Noted" (don't have an example of this one yet)
        /// "Revisions Required" "Approved"
        /// "Project Ended Success"
        /// </summary>
        /// <param name="projectRecord"></param>
        /// <returns></returns>
        public bool SyncPlanReviewHours(List<AIONQueueRecordBE> projectRecords)
        {

            try
            {
                //TODO: add in a save for the actual hoursSpent LES-3873

                string recIdCsv = string.Join(",", projectRecords.Select(x => x.REC_ID_NUM).ToList());

                string receiveddate = projectRecords.Select(x => x.RECEIVED_DT.Value.ToShortDateString()).FirstOrDefault();
                int rows = new UserScheduleBO().DeleteUserSchedulesByRecIDAfterDate(recIdCsv, receiveddate);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SyncPlanReviewHours - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                //send failure email
                new EmailAdapter().SendFunctionAdapterSyncFailure(
                    new Failure
                    {
                        Environment = _aionEnvironment,
                        FailureType = "Error in SyncPlanReviewHours",
                        Message = errorMessage,
                        TimeStamp = DateTime.Now.ToString()
                    });
                return false;
            }

            return true;
        }

        public List<AIONQueueRecordBE> GetMeetingRecordsToProcess(List<AIONQueueRecordBE> queueRecords)
        {
            List<AIONQueueRecordBE> meetingRecords = new List<AIONQueueRecordBE>();

            meetingRecords = queueRecords
                .Where(x => x.REC_TYP_DESC == "Meeting Request" || x.WORKFLOW_TASK_STATUS == "Meeting Requested")
                .ToList();

            return meetingRecords;
        }

        public List<AIONQueueRecordBE> GetProjectRecordsToProcess(List<AIONQueueRecordBE> queueRecords)
        {
            List<AIONQueueRecordBE> projectRecords = new List<AIONQueueRecordBE>();

            projectRecords = queueRecords
                .Where(x => x.REC_TYP_DESC != "Meeting Request" && x.WORKFLOW_TASK_STATUS != "Meeting Requested")
                .ToList();

            return projectRecords;
        }

        /// <summary>
        /// Queue records have already been selected to be only project records, excludes meeting records
        /// </summary>
        /// <param name="projectRecords"></param>
        /// <returns></returns>
        public List<AIONQueueRecordBE> GetApprovedRecordsToProcess(List<AIONQueueRecordBE> projectRecords)
        {


            List<AIONQueueRecordBE> approvedRecords = projectRecords
                .Where(x => x.STATUS_DESC == "Approved" && x.WORKFLOW_TASK_STATUS == "Approved")
                .ToList();

            List<AIONQueueRecordBE> projectEndedRecords = projectRecords
                .Where(x => x.STATUS_DESC == "Project Ended - Success" && x.WORKFLOW_TASK_STATUS == "Complete")
                .ToList();
            List<AIONQueueRecordBE> projectClosureRecords = projectRecords
                .Where(x => x.STATUS_DESC == "Project Ended - Success" && x.WORKFLOW_TASK_STATUS == "Closure")
                .ToList();

            List<AIONQueueRecordBE> projectclosedRecords = projectRecords
                .Where(x => x.STATUS_DESC == "Closure" && x.WORKFLOW_TASK_STATUS == "Complete")
                .ToList();

            List<AIONQueueRecordBE> allRecords = new List<AIONQueueRecordBE>();
            allRecords.AddRange(approvedRecords);
            allRecords.AddRange(projectEndedRecords);
            allRecords.AddRange(projectclosedRecords);
            allRecords.AddRange(projectClosureRecords);

            return allRecords;
        }
        public AccelaAIONMeetingModel ProcessMeetingModel(AIONQueueRecordBE be)
        {
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

            string recIdTxt = be.REC_ID_NUM;
            string projectId = be.REC_ID_NUM;
            string meetingProjectId = string.Empty;

            AccelaProjectModel meetingProject =
                estimationAccelaAdapter.GetProjectDetailsLoad(new ProjectParms { ProjectId = projectId, RecIdTxt = recIdTxt });

            if (meetingProject == null)
            {
                //jcl send email to Accela - build list
                _accelaFailures.Add(AccelaSyncHelper.GetAccelaFailure(_accelaEnvironment
                    , ""
                    , "Meeting Project Record is not found",
                    "", be.REC_ID_NUM));

            }

            if (be.REC_TYP_DESC == "Meeting Request")
            {
                RecordRelatedModelBE relatedRecord = estimationAccelaAdapter.GetRelatedRecord(recIdTxt, RecordRelatedModelBE.RelationshipEnum.Parent);
                if (relatedRecord == null)
                {
                    //jcl send email to Accela - build list
                    _accelaFailures.Add(AccelaSyncHelper.GetAccelaFailure(_accelaEnvironment
                        , ""
                        , "Meeting Request Related Record is not found",
                        meetingProject.ProjectNumber, be.REC_ID_NUM));
                }
                else
                {
                    meetingProjectId = relatedRecord.CustomId;
                }

            }
            else
            {
                if (meetingProject != null && !string.IsNullOrWhiteSpace(meetingProject.ProjectNumber))
                {
                    meetingProjectId = meetingProject.ProjectNumber;
                }
            }

            AccelaAIONMeetingModel meetingModel = new AccelaAIONMeetingModel()
            {
                AIONQueueRecordBE = be,
                MeetingRequest = meetingProject,
                ProjectId = meetingProjectId
            };

            return meetingModel;
        }

        #region Queue record workfkow task status processing
        //public bool ProcessFifoDueDate(string recordId, string projectNumber, int cycleNumber)
        //{
        //    AccelaFifoDueDateBO processFifoDueDate = new AccelaFifoDueDateBO();
        //    EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

        //    var accelaRecordModel = estimationAccelaAdapter.GetProjectDetails(new ProjectParms { ProjectId = recordId, RecIdTxt = recordId });

        //    var mapresult = estimationAccelaAdapter.GetAccelaAIONMapByAccelaRecordType(accelaRecordModel.result[0].ParseRecType);
        //    mapresult.Wait();
        //    var mAccelaAONMap = mapresult.Result;

        //    DateTime? fifoDueDate = processFifoDueDate.GetFifoDueDateFromCustomTable(accelaRecordModel, mAccelaAONMap, cycleNumber);

        //    if (fifoDueDate != null)
        //    {
        //        ProjectBO projectBO = new ProjectBO();
        //        ProjectBE projectBE = new ProjectBO().GetByExternalRefInfo(projectNumber);
        //        projectBE.FifoDueAccelaDt = fifoDueDate;
        //        projectBO.Update(projectBE);

        //        return true;
        //    }

        //    return false;
        //}

        //public bool ProcessPlanReviewCycle(ProjectEstimation projectEstimation, bool hasRevisions)
        //{
        //    ProcessPlanReviewCycleBO processPlanReviewCycle = new ProcessPlanReviewCycleBO();

        //    return processPlanReviewCycle.ProcessPlanReviewCycle(projectEstimation, hasRevisions);
        //}

        //public bool ProcessFIFOCycle(ProjectEstimation projectEstimation)
        //{
        //    bool processCycle = ProcessPlanReviewCycle(projectEstimation, false);

        //    ProjectBO projectBO = new ProjectBO();

        //    // get updated cycle from BO
        //    ProjectBE projectBE = projectBO.GetById(projectEstimation.ID);
        //    projectEstimation.CycleNbr = projectBE.CycleNbr;

        //    if (projectBE.CycleNbr.Value > 1)
        //    {
        //        DateTime newFifoDueDate = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 7);
        //        projectEstimation.FifoDueDt = newFifoDueDate;
        //        projectBE.FifoDueDt = newFifoDueDate;
        //        projectBO.Update(projectBE);
        //    }

        //    var fifoProcess = Task.Run(() => new FIFOAdapter().ProcessFIFO(projectEstimation));

        //    fifoProcess.Wait();

        //    if (!fifoProcess.IsCompleted || fifoProcess.IsCanceled || fifoProcess.IsFaulted)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        #endregion

        public bool UpdatePlanReviewStatusByAccela()
        {
            var errorMessage = $"Info: CancelMeetingSavedUserSchedules in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
    string.Empty, string.Empty, string.Empty);

            try
            {

                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in UpdatePlanReviewStatusByAccela-FunctionAdapter - " + ex.Message;
                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool CancelAppointment(Appointment appointment)
        {
            if (appointment.GetType() == typeof(FacilitatorMeetingAppointment))
            {
                FacilitatorMeetingAppointment fma = (FacilitatorMeetingAppointment)appointment;
                IAppointmentAdapter adapter = new FMAAdapter(fma);
                adapter.CancelAppointment();
            }

            //TODO: update so that it cancels the Plan Review Schedule - not sure this is called from anywhere???
            if (appointment.GetType() == typeof(ExpressMeetingAppointment))
            {
                ExpressMeetingAppointment ema = (ExpressMeetingAppointment)appointment;
                IAppointmentAdapter adapter = new EMAAdapter(ema);
                adapter.CancelAppointment();
            }

            if (appointment.GetType() == typeof(PreliminaryMeetingAppointment))
            {
                PreliminaryMeetingAppointment pma = (PreliminaryMeetingAppointment)appointment;
                IAppointmentAdapter adapter = new PMAAdapter(pma);
                adapter.CancelAppointment();
            }

            if (appointment.GetType() == typeof(ReserveExpressReservation))
            {
                ReserveExpressReservation exp = (ReserveExpressReservation)appointment;
                IAppointmentAdapter adapter = new ExpressAdapter(exp);
                adapter.CancelAppointment();
            }

            return true;
        }

        public bool ProcessCalendarEventQueueRecords(bool inProcess, string environment)
        {
            var errorMessage = $"Info: ProcessCalendarEventQueueRecords in FunctionAdapter executed at: {DateTime.Now}";
            var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

            try
            {
                ICalendarEventAdapter calendarEventAdapter = new CalendarEventAdapter();
                List<CalendarEventBE> calendarEventBEs = calendarEventAdapter.GetCalendarEvents(inProcess);

                foreach (CalendarEventBE calendarEvent in calendarEventBEs)
                {
                    OutlookCalendarAppointment appointment =
                        JsonConvert.DeserializeObject<OutlookCalendarAppointment>(calendarEvent.JsonObjectTxt);

                    try
                    {
                        Enum.TryParse(calendarEvent.ActionDesc, out CalendarAppointmentAction actionEnum);

                        bool eventFullyProcessed = true;

                        switch (actionEnum)
                        {
                            case CalendarAppointmentAction.Create:
                                foreach (UserOutlookDetail userOutlookDetail in appointment.UserOutlookDetails)
                                {
                                    bool userExists = CheckIfUserCalendarExistsInOutlook(userOutlookDetail);

                                    if (userExists)
                                    {
                                        string eventId = AddOutlookCalendarAppointment(appointment, userOutlookDetail);

                                        if (!string.IsNullOrEmpty(eventId))
                                        {
                                            userOutlookDetail.IsProcessed = true;
                                        }
                                        else
                                        {
                                            eventFullyProcessed = false;
                                        }
                                    }
                                    else
                                    {
                                        eventFullyProcessed = true; // do not fail if user does not exist in MS Graph
                                    }
                                }
                                break;

                            case CalendarAppointmentAction.Delete:
                                foreach (UserOutlookDetail userOutlookDetail in appointment.UserOutlookDetails)
                                {
                                    bool outlookAppointmentDeleted =
                                        RemoveOutlookCalendarAppointment(
                                            userOutlookDetail,
                                            appointment.Event.TransactionId,
                                            appointment.StartDate,
                                            appointment.EndDate);
                                }
                                break;

                        }
                        calendarEvent.ProcessedInd = eventFullyProcessed;
                        calendarEvent.InProcessInd = eventFullyProcessed == false ? true : false;
                        calendarEvent.ProcessedDate = DateTime.Now;
                        calendarEvent.UserId = environment;

                        if (!eventFullyProcessed)
                        {
                            calendarEvent.InProcessDate = DateTime.Now;

                            if (inProcess)
                            {
                                calendarEvent.RetryCount += 1;
                            }
                        }
                    }
                    catch
                    {
                        calendarEvent.ProcessedInd = false;
                        calendarEvent.InProcessInd = true;
                        calendarEvent.ProcessedDate = DateTime.Now;
                        calendarEvent.InProcessDate = DateTime.Now;
                    }
                    finally
                    {
                        calendarEvent.JsonObjectTxt = JsonConvert.SerializeObject(appointment);
                        SaveCalendarQueueRecord(calendarEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error in ProcessCalendarEventQueueRecords FunctionAdapter - " + ex.Message;

                var loggingex = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return true;
        }

        public List<int> GetFIFOProjectIdsToBeOptimized()
        {
            FIFOAdapter fifoAdapter = new FIFOAdapter();

            List<ProjectBE> projectBEs = fifoAdapter.GetProjectListByFIFODueDate();

            DateTime sevenWorkdaysInFuture = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 7);

            List<int> projectIdList = projectBEs.Where(x => x.FifoDueDt.Value.Date <= sevenWorkdaysInFuture.Date).Select(x => x.ProjectId.Value).ToList();

            return projectIdList;
        }

        public bool OptimizeFIFOProject(int projectId)
        {
            try
            {
                PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                FIFOAdapter fifoAdapter = new FIFOAdapter();

                // get current cycle by project
                List<ProjectCycle> projectCycles = planReviewAdapter.GetProjectCyclesByProjectId(projectId);
                ProjectCycle projectCycle = projectCycles.FirstOrDefault(x => x.CurrentCycleInd == true);

                if (projectCycle != null)
                {
                    List<PlanReview> planReviews = planReviewAdapter.GetPlanReviewsByProjectCycle(projectCycle.ID);
                    PlanReview fifoSchedule = planReviews.FirstOrDefault();

                    bool hasActiveSchedules = fifoAdapter.HasActiveSchedules(fifoSchedule);

                    if (hasActiveSchedules)
                    {
                        ProjectEstimation currentProject = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(projectId);

                        if (currentProject != null)
                        {
                            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

                            string accelaWorkFlowTaskStatus = estimationAccelaAdapter.GetAccelaWorkflowTaskStatusFromQueueTable(currentProject.RecIdTxt);

                            FIFOEngineParams data = new FIFOEngineParams()
                            {
                                CurrentProject = currentProject,
                                PlanReview = fifoSchedule,
                                AccelaWorkflowTaskStatus = accelaWorkFlowTaskStatus
                            };

                            FIFOOptimizationEngine engine = new FIFOOptimizationEngine(data);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in OptimizeFIFOAssignments FunctionAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gets newly eligible users and adds them to NPAs that indicate all users 
        /// by type (BEMPFZetc) or indicates all users
        /// </summary>
        /// <param name="inProcess"></param>
        /// <returns>bool indicates success</returns>
        public bool AddEligibleUsersToExistingNPAs(bool inProcess)
        {
            try
            {
                bool success = false;
                //UserDepartmentXref item
                UserBusinessRelationshipBO bo = new UserBusinessRelationshipBO();
                //get the list of users and dept that need npas processed
                List<UserBusinessRelationshipBE> userbusinessrelationships = bo.GetProcessNpaInd();
                INPAAccessor npaAccessor = new NPAAccessor();
                success = npaAccessor.ProcessNpas(userbusinessrelationships, 1, DateTime.Now);
                //update process_npa_ind to false
                bo.UpdateProcessNpaToFalse();
                return success;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AddEligibleUsersToExistingNPAs FunctionAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        private bool CheckIfUserCalendarExistsInOutlook(UserOutlookDetail userOutlookDetail)
        {
            try
            {
                OutlookAdapter outlookAdapter = new OutlookAdapter();

                string defaultCalendar = outlookAdapter.GetUserDefaultCalendar(userOutlookDetail.UserPrincipalName);

                if (string.IsNullOrWhiteSpace(defaultCalendar))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CheckIfUserCalendarExistsInOutlook FunctionAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        private string AddOutlookCalendarAppointment(OutlookCalendarAppointment appointment, UserOutlookDetail userOutlookDetail)
        {
            try
            {
                OutlookAdapter outlookAdapter = new OutlookAdapter();

                return outlookAdapter.AddEventForUser(
                    appointment,
                    userOutlookDetail.UserPrincipalName,
                    userOutlookDetail.CalendarId);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AddOutlookAppointment FunctionAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        private bool RemoveOutlookCalendarAppointment(UserOutlookDetail userOutlookDetail, string transactionId, DateTime start, DateTime end)
        {
            bool success = false;
            try
            {
                OutlookAdapter outlookAdapter = new OutlookAdapter();

                Event evt = outlookAdapter.SearchForEventForUserCalendar(userOutlookDetail.UserPrincipalName,
                    userOutlookDetail.CalendarId,
                    transactionId, start, end);

                if (evt != null)
                {
                    success = outlookAdapter.DeleteEventForUser(
                        userOutlookDetail.UserPrincipalName, userOutlookDetail.CalendarId, evt.Id);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in RemoveOutlookCalendarAppointment FunctionAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return success;
        }

        private bool SaveCalendarQueueRecord(CalendarEventBE calendarEvent)
        {
            bool success = false;
            try
            {
                ICalendarEventAdapter calendarEventQueueAdapter = new CalendarEventAdapter();
                calendarEventQueueAdapter.UpdateCalendarEvent(calendarEvent);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveCalendarQueueRecord FunctionAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return success;
        }

        private Tuple<bool,bool> ValidateSyncAccelaAION(IAccelaSyncEngine engine, ProjectEstimation projectEstimation)
        {
            bool syncDepartments = engine.ValidateSyncAccelaAIONDepartments(projectEstimation);
            bool syncCyleInfo = engine.ValidateSyncAccelaAIONCycleInfo(projectEstimation);

            if (!syncDepartments)
            {
                string status = $"Error processing departments coming from Accela - {projectEstimation.RecIdTxt}";
                ReportQueueStatus(status, Enums.LoggingType.Information);
            }

            if (!syncCyleInfo)
            {
                string status = $"Error in AION due to no cycle record processing - {projectEstimation.RecIdTxt}";
                ReportQueueStatus(status, Enums.LoggingType.Information);
            }

            return new Tuple<bool, bool>(syncDepartments, syncCyleInfo);
        }

        private void ReportQueueStatus(string status, Meck.Logging.Enums.LoggingType loggingType)
        {
            var logging = Logger.LogMessageAsync(loggingType, MethodBase.GetCurrentMethod(), status,
            string.Empty, string.Empty, string.Empty);
        }
    }

    public interface IFunctionAdapter
    {
        bool CancelScheduledExpressPlanReview();
        bool CancelPrelimMeeting();
        bool CancelMeetingSavedUserSchedules();
        bool CancelSchedulePlanReview();
        bool CancelFacilitatorMeetingAppointment();
        bool CancelFacilitatorMeetingAppointmentById(int id);
        bool CancelReserveExpressReservation();
        bool UpdatePlanReviewStatusByAccela();
        bool CancelAppointment(Appointment appt);
        bool ProcessCalendarEventQueueRecords(bool inProcess, string environment);
        List<int> GetFIFOProjectIdsToBeOptimized();
        bool OptimizeFIFOProject(int projectId);
        bool AddEligibleUsersToExistingNPAs(bool inProcess);
        bool SyncAccelaAION();
    }
}
