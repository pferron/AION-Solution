using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Reflection;

namespace AION.Function
{
    public static class CancelScheduledExpressPlanReviewTrigger
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;
        //[Disable]
        // "0 */1 * * * *"   = every 1 minute
        // "0 00 23 * * 1-5" = every weekday 11 PM
        [FunctionName("CancelScheduledExpressPlanReviewTrigger")]

        public static void Run([TimerTrigger("0 00 23 * * 1-5")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) //Every Weekday 11PM
        {
            applicationMessageGuid = Guid.NewGuid().ToString();

            logmessage = $"Timer trigger function CancelScheduledExpressPlanReviewTrigger executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);

            try
            {
                if (APIHelper.CancelScheduledExpressPlanReview() == false)
                {
                    logmessage = "Error occurred during CancelScheduledExpressPlanReviewTrigger - APIHelper.CancelScheduledExpressPlanReview() = false";
                    throw new Exception(logmessage);

                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during CancelScheduledExpressPlanReviewTrigger. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ", Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;

            }
        }
    }
}
