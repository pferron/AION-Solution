using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class ProjectDetailModel
    {
        public ProjectEstimation ProjectEstimation { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<ProjectAudit> ProjectAudits { get; set; } = new List<ProjectAudit>();
        public List<AuditActionRef> AuditActionRefs { get; set; } = new List<AuditActionRef>();
        public List<Facilitator> Facilitators { get; set; } = new List<Facilitator>();
        public List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();
        public List<Meeting> ScheduledMeetings { get; set; } = new List<Meeting>();
        public List<ProjectNotificationEmail> ProjectNotificationEmails { get; set; } = new List<ProjectNotificationEmail>();
        public List<ProjectCycleReview> ProjectCycleReviews { get; set; } = new List<ProjectCycleReview>();
        public List<PlanReview> PlanReviews { get; set; } = new List<PlanReview>();
        public UserIdentity Facilitator { get; set; }
    }
}