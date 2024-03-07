using AION.BL;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Web.BusinessEntities;
using AION.Web.Models.Express;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models.Scheduling
{
    public class ScheduleExpressViewModel : ViewModelBase, IExpressViewModel
    {
        private List<SelectListItem> _meetingRoomSelectList;
        private List<MeetingRoom> _meetingRoomList;
        private string _meetingroomnameselected;

        public ScheduleExpressViewModel()
        {
            _meetingRoomSelectList = new List<SelectListItem>();

            FacilitatorList = new List<Facilitator>();
            EstimatorList = new List<EstimatorUIModel>();
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
            NotesComments = new List<Note>();
            BackFlowApplicationNotes = new ApplicationNotes();
            BEMPApplicationNotes = new ApplicationNotes();
            EHSApplicationNotes = new ApplicationNotes();
            FireApplicationNotes = new ApplicationNotes();
            ZoningApplicationNotes = new ApplicationNotes();
            MeetingRoomList = new List<MeetingRoom>();

        }
        public List<StandardNote> StandardNotes { get; set; }
        public List<StandardNoteGroupEnums> StandardNoteGroups { get; set; }
        public string SchedulingNotes { get; set; }
        public string MandatorySchedulingNotes { get; set; }
        public string AddSchedulingNotes { get; set; }
        public string SchedulingStandardNotes { get; set; }

        public ExpressMeetingAppointment ExpressMeetingAppointment { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }

        private bool _isEstimationComplete;
        private ProjectEstimation _project;
        public bool IsAllNAChecked { get; set; }

        public List<Note> NotesComments { get; set; }

        public string ReservedExpressDates { get; set; }

        public string MinTime { get; set; }

        public string MaxTime { get; set; }

        public List<Facilitator> FacilitatorList { get; set; }

        public List<EstimatorUIModel> EstimatorList { get; set; }

        public List<CatalogItem> PermissionMappingCatalogItemList { get; set; }

        public bool HrsNABuilding
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Building);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Building, value);
            }
        }

        public bool HrsNAElectric
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Electrical);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Electrical, value);
            }
        }

        public bool HrsNAMech
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Mechanical);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Mechanical, value);
            }
        }

        public bool HrsNAPlumbing
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Plumbing);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Plumbing, value);
            }
        }

        public bool HrsNAZone
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Zone_Cornelius);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Zone_Cornelius, value);
            }
        }

        public bool HrsNAFire
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Fire_Cornelius);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Fire_Cornelius, value);
            }
        }

        public bool HrsNABackFlow
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.Backflow);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.Backflow, value);
            }
        }

        public bool HrsNAFood
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.EH_Food);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.EH_Food, value);
            }
        }

        public bool HrsNAPool
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.EH_Pool);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.EH_Pool, value);
            }
        }

        public bool HrsNAFacility
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.EH_Facilities);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.EH_Facilities, value);
            }
        }

        public bool HrsNADayCare
        {
            get
            {
                return GetIsNA(DepartmentNameEnums.EH_Day_Care);
            }
            set
            {
                SetIsNA(DepartmentNameEnums.EH_Day_Care, value);
            }
        }

        private List<SelectListItem> _facilitatorsListViewModel;

        public List<SelectListItem> FacilitatorsListViewModel
        {
            get
            {
                if (_facilitatorsListViewModel == null)
                    _facilitatorsListViewModel = GenerateFacilitatorListViewItems();
                return _facilitatorsListViewModel;
            }
            set { _facilitatorsListViewModel = value; }
        }

        public int AssignedFacilitator
        {
            get
            {
                SelectListItem ret;
                if (Project == null || Project.AssignedFacilitator == null)
                    ret = FacilitatorsListViewModel.Where(x => x.Value == "-1").FirstOrDefault();
                else
                    ret = FacilitatorsListViewModel.Where(x => x.Value == Project.AssignedFacilitator.Value.ToString()).FirstOrDefault();
                //if the facilitator is not active it will not be able to find that id in list and so it comes to null. so in this case leave it null
                if (ret == null)
                    ret = FacilitatorsListViewModel.Where(x => x.Value == "-1").FirstOrDefault();
                ret.Selected = true;
                return int.Parse(ret.Value);
            }
            set
            {
                Project.AssignedFacilitator = Convert.ToInt32(value);
            }
        }


        public decimal HoursBuilding
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Building);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Building, value);
            }
        }

        public decimal HoursElectic
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Electrical);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Electrical, value);
            }
        }

        public decimal HoursMech
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Mechanical);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Mechanical, value);
            }
        }

        public decimal HoursPlumb
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Plumbing);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Plumbing, value);
            }
        }

        public decimal HoursZoning
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Zone_Cornelius);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Zone_Cornelius, value);
            }
        }

        public decimal HoursFire
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Fire_Cornelius);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Fire_Cornelius, value);
            }
        }

        public decimal HoursBackFlow
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.Backflow);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.Backflow, value);
            }
        }

        public decimal HoursFood
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.EH_Food);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.EH_Food, value);
            }
        }

        public decimal HoursPool
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.EH_Pool);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.EH_Pool, value);
            }
        }

        public decimal HoursLodge
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.EH_Facilities);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.EH_Facilities, value);
            }
        }

        public decimal HoursDayCare
        {
            get
            {
                return GetDefaulthours(DepartmentNameEnums.EH_Day_Care);
            }
            set
            {
                SetDefaulthours(DepartmentNameEnums.EH_Day_Care, value);
            }
        }

        public string PrimaryReviewerBuilding
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Building);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Building, value);
            }
        }

        public string SecondaryReviewerBuilding
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Building);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Building, value);
            }
        }

        public string PrimaryReviewerElectrical
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Electrical);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Electrical, value);
            }
        }

        public string SecondaryReviewerelectrical
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Electrical);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Electrical, value);
            }
        }

        public string PrimaryReviewerMechanical
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Mechanical);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Mechanical, value);
            }
        }

        public string SecondaryReviewerMechanical
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Mechanical);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Mechanical, value);
            }
        }

        public string PrimaryReviewerPlumbing
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Plumbing);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Plumbing, value);
            }
        }

        public string SecondaryReviewerPlumbing
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Plumbing);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Plumbing, value);
            }
        }

        public string PrimaryReviewerZone
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Zone_Cornelius);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Zone_Cornelius, value);
            }
        }

        public string SecondaryReviewerZone
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Zone_Cornelius);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Zone_Cornelius, value);
            }
        }

        public string PrimaryReviewerFire
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Fire_Cornelius);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Fire_Cornelius, value);
            }
        }

        public string SecondaryReviewerFire
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Fire_Cornelius);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Fire_Cornelius, value);
            }
        }

        public string PrimaryReviewerBackFlow
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.Backflow);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.Backflow, value);
            }
        }

        public string SecondaryReviewerBackFlow
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.Backflow);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.Backflow, value);
            }
        }

        public string PrimaryReviewerFood
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.EH_Food);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.EH_Food, value);
            }
        }

        public string SecondaryReviewerFood
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.EH_Food);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.EH_Food, value);
            }
        }

        public string PrimaryReviewerPool
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.EH_Pool);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.EH_Pool, value);
            }
        }

        public string SecondaryReviewerPool
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.EH_Pool);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.EH_Pool, value);
            }
        }

        public string PrimaryReviewerFacilities
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.EH_Facilities);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.EH_Facilities, value);
            }
        }

        public string SecondaryReviewerFacilities
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.EH_Facilities);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.EH_Facilities, value);
            }
        }

        public string PrimaryReviewerDayCare
        {
            get
            {
                return GetPrimaryReviewerName(DepartmentNameEnums.EH_Day_Care);
            }
            set
            {
                SetPrimaryReviewerName(DepartmentNameEnums.EH_Day_Care, value);
            }
        }

        public string SecondaryReviewerDayCare
        {
            get
            {
                return GetSecondaryReviewerName(DepartmentNameEnums.EH_Day_Care);
            }
            set
            {
                SetSecondaryReviewerName(DepartmentNameEnums.EH_Day_Care, value);
            }
        }
        /// <summary>
        /// used to display any messages after a submit
        /// can be used in ProjectParms obj for other messages
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// determines if the estimation is complete.
        /// </summary>
        public bool IsEstimationComplete
        {
            get
            {
                if (Project != null)
                {
                    _isEstimationComplete = GetEstimationComplete(Project.Agencies, Project.Trades);

                }
                return _isEstimationComplete;
            }
        }
        public string ScheduledReviewerBuilding
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Building);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Building, value);
            }
        }
        public string ScheduledReviewerElectrical
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Electrical);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Electrical, value);
            }
        }
        public string ScheduledReviewerPlumbing
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Plumbing);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Plumbing, value);
            }
        }
        public string ScheduledReviewerMechanical
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Mechanical);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Mechanical, value);
            }
        }
        public string ScheduledReviewerFire
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Fire_Cornelius);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Fire_Cornelius, value);
            }
        }
        public string ScheduledReviewerZone
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Zone_Cornelius);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Zone_Cornelius, value);
            }
        }
        public string ScheduledReviewerBackFlow
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.Backflow);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.Backflow, value);
            }
        }
        public string ScheduledReviewerFood
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.EH_Food);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.EH_Food, value);
            }
        }
        public string ScheduledReviewerPool
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.EH_Pool);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.EH_Pool, value);
            }
        }
        public string ScheduledReviewerFacilities
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.EH_Facilities);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.EH_Facilities, value);
            }
        }
        public string ScheduledReviewerDayCare
        {
            get
            {
                return GetScheduledReviewerId(DepartmentNameEnums.EH_Day_Care);
            }
            set
            {
                SetScheduledReviewerId(DepartmentNameEnums.EH_Day_Care, value);
            }
        }

        public string InternalNotes { get; set; }
        public DateTime? PlansReadyOnDate { get; set; }
        public string TeamScore { get; set; }
        /// <summary>
        /// this changes depending on the user Save or Submit
        /// </summary>
        public bool IsSubmit { get; set; }
        /// <summary>
        /// hidden field that holds the id for this appointment
        /// </summary>
        public int? ExpressMeetingApptID { get; set; }
        /// <summary>
        /// Current update date for updating record
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// CSV list of holidays from HOliday Configuration table
        /// Used to disable holidays in the calendar on the UI
        /// </summary>
        public string Holidays { get; set; }

        /// <summary>
        /// List of holiday config values
        /// used to disable holidays in the calendar on the UI
        /// </summary>
        public List<HolidayConfig> HolidaysConfigs { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public List<DateTime> RequestedDates { get; set; }
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

        #region Cycle Information

        public int Cycle { get; set; }

        public bool IsNewCycle { get; set; }

        public decimal? ReReviewBuilding { get; set; }

        public decimal? ReReviewElectric { get; set; }

        public decimal? ReReviewMech { get; set; }

        public decimal? ReReviewPlumb { get; set; }

        public decimal? ReReviewZoning { get; set; }

        public decimal? ReReviewFire { get; set; }

        public decimal? ReReviewBackFlow { get; set; }

        public decimal? ReReviewFood { get; set; }

        public decimal? ReReviewPool { get; set; }

        public decimal? ReReviewLodge { get; set; }

        public decimal? ReReviewDayCare { get; set; }

        #endregion

        public string AccelaProjectDeeplink { get; set; }

        #region "Private"
        decimal GetDefaulthours(DepartmentNameEnums deptType)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            decimal defaulthours = 0;

            if (dept != null && dept.EstimationHours.HasValue == true)
            {
                defaulthours = dept.EstimationHours.Value;
                decimal wholeestimationhours = Math.Truncate(defaulthours);
                decimal partestimationhours = defaulthours - wholeestimationhours;
                if (defaulthours % 0.5M != 0)
                {
                    if (partestimationhours > 0.5M)
                    {
                        //add one to truncated value
                        defaulthours = wholeestimationhours + 1.0M;
                    }
                    if (partestimationhours < 0.5M)
                    {
                        //add .5 to truncated value
                        defaulthours = wholeestimationhours + 0.5M;
                    }
                }
            }
            return Math.Round(defaulthours, 1, MidpointRounding.AwayFromZero);
        }

        bool SetDefaulthours(DepartmentNameEnums deptType, decimal value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.EstimationHours = value;
            if (value > 0 && dept.EstimationNotApplicable == true)
                dept.EstimationNotApplicable = false;
            return true;
        }

        List<SelectListItem> GenerateFacilitatorListViewItems()
        {
            if (FacilitatorList == null)
                FacilitatorList = new List<Facilitator>();
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(new SelectListItem { Text = "Not Selected", Value = "-1" }); //Create a 'not Selected' item
            foreach (var item in FacilitatorList)
            {
                ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });
            }
            return ret;
        }
        /// <summary>
        /// gets the name of the reviewer that was previously scheduled on save 
        /// or that the user requested on the application
        /// </summary>
        /// <param name="deptType"></param>
        /// <returns></returns>
        string GetScheduledReviewerId(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem notapplicable = current.Where(x => x.Value == "-1").FirstOrDefault();
            SelectListItem notselected = current.Where(x => x.Value == "0").FirstOrDefault();
            ProjectDepartment dept = GetDepartment(deptType);
            //if dept is NA then pick the NA option -1
            if (dept.EstimationNotApplicable)
                return notapplicable.Value;
            if (dept == null)
            {
                return notapplicable.Value;
            }
            if (dept.AssignedPlanReviewer == null || dept.AssignedPlanReviewer.ID == 0 || dept.AssignedPlanReviewer.ID == -1)
            {
                return notselected.Value;
            }
            var val = current.Where(x => x.Value == dept.AssignedPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Value : notselected.Value;
        }
        /// <summary>
        /// sets the property for the scheduled plan reviewer
        /// </summary>
        /// <param name="deptType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetScheduledReviewerId(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.AssignedPlanReviewer = AllReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }
        /// <summary>
        /// gets the name of the reviewer that was previously scheduled on save 
        /// </summary>
        /// <param name="deptType"></param>
        /// <returns></returns>
        string GetScheduledReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem notapplicable = current.Where(x => x.Value == "-1").FirstOrDefault();
            SelectListItem notselected = current.Where(x => x.Value == "0").FirstOrDefault();
            ProjectDepartment dept = GetDepartment(deptType);
            //if dept is NA then pick the NA option -1
            if (dept.EstimationNotApplicable)
                return notapplicable.Text;
            if (dept == null)
            {
                return notapplicable.Text;
            }
            if (dept.AssignedPlanReviewer == null || dept.AssignedPlanReviewer.ID == 0 || dept.AssignedPlanReviewer.ID == -1)
            {
                return notselected.Text;
            }

            var val = current.Where(x => x.Value == dept.AssignedPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Text : notselected.Text;
        }
        private string GetUserName(UserIdentity user)
        {
            if (user.ID == -1)
                return "NA";
            if (user.ID == 0)
                return "Not Selected";
            return user.FirstName + " " + user.LastName;
        }
        #endregion
    }
}