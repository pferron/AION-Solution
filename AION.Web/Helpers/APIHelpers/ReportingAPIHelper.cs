using AION.Base;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace AION.Web.Helpers
{
    [Authorize]
    public class ReportingAPIHelper : BaseController
    {
        internal static bool GenerateSchedulingLeadTimeData(int userId)
        {
            bool ret = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Reporting/GenerateSchedulingLeadTimeData?wkrId={0}", userId))
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
    }
}