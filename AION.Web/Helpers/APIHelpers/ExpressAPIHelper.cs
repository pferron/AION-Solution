using AION.Base;
using AION.Manager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Web.Helpers.APIHelpers
{
    [Authorize(Roles = "User")]

    public class ExpressAPIHelper : BaseController
    {
        /// <summary>
        /// get list of days configured to reserve express
        /// </summary>
        /// <returns></returns>
        public ExpressModel GetExpressModel()
        {
            ExpressModel item = new ExpressModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Express/GetExpressModel")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<ExpressModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        public static List<ConfigureReserveExpressDays> GetConfigureReserveExpressList()
        {

            List<ConfigureReserveExpressDays> items = new List<ConfigureReserveExpressDays>();
            string responseuri = string.Format("api/Scheduling/GetConfigureReserveExpressList");
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
              items = JsonConvert.DeserializeObject<List<ConfigureReserveExpressDays>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// get list of saved reviewer rotation by business unit for Express Configuration
        /// </summary>
        /// <returns></returns>
        public static List<ReserveExpressPlanReviewer> GetReserveExpressPlanReviewersListAll()
        {
            List<ReserveExpressPlanReviewer> items = new List<ReserveExpressPlanReviewer>();
            string responseuri = string.Format("api/Scheduling/GetReserveExpressPlanReviewerListAll");
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
              items = JsonConvert.DeserializeObject<List<ReserveExpressPlanReviewer>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        public static bool SaveConfigureExpress(List<ConfigureReserveExpressDays> days)
        {
            bool items = false;
            List<ConfigureReserveExpressDays> obj = days;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/SaveConfigureExpress", new StringContent(
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

        public static bool DeleteExpressPlanReviewerRotation()
        {
            bool items = false;

            string responseuri = string.Format("api/Scheduling/DeleteExpressPlanReviewerRotation");
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
                  items = JsonConvert.DeserializeObject<bool>(jsonString.Result);

              });
            task.Wait();
            return items;
        }

        public static bool SaveExpressReviewerRotation(List<ReserveExpressPlanReviewer> scheduledReviewers)
        {
            bool items = false;
            List<ReserveExpressPlanReviewer> obj = scheduledReviewers;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/SaveExpressPlanReviewerRotation", new StringContent(
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

        public static bool UpdateExpressReviewerRotation()
        {
            bool items = false;

            string responseuri = string.Format("api/Express/UpdateReserveExpressReviewerRotation");
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
                  items = JsonConvert.DeserializeObject<bool>(jsonString.Result);

              });
            task.Wait();
            return items;
        }

        /// <summary>
        ///  SearchExpressReservation by dates
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public static List<ExpressSearchResult> SearchExpressReservation(DateTime fromdate, DateTime todate)
        {
            List<ExpressSearchResult> items = new List<ExpressSearchResult>();

            string responseuri = string.Format("api/Express/SearchbyDates?fromdate={0}&todate={1}", fromdate, todate);

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
              items = JsonConvert.DeserializeObject<List<ExpressSearchResult>>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        public static List<ExpressSearchResult> GetExpressScheduledByDates(DateTime fromdate, DateTime todate)
        {
            List<ExpressSearchResult> items = new List<ExpressSearchResult>();

            string responseuri = string.Format("api/Express/GetExpressScheduledByDates?fromdate={0}&todate={1}", fromdate, todate);

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
              items = JsonConvert.DeserializeObject<List<ExpressSearchResult>>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// update Attendees to Express
        /// </summary>
        /// <returns></returns>
        public static bool UpdateAttendeesToExpress(ApptAttendeesManagerModel model)
        {
            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Express/UpdateAttendees", new StringContent(
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
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// Cancel Express reservations
        /// </summary>
        /// <returns></returns>
        public static bool CancelReservations(List<ApptAttendeesManagerModel> model)
        {
            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Express/CancelReservations", new StringContent(
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
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        public static bool TestFunction()
        {

            string responseuri = string.Format("api/Express/ExpressReservation");
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


          });
            task.Wait();
            return true;

        }

    }
}