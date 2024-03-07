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
using System.Web.Mvc;

namespace AION.Web.Helpers
{
    [Authorize]
    public class NoteAPIHelper : BaseController
    {

        internal static int InsertCustomerResponse(Note note)
        {
            int items = 0;
            Note obj = note;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Note/InsertCustomerResponse", new StringContent(
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
              items = JsonConvert.DeserializeObject<int>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        internal static List<Note> GetProjectNotes(int? projectId, NoteTypeEnum? noteTypeEnum, string projectNumber = "")
        {
            InternalNoteManagerModel obj = new InternalNoteManagerModel
            {
                ProjectId = projectId,
                NoteTypeEnum = noteTypeEnum,
                ProjectNumber = projectNumber
            };
            List<Note> items = new List<Note>();
            string responseuri = string.Format("api/Note/GetProjectNotes");

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
              items = JsonConvert.DeserializeObject<List<Note>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        internal static List<StandardNote> GetStandardNotes(NoteTypeEnum noteTypeEnum, PropertyTypeEnums propertyType)
        {
            List<StandardNote> items = new List<StandardNote>();
            string responseuri = string.Format("api/Note/GetStandardNotes?noteTypeEnum={0}&propertyTypeEnum={1}", (int)noteTypeEnum, (int)propertyType);

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
              items = JsonConvert.DeserializeObject<List<StandardNote>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static List<StandardNoteGroupEnums> GetStandardNoteGroupEnums()
        {
            List<StandardNoteGroupEnums> items = new List<StandardNoteGroupEnums>();
            string responseuri = string.Format("api/Note/GetStandardNoteGroupEnums");

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
              items = JsonConvert.DeserializeObject<List<StandardNoteGroupEnums>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static List<NoteType> GetNoteTypeBaseList()
        {
            List<NoteType> items = new List<NoteType>();
            string responseuri = string.Format("api/Note/GetNoteTypeBaseList");

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
              items = JsonConvert.DeserializeObject<List<NoteType>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

    }
}