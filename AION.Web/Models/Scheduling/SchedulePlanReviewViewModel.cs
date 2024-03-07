using AION.BL;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Web.BusinessEntities;
using AION.Web.Models.Express;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class SchedulePlanReviewViewModel : ViewModelBase, IExpressViewModel
    {

        public SchedulePlanReviewViewModel()
        {
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
        }

        private bool _isEstimationComplete;
        public bool IsAllNAChecked { get; set; }
        public int? PlanReviewScheduleId { get; set; }
        public AppointmentResponseStatusEnum ApptResponseStatusEnum { get; set; }
        public DateTime? FifoDueDt { get; set; }
        public int? FifoScheduleId { get; set; }

        public List<Note> NotesComments { get; set; }

        public PropertyTypeEnums PropertyType { get; set; }

        public bool IsGateAccepted
        {
            get { return Project.IsGateAccepted; }
        }

        public List<Facilitator> FacilitatorList { get; set; }

        public List<EstimatorUIModel> EstimatorList { get; set; }

        public List<CatalogItem> PermissionMappingCatalogItemList { get; set; }

        private bool _canPoolForPoorPerformer;

        public bool CanPoolForPoorPerformer
        {
            get
            {
                _canPoolForPoorPerformer = DeterminCanPoolForPoorPerformer();
                return _canPoolForPoorPerformer;
            }
        }

        private bool DeterminCanPoolForPoorPerformer()
        {
            if (Cycle == 1 && (Project.TeamGradeTxt != null && Project.TeamGradeTxt.ToUpper() == "POOR"))
            {
                return false;
            }

            return true;
        }

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

        public decimal HoursBuildingFinal { get; set; }

        public decimal HoursElecticFinal { get; set; }

        public decimal HoursMechFinal { get; set; }

        public decimal HoursPlumbFinal { get; set; }

        public decimal HoursZoningFinal { get; set; }

        public decimal HoursFireFinal { get; set; }

        public decimal HoursBackFlowFinal { get; set; }
        public decimal HoursFoodFinal { get; set; }

        public decimal HoursPoolFinal { get; set; }

        public decimal HoursLodgeFinal { get; set; }
        public decimal HoursDayCareFinal { get; set; }

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

        public List<StandardNote> StandardNotes { get; set; }
        public List<StandardNoteGroupEnums> StandardNoteGroups { get; set; }
        public string SchedulingNotes { get; set; }
        public string MandatorySchedulingNotes { get; set; }
        public string AddSchedulingNotes { get; set; }
        public string SchedulingStandardNotes { get; set; }
        public string ScheduledReviewerBuilding
        {
            get
            {
                if (Project.AccelaPropertyType == PropertyTypeEnums.Express)
                {
                    return GetScheduledReviewerId(DepartmentNameEnums.Building);
                }
                return GetScheduledReviewerName(DepartmentNameEnums.Building);
            }
            set
            {
                if (Project.AccelaPropertyType == PropertyTypeEnums.Express)
                {
                    SetScheduledReviewerId(DepartmentNameEnums.Building, value);
                }
                else
                {
                    SetScheduledReviewerName(DepartmentNameEnums.Building, value);
                }
            }
        }
        public string ScheduledReviewerElectrical
        {
            get
            {
                if (Project.AccelaPropertyType == PropertyTypeEnums.Express)
                {
                    return GetScheduledReviewerId(DepartmentNameEnums.Electrical);
                }
                return GetScheduledReviewerName(DepartmentNameEnums.Electrical);
            }
            set
            {
                if (Project.AccelaPropertyType == PropertyTypeEnums.Express)
                {
                    SetScheduledReviewerName(DepartmentNameEnums.Electrical, value);
                }
                else
                {
                    SetScheduledReviewerName(DepartmentNameEnums.Electrical, value);
                }
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
        public string InternalNotes { get; set; }
        public DateTime? PlansReadyOnDate { get; set; }
        public DateTime? GateDate { get; set; }
        public string TeamScore { get; set; }
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
        /// Current update date for updating record
        /// </summary>
        public DateTime UpdateDate { get; set; }

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

        public bool IsRescheduleOverwrite { get; set; }

        #region Start and End Dates for the Trades/Agencies

        /************** Start and End Dates for the Trades/Agencies **********/
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BuildStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ElectStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MechaStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PlumbStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FireStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ZoneStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BackfStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PoolStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FoodStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FacilStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DaycStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BuildEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ElectEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MechaEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PlumbEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FireEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ZoneEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BackfEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PoolEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FoodEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FacilEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DaycEndDate { get; set; }

        #endregion Start and End Dates for the Trades/Agencies
        #region Pool/Fifo for Trades/Agencies
        public bool? BuildFifo { get; set; }
        public bool? ElectFifo { get; set; }
        public bool? MechaFifo { get; set; }
        public bool? PlumbFifo { get; set; }
        public bool? FireFifo { get; set; }
        public bool? ZoneFifo { get; set; }
        public bool? BackfFifo { get; set; }
        public bool? PoolFifo { get; set; }
        public bool? FoodFifo { get; set; }
        public bool? FacilFifo { get; set; }
        public bool? DaycFifo { get; set; }

        public bool? BuildPool { get; set; }
        public bool? ElectPool { get; set; }
        public bool? MechaPool { get; set; }
        public bool? PlumbPool { get; set; }
        public bool? FirePool { get; set; }
        public bool? ZonePool { get; set; }
        public bool? BackfPool { get; set; }
        public bool? PoolPool { get; set; }
        public bool? FoodPool { get; set; }
        public bool? FacilPool { get; set; }
        public bool? DaycPool { get; set; }
        #endregion Pool/Fifo for Trades/Agencies
        public int? BuildPlanReviewScheduleId { get; set; }
        public int? ElectPlanReviewScheduleId { get; set; }
        public int? MechaPlanReviewScheduleId { get; set; }
        public int? PlumbPlanReviewScheduleId { get; set; }
        public int? FirePlanReviewScheduleId { get; set; }
        public int? ZonePlanReviewScheduleId { get; set; }
        public int? BackfPlanReviewScheduleId { get; set; }
        public int? PoolPlanReviewScheduleId { get; set; }
        public int? FoodPlanReviewScheduleId { get; set; }
        public int? FacilPlanReviewScheduleId { get; set; }
        public int? DaycPlanReviewScheduleId { get; set; }

        public DateTime? BuildPRSUpdateDate { get; set; }
        public DateTime? ElectPRSUpdateDate { get; set; }
        public DateTime? MechaPRSUpdateDate { get; set; }
        public DateTime? PlumbPRSUpdateDate { get; set; }
        public DateTime? FirePRSUpdateDate { get; set; }
        public DateTime? ZonePRSUpdateDate { get; set; }
        public DateTime? BackfPRSUpdateDate { get; set; }
        public DateTime? PoolPRSUpdateDate { get; set; }
        public DateTime? FoodPRSUpdateDate { get; set; }
        public DateTime? FacilPRSUpdateDate { get; set; }
        public DateTime? DaycPRSUpdateDate { get; set; }

        #region EXPRESS
        private List<SelectListItem> _meetingRoomSelectList;
        private List<MeetingRoom> _meetingRoomList = new List<MeetingRoom>();
        private string _meetingroomnameselected;
        public List<DateTime> RequestedDates { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }
        public string ReservedExpressDates { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string MinTime { get; set; }
        public string MaxTime { get; set; }
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

        #endregion

        #region Cycle Information

        public int Cycle { get; set; }
        public int ProjectCycleId { get; set; }
        public bool IsNewCycle { get; set; }
        public bool IsFutureCycle { get; set; }
        public bool CanScheduleFutureCycle { get; set; }
        public bool IsCycleComparison { get; set; }
        public bool PreviouslyRejected { get; set; }
        public bool IsAdjustHours { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleAfterDate { get; set; }

        private decimal? _rereviewbuilding { get; set; }
        private decimal? _rereviewelectric { get; set; }

        private decimal? _rereviewmech { get; set; }

        private decimal? _rereviewplumb { get; set; }

        private decimal? _rereviewzoning { get; set; }

        private decimal? _rereviewfire { get; set; }

        private decimal? _rereviewbackflow { get; set; }

        private decimal? _rereviewfood { get; set; }

        private decimal? _rereviewpool { get; set; }

        private decimal? _rereviewlodge { get; set; }

        private decimal? _rereviewdaycare { get; set; }
        public decimal? ReReviewBuilding
        {
            get
            {
                return this._rereviewbuilding;
            }
            set
            {
                this._rereviewbuilding = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ReReviewElectric
        {
            get
            {
                return this._rereviewelectric;
            }
            set
            {
                this._rereviewelectric = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ReReviewMech
        {
            get
            {
                return this._rereviewmech;
            }
            set
            {
                this._rereviewmech = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ReReviewPlumb
        {
            get
            {
                return this._rereviewplumb;
            }
            set
            {
                this._rereviewplumb = value.HasValue ? value.HasValue ? AdjustReReviewHours(value.Value) : null : null;
            }
        }

        public decimal? ReReviewZoning
        {
            get
            {
                return this._rereviewzoning;
            }
            set
            {
                this._rereviewzoning = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ReReviewFire
        {
            get
            {
                return this._rereviewfire;
            }
            set
            {
                this._rereviewfire = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ReReviewBackFlow
        {
            get
            {
                return this._rereviewbackflow;
            }
            set
            {
                this._rereviewbackflow = value.HasValue ? value.HasValue ? AdjustReReviewHours(value.Value) : null : null;
            }
        }

        public decimal? ReReviewFood
        {
            get
            {
                return this._rereviewfood;
            }
            set
            {
                this._rereviewfood = value.HasValue ? value.HasValue ? AdjustReReviewHours(value.Value) : null : null;
            }
        }

        public decimal? ReReviewPool
        {
            get
            {
                return this._rereviewpool;
            }
            set
            {
                this._rereviewpool = value.HasValue ? value.HasValue ? AdjustReReviewHours(value.Value) : null : null;
            }
        }

        public decimal? ReReviewLodge
        {
            get
            {
                return this._rereviewlodge;
            }
            set
            {
                this._rereviewlodge = value.HasValue ? value.HasValue ? AdjustReReviewHours(value.Value) : null : null;
            }
        }

        public decimal? ReReviewDayCare
        {
            get
            {
                return this._rereviewdaycare;
            }
            set
            {
                this._rereviewdaycare = value.HasValue ? value.HasValue ? AdjustReReviewHours(value.Value) : null : null;
            }
        }

        private decimal? _proposedbuilding { get; set; }
        private decimal? _proposedelectric { get; set; }

        private decimal? _proposedmech { get; set; }

        private decimal? _proposedplumb { get; set; }

        private decimal? _proposedzoning { get; set; }

        private decimal? _proposedfire { get; set; }

        private decimal? _proposedbackflow { get; set; }

        private decimal? _proposedfood { get; set; }

        private decimal? _proposedpool { get; set; }

        private decimal? _proposedlodge { get; set; }

        private decimal? _proposeddaycare { get; set; }

        public decimal? ProposedBuilding
        {
            get
            {
                return this._proposedbuilding;
            }
            set
            {
                this._proposedbuilding = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedElectric
        {
            get
            {
                return this._proposedelectric;
            }
            set
            {
                this._proposedelectric = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedMech
        {
            get
            {
                return this._proposedmech;
            }
            set
            {
                this._proposedmech = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedPlumb
        {
            get
            {
                return this._proposedplumb;
            }
            set
            {
                this._proposedplumb = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedZoning
        {
            get
            {
                return this._proposedzoning;
            }
            set
            {
                this._proposedzoning = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedFire
        {
            get
            {
                return this._proposedfire;
            }
            set
            {
                this._proposedfire = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedBackFlow
        {
            get
            {
                return this._proposedbackflow;
            }
            set
            {
                this._proposedbackflow = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedFood
        {
            get
            {
                return this._proposedfood;
            }
            set
            {
                this._proposedfood = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedPool
        {
            get
            {
                return this._proposedpool;
            }
            set
            {
                this._proposedpool = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedLodge
        {
            get
            {
                return this._proposedlodge;
            }
            set
            {
                this._proposedlodge = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }

        public decimal? ProposedDayCare
        {
            get
            {
                return this._proposeddaycare;
            }
            set
            {
                this._proposeddaycare = value.HasValue ? AdjustReReviewHours(value.Value) : null;
            }
        }


        public decimal AssignedHoursBuilding
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
        public decimal AssignedHoursElectric
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

        public decimal AssignedHoursMech
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

        public decimal AssignedHoursPlumb
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

        public decimal AssignedHoursZoning
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

        public decimal AssignedHoursFire
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

        public decimal AssignedHoursBackflow
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

        public decimal AssignedHoursFood
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

        public decimal AssignedHoursPool
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

        public decimal AssignedHoursLodge
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

        public decimal AssignedHoursDayCare
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

        #endregion

        public string SchedulingMultiplierName { get; set; }
        public string SchedulingMultiplierFactor { get; set; }
        public string[] SchedulingMultiplierProjectTypes { get; set; }

        public string AccelaLink { get; set; }
        public string AccelaProjectDeeplink { get; set; }

        //jcl save these on the front end so when we do updates, we have the correct enum
        public DepartmentNameEnums FireAgency { get; set; }
        public DepartmentNameEnums ZoneAgency { get; set; }
        /// <summary>
        /// LES-3554 jcl 9/27/2021
        /// Retain the original assigned facilitator id to compare
        /// If this is different than the one saved in the dropdown,
        /// call the Update FAcilitator method
        /// </summary>
        public int PreviousAssignedFacilitator { get; set; }

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

        decimal? AdjustReReviewHours(decimal? val)
        {
            decimal hours = 0;
            if (val.HasValue == true)
            {
                hours = val.Value;
                decimal wholeestimationhours = Math.Truncate(hours);
                decimal partestimationhours = hours - wholeestimationhours;
                if (hours % 0.5M != 0)
                {
                    if (partestimationhours > 0.5M)
                    {
                        //add one to truncated value
                        hours = wholeestimationhours + 1.0M;
                    }
                    if (partestimationhours < 0.5M)
                    {
                        //add .5 to truncated value
                        hours = wholeestimationhours + 0.5M;
                    }
                }
            }
            return Math.Round(hours, 1, MidpointRounding.AwayFromZero);
        }

        List<SelectListItem> GenerateSchedulePlanReviewerListViewItems(List<Reviewer> reviewers, DepartmentNameEnums? deptType = null)
        {
            if (reviewers == null)
                reviewers = new List<Reviewer>();
            List<SelectListItem> ret = new List<SelectListItem>();
            List<Reviewer> current = new List<Reviewer>();
            //if no argument is passed to function then ignore switch and get everything. This will be used for exclude reviewer dropdowns so no need for Select Reviewer since it is having its own template for now.
            if (deptType == null || deptType == DepartmentNameEnums.NA)
            {
                current = reviewers;
            }
            else
            {
                ret.Add(new SelectListItem { Text = "NA", Value = "-1" });
                ret.Add(new SelectListItem { Text = "Not Selected", Value = "0" });

                switch (deptType.Value)
                {
                    case DepartmentNameEnums.Building:
                        current = reviewers.Where(x => !ExcludedPlanReviewersBuild.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Electrical:
                        current = reviewers.Where(x => !ExcludedPlanReviewersElectric.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Mechanical:
                        current = reviewers.Where(x => !ExcludedPlanReviewersMech.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Plumbing:
                        current = reviewers.Where(x => !ExcludedPlanReviewersPlumb.Any(y => (int.Parse(y) == x.ID))).ToList();
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
                        current = reviewers.Where(x => !ExcludedPlanReviewersZone.Any(y => (int.Parse(y) == x.ID))).ToList();
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
                        current = reviewers.Where(x => !ExcludedPlanReviewersFire.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        current = reviewers.Where(x => !ExcludedPlanReviewersDayCare.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Food:
                        current = reviewers.Where(x => !ExcludedPlanReviewersFood.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        current = reviewers.Where(x => !ExcludedPlanReviewersPool.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        current = reviewers.Where(x => !ExcludedPlanReviewersLodge.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Backflow:
                        current = reviewers.Where(x => !ExcludedPlanReviewersBackFlow.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                }
            }
            foreach (Reviewer item in current)
            {
                ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });
            }
            return ret;
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
            //if dept is NA then pick the NA option -1
            if (dept == null || dept.EstimationNotApplicable)
            {
                return ret.Value;
            }
            if (dept.AssignedPlanReviewer == null)
                return ret.Value;
            //If dept is not NA but the dept.AssignedPlanReviewer is 0 then that means it is set by autoschdule as Not selected.
            else if (dept.AssignedPlanReviewer != null && dept.AssignedPlanReviewer.ID == 0)
                return notselected.Value;
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
        bool SetScheduledReviewerName(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            if (value == "0")
                dept.AssignedPlanReviewer = new UserIdentity() { ID = 0 };
            else
                dept.AssignedPlanReviewer = AllReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }

        bool SetScheduledReviewerId(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.AssignedPlanReviewer = AllReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }
        #endregion

    }
}