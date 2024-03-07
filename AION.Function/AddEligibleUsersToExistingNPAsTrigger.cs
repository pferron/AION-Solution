using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Reflection;

namespace AION.Function
{
    public static class AddEligibleUsersToExistingNPAsTrigger
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;
        //[Disable]
        // "0 */1 * * * *"   = every 1 minute
        //"0 0 */8 * * *" every 8 hours
        // Need to determine how frequently this needs to run
        [FunctionName("AddEligibleUsersToExistingNPAsTrigger")]

        public static void Run([TimerTrigger("0 0 */8 * * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) //Every 8 hours
        {
            applicationMessageGuid = Guid.NewGuid().ToString();
            logmessage = $"Timer trigger function AddEligibleUsersToExistingNPAsTrigger executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);
            try
            {
                if (APIHelper.AddEligibleUsersToExistingNPAs() == false)
                {
                    logmessage = "Error occurred during AddEligibleUsersToExistingNPAsTrigger - APIHelper.AddEligibleUsersToExistingNPAs(log) = false.";
                    throw new Exception(logmessage);
                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during AddEligibleUsersToExistingNPAsTrigger. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ",  Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;
            }
        }
    }
}
