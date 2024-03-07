using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Configuration;

namespace AION.Function
{
    public class GetNewAutoEstimationFactors
    {
        // the first day of the month at 12 am
        [FunctionName("GetNewAutoEstimationFactors")]
        public static void Run([TimerTrigger("0 0 0 1 * *")] TimerInfo myTimer, TraceWriter log, ExecutionContext context) 
        {

            string s = ConfigurationManager.AppSettings["ida:ManagerAADInstance"];
            log.Info($"Timer trigger function GetNewAutoEstimationFactors executed at: {DateTime.Now}");
            try
            {
                if (APIHelper.GetNewAutoEstimationFactors() == false)
                    log.Error("Error occured during GetNewAutoEstimationFactors. Check the logs for more details");
            }
            catch (Exception ex)
            {
                log.Error("Error occured during GetNewAutoEstimationFactors. Check the logs for more details. Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
            }
        }
    }

}

