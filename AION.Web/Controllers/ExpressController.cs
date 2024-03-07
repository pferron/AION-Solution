using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Helpers.APIHelpers;
using AION.Web.Models.Express;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]

    public class ExpressController : BaseControllerWeb
    {
        private string _loggedinUser;
        private ExpressModel _model;

        public ActionResult ExpressMain()
        {
            ExpressAPIHelper expressAPIHelper = new ExpressAPIHelper();

            ExpressViewModel vm = new ExpressViewModel();

            _loggedinUser = GetLoggedInUserEmailAddress();

            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(_loggedinUser))
            {
                vm.UIStatusMessage = UIStatusMessage.Not_Logged_In;
                return RedirectToAction("Index", "Home", new
                {
                    LoggedInUserEmail = vm.LoggedInUserEmail,
                    StatusMessage = vm.UIStatusMessage.ToStringValue()
                });
            }
            else
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            //check perms, send blank vm with status message "insufficient permission"
            if (vm.PermissionMapping.Access_ReserveExpress == false)
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;

            //if uistatusmessage enum != -1 (NA) then return to home controller with message
            if (vm.UIStatusMessage != UIStatusMessage.NA)
                return RedirectToAction("Index", "Home",
                    new
                    {
                        LoggedInUserEmail = vm.LoggedInUserEmail,
                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            _model = expressAPIHelper.GetExpressModel();

            vm.AllReviewers = _model.Reviewers;
            vm.MeetingRoomList = _model.MeetingRooms;
            vm.ExpressConfigViewModel = CreateExpressConfigViewModel();

            //set timepickers on config tab


            var searchReviewers = new List<string>();
            foreach (var reviewer in vm.AllReviewers)
            {
                searchReviewers.Add(reviewer.ID.ToString());
            }
            vm.ExpressReservationViewModel = CreateExpressReservationViewModel(searchReviewers);
            vm.ExpressScheduledViewModel = CreateExpressScheduledViewModel();

            return View(vm);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult Update(ExpressViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        [ActionName("UpdateExpress")]
        public ActionResult UpdateExpress(ExpressSaveViewModel vm)
        {
            string jsonmessage = string.Empty;

            List<ConfigureReserveExpressDays> days = new List<ConfigureReserveExpressDays>();

            foreach (var id in vm.ConfigureReserveExpressId)
            {
                ConfigureReserveExpressDays day = new ConfigureReserveExpressDays();
                if (vm.DaySelected.Contains(id))
                {
                    day.Id = id;
                    day.ActiveInd = true;
                    day.StartDate = vm.StartTime;
                    day.EndDate = vm.EndTime;
                }
                else
                {
                    day.Id = id;
                    day.ActiveInd = false;
                    day.StartDate = null;
                    day.EndDate = null;
                }

                day.ID = vm.LoggedInUserID;
                days.Add(day);
            }

            bool saveConfigure = ExpressAPIHelper.SaveConfigureExpress(days);

            bool updateReviewers = UpdateReviewerRotation(vm.ScheduledReviewers, vm.LoggedInUserID);

            List<ReserveExpressReservation> reservations = new JavaScriptSerializer().Deserialize<List<ReserveExpressReservation>>(vm.SaveReservation);

            bool updateReservation = true;
            if (reservations.Any())
            {
                updateReservation = UpdateReservation(reservations, vm.LoggedInUserID);
            }

            ManualReservation reserv = new JavaScriptSerializer().Deserialize<ManualReservation>(vm.ManualReservation);
            if (reserv.ManualExpressDate.ToString() != "1/1/0001 12:00:00 AM")
            {
                int reserveExpressId = InsertManualReservation(reserv, vm.LoggedInUserID);
            }

            if (saveConfigure && updateReviewers && updateReservation) jsonmessage = "Express updated successfully.";

            return Json(jsonmessage, JsonRequestBehavior.AllowGet);
        }

        private int InsertManualReservation(ManualReservation reserv, int loggedInUserId)
        {
            APIHelper apihelper = new APIHelper();

            var reserveExpress = new ReserveExpressReservation();

            string dt = reserv.ManualExpressDate.ToShortDateString();
            string tmStart = reserv.ManualExpressStartTime.ToShortTimeString();
            string tmEnd = reserv.ManualExpressEndTime.ToShortTimeString();

            DateTime startTime = DateTime.Parse(dt + ' ' + tmStart);
            DateTime endTime = DateTime.Parse(dt + ' ' + tmEnd);

            reserveExpress.ReserveExpressDt = reserv.ManualExpressDate;
            reserveExpress.StartTime = startTime;
            reserveExpress.EndTime = endTime;

            reserveExpress.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Scheduled;

            reserveExpress.MeetingRoomRefId = reserv.MeetingRoomRefId;
            reserveExpress.CreatedUser = new UserIdentity { ID = loggedInUserId };
            reserveExpress.UpdatedUser = reserveExpress.CreatedUser;

            List<AttendeeInfo> attendees = new List<AttendeeInfo>();

            if (reserv.BuildingReviewerSelected != null && reserv.BuildingReviewerSelected != string.Empty)
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.BuildingReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Building
                });
            }
            if (reserv.ElectricalReviewerSelected != null && reserv.ElectricalReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.ElectricalReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Electrical
                });
            }
            if (reserv.MechanicalReviewerSelected != null && reserv.MechanicalReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.MechanicalReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Mechanical
                });
            }
            if (reserv.PlumbingReviewerSelected != null && reserv.PlumbingReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.PlumbingReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Plumbing
                });
            }
            if (reserv.ZoniReviewerSelected != null && reserv.ZoniReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.ZoniReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Zone_County
                });
            }
            if (reserv.ZoniCityReviewerSelected != null && reserv.ZoniCityReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.ZoniCityReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Zone_Cty_Chrlt
                });
            }
            if (reserv.ZoniHuntersvilleReviewerSelected != null && reserv.ZoniHuntersvilleReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.ZoniHuntersvilleReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Zone_Huntersville
                });
            }
            if (reserv.ZoniMintHillReviewerSelected != null && reserv.ZoniMintHillReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.ZoniMintHillReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Zone_Mint_Hill
                });
            }

            if (reserv.FireReviewerSelected != null && reserv.FireReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.FireReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Fire_County
                });
            }
            if (reserv.FireCityReviewerSelected != null && reserv.FireCityReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.FireCityReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Fire_Cty_Chrlt
                });
            }

            if (reserv.BackflowReviewerSelected != null && reserv.BackflowReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.BackflowReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.Backflow
                });
            }
            if (reserv.FoodServiceReviewerSelected != null && reserv.FoodServiceReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.FoodServiceReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.EH_Food
                });
            }
            if (reserv.PublicPoolReviewerSelected != null && reserv.PublicPoolReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.PublicPoolReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool
                });
            }
            if (reserv.FaciLodReviewerSelected != null && reserv.FaciLodReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.FaciLodReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities
                });
            }
            if (reserv.DayCareReviewerSelected != null && reserv.DayCareReviewerSelected != "")
            {
                attendees.Add(new AttendeeInfo()
                {
                    AttendeeId = int.Parse(reserv.DayCareReviewerSelected),
                    DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care
                });
            }

            reserveExpress.NewAttendees = attendees;

            int reserveExpressReserveId = apihelper.UpsertEXP(reserveExpress);

            return reserveExpressReserveId;
        }

        private bool UpdateReservation(List<ReserveExpressReservation> reservations, int loggedInUserId)
        {
            APIHelper apihelper = new APIHelper();

            bool success = false;

            success = apihelper.SaveReservation(reservations);

            return success;
        }

        private bool UpdateReviewerRotation(string scheduledReviewers, int loggedInUserId)
        {
            bool success = false;

            bool deleteSuccess = ExpressAPIHelper.DeleteExpressPlanReviewerRotation();

            if (deleteSuccess)
            {
                List<ApptAttendeeManagerModel> reviewers = new JavaScriptSerializer().Deserialize<List<ApptAttendeeManagerModel>>(scheduledReviewers);

                List<ApptAttendeeManagerModel> reviewersNoDupes = reviewers.GroupBy(x => new { x.DeptNameEnumId, x.AttendeeId })
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .OrderBy(s => s.DeptNameEnumId)
                                                 .ToList();

                var reviewerRotationList = new List<ReserveExpressPlanReviewer>();

                foreach (var reviewer in reviewersNoDupes)
                {
                    reviewerRotationList.Add(
                        new ReserveExpressPlanReviewer
                        {
                            BusinessRefId = reviewer.BusinessRefId,
                            PlanReviewerId = reviewer.AttendeeId,
                            RotationNbr = reviewer.RotationNbr,
                            DeptNameEnumId = reviewer.DeptNameEnumId,
                            CreatedUser = new UserIdentity { ID = loggedInUserId }
                        });
                }

                success = ExpressAPIHelper.SaveExpressReviewerRotation(reviewerRotationList);
            }

            return success;
        }

        private ExpressConfigViewModel CreateExpressConfigViewModel()
        {
            ExpressConfigViewModel model = new ExpressConfigViewModel();

            List<ConfigureReserveExpressDays> selectedDays =
                _model != null ? _model.ConfigureReserveExpressDays : ExpressAPIHelper.GetConfigureReserveExpressList();

            List<ReserveExpressDay> days = new List<ReserveExpressDay>();
            foreach (var item in selectedDays)
            {
                ReserveExpressDay daymodel = new ReserveExpressDay();
                daymodel.ReserveExpressId = item.Id;
                daymodel.WeekDay = item.Day;
                daymodel.IsSelected = item.ActiveInd;
                if (daymodel.IsSelected == true)
                {
                    model.StartTime = item.StartDate.Value;
                    model.EndTime = item.EndDate.Value;
                }

                days.Add(daymodel);
            }
            model.ReserveExpressDay = days;

            // return plan reviewer rotation
            List<ReserveExpressPlanReviewer> reserveExpressPlanReviewers =
                _model != null ? _model.ReserveExpressPlanReviewers : ExpressAPIHelper.GetReserveExpressPlanReviewersListAll();

            model.ScheduledReviewers = ConvertPlanRevToManagerModel(reserveExpressPlanReviewers);

            return model;
        }

        private List<ApptAttendeeManagerModel> ConvertPlanRevToManagerModel(List<ReserveExpressPlanReviewer> reserveExpressPlanReviewers)
        {
            List<ApptAttendeeManagerModel> scheduled = new List<ApptAttendeeManagerModel>();

            foreach (var reviewer in reserveExpressPlanReviewers)
            {
                scheduled.Add(new ApptAttendeeManagerModel()
                {
                    AttendeeId = reviewer.PlanReviewerId,
                    DeptNameEnumId = (int)(DepartmentNameEnums)reviewer.BusinessRefId,
                    DeptNameListId = ScheduleHelpers.GetDepartmentNameListEnumByBusRefIdWSOI(reviewer.BusinessRefId),
                    RotationNbr = reviewer.RotationNbr,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                }); 
            }
            return scheduled;
        }
        private ExpressReservationViewModel CreateExpressReservationViewModel(List<string> searchReviewers)
        {
            ExpressReservationViewModel model = new ExpressReservationViewModel();

            List<ExpressSearchResult> expressList =
                _model != null ? _model.ReserveExpressSearchResults : ExpressAPIHelper.SearchExpressReservation(DateTime.Now, DateTime.Now.AddMonths(1));

            model.ExpressList = expressList;

            return model;
        }

        [ActionName("SearchExpressReservation")]
        public ActionResult SearchExpressReservation(DateTime fromdate, DateTime todate, string LoggedInUserEmail)
        {
            APIHelper apihelper = new APIHelper();
            ExpressViewModel vm = new ExpressViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            if (fromdate.AddMonths(1) <= todate)
            {
                todate = fromdate.AddMonths(1);
            }

            List<ExpressSearchResult> searchList = ExpressAPIHelper.SearchExpressReservation(fromdate, todate);

            vm.MeetingRoomList = apihelper.GetActiveMeetingRooms();

            vm.ExpressReservationViewModel.ExpressList = searchList;

            return PartialView("_ReservationList", vm);
        }

        private ExpressScheduledViewModel CreateExpressScheduledViewModel()
        {
            ExpressScheduledViewModel model = new ExpressScheduledViewModel();

            List<ExpressSearchResult> expressList =
                _model != null ? _model.ScheduledExpressSearchResults : ExpressAPIHelper.GetExpressScheduledByDates(DateTime.Now, DateTime.Now.AddMonths(1));

            model.ExpressList = expressList;

            return model;
        }

        [ActionName("SearchExpressScheduled")]
        public ActionResult SearchExpressScheduled(DateTime fromdate, DateTime todate, string LoggedInUserEmail)
        {
            APIHelper apihelper = new APIHelper();
            ExpressViewModel vm = new ExpressViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            if (fromdate.AddMonths(1) <= todate)
            {
                todate = fromdate.AddMonths(1);
            }

            List<ExpressSearchResult> searchList = ExpressAPIHelper.GetExpressScheduledByDates(fromdate, todate);
            vm.MeetingRoomList = apihelper.GetActiveMeetingRooms();

            vm.ExpressScheduledViewModel.ExpressList = searchList;

            return PartialView("_ScheduledList", vm);
        }

        [ActionName("UpdateAttendees")]
        public ActionResult UpdateAttendees(int expressId, List<ApptAttendeeManagerModel> attendeeIds, int wkrId, int projectScheduleId, bool isSchedule)
        {
            string jsonmessage = string.Empty;
            ApptAttendeesManagerModel model = new ApptAttendeesManagerModel
            {
                AttendeeIds = attendeeIds,
                ApptId = expressId,
                WkrId = wkrId,
                ProjectScheduleID = projectScheduleId,
                IsSchedule = isSchedule,
                ProcessInsertRemoveOnly = true
            };

            //save the attendees
            bool savedAttendees = ExpressAPIHelper.UpdateAttendeesToExpress(model);

            if (savedAttendees) jsonmessage = "Update success.";
            return Json(jsonmessage, JsonRequestBehavior.AllowGet);
        }

        [ActionName("CancelReservations")]
        public ActionResult CancelReservations(List<ApptAttendeesManagerModel> cancellationModel)
        {
            string jsonmessage = string.Empty;

            bool cancelledExpress = ExpressAPIHelper.CancelReservations(cancellationModel);

            if (cancelledExpress) jsonmessage = "Update success.";
            return Json(jsonmessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestReserveFunction()
        {
            ExpressAPIHelper.TestFunction();
            return Json("Test function");
        }

        public ActionResult GetAllAvailableMeetingRoomsForDateRangeForExpress(DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            ExpressViewModel vm = GetAllAvailableMeetingRoomsForDateRange(selDate, selStartTime, selEndTime);
            return PartialView("_MeetingRooms", vm.MeetingRoomList);
        }

        public ActionResult GetAllAvailableMeetingRoomsForDateRangeForExpressScheduled(DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            ExpressViewModel vm = GetAllAvailableMeetingRoomsForDateRange(selDate, selStartTime, selEndTime);
            return PartialView("_MeetingRooms", vm.MeetingRoomList);
        }

        public ActionResult GetAllAvailableMeetingRoomsForDateRangeForManualReservation(DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            ExpressViewModel vm = GetAllAvailableMeetingRoomsForDateRange(selDate, selStartTime, selEndTime);
            return PartialView("_MeetingRooms", vm.MeetingRoomList);
        }

        /// <summary>
        /// Get the ExpressViewModel for the Meeting Rooms Partials
        /// </summary>
        /// <param name="selDate"></param>
        /// <param name="selStartTime"></param>
        /// <param name="selEndTime"></param>
        /// <returns></returns>
        private ExpressViewModel GetAllAvailableMeetingRoomsForDateRange(DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            ExpressViewModel vm = new ExpressViewModel();

            List<MeetingRoom> rooms = new APIHelper().GetActiveMeetingRooms("EXPRESS_MEETING_ROOMS");
            MeetingAllocationRequest data = new MeetingAllocationRequest();
            foreach (var item in rooms)
            {
                data.RequestedParticipantEmailList.Add(item.MeetingRoomEmail);
            }
            data.RequestedStartTime = selDate.Date.AddHours(selStartTime.Hour).AddMinutes(selStartTime.Minute);
            data.RequestedEndTime = selDate.Date.AddHours(selEndTime.Hour).AddMinutes(selEndTime.Minute);
            var ret = APIHelper.CheckForOutlookMeetingAllocationAvailability(data);
            if (ret != null && ret.AllocatedParticipantList != null && ret.AllocatedParticipantList.Count > 0)
            {
                vm.MeetingRoomList = new List<MeetingRoom>();
                foreach (var room in rooms)
                {
                    bool allocated = false;
                    if (ret.AllocatedParticipantList.Any(x => x.ToUpper() == room.MeetingRoomEmail.ToUpper()) == false)
                    {
                        vm.MeetingRoomList.Add(room);
                        continue;
                    }
                    var alc = ret.AllocatedMeetings.Where(x => x.ParticipantEmail == room.MeetingRoomEmail);
                    foreach (var item in alc)
                    {
                        // If meeting allocation shows that the start of new meeting is same as end of allocated meeting then it 
                        // means it can be allocated upto that time since there is no allocation after that point.
                        if (data.RequestedStartTime == item.EndTime)
                            continue;
                        // If meeting allocation shows that the end of new meeting is same as start of allocated meeting then it 
                        // means it can be allocated upto that time since there is no allocation upto that point.
                        if (data.RequestedEndTime == item.StartTime)
                            continue;
                        //if it reaches here then it is truly allocated and so cannot room be allcoated to this time.
                        allocated = true;
                        break;
                    }
                    if (allocated == false)
                        vm.MeetingRoomList.Add(room);
                }
            }
            else
            {
                //no items returned means all rooms are free.
                vm.MeetingRoomList = rooms;
            }
            return vm;
        }
    }
}