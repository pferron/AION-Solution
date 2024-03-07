using AION.BL;
using AION.BL.Models;
using AION.Web.Helpers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class NPAViewModel : NPAViewModelBase
    {
        private List<SelectListItem> _npaTypeSelectList;
        private List<SelectListItem> _npaTypeActiveSelectList;
        private List<SelectListItem> _recurrenceSelectList;
        private List<SelectListItem> _daySelectList;
        private List<SelectListItem> _yNSelectList;
        private List<SelectListItem> _meetingRoomSelectList;
        private List<SelectListItem> _reviewerSearchSelectList;

        private List<MeetingRoom> _meetingRoomList;

        private List<NpaType> _npatypes;
        private string _meetingroomnameselected;
        private List<NPASearchResult> _modifyList;
        private List<NPASearchResult> _endingSoonList;
        private List<NPASearchViewModel> _modifyNpaList;
        private List<NPASearchViewModel> _endingSoonNpaList;
        private List<SelectListItem> _deptNameList;

        public NPAViewModel()
        {

            _meetingRoomSelectList = new List<SelectListItem>();
            _reviewerSearchSelectList = new List<SelectListItem>();
            _modifyNpaList = new List<NPASearchViewModel>();
            _endingSoonNpaList = new List<NPASearchViewModel>();

            ReviewersList = new List<SelectListItem>();
            ModifyList = new List<NPASearchResult>();
            EndingSoonList = new List<NPASearchResult>();
        }
        public List<NPASearchResult> ModifyList
        {

            get { return _modifyList; }
            set { _modifyList = value; }
        }
        public List<NPASearchResult> EndingSoonList
        {
            get { return _endingSoonList; }
            set { _endingSoonList = value; }

        }
        public List<NPASearchViewModel> ModifyNpaList
        {
            get
            {
                if (_modifyList != null)
                    _modifyNpaList = BuildNpaSearchViewModelFromNpaSearchResult(_modifyList);
                return _modifyNpaList;
            }
        }

        public List<NPASearchViewModel> EndingSoonNpaList
        {
            get
            {
                if (_endingSoonList != null)
                    _endingSoonNpaList = BuildNpaSearchViewModelFromNpaSearchResult(_endingSoonList);
                return _endingSoonNpaList;
            }
        }
        public List<NPASearchViewModel> NPAListForAttendees
        {
            get
            {
                //put modify and ending list together to get distinct npas to build the dialogs for the add remove attendees
                if (_modifyNpaList != null && _endingSoonNpaList != null)
                    return _modifyNpaList.Union(_endingSoonNpaList).DistinctBy(x => x.ProjectScheduleId).ToList();
                return new List<NPASearchViewModel>();
            }
        }
        public NPASearchViewModel SelectedNpaSearch { get; set; }
        public List<SelectListItem> ReviewersList { get; set; }
        public int NPAID { get; set; }
        public string NPAName { get; set; }
        public bool NPATypeSelectListChecked { get; set; }

        public string SavedFilterList { get; set; }
        public List<SelectListItem> NPATypeSelectList
        {
            get
            {
                if (_npatypes != null && _npaTypeSelectList == null) BuildNPATypeList();
                return _npaTypeSelectList;
            }
        }
        public List<SelectListItem> NPATypeActiveSelectList
        {
            get
            {
                if (_npatypes != null && _npaTypeActiveSelectList == null) BuildActiveNPATypeList();
                return _npaTypeActiveSelectList;
            }
        }
        public int NPATypeSelected { get; set; }
        public List<NpaType> NpaTypes
        {
            get
            {
                return _npatypes;
            }
            set
            {
                _npatypes = value;
                if (_npatypes != null && _npaTypeSelectList == null)
                {
                    BuildNPATypeList();
                    BuildActiveNPATypeList();
                }
            }
        }
        public List<SelectListItem> RecurrenceSelectList
        {
            get
            {
                if (_recurrenceSelectList == null) BuildRecurrenceList();
                return _recurrenceSelectList;
            }
        }
        public string RecurrenceSelected { get; set; }
        public List<SelectListItem> DaySelectList
        {
            get
            {
                if (_daySelectList == null) BuildDayList();
                return _daySelectList;
            }
        }
        public string DaySelected { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllDay { get; set; }
        public List<SelectListItem> YNSelectList
        {
            get
            {
                if (_yNSelectList == null) BuildYNList();
                return _yNSelectList;
            }
        }
        public string YNSelected { get; set; }
        public List<SelectListItem> MeetingRoomSelectList
        {
            get
            {
                return _meetingRoomSelectList;
            }
            set
            {
                _meetingRoomSelectList = value;
            }
        }
        public int MeetingRoomRefIDSelected { get; set; }
        public string MeetingRoomNameSelected
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_meetingroomnameselected))
                    _meetingroomnameselected = "Select A Meeting Room";
                return _meetingroomnameselected;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _meetingroomnameselected = "Select A Meeting Room";
                else
                    _meetingroomnameselected = value;
            }
        }
        public string BldgReviewerSelected { get; set; }
        public bool BldgSelectAll { get; set; }
        public bool ElecSelectAll { get; set; }
        public string ElecReviewerSelected { get; set; }
        public bool MechSelectAll { get; set; }
        public string MechReviewerSelected { get; set; }
        public bool PlumSelectAll { get; set; }
        public string PlumReviewerSelected { get; set; }
        public bool ZoniSelectAll { get; set; }
        public string ZoniReviewerSelected { get; set; }
        public bool FireSelectAll { get; set; }
        public string FireReviewerSelected { get; set; }
        public bool BackSelectAll { get; set; }
        public string BackReviewerSelected { get; set; }
        public bool FoodSelectAll { get; set; }
        public string FoodReviewerSelected { get; set; }
        public bool PoolSelectAll { get; set; }
        public string PoolReviewerSelected { get; set; }
        public bool FaciSelectAll { get; set; }
        public string FaciReviewerSelected { get; set; }
        public bool DayCSelectAll { get; set; }
        public string DayCReviewerSelected { get; set; }
        public List<MeetingRoom> MeetingRoomList
        {
            get
            {
                return _meetingRoomList;
            }
            set
            {
                _meetingRoomList = value;
            }
        }
        public MeetingRoom SelectedMeetingRoom { get; set; }
        public string SelectedMeetingRoomName { get; set; }
        public bool SelectAllNPAs { get; set; }
        public List<SelectListItem> DeptNameList
        {
            get
            {
                if (_deptNameList == null) BuildDepartmentList();
                return _deptNameList;
            }
        }
        /// <summary>
        /// CSV list of holidays from HOliday Configuration table
        /// Used to disable holidays in the calendar on the UI
        /// </summary>
        public string Holidays { get; set; }

        #region Search Objects
        public List<SelectListItem> ReviewerSearchSelectList
        {
            get
            {
                //if (_reviewerSearchSelectList == null && _allreviewers != null)
                //    _reviewerSearchSelectList = _allReviewersSelectList;
                return _reviewerSearchSelectList;
            }
        }
        public string SearchString { get; set; }
        public DateTime SearchStartDate { get; set; }
        public DateTime SearchEndDate { get; set; }
        #endregion Search Objects

        #region Private Methods
        private void BuildNPATypeList()
        {
            _npaTypeSelectList = BuildSelectList(_npatypes);
        }
        private void BuildActiveNPATypeList()
        {
            _npaTypeActiveSelectList = BuildSelectList(_npatypes.Where(x => x.IsActive == true).ToList());
        }
        private void BuildRecurrenceList()
        {
            _recurrenceSelectList = new List<SelectListItem>();
            _recurrenceSelectList.Add(new SelectListItem { Text = "Once", Value = "Once" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "First", Value = "First" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Second", Value = "Second" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Third", Value = "Third" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Fourth", Value = "Fourth" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Last", Value = "Last" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Weekly", Value = "Weekly" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Daily", Value = "Daily" });
            _recurrenceSelectList.Add(new SelectListItem { Text = "Yearly", Value = "Yearly" });
        }
        private void BuildDayList()
        {
            _daySelectList = new List<SelectListItem>();
            _daySelectList.Add(new SelectListItem { Text = "Monday", Value = "Monday" });
            _daySelectList.Add(new SelectListItem { Text = "Tuesday", Value = "Tuesday" });
            _daySelectList.Add(new SelectListItem { Text = "Wednesday", Value = "Wednesday" });
            _daySelectList.Add(new SelectListItem { Text = "Thursday", Value = "Thursday" });
            _daySelectList.Add(new SelectListItem { Text = "Friday", Value = "Friday" });
        }
        private void BuildYNList()
        {
            _yNSelectList = new List<SelectListItem>();
            _yNSelectList.Add(new SelectListItem { Text = "N", Value = "N" });
            _yNSelectList.Add(new SelectListItem { Text = "Y", Value = "Y" });
        }

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
                        if (ret.Count == 0)
                            ret.Add(new SelectListItem { Text = "NA", Value = "-1" });
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

        private List<NPASearchViewModel> BuildNpaSearchViewModelFromNpaSearchResult(List<NPASearchResult> items)
        {
            string disabledcls = "disabled";
            string disabledhtml = "disabled=\"disabled\"";
            string readonlyhtml = "readonly=\"readonly\"";
            bool showdisabledcls = false;
            ScheduleHelpers scheduleHelpers = new ScheduleHelpers();
            List<NPASearchViewModel> list = new List<NPASearchViewModel>();
            foreach (NPASearchResult item in items)
            {
                showdisabledcls = PermissionMapping.IsViewOnly || PermissionMapping.Create_NPA == false ? true : false;
                list.Add(new NPASearchViewModel
                {
                    Attendees = scheduleHelpers.BuildAttendeesFromAttendeeInfo(item.Attendees),
                    ScheduleList = item.Schedules,
                    Day = item.Day,
                    IsRecurring = item.IsRecurring,
                    MeetingRoomName = item.MeetingRoomName,
                    NPADate = item.NPADate,
                    NPAId = item.NPAId,
                    NPAName = item.NPAName,
                    NPAType = item.NPAType,
                    Time = item.Time,
                    AllReviewers = AllReviewers,
                    PermissionMapping = PermissionMapping,
                    LoggedInUser = LoggedInUser,
                    LoggedInUserEmail = LoggedInUserEmail,
                    IsReadOnly = PermissionMapping.IsViewOnly,
                    DisabledCls = showdisabledcls ? disabledcls : string.Empty,
                    DisabledHtml = showdisabledcls ? disabledhtml : string.Empty,
                    ReadonlyHtml = showdisabledcls ? readonlyhtml : string.Empty,
                    ProjectScheduleId = item.ProjectScheduleID,
                    RecurringDate = item.Recurring_Date.ToString("MM/dd/yyyy")
                });
            }
            return list;
        }
        private void BuildDepartmentList()
        {

            _deptNameList = new List<SelectListItem>();
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Building, Value = DepartmentNameList.Building });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Electrical, Value = DepartmentNameList.Electrical });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Mechanical, Value = DepartmentNameList.Mechanical });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Plumbing, Value = DepartmentNameList.Plumbing });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Zoning, Value = DepartmentNameList.Zoning });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Fire, Value = DepartmentNameList.Fire });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Backflow, Value = DepartmentNameList.Backflow });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Food_Service, Value = DepartmentNameList.Food_Service });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Public_Pool, Value = DepartmentNameList.Public_Pool });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Facility_Lodging, Value = DepartmentNameList.Facility_Lodging });
            _deptNameList.Add(new SelectListItem { Text = DepartmentNameList.Day_Care, Value = DepartmentNameList.Day_Care });
        }

        #endregion Private Methods
    }
}