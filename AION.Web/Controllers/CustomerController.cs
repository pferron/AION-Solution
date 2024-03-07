using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Models;
using AION.Web.Models.Shared;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomerController : BaseProjectDetailController
    {
        public CustomerController()
        {
        }

        /// <summary>
        /// Gets the project detail page by user and project id
        /// </summary>
        /// <param name="projectparms"></param>
        /// <returns></returns>
        public ActionResult ProjectDetail(ProjectParms projectparms)
        {
            APIHelper apihelper = new APIHelper();

            ProjectDetailAPIHelper projectDetailAPIHelper = new ProjectDetailAPIHelper();

            ProjectDetailModel model = projectDetailAPIHelper.GetProjectDetailModel(projectparms);

            CustomerProjectDetailViewModel vm = new CustomerProjectDetailViewModel();

            SetUpViewModelBase<CustomerProjectDetailViewModel>(vm);

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            if (projectparms.ProjectId == null)
            {
                projectparms.StatusMessage = "Project ID is null";
                //redirect to dashboard
                return RedirectToAction("ProjectsDashboard", projectparms);
            }

            vm.Project = model.ProjectEstimation;

            vm.AuditActionRefs = model.AuditActionRefs;

            vm.ProjectAudits = model.ProjectAudits;
            vm.StatusMessage = projectparms.StatusMessage;
            vm.InitMode = "Projects";

            vm.ApptResponseStatusSelected = ((int)vm.ApptResponseStatusSelectedEnum).ToString();

            vm.Project.Notes = model.Notes.Where(x => x.NotesType.Type != NoteTypeEnum.InternalNotes).ToList();

            vm.Notes = vm.Project.Notes
                .Where(x => x.NotesType.Type != NoteTypeEnum.InternalNotes)
                .Select(x => new CustmrResponseNote
                {
                    Attachments = x.Attachments,
                    DeptNameEnum = x.DeptNameEnum,
                    NotesComments = x.NotesComments,
                    NotesType = x.NotesType,
                    ProjectID = x.ProjectID,
                    CreatedDate = x.CreatedDate,
                    CreatedUser = x.CreatedUser,
                    ID = x.ID,
                    UpdatedDate = x.UpdatedDate,
                    UpdatedUser = x.UpdatedUser,
                    ParentNoteID = x.ParentNoteID
                }).ToList();


            UserIdentity facilitator = model.Facilitator;
            vm.AssignedFacilitator = $"{facilitator.LastName}, {facilitator.FirstName}";

            vm.ProjectCycleReviews = model.ProjectCycleReviews;

            vm.PlanReviewViewModels =
                 GetPlanReviewsModel(model.ProjectEstimation, vm.ProjectCycleReviews, vm.PermissionMapping.E_Plns_Rdy_Dt, vm.LoggedInUser);


            PlanReviewEmailModel planReviewEmailModel = new PlanReviewEmailModel();

            // we need to check if rtap here so we send the correct project id?
            if (vm.PlanReviewViewModels.Any(x => x.IsFutureCycle == true))
            {
                var futurePlanReview = vm.PlanReviewViewModels.FirstOrDefault(x => x.IsFutureCycle == true);
                planReviewEmailModel.PlanReviewStartDate = futurePlanReview.ScheduleAfterDt.Value.ToShortDateString();
            }
            else
            {
                var currentPlanReview = vm.PlanReviewViewModels.FirstOrDefault(x => x.IsCurrentCycle == true);
                if (currentPlanReview != null)
                { 
                    planReviewEmailModel.PlanReviewStartDate = currentPlanReview.PlanReviewDate;
                }
            }

            planReviewEmailModel.AccelaProjectRefId = vm.Project.AccelaProjectRefId;
            planReviewEmailModel.ProjectName = vm.Project.ProjectName;
            planReviewEmailModel.ProjectAddress = vm.Project.ProjectAddress;
            planReviewEmailModel.FacilitatorId = vm.Project.AssignedFacilitator.Value;

            vm.ScheduledNotes = model.Notes;

            var acceptanceEmail = apihelper.CreatePlanReviewAcceptanceEmail(planReviewEmailModel);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(acceptanceEmail);

            vm.PlanReviewAcceptanceEmail = htmlDoc.DocumentNode.OuterHtml;

            return View(vm);
        }

        /// <summary>
        /// Update Project Details
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Update")]
        public ActionResult Update(CustmrProjectSaveViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            ConvertProjectHelpers helper = new ConvertProjectHelpers();

            ProjectParms projectparms = new ProjectParms
            {
                ProjectId = vm.Project.AccelaProjectRefId,
                RecIdTxt = vm.Project.RecIdTxt,
                StatusMessage = ""
            };

            vm.CreatedNotes = new List<Note>();
            vm.Project.UpdatedUser = apihelper.GetUserIdentityByID(vm.LoggedInUser.ID);
            bool success = false;
            bool nothingtosave = true;
            string statusmessage = string.Empty;
            ProjectEstimation pe = helper.ConvertSaveCustmrProjectDetailToProjEstimation(vm);

            //LES-3794 Save the customer response
            if (vm.PendingEstimationNotes != null)
            {
                List<CustmrResponseNote> custmrpendingresponses = vm.PendingEstimationNotes
                    .Where(x => x.CustmrCanRespond == true && string.IsNullOrWhiteSpace(x.CustmrResponseComment) == false).ToList();
                NoteType notetype = NoteAPIHelper.GetNoteTypeBaseList().Where(x => x.Type == NoteTypeEnum.PendingNotes).FirstOrDefault();
                foreach (CustmrResponseNote custmrnote in custmrpendingresponses)
                {

                    int id = 0;
                    Note note = new Note
                    {
                        NotesType = notetype,
                        NotesComments = custmrnote.CustmrResponseComment,
                        CreatedUser = vm.Project.UpdatedUser,
                        UpdatedUser = vm.Project.UpdatedUser,
                        DeptNameEnum = custmrnote.DeptNameEnum,
                        ProjectID = vm.Project.ID,
                        ParentNoteID = custmrnote.ID,
                        Attachments = new List<NotesAttachments>()
                    };
                    id = NoteAPIHelper.InsertCustomerResponse(note);
                    note.ID = id;
                    vm.CreatedNotes.Add(note);
                }
            }

            projectparms.ProjectId = pe.AccelaProjectRefId;
            projectparms.LoggedInUserEmail = vm.Project.UpdatedUser.SrcSystemValueText;

            statusmessage = success ? UIStatusMessage.Saved_Successfully.ToStringValue() : UIStatusMessage.Incomplete_Save.ToStringValue();
            TempData["StatusMessage"] = statusmessage;
            projectparms.StatusMessage = nothingtosave ? UIStatusMessage.Saved_Successfully.ToStringValue() : statusmessage;

            return RedirectToAction("ProjectDetail", new { ProjectId = projectparms.ProjectId, RecIdTxt = projectparms.RecIdTxt, StatusMessage = projectparms.StatusMessage });
        }

        public ActionResult PlanReviews(int projectId, bool canEditPlansReadyOnDate)
        {
            List<PlanReviewPartialViewModel> planReviewViewModels = GetPlanReviewsModel(projectId, canEditPlansReadyOnDate);

            return PartialView("_PlanReviewsCustomer", planReviewViewModels);
        }

        [HttpPost]
        public ActionResult UpdateProjectCycle(int projectId, int projectCycleId, int planReviewScheduleId, int response, string prod)
        {
            UserIdentity loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

            UpdateProjectCycleInformation(projectCycleId, planReviewScheduleId, response, prod, loggedInUser.ID, true);

            return RedirectToAction("PlanReviews", new { projectId = projectId, canEditPlansReadyOnDate = true });
        }

        public ActionResult ScheduledMeetings(string projectId, int loggedInUserId)
        {
            ScheduledMeetingsListViewModel scheduledMeetingsListViewModel = GetScheduledMeetingsModel(projectId, loggedInUserId);

            return PartialView("_ScheduledMeetingList", scheduledMeetingsListViewModel);
        }

        public ActionResult MeetingsDashboard(CustmrMeetingsDashboardViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }
            else
            {
                vm.LoggedInUserEmail = vm.LoggedInUserEmail;
            }

            UpdateUserAndPermissions(vm);

            vm.MeetingList = apihelper.GetMeetingsByUserID(vm.LoggedInUser.ID);
            if (TempData["StatusMessage"] != null)
            {
                vm.StatusMessage = TempData["StatusMessage"].ToString();
            }

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            return View(vm);
        }

        /// <summary>
        /// Gets the project detail page by user and project id
        /// </summary>
        /// <param name="projectparms"></param>
        /// <returns></returns>
        public ActionResult MeetingDetail(ProjectParms projectparms)
        {
            APIHelper apihelper = new APIHelper();

            CustomerProjectDetailViewModel vm = new CustomerProjectDetailViewModel();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }

            UpdateUserAndPermissions(vm);

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            //TODO: add all other objects for page
            if (projectparms.ProjectId == null)
            {
                projectparms.StatusMessage = "Project ID is null";
                //redirect to dashboard
                return RedirectToAction("MeetingsDashboard", projectparms);
            }
            vm.Project = ProjectDetailAPIHelper.GetProjectDetailsByAccelaIdforUI(projectparms.ProjectId);
            vm.AuditActionRefs = apihelper.GetAuditActionRefs();
            vm.Project.Notes = NoteAPIHelper.GetProjectNotes(vm.Project.ID, null);
            vm.Notes = vm.Project.Notes.Select(x => new CustmrResponseNote
            {
                Attachments = x.Attachments,
                DeptNameEnum = x.DeptNameEnum,
                NotesComments = x.NotesComments,
                NotesType = x.NotesType,
                ProjectID = x.ProjectID,
                CreatedDate = x.CreatedDate,
                CreatedUser = x.CreatedUser,
                ID = x.ID,
                UpdatedDate = x.UpdatedDate,
                UpdatedUser = x.UpdatedUser,
                ParentNoteID = x.ParentNoteID
            }).ToList();

            vm.ProjectAudits = apihelper.GetProjectAudits(vm.Project.ID);
            vm.StatusMessage = projectparms.StatusMessage;
            vm.InitMode = "Meetings";
            return RedirectToAction("ProjectDetail", projectparms);
        }

        /// <summary>
        /// Update meeting to rejected
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SubmitMeetingDateRequest")]
        public ActionResult SubmitMeetingDateRequest(RequestMeetingDatesManagerModel requestMeetingDatesManagerModel)
        {
            APIHelper apihelper = new APIHelper();
            UserIdentity loggedInUser;

            if (Session["LoggedInUser"] != null)
            {
                loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
                if (requestMeetingDatesManagerModel.MeetingType == MeetingTypeEnum.Preliminary.ToStringValue())
                {
                    var prelimDatesModel = new RequestPrelimDatesManagerModel()
                    {
                        PreliminaryMeetingApptId = requestMeetingDatesManagerModel.MeetingApptId,
                        RequestDate1 = requestMeetingDatesManagerModel.RequestDate1,
                        RequestDate2 = requestMeetingDatesManagerModel.RequestDate2,
                        RequestDate3 = requestMeetingDatesManagerModel.RequestDate3
                    };

                    apihelper.UpdatePrelimDateRequest(prelimDatesModel);

                    apihelper.UpdatePrelimStatus(
                        new SavePrelimStatus()
                        {
                            PrelimID = prelimDatesModel.PreliminaryMeetingApptId,
                            ResponseStatusEnumId = ((int)AppointmentResponseStatusEnum.Reject).ToString(),
                            WkrId = loggedInUser.ID.ToString()

                        });
                }
                else
                {
                    apihelper.UpdateMeetingDateRequest(requestMeetingDatesManagerModel);
                    apihelper.UpdateMeetingStatus(
                        new SaveMeetingStatus()
                        {
                            MeetingId = requestMeetingDatesManagerModel.MeetingApptId,
                            Status = ((int)AppointmentResponseStatusEnum.Reject).ToString(),
                            WkrId = loggedInUser.ID.ToString()
                        });
                }

                apihelper.CancelAppointment(requestMeetingDatesManagerModel.MeetingApptId, requestMeetingDatesManagerModel.MeetingType);
            }
            return RedirectToAction("ScheduledMeetings", new { projectId = requestMeetingDatesManagerModel.ProjectId, loggedInUserId = requestMeetingDatesManagerModel.LoggedInUserId });
        }

        [HttpPost]
        [ActionName("SubmitExpressDateRequest")]
        public ActionResult SubmitExpressDateRequest(RequestExpressDatesManagerModel requestExpressDatesManagerModel)
        {
            UserIdentity loggedInUser;
            APIHelper apihelper = new APIHelper();

            if (Session["LoggedInUser"] != null)
            {
                loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

                requestExpressDatesManagerModel.UserId = loggedInUser.ID.ToString();
                apihelper.UpdateExpressDateRequest(requestExpressDatesManagerModel);
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [ActionName("SearchSelfScheduleDates")]
        public ActionResult SearchSelfScheduleDates(PlanReview pr)
        {
            bool success = false;
            APIHelper apihelper = new APIHelper();

            //Build list of dates to auto schedule for from the start/end dates given
            List<DateTime> scheduleList = new List<DateTime>();
            for (DateTime ii = pr.EarliestDate.Value; ii <= pr.UpdatedDate; ii = ii.AddDays(1)) //Using pr.EarliestDate as placeholder for start date, and pr.UpdatedDate as placeholder for end date
            {
                scheduleList.Add(ii);
            }

            //Get Plan Review info based on project id passed from search

            // we need to check if rtap here so we send the correct project id?
            List<PlanReview> planReviews = apihelper.GetPlanReviewsByProjectCycleId(pr.ProjectCycleId);
            PlanReview prtmp = planReviews.FirstOrDefault(x => x.IsReschedule == false);

            //Create instance of SchedulePlanReviewCapacityParams to pass to PlanReviewProjectSchedulingEngine for auto schedule dates
            SchedulePlanReviewCapacityParams auto_data = new SchedulePlanReviewCapacityParams();

            //Set dates to search between for capacity, using building start/end as placeholders. All trades will look at those dates to check capacity.
            auto_data.BuildingScheduleStart = pr.EarliestDate;
            auto_data.BuildingScheduleEnd = pr.UpdatedDate;
            auto_data.ElectricScheduleStart = pr.EarliestDate;
            auto_data.ElectricScheduleEnd = pr.UpdatedDate;
            auto_data.MechScheduleStart = pr.EarliestDate;
            auto_data.MechScheduleEnd = pr.UpdatedDate;
            auto_data.PlumbScheduleStart = pr.EarliestDate;
            auto_data.PlumbScheduleEnd = pr.UpdatedDate;
            auto_data.BackFlowScheduleStart = pr.EarliestDate;
            auto_data.BackFlowScheduleEnd = pr.UpdatedDate;
            auto_data.DayCareScheduleStart = pr.EarliestDate;
            auto_data.DayCareScheduleEnd = pr.UpdatedDate;
            auto_data.FacilityScheduleStart = pr.EarliestDate;
            auto_data.FacilityScheduleEnd = pr.UpdatedDate;
            auto_data.FireScheduleStart = pr.EarliestDate;
            auto_data.FireScheduleEnd = pr.UpdatedDate;
            auto_data.FoodScheduleStart = pr.EarliestDate;
            auto_data.FoodScheduleEnd = pr.UpdatedDate;
            auto_data.PoolScheduleStart = pr.EarliestDate;
            auto_data.PoolScheduleEnd = pr.UpdatedDate;
            auto_data.ZoneScheduleStart = pr.EarliestDate;
            auto_data.ZoneScheduleEnd = pr.UpdatedDate;

            //Add project data and pool data to SchedulePlanReviewCapacityParams
            auto_data.AccelaProjectIDRef = pr.AccelaProjectRefId;
            auto_data.ProjectID = prtmp.ProjectId;
            auto_data.PlansReadyOnDate = pr.ProdDate.HasValue ? pr.ProdDate : null;
            auto_data.BuildingIsPool = prtmp.BuildPool == null ? false : prtmp.BuildPool.Value;
            auto_data.ElectricIsPool = prtmp.ElectPool == null ? false : prtmp.ElectPool.Value;
            auto_data.MechIsPool = prtmp.MechaPool == null ? false : prtmp.MechaPool.Value;
            auto_data.PlumbIsPool = prtmp.PlumbPool == null ? false : prtmp.PlumbPool.Value;
            auto_data.ZoneIsPool = prtmp.ZonePool == null ? false : prtmp.ZonePool.Value;
            auto_data.FireIsPool = prtmp.FirePool == null ? false : prtmp.FirePool.Value;
            auto_data.FoodServiceIsPool = prtmp.FoodPool == null ? false : prtmp.FoodPool.Value;
            auto_data.PoolIsPool = prtmp.PoolPool == null ? false : prtmp.PoolPool.Value;
            auto_data.FacilityIsPool = prtmp.FacilPool == null ? false : prtmp.FacilPool.Value;
            auto_data.DayCareIsPool = prtmp.DaycPool == null ? false : prtmp.DaycPool.Value;
            auto_data.BackFlowIsPool = prtmp.BackfPool == null ? false : prtmp.BackfPool.Value;

            //Get trade/agency reviewers and add to SchedulePlanReviewCapacityParams
            foreach (AttendeeInfo reviewer in prtmp.AssignedReviewers)
            {
                switch (reviewer.BusinessRefId)
                {
                    case (int)TradeEnums.Building:
                        auto_data.BuildingUserID = reviewer.AttendeeId; break;
                    case (int)TradeEnums.Electrical:
                        auto_data.ElectricUserID = reviewer.AttendeeId; break;
                    case (int)TradeEnums.Mechanical:
                        auto_data.MechUserID = reviewer.AttendeeId; break;
                    case (int)TradeEnums.Plumbing:
                        auto_data.PlumbUserID = reviewer.AttendeeId; break;
                    default:
                        Console.WriteLine("Not a trade"); break;
                }

                switch (reviewer.BusinessRefId)
                {
                    case (int)AgencyEnums.Zone_Davidson:
                    case (int)AgencyEnums.Zone_Cornelius:
                    case (int)AgencyEnums.Zone_Pineville:
                    case (int)AgencyEnums.Zone_Matthews:
                    case (int)AgencyEnums.Zone_Mint_Hill:
                    case (int)AgencyEnums.Zone_Huntersville:
                    case (int)AgencyEnums.Zone_UMC:
                    case (int)AgencyEnums.Zone_Cty_Chrlt:
                    case (int)AgencyEnums.Zone_County:
                        auto_data.ZoneUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.Fire_Davidson:
                    case (int)AgencyEnums.Fire_Cornelius:
                    case (int)AgencyEnums.Fire_Pineville:
                    case (int)AgencyEnums.Fire_Matthews:
                    case (int)AgencyEnums.Fire_Mint_Hill:
                    case (int)AgencyEnums.Fire_Huntersville:
                    case (int)AgencyEnums.Fire_UMC:
                    case (int)AgencyEnums.Fire_Cty_Chrlt:
                    case (int)AgencyEnums.Fire_County:
                        auto_data.FireUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Day_Care:
                        auto_data.DayCareUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Food:
                        auto_data.FoodServiceUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Pool:
                        auto_data.PoolUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Facilities:
                        auto_data.FacilityUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.Backflow:
                        auto_data.BackFlowUserID = reviewer.AttendeeId; break;
                    default:
                        Console.WriteLine("Not an Agency"); break;
                }
            }

            auto_data.Cycle = prtmp.ProjectCycle.CycleNbr.Value;

            //Initialize List to hold returned dates from auto schedule
            List<DateTime> availableDates = CustomerAPIHelper.SearchSelfScheduleCapacity(auto_data);

            //Convert list of DateTime to list of string
            List<string> dates = availableDates.Select(x => x.ToString()).ToList();
            return Json(new { success = success, dates = dates });
        }

        [HttpPost]
        [ActionName("SubmitSelfScheduleDate")]
        public ActionResult SubmitSelfScheduleDate(PlanReview pr)
        {
            APIHelper apihelper = new APIHelper();
            string status = "";
            bool success = false;

            List<PlanReview> planReviews = apihelper.GetPlanReviewsByProjectCycleId(pr.ProjectCycleId);
            PlanReview prtmp = planReviews.FirstOrDefault(x => x.IsReschedule == false);
            UserIdentity loggedInUser;

            if (Session["LoggedInUser"] != null)
            {
                loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

                prtmp.UpdatedUser = loggedInUser;
            }

            AutoScheduledPlanReviewParams data = new AutoScheduledPlanReviewParams();

            data.isSelfSchedule = true;
            data.selfScheduleDate = pr.EarliestDate.Value;

            data.AccelaProjectIDRef = pr.AccelaProjectRefId;
            data.ProjectID = prtmp.ProjectId;
            data.PlansReadyOnDate = pr.ProdDate.HasValue ? pr.ProdDate : null;
            data.BuildingIsPool = prtmp.BuildPool.HasValue ? prtmp.BuildPool.Value : false;
            data.ElectricIsPool = prtmp.ElectPool.HasValue ? prtmp.ElectPool.Value : false;
            data.MechIsPool = prtmp.MechaPool.HasValue ? prtmp.MechaPool.Value : false;
            data.PlumbIsPool = prtmp.PlumbPool.HasValue ? prtmp.PlumbPool.Value : false;
            data.ZoneIsPool = prtmp.ZonePool.HasValue ? prtmp.ZonePool.Value : false;
            data.FireIsPool = prtmp.FirePool.HasValue ? prtmp.FirePool.Value : false;
            data.FoodServiceIsPool = prtmp.FoodPool.HasValue ? prtmp.FoodPool.Value : false;
            data.PoolIsPool = prtmp.PoolPool.HasValue ? prtmp.PoolPool.Value : false;
            data.FacilityIsPool = prtmp.FacilPool.HasValue ? prtmp.FacilPool.Value : false;
            data.DayCareIsPool = prtmp.DaycPool.HasValue ? prtmp.DaycPool.Value : false;
            data.BackFlowIsPool = prtmp.BackfPool.HasValue ? prtmp.BackfPool.Value : false;

            //Get trade/agency reviewers and add to SchedulePlanReviewCapacityParams
            foreach (AttendeeInfo reviewer in prtmp.AssignedReviewers)
            {
                switch (reviewer.BusinessRefId)
                {
                    case (int)TradeEnums.Building:
                        data.BuildingUserID = reviewer.AttendeeId; break;
                    case (int)TradeEnums.Electrical:
                        data.ElectricUserID = reviewer.AttendeeId; break;
                    case (int)TradeEnums.Mechanical:
                        data.MechUserID = reviewer.AttendeeId; break;
                    case (int)TradeEnums.Plumbing:
                        data.PlumbUserID = reviewer.AttendeeId; break;
                    default:
                        Console.WriteLine("Not a trade"); break;
                }

                switch (reviewer.BusinessRefId)
                {
                    case (int)AgencyEnums.Zone_Davidson:
                    case (int)AgencyEnums.Zone_Cornelius:
                    case (int)AgencyEnums.Zone_Pineville:
                    case (int)AgencyEnums.Zone_Matthews:
                    case (int)AgencyEnums.Zone_Mint_Hill:
                    case (int)AgencyEnums.Zone_Huntersville:
                    case (int)AgencyEnums.Zone_UMC:
                    case (int)AgencyEnums.Zone_Cty_Chrlt:
                    case (int)AgencyEnums.Zone_County:
                        data.ZoneUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.Fire_Davidson:
                    case (int)AgencyEnums.Fire_Cornelius:
                    case (int)AgencyEnums.Fire_Pineville:
                    case (int)AgencyEnums.Fire_Matthews:
                    case (int)AgencyEnums.Fire_Mint_Hill:
                    case (int)AgencyEnums.Fire_Huntersville:
                    case (int)AgencyEnums.Fire_UMC:
                    case (int)AgencyEnums.Fire_Cty_Chrlt:
                    case (int)AgencyEnums.Fire_County:
                        data.FireUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Day_Care:
                        data.DayCareUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Food:
                        data.FoodServiceUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Pool:
                        data.PoolUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.EH_Facilities:
                        data.FacilityUserID = reviewer.AttendeeId; break;
                    case (int)AgencyEnums.Backflow:
                        data.BackFlowUserID = reviewer.AttendeeId; break;
                    default:
                        Console.WriteLine("Not an Agency"); break;
                }
            }

            data.Cycle = prtmp.ProjectCycle.CycleNbr.Value;

            Project project = apihelper.GetProjectDetailsByProjectId(pr.ProjectId);

            ProjectCycleSummary summary = apihelper.GetProjectCycleSummary(project.ID);

            if (prtmp.IsFutureCycle)
            {
                data.IsFutureCycle = true;
                data.ScheduleAfterDate = prtmp.ScheduleAfterDate.Value;

                foreach (PlanReviewScheduleDetail detail in summary.PlanReviewScheduleDetailsFuture)
                {
                    switch (detail.BusinessRefId)
                    {
                        case (int)TradeEnums.Building:
                            data.UpdatedBuildingHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)TradeEnums.Electrical:
                            data.UpdatedElectricHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)TradeEnums.Mechanical:
                            data.UpdatedMechHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)TradeEnums.Plumbing:
                            data.UpdatedPlumbHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.EH_Day_Care:
                            data.UpdatedDayCareHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.EH_Food:
                            data.UpdatedFoodHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.EH_Pool:
                            data.UpdatedPoolHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.EH_Facilities:
                            data.UpdatedLodgeHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.Backflow:
                            data.UpdatedBackflowHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.Zone_Davidson:
                        case (int)AgencyEnums.Zone_Cornelius:
                        case (int)AgencyEnums.Zone_Pineville:
                        case (int)AgencyEnums.Zone_Matthews:
                        case (int)AgencyEnums.Zone_Mint_Hill:
                        case (int)AgencyEnums.Zone_Huntersville:
                        case (int)AgencyEnums.Zone_UMC:
                        case (int)AgencyEnums.Zone_Cty_Chrlt:
                        case (int)AgencyEnums.Zone_County:
                            data.UpdatedZoneHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                        case (int)AgencyEnums.Fire_Davidson:
                        case (int)AgencyEnums.Fire_Cornelius:
                        case (int)AgencyEnums.Fire_Pineville:
                        case (int)AgencyEnums.Fire_Matthews:
                        case (int)AgencyEnums.Fire_Mint_Hill:
                        case (int)AgencyEnums.Fire_Huntersville:
                        case (int)AgencyEnums.Fire_UMC:
                        case (int)AgencyEnums.Fire_Cty_Chrlt:
                        case (int)AgencyEnums.Fire_County:
                            data.UpdatedFireHours = detail.AssignedHoursNbr.HasValue ? detail.AssignedHoursNbr.Value : 0; break;
                    }
                }
            }

            data.RecIdTxt = project.RecIdTxt;

            AutoScheduledPlanReviewValues ret = ScheduleAPIHelper.GetAutoScheduledDataPlanReview(data);

            if (data.BuildingUserID != 0 && data.BuildingUserID != -1)
            {
                prtmp.BuildStartDate = ret.BuildingScheduleStart;
                prtmp.BuildEndDate = ret.BuildingScheduleEnd;
            }
            else
            {
                prtmp.BuildStartDate = DateTime.MinValue;
                prtmp.BuildEndDate = DateTime.MinValue;
            }
            if (data.ElectricUserID != 0 && data.ElectricUserID != -1)
            {
                prtmp.ElectStartDate = ret.ElectricScheduleStart;
                prtmp.ElectEndDate = ret.ElectricScheduleEnd;
            }
            else
            {
                prtmp.ElectStartDate = DateTime.MinValue;
                prtmp.ElectEndDate = DateTime.MinValue;
            }
            if (data.MechUserID != 0 && data.MechUserID != -1)
            {
                prtmp.MechaStartDate = ret.MechScheduleStart;
                prtmp.MechaEndDate = ret.MechScheduleEnd;
            }
            else
            {
                prtmp.MechaStartDate = DateTime.MinValue;
                prtmp.MechaEndDate = DateTime.MinValue;
            }
            if (data.PlumbUserID != 0 && data.PlumbUserID != -1)
            {
                prtmp.PlumbStartDate = ret.PlumbScheduleStart;
                prtmp.PlumbEndDate = ret.PlumbScheduleEnd;
            }
            else
            {
                prtmp.PlumbStartDate = DateTime.MinValue;
                prtmp.PlumbEndDate = DateTime.MinValue;
            }
            if (data.ZoneUserID != 0 && data.ZoneUserID != -1)
            {
                prtmp.ZoneStartDate = ret.ZoneScheduleStart;
                prtmp.ZoneEndDate = ret.ZoneScheduleEnd;
            }
            else
            {
                prtmp.ZoneStartDate = DateTime.MinValue;
                prtmp.ZoneEndDate = DateTime.MinValue;
            }
            if (data.FireUserID != 0 && data.FireUserID != -1)
            {
                prtmp.FireStartDate = ret.FireScheduleStart;
                prtmp.FireEndDate = ret.FireScheduleEnd;
            }
            else
            {
                prtmp.FireStartDate = DateTime.MinValue;
                prtmp.FireEndDate = DateTime.MinValue;
            }
            if (data.PoolUserID != 0 && data.PoolUserID != -1)
            {
                prtmp.PoolStartDate = ret.PoolScheduleStart;
                prtmp.PoolEndDate = ret.PoolScheduleEnd;
            }
            else
            {
                prtmp.PoolStartDate = DateTime.MinValue;
                prtmp.PoolEndDate = DateTime.MinValue;
            }
            if (data.FacilityUserID != 0 && data.FacilityUserID != -1)
            {
                prtmp.FacilStartDate = ret.FacilityScheduleStart;
                prtmp.FacilEndDate = ret.FacilityScheduleEnd;
            }
            else
            {
                prtmp.FacilStartDate = DateTime.MinValue;
                prtmp.FacilEndDate = DateTime.MinValue;
            }
            if (data.DayCareUserID != 0 && data.DayCareUserID != -1)
            {
                prtmp.DaycStartDate = ret.DayCareScheduleStart;
                prtmp.DaycEndDate = ret.DayCareScheduleEnd;
            }
            else
            {
                prtmp.DaycStartDate = DateTime.MinValue;
                prtmp.DaycEndDate = DateTime.MinValue;
            }
            if (data.BackFlowUserID != 0 && data.BackFlowUserID != -1)
            {
                prtmp.BackfStartDate = ret.BackFlowScheduleStart;
                prtmp.BackfEndDate = ret.BackFlowScheduleEnd;
            }
            else
            {
                prtmp.BackfStartDate = DateTime.MinValue;
                prtmp.BackfEndDate = DateTime.MinValue;
            }
            if (data.FoodServiceUserID != 0 && data.FoodServiceUserID != -1)
            {
                prtmp.FoodStartDate = ret.FoodScheduleStart;
                prtmp.FoodEndDate = ret.FoodScheduleEnd;
            }
            else
            {
                prtmp.FoodStartDate = DateTime.MinValue;
                prtmp.FoodEndDate = DateTime.MinValue;
            }

            prtmp.PrimaryReviewers = prtmp.AssignedReviewers;
            prtmp.SecondaryReviewers = prtmp.AssignedReviewers;
            prtmp.AssignedFacilitator = apihelper.GetAssignedFacilitator(pr.ProjectId).ToString();
            prtmp.AccelaProjectRefId = pr.AccelaProjectRefId;
            prtmp.IsReschedule = false;
            prtmp.UpdateProjectStatus = true;
            prtmp.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Scheduled;
            prtmp.AutoScheduled = true;

            if (apihelper.UpsertPlanReview(prtmp))
            {
                success = true;
                status = "Scheduled";
            }
            return Json(new { status = status, success = success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFiveAvailDatesForExpress(RequestExpressDatesManagerModel model) {
            APIHelper apihelper = new APIHelper();
            List<DateTime> dateTimes = apihelper.GetAvailDatesForExpress(model);
            return Json(dateTimes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectsDashboard(CustmrProjectsDashboardViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            if (vm == null || string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });
            }
            else
            {
                vm.LoggedInUserEmail = vm.LoggedInUserEmail;
            }

            UpdateUserAndPermissions(vm);

            vm.Facilitators = apihelper.GetAllFacilitators();

            vm.ProjectsList = CustomerAPIHelper.GetProjectList(vm.LoggedInUser.ID);
            vm.ProjectsList = vm.ProjectsList.ToList();
            if (TempData["StatusMessage"] != null)
            {
                vm.StatusMessage = TempData["StatusMessage"].ToString();
            }

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            return View(vm);
        }

        public ActionResult UpdateScheduledMeeting(string projectId, int meetingId, string meetingType, int loggedInUserId)
        {
            UpdateScheduledMeetingInformation(meetingId, meetingType, loggedInUserId);

            return RedirectToAction("ScheduledMeetings", new { projectId = projectId, loggedInUserId = loggedInUserId });
        }

        private void UpdateScheduledMeetingInformation(int meetingId, string meetingType, int loggedInUserId)
        {
            APIHelper apiHelper = new APIHelper();

            string status = ((int)AppointmentResponseStatusEnum.Scheduled).ToString();

            UserIdentity loggedInUser;

            if (Session["LoggedInUser"] != null)
            {
                loggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
            }
            else
            {
                loggedInUser = apiHelper.GetUserIdentityByID(loggedInUserId);
            }

            if (meetingType == MeetingTypeEnum.Preliminary.ToStringValue())
            {
                SavePrelimStatus savePrelimStatus = new SavePrelimStatus();
                savePrelimStatus.PrelimID = meetingId;
                savePrelimStatus.ResponseStatusEnumId = status;
                savePrelimStatus.WkrId = loggedInUser.ID.ToString();
                apiHelper.UpdatePrelimStatus(savePrelimStatus);
                AccelaScheduleAPIHelper.AccelaSchedulePrelim(meetingId);
            }
            else
            {
                SaveMeetingStatus saveMeetingStatus = new SaveMeetingStatus();
                saveMeetingStatus.MeetingId = meetingId;
                saveMeetingStatus.Status = status;
                saveMeetingStatus.WkrId = loggedInUser.ID.ToString();
                apiHelper.UpdateMeetingStatus(saveMeetingStatus);
                AccelaScheduleAPIHelper.AccelaScheduleFMA(meetingId);
            }
        }
    }
}