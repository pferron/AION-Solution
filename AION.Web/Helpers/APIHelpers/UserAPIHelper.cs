using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AION.Web.Helpers
{
    public class UserAPIHelper : BaseController
    {
        internal static List<UserIdentity> GetUsersByFilterModeUserManagement(string filterString = "", string filterMode = "")
        {
            FiltersManagerModel obj = new FiltersManagerModel
            {
                FilterMode = filterMode,
                FilterString = filterString
            };
            List<UserIdentity> items = new List<UserIdentity>();
            string responseuri = string.Format("api/Admin/GetUsersByFilterModeUserManagement");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync(responseuri, new StringContent(
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
              items = JsonConvert.DeserializeObject<List<UserIdentity>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// gets list of schedulable reviewers from aion db
        /// </summary>
        /// <returns></returns>
        internal static List<Reviewer> GetAllReviewers(bool isExpressSched = false)
        {
            List<Reviewer> items = new List<Reviewer>();
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;


            var task = client.GetAsync(string.Format("api/User/GetAllReviewers?isExpressSched={0}", isExpressSched))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<Reviewer>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }
    }
}