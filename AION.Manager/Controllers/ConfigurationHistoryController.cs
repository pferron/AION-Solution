using AION.Base;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models.ConfigurationHistory;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class ConfigurationHistoryController : BaseApiController
    {
        /// <summary>
        /// Admin Configuration History
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<TableAuditLog>))]
        [Route("api/ConfigurationHistory/GetTableAuditLogListWDetails")]
        public IHttpActionResult GetTableAuditLogListWDetails(ConfigurationHistory configurationHistory)
        {
            ConfigurationHistoryAdapter thisengine = new ConfigurationHistoryAdapter();

            // UserIdentity  return
            var result = thisengine.GetTableAuditLogListWDetails(configurationHistory);

            return Ok(result);
        }

    }
}