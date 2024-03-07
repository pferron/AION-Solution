using AION.BL;
using AION.BL.Models;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;

namespace AION.Manager.Engines.SyncAccela
{
    public interface IAccelaSyncEngine
    {
        bool SaveAccelaProject(AccelaProjectModel project, AIONQueueRecordBE be);
        bool UpdateProjectStatus(ProjectEstimation project, ProjectStatusEnum projectStatusEnum);
        void PerformProjectAutoEstimation(AccelaProjectModel project);
        bool ProcessAwaitingPlans(ProjectEstimation projectEstimation);
        bool ProcessPlansReceived(ProjectEstimation projectEstimation);
        bool ProcessInReview(ProjectEstimation projectEstimation);
        bool ProcessRevisionsRequired(ProjectEstimation projectEstimation);
        bool ProcessPlanReviewCycle(ProjectEstimation projectEstimation, bool hasRevisions);
        bool ValidateSyncAccelaAIONDepartments(ProjectEstimation projectEstimation);
        bool ValidateSyncAccelaAIONCycleInfo(ProjectEstimation projectEstimation);
    }
}
