using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Web.Models;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Web.Helpers
{
    public class ConvertProjectHelpers
    {

        private EstimationSaveViewModel _estimationSaveVM;
        private List<Reviewer> _allReviewers;

        private ProjectEstimation _pe;
        private List<CatalogItem> _pendingreasons;
        private APIHelper _aPIHelper;
        private List<ProjectStatus> _projectStatusBaseList;
        private List<NoteType> _noteTypesBaseList;

        private List<ProjectTrade> _projectTradesToEstimate = new List<ProjectTrade>();
        private List<ProjectAgency> _projectAgenciesToEstimate = new List<ProjectAgency>();

        public ConvertProjectHelpers()
        {
            _aPIHelper = new APIHelper();
        }

        private void ConvertToProjectEstimation(EstimationSaveViewModel vm)
        {
            _estimationSaveVM = vm;
            _allReviewers = ScheduleAPIHelper.GetReviewers(0, -1, false);
            _pendingreasons = _aPIHelper.GetAllPendingReasons();

            //adding a NA user so that its's id -1 can be picked up and saved.
            UserIdentity naUser = _aPIHelper.GetUserIdentityByID(-1);
            Reviewer naReviewer = new Reviewer
            {
                FirstName = naUser.FirstName,
                LastName = naUser.LastName,
                ID = naUser.ID
            };
            _allReviewers.Add(naReviewer);

            _pe = new ProjectEstimation();

            _pe = _aPIHelper.GetProjectDetailsForEstimationByAccelaId(vm.Project.AccelaProjectRefId, vm.Project.RecIdTxt);

            _pe.UpdatedUser = vm.Project.UpdatedUser;

            //setting the inital status. this will be overidden below incase any of the pending status is set from UI.
            _pe.AIONProjectStatus = GetProjectStatus(ProjectStatusEnum.Estimation_In_Progress);

            _pe.AssignedFacilitator = int.Parse(vm.AssignedFacilitator);

            _pe.AssignedEstimator = vm.AssignedEstimatorId;

            _pe.AuditAction = AuditActionEnum.Estimation_Change;

            DetermineTradesAndAgenciesForEstimation(vm);
        }

        private void DetermineTradesAndAgenciesForEstimation(EstimationSaveViewModel vm)
        {
            Helper helper = new Helper();
            // narrow down trades/agencies by active tab
            switch (vm.ActiveTab)
            {
                case "nav-bemp-tab":
                    _projectTradesToEstimate.Add(_pe.Trades.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.Building));
                    _projectTradesToEstimate.Add(_pe.Trades.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.Electrical));
                    _projectTradesToEstimate.Add(_pe.Trades.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical));
                    _projectTradesToEstimate.Add(_pe.Trades.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing));
                    break;

                case "nav-zoning-tab":
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)));

                    break;

                case "nav-fire-tab":
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)));

                    break;

                case "nav-backflow-tab":
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.Backflow));
                    break;

                case "nav-health-tab":
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care));
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities));
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food));
                    _projectAgenciesToEstimate.Add(_pe.Agencies.FirstOrDefault(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool));
                    break;
            }
        }

        private void DetermineEstimationTradeAgencyStatus(EstimationSaveViewModel vm)
        {
            bool isEstimationComplete = false;

            bool areAllTradesComplete = true;
            bool areAllAgenciesComplete = true;

            //set the project level workflow status.
            if (vm.IsAllNAChecked)
            {
                isEstimationComplete = true;
                areAllTradesComplete = true;
                areAllAgenciesComplete = true;
            }
            else
            {
                foreach (var item in _pe.Trades)
                {
                    //skips check in case dept is set as NA
                    if ((item.DepartmentDivision == DepartmentDivisionEnum.NA && item.DepartmentInfo == DepartmentNameEnums.NA) || item.EstimationNotApplicable == true)
                    {
                        continue;
                    }

                    if (item.DepartmentStatus == ProjectDisplayStatus.Complete)
                    {
                        //
                    }
                    else
                    {
                        areAllTradesComplete = false;
                        break;
                    }
                }
                if (areAllTradesComplete == true) //do not continue if one of the trade is incomplete since the estimation is not complete.
                {
                    foreach (var item in _pe.Agencies)
                    {
                        //skips check in case dept is set as NA
                        if ((item.DepartmentDivision == DepartmentDivisionEnum.NA
                            && item.DepartmentInfo == DepartmentNameEnums.NA)
                            || item.EstimationNotApplicable == true)
                        {
                            continue;
                        }
                        if (item.DepartmentStatus == ProjectDisplayStatus.Complete)
                        {
                            //
                        }
                        else
                        {
                            areAllAgenciesComplete = false;
                            break;
                        }
                    }
                }
            }
            isEstimationComplete = (areAllTradesComplete && areAllAgenciesComplete);

            if (isEstimationComplete == true && vm.IsAllNAChecked == false && vm.IsSubmit == true)
            {
                _pe.AIONProjectWorkFlowStatus = GetProjectStatus(ProjectStatusEnum.Not_Scheduled);

                if (_pe.IsFifo)
                {
                    _pe.AIONProjectWorkFlowStatus = GetProjectStatus(ProjectStatusEnum.Scheduled);
                }

                _pe.AIONProjectStatus = _pe.AIONProjectWorkFlowStatus;
            }
            else
            {
                _pe.AIONProjectWorkFlowStatus = GetProjectStatus(ProjectStatusEnum.Estimation_In_Progress);
                _pe.AIONProjectStatus = _pe.AIONProjectWorkFlowStatus;
            }
        }

        public ProjectEstimation ConvertSaveBulkEstimationToProjectEstimation(BulkEstimationSaveViewModel vm)
        {
            ConvertToProjectEstimation(vm);

            bool pending = false;

            ProjectStatus deptstatus = new ProjectStatus();

            deptstatus = _pe.AIONProjectStatus;

            bool isSubmit = false;
            string departmentStatus = string.Empty;

            foreach (ProjectTrade trade in _pe.Trades)
            {
                departmentStatus = trade.DepartmentStatus == ProjectDisplayStatus.NewApplication ? string.Empty : trade.DepartmentStatus;
                isSubmit = false;

                trade.AuditAction = AuditActionEnum.Estimation_Change;

                switch (trade.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:
                        trade.EstimationHours = vm.HoursBuilding;
                        trade.EstimationNotApplicable = vm.HrsNABuilding;

                        if (trade.EstimationNotApplicable == false && trade.EstimationHours > 0 || trade.EstimationNotApplicable)
                        {
                            isSubmit = true;
                        }

                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, departmentStatus, isSubmit, true, true);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersBuild);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerBuilding);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerBuilding);
                        trade.DepartmentStatusRef = GetInProgressDeptStatusRef(deptstatus, trade.DepartmentStatus);
                        break;

                    case DepartmentNameEnums.Electrical:
                        trade.EstimationHours = vm.HoursElectic;
                        trade.EstimationNotApplicable = vm.HrsNAElectric;

                        if (trade.EstimationNotApplicable == false && trade.EstimationHours > 0 || trade.EstimationNotApplicable)
                        {
                            isSubmit = true;
                        }

                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, departmentStatus, isSubmit, true, true);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersElectric);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerElectrical);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerelectrical);
                        trade.DepartmentStatusRef = GetInProgressDeptStatusRef(deptstatus, trade.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.Mechanical:
                        trade.EstimationHours = vm.HoursMech;
                        trade.EstimationNotApplicable = vm.HrsNAMech;

                        if (trade.EstimationNotApplicable == false && trade.EstimationHours > 0 || trade.EstimationNotApplicable)
                        {
                            isSubmit = true;
                        }

                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, departmentStatus, isSubmit, true, true);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersMech);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerMechanical);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerMechanical);
                        trade.DepartmentStatusRef = GetInProgressDeptStatusRef(deptstatus, trade.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.Plumbing:
                        trade.EstimationHours = vm.HoursPlumb;
                        trade.EstimationNotApplicable = vm.HrsNAPlumbing;

                        if (trade.EstimationNotApplicable == false && trade.EstimationHours > 0 || trade.EstimationNotApplicable)
                        {
                            isSubmit = true;
                        }

                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, departmentStatus, isSubmit, true, true);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersPlumb);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerPlumbing);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerPlumbing);
                        trade.DepartmentStatusRef = GetInProgressDeptStatusRef(deptstatus, trade.DepartmentStatus);
                        break;
                    default:
                        break;
                }
            }
            foreach (ProjectAgency agency in _pe.Agencies)
            {
                departmentStatus = agency.DepartmentStatus == ProjectDisplayStatus.NewApplication ? string.Empty : agency.DepartmentStatus;
                isSubmit = false;

                agency.AuditAction = AuditActionEnum.Estimation_Change;

                switch (agency.DepartmentInfo)
                {
                    case DepartmentNameEnums.Fire_Davidson:
                    case DepartmentNameEnums.Fire_Cornelius:
                    case DepartmentNameEnums.Fire_Pineville:
                    case DepartmentNameEnums.Fire_Matthews:
                    case DepartmentNameEnums.Fire_Mint_Hill:
                    case DepartmentNameEnums.Fire_Huntersville:
                    case DepartmentNameEnums.Fire_UMC:
                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                    case DepartmentNameEnums.Fire_County:
                        agency.EstimationHours = vm.HoursFire;
                        agency.EstimationNotApplicable = vm.HrsNAFire;

                        if (agency.EstimationNotApplicable == false && agency.EstimationHours > 0 || agency.EstimationNotApplicable)
                        {
                            isSubmit = true;
                        }

                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, departmentStatus, isSubmit, true, true);
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersFire);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerFire);
                        agency.DepartmentStatusRef = GetInProgressDeptStatusRef(deptstatus, agency.DepartmentStatus);
                        break;
                    default:
                        break;
                }
            }

            DetermineEstimationTradeAgencyStatus(vm);
            //jcl LES-4132 1/18/22 remove Reviewers since this doesn't get saved
            _pe.Reviewers = null;

            return _pe;
        }

        /// <summary>
        /// Used to convert save view model from EstimationMain Update
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ProjectEstimation ConvertSaveEstimationVmToProjectEstimation(EstimationSaveViewModel vm)
        {
            ConvertToProjectEstimation(vm);

            bool pending;
            int pendingid;

            int projId = _pe.ID == 0 ? -1 : _pe.ID;

            ProjectStatus deptstatus = new ProjectStatus();

            deptstatus = _pe.AIONProjectStatus;

            //update the values that have been changed in the save
            //need to get the trades and agencies so I can save the hours,NA, reviewers

            bool includedForEstimation;

            foreach (ProjectTrade trade in _pe.Trades)
            {
                trade.UpdatedUser = vm.Project.UpdatedUser;

                includedForEstimation = false;

                if (_projectTradesToEstimate.Contains(trade)) // falls within the UI tab selected for estimating
                {
                    includedForEstimation = true;
                    trade.AuditAction = AuditActionEnum.Estimation_Change;
                }

                switch (trade.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:
                        pendingid = (int)vm.BEMPApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);

                        trade.EstimationHours = vm.HoursBuilding;
                        trade.EstimationNotApplicable = vm.HrsNABuilding;
                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, trade.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersBuild);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerBuilding);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerBuilding);
                        if (!string.IsNullOrEmpty(vm.BEMPApplicationNotes.PendingNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.PendingNotes, vm.BEMPApplicationNotes.PendingNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, DepartmentNameEnums.Building, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.BEMPApplicationNotes.PendingGateNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.GateNotes, vm.BEMPApplicationNotes.PendingGateNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, DepartmentNameEnums.Building, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.BEMPApplicationNotes.InternalNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.InternalNotes, vm.BEMPApplicationNotes.InternalNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, DepartmentNameEnums.Building, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);
                        }
                        trade.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, trade.DepartmentStatus);
                        break;

                    case DepartmentNameEnums.Electrical:
                        pendingid = (int)vm.BEMPApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);

                        trade.EstimationHours = vm.HoursElectic;
                        trade.EstimationNotApplicable = vm.HrsNAElectric;
                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, trade.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersElectric);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerElectrical);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerelectrical);
                        trade.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, trade.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.Mechanical:
                        pendingid = (int)vm.BEMPApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);

                        trade.EstimationHours = vm.HoursMech;
                        trade.EstimationNotApplicable = vm.HrsNAMech;
                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, trade.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersMech);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerMechanical);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerMechanical);
                        trade.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, trade.DepartmentStatus); ;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        pendingid = (int)vm.BEMPApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.BEMPApplicationNotes.PendingReason);

                        trade.EstimationHours = vm.HoursPlumb;
                        trade.EstimationNotApplicable = vm.HrsNAPlumbing;
                        trade.DepartmentStatus = GetDeptStatus(trade.EstimationNotApplicable, trade.EstimationHours, pending, trade.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        trade.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersPlumb);
                        trade.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerPlumbing);
                        trade.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerPlumbing);
                        trade.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, trade.DepartmentStatus);
                        break;
                    default:
                        break;
                }
            }

            foreach (ProjectAgency agency in _pe.Agencies)
            {
                agency.UpdatedUser = vm.Project.UpdatedUser;

                includedForEstimation = false;

                if (_projectAgenciesToEstimate.Contains(agency)) // falls within the UI tab selected for estimating
                {
                    includedForEstimation = true;
                    agency.AuditAction = AuditActionEnum.Estimation_Change;
                }

                switch (agency.DepartmentInfo)
                {
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        pendingid = (int)vm.ZoningApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.ZoningApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursZoning;
                        agency.EstimationNotApplicable = vm.HrsNAZone;
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersZone);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerZone);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerZone);
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        if (!string.IsNullOrEmpty(vm.ZoningApplicationNotes.PendingNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.PendingNotes, vm.ZoningApplicationNotes.PendingNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.ZoningApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.ZoningApplicationNotes.PendingGateNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.GateNotes, vm.ZoningApplicationNotes.PendingGateNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.ZoningApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.ZoningApplicationNotes.InternalNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.InternalNotes, vm.ZoningApplicationNotes.InternalNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.ZoningApplicationNotes.PendingReason);
                        }
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
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
                        pendingid = (int)vm.FireApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.FireApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursFire;
                        agency.EstimationNotApplicable = vm.HrsNAFire;
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersFire);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerFire);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerFire);
                        if (!string.IsNullOrEmpty(vm.FireApplicationNotes.PendingNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.PendingNotes, vm.FireApplicationNotes.PendingNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.FireApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.FireApplicationNotes.PendingGateNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.GateNotes, vm.FireApplicationNotes.PendingGateNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.FireApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.FireApplicationNotes.InternalNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.InternalNotes, vm.FireApplicationNotes.InternalNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.FireApplicationNotes.PendingReason);
                        }
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        pendingid = (int)vm.EHSApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursDayCare;
                        agency.EstimationNotApplicable = vm.HrsNADayCare;
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersDayCare);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerDayCare);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerDayCare);
                        if (!string.IsNullOrEmpty(vm.EHSApplicationNotes.PendingNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.PendingNotes, vm.EHSApplicationNotes.PendingNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.EHSApplicationNotes.PendingGateNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.GateNotes, vm.EHSApplicationNotes.PendingGateNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.EHSApplicationNotes.InternalNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.InternalNotes, vm.EHSApplicationNotes.InternalNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, agency.DepartmentInfo, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);
                        }
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.EH_Food:
                        pendingid = (int)vm.EHSApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursFood;
                        agency.EstimationNotApplicable = vm.HrsNAFood;
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersFood);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerFood);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerFood);
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        pendingid = (int)vm.EHSApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursPool;
                        agency.EstimationNotApplicable = vm.HrsNAPool;
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersPool);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerPool);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerPool);
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        pendingid = (int)vm.EHSApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.EHSApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursLodge;
                        agency.EstimationNotApplicable = vm.HrsNAFacility;
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersLodge);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerFacilities);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerFacilities);
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.Backflow:
                        pendingid = (int)vm.BackFlowApplicationNotes.PendingReason;
                        pending = _pendingreasons.Where(x => x.SubKey == pendingid.ToString()).Any();
                        deptstatus = GetProjectStatus(vm.BackFlowApplicationNotes.PendingReason);

                        agency.EstimationHours = vm.HoursBackFlow;
                        agency.EstimationNotApplicable = vm.HrsNABackFlow;
                        agency.DepartmentStatus = GetDeptStatus(agency.EstimationNotApplicable, agency.EstimationHours, pending, agency.DepartmentStatus, vm.IsSubmit, includedForEstimation, vm.IsAllNAChecked);
                        agency.ExcludedPlanReviewers = GetExcludedPlanReviewers(_allReviewers, vm.ExcludedPlanReviewersBackFlow);
                        agency.PrimaryPlanReviewer = GetUserIdentity(_allReviewers, vm.PrimaryReviewerBackFlow);
                        agency.SecondaryPlanReviewer = GetUserIdentity(_allReviewers, vm.SecondaryReviewerBackFlow);
                        if (!string.IsNullOrEmpty(vm.BackFlowApplicationNotes.PendingNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.PendingNotes, vm.BackFlowApplicationNotes.PendingNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, DepartmentNameEnums.Backflow, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.BackFlowApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.BackFlowApplicationNotes.PendingGateNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.GateNotes, vm.BackFlowApplicationNotes.PendingGateNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, DepartmentNameEnums.Backflow, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.BackFlowApplicationNotes.PendingReason);
                        }
                        if (!string.IsNullOrEmpty(vm.BackFlowApplicationNotes.InternalNotesComments))
                        {
                            _pe.Notes.Add(CreateNote(NoteTypeEnum.InternalNotes, vm.BackFlowApplicationNotes.InternalNotesComments, vm.Project.UpdatedUser, vm.Project.UpdatedUser, DepartmentNameEnums.Backflow, projId));
                            _pe.AIONProjectStatus = GetProjectStatus(vm.BackFlowApplicationNotes.PendingReason);
                        }
                        agency.DepartmentStatusRef = GetDeptStatusRef(vm.IsSubmit, deptstatus, agency.DepartmentStatus);
                        break;
                    case DepartmentNameEnums.NA:
                        break;
                    default:
                        break;
                }
            }

            DetermineEstimationTradeAgencyStatus(vm);

            //jcl LES-4132 1/18/22 remove Reviewers since this doesn't get saved
            _pe.Reviewers = null;
            return _pe;
        }

        public List<int> GetExcludedPlanReviewers(List<Reviewer> allreviewers, List<string> excludedReviewers)
        {
            List<int> ret = new List<int>();
            if (excludedReviewers == null)
                return ret;
            foreach (string item in excludedReviewers)
            {
                ret.Add(int.Parse(item));
            }
            return ret;
        }

        public UserIdentity GetUserIdentity(List<Reviewer> allreviewers, string reviewerID)
        {
            if (string.IsNullOrWhiteSpace(reviewerID))
                return new UserIdentity { ID = -1 };
            return new UserIdentity { ID = int.Parse(reviewerID) };
        }

        public ProjectEstimation ConvertSaveCustmrProjectDetailToProjEstimation(CustmrProjectSaveViewModel vm)
        {
            ProjectEstimation pe = new ProjectEstimation();
            pe = _aPIHelper.GetProjectDetailsForEstimationByAccelaId(vm.Project.AccelaProjectRefId, vm.Project.RecIdTxt);
            pe.UpdatedUser = vm.Project.UpdatedUser;
            foreach (Note note in vm.CreatedNotes)
            {
                if (pe.Trades.Where(x => x.DepartmentInfo == note.DeptNameEnum).Any())
                {
                    //BEMP needs to be set to CR if any trade gets a reponse
                    foreach (ProjectTrade trade in pe.Trades.Where(x => new Helper().BEMPDepartmentNames.Contains(x.DepartmentInfo)))
                    {
                        trade.DepartmentStatus = ProjectDisplayStatus.CustomerResponded;
                    }
                }
                if (pe.Agencies.Where(x => x.DepartmentInfo == note.DeptNameEnum).Any())
                {
                    foreach (ProjectAgency agency in pe.Agencies)
                    {
                        {
                            if (agency.DepartmentInfo == note.DeptNameEnum)
                            {
                                agency.DepartmentStatus = ProjectDisplayStatus.CustomerResponded;
                            }
                        }
                    }
                }
            }
            return pe;
        }

        public ProjectEstimation ConvertProjectDetailToProjEstimation(ProjectDetailViewModel vm)
        {
            ProjectEstimation pe = new ProjectEstimation();
            pe = _aPIHelper.GetProjectDetailsForEstimationByAccelaId(vm.Project.AccelaProjectRefId, vm.Project.RecIdTxt);
            pe.PlansReadyOnDate = vm.Project.PlansReadyOnDate;
            foreach (Note note in vm.Notes)
            {
                if (pe.Trades.Where(x => x.DepartmentInfo == note.DeptNameEnum).Any())
                {
                    //BEMP needs to be set to CR if any trade gets a reponse
                    foreach (ProjectTrade trade in pe.Trades.Where(x => new Helper().BEMPDepartmentNames.Contains(x.DepartmentInfo)))
                    {
                        trade.DepartmentStatus = "CR";
                    }
                }
                if (pe.Agencies.Where(x => x.DepartmentInfo == note.DeptNameEnum).Any())
                {
                    foreach (ProjectAgency agency in pe.Agencies)
                    {
                        {
                            if (agency.DepartmentInfo == note.DeptNameEnum)
                            {
                                agency.DepartmentStatus = "CR";
                            }
                        }
                    }
                }
            }
            return pe;
        }

        #region Private Methods
        /// <summary>
        /// gets the string C or P depending on values
        /// </summary>
        /// <returns></returns>
        private string GetDeptStatus(bool na, decimal? hours, bool pending, string deptstatus, bool issubmit, bool includedForEstimation, bool isAllMarkedNA)
        {
            if (issubmit)
            {
                if (na == false && hours > 0 || na == true)
                {
                    if (pending)
                    {
                        return ProjectDisplayStatus.Pending;
                    }
                    else if (includedForEstimation || isAllMarkedNA)
                    {
                        return ProjectDisplayStatus.Complete;
                    }
                    else
                    {
                        return deptstatus;
                    }
                }
            }
            else
            {
                string newdeptstatus = "";
                newdeptstatus = deptstatus == ProjectDisplayStatus.Pending && pending == false ? "" : deptstatus;
                return pending ? ProjectDisplayStatus.Pending : newdeptstatus;

            }
            return deptstatus;
        }

        private ProjectStatus GetInProgressDeptStatusRef(ProjectStatus deptstatus, string departmentStatus)
        {
            ProjectStatus completestatus = GetProjectStatus(ProjectStatusEnum.Estimation_In_Progress);
            return (departmentStatus == ProjectDisplayStatus.Complete) ? completestatus : deptstatus;
        }

        private ProjectStatus GetDeptStatusRef(bool isSubmit, ProjectStatus deptstatus, string departmentStatus)
        {
            ProjectStatus completestatus = GetProjectStatus(ProjectStatusEnum.Not_Scheduled);
            return (departmentStatus == ProjectDisplayStatus.Complete) ? completestatus : deptstatus;
        }

        private ProjectStatus GetProjectStatus(ProjectStatusEnum ProjectStatusEnum)
        {
            if (_projectStatusBaseList == null)
                _projectStatusBaseList = _aPIHelper.GetProjectStatusBaseList();

            var t = _projectStatusBaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum).FirstOrDefault();
            return t;
        }
        private NoteType GetNoteType(NoteTypeEnum NoteTypeEnum)
        {
            if (_noteTypesBaseList == null)
                _noteTypesBaseList = NoteAPIHelper.GetNoteTypeBaseList();
            var t = _noteTypesBaseList.Where(x => x.Type == NoteTypeEnum).FirstOrDefault();
            return t;
        }
        private Note CreateNote(NoteTypeEnum noteType, string noteComment, UserIdentity createdUser, UserIdentity updatedUser, DepartmentNameEnums deptNameEnum, int projectID = -1, int parentID = 0)
        {

            Note ret = new Note();
            ret.ProjectID = projectID;
            ret.Attachments = new List<NotesAttachments>();
            ret.NotesType = GetNoteType(noteType);
            ret.NotesComments = noteComment;
            ret.CreatedDate = DateTime.Now;
            //   ret.CreatedUser = new UserIdentity();
            ret.CreatedUser = createdUser;
            ret.UpdatedDate = DateTime.Now;
            //  ret.UpdatedUser = new UserIdentity();
            ret.UpdatedUser = updatedUser;
            ret.ParentNoteID = parentID;
            ret.DeptNameEnum = deptNameEnum;
            return ret;
        }
        #endregion Private Methods
    }
}