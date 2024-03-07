using AION.Base;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Models.Scheduling;
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
    public class ScheduleAPIHelper : BaseController
    {
        public SchedulingModel GetSchedulingModel(ProjectParms parms)
        {
            SchedulingModel item = new SchedulingModel();

            var httpClient = Task.Run(() => GetManagerHttpClient());

            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/GetSchedulingModel", new StringContent(
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
                  item = JsonConvert.DeserializeObject<SchedulingModel>(jsonString.Result);

              });
            task.Wait();
            return item;
        }

        internal static List<Reviewer> GetReviewers(int propertyTypeEnum, int deptNameEnum, bool isExpressSchedulable = false)
        {
            List<Reviewer> items = new List<Reviewer>();
            string responseuri = string.Format("api/User/GetReviewers?propertyTypeEnum={0}&deptNameEnum={1}&isExpressSchedulable={2}", propertyTypeEnum, deptNameEnum, isExpressSchedulable);

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
              items = JsonConvert.DeserializeObject<List<Reviewer>>(jsonString.Result);

          });
            task.Wait();

            return items;
        }

        /// <summary>
        /// Get Plan Review Auto Schedule Data
        /// </summary>
        /// <returns></returns>
        internal static AutoScheduledPlanReviewViewModel GetAutoScheduledDataPlanReview(AutoScheduledPlanReviewParams model)
        {
            AutoScheduledPlanReviewValues items = new AutoScheduledPlanReviewValues();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/GetAutoScheduledDataPlanReview", new StringContent(
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
              items = JsonConvert.DeserializeObject<AutoScheduledPlanReviewValues>(jsonString.Result);

          });
            task.Wait();
            return ConvertAutoScheduledPRtoViewModel(items);
        }

        internal static bool UpsertFIFO(PlanReview pr)
        {
            bool success = false;
            PlanReview obj = pr;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/UpsertFIFO", new StringContent(
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
              success = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return success;
        }

        /// <summary>
        /// Upsert express schedule
        /// From Schedule Express Appointment page
        /// </summary>
        /// <returns></returns>
        internal static bool UpsertEMA(PlanReview pr)
        {
            bool success = false;
            PlanReview obj = pr;
            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;

            var task = client.PostAsync("api/Scheduling/UpsertEMA", new StringContent(
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
              success = JsonConvert.DeserializeObject<bool>(jsonString.Result);
          });
            task.Wait();
            return success;
        }

        /// <summary>
        /// Get Auto Scheduled data for Preliminary Meeting
        /// </summary>
        /// <returns></returns>
        internal static AutoScheduledPrelimValues GetPreliminaryAutoScheduledData(AutoScheduledPrelimParams model)
        {
            AutoScheduledPrelimValues items = new AutoScheduledPrelimValues();

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/GetAutoScheduledData", new StringContent(
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
              items = JsonConvert.DeserializeObject<AutoScheduledPrelimValues>(jsonString.Result);

          });
            task.Wait();
            return ConvertAutoScheduledPrelimtoViewModel(items);
        }

        /// <summary>
        /// This updates the facilitator by project id
        /// Required: project.ID, project.UpdatedUser.ID, project.AssignedFacilitator.Value, current UpdatedDate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal static bool UpdateAssignedFacilitator(ProjectEstimation model)
        {

            bool items = false;

            var httpClient = Task.Run(() => GetManagerHttpClient());
            HttpClient client = httpClient.Result;


            var task = client.PostAsync("api/Scheduling/UpdateAssignedFacilitator", new StringContent(
            JsonConvert.SerializeObject(model, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"))
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

        private static AutoScheduledPlanReviewViewModel ConvertAutoScheduledPRtoViewModel(AutoScheduledPlanReviewValues reviewValues)
        {
            AutoScheduledPlanReviewViewModel retobj = new AutoScheduledPlanReviewViewModel
            {
                BackFlowHours = reviewValues.BackFlowHours,
                BackFlowScheduleEnd = reviewValues.BackFlowScheduleEnd,
                BackFlowScheduleEndTxt = ConvertNullableDTtoString(reviewValues.BackFlowScheduleEnd),
                BackFlowScheduleStart = reviewValues.BackFlowScheduleStart,
                BackFlowScheduleStartTxt = ConvertNullableDTtoString(reviewValues.BackFlowScheduleStart),
                BackFlowUserID = reviewValues.BackFlowUserID,
                BuildingHours = reviewValues.BuildingHours,
                BuildingScheduleEnd = reviewValues.BuildingScheduleEnd,
                BuildingScheduleEndTxt = ConvertNullableDTtoString(reviewValues.BuildingScheduleEnd),
                BuildingScheduleStart = reviewValues.BuildingScheduleStart,
                BuildingScheduleStartTxt = ConvertNullableDTtoString(reviewValues.BuildingScheduleStart),
                BuildingUserID = reviewValues.BuildingUserID,
                DayCareHours = reviewValues.DayCareHours,
                DayCareScheduleEnd = reviewValues.DayCareScheduleEnd,
                DayCareScheduleEndTxt = ConvertNullableDTtoString(reviewValues.DayCareScheduleEnd),
                DayCareScheduleStart = reviewValues.DayCareScheduleStart,
                DayCareScheduleStartTxt = ConvertNullableDTtoString(reviewValues.DayCareScheduleStart),
                DayCareUserID = reviewValues.DayCareUserID,
                ElectricHours = reviewValues.ElectricHours,
                ElectricScheduleEnd = reviewValues.ElectricScheduleEnd,
                ElectricScheduleEndTxt = ConvertNullableDTtoString(reviewValues.ElectricScheduleEnd),
                ElectricScheduleStart = reviewValues.ElectricScheduleStart,
                ElectricScheduleStartTxt = ConvertNullableDTtoString(reviewValues.ElectricScheduleStart),
                ElectricUserID = reviewValues.ElectricUserID,
                ErrorMessage = reviewValues.ErrorMessage,
                FacilityHours = reviewValues.FacilityHours,
                FacilityScheduleEnd = reviewValues.FacilityScheduleEnd,
                FacilityScheduleEndTxt = ConvertNullableDTtoString(reviewValues.FacilityScheduleEnd),
                FacilityScheduleStart = reviewValues.FacilityScheduleStart,
                FacilityScheduleStartTxt = ConvertNullableDTtoString(reviewValues.FacilityScheduleStart),
                FacilityUserID = reviewValues.FacilityUserID,
                FireHours = reviewValues.FireHours,
                FireScheduleEnd = reviewValues.FireScheduleEnd,
                FireScheduleEndTxt = ConvertNullableDTtoString(reviewValues.FireScheduleEnd),
                FireScheduleStart = reviewValues.FireScheduleStart,
                FireScheduleStartTxt = ConvertNullableDTtoString(reviewValues.FireScheduleStart),
                FireUserID = reviewValues.FireUserID,
                FoodScheduleEnd = reviewValues.FoodScheduleEnd,
                FoodScheduleEndTxt = ConvertNullableDTtoString(reviewValues.FoodScheduleEnd),
                FoodScheduleStart = reviewValues.FoodScheduleStart,
                FoodScheduleStartTxt = ConvertNullableDTtoString(reviewValues.FoodScheduleStart),
                FoodServiceHours = reviewValues.FoodServiceHours,
                FoodServiceUserID = reviewValues.FoodServiceUserID,
                MechHours = reviewValues.MechHours,
                MechScheduleEnd = reviewValues.MechScheduleEnd,
                MechScheduleEndTxt = ConvertNullableDTtoString(reviewValues.MechScheduleEnd),
                MechScheduleStart = reviewValues.MechScheduleStart,
                MechScheduleStartTxt = ConvertNullableDTtoString(reviewValues.MechScheduleStart),
                MechUserID = reviewValues.MechUserID,
                PlumbHours = reviewValues.PlumbHours,
                PlumbScheduleEnd = reviewValues.PlumbScheduleEnd,
                PlumbScheduleEndTxt = ConvertNullableDTtoString(reviewValues.PlumbScheduleEnd),
                PlumbScheduleStart = reviewValues.PlumbScheduleStart,
                PlumbScheduleStartTxt = ConvertNullableDTtoString(reviewValues.PlumbScheduleStart),
                PlumbUserID = reviewValues.PlumbUserID,
                PoolHours = reviewValues.PoolHours,
                PoolScheduleEnd = reviewValues.PoolScheduleEnd,
                PoolScheduleEndTxt = ConvertNullableDTtoString(reviewValues.PoolScheduleEnd),
                PoolScheduleStart = reviewValues.PoolScheduleStart,
                PoolScheduleStartTxt = ConvertNullableDTtoString(reviewValues.PoolScheduleStart),
                PoolUserID = reviewValues.PoolUserID,
                ZoneHours = reviewValues.ZoneHours,
                ZoneScheduleEnd = reviewValues.ZoneScheduleEnd,
                ZoneScheduleEndTxt = ConvertNullableDTtoString(reviewValues.ZoneScheduleEnd),
                ZoneScheduleStart = reviewValues.ZoneScheduleStart,
                ZoneScheduleStartTxt = ConvertNullableDTtoString(reviewValues.ZoneScheduleStart),
                ZoneUserID = reviewValues.ZoneUserID,
                ZoneIsPool = reviewValues.ZoneIsPool
            };
            return retobj;
        }

        private static AutoScheduledPreliminaryMeetingViewModel ConvertAutoScheduledPrelimtoViewModel(AutoScheduledPrelimValues reviewValues)
        {
            AutoScheduledPreliminaryMeetingViewModel retobj = new AutoScheduledPreliminaryMeetingViewModel
            {
                BackFlowUserID = reviewValues.BackFlowUserID,
                BuildingUserID = reviewValues.BuildingUserID,
                DayCareUserID = reviewValues.DayCareUserID,
                ElectricUserID = reviewValues.ElectricUserID,
                FacilityUserID = reviewValues.FacilityUserID,
                FireUserID = reviewValues.FireUserID,
                FoodServiceUserID = reviewValues.FoodServiceUserID,
                MechUserID = reviewValues.MechUserID,
                PlumbUserID = reviewValues.PlumbUserID,
                PoolUserID = reviewValues.PoolUserID,
                ZoneUserID = reviewValues.ZoneUserID,
                ScheduleEnd = reviewValues.ScheduleEnd,
                ScheduleStart = reviewValues.ScheduleStart,
                ScheduleEndTxt = ConvertNullableDTtoDTString(reviewValues.ScheduleEnd),
                ScheduleStartTxt = ConvertNullableDTtoDTString(reviewValues.ScheduleStart)
            };
            return retobj;
        }

        private static string ConvertNullableDTtoString(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToShortDateString();
            }

            return string.Empty;
        }

        private static string ConvertNullableDTtoDTString(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToShortDateString() + " " + dateTime.Value.ToShortTimeString();
            }

            return string.Empty;
        }
    }
}
