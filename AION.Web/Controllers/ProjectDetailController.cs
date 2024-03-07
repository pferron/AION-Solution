using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Models;
using AION.Web.Models.ProjectDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class ProjectDetailController : BaseProjectDetailController
    {
        public ActionResult PlanReviews(int projectId, bool canEditPlansReadyOnDate)
        {
            List<PlanReviewPartialViewModel> planReviewViewModels = GetPlanReviewsModel(projectId, canEditPlansReadyOnDate);

            return PartialView("_PlanReviews", planReviewViewModels);
        }

        public ActionResult UpdateProjectCycle(int projectId, int projectCycleId, int planReviewScheduleId, int response, string prod)
        {
            UserIdentity loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

            UpdateProjectCycleInformation(projectCycleId, planReviewScheduleId, response, prod, loggedInUser.ID, false);

            return RedirectToAction("PlanReviews", new { projectId = projectId, canEditPlansReadyOnDate = true });
        }

        public ActionResult AssignedFacilitator(int facilitatorId)
        {
            string facilitatorName = GetUserFNLM(facilitatorId);

            AssignedFacilitatorViewModel model = new AssignedFacilitatorViewModel()
            {
                FacilitatorId = facilitatorId,
                FacilitatorName = facilitatorName
            };

            return PartialView("_AssignedFacilitator", model);
        }

        public ActionResult ProjectDetail(ProjectParms projectparms)
        {
            ProjectDetailAPIHelper projectDetailAPIHelper = new ProjectDetailAPIHelper();

            ProjectDetailModel model = projectDetailAPIHelper.GetProjectDetailModel(projectparms);

            ProjectDetailViewModel vm = new ProjectDetailViewModel();

            projectparms.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = projectparms.LoggedInUserEmail;
            if (string.IsNullOrWhiteSpace(projectparms.LoggedInUserEmail))
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }
            else
            {
                vm.LoggedInUserEmail = projectparms.LoggedInUserEmail;
            }

            UpdateUserAndPermissions(vm);

            if (projectparms.ProjectId == null)
            {
                projectparms.StatusMessage = "Project ID is null";
                //redirect to home
                return RedirectToAction("Index", "Home", new { StatusMessage = "Project ID is null" });
            }

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            vm.Project = model.ProjectEstimation;
            vm.Project.Notes = model.Notes;
            vm.ProjectAudits = model.ProjectAudits;
            vm.AuditActionRefs = model.AuditActionRefs;
            vm.AuditLogs = new List<TableAuditLog>();
            vm.MeetingRoomList = model.MeetingRooms;
            vm.FacilitatorList = model.Facilitators;
            vm.ProjectCycleReviews = model.ProjectCycleReviews;

            vm.PRResponse = new int?();

            vm.ProdNotKnown = vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.PROD_Not_Known ? true : false;
            vm.IsAbort = vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Abort_Package ? true : false;

            vm.ScheduledMeetingList = model.ScheduledMeetings;

            //Generate deeplink to accela
            vm.AccelaProjectDeeplink = GenerateAccelaDeeplink(vm.Project.AccelaProjectRefId);

            //get the project notifications for the _SendNotifications modal
            vm.ProjectNotifs = GetProjectNotifs(model.ProjectNotificationEmails);

            bool cycleHasPROD = false;

            if (vm.ProjectCycleReviews.Count > 0)
            {
                var currentCycle = vm.ProjectCycleReviews.FirstOrDefault(x => x.ProjectCycle.CurrentCycleInd.Value == true);

                if (currentCycle != null)
                {
                    if (currentCycle.ProjectCycle.PlansReadyOnDt != null)
                    {
                        cycleHasPROD = true;
                    }

                    if (currentCycle.PlanReviews != null && currentCycle.PlanReviews.Count > 0)
                    {
                        foreach (PlanReview planReview in currentCycle.PlanReviews)
                        {
                            vm.PlanReviewDate = planReview.StartDate;

                            if (planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled ||
                                planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled)
                            {
                                vm.RescheduleExpressReviewDisabled = "disabled";
                            }
                        }
                    }
                }
            }

            vm.PlanReviewViewModels =
                 GetPlanReviewsModel(model.ProjectEstimation, vm.ProjectCycleReviews, vm.PermissionMapping.E_Plns_Rdy_Dt, vm.LoggedInUser);


            if (vm.PlanReviewDate.HasValue) vm.RescheduleWarning = RescheduleWarning(DateTime.Now, vm.PlanReviewDate.Value);

            // cases where Schedule Plan Review needs to be disabled
            //check project status
            if ((vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Not_Scheduled
                || vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Tentatively_Scheduled
                || vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Scheduled)
                && cycleHasPROD)
            {
                vm.ReschedulePlanReviewDisabled = "";
            }

            //check if express or prelim or cycle missing PROD
            if (vm.Project.IsProjectPreliminary
                || vm.Project.AionPropertyType == PropertyTypeEnums.Express)
            {
                vm.ReschedulePlanReviewDisabled = "disabled";
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult ChangeFacilitator(ChangeFacilitatorModel model)
        {
            // update project with new facilitator
            APIHelper apiHelper = new APIHelper();
            ProjectEstimation project = apiHelper.GetProjectDetailsByProjectId(model.ProjectId);
            project.AssignedFacilitator = model.FacilitatorId;

            if (Session["LoggedInUser"] != null)
            {
                project.UpdatedUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
            }

            bool success = ScheduleAPIHelper.UpdateAssignedFacilitator(project);

            if (success)
            {
                // return that view that will replace the Assigned Facilitator partial
                return RedirectToAction("AssignedFacilitator", new { facilitatorId = model.FacilitatorId });
            }

            return Json("There was an issue with this update.", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAccelaFacilitator(ChangeFacilitatorModel model)
        {
            APIHelper apiHelper = new APIHelper();
            ProjectEstimation project = apiHelper.GetProjectDetailsByProjectId(model.ProjectId);
            project.AssignedFacilitator = model.FacilitatorId;

            if (project != null)
            {
                // notify Accela of facilitator change
                bool success = AccelaScheduleAPIHelper.AccelaChangeAssignedFacilitator(project);

                if (success)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("Unsuccessful", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SubmitPrelimDateRequest")]
        public ActionResult SubmitPrelimDateRequest(RequestPrelimDatesManagerModel requestPrelimDatesManagerModel)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            apihelper.UpdatePrelimDateRequest(requestPrelimDatesManagerModel);
            return Json(new { success = true });
        }

        [HttpPost]
        [ActionName("SubmitExpressDateRequest")]
        public ActionResult SubmitExpressDateRequest(RequestExpressDatesManagerModel requestExpressDatesManagerModel)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            apihelper.UpdateExpressDateRequest(requestExpressDatesManagerModel);
            return Json(new { success = true });
        }

        [HttpPost]
        [ActionName("CancelMeeting")]
        public ActionResult CancelMeeting(CancelMeetingModel model)
        {
            if (model.AppointmentId == 0)
            {
                return Json(new { success = false });
            }

            bool cancelled = ProjectDetailAPIHelper.CancelMeetingById(model.AppointmentId, model.Notes, model.UserId);

            if (cancelled)
            {
                AccelaScheduleAPIHelper.AccelaScheduleFMA(model.AppointmentId);

                return Json(AppointmentResponseStatusDisplay.Cancelled.ToStringValue());
            }
            return null;
        }

        public bool RescheduleWarning(DateTime fromDt, DateTime toDt)
        {
            //only check for warning if a plan review date exists
            if (toDt == DateTime.MaxValue) return false;
            int diff = 0;
            foreach (DateTime day in EachDay(fromDt, toDt))
                diff++;

            if (diff <= 5)
                return true;

            return false;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            List<HolidayConfig> holidays = new APIHelper().GetHolidayConfigList();
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                if (!holidays.Where(x => x.HolidayDate.Equals(day)).Any() && day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
                    yield return day;
            }
        }

        /// <summary>
        /// This will resend the notifications as requested on the _SendNotifications modal
        /// </summary>
        /// <returns></returns>
        public ActionResult ResendProjectNotifications(List<SendProjectNotifViewModel> items)
        {
            string jsonSaveMessage = string.Empty;
            bool success = false;
            //for each row, do the thing
            foreach (SendProjectNotifViewModel vm in items)
            {
                SendProjectNotification notification = new SendProjectNotification
                {
                    EmailTxts = new List<string>(),
                    UserIds = new List<int>(),
                    ProjectNotificationEmailId = vm.ProjectNotifEmailId,
                    SendToAll = vm.ResendAll
                };
                if (!string.IsNullOrWhiteSpace(vm.AddRecipients))
                {
                    string[] recipientList = vm.AddRecipients.Split(new Char[] { ';' });
                    foreach (string s in recipientList)
                    {
                        if (!String.IsNullOrWhiteSpace(s))
                            notification.EmailTxts.Add(s);
                    }
                }
                int userid = 0;
                if (int.TryParse(vm.SelectedRecipient, out userid))
                {
                    if (userid > 0)
                    {
                        notification.UserIds.Add(userid);
                    }

                }
                else
                {
                    //email txt
                    if (!String.IsNullOrWhiteSpace(vm.SelectedRecipient))
                        notification.EmailTxts.Add(vm.SelectedRecipient);

                }
                success = APIHelper.ResendProjectNotification(notification);
            }
            jsonSaveMessage = success ? "Email sent successfully." : "Failure to send email. Please contact support.";
            return Json(jsonSaveMessage, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// build the list for the _SendNotifications modal
        /// input the AION project id (int)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        private List<SendProjectNotifViewModel> GetProjectNotifs(List<ProjectNotificationEmail> projectNotificationEmails)
        {
            List<SendProjectNotifViewModel> notifs = new List<SendProjectNotifViewModel>();
            foreach (ProjectNotificationEmail item in projectNotificationEmails)
            {
                //convert to VM
                SendProjectNotifViewModel svm = new SendProjectNotifViewModel();
                List<SelectListItem> recips = new List<SelectListItem>();
                svm.ProjectNotifEmailId = item.ProjectEmailNotificationId.Value;
                svm.SendDate = item.EmailSentDt.Value;
                svm.Recipients = new List<SelectListItem>();
                svm.EmailNotif = (EmailNotifType)Enum.Parse(typeof(EmailNotifType), item.EmailTypeDesc, true);
                svm.Recipients.Add(new SelectListItem { Text = "Select A Recipient", Value = "-1" });
                if (item.NotificationEmailList.Count > 0)
                {
                    foreach (var rec in item.NotificationEmailList)
                    {
                        if (rec.SendUserId.HasValue && rec.SendUserId.Value > 0)
                            svm.Recipients.Add(new SelectListItem { Text = rec.FirstName + " " + rec.LastName, Value = rec.SendUserId.Value.ToString() });
                        else
                            svm.Recipients.Add(new SelectListItem { Text = rec.EmailAddressTxt, Value = rec.EmailAddressTxt });
                    }
                }

                notifs.Add(svm);
            }

            return notifs;
        }
        private string GenerateAccelaDeeplink(string id)
        {
            string env = ConfigurationManager.AppSettings["Environment"].ToString();
            if (env.ToLower().Equals("tst")) id = String.Join("", id.Split('-')).ToLower();
            string url = ConfigurationManager.AppSettings["AccelaBaseApplicationDeeplink"].ToString();

            url = url.Replace("{AccelaId}", Server.UrlEncode(id));

            return url;
        }

        private string GetUserFNLM(int? id)
        {
            string facilitatorname = string.Empty;
            if (id != null && id > 0)
            {
                //get the facilitator name
                UserIdentity facilitator = new APIHelper().GetUserIdentityByID(id.Value);
                if (facilitator != null)
                {
                    facilitatorname = facilitator.FirstName + " " + facilitator.LastName;
                }
            }
            return facilitatorname;
        }
    }
}