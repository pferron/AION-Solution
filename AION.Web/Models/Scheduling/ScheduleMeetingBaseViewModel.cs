using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Web.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class ScheduleMeetingBaseViewModel : ViewModelBase
    {
        private List<SelectListItem> _meetingRoomSelectList;
        private List<MeetingRoom> _meetingRoomList;
        private string _meetingroomnameselected;

        public ScheduleMeetingBaseViewModel()
        {
            _meetingRoomSelectList = new List<SelectListItem>();

            Project = new ProjectEstimation();
            ReviewersList = new List<Reviewer>();
            FacilitatorList = new List<Facilitator>();
            EstimatorList = new List<EstimatorUIModel>();
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
            ExcludedPlanReviewersBuild = new List<string>();
            ExcludedPlanReviewersElectric = new List<string>();
            ExcludedPlanReviewersMech = new List<string>();
            ExcludedPlanReviewersPlumb = new List<string>();
            ExcludedPlanReviewersZone = new List<string>();
            ExcludedPlanReviewersFire = new List<string>();
            ExcludedPlanReviewersBackFlow = new List<string>();
            ExcludedPlanReviewersFood = new List<string>();
            ExcludedPlanReviewersPool = new List<string>();
            ExcludedPlanReviewersLodge = new List<string>();
            ExcludedPlanReviewersDayCare = new List<string>();
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
        private ProjectEstimation _project;
        public bool IsAllNAChecked { get; set; }

        public ApplicationNotes BEMPApplicationNotes { get; set; }
        public ApplicationNotes ZoningApplicationNotes { get; set; }

        public ApplicationNotes FireApplicationNotes { get; set; }

        public ApplicationNotes BackFlowApplicationNotes { get; set; }
        public ApplicationNotes EHSApplicationNotes { get; set; }

        public List<Note> NotesComments { get; set; }

        public List<Facilitator> FacilitatorWorkloadSummary { get; set; }

        public ProjectEstimation Project
        {
            get { return _project; }
            set
            {
                _project = value;
                if (_project != null)
                    SetApplicationNotesStatus();
            }
        }

        public List<Reviewer> ReviewersList { get; set; }

        public List<Facilitator> FacilitatorList { get; set; }

        public List<EstimatorUIModel> EstimatorList { get; set; }

        public List<CatalogItem> PermissionMappingCatalogItemList { get; set; }

        private bool _canEditBEMP;

        public bool CanEditBEMP
        {
            get { return _canEditBEMP; }
            set { _canEditBEMP = value; }
        }

        private bool _canViewBEMP;

        public bool CanViewBEMP
        {
            get { return _canViewBEMP; }
            set { _canViewBEMP = value; }
        }

        private bool _canEditZoning;

        public bool CanEditZoning
        {
            get { return _canEditZoning; }
            set { _canEditZoning = value; }
        }

        private bool _canViewZoning;

        public bool CanViewZoning
        {
            get { return _canViewZoning; }
            set { _canViewZoning = value; }
        }

        private bool _canEditFire;

        public bool CanEditFire
        {
            get { return _canEditFire; }
            set { _canEditFire = value; }
        }


        private bool _canViewFire;

        public bool CanViewFire
        {
            get { return _canViewFire; }
            set { _canViewFire = value; }
        }

        private bool _canEditBackFlow;

        public bool CanEditBackFlow
        {
            get { return _canEditBackFlow; }
            set { _canEditBackFlow = value; }
        }

        private bool _canViewBackFlow;

        public bool CanViewBackFlow
        {
            get { return _canViewBackFlow; }
            set { _canViewBackFlow = value; }
        }

        private bool _canEditHealth;

        public bool CanEditHealth
        {
            get { return _canEditHealth; }
            set { _canEditHealth = value; }
        }

        private bool _canViewHealth;

        public bool CanViewHealth
        {
            get { return _canViewHealth; }
            set { _canViewHealth = value; }
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
        private List<SelectListItem> _planReviewersListViewModel;
        public List<SelectListItem> PlanReviewersListViewModel
        {
            get
            {
                if (_planReviewersListViewModel == null)
                    _planReviewersListViewModel = GeneratePlanReviewerListViewItems();
                return _planReviewersListViewModel;
            }
            set
            {
                _planReviewersListViewModel = value;
            }
        }
        List<SelectListItem> _scheduleReviewersListBuild;
        public List<SelectListItem> ScheduleReviewersListBuild
        {
            get
            {
                if (_scheduleReviewersListBuild == null)
                    _scheduleReviewersListBuild = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Building);
                return _scheduleReviewersListBuild;
            }
            set
            {
                _scheduleReviewersListBuild = value;
            }

        }

        List<SelectListItem> _scheduleReviewersListElectric;
        public List<SelectListItem> ScheduleReviewersListElectric
        {
            get
            {
                if (_scheduleReviewersListElectric == null)
                    _scheduleReviewersListElectric = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Electrical);
                return _scheduleReviewersListElectric;
            }
            set
            {
                _scheduleReviewersListElectric = value;
            }

        }



        List<SelectListItem> _scheduleReviewersListPlmub;
        public List<SelectListItem> ScheduleReviewersListPlmub
        {
            get
            {
                if (_scheduleReviewersListPlmub == null)
                    _scheduleReviewersListPlmub = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Plumbing);
                return _scheduleReviewersListPlmub;
            }
            set
            {
                _scheduleReviewersListPlmub = value;
            }

        }

        List<SelectListItem> _scheduleReviewersListMech;
        public List<SelectListItem> ScheduleReviewersListMech
        {
            get
            {
                if (_scheduleReviewersListMech == null)
                    _scheduleReviewersListMech = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Mechanical);
                return _scheduleReviewersListMech;
            }
            set
            {
                _scheduleReviewersListMech = value;
            }

        }

        List<SelectListItem> _scheduleReviewersListZone;
        public List<SelectListItem> ScheduleReviewersListZone
        {
            get
            {
                if (_scheduleReviewersListZone == null)
                    _scheduleReviewersListZone = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Zone_Cornelius);
                return _scheduleReviewersListZone;
            }
            set
            {
                _scheduleReviewersListZone = value;
            }

        }


        List<SelectListItem> _scheduleReviewersListFire;
        public List<SelectListItem> ScheduleReviewersListFire
        {
            get
            {
                if (_scheduleReviewersListFire == null)
                    _scheduleReviewersListFire = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Fire_Cornelius);
                return _scheduleReviewersListFire;
            }
            set
            {
                _scheduleReviewersListFire = value;
            }

        }

        List<SelectListItem> _scheduleReviewersListBackFlow;
        public List<SelectListItem> ScheduleReviewersListBackFlow
        {
            get
            {
                if (_scheduleReviewersListBackFlow == null)
                    _scheduleReviewersListBackFlow = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.Backflow);
                return _scheduleReviewersListBackFlow;
            }
            set
            {
                _scheduleReviewersListBackFlow = value;
            }

        }

        List<SelectListItem> _scheduleReviewersListFood;
        public List<SelectListItem> ScheduleReviewersListFood
        {
            get
            {
                if (_scheduleReviewersListFood == null)
                    _scheduleReviewersListFood = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.EH_Food);
                return _scheduleReviewersListFood;
            }
            set
            {
                _scheduleReviewersListFood = value;
            }

        }


        List<SelectListItem> _scheduleReviewersListPool;
        public List<SelectListItem> ScheduleReviewersListPool
        {
            get
            {
                if (_scheduleReviewersListPool == null)
                    _scheduleReviewersListPool = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.EH_Pool);
                return _scheduleReviewersListPool;
            }
            set
            {
                _scheduleReviewersListPool = value;
            }

        }


        List<SelectListItem> _scheduleReviewersListLodge;
        public List<SelectListItem> ScheduleReviewersListLodge
        {
            get
            {
                if (_scheduleReviewersListLodge == null)
                    _scheduleReviewersListLodge = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.EH_Facilities);
                return _scheduleReviewersListLodge;
            }
            set
            {
                _scheduleReviewersListLodge = value;
            }

        }

        List<SelectListItem> _scheduleReviewersListDayCare;
        public List<SelectListItem> ScheduleReviewersListDayCare
        {
            get
            {
                if (_scheduleReviewersListDayCare == null)
                    _scheduleReviewersListDayCare = GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums.EH_Day_Care);
                return _scheduleReviewersListDayCare;
            }
            set
            {
                _scheduleReviewersListDayCare = value;
            }

        }
        List<SelectListItem> _planReviewersListBuild;
        public List<SelectListItem> PlanReviewersListBuild
        {
            get
            {
                if (_planReviewersListBuild == null)
                    _planReviewersListBuild = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Building);
                return _planReviewersListBuild;
            }
            set
            {
                _planReviewersListBuild = value;
            }

        }

        List<SelectListItem> _planReviewersListElectric;
        public List<SelectListItem> PlanReviewersListElectric
        {
            get
            {
                if (_planReviewersListElectric == null)
                    _planReviewersListElectric = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Electrical);
                return _planReviewersListElectric;
            }
            set
            {
                _planReviewersListElectric = value;
            }

        }



        List<SelectListItem> _planReviewersListPlmub;
        public List<SelectListItem> PlanReviewersListPlmub
        {
            get
            {
                if (_planReviewersListPlmub == null)
                    _planReviewersListPlmub = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Plumbing);
                return _planReviewersListPlmub;
            }
            set
            {
                _planReviewersListPlmub = value;
            }

        }

        List<SelectListItem> _planReviewersListMech;
        public List<SelectListItem> PlanReviewersListMech
        {
            get
            {
                if (_planReviewersListMech == null)
                    _planReviewersListMech = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Mechanical);
                return _planReviewersListMech;
            }
            set
            {
                _planReviewersListMech = value;
            }

        }

        List<SelectListItem> _planReviewersListZone;
        public List<SelectListItem> PlanReviewersListZone
        {
            get
            {
                if (_planReviewersListZone == null)
                    _planReviewersListZone = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Zone_Cornelius);
                return _planReviewersListZone;
            }
            set
            {
                _planReviewersListZone = value;
            }

        }


        List<SelectListItem> _PlanReviewersListFire;
        public List<SelectListItem> PlanReviewersListFire
        {
            get
            {
                if (_PlanReviewersListFire == null)
                    _PlanReviewersListFire = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Fire_Cornelius);
                return _PlanReviewersListFire;
            }
            set
            {
                _PlanReviewersListFire = value;
            }

        }

        List<SelectListItem> _planReviewersListBackFlow;
        public List<SelectListItem> PlanReviewersListBackFlow
        {
            get
            {
                if (_planReviewersListBackFlow == null)
                    _planReviewersListBackFlow = GeneratePlanReviewerListViewItems(DepartmentNameEnums.Backflow);
                return _planReviewersListBackFlow;
            }
            set
            {
                _planReviewersListBackFlow = value;
            }

        }

        List<SelectListItem> _planReviewersListFood;
        public List<SelectListItem> PlanReviewersListFood
        {
            get
            {
                if (_planReviewersListFood == null)
                    _planReviewersListFood = GeneratePlanReviewerListViewItems(DepartmentNameEnums.EH_Food);
                return _planReviewersListFood;
            }
            set
            {
                _planReviewersListFood = value;
            }

        }


        List<SelectListItem> _planReviewersListPool;
        public List<SelectListItem> PlanReviewersListPool
        {
            get
            {
                if (_planReviewersListPool == null)
                    _planReviewersListPool = GeneratePlanReviewerListViewItems(DepartmentNameEnums.EH_Pool);
                return _planReviewersListPool;
            }
            set
            {
                _planReviewersListPool = value;
            }

        }


        List<SelectListItem> _planReviewersListLodge;
        public List<SelectListItem> PlanReviewersListLodge
        {
            get
            {
                if (_planReviewersListLodge == null)
                    _planReviewersListLodge = GeneratePlanReviewerListViewItems(DepartmentNameEnums.EH_Facilities);
                return _planReviewersListLodge;
            }
            set
            {
                _planReviewersListLodge = value;
            }

        }

        List<SelectListItem> _planReviewersListDayCare;
        public List<SelectListItem> PlanReviewersListDayCare
        {
            get
            {
                if (_planReviewersListDayCare == null)
                    _planReviewersListDayCare = GeneratePlanReviewerListViewItems(DepartmentNameEnums.EH_Day_Care);
                return _planReviewersListDayCare;
            }
            set
            {
                _planReviewersListDayCare = value;
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

        public List<string> ExcludedPlanReviewersBuild { get; set; }

        public List<string> ExcludedPlanReviewersElectric { get; set; }

        public List<string> ExcludedPlanReviewersMech { get; set; }

        public List<string> ExcludedPlanReviewersPlumb { get; set; }

        public List<string> ExcludedPlanReviewersZone { get; set; }

        public List<string> ExcludedPlanReviewersFire { get; set; }

        public List<string> ExcludedPlanReviewersBackFlow { get; set; }

        public List<string> ExcludedPlanReviewersFood { get; set; }

        public List<string> ExcludedPlanReviewersPool { get; set; }

        public List<string> ExcludedPlanReviewersLodge { get; set; }

        public List<string> ExcludedPlanReviewersDayCare { get; set; }

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
        public string GetProposedPlanReviewerName(DepartmentNameEnums deptType)
        {
            string retval = "";
            var dept = GetDepartment(deptType);
            if (dept != null)
            {
                if (dept.ProposedPlanReviewer.ID != -1)
                {
                    retval = dept.ProposedPlanReviewer.FirstName + " " + dept.ProposedPlanReviewer.LastName;
                }
                else
                {
                    retval = dept.IsDeptRequested ? "Y" : "N";
                }
            }
            return retval;
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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ScheduleDate { get; set; }
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
        public string InternalNotes { get; set; }
        public DateTime ProposedDate1 { get; set; }
        public DateTime ProposedDate2 { get; set; }
        public DateTime ProposedDate3 { get; set; }
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

        string GetPrimaryReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetPlanReviewerListByDept(deptType);
            SelectListItem ret = current.Where(x => x.Value == "-1").FirstOrDefault();  // If the reviewer found as selected then select that person else select an empty list item.
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null || dept.PrimaryPlanReviewer == null || dept.PrimaryPlanReviewer.ID == 0)
                return ret.Value;
            var val = current.Where(x => x.Value == dept.PrimaryPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Value : null;
        }

        bool SetPrimaryReviewerName(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.PrimaryPlanReviewer = ReviewersList.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }

        string GetSecondaryReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetPlanReviewerListByDept(deptType);
            SelectListItem ret = current.Where(x => x.Value == "-1").FirstOrDefault(); // If the reviewer found as selected then select that person else select an empty list item.
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null || dept.SecondaryPlanReviewer == null || dept.SecondaryPlanReviewer.ID == 0)
                return ret.Value;
            var val = current.Where(x => x.Value == dept.SecondaryPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Value : null;
        }

        List<SelectListItem> GetPlanReviewerListByDept(DepartmentNameEnums? deptType)
        {
            if (deptType == null || deptType == DepartmentNameEnums.NA)
                //returns all items.
                return GeneratePlanReviewerListViewItems();
            switch (deptType.Value)
            {
                case DepartmentNameEnums.Building:
                    return PlanReviewersListBuild;
                case DepartmentNameEnums.Electrical:
                    return PlanReviewersListElectric;
                case DepartmentNameEnums.Mechanical:
                    return PlanReviewersListMech;
                case DepartmentNameEnums.Plumbing:
                    return PlanReviewersListPlmub;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return PlanReviewersListZone;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return PlanReviewersListFire;
                case DepartmentNameEnums.EH_Day_Care:
                    return PlanReviewersListDayCare;
                case DepartmentNameEnums.EH_Food:
                    return PlanReviewersListFood;
                case DepartmentNameEnums.EH_Pool:
                    return PlanReviewersListPool;
                case DepartmentNameEnums.EH_Facilities:
                    return PlanReviewersListLodge;
                case DepartmentNameEnums.Backflow:
                    return PlanReviewersListBackFlow;
                default:
                    throw new ArgumentException("Unexpected option! Contact IT Adminstrator.");
            }
        }
        List<SelectListItem> GetSchedulePlanReviewerListByDept(DepartmentNameEnums? deptType)
        {
            if (deptType == null || deptType == DepartmentNameEnums.NA)
                //returns all items.
                return GenerateSchedulePlanReviewerListViewItems();
            switch (deptType.Value)
            {
                case DepartmentNameEnums.Building:
                    return ScheduleReviewersListBuild;
                case DepartmentNameEnums.Electrical:
                    return ScheduleReviewersListElectric;
                case DepartmentNameEnums.Mechanical:
                    return ScheduleReviewersListMech;
                case DepartmentNameEnums.Plumbing:
                    return ScheduleReviewersListPlmub;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return ScheduleReviewersListZone;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return ScheduleReviewersListFire;
                case DepartmentNameEnums.EH_Day_Care:
                    return ScheduleReviewersListDayCare;
                case DepartmentNameEnums.EH_Food:
                    return ScheduleReviewersListFood;
                case DepartmentNameEnums.EH_Pool:
                    return ScheduleReviewersListPool;
                case DepartmentNameEnums.EH_Facilities:
                    return ScheduleReviewersListLodge;
                case DepartmentNameEnums.Backflow:
                    return ScheduleReviewersListBackFlow;
                default:
                    throw new ArgumentException("Unexpected option! Contact IT Adminstrator.");
            }
        }

        bool SetSecondaryReviewerName(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.SecondaryPlanReviewer = ReviewersList.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }

        ProjectDepartment GetDepartment(DepartmentNameEnums deptType)
        {
            Helper helper = new Helper();
            if (Project == null)
                return null;
            if (deptType == DepartmentNameEnums.Electrical || deptType == DepartmentNameEnums.Mechanical || deptType == DepartmentNameEnums.Plumbing || deptType == DepartmentNameEnums.Building)
                return Project.Trades.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
            else
            {
                //in the property the first fire department is assigned. So assume it is only one fire at a time and then pick the related fire dept from list.
                if (helper.FireDepartmentNames.Contains(deptType))
                {
                    return Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                }
                //in the property the first Zone department is assigned. So assume it is only one fire at a time and then pick the related zone dept from list.
                else if (helper.ZoneDepartmentNames.Contains(deptType))
                {
                    return Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                }
                else
                {
                    return Project.Agencies.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
                }
            }
        }
        List<SelectListItem> GeneratePlanReviewerListViewItems(DepartmentNameEnums? deptType = null)
        {
            if (ReviewersList == null)
                ReviewersList = new List<Reviewer>();
            List<SelectListItem> ret = new List<SelectListItem>();
            List<Reviewer> current = new List<Reviewer>();
            //if no argument is passed to function then ignore switch and get everything. This will be used for exclude reviewer dropdowns so no need for Select Reviewer since it is having its own template for now.
            if (deptType == null || deptType == DepartmentNameEnums.NA)
            {
                current = ReviewersList;
            }
            else
            {
                ret.Add(new SelectListItem { Text = "Not Selected", Value = "-1" });

                switch (deptType.Value)
                {
                    case DepartmentNameEnums.Building:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersBuild.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Electrical:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersElectric.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Mechanical:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersMech.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Plumbing:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersPlumb.Any(y => (int.Parse(y) == x.ID))).ToList();
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
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersZone.Any(y => (int.Parse(y) == x.ID))).ToList();
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
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersFire.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersDayCare.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Food:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersFood.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersPool.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersLodge.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Backflow:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersBackFlow.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    default:
                        throw new ArgumentException("Unexpected option in plan reviewer selection! Contact IT Adminstrator.");
                }
            }
            foreach (var item in current)
            {
                ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });
            }
            return ret;
        }
        List<SelectListItem> GenerateSchedulePlanReviewerListViewItems(DepartmentNameEnums? deptType = null)
        {
            if (ReviewersList == null)
                ReviewersList = new List<Reviewer>();
            List<SelectListItem> ret = new List<SelectListItem>();
            List<Reviewer> current = new List<Reviewer>();
            //if no argument is passed to function then ignore switch and get everything. This will be used for exclude reviewer dropdowns so no need for Select Reviewer since it is having its own template for now.
            if (deptType == null || deptType == DepartmentNameEnums.NA)
            {
                current = ReviewersList;
            }
            else
            {
                ret.Add(new SelectListItem { Text = "NA", Value = "-1" });
                ret.Add(new SelectListItem { Text = "Not Selected", Value = "0" });

                switch (deptType.Value)
                {
                    case DepartmentNameEnums.Building:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersBuild.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Electrical:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersElectric.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Mechanical:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersMech.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Plumbing:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersPlumb.Any(y => (int.Parse(y) == x.ID))).ToList();
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
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersZone.Any(y => (int.Parse(y) == x.ID))).ToList();
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
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersFire.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersDayCare.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Food:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersFood.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersPool.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersLodge.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    case DepartmentNameEnums.Backflow:
                        current = ReviewersList.Where(x => !ExcludedPlanReviewersBackFlow.Any(y => (int.Parse(y) == x.ID))).ToList();
                        break;
                    default:
                        throw new ArgumentException("Unexpected option in plan reviewer selection! Contact IT Adminstrator.");
                }
            }
            foreach (var item in current)
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
        bool GetIsNA(DepartmentNameEnums deptType)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept != null)
                return dept.EstimationNotApplicable;
            else
                // I am assuming dept shoud be added from accela or later in pre-estimation process. 
                // So if department not found in project list then assume that is not applicable for this specific application.
                return true;
        }

        bool SetIsNA(DepartmentNameEnums deptType, bool value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.EstimationNotApplicable = value;
            return true;
        }
        private void SetApplicationNotesStatus()
        {
            foreach (ProjectTrade trade in Project.Trades)
            {
                if (trade.DepartmentStatusRef != null)
                    SetApplicationNotesStatusByProjDept(trade.DepartmentInfo, trade.DepartmentStatusRef.ProjectStatusEnum);
                else
                    SetApplicationNotesStatusByProjDept(trade.DepartmentInfo, ProjectStatusEnum.Estimation_In_Progress);
            }
            foreach (ProjectAgency agency in Project.Agencies)
            {
                if (agency.DepartmentStatusRef != null)
                    SetApplicationNotesStatusByProjDept(agency.DepartmentInfo, agency.DepartmentStatusRef.ProjectStatusEnum);
                else
                    SetApplicationNotesStatusByProjDept(agency.DepartmentInfo, ProjectStatusEnum.Estimation_In_Progress);
            }
        }

        private void SetApplicationNotesStatusByProjDept(DepartmentNameEnums deptname, ProjectStatusEnum status)
        {
            //BackFlowApplicationNotes.PendingReason = ProjectStatusEnum.Ready_for_Estimator
            switch (deptname)
            {
                case DepartmentNameEnums.Building:
                case DepartmentNameEnums.Electrical:
                case DepartmentNameEnums.Mechanical:
                case DepartmentNameEnums.Plumbing:
                    BEMPApplicationNotes.PendingReason = status;
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
                    ZoningApplicationNotes.PendingReason = status;
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
                    FireApplicationNotes.PendingReason = status;
                    break;
                case DepartmentNameEnums.EH_Day_Care:
                case DepartmentNameEnums.EH_Food:
                case DepartmentNameEnums.EH_Pool:
                case DepartmentNameEnums.EH_Facilities:
                    EHSApplicationNotes.PendingReason = status;
                    break;
                case DepartmentNameEnums.Backflow:
                    BackFlowApplicationNotes.PendingReason = status;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// gets the name of the reviewer that was previously scheduled on save 
        /// or that the user requested on the application
        /// </summary>
        /// <param name="deptType"></param>
        /// <returns></returns>
        string GetScheduledReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetSchedulePlanReviewerListByDept(deptType);
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
            dept.AssignedPlanReviewer = ReviewersList.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }
        #endregion

    }
}