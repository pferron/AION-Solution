using AION.Base;
using AION.BL.Adapters;
using AION.BL.Controller;
using AION.BL.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class EstimationAccelaController : BaseApiController
    {
        // EstimationAccela
        public EstimationAccelaController()
        {
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectEstimation>))]
        [ActionName("GetProjectEstimationList")]
        [Route("api/EstimationAccela/GetProjectEstimationList")]
        public IHttpActionResult GetProjectEstimationList()
        {
            EstimationAccelaAdapter thisAdapter = new EstimationAccelaAdapter();
            var mAccelaProjects = thisAdapter.GetProjectEstimationList();

            return Ok(mAccelaProjects);
        }

        /// <summary>
        /// GetAllAgencies
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        // public List<string> GetAllAgencies(ProjectParms parms)
        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [ActionName("GetAllAgencies")]
        [Route("api/EstimationAccela/GetAllAgencies")]
        public IHttpActionResult GetAllAgencies(ProjectParms parms)
        {
            IEstimationAccelaAdapter thisAdapter = new EstimationAccelaAdapter();
            var mAgencies = thisAdapter.GetAllAgencies(parms);

            return Ok(mAgencies);
        }

        /// <summary>
        /// GetAllEstimators
        /// </summary>
        /// <returns></returns>
        // public List<EstimatorUIModel> GetAllEstimators()
        [HttpGet]
        [ResponseType(typeof(List<EstimatorUIModel>))]
        [ActionName("GetAllEstimators")]
        [Route("api/EstimationAccela/GetAllEstimators")]
        public IHttpActionResult GetAllEstimators()
        {
            IEstimationAccelaAdapter thisAdapter = new EstimationAccelaAdapter();
            var mEstimators = thisAdapter.GetAllEstimators();

            return Ok(mEstimators);
        }

        /// <summary>
        ///  GetAllFacilitators
        /// </summary>
        /// <returns></returns>
        // public List<Facilitator> GetAllFacilitators()
        [HttpGet]
        [ResponseType(typeof(List<Facilitator>))]
        [ActionName("GetAllFacilitators")]
        [Route("api/EstimationAccela/GetAllFacilitators")]
        public IHttpActionResult GetAllFacilitators()
        {
            IEstimationAccelaAdapter thisAdapter = new EstimationAccelaAdapter();
            var mFacilitators = thisAdapter.GetAllFacilitators();

            return Ok(mFacilitators);
        }

        /// <summary>
        /// GetAllReviewers
        /// </summary>
        /// <returns></returns>
        // public List<Reviewer> GetAllReviewers()
        [HttpGet]
        [ResponseType(typeof(List<Reviewer>))]
        [ActionName("GetAllReviewers")]
        [Route("api/EstimationAccela/GetAllReviewers")]
        public IHttpActionResult GetAllReviewers(bool isExpressSched = false, bool isSchedulable = false)
        {
            IEstimationAccelaAdapter thisAdapter = new EstimationAccelaAdapter();
            var mReviewers = thisAdapter.GetAllReviewers(isExpressSched, isSchedulable);

            return Ok(mReviewers);
        }

        /// <summary>
        /// Used by Function to get the new autoestimation calcs each month
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetNewAutoEstimationFactors()
        {
            IProjectAutoEstimationEngine thisengine = new ProjectAutoEstimationEngine();
            var success = thisengine.CalculateAverageEstimationHoursFactors();
            return Ok(success);
        }
    }
}