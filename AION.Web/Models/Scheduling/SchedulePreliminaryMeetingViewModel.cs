using AION.BL;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Manager.Models;
using AION.Web.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class SchedulePreliminaryMeetingViewModel : ViewModelBase
    {
        private List<SelectListItem> _meetingRoomSelectList;
        private List<MeetingRoom> _meetingRoomList;
        private string _meetingroomnameselected;

        public SchedulePreliminaryMeetingViewModel()
        {
            _meetingRoomSelectList = new List<SelectListItem>();

            FacilitatorList = new List<Facilitator>();
            EstimatorList = new List<EstimatorUIModel>();
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
            NotesComments = new List<Note>();
            FacilitatorWorkloadSummary = new List<Facilitator>();
            BackFlowApplicationNotes = new ApplicationNotes();
            BEMPApplicationNotes = new ApplicationNotes();
            EHSApplicationNotes = new ApplicationNotes();
            FireApplicationNotes = new ApplicationNotes();
            ZoningApplicationNotes = new ApplicationNotes();
            MeetingRoomList = new List<MeetingRoom>();
            UserIdentities = new List<UserIdentity>();
            CurrentAttendees = new List<UserIdentity>();
        }

        private bool _isEstimationComplete;

        public bool IsAllNAChecked { get; set; }

        public List<Note> NotesComments { get; set; }

        public List<Facilitator> FacilitatorWorkloadSummary { get; set; }

        public List<Facilitator> FacilitatorList { get; set; }

        public List<EstimatorUIModel> EstimatorList { get; set; }

        public List<CatalogItem> PermissionMappingCatalogItemList { get; set; }

        public DepartmentNameEnums FireAgency { get; set; }
        public DepartmentNameEnums ZoneAgency { get; set; }

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
        public string StatusMessage { get; set; }
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
        public List<StandardNote> StandardNotes { get; set; }
        public List<StandardNoteGroupEnums> StandardNoteGroups { get; set; }
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
        public string ScheduledReviewerBuilding
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Building);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Building, value);
            }
        }
        public string ScheduledReviewerElectrical
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Electrical);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Electrical, value);
            }
        }
        public string ScheduledReviewerPlumbing
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Plumbing);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Plumbing, value);
            }
        }
        public string ScheduledReviewerMechanical
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Mechanical);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Mechanical, value);
            }
        }
        public string ScheduledReviewerFire
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Fire_Cornelius);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Fire_Cornelius, value);
            }
        }
        public string ScheduledReviewerZone
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Zone_Cornelius);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Zone_Cornelius, value);
            }
        }
        public string ScheduledReviewerBackFlow
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.Backflow);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.Backflow, value);
            }
        }
        public string ScheduledReviewerFood
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.EH_Food);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.EH_Food, value);
            }
        }
        public string ScheduledReviewerPool
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.EH_Pool);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.EH_Pool, value);
            }
        }
        public string ScheduledReviewerFacilities
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.EH_Facilities);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.EH_Facilities, value);
            }
        }
        public string ScheduledReviewerDayCare
        {
            get
            {
                return GetScheduledReviewerName(DepartmentNameEnums.EH_Day_Care);
            }
            set
            {
                SetScheduledReviewerName(DepartmentNameEnums.EH_Day_Care, value);
            }
        }

        public void SetSelectedReviewers(List<AttendeeInfo> attendees)
        {
            foreach (AttendeeInfo attendee in attendees)
            {
                DepartmentNameEnums department = (DepartmentNameEnums)attendee.DeptNameEnumId;

                switch (department)
                {
                    case DepartmentNameEnums.Building:
                        ScheduledReviewerBuilding = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.Electrical:
                        ScheduledReviewerElectrical = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.Mechanical:
                        ScheduledReviewerMechanical = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.Plumbing:
                        ScheduledReviewerPlumbing = attendee.AttendeeId.ToString();
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
                        ScheduledReviewerZone = attendee.AttendeeId.ToString();
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
                        ScheduledReviewerFire = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        ScheduledReviewerDayCare = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.EH_Food:
                        ScheduledReviewerFood = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        ScheduledReviewerPool = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        ScheduledReviewerFacilities = attendee.AttendeeId.ToString();
                        break;
                    case DepartmentNameEnums.Backflow:
                        ScheduledReviewerBackFlow = attendee.AttendeeId.ToString();
                        break;
                    default:
                        throw new ArgumentException("Unexpected option! Contact IT Adminstrator.");
                }
            }
        }

        public string InternalNotes { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }
        public int TotalCustomerAttendees { get; set; }
        public DateTime PlansReadyOnDate { get; set; }
        public string TeamScore { get; set; }
        /// <summary>
        /// List of all users in AION
        /// TODO: filtering?
        /// </summary>
        public List<UserIdentity> UserIdentities { get; set; }
        /// <summary>
        /// attendees saved for this project meeting
        /// </summary>
        public List<UserIdentity> CurrentAttendees { get; set; }
        /// <summary>
        /// csv of useridentity.id, used in Submit method
        /// </summary>
        public string AttendeeIds { get; set; }
        /// <summary>
        /// this changes depending on the user Save or Submit
        /// </summary>
        public bool IsSubmit { get; set; }
        /// <summary>
        /// hidden field that holds the id for this appointment
        /// </summary>
        public int? PreliminaryMeetingApptID { get; set; }
        /// <summary>
        /// Preliminary Meeting appointment data
        /// </summary>
        public PreliminaryMeetingAppointment PreliminaryMeetingAppointment { get; set; }

        /// <summary>
        /// Current update date for updating PMA
        /// </summary>
        public DateTime PMAUpdateDate { get; set; }

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
        string GetScheduledReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem ret = current.Where(x => x.Value == "-1").FirstOrDefault();
            SelectListItem notselected = current.Where(x => x.Value == "0").FirstOrDefault();
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null || dept.AssignedPlanReviewer == null || dept.AssignedPlanReviewer.ID == 0)
            {
                return ret.Value;
            }
            //if dept is NA then pick the NA option -1
            if (dept.EstimationNotApplicable)
                return ret.Value;
            //not assigned, not NA
            if (dept.AssignedPlanReviewer.ID == -1)
            {
                //if primary is picked, use that one
                if (dept.PrimaryPlanReviewer != null && dept.PrimaryPlanReviewer.ID > 0)
                {
                    ret = current.Where(x => x.Value == dept.PrimaryPlanReviewer.ID.ToString()).FirstOrDefault();
                    if (ret == null)
                        ret = notselected;
                    return ret.Value;
                }
                //get the requested if there's no assigned reviewer or primary 
                if (dept.ProposedPlanReviewer != null && dept.ProposedPlanReviewer.ID > 0)
                {
                    ret = current.Where(x => x.Value == dept.ProposedPlanReviewer.ID.ToString()).FirstOrDefault();
                    if (ret == null)
                        ret = notselected;
                    return ret.Value;
                }
                //no primary, no proposed, no assigned, return not selected
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
        bool SetScheduledReviewerName(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.AssignedPlanReviewer = AllJurisdictionReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }
        #endregion

    }
}