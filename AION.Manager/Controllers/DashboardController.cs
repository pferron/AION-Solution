using AION.Base;
using AION.Manager.Adapters;
using AION.Manager.Models.Dashboard;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class DashboardController : BaseApiController
    {
        /// <summary>
        /// Gets the project list as a dashboard item list for EstimationDashboard.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(DashboardListBase))]
        [Route("api/Dashboard/GetEstimationDashboardProjectList")]
        public IHttpActionResult GetEstimationDashboardProjectList(int userid, DateTime? fromdate = null, DateTime? todate = null)
        {
            IDashboardAdapter thisengine = new DashboardAdapter();

            var result = thisengine.GetEstimationDashboardListBase(userid: userid);

            return Ok(result);
        }

    }
}