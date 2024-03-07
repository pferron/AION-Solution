using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.BusinessEntities;
using AION.Web.Models.Express;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AION.Web.Models.Scheduling
{
    public class ScheduleMeetingViewModel : ViewModelBase, IExpressViewModel
    {
        private List<SelectListItem> _meetingRoomSelectList;
        private List<MeetingRoom> _meetingRoomList;
        private string _meetingroomnameselected;

        public ScheduleMeetingViewModel()
        {
            _meetingRoomSelectList = new List<SelectListItem>();

            FacilitatorList = new List<Facilitator>();
            EstimatorList = new List<EstimatorUIModel>();
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
            NotesComments = new List<Note>();
            MeetingRoomList = new List<MeetingRoom>();
            RequestedDates = new List<DateTime?>();

            DateTime date = DateTime.Now;
            TimeSpan timeSpanStart = new TimeSpan(13, 00, 0);
            TimeSpan timeSpanEnd = new TimeSpan(14, 00, 0);

            StartTime = date.Date + timeSpanStart;
            EndTime = date.Date + timeSpanEnd;

            UserIdentities = new List<UserIdentity>();
            CurrentAttendees = new List<UserIdentity>();
        }

        public string MeetingTypeDesc { get; set; }
        public int MeetingTypeRefId { get; set; }
        public List<StandardNote> StandardNotes { get; set; }
        public List<StandardNoteGroupEnums> StandardNoteGroups { get; set; }
        public string SchedulingNotes { get; set; }
        public string MandatorySchedulingNotes { get; set; }
        public string AddSchedulingNotes { get; set; }
        public string SchedulingStandardNotes { get; set; }

        public FacilitatorMeetingAppointment FacilitatorMeetingAppointment { get; set; }

        public DateTime? RequestedDate1 { get; set; }
        public DateTime? RequestedDate2 { get; set; }
        public DateTime? RequestedDate3 { get; set; }
        public bool IsVirtualMeeting { get; set; } = false;
        public bool IsPostAutoSchedule { get; set; } = false;

        private ProjectEstimation _project;

        public List<Note> NotesComments { get; set; }

        public string MinTime { get; set; }

        public string MaxTime { get; set; }

        public string AccelaProjectDeeplink { get; set; }

        public List<Facilitator> FacilitatorList { get; set; }

        public List<EstimatorUIModel> EstimatorList { get; set; }

        public List<UserIdentity> UserIdentities { get; set; }

        public List<UserIdentity> CurrentAttendees { get; set; }

        public DepartmentNameEnums FireAgency { get; set; }
        public DepartmentNameEnums ZoneAgency { get; set; }

        public string AttendeeIds { get; set; }
        public List<int> AdditionalAttendeeIds { get; set; }

        public List<CatalogItem> PermissionMappingCatalogItemList { get; set; }

        public string DurationHours { get; set; }

        List<SelectListItem> _durationHoursSelectList;
        public List<SelectListItem> DurationHoursSelectList
        {
            get
            {
                if (_durationHoursSelectList == null)
                    _durationHoursSelectList = GenerateDurationHoursListViewItems();
                return _durationHoursSelectList;
            }
            set
            {
                _durationHoursSelectList = value;
            }
        }

        public string DurationMinutes { get; set; }

        List<SelectListItem> _durationMinutesSelectList;
        public List<SelectListItem> DurationMinutesSelectList
        {
            get
            {
                if (_durationMinutesSelectList == null)
                    _durationMinutesSelectList = GenerateDurationMinutesListViewItems();
                return _durationMinutesSelectList;
            }
            set
            {
                _durationMinutesSelectList = value;
            }
        }

        /// <summary>
        /// used to display any messages after a submit
        /// can be used in ProjectParms obj for other messages
        /// </summary>
        public string StatusMessage { get; set; }

        public string ScheduledReviewerBuilding { get; set; }
        public string ScheduledReviewerElectrical { get; set; }
        public string ScheduledReviewerPlumbing { get; set; }
        public string ScheduledReviewerMechanical { get; set; }
        public string ScheduledReviewerFire { get; set; }
        public string ScheduledReviewerZone { get; set; }
        public string ScheduledReviewerBackFlow { get; set; }
        public string ScheduledReviewerFood { get; set; }
        public string ScheduledReviewerPool { get; set; }
        public string ScheduledReviewerFacilities { get; set; }
        public string ScheduledReviewerDayCare { get; set; }

        public string InternalNotes { get; set; }
        /// <summary>
        /// this changes depending on the user Save or Submit
        /// </summary>
        public bool IsSubmit { get; set; }
        /// <summary>
        /// hidden field that holds the id for this appointment
        /// </summary>
        public int? FacilitatorMeetingApptID { get; set; }
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

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleDate { get; set; }

        public List<DateTime?> RequestedDates { get; set; }
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

        #region "Private"

        List<SelectListItem> GenerateDurationHoursListViewItems()
        {
            List<SelectListItem> durationHours = new List<SelectListItem>();
            durationHours.Add(new SelectListItem() { Text = "0 hours", Value = "0" });
            durationHours.Add(new SelectListItem() { Text = "1 hour", Value = "1", Selected = true });
            durationHours.Add(new SelectListItem() { Text = "2 hours", Value = "2" });
            durationHours.Add(new SelectListItem() { Text = "3 hours", Value = "3" });
            durationHours.Add(new SelectListItem() { Text = "4 hours", Value = "4" });
            durationHours.Add(new SelectListItem() { Text = "5 hours", Value = "5" });
            durationHours.Add(new SelectListItem() { Text = "6 hours", Value = "6" });
            durationHours.Add(new SelectListItem() { Text = "7 hours", Value = "7" });
            durationHours.Add(new SelectListItem() { Text = "8 hours", Value = "8" });

            return durationHours;
        }

        List<SelectListItem> GenerateDurationMinutesListViewItems()
        {
            List<SelectListItem> durationMinutes = new List<SelectListItem>();
            durationMinutes.Add(new SelectListItem() { Text = "00 minutes", Value = "0", Selected = true });
            durationMinutes.Add(new SelectListItem() { Text = "15 minutes", Value = "15" });
            durationMinutes.Add(new SelectListItem() { Text = "30 minutes", Value = "30" });
            durationMinutes.Add(new SelectListItem() { Text = "45 minutes", Value = "45" });

            return durationMinutes;
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

        #endregion
    }
}