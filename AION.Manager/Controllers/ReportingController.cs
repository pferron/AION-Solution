using AION.Base;
using AION.Manager.Adapters;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Manager.Controllers
{
    [Authorize]
    public class ReportingController : BaseApiController
    {
        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Reporting/GenerateSchedulingLeadTimeData")]
        public IHttpActionResult GenerateSchedulingLeadTimeData(int wkrId)
        {
            ReportingAdapter thisadapter = new ReportingAdapter();

            var result = thisadapter.GenerateSchedulingLeadTimeData(wkrId);

            return Ok(result);
        }


    }
}