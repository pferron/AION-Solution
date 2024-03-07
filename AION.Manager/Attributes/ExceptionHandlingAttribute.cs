using Meck.Logging;
using System.Reflection;
using System.Web.Http.Filters;

namespace AION.Manager.Attributes
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Logger logger = new Logger();

            var exceptionLogging = logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex: context.Exception);
        }
    }
}