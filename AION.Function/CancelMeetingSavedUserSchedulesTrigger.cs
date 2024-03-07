using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Configuration;
using System.Reflection;

namespace AION.Function
{
    public static class CancelMeetingSavedUserSchedulesTrigger
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;

        //[Disable]
        // "0 */1 * * * *"   = every 1 minute
        // "0 00 23 * * 1-5" = every weekday 11 PM
        [FunctionName("CancelMeetingSavedUserSchedules")]
        public static void Run([TimerTrigger("0 0 */2 * * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) //Every two hours
        {
            applicationMessageGuid = Guid.NewGuid().ToString();
            string s = ConfigurationManager.AppSettings["ida:ManagerAADInstance"];
            logmessage = $"Timer trigger function CancelMeetingSavedUserSchedules executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);

            try
            {
                if (APIHelper.CancelMeetingSavedUserSchedules() == false)
                {
                    logmessage = "Error occurred during CancelMeetingSavedUserSchedules - APIHelper.CancelMeetingSavedUserSchedules() = false.";
                    throw new Exception(logmessage);

                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during CancelMeetingSavedUserSchedules. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ", Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;

            }
        }
    }
}
