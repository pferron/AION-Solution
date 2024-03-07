using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Manager.Models.Dashboard;
using AION.Web.Helpers;
using AION.Web.Helpers.APIHelpers;
using AION.Web.Models;
using AION.Web.Models.Express;
using AION.Web.Models.Scheduling;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class SchedulingController : BaseControllerWeb
    {
        private string _loggedinUser;
        public ActionResult SchedulingDashboard(SchedDashboardViewModel vm)
        {
            SetUpViewModelBase<SchedDashboardViewModel>(vm);

            if (vm.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Permission denied." });

            if (TempData["StatusMessage"] != null)
            {
                vm.StatusMessage = TempData["StatusMessage"].ToString();
            }

            DashboardListBase dashboardListBase = DashboardAPIHelper.GetSchedulingDashboardList(vm.LoggedInUser.ID);

            vm.Projects = dashboardListBase.SchedulingDashboardList.Select(x => new SchedulingDashBoardDetails(x)).ToList();

            vm.SavedFilterList = UIHelpers.ConvertUserUISettingsJsonToDashboardString(dashboardListBase.UserUISettings, "scheduling");

            return View(vm);
        }

        public ActionResult GetAllAvailableMeetingRoomsForDateRange(DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            SchedulePreliminaryMeetingViewModel vm = new SchedulePreliminaryMeetingViewModel();
            List<MeetingRoom> rooms = new APIHelper().GetActiveMeetingRooms("PRELIM_MEETING_ROOMS");
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
            return PartialView("_MeetingRooms", rooms);
        }

        public ActionResult CheckMeetingRoomSelectedIsAvailable(int selMeetingRoomRefID, DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            var meetingroomemail = new APIHelper().GetActiveMeetingRooms("PRELIM_MEETING_ROOMS").Where(x => x.ID == selMeetingRoomRefID).FirstOrDefault();
            if (meetingroomemail == null)
                return Json(false);
            MeetingAllocationRequest data = new MeetingAllocationRequest();
            data.RequestedParticipantEmailList.Add(meetingroomemail.MeetingRoomEmail);
            data.RequestedStartTime = selDate.Date.AddHours(selStartTime.Hour).AddMinutes(selStartTime.Minute);
            data.RequestedEndTime = selDate.Date.AddHours(selEndTime.Hour).AddMinutes(selEndTime.Minute);
            try
            {
                var ret = APIHelper.CheckForOutlookMeetingAllocationAvailability(data);
                if (ret != null && ret.AllocatedParticipantList != null && ret.AllocatedParticipantList.Count > 0)
                {
                    var alc = ret.AllocatedMeetings.Where(x => x.ParticipantEmail == meetingroomemail.MeetingRoomEmail);
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
                        return Json(false);
                    }
                }
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        public ActionResult GetAllAvailableMeetingRoomsForDateRangeForExpress(DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            IExpressViewModel vm = new ExpressViewModel();

            vm = GetActiveMeetingRoomsForExpress(selDate, selStartTime, selEndTime, vm);
            List<MeetingRoom> rooms = vm.MeetingRoomList;

            return PartialView("_MeetingRooms.cshtml", rooms);
        }

        public ActionResult GetAllAvailableMeetingRoomsForDateRangeForExpressScheduling(string selDate, string selStartTime, string selEndTime)
        {
            IExpressViewModel vm = new SchedulePlanReviewViewModel();
            DateTime selectedDate = DateTime.Parse(selDate);
            DateTime startTime = DateTime.Parse(selStartTime);
            DateTime endTime = DateTime.Parse(selEndTime);

            vm = GetMeetingRoomForExpress(selectedDate, startTime, endTime, vm);

            List<MeetingRoom> rooms = vm.MeetingRoomList;

            return PartialView("_MeetingRooms.cshtml", rooms);
        }

        private IExpressViewModel GetMeetingRoomForExpress(DateTime selDate, DateTime selStartTime, DateTime selEndTime, IExpressViewModel vm)
        {
            MeetingAllocationRequest data = new MeetingAllocationRequest();
            int MeetingRoomId = -1;
            data.RequestedStartTime = selDate.Date.AddHours(selStartTime.Hour).AddMinutes(selStartTime.Minute);
            data.RequestedEndTime = selDate.Date.AddHours(selEndTime.Hour).AddMinutes(selEndTime.Minute);
            if (new APIHelper().GetExpressReservationList().Where(x => x.ReserveExpressDt == selDate.Date).FirstOrDefault() != null)
            {
                MeetingRoomId = new APIHelper().GetExpressReservationList().Where(x => x.ReserveExpressDt == selDate.Date).FirstOrDefault().MeetingRoomRefId.Value;
            }


            List<MeetingRoom> rooms = new APIHelper().GetActiveMeetingRooms().Where(x => x.MeetingRoomRefID == MeetingRoomId).ToList();
            vm.MeetingRoomList = rooms;
            return vm;

        }

        private IExpressViewModel GetActiveMeetingRoomsForExpress(DateTime selDate, DateTime selStartTime, DateTime selEndTime, IExpressViewModel vm)
        {
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
                        break;
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
                vm.MeetingRoomList.Add(rooms[0]);
            }

            return vm;
        }

        public ActionResult CheckMeetingRoomSelectedIsAvailableForExpress(int selMeetingRoomRefID, DateTime selDate, DateTime selStartTime, DateTime selEndTime)
        {
            var meetingroomemail = new APIHelper().GetActiveMeetingRooms("EXPRESS_MEETING_ROOMS").Where(x => x.ID == selMeetingRoomRefID).FirstOrDefault();
            if (meetingroomemail == null)
                return Json(false);
            MeetingAllocationRequest data = new MeetingAllocationRequest();
            data.RequestedParticipantEmailList.Add(meetingroomemail.MeetingRoomEmail);
            data.RequestedStartTime = selDate.Date.AddHours(selStartTime.Hour).AddMinutes(selStartTime.Minute);
            data.RequestedEndTime = selDate.Date.AddHours(selEndTime.Hour).AddMinutes(selEndTime.Minute);
            try
            {
                var ret = APIHelper.CheckForOutlookMeetingAllocationAvailability(data);
                if (ret != null && ret.AllocatedParticipantList != null && ret.AllocatedParticipantList.Count > 0)
                {
                    var alc = ret.AllocatedMeetings.Where(x => x.ParticipantEmail == meetingroomemail.MeetingRoomEmail);
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
                        return Json(false);
                    }
                }
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        public ActionResult MeetingsDashboard(InternalMeetingsDashboardViewModel vm)
        {
            SetUpViewModelBase<InternalMeetingsDashboardViewModel>(vm);

            if (vm.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            //if the user is a facilitator, show all the meetings
            int inputUserId = vm.LoggedInUser.ID;

            DashboardListBase dashboardListBase = DashboardAPIHelper.GetInternalMeetings(inputUserId);
            vm.MeetingList = dashboardListBase.InternalMeetings;
            vm.SavedFilterList = UIHelpers.ConvertUserUISettingsJsonToDashboardString(dashboardListBase.UserUISettings, "meeting");
            return View(vm);
        }

        public ActionResult SchedulePreliminaryMeeting(ProjectParms parms)
        {
            APIHelper apihelper = new APIHelper();

            SchedulingModel model = BuildSchedulingModel(parms);

            SchedulePreliminaryMeetingViewModel vm = PrepareSchedulePreliminaryMeetingData(parms, model);
            //if uistatusmessage enum != -1 (NA) then return to home controller with message
            if (vm.UIStatusMessage != UIStatusMessage.NA)
                return RedirectToAction("Index", "Home",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });
            PreliminaryMeetingAppointment pma = model.PreliminaryMeetingAppointment;
            if (pma.PreliminaryMeetingApptID.HasValue)
            {
                vm.PreliminaryMeetingAppointment = pma;
                vm.PreliminaryMeetingApptID = pma.PreliminaryMeetingApptID;
                vm.ScheduleDate = pma.FromDt;
                vm.StartTime = pma.FromDt;
                vm.EndTime = pma.ToDt;
                vm.ProposedDate1 = pma.ProposedDate1.HasValue ? pma.ProposedDate1.Value : (DateTime?)null;
                vm.ProposedDate2 = pma.ProposedDate2.HasValue ? pma.ProposedDate2.Value : (DateTime?)null;
                vm.ProposedDate3 = pma.ProposedDate3.HasValue ? pma.ProposedDate3.Value : (DateTime?)null;
                if (vm.PreliminaryMeetingAppointment.MeetingRoom != null)
                {
                    vm.MeetingRoomNameSelected = pma.MeetingRoom.MeetingRoomName;
                    vm.MeetingRoomRefIDSelected = pma.MeetingRoom.MeetingRoomRefID.Value;
                }
                vm.PMAUpdateDate = pma.UpdatedDate;
                if (pma.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                    || pma.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled)
                    vm.IsReschedule = true;
            }
            else
            {
                vm.PreliminaryMeetingAppointment = new PreliminaryMeetingAppointment();
                vm.ScheduleDate = (DateTime?)null;
                vm.EndTime = (DateTime?)null;
                vm.StartTime = (DateTime?)null;
            }

            if (pma.Attendees != null)
            {
                vm.SetSelectedReviewers(pma.Attendees);
                foreach (AttendeeInfo attendee in pma.Attendees)
                {
                    vm.CurrentAttendees.Add(new UserIdentity() { ID = attendee.AttendeeId });
                    vm.AttendeeIds += attendee.AttendeeId.ToString() + ",";
                }
            }

            vm.Holidays = GenerateViewModelHolidays(model.Holidays);

            //check Perms
            //AION Permissions - System Administrator, Facilitator and Management have access
            //Plan reviewer admin setting must be Y to schedule to a prelim meeting.
            bool canEdit = vm.PermissionMapping.IsManager
                || vm.PermissionMapping.IsFacilitator
                || vm.PermissionMapping.IsViewOnly
                || vm.PermissionMapping.IsSysAdmin;

            bool hasSchedulePrelim = vm.PermissionMapping.Schdul_Prlim_Mtng_Man || vm.PermissionMapping.Schdul_Prlim_Mtng_Auto;

            //Generate deeplink to accela
            vm.AccelaProjectDeeplink = GenerateAccelaDeeplink(vm.Project.AccelaProjectRefId);

            if (canEdit == false || hasSchedulePrelim == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            if (vm == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                vm = new SchedulePreliminaryMeetingViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." };
                return View(vm);
            }
            return View(vm);
        }

        /// <summary>
        /// Get Preliminary Meeting autoschedule values
        /// </summary>
        /// <param name="suggestedDate1"></param>
        /// <param name="suggestedDate2"></param>
        /// <param name="suggestedDate3"></param>
        /// <param name="wrkrId"></param>
        /// <param name="accelaProjectIdRef"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAutoSchedulePreliminaryMeetingData(AutoScheduledPrelimParams data)
        {
            AutoScheduledPrelimValues items = ScheduleAPIHelper.GetPreliminaryAutoScheduledData(data);
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SavePreliminaryMeeting(ScheduleSaveViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            int? meetingroomrefid = null;
            int returnInt = 0;
            if (vm.MeetingRoomRefIDSelected != 0) meetingroomrefid = vm.MeetingRoomRefIDSelected;
            //put together date and time
            string dt = vm.ScheduleDate.ToShortDateString();
            string dt2 = vm.ScheduleDate.ToShortDateString();
            string tm = vm.StartTime.ToShortTimeString();
            string tm2 = vm.EndTime.ToShortTimeString();

            //can set the time and date to null if they haven't chosen a date
            DateTime? s = vm.ScheduleDate == DateTime.MinValue ? (DateTime?)null : DateTime.Parse(dt + ' ' + tm);
            DateTime? e = vm.StartTime != DateTime.MinValue && vm.EndTime != DateTime.MinValue ? DateTime.Parse(dt2 + ' ' + tm2) : (DateTime?)null;

            //set the view model's start and end time after above adjustments (0 seconds for true comparison)
            if (s != null)
            {
                DateTime start = s.Value;
                vm.StartTime = new DateTime(start.Year, start.Month, start.Day) + new TimeSpan(0, start.Hour, start.Minute, 0);
                vm.ScheduleDate = vm.StartTime;
            }

            if (e != null)
            {
                DateTime end = e.Value;
                vm.EndTime = new DateTime(end.Year, end.Month, end.Day) + new TimeSpan(0, end.Hour, end.Minute, 0);
            }

            //save schedule choices
            //save attendees
            //save any changes to primary/secondary/excluded
            PreliminaryMeetingAppointment pma = new PreliminaryMeetingAppointment();

            bool sendEmail = vm.IsSubmit;
            pma.CreatedUser = vm.LoggedInUser;
            pma.UpdatedUser = vm.LoggedInUser;
            pma.MeetingRoomRefId = meetingroomrefid;
            pma.ProjectID = vm.Project.ID;
            pma.FromDt = s;
            pma.ToDt = e;
            pma.PreliminaryMeetingApptID = (vm.PreliminaryMeetingApptID == null ? 0 : vm.PreliminaryMeetingApptID);
            pma.UpdatedDate = vm.PMAUpdateDate;
            pma.IsReschedule = vm.IsReschedule;
            pma.AppendixAgendaDueDt = vm.ScheduleDate.AddDays(-1);

            if (vm.IsSubmit)
            {
                pma.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Tentatively_Scheduled;
            }
            else
            {
                pma.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled;
            }

            //get the attendees string
            //Get the PMA Attendees who were added
            vm.AttendeeIds = string.IsNullOrWhiteSpace(vm.AttendeeIds) ? "" : vm.AttendeeIds;
            string[] attendeeids = vm.AttendeeIds.Split(',');

            List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();
            foreach (string item in attendeeids)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    int userId = int.Parse(item);
                    if (userId > 0)
                        attendeeIds.Add(new AttendeeInfo { AttendeeId = userId, DeptNameEnumId = -1, BusinessRefId = -1 });

                }
            }
            //add the reviewers to attendees
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

            //add the reviewers to assignedreviewers list for save model
            List<AttendeeInfo> assignedreviewers = new List<AttendeeInfo>();
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

            pma.AssignedReviewers = assignedreviewers;
            pma.InternalNotes = vm.InternalNotes;

            pma.AssignedFacilitator = vm.AssignedFacilitator;

            pma.PrimaryReviewers = ScheduleHelpers.GetReviewerListByDept(vm, ScheduleHelpers.ReviewerType.Primary);
            pma.SecondaryReviewers = ScheduleHelpers.GetReviewerListByDept(vm, ScheduleHelpers.ReviewerType.Secondary);

            pma.ExcludedPlanReviewersBuild = vm.ExcludedPlanReviewersBuild;

            pma.ExcludedPlanReviewersElectric = vm.ExcludedPlanReviewersElectric;

            pma.ExcludedPlanReviewersMech = vm.ExcludedPlanReviewersMech;

            pma.ExcludedPlanReviewersPlumb = vm.ExcludedPlanReviewersPlumb;

            pma.ExcludedPlanReviewersZone = vm.ExcludedPlanReviewersZone;

            pma.ExcludedPlanReviewersFire = vm.ExcludedPlanReviewersFire;

            pma.ExcludedPlanReviewersBackFlow = vm.ExcludedPlanReviewersBackFlow;

            pma.ExcludedPlanReviewersFood = vm.ExcludedPlanReviewersFood;

            pma.ExcludedPlanReviewersPool = vm.ExcludedPlanReviewersPool;

            pma.ExcludedPlanReviewersLodge = vm.ExcludedPlanReviewersLodge;

            pma.ExcludedPlanReviewersDayCare = vm.ExcludedPlanReviewersDayCare;

            List<AttendeeInfo> apptAttendees = attendeeIds.Where(x => x.AttendeeId > 0).ToList();

            pma.IsSubmit = vm.IsSubmit;
            pma.NewAttendees = apptAttendees.GroupBy(x => x.AttendeeId).Select(group => group.First()).ToList();
            //LES-3809 - add project audit for auto schedule


            pma.AutoScheduled = ProjectAuditHelper.SetAutoScheduleProjectAuditForMeeting(vm);

            //save the pma and attendees
            returnInt = apihelper.UpsertPMA(pma);

            string jsonmessage = "Preliminary Meeting Appointment {0} {1}.";
            //change status message for 'submitted' vs 'saved'
            jsonmessage = String.Format(jsonmessage, vm.IsSubmit ? "submitted" : "saved", returnInt > 0 ? "Successfully" : ": an error occurred");
            TempData["StatusMessage"] = jsonmessage;
            if (vm.IsSubmit && returnInt > 0)
                return RedirectToAction("SchedulingDashboard");

            return RedirectToAction("SchedulePreliminaryMeeting",
                new
                {

                    ProjectId = vm.Project.AccelaProjectRefId,
                    StatusMessage = jsonmessage
                });
        }
        /// <summary>
        /// UI to schedule the Express Meeting
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public ActionResult ScheduleExpressMeeting(ProjectParms parms)
        {
            APIHelper apihelper = new APIHelper();

            SchedulingModel model = BuildSchedulingModel(parms);

            SchedulePlanReviewViewModel vm = PrepareExpressData(parms, model);

            if (vm.UIStatusMessage == UIStatusMessage.Project_Cycle_Missing)
                return RedirectToAction("SchedulingDashboard", "Scheduling",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });

            //if uistatusmessage enum != -1 (NA) then return to home controller with message
            if (vm.UIStatusMessage != UIStatusMessage.NA)
                return RedirectToAction("Index", "Home",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });


            //check Perms
            //AION Permissions - System Administrator, Facilitator and Management have access
            //Plan reviewer admin setting must be Y to schedule to a prelim meeting.
            bool canEdit = vm.PermissionMapping.IsManager
                || vm.PermissionMapping.IsFacilitator
                || vm.PermissionMapping.IsViewOnly
                || vm.PermissionMapping.IsSysAdmin;

            bool hasSchedulePrelim = vm.PermissionMapping.Schdul_Express_Man || vm.PermissionMapping.Schdul_Express_Auto;

            //Generate deeplink to accela
            vm.AccelaProjectDeeplink = GenerateAccelaDeeplink(vm.Project.AccelaProjectRefId);

            if (canEdit == false || hasSchedulePrelim == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            if (vm == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                vm = new SchedulePlanReviewViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." };
                return View(vm);
            }
            //if called from auto estimation details then assign auto calculated values.
            if (parms.PerformAutoEstimation == true)
            {
                AutoScheduledExpressParams data = new AutoScheduledExpressParams();
                data.AccelaProjectIDRef = parms.ProjectId;
                data.Cycle = vm.Cycle;
                data.RecIdTxt = vm.Project.RecIdTxt;

                AutoScheduledExpressValues ret = apihelper.GetAutoScheduledDataExpress(data);
                //if userid = -1, then set to 0 so UI shows "Not Selected"
                vm.ScheduledReviewerBuilding = ret.BuildingUserID == -1 ? "0" : ret.BuildingUserID.ToString();
                vm.ScheduledReviewerElectrical = ret.ElectricUserID == -1 ? "0" : ret.ElectricUserID.ToString();
                vm.ScheduledReviewerMechanical = ret.MechUserID == -1 ? "0" : ret.MechUserID.ToString();
                vm.ScheduledReviewerPlumbing = ret.PlumbUserID == -1 ? "0" : ret.PlumbUserID.ToString();
                vm.ScheduledReviewerZone = ret.ZoneUserID == -1 ? "0" : ret.ZoneUserID.ToString();
                vm.ScheduledReviewerFire = ret.FireUserID == -1 ? "0" : ret.FireUserID.ToString();
                vm.ScheduledReviewerBackFlow = ret.BackFlowUserID == -1 ? "0" : ret.BackFlowUserID.ToString();
                vm.ScheduledReviewerFood = ret.FoodServiceUserID == -1 ? "0" : ret.FoodServiceUserID.ToString();
                vm.ScheduledReviewerPool = ret.PoolUserID == -1 ? "0" : ret.PoolUserID.ToString();
                vm.ScheduledReviewerFacilities = ret.FacilityUserID == -1 ? "0" : ret.FacilityUserID.ToString();
                vm.ScheduledReviewerDayCare = ret.DayCareUserID == -1 ? "0" : ret.DayCareUserID.ToString();
                vm.ScheduleDate = ret.SelectedStartDateTime;
                vm.StartTime = ret.SelectedStartDateTime;
                vm.EndTime = ret.SelectedEndDateTime;
                vm.MeetingRoomRefIDSelected = ret.MeetingRoomId;

                string MeetingRoomNameSelected = "Select A Meeting Room";

                if (ret.MeetingRoomId > 0)
                {
                    var activeMeetingRoom = vm.MeetingRoomList.Where(x => x.MeetingRoomRefID == ret.MeetingRoomId).FirstOrDefault();
                    if (activeMeetingRoom != null)
                    {
                        MeetingRoomNameSelected = activeMeetingRoom.MeetingRoomName;
                    }
                }

                vm.MeetingRoomNameSelected = MeetingRoomNameSelected;

                vm.StatusMessage = ret.ErrorMessage;
            }

            //TODO: get the auto generated reviewers for express
            //TODO: includes reviewers and meeting room
            return View(vm);
        }

        /// <summary>
        /// Post Save Express Meeting
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveExpressMeeting(ScheduleSaveViewModel vm)
        {
            APIHelper apihelper = new APIHelper();

            PlanReview pr = new PlanReview();

            pr.ProjectScheduleRefEnum = ProjectScheduleRefEnum.EMA;
            pr.IsManualAssignment = false;

            int? meetingroomrefid = null;

            if (vm.MeetingRoomRefIDSelected != 0) meetingroomrefid = vm.MeetingRoomRefIDSelected;

            //put together date and time
            string dt = vm.ScheduleDate.ToShortDateString();
            string dt2 = vm.ScheduleDate.ToShortDateString();
            string tm = vm.StartTime.ToShortTimeString();
            string tm2 = vm.EndTime.ToShortTimeString();
            //if this is an all day appt, get the default times from the admin
            // and use those times for start and end
            DateTime startDate = DateTime.Parse(dt + ' ' + tm);
            DateTime endDate = DateTime.Parse(dt2 + ' ' + tm2);

            pr.StartDate = startDate;
            pr.EndDate = endDate;

            //should be able to use the view model for saving for this one.
            //save schedule choices
            //save attendees
            //save any changes to primary/secondary/excluded

            bool sendEmail = vm.IsSubmit;
            pr.CreatedUser = vm.LoggedInUser;
            pr.UpdatedUser = vm.LoggedInUser;
            pr.MeetingRoomRefId = meetingroomrefid;
            pr.ProjectId = vm.Project.ID;

            pr.Cycle = vm.Cycle;
            pr.UpdatedDate = vm.UpdatedDate;
            pr.IsReschedule = vm.IsReschedule;
            pr.IsFutureCycle = vm.IsFutureCycle;

            pr.AutoScheduled = ProjectAuditHelper.SetAutoScheduleByButtonStatus(vm);

            var timeSpan = endDate - startDate;
            var hours = (decimal)Math.Round(timeSpan.TotalHours, 2);

            //save hours

            pr.HoursBuilding = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerBuilding)
                && (vm.ScheduledReviewerBuilding != "-1"
                && vm.ScheduledReviewerBuilding != "0") ? vm.HoursBuilding : 0;

            pr.HoursElectic = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerElectrical)
                && (vm.ScheduledReviewerElectrical != "-1"
                && vm.ScheduledReviewerElectrical != "0") ? vm.HoursElectic : 0;

            pr.HoursMech = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerMechanical)
                && (vm.ScheduledReviewerMechanical != "-1"
                && vm.ScheduledReviewerMechanical != "0") ? vm.HoursMech : 0;

            pr.HoursPlumb = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerPlumbing)
                && (vm.ScheduledReviewerPlumbing != "-1"
                && vm.ScheduledReviewerPlumbing != "0") ? vm.HoursPlumb : 0;

            pr.HoursZoning = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerZone)
                && (vm.ScheduledReviewerZone != "-1"
                && vm.ScheduledReviewerZone != "0") ? vm.HoursZoning : 0;

            pr.HoursFire = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerFire)
                && (vm.ScheduledReviewerFire != "-1"
                && vm.ScheduledReviewerFire != "0") ? vm.HoursFire : 0;

            pr.HoursBackFlow = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerBackFlow)
                && (vm.ScheduledReviewerBackFlow != "-1"
                && vm.ScheduledReviewerBackFlow != "0") ? vm.HoursBackFlow : 0;

            pr.HoursFood = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerFood)
                && (vm.ScheduledReviewerFood != "-1"
                && vm.ScheduledReviewerFood != "0") ? vm.HoursFood : 0;

            pr.HoursPool = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerPool)
                && (vm.ScheduledReviewerPool != "-1"
                && vm.ScheduledReviewerPool != "0") ? vm.HoursPool : 0;

            pr.HoursLodge = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerFacilities)
                && (vm.ScheduledReviewerFacilities != "-1"
                && vm.ScheduledReviewerFacilities != "0") ? vm.HoursLodge : 0;

            pr.HoursDayCare = !string.IsNullOrWhiteSpace(vm.ScheduledReviewerDayCare)
                && (vm.ScheduledReviewerDayCare != "-1"
                && vm.ScheduledReviewerDayCare != "0") ? vm.HoursDayCare : 0;

            pr.BackfStartDate = pr.HoursBackFlow > 0 ? startDate : (DateTime?)null;
            pr.BackfEndDate = pr.HoursBackFlow > 0 ? endDate : (DateTime?)null;

            pr.BuildStartDate = pr.HoursBuilding > 0 ? startDate : (DateTime?)null;
            pr.BuildEndDate = pr.HoursBuilding > 0 ? endDate : (DateTime?)null;

            pr.ElectStartDate = pr.HoursElectic > 0 ? startDate : (DateTime?)null;
            pr.ElectEndDate = pr.HoursElectic > 0 ? endDate : (DateTime?)null;

            pr.MechaStartDate = pr.HoursMech > 0 ? startDate : (DateTime?)null;
            pr.MechaEndDate = pr.HoursMech > 0 ? endDate : (DateTime?)null;

            pr.PlumbStartDate = pr.HoursPlumb > 0 ? startDate : (DateTime?)null;
            pr.PlumbEndDate = pr.HoursPlumb > 0 ? endDate : (DateTime?)null;

            pr.FireStartDate = pr.HoursFire > 0 ? startDate : (DateTime?)null;
            pr.FireEndDate = pr.HoursFire > 0 ? endDate : (DateTime?)null;

            pr.ZoneStartDate = pr.HoursZoning > 0 ? startDate : (DateTime?)null;
            pr.ZoneEndDate = pr.HoursZoning > 0 ? endDate : (DateTime?)null;

            pr.FoodStartDate = pr.HoursFood > 0 ? startDate : (DateTime?)null;
            pr.FoodEndDate = pr.HoursFood > 0 ? endDate : (DateTime?)null;

            pr.PoolStartDate = pr.HoursPool > 0 ? startDate : (DateTime?)null;
            pr.PoolEndDate = pr.HoursPool > 0 ? endDate : (DateTime?)null;

            pr.FacilStartDate = pr.HoursLodge > 0 ? startDate : (DateTime?)null;
            pr.FacilEndDate = pr.HoursLodge > 0 ? endDate : (DateTime?)null;

            pr.DaycStartDate = pr.HoursDayCare > 0 ? startDate : (DateTime?)null;
            pr.DaycEndDate = pr.HoursDayCare > 0 ? endDate : (DateTime?)null;

            if (vm.IsSubmit)
            {
                pr.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Tentatively_Scheduled;
            }
            else
            {
                pr.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled;
            }

            pr.UpdateProjectStatus = true;

            //get the attendees string
            //Get the  Attendees who were added
            vm.AttendeeIds = string.IsNullOrWhiteSpace(vm.AttendeeIds) ? "" : vm.AttendeeIds;
            string[] attendeeids = vm.AttendeeIds.Split(',');

            List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();
            foreach (string item in attendeeids)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    int userId = int.Parse(item);
                    if (userId > 0)
                        attendeeIds.Add(new AttendeeInfo { AttendeeId = userId, DeptNameEnumId = -1, BusinessRefId = -1 });

                }
            }

            //add the reviewers to attendees
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

            //add the reviewers to assignedreviewers list for save model
            List<AttendeeInfo> assignedreviewers = new List<AttendeeInfo>();
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

            pr.AssignedReviewers = assignedreviewers;
            pr.InternalNotes = vm.InternalNotes;

            pr.AssignedFacilitator = vm.AssignedFacilitator;

            List<AttendeeInfo> apptAttendees = attendeeIds.Where(x => x.AttendeeId > 0).ToList();

            pr.IsSubmit = vm.IsSubmit;
            pr.NewAttendees = apptAttendees.GroupBy(x => x.AttendeeId).Select(group => group.First()).ToList();

            bool success = ScheduleAPIHelper.UpsertEMA(pr);

            string jsonmessage = "Express {0} {1}.";

            //change status message for 'submitted' vs 'saved'
            jsonmessage = String.Format(jsonmessage, vm.IsSubmit ? "submitted" : "saved", success ? "Successfully" : ": an error occurred");
            TempData["StatusMessage"] = jsonmessage;
            if (vm.IsSubmit && success)
                return RedirectToAction("SchedulingDashboard");

            return RedirectToAction("ScheduleExpressMeeting",
                new
                {

                    ProjectId = vm.Project.AccelaProjectRefId,
                    RecIdTxt = vm.Project.RecIdTxt,
                    StatusMessage = jsonmessage
                });
        }

        /// <summary>
        /// Get the data in json format for the "Manually Schedule" button on Schedule Express page
        /// </summary>
        /// <param name="selDate"></param>
        /// <param name="selStartTime"></param>
        /// <param name="selEndTime"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetManuallyScheduledExpressData(DateTime selDate, DateTime selStartTime, DateTime selEndTime, int wrkrId, string accelaProjectIdRef, string recidtxt, int cycle)
        {
            APIHelper apihelper = new APIHelper();
            //get the dates and times
            //put together date and time
            string dt = selDate.ToShortDateString();
            string tm = selStartTime.ToShortTimeString();
            string tm2 = selEndTime.ToShortTimeString();
            DateTime startDateTime = DateTime.Parse(dt + ' ' + tm);
            DateTime endDateTime = DateTime.Parse(dt + ' ' + tm2);
            AutoScheduledExpressParams item = new AutoScheduledExpressParams
            {
                ManualStartDateTime = startDateTime,
                ManualEndDateTime = endDateTime,
                AccelaProjectIDRef = accelaProjectIdRef,
                RecIdTxt = recidtxt,
                Cycle = cycle
            };
            AutoScheduledExpressUIValues items = apihelper.GetManuallyScheduledExpressData(item);
            Object jsonobj = new
            {
                scheduledReviewerBuilding = new { reviewername = items.BuildingUserName, reviewerid = items.BuildingUserID },
                scheduledReviewerElectrical = new { reviewername = items.ElectricUserName, reviewerid = items.ElectricUserID },
                scheduledReviewerMechanical = new { reviewername = items.MechUserName, reviewerid = items.MechUserID },
                scheduledReviewerPlumbing = new { reviewername = items.PlumbUserName, reviewerid = items.PlumbUserID },
                scheduledReviewerZone = new { reviewername = items.ZoneUserName, reviewerid = items.ZoneUserID },
                scheduledReviewerFire = new { reviewername = items.FireUserName, reviewerid = items.FireUserID },
                scheduledReviewerBackflow = new { reviewername = items.BackFlowUserName, reviewerid = items.BackFlowUserID },
                scheduledReviewerFood = new { reviewername = items.FoodServiceUserName, reviewerid = items.FoodServiceUserID },
                scheduledReviewerPool = new { reviewername = items.PoolUserName, reviewerid = items.PoolUserID },
                scheduledReviewerFacilities = new { reviewername = items.FacilityUserName, reviewerid = items.FacilityUserID },
                scheduledReviewerDayCare = new { reviewername = items.DayCareUserName, reviewerid = items.DayCareUserID },
                meetingRoomRefID = items.MeetingRoomId,
                meetingRoomName = items.MeetingRoomName,
                errorMessage = items.ErrorMessage
            };

            return Json(jsonobj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchedulePlanReview(ProjectParms parms)
        {
            APIHelper apihelper = new APIHelper();

            SchedulingModel model = BuildSchedulingModel(parms);

            SchedulePlanReviewViewModel vm = PreparePlanReviewEstimationData(parms, model);

            if (vm.UIStatusMessage == UIStatusMessage.Project_Cycle_Missing)
                return RedirectToAction("SchedulingDashboard", "Scheduling",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });

            if (vm == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                vm = new SchedulePlanReviewViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." };
                return View(vm);
            }

            bool isFifo = vm.Project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes ||
                         vm.Project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans ||
                         vm.Project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home ||
                         vm.Project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial;

            if (isFifo)
            {
                //redirect to fifo page
                return RedirectToAction("ScheduleFIFOPlanReview", "Scheduling",
                                            new
                                            {
                                                ProjectId = parms.ProjectId
                                            });

            }

            //if uistatusmessage enum != -1 (NA) then return to home controller with message
            if (vm.UIStatusMessage != UIStatusMessage.NA)
                return RedirectToAction("Index", "Home",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });


            //check Perms
            //AION Permissions - System Administrator, Facilitator and Management have access
            //Plan reviewer admin setting must be Y to schedule to a prelim meeting.
            bool canEdit = vm.PermissionMapping.IsManager
                || vm.PermissionMapping.IsFacilitator
                || vm.PermissionMapping.IsViewOnly
                || vm.PermissionMapping.IsSysAdmin;

            bool hasAccess = vm.PermissionMapping.Access_SchedulePR;

            if (vm.PermissionMapping.Access_SchedulePR == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            //if called from auto estimation details then assign auto calculated values.
            if (parms.PerformAutoEstimation == true)
            {
                AutoScheduledPlanReviewParams data = new AutoScheduledPlanReviewParams();
                data.AccelaProjectIDRef = parms.ProjectId;
                data.ProjectID = vm.Project.ID;
                data.BuildingIsPool = parms.BuildingIsPool;
                data.ElectricIsPool = parms.ElectricIsPool;
                data.MechIsPool = parms.MechIsPool;
                data.PlumbIsPool = parms.PlumbIsPool;
                data.ZoneIsPool = parms.ZoneIsPool;
                data.FireIsPool = parms.FireIsPool;
                data.FoodServiceIsPool = parms.FoodServiceIsPool;
                data.PoolIsPool = parms.PoolIsPool;
                data.FacilityIsPool = parms.FacilityIsPool;
                data.DayCareIsPool = parms.DayCareIsPool;
                data.BackFlowIsPool = parms.BackFlowIsPool;

                data.BuildingUserID = parms.BuildingUserID;
                data.ElectricUserID = parms.ElectricUserID;
                data.MechUserID = parms.MechUserID;
                data.PlumbUserID = parms.PlumbUserID;
                data.ZoneUserID = parms.ZoneUserID;
                data.FireUserID = parms.FireUserID;
                data.FoodServiceUserID = parms.FoodServiceUserID;
                data.PoolUserID = parms.PoolUserID;
                data.FacilityUserID = parms.FacilityUserID;
                data.DayCareUserID = parms.DayCareUserID;
                data.BackFlowUserID = parms.BackFlowUserID;

                data.Cycle = vm.Cycle;
                data.IsAdjustHours = parms.IsAdjustHours;
                data.IsCycleComparison = vm.IsCycleComparison;
                data.PlansReadyOnDate = vm.PlansReadyOnDate;
                //if page was loaded as Auto Schedule + Future Cycle was toggled, this is an auto scheduled future cycle
                if (parms.IsFutureCycle)
                {
                    data.IsFutureCycle = true;
                    data.ScheduleAfterDate = vm.ScheduleAfterDate.Value;
                    data.UpdatedBuildingHours = vm.ProposedBuilding.HasValue ? vm.ProposedBuilding.Value : 0;
                    data.UpdatedElectricHours = vm.ProposedElectric.HasValue ? vm.ProposedElectric.Value : 0;
                    data.UpdatedMechHours = vm.ProposedMech.HasValue ? vm.ProposedMech.Value : 0;
                    data.UpdatedPlumbHours = vm.ProposedPlumb.HasValue ? vm.ProposedPlumb.Value : 0;
                    data.UpdatedDayCareHours = vm.ProposedDayCare.HasValue ? vm.ProposedDayCare.Value : 0;
                    data.UpdatedFoodHours = vm.ProposedFood.HasValue ? vm.ProposedFood.Value : 0;
                    data.UpdatedPoolHours = vm.ProposedPool.HasValue ? vm.ProposedPool.Value : 0;
                    data.UpdatedLodgeHours = vm.ProposedLodge.HasValue ? vm.ProposedLodge.Value : 0;
                    data.UpdatedBackflowHours = vm.ProposedBackFlow.HasValue ? vm.ProposedBackFlow.Value : 0;
                    data.UpdatedZoneHours = vm.ProposedZoning.HasValue ? vm.ProposedZoning.Value : 0;
                    data.UpdatedFireHours = vm.ProposedFire.HasValue ? vm.ProposedFire.Value : 0;
                }
                data.RecIdTxt = vm.Project.RecIdTxt;

                AutoScheduledPlanReviewValues ret = ScheduleAPIHelper.GetAutoScheduledDataPlanReview(data);
                //if userid = -1, then set to 0 so UI shows "Not Selected"
                vm.ScheduledReviewerBuilding = ret.BuildingUserID.ToString();// == -1 ? "0" : ret.BuildingUserID.ToString();
                vm.ScheduledReviewerElectrical = ret.ElectricUserID.ToString();// == -1 ? "0" : ret.ElectricUserID.ToString();
                vm.ScheduledReviewerMechanical = ret.MechUserID.ToString();// == -1;// ? "0" : ret.MechUserID.ToString();
                vm.ScheduledReviewerPlumbing = ret.PlumbUserID.ToString();// == -1 ? "0" : ret.PlumbUserID.ToString();
                vm.ScheduledReviewerZone = ret.ZoneUserID.ToString();// == -1 ? "0" : ret.ZoneUserID.ToString();
                vm.ScheduledReviewerFire = ret.FireUserID.ToString();// == -1 ? "0" : ret.FireUserID.ToString();
                vm.ScheduledReviewerBackFlow = ret.BackFlowUserID.ToString();// == -1 ? "0" : ret.BackFlowUserID.ToString();
                vm.ScheduledReviewerFood = ret.FoodServiceUserID.ToString();// == -1 ? "0" : ret.FoodServiceUserID.ToString();
                vm.ScheduledReviewerPool = ret.PoolUserID.ToString();// == -1 ? "0" : ret.PoolUserID.ToString();
                vm.ScheduledReviewerFacilities = ret.FacilityUserID.ToString();// == -1 ? "0" : ret.FacilityUserID.ToString();
                vm.ScheduledReviewerDayCare = ret.DayCareUserID.ToString();// == -1 ? "0" : ret.DayCareUserID.ToString();
                vm.BuildPool = parms.BuildingIsPool;
                vm.ElectPool = parms.ElectricIsPool;
                vm.MechaPool = parms.MechIsPool;
                vm.PlumbPool = parms.PlumbIsPool;
                vm.ZonePool = parms.ZoneIsPool;
                vm.FirePool = parms.FireIsPool;
                vm.FoodPool = parms.FoodServiceIsPool;
                vm.PoolPool = parms.PoolIsPool;
                vm.FacilPool = parms.FacilityIsPool;
                vm.DaycPool = parms.DayCareIsPool;
                vm.BackfPool = parms.BackFlowIsPool;

                if (vm.ScheduledReviewerBuilding != "0" && vm.ScheduledReviewerBuilding != "-1")
                {
                    vm.BuildStartDate = ret.BuildingScheduleStart;
                    vm.BuildEndDate = ret.BuildingScheduleEnd;
                }
                else
                {
                    vm.BuildStartDate = null;
                    vm.BuildEndDate = null;
                }
                if (vm.ScheduledReviewerElectrical != "0" && vm.ScheduledReviewerElectrical != "-1")
                {
                    vm.ElectStartDate = ret.ElectricScheduleStart;
                    vm.ElectEndDate = ret.ElectricScheduleEnd;
                }
                else
                {
                    vm.ElectStartDate = null;
                    vm.ElectEndDate = null;
                }
                if (vm.ScheduledReviewerMechanical != "0" && vm.ScheduledReviewerMechanical != "-1")
                {
                    vm.MechaStartDate = ret.MechScheduleStart;
                    vm.MechaEndDate = ret.MechScheduleEnd;
                }
                else
                {
                    vm.MechaStartDate = null;
                    vm.MechaEndDate = null;
                }
                if (vm.ScheduledReviewerPlumbing != "0" && vm.ScheduledReviewerPlumbing != "-1")
                {
                    vm.PlumbStartDate = ret.PlumbScheduleStart;
                    vm.PlumbEndDate = ret.PlumbScheduleEnd;
                }
                else
                {
                    vm.PlumbStartDate = null;
                    vm.PlumbEndDate = null;
                }
                if (vm.ScheduledReviewerZone != "0" && vm.ScheduledReviewerZone != "-1")
                {
                    vm.ZoneStartDate = ret.ZoneScheduleStart;
                    vm.ZoneEndDate = ret.ZoneScheduleEnd;
                }
                else
                {
                    vm.ZoneStartDate = null;
                    vm.ZoneEndDate = null;
                }
                if (vm.ScheduledReviewerFire != "0" && vm.ScheduledReviewerFire != "-1")
                {
                    vm.FireStartDate = ret.FireScheduleStart;
                    vm.FireEndDate = ret.FireScheduleEnd;
                }
                else
                {
                    vm.FireStartDate = null;
                    vm.FireEndDate = null;
                }
                if (vm.ScheduledReviewerPool != "0" && vm.ScheduledReviewerPool != "-1")
                {
                    vm.PoolStartDate = ret.PoolScheduleStart;
                    vm.PoolEndDate = ret.PoolScheduleEnd;
                }
                else
                {
                    vm.PoolStartDate = null;
                    vm.PoolEndDate = null;
                }
                if (vm.ScheduledReviewerFacilities != "0" && vm.ScheduledReviewerFacilities != "-1")
                {
                    vm.FacilStartDate = ret.FacilityScheduleStart;
                    vm.FacilEndDate = ret.FacilityScheduleEnd;
                }
                else
                {
                    vm.FacilStartDate = null;
                    vm.FacilEndDate = null;
                }
                if (vm.ScheduledReviewerDayCare != "0" && vm.ScheduledReviewerDayCare != "-1")
                {
                    vm.DaycStartDate = ret.DayCareScheduleStart;
                    vm.DaycEndDate = ret.DayCareScheduleEnd;
                }
                else
                {
                    vm.DaycStartDate = null;
                    vm.DaycEndDate = null;
                }
                if (vm.ScheduledReviewerBackFlow != "0" && vm.ScheduledReviewerBackFlow != "-1")
                {
                    vm.BackfStartDate = ret.BackFlowScheduleStart;
                    vm.BackfEndDate = ret.BackFlowScheduleEnd;
                }
                else
                {
                    vm.BackfStartDate = null;
                    vm.BackfEndDate = null;
                }
                if (vm.ScheduledReviewerFood != "0" && vm.ScheduledReviewerFood != "-1")
                {
                    vm.FoodStartDate = ret.FoodScheduleStart;
                    vm.FoodEndDate = ret.FoodScheduleEnd;
                }
                else
                {
                    vm.FoodStartDate = null;
                    vm.FoodEndDate = null;
                }
            }
            //Only do this if not auto schedule
            if (parms.PerformAutoEstimation == false)
            {
                //LES-379 if this is county fire shop drawings, automatically set fire agency to pool
                if (vm.Project.AionPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                && vm.Project.Agencies.Where(x => x.DepartmentDivision == DepartmentDivisionEnum.Fire
                    && x.DepartmentInfo != DepartmentNameEnums.Fire_Cty_Chrlt).Any())
                {
                    List<Reviewer> o = model.FireAgencyReviewers;
                    List<Reviewer> r = model.Reviewers;
                    Reviewer countyfire = new Reviewer();
                    List<Reviewer> q = o
                        .Join(r,
                        county => county.ID,
                        reviewer => reviewer.ID,
                        (county, reviewer) => county)
                        .ToList();
                    if (q != null && q.Count() > 0)
                        countyfire = q.FirstOrDefault();
                    //set the agency fire to countyfire
                    foreach (ProjectAgency agency in vm.Project.Agencies)
                    {
                        if (agency.DepartmentDivision == DepartmentDivisionEnum.Fire)
                        {
                            agency.AssignedPlanReviewer = countyfire;
                        }
                    }
                    vm.FirePool = true;
                }
            }

            vm.AccelaLink = ConfigurationManager.AppSettings["AccelaBaseLink"].ToString();
            //Generate deeplink to accela
            vm.AccelaProjectDeeplink = GenerateAccelaDeeplink(vm.Project.AccelaProjectRefId);

            //Get multipliers set in Admin menu
            List<CatalogItem> catalogs = model.CatalogItems;
            if (catalogs.Any())
            {
                vm.SchedulingMultiplierName = catalogs.FirstOrDefault(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.NAME").Value;
                vm.SchedulingMultiplierFactor = catalogs.FirstOrDefault(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR").Value;
                vm.SchedulingMultiplierProjectTypes = catalogs.FirstOrDefault(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE").Value.Split(',');
            }

            return View(vm);
        }

        /// <summary>
        /// Performs auto Schedule for Plan Review (SchedulePlanReview)
        /// </summary>
        /// <param name="data">AutoScheduledPlanReviewParams</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAutoSchedulePlanReviewData(AutoScheduledPlanReviewParams data)
        {

            AutoScheduledPlanReviewViewModel ret = ScheduleAPIHelper.GetAutoScheduledDataPlanReview(data);

            return Json(ret, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SavePlanReview(ScheduleSaveViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            PlanReview pr = new PlanReview();

            pr.ProjectScheduleRefEnum = ProjectScheduleRefEnum.PR;
            pr.IsManualAssignment = false; // TODO: needs to be determined

            if (vm.IsActivateNAReview)
            {
                pr = SchedulePlanReviewHelper.GetActivateNAPlanReviewForSave(vm);
            }
            else
            {
                //LES-3809 - add project audit for auto schedule
                pr.AutoScheduled = ProjectAuditHelper.SetAutoScheduleProjectAuditForPlanReview(vm);

                //jcl 8-10-21 LES-3463 update project status if not IsActivateNAReview
                pr.UpdateProjectStatus = true;
                pr.SendEmail = true;
                pr.ResetApproval = true;

                if (vm.IsFutureCycle) vm.IsRescheduleOverwrite = false;
                bool newId = (vm.IsNewCycle || vm.IsFutureCycle || vm.IsReschedule) && !vm.IsRescheduleOverwrite;
                //ids for updates
                pr.BackfPlanReviewScheduleId = newId ? null : vm.BackfPlanReviewScheduleId;
                pr.BuildPlanReviewScheduleId = newId ? null : vm.BuildPlanReviewScheduleId;
                pr.ElectPlanReviewScheduleId = newId ? null : vm.ElectPlanReviewScheduleId;
                pr.MechaPlanReviewScheduleId = newId ? null : vm.MechaPlanReviewScheduleId;
                pr.PlumbPlanReviewScheduleId = newId ? null : vm.PlumbPlanReviewScheduleId;
                pr.FirePlanReviewScheduleId = newId ? null : vm.FirePlanReviewScheduleId;
                pr.ZonePlanReviewScheduleId = newId ? null : vm.ZonePlanReviewScheduleId;
                pr.PoolPlanReviewScheduleId = newId ? null : vm.PoolPlanReviewScheduleId;
                pr.FoodPlanReviewScheduleId = newId ? null : vm.FoodPlanReviewScheduleId;
                pr.FacilPlanReviewScheduleId = newId ? null : vm.FacilPlanReviewScheduleId;
                pr.DaycPlanReviewScheduleId = newId ? null : vm.DaycPlanReviewScheduleId;

                //update dates
                pr.BackfPRSUpdateDate = vm.BackfPRSUpdateDate;
                pr.BuildPRSUpdateDate = vm.BuildPRSUpdateDate;
                pr.ElectPRSUpdateDate = vm.ElectPRSUpdateDate;
                pr.MechaPRSUpdateDate = vm.MechaPRSUpdateDate;
                pr.PlumbPRSUpdateDate = vm.PlumbPRSUpdateDate;
                pr.FirePRSUpdateDate = vm.FirePRSUpdateDate;
                pr.ZonePRSUpdateDate = vm.ZonePRSUpdateDate;
                pr.PoolPRSUpdateDate = vm.PoolPRSUpdateDate;
                pr.FoodPRSUpdateDate = vm.FoodPRSUpdateDate;
                pr.FacilPRSUpdateDate = vm.FacilPRSUpdateDate;
                pr.DaycPRSUpdateDate = vm.DaycPRSUpdateDate;

                //save hours
                if (vm.IsCycleComparison)
                {
                    if (!vm.IsAdjustHours)
                    {
                        pr.HoursBuilding = vm.ProposedBuilding.HasValue ? vm.ProposedBuilding.Value : 0;
                        pr.HoursElectic = vm.ProposedElectric.HasValue ? vm.ProposedElectric.Value : 0;
                        pr.HoursMech = vm.ProposedMech.HasValue ? vm.ProposedMech.Value : 0;
                        pr.HoursPlumb = vm.ProposedPlumb.HasValue ? vm.ProposedPlumb.Value : 0;
                        pr.HoursZoning = vm.ProposedZoning.HasValue ? vm.ProposedZoning.Value : 0;
                        pr.HoursFire = vm.ProposedFire.HasValue ? vm.ProposedFire.Value : 0;
                        pr.HoursBackFlow = vm.ProposedBackFlow.HasValue ? vm.ProposedBackFlow.Value : 0;
                        pr.HoursFood = vm.ProposedFood.HasValue ? vm.ProposedFood.Value : 0;
                        pr.HoursPool = vm.ProposedPool.HasValue ? vm.ProposedPool.Value : 0;
                        pr.HoursLodge = vm.ProposedLodge.HasValue ? vm.ProposedLodge.Value : 0;
                        pr.HoursDayCare = vm.ProposedDayCare.HasValue ? vm.ProposedDayCare.Value : 0;
                    }
                    else
                    {
                        pr.HoursBuilding = vm.ReReviewBuilding.HasValue ? vm.ReReviewBuilding.Value : 0;
                        pr.HoursElectic = vm.ReReviewElectric.HasValue ? vm.ReReviewElectric.Value : 0;
                        pr.HoursMech = vm.ReReviewMech.HasValue ? vm.ReReviewMech.Value : 0;
                        pr.HoursPlumb = vm.ReReviewPlumb.HasValue ? vm.ReReviewPlumb.Value : 0;
                        pr.HoursZoning = vm.ReReviewZoning.HasValue ? vm.ReReviewZoning.Value : 0;
                        pr.HoursFire = vm.ReReviewFire.HasValue ? vm.ReReviewFire.Value : 0;
                        pr.HoursBackFlow = vm.ReReviewBackFlow.HasValue ? vm.ReReviewBackFlow.Value : 0;
                        pr.HoursFood = vm.ReReviewFood.HasValue ? vm.ReReviewFood.Value : 0;
                        pr.HoursPool = vm.ReReviewPool.HasValue ? vm.ReReviewPool.Value : 0;
                        pr.HoursLodge = vm.ReReviewLodge.HasValue ? vm.ReReviewLodge.Value : 0;
                        pr.HoursDayCare = vm.ReReviewDayCare.HasValue ? vm.ReReviewDayCare.Value : 0;
                    }
                    //if cycle comparison was submitted, lock in whichever column the user selected as the rereview hours
                    if (vm.IsSubmit)
                    {
                        //apihelper.AdjustScheduleBusinessRelationship(vm.Project.ID, vm.IsAdjustHours);
                    }
                }
                else if (vm.IsFutureCycle)
                {
                    pr.HoursBuilding = vm.ProposedBuilding.HasValue ? vm.ProposedBuilding.Value : 0;
                    pr.HoursElectic = vm.ProposedElectric.HasValue ? vm.ProposedElectric.Value : 0;
                    pr.HoursMech = vm.ProposedMech.HasValue ? vm.ProposedMech.Value : 0;
                    pr.HoursPlumb = vm.ProposedPlumb.HasValue ? vm.ProposedPlumb.Value : 0;
                    pr.HoursZoning = vm.ProposedZoning.HasValue ? vm.ProposedZoning.Value : 0;
                    pr.HoursFire = vm.ProposedFire.HasValue ? vm.ProposedFire.Value : 0;
                    pr.HoursBackFlow = vm.ProposedBackFlow.HasValue ? vm.ProposedBackFlow.Value : 0;
                    pr.HoursFood = vm.ProposedFood.HasValue ? vm.ProposedFood.Value : 0;
                    pr.HoursPool = vm.ProposedPool.HasValue ? vm.ProposedPool.Value : 0;
                    pr.HoursLodge = vm.ProposedLodge.HasValue ? vm.ProposedLodge.Value : 0;
                    pr.HoursDayCare = vm.ProposedDayCare.HasValue ? vm.ProposedDayCare.Value : 0;
                }
                else if (vm.Cycle > 1)
                {
                    pr.HoursBuilding = vm.ReReviewBuilding.HasValue ? vm.ReReviewBuilding.Value : 0;
                    pr.HoursElectic = vm.ReReviewElectric.HasValue ? vm.ReReviewElectric.Value : 0;
                    pr.HoursMech = vm.ReReviewMech.HasValue ? vm.ReReviewMech.Value : 0;
                    pr.HoursPlumb = vm.ReReviewPlumb.HasValue ? vm.ReReviewPlumb.Value : 0;
                    pr.HoursZoning = vm.ReReviewZoning.HasValue ? vm.ReReviewZoning.Value : 0;
                    pr.HoursFire = vm.ReReviewFire.HasValue ? vm.ReReviewFire.Value : 0;
                    pr.HoursBackFlow = vm.ReReviewBackFlow.HasValue ? vm.ReReviewBackFlow.Value : 0;
                    pr.HoursFood = vm.ReReviewFood.HasValue ? vm.ReReviewFood.Value : 0;
                    pr.HoursPool = vm.ReReviewPool.HasValue ? vm.ReReviewPool.Value : 0;
                    pr.HoursLodge = vm.ReReviewLodge.HasValue ? vm.ReReviewLodge.Value : 0;
                    pr.HoursDayCare = vm.ReReviewDayCare.HasValue ? vm.ReReviewDayCare.Value : 0;
                }
                else
                {
                    pr.HoursBuilding = vm.HoursBuilding;
                    pr.HoursElectic = vm.HoursElectic;
                    pr.HoursMech = vm.HoursMech;
                    pr.HoursPlumb = vm.HoursPlumb;
                    pr.HoursZoning = vm.HoursZoning;
                    pr.HoursFire = vm.HoursFire;
                    pr.HoursBackFlow = vm.HoursBackFlow;
                    pr.HoursFood = vm.HoursFood;
                    pr.HoursPool = vm.HoursPool;
                    pr.HoursLodge = vm.HoursLodge;
                    pr.HoursDayCare = vm.HoursDayCare;
                }

                //dates
                //if pool or fifo, dates should be null/blank
                pr.BackfStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BackfFifo, vm.BackfPool, vm.BackfStartDate);
                pr.BackfEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BackfFifo, vm.BackfPool, vm.BackfEndDate);

                pr.BuildStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BuildFifo, vm.BuildPool, vm.BuildStartDate);
                pr.BuildEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BuildFifo, vm.BuildPool, vm.BuildEndDate);
                pr.ElectStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ElectFifo, vm.ElectPool, vm.ElectStartDate);
                pr.ElectEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ElectFifo, vm.ElectPool, vm.ElectEndDate);
                pr.MechaStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.MechaFifo, vm.MechaPool, vm.MechaStartDate);
                pr.MechaEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.MechaFifo, vm.MechaPool, vm.MechaEndDate);
                pr.PlumbStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PlumbFifo, vm.PlumbPool, vm.PlumbStartDate);
                pr.PlumbEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PlumbFifo, vm.PlumbPool, vm.PlumbEndDate);

                pr.FireStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FireFifo, vm.FirePool, vm.FireStartDate);
                pr.FireEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FireFifo, vm.FirePool, vm.FireEndDate);

                pr.ZoneStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ZoneFifo, vm.ZonePool, vm.ZoneStartDate);
                pr.ZoneEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ZoneFifo, vm.ZonePool, vm.ZoneEndDate);

                pr.FoodStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FoodFifo, vm.FoodPool, vm.FoodStartDate);
                pr.FoodEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FoodFifo, vm.FoodPool, vm.FoodEndDate);
                pr.PoolStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PoolFifo, vm.PoolPool, vm.PoolStartDate);
                pr.PoolEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PoolFifo, vm.PoolPool, vm.PoolEndDate);
                pr.FacilStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FacilFifo, vm.FacilPool, vm.FacilStartDate);
                pr.FacilEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FacilFifo, vm.FacilPool, vm.FacilEndDate);
                pr.DaycStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.DaycFifo, vm.DaycPool, vm.DaycStartDate);
                pr.DaycEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.DaycFifo, vm.DaycPool, vm.DaycEndDate);

                //pool
                //fifo
                pr.BackfPool = vm.BackfPool;
                pr.BackfFifo = vm.BackfFifo;

                pr.BuildPool = vm.BuildPool;
                pr.BuildFifo = vm.BuildFifo;
                pr.ElectPool = vm.ElectPool;
                pr.ElectFifo = vm.ElectFifo;
                pr.MechaPool = vm.MechaPool;
                pr.MechaFifo = vm.MechaFifo;
                pr.PlumbPool = vm.PlumbPool;
                pr.PlumbFifo = vm.PlumbFifo;

                pr.FirePool = vm.FirePool;
                pr.FireFifo = vm.FireFifo;

                pr.ZonePool = vm.ZonePool;
                pr.ZoneFifo = vm.ZoneFifo;

                pr.FoodPool = vm.FoodPool;
                pr.FoodFifo = vm.FoodFifo;
                pr.PoolPool = vm.PoolPool;
                pr.PoolFifo = vm.PoolFifo;
                pr.FacilPool = vm.FacilPool;
                pr.FacilFifo = vm.FacilFifo;
                pr.DaycPool = vm.DaycPool;
                pr.DaycFifo = vm.DaycFifo;

                //pr.AllPool = SchedulePlanReviewHelper. // Create one to determine all pool


                //save schedule choices
                //save attendees
                //save any changes to primary/secondary/excluded
                bool sendEmail = vm.IsSubmit;
                pr.CreatedUser = vm.LoggedInUser;
                pr.UpdatedUser = vm.LoggedInUser;
                pr.ProjectId = vm.Project.ID;
                //pr.PlanReviewId = (vm.PlanReviewId == null ? 0 : vm.PlanReviewId);
                pr.UpdatedDate = vm.PMAUpdateDate;

                pr.PlanReviewScheduleId = vm.PlanReviewScheduleId;

                if (vm.IsSubmit)
                {
                    pr.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Tentatively_Scheduled;
                }
                else
                {
                    pr.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled;
                }

                //get the attendees string
                //Get the Attendees who were added
                vm.AttendeeIds = string.IsNullOrWhiteSpace(vm.AttendeeIds) ? "" : vm.AttendeeIds;
                string[] attendeeids = vm.AttendeeIds.Split(',');

                List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();
                foreach (string item in attendeeids)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        int userId = int.Parse(item);
                        if (userId > 0)
                            attendeeIds.Add(new AttendeeInfo { AttendeeId = userId, DeptNameEnumId = -1, BusinessRefId = -1 });

                    }
                }

                // set the PR agency assigned IDs
                pr.BuildAssignedReviewerId = int.Parse(vm.ScheduledReviewerBuilding);
                pr.BackFlowAssignedReviewerId = int.Parse(vm.ScheduledReviewerBackFlow);
                pr.DayCareAssignedReviewerId = int.Parse(vm.ScheduledReviewerDayCare);
                pr.ElectricAssignedReviewerId = int.Parse(vm.ScheduledReviewerElectrical);
                pr.FacilityAssignedReviewerId = int.Parse(vm.ScheduledReviewerFacilities);
                pr.FireAssignedReviewerId = int.Parse(vm.ScheduledReviewerFire);
                pr.FoodAssignedReviewerId = int.Parse(vm.ScheduledReviewerFood);
                pr.MechAssignedReviewerId = int.Parse(vm.ScheduledReviewerMechanical);
                pr.PlumbAssignedReviewerId = int.Parse(vm.ScheduledReviewerPlumbing);
                pr.PoolAssignedReviewerId = int.Parse(vm.ScheduledReviewerPool);
                pr.ZoningAssigedReviewerId = int.Parse(vm.ScheduledReviewerZone);

                //add the reviewers to attendees
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
                attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

                //add the reviewers to assignedreviewers list for save model
                List<AttendeeInfo> assignedreviewers = new List<AttendeeInfo>();
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
                assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

                pr.AssignedReviewers = assignedreviewers;
                pr.InternalNotes = vm.InternalNotes;

                pr.AssignedFacilitator = vm.AssignedFacilitator;

                pr.PrimaryReviewers = ScheduleHelpers.GetReviewerListByDept(vm, ScheduleHelpers.ReviewerType.Primary);
                pr.SecondaryReviewers = ScheduleHelpers.GetReviewerListByDept(vm, ScheduleHelpers.ReviewerType.Secondary);

                pr.ExcludedPlanReviewersBuild = vm.ExcludedPlanReviewersBuild;

                pr.ExcludedPlanReviewersElectric = vm.ExcludedPlanReviewersElectric;

                pr.ExcludedPlanReviewersMech = vm.ExcludedPlanReviewersMech;

                pr.ExcludedPlanReviewersPlumb = vm.ExcludedPlanReviewersPlumb;

                pr.ExcludedPlanReviewersZone = vm.ExcludedPlanReviewersZone;

                pr.ExcludedPlanReviewersFire = vm.ExcludedPlanReviewersFire;

                pr.ExcludedPlanReviewersBackFlow = vm.ExcludedPlanReviewersBackFlow;

                pr.ExcludedPlanReviewersFood = vm.ExcludedPlanReviewersFood;

                pr.ExcludedPlanReviewersPool = vm.ExcludedPlanReviewersPool;

                pr.ExcludedPlanReviewersLodge = vm.ExcludedPlanReviewersLodge;

                pr.ExcludedPlanReviewersDayCare = vm.ExcludedPlanReviewersDayCare;

                List<AttendeeInfo> apptAttendees = attendeeIds.Where(x => x.AttendeeId > 0).ToList();

                pr.IsSubmit = vm.IsSubmit;
                pr.IsReschedule = vm.IsReschedule;
                pr.NewAttendees = apptAttendees.GroupBy(x => x.AttendeeId).Select(group => group.First()).ToList();

                //get the notes
                pr.InternalNotes = vm.InternalNotes;
                pr.AddSchedulingNotes = vm.AddSchedulingNotes;
                pr.MandatorySchedulingNotes = vm.MandatorySchedulingNotes;
                pr.SchedulingNotes = vm.SchedulingNotes;
                pr.SchedulingStandardNotes = vm.SchedulingStandardNotes;

                //save as current cycle
                pr.Cycle = vm.Cycle;
                pr.ProjectCycleId = vm.ProjectCycleId;

                if (vm.PlansReadyOnDate.HasValue) pr.ProdDate = vm.PlansReadyOnDate;

                pr.GateDate = vm.GateDate;

                pr.IsFutureCycle = vm.IsFutureCycle;

                //if this is a future cycle, process auto generated values
                if (vm.IsFutureCycle)
                {
                    pr.IsReschedule = false;
                    pr.ProposedBuilding = vm.ProposedBuilding;
                    pr.ProposedElectric = vm.ProposedElectric;
                    pr.ProposedMech = vm.ProposedMech;
                    pr.ProposedPlumb = vm.ProposedPlumb;
                    pr.ProposedDayCare = vm.ProposedDayCare;
                    pr.ProposedFood = vm.ProposedFood;
                    pr.ProposedPool = vm.ProposedPool;
                    pr.ProposedLodge = vm.ProposedLodge;
                    pr.ProposedBackFlow = vm.ProposedBackFlow;
                    pr.ProposedZoning = vm.ProposedZoning;
                    pr.ProposedFire = vm.ProposedFire;
                    pr.ScheduleAfterDate = vm.ScheduleAfterDate;
                    pr.ProdDate = vm.ScheduleAfterDate;
                }
            }

            //save the pr and attendees
            bool returnInt = apihelper.UpsertPlanReview(pr);

            //if the facilitator was changed, save the new facilitator
            if (vm.PreviousAssignedFacilitator.ToString().Equals(vm.AssignedFacilitator) == false)
            {
                //update the facilitator
                ProjectEstimation project = new ProjectEstimation
                {
                    ID = vm.Project.ID,
                    UpdatedDate = vm.Project.UpdatedDate,
                    UpdatedUser = vm.LoggedInUser,
                    AssignedFacilitator = Convert.ToInt32(vm.AssignedFacilitator)
                };

                bool saveFacilitatorSuccess = ScheduleAPIHelper.UpdateAssignedFacilitator(project);
            }

            string jsonmessage = "Plan Review {0} {1}.";
            //change status message for 'submitted' vs 'saved'
            jsonmessage = String.Format(jsonmessage, vm.IsSubmit ? "submitted" : "saved", returnInt ? "Successfully" : ": an error occurred");
            TempData["StatusMessage"] = jsonmessage;
            if (vm.IsSubmit && returnInt)
                return RedirectToAction("SchedulingDashboard");

            return RedirectToAction("SchedulePlanReview",
                new
                {

                    ProjectId = vm.Project.AccelaProjectRefId,
                    StatusMessage = jsonmessage
                });
        }

        public ActionResult ScheduleMeeting(ProjectParms parms)
        {
            APIHelper apihelper = new APIHelper();

            SchedulingModel model = BuildSchedulingModel(parms);

            ScheduleMeetingViewModel vm = new ScheduleMeetingViewModel();

            if (string.IsNullOrEmpty(parms.MeetingTypeDesc))
            {
                string jsonmessage = "The meeting type has been omitted from the parameter collection.";

                return RedirectToAction("SchedulingDashboard",
                   new
                   {

                       StatusMessage = jsonmessage
                   });
            }

            vm = PrepareMeetingData(parms, model);
            //if uistatusmessage enum != -1 (NA) then return to home controller with message
            if (vm.UIStatusMessage != UIStatusMessage.NA)
                return RedirectToAction("Index", "Home",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });

            List<FacilitatorMeetingAppointment> fmas = model.FacilitatorMeetingAppointments;

            FacilitatorMeetingAppointment fma = fmas.FirstOrDefault();

            if (fma != null)
            {
                fma.MeetingTypeEnum = (MeetingTypeEnum)fma.MeetingTypeRefId;
                vm.MeetingTypeDesc = fma.MeetingTypeEnum.ToString();
                vm.FacilitatorMeetingAppointment = fma;
                vm.FacilitatorMeetingApptID = fma.FacilitatorMeetingApptID;
                vm.ScheduleDate = fma.FromDt;
                vm.StartTime = fma.FromDt;
                vm.EndTime = fma.ToDt;

                if (fma.FromDt != null && fma.ToDt != null)
                {
                    TimeSpan span = fma.ToDt.Value.Subtract(fma.FromDt.Value);
                    Console.WriteLine("Time Difference (seconds): " + span.Seconds);
                    Console.WriteLine("Time Difference (minutes): " + span.Minutes);

                    vm.DurationHours = span.Hours.ToString();
                    vm.DurationMinutes = span.Minutes.ToString();
                }

                if (vm.FacilitatorMeetingAppointment.MeetingRoom != null)
                {
                    if (vm.MeetingRoomList.Contains(vm.FacilitatorMeetingAppointment.MeetingRoom))
                    {
                        vm.MeetingRoomNameSelected = fma.MeetingRoom.MeetingRoomName;
                        vm.MeetingRoomRefIDSelected = fma.MeetingRoom.MeetingRoomRefID.Value;
                    }
                }
                vm.UpdatedDate = fma.UpdatedDate;

                if (fma.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                || fma.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled)
                    vm.IsReschedule = true;

                vm.RequestedDates = new List<DateTime?>();
                if (fma.RequestedDate1.HasValue) vm.RequestedDates.Add(fma.RequestedDate1.Value);
                if (fma.RequestedDate2.HasValue) vm.RequestedDates.Add(fma.RequestedDate2.Value);
                if (fma.RequestedDate3.HasValue) vm.RequestedDates.Add(fma.RequestedDate3.Value);

                vm.RequestedDate1 = fma.RequestedDate1;
                vm.RequestedDate2 = fma.RequestedDate2;
                vm.RequestedDate3 = fma.RequestedDate3;

                vm.MeetingTypeRefId = (int)fma.MeetingTypeRefId;

                if (fma.Attendees != null)
                {
                    vm.SetSelectedReviewers(fma.Attendees);
                    foreach (AttendeeInfo attendee in fma.Attendees)
                    {
                        vm.CurrentAttendees.Add(new UserIdentity() { ID = attendee.AttendeeId });
                        vm.AttendeeIds += attendee.AttendeeId.ToString() + ",";
                    }
                }
            }
            else
            {
                vm.FacilitatorMeetingAppointment = new FacilitatorMeetingAppointment();
                vm.ScheduleDate = (DateTime?)null;
                vm.EndTime = (DateTime?)null;
                vm.StartTime = (DateTime?)null;
            }

            vm.Holidays = GenerateViewModelHolidays(model.Holidays);

            //check Perms
            //AION Permissions - System Administrator, Facilitator and Management have access
            //Plan reviewer admin setting must be Y to schedule to a facilitator meeting.
            bool canEdit = vm.PermissionMapping.IsManager
                || vm.PermissionMapping.IsFacilitator
                || vm.PermissionMapping.IsViewOnly
                || vm.PermissionMapping.IsSysAdmin;

            bool hasSchedulePrelim = vm.PermissionMapping.Schdul_Express_Man || vm.PermissionMapping.Schdul_Express_Auto;

            if (canEdit == false || hasSchedulePrelim == false)
            {
                return RedirectToAction("Index", "Home");
            }

            vm.AccelaProjectDeeplink = GenerateAccelaDeeplink(vm.Project.AccelaProjectRefId);


            if (vm.LoggedInUser?.ID == 0)
                return RedirectToAction("Index", "Home");

            if (vm == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                vm = new ScheduleMeetingViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." };
                return View(vm);
            }

            if (parms.PerformAutoEstimation == true)
            {
                AutoScheduledFacilitatorMeetingParams data = new AutoScheduledFacilitatorMeetingParams();
                data.AccelaProjectIDRef = parms.ProjectId;
                data.DurationHours = parms.DurationHours;
                data.DurationMinutes = parms.DurationMinutes;
                data.SuggestedDate1 = vm.RequestedDate1;
                data.SuggestedDate2 = vm.RequestedDate2;
                data.SuggestedDate3 = vm.RequestedDate3;
                data.BuildingUserID = parms.BuildingUserID;
                data.ElectricUserID = parms.ElectricUserID;
                data.MechUserID = parms.MechUserID;
                data.PlumbUserID = parms.PlumbUserID;
                data.ZoneUserID = parms.ZoneUserID;
                data.FireUserID = parms.FireUserID;
                data.FoodServiceUserID = parms.FoodServiceUserID;
                data.PoolUserID = parms.PoolUserID;
                data.FacilityUserID = parms.FacilityUserID;
                data.DayCareUserID = parms.DayCareUserID;
                data.BackFlowUserID = parms.BackFlowUserID;
                data.AdditionalAttendees = parms.AdditionalAttendees;
                data.RecIdTxt = parms.RecIdTxt;

                AutoScheduledFacilitatorMeetingValues ret = apihelper.GetAutoScheduledDataFacilitatorMeeting(data);
                //if userid = -1, then set to 0 so UI shows "Not Selected"
                vm.IsPostAutoSchedule = true;
                vm.ScheduledReviewerBuilding = ret.BuildingUserID == -1 ? "0" : ret.BuildingUserID.ToString();
                vm.ScheduledReviewerElectrical = ret.ElectricUserID == -1 ? "0" : ret.ElectricUserID.ToString();
                vm.ScheduledReviewerMechanical = ret.MechUserID == -1 ? "0" : ret.MechUserID.ToString();
                vm.ScheduledReviewerPlumbing = ret.PlumbUserID == -1 ? "0" : ret.PlumbUserID.ToString();
                vm.ScheduledReviewerZone = ret.ZoneUserID == -1 ? "0" : ret.ZoneUserID.ToString();
                vm.ScheduledReviewerFire = ret.FireUserID == -1 ? "0" : ret.FireUserID.ToString();
                vm.ScheduledReviewerBackFlow = ret.BackFlowUserID == -1 ? "0" : ret.BackFlowUserID.ToString();
                vm.ScheduledReviewerFood = ret.FoodServiceUserID == -1 ? "0" : ret.FoodServiceUserID.ToString();
                vm.ScheduledReviewerPool = ret.PoolUserID == -1 ? "0" : ret.PoolUserID.ToString();
                vm.ScheduledReviewerFacilities = ret.FacilityUserID == -1 ? "0" : ret.FacilityUserID.ToString();
                vm.ScheduledReviewerDayCare = ret.DayCareUserID == -1 ? "0" : ret.DayCareUserID.ToString();
                vm.ScheduleDate = ret.SelectedStartDateTime;
                vm.StartTime = ret.SelectedStartDateTime;
                vm.EndTime = ret.SelectedEndDateTime;
                vm.MeetingRoomRefIDSelected = ret.MeetingRoomId;
                string MeetingRoomNameSelected = ret.MeetingRoomId > 0 ? vm.MeetingRoomList.Where(x => x.MeetingRoomRefID == ret.MeetingRoomId).FirstOrDefault().MeetingRoomName : "Select A Meeting Room";
                vm.MeetingRoomNameSelected = MeetingRoomNameSelected;
                vm.AdditionalAttendeeIds = ret.AdditionalAttendeeIds;
                vm.DurationHours = ret.DurationHours;
                vm.DurationMinutes = ret.DurationMinutes;

                if (vm.StartTime == DateTime.MinValue)
                {
                    vm.StartTime = null;
                }

                if (vm.EndTime == DateTime.MinValue)
                {
                    vm.EndTime = null;
                }
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult SaveMeeting(ScheduleSaveViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            int? meetingroomrefid = null;
            int returnInt = 0;

            //meeting room has to be greater than -1 and 0 or else it should be null
            if (vm.MeetingRoomRefIDSelected > 0) meetingroomrefid = vm.MeetingRoomRefIDSelected;

            //put together date and time
            string dt = vm.ScheduleDate.ToShortDateString();
            string dt2 = vm.ScheduleDate.ToShortDateString();
            string tm = vm.StartTime.ToShortTimeString();
            string tm2 = vm.EndTime.ToShortTimeString();

            //can set the time and date to null if they haven't chosen a date - DATE SHOULD NOT BE NULL ON SAVE PER NATHAN OLSON 1/11/21
            DateTime? s = vm.ScheduleDate == DateTime.MinValue ? (DateTime?)null : DateTime.Parse(dt + ' ' + tm);
            DateTime? e = vm.StartTime != DateTime.MinValue && vm.EndTime != DateTime.MinValue ? DateTime.Parse(dt2 + ' ' + tm2) : (DateTime?)null;

            //save schedule choices
            //save attendees
            //save any changes to primary/secondary/excluded
            FacilitatorMeetingAppointment fma = new FacilitatorMeetingAppointment();
            bool sendEmail = vm.IsSubmit;
            //LES-3809 - add project audit for auto schedule
            fma.AutoScheduled = ProjectAuditHelper.SetAutoScheduleByButtonStatus(vm);
            fma.CreatedUser = vm.LoggedInUser;
            fma.UpdatedUser = vm.LoggedInUser;
            fma.MeetingRoomRefId = meetingroomrefid;
            fma.MeetingTypeEnum = (MeetingTypeEnum)Enum.Parse(typeof(MeetingTypeEnum), vm.MeetingTypeDesc);
            fma.MeetingTypeRefId = vm.MeetingTypeRefId;
            fma.VirtualMeetingInd = vm.IsVirtualMeeting;
            fma.ProjectID = vm.Project.ID;
            fma.FromDt = s.Value;
            fma.ToDt = e.Value;
            fma.FacilitatorMeetingApptID = (vm.FacilitatorMeetingApptID == null ? 0 : vm.FacilitatorMeetingApptID);
            fma.UpdatedDate = vm.UpdatedDate;
            fma.IsReschedule = vm.IsReschedule;
            fma.UserId = vm.LoggedInUser.ID.ToString();

            if (vm.IsSubmit)
            {
                fma.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Tentatively_Scheduled;
            }
            else
            {
                if (!fma.IsReschedule)
                {
                    fma.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled;
                }
            }

            //get the attendees string
            //Get the FMA Attendees who were added
            vm.AttendeeIds = string.IsNullOrWhiteSpace(vm.AttendeeIds) ? "" : vm.AttendeeIds;
            string[] attendeeids = vm.AttendeeIds.Split(',');

            List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();
            foreach (string item in attendeeids)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    int userId = int.Parse(item);
                    if (userId > 0)
                        attendeeIds.Add(new AttendeeInfo { AttendeeId = userId, DeptNameEnumId = -1, BusinessRefId = -1 });

                }
            }
            //add the reviewers to attendees
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerBuilding), (int)DepartmentNameEnums.Building));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerBackFlow), (int)DepartmentNameEnums.Backflow));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerDayCare), (int)DepartmentNameEnums.EH_Day_Care));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerElectrical), (int)DepartmentNameEnums.Electrical));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerFacilities), (int)DepartmentNameEnums.EH_Facilities));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerFire), (int)vm.FireAgency));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerFood), (int)DepartmentNameEnums.EH_Food));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerMechanical), (int)DepartmentNameEnums.Mechanical));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerPlumbing), (int)DepartmentNameEnums.Plumbing));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerPool), (int)DepartmentNameEnums.EH_Pool));
            attendeeIds.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerZone), (int)vm.ZoneAgency));

            //add the reviewers to assignedreviewers list for save model
            List<AttendeeInfo> assignedreviewers = new List<AttendeeInfo>();
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerBuilding), (int)DepartmentNameEnums.Building));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerBackFlow), (int)DepartmentNameEnums.Backflow));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerDayCare), (int)DepartmentNameEnums.EH_Day_Care));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerElectrical), (int)DepartmentNameEnums.Electrical));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerFacilities), (int)DepartmentNameEnums.EH_Facilities));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerFire), (int)vm.FireAgency));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerFood), (int)DepartmentNameEnums.EH_Food));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerMechanical), (int)DepartmentNameEnums.Mechanical));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerPlumbing), (int)DepartmentNameEnums.Plumbing));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerPool), (int)DepartmentNameEnums.EH_Pool));
            assignedreviewers.Add(new AttendeeInfo(int.Parse(vm.ScheduledReviewerZone), (int)vm.ZoneAgency));

            fma.AssignedReviewers = assignedreviewers;
            fma.InternalNotes = vm.InternalNotes;

            List<AttendeeInfo> apptAttendees = attendeeIds.Where(x => x.AttendeeId > 0).ToList();

            fma.IsSubmit = vm.IsSubmit;
            fma.NewAttendees = apptAttendees.OrderByDescending(x => x.BusinessRefId).GroupBy(x => x.AttendeeId).Select(group => group.First()).ToList();

            //save the fma and attendees
            returnInt = apihelper.UpsertFMA(fma);

            string jsonmessage = "Facilitator Meeting Appointment {0} {1}.";
            //change status message for 'submitted' vs 'saved'
            jsonmessage = String.Format(jsonmessage, vm.IsSubmit ? "submitted" : "saved", returnInt > 0 ? "Successfully" : ": an error occurred");
            TempData["StatusMessage"] = jsonmessage;
            if (vm.IsSubmit && returnInt > 0)
                return RedirectToAction("SchedulingDashboard");

            return RedirectToAction("ScheduleMeeting",
                new
                {

                    ProjectId = vm.Project.AccelaProjectRefId,
                    MeetingTypeDesc = vm.MeetingTypeDesc,
                    StatusMessage = jsonmessage
                });
        }

        /// <summary>
        /// ScheduleFIFOPlanReview page
        /// Gets Fifo schedule
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public ActionResult ScheduleFIFOPlanReview(ProjectParms parms)
        {
            APIHelper apihelper = new APIHelper();

            SchedulingModel model = BuildSchedulingModel(parms);

            SchedulePlanReviewViewModel vm = PrepareFIFOPlanReviewData(parms, model);

            if (vm.UIStatusMessage == UIStatusMessage.Project_Cycle_Missing)
                return RedirectToAction("SchedulingDashboard", "Scheduling",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });

            //if uistatusmessage enum != -1 (NA) then return to home controller with message
            if (vm.UIStatusMessage != UIStatusMessage.NA)
                return RedirectToAction("Index", "Home",
                    new
                    {

                        StatusMessage = vm.UIStatusMessage.ToStringValue()
                    });

            //check Perms
            //AION Permissions - System Administrator, Facilitator and Management have access
            //Plan reviewer admin setting must be Y to schedule to a prelim meeting.
            bool canEdit = vm.PermissionMapping.IsManager
                || vm.PermissionMapping.IsFacilitator
                || vm.PermissionMapping.IsViewOnly
                || vm.PermissionMapping.IsSysAdmin;

            bool hasAccess = vm.PermissionMapping.Access_SchedulePR;

            if (vm.PermissionMapping.Access_SchedulePR == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");

            if (vm == null)
            {
                ModelState.AddModelError("Error", "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.");
                vm = new SchedulePlanReviewViewModel { StatusMessage = "Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator." };
                return View(vm);
            }

            vm.AccelaLink = ConfigurationManager.AppSettings["AccelaBaseLink"].ToString();
            //Generate deeplink to accela
            vm.AccelaProjectDeeplink = GenerateAccelaDeeplink(vm.Project.AccelaProjectRefId);

            //Get multipliers set in Admin menu
            List<CatalogItem> catalogs = apihelper.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER");
            if (catalogs.Any())
            {
                vm.SchedulingMultiplierName = catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.NAME").FirstOrDefault().Value;
                vm.SchedulingMultiplierFactor = catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR").FirstOrDefault().Value;
                vm.SchedulingMultiplierProjectTypes = catalogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE").FirstOrDefault().Value.Split(',');
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult SaveFifoPlanReview(ScheduleSaveViewModel vm)
        {
            PlanReview pr = new PlanReview();

            pr.ProjectScheduleRefEnum = ProjectScheduleRefEnum.FIFO;
            pr.IsManualAssignment = false; // TODO: needs to be determined
            pr.AssignedFacilitator = vm.AssignedFacilitator;
            pr.IsReschedule = false;
            //LES-3809 - add project audit for auto schedule
            pr.AutoScheduled = vm.AutoScheduled;

            if (vm.IsFutureCycle) vm.IsRescheduleOverwrite = false;
            bool newId = (vm.IsNewCycle || vm.IsFutureCycle || vm.IsReschedule) && !vm.IsRescheduleOverwrite;

            //ids for updates
            pr.BackfPlanReviewScheduleId = vm.BackfPlanReviewScheduleId;
            pr.BuildPlanReviewScheduleId = vm.BuildPlanReviewScheduleId;
            pr.ElectPlanReviewScheduleId = vm.ElectPlanReviewScheduleId;
            pr.MechaPlanReviewScheduleId = vm.MechaPlanReviewScheduleId;
            pr.PlumbPlanReviewScheduleId = vm.PlumbPlanReviewScheduleId;
            pr.FirePlanReviewScheduleId = vm.FirePlanReviewScheduleId;
            pr.ZonePlanReviewScheduleId = vm.ZonePlanReviewScheduleId;
            pr.PoolPlanReviewScheduleId = vm.PoolPlanReviewScheduleId;
            pr.FoodPlanReviewScheduleId = vm.FoodPlanReviewScheduleId;
            pr.FacilPlanReviewScheduleId = vm.FacilPlanReviewScheduleId;
            pr.DaycPlanReviewScheduleId = vm.DaycPlanReviewScheduleId;

            //update dates
            pr.BackfPRSUpdateDate = vm.BackfPRSUpdateDate;
            pr.BuildPRSUpdateDate = vm.BuildPRSUpdateDate;
            pr.ElectPRSUpdateDate = vm.ElectPRSUpdateDate;
            pr.MechaPRSUpdateDate = vm.MechaPRSUpdateDate;
            pr.PlumbPRSUpdateDate = vm.PlumbPRSUpdateDate;
            pr.FirePRSUpdateDate = vm.FirePRSUpdateDate;
            pr.ZonePRSUpdateDate = vm.ZonePRSUpdateDate;
            pr.PoolPRSUpdateDate = vm.PoolPRSUpdateDate;
            pr.FoodPRSUpdateDate = vm.FoodPRSUpdateDate;
            pr.FacilPRSUpdateDate = vm.FacilPRSUpdateDate;
            pr.DaycPRSUpdateDate = vm.DaycPRSUpdateDate;

            //dates
            //if pool or fifo, dates should be null/blank
            pr.BackfStartDate = vm.BackfStartDate;
            pr.BackfEndDate = vm.BackfEndDate;

            pr.BuildStartDate = vm.BuildStartDate;
            pr.BuildEndDate = vm.BuildEndDate;
            pr.ElectStartDate = vm.ElectStartDate;
            pr.ElectEndDate = vm.ElectEndDate;
            pr.MechaStartDate = vm.MechaStartDate;
            pr.MechaEndDate = vm.MechaEndDate;
            pr.PlumbStartDate = vm.PlumbStartDate;
            pr.PlumbEndDate = vm.PlumbEndDate;

            pr.FireStartDate = vm.FireStartDate;
            pr.FireEndDate = vm.FireEndDate;

            pr.ZoneStartDate = vm.ZoneStartDate;
            pr.ZoneEndDate = vm.ZoneEndDate;

            pr.FoodStartDate = vm.FoodStartDate;
            pr.FoodEndDate = vm.FoodEndDate;
            pr.PoolStartDate = vm.PoolStartDate;
            pr.PoolEndDate = vm.PoolEndDate;
            pr.FacilStartDate = vm.FacilStartDate;
            pr.FacilEndDate = vm.FacilEndDate;
            pr.DaycStartDate = vm.DaycStartDate;
            pr.DaycEndDate = vm.DaycEndDate;

            pr.HoursBuilding = vm.HoursBuilding;
            pr.HoursBackFlow = vm.HoursBackFlow;
            pr.HoursDayCare = vm.HoursDayCare;
            pr.HoursElectic = vm.HoursElectic;
            pr.HoursFire = vm.HoursFire;
            pr.HoursFood = vm.HoursFood;
            pr.HoursLodge = vm.HoursLodge;
            pr.HoursMech = vm.HoursMech;
            pr.HoursPlumb = vm.HoursPlumb;
            pr.HoursPool = vm.HoursPool;
            pr.HoursZoning = vm.HoursZoning;

            //get the attendees
            List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();

            //add the reviewers to attendees
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), BusinessRefId = (int)DepartmentNameEnums.Building, DeptNameEnumId = (int)DepartmentNameEnums.Building });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), BusinessRefId = (int)DepartmentNameEnums.Backflow, DeptNameEnumId = (int)DepartmentNameEnums.Backflow });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care, DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), BusinessRefId = (int)DepartmentNameEnums.Electrical, DeptNameEnumId = (int)DepartmentNameEnums.Electrical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), BusinessRefId = (int)DepartmentNameEnums.EH_Facilities, DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), BusinessRefId = (int)vm.FireAgency, DeptNameEnumId = (int)vm.FireAgency });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), BusinessRefId = (int)DepartmentNameEnums.EH_Food, DeptNameEnumId = (int)DepartmentNameEnums.EH_Food });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), BusinessRefId = (int)DepartmentNameEnums.Mechanical, DeptNameEnumId = (int)DepartmentNameEnums.Mechanical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), BusinessRefId = (int)DepartmentNameEnums.Plumbing, DeptNameEnumId = (int)DepartmentNameEnums.Plumbing });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), BusinessRefId = (int)DepartmentNameEnums.EH_Pool, DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), BusinessRefId = (int)vm.ZoneAgency, DeptNameEnumId = (int)vm.ZoneAgency });

            pr.AssignedReviewers = attendeeIds;

            //save schedule choices
            //save attendees
            bool sendEmail = vm.IsSubmit;
            pr.CreatedUser = vm.LoggedInUser;
            pr.UpdatedUser = vm.LoggedInUser;
            pr.ProjectId = vm.Project.ID;
            pr.UpdatedDate = vm.PMAUpdateDate;

            pr.PlanReviewScheduleId = vm.PlanReviewScheduleId;

            pr.ApptResponseStatusEnum = AppointmentResponseStatusEnum.Scheduled;

            bool returnInt = ScheduleAPIHelper.UpsertFIFO(pr);

            //if the facilitator was changed, save the new facilitator
            if (vm.PreviousAssignedFacilitator.ToString().Equals(vm.AssignedFacilitator) == false)
            {
                //update the facilitator
                ProjectEstimation project = new ProjectEstimation
                {
                    ID = vm.Project.ID,
                    UpdatedDate = vm.Project.UpdatedDate,
                    UpdatedUser = vm.LoggedInUser,
                    AssignedFacilitator = Convert.ToInt32(vm.AssignedFacilitator)
                };

                bool saveFacilitatorSuccess = ScheduleAPIHelper.UpdateAssignedFacilitator(project);
            }

            string jsonmessage = "Plan Review {0} {1}.";
            //change status message for 'submitted' vs 'saved'
            jsonmessage = String.Format(jsonmessage, vm.IsSubmit ? "submitted" : "saved", returnInt ? "Successfully" : ": an error occurred");
            TempData["StatusMessage"] = jsonmessage;
            if (vm.IsSubmit && returnInt)
                return RedirectToAction("SchedulingDashboard");

            return RedirectToAction("ScheduleFIFOPlanReview",
                new
                {
                    ProjectId = vm.Project.AccelaProjectRefId,
                    StatusMessage = jsonmessage
                });
        }

        #region Private Methods

        private string GenerateViewModelHolidays(List<DateTime> holidayDates)
        {

            List<DateTime> holidays = holidayDates != null && holidayDates.Any() ? holidayDates : new APIHelper().GetHolidayDateList();

            StringBuilder holidaysb = new StringBuilder();

            foreach (DateTime holiday in holidays)
            {
                if (holiday > DateTime.Today.AddDays(-1))
                {
                    holidaysb.Append(holiday.ToShortDateString());
                    holidaysb.Append(",");
                }
            }

            return holidaysb.ToString();
        }

        private SchedulingModel BuildSchedulingModel(ProjectParms parms)
        {
            ScheduleAPIHelper scheduleAPIHelper = new ScheduleAPIHelper();

            SchedulingModel model = scheduleAPIHelper.GetSchedulingModel(parms);

            return model;
        }

        private SchedulePreliminaryMeetingViewModel PrepareSchedulePreliminaryMeetingData(ProjectParms parms, SchedulingModel model)
        {
            SchedulePreliminaryMeetingViewModel vm = new SchedulePreliminaryMeetingViewModel();
            ProjectEstimation basedata;
            DateTime today = DateTime.Now;

            UIHelpers uihelper = new UIHelpers();
            APIHelper apihelper = new APIHelper();
            Helper helper = new Helper();

            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;
            if (parms.ProjectId == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_ID_Missing;
                return vm;
            }
            if (vm.LoggedInUserEmail == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Not_Logged_In;
                return vm;
            }

            UpdateUserAndPermissions(vm);

            //check perms, send blank vm with status message "insufficient permission"
            if (vm.PermissionMapping.Access_SchedulePrelim == false)
            {
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                return vm;
            }

            basedata = model.ProjectEstimation;
            vm.Project = basedata;

            ProjectAgency fireagency = vm.Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            ProjectAgency zoneagency = vm.Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            vm.FireAgency = fireagency.DepartmentInfo;
            vm.ZoneAgency = zoneagency.DepartmentInfo;

            vm.TeamScore = vm.Project.TeamGradeTxt;

            vm.FacilitatorList = model.Facilitators;
            //If this is express, only allow reviewers that have IsExpressSched = true
            bool isExpressProject = (vm.Project.AccelaPropertyType == PropertyTypeEnums.Express);
            vm.AllReviewers = vm.Project.Reviewers;

            vm.NotesComments = model.Notes;
            //uihelper.SetPermissionsForEstimationUI(vm);//TODO
            //removes underscore from Action params. this will avoid %20 from links but with keeping the message readable
            vm.StatusMessage = parms.StatusMessage != null ? parms.StatusMessage.Replace('_', ' ') : "";
            vm.ExcludedPlanReviewersBuild.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersElectric.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersMech.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersPlumb.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersFire.AddRange(basedata.Agencies.Where(x => vm.FireDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersZone.AddRange(basedata.Agencies.Where(x => vm.ZoneDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersDayCare.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersFood.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersLodge.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersBackFlow.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersPool.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool)
                            .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.MeetingRoomList = model.MeetingRooms;
            vm.UserIdentities = model.Users;
            return vm;
        }

        private SchedulePlanReviewViewModel PreparePlanReviewEstimationData(ProjectParms parms, SchedulingModel model)
        {
            SchedulePlanReviewViewModel vm = new SchedulePlanReviewViewModel();
            ProjectEstimation basedata;
            DateTime today = DateTime.Now;

            UIHelpers uihelper = new UIHelpers();
            APIHelper apihelper = new APIHelper();
            Helper helper = new Helper();

            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;
            parms.LoggedInUserEmail = _loggedinUser;
            if (parms.ProjectId == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_ID_Missing;
                return vm;
            }
            if (parms.LoggedInUserEmail == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Not_Logged_In;
                return vm;
            }

            vm.LoggedInUserEmail = parms.LoggedInUserEmail;

            UpdateUserAndPermissions(vm);

            //check perms, send blank vm with status message "insufficient permission"
            if (vm.PermissionMapping.Access_SchedulePR == false)
            {
                vm.StatusMessage = "Insufficient permission";
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                return vm;
            }

            vm.Holidays = GenerateViewModelHolidays(model.Holidays);

            basedata = model.ProjectEstimation;
            vm.Project = basedata;

            ProjectAgency fireagency = vm.Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            ProjectAgency zoneagency = vm.Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            vm.FireAgency = fireagency.DepartmentInfo;
            vm.ZoneAgency = zoneagency.DepartmentInfo;

            vm.PropertyType = vm.Project.AccelaPropertyType;
            vm.AllReviewers = vm.Project.Reviewers;

            //GET ALL INFO FOR PROJECT
            ProjectCycleSummary projectCycleSummary = model.ProjectCycleSummary;

            if (projectCycleSummary == null
                || projectCycleSummary.ProjectCycleCurrent == null
                || projectCycleSummary.ProjectCycleCurrent.ID == 0)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_Cycle_Missing;
                return vm;
            }

            vm.Cycle = projectCycleSummary.ProjectCycleCurrent.CycleNbr.Value;

            PlanReview planReview = new PlanReview();
            planReview.ProjectCycle = projectCycleSummary.ProjectCycleCurrent;

            if (planReview.ProjectCycle.IsAprvInd != null && planReview.ProjectCycle.IsAprvInd == false)
            {
                // plan review was previously rejected
                vm.PreviouslyRejected = true;
            }

            vm.IsFutureCycle = planReview.ProjectCycle.FutureCycleInd.HasValue && planReview.ProjectCycle.FutureCycleInd.Value == true;

            if (projectCycleSummary.PlanReviewCurrent != null && projectCycleSummary.PlanReviewCurrent.ID > 0)
            {
                planReview = projectCycleSummary.PlanReviewCurrent;
                if ((planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                    || planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled))
                    vm.IsReschedule = true;

                if (planReview.IsReschedule) vm.IsRescheduleOverwrite = true;

                vm.PlanReviewScheduleId = planReview.PlanReviewScheduleId;
                vm.ProjectCycleId = planReview.ProjectCycle.ID;
                vm.GateDate = planReview.ProjectCycle.GateDt == null ? DateTime.MinValue : planReview.ProjectCycle.GateDt.Value;
            }
            else
            {
                vm.IsNewCycle = true;
                vm.IsAdjustHours = parms.IsAdjustHours;
            }

            vm.IsCycleComparison = projectCycleSummary.ProjectCycleDetailsCurrent != null
                && projectCycleSummary.ProjectCycleDetailsCurrent.Count > 0
                && projectCycleSummary.PlanReviewScheduleDetailsCurrent != null
                && projectCycleSummary.PlanReviewScheduleDetailsCurrent.Count > 0;

            planReview.HasFutureCycle = projectCycleSummary.ProjectCycleFuture != null
                && projectCycleSummary.ProjectCycleFuture.ID > 0;

            //determine if schedule future cycle is available
            vm.CanScheduleFutureCycle =
                (vm.Project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family ||
                vm.Project.AccelaPropertyType == PropertyTypeEnums.Commercial ||
                vm.Project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team) &&
                !planReview.HasFutureCycle &&
                !vm.IsNewCycle &&
                (vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Scheduled ||
                vm.Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Tentatively_Scheduled) &&
                Enum.IsDefined(typeof(AppointmentResponseStatusEnum), planReview.ApptResponseStatusEnum);

            vm.TeamScore = vm.Project.TeamGradeTxt;

            // if there are project cycle details for the current cycle that came from Accela
            // and plan review schedule detail records for the current cycle, 
            // it's not a reschedule and is a cycle comparison

            PlanReview previousPlanReview = null;

            // set up subsequent cycle LES-616
            if (vm.Cycle > 1 && projectCycleSummary.PlanReviewScheduleDetailsFuture == null) // set up next plan review cycle using previous
            {
                int previousCycle = vm.Cycle - 1;
                previousPlanReview = projectCycleSummary.PlanReviews.FirstOrDefault(x => x.ProjectCycle.CycleNbr == previousCycle);

                List<PlanReviewScheduleDetail> previousReviewSchedules = new List<PlanReviewScheduleDetail>();
                if (previousPlanReview != null && previousPlanReview.ID > 0)
                {
                    previousReviewSchedules = apihelper.GetPlanReviewScheduleDetailsByPlanReviewSchedule(previousPlanReview.ID);
                }

                //get plan review details by plan review schedule id
                foreach (ProjectCycleDetail cycle in projectCycleSummary.ProjectCycleDetailsCurrent)
                {
                    var prevSchedule = previousReviewSchedules.FirstOrDefault(x => x.BusinessRefId == cycle.BusinessRefId);

                    switch (cycle.BusinessRefId)
                    {
                        case (int)DepartmentNameEnums.Building:
                            vm.ReReviewBuilding = cycle.RereviewHoursNbr;
                            vm.ProposedBuilding = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.PrimaryReviewerBuilding = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListBuild, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewBuilding <= (decimal)1.0) planReview.BuildPool = true;
                            break;
                        case (int)DepartmentNameEnums.Electrical:
                            vm.ReReviewElectric = cycle.RereviewHoursNbr;
                            vm.ProposedElectric = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.PrimaryReviewerElectrical = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListElectric, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewElectric <= (decimal)1.0) planReview.ElectPool = true;
                            break;
                        case (int)DepartmentNameEnums.Mechanical:
                            vm.ReReviewMech = cycle.RereviewHoursNbr;
                            vm.ProposedMech = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.PrimaryReviewerMechanical = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListMech, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewMech <= (decimal)1.0) planReview.MechaPool = true;
                            break;
                        case (int)DepartmentNameEnums.Plumbing:
                            vm.ReReviewPlumb = cycle.RereviewHoursNbr;
                            vm.ProposedPlumb = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.PrimaryReviewerPlumbing = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListPlumb, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewPlumb <= (decimal)1.0) planReview.PlumbPool = true;
                            break;
                        case (int)DepartmentNameEnums.Fire_Davidson:
                        case (int)DepartmentNameEnums.Fire_Cornelius:
                        case (int)DepartmentNameEnums.Fire_Pineville:
                        case (int)DepartmentNameEnums.Fire_Matthews:
                        case (int)DepartmentNameEnums.Fire_Mint_Hill:
                        case (int)DepartmentNameEnums.Fire_Huntersville:
                        case (int)DepartmentNameEnums.Fire_UMC:
                        case (int)DepartmentNameEnums.Fire_Cty_Chrlt:
                        case (int)DepartmentNameEnums.Fire_County:
                            vm.ReReviewFire = cycle.RereviewHoursNbr;
                            vm.ProposedFire = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerFire = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListFire, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewFire <= (decimal)1.0) planReview.FirePool = true;
                            break;
                        case (int)DepartmentNameEnums.Zone_Davidson:
                        case (int)DepartmentNameEnums.Zone_Cornelius:
                        case (int)DepartmentNameEnums.Zone_Pineville:
                        case (int)DepartmentNameEnums.Zone_Matthews:
                        case (int)DepartmentNameEnums.Zone_Mint_Hill:
                        case (int)DepartmentNameEnums.Zone_Huntersville:
                        case (int)DepartmentNameEnums.Zone_UMC:
                        case (int)DepartmentNameEnums.Zone_Cty_Chrlt:
                        case (int)DepartmentNameEnums.Zone_County:
                            vm.ReReviewZoning = cycle.RereviewHoursNbr;
                            vm.ProposedZoning = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerZone = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListZone, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewZoning <= (decimal)1.0) planReview.ZonePool = true;
                            break;
                        case (int)DepartmentNameEnums.EH_Day_Care:
                            vm.ReReviewDayCare = cycle.RereviewHoursNbr;
                            vm.ProposedDayCare = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerDayCare = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListDayCare, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewDayCare <= (decimal)1.0) planReview.DaycPool = true;
                            break;
                        case (int)DepartmentNameEnums.EH_Facilities:
                            vm.ReReviewLodge = cycle.RereviewHoursNbr;
                            vm.ProposedLodge = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerFacilities = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListLodge, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewLodge <= (decimal)1.0) planReview.FacilPool = true;
                            break;
                        case (int)DepartmentNameEnums.EH_Food:
                            vm.ReReviewFood = cycle.RereviewHoursNbr;
                            vm.ProposedFood = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerFood = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListFood, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewFood <= (decimal)1.0) planReview.FoodPool = true;
                            break;
                        case (int)DepartmentNameEnums.EH_Pool:
                            vm.ReReviewPool = cycle.RereviewHoursNbr;
                            vm.ProposedPool = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerPool = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListPool, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewPool <= (decimal)1.0) planReview.PoolPool = true;
                            break;
                        case (int)DepartmentNameEnums.Backflow:
                            vm.ReReviewBackFlow = cycle.RereviewHoursNbr;
                            vm.ProposedBackFlow = cycle.RereviewHoursNbr;
                            if (prevSchedule != null)
                            {
                                vm.ScheduledReviewerBackFlow = ScheduleHelpers.GetReReviewerName(vm.AssignPlanReviewersListBackFlow, (int)prevSchedule.AssignedPlanReviewerId);
                            }
                            if (vm.ReReviewBackFlow <= (decimal)1.0) planReview.BackfPool = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            //prepare future cycle scheduling info
            //TODO use hours from previous cycle, not just
            foreach (ProjectAgency agency in vm.Project.Agencies)
            {
                switch (agency.DepartmentInfo)
                {
                    case DepartmentNameEnums.EH_Day_Care:
                        //If this value hasn't already been set above
                        if (!vm.ProposedDayCare.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedDayCare = vm.ReReviewDayCare.HasValue ? vm.ReReviewDayCare / 2 : null;
                            else vm.ProposedDayCare = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.EH_Food:
                        if (!vm.ProposedFood.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedFood = vm.ReReviewFood.HasValue ? vm.ReReviewFood / 2 : null;
                            else vm.ProposedFood = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        if (!vm.ProposedPool.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedPool = vm.ReReviewPool.HasValue ? vm.ReReviewPool / 2 : null;
                            else vm.ProposedPool = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        if (!vm.ProposedLodge.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedLodge = vm.ReReviewLodge.HasValue ? vm.ReReviewLodge / 2 : null;
                            else vm.ProposedLodge = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.Backflow:
                        if (!vm.ProposedBackFlow.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedBackFlow = vm.ReReviewBackFlow.HasValue ? vm.ReReviewBackFlow / 2 : null;
                            else vm.ProposedBackFlow = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        if (!vm.ProposedZoning.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedZoning = vm.ReReviewZoning.HasValue ? vm.ReReviewZoning / 2 : null;
                            else vm.ProposedZoning = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.Fire_Davidson:
                    case DepartmentNameEnums.Fire_Cornelius:
                    case DepartmentNameEnums.Fire_Pineville:
                    case DepartmentNameEnums.Fire_Matthews:
                    case DepartmentNameEnums.Fire_Mint_Hill:
                    case DepartmentNameEnums.Fire_Huntersville:
                    case DepartmentNameEnums.Fire_UMC:
                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                    case DepartmentNameEnums.Fire_County:
                        if (!vm.ProposedFire.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedFire = vm.ReReviewFire.HasValue ? vm.ReReviewFire / 2 : null;
                            else vm.ProposedFire = agency.EstimationHours.HasValue ? agency.EstimationHours / 2 : null;
                        }
                        break;
                    default:
                        break;
                }
            }
            foreach (ProjectTrade trade in vm.Project.Trades)
            {
                switch (trade.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:
                        if (!vm.ProposedBuilding.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedBuilding = vm.ReReviewBuilding.HasValue ? vm.ReReviewBuilding / 2 : null;
                            else vm.ProposedBuilding = trade.EstimationHours.HasValue ? trade.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.Electrical:
                        if (!vm.ProposedElectric.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedElectric = vm.ReReviewElectric.HasValue ? vm.ReReviewElectric / 2 : null;
                            else vm.ProposedElectric = trade.EstimationHours.HasValue ? trade.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.Mechanical:
                        if (!vm.ProposedMech.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedMech = vm.ReReviewMech.HasValue ? vm.ReReviewMech / 2 : null;
                            else vm.ProposedMech = trade.EstimationHours.HasValue ? trade.EstimationHours / 2 : null;
                        }
                        break;
                    case DepartmentNameEnums.Plumbing:
                        if (!vm.ProposedPlumb.HasValue && !vm.IsCycleComparison)
                        {
                            if (vm.Cycle > 1) vm.ProposedPlumb = vm.ReReviewPlumb.HasValue ? vm.ReReviewPlumb / 2 : null;
                            else vm.ProposedPlumb = trade.EstimationHours.HasValue ? trade.EstimationHours / 2 : null;
                        }
                        break;
                    default:
                        break;
                }
            }

            //build vm for plan review schedules

            //build vm for project
            //LES-2979 jlindsay If this is not the first cycle, return blank datetime for the trades/agencies
            vm.BackfEndDate = ScheduleHelpers.GetCycleDate(planReview.BackfEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.BackfPlanReviewScheduleId = planReview.BackfPlanReviewScheduleId;
            vm.BackfPool = planReview.BackfPool;
            vm.BackfPRSUpdateDate = planReview.BackfPRSUpdateDate;
            vm.BackfStartDate = ScheduleHelpers.GetCycleDate(planReview.BackfStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.BuildEndDate = ScheduleHelpers.GetCycleDate(planReview.BuildEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.BuildPlanReviewScheduleId = planReview.BuildPlanReviewScheduleId;
            vm.BuildPool = planReview.BuildPool;
            vm.BuildPRSUpdateDate = planReview.BuildPRSUpdateDate;
            vm.BuildStartDate = ScheduleHelpers.GetCycleDate(planReview.BuildStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.ElectEndDate = ScheduleHelpers.GetCycleDate(planReview.ElectEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.ElectPlanReviewScheduleId = planReview.ElectPlanReviewScheduleId;
            vm.ElectPool = planReview.ElectPool;
            vm.ElectPRSUpdateDate = planReview.ElectPRSUpdateDate;
            vm.ElectStartDate = ScheduleHelpers.GetCycleDate(planReview.ElectStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.PlumbEndDate = ScheduleHelpers.GetCycleDate(planReview.PlumbEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.PlumbPlanReviewScheduleId = planReview.PlumbPlanReviewScheduleId;
            vm.PlumbPool = planReview.PlumbPool;
            vm.PlumbPRSUpdateDate = planReview.PlumbPRSUpdateDate;
            vm.PlumbStartDate = ScheduleHelpers.GetCycleDate(planReview.PlumbStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.FireEndDate = ScheduleHelpers.GetCycleDate(planReview.FireEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.FirePlanReviewScheduleId = planReview.FirePlanReviewScheduleId;
            vm.FirePool = planReview.FirePool;
            vm.FirePRSUpdateDate = planReview.FirePRSUpdateDate;
            vm.FireStartDate = ScheduleHelpers.GetCycleDate(planReview.FireStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.ZoneEndDate = ScheduleHelpers.GetCycleDate(planReview.ZoneEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.ZonePlanReviewScheduleId = planReview.ZonePlanReviewScheduleId;
            vm.ZonePool = planReview.ZonePool;
            vm.ZonePRSUpdateDate = planReview.ZonePRSUpdateDate;
            vm.ZoneStartDate = ScheduleHelpers.GetCycleDate(planReview.ZoneStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.PoolEndDate = ScheduleHelpers.GetCycleDate(planReview.PoolEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.PoolPlanReviewScheduleId = planReview.PoolPlanReviewScheduleId;
            vm.PoolPool = planReview.PoolPool;
            vm.PoolPRSUpdateDate = planReview.PoolPRSUpdateDate;
            vm.PoolStartDate = ScheduleHelpers.GetCycleDate(planReview.PoolStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.MechaEndDate = ScheduleHelpers.GetCycleDate(planReview.MechaEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.MechaPlanReviewScheduleId = planReview.MechaPlanReviewScheduleId;
            vm.MechaPool = planReview.MechaPool;
            vm.MechaPRSUpdateDate = planReview.MechaPRSUpdateDate;
            vm.MechaStartDate = ScheduleHelpers.GetCycleDate(planReview.MechaStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.FoodEndDate = ScheduleHelpers.GetCycleDate(planReview.FoodEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.FoodPlanReviewScheduleId = planReview.FoodPlanReviewScheduleId;
            vm.FoodPool = planReview.FoodPool;
            vm.FoodPRSUpdateDate = planReview.FoodPRSUpdateDate;
            vm.FoodStartDate = ScheduleHelpers.GetCycleDate(planReview.FoodStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.FacilEndDate = ScheduleHelpers.GetCycleDate(planReview.FacilEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.FacilPlanReviewScheduleId = planReview.FacilPlanReviewScheduleId;
            vm.FacilPool = planReview.FacilPool;
            vm.FacilPRSUpdateDate = planReview.FacilPRSUpdateDate;
            vm.FacilStartDate = ScheduleHelpers.GetCycleDate(planReview.FacilStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.DaycEndDate = ScheduleHelpers.GetCycleDate(planReview.DaycEndDate, vm.IsNewCycle, vm.PreviouslyRejected);
            vm.DaycPlanReviewScheduleId = planReview.DaycPlanReviewScheduleId;
            vm.DaycPool = planReview.DaycPool;
            vm.DaycPRSUpdateDate = planReview.DaycPRSUpdateDate;
            vm.DaycStartDate = ScheduleHelpers.GetCycleDate(planReview.DaycStartDate, vm.IsNewCycle, vm.PreviouslyRejected);

            vm.FacilitatorList = apihelper.GetAllFacilitators();
            //If this is express, only allow reviewers that have IsExpressSched = true
            bool isExpressProject = (vm.Project.AccelaPropertyType == PropertyTypeEnums.Express);
            int projectTypeEnum = (int)vm.Project.AccelaPropertyType;

            vm.NotesComments = NoteAPIHelper.GetProjectNotes(basedata.ID, null);

            //TODO: if mandatory notes type has content, add to the view model, otherwishe, get the standard note for mandatory by project type
            List<Note> mandatorynotes = vm.NotesComments.Where(x => x.NotesType.Type == NoteTypeEnum.SchedulingMandatoryNotes).ToList();
            if (mandatorynotes != null && !mandatorynotes.Any())
            {
                List<StandardNote> standardmandatorynotes = NoteAPIHelper.GetStandardNotes(NoteTypeEnum.SchedulingMandatoryNotes, vm.Project.AionPropertyType);
                if (standardmandatorynotes != null && standardmandatorynotes.Count() > 0)
                    vm.MandatorySchedulingNotes = standardmandatorynotes.FirstOrDefault().StandardNoteText;
            }
            else
            {
                vm.MandatorySchedulingNotes = mandatorynotes.FirstOrDefault().NotesComments;
            }

            vm.StandardNotes = NoteAPIHelper.GetStandardNotes(NoteTypeEnum.EstimationStandardNotes, PropertyTypeEnums.NA);
            vm.StandardNoteGroups = NoteAPIHelper.GetStandardNoteGroupEnums();

            //start this date at 3 weeks after last day of previous plan review
            vm.ScheduleAfterDate = previousPlanReview != null ? previousPlanReview.MaxDate?.AddDays(21) : null;

            if (planReview.ID > 0)
            {
                if (planReview.ProjectCycle.PlansReadyOnDt.HasValue)
                {
                    vm.PlansReadyOnDate = planReview.ProjectCycle.PlansReadyOnDt.Value;
                }
                else if (vm.Project.PlansReadyOnDate.HasValue)
                {

                    vm.PlansReadyOnDate = vm.Project.PlansReadyOnDate.Value;
                }
                else
                {
                    vm.PlansReadyOnDate = null;
                }
            }
            else
            {
                vm.PlansReadyOnDate = vm.Project.PlansReadyOnDate;
            }

            //uihelper.SetPermissionsForEstimationUI(vm);//TODO
            //removes underscore from Action params. this will avoid %20 from links but with keeping the message readable
            vm.StatusMessage = parms.StatusMessage != null ? parms.StatusMessage.Replace('_', ' ') : "";
            vm.ExcludedPlanReviewersBuild.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersElectric.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersMech.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersPlumb.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersFire.AddRange(basedata.Agencies.Where(x => vm.FireDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersZone.AddRange(basedata.Agencies.Where(x => vm.ZoneDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersDayCare.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersFood.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersLodge.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersBackFlow.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersPool.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool)
                            .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));

            //Set final plan review hours based on estimated, rereview, and adjust hours selections
            if (vm.IsCycleComparison)
            {
                if (!vm.IsAdjustHours)
                {
                    vm.HoursBuildingFinal = vm.ProposedBuilding.HasValue ? vm.ProposedBuilding.Value : 0;
                    vm.HoursElecticFinal = vm.ProposedElectric.HasValue ? vm.ProposedElectric.Value : 0;
                    vm.HoursMechFinal = vm.ProposedMech.HasValue ? vm.ProposedMech.Value : 0;
                    vm.HoursPlumbFinal = vm.ProposedPlumb.HasValue ? vm.ProposedPlumb.Value : 0;
                    vm.HoursZoningFinal = vm.ProposedZoning.HasValue ? vm.ProposedZoning.Value : 0;
                    vm.HoursFireFinal = vm.ProposedFire.HasValue ? vm.ProposedFire.Value : 0;
                    vm.HoursBackFlowFinal = vm.ProposedBackFlow.HasValue ? vm.ProposedBackFlow.Value : 0;
                    vm.HoursFoodFinal = vm.ProposedFood.HasValue ? vm.ProposedFood.Value : 0;
                    vm.HoursPoolFinal = vm.ProposedPool.HasValue ? vm.ProposedPool.Value : 0;
                    vm.HoursLodgeFinal = vm.ProposedLodge.HasValue ? vm.ProposedLodge.Value : 0;
                    vm.HoursDayCareFinal = vm.ProposedDayCare.HasValue ? vm.ProposedDayCare.Value : 0;
                }
                else
                {
                    vm.HoursBuildingFinal = vm.ReReviewBuilding.HasValue ? vm.ReReviewBuilding.Value : 0;
                    vm.HoursElecticFinal = vm.ReReviewElectric.HasValue ? vm.ReReviewElectric.Value : 0;
                    vm.HoursMechFinal = vm.ReReviewMech.HasValue ? vm.ReReviewMech.Value : 0;
                    vm.HoursPlumbFinal = vm.ReReviewPlumb.HasValue ? vm.ReReviewPlumb.Value : 0;
                    vm.HoursZoningFinal = vm.ReReviewZoning.HasValue ? vm.ReReviewZoning.Value : 0;
                    vm.HoursFireFinal = vm.ReReviewFire.HasValue ? vm.ReReviewFire.Value : 0;
                    vm.HoursBackFlowFinal = vm.ReReviewBackFlow.HasValue ? vm.ReReviewBackFlow.Value : 0;
                    vm.HoursFoodFinal = vm.ReReviewFood.HasValue ? vm.ReReviewFood.Value : 0;
                    vm.HoursPoolFinal = vm.ReReviewPool.HasValue ? vm.ReReviewPool.Value : 0;
                    vm.HoursLodgeFinal = vm.ReReviewLodge.HasValue ? vm.ReReviewLodge.Value : 0;
                    vm.HoursDayCareFinal = vm.ReReviewDayCare.HasValue ? vm.ReReviewDayCare.Value : 0;
                }
            }
            else if (vm.IsFutureCycle)
            {
                vm.HoursBuildingFinal = vm.ProposedBuilding.HasValue ? vm.ProposedBuilding.Value : 0;
                vm.HoursElecticFinal = vm.ProposedElectric.HasValue ? vm.ProposedElectric.Value : 0;
                vm.HoursMechFinal = vm.ProposedMech.HasValue ? vm.ProposedMech.Value : 0;
                vm.HoursPlumbFinal = vm.ProposedPlumb.HasValue ? vm.ProposedPlumb.Value : 0;
                vm.HoursZoningFinal = vm.ProposedZoning.HasValue ? vm.ProposedZoning.Value : 0;
                vm.HoursFireFinal = vm.ProposedFire.HasValue ? vm.ProposedFire.Value : 0;
                vm.HoursBackFlowFinal = vm.ProposedBackFlow.HasValue ? vm.ProposedBackFlow.Value : 0;
                vm.HoursFoodFinal = vm.ProposedFood.HasValue ? vm.ProposedFood.Value : 0;
                vm.HoursPoolFinal = vm.ProposedPool.HasValue ? vm.ProposedPool.Value : 0;
                vm.HoursLodgeFinal = vm.ProposedLodge.HasValue ? vm.ProposedLodge.Value : 0;
                vm.HoursDayCareFinal = vm.ProposedDayCare.HasValue ? vm.ProposedDayCare.Value : 0;
            }
            else if (vm.Cycle > 1)
            {
                vm.HoursBuildingFinal = vm.ReReviewBuilding.HasValue ? vm.ReReviewBuilding.Value : 0;
                vm.HoursElecticFinal = vm.ReReviewElectric.HasValue ? vm.ReReviewElectric.Value : 0;
                vm.HoursMechFinal = vm.ReReviewMech.HasValue ? vm.ReReviewMech.Value : 0;
                vm.HoursPlumbFinal = vm.ReReviewPlumb.HasValue ? vm.ReReviewPlumb.Value : 0;
                vm.HoursZoningFinal = vm.ReReviewZoning.HasValue ? vm.ReReviewZoning.Value : 0;
                vm.HoursFireFinal = vm.ReReviewFire.HasValue ? vm.ReReviewFire.Value : 0;
                vm.HoursBackFlowFinal = vm.ReReviewBackFlow.HasValue ? vm.ReReviewBackFlow.Value : 0;
                vm.HoursFoodFinal = vm.ReReviewFood.HasValue ? vm.ReReviewFood.Value : 0;
                vm.HoursPoolFinal = vm.ReReviewPool.HasValue ? vm.ReReviewPool.Value : 0;
                vm.HoursLodgeFinal = vm.ReReviewLodge.HasValue ? vm.ReReviewLodge.Value : 0;
                vm.HoursDayCareFinal = vm.ReReviewDayCare.HasValue ? vm.ReReviewDayCare.Value : 0;
            }
            else
            {
                vm.HoursBuildingFinal = vm.HoursBuilding;
                vm.HoursElecticFinal = vm.HoursElectic;
                vm.HoursMechFinal = vm.HoursMech;
                vm.HoursPlumbFinal = vm.HoursPlumb;
                vm.HoursZoningFinal = vm.HoursZoning;
                vm.HoursFireFinal = vm.HoursFire;
                vm.HoursBackFlowFinal = vm.HoursBackFlow;
                vm.HoursFoodFinal = vm.HoursFood;
                vm.HoursPoolFinal = vm.HoursPool;
                vm.HoursLodgeFinal = vm.HoursLodge;
                vm.HoursDayCareFinal = vm.HoursDayCare;
            }
            //clear dates and selected reviewers for any depts that are set to 0 hours
            if (vm.HoursBuildingFinal == 0)
            {
                vm.ScheduledReviewerBuilding = "";
                vm.BuildStartDate = null;
                vm.BuildEndDate = null;
            }
            if (vm.HoursElecticFinal == 0)
            {
                vm.ScheduledReviewerElectrical = "";
                vm.ElectStartDate = null;
                vm.ElectEndDate = null;
            }
            if (vm.HoursMechFinal == 0)
            {
                vm.ScheduledReviewerMechanical = "";
                vm.MechaStartDate = null;
                vm.MechaEndDate = null;
            }
            if (vm.HoursPlumbFinal == 0)
            {
                vm.ScheduledReviewerPlumbing = "";
                vm.PlumbStartDate = null;
                vm.PlumbEndDate = null;
            }
            if (vm.HoursZoningFinal == 0)
            {
                vm.ScheduledReviewerZone = "";
                vm.ZoneStartDate = null;
                vm.ZoneEndDate = null;
            }
            if (vm.HoursFireFinal == 0)
            {
                vm.ScheduledReviewerFire = "";
                vm.FireStartDate = null;
                vm.FireEndDate = null;
            }
            if (vm.HoursBackFlowFinal == 0)
            {
                vm.ScheduledReviewerBackFlow = "";
                vm.BackfStartDate = null;
                vm.BackfEndDate = null;
            }
            if (vm.HoursFoodFinal == 0)
            {
                vm.ScheduledReviewerFood = "";
                vm.FoodStartDate = null;
                vm.FoodEndDate = null;
            }
            if (vm.HoursPoolFinal == 0)
            {
                vm.ScheduledReviewerPool = "";
                vm.PoolStartDate = null;
                vm.PoolEndDate = null;
            }
            if (vm.HoursLodgeFinal == 0)
            {
                vm.ScheduledReviewerFacilities = "";
                vm.FacilStartDate = null;
                vm.FacilEndDate = null;
            }
            if (vm.HoursDayCareFinal == 0)
            {
                vm.ScheduledReviewerDayCare = "";
                vm.DaycStartDate = null;
                vm.DaycEndDate = null;
            }

            vm.ApptResponseStatusEnum = planReview.ApptResponseStatusEnum;

            //LES-2884 - if this was rejected by customer, remove start and end dates
            if (planReview.ProjectCycle.ID > 0 &&
                planReview.ProjectCycle.IsAprvInd.HasValue
                && planReview.ProjectCycle.IsAprvInd.Value == false)
            {
                vm.BuildStartDate = null;
                vm.BuildEndDate = null;
                vm.ElectStartDate = null;
                vm.ElectEndDate = null;
                vm.MechaStartDate = null;
                vm.MechaEndDate = null;
                vm.PlumbStartDate = null;
                vm.PlumbEndDate = null;
                vm.ZoneStartDate = null;
                vm.ZoneEndDate = null;
                vm.FireStartDate = null;
                vm.FireEndDate = null;
                vm.BackfStartDate = null;
                vm.BackfEndDate = null;
                vm.FoodStartDate = null;
                vm.FoodEndDate = null;
                vm.PoolStartDate = null;
                vm.PoolEndDate = null;
                vm.FacilStartDate = null;
                vm.FacilEndDate = null;
                vm.DaycStartDate = null;
                vm.DaycEndDate = null;
            }

            //We need this for comparison in the save view model so we can decide if we need to save the facilitator
            vm.PreviousAssignedFacilitator = vm.Project.AssignedFacilitator.HasValue ? vm.Project.AssignedFacilitator.Value : 0;

            return vm;
        }

        private SchedulePlanReviewViewModel PrepareFIFOPlanReviewData(ProjectParms parms, SchedulingModel model)
        {
            SchedulePlanReviewViewModel vm = new SchedulePlanReviewViewModel();
            ProjectEstimation basedata;
            DateTime today = DateTime.Now;
            Helper helper = new Helper();

            UIHelpers uihelper = new UIHelpers();
            APIHelper apihelper = new APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;
            parms.LoggedInUserEmail = _loggedinUser;

            if (parms.ProjectId == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_ID_Missing;
                return vm;
            }
            if (parms.LoggedInUserEmail == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Not_Logged_In;
                return vm;
            }

            vm.LoggedInUserEmail = parms.LoggedInUserEmail;

            UpdateUserAndPermissions(vm);

            //check perms, send blank vm with status message "insufficient permission"
            if (vm.PermissionMapping.Access_SchedulePR == false)
            {
                vm.StatusMessage = "Insufficient permission";
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                return vm;
            }

            vm.Holidays = GenerateViewModelHolidays(model.Holidays);

            basedata = model.ProjectEstimation;
            vm.Project = basedata;
            ProjectAgency fireagency = vm.Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            ProjectAgency zoneagency = vm.Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            vm.FireAgency = fireagency.DepartmentInfo;
            vm.ZoneAgency = zoneagency.DepartmentInfo;

            vm.PropertyType = vm.Project.AccelaPropertyType;
            //GET ALL INFO FOR PROJECT
            ProjectCycleSummary projectCycleSummary = model.ProjectCycleSummary;

            if (projectCycleSummary == null
                || projectCycleSummary.ProjectCycleCurrent == null
                || projectCycleSummary.ProjectCycleCurrent.ID == 0)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_Cycle_Missing;
                return vm;
            }

            vm.Cycle = projectCycleSummary.ProjectCycleCurrent.CycleNbr.Value;

            PlanReview planReview = new PlanReview();

            planReview.ProjectCycle = projectCycleSummary.ProjectCycleCurrent;

            if (projectCycleSummary.PlanReviewCurrent != null && projectCycleSummary.PlanReviewCurrent.ID > 0)
            {
                planReview = projectCycleSummary.PlanReviewCurrent;
                if ((planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                    || planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled))
                    vm.IsReschedule = true;

                if (planReview.IsReschedule) vm.IsRescheduleOverwrite = true;

                vm.PlanReviewScheduleId = planReview.PlanReviewScheduleId;
                vm.ProjectCycleId = planReview.ProjectCycle.ID;
                vm.GateDate = planReview.ProjectCycle.GateDt == null ? DateTime.MinValue : planReview.ProjectCycle.GateDt.Value;
            }
            else
            {
                vm.IsNewCycle = true;
                vm.IsCycleComparison = false;
                vm.IsAdjustHours = parms.IsAdjustHours;
            }

            vm.ApptResponseStatusEnum = planReview.ApptResponseStatusEnum;

            if ((planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                || planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled))
                vm.IsReschedule = true;
            if (planReview.IsReschedule) vm.IsRescheduleOverwrite = true;

            //get rereview times if this is a cycle
            vm.IsNewCycle = false;
            vm.IsCycleComparison = false;
            vm.IsAdjustHours = parms.IsAdjustHours;
            vm.GateDate = planReview.GateDate == null ? DateTime.MinValue : planReview.GateDate;
            vm.TeamScore = vm.Project.TeamGradeTxt;
            vm.UpdateDate = DateTime.Now;
            vm.FifoDueDt = planReview.FIFODueDate;
            vm.FifoScheduleId = planReview.ID;

            //determine if schedule future cycle is available
            vm.CanScheduleFutureCycle = false;
            vm.IsFutureCycle = false;

            //build vm for plan review schedules

            //build vm for project
            //LES-2979 jlindsay If this is not the first cycle, return blank datetime for the trades/agencies
            //For FIFO, we don't care about changing the dates, the only thing that can change is the reviewers
            //      IsNewCycle is always False for FIFO since we just want to display the dates for this review

            vm.BackfEndDate = ScheduleHelpers.GetCycleDate(planReview.BackfEndDate, vm.IsNewCycle);
            vm.BackfFifo = planReview.BackfFifo;
            vm.BackfPlanReviewScheduleId = planReview.BackfPlanReviewScheduleId;
            vm.BackfPool = planReview.BackfPool;
            vm.BackfPRSUpdateDate = planReview.BackfPRSUpdateDate;
            vm.BackfStartDate = ScheduleHelpers.GetCycleDate(planReview.BackfStartDate, vm.IsNewCycle);
            vm.AssignedHoursBackflow = planReview.HoursBackFlow;

            vm.BuildEndDate = ScheduleHelpers.GetCycleDate(planReview.BuildEndDate, vm.IsNewCycle);
            vm.BuildFifo = planReview.BuildFifo;
            vm.BuildPlanReviewScheduleId = planReview.BuildPlanReviewScheduleId;
            vm.BuildPool = planReview.BuildPool;
            vm.BuildPRSUpdateDate = planReview.BuildPRSUpdateDate;
            vm.BuildStartDate = ScheduleHelpers.GetCycleDate(planReview.BuildStartDate, vm.IsNewCycle);
            vm.AssignedHoursBuilding = planReview.HoursBuilding;

            vm.ElectEndDate = ScheduleHelpers.GetCycleDate(planReview.ElectEndDate, vm.IsNewCycle);
            vm.ElectFifo = planReview.ElectFifo;
            vm.ElectPlanReviewScheduleId = planReview.ElectPlanReviewScheduleId;
            vm.ElectPool = planReview.ElectPool;
            vm.ElectPRSUpdateDate = planReview.ElectPRSUpdateDate;
            vm.ElectStartDate = ScheduleHelpers.GetCycleDate(planReview.ElectStartDate, vm.IsNewCycle);
            vm.AssignedHoursElectric = planReview.HoursElectic;

            vm.PlumbEndDate = ScheduleHelpers.GetCycleDate(planReview.PlumbEndDate, vm.IsNewCycle);
            vm.PlumbFifo = planReview.PlumbFifo;
            vm.PlumbPlanReviewScheduleId = planReview.PlumbPlanReviewScheduleId;
            vm.PlumbPool = planReview.PlumbPool;
            vm.PlumbPRSUpdateDate = planReview.PlumbPRSUpdateDate;
            vm.PlumbStartDate = ScheduleHelpers.GetCycleDate(planReview.PlumbStartDate, vm.IsNewCycle);
            vm.AssignedHoursPlumb = planReview.HoursPlumb;

            vm.FireEndDate = ScheduleHelpers.GetCycleDate(planReview.FireEndDate, vm.IsNewCycle);
            vm.FireFifo = planReview.FireFifo;
            vm.FirePlanReviewScheduleId = planReview.FirePlanReviewScheduleId;
            vm.FirePool = planReview.FirePool;
            vm.FirePRSUpdateDate = planReview.FirePRSUpdateDate;
            vm.FireStartDate = ScheduleHelpers.GetCycleDate(planReview.FireStartDate, vm.IsNewCycle);
            vm.AssignedHoursFire = planReview.HoursFire;

            vm.ZoneEndDate = ScheduleHelpers.GetCycleDate(planReview.ZoneEndDate, vm.IsNewCycle);
            vm.ZoneFifo = planReview.ZoneFifo;
            vm.ZonePlanReviewScheduleId = planReview.ZonePlanReviewScheduleId;
            vm.ZonePool = planReview.ZonePool;
            vm.ZonePRSUpdateDate = planReview.ZonePRSUpdateDate;
            vm.ZoneStartDate = ScheduleHelpers.GetCycleDate(planReview.ZoneStartDate, vm.IsNewCycle);
            vm.AssignedHoursZoning = planReview.HoursZoning;

            vm.PoolEndDate = ScheduleHelpers.GetCycleDate(planReview.PoolEndDate, vm.IsNewCycle);
            vm.PoolFifo = planReview.PoolFifo;
            vm.PoolPlanReviewScheduleId = planReview.PoolPlanReviewScheduleId;
            vm.PoolPool = planReview.PoolPool;
            vm.PoolPRSUpdateDate = planReview.PoolPRSUpdateDate;
            vm.PoolStartDate = ScheduleHelpers.GetCycleDate(planReview.PoolStartDate, vm.IsNewCycle);
            vm.AssignedHoursPool = planReview.HoursPool;

            vm.MechaEndDate = ScheduleHelpers.GetCycleDate(planReview.MechaEndDate, vm.IsNewCycle);
            vm.MechaFifo = planReview.MechaFifo;
            vm.MechaPlanReviewScheduleId = planReview.MechaPlanReviewScheduleId;
            vm.MechaPool = planReview.MechaPool;
            vm.MechaPRSUpdateDate = planReview.MechaPRSUpdateDate;
            vm.MechaStartDate = ScheduleHelpers.GetCycleDate(planReview.MechaStartDate, vm.IsNewCycle);
            vm.AssignedHoursMech = planReview.HoursMech;

            vm.FoodEndDate = ScheduleHelpers.GetCycleDate(planReview.FoodEndDate, vm.IsNewCycle);
            vm.FoodFifo = planReview.FoodFifo;
            vm.FoodPlanReviewScheduleId = planReview.FoodPlanReviewScheduleId;
            vm.FoodPool = planReview.FoodPool;
            vm.FoodPRSUpdateDate = planReview.FoodPRSUpdateDate;
            vm.FoodStartDate = ScheduleHelpers.GetCycleDate(planReview.FoodStartDate, vm.IsNewCycle);
            vm.AssignedHoursFood = planReview.HoursFood;

            vm.FacilEndDate = ScheduleHelpers.GetCycleDate(planReview.FacilEndDate, vm.IsNewCycle);
            vm.FacilFifo = planReview.FacilFifo;
            vm.FacilPlanReviewScheduleId = planReview.FacilPlanReviewScheduleId;
            vm.FacilPool = planReview.FacilPool;
            vm.FacilPRSUpdateDate = planReview.FacilPRSUpdateDate;
            vm.FacilStartDate = ScheduleHelpers.GetCycleDate(planReview.FacilStartDate, vm.IsNewCycle);
            vm.AssignedHoursLodge = planReview.HoursLodge;

            vm.DaycEndDate = ScheduleHelpers.GetCycleDate(planReview.DaycEndDate, vm.IsNewCycle);
            vm.DaycFifo = planReview.DaycFifo;
            vm.DaycPlanReviewScheduleId = planReview.DaycPlanReviewScheduleId;
            vm.DaycPool = planReview.DaycPool;
            vm.DaycPRSUpdateDate = planReview.DaycPRSUpdateDate;
            vm.DaycStartDate = ScheduleHelpers.GetCycleDate(planReview.DaycStartDate, vm.IsNewCycle);
            vm.AssignedHoursDayCare = planReview.HoursDayCare;

            vm.FacilitatorList = apihelper.GetAllFacilitators();
            //If this is express, only allow reviewers that have IsExpressSched = true
            bool isExpressProject = (vm.Project.AccelaPropertyType == PropertyTypeEnums.Express);
            vm.AllReviewers = vm.Project.Reviewers;
            int projectTypeEnum = (int)vm.Project.AccelaPropertyType;

            // for zone reviewers, possible get all FIFO property type
            vm.ZoningJurisdictionReviewers = model.ZoningJurisdictionReviewers.DistinctBy(x => x.ID).ToList();

            // set assigned reviewers
            foreach (AttendeeInfo attendee in planReview.AssignedReviewers)
            {
                switch (attendee.BusinessRefId)
                {
                    case (int)DepartmentNameEnums.Backflow:
                        vm.ScheduledReviewerBackFlow = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.Building:
                        vm.ScheduledReviewerBuilding = attendee.AttendeeId.ToString();

                        break;
                    case (int)DepartmentNameEnums.Electrical:
                        vm.ScheduledReviewerElectrical = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.Plumbing:
                        vm.ScheduledReviewerPlumbing = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.Fire_Cornelius:
                    case (int)DepartmentNameEnums.Fire_Cty_Chrlt:
                    case (int)DepartmentNameEnums.Fire_Davidson:
                    case (int)DepartmentNameEnums.Fire_Huntersville:
                    case (int)DepartmentNameEnums.Fire_Matthews:
                    case (int)DepartmentNameEnums.Fire_Mint_Hill:
                    case (int)DepartmentNameEnums.Fire_Pineville:
                    case (int)DepartmentNameEnums.Fire_UMC:
                    case (int)DepartmentNameEnums.Fire_County:
                        vm.ScheduledReviewerFire = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.Zone_Cornelius:
                    case (int)DepartmentNameEnums.Zone_Cty_Chrlt:
                    case (int)DepartmentNameEnums.Zone_Davidson:
                    case (int)DepartmentNameEnums.Zone_Huntersville:
                    case (int)DepartmentNameEnums.Zone_Matthews:
                    case (int)DepartmentNameEnums.Zone_Mint_Hill:
                    case (int)DepartmentNameEnums.Zone_Pineville:
                    case (int)DepartmentNameEnums.Zone_UMC:
                    case (int)DepartmentNameEnums.Zone_County:
                        vm.ScheduledReviewerZone = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.EH_Pool:
                        vm.ScheduledReviewerPool = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.Mechanical:
                        vm.ScheduledReviewerMechanical = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.EH_Food:
                        vm.ScheduledReviewerFood = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.EH_Facilities:
                        vm.ScheduledReviewerFacilities = attendee.AttendeeId.ToString();
                        break;
                    case (int)DepartmentNameEnums.EH_Day_Care:
                        vm.ScheduledReviewerDayCare = attendee.AttendeeId.ToString();
                        break;
                }
            }

            vm.NotesComments = model.Notes;

            //TODO: if mandatory notes type has content, add to the view model, otherwishe, get the standard note for mandatory by project type
            List<Note> mandatorynotes = vm.NotesComments.Where(x => x.NotesType.Type == NoteTypeEnum.SchedulingMandatoryNotes).ToList();
            if (mandatorynotes != null && !mandatorynotes.Any())
            {
                List<StandardNote> standardmandatorynotes = model.MandatoryNotes;
                if (standardmandatorynotes != null && standardmandatorynotes.Count() > 0)
                    vm.MandatorySchedulingNotes = standardmandatorynotes.FirstOrDefault().StandardNoteText;
            }
            else
            {
                vm.MandatorySchedulingNotes = mandatorynotes.FirstOrDefault().NotesComments;
            }

            vm.StandardNotes = model.StandardNotes;
            vm.StandardNoteGroups = model.StandardNoteGroupEnums;

            vm.PlansReadyOnDate = planReview.ProdDate;

            //uihelper.SetPermissionsForEstimationUI(vm);//TODO
            //removes underscore from Action params. this will avoid %20 from links but with keeping the message readable
            vm.StatusMessage = parms.StatusMessage != null ? parms.StatusMessage.Replace('_', ' ') : "";
            vm.ExcludedPlanReviewersBuild.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersElectric.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersMech.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersPlumb.AddRange(basedata.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersFire.AddRange(basedata.Agencies.Where(x => vm.FireDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersZone.AddRange(basedata.Agencies.Where(x => vm.ZoneDepartmentNames.Contains(x.DepartmentInfo))
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersDayCare.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersFood.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersLodge.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersBackFlow.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow)
                .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));
            vm.ExcludedPlanReviewersPool.AddRange(basedata.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool)
                            .SelectMany(x => x.ExcludedPlanReviewers.Select(y => y.ToString())));

            //Set final plan review hours based on estimated, rereview, and adjust hours selections
            vm.HoursBuildingFinal = vm.HoursBuilding;
            vm.HoursElecticFinal = vm.HoursElectic;
            vm.HoursMechFinal = vm.HoursMech;
            vm.HoursPlumbFinal = vm.HoursPlumb;
            vm.HoursZoningFinal = vm.HoursZoning;
            vm.HoursFireFinal = vm.HoursFire;
            vm.HoursBackFlowFinal = vm.HoursBackFlow;
            vm.HoursFoodFinal = vm.HoursFood;
            vm.HoursPoolFinal = vm.HoursPool;
            vm.HoursLodgeFinal = vm.HoursLodge;
            vm.HoursDayCareFinal = vm.HoursDayCare;

            //clear dates and selected reviewers for any depts that are set to 0 hours
            if (vm.HoursBuildingFinal == 0)
            {
                vm.ScheduledReviewerBuilding = "";
                vm.BuildStartDate = null;
                vm.BuildEndDate = null;
            }
            if (vm.HoursElecticFinal == 0)
            {
                vm.ScheduledReviewerElectrical = "";
                vm.ElectStartDate = null;
                vm.ElectEndDate = null;
            }
            if (vm.HoursMechFinal == 0)
            {
                vm.ScheduledReviewerMechanical = "";
                vm.MechaStartDate = null;
                vm.MechaEndDate = null;
            }
            if (vm.HoursPlumbFinal == 0)
            {
                vm.ScheduledReviewerPlumbing = "";
                vm.PlumbStartDate = null;
                vm.PlumbEndDate = null;
            }
            if (vm.HoursZoningFinal == 0)
            {
                vm.ScheduledReviewerZone = "";
                vm.ZoneStartDate = null;
                vm.ZoneEndDate = null;
            }
            if (vm.HoursFireFinal == 0)
            {
                vm.ScheduledReviewerFire = "";
                vm.FireStartDate = null;
                vm.FireEndDate = null;
            }
            if (vm.HoursBackFlowFinal == 0)
            {
                vm.ScheduledReviewerBackFlow = "";
                vm.BackfStartDate = null;
                vm.BackfEndDate = null;
            }
            if (vm.HoursFoodFinal == 0)
            {
                vm.ScheduledReviewerFood = "";
                vm.FoodStartDate = null;
                vm.FoodEndDate = null;
            }
            if (vm.HoursPoolFinal == 0)
            {
                vm.ScheduledReviewerPool = "";
                vm.PoolStartDate = null;
                vm.PoolEndDate = null;
            }
            if (vm.HoursLodgeFinal == 0)
            {
                vm.ScheduledReviewerFacilities = "";
                vm.FacilStartDate = null;
                vm.FacilEndDate = null;
            }
            if (vm.HoursDayCareFinal == 0)
            {
                vm.ScheduledReviewerDayCare = "";
                vm.DaycStartDate = null;
                vm.DaycEndDate = null;
            }

            //We need this for comparison in the save view model so we can decide if we need to save the facilitator
            vm.PreviousAssignedFacilitator = vm.Project.AssignedFacilitator.HasValue ? vm.Project.AssignedFacilitator.Value : 0;

            return vm;
        }

        private SchedulePlanReviewViewModel PrepareExpressData(ProjectParms parms, SchedulingModel model)
        {
            SchedulePlanReviewViewModel vm = new SchedulePlanReviewViewModel();
            ProjectEstimation basedata;
            DateTime today = DateTime.Now;

            UIHelpers uihelper = new UIHelpers();
            APIHelper apihelper = new APIHelper();
            Helper helper = new Helper();

            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;
            parms.LoggedInUserEmail = _loggedinUser;

            if (parms.ProjectId == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_ID_Missing;
                return vm;
            }
            if (parms.LoggedInUserEmail == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Not_Logged_In;
                return vm;
            }

            vm.LoggedInUserEmail = parms.LoggedInUserEmail;

            UpdateUserAndPermissions(vm);

            //check perms, send blank vm with status message "insufficient permission"
            if (vm.PermissionMapping.Access_ScheduleExpress == false)
            {
                vm.StatusMessage = "Insufficient permission";
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                return vm;
            }

            vm.Holidays = GenerateViewModelHolidays(model.Holidays);

            basedata = model.ProjectEstimation;
            vm.Project = basedata;

            ProjectAgency fireagency = vm.Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            ProjectAgency zoneagency = vm.Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            vm.FireAgency = fireagency.DepartmentInfo;
            vm.ZoneAgency = zoneagency.DepartmentInfo;

            vm.AllReviewers = vm.Project.Reviewers;

            ProjectCycleSummary projectCycleSummary = model.ProjectCycleSummary;

            if (projectCycleSummary == null
                || projectCycleSummary.ProjectCycleCurrent == null
                || projectCycleSummary.ProjectCycleCurrent.ID == 0)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_Cycle_Missing;
                return vm;
            }

            vm.Cycle = projectCycleSummary.ProjectCycleCurrent.CycleNbr.Value;

            PlanReview planReview = new PlanReview();
            planReview.ProjectCycle = projectCycleSummary.ProjectCycleCurrent;

            if (projectCycleSummary.PlanReviewCurrent != null && projectCycleSummary.PlanReviewCurrent.ID > 0)
            {
                planReview = projectCycleSummary.PlanReviewCurrent;
                if ((planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                    || planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled))
                    vm.IsReschedule = true;

                if (planReview.IsReschedule) vm.IsRescheduleOverwrite = true;

                vm.PlanReviewScheduleId = planReview.PlanReviewScheduleId;
                vm.ProjectCycleId = planReview.ProjectCycle.ID;
                vm.GateDate = planReview.ProjectCycle.GateDt == null ? DateTime.MinValue : planReview.ProjectCycle.GateDt.Value;

                vm.ProposedDate1 = planReview.ProposedDate1;
                vm.ProposedDate2 = planReview.ProposedDate2;
                vm.ProposedDate3 = planReview.ProposedDate3;
            }
            else
            {
                vm.IsNewCycle = true;
                vm.IsCycleComparison = false;
                vm.IsAdjustHours = parms.IsAdjustHours;
            }

            vm.TeamScore = vm.Project.TeamGradeTxt;
            vm.IsFutureCycle = false;

            vm.FacilitatorList = model.Facilitators;
            vm.MeetingRoomList = model.MeetingRooms;

            //If this is express, only allow reviewers that have IsExpressSched = true
            bool isExpressProject = (vm.Project.AccelaPropertyType == PropertyTypeEnums.Express);

            int projectTypeEnum = (int)vm.Project.AccelaPropertyType;

            List<ReserveExpressReservation> expressReservationList = model.ReserveExpressReservations; //apihelper.GetExpressReservationList();
            StringBuilder reserveexprsb = new StringBuilder();
            foreach (ReserveExpressReservation exp in expressReservationList)
            {
                reserveexprsb.Append(exp.ReserveExpressDt.ToShortDateString());
                reserveexprsb.Append(",");
            }

            //*******get notes data
            vm.NotesComments = model.Notes;

            //TODO: if mandatory notes type has content, add to the view model, otherwishe, get the standard note for mandatory by project type
            List<Note> mandatorynotes = vm.NotesComments.Where(x => x.NotesType.Type == NoteTypeEnum.SchedulingMandatoryNotes).ToList();
            if (mandatorynotes != null && !mandatorynotes.Any())
            {
                List<StandardNote> standardmandatorynotes = model.MandatoryNotes;
                if (standardmandatorynotes != null && standardmandatorynotes.Count() > 0)
                    vm.MandatorySchedulingNotes = standardmandatorynotes.FirstOrDefault().StandardNoteText;
            }
            else
            {
                vm.MandatorySchedulingNotes = mandatorynotes.FirstOrDefault().NotesComments;
            }

            vm.StandardNotes = model.StandardNotes;
            vm.StandardNoteGroups = model.StandardNoteGroupEnums;

            //uihelper.SetPermissionsForEstimationUI(vm);//TODO
            //removes underscore from Action params. this will avoid %20 from links but with keeping the message readable
            vm.StatusMessage = parms.StatusMessage != null ? parms.StatusMessage.Replace('_', ' ') : "";

            //set express specific info
            vm.ReservedExpressDates = reserveexprsb.ToString();
            vm.MaxTime = expressReservationList.Select(x => x.EndTime).FirstOrDefault().ToString("hh:mm tt");
            vm.MinTime = expressReservationList.Select(x => x.StartTime).FirstOrDefault().ToString("hh:mm tt");

            if (vm.MaxTime == DateTime.MinValue.ToString("hh:mm tt")) vm.MaxTime = new DateTime(1, 1, 1, 16, 45, 0).ToString("hh:mm tt");
            if (vm.MinTime == DateTime.MinValue.ToString("hh:mm tt")) vm.MinTime = new DateTime(1, 1, 1, 8, 30, 0).ToString("hh:mm tt");

            vm.Cycle = projectCycleSummary.ProjectCycleCurrent.CycleNbr.Value;

            if (projectCycleSummary.PlanReviewScheduleDetailsCurrent != null)
            {
                if (projectCycleSummary.PlanReviewScheduleDetailsCurrent.Count > 0)
                {
                    var currentPlanReview = projectCycleSummary.PlanReviewScheduleDetailsCurrent.FirstOrDefault();

                    if (currentPlanReview.StartDt != null)
                    {
                        vm.StartTime = currentPlanReview.StartDt.Value;
                    }
                    if (currentPlanReview.EndDt != null)
                    {
                        vm.EndTime = currentPlanReview.EndDt.Value;
                    }

                    vm.ScheduleDate = vm.StartTime;
                }


                //TODO: PlanReview convert meeting room like EMA
                if (projectCycleSummary.PlanReviewCurrent != null)
                {
                    if (projectCycleSummary.PlanReviewCurrent.MeetingRoomRefId != null)
                    {
                        if (vm.MeetingRoomList.Contains(projectCycleSummary.PlanReviewCurrent.MeetingRoom))
                        {
                            vm.MeetingRoomNameSelected = projectCycleSummary.PlanReviewCurrent.MeetingRoom.MeetingRoomName;
                            vm.MeetingRoomRefIDSelected = projectCycleSummary.PlanReviewCurrent.MeetingRoom.MeetingRoomRefID.Value;
                        }
                    }

                    vm.UpdateDate = projectCycleSummary.PlanReviewCurrent.UpdatedDate;

                    if (projectCycleSummary.PlanReviewCurrent.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled
                        || projectCycleSummary.PlanReviewCurrent.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled)
                        vm.IsReschedule = true;
                }
                else
                {
                    vm.IsNewCycle = true;
                }

                //get rereview times if this is a cycle
                vm.IsNewCycle = false;

                if (vm.Cycle > 1)
                {
                    foreach (ProjectCycleDetail item in projectCycleSummary.ProjectCycleDetailsCurrent)
                    {
                        switch (item.BusinessRefId)
                        {
                            case (int)DepartmentNameEnums.Building:
                                vm.ReReviewBuilding = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.Electrical:
                                vm.ReReviewElectric = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.Mechanical:
                                vm.ReReviewMech = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.Plumbing:
                                vm.ReReviewPlumb = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.Fire_Cornelius:
                                vm.ReReviewFire = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.Zone_Cornelius:
                                vm.ReReviewZoning = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.EH_Day_Care:
                                vm.ReReviewDayCare = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.EH_Facilities:
                                vm.ReReviewLodge = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.EH_Food:
                                vm.ReReviewFood = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.EH_Pool:
                                vm.ReReviewPool = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            case (int)DepartmentNameEnums.Backflow:
                                vm.ReReviewBackFlow = Math.Round((decimal)item.RereviewHoursNbr, 1, MidpointRounding.AwayFromZero);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                vm.IsNewCycle = true;
                vm.ScheduleDate = (DateTime?)null;
                vm.EndTime = (DateTime?)null;
                vm.StartTime = (DateTime?)null;
            }

            //determine PROD
            if (vm.IsNewCycle)
            {
                vm.PlansReadyOnDate = vm.Project.PlansReadyOnDate;
            }
            else
            {
                vm.PlansReadyOnDate = projectCycleSummary.ProjectCycleCurrent.PlansReadyOnDt;
            }

            //LES-2884 - if this was rejected by customer, remove start and end dates
            if (projectCycleSummary.ProjectCycleCurrent.ID > 0
                && projectCycleSummary.ProjectCycleCurrent.IsAprvInd.HasValue
                && projectCycleSummary.ProjectCycleCurrent.IsAprvInd.Value == false)
            {
                vm.ScheduleDate = (DateTime?)null;
                vm.EndTime = (DateTime?)null;
                vm.StartTime = (DateTime?)null;
            }

            //We need this for comparison in the save view model so we can decide if we need to save the facilitator
            vm.PreviousAssignedFacilitator = vm.Project.AssignedFacilitator.HasValue ? vm.Project.AssignedFacilitator.Value : 0;

            return vm;
        }

        private ScheduleMeetingViewModel PrepareMeetingData(ProjectParms parms, SchedulingModel model)
        {
            ScheduleMeetingViewModel vm = new ScheduleMeetingViewModel();
            ProjectEstimation basedata;
            DateTime today = DateTime.Now;

            UIHelpers uihelper = new UIHelpers();
            APIHelper apihelper = new APIHelper();
            Helper helper = new Helper();

            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;
            parms.LoggedInUserEmail = _loggedinUser;

            if (parms.ProjectId == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Project_ID_Missing;
                return vm;
            }
            if (parms.LoggedInUserEmail == null)
            {
                vm.UIStatusMessage = UIStatusMessage.Not_Logged_In;
                return vm;
            }

            vm.LoggedInUserEmail = parms.LoggedInUserEmail;

            UpdateUserAndPermissions(vm);

            ////check perms, send blank vm with status message "insufficient permission"
            if (vm.PermissionMapping.Schdul_Mtng == false)
            {
                vm.StatusMessage = "Insufficient permission";
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                return vm;
            }

            basedata = model.ProjectEstimation;
            vm.Project = basedata;

            ProjectAgency fireagency = vm.Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            ProjectAgency zoneagency = vm.Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            vm.FireAgency = fireagency.DepartmentInfo;
            vm.ZoneAgency = zoneagency.DepartmentInfo;

            vm.FacilitatorList = model.Facilitators;
            //If this is express, only allow reviewers that have IsExpressSched = true
            bool isExpressProject = (vm.Project.AccelaPropertyType == PropertyTypeEnums.Express);
            vm.AllReviewers = vm.Project.Reviewers;
            int projectTypeEnum = (int)vm.Project.AccelaPropertyType;

            if (vm.MaxTime == DateTime.MinValue.ToString("hh:mm tt")) vm.MaxTime = new DateTime(1, 1, 1, 16, 45, 0).ToString("hh:mm tt");
            if (vm.MinTime == DateTime.MinValue.ToString("hh:mm tt")) vm.MinTime = new DateTime(1, 1, 1, 8, 0, 0).ToString("hh:mm tt");

            //*******get notes data
            vm.NotesComments = model.Notes;

            //TODO: if mandatory notes type has content, add to the view model, otherwishe, get the standard note for mandatory by project type
            List<Note> mandatorynotes = vm.NotesComments.Where(x => x.NotesType.Type == NoteTypeEnum.SchedulingMandatoryNotes).ToList();
            if (mandatorynotes != null && !mandatorynotes.Any())
            {
                List<StandardNote> standardmandatorynotes = model.MandatoryNotes;
                if (standardmandatorynotes != null && standardmandatorynotes.Count() > 0)
                    vm.MandatorySchedulingNotes = standardmandatorynotes.FirstOrDefault().StandardNoteText;
            }
            else
            {
                vm.MandatorySchedulingNotes = mandatorynotes.FirstOrDefault().NotesComments;
            }

            vm.StandardNotes = model.StandardNotes;
            vm.StandardNoteGroups = model.StandardNoteGroupEnums;

            vm.StatusMessage = parms.StatusMessage != null ? parms.StatusMessage.Replace('_', ' ') : "";
            vm.MeetingRoomList = model.MeetingRooms;

            vm.UserIdentities = model.Users;

            return vm;
        }

        private string GenerateAccelaDeeplink(string id)
        {
            string env = ConfigurationManager.AppSettings["Environment"].ToString();
            if (env.ToLower().Equals("tst")) id = String.Join("", id.Split('-')).ToLower();
            string url = ConfigurationManager.AppSettings["AccelaBaseApplicationDeeplink"].ToString();

            url = url.Replace("{AccelaId}", Server.UrlEncode(id));

            return url;
        }

        #endregion Private Methods
    }
}