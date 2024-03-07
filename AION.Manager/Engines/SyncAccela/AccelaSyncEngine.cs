using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Controller;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Engines.SyncAccela
{

    public class AccelaSyncEngine : BaseAdapter, IAccelaSyncEngine
    {
        private EstimationCRUDAdapter _estimationCRUDAdapter;

        public bool SaveAccelaProject(AccelaProjectModel project, AIONQueueRecordBE be)
        {
            try
            {
                _estimationCRUDAdapter = new EstimationCRUDAdapter();
                UserIdentity systemUser = new UserIdentityModelBO().GetInstance(1);
                //get details from AION which is required to save
                ProjectEstimation aionProjectModel = _estimationCRUDAdapter.GetProjectDetailsForEstimation(project);
                aionProjectModel.IsAccelaUpdate = true;

                if (!_estimationCRUDAdapter.UpdateAccelaProjectPreliminaryMeetingStatus(project)) return false;

                if (aionProjectModel.ID == 0)
                {
                    ProjectStatus projectstatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.NA);
                    aionProjectModel.AIONProjectStatus = projectstatus;
                    aionProjectModel.AccelaProjectStatus = new AccelaProjectStatusBO(
                        aionProjectModel.AionPropertyType, aionProjectModel.PlansReadyOnDate,
                        aionProjectModel.AIONProjectStatus).GetPrettyAccelaStatus(
                        be.WORKFLOW_TASK_STATUS,
                            be.STATUS_DESC
                        );

                    //get system user
                    aionProjectModel.UpdatedUser = systemUser;
                    aionProjectModel.CreatedUser = systemUser;
                    foreach (var trade in aionProjectModel.Trades)
                    {
                        trade.CreatedUser = systemUser;
                        trade.UpdatedUser = systemUser;
                    }
                    foreach (var agency in aionProjectModel.Agencies)
                    {
                        agency.CreatedUser = systemUser;
                        agency.UpdatedUser = systemUser;
                    }
                    _estimationCRUDAdapter.SaveProjectEstimationDetails(aionProjectModel);

                    PerformProjectAutoEstimation(project);
                }
                else
                {
                    // This is an existing project. Update with incoming Accela data if estimation process is not complete.
                    AccelaProjectStatusBO accelaProjectStatusBO = new AccelaProjectStatusBO(
                        aionProjectModel.AionPropertyType, aionProjectModel.PlansReadyOnDate,
                        aionProjectModel.AIONProjectStatus);

                    aionProjectModel.AIONProjectStatus =
                        accelaProjectStatusBO.GetCurrentProjectStatusFromAccelaStatus
                        (
                            be.WORKFLOW_TASK_STATUS,
                            be.STATUS_DESC
                        );
                    aionProjectModel.AccelaProjectStatus = accelaProjectStatusBO.GetPrettyAccelaStatus
                        (
                            be.WORKFLOW_TASK_STATUS,
                            be.STATUS_DESC
                        );
                    ProjectStatusEnum projectStatus = aionProjectModel.AIONProjectStatus.ProjectStatusEnum;
                    //Check Express swap
                    if (project.IsExpress ^ aionProjectModel.AionPropertyType == PropertyTypeEnums.Express)
                    {
                        aionProjectModel = _estimationCRUDAdapter.SwapEstimationExpressStatus(aionProjectModel, project.IsExpress);
                    }
                    aionProjectModel.UpdatedUser = new UserIdentityModelBO().GetInstance(1);

                    ProjectEstimationModelBO projectBO = new ProjectEstimationModelBO();
                    //LES-1261 - Abort Package status will cancel current plan review, remove scheduling, and clear PROD
                    if (projectStatus == ProjectStatusEnum.Abort_Package)
                    {
                        projectBO.AbortProject(aionProjectModel);

                        aionProjectModel.PlansReadyOnDate = null;

                        //update project cycle plans ready on date to null
                        PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                        ProjectCycle projectCycle = planReviewAdapter.GetProjectCyclesByProjectId(aionProjectModel.ID).FirstOrDefault(x => x.CurrentCycleInd == true);
                        projectCycle.PlansReadyOnDt = null;
                        planReviewAdapter.UpdateProjectCycle(projectCycle);
                    }

                    //Save the project changes
                    if (projectBO.UpdateEstimationProjectDetailsFromAccela(aionProjectModel) == false)
                    {
                        return false;
                    }

                    //Cancel
                    if (projectStatus == ProjectStatusEnum.Cancelled)
                    {
                        projectBO.CancelProject(aionProjectModel);
                    }

                    //Close Success
                    {
                        if (projectStatus == ProjectStatusEnum.Complete)
                        {
                            IDataContextProjectCycleBO dataContextProjectCycleBO = new ProjectCycleBO();
                            IAccelaEngine accelaEngine = new AccelaApiBO();
                            IDataContextProjectBusinessRelationshipBO dataContextProjectBusinessRelationshipBO = new ProjectBusinessRelationshipBO();
                            
                            CloseProjectAdapter closeProjectAdapter = 
                                new CloseProjectAdapter(accelaEngine, dataContextProjectCycleBO, dataContextProjectBusinessRelationshipBO);
                            
                            bool success = closeProjectAdapter.Process(aionProjectModel);
                        }
                    }
                }
                //update Accela status for project id - after save, project id is added to the model
                new ProjectAuditModelBO().InsertProjectAudit(aionProjectModel.ID, aionProjectModel.AccelaProjectStatus, "1", AuditActionEnum.Accela_Status);

                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in SaveAccelaProject - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        public bool UpdateProjectStatus(ProjectEstimation project, ProjectStatusEnum projectStatusEnum)
        {
            ProjectStatus projectstatus = new ProjectStatusModelBO().GetInstance(projectStatusEnum);
            project.AIONProjectStatus = projectstatus;
            project.AccelaProjectStatus = project.AIONProjectStatus.AccelaProjectStatus;

            bool success = ProjectModelBaseBO.UpdateProjectStatus(project);

            return success;
        }

        public void PerformProjectAutoEstimation(AccelaProjectModel project)
        {
            //Execute Preliminary Estimation

            PreliminaryProjectEstimationEngine preliminaryProjectEstimationEngine = new PreliminaryProjectEstimationEngine();
            ProjectAutoEstimationEngine projectAutoEstimationEngine = new ProjectAutoEstimationEngine();
            ProjectParms projectParms = new ProjectParms()
            {
                ProjectId = project.ProjectNumber
            };

            ProjectEstimation projectEstimation =
                project.IsPreliminaryMeetingRequested ? preliminaryProjectEstimationEngine.ExecutePreliminaryProjectEstimation(projectParms)
                : projectAutoEstimationEngine.ExecuteProjectEstimation(projectParms, false);
        }

        public bool ProcessAwaitingPlans(ProjectEstimation project)
        {
            bool updateProjectStatus = UpdateProjectStatus(project, ProjectStatusEnum.Auto_Estimation_Pending);

            if (updateProjectStatus)
            {
                return ProcessPlanReviewCycle(project, false);
            }
            else
            {
                return false;
            }
        }

        public bool ProcessInReview(ProjectEstimation project)
        {
            return true;
        }

        public bool ProcessPlansReceived(ProjectEstimation project)
        {
            return true;
        }

        public virtual bool ProcessRevisionsRequired(ProjectEstimation project)
        {
            return ProcessPlanReviewCycle(project, true);
        }

        public bool ProcessPlanReviewCycle(ProjectEstimation projectEstimation, bool hasRevisions)
        {
            ProcessPlanReviewCycleBO processPlanReviewCycle = new ProcessPlanReviewCycleBO();

            return processPlanReviewCycle.ProcessPlanReviewCycle(projectEstimation, hasRevisions);
        }

        public bool ValidateSyncAccelaAIONDepartments(ProjectEstimation projectEstimation)
        {
            bool hasValidDepartments = true;

            foreach (var trade in projectEstimation.Trades)
            {
                if (trade.ID == -1)
                {
                    hasValidDepartments = false;
                }
            }

            foreach (var agency in projectEstimation.Agencies)
            {
                if (agency.ID == -1)
                {
                    hasValidDepartments = false;
                }
            }

            return hasValidDepartments;
        }

        public bool ValidateSyncAccelaAIONCycleInfo(ProjectEstimation projectEstimation)
        {
            bool hasCycleInformation = true;

            ProjectCycleBO projectCycleBO = new ProjectCycleBO();
            
            List<ProjectCycleBE> projectCycleBEs = projectCycleBO.GetListByProject(projectEstimation.ID);

            if (projectCycleBEs.Count == 0)
            {
                hasCycleInformation = false;
            }

            return hasCycleInformation;
        }
    }
}