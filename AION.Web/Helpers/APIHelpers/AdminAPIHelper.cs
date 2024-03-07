using AION.Base;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Manager.Models.User;
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
    public class AdminAPIHelper : BaseController
    {
        public AdminModel GetAdminModel()
        {
            AdminModel item = new AdminModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/Admin/GetAdminModel")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<AdminModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }


        internal static bool SaveUserJurisdiction(int userId, List<string> jurisdictionList, string wrkId)
        {
            bool success = false;

            UserJurisdictionSaveModel obj = new UserJurisdictionSaveModel
            {
                UserId = userId,
                JurisdictionList = jurisdictionList == null ? new List<string>() : jurisdictionList,
                WrkId = wrkId
            };
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/SaveUserJurisdiction", new StringContent(
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

        /// <summary>
        /// AdminControlelr
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of enum mapping val nbr</returns>
        internal static List<string> GetJurisdictionListByUser(int userId)
        {
            List<string> items = new List<string>();
            string responseuri = string.Format("api/User/GetJurisdictionListByUser?id={0}", userId);

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
              items = JsonConvert.DeserializeObject<List<string>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Admin Message Configuration: get the template by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static MessageTemplate GetMessageTemplateById(int id)
        {
            MessageTemplate items = new MessageTemplate();
            string responseuri = string.Format("api/Admin/GetMessageTemplateById?id={0}", id);

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
              items = JsonConvert.DeserializeObject<MessageTemplate>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Admin Message Configuration: get the template types list
        /// </summary>
        /// <returns>list of enum mapping val nbr</returns>
        internal static List<MessageTemplateType> GetMessageTemplateTypes()
        {
            List<MessageTemplateType> items = new List<MessageTemplateType>();
            string responseuri = "api/Admin/GetMessageTemplateTypes";

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
              items = JsonConvert.DeserializeObject<List<MessageTemplateType>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Admin Message Configuration: get the templates list by type
        /// </summary>
        /// <param name="templateTypeId"></param>
        /// <returns></returns>
        internal static List<MessageTemplate> GetMessageTemplatesByTypeId(int templateTypeId)
        {
            List<MessageTemplate> items = new List<MessageTemplate>();
            string responseuri = string.Format("api/Admin/GetMessageTemplatesByTypeId?id={0}", templateTypeId);

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
              items = JsonConvert.DeserializeObject<List<MessageTemplate>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Admin Message Configuration: get the data elements list for the templates
        /// </summary>
        /// <returns></returns>
        internal static List<MessageTemplateDataElement> GetMessageTemplateDataElements()
        {
            List<MessageTemplateDataElement> items = new List<MessageTemplateDataElement>();
            string responseuri = "api/Admin/GetMessageTemplateDataElements";

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
              items = JsonConvert.DeserializeObject<List<MessageTemplateDataElement>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static bool InsertMessageTemplate(MessageTemplate messageTemplate)
        {
            bool success = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/InsertMessageTemplate", new StringContent(
            JsonConvert.SerializeObject(messageTemplate), Encoding.UTF8, "application/json"))
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
        internal static bool UpdateMessageTemplate(MessageTemplate messageTemplate)
        {
            bool success = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/UpdateMessageTemplate", new StringContent(
            JsonConvert.SerializeObject(messageTemplate), Encoding.UTF8, "application/json"))
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

        internal static bool UpdateAutoAssignFacilitator(string projectTypeRefIdCsvList, bool autoAssignFacilitator, string wkrId)
        {
            bool success = false;
            string responseuri = string.Format("api/Admin/UpdateAutoAssignFacilitator?projectTypeRefIdCsvList={0}&autoAssignFacilitator={1}&wkrId={2}", projectTypeRefIdCsvList, autoAssignFacilitator, wkrId);

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
              success = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return success;
        }

        internal static List<ProjectType> GetProjectTypeList()
        {
            List<ProjectType> items = new List<ProjectType>();
            string responseuri = "api/Admin/GetProjectTypeList";

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
              items = JsonConvert.DeserializeObject<List<ProjectType>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
    }
}