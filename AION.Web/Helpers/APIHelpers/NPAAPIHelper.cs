using AION.Base;
using AION.Manager.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AION.Web.Helpers
{
    public class NPAAPIHelper : BaseController
    {
        public NPAModel GetNPAModel()
        {
            NPAModel item = new NPAModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/NPA/GetNPAModel")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<NPAModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        /// <summary>
        /// update Attendees to NPA
        /// </summary>
        /// <returns></returns>
        internal static bool UpdateAttendeesToNPA(ApptAttendeesManagerModel model)
        {

            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/UpdateAttendees", new StringContent(
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
    }
}