using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.BusinessEntities;
using Meck.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Web.Helpers
{
    [Authorize]
    public class APIHelper : BaseController
    {

        #region Properties

        Logger m_Logger = new Logger();
        public Logger Logging
        {
            get { return m_Logger; }
            set { m_Logger = value; }
        }

        #endregion

        public List<PlanReview> GetPlanReviewsByProjectId(string accelaprojectid)
        {
            List<PlanReview> items = new List<PlanReview>();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Scheduling/GetPlanReviewsByProjectId?projectId={0}", accelaprojectid))
                .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  items = JsonConvert.DeserializeObject<List<PlanReview>>(jsonString.Result);

              });
            task.Wait();
            return items;
        }

        public List<PlanReviewScheduleDetail> GetPlanReviewScheduleDetailsByPlanReviewSchedule(int planReviewScheduleId)
        {
            List<PlanReviewScheduleDetail> items = new List<PlanReviewScheduleDetail>();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Scheduling/GetPlanReviewScheduleDetailsByPlanReviewSchedule?planReviewScheduleId={0}", planReviewScheduleId))
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    if (response.IsSuccessStatusCode == false)
                    {
                        var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                    }
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    items = JsonConvert.DeserializeObject<List<PlanReviewScheduleDetail>>(jsonString.Result);

                });
            task.Wait();
            return items;
        }

        public List<ProjectCycleReview> GetProjectCycleReviews(int projectId)
        {
            List<ProjectCycleReview> items = new List<ProjectCycleReview>();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Scheduling/GetProjectCycleReviews?projectId={0}", projectId))
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    if (response.IsSuccessStatusCode == false)
                    {
                        var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                    }
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    items = JsonConvert.DeserializeObject<List<ProjectCycleReview>>(jsonString.Result);

                });
            task.Wait();
            return items;
        }

        public ProjectCycleSummary GetProjectCycleSummary(int projectId)
        {
            ProjectCycleSummary item = new ProjectCycleSummary();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Scheduling/GetProjectCycleSummary?projectId={0}", projectId))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<ProjectCycleSummary>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        public ProjectCycle GetProjectCycleById(int projectCycleId)
        {
            ProjectCycle item = new ProjectCycle();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Scheduling/GetProjectCycleById?projectCycleId={0}", projectCycleId))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  item = JsonConvert.DeserializeObject<ProjectCycle>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        public List<PlanReview> GetPlanReviewsByProjectCycleId(int projectCycleId)
        {
            List<PlanReview> items = new List<PlanReview>();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Scheduling/GetPlanReviewsByProjectCycleId?projectCycleId={0}", projectCycleId))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  items = JsonConvert.DeserializeObject<List<PlanReview>>(jsonString.Result);

              });
            task.Wait();
            return items;
        }

        /// <summary>
        /// gets AION project by ID
        /// </summary>
        /// <returns></returns>
        public ProjectEstimation GetProjectDetailsByProjectId(int projectId)
        {
            ProjectEstimation item = new ProjectEstimation();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/EstimationCRUD/GetProjectDetailsByProjectId?projectId={0}", projectId.ToString()))
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

        /// <summary>
        /// gets list of catalogitems for pending reasons
        /// </summary>
        /// <returns></returns>
        public List<CatalogItem> GetAllPendingReasons()
        {

            List<CatalogItem> items = new List<CatalogItem>();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync("api/EstimationCRUD/GetAllPendingReasons")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  if (response.IsSuccessStatusCode == false)
                  {
                      var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                  }
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  items = JsonConvert.DeserializeObject<List<CatalogItem>>(jsonString.Result);

              });
            task.Wait();
            return items;
        }
        /// <summary>
        /// get all facilitators from accela
        /// </summary>
        /// <returns></returns>
        public List<Facilitator> GetAllFacilitators()
        {

            List<Facilitator> items = new List<Facilitator>();
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;


            var task = client.GetAsync("api/User/GetAllFacilitators")
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<Facilitator>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }


        public List<UserIdentity> GetAllManagers()
        {

            List<UserIdentity> items = new List<UserIdentity>();
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;


            var task = client.GetAsync("api/User/GetAllManagers")
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
        /// get all estimators
        /// </summary>
        /// <returns></returns>
        public List<EstimatorUIModel> GetAllEstimators()
        {

            List<EstimatorUIModel> items = new List<EstimatorUIModel>();
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;


            var task = client.GetAsync("api/User/GetAllEstimators")
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<EstimatorUIModel>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }
        /// <summary>
        /// Get ALL NPA Types for Admin
        /// </summary>
        /// <returns></returns>
        public List<NpaType> GetAllNpaTypes()
        {

            List<NpaType> items = new List<NpaType>();
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;


            var task = client.GetAsync("api/NPA/GetAllNpaTypes")
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<NpaType>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }
        /// <summary>
        /// get user by email, accela system type(2)
        /// </summary>
        /// <returns></returns>
        public UserIdentity GetUserIdentityByEmailSysRef(string email, int sysrefid)
        {
            //To Do remove after we fix -wkb
            var message = "[GetUserIdentityByEmailSysRef] We are hitting this method a lot. Adding  GUID to help define differences:" + Guid.NewGuid() + ".";
            var logging = Logging.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message,
                string.Empty, string.Empty, string.Empty);

            UserIdentity items = new UserIdentity();
            //string email, int externalSystemEnumID
            string responseuri = string.Format("api/User/GetUserIdentityByEmailExtSystem?email={0}&externalSystemEnumID={1}", email, sysrefid);

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
              items = JsonConvert.DeserializeObject<UserIdentity>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        public UserIdentity GetUserIdentityByUserName(string userName)
        {
            //To Do remove after we fix -wkb
            var message = "[GetUserIdentityByUserName] We are hitting this method a lot. Adding  GUID to help define differences:" + Guid.NewGuid() + ".";
            var logging = Logging.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message,
                string.Empty, string.Empty, string.Empty);

            UserIdentity items = new UserIdentity();
            //string email, int externalSystemEnumID
            string responseuri = string.Format("api/User/GetUserIdentityByUserName?userName={0}", userName);

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
              items = JsonConvert.DeserializeObject<UserIdentity>(jsonString.Result);

          });
            task.Wait();
            return items;
        }
        /// <summary>
        /// get user identity by ID
        /// </summary>
        /// <returns></returns>
        public UserIdentity GetUserIdentityByID(int ID)
        {

            var message = "[GetUserIdentityByID] We are hitting this method a lot. Adding  GUID to help define differences:" + Guid.NewGuid() + ".";
            var logging = Logging.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message,
                string.Empty, string.Empty, string.Empty);

            UserIdentity items = new UserIdentity();
            string responseuri = string.Format("api/User/GetUserIdentityByID?id={0}", ID);

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
              items = JsonConvert.DeserializeObject<UserIdentity>(jsonString.Result);

          });
            task.Wait();
            return items;
        }
        /// <summary>
        /// Upsert NPA
        /// </summary>
        /// <returns></returns>
        public int UpsertNPA(NonProjectAppointment NPA)
        {

            int items = 0;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/UpsertNPA", new StringContent(
            JsonConvert.SerializeObject(NPA, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
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

        /// <summary>
        /// Upsert NPA
        /// </summary>
        /// <returns></returns>


        internal List<CustmrMeetings> GetMeetingsByUserID(int userId)
        {
            //EstimationCRUDAdapter

            List<CustmrMeetings> item = new List<CustmrMeetings>();
            string responseuri = string.Format("api/Customer/GetMeetingsByUserID?userId={0}", userId);

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
              item = JsonConvert.DeserializeObject<List<CustmrMeetings>>(jsonString.Result);

          });
            task.Wait();
            return item;
        }

        public List<NPASearchResult> SearchNPA(int type, int reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {


            List<NPASearchResult> items = new List<NPASearchResult>();
            string responseuri = string.Format("api/NPA/SearchNPAs?type={0}&reviewerId={1}&searchtxt={2}&startdate={3}&enddate={4}", type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            //var inputData = new SearchInput { Type = type, ReviewerId = reviewerId,SearchText=searchtxt,StartDate=startdate, EndDate=enddate };
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
              items = JsonConvert.DeserializeObject<List<NPASearchResult>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public bool DeleteNPAs(NPADeleteInput npaDeleteInput)
        {

            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/DeleteNPA?scheduleIdList={0}&flag={1}", new StringContent(
            JsonConvert.SerializeObject(npaDeleteInput), Encoding.UTF8, "application/json"))
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
        /// Add Attendees to NPA
        /// </summary>
        /// <returns></returns>
        public bool AddAttendeesToNPA(ApptAttendeesManagerModel model)
        {

            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/InsertNewNPAAttendees", new StringContent(
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
        /// Get Active Meeting Rooms
        /// </summary>
        /// <returns></returns>
        public List<MeetingRoom> GetActiveMeetingRooms(string meetingType = "")
        {

            List<MeetingRoom> items = new List<MeetingRoom>();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            string requestUri = string.Format("api/NPA/GetActiveMeetingRooms?meetingType={0}", meetingType);

            var task = client.GetAsync(requestUri)
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<MeetingRoom>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        public List<NPASearchResult> GetNPAList()
        {
            List<NPASearchResult> items = new List<NPASearchResult>();
            string responseuri = string.Format("api/NPA/GetNPAList");

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
              items = JsonConvert.DeserializeObject<List<NPASearchResult>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public List<NPASearchResult> GetEndingSoonList()
        {
            List<NPASearchResult> items = new List<NPASearchResult>();
            string responseuri = string.Format("api/NPA/GetEndingSoonList");

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
              items = JsonConvert.DeserializeObject<List<NPASearchResult>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
        /// <summary>
        /// Get PermissionMapping object by User ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public PermissionMapping GetPermissionMapByUserID(int ID)
        {
            PermissionMapping item = new PermissionMapping();
            string responseuri = string.Format("api/User/GetPermissionMapByUserID?id={0}", ID);

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
              item = JsonConvert.DeserializeObject<PermissionMapping>(jsonString.Result);

          });
            task.Wait();

            return item;
        }

        public bool ManualScheduleCapacity(SchedulePlanReviewCapacityParams model)
        {

            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/ManualScheduleCapacity", new StringContent(
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

        public AutoScheduledExpressValues GetAutoScheduledDataExpress(AutoScheduledExpressParams model)
        {
            AutoScheduledExpressValues items = new AutoScheduledExpressValues();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/GetAutoScheduledDataExpress", new StringContent(
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
              items = JsonConvert.DeserializeObject<AutoScheduledExpressValues>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        public AutoScheduledFacilitatorMeetingValues GetAutoScheduledDataFacilitatorMeeting(AutoScheduledFacilitatorMeetingParams model)
        {
            AutoScheduledFacilitatorMeetingValues items = new AutoScheduledFacilitatorMeetingValues();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/GetAutoScheduledDataFacilitatorMeeting", new StringContent(
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
              items = JsonConvert.DeserializeObject<AutoScheduledFacilitatorMeetingValues>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// update User IsExpressSched
        /// </summary>
        /// <returns></returns>
        public bool UpdateUser(UserIdentity user)
        {

            bool items = false;
            UserIdentity obj = user;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/UpdateUser", new StringContent(
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

        public int CreateUser(UserIdentity user)
        {

            int items = 0;
            UserIdentity obj = user;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/CreateUser", new StringContent(
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

        public bool GetUserIdentityByUserBE(UserIdentity user)
        {

            bool items = false;
            UserIdentity obj = user;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;



            var task = client.PostAsync("api/User/GetUserIdentityByUserBE", new StringContent(
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
        /// update User UpdateUserProjectTypeXRef
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserProjectTypeXRef(UserProjectTypeXref userProjRef)
        {

            bool items = false;
            UserProjectTypeXref obj = userProjRef;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/UpdateUserProjectTypeXref", new StringContent(
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

        public UserProjectTypeXref GetSelectedProjectTypeXrefList(int userID)
        {

            UserProjectTypeXref item = new UserProjectTypeXref();
            string responseuri = string.Format("api/User/GetSelectedProjectTypeXrefList?userID={0}", userID);

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
              item = JsonConvert.DeserializeObject<UserProjectTypeXref>(jsonString.Result);

          });
            task.Wait();
            return item;
        }

        /// <summary>
        /// update User UpdateUserDepartmentXRef
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserDepartmentXRef(UserDepartmentXref userDeptXRef)
        {

            bool items = false;
            UserDepartmentXref obj = userDeptXRef;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/UpdateUserDepartmentXref", new StringContent(
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

        public UserDepartmentXref GetSelectedDepartmentXrefList(int userID)
        {

            UserDepartmentXref item = new UserDepartmentXref();
            string responseuri = string.Format("api/User/GetSelectedDepartmentXrefList?userID={0}", userID);

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
              item = JsonConvert.DeserializeObject<UserDepartmentXref>(jsonString.Result);

          });
            task.Wait();
            return item;
        }


        /*******************************************************************/
        /*******************************************************************/
        public List<HolidayConfig> GetHolidayConfigList()
        {

            List<HolidayConfig> items = new List<HolidayConfig>();
            string responseuri = string.Format("api/Admin/GetHolidayConfigList");

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
              items = JsonConvert.DeserializeObject<List<HolidayConfig>>(jsonString.Result);

          });
            task.Wait();

            return items;

        }

        public List<DateTime> GetHolidayDateList()
        {
            List<DateTime> items = new List<DateTime>();
            string responseuri = string.Format("api/Admin/GetHolidayDateList");

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
              items = JsonConvert.DeserializeObject<List<DateTime>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public int DeleteHoliday(List<int> HolidayIds, int wrkrid)
        {
            //HolidayConfigAdapter
            // public int DeleteHoliday(IEnumerable<int> HolidayIds)

            int items = 0;
            HolidayConfigManagerModel obj = new HolidayConfigManagerModel
            {
                HolidayIds = HolidayIds,
                WrkrId = wrkrid
            };
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/DeleteHoliday", new StringContent(
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
        public int InsertHolidayConfig(HolidayConfig holidayConfig)
        {
            //HolidayConfigModelBO holiday = new HolidayConfigModelBO();

            int items = 0;
            HolidayConfig obj = holidayConfig;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/InsertHolidayConfig", new StringContent(
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

        public bool CreateRoleMappings(int userID, List<int> roleMappings)
        {
            //UserIdentityModelBO bo = new UserIdentityModelBO();
            //RoleMappingManagerModel

            bool items = false;
            RoleMappingManagerModel obj = new RoleMappingManagerModel
            {
                RoleMappings = roleMappings,
                UserId = userID,
                WrkrId = 1//TODO
            };
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/CreateRoleMappings", new StringContent(
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
        public bool DeleteRoleMappings(int userID, List<int> roleMappings)
        {
            //UserIdentityModelBO bo = new UserIdentityModelBO();
            //RoleMappingManagerModel

            bool items = false;
            RoleMappingManagerModel obj = new RoleMappingManagerModel
            {
                RoleMappings = roleMappings,
                UserId = userID,
                WrkrId = 1//TODO
            };
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/User/DeleteRoleMappings", new StringContent(
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
        public ProjectEstimation GetProjectDetailsForEstimationByAccelaId(string id, string recidtxt)
        {
            //EstimationCRUDAdapter

            ProjectEstimation item = new ProjectEstimation();
            string responseuri = string.Format("api/EstimationCRUD/GetProjectDetailsForEstimationByAccelaId?accelaModelProjectId={0}&recidtxt={1}", id, recidtxt);

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

        public ProjectEstimation GetProjectDetailsByExternalRefInfo(string id)
        {
            //EstimationCRUDAdapter

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

        public List<ProjectAudit> GetProjectAudits(int projectid)
        {
            // new ProjectAuditModelBO().GetProjectAudits

            List<ProjectAudit> items = new List<ProjectAudit>();
            string responseuri = string.Format("api/EstimationCRUD/GetProjectAudits?projectid={0}", projectid.ToString());

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
              items = JsonConvert.DeserializeObject<List<ProjectAudit>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
        public List<AuditActionRef> GetAuditActionRefs()
        {

            List<AuditActionRef> items = new List<AuditActionRef>();
            string responseuri = string.Format("api/Admin/GetAuditActionRefs");

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
              items = JsonConvert.DeserializeObject<List<AuditActionRef>>(jsonString.Result);

          });
            task.Wait();

            return items;

        }
        public bool DefaultEstimationHourModelRefreshList()
        {
            //DefaultEstimationHourModelBO().RefreshList(); //refre

            bool items = false;
            string responseuri = string.Format("api/Admin/DefaultEstimationHourModelRefreshList");

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

        public List<SystemRole> GetSystemRolesByUserId(int userID)
        {
            //SystemRoleModelBO bo = new SystemRoleModelBO();

            List<SystemRole> items = new List<SystemRole>();
            string responseuri = string.Format("api/User/GetSystemRolesByUserId?userid={0}", userID.ToString());

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
              items = JsonConvert.DeserializeObject<List<SystemRole>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public List<Department> GetDesignatedDepartmentsByUserId(int userID)
        {
            //SystemRoleModelBO bo = new SystemRoleModelBO();

            List<Department> items = new List<Department>();
            string responseuri = string.Format("api/User/GetDesignatedDepartmentsByUserId?userid={0}", userID.ToString());

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
              items = JsonConvert.DeserializeObject<List<Department>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public Department GetDepartmentByEnum(DepartmentNameEnums departmentNameEnum)
        {
            Department item = new Department();
            string responseuri = string.Format("api/User/GetGetDepartmentByEnum?DeptEnumID={0}", (int)departmentNameEnum);

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
              item = JsonConvert.DeserializeObject<Department>(jsonString.Result);

          });
            task.Wait();

            return item;
        }


        public bool InsertNpaType(NpaType data)
        {
            bool items = false;
            NpaType obj = data;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/InsertNpaType", new StringContent(
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
        public bool MakeNpaTypeActive(NpaType data)
        {
            bool items = false;
            NpaType obj = data;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/MakeNpaTypeActive", new StringContent(
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
        public bool MakeNpaTypeInActive(NpaType data)
        {
            bool items = false;
            NpaType obj = data;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/NPA/MakeNpaTypeInActive", new StringContent(
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
        public List<PlanReviewerAvailableHour> GetAllPlanReviewerHours()
        {
            List<PlanReviewerAvailableHour> items = new List<PlanReviewerAvailableHour>();
            string responseuri = string.Format("api/Admin/GetAllPlanReviewerHours");

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
              items = JsonConvert.DeserializeObject<List<PlanReviewerAvailableHour>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public List<PlanReviewerAvailableTime> GetAllPlanReviewerTimes()
        {
            List<PlanReviewerAvailableTime> items = new List<PlanReviewerAvailableTime>();
            string responseuri = string.Format("api/Admin/GetAllPlanReviewerTimes");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.GetAsync(responseuri)
                .ContinueWith(taskwithresponse =>
                {
                    var response = taskwithresponse.Result;
                    if (response.IsSuccessStatusCode == false)
                    {
                        var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
                    }
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    items = JsonConvert.DeserializeObject<List<PlanReviewerAvailableTime>>(jsonString.Result);
                });
            task.Wait();

            return items;
        }

        public bool UpdatePlanReviewAvailableHours(PlanReviewerAvailableHour value)
        {
            bool items = false;
            PlanReviewerAvailableHour obj = value;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/UpdatePlanReviewAvailableHours", new StringContent(
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

        public bool UpdatePlanReviewAvailableTimes(PlanReviewerAvailableTime value)
        {
            bool items = false;
            PlanReviewerAvailableTime obj = value;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/UpdatePlanReviewAvailableTimes", new StringContent(
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

        public DefaultEstimationHour GetDefaultEstimationHour(DepartmentNameEnums departmentNameEnum, PropertyTypeEnums propertyTypeEnum)
        {
            DefaultEstimationHourManagerModel obj = new DefaultEstimationHourManagerModel
            {
                DepartmentNameEnum = departmentNameEnum,
                PropertyTypeEnum = propertyTypeEnum
            };

            DefaultEstimationHour items = new DefaultEstimationHour();
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/GetDefaultEstimationHour", new StringContent(
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
              items = JsonConvert.DeserializeObject<DefaultEstimationHour>(jsonString.Result);
          });
            task.Wait();
            return items;
        }
        public bool UpdateDefaultEstimationHour(DefaultEstimationHour data)
        {
            bool items = false;
            DefaultEstimationHour obj = data;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/UpdateDefaultEstimationHour", new StringContent(
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
        public List<CatalogItem> GetCatalogItems(string catalogGroupExternalRef)
        {
            List<CatalogItem> items = new List<CatalogItem>();
            string responseuri = string.Format("api/Admin/GetCatalogItems?catalogGroupExternalRef={0}", catalogGroupExternalRef);

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
              items = JsonConvert.DeserializeObject<List<CatalogItem>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
        public bool UpdateCatalogItem(CatalogItem data)
        {
            bool items = false;
            CatalogItem obj = data;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Admin/UpdateCatalogItem", new StringContent(
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
        internal static List<SystemRole> GetSystemRoles()
        {
            List<SystemRole> items = new List<SystemRole>();
            string responseuri = string.Format("api/Admin/GetSystemRoles");

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
              items = JsonConvert.DeserializeObject<List<SystemRole>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }


        public List<Facilitator> GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate)
        {
            List<Facilitator> items = new List<Facilitator>();
            string responseuri = string.Format("api/EstimationCRUD/GetFacilitatorWorkloadSummary?startdate={0}&enddate={1}", startdate.ToShortDateString(), enddate.ToShortDateString());

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
              items = JsonConvert.DeserializeObject<List<Facilitator>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }


        public List<ProjectStatus> GetProjectStatusBaseList()
        {
            List<ProjectStatus> items = new List<ProjectStatus>();
            string responseuri = string.Format("api/EstimationCRUD/GetProjectStatusBaseList");

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
              items = JsonConvert.DeserializeObject<List<ProjectStatus>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// get Square footage list
        /// </summary>
        /// <returns></returns>
        public List<OccupancySquareFootage> GetSquareFootageList()
        {

            List<OccupancySquareFootage> items = new List<OccupancySquareFootage>();
            string responseuri = string.Format("api/User/GetSquareFootageList");
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
              items = JsonConvert.DeserializeObject<List<OccupancySquareFootage>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// get Square footage list by user occupancy
        /// </summary>
        /// <returns></returns>
        public List<UserMgmtOccupancy> GetSquareFootageListbyUserOccupancy(int userID)
        {
            List<UserMgmtOccupancy> items = new List<UserMgmtOccupancy>();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/User/GetSquareFootageListbyUserOccupancy?userID={0}", userID))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<UserMgmtOccupancy>>(jsonString.Result);

          });
            task.Wait();
            return items;
        }

        public bool CreateOccupancy(List<OccupancyOutput> occupancy)
        {
            bool items = false;
            List<OccupancyOutput> obj = occupancy;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/User/CreateOccupancy", new StringContent(
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

        public bool DeleteOccupancy(int userId)
        {
            bool items = false;

            string responseuri = string.Format("api/User/DeleteOccupancy?userId={0}", userId);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(userId), Encoding.UTF8, "application/json"))
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
        /// search users by firstname lastname
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public List<UserIdentity> SearchUsers(string firstname, string lastname)
        {
            List<UserIdentity> items = new List<UserIdentity>();

            string responseuri = string.Format("api/User/Search?firstname={0}&lastname={1}", firstname, lastname);

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
              items = JsonConvert.DeserializeObject<List<UserIdentity>>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// Upsert PMA
        /// </summary>
        /// <returns></returns>
        public int UpsertPMA(PreliminaryMeetingAppointment item)
        {
            int items = 0;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/UpsertPMA", new StringContent(
            JsonConvert.SerializeObject(item, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
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
        /// <summary>
        /// Get PMA by Project ID
        /// </summary>
        /// <returns></returns>
        public PreliminaryMeetingAppointment GetPMAById(int id)
        {

            PreliminaryMeetingAppointment items = new PreliminaryMeetingAppointment();

            string responseuri = string.Format("api/Scheduling/GetPMAById?id={0}", id);

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
              items = JsonConvert.DeserializeObject<PreliminaryMeetingAppointment>(jsonString.Result);
          });
            task.Wait();
            return items;
        }



        /// <summary>
        /// Get PMA by Project ID
        /// </summary>
        /// <returns></returns>
        public CustomerMeetingsList GetScheduledMeetingDetailsByProjectId(string projectId)
        {
            CustomerMeetingsList items = new CustomerMeetingsList();

            string responseuri = string.Format("api/Scheduling/GetSchedMeetingsByProjectId?projectId={0}", projectId);

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
              items = JsonConvert.DeserializeObject<CustomerMeetingsList>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// UpdatePrelimDateRequest
        /// </summary>
        /// <returns></returns>
        public bool UpdatePrelimDateRequest(RequestPrelimDatesManagerModel model)
        {
            bool items = false;
            RequestPrelimDatesManagerModel obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Customer/UpdatePrelimDateRequest", new StringContent(
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

        public bool UpdateMeetingDateRequest(RequestMeetingDatesManagerModel model)
        {
            bool items = false;
            RequestMeetingDatesManagerModel obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Customer/UpdateMeetingDateRequest", new StringContent(
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

        public bool UpdateMeetingStatus(SaveMeetingStatus model)
        {
            bool items = false;
            SaveMeetingStatus obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Customer/UpdateMeetingStatus", new StringContent(
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

        public bool UpdateExpressDateRequest(RequestExpressDatesManagerModel model)
        {
            bool items = false;
            RequestExpressDatesManagerModel obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Customer/UpdateExpressDateRequest", new StringContent(
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

        public bool UpdatePrelimStatus(SavePrelimStatus model)
        {
            bool items = false;
            SavePrelimStatus obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Customer/UpdatePrelimStatus", new StringContent(
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

        internal static MeetingAllocationResponse CheckForOutlookMeetingAllocationAvailability(MeetingAllocationRequest data)
        {
            MeetingAllocationResponse items = new MeetingAllocationResponse();
            string responseuri = string.Format("api/Outlook/GetOutlookMeetingAllocation");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            //var task = client.GetAsync(responseuri)
            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              //debugging.
              string jsonrslt = jsonString.Result;
              if (jsonrslt.Contains("START ERROR:"))
                  throw new Exception(response.ReasonPhrase + " content: " + jsonrslt);
              //debugging end.
              items = JsonConvert.DeserializeObject<MeetingAllocationResponse>(jsonString.Result);

          });
            task.Wait();
            return items;
        }



        /// <summary>
        /// Used by the Schedule Capacity pop up
        /// Returns list by person of scheduled hours, express, npa, plan review
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<ScheduleCapacitySearchResult> SearchScheduleCapacity(ScheduleCapacitySearch search)
        {

            List<ScheduleCapacitySearchResult> items = new List<ScheduleCapacitySearchResult>();
            string responseuri = string.Format("api/Scheduling/SearchScheduleCapacity");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            //var task = client.GetAsync(responseuri)
            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<ScheduleCapacitySearchResult>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }



        /// <summary>
        /// Send NA email if user selects all NA for estimation
        /// </summary>
        /// <returns></returns>
        public bool SendNAEmail(string projectId, string projectName, string projectAddress)
        {
            bool success = false;
            SendNAEmailModel _sendNAEmailModel = new SendNAEmailModel
            {
                AccelaProjectRefId = projectId,
                ProjectAddress = projectAddress,
                ProjectName = projectName
            };

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Email/SendNAEmail", new StringContent(
            JsonConvert.SerializeObject(_sendNAEmailModel), Encoding.UTF8, "application/json"))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  success = JsonConvert.DeserializeObject<bool>(jsonString.Result);

              });
            task.Wait();
            return success;
        }

        /// <summary>
        /// Send email if Estimation set to Pending
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projectStatusDesc"></param>
        /// <param name="pendingCommentsToCustomer"></param>
        /// <param name="username"></param>
        /// <param name="timestamp"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public bool SendPendingNotificationEmail(SendPendingEmailModel sendPendingEmailModel)
        {
            bool success = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Email/SendPendingNotificationEmail", new StringContent(
            JsonConvert.SerializeObject(sendPendingEmailModel), Encoding.UTF8, "application/json"))
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  success = JsonConvert.DeserializeObject<bool>(jsonString.Result);

              });
            task.Wait();

            return success;
        }



        public bool UpdateProjectCycle(ProjectCycle projectCycle)
        {
            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/UpdateProjectCycle", new StringContent(
            JsonConvert.SerializeObject(projectCycle), Encoding.UTF8, "application/json"))
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

        public bool SubmitPlanReviewProjectDetails(PlanReviewProjectDetails projectDetails)
        {
            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/SubmitPlanReviewProjectDetails", new StringContent(
            JsonConvert.SerializeObject(projectDetails), Encoding.UTF8, "application/json"))
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

        public bool UpdateProjectDetails(int project, int status, int updated, DateTime? prod)
        {
            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            string responseuri = String.Format("api/Scheduling/UpdateProjectDetails?project={0}&status={1}&updated={2}&prod={3}", project, status, updated, prod);

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

        public bool TestFunction()
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


        /// <summary>
        ///  SearchExpressReservation by dates
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public List<ExpressSearchResult> SearchExpressReservation(DateTime fromdate, DateTime todate)
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

        /// <summary>
        /// Update/Insert Plan Review
        /// Used by Schedule Plan Review page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpsertPlanReview(PlanReview model)
        {

            bool items = false;
            PlanReview obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/UpsertPlanReview", new StringContent(
            JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
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

        public bool UpdatePlanReviewStatus(PlanReview model, AppointmentResponseStatusEnum status)
        {
            bool items = false;
            PlanReview obj = model;
            obj.ApptResponseStatusEnum = status;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/UpdatePlanReviewStatus", new StringContent(
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

        public bool ReschedulePlanReview(PlanReview model)
        {
            bool items = false;
            PlanReview obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/ReschedulePlanReview", new StringContent(
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
        /// Search for reviewer capacity by dates
        /// used by the Schedule Plan Review page
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<ScheduleCapacitySearchResult> ReviewerCapacitySearch(List<ScheduleCapacitySearch> search)
        {

            List<ScheduleCapacitySearchResult> items = new List<ScheduleCapacitySearchResult>();
            string responseuri = string.Format("api/Scheduling/SearchReviewerCapacity");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            //var task = client.GetAsync(responseuri)
            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<List<ScheduleCapacitySearchResult>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }



        public bool SaveReservation(List<ReserveExpressReservation> reservation)
        {
            bool items = false;
            List<ReserveExpressReservation> obj = reservation;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Express/SaveReservation", new StringContent(
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

        public int UpsertEXP(ReserveExpressReservation item)
        {

            int items = 0;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Express/UpsertEXP", new StringContent(
            JsonConvert.SerializeObject(item, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
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

        public List<ReserveExpressReservation> GetExpressReservationList()
        {
            List<ReserveExpressReservation> items = new List<ReserveExpressReservation>();
            string responseuri = string.Format("api/Express/GetExpressReservationList");

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
              items = JsonConvert.DeserializeObject<List<ReserveExpressReservation>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Get fmas by Project ID and Meeting Type
        /// </summary>
        /// <returns></returns>
        public List<FacilitatorMeetingAppointment> GetFMAByProjectIdAndMeetingType(string projectId, string meetingTypeDesc)
        {
            List<FacilitatorMeetingAppointment> items = new List<FacilitatorMeetingAppointment>();

            string responseuri = string.Format("api/Scheduling/GetFMAByProjectIdAndMeetingType?projectId={0}&meetingTypeDesc={1}", projectId, meetingTypeDesc);

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
              items = JsonConvert.DeserializeObject<List<FacilitatorMeetingAppointment>>(jsonString.Result);
          });
            task.Wait();
            return items;
        }

        /// <summary>
        /// Scheduling Express: Manually Schedule button
        ///
        /// </summary>
        /// <param name="item">AutoScheduledExpressParams</param>
        /// <returns>AutoScheduledExpressValues</returns>
        public AutoScheduledExpressUIValues GetManuallyScheduledExpressData(AutoScheduledExpressParams item)
        {
            AutoScheduledExpressUIValues items = new AutoScheduledExpressUIValues();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/GetAutoScheduledDataExpress", new StringContent(
            JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"))
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              if (response.IsSuccessStatusCode == false)
              {
                  var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
              }
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              items = JsonConvert.DeserializeObject<AutoScheduledExpressUIValues>(jsonString.Result);

          });
            task.Wait();
            return items;
        }



        public bool CancelAppointment(int meetingId, string meetingType)
        {
            bool items = false;

            var obj = new
            {
                meetingId = meetingId,
                meetingType = meetingType
            };

            string responseuri = string.Format("api/Scheduling/CancelAppointment?meetingId={0}&meetingType={1}", meetingId, meetingType);

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
              items = JsonConvert.DeserializeObject<bool>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        public ProjectRtapMapping GetProjectRtapMapping(int projectId)
        {
            ProjectRtapMapping item = new ProjectRtapMapping();
            string responseuri = string.Format("api/ProjectEstimation/GetProjectRtapMapping?projectId={0}", projectId);

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
              item = JsonConvert.DeserializeObject<ProjectRtapMapping>(jsonString.Result);

          });
            task.Wait();

            return item;
        }

        public string CreatePlanReviewAcceptanceEmail(PlanReviewEmailModel model)
        {
            string items = null;
            string responseuri = string.Format("api/Customer/CreatePlanReviewAcceptanceEmail");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            //var task = client.GetAsync(responseuri)
            var task = client.PostAsync(responseuri, new StringContent(
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
              items = JsonConvert.DeserializeObject<string>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Gets the list of base permissions
        /// for Create role and modify permissions tabs
        /// in Admin
        /// </summary>
        /// <returns></returns>
        public List<Permission> GetPermissionsList()
        {
            List<Permission> item = new List<Permission>();
            string responseuri = string.Format("api/Admin/GetPermissionList");

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
              item = JsonConvert.DeserializeObject<List<Permission>>(jsonString.Result);

          });
            task.Wait();
            return item;

        }

        /// <summary>
        /// Saves system role
        /// used by Admin Create and Modify Role tab
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal static bool SaveSystemRole(SystemRole model)
        {
            bool items = false;
            string responseuri = string.Format("api/Admin/SaveSystemRole");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
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

        public int GetAssignedFacilitator(int projectId)
        {
            int item = 0;
            string responseuri = string.Format("api/ProjectEstimation/GetAssignedFacilitator?projectId={0}", projectId);

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
              item = JsonConvert.DeserializeObject<int>(jsonString.Result);

          });
            task.Wait();

            return item;
        }

        /// <summary>
        /// Gets the system roles with the permissions list populated
        /// </summary>
        /// <returns></returns>
        public List<SystemRole> GetSystemRolesWithPermissions()
        {
            //SystemRoleModelBO bo = new SystemRoleModelBO();

            List<SystemRole> items = new List<SystemRole>();
            string responseuri = string.Format("api/Admin/GetSystemRolesWithPermissions");

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
              items = JsonConvert.DeserializeObject<List<SystemRole>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
        /// <summary>
        /// Gets the Permission List for a system role
        /// </summary>
        /// <returns></returns>
        internal static List<Permission> GetSystemRolePermissionList(string systemRoleId)
        {
            //SystemRoleModelBO bo = new SystemRoleModelBO();

            List<Permission> items = new List<Permission>();
            string responseuri = string.Format("api/Admin/GetSystemRolePermissionList?id={0}", systemRoleId);

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
              items = JsonConvert.DeserializeObject<List<Permission>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Gets the Permission List for user
        /// </summary>
        /// <returns></returns>
        internal static List<Permission> GetUserPermissionList(string userid)
        {
            List<Permission> items = new List<Permission>();
            string responseuri = string.Format("api/Admin/GetUserPermissionList?id={0}", userid);

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
              items = JsonConvert.DeserializeObject<List<Permission>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        internal static bool SaveUserPermissions(UserPermissionsSaveModel model)
        {
            bool items = false;
            string responseuri = string.Format("api/Admin/SaveUserPermissions");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
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

        internal static bool ResendProjectNotification(SendProjectNotification model)
        {
            bool items = false;
            string responseuri = string.Format("api/Email/ResendProjectNotification");

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
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

        internal static List<ProjectNotificationEmail> GetProjectNotificationEmailList(int id)
        {
            List<ProjectNotificationEmail> items = new List<ProjectNotificationEmail>();
            string responseuri = string.Format("api/Email/GetProjectNotificationEmailList?id={0}", id);

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
              items = JsonConvert.DeserializeObject<List<ProjectNotificationEmail>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }
        /// <summary>
        /// Upsert FMA
        /// </summary>
        /// <returns></returns>
        public int UpsertFMA(FacilitatorMeetingAppointment item)
        {
            int items = 0;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/UpsertFMA", new StringContent(
            JsonConvert.SerializeObject(item, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
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



        public decimal AddSchedulingMultiplier(PropertyTypeEnums propertyType, decimal defaultHrs, DateTime PRstart, DateTime PRend)
        {
            decimal item = 0;
            string responseuri = string.Format("api/Scheduling/AddSchedulingMultiplier?propertyType={0}&defaultHrs={1}&PRstart={2}&PRend={3}", propertyType, defaultHrs, PRstart, PRend);

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
              item = JsonConvert.DeserializeObject<decimal>(jsonString.Result);

          });
            task.Wait();
            return item;
        }

        public bool SendInactivePlanReviewerEmail(int selectedUserId)
        {
            bool items = false;
            string responseuri = string.Format("api/Admin/SendInactivePlanReviewerEmail?userId={0}", selectedUserId);

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync(responseuri, new StringContent(
            JsonConvert.SerializeObject(selectedUserId), Encoding.UTF8, "application/json"))
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

        #region Optimize FIFO
        public bool OptimizeFIFOProjects()
        {
            bool ret = false;

            List<int> projectIds = GetFIFOProjectIdsToBeOptimized();

            foreach (int projectId in projectIds)
            {
                bool success = OptimizeFIFOProject(projectId);
            }

            ret = true;
            return ret;
        }

        public List<int> GetFIFOProjectIdsToBeOptimized()
        {
            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            List<int> projectIds = new List<int>();

            var taskGetProjects = client.GetAsync("api/Function/GetFIFOProjectIdsToBeOptimized")
           .ContinueWith((taskwithresponse) =>
           {
               var response = taskwithresponse.Result;
               if (response.IsSuccessStatusCode == false)
               {
                   var content = response.Content.ReadAsStringAsync().Result; throw new Exception(response.ReasonPhrase + " content: " + content);
               }
               var jsonString = response.Content.ReadAsStringAsync();
               jsonString.Wait();
               projectIds = JsonConvert.DeserializeObject<List<int>>(jsonString.Result);
           });
            taskGetProjects.Wait();

            return projectIds;
        }

        public bool OptimizeFIFOProject(int projectId)
        {
            bool ret = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.GetAsync(string.Format("api/Function/OptimizeFIFOProject?projectId={0}", projectId))
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

        #endregion

        public List<DateTime> GetAvailDatesForExpress(RequestExpressDatesManagerModel model)
        {
            List<DateTime> items = new List<DateTime>();
            RequestExpressDatesManagerModel obj = model;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;
            
            var task = client.PostAsync("api/Customer/SearchAvailableExpressDates", new StringContent(
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
                    items = JsonConvert.DeserializeObject<List<DateTime>>(jsonString.Result);
                });
            task.Wait();
            return items;
        }

    }
}