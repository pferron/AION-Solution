using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AION.Manager.AccelaBusinessObjects
{
    public class ProcessPlanReviewCycleBO
    {
        public bool ProcessPlanReviewCycle(ProjectEstimation project, bool hasRevisions)
        {
            ProjectCycleProcessor cycleProcessor = new ProjectCycleProcessor(project);
            ProjectCycleBO projectCycleBO = new ProjectCycleBO();
            ProjectCycleModelBO projectCycleModelBO = new ProjectCycleModelBO();

            ProjectCycleBE currentCycle = cycleProcessor.CurrentCycleBE;
            
            // new project, first cycle
            if (project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Auto_Estimation_Pending
                || currentCycle == null)
            {
                cycleProcessor.ProcessCycle();
                currentCycle = cycleProcessor.CurrentCycleBE;
            }

            if (hasRevisions) // create cycle detail for revisions
            {
                if (currentCycle != null && currentCycle.CycleNbr.HasValue)
                {
                    int cycleNumber = currentCycle.CycleNbr.Value;

                    AccelaReReviewTaskBO accelaReReviewTaskBO = new AccelaReReviewTaskBO();

                    var reviewsTask = Task.Run(() => accelaReReviewTaskBO.GetReReviews(project.RecIdTxt, cycleNumber.ToString()));

                    reviewsTask.Wait();

                    if (!reviewsTask.IsCompleted || reviewsTask.IsCanceled || reviewsTask.IsFaulted)
                    {
                        return false;
                    }

                    cycleProcessor.TaskReviews = reviewsTask.Result;
                }
            }

            // existing project
            if (project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Scheduled || project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.PROD_Not_Known)
            {
                cycleProcessor.ProcessCycle();

                int userId = Convert.ToInt32(cycleProcessor.NewCycleBE.CreatedByWkrId);

                if (!project.IsFifo)
                {
                    //All pool subcycle LES-1334
                    if (hasRevisions && !cycleProcessor.TaskReviews.Any(x => x.EstimatedRereviewTime > 1))
                    {
                        // update project status to NOT SCHEDULED for all pool revisions
                        ProjectStatus projectstatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Not_Scheduled);
                        project.AIONProjectStatus = projectstatus;
                        project.AccelaProjectStatus = project.AIONProjectStatus.AccelaProjectStatus;

                        bool updateProjectStatusSuccess = ProjectModelBaseBO.UpdateProjectStatus(project);

                        // get cycle just processed
                        if (cycleProcessor.NewCycleBE.ProjectCycleId.HasValue)
                        {
                            ProjectCycleBE newCycle = projectCycleBO.GetById(cycleProcessor.NewCycleBE.ProjectCycleId.Value);
                            if (newCycle != null)
                            {
                                newCycle.PlansReadyOnDt = null;
                                newCycle.IsAprvInd = true;
                                newCycle.ResponderUserId = 1;
                                newCycle.ResponseDt = DateTime.Now;

                                new ProjectCycleBO().Update(newCycle);

                                ProjectCycle projectCycle = projectCycleModelBO.ConvertBEToModel(newCycle);

                                PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                                bool processAllPool = planReviewAdapter.ProcessAllPoolSubsequentCycle(projectCycle);
                            }
                        }
                    }
                    else
                    {
                        // update project status to PROD not known
                        EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                        project.UpdatedUser = new UserIdentity { ID = 1 };
                        project.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.PROD_Not_Known);
                        estimationCRUDAdapter.SaveEstimation(project);
                    }
                }
            }

            // get updated current cycle
            currentCycle = projectCycleBO.GetById(cycleProcessor.CurrentCycleBE.ProjectCycleId.Value);

            if (UpdateProjectCycleInformation(project.ID, currentCycle.CycleNbr.Value))
            {
                return true;
            }

            return false;
        }

        private bool UpdateProjectCycleInformation(int projectId, int cycleNumber)
        {
            ProjectBO projectBO = new ProjectBO();
            ProjectBE project = projectBO.GetById(projectId);
            project.CycleNbr = cycleNumber;
            int rows = projectBO.Update(project);

            return rows > 0 ? true : false;
        }
    }
}