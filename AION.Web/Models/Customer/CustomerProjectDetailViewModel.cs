using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class CustomerProjectDetailViewModel : ViewModelBase
    {
        private List<CustmrResponseNote> _notes;
        private List<CustmrResponseNote> _pendingEstimationNotes;
        private List<SelectListItem> _apptResponseSelectList;
        private List<CustmrResponseNote> _exitMeetingNotes;
        private List<CustmrResponseNote> _meetingDocNotes;
        private List<ProjectAudit> _projectAudits;
        private List<AuditActionRef> _auditActionRefs;
        private List<PreliminaryMeetingAppointment> _prelimStatus;
        public CustomerProjectDetailViewModel()
        {
            LoggedInUser = new UserIdentity();
            ScheduledNotes = new List<Note>();
            PRResponse = new int?();
            FuturePRResponse = new int?();
            EMAResponse = new int?();

        }

        public string AssignedFacilitator { get; set; }

        public CustExpressMeeting CustExpressMeetingDetails { get; set; }
        public List<ProjectCycleReview> ProjectCycleReviews { get; set; } = new List<ProjectCycleReview>();
        public List<PlanReview> PlanReviews { get; set; }
        public PlanReview PlanReview { get; set; }
        public PlanReview FuturePlanReview { get; set; }
        public int PlanReviewExpressId { get; set; }
        public ExpressMeetingAppointment EMA { get; set; }
        public DateTime PlanReviewDate { get; set; }
        public int? PRResponse { get; set; }
        public int? FuturePRResponse { get; set; }
        public int? EMAResponse { get; set; }
        public DateTime? PRProdDate { get; set; }
        public int Cycle { get; set; }
        public bool ProdNotKnown { get; set; }
        public bool IsAbort { get; set; }
        public List<Note> ScheduledNotes { get; set; }
        public string PlanReviewAcceptanceEmail { get; set; }
        public string AcceptedMeetings { get; set; }
        public List<CustmrResponseNote> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                if (_notes != null)
                    BuildNotesList();
            }
        }
        public List<CustmrResponseNote> PendingEstimationNotes
        {
            get { return _pendingEstimationNotes; }
            set { _pendingEstimationNotes = value; }
        }
        public List<CustmrResponseNote> ExitMeetingNotes
        {
            get { return _exitMeetingNotes; }
            set { _exitMeetingNotes = value; }
        }
        public List<CustmrResponseNote> MeetingDocNotes
        {
            get { return _meetingDocNotes; }
            set { _meetingDocNotes = value; }
        }

        public List<PreliminaryMeetingAppointment> PreliminaryStatus
        {
            get { return _prelimStatus; }
            set { _prelimStatus = value; }
        }

        public string InitMode { get; set; }

        public AppointmentResponseStatusEnum ApptResponseStatusSelectedEnum { get; set; }
        public string ApptResponseStatusSelected { get; set; }

        public int MeetingId { get; set; }


        public string StatusMessage { get; set; }
        public List<ProjectAudit> ProjectAudits
        {
            get { return _projectAudits; }
            set
            {
                _projectAudits = value;
                if (value != null)
                    SetAuditActionRef();
            }
        }
        public List<AuditActionRef> AuditActionRefs
        {
            get
            {
                return _auditActionRefs;
            }

            set
            {
                _auditActionRefs = value;
            }

        }

        public List<SelectListItem> ApptResponseSelectList
        {
            get
            {
                if (_apptResponseSelectList == null) BuildApptResponseSelectList();
                return _apptResponseSelectList;
            }
        }

        public bool IsCustomerResponseAllowed
        {
            get
            {
                return CanCustomerRespondToMeeting();
            }
        }

        private bool CanCustomerRespondToMeeting()
        {
            return ApptResponseStatusSelectedEnum != AppointmentResponseStatusEnum.Not_Scheduled ? true : false;
        }

        public bool HasPlanReviews { get; set; }

        public List<PlanReviewPartialViewModel> PlanReviewViewModels { get; set; }

        #region "Private Methods"
        private void BuildNotesList()
        {
            _pendingEstimationNotes = GetParentChild(true, _notes, NoteTypeEnum.PendingNotes);
            _exitMeetingNotes = GetParentChild(true, _notes, NoteTypeEnum.ExitMeetingNotes);
            _meetingDocNotes = GetParentChild(true, _notes, NoteTypeEnum.MeetingDocNotes);

            BuildNote(_pendingEstimationNotes, NoteTypeEnum.PendingNotes);
            BuildNote(_exitMeetingNotes, NoteTypeEnum.ExitMeetingNotes);
            BuildNote(_meetingDocNotes, NoteTypeEnum.MeetingDocNotes);

        }
        private List<CustmrResponseNote> GetParentChild(bool parent, List<CustmrResponseNote> notes, NoteTypeEnum notetype)
        {
            if (parent)
            {
                return notes.Where(x => x.ParentNoteID == 0 && x.NotesType.Type == notetype).ToList();
            }
            else
            {
                return notes.Where(x => x.ParentNoteID > 0 && x.NotesType.Type == notetype).ToList();
            }
        }
        private void BuildNote(List<CustmrResponseNote> parentnotes, NoteTypeEnum notetype)
        {
            //get the responses, if any
            List<CustmrResponseNote> childnotes = GetParentChild(false, _notes, notetype);
            foreach (CustmrResponseNote note in parentnotes)
            {
                if (childnotes.Where(x => x.ParentNoteID == note.ID).Any())
                {
                    note.CustmrCanRespond = false;
                    note.CustmrResponseComment = childnotes.Where(x => x.ParentNoteID == note.ID).FirstOrDefault().NotesComments;
                }
                else
                {
                    note.CustmrCanRespond = true;
                }
            }
        }
        private void SetAuditActionRef()
        {
            foreach (ProjectAudit projectaudit in _projectAudits)
            {
                projectaudit.AuditActionRef = _auditActionRefs.Where(x => x.AuditActionRefId == projectaudit.AuditActionRefId).FirstOrDefault();
            }
        }

        private void BuildApptResponseSelectList()
        {
            _apptResponseSelectList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = "Accept", Value = ((int)AppointmentResponseStatusEnum.Accept).ToString()},
                 new SelectListItem() { Text = "Reject", Value = ((int)AppointmentResponseStatusEnum.Reject).ToString()},
                 new SelectListItem() { Text = "Self Schedule", Value = ((int)AppointmentResponseStatusEnum.Self_Schedule).ToString()},
                 new SelectListItem() { Text = "No Reply", Value = ((int)AppointmentResponseStatusEnum.No_Reply).ToString()}
            };
        }
        #endregion "Private Methods"
    }
    public class CustmrResponseNote : Note
    {
        public string CustmrResponseComment { get; set; }

        public bool CustmrCanRespond { get; set; }

    }
}