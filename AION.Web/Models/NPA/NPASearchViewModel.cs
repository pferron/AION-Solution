using AION.BL;
using AION.Manager.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class NPASearchViewModel : NPAViewModelBase
    {
        private List<SelectListItem> _attendeesList;
        private List<ApptAttendeeManagerModel> _attendees;
        private List<ProjectSchedule> _schedule;

        private List<SelectListItem> _deptNameList;
        public int? NPAId { get; set; }

        public string NPAType { get; set; }

        public string NPADate { get; set; }

        public string NPAName { get; set; }

        public string IsRecurring { get; set; }

        public string MeetingRoomName { get; set; }

        public string Day { get; set; }

        public string Time { get; set; }

        public List<ApptAttendeeManagerModel> Attendees
        {
            get { return _attendees; }
            set { _attendees = value; }
        }

        public List<ProjectSchedule> ScheduleList
        {
            get { return _schedule; }
            set { _schedule = value; }
        }

        public List<SelectListItem> AttendeesList
        {
            get
            {
                if (_attendees != null)
                {
                    //sort out attendees to Building role first

                    _attendeesList = BuildSelectList(_attendees);

                }

                return _attendeesList;
            }
        }

        public string ReviewerTypeSelected { get; set; }
        public string ReviewerSelected { get; set; }
        public string AttendeesSelected { get; set; }
        public DepartmentNameList DeptSelected { get; set; }
        public List<SelectListItem> DeptNameList
        {
            get
            {
                if (_deptNameList == null) BuildDepartmentList();
                return _deptNameList;
            }
        }
        public string SinglePerson { get; set; }
        public int ProjectScheduleId { get; set; }
        public string RecurringDate { get; set; }
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
                    case "UserIdentity":
                        if (ret.Count == 0)
                            ret.Add(new SelectListItem { Text = "NA", Value = "-1" });
                        ret.Add(new SelectListItem
                        {
                            Text = item.GetType().GetProperty("FirstName").GetValue(item) + " " + item.GetType().GetProperty("LastName").GetValue(item),
                            Value = item.GetType().GetProperty("ID").GetValue(item).ToString()
                        });

                        break;
                    case "ApptAttendeeManagerModel":
                        ret.Add(new SelectListItem
                        {
                            Text = item.GetType().GetProperty("FirstName").GetValue(item) + " " + item.GetType().GetProperty("LastName").GetValue(item),
                            Value = item.GetType().GetProperty("AttendeeId").GetValue(item).ToString()
                        });

                        break;

                    default:
                        break;
                }
            }
            return ret;
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