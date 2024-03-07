using Meck.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Reflection;

namespace AION.Function
{
    public static class GenerateSchedulingLeadTimeReportData
    {
        static Logger m_Logger = new Logger();
        static string logmessage = string.Empty;
        static string applicationMessageGuid = string.Empty;


        /// <summary>
        /// Run Scheduling lead time report Generate Data once a week
        /// so the auto generated report will have up to date data
        /// Run Sunday at 3am
        /// </summary>
        /// <param name="myTimer">"0 0 3 * * SUN"</param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        [FunctionName("GenerateSchedulingLeadTimeReportData")]

        public static void Run([TimerTrigger("0 0 3 * * SUN")] TimerInfo myTimer, TraceWriter log, ExecutionContext context)
        {
            applicationMessageGuid = Guid.NewGuid().ToString();
            logmessage = $"Timer trigger function GenerateSchedulingLeadTimeReportData executed at: {DateTime.Now}";
            log.Info(logmessage);
            var logging = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid);
            try
            {
                if (APIHelper.GenerateSchedulingLeadTimeReportData() == false)
                {
                    logmessage = "Error occurred during GenerateSchedulingLeadTimeReportData - APIHelper.AddEligibleUsersToExistingNPAs(log) = false.";
                    throw new Exception(logmessage);
                }
            }
            catch (Exception ex)
            {
                logmessage = "Error occurred during GenerateSchedulingLeadTimeReportData. Check the logs for more details. ApplicationMessageGuid:" + applicationMessageGuid + ",  Message: " + ex.Message + ", StackTrace: " + ex.StackTrace;
                log.Error(logmessage);
                var logging3 = m_Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), logmessage, applicationMessageGuid: applicationMessageGuid, ex: ex);
                throw ex;
            }
        }
    }
}
