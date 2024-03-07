using AION.BL;
using AION.BL.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class SchedulingModel
    {
        public List<DateTime> Holidays { get; set; } = new List<DateTime>();
        public ProjectEstimation ProjectEstimation { get; set; }
        public ProjectCycleSummary ProjectCycleSummary { get; set; }
        public List<Facilitator> Facilitators { get; set; } = new List<Facilitator>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<ReserveExpressReservation> ReserveExpressReservations { get; set; } = new List<ReserveExpressReservation>();
        public List<StandardNote> MandatoryNotes { get; set; } = new List<StandardNote>();
        public List<StandardNote> StandardNotes { get; set; } = new List<StandardNote>();
        public List<StandardNoteGroupEnums> StandardNoteGroupEnums { get; set; } = new List<StandardNoteGroupEnums>();
        public List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();
        public List<Reviewer> Reviewers { get; set; } = new List<Reviewer>();
        public List<Reviewer> FireAgencyReviewers { get; set; } = new List<Reviewer>();
        public List<Reviewer> ZoningJurisdictionReviewers { get; set; } = new List<Reviewer>();
        public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
        public List<UserIdentity> Users { get; set; } = new List<UserIdentity>();
        public List<FacilitatorMeetingAppointment> FacilitatorMeetingAppointments { get; set; } = new List<FacilitatorMeetingAppointment>();
        public PreliminaryMeetingAppointment PreliminaryMeetingAppointment { get; set; }
    }
}