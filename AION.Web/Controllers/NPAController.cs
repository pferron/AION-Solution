using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Models;
using AION.Web.Models.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class NPAController : BaseControllerWeb
    {
        private string _loggedinUser = "";
        private NPAModel _model;

        public ActionResult NPAMain()
        {
            NPAAPIHelper npaApiHelper = new NPAAPIHelper();

            NPAViewModel vm = new NPAViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            UpdateUserAndPermissions(vm);

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            //only allow facilitator role, npa role, manager, admin, viewonly
            bool canEdit = vm.PermissionMapping.IsManager || vm.PermissionMapping.IsFacilitator || vm.PermissionMapping.IsViewOnly;

            if (canEdit == false)
            {
                return RedirectToAction("Index", "Home");
            }

            _model = npaApiHelper.GetNPAModel();

            vm.AllReviewers = UserAPIHelper.GetAllReviewers(); //_model.Reviewers;
            vm.NpaTypes = _model.NpaTypes;
            vm.EndingSoonList = _model.EndingSoonResults;
            vm.MeetingRoomList = _model.MeetingRooms;
            vm.SelectedMeetingRoom = new MeetingRoom { MeetingRoomName = "Select a Meeting Room", MeetingRoomRefID = -1 };
            //get the holidays TODO: up to 2 years from now
            List<HolidayConfig> holidays = _model.Holidays;
            //add annual holidays for up to 2 years
            //get the current year 
            int year = DateTime.Now.Year;

            StringBuilder holidaysb = new StringBuilder();
            foreach (HolidayConfig holiday in holidays)
            {
                //if this is an annual holiday, ignore the year and get the correct years for this UI instance
                if (holiday.HolidayAnnualRecurInd)
                {
                    int numberofyears = 2;
                    //this year, this could be a past date from today
                    DateTime thisyearholiday = new DateTime(year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                    if (holiday.HolidayDate > DateTime.Today.AddDays(-1))
                    {
                        holidaysb.Append(thisyearholiday.ToShortDateString());
                        holidaysb.Append(",");
                    }
                    //two years in the future
                    for (int i = 1; i <= numberofyears; i++)
                    {
                        if (holiday.HolidayDate > DateTime.Today.AddDays(-1))
                        {
                            holidaysb.Append(thisyearholiday.AddYears(i).ToShortDateString());
                            holidaysb.Append(",");
                        }
                    }
                }
                else
                {
                    //find out if this is today or the future
                    if (holiday.HolidayDate > DateTime.Today.AddDays(-1))
                    {
                        holidaysb.Append(holiday.HolidayDate.ToShortDateString());
                        holidaysb.Append(",");
                    }
                }
            }
            vm.Holidays = holidaysb.ToString();
            return View(vm);
        }

        [HttpPost]
        [ActionName("CreateNPA")]
        public ActionResult CreateNPA(NPACreateViewModel vm)
        {
            string jsonmessage = string.Empty;
            //reject if no user
            if (vm.LoggedInUserID != 0)
            {
                Helpers.APIHelper apihelper = new Helpers.APIHelper();
                Helpers.ScheduleHelpers scheduleHelper = new Helpers.ScheduleHelpers();
                string loggedinuseremail = GetLoggedInUserEmailAddress();
                UserIdentity updatingUser = apihelper.GetUserIdentityByID(vm.LoggedInUserID);
                vm.UpdatingUser = updatingUser;

                AppointmentRecurrenceRefEnum recur = new AppointmentRecurrenceRefEnum().CreateInstance(vm.DaySelected, vm.RecurrenceSelected);
                //put together date and time
                string dt = vm.StartDate.ToShortDateString();
                string dt2 = vm.EndDate.ToShortDateString();
                string tm = vm.StartTime.ToShortTimeString();
                string tm2 = vm.EndTime.ToShortTimeString();
                //if this is an all day appt, get the default times from the admin 
                // and use those times for start and end
                //TODO: get these values from the Admin Misc Configuration
                //default to 8am and 5pm
                if (vm.AllDay)
                {
                    tm = "08:00 AM";
                    tm2 = "05:00 PM";
                }
                DateTime s = DateTime.Parse(dt + ' ' + tm);
                DateTime e = DateTime.Parse(dt2 + ' ' + tm2);

                int newNPAId = 0;
                DateTime start = s;
                DateTime end = e;

                bool isallplanreviewers = (vm.YNSelected == "Y");
                int? meetingroomrefid = null;
                if (vm.MeetingRoomRefIDSelected != 0) meetingroomrefid = vm.MeetingRoomRefIDSelected;
                DateTime today = DateTime.Now;

                //Get the NPA for saving
                NonProjectAppointment npa = scheduleHelper.CreateSaveModelForNPA(vm);

                //Get the NPA Attendees
                List<ApptAttendeeManagerModel> attendeeIds = scheduleHelper.GetReviewerListByDept(vm);

                //save the npa, get the id back
                newNPAId = apihelper.UpsertNPA(npa);

                if (newNPAId == -1) // could not be scheduled
                {
                    jsonmessage = "NPA could not be scheduled with the recurrence criteria selected.";
                }
                else
                {
                    vm.NPAID = newNPAId;

                    ApptAttendeesManagerModel model = new ApptAttendeesManagerModel
                    {
                        AttendeeIds = attendeeIds,
                        ApptId = newNPAId,
                        WkrId = vm.UpdatingUser.ID
                    };

                    //save the attendees
                    bool savedattendees = apihelper.AddAttendeesToNPA(model);

                    if (savedattendees) jsonmessage = "NPA Created Successfully.";
                }
            }
            else
            {
                jsonmessage = "User Not Found. NPA not created.";
            }
            return Json(jsonmessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("ExpressConflicts")]
        public ActionResult ExpressConflicts(NPACreateViewModel vm)
        {
            string jsonmessage = string.Empty;

            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            Helpers.ScheduleHelpers scheduleHelper = new Helpers.ScheduleHelpers();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            UserIdentity updatingUser = apihelper.GetUserIdentityByID(vm.LoggedInUserID);
            vm.UpdatingUser = updatingUser;

            string dt = vm.StartDate.ToShortDateString();
            string dt2 = vm.EndDate.ToShortDateString();
            string tm = vm.StartTime.ToShortTimeString();
            string tm2 = vm.EndTime.ToShortTimeString();
            //if this is an all day appt, get the default times from the admin 
            // and use those times for start and end
            //TODO: get these values from the Admin Misc Configuration
            //default to 8am and 5pm
            if (vm.AllDay)
            {
                tm = "08:00 AM";
                tm2 = "05:00 PM";
            }
            DateTime s = DateTime.Parse(dt + ' ' + tm);
            DateTime e = DateTime.Parse(dt2 + ' ' + tm2);

            //Get the NPA
            NonProjectAppointment npa = scheduleHelper.CreateSaveModelForNPA(vm);

            //Get the NPA Attendees
            List<ApptAttendeeManagerModel> attendeeIds = scheduleHelper.GetReviewerListByDept(vm);
            List<ReviewerCapacityViewModel> conflictList = new List<ReviewerCapacityViewModel>();

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }
            List<ScheduleCapacitySearch> capacitySearches = new List<ScheduleCapacitySearch>();

            List<ScheduleTime> recurringDates = DateTimeHelper.GetReccuringDates(npa.AppointmentFrom.Value, npa.AppointmentTo.Value, npa.AppointmentRecurrence);

            foreach (ScheduleTime date in recurringDates)
            {
                capacitySearches.Add(new ScheduleCapacitySearch
                {
                    ReviewerSearchList = attendeeIds.Select(x => x.AttendeeId.ToString()).ToList(),
                    BeginDateTime = date.StartDate,
                    EndDateTime = date.EndDate,
                    WrkrId = 1
                });
            }

            List<ScheduleCapacitySearchResult> conflicts = apihelper.ReviewerCapacitySearch(capacitySearches);
            foreach (ScheduleCapacitySearchResult item in conflicts)
            {
                string reviewerName = item.FirstName + " " + item.LastName;
                foreach (Meeting meeting in item.ExpressMeetings)
                {
                    string meetingtypename = meeting.MeetingType > 0 ? meeting.MeetingType.ToStringValue() : string.Empty;
                    conflictList.Add(new ReviewerCapacityViewModel
                    {
                        ReviewerName = reviewerName,
                        MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meetingtypename : meeting.MeetingName,
                        MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                        MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString()
                    });

                }
                foreach (Meeting meeting in item.ExpressReservations)
                {
                    string meetingtypename = meeting.MeetingType > 0 ? meeting.MeetingType.ToStringValue() : string.Empty;
                    conflictList.Add(new ReviewerCapacityViewModel
                    {
                        ReviewerName = reviewerName,
                        MeetingName = string.IsNullOrWhiteSpace(meeting.MeetingName) ? meetingtypename : meeting.MeetingName,
                        MeetingBeginDtTm = meeting.MeetingStart.ToShortDateString() + " " + meeting.MeetingStart.ToShortTimeString(),
                        MeetingEndDtTm = meeting.MeetingEnd.ToShortDateString() + " " + meeting.MeetingEnd.ToShortTimeString()
                    });

                }
            }


            return Json(conflictList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("SearchNPA")]
        public ActionResult SearchNPA(int type, int reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate, string LoggedInUserEmail)
        {

            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            NPAViewModel vm = new NPAViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            vm.ModifyList = apihelper.SearchNPA(type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);
            return PartialView("_ModifySearchList", vm);
        }

        [ActionName("EndingSearchNPA")]
        public ActionResult EndingSearchNPA(int type, int reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate, string LoggedInUserEmail)
        {

            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            NPAViewModel vm = new NPAViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            //if start and end date are blank set the search to 30 days
            if (!startdate.HasValue) { startdate = DateTime.Now; }
            if (!enddate.HasValue) { enddate = DateTime.Now.AddMonths(1); }
            var npaSearchResults = apihelper.SearchNPA(type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);
            vm.EndingSoonList = npaSearchResults.Where(x => x.IsRecurring == "Y").ToList();

            return PartialView("_EndingSearchList", vm);
        }


        [HttpPost]
        [ActionName("DeleteAndSearchNPA")]
        public ActionResult DeleteAndSearchNPA(List<int> scheduleIds, int type, int reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate, string LoggedInUserEmail, bool cancelRecurringflag)
        {

            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            NPAViewModel vm = new NPAViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            NPADeleteInput model = new NPADeleteInput
            {
                ScheduleIds = scheduleIds,
                Flag = cancelRecurringflag
            };
            apihelper.DeleteNPAs(model);
            vm.ModifyList = apihelper.SearchNPA(type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);
            return PartialView("_ModifySearchList", vm);
        }


        [HttpPost]
        [ActionName("DeleteAndSearchNPAEnding")]
        public ActionResult DeleteAndSearchNPAEnding(List<int> scheduleIds, int type, int reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate, string LoggedInUserEmail, bool cancelRecurringflag = false)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            NPAViewModel vm = new NPAViewModel();

            SetUpViewModelBase<NPAViewModel>(vm);

            NPADeleteInput model = new NPADeleteInput
            {
                ScheduleIds = scheduleIds,
                Flag = cancelRecurringflag
            };
            apihelper.DeleteNPAs(model);

            var endingSoonResults = apihelper.SearchNPA(type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);
            vm.EndingSoonList = endingSoonResults.Where(x => x.IsRecurring == "Y").ToList();
            return PartialView("_EndingSearchList", vm);
        }

        /// <summary>
        /// Insert attendees from Add/Remove 
        /// needs npaid, (userid, businessrefid)
        /// </summary>
        /// <param name="npaid"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAttendees")]
        public ActionResult UpdateAttendees(int npaId, List<ApptAttendeeManagerModel> attIds, int wrkrId, int schId)
        {
            string jsonmessage = string.Empty;
            ApptAttendeesManagerModel model = new ApptAttendeesManagerModel
            {
                AttendeeIds = attIds,
                ApptId = npaId,
                WkrId = wrkrId,
                ProjectScheduleID = schId
            };

            //save the attendees
            bool savedattendees = NPAAPIHelper.UpdateAttendeesToNPA(model);

            if (savedattendees) jsonmessage = "Update sucess.";
            return Json(jsonmessage, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Gets Reviewer Select List based on Dept Enum id
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult GetReviewerSelectList(string txt)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            NPASearchViewModel vm = new NPASearchViewModel();
            vm.AllReviewers = UserAPIHelper.GetAllReviewers();

            switch (txt)
            {
                case DepartmentNameList.Building:
                    return Json(vm.BldgPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Electrical:
                    return Json(vm.ElecPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Mechanical:
                    return Json(vm.MechPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Plumbing:
                    return Json(vm.PlumPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Fire:
                    return Json(vm.FirePersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Backflow:
                    return Json(vm.BackPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Day_Care:
                    return Json(vm.DayCPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Facility_Lodging:
                    return Json(vm.FaciPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Food_Service:
                    return Json(vm.FoodPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Public_Pool:
                    return Json(vm.PoolPersonSelectList, JsonRequestBehavior.AllowGet);
                case DepartmentNameList.Zoning:
                    return Json(vm.ZoniPersonSelectList, JsonRequestBehavior.AllowGet);
                default:
                    break;
            }


            return Json(vm.AllReviewersSelectList, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods

        #endregion Private Methods

    }
}