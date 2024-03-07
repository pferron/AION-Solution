using Meck.Logging;
using System.Web.Http;

namespace AION.Base
{
    public class BaseApiController : ApiController
    {
        public Logger Logger { get; set; } = new Logger();

    }
}
