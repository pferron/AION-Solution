using System;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Accela.WebApi.Controllers
{
    public class PingController : ApiController
    {
      
        /// <summary>
        /// for testing for com testing only
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(String))]
        [ActionName("PING")]
        [Route("api/ping/PingTest")]
        public IHttpActionResult PingTest()
        {
            string pingdatetime = "Ping Test: Server DateTime " + DateTime.Now.ToString() + " [Eastern]  ";

            return Ok(pingdatetime);
        }



    }
}