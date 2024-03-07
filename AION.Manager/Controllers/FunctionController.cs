using AION.Base;
using AION.BL.Adapters;
using AION.Estimator.Engine.BusinessEntities;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Manager.Controllers
{
    [Authorize]
    public class FunctionController : BaseApiController
    {
        public FunctionController()
        {
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/CancelPrelimMeeting")]
        public IHttpActionResult CancelPrelimMeeting()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelPrelimMeeting();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/CancelScheduledExpressPlanReview")]
        public IHttpActionResult CancelScheduledExpressPlanReview()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelScheduledExpressPlanReview();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/CancelSchedulePlanReview")]
        public IHttpActionResult CancelSchedulePlanReview()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelSchedulePlanReview();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/CancelFacilitatorMeetingAppointment")]
        public IHttpActionResult CancelFacilitatorMeetingAppointment()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelFacilitatorMeetingAppointment();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/CancelReserveExpressReservation")]
        public IHttpActionResult CancelReserveExpressReservation()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelReserveExpressReservation();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/CancelMeetingSavedUserSchedules")]
        public IHttpActionResult CancelMeetingSavedUserSchedules()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.CancelMeetingSavedUserSchedules();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/UpdatePlanReviewerHoursByAccela")]
        public IHttpActionResult UpdatePlanReviewerHoursByAccela()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.UpdatePlanReviewStatusByAccela();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/ProcessCalendarEventQueueRecords")]
        public IHttpActionResult ProcessCalendarEventQueueRecords(bool inProcess, string environment)
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.ProcessCalendarEventQueueRecords(inProcess, environment);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/AddEligibleUsersToExistingNPAs")]
        public IHttpActionResult AddEligibleUsersToExistingNPAs()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.AddEligibleUsersToExistingNPAs(true);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/SyncAccelaAION")]
        public IHttpActionResult SyncAccelaAION()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.SyncAccelaAION();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<int>))]
        [Route("api/Function/GetFIFOProjectIdsToBeOptimized")]
        public IHttpActionResult GetFIFOProjectIdsToBeOptimized()
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.GetFIFOProjectIdsToBeOptimized();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Function/OptimizeFIFOProject")]
        public IHttpActionResult OptimizeFIFOProject(int projectId)
        {
            IFunctionAdapter thisengine = new FunctionAdapter();
            var result = thisengine.OptimizeFIFOProject(projectId);
            return Ok(result);
        }
    }
}