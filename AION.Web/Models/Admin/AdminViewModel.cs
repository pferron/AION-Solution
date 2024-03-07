using AION.BL;
using AION.Manager.Models;
using AION.Web.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class AdminViewModel : ViewModelBase
    {
        public AdminViewModel()
        {
            DefaultHoursViewModel = new DefaultHoursAdminViewModel();
            HolidayConfigAdminViewModel = new HolidayConfigAdminViewModel();
            MessageConfigAdminViewModel = new MessageConfigAdminViewModel();
            HolidayConfigList = new List<HolidayConfig>();
            MiscConfigurationViewModel = new MiscConfigurationViewModel();
            UserManagementViewModel = new UserManagementViewModel();
            NPAConfigViewModel = new NPAConfigViewModel();
            OccupancyViewModel = new OccupancyViewModel();
            MiscConfigSchedulingMultiplierViewModel = new MiscConfigSchedulingMultiplierViewModel();
        }

        public DefaultHoursAdminViewModel DefaultHoursViewModel { get; set; }
        public HolidayConfigAdminViewModel HolidayConfigAdminViewModel { get; set; }

        public MessageConfigAdminViewModel MessageConfigAdminViewModel { get; set; }

        public List<HolidayConfig> HolidayConfigList { get; set; }
        public MiscConfigurationViewModel MiscConfigurationViewModel { get; set; }

        public MiscConfigSchedulingMultiplierViewModel MiscConfigSchedulingMultiplierViewModel { get; set; }
        public UserManagementViewModel UserManagementViewModel { get; set; }
        public bool ChkRecurAnnually { get; set; }

        public NPAConfigViewModel NPAConfigViewModel { get; set; }

        public OccupancyViewModel OccupancyViewModel { get; set; }
        public CreateRoleViewModel CreateRoleViewModel { get; set; }
        public ModifyRoleViewModel ModifyRoleViewModel { get; set; }
        public ModifyUserPermissionViewModel ModifyUserPermissionViewModel { get; set; }
    }
    public class HolidayConfigAdminViewModel
    {
        public HolidayConfigAdminViewModel()
        {
            AddHoliday = new HolidayConfig();
        }
        public HolidayConfig AddHoliday { get; set; }

    }


    public class MessageConfigAdminViewModel
    {
        private List<SelectListItem> _statusSelectList;

        public MessageConfigAdminViewModel()
        {
            MessageTemplateTypes = new List<SelectListItem>();
            MessageTemplates = new List<SelectListItem>();
        }
        public List<SelectListItem> MessageTemplateTypes { get; set; }
        public string MessageTemplateTypeId { get; set; }
        public List<SelectListItem> MessageTemplates { get; set; }
        public string MessageTemplateId { get; set; }
        public List<SelectListItem> StatusList
        {
            get
            {
                if (_statusSelectList == null) BuildStatusList();
                return _statusSelectList;
            }
        }

        public string MessageTemplateName { get; set; }

        public DateTime ActiveDate { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public string MessageTemplateText { get; set; }

        public List<MessageTemplateDataElement> MessageTemplateDataElements { get; set; }

        public bool IsActive { get; set; }

        public string IsEdit { get; set; }

        private void BuildStatusList()
        {
            _statusSelectList = new List<SelectListItem>();
            _statusSelectList.Add(new SelectListItem { Text = "Active ", Value = "true" });
            _statusSelectList.Add(new SelectListItem { Text = "Inactive", Value = "false" });

        }
    }


    public class DefaultHoursAdminViewModel
    {
        public DefaultHoursAdminViewModel()
        {
            ProjectType = new List<SelectListItem>();
            TradeSelectOptions = new List<SelectListItem>();
            AgencySelectOptions = new List<SelectListItem>();
            ZoneConfigSelectOptions = new List<SelectListItem>();
        }

        //[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal BuildingHrs { get; set; }

        public decimal ElectricHrs { get; set; }

        public decimal MechHrs { get; set; }

        public decimal PlumbHrs { get; set; }

        public decimal CountyZoneHrs { get; set; }

        public decimal CityZoneHrs { get; set; }

        public decimal CountyFireHrs { get; set; }

        public decimal CityFireHrs { get; set; }

        public decimal EHSDayCareHrs { get; set; }

        public decimal EHSFoodServiceHrs { get; set; }

        public decimal EHSPoolHrs { get; set; }

        public decimal EHSLodgingHrs { get; set; }

        public decimal BackFlowHrs { get; set; }

        public decimal CorneliusZoneHrs { get; set; }

        public decimal DavidsonZoneHrs { get; set; }

        public decimal HuntersvilleZoneHrs { get; set; }

        public decimal MatthewsZoneHrs { get; set; }

        public decimal MintHillZoneHrs { get; set; }

        public decimal PinevilleZoneHrs { get; set; }

        public List<SelectListItem> ProjectType { get; set; }

        public List<SelectListItem> TradeSelectOptions { get; set; }

        public List<SelectListItem> AgencySelectOptions { get; set; }

        public List<SelectListItem> ZoneConfigSelectOptions { get; set; }

        public string SelectedProjectType { get; set; }

        public string BuildingSelection { get; set; }

        public string ElectricSelection { get; set; }

        public string MechSelection { get; set; }

        public string PlumbSelection { get; set; }

        public string CountyZoneSelection { get; set; }

        public string CityZoneSelection { get; set; }

        public string CountyFireSelection { get; set; }

        public string CityFireSelection { get; set; }

        public string EHSDayCareSelection { get; set; }

        public string EHSFoodServiceSelection { get; set; }

        public string EHSPoolSelection { get; set; }

        public string EHSLodgingSelection { get; set; }

        public string BackFlowSelection { get; set; }

        public string CorneliusZoneSelection { get; set; }

        public string DavidsonZoneSelection { get; set; }

        public string HuntersvilleZoneSelection { get; set; }

        public string MatthewsZoneSelection { get; set; }

        public string MintHillZoneSelection { get; set; }

        public string PinevilleZoneSelection { get; set; }

        public string BuildingHrsAllowed { get; set; }

        public string ElectricHrsAllowed { get; set; }

        public string MechHrsAllowed { get; set; }

        public string PlumbHrsAllowed { get; set; }

        public string CountyZoneHrsAllowed { get; set; }

        public string CityZoneHrsAllowed { get; set; }

        public string CountyFireHrsAllowed { get; set; }

        public string CityFireHrsAllowed { get; set; }

        public string EHSDayCareHrsAllowed { get; set; }

        public string EHSFoodServiceHrsAllowed { get; set; }

        public string EHSPoolHrsAllowed { get; set; }

        public string EHSLodgingHrsAllowed { get; set; }

        public string BackFlowHrsAllowed { get; set; }

        public string CorneliusZoneHrsAllowed { get; set; }

        public string DavidsonZoneHrsAllowed { get; set; }

        public string HuntersvilleZoneHrsAllowed { get; set; }

        public string MatthewsZoneHrsAllowed { get; set; }

        public string MintHillZoneHrsAllowed { get; set; }

        public string PinevilleZoneHrsAllowed { get; set; }

    }

    public class NPAConfigViewModel
    {
        public NPAConfigViewModel()
        {
            ActiveNPAList = new List<SelectListItem>();
            InactiveNPAList = new List<SelectListItem>();
            SelectedInactiveNPAList = new List<string>();
            SelectedActiveNPAList = new List<string>();
        }
        public List<SelectListItem> ActiveNPAList { get; set; }

        public List<SelectListItem> InactiveNPAList { get; set; }

        public List<string> SelectedActiveNPAList { get; set; }

        public List<string> SelectedInactiveNPAList { get; set; }

        private List<SelectListItem> _timeAllocationTypeList;
        public List<SelectListItem> TimeAllocationTypeList
        {
            get
            {
                if (_timeAllocationTypeList == null)
                {
                    _timeAllocationTypeList = new List<SelectListItem>();
                    _timeAllocationTypeList.Add(new SelectListItem { Text = TimeAllocationType.NPA.ToStringValue(), Value = ((int)TimeAllocationType.NPA).ToString() });
                    _timeAllocationTypeList.Add(new SelectListItem { Text = TimeAllocationType.NPA_Staff_Meeting.ToStringValue(), Value = ((int)TimeAllocationType.NPA_Staff_Meeting).ToString() });
                    _timeAllocationTypeList.Add(new SelectListItem { Text = TimeAllocationType.NPA_PersonalTime.ToStringValue(), Value = ((int)TimeAllocationType.NPA_PersonalTime).ToString() });
                }
                return _timeAllocationTypeList;
            }
        }
        public string SelectedTimeAllocationType { get; set; }
        public string Addnewtext { get; set; }

    }

    public class MiscConfigurationViewModel
    {
        public MiscConfigurationViewModel()
        {
            FacilitatorManualProjectTypes = new List<SelectListItem>();
            FacilitatorAutoAssignableProjectTypes = new List<SelectListItem>();
            SelectedFacilitatorAutoAssignableProjectTypes = new List<string>();
            SelectedFacilitatorManualProjectTypes = new List<string>();
        }

        public CatalogItem ScheduleDateConfigurationCatalogItem { get; set; }

        //LES-1946 jcl 8-16-2021
        public CatalogItem CancellationFeePerHourCatalogItem { get; set; }

        public List<SelectListItem> FacilitatorManualProjectTypes { get; set; }
        public List<SelectListItem> FacilitatorAutoAssignableProjectTypes { get; set; }

        public List<string> SelectedFacilitatorManualProjectTypes { get; set; }
        public List<string> SelectedFacilitatorAutoAssignableProjectTypes { get; set; }

        public decimal HoursPlanReviewerMMF { get; set; }

        public decimal HoursMMF { get; set; }

        public decimal HoursCountyFire { get; set; }

        public decimal HoursExpress { get; set; }

        public decimal HoursFIFOSmComm { get; set; }

        public decimal HoursFIFOSingleFH { get; set; }

        public decimal HoursFIFOMsPln { get; set; }

        public decimal HoursFIFOAddRenSFH { get; set; }
        public CatalogItem SameBuildingContractorReviewDaysCatalogItem { get; set; }
        public DateTime StartTimeCommercial { get; set; }
        public DateTime EndTimeCommercial { get; set; }
        public DateTime StartTimeMMF { get; set; }
        public DateTime EndTimeMMF { get; set; }
        public DateTime StartTimeFIFO { get; set; }
        public DateTime EndTimeFIFO { get; set; }
        public DateTime StartTimeSpecialTeams { get; set; }
        public DateTime EndTimeSpecialTeams { get; set; }
        public DateTime StartTimeTownhomes { get; set; }
        public DateTime EndTimeTownhomes { get; set; }
        public DateTime StartTimeCSD { get; set; }
        public DateTime EndTimeCSD { get; set; }
    }

    public class MiscConfigSchedulingMultiplierViewModel
    {

        public MiscConfigSchedulingMultiplierViewModel()
        {
            SchedulingMultiplierUse = new List<SelectListItem>();
            SchedulingMultiplierProjectTypeList = new List<SelectListItem>();
        }

        private List<SelectListItem> _schedulingMultiplierUseSelectList;

        public string SchedulingMultiplierUseSelected { get; set; }

        public List<SelectListItem> SchedulingMultiplierUse { get; set; }

        public List<SelectListItem> SchedulingMultiplierUseSelectList
        {
            get
            {
                if (_schedulingMultiplierUseSelectList == null) BuildSchMultiplierUseList();
                return _schedulingMultiplierUseSelectList;
            }
        }
        public string SchedulingMultiplierName { get; set; }

        public DateTime SchedulingMultiplierStartDate { get; set; }
        public DateTime SchedulingMultiplierEndDate { get; set; }

        public decimal SchedulingMultiplierFactor { get; set; }

        public List<string> SchedulingMultiplierSelectedProjectTypes { get; set; }
        public List<SelectListItem> SchedulingMultiplierProjectTypeList { get; set; }
        private void BuildSchMultiplierUseList()
        {
            _schedulingMultiplierUseSelectList = new List<SelectListItem>();
            _schedulingMultiplierUseSelectList.Add(new SelectListItem { Text = "", Value = "-1" });
            _schedulingMultiplierUseSelectList.Add(new SelectListItem { Text = "Percentage", Value = "Percentage" });
            _schedulingMultiplierUseSelectList.Add(new SelectListItem { Text = "Hours", Value = "Hours" });

        }
    }

    public class UserManagementViewModel : ViewModelBase
    {
        private List<SelectListItem> _yNSelectList;

        private List<SelectListItem> _levelSelectList;

        private List<SelectListItem> _hoursEstimatedSelectList;

        public UserManagementViewModel()
        {
            UserNameList = new List<SelectListItem>();
            SelectedFacilitatorProjectTypes = new List<string>();
            UserTypeFilterList = new List<SelectListItem>();
            ProjectTypeList = new List<SelectListItem>();
            SelectedRoles = new List<string>();
            RoleList = new List<SelectListItem>();
            SelectedProjectTypes = new List<string>();
            SelectedTradeAgency = new List<string>();
            TradeAgencyList = new List<SelectListItem>();
            SelectedOccupancies = new List<string>();
            OccupancyList = new List<SelectListItem>();
            JurisdictionList = new List<SelectListItem>();
        }

        public string SelectedUserTypeFilter { get; set; }
        public string SelectedUserSearchFilter { get; set; }
        public List<SelectListItem> UserTypeFilterList { get; set; }
        public List<SelectListItem> UserNameList { get; set; }

        public List<SelectListItem> ProjectTypeList { get; set; }

        public List<string> SelectedProjectTypes { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string AdAccount { get; set; }

        public string PriliminaryMeetingSelected { get; set; }

        public List<SelectListItem> RoleList { get; set; }

        public List<string> SelectedRoles { get; set; }

        public List<string> SelectedTradeAgency { get; set; }

        public List<SelectListItem> TradeAgencyList { get; set; }

        public List<string> SelectedOccupancies { get; set; }

        public List<SelectListItem> OccupancyList { get; set; }

        public List<SelectListItem> SquareFootage { get; set; }

        public List<string> SelectedSquareFootage { get; set; }

        public string Notes { get; set; }

        public List<SelectListItem> IsSchedulableYNSelectList
        {
            get
            {
                if (_yNSelectList == null) BuildYNList();
                return _yNSelectList;
            }
        }

        public List<SelectListItem> IsActiveYNSelectList
        {
            get
            {
                if (_yNSelectList == null) BuildYNList();
                return _yNSelectList;
            }
        }

        public string IsSchedulableSelected { get; set; }

        public string IsActiveSelected { get; set; }

        public string PlanReviewHoursOverride { get; set; }

        public string SelectedHoursEstimated { get; set; }

        public List<SelectListItem> HoursEstimatedList
        {
            get
            {
                if (_hoursEstimatedSelectList == null) BuildHoursEstimatedList();
                return _hoursEstimatedSelectList;
            }
        }

        public string SelectedJurisdiction { get; set; }

        public List<SelectListItem> JurisdictionList { get; set; }

        public string LevelSelected { get; set; }

        public List<SelectListItem> LevelList
        {
            get
            {
                if (_levelSelectList == null) BuilSchedulableLevelList();
                return _levelSelectList;
            }
        }
        public string SelectedUser { get; set; }

        //public List<SelectListItem> AutoFacilitatorProjectTypes { get; set; }

        public List<string> SelectedFacilitatorProjectTypes { get; set; }
        public List<SelectListItem> YNSelectList
        {
            get
            {
                if (_yNSelectList == null) BuildYNList();
                return _yNSelectList;
            }
        }

        public string ExpressSelected { get; set; }

        public List<SelectListItem> IsCityYNSelectList
        {
            get
            {
                if (_yNSelectList == null) BuildYNList();
                return _yNSelectList;
            }
        }

        public string IsCityUserSelected { get; set; }

        public string UserPrincipalName { get; set; }
        public string CalendarId { get; set; }


        #region Private Methods
        private void BuildYNList()
        {
            _yNSelectList = new List<SelectListItem>();
            _yNSelectList.Add(new SelectListItem { Text = "N", Value = "N" });
            _yNSelectList.Add(new SelectListItem { Text = "Y", Value = "Y" });
        }

        private void BuilSchedulableLevelList()
        {
            _levelSelectList = new List<SelectListItem>();
            _levelSelectList.Add(new SelectListItem { Text = " ", Value = " " });
            _levelSelectList.Add(new SelectListItem { Text = "Level 1", Value = "Level1" });
            _levelSelectList.Add(new SelectListItem { Text = "Level 2", Value = "Level2" });
            _levelSelectList.Add(new SelectListItem { Text = "Level 3", Value = "Level3" });
        }



        private void BuildHoursEstimatedList()
        {
            _hoursEstimatedSelectList = new List<SelectListItem>();
            _hoursEstimatedSelectList.Add(new SelectListItem { Text = "All", Value = "All" });
            _hoursEstimatedSelectList.Add(new SelectListItem { Text = ".5-2", Value = ".5-2" });
            _hoursEstimatedSelectList.Add(new SelectListItem { Text = "2.5-4", Value = "2.5-4" });
            _hoursEstimatedSelectList.Add(new SelectListItem { Text = "4.5-8", Value = "4.5-8" });
            _hoursEstimatedSelectList.Add(new SelectListItem { Text = "8.5+", Value = "8.5+" });

        }


        #endregion Private Methods
    }

    public class OccupancyViewModel
    {
        public OccupancyViewModel()
        {
            SquareFootageList = new List<SelectListItem>();
            UserOccupancySquareFootageList = new List<UserMgmtOccupancy>();
        }

        public string SquareFootageIdSelected { get; set; }
        public List<SelectListItem> SquareFootageList { get; set; }
        public List<UserMgmtOccupancy> UserOccupancySquareFootageList { get; set; }

    }

}