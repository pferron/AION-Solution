using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Web.BusinessEntities;
using AION.Web.Models.Scheduling;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class ViewModelBase : AutoScheduleAuditViewModel
    {
        private Helper _helper;
        public ViewModelBase()
        {
            _helper = new Helper();
            UIStatusMessage = UIStatusMessage.NA;


            var httpContext = HttpContext.Current;
            var request = httpContext.Request;

            // for breadcrumbs
            Controller = request.RequestContext.RouteData.Values["controller"].ToString();
            Action = request.RequestContext.RouteData.Values["action"].ToString();
            AccelaBaseLink = System.Configuration.ConfigurationManager.AppSettings["AccelaBaseLink"].ToString();

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
            Project = new ProjectEstimation();
            BackFlowApplicationNotes = new ApplicationNotes();
            BEMPApplicationNotes = new ApplicationNotes();
            EHSApplicationNotes = new ApplicationNotes();
            FireApplicationNotes = new ApplicationNotes();
            ZoningApplicationNotes = new ApplicationNotes();
            AllReviewers = new List<Reviewer>();

            AccelaProjectDeeplink = GenerateAccelaDeeplink(Project.AccelaProjectRefId);

            SetMenuItems();
        }
        #region UI LoggedInUser and Permissions
        public UserIdentity LoggedInUser { get; set; }

        // for breadcrumbs
        public bool IsUserInternal { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string AccelaBaseLink { get; set; }
        public string AccelaProjectDeeplink { get; set; }

        #region Breadcrumbs
        public string MenuHeading { get; set; }
        public string MenuLink { get; set; }
        public string MenuLinkText { get; set; }
        public string ActionText { get; set; }
        #endregion

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

        #endregion UI LoggedInUser and Permissions

        #region UI Helpers
        public List<DepartmentNameEnums> FireDepartmentNames
        {
            get
            {
                return _helper.FireDepartmentNames;
            }
        }
        public List<DepartmentNameEnums> ZoneDepartmentNames
        {
            get
            {
                return _helper.ZoneDepartmentNames;
            }
        }
        public List<DepartmentNameEnums> EhsDepartmentNames
        {
            get
            {
                return _helper.EhsDepartmentNames;
            }
        }
        public List<DepartmentNameEnums> BEMPDepartmentNames
        {
            get
            {
                return _helper.BEMPDepartmentNames;
            }
        }
        public bool GetEstimationComplete(List<ProjectAgency> agencies, List<ProjectTrade> trades)
        {
            if (agencies == null) agencies = new List<ProjectAgency>();
            if (trades == null) trades = new List<ProjectTrade>();
            int agencycount = agencies.Count();
            int tradecount = trades.Count();
            int deptcount = agencycount + tradecount;

            int completeagencycount = agencies.Where(x => x.DepartmentStatus != null && (x.EstimationNotApplicable == true
                || (x.DepartmentDivision == DepartmentDivisionEnum.NA && x.DepartmentInfo == DepartmentNameEnums.NA)
                || x.DepartmentStatus.Equals(ProjectDisplayStatus.Complete, System.StringComparison.OrdinalIgnoreCase))).Count();
            int completetradecount = trades.Where(x => x.DepartmentStatus != null && (x.EstimationNotApplicable == true
                || (x.DepartmentDivision == DepartmentDivisionEnum.NA && x.DepartmentInfo == DepartmentNameEnums.NA)
                || x.DepartmentStatus.Equals(ProjectDisplayStatus.Complete, System.StringComparison.OrdinalIgnoreCase))).Count();
            int completedeptcount = completeagencycount + completetradecount;

            return deptcount == completedeptcount;
        }
        public string LoggedUserDisplayName
        {
            get
            {
                if (LoggedInUser == null || LoggedInUser.ID == 0)
                    return "Anonymous User";
                else
                    return LoggedInUser.FirstName + " " + LoggedInUser.LastName;
            }
        }
        public string LoggedInUserEmail { get; set; }
        public PermissionMapping PermissionMapping { get; set; }
        public string DisabledCls { get; set; }
        public string DisabledHtml { get; set; }
        public string ReadonlyHtml { get; set; }
        public bool IsReadOnly { get; set; }
        public string LoggingId { get; set; }
        public UIStatusMessage UIStatusMessage { get; set; }


        private List<SelectListItem> _deptNameList;
        public List<SelectListItem> DeptNameList
        {
            get
            {
                if (_deptNameList == null) BuildDepartmentList();
                return _deptNameList;
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

        #endregion  UI Helpers

        #region Reviewer Lists

        public DepartmentNameEnums ZoneJurisdiction { get { return GetJuridiction("Zone"); } }

        public DepartmentNameEnums FireJurisdiction { get { return GetJuridiction("Fire"); } }

        private List<Reviewer> _allreviewers;
        public List<Reviewer> AllReviewers
        {
            get
            {
                return _allreviewers;
            }
            set
            {
                _allreviewers = value;
            }
        }

        private List<Reviewer> _allJurisdictionReviewers;
        /// <summary>
        /// Returns all reviewers based on the ZoneJurisdiction and FireJurisdiction properties set by the Project
        /// </summary>
        public List<Reviewer> AllJurisdictionReviewers
        {
            get
            {
                if (_allreviewers != null)
                {
                    _allJurisdictionReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.NA, DepartmentNameEnums.NA, true);
                }
                return _allJurisdictionReviewers;
            }
        }

        #region Reviewers list by dept
        /****************************************************************************/
        /*              Reviewers list by dept     */
        private List<Reviewer> _buildingReviewers;
        public List<Reviewer> BuildingReviewers
        {
            get
            {
                if (_buildingReviewers == null)
                    _buildingReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Building, DepartmentNameEnums.Building, true);
                return _buildingReviewers;
            }
            set { _buildingReviewers = value; }
        }
        private List<Reviewer> _electricalReviewers;
        public List<Reviewer> ElectricalReviewers
        {
            get
            {
                if (_electricalReviewers == null)
                    _electricalReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Electrical, DepartmentNameEnums.Electrical, true);

                return _electricalReviewers;
            }
            set { _electricalReviewers = value; }
        }
        private List<Reviewer> _mechanicalReviewers;

        public List<Reviewer> MechanicalReviewers
        {
            get
            {
                if (_mechanicalReviewers == null)
                    _mechanicalReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Mechanical, DepartmentNameEnums.Mechanical, true);
                return _mechanicalReviewers;
            }
            set { _mechanicalReviewers = value; }
        }
        private List<Reviewer> _plumbingReviewers;

        public List<Reviewer> PlumbingReviewers
        {
            get
            {
                if (_plumbingReviewers == null)
                    _plumbingReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Plumbing, DepartmentNameEnums.Plumbing, true);
                return _plumbingReviewers;
            }
            set { _plumbingReviewers = value; }
        }
        private List<Reviewer> _fireJurisdictionReviewers;

        public List<Reviewer> FireJurisdictionReviewers
        {
            get
            {
                if (_fireJurisdictionReviewers == null)
                    _fireJurisdictionReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Fire, FireJurisdiction, true);
                return _fireJurisdictionReviewers;
            }
            set { _fireJurisdictionReviewers = value; }
        }


        private List<Reviewer> _fireReviewers;
        /// <summary>
        /// All reviewers that have any Fire permission
        /// does not use jurisdiction
        /// </summary>
        public List<Reviewer> FireReviewers
        {
            get
            {
                if (_fireReviewers == null)
                    _fireReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Fire, DepartmentNameEnums.Fire_Cornelius, false);
                return _fireReviewers;
            }
            set { _fireReviewers = value; }
        }
        private List<Reviewer> _fireCountyReviewers;

        public List<Reviewer> FireCountyReviewers
        {
            get
            {
                if (_fireCountyReviewers == null)
                    _fireCountyReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Fire, DepartmentNameEnums.Fire_Cornelius, true);
                return _fireCountyReviewers;
            }
            set { _fireCountyReviewers = value; }
        }

        private List<Reviewer> _fireCityReviewers;

        public List<Reviewer> FireCityReviewers
        {
            get
            {
                if (_fireCityReviewers == null)
                    _fireCityReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Fire, DepartmentNameEnums.Fire_Cty_Chrlt, true);
                return _fireCityReviewers;
            }
            set { _fireCityReviewers = value; }
        }
        private List<Reviewer> _zoningJurisdictionReviewers;

        public List<Reviewer> ZoningJurisdictionReviewers
        {
            get
            {
                if (_zoningJurisdictionReviewers == null)
                    _zoningJurisdictionReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Zoning, ZoneJurisdiction, true);
                return _zoningJurisdictionReviewers;
            }
            set { _zoningJurisdictionReviewers = value; }
        }

        private List<Reviewer> _zoningReviewers;

        /// <summary>
        /// All reviewers that have any zoning permission
        /// does not use jurisdiction
        /// </summary>
        public List<Reviewer> ZoningReviewers
        {
            get
            {
                if (_zoningReviewers == null)
                    _zoningReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Zoning, DepartmentNameEnums.Zone_Cornelius, false);
                return _zoningReviewers;
            }
            set { _zoningReviewers = value; }
        }

        private List<Reviewer> _zoningCountyReviewers;

        public List<Reviewer> ZoningCountyReviewers
        {
            get
            {
                if (_zoningCountyReviewers == null)
                    _zoningCountyReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Zoning, DepartmentNameEnums.Zone_Cornelius, true);
                return _zoningCountyReviewers;
            }
            set { _zoningCountyReviewers = value; }
        }


        private List<Reviewer> _zoningMintHillReviewers;

        public List<Reviewer> ZoningMintHillReviewers
        {
            get
            {
                if (_zoningMintHillReviewers == null)
                    _zoningMintHillReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Zoning, DepartmentNameEnums.Zone_Mint_Hill, true);
                return _zoningMintHillReviewers;
            }
            set { _zoningMintHillReviewers = value; }
        }


        private List<Reviewer> _zoningHuntersvilleReviewers;

        public List<Reviewer> ZoningHuntersvilleReviewers
        {
            get
            {
                if (_zoningHuntersvilleReviewers == null)
                    _zoningHuntersvilleReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Zoning, DepartmentNameEnums.Zone_Huntersville, true);
                return _zoningHuntersvilleReviewers;
            }
            set { _zoningHuntersvilleReviewers = value; }
        }

        private List<Reviewer> _zoningCityReviewers;

        public List<Reviewer> ZoningCityReviewers
        {
            get
            {
                if (_zoningCityReviewers == null)
                    _zoningCityReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Zoning, DepartmentNameEnums.Zone_Cty_Chrlt, true);
                return _zoningCityReviewers;
            }
            set { _zoningCityReviewers = value; }
        }

        private List<Reviewer> _backflowReviewers;

        public List<Reviewer> BackflowReviewers
        {
            get
            {
                if (_backflowReviewers == null)
                    _backflowReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Backflow, DepartmentNameEnums.Backflow, true);
                return _backflowReviewers;
            }
            set { _backflowReviewers = value; }
        }
        private List<Reviewer> _foodReviewers;

        public List<Reviewer> FoodReviewers
        {
            get
            {
                if (_foodReviewers == null)
                    _foodReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Food, true);
                return _foodReviewers;
            }
            set { _foodReviewers = value; }
        }
        private List<Reviewer> _poolReviewers;

        public List<Reviewer> PoolReviewers
        {
            get
            {
                if (_poolReviewers == null)
                    _poolReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Pool, true);
                return _poolReviewers;
            }
            set { _poolReviewers = value; }
        }
        private List<Reviewer> _facilityReviewers;

        public List<Reviewer> FacilityReviewers
        {
            get
            {
                if (_facilityReviewers == null)
                    _facilityReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Facilities, true);
                return _facilityReviewers;
            }
            set { _facilityReviewers = value; }
        }
        private List<Reviewer> _daycareReviewers;

        public List<Reviewer> DaycareReviewers
        {
            get
            {
                if (_daycareReviewers == null)
                    _daycareReviewers = GetDeptReviewers(_allreviewers, DepartmentDivisionEnum.Environmental, DepartmentNameEnums.EH_Day_Care, true);
                return _daycareReviewers;
            }
            set { _daycareReviewers = value; }
        }

        /**************************************************************************************/
        #endregion Reviewers list by dept - All reviewers

        #region Reviewer Select lists
        /***************************************************************************/
        private List<SelectListItem> _nANotSelectedBaseList;
        public List<SelectListItem> NANotSelectedBaseList
        {
            get
            {
                if (_nANotSelectedBaseList == null)
                {
                    _nANotSelectedBaseList = new List<SelectListItem>();
                    _nANotSelectedBaseList.Add(new SelectListItem { Text = "NA", Value = "-1" });
                    _nANotSelectedBaseList.Add(new SelectListItem { Text = "Not Selected", Value = "0" });
                }
                return _nANotSelectedBaseList;
            }
        }

        private List<SelectListItem> _notSelectedBaseList;
        public List<SelectListItem> NotSelectedBaseList
        {
            get
            {
                if (_notSelectedBaseList == null)
                {
                    _notSelectedBaseList = new List<SelectListItem>();
                    _notSelectedBaseList.Add(new SelectListItem { Text = "Not Selected", Value = "0" });
                }
                return _notSelectedBaseList;

            }
        }

        private List<SelectListItem> _selectReviewerBaseList;
        public List<SelectListItem> SelectReviewerBaseList
        {
            get
            {
                if (_selectReviewerBaseList == null)
                {
                    _selectReviewerBaseList = new List<SelectListItem>();
                    _selectReviewerBaseList.Add(new SelectListItem { Text = "Select Reviewer", Value = "-1" });
                }
                return _selectReviewerBaseList;

            }
        }

        private List<SelectListItem> _planReviewersListViewModel;
        public List<SelectListItem> PlanReviewersListViewModel
        {
            get
            {
                if (_planReviewersListViewModel == null)
                    _planReviewersListViewModel = GetReviewerSelectListExcludeReviewers(_allreviewers, new List<string>(), true, new List<SelectListItem>());
                return _planReviewersListViewModel;
            }
            set
            {
                _planReviewersListViewModel = value;
            }
        }

        #region Select List that is just reviewers
        private List<SelectListItem> _bldgPersonSelectList;
        public List<SelectListItem> BldgPersonSelectList
        {
            get
            {
                if (_bldgPersonSelectList == null)
                {
                    _bldgPersonSelectList = BuildSelectList(BuildingReviewers);

                }
                return _bldgPersonSelectList;
            }
            set
            {
                _bldgPersonSelectList = value;
            }
        }
        private List<SelectListItem> _elecPersonSelectList;
        public List<SelectListItem> ElecPersonSelectList
        {
            get
            {
                if (_elecPersonSelectList == null)
                {
                    _elecPersonSelectList = BuildSelectList(ElectricalReviewers);

                }
                return _elecPersonSelectList;
            }
            set
            {
                _elecPersonSelectList = value;
            }
        }
        private List<SelectListItem> _mechPersonSelectList;
        public List<SelectListItem> MechPersonSelectList
        {
            get
            {
                if (_mechPersonSelectList == null)
                {
                    _mechPersonSelectList = BuildSelectList(MechanicalReviewers);

                }

                return _mechPersonSelectList;
            }
            set
            {
                _mechPersonSelectList = value;
            }
        }
        private List<SelectListItem> _plumPersonSelectList;
        public List<SelectListItem> PlumPersonSelectList
        {
            get
            {
                if (_plumPersonSelectList == null)
                {
                    _plumPersonSelectList = BuildSelectList(PlumbingReviewers);

                }

                return _plumPersonSelectList;
            }
            set
            {
                _plumPersonSelectList = value;
            }
        }
        private List<SelectListItem> _zoniPersonSelectList;

        public List<SelectListItem> ZoniPersonSelectList
        {
            get
            {
                if (_zoniPersonSelectList == null)
                {
                    _zoniPersonSelectList = BuildSelectList(ZoningReviewers);

                }

                return _zoniPersonSelectList;
            }
            set
            {
                _zoniPersonSelectList = value;
            }
        }
        private List<SelectListItem> _zoniCountyPersonSelectList;

        public List<SelectListItem> ZoniCountyPersonSelectList
        {
            get
            {
                if (_zoniCountyPersonSelectList == null)
                {
                    _zoniCountyPersonSelectList = BuildSelectList(ZoningCountyReviewers);

                }

                return _zoniCountyPersonSelectList;
            }
            set
            {
                _zoniCountyPersonSelectList = value;
            }
        }
        private List<SelectListItem> _zoniCityPersonSelectList;

        public List<SelectListItem> ZoniCityPersonSelectList
        {
            get
            {
                if (_zoniCityPersonSelectList == null)
                {
                    _zoniCityPersonSelectList = BuildSelectList(ZoningCityReviewers);
                }

                return _zoniCityPersonSelectList;
            }
            set
            {
                _zoniCityPersonSelectList = value;
            }
        }
        private List<SelectListItem> _zoniMintHillPersonSelectList;

        public List<SelectListItem> ZoniMintHillPersonSelectList
        {
            get
            {
                if (_zoniMintHillPersonSelectList == null)
                {
                    _zoniMintHillPersonSelectList = BuildSelectList(ZoningMintHillReviewers);
                }

                return _zoniMintHillPersonSelectList;
            }
            set
            {
                _zoniMintHillPersonSelectList = value;
            }
        }
        private List<SelectListItem> _zoniHuntersvillePersonSelectList;

        public List<SelectListItem> ZoniHuntersvillePersonSelectList
        {
            get
            {
                if (_zoniHuntersvillePersonSelectList == null)
                {
                    _zoniHuntersvillePersonSelectList = BuildSelectList(ZoningHuntersvilleReviewers);
                }

                return _zoniHuntersvillePersonSelectList;
            }
            set
            {
                _zoniHuntersvillePersonSelectList = value;
            }
        }
        private List<SelectListItem> _firePersonSelectList;
        public List<SelectListItem> FirePersonSelectList
        {
            get
            {
                if (_firePersonSelectList == null)
                {
                    _firePersonSelectList = BuildSelectList(FireReviewers);
                }

                return _firePersonSelectList;
            }
            set
            {
                _firePersonSelectList = value;
            }
        }
        private List<SelectListItem> _fireCountyPersonSelectList;
        public List<SelectListItem> FireCountyPersonSelectList
        {
            get
            {
                if (_fireCountyPersonSelectList == null)
                {
                    _fireCountyPersonSelectList = BuildSelectList(FireCountyReviewers);
                }

                return _fireCountyPersonSelectList;
            }
            set
            {
                _fireCountyPersonSelectList = value;
            }
        }
        private List<SelectListItem> _fireCityPersonSelectList;
        public List<SelectListItem> FireCityPersonSelectList
        {
            get
            {
                if (_fireCityPersonSelectList == null)
                {
                    _fireCityPersonSelectList = BuildSelectList(FireCityReviewers);
                }

                return _fireCityPersonSelectList;
            }
            set
            {
                _fireCityPersonSelectList = value;
            }
        }
        private List<SelectListItem> _backPersonSelectList;
        public List<SelectListItem> BackPersonSelectList
        {
            get
            {
                if (_backPersonSelectList == null)
                {
                    _backPersonSelectList = BuildSelectList(BackflowReviewers);
                }

                return _backPersonSelectList;
            }
            set
            {
                _backPersonSelectList = value;
            }
        }
        private List<SelectListItem> _foodPersonSelectList;
        public List<SelectListItem> FoodPersonSelectList
        {
            get
            {
                if (_foodPersonSelectList == null)
                {
                    _foodPersonSelectList = BuildSelectList(FoodReviewers);
                }

                return _foodPersonSelectList;
            }
            set
            {
                _foodPersonSelectList = value;
            }
        }
        private List<SelectListItem> _poolPersonSelectList;
        public List<SelectListItem> PoolPersonSelectList
        {
            get
            {
                if (_poolPersonSelectList == null)
                {
                    _poolPersonSelectList = BuildSelectList(PoolReviewers);
                }

                return _poolPersonSelectList;
            }
            set
            {
                _poolPersonSelectList = value;
            }
        }
        private List<SelectListItem> _faciPersonSelectList;
        public List<SelectListItem> FaciPersonSelectList
        {
            get
            {
                if (_faciPersonSelectList == null)
                {
                    _faciPersonSelectList = BuildSelectList(FacilityReviewers);
                }

                return _faciPersonSelectList;
            }
            set
            {
                _faciPersonSelectList = value;
            }
        }
        private List<SelectListItem> _dayCPersonSelectList;
        public List<SelectListItem> DayCPersonSelectList
        {
            get
            {
                if (_dayCPersonSelectList == null)
                {
                    _dayCPersonSelectList = BuildSelectList(DaycareReviewers);
                }

                return _dayCPersonSelectList;
            }
            set
            {
                _dayCPersonSelectList = value;
            }
        }
        #endregion  Select List that is just reviewers

        #region Assign Reviewers list - Removes Excluded Reviewers -- includes NA and Not Selected

        List<SelectListItem> _planReviewersListBuild;
        public List<SelectListItem> AssignPlanReviewersListBuild
        {
            get
            {
                if (_planReviewersListBuild == null)
                    _planReviewersListBuild = GetReviewerSelectListExcludeReviewers(BuildingReviewers, ExcludedPlanReviewersBuild, true, NANotSelectedBaseList);
                return _planReviewersListBuild;
            }
            set
            {
                _planReviewersListBuild = value;
            }

        }

        List<SelectListItem> _planReviewersListElectric;
        public List<SelectListItem> AssignPlanReviewersListElectric
        {
            get
            {
                if (_planReviewersListElectric == null)
                    _planReviewersListElectric = GetReviewerSelectListExcludeReviewers(ElectricalReviewers, ExcludedPlanReviewersElectric, true, NANotSelectedBaseList);
                return _planReviewersListElectric;
            }
            set
            {
                _planReviewersListElectric = value;
            }

        }



        List<SelectListItem> _planReviewersListPlmub;
        public List<SelectListItem> AssignPlanReviewersListPlumb
        {
            get
            {
                if (_planReviewersListPlmub == null)
                    _planReviewersListPlmub = GetReviewerSelectListExcludeReviewers(PlumbingReviewers, ExcludedPlanReviewersPlumb, true, NANotSelectedBaseList);
                return _planReviewersListPlmub;
            }
            set
            {
                _planReviewersListPlmub = value;
            }

        }

        List<SelectListItem> _planReviewersListMech;
        public List<SelectListItem> AssignPlanReviewersListMech
        {
            get
            {
                if (_planReviewersListMech == null)
                    _planReviewersListMech = GetReviewerSelectListExcludeReviewers(MechanicalReviewers, ExcludedPlanReviewersMech, true, NANotSelectedBaseList);
                return _planReviewersListMech;
            }
            set
            {
                _planReviewersListMech = value;
            }

        }

        List<SelectListItem> _planReviewersListZone;
        public List<SelectListItem> AssignPlanReviewersListZone
        {
            get
            {
                if (_planReviewersListZone == null)
                    _planReviewersListZone = GetReviewerSelectListExcludeReviewers(ZoningJurisdictionReviewers, ExcludedPlanReviewersZone, true, NANotSelectedBaseList);
                return _planReviewersListZone;
            }
            set
            {
                _planReviewersListZone = value;
            }

        }


        List<SelectListItem> _PlanReviewersListFire;
        public List<SelectListItem> AssignPlanReviewersListFire
        {
            get
            {
                if (_PlanReviewersListFire == null)
                    _PlanReviewersListFire = GetReviewerSelectListExcludeReviewers(FireJurisdictionReviewers, ExcludedPlanReviewersFire, true, NANotSelectedBaseList);
                return _PlanReviewersListFire;
            }
            set
            {
                _PlanReviewersListFire = value;
            }

        }

        List<SelectListItem> _planReviewersListBackFlow;
        public List<SelectListItem> AssignPlanReviewersListBackFlow
        {
            get
            {
                if (_planReviewersListBackFlow == null)
                    _planReviewersListBackFlow = GetReviewerSelectListExcludeReviewers(BackflowReviewers, ExcludedPlanReviewersBackFlow, true, NANotSelectedBaseList);
                return _planReviewersListBackFlow;
            }
            set
            {
                _planReviewersListBackFlow = value;
            }

        }

        List<SelectListItem> _planReviewersListFood;
        public List<SelectListItem> AssignPlanReviewersListFood
        {
            get
            {
                if (_planReviewersListFood == null)
                    _planReviewersListFood = GetReviewerSelectListExcludeReviewers(FoodReviewers, ExcludedPlanReviewersFood, true, NANotSelectedBaseList);
                return _planReviewersListFood;
            }
            set
            {
                _planReviewersListFood = value;
            }

        }


        List<SelectListItem> _planReviewersListPool;
        public List<SelectListItem> AssignPlanReviewersListPool
        {
            get
            {
                if (_planReviewersListPool == null)
                    _planReviewersListPool = GetReviewerSelectListExcludeReviewers(PoolReviewers, ExcludedPlanReviewersPool, true, NANotSelectedBaseList);
                return _planReviewersListPool;
            }
            set
            {
                _planReviewersListPool = value;
            }

        }


        List<SelectListItem> _planReviewersListLodge;
        public List<SelectListItem> AssignPlanReviewersListLodge
        {
            get
            {
                if (_planReviewersListLodge == null)
                    _planReviewersListLodge = GetReviewerSelectListExcludeReviewers(FacilityReviewers, ExcludedPlanReviewersLodge, true, NANotSelectedBaseList);
                return _planReviewersListLodge;
            }
            set
            {
                _planReviewersListLodge = value;
            }

        }

        List<SelectListItem> _planReviewersListDayCare;
        public List<SelectListItem> AssignPlanReviewersListDayCare
        {
            get
            {
                if (_planReviewersListDayCare == null)
                    _planReviewersListDayCare = GetReviewerSelectListExcludeReviewers(DaycareReviewers, ExcludedPlanReviewersDayCare, true, NANotSelectedBaseList);
                return _planReviewersListDayCare;
            }
            set
            {
                _planReviewersListDayCare = value;
            }

        }

        #endregion Assign Reviewers list - Removes Excluded Reviewers -- includes NA and Not Selected

        #region Assign Reviewers list - Removes Excluded Reviewers -- includes Not Selected

        List<SelectListItem> _planReviewersListBuildNS;
        public List<SelectListItem> AssignPlanReviewersListBuildNS
        {
            get
            {
                if (_planReviewersListBuildNS == null)
                    _planReviewersListBuildNS = GetReviewerSelectListExcludeReviewers(BuildingReviewers, ExcludedPlanReviewersBuild, true, NotSelectedBaseList);
                return _planReviewersListBuildNS;
            }
            set
            {
                _planReviewersListBuildNS = value;
            }

        }

        List<SelectListItem> _planReviewersListElectricNS;
        public List<SelectListItem> AssignPlanReviewersListElectricNS
        {
            get
            {
                if (_planReviewersListElectricNS == null)
                    _planReviewersListElectricNS = GetReviewerSelectListExcludeReviewers(ElectricalReviewers, ExcludedPlanReviewersElectric, true, NotSelectedBaseList);
                return _planReviewersListElectricNS;
            }
            set
            {
                _planReviewersListElectricNS = value;
            }

        }



        List<SelectListItem> _planReviewersListPlmubNS;
        public List<SelectListItem> AssignPlanReviewersListPlumbNS
        {
            get
            {
                if (_planReviewersListPlmubNS == null)
                    _planReviewersListPlmubNS = GetReviewerSelectListExcludeReviewers(PlumbingReviewers, ExcludedPlanReviewersPlumb, true, NotSelectedBaseList);
                return _planReviewersListPlmubNS;
            }
            set
            {
                _planReviewersListPlmubNS = value;
            }

        }

        List<SelectListItem> _planReviewersListMechNS;
        public List<SelectListItem> AssignPlanReviewersListMechNS
        {
            get
            {
                if (_planReviewersListMechNS == null)
                    _planReviewersListMechNS = GetReviewerSelectListExcludeReviewers(MechanicalReviewers, ExcludedPlanReviewersMech, true, NotSelectedBaseList);
                return _planReviewersListMechNS;
            }
            set
            {
                _planReviewersListMechNS = value;
            }

        }

        List<SelectListItem> _planReviewersListZoneNS;
        public List<SelectListItem> AssignPlanReviewersListZoneNS
        {
            get
            {
                if (_planReviewersListZoneNS == null)
                    _planReviewersListZoneNS = GetReviewerSelectListExcludeReviewers(ZoningJurisdictionReviewers, ExcludedPlanReviewersZone, true, NotSelectedBaseList);
                return _planReviewersListZoneNS;
            }
            set
            {
                _planReviewersListZoneNS = value;
            }

        }


        List<SelectListItem> _PlanReviewersListFireNS;
        public List<SelectListItem> AssignPlanReviewersListFireNS
        {
            get
            {
                if (_PlanReviewersListFireNS == null)
                    _PlanReviewersListFireNS = GetReviewerSelectListExcludeReviewers(FireJurisdictionReviewers, ExcludedPlanReviewersFire, true, NotSelectedBaseList);
                return _PlanReviewersListFireNS;
            }
            set
            {
                _PlanReviewersListFireNS = value;
            }

        }

        List<SelectListItem> _planReviewersListBackFlowNS;
        public List<SelectListItem> AssignPlanReviewersListBackFlowNS
        {
            get
            {
                if (_planReviewersListBackFlowNS == null)
                    _planReviewersListBackFlowNS = GetReviewerSelectListExcludeReviewers(BackflowReviewers, ExcludedPlanReviewersBackFlow, true, NotSelectedBaseList);
                return _planReviewersListBackFlowNS;
            }
            set
            {
                _planReviewersListBackFlowNS = value;
            }

        }

        List<SelectListItem> _planReviewersListFoodNS;
        public List<SelectListItem> AssignPlanReviewersListFoodNS
        {
            get
            {
                if (_planReviewersListFoodNS == null)
                    _planReviewersListFoodNS = GetReviewerSelectListExcludeReviewers(FoodReviewers, ExcludedPlanReviewersFood, true, NotSelectedBaseList);
                return _planReviewersListFoodNS;
            }
            set
            {
                _planReviewersListFoodNS = value;
            }

        }


        List<SelectListItem> _planReviewersListPoolNS;
        public List<SelectListItem> AssignPlanReviewersListPoolNS
        {
            get
            {
                if (_planReviewersListPoolNS == null)
                    _planReviewersListPoolNS = GetReviewerSelectListExcludeReviewers(PoolReviewers, ExcludedPlanReviewersPool, true, NotSelectedBaseList);
                return _planReviewersListPoolNS;
            }
            set
            {
                _planReviewersListPoolNS = value;
            }

        }


        List<SelectListItem> _planReviewersListLodgeNS;
        public List<SelectListItem> AssignPlanReviewersListLodgeNS
        {
            get
            {
                if (_planReviewersListLodgeNS == null)
                    _planReviewersListLodgeNS = GetReviewerSelectListExcludeReviewers(FacilityReviewers, ExcludedPlanReviewersLodge, true, NotSelectedBaseList);
                return _planReviewersListLodgeNS;
            }
            set
            {
                _planReviewersListLodgeNS = value;
            }

        }

        List<SelectListItem> _planReviewersListDayCareNS;
        public List<SelectListItem> AssignPlanReviewersListDayCareNS
        {
            get
            {
                if (_planReviewersListDayCareNS == null)
                    _planReviewersListDayCareNS = GetReviewerSelectListExcludeReviewers(DaycareReviewers, ExcludedPlanReviewersDayCare, true, NotSelectedBaseList);
                return _planReviewersListDayCareNS;
            }
            set
            {
                _planReviewersListDayCareNS = value;
            }

        }

        #endregion Assign Reviewers list - Removes Excluded Reviewers -- includes Not Selected

        #region Assign Reviewers list - Removes Excluded Reviewers -- includes Select Reviewer

        List<SelectListItem> _planReviewersListBuildSR;
        public List<SelectListItem> AssignPlanReviewersListBuildSR
        {
            get
            {
                if (_planReviewersListBuildSR == null)
                    _planReviewersListBuildSR = GetReviewerSelectListExcludeReviewers(BuildingReviewers, ExcludedPlanReviewersBuild, true, SelectReviewerBaseList);
                return _planReviewersListBuildSR;
            }
            set
            {
                _planReviewersListBuildSR = value;
            }

        }

        List<SelectListItem> _planReviewersListElectricSR;
        public List<SelectListItem> AssignPlanReviewersListElectricSR
        {
            get
            {
                if (_planReviewersListElectricSR == null)
                    _planReviewersListElectricSR = GetReviewerSelectListExcludeReviewers(ElectricalReviewers, ExcludedPlanReviewersElectric, true, SelectReviewerBaseList);
                return _planReviewersListElectricSR;
            }
            set
            {
                _planReviewersListElectricSR = value;
            }

        }



        List<SelectListItem> _planReviewersListPlmubSR;
        public List<SelectListItem> AssignPlanReviewersListPlumbSR
        {
            get
            {
                if (_planReviewersListPlmubSR == null)
                    _planReviewersListPlmubSR = GetReviewerSelectListExcludeReviewers(PlumbingReviewers, ExcludedPlanReviewersPlumb, true, SelectReviewerBaseList);
                return _planReviewersListPlmubSR;
            }
            set
            {
                _planReviewersListPlmubSR = value;
            }

        }

        List<SelectListItem> _planReviewersListMechSR;
        public List<SelectListItem> AssignPlanReviewersListMechSR
        {
            get
            {
                if (_planReviewersListMechSR == null)
                    _planReviewersListMechSR = GetReviewerSelectListExcludeReviewers(MechanicalReviewers, ExcludedPlanReviewersMech, true, SelectReviewerBaseList);
                return _planReviewersListMechSR;
            }
            set
            {
                _planReviewersListMechSR = value;
            }

        }

        List<SelectListItem> _planReviewersListZoneSR;
        public List<SelectListItem> AssignPlanReviewersListZoneSR
        {
            get
            {
                if (_planReviewersListZoneSR == null)
                    _planReviewersListZoneSR = GetReviewerSelectListExcludeReviewers(ZoningJurisdictionReviewers, ExcludedPlanReviewersZone, true, SelectReviewerBaseList);
                return _planReviewersListZoneSR;
            }
            set
            {
                _planReviewersListZoneSR = value;
            }

        }


        List<SelectListItem> _PlanReviewersListFireSR;
        public List<SelectListItem> AssignPlanReviewersListFireSR
        {
            get
            {
                if (_PlanReviewersListFireSR == null)
                    _PlanReviewersListFireSR = GetReviewerSelectListExcludeReviewers(FireJurisdictionReviewers, ExcludedPlanReviewersFire, true, SelectReviewerBaseList);
                return _PlanReviewersListFireSR;
            }
            set
            {
                _PlanReviewersListFireSR = value;
            }

        }

        List<SelectListItem> _planReviewersListBackFlowSR;
        public List<SelectListItem> AssignPlanReviewersListBackFlowSR
        {
            get
            {
                if (_planReviewersListBackFlowSR == null)
                    _planReviewersListBackFlowSR = GetReviewerSelectListExcludeReviewers(BackflowReviewers, ExcludedPlanReviewersBackFlow, true, SelectReviewerBaseList);
                return _planReviewersListBackFlowSR;
            }
            set
            {
                _planReviewersListBackFlowSR = value;
            }

        }

        List<SelectListItem> _planReviewersListFoodSR;
        public List<SelectListItem> AssignPlanReviewersListFoodSR
        {
            get
            {
                if (_planReviewersListFoodSR == null)
                    _planReviewersListFoodSR = GetReviewerSelectListExcludeReviewers(FoodReviewers, ExcludedPlanReviewersFood, true, SelectReviewerBaseList);
                return _planReviewersListFoodSR;
            }
            set
            {
                _planReviewersListFoodSR = value;
            }

        }


        List<SelectListItem> _planReviewersListPoolSR;
        public List<SelectListItem> AssignPlanReviewersListPoolSR
        {
            get
            {
                if (_planReviewersListPoolSR == null)
                    _planReviewersListPoolSR = GetReviewerSelectListExcludeReviewers(PoolReviewers, ExcludedPlanReviewersPool, true, SelectReviewerBaseList);
                return _planReviewersListPoolSR;
            }
            set
            {
                _planReviewersListPoolSR = value;
            }

        }


        List<SelectListItem> _planReviewersListLodgeSR;
        public List<SelectListItem> AssignPlanReviewersListLodgeSR
        {
            get
            {
                if (_planReviewersListLodgeSR == null)
                    _planReviewersListLodgeSR = GetReviewerSelectListExcludeReviewers(FacilityReviewers, ExcludedPlanReviewersLodge, true, SelectReviewerBaseList);
                return _planReviewersListLodgeSR;
            }
            set
            {
                _planReviewersListLodgeSR = value;
            }

        }

        List<SelectListItem> _planReviewersListDayCareSR;
        public List<SelectListItem> AssignPlanReviewersListDayCareSR
        {
            get
            {
                if (_planReviewersListDayCareSR == null)
                    _planReviewersListDayCareSR = GetReviewerSelectListExcludeReviewers(DaycareReviewers, ExcludedPlanReviewersDayCare, true, SelectReviewerBaseList);
                return _planReviewersListDayCareSR;
            }
            set
            {
                _planReviewersListDayCareSR = value;
            }

        }

        #endregion Assign Reviewers list - Removes Excluded Reviewers -- includes Select Reviewer


        #endregion Reviewer Select lists

        #region Excluded List<string> 
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

        #endregion  Excluded List<string> 


        #region Reviewers for display only

        //*********** Reviewers for display only *********************//
        public string DisplayScheduledReviewerBuilding { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Building); } }
        public string DisplayScheduledReviewerElectrical { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Electrical); } }
        public string DisplayScheduledReviewerMechanical { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Mechanical); } }
        public string DisplayScheduledReviewerPlumbing { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Plumbing); } }
        public string DisplayScheduledReviewerZone { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Zone_Cornelius); } }
        public string DisplayScheduledReviewerFire { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Fire_Cornelius); } }
        public string DisplayScheduledReviewerBackflow { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Backflow); } }
        public string DisplayScheduledReviewerFood { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Food); } }
        public string DisplayScheduledReviewerPool { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Pool); } }
        public string DisplayScheduledReviewerFacilities { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Facilities); } }
        public string DisplayScheduledReviewerDaycare { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Day_Care); } }
        //*************************************************************//

        //*********** Reviewers for display only - not limited by department *********************//
        public string DisplayScheduledReviewersAllReviewersBuilding { get { return GetReviewerSelectListItemAllReviewers(DepartmentNameEnums.Building); } }
        public string DisplayScheduledReviewerAllReviewersElectrical { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Electrical); } }
        public string DisplayScheduledReviewerAllReviewersMechanical { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Mechanical); } }
        public string DisplayScheduledReviewerAllReviewersPlumbing { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Plumbing); } }
        public string DisplayScheduledReviewerAllReviewersZone { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Zone_Cornelius); } }
        public string DisplayScheduledReviewerAllReviewersFire { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Fire_Cornelius); } }
        public string DisplayScheduledReviewerAllReviewersBackflow { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.Backflow); } }
        public string DisplayScheduledReviewerAllReviewersFood { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Food); } }
        public string DisplayScheduledReviewerAllReviewersPool { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Pool); } }
        public string DisplayScheduledReviewerAllReviewersFacilities { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Facilities); } }
        public string DisplayScheduledReviewerAllReviewersDaycare { get { return GetReviewerSelectListItemByDept(DepartmentNameEnums.EH_Day_Care); } }

        #endregion  Reviewers for display only

        #endregion Reviewer Lists

        #region Project Info
        public string RecIdTxt { get; set; }
        private ProjectEstimation _project;

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
        public ApplicationNotes BEMPApplicationNotes { get; set; }
        public ApplicationNotes ZoningApplicationNotes { get; set; }

        public ApplicationNotes FireApplicationNotes { get; set; }

        public ApplicationNotes BackFlowApplicationNotes { get; set; }
        public ApplicationNotes EHSApplicationNotes { get; set; }

        /// <summary>
        /// Used to indicate if this is to be rescheduled if submitted
        /// </summary>
        public bool IsReschedule { get; set; }

        #endregion Project Info

        #region Methods
        public void SetApplicationNotesStatus()
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
        public void SetApplicationNotesStatusByProjDept(DepartmentNameEnums deptname, ProjectStatusEnum status)
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
        public bool SetPrimaryReviewerName(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.PrimaryPlanReviewer = AllJurisdictionReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }
        public string GetPrimaryReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem ret = NANotSelectedBaseList.Where(x => x.Value == "-1").FirstOrDefault();  // If the reviewer found as selected then select that person else select an empty list item.
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null || dept.PrimaryPlanReviewer == null || dept.PrimaryPlanReviewer.ID == 0)
                return ret.Value;
            var val = current.Where(x => x.Value == dept.PrimaryPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Value : null;
        }

        public string GetSecondaryReviewerName(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem ret = NANotSelectedBaseList.Where(x => x.Value == "-1").FirstOrDefault(); // If the reviewer found as selected then select that person else select an empty list item.
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null || dept.SecondaryPlanReviewer == null || dept.SecondaryPlanReviewer.ID == 0)
                return ret.Value;
            var val = current.Where(x => x.Value == dept.SecondaryPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Value : null;
        }
        public bool SetSecondaryReviewerName(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.SecondaryPlanReviewer = AllJurisdictionReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }


        /// <summary>
        /// Build a SelectItem list by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<SelectListItem> BuildSelectList<T>(List<T> list)
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
        public List<SelectListItem> GetReviewerSelectListExcludeReviewers(List<Reviewer> reviewers, List<string> excludedReviewers, bool includeOnlySchedulable, List<SelectListItem> baseList)
        {
            if (reviewers == null)
                reviewers = new List<Reviewer>();
            List<SelectListItem> ret = new List<SelectListItem>();
            List<Reviewer> current = new List<Reviewer>();
            ret.AddRange(baseList);

            current = reviewers.Where(x => !excludedReviewers.Any(y => (int.Parse(y) == x.ID))).ToList();

            foreach (Reviewer item in current)
            {
                if (includeOnlySchedulable)
                {
                    if (item.IsSchedulable == true)
                    {
                        ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });

                    }
                }
                else
                {
                    ret.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.ID.ToString() });
                }
            }
            return ret;
        }

        /// <summary>
        /// </summary>
        /// <param name="deptType"></param>
        /// <returns></returns>
        public string GetReviewerSelectListItemByDeptWProposedPrimary(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem ret = NANotSelectedBaseList.Where(x => x.Value == "-1").FirstOrDefault();
            SelectListItem notselected = NANotSelectedBaseList.Where(x => x.Value == "0").FirstOrDefault();
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

        public string GetReviewerSelectListItemByDept(DepartmentNameEnums deptType)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(deptType);
            SelectListItem notapplicable = NANotSelectedBaseList.Where(x => x.Value == "-1").FirstOrDefault();
            SelectListItem notselected = NANotSelectedBaseList.Where(x => x.Value == "0").FirstOrDefault();
            ProjectDepartment dept = GetDepartment(deptType);
            //if dept is NA then pick the NA option -1
            if (dept == null || dept.EstimationNotApplicable)
                return notapplicable.Text;

            if (dept.AssignedPlanReviewer == null || dept.AssignedPlanReviewer.ID == 0 || dept.AssignedPlanReviewer.ID == -1)
            {
                return notselected.Text;
            }

            var val = current.Where(x => x.Value == dept.AssignedPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Text : notselected.Text;
        }

        public string GetReviewerSelectListItemAllReviewers(DepartmentNameEnums projectDepartment)
        {
            List<SelectListItem> current = GetAssignPlanReviewerListByDept(DepartmentNameEnums.NA);
            SelectListItem notapplicable = NANotSelectedBaseList.Where(x => x.Value == "-1").FirstOrDefault();
            SelectListItem notselected = NANotSelectedBaseList.Where(x => x.Value == "0").FirstOrDefault();
            ProjectDepartment dept = GetDepartment(projectDepartment);
            //if dept is NA then pick the NA option -1
            if (dept == null || dept.EstimationNotApplicable)
                return notapplicable.Text;

            if (dept.AssignedPlanReviewer == null || dept.AssignedPlanReviewer.ID == 0 || dept.AssignedPlanReviewer.ID == -1)
            {
                return notselected.Text;
            }

            var val = current.Where(x => x.Value == dept.AssignedPlanReviewer.ID.ToString());
            return val.Any() ? val.FirstOrDefault().Text : notselected.Text;
        }

        /// <summary>
        /// sets the property for the scheduled plan reviewer
        /// </summary>
        /// <param name="deptType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetAssignedReviewerForDept(DepartmentNameEnums deptType, string value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            if (value == "0")
                dept.AssignedPlanReviewer = new UserIdentity() { ID = 0 };
            else
                dept.AssignedPlanReviewer = AllJurisdictionReviewers.Where(x => x.ID.ToString() == value).FirstOrDefault();
            return true;
        }


        public ProjectDepartment GetDepartment(DepartmentNameEnums deptType)
        {
            Helper helper = new Helper();

            if (Project == null)
                return null;
            if (helper.BEMPDepartmentNames.Any(x => x == deptType))
                return Project.Trades.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
            else
            {
                //in the property the first fire department is assigned. So assume it is only one fire at a time and then pick the related fire dept from list.
                if (helper.FireDepartmentNames.Any(x => x == deptType))
                {
                    return Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)
                    ).FirstOrDefault();
                }
                //in the property the first Zone department is assigned. So assume it is only one fire at a time and then pick the related zone dept from list.
                else if (helper.ZoneDepartmentNames.Any(x => x == deptType))
                {
                    return Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)
                    ).FirstOrDefault();
                }
                else
                {
                    //EHS and Backflow
                    return Project.Agencies.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
                }
            }
        }

        public bool GetIsNA(DepartmentNameEnums deptType)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept != null)
                return dept.EstimationNotApplicable;
            else
                // I am assuming dept shoud be added from accela or later in pre-estimation process. 
                // So if department not found in project list then assume that is not applicable for this specific application.
                return true;
        }

        public bool SetIsNA(DepartmentNameEnums deptType, bool value)
        {
            ProjectDepartment dept = GetDepartment(deptType);
            if (dept == null)
                return false;
            dept.EstimationNotApplicable = value;
            return true;
        }
        public List<SelectListItem> GetPlanReviewerListByDept(DepartmentNameEnums? deptType)
        {
            if (deptType == null || deptType == DepartmentNameEnums.NA)
                //returns all items.
                return PlanReviewersListViewModel;
            switch (deptType.Value)
            {
                case DepartmentNameEnums.Building:
                    return BldgPersonSelectList;
                case DepartmentNameEnums.Electrical:
                    return ElecPersonSelectList;
                case DepartmentNameEnums.Mechanical:
                    return MechPersonSelectList;
                case DepartmentNameEnums.Plumbing:
                    return PlumPersonSelectList;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return ZoniPersonSelectList;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return FirePersonSelectList;
                case DepartmentNameEnums.EH_Day_Care:
                    return DayCPersonSelectList;
                case DepartmentNameEnums.EH_Food:
                    return FoodPersonSelectList;
                case DepartmentNameEnums.EH_Pool:
                    return PoolPersonSelectList;
                case DepartmentNameEnums.EH_Facilities:
                    return FaciPersonSelectList;
                case DepartmentNameEnums.Backflow:
                    return BackPersonSelectList;
                default:
                    throw new ArgumentException("Unexpected option! Contact IT Adminstrator.");
            }
        }
        public List<SelectListItem> GetAssignPlanReviewerListByDept(DepartmentNameEnums? deptType)
        {
            if (deptType == null || deptType == DepartmentNameEnums.NA)
            {
                //returns all items.
                List<string> excludedReviewers = new List<string>();
                return GetReviewerSelectListExcludeReviewers(AllJurisdictionReviewers, excludedReviewers, true, new List<SelectListItem>());
            }
            switch (deptType.Value)
            {
                case DepartmentNameEnums.Building:
                    return AssignPlanReviewersListBuild;
                case DepartmentNameEnums.Electrical:
                    return AssignPlanReviewersListElectric;
                case DepartmentNameEnums.Mechanical:
                    return AssignPlanReviewersListMech;
                case DepartmentNameEnums.Plumbing:
                    return AssignPlanReviewersListPlumb;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return AssignPlanReviewersListZone;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return AssignPlanReviewersListFire;
                case DepartmentNameEnums.EH_Day_Care:
                    return AssignPlanReviewersListDayCare;
                case DepartmentNameEnums.EH_Food:
                    return AssignPlanReviewersListFood;
                case DepartmentNameEnums.EH_Pool:
                    return AssignPlanReviewersListPool;
                case DepartmentNameEnums.EH_Facilities:
                    return AssignPlanReviewersListLodge;
                case DepartmentNameEnums.Backflow:
                    return AssignPlanReviewersListBackFlow;
                default:
                    throw new ArgumentException("Unexpected option! Contact IT Adminstrator.");
            }
        }

        /// <summary>
        /// Get the reviewers for each department
        /// </summary>
        /// <param name="departmentDivision"></param>
        /// <param name="departmentJurisdiction">ex BEMP or Zone_Huntersville to indicate what jurisdiction</param>
        /// <param name="allReviewers"></param>
        /// <param name="returnSOI">Used by NPA and Schedule Capacity to show all reviewers regardless of jurisdiction</param>
        /// <returns></returns>
        public List<Reviewer> GetDeptReviewers(List<Reviewer> allReviewers, DepartmentDivisionEnum departmentDivision,
            DepartmentNameEnums departmentJurisdiction, bool returnSOI)
        {
            Helper helper = new Helper();
            bool isCity = departmentJurisdiction == DepartmentNameEnums.Zone_Cty_Chrlt || departmentJurisdiction == DepartmentNameEnums.Fire_Cty_Chrlt;
            bool isHuntersvilleZoning = departmentJurisdiction == DepartmentNameEnums.Zone_Huntersville;
            bool isMintHillZoning = departmentJurisdiction == DepartmentNameEnums.Zone_Mint_Hill;

            List<Reviewer> reviewers = new List<Reviewer>();
            foreach (Reviewer reviewer in allReviewers)
            {
                List<Department> reviewerDepartments = reviewer.DesignatedDepartments;
                switch (departmentDivision)
                {
                    case DepartmentDivisionEnum.Building:
                    case DepartmentDivisionEnum.Electrical:
                    case DepartmentDivisionEnum.Mechanical:
                    case DepartmentDivisionEnum.Plumbing:
                    case DepartmentDivisionEnum.Environmental:
                    case DepartmentDivisionEnum.Backflow:
                        if (reviewerDepartments.Where(x => x.DepartmentEnum == departmentJurisdiction).Any())
                        {
                            reviewers.Add(reviewer);
                        }
                        break;
                    case DepartmentDivisionEnum.Zoning:
                        //get the deparment and return the correct list
                        if (returnSOI)
                        {
                            //if this is city of charlotte or mint hill or huntersville, get the exact list
                            //otherwise, get anyone who has zoning for county
                            if (isCity || isMintHillZoning || isHuntersvilleZoning)
                            {
                                if (reviewerDepartments.Any(x => x.DepartmentEnum == departmentJurisdiction))
                                {
                                    reviewers.Add(reviewer);

                                }

                            }
                            else
                            {
                                if (reviewerDepartments.Any(x => helper.CountyZoneDepartmentNames.Contains(x.DepartmentEnum)))
                                {
                                    reviewers.Add(reviewer);
                                }

                            }
                        }
                        else
                        {
                            if (reviewerDepartments.Any(x => helper.ZoneDepartmentNames.Contains(x.DepartmentEnum)))
                            {
                                reviewers.Add(reviewer);
                            }
                        }
                        break;
                    case DepartmentDivisionEnum.Fire:
                        //get the deparment and return the correct list
                        if (returnSOI)
                        {
                            //if this jurisdiction is city, then just return that list, otherwise return the county list
                            if (isCity)
                            {
                                if (reviewerDepartments.Any(x => x.DepartmentEnum == departmentJurisdiction))
                                {
                                    reviewers.Add(reviewer);

                                }

                            }
                            else
                            {
                                if (reviewerDepartments.Any(x => helper.CountyFireDepartmentNames.Contains(x.DepartmentEnum)))
                                {
                                    reviewers.Add(reviewer);
                                }
                            }
                        }
                        else
                        {
                            if (reviewerDepartments.Any(x => helper.FireDepartmentNames.Contains(x.DepartmentEnum)))
                            {
                                reviewers.Add(reviewer);
                            }
                        }

                        break;
                    case DepartmentDivisionEnum.NA:

                        //used to get all reviewers 
                        if (reviewerDepartments.Any(x => helper.BEMPDepartmentNames.Contains(x.DepartmentEnum)))
                        {
                            reviewers.Add(reviewer);
                        }
                        if (reviewerDepartments.Any(x => helper.EhsDepartmentNames.Contains(x.DepartmentEnum)))
                        {
                            reviewers.Add(reviewer);
                        }
                        if (reviewerDepartments.Any(x => x.DepartmentEnum == DepartmentNameEnums.Backflow))
                        {
                            reviewers.Add(reviewer);
                        }

                        if (returnSOI)
                        {
                            //department jurisdiction in this case should be the jurisdiction of the project
                            if (reviewerDepartments.Any(x => x.DepartmentEnum == ZoneJurisdiction))
                            {
                                reviewers.Add(reviewer);

                            }
                            if (reviewerDepartments.Any(x => x.DepartmentEnum == FireJurisdiction))
                            {
                                reviewers.Add(reviewer);

                            }
                        }
                        else
                        {

                            if (reviewerDepartments.Any(x => helper.ZoneDepartmentNames.Contains(x.DepartmentEnum)))
                            {
                                reviewers.Add(reviewer);
                            }
                            if (reviewerDepartments.Any(x => helper.FireDepartmentNames.Contains(x.DepartmentEnum)))
                            {
                                reviewers.Add(reviewer);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            return reviewers;
        }

        public DepartmentNameEnums GetJuridiction(string jurisdictionType)
        {
            Helper helper = new Helper();

            if (Project == null || Project.Agencies == null || Project.Agencies.Count() == 0
                || Project.Trades == null || Project.Trades.Count() == 0)
                return DepartmentNameEnums.NA;
            switch (jurisdictionType)
            {
                case "Fire":
                    return Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)
                    ).FirstOrDefault().DepartmentInfo;
                case "Zone":
                    return Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)
                    ).FirstOrDefault().DepartmentInfo;
                default:
                    return DepartmentNameEnums.NA;
            }

        }

        public void BuildDepartmentList()
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

        private string GenerateAccelaDeeplink(string id)
        {
            string url = ConfigurationManager.AppSettings["AccelaBaseApplicationDeeplink"].ToString();

            url = url.Replace("{AccelaId}", HttpUtility.UrlEncode(id));

            return url;
        }

        private void SetMenuItems()
        {
            MenuHeading = Controller;
            MenuLink = Action;
            MenuLinkText = Action;
            ActionText = Action;

            switch (Controller.ToLower())
            {
                case "search":
                    switch (Action.ToLower())
                    {
                        case "searchdashboard":
                            MenuHeading = "Search";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Search Projects";
                            break;
                    }
                    break;
                case "estimation":
                    switch (Action.ToLower())
                    {
                        case "estimationdashboard":
                            MenuHeading = "Estimation";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Estimation Dashboard";
                            break;

                        case "estimationmain":
                            MenuHeading = "Estimation";
                            MenuLink = "/Estimation/EstimationDashboard";
                            MenuLinkText = "Estimation Dashboard";
                            ActionText = "Estimation";
                            break;

                        case "preliminaryestimation":
                            MenuHeading = "Preliminary Estimation";
                            MenuLink = "/Estimation/EstimationDashboard";
                            MenuLinkText = "Estimation Dashboard";
                            ActionText = "Preliminary Estimation";
                            break;
                    }
                    break;

                case "scheduling":
                    switch (Action.ToLower())
                    {
                        case "schedulingdashboard":
                            MenuHeading = "Scheduling";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Scheduling Dashboard";
                            break;

                        case "meetingsdashboard":
                            MenuHeading = "Meetings";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Meetings Dashboard";
                            break;

                        case "scheduleplanreview":
                            MenuHeading = "Scheduling";
                            MenuLink = "/Scheduling/SchedulingDashboard";
                            MenuLinkText = "Scheduling Dashboard";
                            ActionText = "Schedule Plan Review";
                            break;

                        case "scheduleexpressmeeting":
                            MenuHeading = "Scheduling";
                            MenuLink = "/Scheduling/SchedulingDashboard";
                            MenuLinkText = "Scheduling Dashboard";
                            ActionText = "Schedule Express Meeting";
                            break;

                        case "schedulefifoplanreview":
                            MenuHeading = "Scheduling";
                            MenuLink = "/Scheduling/SchedulingDashboard";
                            MenuLinkText = "Scheduling Dashboard";
                            ActionText = "Schedule FIFO Plan Review";
                            break;

                        case "schedulemeeting":
                            MenuHeading = "Scheduling";
                            MenuLink = "/Scheduling/SchedulingDashboard";
                            MenuLinkText = "Scheduling Dashboard";
                            ActionText = "Schedule Meeting";
                            break;

                        case "schedulepreliminarymeeting":
                            MenuHeading = "Scheduling";
                            MenuLink = "/Scheduling/SchedulingDashboard";
                            MenuLinkText = "Scheduling Dashboard";
                            ActionText = "Schedule Preliminary Meeting";
                            break;
                    }
                    break;

                case "projectdetail":
                    switch (Action.ToLower())
                    {
                        case "projectdetail":
                            MenuHeading = "Project Detail";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Project Detail";
                            break;
                    }
                    break;

                case "express":
                    switch (Action.ToLower())
                    {
                        case "expressmain":
                            MenuHeading = "Express";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Express";
                            break;
                    }
                    break;

                case "npa":
                    switch (Action.ToLower())
                    {
                        case "npamain":
                            MenuHeading = "NPA";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "NPA";
                            break;
                    }
                    break;

                case "reporting":
                    switch (Action.ToLower())
                    {
                        case "reportingdashboard":
                            MenuHeading = "Reporting";
                            MenuLink = "/Search/SearchDashboard";
                            MenuLinkText = "Search";
                            ActionText = "Reporting Dashboard";
                            break;
                    }
                    break;

                case "customer":
                    switch (Action.ToLower())
                    {
                        case "projectsdashboard":
                            MenuHeading = "Projects";
                            MenuLink = "/";
                            MenuLinkText = "Home";
                            ActionText = "Projects Dashboard";
                            break;

                        case "meetingsdashboard":
                            MenuHeading = "Meetings";
                            MenuLink = "/";
                            MenuLinkText = "Home";
                            ActionText = "Meetings Dashboard";
                            break;

                        case "projectdetail":
                            MenuHeading = "Project Detail";
                            MenuLink = "projectsdashboard";
                            MenuLinkText = "Projects Dashboard";
                            ActionText = "Project Detail";
                            break;

                    }
                    break;
                case "schedulecapacity":
                    MenuHeading = "Schedule Capacity";
                    MenuLink = "/Search/SearchDashboard";
                    MenuLinkText = "Search";
                    ActionText = "Schedule Capacity";
                    break;

                case "admin":
                    MenuHeading = "Admin";
                    MenuLink = "/Search/SearchDashboard";
                    MenuLinkText = "Search";
                    ActionText = "Admin";
                    break;
            }
        }

        #endregion Methods
    }
}