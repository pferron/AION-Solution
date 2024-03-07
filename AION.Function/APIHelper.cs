using AION.Base;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Function
{
    [Authorize]
    public class APIHelper : BaseController
    {
        public static bool ProcessExpressReservations()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Express/ExpressReservation")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        public static bool CancelPrelimMeeting(TraceWriter log)
        {
            bool ret = false;

            //var ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            //var hostname = System.Web.HttpContext.Current.Request.UserHostName;
            log.Info($"CancelPrelimMeeting-AION.function - APIhelper executed at: {DateTime.Now}");
            //+ "IPAddress" + ipAddress + "HostName" + hostname);
            try
            {

                var httpClient = Task.Run(() => GetManagerHttpClient());

                HttpClient client = httpClient.Result;

                var task = client.GetAsync("api/Function/CancelPrelimMeeting")
               .ContinueWith((taskwithresponse) =>
               {
                   var response = taskwithresponse.Result;
                   if (response.IsSuccessStatusCode == false)
                   {
                       var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                   }
                   var jsonString = response.Content.ReadAsStringAsync();
                   jsonString.Wait();
                   ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
               });
                task.Wait();

            }

            catch (Exception ex)
            {

                log.Error("Error occurred during cancel prelim meeting - API helper function. Check the logs for more details. Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
                //+ "IPAddress" + ipAddress + "HostName" + hostname);
            }
            return ret;
        }

        public static bool SyncAccelaAION(TraceWriter log)
        {
            bool ret = false;
            try
            {

                var httpClient = Task.Run(() => GetManagerHttpClient());

                HttpClient client = httpClient.Result;

                var task = client.GetAsync("api/Function/SyncAccelaAION")
               .ContinueWith((taskwithresponse) =>
               {
                   var response = taskwithresponse.Result;
                   if (response.IsSuccessStatusCode == false)
                   {
                       var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                   }
                   var jsonString = response.Content.ReadAsStringAsync();
                   jsonString.Wait();
                   ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
               });
                task.Wait();
                return ret;

            }

            catch (Exception ex)
            {

                log.Error("Error occured during SyncAccelaAION - API helper function. Check the logs for more details. Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
                //    "IPAddress" + ipAddress +
                //"HostName" + hostname);
            }
            return ret;
        }

        internal static bool AddEligibleUsersToExistingNPAs()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/AddEligibleUsersToExistingNPAs")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;

        }

        public static bool CancelScheduleExpressHolds()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/CancelScheduleExpressHolds")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        public static bool CancelScheduledPlanReview()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/CancelSchedulePlanReview")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        public static bool CancelScheduledExpressPlanReview()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/CancelScheduledExpressPlanReview")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        public static bool CancelReserveExpressReservation()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/CancelReserveExpressReservation")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        public static bool CancelMeetingSavedUserSchedules()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/CancelMeetingSavedUserSchedules")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        public static bool UpdatePlanReviewerHoursByAccela(TraceWriter log)
        {
            bool ret = false;

            //var ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            //var hostname = System.Web.HttpContext.Current.Request.UserHostName;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            log.Info($"UpdatePlanReviewerHoursByAccela-AION.function - APIhelper executed at: {DateTime.Now}");
            //+ "IPAddress" + ipAddress + "HostName" + hostname);
            try
            {
                HttpClient client = httpClient.Result;

                var task = client.GetAsync("api/Function/UpdatePlanReviewerHoursByAccela")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    if (response.IsSuccessStatusCode == false)
                    {
                        var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                    }
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
                });

                task.Wait();
                return ret;
            }

            catch (Exception ex)
            {

                log.Error("Error occurred during updating unused plan reviewer hours - API helper function. Check the logs for more details. Message: " + ex.Message + ", StackTrace: " + ex.StackTrace);
                //+ "IPAddress" + ipAddress + "HostName" + hostname);
            }
            return ret;
        }

        public static bool CancelFacilitatorMeetingAppointment()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Function/CancelFacilitatorMeetingAppointment")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        #region Optimize FIFO
        public static bool OptimizeFIFOProjects()
        {
            bool ret = false;

            List<int> projectIds = GetFIFOProjectIdsToBeOptimized();

            foreach (int projectId in projectIds)
            {
                return OptimizeFIFOProject(projectId);
            }

            ret = true;
            return ret;
        }

        public static List<int> GetFIFOProjectIdsToBeOptimized()
        {
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            List<int> projectIds = new List<int>();

            var taskGetProjects = client.GetAsync("api/Function/GetFIFOProjectIdsToBeOptimized")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               projectIds = JsonConvert.DeserializeObject<List<int>>(jsonString.Result);
           });
            taskGetProjects.Wait();

            return projectIds;
        }

        public static bool OptimizeFIFOProject(int projectId)
        {
            bool ret = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Function/OptimizeFIFOProject?projectId={0}", projectId))
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        #endregion

        #region MS Graph Appointment Scheduling

        public static bool ProcessCalendarEventQueueRecords(bool inProcess, string environment)
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Function/ProcessCalendarEventQueueRecords?inProcess={0}&environment={1}", inProcess, environment))
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        #endregion

        #region Scheduling Lead Time Report
        internal static bool GenerateSchedulingLeadTimeReportData()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Reporting/GenerateSchedulingLeadTimeData?wkrId=1")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;

        }

        #endregion

        #region AutoEstimation Factors Monthly
        public static bool GetNewAutoEstimationFactors()
        {
            bool ret = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/EstimationAccela/GetNewAutoEstimationFactors")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               ret = JsonConvert.DeserializeObject<bool>(jsonString.Result);
           });
            task.Wait();
            return ret;
        }

        #endregion
    }
}
