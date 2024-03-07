using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models.Scheduling
{
    public class ScheduleCapacityViewModel : ViewModelBase
    {
        private List<SelectListItem> _bldgPersonSelectList;
        private List<SelectListItem> _elecPersonSelectList;
        private List<SelectListItem> _mechPersonSelectList;
        private List<SelectListItem> _plumPersonSelectList;
        private List<SelectListItem> _zoniPersonSelectList;
        private List<SelectListItem> _firePersonSelectList;
        private List<SelectListItem> _backPersonSelectList;
        private List<SelectListItem> _foodPersonSelectList;
        private List<SelectListItem> _poolPersonSelectList;
        private List<SelectListItem> _faciPersonSelectList;
        private List<SelectListItem> _dayCPersonSelectList;
        private List<SelectListItem> _allReviewersSelectList;
        private List<Reviewer> _allreviewers;
        private List<ApptAttendeeManagerModel> _allReviewersVm;
        public ScheduleCapacityViewModel()
        {
            _bldgPersonSelectList = new List<SelectListItem>();
            _elecPersonSelectList = new List<SelectListItem>();
            _mechPersonSelectList = new List<SelectListItem>();
            _plumPersonSelectList = new List<SelectListItem>();
            _zoniPersonSelectList = new List<SelectListItem>();
            _firePersonSelectList = new List<SelectListItem>();
            _backPersonSelectList = new List<SelectListItem>();
            _foodPersonSelectList = new List<SelectListItem>();
            _poolPersonSelectList = new List<SelectListItem>();
            _faciPersonSelectList = new List<SelectListItem>();
            _dayCPersonSelectList = new List<SelectListItem>();
            ScheduleCapacitySearchList = new List<ScheduleCapacitySearchResult>();
        }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<string> BldgReviewerSelected { get; set; }
        public bool BldgSelectAll { get; set; }
        public bool ElecSelectAll { get; set; }
        public List<string> ElecReviewerSelected { get; set; }
        public bool MechSelectAll { get; set; }
        public List<string> MechReviewerSelected { get; set; }
        public bool PlumSelectAll { get; set; }
        public List<string> PlumReviewerSelected { get; set; }
        public bool ZoniSelectAll { get; set; }
        public List<string> ZoniReviewerSelected { get; set; }
        public bool FireSelectAll { get; set; }
        public List<string> FireReviewerSelected { get; set; }
        public bool BackSelectAll { get; set; }
        public List<string> BackReviewerSelected { get; set; }
        public bool FoodSelectAll { get; set; }
        public List<string> FoodReviewerSelected { get; set; }
        public bool PoolSelectAll { get; set; }
        public List<string> PoolReviewerSelected { get; set; }
        public bool FaciSelectAll { get; set; }
        public List<string> FaciReviewerSelected { get; set; }
        public bool DayCSelectAll { get; set; }
        public List<string> DayCReviewerSelected { get; set; }

        public List<Reviewer> AllReviewers
        {
            get
            {
                if (_allreviewers == null) _allreviewers = ScheduleAPIHelper.GetReviewers(0, -1, false);
                _allReviewersSelectList = BuildSelectList(_allreviewers);
                return _allreviewers;
            }
            set
            {

            }
        }
        public List<SelectListItem> BldgPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Building, DepartmentNameEnums.Building);
                return BuildSelectList(reviewers);
            }
            set
            {
                _bldgPersonSelectList = value;
            }
        }
        public List<SelectListItem> ElecPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Electrical, DepartmentNameEnums.Electrical);
                return BuildSelectList(reviewers);
            }
            set
            {
                _elecPersonSelectList = value;
            }
        }
        public List<SelectListItem> MechPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Mechanical, DepartmentNameEnums.Mechanical);
                return BuildSelectList(reviewers);
            }
            set
            {
                _mechPersonSelectList = value;
            }
        }
        public List<SelectListItem> PlumPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Plumbing, DepartmentNameEnums.Plumbing);
                return BuildSelectList(reviewers);
            }
            set
            {
                _plumPersonSelectList = value;
            }
        }
        public List<SelectListItem> ZoniPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Zoning, DepartmentNameEnums.Zone_Cornelius);
                return BuildSelectList(reviewers);
            }
            set
            {
                _zoniPersonSelectList = value;
            }
        }
        public List<SelectListItem> FirePersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Fire, DepartmentNameEnums.Fire_Cornelius);
                return BuildSelectList(reviewers);
            }
            set
            {
                _firePersonSelectList = value;
            }
        }
        public List<SelectListItem> BackPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Backflow, DepartmentNameEnums.Backflow);
                return BuildSelectList(reviewers);
            }
            set
            {
                _backPersonSelectList = value;
            }
        }
        public List<SelectListItem> FoodPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Food);
                return BuildSelectList(reviewers);
            }
            set
            {
                _foodPersonSelectList = value;
            }
        }
        public List<SelectListItem> PoolPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Pool);
                return BuildSelectList(reviewers);
            }
            set
            {
                _poolPersonSelectList = value;
            }
        }
        public List<SelectListItem> FaciPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Facilities);
                return BuildSelectList(reviewers);
            }
            set
            {
                _faciPersonSelectList = value;
            }
        }
        public List<SelectListItem> DayCPersonSelectList
        {
            get
            {
                List<Reviewer> reviewers = GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Day_Care);
                return BuildSelectList(reviewers);
            }
            set
            {
                _dayCPersonSelectList = value;
            }
        }
        public List<SelectListItem> AllReviewersSelectList
        {
            get
            {
                return _allReviewersSelectList;
            }
        }
        public List<ApptAttendeeManagerModel> AllReviewersVm
        {
            get
            {
                _allReviewersVm = BuildAllReviewersVm();
                return _allReviewersVm;
            }
        }

        public List<ScheduleCapacitySearchResult> ScheduleCapacitySearchList { get; set; }

        public List<string> ReviewerSearchList { get; set; }

        /// <summary>
        /// used to send to different end point to get 
        /// specific data about a date range for a reviewer
        /// rather than the inclusive dates list for the
        /// schedule capacity popup
        /// </summary>
        public bool IsReviewerCapacitySearch { get; set; }

        #region Private Methods

        /// <summary>
        /// Build a SelectItem list by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<SelectListItem> BuildSelectList<T>(List<T> list)
        {
            if (list == null)
                list = new List<T>();
            List<SelectListItem> ret = new List<SelectListItem>();
            foreach (var item in list)
            {
                string stype = item.GetType().Name;
                switch (stype)
                {
                    case "NpaType":
                        ret.Add(new SelectListItem
                        {
                            Text = item.GetType().GetProperty("AppointmentTypeName").GetValue(item).ToString(),
                            Value = item.GetType().GetProperty("ID").GetValue(item).ToString()
                        });
                        break;
                    case "Reviewer":
                        //if (ret.Count == 0)
                        //    ret.Add(new SelectListItem { Text = "NA", Value = "-1" });
                        ret.Add(new SelectListItem
                        {
                            Text = item.GetType().GetProperty("FirstName").GetValue(item) + " " + item.GetType().GetProperty("LastName").GetValue(item),
                            Value = item.GetType().GetProperty("ID").GetValue(item).ToString()
                        });

                        break;
                    case "UserIdentity":
                        //if (ret.Count == 0)
                        //    ret.Add(new SelectListItem { Text = "NA", Value = "-1" });
                        ret.Add(new SelectListItem
                        {
                            Text = item.GetType().GetProperty("FirstName").GetValue(item) + " " + item.GetType().GetProperty("LastName").GetValue(item),
                            Value = item.GetType().GetProperty("ID").GetValue(item).ToString()
                        });

                        break;
                    default:
                        break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get the reviewers for each department
        /// </summary>
        /// <param name="roleEnum"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        private List<Reviewer> GetDeptSelectListsFromAllReviewers(DepartmentDivisionEnum roleEnum, DepartmentNameEnums department)
        {
            return ScheduleAPIHelper.GetReviewers(0, (int)department, false);

        }

        private List<ApptAttendeeManagerModel> BuildAllReviewersVm()
        {
            Reviewer reviewer;
            List<ApptAttendeeManagerModel> items = new List<ApptAttendeeManagerModel>();
            int deptnameenumid = 0;
            int businessrefid = 0;
            string deptnamelistid = string.Empty;
            foreach (SelectListItem item in BldgPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Building;
                businessrefid = (int)DepartmentNameEnums.Building;
                deptnamelistid = DepartmentNameList.Building;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ElecPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Electrical;
                businessrefid = (int)DepartmentNameEnums.Electrical;
                deptnamelistid = DepartmentNameList.Electrical;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in MechPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Mechanical;
                businessrefid = (int)DepartmentNameEnums.Mechanical;
                deptnamelistid = DepartmentNameList.Mechanical;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in PlumPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Plumbing;
                businessrefid = (int)DepartmentNameEnums.Plumbing;
                deptnamelistid = DepartmentNameList.Plumbing;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in BackPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Backflow;
                businessrefid = (int)DepartmentNameEnums.Backflow;
                deptnamelistid = DepartmentNameList.Backflow;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in DayCPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Day_Care;
                businessrefid = (int)DepartmentNameEnums.EH_Day_Care;
                deptnamelistid = DepartmentNameList.Day_Care;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FaciPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Facilities;
                businessrefid = (int)DepartmentNameEnums.EH_Facilities;
                deptnamelistid = DepartmentNameList.Facility_Lodging;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FirePersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Fire_Davidson;
                businessrefid = (int)DepartmentNameEnums.Fire_Davidson;
                deptnamelistid = DepartmentNameList.Fire;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FoodPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Food;
                businessrefid = (int)DepartmentNameEnums.EH_Food;
                deptnamelistid = DepartmentNameList.Food_Service;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in PoolPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Pool;
                businessrefid = (int)DepartmentNameEnums.EH_Pool;
                deptnamelistid = DepartmentNameList.Public_Pool;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ZoniPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Zone_Davidson;
                businessrefid = (int)DepartmentNameEnums.Zone_Davidson;
                deptnamelistid = DepartmentNameList.Zoning;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            return items;
        }

        #endregion Private Methods
    }

    public class ScheduleCapacityListViewModel : ViewModelBase
    {
        public List<ScheduleCapacityViewModel> ScheduleCapacityList { get; set; }
    }

    public class ScheduleCapacityListPlanReviewViewModel : ScheduleCapacityListViewModel
    {
        public decimal BuildingHours { get; set; }
        public decimal ElectricHours { get; set; }
        public decimal MechanicalHours { get; set; }
        public decimal PlumbingHours { get; set; }
        public decimal ZoningHours { get; set; }
        public decimal FireHours { get; set; }
        public decimal BackflowHours { get; set; }
        public decimal FoodHours { get; set; }
        public decimal PoolHours { get; set; }
        public decimal LodgeHours { get; set; }
        public decimal DaycareHours { get; set; }
        public PropertyTypeEnums PropertyType { get; set; }
        public string AccelaProjectRefId { get; set; }
        public int ProjectId { get; set; }
        public bool BuildingIsPool { get; set; }
        public bool ElectricIsPool { get; set; }
        public bool MechIsPool { get; set; }
        public bool PlumbIsPool { get; set; }
        public bool ZoneIsPool { get; set; }
        public bool FireIsPool { get; set; }
        public bool FoodServiceIsPool { get; set; }
        public bool PoolIsPool { get; set; }
        public bool FacilityIsPool { get; set; }
        public bool DayCareIsPool { get; set; }
        public bool BackFlowIsPool { get; set; }
        public int BuildingUserID { get; set; }
        public int ElectricUserID { get; set; }
        public int MechUserID { get; set; }
        public int PlumbUserID { get; set; }
        public int ZoneUserID { get; set; }
        public int FireUserID { get; set; }
        public int FoodServiceUserID { get; set; }
        public int PoolUserID { get; set; }
        public int FacilityUserID { get; set; }
        public int DayCareUserID { get; set; }
        public int BackFlowUserID { get; set; }
        public int Cycle { get; set; }
    }


    public class FIFOScheduleCapacityListPlanReviewViewModel : ScheduleCapacityListViewModel
    {
        public decimal BuildingHours { get; set; }
        public decimal ElectricHours { get; set; }
        public decimal MechanicalHours { get; set; }
        public decimal PlumbingHours { get; set; }
        public decimal ZoningHours { get; set; }
        public decimal FireHours { get; set; }
        public decimal BackflowHours { get; set; }
        public decimal FoodHours { get; set; }
        public decimal PoolHours { get; set; }
        public decimal LodgeHours { get; set; }
        public decimal DaycareHours { get; set; }
        public PropertyTypeEnums PropertyType { get; set; }
        public string AccelaProjectRefId { get; set; }
        public int ProjectId { get; set; }

        public int BuildingUserID { get; set; }
        public int ElectricUserID { get; set; }
        public int MechUserID { get; set; }
        public int PlumbUserID { get; set; }
        public int ZoneUserID { get; set; }
        public int FireUserID { get; set; }
        public int FoodServiceUserID { get; set; }
        public int PoolUserID { get; set; }
        public int FacilityUserID { get; set; }
        public int DayCareUserID { get; set; }
        public int BackFlowUserID { get; set; }
        public int Cycle { get; set; }
    }
}
