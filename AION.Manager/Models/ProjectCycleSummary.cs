using AION.BL;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class ProjectCycleSummary
    {
        public List<ProjectCycleReview> ProjectCycleReviews { get; set; }
        public ProjectCycle ProjectCycleCurrent { get; set; }
        public ProjectCycle ProjectCycleFuture { get; set; }
        public ProjectCycle ProjectCyclePrevious { get; set; }
        public List<ProjectCycleDetail> ProjectCycleDetailsCurrent { get; set; }
        public List<ProjectCycleDetail> ProjectCycleDetailsFuture { get; set; }
        public List<ProjectCycleDetail> ProjectCycleDetailsPrevious { get; set; }
        public PlanReviewSchedule PlanReviewScheduleCurrent { get; set; }
        public PlanReviewSchedule PlanReviewScheduleFuture { get; set; }
        public PlanReviewSchedule PlanReviewSchedulePrevious { get; set; }
        public List<PlanReviewScheduleDetail> PlanReviewScheduleDetailsCurrent { get; set; }
        public List<PlanReviewScheduleDetail> PlanReviewScheduleDetailsFuture { get; set; }
        public List<PlanReviewScheduleDetail> PlanReviewScheduleDetailsPrevious { get; set; }
        public List<PlanReview> PlanReviews { get; set; }
        public PlanReview PlanReviewCurrent { get; set; }
        public PlanReview PlanReviewFuture { get; set; }
        public PlanReview PlanReviewPrevious { get; set; }
    }

}