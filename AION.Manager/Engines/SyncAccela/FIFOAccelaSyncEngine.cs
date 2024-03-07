using AION.BL;
using AION.BL.Adapters;
using AION.BL.Common;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Adapters;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AION.Manager.Engines.SyncAccela
{
    public class FIFOAccelaSyncEngine : AccelaSyncEngine, IAccelaSyncEngine
    {
        public new bool ProcessAwaitingPlans(ProjectEstimation projectEstimation)
        {
           return UpdateProjectStatus(projectEstimation, ProjectStatusEnum.NA);
        }

        public new bool ProcessPlansReceived(ProjectEstimation projectEstimation)
        {
            bool plansReceivedSuccess = false;

            if (!projectEstimation.CycleNbr.HasValue || projectEstimation.CycleNbr == 0) // ready for first cycle
            {
                bool updateProjectStatusSuccess = UpdateProjectStatus(projectEstimation, ProjectStatusEnum.Auto_Estimation_Pending);

                if (updateProjectStatusSuccess)
                {
                    plansReceivedSuccess = ProcessPlanReviewCycle(projectEstimation, false);
                }
            }
            else
            {
                bool incrementCycle = AnalyzeCurrentCycle(projectEstimation);
                
                if (incrementCycle)
                {
                    plansReceivedSuccess = ProcessFIFOCycle(projectEstimation, incrementCycle);
                }
                else
                {
                    plansReceivedSuccess = true;
                }
            }

            return plansReceivedSuccess;

        }

        public new bool ProcessInReview(ProjectEstimation projectEstimation)
        {
            bool inReviewSuccess = false;

            if (projectEstimation.CycleNbr != null)
            {
                inReviewSuccess = ProcessFifoDueDate(projectEstimation.RecIdTxt, projectEstimation.AccelaProjectRefId, projectEstimation.CycleNbr.Value);
            }

            return inReviewSuccess;
        }
 
        public override bool ProcessRevisionsRequired(ProjectEstimation projectEstimation)
        {
            ManageCycleSwitch(projectEstimation, true);

            return true;
        }

        public bool ProcessFIFOCycle(ProjectEstimation projectEstimation, bool hasRevisions)
        {
            bool processCycle = ProcessPlanReviewCycle(projectEstimation, hasRevisions);

            ProjectBO projectBO = new ProjectBO();

            // get updated cycle from BO
            ProjectBE projectBE = projectBO.GetById(projectEstimation.ID);
            projectEstimation.CycleNbr = projectBE.CycleNbr;

            if (projectBE.CycleNbr.Value > 1)
            {
                DateTime newFifoDueDate = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 7);
                projectEstimation.FifoDueDt = newFifoDueDate;
                projectBE.FifoDueDt = newFifoDueDate;
                projectBO.Update(projectBE);
            }

            var fifoProcess = Task.Run(() => new FIFOAdapter().ProcessFIFO(projectEstimation));

            fifoProcess.Wait();

            if (!fifoProcess.IsCompleted || fifoProcess.IsCanceled || fifoProcess.IsFaulted)
            {
                return false;
            }

            ManageCycleSwitch(projectEstimation, false);

            return true;
        }

        public bool ProcessFifoDueDate(string recordId, string projectNumber, int cycleNumber)
        {
            AccelaFifoDueDateBO processFifoDueDate = new AccelaFifoDueDateBO();
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

            var accelaRecordModel = estimationAccelaAdapter.GetProjectDetails(new ProjectParms { ProjectId = recordId, RecIdTxt = recordId });

            var mapresult = estimationAccelaAdapter.GetAccelaAIONMapByAccelaRecordType(accelaRecordModel.result[0].ParseRecType);
            mapresult.Wait();
            var mAccelaAONMap = mapresult.Result;

            DateTime? fifoDueDate = processFifoDueDate.GetFifoDueDateFromCustomTable(accelaRecordModel, mAccelaAONMap, cycleNumber);

            if (fifoDueDate != null)
            {
                ProjectBO projectBO = new ProjectBO();
                ProjectBE projectBE = new ProjectBO().GetByExternalRefInfo(projectNumber);
                projectBE.FifoDueAccelaDt = fifoDueDate;
                projectBO.Update(projectBE);

                return true;
            }

            return false;
        }

        private bool AnalyzeCurrentCycle(ProjectEstimation projectEstimation)
        {
            bool incrementCycle = true;

            ProjectCycleBE currentCycle = GetCurrentCycle(projectEstimation.ID);
            
            // do not process a new cycle for the new plans received

            if (currentCycle != null)
            {
                switch (currentCycle.IncrementOnPlansReceivedInd)
                {
                    case null:
                    case false:
                        incrementCycle = false;
                        break;
                    case true:
                        incrementCycle = true;
                        break;
                }
            }

            return incrementCycle;
        }

        private void ManageCycleSwitch(ProjectEstimation projectEstimation, bool increment)
        {
            ProjectCycleBE projectCycleBE = GetCurrentCycle(projectEstimation.ID);

            projectCycleBE.IncrementOnPlansReceivedInd = increment;

            new ProjectCycleBO().Update(projectCycleBE);
        }

        private ProjectCycleBE GetCurrentCycle(int projectId)
        {
            List<ProjectCycleBE> projectCycles = new ProjectCycleBO().GetListByProject(projectId);

            return projectCycles.FirstOrDefault(x => x.CurrentCycleInd == true);
        }
    }
}