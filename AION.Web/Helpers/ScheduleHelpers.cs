using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Models;
using AION.Web.Models.ProjectDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Helpers
{
    public class ScheduleHelpers
    {
        public enum ReviewerType { Primary, Secondary, Excluded };

        public NonProjectAppointment CreateSaveModelForNPA(NPACreateViewModel vm)
        {
            List<ApptAttendeeManagerModel> attendeeIds = new List<ApptAttendeeManagerModel>();
            AppointmentRecurrenceRefEnum recur = new AppointmentRecurrenceRefEnum().CreateInstance(vm.DaySelected, vm.RecurrenceSelected);
            //put together date and time
            string dt = vm.StartDate.ToShortDateString();
            string dt2 = vm.EndDate.ToShortDateString();
            string tm = vm.StartTime.ToShortTimeString();
            string tm2 = vm.EndTime.ToShortTimeString();
            if (vm.AllDay)
            {
                tm = "08:00 AM";
                tm2 = "05:00 PM";
            }
            DateTime s = DateTime.Parse(dt + ' ' + tm);
            DateTime e = DateTime.Parse(dt2 + ' ' + tm2);


            DateTime start = s;
            DateTime end = e;

            bool isallplanreviewers = (vm.YNSelected == "Y");
            int? meetingroomrefid = null;
            if (vm.MeetingRoomRefIDSelected != 0) meetingroomrefid = vm.MeetingRoomRefIDSelected;
            DateTime today = DateTime.Now;
            //check arrays for null
            //add function for this

            NonProjectAppointment npa = new NonProjectAppointment
            {
                NonProjectAppointmentID = 0,
                NPATypeRefID = vm.NPATypeSelected,
                AppoinmentName = vm.NPAName,
                //AppoinmentRecurrenceRefID = (int)recur,
                AppointmentRecurrence = recur,
                AppointmentFrom = start,
                AppointmentTo = end,
                CreatedUser = vm.UpdatingUser,
                CreatedDate = today,
                UpdatedUser = vm.UpdatingUser,
                UpdatedDate = today,
                IsAllBackFlow = vm.BackSelectAll,
                IsAllBuild = vm.BldgSelectAll,
                IsAllDay = vm.AllDay,
                IsAllEhsDayCare = vm.DayCSelectAll,
                IsAllEhsFood = vm.FoodSelectAll,
                IsAllEhsLodge = vm.FaciSelectAll,
                IsAllEhsPool = vm.PoolSelectAll,
                IsAllElectric = vm.ElecSelectAll,
                IsAllFire = vm.FireSelectAll,
                IsAllMech = vm.MechSelectAll,
                IsAllPlanReviewers = isallplanreviewers,
                IsAllPlumb = vm.PlumSelectAll,
                IsAllZoning = vm.ZoniSelectAll,
                MeetingRoomRefId = meetingroomrefid,
                UserId = vm.UpdatingUser.ID.ToString()
            };

            return npa;
        }
        public List<ApptAttendeeManagerModel> GetReviewerListByDept(NPACreateViewModel vm)
        {
            List<ApptAttendeeManagerModel> allreviewers = new List<ApptAttendeeManagerModel>();
            foreach (var item in GetList(vm.ElecReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Electrical });
            }
            foreach (var item in GetList(vm.BldgReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Building });
            }
            foreach (var item in GetList(vm.MechReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical });
            }
            foreach (var item in GetList(vm.PlumReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing });
            }
            foreach (var item in GetList(vm.PoolReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool });
            }
            foreach (var item in GetList(vm.ZoniReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Zone_Davidson });
            }
            foreach (var item in GetList(vm.DayCReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care });
            }
            foreach (var item in GetList(vm.FireReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Fire_Davidson });
            }
            foreach (var item in GetList(vm.FaciReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities });
            }
            foreach (var item in GetList(vm.BackReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.Backflow });
            }
            foreach (var item in GetList(vm.FoodReviewerSelected))
            {
                allreviewers.Add(new ApptAttendeeManagerModel { AttendeeId = int.Parse(item), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food });
            }

            return allreviewers.Where(x => x.AttendeeId > 0).ToList();
        }

        public int GetDepartmentNameEnumsByBusRefId(int businessrefid)
        {
            DepartmentNameEnums e = (DepartmentNameEnums)businessrefid;
            return (int)e;
        }
        public string GetDepartmentNameListEnumByBusRefId(int businessrefid)
        {
            DepartmentNameEnums e = (DepartmentNameEnums)businessrefid;
            switch (e)
            {
                case DepartmentNameEnums.Building:
                    return DepartmentNameList.Building;
                case DepartmentNameEnums.Electrical:
                    return DepartmentNameList.Electrical;
                case DepartmentNameEnums.Mechanical:
                    return DepartmentNameList.Mechanical;
                case DepartmentNameEnums.Plumbing:
                    return DepartmentNameList.Plumbing;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return DepartmentNameList.Zoning;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return DepartmentNameList.Fire;
                case DepartmentNameEnums.EH_Day_Care:
                    return DepartmentNameList.Day_Care;
                case DepartmentNameEnums.EH_Food:
                    return DepartmentNameList.Food_Service;
                case DepartmentNameEnums.EH_Pool:
                    return DepartmentNameList.Public_Pool;
                case DepartmentNameEnums.EH_Facilities:
                    return DepartmentNameList.Facility_Lodging;
                case DepartmentNameEnums.Backflow:
                    return DepartmentNameList.Backflow;
                default:
                    break;
            }
            return string.Empty;
        }

        public static string GetDepartmentNameListEnumByBusRefIdWSOI(int businessrefid)
        {
            DepartmentNameEnums e = (DepartmentNameEnums)businessrefid;
            switch (e)
            {
                case DepartmentNameEnums.Building:
                    return DepartmentNameList.Building;
                case DepartmentNameEnums.Electrical:
                    return DepartmentNameList.Electrical;
                case DepartmentNameEnums.Mechanical:
                    return DepartmentNameList.Mechanical;
                case DepartmentNameEnums.Plumbing:
                    return DepartmentNameList.Plumbing;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_County:
                    return DepartmentNameList.Zoning;
                case DepartmentNameEnums.Zone_Mint_Hill:
                    return DepartmentNameList.Zoning_MintHill;

                case DepartmentNameEnums.Zone_Huntersville:
                    return DepartmentNameList.Zoning_Huntersville;

                case DepartmentNameEnums.Zone_Cty_Chrlt:
                    return DepartmentNameList.Zoning_Charlotte;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_County:
                    return DepartmentNameList.Fire;
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                    return DepartmentNameList.Fire_Charlotte;
                case DepartmentNameEnums.EH_Day_Care:
                    return DepartmentNameList.Day_Care;
                case DepartmentNameEnums.EH_Food:
                    return DepartmentNameList.Food_Service;
                case DepartmentNameEnums.EH_Pool:
                    return DepartmentNameList.Public_Pool;
                case DepartmentNameEnums.EH_Facilities:
                    return DepartmentNameList.Facility_Lodging;
                case DepartmentNameEnums.Backflow:
                    return DepartmentNameList.Backflow;
                default:
                    break;
            }
            return string.Empty;
        }

        public List<ApptAttendeeManagerModel> BuildAttendeesFromAttendeeInfo(List<AttendeeInfo> attendees)
        {
            List<ApptAttendeeManagerModel> items = new List<ApptAttendeeManagerModel>();
            foreach (AttendeeInfo item in attendees)
            {
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = item.AttendeeId,
                    DeptNameEnumId = GetDepartmentNameEnumsByBusRefId(item.BusinessRefId),
                    BusinessRefId = item.BusinessRefId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DeptNameListId = GetDepartmentNameListEnumByBusRefIdWSOI(item.BusinessRefId)
                });
            }
            var distinctItems = items.GroupBy(x => new { x.AttendeeId, x.BusinessRefId }).Select(y => y.First());
            return distinctItems.ToList();
        }

        public static DateTime? GetDateTimeByFifoPool(bool? isFifo, bool? isPool, DateTime? indate)
        {
            DateTime? returndate = (DateTime?)null;
            bool fifo = isFifo.HasValue;
            bool pool = isPool.HasValue;
            //indate is ms min date
            if (indate.HasValue && indate.Value == DateTime.MinValue)
            {
                return returndate;
            }
            //not fifo or pool
            if (!fifo && !pool) return indate;

            //fifo value is true, return null date
            if (fifo)
            {
                if (isFifo.Value == true) return returndate;
            }
            //pool value is true, return null date
            if (pool)
            {
                if (isPool.Value == true) return returndate;
            }

            return indate;
        }

        /// <summary>
        /// Used by SavePreliminaryMeeting to get the lists for reviewers
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="reviewerType"></param>
        /// <returns></returns>
        public static List<AttendeeInfo> GetReviewerListByDept(ScheduleSaveViewModel vm, ReviewerType reviewerType)
        {
            List<AttendeeInfo> allreviewers = new List<AttendeeInfo>();
            if (reviewerType == ReviewerType.Primary)
            {
                //primary reviewers
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerFire), DeptNameEnumId = (int)vm.FireAgency });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.PrimaryReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care });

            }
            else
            {
                //secondary reviewers
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerelectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerFire), DeptNameEnumId = (int)vm.FireAgency });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities });
                allreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.SecondaryReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care });
            }

            return allreviewers.ToList();
        }

        public static string GetReReviewerName(List<SelectListItem> reviewers, int cycleReviewerId)
        {
            string reviewerName = "";

            SelectListItem reviewer = reviewers.FirstOrDefault(x => x.Value == cycleReviewerId.ToString());

            if (reviewer != null)
            {
                reviewerName = reviewer.Text;
            }

            return reviewerName;
        }

        /// <summary>
        /// //LES-2979 jlindsay If this is not the first cycle, return blank datetime for the trades/agencies
        /// </summary>
        /// <param name="indate"></param>
        /// <param name="isNewCycle"></param>
        /// <returns></returns>
        public static DateTime? GetCycleDate(DateTime? indate, bool isNewCycle, bool? previouslyRejected = false)
        {
            if (isNewCycle == true) return (DateTime?)null;
            if (previouslyRejected != null && previouslyRejected == true) return (DateTime?)null;
            return indate;
        }

        public static List<Reviewer> GetPreliminaryReviewers(List<Reviewer> reviewers)
        {
            return reviewers.Where(x => x.IsPrelimMeetingAllowed == true).ToList();
        }

        /// <summary>
        /// LES 4028, LES-4029 - add reviewer list
        /// </summary>
        /// <param name="attendeeInfos"></param>
        /// <returns></returns>
        public static List<AssignedReviewerListViewModel> BuildListFromAttendeeInfo(List<AttendeeInfo> attendeeInfos)
        {
            List<AssignedReviewerListViewModel> assignedReviewerListViewModels = new List<AssignedReviewerListViewModel>();
            foreach (AttendeeInfo attendee in attendeeInfos)
            {
                DepartmentDivisionEnum departmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)attendee.BusinessRefId);
                DepartmentNameEnums departmentName = (DepartmentNameEnums)(int)attendee.BusinessRefId;
                string trade = "";
                switch (departmentName)
                {
                    case DepartmentNameEnums.EH_Day_Care:
                    case DepartmentNameEnums.EH_Food:
                    case DepartmentNameEnums.EH_Pool:
                    case DepartmentNameEnums.EH_Facilities:
                        trade = ((ShortDepartmentNameEnums)attendee.BusinessRefId).ToStringValue();
                        break;
                    default:
                        trade = departmentDivisionEnum.ToStringValue();
                        break;
                }
                assignedReviewerListViewModels.Add(new AssignedReviewerListViewModel
                {
                    ReviewerName = attendee.FirstName + " " + attendee.LastName,
                    TradeName = trade
                });

            }
            return assignedReviewerListViewModels;
        }

        public static string BuildDateRangeString(DateTime startDt, DateTime endDt)
        {
            if (startDt.Date != endDt.Date)
            {
                return startDt.ToString("MM/dd/yyyy") + " - " + endDt.ToString("MM/dd/yyyy");
            }
            else
            {
                return startDt.ToString("MM/dd/yyyy");
            }
        }
        #region Private Methods

        private List<string> GetList(List<string> items)
        {
            return items == null ? new List<string>() : items;
        }
        #endregion Private Methods
    }
}