using AION.Base;
using AION.Manager.Models.Dashboard;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Web.Helpers.APIHelpers
{
    [Authorize]
    public class DashboardAPIHelper : BaseController
    {
        /// <summary>
        /// Returns list of Projects that are Unscheduled for the
        /// Scheduling Dashboard
        /// </summary>
        /// <returns></returns>
        internal static DashboardListBase GetSchedulingDashboardList(int userid)
        {
            DashboardListBase items = new DashboardListBase();
            string responseuri = string.Format("api/Scheduling/GetSchedulingDashboardList?userid={0}", userid);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.GetAsync(responseuri)
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<DashboardListBase>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Get the estimation dashboard project list
        /// </summary>
        /// <param name="fromdate">Format MM/dd/yyyy</param>
        /// <param name="todate">Format MM/dd/yyyy</param>
        /// <param name="numofrows"></param>
        /// <returns></returns>
        internal static DashboardListBase GetEstimationDashboardProjectList(int userid, string fromdate = null, string todate = null, int numofrows = 50)
        {
            DashboardListBase items = new DashboardListBase();
            string responseuri = string.Format("api/Dashboard/GetEstimationDashboardProjectList?numofrows={0}&fromdate={1}&todate={2}&userid={3}", numofrows, fromdate, todate, userid);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.GetAsync(responseuri)
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<DashboardListBase>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static DashboardListBase GetInternalMeetings(int wrkId)
        {
            DashboardListBase item = new DashboardListBase();
            string responseuri = string.Format("api/Scheduling/GetInternalMeetings?wrkId={0}", wrkId);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.GetAsync(responseuri)
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              item = JsonConvert.DeserializeObject<DashboardListBase>(jsonString.Result);

          });
            task.Wait();
            return item;
        }

    }
}