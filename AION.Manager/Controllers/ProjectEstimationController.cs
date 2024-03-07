using AION.Base;
using AION.BL.Adapters;
using AION.BL.Controller;
using AION.BL.Models;
using AION.Manager.Models;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class ProjectEstimationController : BaseApiController
    {
        [HttpPost]
        [ResponseType(typeof(EstimationModel))]
        [Route("api/ProjectEstimation/GetEstimationModel")]
        public IHttpActionResult GetEstimationModel(ProjectParms projectParms)
        {
            IProjectEstimationAdapter thisengine = new ProjectEstimationAdapter();

            var result = thisengine.GetEstimationModel(projectParms);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(BulkEstimationModel))]
        [Route("api/ProjectEstimation/GetBulkEstimationModel")]
        public IHttpActionResult GetBulkEstimationModel()
        {
            IProjectEstimationAdapter thisengine = new ProjectEstimationAdapter();

            var result = thisengine.GetBulkEstimationModel();

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(ProjectEstimation))]
        [Route("api/ProjectEstimation/ExecuteProjectEstimation")]
        public IHttpActionResult ExecuteProjectEstimation(ProjectEstimationManagerModel obj)
        {
            IProjectAutoEstimationEngine thisengine = new ProjectAutoEstimationEngine();

            var mProjectEstimation = thisengine.ExecuteProjectEstimation(obj.ProjInfo, obj.ForceAutoCalc);

            return Ok(mProjectEstimation);
        }


        [HttpPost]
        [ResponseType(typeof(ProjectEstimation))]
        [Route("api/ProjectEstimation/ExecutePreliminaryProjectEstimation")]
        public IHttpActionResult ExecutePreliminaryProjectEstimation(ProjectEstimationManagerModel obj)
        {
            IPreliminaryProjectEstimationEngine thisengine = new PreliminaryProjectEstimationEngine();

            var mProjectEstimation = thisengine.ExecutePreliminaryProjectEstimation(obj.ProjInfo);

            return Ok(mProjectEstimation);
        }

        [HttpGet]
        [ResponseType(typeof(ProjectRtapMapping))]
        [Route("api/ProjectEstimation/GetProjectRtapMapping")]
        public IHttpActionResult GetProjectRtapMapping(int projectId)
        {
            IProjectEstimationAdapter thisengine = new ProjectEstimationAdapter();

            var result = thisengine.GetProjectRtapMapping(projectId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("api/ProjectEstimation/GetAssignedFacilitator")]
        public IHttpActionResult GetProjectEstimationByProjectId(int projectId)
        {
            IProjectEstimationAdapter thisengine = new ProjectEstimationAdapter();

            var result = thisengine.GetAssignedFacilitator(projectId);

            return Ok(result);
        }

    }
}