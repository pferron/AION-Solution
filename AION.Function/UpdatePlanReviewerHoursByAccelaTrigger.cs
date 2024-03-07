using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Reflection;

namespace AION.Function
{
    public static class UpdatePlanReviewerHoursByAccelaTrigger
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;
        //[Disable]
        // "0 */1 * * * *"   = every 1 minute
        //"0 */15 * * * *" every 15 mins
        // Need to determine how frequently this needs to run
        [FunctionName("UpdatePlanReviewerHoursByAccelaTrigger")]

        public static void Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) //Every 15 mins
        {
            applicationMessageGuid = Guid.NewGuid().ToString();
            logmessage = $"Timer trigger function UpdatePlanReviewerHoursByAccelaTrigger executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);
            try
            {
                if (APIHelper.UpdatePlanReviewerHoursByAccela(log) == false)
                {
                    logmessage = "Error occurred during UpdatePlanReviewerHoursByAccelaTrigger - APIHelper.UpdatePlanReviewerHoursByAccela(log) = false.";
                    throw new Exception(logmessage);
                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during UpdatePlanReviewerHoursByAccelaTrigger. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ",  Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;
            }
        }
    }
}
