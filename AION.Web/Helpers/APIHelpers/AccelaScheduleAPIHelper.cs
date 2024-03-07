using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Web.Helpers
{
    [Authorize]
    public class AccelaScheduleAPIHelper : BaseController
    {

        internal static bool AccelaSchedulePrelim(int id)
        {
            bool items = false;
            string responseuri = string.Format("api/Scheduling/AccelaSchedulePrelim?id={0}", id);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static bool AccelaScheduleFMA(int id)
        {
            bool items = false;
            string responseuri = string.Format("api/Scheduling/AccelaScheduleFMA?id={0}", id);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static bool AccelaSchedulePlanReview(PlanReview pr)
        {
            bool items = false;
            PlanReview obj = pr;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/AccelaSchedulePlanReview", new StringContent(
            JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        internal static bool AccelaChangeAssignedFacilitator(ProjectEstimation project)
        {
            bool items = false;
            ProjectEstimation obj = project;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/AccelaChangeAssignedFacilitator", new StringContent(
            JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return items;
        }
    }
}