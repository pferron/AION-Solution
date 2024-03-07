using AION.Base;
using System.Web.Http;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class ProjectFacilitatorController : BaseApiController
    {
        // GET: ProjectFacilitator
        public ProjectFacilitatorController()
        {
        }

        ///// <summary>
        /////  GetAssignedFacilitator
        ///// </summary>
        ///// <param name="model">object  ProjectEstimation </param>
        ///// <returns></returns>
        //[HttpGet]
        //[ResponseType(typeof(bool))]
        //[ActionName("GetAssignedFacilitator")]
        //[Route("api/ProjectFacilitator/GetAssignedFacilitator")]
        //public IHttpActionResult GetAssignedFacilitator(ProjectEstimation model)
        //{
        //    IProjectFacilitatorAdapter thisengine = new ProjectFacilitatorAdapter();
        //    var result = thisengine.GetAssignedFacilitator(model);

        //    return Ok(result);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="model">Json string of Project Model</param>
        ///// <returns></returns>
        //[HttpGet]
        //[ResponseType(typeof(bool))]
        //[ActionName("GetFacilitator")]
        //[Route("api/ProjectFacilitator/GetAssignedFacilitator")]
        //public IHttpActionResult GetFacilitator(Project model)
        //{
        //    IProjectFacilitatorAdapter thisengine = new ProjectFacilitatorAdapter();

        //    var result = thisengine.GetFacilitator(model);

        //    return Ok(result);
        //}
    }
}