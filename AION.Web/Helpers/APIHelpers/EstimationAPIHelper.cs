using AION.Base;
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

    public class EstimationAPIHelper : BaseController
    {
        public EstimationModel GetEstimationModel(ProjectParms projInfo)
        {
            EstimationModel item = new EstimationModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/ProjectEstimation/GetEstimationModel", new StringContent(
            JsonConvert.SerializeObject(projInfo), Encoding.UTF8, "application/json"))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<EstimationModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        public BulkEstimationModel GetBulkEstimationModel()
        {
            BulkEstimationModel item = new BulkEstimationModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/ProjectEstimation/GetBulkEstimationModel")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<BulkEstimationModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        internal static ProjectEstimation ExecuteProjectEstimation(ProjectParms projInfo, bool forceAutoCalc = false)
        {
            //_autoestimator = new ProjectAutoEstimationEngine(_api, _crud, _estimation);

            ProjectEstimation items = new ProjectEstimation();
            ProjectEstimationManagerModel obj = new ProjectEstimationManagerModel
            {
                ForceAutoCalc = forceAutoCalc,
                ProjInfo = projInfo
            };
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/ProjectEstimation/ExecuteProjectEstimation", new StringContent(
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
              items = JsonConvert.DeserializeObject<ProjectEstimation>(jsonString.Result);
          });
            task.Wait();
            return items;
        }
        internal static ProjectEstimation ExecutePreliminaryProjectEstimation(ProjectParms projInfo)
        {
            // PreliminaryProjectEstimationEngine _preliminaryestimator = new PreliminaryProjectEstimationEngine(_api, _crud, _estimation);

            ProjectEstimation items = new ProjectEstimation();
            ProjectEstimationManagerModel obj = new ProjectEstimationManagerModel
            {
                ProjInfo = projInfo
            };
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/ProjectEstimation/ExecutePreliminaryProjectEstimation", new StringContent(
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
              items = JsonConvert.DeserializeObject<ProjectEstimation>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        internal static bool SaveProjectEstimationDetails(ProjectEstimation model)
        {
            bool items = false;
            ProjectEstimation obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/EstimationCRUD/SaveProjectEstimationDetails", new StringContent(
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
        /// <summary>
        /// used to save estimation from EstimationMain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal static bool SaveEstimation(ProjectEstimation model)
        {
            bool items = false;
            ProjectEstimation obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;
            string s = JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            var task = client.PostAsync("api/EstimationCRUD/SaveEstimation", new StringContent(
            s, Encoding.UTF8, "application/json"))
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