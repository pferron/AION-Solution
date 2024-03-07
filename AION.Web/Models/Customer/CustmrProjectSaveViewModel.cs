using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.Web.Models
{
    public class CustmrProjectSaveViewModel : ViewModelBase
    {
        public ProjectEstimation Project { get; set; }
        public int AIONProjectStatusID { get; set; }
        public List<CustmrResponseNote> Notes { get; set; }
        public List<CustmrResponseNote> PendingEstimationNotes { get; set; }
        public List<CustmrResponseNote> ExitMeetingNotes { get; set; }
        public List<CustmrResponseNote> MeetingDocNotes { get; set; }
        public List<Note> CreatedNotes { get; set; }
        public string InitMode { get; set; }

        public string ApptResponseStatusSelected { get; set; }

        public PlanReview PlanReview { get; set; }
        public PlanReview FuturePlanReview { get; set; }
        public ExpressMeetingAppointment EMA { get; set; }
        public int? PRResponse { get; set; }
        public int? FuturePRResponse { get; set; }
        public int? EMAResponse { get; set; }
        public DateTime? PRProdDate { get; set; }
        public DateTime? FuturePRProdDate { get; set; }
        public DateTime? EMAProdDate { get; set; }
        public int Cycle { get; set; }
        public bool ProdNotKnown { get; set; }
        public bool IsAbort { get; set; }

        public string PrelimId { get; set; }
        public string AcceptedMeetings { get; set; }

        public List<PreliminaryMeetingAppointment> PreliminaryStatus { get; set; }

        public ProjectCycle ProjectCycle { get; set; }
    }
}