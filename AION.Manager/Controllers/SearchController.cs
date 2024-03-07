using AION.Base;
using AION.BL.Models;
using AION.Manager.Adapters;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class SearchController : BaseApiController
    {
        /// <summary>
        /// SearchProjects Search Projects and Preliminary Meetings by filter list
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="projectNumber"></param>
        /// <param name="projectName"></param>
        /// <param name="projectAddress"></param>
        /// <param name="customerName"></param>
        /// <param name="planReviewer"></param>
        /// <param name="projectStatus"></param>
        /// <param name="estimatorId"></param>
        /// <param name="facilitatorId"></param>
        /// <param name="meetingType"></param>
        /// <returns> string of List<ProjectSearchResult></returns>
        [HttpGet]
        [ResponseType(typeof(List<ProjectSearchResult>))] // create a project search result on manager side
        [Route("api/Search/SearchProjects")]
        public IHttpActionResult SearchProjects(DateTime? startDate, DateTime? endDate, string projectNumber, string projectName, string projectAddress,
            string customerName, string planReviewer, int? projectStatus = null, int? estimatorId = null, int? facilitatorId = null, int? meetingType = null)
        {
            ISearchAdapter thisengine = new SearchAdapter();

            var result = thisengine.SearchProjects(startDate, endDate, projectNumber, projectName, projectAddress,
                customerName, planReviewer, projectStatus, estimatorId, facilitatorId, meetingType);

            return Ok(result);
        }
    }
}