using AION.BL;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Web.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class EstimationViewModel : ViewModelBase, IEstimationViewModel
    {
        private List<SelectListItem> _yNSelectList;
        private List<SelectListItem> _projectTypeList;
        public EstimationViewModel()
        {
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
            PendingReasonList = new List<SelectListItem>();
            NotesComments = new List<Note>();
            FacilitatorWorkloadSummary = new List<Facilitator>();
            BackFlowApplicationNotes = new ApplicationNotes();
            BEMPApplicationNotes = new ApplicationNotes();
            EHSApplicationNotes = new ApplicationNotes();
            FireApplicationNotes = new ApplicationNotes();
            ZoningApplicationNotes = new ApplicationNotes();
            PreviousProject = new ProjectEstimation();
            StandardNoteGroups = new List<StandardNoteGroupEnums>();
            StandardNotes = new List<StandardNote>();
        }

        public ProjectEstimation PreviousProject { get; set; }
        public int ProjectTypeEnumSelected { get; set; }
        public List<SelectListItem> ProjectTypeList
        {
            get
            {
                if (_projectTypeList == null) BuildProjectTypeList();
                return _projectTypeList;
            }
            set
            {
                _projectTypeList = value;
            }
        }
        private bool _isEstimationComplete;
        List<SelectListItem> _pendingReasonList;

        public List<SelectListItem> PendingReasonList
        {
            get
            {
                if (_pendingReasonList.Any() == false)
                    _pendingReasonList = GeneratPendingReasonListViewItems();
                return _pendingReasonList;
            }
            set
            {
                _pendingReasonList = value;
            }
        }

        public bool IsAllNAChecked { get; set; }

        public List<Note> NotesComments { get; set; }

        public List<Facilitator> FacilitatorWorkloadSummary { get; set; }

        public List<Reviewer> ReviewersList { get; set; }

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


        private List<SelectListItem> _estimatorsListViewModel;

        public List<SelectListItem> EstimatorsListViewModel
        {
            get
            {
                if (_estimatorsListViewModel == null)
                    _estimatorsListViewModel = GenerateEstimatorListViewItems();
                return _estimatorsListViewModel;
            }
            set { _estimatorsListViewModel = value; }
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Building);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Electrical);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Mechanical);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Plumbing);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Zone_Cornelius);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Fire_Cornelius);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.Backflow);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.EH_Food);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.EH_Pool);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.EH_Facilities);
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
                return GetEstimationPrimaryReviewerName(DepartmentNameEnums.EH_Day_Care);
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

        public string AccelaProjectDeeplink { get; set; }

        #region "Private"
        private void BuildProjectTypeList()
        {
            _projectTypeList = new List<SelectListItem>();
            _projectTypeList.Add(new SelectListItem { Text = "Select Project Type", Value = "-1" }); //Create a 'not Selected' item
            foreach (PropertyTypeEnums item in Enum.GetValues(typeof(PropertyTypeEnums)))
            {
                switch (item)
                {
                    case PropertyTypeEnums.Commercial:
                    case PropertyTypeEnums.Mega_Multi_Family:
                    case PropertyTypeEnums.Special_Projects_Team:
                    case PropertyTypeEnums.Townhomes:
                    case PropertyTypeEnums.FIFO_Small_Commercial:
                    case PropertyTypeEnums.FIFO_Single_Family_Homes:
                    case PropertyTypeEnums.FIFO_Master_Plans:
                    case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    case PropertyTypeEnums.County_Fire_Shop_Drawings:
                        _projectTypeList.Add(new SelectListItem { Text = item.ToStringValue(), Value = ((int)item).ToString() });
                        break;
                    default:
                        break;
                }
            }
        }
        private void BuildYNList()
        {
            _yNSelectList = new List<SelectListItem>();
            _yNSelectList.Add(new SelectListItem { Text = "N", Value = "N" });
            _yNSelectList.Add(new SelectListItem { Text = "Y", Value = "Y" });
        }
        public List<SelectListItem> YNSelectList
        {
            get
            {
                if (_yNSelectList == null) BuildYNList();
                return _yNSelectList;
            }
        }
        public string ExpressYNSelected { get; set; }

        decimal GetDefaulthours(DepartmentNameEnums deptType)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            decimal defaulthours = 0;

            if (dept != null && dept.EstimationHours.HasValue == true)
            {
                defaulthours = dept.EstimationHours.Value;
            }


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

        string GetEstimationPrimaryReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetPlanReviewerListByDept(deptType);
            SelectListItem ret = NANotSelectedBaseList.Where(x => x.Value == "-1").FirstOrDefault();  // If the reviewer found as selected then select that person else select an empty list item.
            ProjectDepartment dept = GetDepartment(deptType);
            //if (dept == null)
            //    return ret.Value;
            //if RTAP and no one has been chosen
            if (Project.IsProjectRTAP && PreviousProject != null && PreviousProject.ID > 0 && dept != null)
            {
                //set the primary to the scheduled reviewer if they are schedulable
                if (dept.PrimaryPlanReviewer == null || dept.PrimaryPlanReviewer.ID <= 0)
                {
                    if (BEMPDepartmentNames.Where(x => x == deptType).Any())
                    {
                        ProjectTrade trade = PreviousProject.Trades.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
                        if (trade != null && trade.AssignedPlanReviewer.ID > 0 && trade.AssignedPlanReviewer.IsSchedulable)
                        {
                            //set the primary reviewer
                            var reviewer = current.Where(x => x.Value == trade.AssignedPlanReviewer.ID.ToString());
                            return reviewer.Any() ? reviewer.FirstOrDefault().Value : null;
                        }
                    }
                    else
                    {
                        ProjectAgency agency = new ProjectAgency();
                        if (dept.DepartmentDivision == DepartmentDivisionEnum.Fire || dept.DepartmentDivision == DepartmentDivisionEnum.Zoning)
                        {
                            if (dept.DepartmentDivision == DepartmentDivisionEnum.Zoning)
                                agency = PreviousProject.Agencies.Where(x => ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                            if (dept.DepartmentDivision == DepartmentDivisionEnum.Fire)
                                agency = PreviousProject.Agencies.Where(x => FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                        }
                        else
                        {
                            agency = PreviousProject.Agencies.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();

                        }
                        if (agency != null && agency.AssignedPlanReviewer.ID > 0 && agency.AssignedPlanReviewer.IsSchedulable)
                        {
                            //set the primary reviewer
                            var reviewer = current.Where(x => x.Value == agency.AssignedPlanReviewer.ID.ToString());
                            return reviewer.Any() ? reviewer.FirstOrDefault().Value : null;
                        }
                    }
                }

            }

            if (dept == null || dept.PrimaryPlanReviewer == null || dept.PrimaryPlanReviewer.ID == 0)
                return ret.Value;
            var val = current.Where(x => x.Value == dept.PrimaryPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Value : null;
        }

        List<SelectListItem> GeneratPendingReasonListViewItems()
        {
            if (PermissionMappingCatalogItemList == null)
                PermissionMappingCatalogItemList = new List<CatalogItem>();
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(new SelectListItem { Text = "Select Pending Reason", Value = "-1" }); //Create a 'not Selected' item
            foreach (var item in PermissionMappingCatalogItemList)
            {
                ret.Add(new SelectListItem { Text = item.Value, Value = item.SubKey.ToString() });
            }
            return ret;
        }

        public List<SelectListItem> GenerateFacilitatorListViewItems()
        {
            if (FacilitatorList == null)
                FacilitatorList = new List<Facilitator>();
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(new SelectListItem { Text = "Select Facilitator", Value = "-1" }); //Create a 'not Selected' item
            foreach (var item in FacilitatorList)
            {
                ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });
            }
            return ret;
        }
        public List<SelectListItem> GenerateEstimatorListViewItems()
        {
            if (EstimatorList == null)
                EstimatorList = new List<EstimatorUIModel>();
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(new SelectListItem { Text = "Select Estimator", Value = "-1" }); //Create a 'not Selected' item
            foreach (var item in EstimatorList)
            {
                ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });
            }
            return ret;
        }
        #endregion
    }
}