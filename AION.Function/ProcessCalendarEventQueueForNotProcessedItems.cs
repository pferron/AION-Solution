using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Configuration;
using System.Reflection;

namespace AION.Function
{
    public static class ProcessCalendarEventQueueForNotProcessedItems
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;
        static string environment = ConfigurationManager.AppSettings["Environment"].ToUpper();
        
        [FunctionName("ProcessCalendarEventQueueForNotProcessedItems")]
        public static void Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, TraceWriter log) // every 30 seconds
        {
            applicationMessageGuid = Guid.NewGuid().ToString();

            logmessage = $"Timer trigger function ProcessCalendarEventQueueForNotProcessedItems executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);

            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                bool inProcess = false;
                if (APIHelper.ProcessCalendarEventQueueRecords(inProcess, environment) == false)
                {
                    logmessage = "Error occurred during ProcessCalendarEventQueueForNotProcessedItems - APIHelper.ProcessCalendarEventQueueRecords(false) = false";
                    throw new Exception(logmessage);

                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during ProcessCalendarEventQueueForNotProcessedItems. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ", Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;
            }
        }
    }
}
