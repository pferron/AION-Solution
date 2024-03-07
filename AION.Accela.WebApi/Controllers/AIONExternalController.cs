using AION.Accela.WebApi.Adapter;
using AION.BL.Adapters;
using Meck.Logging;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Accela.WebApi.Controllers
{
    [Authorize]
    public class AIONExternalController : ApiController
    {

        Logger _mLogger = new Logger();
        EmailAdapter _emailAdapter = new EmailAdapter();

        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }
        }


        /// <summary>
        /// for testing for com testing only
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(string))]
        [ActionName("TestPing")]
        [Route("api/AccelaExternal/V1/TestPing")]
        public IHttpActionResult TestPing()
        {
            string pingdatetime = "Ping Test: Server DateTime " + DateTime.Now.ToString() + " [Eastern]  ";

            Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), pingdatetime, ex: null));

            return Ok(pingdatetime);
        }

        //// GET: AccelaToAION
        //public IHttpActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// InsertAIONRecord
        /// </summary>
        /// <param name="inBoundMessage">Json string of Type RecordNotification </param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AIONRecordQueueResponse))]
        [ActionName("InsertAIONRecord")]
        [Route("api/AccelaExternal/V1/InsertAIONRecord")]
        public IHttpActionResult InsertAIONRecord(RecordNotification inComingRecord)
        {
            try
            {
                AccelaCRUDAdapter thisadapter = new AccelaCRUDAdapter();

                var startUpDetails = "InsertAionRecord starting: " + JsonConvert.SerializeObject(inComingRecord);

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), startUpDetails,
                     ex: null));

                var result = Task.Run(() => thisadapter.InsertNewRecordQueue(inComingRecord));


                var mRecordQueueResponse = result;

                AIONRecordQueueResponse aIONRecordQueueResponse = (AIONRecordQueueResponse)result.Result;

                string errormessage = aIONRecordQueueResponse.errors;

                //11-03-2023 jcl if there is an error message, return 400
                if (!string.IsNullOrWhiteSpace(errormessage))
                {
                    _emailAdapter.SendErrorEmail(errormessage);

                    Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errormessage));

                    return BadRequest("Error Occurred: " + errormessage);
                }

                return Ok(mRecordQueueResponse);

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                _emailAdapter.SendErrorEmail(errmsg);

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                return BadRequest("Error Occurred");
            }
        }

        // InsertNewPlanReviewHistoryRecord
        /// <summary>
        /// InsertAIONPlanReviewHistoryRecord
        /// </summary>
        /// <param name="inBoundMessage"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AIONPlanReviewHistoryResponse))]
        [ActionName("InsertAIONPlanReviewHistory")]
        [Route("api/AccelaExternal/V1/InsertAIONPlanReviewHistory")]
        public IHttpActionResult InsertAIONPlanReviewHistory(PlanReviewHistory inComingRecord)
        {
            try
            {
                AccelaCRUDAdapter thisadapter = new AccelaCRUDAdapter();

                var startUpDetails = "InsertAIONPlanReviewHistory starting: " + JsonConvert.SerializeObject(inComingRecord);

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), startUpDetails,
                     ex: null));

                var result = thisadapter.InsertPlanReviewHistoryRecord(inComingRecord);

                AIONPlanReviewHistoryResponse aIONPlanReviewHistoryResponse = (AIONPlanReviewHistoryResponse)result;

                string errormessage = aIONPlanReviewHistoryResponse.errors;

                //11-03-2023 jcl if there is an error message, return 400
                if (!string.IsNullOrWhiteSpace(errormessage))
                {
                    _emailAdapter.SendErrorEmail(errormessage);

                    Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errormessage));

                    return BadRequest("Error Occurred: " + errormessage);
                }


                return Ok(result);

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                _emailAdapter.SendErrorEmail(errmsg);

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message,
                    ex: ex));

                return BadRequest("Error Occurred");
            }
        }

        [HttpPost]
        [ResponseType(typeof(AIONAEDataResponse))]
        [ActionName("InsertAIONPlanReviewAEData")]
        [Route("api/AccelaExternal/V1/InsertAIONPlanReviewAEData")]
        public IHttpActionResult InsertAIONPlanReviewAEData(AccelaAIONAEData receivedRecord)
        {
            try
            {
                AccelaCRUDAdapter thisadapter = new AccelaCRUDAdapter();

                var startUpDetails = "InsertAIONPlanReviewHistory starting: " + JsonConvert.SerializeObject(receivedRecord);

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Information, MethodBase.GetCurrentMethod(), startUpDetails,
                      ex: null));

                var result = thisadapter.InsertAIONPlanReviewAEData(receivedRecord);

                AIONAEDataResponse aIONAEDataResponse = (AIONAEDataResponse)result;

                string errormessage = aIONAEDataResponse.errors;

                //11-03-2023 jcl if there is an error message, return 400
                if (!string.IsNullOrWhiteSpace(errormessage))
                {
                    _emailAdapter.SendErrorEmail(errormessage);

                    Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errormessage));

                    return BadRequest("Error Occurred: " + errormessage);
                }


                return Ok(result);
            }
            catch (Exception Ex)
            {
                string errmsg = Ex.Message;
                _emailAdapter.SendErrorEmail(errmsg);


                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), Ex.Message,
                     ex: Ex));

                return BadRequest("Error Occurred");

            }
        }
    }
}