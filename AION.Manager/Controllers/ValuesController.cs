using AION.Base;
using System.Collections.Generic;
using System.Web.Http;


namespace AION.Manager.Controllers
{

    /*
     * DO NOT DELETE THIS CONTROLLER !!! USED FOR TESTING AUTHNTICATION AND BASIC STUFF FROM ELLIOT
     */
    [Authorize]
    public class ValuesController : BaseApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
