using AION.Base;
using AION.BL.Models;
using AION.Manager.Models;
using Meck.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace AION.Web.Helpers
{
    [Authorize]
    public class ProjectDetailAPIHelper : BaseController
    {

        #region Properties

        Logger m_Logger = new Logger();
        public Logger Logging
        {
            get { return m_Logger; }
            set { m_Logger = value; }
        }

        #endregion

        public ProjectDetailModel GetProjectDetailModel(ProjectParms parms)
        {
            ProjectDetailModel item = new ProjectDetailModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/EstimationCRUD/GetProjectDetailModel", new StringContent(
            JsonConvert.SerializeObject(parms), Encoding.UTF8, "application/json"))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<ProjectDetailModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        /// <summary>
        /// This endpoint gets the latest from accela,then saves any changes and returns the most current values to the UI
        /// Only use for UI display.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static ProjectEstimation GetProjectDetailsByAccelaIdforUI(string id)
        {

            ProjectEstimation item = new ProjectEstimation();
            string responseuri = string.Format("api/EstimationCRUD/GetProjectDetailsByProjectSrcSourceTxt?accelaModelProjectId={0}", id);

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
              item = JsonConvert.DeserializeObject<ProjectEstimation>(jsonString.Result);

          });
            task.Wait();
            return item;
        }

        internal static bool CancelMeetingById(int appointmentId, string notes, int wkrId)
        {
            bool success = false;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            CancelMeetingModel obj = new CancelMeetingModel
            {
                AppointmentId = appointmentId,
                Notes = notes,
                UserId = wkrId
            };

            var task = client.PostAsync("api/Scheduling/CancelMeetingById", new StringContent(
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
                   success = JsonConvert.DeserializeObject<bool>(jsonString.Result);
               });
            task.Wait();
            return success;
        }
    }
}