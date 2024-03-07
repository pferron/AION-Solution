using AION.Base;
using AION.Manager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace AION.Web.Helpers
{
    [Authorize]
    public class CustomerAPIHelper : BaseController
    {
        internal static List<ProjectsList> GetProjectList(int userId)
        {
            List<ProjectsList> item = new List<ProjectsList>();
            string responseuri = string.Format("api/Customer/GetProjectList?loggedinUserId={0}", userId);

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
              item = JsonConvert.DeserializeObject<List<ProjectsList>>(jsonString.Result);

          });
            task.Wait();
            return item;
        }

        internal static List<DateTime> SearchSelfScheduleCapacity(SchedulePlanReviewCapacityParams model)
        {
            List<DateTime> items = new List<DateTime>();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/SearchSelfScheduleCapacity", new StringContent(
            JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"))
             .ContinueWith((taskwithresponse) =>
             {
                 var response = taskwithresponse.Result;
                 if (response.IsSuccessStatusCode == false)
                 {
                     var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                 }
                 var jsonString = response.Content.ReadAsStringAsync();
                 jsonString.Wait();
                 items = JsonConvert.DeserializeObject<List<DateTime>>(jsonString.Result);

             });
            task.Wait();
            return items;
        }
    }
}