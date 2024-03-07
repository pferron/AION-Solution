using AION.BL;
using AION.BL.Models;
using AION.Web.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class BulkEstimationViewModel : ViewModelBase, IEstimationViewModel
    {
        public BulkEstimationViewModel()
        {
            ReviewersList = new List<Reviewer>();
            FacilitatorList = new List<Facilitator>();
            EstimatorList = new List<EstimatorUIModel>();
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
        }

        public bool IsAllNAChecked { get; set; }

        public List<Reviewer> ReviewersList { get; set; }

        public List<Facilitator> FacilitatorList { get; set; }

        public List<EstimatorUIModel> EstimatorList { get; set; }

        public string PrimaryReviewerBuilding { get; set; }

        public string SecondaryReviewerBuilding { get; set; }

        public string PrimaryReviewerElectrical { get; set; }

        public string SecondaryReviewerelectrical { get; set; }

        public string PrimaryReviewerMechanical { get; set; }

        public string SecondaryReviewerMechanical { get; set; }

        public string PrimaryReviewerPlumbing { get; set; }

        public string SecondaryReviewerPlumbing { get; set; }

        public string PrimaryReviewerFire { get; set; }

        public string SecondaryReviewerFire { get; set; }

        public List<CatalogItem> PermissionMappingCatalogItemList { get; set; }

        public bool HrsNABuilding { get; set; } = false;

        public bool HrsNAElectric { get; set; } = false;

        public bool HrsNAMech { get; set; } = false;

        public bool HrsNAPlumbing { get; set; } = false;

        public bool HrsNAFire { get; set; } = false;

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

        public int AssignedFacilitator { get; set; }

        public decimal HoursBuilding { get; set; } = 0;

        public decimal HoursElectic { get; set; } = 0;

        public decimal HoursMech { get; set; } = 0;

        public decimal HoursPlumb { get; set; } = 0;

        public decimal HoursFire { get; set; } = 0;

        public List<SelectListItem> GeneratePlanReviewerListViewItems(DepartmentNameEnums? deptType = null)
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
                ret.Add(new SelectListItem { Text = "Select Reviewer", Value = "-1" }); //Create a 'not Selected' item
                switch (deptType.Value)
                {
                    case DepartmentNameEnums.Building:
                        current = BuildingReviewers;
                        break;
                    case DepartmentNameEnums.Electrical:
                        current = ElectricalReviewers;
                        break;
                    case DepartmentNameEnums.Mechanical:
                        current = MechanicalReviewers;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        current = PlumbingReviewers;
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
                        current = FireReviewers;
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

    }
}