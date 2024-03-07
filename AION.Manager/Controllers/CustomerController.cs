using AION.Base;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]

    public class CustomerController : BaseApiController
    {
        // EstimationCRUD
        public CustomerController()
        {
        }

        // string of ProjectEstimation GetProjectDetailsForEstimationByAccelaId(string accelaModelProjectId);
        /// <summary>
        /// GetProjectDetailsForEstimationByAccelaId
        /// </summary>
        /// <param name="accelaModelProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<CustmrMeetings>))]
        [ActionName("GetMeetingsByUserID")]
        [Route("api/Customer/GetMeetingsByUserID")]
        public IHttpActionResult GetMeetingsByUserID(int userId)
        {

            SchedulerAdapter thisAdapter = new SchedulerAdapter();
            List<CustmrMeetings> ret = thisAdapter.GetMeetingsByUserID(userId);

            return Ok(ret);
        }

        [HttpGet]
        [ResponseType(typeof(List<InternalMeetings>))]
        [ActionName("GetInternalMeetingsByUserID")]
        [Route("api/Customer/GetInternalMeetingsByUserID")]
        public IHttpActionResult GetInternalMeetingsByUserID(int userId)
        {

            SchedulerAdapter thisAdapter = new SchedulerAdapter();
            List<InternalMeetings> ret = thisAdapter.GetInternalMeetings(userId);

            return Ok(ret);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [ActionName("UpdatePrelimDateRequest")]
        [Route("api/Customer/UpdatePrelimDateRequest")]
        public IHttpActionResult UpdatePrelimDateRequest(RequestPrelimDatesManagerModel model)
        {

            ISchedulerAdapter thisAdapter = new SchedulerAdapter();
            var result = thisAdapter.UpdatePrelimDateRequest(model);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [ActionName("UpdatMeetingDateRequest")]
        [Route("api/Customer/UpdateMeetingDateRequest")]
        public IHttpActionResult UpdateMeetingDateRequest(RequestMeetingDatesManagerModel model)
        {

            ISchedulerAdapter thisAdapter = new SchedulerAdapter();
            var result = thisAdapter.UpdateMeetingDateRequest(model);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [ActionName("UpdateExpressDateRequest")]
        [Route("api/Customer/UpdateExpressDateRequest")]
        public IHttpActionResult UpdateExpressDateRequest(RequestExpressDatesManagerModel model)
        {
            IPlanReviewAdapter thisAdapter = new PlanReviewAdapter();
            var result = thisAdapter.UpdateExpressDateRequest(model);
            return Ok(result);
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [ActionName("UpdatePrelimStatus")]
        [Route("api/Customer/UpdatePrelimStatus")]
        public IHttpActionResult UpdatePrelimStatus(SavePrelimStatus model)
        {
            ISchedulerAdapter thisAdapter = new SchedulerAdapter();
            var result = thisAdapter.UpdatePrelimStatusFromCustomer(model);


            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [ActionName("UpdateMeetingStatus")]
        [Route("api/Customer/UpdateMeetingStatus")]
        public IHttpActionResult UpdateMeetingStatus(SaveMeetingStatus model)
        {
            ISchedulerAdapter thisAdapter = new SchedulerAdapter();
            var result = thisAdapter.UpdateMeetingStatus(model);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectEstimation>))]
        [Route("api/Customer/GetProjectList")]
        public IHttpActionResult GetProjectList(int loggedinUserId)
        {
            DashboardAdapter thisAdapter = new DashboardAdapter();
            var mAccelaProjects = thisAdapter.GetProjects(loggedinUserId);

            return Ok(mAccelaProjects);
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/Customer/CreatePlanReviewAcceptanceEmail")]
        public IHttpActionResult CreatePlanReviewAcceptanceEmail(PlanReviewEmailModel model)
        {
            PlanReviewAdapter thisAdapter = new PlanReviewAdapter();
            var email = thisAdapter.CreatePlanReviewAcceptanceEmail(model);

            return Ok(email);
        }

        [HttpPost]
        [ResponseType(typeof(List<DateTime>))]
        [Route("api/Customer/SearchAvailableExpressDates")]
        public IHttpActionResult SearchAvailableExpressDates(RequestExpressDatesManagerModel model)
        {
            SchedulerAdapter thisengine = new SchedulerAdapter();
            var datelist = thisengine.GetAvailDateForExpress(model);
            return Ok(datelist);
        }

    }
}