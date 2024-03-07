using AION.Base;
using AION.BL.Models;
using AION.Manager.Models.ConfigurationHistory;
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
    public class ConfigurationHistoryAPIHelper : BaseController
    {

        internal static List<TableAuditLog> GetAuditLogListWDetails(ConfigurationHistory configurationHistory)
        {

            List<TableAuditLog> items = new List<TableAuditLog>();
            string responseuri = string.Format("api/ConfigurationHistory/GetTableAuditLogListWDetails");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(configurationHistory), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<TableAuditLog>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

    }
}