using System;
using System.Reflection;
using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace AION.Function
{
    public static class ProcessFIFOOptimizationTrigger
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;

        [FunctionName("ProcessFIFOOptimizationTrigger")]
        public static void Run([TimerTrigger("0 00 23 * * 1-5")] TimerInfo myTimer, TraceWriter log)
        {
            applicationMessageGuid = Guid.NewGuid().ToString();

            logmessage = $"Timer trigger function ProcessFIFOOptimizationTrigger executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);

            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                if (APIHelper.OptimizeFIFOProjects() == false)
                {
                    logmessage = "Error occurred during ProcessFIFOOptimizationTrigger - APIHelper.OptimizeFIFOProjects";
                    throw new Exception(logmessage);

                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during OptimizeFIFOAssignments. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ", Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;
            }
        }
    }
}
