using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Models;
using AION.Web.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    public class BaseProjectDetailController : BaseControllerWeb
    {
        public ActionResult UpdateProjectCyclePROD(int projectId, int projectCycleId, int planReviewScheduleId, int response, string prod)
        {
            UpdateProjectCycleInformationPROD(projectCycleId, prod);

            return RedirectToAction("PlanReviews", new { projectId = projectId, canEditPlansReadyOnDate = true });
        }

        public ActionResult ProjectAudits(int projectId)
        {
            APIHelper apihelper = new APIHelper();

            List<ProjectAudit> projectAudits = apihelper.GetProjectAudits(projectId);

            return PartialView("_ProjectAudit", projectAudits);
        }

        public ActionResult GetProjectStatus(int projectId)
        {
            APIHelper apiHelper = new APIHelper();

            ProjectEstimation project = apiHelper.GetProjectDetailsByProjectId(projectId);

            return PartialView("_ProjectStatusLabel", project.AIONProjectStatus.ProjectStatusEnum);
        }

        protected void UpdateProjectCycleInformation(int projectCycleId, int planReviewScheduleId, int response, string prod, int loggedInUserId, bool byCustomer = false)
        {
            APIHelper apihelper = new APIHelper();
            ProjectStatus status = new ProjectStatus();
            //TODO: this is in the session, don't need to make this call
            UserIdentity loggedInUser = apihelper.GetUserIdentityByID(loggedInUserId);

            // get project cycle
            ProjectCycle projectCycle = apihelper.GetProjectCycleById(projectCycleId);
            projectCycle.IsResponderCustomer = byCustomer;

            bool? isApproved = false;

            // get plan review
            List<PlanReview> planReviews = apihelper.GetPlanReviewsByProjectCycleId(projectCycleId);
            PlanReview planReview = planReviews.FirstOrDefault(x => x.PlanReviewScheduleId == planReviewScheduleId);

            if (planReview != null)
            {
                planReview.SendEmail = true;
               
                if (planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled)
                {
                    planReview.IsReschedule = true;
                }
            }

            // get project estimation
            ProjectEstimation projectEstimation = apihelper.GetProjectDetailsByProjectId(projectCycle.ProjectId.Value);
            projectEstimation.UpdatedUser = loggedInUser;
            List<ProjectStatus> projectStatusBaseList = apihelper.GetProjectStatusBaseList();

            if (response > 0)
            {
                switch (response)
                {
                    case 0:

                        break;
                    case (int)PlanReviewResponseStatusEnum.Accept:
                        // project status
                        status = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.Scheduled).FirstOrDefault();

                        isApproved = true;

                        // plan review status
                        planReview.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Scheduled;

                        projectEstimation.AIONProjectStatus = status;

                        break;
                    case (int)PlanReviewResponseStatusEnum.Reject:
                        // project status
                        status = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.Scheduled).FirstOrDefault();

                        ProjectStatus prodNotKnownStatus = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.PROD_Not_Known).FirstOrDefault();

                        isApproved = false;

                        // plan review status
                        planReview.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled;

                        if (!planReview.IsReschedule)
                        {

                            if (!string.IsNullOrWhiteSpace(prod))
                            {
                                projectEstimation.AIONProjectStatus = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.Not_Scheduled).FirstOrDefault();
                            }
                            else
                            {
                                projectEstimation.AIONProjectStatus = prodNotKnownStatus;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                isApproved = null;

                ProjectStatus prodNotKnownStatus = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.PROD_Not_Known).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(prod))
                {
                    if (planReview != null)
                    {
                        planReview.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled;
                    }
                    projectEstimation.AIONProjectStatus = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.Not_Scheduled).FirstOrDefault();
                }
                else
                {
                    projectEstimation.AIONProjectStatus = prodNotKnownStatus;
                }
            }

            // update project cycle if not rejecting a reschedule

            if (response == (int)PlanReviewResponseStatusEnum.Reject && planReview.IsReschedule)
            {
                // do nothing
            }
            else
            {
                DateTime? newProd = !string.IsNullOrWhiteSpace(prod) ? DateTime.Parse(prod) : (DateTime?)null;

                projectCycle.PlansReadyOnDt = newProd;
                projectCycle.UpdatedUser = loggedInUser;
                projectCycle.IsAprvInd = isApproved;
                projectCycle.ResponderUserId = loggedInUserId;
                projectCycle.ResponseDt = DateTime.Now;

                string responder = projectCycle.IsResponderCustomer ? "by customer" : "on behalf of customer";

                //per LES-4831 Nathan's response: "No email for rejected on behalf of customer"
                if (responder.Equals("on behalf of customer")) { planReview.SendEmail = false; }
                string auditResponse = string.Empty;

                if (isApproved.HasValue)
                {
                    auditResponse = projectCycle.IsAprvInd.Value ? "accepted" : "rejected";

                    projectCycle.AuditText = $"Plan Review Date {auditResponse} {responder} {planReview.ScheduleDate}";
                }

                apihelper.UpdateProjectCycle(projectCycle);

                apihelper.UpdateProjectDetails(projectEstimation.ID, projectEstimation.AIONProjectStatus.ID, projectEstimation.UpdatedUser.ID, newProd);
            }

            if (planReview != null)
            {
                planReview.ProjectCycle = projectCycle;  // assign the cycle values to be updated to the plan review's cycle

                if ((int)planReview.ApptResponseStatusEnum > 0)
                {
                    apihelper.UpdatePlanReviewStatus(planReview, planReview.ApptResponseStatusEnum);

                    if (planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled) AccelaScheduleAPIHelper.AccelaSchedulePlanReview(planReview);
                }
            }
        }

        protected void UpdateProjectCycleInformationPROD(int projectCycleId, string prod)
        {
            APIHelper apihelper = new APIHelper();
            ProjectStatus status = new ProjectStatus();

            UserIdentity loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

            // get project cycle
            ProjectCycle projectCycle = apihelper.GetProjectCycleById(projectCycleId);

            // get project estimation
            ProjectEstimation projectEstimation = apihelper.GetProjectDetailsByProjectId(projectCycle.ProjectId.Value);
            projectEstimation.UpdatedUser = loggedInUser;
            List<ProjectStatus> projectStatusBaseList = apihelper.GetProjectStatusBaseList();

            ProjectStatus prodNotKnownStatus = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.PROD_Not_Known).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(prod))
            {
                projectEstimation.AIONProjectStatus = projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum.Not_Scheduled).FirstOrDefault();
            }
            else
            {
                projectEstimation.AIONProjectStatus = prodNotKnownStatus;
            }

            // update project cycle
            DateTime? newProd = !string.IsNullOrWhiteSpace(prod) ? DateTime.Parse(prod) : (DateTime?)null;

            projectCycle.PlansReadyOnDt = newProd;
            projectCycle.UpdatedUser = loggedInUser;

            apihelper.UpdateProjectCycle(projectCycle);

            apihelper.UpdateProjectDetails(projectEstimation.ID, projectEstimation.AIONProjectStatus.ID, projectEstimation.UpdatedUser.ID, newProd);
        }

        /// <summary>
        /// Called upon initial page load
        /// </summary>
        /// <param name="project"></param>
        /// <param name="projectCycleSummary"></param>
        /// <param name="canEditPlansReadyOnDate"></param>
        /// <param name="loggedInUser"></param>
        /// <returns></returns>
        protected List<PlanReviewPartialViewModel> GetPlanReviewsModel(
            ProjectEstimation project, 
            List<ProjectCycleReview> projectCycleReviews, 
            bool canEditPlansReadyOnDate, 
            UserIdentity loggedInUser)
        {
            List<PlanReviewPartialViewModel> planReviewViewModels = new List<PlanReviewPartialViewModel>();

            //Process responses to plan reviews
            foreach (ProjectCycleReview projectCycleReview in projectCycleReviews)
            {
                PlanReviewPartialViewModel cycle = new PlanReviewPartialViewModel();

                foreach (PlanReview planReview in projectCycleReview.PlanReviews)
                {
                    cycle = new PlanReviewPartialViewModel(
                    project,
                    projectCycleReview.ProjectCycle,
                    planReview,
                    canEditPlansReadyOnDate,
                    loggedInUser);

                    planReviewViewModels.Add(cycle);
                }

                if (projectCycleReview.PlanReviews.Count == 0)
                {
                    cycle = new PlanReviewPartialViewModel(
                    project,
                    projectCycleReview.ProjectCycle,
                    null,
                    canEditPlansReadyOnDate,
                    loggedInUser);

                    planReviewViewModels.Add(cycle);
                }
            }

            return planReviewViewModels;
        }

        /// <summary>
        /// Called on update and refresh to the partial view
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="canEditPlansReadyOnDate"></param>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        protected List<PlanReviewPartialViewModel> GetPlanReviewsModel(int projectId, bool canEditPlansReadyOnDate)
        {
            APIHelper apiHelper = new APIHelper();

            UserIdentity loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

            List<PlanReviewPartialViewModel> planReviewViewModels = new List<PlanReviewPartialViewModel>();

            //Process responses to plan reviews
            List<ProjectCycleReview> projectCycleReviews = apiHelper.GetProjectCycleReviews(projectId);

            ProjectEstimation project = apiHelper.GetProjectDetailsByProjectId(projectId);

            foreach (ProjectCycleReview projectCycleReview in projectCycleReviews)
            {
                foreach (PlanReview planReview in projectCycleReview.PlanReviews)
                {
                    PlanReviewPartialViewModel cycle = new PlanReviewPartialViewModel(
                    project,
                    projectCycleReview.ProjectCycle,
                    planReview,
                    canEditPlansReadyOnDate,
                    loggedInUser);

                    planReviewViewModels.Add(cycle);
                }
            }

            return planReviewViewModels;
        }

        protected ScheduledMeetingsListViewModel GetScheduledMeetingsModel(string projectId, int loggedInUserId)
        {
            APIHelper apiHelper = new APIHelper();
            ScheduledMeetingsListViewModel scheduledMeetingsListViewModel = new ScheduledMeetingsListViewModel();
            List<ScheduledMeetingPartialViewModel> scheduledMeetingsModel = new List<ScheduledMeetingPartialViewModel>();

            CustomerMeetingsList customerMeetingsList = apiHelper.GetScheduledMeetingDetailsByProjectId(projectId);
            List<CustSchedMeeting> custSchedMeetings = customerMeetingsList.CustSchedMeetings;


            foreach (CustSchedMeeting scheduledMeeting in custSchedMeetings)
            {
                ScheduledMeetingPartialViewModel meeting = new ScheduledMeetingPartialViewModel();
                meeting.ProjectId = projectId;
                meeting.AgendaDue = scheduledMeeting.AgendaDue;
                meeting.AppointmentCancellationEnum = scheduledMeeting.AppointmentCancellationEnum;
                meeting.AppointmentResponseStatusEnum = scheduledMeeting.AppointmentResponseStatusEnum;
                meeting.ApptResponseStatusId = scheduledMeeting.ApptResponseStatusId;
                meeting.MeetingDate = scheduledMeeting.MeetingDate;
                meeting.MeetingId = scheduledMeeting.MeetingId;
                meeting.MeetingTime = scheduledMeeting.MeetingTime;
                meeting.MeetingTypeEnum = scheduledMeeting.MeetingTypeEnum;
                meeting.ResponseDue = scheduledMeeting.ResponseDue;
                meeting.StatusLabel = $"{scheduledMeeting.AppointmentResponseStatusEnum.ToStringValue()} - {meeting.AppointmentCancellationEnum?.ToStringValue()}";
                meeting.LoggedInUserId = loggedInUserId;
                meeting.Attendees = ScheduleHelpers.BuildListFromAttendeeInfo(scheduledMeeting.Attendees);
                scheduledMeetingsModel.Add(meeting);
            }
            scheduledMeetingsListViewModel.ScheduledMeetingPartialViewModels = scheduledMeetingsModel;
            scheduledMeetingsListViewModel.ProjectStatus = customerMeetingsList.ProjectStatus;
            return scheduledMeetingsListViewModel;
        }
    }
}