using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Models.ProjectDetail;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class PlanReviewPartialViewModel
    {
        public int ProjectCycleId { get; set; }
        public int PlanReviewScheduleId { get; set; }
        public string PlanReviewDate { get; set; }

        public bool IsProjectSuspended
        {
            get
            {
                if (Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Suspended_Fees_Due)
                {
                    return true;
                }
                return false;
            }
        }

        public AppointmentResponseStatusEnum StatusEnum { get; set; }
        public string StatusText { get; set; }
        public int CycleNumber { get; set; }
        public string PlansReadyOnDate { get; set; }
        public DateTime? ScheduleAfterDt { get; set; }
        public DateTime? GateDate { get; set; }
        public string ResponseDate { get; set; }

        public bool IsCurrentCycle { get; set; }
        public bool IsFutureCycle { get; set; }
        public bool IsPreviousCycle { get; set; }
        public bool IsFifoProject { get; set; }
        public bool IsStatusEditable { get; set; } = false;
        public bool IsPlansReadyOnDateEditable { get; set; }
        public bool CanUpdate { get; set; }
        public bool IsPooled { get; set; }
        public UserIdentity LoggedInUser { get; set; }
        public ProjectEstimation Project { get; set; }
        public bool HasPlanReview { get; set; }
        public string CancellationMessage { get; set; }

        public bool PRODNotKnown { get; set; }

        public bool EditPROD { get; set; }
        public bool EditPR { get; set; }

        /// <summary>
        /// LES-4028 - add attendees for each cycle for each plan review
        /// </summary>
        public List<AssignedReviewerListViewModel> Attendees { get; set; }

        private ProjectCycle _ProjectCycle;
        private PlanReview _PlanReview;

        private bool _IsNewCycle;
        private bool? _IsApprovedCycle;
        private bool _IsRejectedCycle;
        private bool _HasCyclePROD;
        private bool _PreviouslyRejectedCycle;
        private bool _CanEditPlansReadyOnDate;

        /// <summary>
        /// LES-4028 get an empty model
        /// </summary>
        public PlanReviewPartialViewModel() { }

        public PlanReviewPartialViewModel(ProjectEstimation project, ProjectCycle projectCycle, PlanReview planReview, bool canEditPlansReadyOnDate, UserIdentity loggedInUser)
        {
            Project = project;
            LoggedInUser = loggedInUser;

            _ProjectCycle = projectCycle;

            _PlanReview = planReview;

            _CanEditPlansReadyOnDate = canEditPlansReadyOnDate;

            SetPrivateProperties();

            SetPublicProperties();
        }

        private void SetPrivateProperties()
        {
            _IsNewCycle = _ProjectCycle.IsAprvInd == null;

            _IsApprovedCycle = _ProjectCycle.IsAprvInd;
            _IsRejectedCycle = _ProjectCycle.IsAprvInd.HasValue && _ProjectCycle.IsAprvInd == false;
            _HasCyclePROD = _ProjectCycle.PlansReadyOnDt.HasValue;

            _PreviouslyRejectedCycle = _ProjectCycle.IsAprvInd.HasValue && _ProjectCycle.IsAprvInd.Value == false;
        }

        private void SetPublicProperties()
        {
            PRODNotKnown = Project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.PROD_Not_Known;

            ProjectCycleId = _ProjectCycle.ID;

            IsFifoProject = Project.IsFifo;

            HasPlanReview = _PlanReview != null && _PlanReview.ID > 0;

            IsCurrentCycle = _ProjectCycle.CurrentCycleInd.Value == true;
            IsFutureCycle = _ProjectCycle.FutureCycleInd.Value == true;
            IsPreviousCycle = _ProjectCycle.CurrentCycleInd.Value == false && _ProjectCycle.FutureCycleInd.Value == false;

            StatusEnum = _PlanReview != null ? _PlanReview.ApptResponseStatusEnum : AppointmentResponseStatusEnum.Not_Scheduled;

            StatusText = _PlanReview != null ? GenerateStatusText() : string.Empty;

            CycleNumber = _ProjectCycle.CycleNbr.Value;

            IsStatusEditable = StatusEditable();

            IsPlansReadyOnDateEditable = PlansReadyOnDateEditable();

            PlanReviewDate = GeneratePlanReviewDate();
            PlansReadyOnDate = _ProjectCycle.PlansReadyOnDt.HasValue ? _ProjectCycle.PlansReadyOnDt.Value.ToString("MM/dd/yyyy") : "";
            ScheduleAfterDt = _ProjectCycle.ScheduleAfterDt;
            ResponseDate = GenerateResponseDate();

            CanUpdate = IsStatusEditable || IsPlansReadyOnDateEditable;

            IsPooled = HasPlanReview && _PlanReview.AllPool && !IsFifoProject;

            if (IsPooled)
            {
                PlanReviewDate = string.Empty;
            }

            CancellationMessage = GenerateCancellationMessage();

            Attendees = GenerateTradesAgencies();

            if (_PlanReview != null)
            {
                PlanReviewScheduleId = _PlanReview.PlanReviewScheduleId.Value;
            }
        }

        private List<AssignedReviewerListViewModel> GenerateTradesAgencies()
        {
            if (_PlanReview == null)
            {
                return new List<AssignedReviewerListViewModel>();
            }
            List<AssignedReviewerListViewModel> tradesAgenciesViewModels = new List<AssignedReviewerListViewModel>();

            if (!string.IsNullOrWhiteSpace(_PlanReview.BuildAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.BuildAssignedReviewerName,
                                                _PlanReview.HoursBuilding,
                                                DepartmentDivisionEnum.Building.ToStringValue(),
                                                _PlanReview.BuildStartDate,
                                                _PlanReview.BuildEndDate,
                                                _PlanReview.BuildPool);

                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.ElectAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.ElectAssignedReviewerName,
                                                _PlanReview.HoursElectic,
                                                DepartmentDivisionEnum.Electrical.ToStringValue(),
                                                _PlanReview.ElectStartDate,
                                                _PlanReview.ElectEndDate,
                                                _PlanReview.ElectPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.MechaAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.MechaAssignedReviewerName,
                                                _PlanReview.HoursMech,
                                                DepartmentDivisionEnum.Mechanical.ToStringValue(),
                                                _PlanReview.MechaStartDate,
                                                _PlanReview.MechaEndDate,
                                                _PlanReview.MechaPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.PlumbAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.PlumbAssignedReviewerName,
                                                _PlanReview.HoursPlumb,
                                                DepartmentDivisionEnum.Plumbing.ToStringValue(),
                                                _PlanReview.PlumbStartDate,
                                                _PlanReview.PlumbEndDate,
                                                _PlanReview.PlumbPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.BackfAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.BackfAssignedReviewerName,
                                                _PlanReview.HoursBackFlow,
                                                DepartmentDivisionEnum.Backflow.ToStringValue(),
                                                _PlanReview.BackfStartDate,
                                                _PlanReview.BackfEndDate,
                                                _PlanReview.BackfPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.ZoneAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.ZoneAssignedReviewerName,
                                                _PlanReview.HoursZoning,
                                                DepartmentDivisionEnum.Zoning.ToStringValue(),
                                                _PlanReview.ZoneStartDate,
                                                _PlanReview.ZoneEndDate,
                                                _PlanReview.ZonePool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }

            if (!string.IsNullOrWhiteSpace(_PlanReview.FireAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.FireAssignedReviewerName,
                                                _PlanReview.HoursFire,
                                                DepartmentDivisionEnum.Fire.ToStringValue(),
                                                _PlanReview.FireStartDate,
                                                _PlanReview.FireEndDate,
                                                _PlanReview.FirePool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.DaycAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.DaycAssignedReviewerName,
                                                _PlanReview.HoursDayCare,
                                                ShortDepartmentNameEnums.EH_Day_Care.ToStringValue(),
                                                _PlanReview.DaycStartDate,
                                                _PlanReview.DaycEndDate,
                                                _PlanReview.DaycPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.PoolAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.PoolAssignedReviewerName,
                                                _PlanReview.HoursPool,
                                                ShortDepartmentNameEnums.EH_Pool.ToStringValue(),
                                                _PlanReview.PoolStartDate,
                                                _PlanReview.PoolEndDate,
                                                _PlanReview.PoolPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.FoodAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.FoodAssignedReviewerName,
                                                _PlanReview.HoursFood,
                                                ShortDepartmentNameEnums.EH_Food.ToStringValue(),
                                                _PlanReview.FoodStartDate,
                                                _PlanReview.FoodEndDate,
                                                _PlanReview.FoodPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            if (!string.IsNullOrWhiteSpace(_PlanReview.FacilAssignedReviewerName))
            {
                AssignedReviewerListViewModel tradesAgenciesViewModel = BuildTradesAgencyItem(
                                                _PlanReview.FacilAssignedReviewerName,
                                                _PlanReview.HoursLodge,
                                                ShortDepartmentNameEnums.EH_Facilities.ToStringValue(),
                                                _PlanReview.FacilStartDate,
                                                _PlanReview.FacilEndDate,
                                                _PlanReview.FacilPool);
                tradesAgenciesViewModels.Add(tradesAgenciesViewModel);
            }
            return tradesAgenciesViewModels;

        }
        private AssignedReviewerListViewModel BuildTradesAgencyItem(string reviewerName, decimal hours, string deptName, DateTime? startDt, DateTime? endDt, bool? isPool)
        {
            bool isPooled = isPool.HasValue && isPool.Value == true;

            AssignedReviewerListViewModel tradesAgenciesViewModel = new AssignedReviewerListViewModel();
            tradesAgenciesViewModel.TradeName = deptName;
            tradesAgenciesViewModel.Hours = isPooled ? "" : hours.ToString();
            tradesAgenciesViewModel.ReviewerName = reviewerName;
            if (isPooled)
            {
                tradesAgenciesViewModel.DateRange = "Pooled";
            }
            else
            {
                tradesAgenciesViewModel.DateRange = ScheduleHelpers.BuildDateRangeString(startDt.HasValue?startDt.Value:default, endDt.HasValue?endDt.Value:default);

            }

            if (IsFifoProject)
            {
                tradesAgenciesViewModel.DateRange = "FIFO";
            }

            return tradesAgenciesViewModel;
        }

        private string GenerateResponseDate()
        {
            if (HasPlanReview && IsStatusEditable && _PlanReview.ResponseDate.HasValue && !_IsRejectedCycle)
            {
                return _PlanReview.ResponseDate.Value.ToString("MM/dd/yyyy");
            }
            else { return ""; };
        }
        private bool StatusEditable()
        {
            bool retStatusEditable = false;
            if (HasPlanReview && (IsCurrentCycle || IsFutureCycle)
                && PRODNotKnown == false
                && StatusEnum != AppointmentResponseStatusEnum.Cancelled)
            {
                if (_HasCyclePROD && (_PlanReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled))
                {
                    retStatusEditable = true;
                }

                if (_PreviouslyRejectedCycle && !_HasCyclePROD)
                {
                    retStatusEditable = false;
                }

                if (_PreviouslyRejectedCycle && _HasCyclePROD && (_PlanReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled))
                {
                    retStatusEditable = true;
                }
            }
            return retStatusEditable;
        }
        private bool PlansReadyOnDateEditable()
        {
            if (_PreviouslyRejectedCycle || PRODNotKnown && (IsCurrentCycle || IsFutureCycle))
            {
                return true;
            }
            return false;
        }
        private string GenerateStatusText()
        {
            string retStatusText = string.Empty;

            if (_PlanReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Accept
                || _PlanReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled)
            {
                retStatusText = "Accepted";
            }

            if (_PlanReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Reject)
            {
                retStatusText = "Rejected";
            }

            return retStatusText;
        }
        private string GeneratePlanReviewDate()
        {
            string retPlanReviewDate = string.Empty;

            if (StatusEnum != AppointmentResponseStatusEnum.Cancelled
                && StatusEnum != AppointmentResponseStatusEnum.No_Reply
                && StatusEnum != AppointmentResponseStatusEnum.Not_Scheduled
                && HasPlanReview
                 && _PlanReview.StartDate.HasValue
                && !_IsRejectedCycle
                && PRODNotKnown == false)
            {
                retPlanReviewDate = _PlanReview.StartDate.Value.ToString("MM/dd/yyyy");
            }

            if (HasPlanReview && _PlanReview.IsReschedule)
            {
                retPlanReviewDate += " - Tentative";
            }
            //TODO: jcl is this all pooled with no dates?  then show POOLED in the detail row
            //????????????????????????????
            if (HasPlanReview)
            {

            }
            return retPlanReviewDate;
        }


        private string GenerateCancellationMessage()
        {
            string retCancellationMessage = string.Empty;

            if (_PlanReview != null && StatusEnum != AppointmentResponseStatusEnum.Cancelled)
            {
                retCancellationMessage = _PlanReview.CancellationMessage;
            }
            return retCancellationMessage;
        }
        private List<SelectListItem> _planReviewResponseSelectList;
        public List<SelectListItem> PlanReviewResponseSelectList
        {
            get
            {
                if (_planReviewResponseSelectList == null) BuildPlanReviewResponseSelectList();
                return _planReviewResponseSelectList;
            }
        }

        private void BuildPlanReviewResponseSelectList()
        {
            _planReviewResponseSelectList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = PlanReviewResponseStatusEnum.Accept.ToStringValue(), Value = ((int)PlanReviewResponseStatusEnum.Accept).ToString()},
                 new SelectListItem() { Text = PlanReviewResponseStatusEnum.Reject.ToStringValue(), Value = ((int)PlanReviewResponseStatusEnum.Reject).ToString()},

            };
        }
        private List<SelectListItem> _planReviewCustomerResponseSelectList;
        public List<SelectListItem> PlanReviewCustomerResponseSelectList
        {
            get
            {
                if (_planReviewCustomerResponseSelectList == null) BuildPlanReviewCustomerResponseSelectList();
                return _planReviewCustomerResponseSelectList;
            }
        }

        private void BuildPlanReviewCustomerResponseSelectList()
        {
            _planReviewCustomerResponseSelectList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = PlanReviewResponseStatusEnum.Accept.ToStringValue(), Value = ((int)PlanReviewResponseStatusEnum.Accept).ToString()},
                 new SelectListItem() { Text = PlanReviewResponseStatusEnum.Reject.ToStringValue(), Value = ((int)PlanReviewResponseStatusEnum.Reject).ToString()},
                 new SelectListItem() { Text = PlanReviewResponseStatusEnum.Self_Schedule.ToStringValue(), Value = ((int)PlanReviewResponseStatusEnum.Self_Schedule).ToString()}

            };
        }
    }
}