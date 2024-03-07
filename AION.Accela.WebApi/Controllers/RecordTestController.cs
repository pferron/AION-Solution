using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AION.Accela.WebApi.Adapter;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;

using Microsoft.Owin.Security.Provider;
using AION.Accela.WebApi;



namespace AION.Accela.WebApi.Controllers
{
    [Authorize]
    public class RecordTestController : ApiController
    {

        /// <summary>
        /// AuditReceivedRecordsQueUe()
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<AIONQueueRecordBE>))]
        [ActionName("GetReceivedRecordsToProcess")]
        [Route("api/RecordTest/V1/GetReceivedRecordsToProcess")]
        public IHttpActionResult GetReceivedRecordsToProcess()
        {
            AccelaCRUDAdapter thisadapter = new AccelaCRUDAdapter();

            var result = thisadapter.GetReceivedRecordsToProcess();

            return Ok(result);

        }

        /// <summary>
        /// AuditPlanReviewHistory()
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<PlanReviewHistoryAllFields>))]
        [ActionName("AuditPlanReviewHistory")]
        [Route("api/RecordTest/V1/AuditPlanReviewHistory")]
        public IHttpActionResult AuditPlanReviewHistory()
        {
            AccelaCRUDAdapter thisadapter = new AccelaCRUDAdapter();

            var result = thisadapter.AuditPlanReviewHistory();

            return Ok(result);
        }


        /// <summary>
        /// AuditPlanReviewHistory()
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<AccelaAIONAEData>))]
        [ActionName("GetAERecordDetailByRecordId")]
        [Route("api/RecordTest/V1/GetAERecordDetailByRecordId")]
        public IHttpActionResult GetAERecordDetailByRecordId(string recordId)
        {
            AccelaCRUDAdapter thisadapter = new AccelaCRUDAdapter();

            var result = thisadapter.GetAERecordDetailByRecordId(recordId);

            return Ok(result);
        }
    }
}
