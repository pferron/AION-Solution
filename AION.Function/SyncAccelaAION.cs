using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Reflection;

namespace AION.Function
{
    public static class SyncAccelaAION
    {
        // "0 */1 * * * *"   = every 1 minute
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;
        // Need to determine how frequently this needs to run
        [FunctionName("SyncAccelaAION")]

        public static void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) //Every minute
        {
            applicationMessageGuid = Guid.NewGuid().ToString();
            logmessage = $"Timer trigger function SyncAccelaAION executed at: {DateTime.Now}";
            log.Info(logmessage);
            try
            {
                if (APIHelper.SyncAccelaAION(log) == false)
                {
                    logmessage = "Error occurred during SyncAccelaAION - APIHelper.SyncAccelaAION(log) = false.";
                    throw new Exception(logmessage);
                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during SyncAccelaAION. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ",  Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;
            }
        }
    }
}
