using AION.Base;
using AION.BL;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Models.Scheduling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class ScheduleCapacityController : BaseControllerWeb
    {
        private string _loggedinUser;

        /// <summary>
        /// Perform the search and return the partial view with results
        /// </summary>
        /// <returns></returns>
        public ActionResult ScheduleCapacitySearch(ScheduleCapacityViewModel vm)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            ScheduleCapacitySearch search = new ScheduleCapacitySearch();
            search.BeginDateTime = vm.StartDate;
            search.EndDateTime = vm.EndDate;
            search.ReviewerSearchList = new List<string>();

            //init any null lists
            vm.BldgReviewerSelected = vm.BldgReviewerSelected == null ? new List<string>() : vm.BldgReviewerSelected;
            vm.ElecReviewerSelected = vm.ElecReviewerSelected == null ? new List<string>() : vm.ElecReviewerSelected;
            vm.MechReviewerSelected = vm.MechReviewerSelected == null ? new List<string>() : vm.MechReviewerSelected;
            vm.PlumReviewerSelected = vm.PlumReviewerSelected == null ? new List<string>() : vm.PlumReviewerSelected;
            vm.FireReviewerSelected = vm.FireReviewerSelected == null ? new List<string>() : vm.FireReviewerSelected;
            vm.ZoniReviewerSelected = vm.ZoniReviewerSelected == null ? new List<string>() : vm.ZoniReviewerSelected;
            vm.PoolReviewerSelected = vm.PoolReviewerSelected == null ? new List<string>() : vm.PoolReviewerSelected;
            vm.DayCReviewerSelected = vm.DayCReviewerSelected == null ? new List<string>() : vm.DayCReviewerSelected;
            vm.FaciReviewerSelected = vm.FaciReviewerSelected == null ? new List<string>() : vm.FaciReviewerSelected;
            vm.FoodReviewerSelected = vm.FoodReviewerSelected == null ? new List<string>() : vm.FoodReviewerSelected;
            vm.BackReviewerSelected = vm.BackReviewerSelected == null ? new List<string>() : vm.BackReviewerSelected;

            search.ReviewerSearchList.AddRange(vm.BldgReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.ElecReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.MechReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.PlumReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.FireReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.ZoniReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.PoolReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.DayCReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.FaciReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.FoodReviewerSelected);
            search.ReviewerSearchList.AddRange(vm.BackReviewerSelected);

            vm.ScheduleCapacitySearchList = apihelper.SearchScheduleCapacity(search);
            return PartialView("_ScheduleCapacitySearchResults", vm);

        }

        public ActionResult ReviewerCapacitySearch(ScheduleCapacityListViewModel vmlist)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            ScheduleCapacityViewModel vm = vmlist.ScheduleCapacityList.FirstOrDefault();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            List<ScheduleCapacitySearch> capacitySearches = new List<ScheduleCapacitySearch>();

            foreach (ScheduleCapacityViewModel model in vmlist.ScheduleCapacityList)
            {
                ScheduleCapacitySearch search = new ScheduleCapacitySearch();
                search.BeginDateTime = model.StartDate;
                search.EndDateTime = model.EndDate;
                search.ReviewerSearchList = new List<string>();

                //init any null lists
                model.BldgReviewerSelected = model.BldgReviewerSelected == null ? new List<string>() : model.BldgReviewerSelected;
                model.ElecReviewerSelected = model.ElecReviewerSelected == null ? new List<string>() : model.ElecReviewerSelected;
                model.MechReviewerSelected = model.MechReviewerSelected == null ? new List<string>() : model.MechReviewerSelected;
                model.PlumReviewerSelected = model.PlumReviewerSelected == null ? new List<string>() : model.PlumReviewerSelected;
                model.FireReviewerSelected = model.FireReviewerSelected == null ? new List<string>() : model.FireReviewerSelected;
                model.ZoniReviewerSelected = model.ZoniReviewerSelected == null ? new List<string>() : model.ZoniReviewerSelected;
                model.PoolReviewerSelected = model.PoolReviewerSelected == null ? new List<string>() : model.PoolReviewerSelected;
                model.DayCReviewerSelected = model.DayCReviewerSelected == null ? new List<string>() : model.DayCReviewerSelected;
                model.FaciReviewerSelected = model.FaciReviewerSelected == null ? new List<string>() : model.FaciReviewerSelected;
                model.FoodReviewerSelected = model.FoodReviewerSelected == null ? new List<string>() : model.FoodReviewerSelected;
                model.BackReviewerSelected = model.BackReviewerSelected == null ? new List<string>() : model.BackReviewerSelected;

                search.ReviewerSearchList.AddRange(model.BldgReviewerSelected);
                search.ReviewerSearchList.AddRange(model.ElecReviewerSelected);
                search.ReviewerSearchList.AddRange(model.MechReviewerSelected);
                search.ReviewerSearchList.AddRange(model.PlumReviewerSelected);
                search.ReviewerSearchList.AddRange(model.FireReviewerSelected);
                search.ReviewerSearchList.AddRange(model.ZoniReviewerSelected);
                search.ReviewerSearchList.AddRange(model.PoolReviewerSelected);
                search.ReviewerSearchList.AddRange(model.DayCReviewerSelected);
                search.ReviewerSearchList.AddRange(model.FaciReviewerSelected);
                search.ReviewerSearchList.AddRange(model.FoodReviewerSelected);
                search.ReviewerSearchList.AddRange(model.BackReviewerSelected);

                capacitySearches.Add(search);

            }

            //returns the list to the ajax request
            vm.ScheduleCapacitySearchList = apihelper.ReviewerCapacitySearch(capacitySearches);
            List<ScheduleCapacitySearchResult> ScheduleCapacitySearchList = vm.ScheduleCapacitySearchList;
            List<ReviewerCapacityViewModel> reviewerCapacities = new List<ReviewerCapacityViewModel>();
            foreach (ScheduleCapacitySearchResult item in ScheduleCapacitySearchList)
            {

                string reviewerName = item.FirstName + " " + item.LastName;
                foreach (Meeting meeting in item.Meetings)
                {
                    reviewerCapacities.Add(new ReviewerCapacityViewModel
                    {
                        ReviewerName = reviewerName,
                        MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                        MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                        MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                        ApptId = meeting.AppointmentId.ToString(),
                        UserId = item.UserId,
                        MeetingTypeName = meeting.MeetingType == 0 ? "Meeting" : meeting.MeetingType.ToStringValue()
                    });

                }
                foreach (Meeting meeting in item.NpaMeetings)
                {
                    reviewerCapacities.Add(new ReviewerCapacityViewModel
                    {
                        ReviewerName = reviewerName,
                        MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                        MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                        MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                        ApptId = meeting.AppointmentId.ToString(),
                        UserId = item.UserId,
                        MeetingTypeName = meeting.MeetingType == 0 ? "NPA" : meeting.MeetingType.ToStringValue()
                    });

                }
                foreach (Meeting meeting in item.ExpressMeetings)
                {
                    reviewerCapacities.Add(new ReviewerCapacityViewModel
                    {
                        ReviewerName = reviewerName,
                        MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                        MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                        MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                        ApptId = meeting.AppointmentId.ToString(),
                        MeetingTypeName = meeting.MeetingType == 0 ? "Meeting" : meeting.MeetingType.ToStringValue(),
                        UserId = item.UserId
                    });

                }
                foreach (Meeting meeting in item.PlanReviews)
                {
                    reviewerCapacities.Add(new ReviewerCapacityViewModel
                    {
                        ReviewerName = reviewerName,
                        MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                        MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                        MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                        ApptId = meeting.AppointmentId.ToString(),
                        MeetingTypeName = meeting.MeetingType == 0 ? "Meeting" : meeting.MeetingType.ToStringValue(),
                        UserId = item.UserId
                    });

                }
            }
            //remove duplicates
            List<ReviewerCapacityViewModel> capacityReturnList = new List<ReviewerCapacityViewModel>();
            foreach (ReviewerCapacityViewModel capacity in reviewerCapacities)
            {
                if (!capacityReturnList.Where(x => x.ApptId == capacity.ApptId
                 && x.UserId == capacity.UserId
                 && x.MeetingTypeName == capacity.MeetingTypeName).Any())
                {
                    capacityReturnList.Add(capacity);
                }
            }

            return Json(capacityReturnList, JsonRequestBehavior.AllowGet);

        }

        private class UserCapacity
        {
            public int UserID;
            public Dictionary<DateTime, double> Capacities;
            public List<ReviewerCapacityViewModel> Appointments;
            public double SearchHours;
        }

        private class PlanReviewCapacityResponse
        {
            public bool isActive;
            public string message;
            public List<ReviewerCapacityViewModel> conflicts;
        }

        public ActionResult ReviewerCapacitySearchPlanReview(ScheduleCapacityListPlanReviewViewModel vmlist)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            SchedulePlanReviewCapacityParams auto_data = new SchedulePlanReviewCapacityParams();

            auto_data.AccelaProjectIDRef = vmlist.AccelaProjectRefId;
            auto_data.ProjectID = vmlist.ProjectId;
            auto_data.BuildingIsPool = vmlist.BuildingIsPool;
            auto_data.ElectricIsPool = vmlist.ElectricIsPool;
            auto_data.MechIsPool = vmlist.MechIsPool;
            auto_data.PlumbIsPool = vmlist.PlumbIsPool;
            auto_data.ZoneIsPool = vmlist.ZoneIsPool;
            auto_data.FireIsPool = vmlist.FireIsPool;
            auto_data.FoodServiceIsPool = vmlist.FoodServiceIsPool;
            auto_data.PoolIsPool = vmlist.PoolIsPool;
            auto_data.FacilityIsPool = vmlist.FacilityIsPool;
            auto_data.DayCareIsPool = vmlist.DayCareIsPool;
            auto_data.BackFlowIsPool = vmlist.BackFlowIsPool;

            auto_data.BuildingUserID = vmlist.BuildingUserID;
            auto_data.ElectricUserID = vmlist.ElectricUserID;
            auto_data.MechUserID = vmlist.MechUserID;
            auto_data.PlumbUserID = vmlist.PlumbUserID;
            auto_data.ZoneUserID = vmlist.ZoneUserID;
            auto_data.FireUserID = vmlist.FireUserID;
            auto_data.FoodServiceUserID = vmlist.FoodServiceUserID;
            auto_data.PoolUserID = vmlist.PoolUserID;
            auto_data.FacilityUserID = vmlist.FacilityUserID;
            auto_data.DayCareUserID = vmlist.DayCareUserID;
            auto_data.BackFlowUserID = vmlist.BackFlowUserID;

            auto_data.Cycle = vmlist.Cycle;

            auto_data.BuildingHours = vmlist.BuildingHours;
            auto_data.ElectricHours = vmlist.ElectricHours;
            auto_data.MechHours = vmlist.MechanicalHours;
            auto_data.PlumbHours = vmlist.PlumbingHours;
            auto_data.ZoneHours = vmlist.ZoningHours;
            auto_data.FireHours = vmlist.FireHours;
            auto_data.FoodServiceHours = vmlist.FoodHours;
            auto_data.PoolHours = vmlist.PoolHours;
            auto_data.FacilityHours = vmlist.LodgeHours;
            auto_data.DayCareHours = vmlist.DaycareHours;
            auto_data.BackFlowHours = vmlist.DaycareHours;

            ScheduleCapacityViewModel vm = vmlist.ScheduleCapacityList.FirstOrDefault();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            UpdateUserAndPermissions(vm);

            List<ScheduleCapacitySearchResultPlanReview> ScheduleCapacitySearchList = new List<ScheduleCapacitySearchResultPlanReview>();
            List<UserCapacity> UserCapacities = new List<UserCapacity>();
            //TODO: change this so it looks at the distinct dates and searches for reviewers on those dates
            //      instead of looping thru each row for each reviewer
            foreach (ScheduleCapacityViewModel model in vmlist.ScheduleCapacityList)
            {
                ScheduleCapacitySearch search = new ScheduleCapacitySearch();
                search.BeginDateTime = model.StartDate;
                search.EndDateTime = model.EndDate;
                search.ReviewerSearchList = new List<string>();
                double SearchHours = 0;

                //init any null lists
                model.BldgReviewerSelected = model.BldgReviewerSelected == null ? new List<string>() : model.BldgReviewerSelected;
                if (model.BldgReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.BuildingHours);
                    auto_data.BuildingScheduleStart = model.StartDate;
                    auto_data.BuildingScheduleEnd = model.EndDate;
                    if (vmlist.BuildingIsPool) continue;
                }
                model.ElecReviewerSelected = model.ElecReviewerSelected == null ? new List<string>() : model.ElecReviewerSelected;
                if (model.ElecReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.ElectricHours);
                    auto_data.ElectricScheduleStart = model.StartDate;
                    auto_data.ElectricScheduleEnd = model.EndDate;
                    if (vmlist.ElectricIsPool) continue;
                }
                model.MechReviewerSelected = model.MechReviewerSelected == null ? new List<string>() : model.MechReviewerSelected;
                if (model.MechReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.MechanicalHours);
                    auto_data.MechScheduleStart = model.StartDate;
                    auto_data.MechScheduleEnd = model.EndDate;
                    if (vmlist.MechIsPool) continue;
                }
                model.PlumReviewerSelected = model.PlumReviewerSelected == null ? new List<string>() : model.PlumReviewerSelected;
                if (model.PlumReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.PlumbingHours);
                    auto_data.PlumbScheduleStart = model.StartDate;
                    auto_data.PlumbScheduleEnd = model.EndDate;
                    if (vmlist.PlumbIsPool) continue;
                }
                model.FireReviewerSelected = model.FireReviewerSelected == null ? new List<string>() : model.FireReviewerSelected;
                if (model.FireReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.FireHours);
                    auto_data.FireScheduleStart = model.StartDate;
                    auto_data.FireScheduleEnd = model.EndDate;
                    if (vmlist.FireIsPool) continue;
                }
                model.ZoniReviewerSelected = model.ZoniReviewerSelected == null ? new List<string>() : model.ZoniReviewerSelected;
                if (model.ZoniReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.ZoningHours);
                    auto_data.ZoneScheduleStart = model.StartDate;
                    auto_data.ZoneScheduleEnd = model.EndDate;
                    if (vmlist.ZoneIsPool) continue;
                }
                model.PoolReviewerSelected = model.PoolReviewerSelected == null ? new List<string>() : model.PoolReviewerSelected;
                if (model.PoolReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.PoolHours);
                    auto_data.PoolScheduleStart = model.StartDate;
                    auto_data.PoolScheduleEnd = model.EndDate;
                    if (vmlist.PoolIsPool) continue;
                }
                model.DayCReviewerSelected = model.DayCReviewerSelected == null ? new List<string>() : model.DayCReviewerSelected;
                if (model.DayCReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.DaycareHours);
                    auto_data.DayCareScheduleStart = model.StartDate;
                    auto_data.DayCareScheduleEnd = model.EndDate;
                    if (vmlist.DayCareIsPool) continue;
                }
                model.FaciReviewerSelected = model.FaciReviewerSelected == null ? new List<string>() : model.FaciReviewerSelected;
                if (model.FaciReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.LodgeHours);
                    auto_data.FacilityScheduleStart = model.StartDate;
                    auto_data.FacilityScheduleEnd = model.EndDate;
                    if (vmlist.FacilityIsPool) continue;
                }
                model.FoodReviewerSelected = model.FoodReviewerSelected == null ? new List<string>() : model.FoodReviewerSelected;
                if (model.FoodReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.FoodHours);
                    auto_data.FoodScheduleStart = model.StartDate;
                    auto_data.FoodScheduleEnd = model.EndDate;
                    if (vmlist.FoodServiceIsPool) continue;
                }
                model.BackReviewerSelected = model.BackReviewerSelected == null ? new List<string>() : model.BackReviewerSelected;
                if (model.BackReviewerSelected.Count() > 0)
                {
                    SearchHours = Decimal.ToDouble(vmlist.BackflowHours);
                    auto_data.BackFlowScheduleStart = model.StartDate;
                    auto_data.BackFlowScheduleEnd = model.EndDate;
                    if (vmlist.BackFlowIsPool) continue;
                }

                search.ReviewerSearchList.AddRange(model.BldgReviewerSelected);
                search.ReviewerSearchList.AddRange(model.ElecReviewerSelected);
                search.ReviewerSearchList.AddRange(model.MechReviewerSelected);
                search.ReviewerSearchList.AddRange(model.PlumReviewerSelected);
                search.ReviewerSearchList.AddRange(model.FireReviewerSelected);
                search.ReviewerSearchList.AddRange(model.ZoniReviewerSelected);
                search.ReviewerSearchList.AddRange(model.PoolReviewerSelected);
                search.ReviewerSearchList.AddRange(model.DayCReviewerSelected);
                search.ReviewerSearchList.AddRange(model.FaciReviewerSelected);
                search.ReviewerSearchList.AddRange(model.FoodReviewerSelected);
                search.ReviewerSearchList.AddRange(model.BackReviewerSelected);

                List<ScheduleCapacitySearchResult> result = apihelper.SearchScheduleCapacity(search);
                var query_bydate = result.GroupBy(x => x.ScheduleDate);
                for (int i = 0; i < query_bydate.Count(); i++)
                {
                    ScheduleCapacitySearchResult s = query_bydate.ElementAt(i).FirstOrDefault();
                    ScheduleCapacitySearchResultPlanReview obj = new ScheduleCapacitySearchResultPlanReview(s);
                    obj.SearchStartDate = search.BeginDateTime;
                    obj.SearchEndDate = search.EndDateTime;
                    obj.SearchHours = (double)apihelper.AddSchedulingMultiplier(vmlist.PropertyType, (decimal)SearchHours, obj.SearchStartDate, model.EndDate);
                    ScheduleCapacitySearchList.Add(obj);

                    //Add to list of users
                    if (!UserCapacities.Where(x => x.UserID == s.UserId).Any())
                    {
                        UserCapacities.Add(new UserCapacity
                        {
                            UserID = s.UserId,
                            Capacities = new Dictionary<DateTime, double>(),
                            Appointments = new List<ReviewerCapacityViewModel>(),
                            SearchHours = obj.SearchHours
                        });
                    }
                }

            }

            bool AllocationFound = apihelper.ManualScheduleCapacity(auto_data);

            List<ReviewerCapacityViewModel> reviewerCapacities = new List<ReviewerCapacityViewModel>();

            //assemble query based on user
            var query = ScheduleCapacitySearchList.GroupBy(x => x.UserId);
            for (int i = 0; i < query.Count(); i++)
            {
                var result = query.ElementAt(i);
                //double totalHours = 0.0;
                //double maxHours = 0.0;
                //double searchHours = 0.0;
                List<ReviewerCapacityViewModel> reviewerCapacities_stage = new List<ReviewerCapacityViewModel>();
                foreach (ScheduleCapacitySearchResultPlanReview item in result)
                {
                    string reviewerName = item.FirstName + " " + item.LastName;
                    foreach (Meeting meeting in item.Meetings)
                    {
                        if (!reviewerCapacities_stage.Any(x => x.UserId == item.UserId && x.ApptId == meeting.AppointmentId.ToString()))
                        {
                            reviewerCapacities_stage.Add(new ReviewerCapacityViewModel
                            {
                                ReviewerName = reviewerName,
                                MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                                MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                                MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                                MeetingDuration = meeting.MeetingEnd.Subtract(meeting.MeetingStart).TotalHours,
                                ApptId = meeting.AppointmentId.ToString(),
                                UserId = item.UserId,
                                MeetingTypeName = meeting.MeetingType == 0 ? "Meeting" : meeting.MeetingType.ToStringValue()
                            });

                        }

                    }
                    foreach (Meeting meeting in item.NpaMeetings)
                    {
                        if (!reviewerCapacities_stage.Any(x => x.UserId == item.UserId && x.ApptId == meeting.AppointmentId.ToString()))
                        {
                            reviewerCapacities_stage.Add(new ReviewerCapacityViewModel
                            {
                                ReviewerName = reviewerName,
                                MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                                MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                                MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                                MeetingDuration = meeting.MeetingEnd.Subtract(meeting.MeetingStart).TotalHours,
                                ApptId = meeting.AppointmentId.ToString(),
                                UserId = item.UserId,
                                MeetingTypeName = meeting.MeetingType == 0 ? "NPA" : meeting.MeetingType.ToStringValue()
                            });
                        }
                    }
                    foreach (Meeting meeting in item.ExpressMeetings)
                    {
                        if (!reviewerCapacities_stage.Any(x => x.UserId == item.UserId && x.ApptId == meeting.AppointmentId.ToString()))
                        {
                            reviewerCapacities_stage.Add(new ReviewerCapacityViewModel
                            {
                                ReviewerName = reviewerName,
                                MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                                MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                                MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                                MeetingDuration = meeting.MeetingEnd.Subtract(meeting.MeetingStart).TotalHours,
                                ApptId = meeting.AppointmentId.ToString(),
                                MeetingTypeName = meeting.MeetingType == 0 ? "Meeting" : meeting.MeetingType.ToStringValue(),
                                UserId = item.UserId
                            });
                        }
                    }
                    foreach (Meeting meeting in item.PlanReviews)
                    {
                        if (!reviewerCapacities_stage.Any(x => x.UserId == item.UserId && x.ApptId == meeting.AppointmentId.ToString()))
                        {
                            reviewerCapacities_stage.Add(new ReviewerCapacityViewModel
                            {
                                ReviewerName = reviewerName,
                                MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meeting.MeetingType.ToStringValue() : meeting.MeetingName,
                                MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                                MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString(),
                                MeetingDuration = meeting.MeetingEnd.Subtract(meeting.MeetingStart).TotalHours,
                                ApptId = meeting.AppointmentId.ToString(),
                                MeetingTypeName = meeting.MeetingType == 0 ? "Meeting" : meeting.MeetingType.ToStringValue(),
                                UserId = item.UserId
                            });
                        }
                    }

                    //add appointments to user's list and check for any capacity conflicts
                    UserCapacity userCapacity = UserCapacities.Where(x => x.UserID == item.UserId).FirstOrDefault();
                    int index = UserCapacities.FindIndex(x => x.Equals(userCapacity));
                    //add day to dict if it isn't already there
                    if (!userCapacity.Capacities.ContainsKey(item.ScheduleDate))
                        userCapacity.Capacities.Add(item.ScheduleDate, 0);
                    foreach (ReviewerCapacityViewModel m in reviewerCapacities_stage)
                    {
                        if (!userCapacity.Appointments.Contains(m))
                        {
                            userCapacity.Capacities[item.ScheduleDate] = userCapacity.Capacities[item.ScheduleDate] + m.MeetingDuration;
                            userCapacity.Appointments.Add(m);
                        }
                    }
                    UserCapacities[index] = userCapacity;

                }
            }

            List<ReviewerCapacityViewModel> capacityReturnList = new List<ReviewerCapacityViewModel>();

            //Go through UserCapacities and check for any over capacity conflicts
            foreach (UserCapacity userCapacity in UserCapacities)
            {
                capacityReturnList.AddRange(userCapacity.Appointments);
            }

            PlanReviewCapacityResponse response = new PlanReviewCapacityResponse();
            if (!AllocationFound)
            {
                response.isActive = true;
                response.message = "The scheduled reviewers are over capacity for the selected dates.  If you continue they will be scheduled over capacity.";
                response.conflicts = capacityReturnList;
            }

            return Json(response, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ScheduleCapacityDDLs(ScheduleCapacityViewModel vm)
        {
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }
            if (Session["LoggedInUser"] == null)
            {
                new AuthenticateHelper().GetViewModelWPerms(vm);
                Session["LoggedInUser"] = JsonConvert.SerializeObject(vm.LoggedInUser);
                Session["PermissionMap"] = JsonConvert.SerializeObject(vm.PermissionMapping);
            }
            else
            {
                vm.PermissionMapping = JsonConvert.DeserializeObject<BusinessEntities.PermissionMapping>(Session["PermissionMap"].ToString());
                vm.LoggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
            }
            //get the reviewers lists

            vm.AllReviewers = UserAPIHelper.GetAllReviewers();


            return PartialView("_ScheduleCapacityDDLs", vm);
        }
        public ActionResult ScheduleCapacityMain(ScheduleCapacityViewModel vm)
        {
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            if (vm == null || vm.LoggedInUser == null)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = UIStatusMessage.Not_Logged_In });
            }
            if (vm.PermissionMapping.Vw_Schdul_Cpcty == false)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = UIStatusMessage.Insufficient_Permission });
            }

            vm.AllReviewers = UserAPIHelper.GetAllReviewers();
            return View(vm);
        }
        #region Private Methods

        #endregion Private Methods

    }
}