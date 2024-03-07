using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Models.ProjectDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class ProjectDetailViewModel : ViewModelBase
    {
        private List<ProjectAudit> _projectAudits;
        private List<AuditActionRef> _auditActionRefs;
        private List<SelectListItem> _planReviewResponseSelectList;

        public ProjectDetailViewModel()
        {
            AuditLogs = new List<TableAuditLog>();
            MeetingRoomList = new List<MeetingRoom>();
            _projectAudits = new List<ProjectAudit>();
            _auditActionRefs = new List<AuditActionRef>();
        }
        public List<ProjectCycleReview> ProjectCycleReviews { get; set; } = new List<ProjectCycleReview>();
        public ProjectCycle ProjectCycle { get; set; }
        public List<PlanReviewPartialViewModel> PlanReviewViewModels { get; set; } = new List<PlanReviewPartialViewModel>();
        public PlanReviewResponseStatusEnum? PlanReviewResponseStatusSelectedEnum { get; set; }
        public int? PlanReviewResponseStatusSelected { get; set; }
        public int? PRResponse { get; set; }
        public int? FuturePRResponse { get; set; }
        public int? EMAResponse { get; set; }
        public bool RescheduleWarning { get; set; }
        public List<AuditActionRef> AuditActionRefs { get { return _auditActionRefs; } set { _auditActionRefs = value; } }
        public List<Note> Notes { get; set; }
        public List<ProjectAudit> ProjectAudits
        {
            get { return _projectAudits; }
            set
            {
                _projectAudits = value;
                if (_projectAudits != null)
                {
                    SetAuditActionRef();
                }
                else
                {
                    _projectAudits = new List<ProjectAudit>();
                }
            }
        }
        public List<TableAuditLog> AuditLogs { get; set; }
        public PlanReview PlanReview { get; set; }
        public FIFOSchedule FIFOSchedule { get; set; }
        public int Cycle { get; set; }
        public bool ProdNotKnown { get; set; }
        public bool IsAbort { get; set; }
        public DateTime? PRProdDate { get; set; }
        public DateTime? PlanReviewDate { get; set; }
        public DateTime? GateDate { get; set; }
        public List<Meeting> ScheduledMeetingList { get; set; }

        public List<SelectListItem> PlanReviewResponseSelectList
        {
            get
            {
                if (_planReviewResponseSelectList == null) BuildPlanReviewResponseSelectList();
                return _planReviewResponseSelectList;
            }
        }
        public string SelectedMeetingAction { get; set; }
        public string MeetingRoomNameSelected { get; set; }
        public List<MeetingRoom> MeetingRoomList { get; set; }
        public string ReschedulePlanReviewDisabled { get; set; } = "disabled";
        public string RescheduleExpressReviewDisabled { get; set; }
        public string AccelaProjectDeeplink { get; set; }

        /// <summary>
        /// The list of notifications for _SendNotifications modal
        /// </summary>
        public List<SendProjectNotifViewModel> ProjectNotifs { get; set; }
        public string FacilitatorName { get; internal set; }
        public List<Facilitator> FacilitatorList { get; set; }

        public Facilitator SelectedFacilitator { get; set; }

        public string CancelMeetingNotes { get; set; }

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

        #region Private Methods
        private void SetAuditActionRef()
        {
            foreach (ProjectAudit projectaudit in _projectAudits)
            {
                projectaudit.AuditActionRef = _auditActionRefs.Where(x => x.AuditActionRefId == projectaudit.AuditActionRefId).FirstOrDefault();
            }
        }

        private void BuildPlanReviewResponseSelectList()
        {
            _planReviewResponseSelectList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = "Accept", Value = ((int)PlanReviewResponseStatusEnum.Accept).ToString()},
                 new SelectListItem() { Text = "Reject", Value = ((int)PlanReviewResponseStatusEnum.Reject).ToString()}
            };
        }

        private List<SelectListItem> GenerateFacilitatorListViewItems()
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

        #endregion Private Methods
    }

}