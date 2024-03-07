using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Reflection;

namespace AION.Function
{
    public static class ProcessExpressReservationTrigger
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;
        //[Disable]
        [FunctionName("ProcessExpressReservationTrigger")]
        public static void Run([TimerTrigger("0 0 0 1 * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) //First of every month
        {
            applicationMessageGuid = Guid.NewGuid().ToString();
            logmessage = $"Timer trigger function ProcessExpressReservationTrigger executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);
            try
            {
                if (APIHelper.ProcessExpressReservations() == false)
                {
                    logmessage = "Error occurred during ProcessExpressReservationTrigger - APIHelper.ProcessExpressReservations() = false.";
                    throw new Exception(logmessage);
                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during ProcessExpressReservationTrigger. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ", Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;

            }
        }
    }
}
