using AION.Base;
using AION.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace AION.Web.Helpers
{
    [Authorize]
    public class SearchAPIHelper : BaseController
    {
        internal static List<ProjectSearchResult> SearchProjects(DateTime? startDate, DateTime? endDate, string projectNumber, string projectName, string projectAddress,
           string customerName, string planReviewer, int? projectStatus = null, int? estimatorId = null, int? facilitatorId = null, int? meetingType = null)
        {
            List<ProjectSearchResult> items = new List<ProjectSearchResult>();
            string responseuri =
                string.Format("api/Search/SearchProjects?startDate={0}&endDate={1}&projectNumber={2}&projectName={3}&projectAddress={4}" +
                "&customerName={5}&planReviewer={6}&projectStatus={7}&estimatorId={8}&facilitatorId={9}&meetingType={10}",
                startDate, endDate, projectNumber, projectName, projectAddress, customerName, planReviewer, projectStatus,
                estimatorId, facilitatorId, meetingType);

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
              items = JsonConvert.DeserializeObject<List<ProjectSearchResult>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
    }
}