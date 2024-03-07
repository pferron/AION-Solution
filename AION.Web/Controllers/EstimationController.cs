using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Helpers.APIHelpers;
using AION.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class EstimationController : BaseControllerWeb
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstimationViewModel model, string actionType)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult Update(EstimationSaveViewModel vm)
        {
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            if (vm == null || vm.LoggedInUser == null)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }

            UpdateUserAndPermissions(vm);

            if (vm.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }
            bool success = false;
            ProjectParms projectparms = new ProjectParms();
            Helpers.ConvertProjectHelpers helper = new Helpers.ConvertProjectHelpers();
            Helpers.SendEmailHelpers emailhelper = new Helpers.SendEmailHelpers();
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            vm.Project.UpdatedUser = vm.LoggedInUser;
            vm.AssignedEstimatorId = vm.LoggedInUser.ID;
            ProjectEstimation pe = helper.ConvertSaveEstimationVmToProjectEstimation(vm);
            SaveTypeEnum saveType = (SaveTypeEnum)vm.SaveType;
            pe.SaveType = saveType;
            success = EstimationAPIHelper.SaveEstimation(pe);
            projectparms.ProjectId = pe.AccelaProjectRefId;
            projectparms.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            projectparms.StatusMessage = success ? "Saved Successfully" : "Save did not complete";
            projectparms.RecIdTxt = vm.Project.RecIdTxt;
            //LES-782
            //only send pending email if this is SendPendingEmail type triggered by button on dept tab
            //TODO: move these emails to EstimationCRUDAdapter SaveEstimation method

            switch (saveType)
            {
                case SaveTypeEnum.Submittal:
                    if (vm.IsAllNAChecked)
                    {
                        bool emailed = apihelper.SendNAEmail(pe.AccelaProjectRefId, pe.ProjectName, pe.ProjectAddress);
                    }
                    break;
                case SaveTypeEnum.Save:
                    break;
                case SaveTypeEnum.SendPendingEmail:
                    //send mail to customers incase the estimatin is set to pending status due to required information from customer. These 3 are reason given in UI.
                    if ((vm.BEMPApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.BEMPApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.BEMPApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.FireApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.FireApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.FireApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.ZoningApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.ZoningApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.ZoningApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.BackFlowApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.BackFlowApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.BackFlowApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.EHSApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.EHSApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.EHSApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required))
                    {
                        List<int> senduserids = new List<int>();
                        if (pe.PMId.HasValue)
                            senduserids.Add(pe.PMId.Value);
                        SendPendingEmailModel sendEmailModel = new SendPendingEmailModel
                        {
                            ProjectId = pe.ID,
                            AccelaProjectId = pe.AccelaProjectRefId,
                            ProjectName = pe.ProjectName + " - " + pe.AccelaProjectRefId,
                            ProjectStatusDesc = pe.AIONProjectStatus.ProjectStatusEnum.ToStringValue(),
                            PendingCommentsToCustomer = emailhelper.ComposeCustomerPendingNoteMessage(vm),
                            Usernamepublic = vm.Project.UpdatedUser.FirstName + " " + vm.Project.UpdatedUser.LastName,
                            Timestamp = pe.UpdatedDate,
                            WrkId = vm.LoggedInUser.ID,
                            SendUserIds = senduserids,
                            IsExpress = pe.AionPropertyType == PropertyTypeEnums.Express
                        };
                        switch ((DepartmentDivisionEnum)vm.PendingEmailType)
                        {
                            case DepartmentDivisionEnum.Building:
                            case DepartmentDivisionEnum.Zoning:
                            case DepartmentDivisionEnum.Fire:
                            case DepartmentDivisionEnum.Environmental:
                            case DepartmentDivisionEnum.Backflow:
                                apihelper.SendPendingNotificationEmail(sendEmailModel);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            TempData["StatusMessage"] = projectparms.StatusMessage;
            if (success && saveType == SaveTypeEnum.Submittal)
            {
                return RedirectToAction("EstimationDashboard");
            }
            else
            {
                return RedirectToAction("EstimationMain", new { ProjectId = projectparms.ProjectId, RecIdTxt = projectparms.RecIdTxt, StatusMessage = projectparms.StatusMessage });
            }
        }

        // GET: Estimation
        public ActionResult EstimationMain(ProjectParms parms)
        {
            EstimationViewModel estimationViewModel = PrepareEstimationData(parms);

            //if logged in user is null then return home controller
            if (estimationViewModel.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            if (estimationViewModel.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            if (estimationViewModel == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                return View(new EstimationViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." });
            }
            if (estimationViewModel.Project.IsProjectPreliminary == true)
            {
                //return RedirectToAction("PreliminaryEstimation", parms);
                return View(new EstimationViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." });

            }

            //if they don't have any trades/agencies, redirect to dashboard with insufficient permission message
            if (estimationViewModel.PermissionMapping.Access_Estimation == false)
            {
                return RedirectToAction("EstimationDashboard", "Estimation", new { StatusMessage = UIStatusMessage.Insufficient_Permission });
            }


            return View(estimationViewModel);
        }

        public ActionResult EstimationDashboard(EstimationDashboardViewModel vm)
        {
            SetUpViewModelBase<EstimationDashboardViewModel>(vm);

            if (vm == null || string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm = new EstimationDashboardViewModel();
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }

            if (TempData["StatusMessage"] != null)
            {
                vm.StatusMessage = TempData["StatusMessage"].ToString();
            }

            //if logged in user is null then return home controller
            if (vm.LoggedInUser == null || vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            if (vm.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            vm.DashboardListBase = DashboardAPIHelper.GetEstimationDashboardProjectList(userid: vm.LoggedInUser.ID, numofrows: 300);
            vm.EstimationDashboardListItems = vm.DashboardListBase.EstimationDashboardList;
            vm.SavedFilterList = UIHelpers.ConvertUserUISettingsJsonToDashboardString(vm.DashboardListBase.UserUISettings, "estimation");

            return View(vm);
        }
        /// <summary>
        /// save user UI filter options for estimation dashboard
        /// </summary>
        /// <returns>string</returns>
        [HttpPost]
        public JsonResult SaveUserUIFilterOptions(SaveUserUiSettingViewModel vm)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            if (vm == null || string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
                return Json("fail", JsonRequestBehavior.AllowGet);

            //save ui settings

            UpdateUserAndPermissions(vm);

            //update ui object with dashboard type columnsFilter
            UserIdentity useridentity = apihelper.GetUserIdentityByID(vm.LoggedInUser.ID);

            UiSettings uisettings = UIHelpers.ConvertJsonStringToUiSettings(useridentity.UiSetting);
            switch (vm.DashboardType)
            {
                case "estimation":
                    uisettings.EstimationDashboard.ColumnsFilter = vm.SaveFilterList;
                    break;
                case "meeting":
                    uisettings.MeetingDashboard.ColumnsFilter = vm.SaveFilterList;
                    break;
                case "scheduling":
                    uisettings.SchedulingDashboard.ColumnsFilter = vm.SaveFilterList;
                    break;
                default:
                    break;
            }
            //convert uisettings to string
            useridentity.UiSetting = UIHelpers.ConvertUiSettingsToJsonString(uisettings);

            apihelper.UpdateUser(useridentity);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PreliminaryEstimation(ProjectParms parms)
        {
            ConvertProjectHelpers helper = new ConvertProjectHelpers();
            EstimationViewModel preliminaryEstimationViewModel = PrepareEstimationData(parms, true);
            //if logged in user is null then return home controller
            if (preliminaryEstimationViewModel.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            if (preliminaryEstimationViewModel.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            if (preliminaryEstimationViewModel == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                return View(new EstimationViewModel());
            }
            //if they don't have any trades/agencies, redirect to dashboard with insufficient permission message
            if (preliminaryEstimationViewModel.PermissionMapping.Access_PrelimEstimation == false)
            {
                return RedirectToAction("EstimationDashboard", "Estimation", new { StatusMessage = UIStatusMessage.Insufficient_Permission });
            }

            return View("EstimationMain", preliminaryEstimationViewModel);
        }

        [HttpPost]
        [ActionName("UpdatePreliminaryEstimation")]
        public ActionResult UpdatePreliminaryEstimation(PreliminaryEstimationSaveViewModel vm)
        {
            bool success = false;
            ProjectParms projectparms = new ProjectParms();
            Helpers.ConvertProjectHelpers helper = new Helpers.ConvertProjectHelpers();
            Helpers.SendEmailHelpers emailhelper = new Helpers.SendEmailHelpers();
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            if (vm == null || vm.LoggedInUser == null)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }

            UpdateUserAndPermissions(vm);

            if (Session["LoggedInUser"] == null)
            {
                new AuthenticateHelper().GetViewModelWPerms(vm);
                Session["LoggedInUser"] = JsonConvert.SerializeObject(vm.LoggedInUser);
                Session["PermissionMap"] = JsonConvert.SerializeObject(vm.PermissionMapping);
            }
            else
            {
                vm.PermissionMapping = JsonConvert.DeserializeObject<BusinessEntities.PermissionMapping>(Session["PermissionMap"].ToString());
                vm.LoggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
            }
            if (vm.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            vm.Project.UpdatedUser = vm.LoggedInUser;
            ProjectEstimation pe = helper.ConvertSaveEstimationVmToProjectEstimation(vm);
            SaveTypeEnum saveType = (SaveTypeEnum)vm.SaveType;
            pe.SaveType = saveType;
            success = EstimationAPIHelper.SaveProjectEstimationDetails(pe);
            projectparms.ProjectId = pe.AccelaProjectRefId;
            projectparms.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            projectparms.StatusMessage = success ? "Saved_Successfully" : "Save_did_not_complete";
            projectparms.RecIdTxt = vm.Project.RecIdTxt;
            //LES-782
            //only send pending email if this is SendPendingEmail type triggered by button on dept tab
            switch (saveType)
            {
                case SaveTypeEnum.Submittal:
                    if (vm.IsAllNAChecked)
                    {
                        bool emailed = apihelper.SendNAEmail(pe.AccelaProjectRefId, pe.ProjectName, pe.ProjectAddress);
                    }
                    break;
                case SaveTypeEnum.Save:
                    break;
                case SaveTypeEnum.SendPendingEmail:
                    //send mail to customers incase the estimatin is set to pending status due to required information from customer. These 3 are reason given in UI.
                    if ((vm.BEMPApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.BEMPApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.BEMPApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.FireApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.FireApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.FireApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.ZoningApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.ZoningApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.ZoningApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.BackFlowApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.BackFlowApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.BackFlowApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required) ||
                        (vm.EHSApplicationNotes.PendingReason == ProjectStatusEnum.Information_Required || vm.EHSApplicationNotes.PendingReason == ProjectStatusEnum.Scope_Drawings_Required || vm.EHSApplicationNotes.PendingReason == ProjectStatusEnum.Preliminary_Meeting_Required))
                    {
                        List<int> senduserids = new List<int>();
                        if (pe.PMId.HasValue)
                            senduserids.Add(pe.PMId.Value);
                        SendPendingEmailModel sendEmailModel = new SendPendingEmailModel
                        {
                            ProjectId = pe.ID,
                            AccelaProjectId = pe.AccelaProjectRefId,
                            ProjectName = pe.ProjectName + " - " + pe.AccelaProjectRefId,
                            ProjectStatusDesc = pe.AIONProjectStatus.ProjectStatusEnum.ToStringValue(),
                            PendingCommentsToCustomer = emailhelper.ComposeCustomerPendingNoteMessage(vm),
                            Usernamepublic = vm.Project.UpdatedUser.FirstName + " " + vm.Project.UpdatedUser.LastName,
                            Timestamp = pe.UpdatedDate,
                            WrkId = vm.LoggedInUser.ID,
                            SendUserIds = senduserids,
                            IsPreliminaryMeeting = true,
                            IsExpress = false
                        };
                        switch ((DepartmentDivisionEnum)vm.PendingEmailType)
                        {
                            case DepartmentDivisionEnum.Building:
                            case DepartmentDivisionEnum.Zoning:
                            case DepartmentDivisionEnum.Fire:
                            case DepartmentDivisionEnum.Environmental:
                            case DepartmentDivisionEnum.Backflow:
                                apihelper.SendPendingNotificationEmail(sendEmailModel);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            TempData["StatusMessage"] = projectparms.StatusMessage;
            if (success && saveType == SaveTypeEnum.Submittal)
            {
                return RedirectToAction("EstimationDashboard");
            }
            else
            {
                return RedirectToAction("PreliminaryEstimation", new { ProjectId = projectparms.ProjectId, RecIdTxt = projectparms.RecIdTxt, StatusMessage = projectparms.StatusMessage });
            }
        }

        public ActionResult FacilitatorWorkloadSummary() {
            FacilitatorWorkloadSummaryViewModel vm = new FacilitatorWorkloadSummaryViewModel()
            {
                FacilitatorWorkloadSummary = new APIHelper().GetFacilitatorWorkloadSummary(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date),
                LoggedInUserEmail = GetLoggedInUserEmailAddress()
            };

            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            UpdateUserAndPermissions(vm);

            if (vm == null || vm.LoggedInUser == null)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = UIStatusMessage.Not_Logged_In });
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult FacilitatorWorkloadSummary(DateTime startdate, DateTime enddate, string LoggedInUserEmail)
        {
            FacilitatorWorkloadSummaryViewModel vm = new FacilitatorWorkloadSummaryViewModel()
            {
                FacilitatorWorkloadSummary = new APIHelper().GetFacilitatorWorkloadSummary(startdate, enddate),
                LoggedInUserEmail = GetLoggedInUserEmailAddress(),
                StartDate = startdate,
                EndDate = enddate
            };

            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            UpdateUserAndPermissions(vm);

            if (vm == null || vm.LoggedInUser == null)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = UIStatusMessage.Not_Logged_In });
            }
            return View(vm);
        }

        //this is replaced by FacilitatorWorkloadSummary post method, will be removed after review
        [HttpPost]
        [ActionName("GetFacilitatorWorkloadSummary")]
        public ActionResult GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate, string LoggedInUserEmail)
        {
            try
            {
                string loggedInUserEmail = GetLoggedInUserEmailAddress();
                if (string.IsNullOrWhiteSpace(loggedInUserEmail))
                    return View();

                Helpers.APIHelper apihelper = new Helpers.APIHelper();

                EstimationViewModel estimationViewModel = new EstimationViewModel();
                estimationViewModel.LoggedInUserEmail = loggedInUserEmail;

                UpdateUserAndPermissions(estimationViewModel);

                estimationViewModel.FacilitatorWorkloadSummary = apihelper.GetFacilitatorWorkloadSummary(startdate, enddate);

                return PartialView("_FacilitatorWorkload", estimationViewModel.FacilitatorWorkloadSummary);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        [ActionName("BulkEstimation")]
        public ActionResult BulkEstimation()
        {
            ViewData.Clear();

            BulkEstimationViewModel vm = PrepareBulkEstimationData();

            return PartialView("_BulkEstimationDetails", vm);
        }

        [HttpPost]
        [ActionName("BulkEstimationUpdate")]
        public ActionResult BulkEstimationUpdate(BulkEstimationSaveViewModel vm)
        {
            List<ProjectIdSelectedViewModel> projectIds = new JavaScriptSerializer().Deserialize<List<ProjectIdSelectedViewModel>>(vm.ProjectIds);

            bool success = false;
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            UpdateUserAndPermissions(vm);

            ProjectParms projectparms = new ProjectParms();
            Helpers.ConvertProjectHelpers helper = new Helpers.ConvertProjectHelpers();
            Helpers.SendEmailHelpers emailhelper = new Helpers.SendEmailHelpers();
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            foreach (ProjectIdSelectedViewModel projectId in projectIds)
            {
                vm.Project = apihelper.GetProjectDetailsByExternalRefInfo(projectId.Id);
                vm.Project.AccelaProjectRefId = projectId.Id;
                vm.Project.AuditAction = AuditActionEnum.Estimation_Change;
                vm.Project.UpdatedUser = vm.LoggedInUser;
                vm.AssignedEstimatorId = vm.LoggedInUser.ID;
                ProjectEstimation pe = helper.ConvertSaveBulkEstimationToProjectEstimation(vm);

                pe.SaveType = SaveTypeEnum.Save;

                success = EstimationAPIHelper.SaveEstimation(pe);

                projectparms.ProjectId = pe.AccelaProjectRefId;
                projectparms.LoggedInUserEmail = vm.Project.UpdatedUser.SrcSystemValueText;

                if (!success)
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }

            if (success)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }


        protected BulkEstimationViewModel PrepareBulkEstimationData()
        {
            BulkEstimationViewModel estimationViewModel = new BulkEstimationViewModel();
            Helper helper = new Helper();
            UIHelpers uihelper = new Helpers.UIHelpers();

            estimationViewModel.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            UpdateUserAndPermissions(estimationViewModel);

            EstimationAPIHelper estimationApiHelper = new EstimationAPIHelper();
            BulkEstimationModel model = estimationApiHelper.GetBulkEstimationModel();

            estimationViewModel.FacilitatorList = model.Facilitators;
            estimationViewModel.ReviewersList = model.Reviewers;
            estimationViewModel.AllReviewers = model.Reviewers;
            estimationViewModel.EstimatorList = model.EstimatorUIModels;
            uihelper.SetPermissionsForEstimationUI(estimationViewModel);

            estimationViewModel.BuildingReviewers = model.Reviewers.Where(x => x.DesignatedDepartments.Any(y => y.DepartmentEnum == DepartmentNameEnums.Building)).ToList();
            estimationViewModel.ElectricalReviewers = model.Reviewers.Where(x => x.DesignatedDepartments.Any(y => y.DepartmentEnum == DepartmentNameEnums.Electrical)).ToList();
            estimationViewModel.MechanicalReviewers = model.Reviewers.Where(x => x.DesignatedDepartments.Any(y => y.DepartmentEnum == DepartmentNameEnums.Mechanical)).ToList();
            estimationViewModel.PlumbingReviewers = model.Reviewers.Where(x => x.DesignatedDepartments.Any(y => y.DepartmentEnum == DepartmentNameEnums.Plumbing)).ToList();
            estimationViewModel.FireReviewers = model.Reviewers.Where(x => x.DesignatedDepartments.Any(y => helper.FireDepartmentNames.Contains(y.DepartmentEnum))).ToList();

            return estimationViewModel;
        }


        #region Private Methods

        private EstimationViewModel PrepareEstimationData(ProjectParms parms, bool isPreliminaryEstimation = false)
        {
            EstimationViewModel estimationViewModel = new EstimationViewModel();
            ProjectEstimation basedata;
            DateTime today = DateTime.Now;

            Helpers.UIHelpers uihelper = new Helpers.UIHelpers();
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            estimationViewModel.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            if (parms.ProjectId == null)
            {
                estimationViewModel.StatusMessage = "Need Project ID to continue";
                return estimationViewModel;
            }
            if (estimationViewModel.LoggedInUserEmail == null)
            {
                estimationViewModel.StatusMessage = "Please Log in";
                return estimationViewModel;
            }

            UpdateUserAndPermissions(estimationViewModel);

            EstimationAPIHelper estimationApiHelper = new EstimationAPIHelper();
            parms.IsPreliminary = isPreliminaryEstimation;
            EstimationModel model = estimationApiHelper.GetEstimationModel(parms);

            basedata = model.ProjectEstimation;

            if (basedata == null)
            {
                estimationViewModel.StatusMessage = "Project Error. Please contact system admin.";
                return estimationViewModel;
            }

            estimationViewModel.Project = basedata;
            //if this is RTAP get the previous projects to set primary reviewers
            if (estimationViewModel.Project.IsProjectRTAP && !String.IsNullOrWhiteSpace(estimationViewModel.Project.AccelaRTAPProjectRefId))
            {
                try
                {
                    estimationViewModel.PreviousProject = apihelper.GetProjectDetailsForEstimationByAccelaId(estimationViewModel.Project.AccelaRTAPProjectRefId, basedata.RecIdTxt);
                }
                catch (Exception ex)
                {
                    //catches bad data from projects.json
                    string message = ex.InnerException.Message;
                    estimationViewModel.PreviousProject = new ProjectEstimation();
                }
            }

            estimationViewModel.FacilitatorList = model.Facilitators;
            //If this is express, only allow reviewers that have IsExpressSched = true
            bool isExpressProject = (estimationViewModel.Project.AccelaPropertyType == PropertyTypeEnums.Express);

            estimationViewModel.AllReviewers = estimationViewModel.Project.Reviewers;

            estimationViewModel.EstimatorList = apihelper.GetAllEstimators();
            estimationViewModel.PermissionMappingCatalogItemList = model.PermissionMappingCatalogItems;
            estimationViewModel.NotesComments = model.Notes;
            estimationViewModel.FacilitatorWorkloadSummary = model.FacilitatorWorkloadSummary;
            uihelper.SetPermissionsForEstimationUI(estimationViewModel);
            //removes underscore from Action params. this will avoid %20 from links but with keeping the message readable
            estimationViewModel.StatusMessage = parms.StatusMessage != null ? parms.StatusMessage.Replace('_', ' ') : "";
            estimationViewModel.StandardNotes = model.StandardNotes;
            estimationViewModel.StandardNoteGroups = model.StandardNoteGroupEnums;

            estimationViewModel.ExcludedPlanReviewersBuild.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersElectric.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersMech.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersPlumb.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersFire.AddRange(basedata.Agencies.Where(x => estimationViewModel.FireDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersZone.AddRange(basedata.Agencies.Where(x => estimationViewModel.ZoneDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersDayCare.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersFood.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersLodge.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersBackFlow.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            estimationViewModel.ExcludedPlanReviewersPool.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool)
                            .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            //check for express type
            estimationViewModel.ExpressYNSelected = estimationViewModel.Project.AionPropertyType == PropertyTypeEnums.Express ? "Y" : "N";
            //set the property type dropdown
            estimationViewModel.ProjectTypeEnumSelected = (int)estimationViewModel.Project.AionPropertyType;

            //Generate deeplink to accela
            estimationViewModel.AccelaProjectDeeplink = GenerateAccelaDeeplink(estimationViewModel.Project.AccelaProjectRefId);

            return estimationViewModel;
        }

        private string GenerateAccelaDeeplink(string id)
        {
            string env = ConfigurationManager.AppSettings["Environment"].ToString();
            if (env.ToLower().Equals("tst")) id = String.Join("", id.Split('-')).ToLower();
            string url = ConfigurationManager.AppSettings["AccelaBaseApplicationDeeplink"].ToString();

            url = url.Replace("{AccelaId}", Server.UrlEncode(id));

            return url;
        }

        #endregion Private Methods
    }
}
