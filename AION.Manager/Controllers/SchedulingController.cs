using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.Common;
using AION.Manager.Models;
using AION.Manager.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Manager.Controllers
{
    [Authorize]
    public class SchedulingController : BaseApiController
    {
        public SchedulingController()
        {
        }

        [HttpPost]
        [ResponseType(typeof(SchedulingModel))]
        [Route("api/Scheduling/GetSchedulingModel")]
        public IHttpActionResult GetSchedulingModel(ProjectParms projectParms)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.GetSchedulingModel(projectParms);

            return Ok(result);
        }

        /// <summary>
        /// Returns list of Projects that are Unscheduled for the
        /// Scheduling Dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(DashboardListBase))]
        [Route("api/Scheduling/GetSchedulingDashboardList")]
        public IHttpActionResult GetSchedulingDashboardList(int userid)
        {
            IDashboardAdapter thisAdapter = new DashboardAdapter();
            var mAccelaProjects = thisAdapter.GetSchedulingDashboardListBase(userid);

            return Ok(mAccelaProjects);
        }

        /// <summary>
        ///  UpsertPMA
        /// </summary>
        /// <param name="objectPMA">object of PreliminaryMeetingAppointment</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Scheduling/UpsertPMA")]
        public IHttpActionResult UpsertPMA(PreliminaryMeetingAppointment objectPMA)
        {
            IPMAAdapter thisengine = new PMAAdapter(objectPMA);
            foreach (AttendeeInfo attendee in objectPMA.AssignedReviewers)
            {
                int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendee.DeptNameEnumId).ID;
                attendee.BusinessRefId = businessRefId;
            }
            var result = thisengine.Upsert();
            return Ok(result);
        }



        /// <summary>
        /// Get useridentity list by PMA for add attendees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<AutoScheduledPrelimValues>))]
        [Route("api/Scheduling/GetAutoScheduledData")]
        public IHttpActionResult GetAutoScheduledData(AutoScheduledPrelimParams model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.GetPrelimAutoScheduledData(model);

            return Ok(result);

        }


        /// <summary>
        /// Get useridentity list by PMA for add attendees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<AutoScheduledPlanReviewValues>))]
        [Route("api/Scheduling/GetAutoScheduledDataPlanReview")]
        public IHttpActionResult GetAutoScheduledDataPlanReview(AutoScheduledPlanReviewParams model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.GetAutoScheduledDataPlanReview(model);

            return Ok(result);

        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/ManualScheduleCapacity")]
        public IHttpActionResult ManualScheduleCapacity(SchedulePlanReviewCapacityParams model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.ManualScheduleCapacity(model);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(AutoScheduledExpressUIValues))]
        [Route("api/Scheduling/GetAutoScheduledDataExpress")]
        public IHttpActionResult GetAutoScheduledDataExpress(AutoScheduledExpressParams model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();
            AutoScheduledExpressUIValues item = new AutoScheduledExpressUIValues();
            UserIdentityModelBO userIdentityBO = new UserIdentityModelBO();

            AutoScheduledExpressValues result = thisengine.GetAutoScheduledDataExpress(model);

            //get the UI values
            //get the reviewer names
            //TODO: check for zero
            UserIdentity reviewer = new UserIdentity();
            reviewer = userIdentityBO.GetInstance(result.BuildingUserID);
            item.BuildingUserID = result.BuildingUserID;
            item.BuildingUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.ElectricUserID);
            item.ElectricUserID = result.ElectricUserID;
            item.ElectricUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.MechUserID);
            item.MechUserID = result.MechUserID;
            item.MechUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.PlumbUserID);
            item.PlumbUserID = result.PlumbUserID;
            item.PlumbUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.FireUserID);
            item.FireUserID = result.FireUserID;
            item.FireUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.ZoneUserID);
            item.ZoneUserID = result.ZoneUserID;
            item.ZoneUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.BackFlowUserID);
            item.BackFlowUserID = result.BackFlowUserID;
            item.BackFlowUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.FoodServiceUserID);
            item.FoodServiceUserID = result.FoodServiceUserID;
            item.FoodServiceUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.PoolUserID);
            item.PoolUserID = result.PoolUserID;
            item.PoolUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.FacilityUserID);
            item.FacilityUserID = result.FacilityUserID;
            item.FacilityUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.DayCareUserID);
            item.DayCareUserID = result.DayCareUserID;
            item.DayCareUserName = GetReviewerName();

            //get the meeting room name
            MeetingRoom meetingRoom = result.MeetingRoomId > 0 ? new MeetingRoomBO().GetById(result.MeetingRoomId) : new MeetingRoom();
            item.MeetingRoomId = result.MeetingRoomId;
            item.MeetingRoomName = meetingRoom != null && result.MeetingRoomId > 0 ? meetingRoom.MeetingRoomName : "Not Selected";

            item.SelectedStartDateTime = result.SelectedStartDateTime;
            item.SelectedEndDateTime = result.SelectedEndDateTime;

            item.ErrorMessage = result.ErrorMessage;

            string GetReviewerName()
            {
                if (reviewer != null)
                {
                    if (reviewer.ID == 0)
                        return "Not Selected";
                    if (reviewer.ID == -1)
                        return "NA";
                    return reviewer.FirstName + " " + reviewer.LastName;
                }
                return "Not Selected";
            }

            return Ok(item);

        }

        [HttpPost]
        [ResponseType(typeof(AutoScheduledFacilitatorMeetingUIValues))]
        [Route("api/Scheduling/GetAutoScheduledDataFacilitatorMeeting")]
        public IHttpActionResult GetAutoScheduledDataFacilitatorMeeting(AutoScheduledFacilitatorMeetingParams model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();
            AutoScheduledFacilitatorMeetingUIValues item = new AutoScheduledFacilitatorMeetingUIValues();
            UserIdentityModelBO userIdentityBO = new UserIdentityModelBO();

            AutoScheduledFacilitatorMeetingValues result = thisengine.GetAutoScheduledDataFacilitatorMeeting(model);

            //get the UI values
            //get the reviewer names
            //TODO: check for zero
            UserIdentity reviewer = new UserIdentity();
            reviewer = userIdentityBO.GetInstance(result.BuildingUserID);
            item.BuildingUserID = result.BuildingUserID;
            item.BuildingUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.ElectricUserID);
            item.ElectricUserID = result.ElectricUserID;
            item.ElectricUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.MechUserID);
            item.MechUserID = result.MechUserID;
            item.MechUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.PlumbUserID);
            item.PlumbUserID = result.PlumbUserID;
            item.PlumbUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.FireUserID);
            item.FireUserID = result.FireUserID;
            item.FireUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.ZoneUserID);
            item.ZoneUserID = result.ZoneUserID;
            item.ZoneUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.BackFlowUserID);
            item.BackFlowUserID = result.BackFlowUserID;
            item.BackFlowUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.FoodServiceUserID);
            item.FoodServiceUserID = result.FoodServiceUserID;
            item.FoodServiceUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.PoolUserID);
            item.PoolUserID = result.PoolUserID;
            item.PoolUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.FacilityUserID);
            item.FacilityUserID = result.FacilityUserID;
            item.FacilityUserName = GetReviewerName();
            reviewer = userIdentityBO.GetInstance(result.DayCareUserID);
            item.DayCareUserID = result.DayCareUserID;
            item.DayCareUserName = GetReviewerName();
            item.DurationHours = result.DurationHours;
            item.DurationMinutes = result.DurationMinutes;

            //get the meeting room name
            MeetingRoom meetingRoom = result.MeetingRoomId > 0 ? new MeetingRoomBO().GetById(result.MeetingRoomId) : new MeetingRoom();
            item.MeetingRoomId = result.MeetingRoomId;
            item.MeetingRoomName = meetingRoom != null && result.MeetingRoomId > 0 ? meetingRoom.MeetingRoomName : "Not Selected";

            item.SelectedStartDateTime = result.SelectedStartDateTime;
            item.SelectedEndDateTime = result.SelectedEndDateTime;

            item.AdditionalAttendeeIds = result.AdditionalAttendeeIds;

            string GetReviewerName()
            {
                if (reviewer != null)
                {
                    if (reviewer.ID == 0)
                        return "Not Selected";
                    if (reviewer.ID == -1)
                        return "NA";
                    return reviewer.FirstName + " " + reviewer.LastName;
                }
                return "Not Selected";
            }

            return Ok(item);
        }

        /// <summary>
        /// Get useridentity list by PMA for add attendees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<UserIdentity>))]
        [Route("api/Scheduling/GetAttendeesByPmaId")]
        public IHttpActionResult GetAttendeesByPmaId(int id)
        {
            PreliminaryMeetingAppointment pma = new PreliminaryMeetingApptModelBO().GetInstance(id);

            IAppointmentAdapter thisengine = new PMAAdapter(pma);

            var result = thisengine.GetAttendeesByApptId(id);

            return Ok(result);

        }

        /// <summary>
        /// Get PMA by Project ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(PreliminaryMeetingAppointment))]
        [Route("api/Scheduling/GetPMAById")]
        public IHttpActionResult GetPMAById(int id)
        {
            IPMAAccessor thisengine = new PMAAccessor();

            var result = thisengine.GetByProjectId(id);

            return Ok(result);

        }

        /// <summary>
        /// Get useridentity list by PMA for add attendees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CustomerMeetingsList))]
        [Route("api/Scheduling/GetSchedMeetingsByProjectId")]
        public IHttpActionResult GetSchedMeetingsByProjectId(string projectId)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            CustomerMeetingsList customerMeetingsList = new CustomerMeetingsList();
            customerMeetingsList.CustSchedMeetings = thisengine.GetSchedMeetingsByProjectId(projectId);
            int projectStatusRefId = new ProjectBO().GetByExternalRefInfo(projectId).ProjectStatusRefId.Value;
            customerMeetingsList.ProjectStatus = new ProjectStatusModelBO().GetInstance(projectStatusRefId).ProjectStatusEnum.ToStringValue();
            var result = customerMeetingsList;

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelMeetingById")]
        public IHttpActionResult CancelMeetingById(CancelMeetingModel model)
        {
            IFMAAdapter thisengine = new FMAAdapter();

            var result = thisengine.CancelMeetingById(model);

            return Ok(result);
        }

        /// <summary>
        /// Gets the Scheduled Meetings by Accela Project ID
        /// Used on Project Details dashboard pages
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<InternalMeetings>))]
        [Route("api/Scheduling/GetScheduledMeetingListByProjectId")]
        public IHttpActionResult GetScheduledMeetingListByProjectId(string id, string recidtxt)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();
            ProjectParms projectparms = new ProjectParms
            {
                ProjectId = id,
                RecIdTxt = recidtxt
            };
            var result = thisengine.GetSchedMeetingListbyProjectId(projectparms);

            return Ok(result);

        }

        /// <summary>
        /// Get all internal meetings
        /// Internal meetings dashboard
        /// TODO: add date range
        /// </summary>
        /// <param name="wrkId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(DashboardListBase))]
        [Route("api/Scheduling/GetInternalMeetings")]
        public IHttpActionResult GetInternalMeetings(int wrkId)
        {

            IDashboardAdapter thisAdapter = new DashboardAdapter();
            DashboardListBase ret = thisAdapter.GetInternalMeetingsListBase(wrkId);

            return Ok(ret);
        }

        /// <summary>
        /// Get all internal meetings
        /// Internal meetings dashboard
        /// TODO: add date range
        /// </summary>
        /// <param name="wrkId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<InternalMeetings>))]
        [Route("api/Scheduling/GetConfigureReserveExpressList")]
        public IHttpActionResult GetConfigureReserveExpressList()
        {

            SchedulerAdapter thisAdapter = new SchedulerAdapter();
            List<ConfigureReserveExpressDays> ret = thisAdapter.GetConfigureReserveExpressList();

            return Ok(ret);
        }

        /// <summary>
        /// Get saved reviewer rotation by business unit
        /// Express main config
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<ReserveExpressPlanReviewer>))]
        [Route("api/Scheduling/GetReserveExpressPlanReviewerListAll")]
        public IHttpActionResult GetReserveExpressPlanReviewerListAll()
        {
            IExpressAdapter thisAdapter = new ExpressAdapter();
            List<ReserveExpressPlanReviewer> ret = thisAdapter.GetReserveExpressPlanReviewerListAll();

            return Ok(ret);
        }

        /// <summary>
        /// Search Schedule Capacity
        /// Reviewer Hours per person,
        /// plan review total hours, express hours, npa hours, meetings
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<ScheduleCapacitySearchResult>))]
        [Route("api/Scheduling/SearchScheduleCapacity")]
        public IHttpActionResult SearchScheduleCapacity(ScheduleCapacitySearch search)
        {
            IScheduleCapacityAdapter thisAdapter = new ScheduleCapacityAdapter();

            List<ScheduleCapacitySearchResult> ret = thisAdapter.ScheduleCapacitySearch(search);

            return Ok(ret);

        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/SaveConfigureExpress")]
        public IHttpActionResult SaveConfigureExpress(List<ConfigureReserveExpressDays> days)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.SaveConfigureExpress(days);

            return Ok(result);


        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/SaveExpressPlanReviewerRotation")]
        public IHttpActionResult SaveExpressPlanReviewerRotation(List<ReserveExpressPlanReviewer> scheduledReviewers)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            foreach (var reviewer in scheduledReviewers)
            {
                reviewer.BusinessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)reviewer.DeptNameEnumId).ID;
            }

            var result = thisengine.SaveExpressPlanReviewerRotation(scheduledReviewers);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/DeleteExpressPlanReviewerRotation")]
        public IHttpActionResult DeleteExpressPlanReviewerRotation()
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.DeleteExpressPlanReviewerRotation();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/GetAllBusinessRefs")]
        public IHttpActionResult GetAllBusinessRefs()
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.GetAllBusinessRefs();

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/SaveMeetingAction")]
        public IHttpActionResult SaveMeetingAction(ProjectDetailMeetingAction meetingAction)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.SaveMeetingAction(meetingAction);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpdateProjectDetails")]
        public IHttpActionResult UpdateProjectDetails(int project, int status, int updated, DateTime? prod)
        {
            EstimationCRUDAdapter thisengine = new EstimationCRUDAdapter();
            ProjectEstimation pe = thisengine.GetProjectDetailsByProjectId(project);
            pe.UpdatedUser = new UserIdentityModelBO().GetInstance(updated);
            pe.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(status);
            pe.PlansReadyOnDate = prod;

            var result = new ProjectEstimationModelBO().UpdateProjectDetails(pe);

            return Ok(result);
        }

        //[HttpPost]
        //[ResponseType(typeof(bool))]
        //[Route("api/Scheduling/ReschedulePlanReview")]
        //public IHttpActionResult ReschedulePlanReview(PlanReview planReview)
        //{
        //    PlanReviewAdapter thisengine = new PlanReviewAdapter();

        //    var result = thisengine.ReschedulePlanReview(planReview);

        //    return Ok(result);
        //}

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/ScheduleFuturePRCycle")]
        public IHttpActionResult ScheduleFuturePRCycle(PlanReview pr)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.ScheduleFuturePRCycle(pr);

            return Ok(result);
        }


        //[HttpGet]
        //[ResponseType(typeof(List<ScheduleBusinessRelationship>))]
        //[Route("api/Scheduling/GetScheduleBusinessRelationshipListByCycle")]
        //public IHttpActionResult GetScheduleBusinessRelationshipListByCycle(string id, int cycle, string recidtxt)
        //{
        //    SchedulerAdapter thisengine = new SchedulerAdapter();
        //    ProjectParms projectparms = new ProjectParms
        //    {
        //        RecIdTxt = recidtxt,
        //        ProjectId = id,
        //        CycleNbr = cycle
        //    };
        //    var result = thisengine.GetScheduleBusinessRelationshipListByCycle(projectparms);

        //    return Ok(result);
        //}

        //[HttpGet]
        //[ResponseType(typeof(List<ScheduleBusinessRelationship>))]
        //[Route("api/Scheduling/GetScheduleBusinessRelationshipList")]
        //public IHttpActionResult GetScheduleBusinessRelationshipList(int id)
        //{
        //    SchedulerAdapter thisengine = new SchedulerAdapter();

        //    var result = thisengine.GetScheduleBusinessRelationshipList(id);

        //    return Ok(result);
        //}

        //[HttpGet]
        //[ResponseType(typeof(List<ScheduleBusinessRelationship>))]
        //[Route("api/Scheduling/GetScheduleBusinessRelationship")]
        //public IHttpActionResult GetScheduleBusinessRelationship(int id, bool isRTAP, int cycle)
        //{
        //    SchedulerAdapter thisengine = new SchedulerAdapter();

        //    var result = thisengine.GetScheduleBusinessRelationship(id, isRTAP, cycle);

        //    return Ok(result);
        //}

        /// <summary>
        /// Get the Project Cycles for a project
        /// Used by Schedule Plan REview page
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<PlanReview>))]
        [Route("api/Scheduling/GetPlanReviewsByProjectId")]
        public IHttpActionResult GetPlanReviewsByProjectId(string projectId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetPlanReviewsByProjectId(projectId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(ProjectCycle))]
        [Route("api/Scheduling/GetProjectCycleById")]
        public IHttpActionResult GetProjectCycleById(int projectCycleId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetProjectCycleById(projectCycleId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(PlanReview))]
        [Route("api/Scheduling/GetPlanReviewsByProjectCycleId")]
        public IHttpActionResult GetPlanReviewsByProjectCycleId(int projectCycleId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetPlanReviewsByProjectCycle(projectCycleId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectCycle>))]
        [Route("api/Scheduling/GetProjectCyclesByProjectId")]
        public IHttpActionResult GetProjectCyclesByProjectId(int projectId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetProjectCyclesByProjectId(projectId);

            return Ok(result);
        }


        [HttpGet]
        [ResponseType(typeof(List<ProjectCycleDetail>))]
        [Route("api/Scheduling/GetProjectCycleDetailsByProjectCycleId")]
        public IHttpActionResult GetProjectCycleDetailsByProjectCycleId(int projectCycleId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetProjectCycleDetailsByProjectCycleId(projectCycleId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectCycleReview>))]
        [Route("api/Scheduling/GetProjectCycleReviews")]
        public IHttpActionResult GetProjectCycleReviews(int projectId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetProjectCycleReviews(projectId);

            return Ok(result);
        }


        [HttpGet]
        [ResponseType(typeof(ProjectCycleSummary))]
        [Route("api/Scheduling/GetProjectCycleSummary")]
        public IHttpActionResult GetProjectCycleSummary(int projectId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetProjectCycleSummary(projectId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<PlanReviewScheduleDetail>))]
        [Route("api/Scheduling/GetPlanReviewScheduleDetailsByPlanReviewSchedule")]
        public IHttpActionResult GetPlanReviewScheduleDetailsByPlanReviewSchedule(int planReviewScheduleId)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.GetPlanReviewScheduleDetailsByPlanReviewSchedule(planReviewScheduleId);

            return Ok(result);
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpsertPlanReview")]
        public IHttpActionResult UpsertPlanReview(PlanReview item)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();
            //var t = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var result = thisengine.UpsertPlanReview(item);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpdatePlanReviewStatus")]
        public IHttpActionResult UpdatePlanReviewStatus(PlanReview item)
        {
            PlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.UpdatePlanReviewStatus(item, item.ApptResponseStatusEnum);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpdateProjectCycle")]
        public IHttpActionResult UpdateProjectCycle(ProjectCycle projectCycle)
        {
            IPlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.UpdateProjectCycle(projectCycle);

            return Ok(result);
        }


        [HttpGet]
        [ResponseType(typeof(double))]
        [Route("api/Scheduling/AddSchedulingMultiplier")]
        public IHttpActionResult AddSchedulingMultiplier(PropertyTypeEnums propertyType, decimal defaultHrs, DateTime PRstart, DateTime PRend)
        {
            PlanReviewAdapter thisengine = new PlanReviewAdapter();

            var result = thisengine.AddSchedulingMultiplier(propertyType, defaultHrs, PRstart, PRend);

            return Ok(result);
        }

        //[HttpPost]
        //[ResponseType(typeof(bool))]
        //[Route("api/Scheduling/UpdateEMAStatus")]
        //public IHttpActionResult UpdateEMAStatus(ExpressMeetingAppointment item)
        //{
        //    IEMAAdapter thisengine = new EMAAdapter(item);

        //    var result = thisengine.UpdateStatus(item.ApptResponseStatusEnum);

        //    return Ok(result);
        //}

        /// <summary>
        /// Search Schedule Capacity
        /// Get Reviewer Availability
        /// Reviewer Hours per person,
        /// plan review total hours, express hours, npa hours, meetings
        /// </summary>
        /// <param name="searchlist"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<ScheduleCapacitySearchResult>))]
        [Route("api/Scheduling/SearchReviewerCapacity")]
        public IHttpActionResult SearchReviewerCapacity(List<ScheduleCapacitySearch> searchlist)
        {
            IScheduleCapacityAdapter thisAdapter = new ScheduleCapacityAdapter();

            List<ScheduleCapacitySearchResult> ret = thisAdapter.SearchReviewerCapacity(searchlist);

            return Ok(ret);

        }

        /// <summary>
        /// Get FMAs by Project ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<FacilitatorMeetingAppointment>))]
        [Route("api/Scheduling/GetFMAByProjectId")]
        public IHttpActionResult GetFMAByProjectId(int projectId)
        {
            IFMAAccessor thisengine = new FMAAccessor();

            var result = thisengine.GetListByProjectId(projectId);

            return Ok(result);

        }

        /// <summary>
        /// Get FMAs by Project ID and Meeting Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<FacilitatorMeetingAppointment>))]
        [Route("api/Scheduling/GetFMAByProjectIdAndMeetingType")]
        public IHttpActionResult GetFMAByProjectIdAndMeetingType(string projectId, string meetingTypeDesc)
        {
            IFMAAccessor thisengine = new FMAAccessor();

            var result = thisengine.GetByProjectIDAndMeetingType(projectId, meetingTypeDesc);

            return Ok(result);

        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelSchedulePlanReview")]
        public IHttpActionResult CancelSchedulePlanReview()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelSchedulePlanReview();
            return Ok(result);
        }


        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelFacilitatorMeetingAppointment")]
        public IHttpActionResult CancelFacilitatorMeetingAppointment()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelFacilitatorMeetingAppointment();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelScheduledExpressPlanReview")]
        public IHttpActionResult CancelScheduledExpressPlanReview()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelScheduledExpressPlanReview();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelReserveExpressReservation")]
        public IHttpActionResult CancelReserveExpressReservation()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelReserveExpressReservation();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelMeetingSavedUserSchedules")]
        public IHttpActionResult CancelMeetingSavedUserSchedules()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelMeetingSavedUserSchedules();
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/CancelAppointment")]
        public IHttpActionResult CancelAppointment(int meetingId, string meetingType)
        {
            bool result = true;

            //express no longer called here

            Appointment appointment = AppointmentHelper.GetAppointment(meetingId, meetingType);

            IAppointmentAdapter appointmentAdapter;

            if (appointment.GetType() == typeof(FacilitatorMeetingAppointment))
            {
                FacilitatorMeetingAppointment fma = (FacilitatorMeetingAppointment)appointment;
                appointmentAdapter = new FMAAdapter(fma);
                appointmentAdapter.CancelAppointment();
            }

            if (appointment.GetType() == typeof(PreliminaryMeetingAppointment))
            {
                PreliminaryMeetingAppointment pma = (PreliminaryMeetingAppointment)appointment;
                appointmentAdapter = new PMAAdapter(pma);
                appointmentAdapter.CancelAppointment();
            }

            if (appointment.GetType() == typeof(ReserveExpressReservation))
            {
                ReserveExpressReservation exp = (ReserveExpressReservation)appointment;
                appointmentAdapter = new ExpressAdapter(exp);
                appointmentAdapter.CancelAppointment();
            }

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpdatePlanReviewerHoursByAccela")]
        public IHttpActionResult UpdatePlanReviewerHoursByAccela()
        {
            PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();

            var result = planReviewAdapter.UpdatePlanReviewStatusByAccela();

            return Ok(result);
        }

        /// <summary>
        /// Express Scheduling Manually Schedule button
        /// returns reviewers for date time
        /// and meeting room for date time
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/Scheduling/GetManuallyScheduledExpressData")]
        public IHttpActionResult GetManuallyScheduledExpressData(ScheduleCapacitySearch item)
        {
            string result = "";

            return Ok(result);
        }

        /// <summary>
        ///  UpsertFMA
        /// </summary>
        /// <param name="objectFMA">object of FacilitatorMeetingAppointment</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Scheduling/UpsertFMA")]
        public IHttpActionResult UpsertFMA(FacilitatorMeetingAppointment objectFMA)
        {
            foreach (AttendeeInfo attendee in objectFMA.AssignedReviewers)
            {
                int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendee.DeptNameEnumId).ID;
                attendee.BusinessRefId = businessRefId;
            }
            FMAAdapter thisengine = new FMAAdapter(objectFMA);
            var result = thisengine.Upsert();
            return Ok(result);
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpsertFIFO")]
        public IHttpActionResult UpsertFIFO(PlanReview planReview)
        {
            FIFOAdapter thisengine = new FIFOAdapter();

            var result = thisengine.UpsertFIFO(planReview);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Scheduling/UpsertEMA")]
        public IHttpActionResult UpsertEMA(PlanReview planReview)
        {
            IEMAAdapter thisengine = new EMAAdapter();

            var result = thisengine.UpsertEMA(planReview);
            return Ok(result);
        }



        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/AccelaSchedulePlanReview")]
        public IHttpActionResult AccelaSchedulePlanReview(PlanReview pr)
        {
            AccelaBOAdapter thisengine = new AccelaBOAdapter();
            var result = thisengine.SchedulePlanReview(pr);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/AccelaScheduleFMA")]
        public IHttpActionResult AccelaScheduleFMA(int id)
        {
            AccelaBOAdapter thisengine = new AccelaBOAdapter();
            var result = thisengine.ScheduleFMA(id);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/AccelaSchedulePrelim")]
        public IHttpActionResult AccelaSchedulePrelim(int id)
        {
            AccelaBOAdapter thisengine = new AccelaBOAdapter();
            bool result = false;
            try
            {
                result = thisengine.SchedulePrelim(id);
            }
            catch (Exception)
            {
                //TODO: do something to let someone know the Accela send failed
            }
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/AccelaChangeAssignedFacilitator")]
        public IHttpActionResult AccelaChangeAssignedFacilitator(ProjectEstimation project)
        {
            AccelaBOAdapter thisengine = new AccelaBOAdapter();
            bool result = false;
            try
            {
                result = thisengine.ChangeAssignedFacilitator(project);
            }
            catch (Exception)
            {
                //TODO: do something to let someone know the Accela send failed
            }
            return Ok(result);
        }

        /// <summary>
        /// This updates the facilitator by project id
        /// Required: project.ID, project.UpdatedUser.ID, project.AssignedFacilitator.Value, current UpdatedDate
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Scheduling/UpdateAssignedFacilitator")]
        public IHttpActionResult UpdateAssignedFacilitator(ProjectEstimation project)
        {
            ProjectFacilitatorAdapter thisengine = new ProjectFacilitatorAdapter();
            var result = thisengine.UpdateAssignedFacilitator(project);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(List<DateTime>))]
        [Route("api/Scheduling/SearchSelfScheduleCapacity")]
        public IHttpActionResult SearchSelfScheduleCapacity(SchedulePlanReviewCapacityParams model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.SearchSelfScheduleCapacity(model);

            return Ok(result);
        }
    }
}