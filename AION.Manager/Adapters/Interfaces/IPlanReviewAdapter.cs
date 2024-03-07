using AION.BL;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IPlanReviewAdapter
    {
        /// <summary>
        /// Gets plan review object for UI
        /// </summary>
        /// <param name="projectid">AION project identity int</param>
        /// <returns></returns>
        /// 
        List<PlanReview> GetPlanReviewsByProjectCycle(int projectCycleId);
        List<PlanReview> GetPlanReviewsByProjectId(string projectId);
        List<PlanReview> GetPlanReviewsByProjectId(int projectId);
        List<PlanReview> GetPlanReviewsByProject(ProjectBE project);

        List<ProjectCycleReview> GetProjectCycleReviews(int projectId);
        ProjectCycleSummary GetProjectCycleSummary(int projectId, ProjectBE projectBE = null);
        ProjectCycle GetProjectCycleById(int projectCycleId);
        List<ProjectCycle> GetProjectCyclesByProjectId(int projectId);
        List<ProjectCycleDetail> GetProjectCycleDetailsByProjectCycleId(int projectCycleId);
        List<PlanReviewScheduleDetail> GetPlanReviewScheduleDetailsByPlanReviewSchedule(int planReviewScheduleId);
        bool ScheduleFuturePRCycle(PlanReview planReview);
        bool UpdateProjectCycle(ProjectCycle projectCycle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planReview"></param>
        /// <returns></returns>
        bool UpdateAttendeeSchedules(int projectScheduleId, List<AttendeeInfo> attendees, int wkrId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planReview"></param>
        /// <returns></returns>
        bool UpsertPlanReview(PlanReview planReview);
        bool UpdateExpressDateRequest(RequestExpressDatesManagerModel model);
        /// <summary>
        /// updates the Project Dept Trades and Agencies, facilitator, notes, and sends email from the plan review object
        /// </summary>
        /// <param name="planReview"></param>
        /// <returns></returns>
        bool FinalizePlanReview(PlanReview planReview, DateTime? gateDt, DateTime? prevGateDt);
        string CreatePlanReviewAcceptanceEmail(PlanReviewEmailModel model);
    }
}
