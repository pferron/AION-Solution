using AION.BL;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Adapters;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Models
{
    public class ProjectCycleProcessor
    {
        public ProjectCycleBE CurrentCycleBE;
        public ProjectCycleBE FutureCycleBE;

        public ProjectCycleBE NewCycleBE;
        
        private ProjectEstimation _Project;
        private ProjectCycleBO _ProjectCycleBO;
        
        public List<ProjectCycleBE> ProjectCycles;

        public List<TaskReview> TaskReviews = new List<TaskReview>();

        public ProjectCycleProcessor(ProjectEstimation project)
        {
            _Project = project;
            _ProjectCycleBO = new ProjectCycleBO();

            GetCyclesForProject();
        }

        public void ProcessCycle()
        {
            if (FutureCycleBE != null)
            {
                FutureCycleBE.CurrentCycleInd = true;
                FutureCycleBE.FutureCycleInd = false;

                NewCycleBE = FutureCycleBE;
            }
            else
            {
                int newCycleNbr = 1;
                if (CurrentCycleBE != null)
                {
                    newCycleNbr = CurrentCycleBE.CycleNbr.Value + 1;
                }
                     
                ProjectCycleBE newCycle = new ProjectCycleBE();
                newCycle.ProjectId = _Project.ID;
                newCycle.CurrentCycleInd = true;
                newCycle.FutureCycleInd = false;
                newCycle.CycleNbr = newCycleNbr;
                newCycle.IsActive = true;
                newCycle.IsAprvInd = null;
                newCycle.IsCompleteInd = false;
                newCycle.PlansReadyOnDt = newCycleNbr > 1 ? (DateTime?)null : _Project.PlansReadyOnDate;  // sub cycles no PROD
                newCycle.ResponderUserId = null;
                newCycle.ResponseDt = null;
                newCycle.CreatedDate = DateTime.Now;
                newCycle.CreatedByWkrId = CurrentCycleBE == null ? "1" : CurrentCycleBE.CreatedByWkrId;
                newCycle.UpdatedDate = DateTime.Now;
                newCycle.UpdatedByWkrId = CurrentCycleBE == null ? "1" : CurrentCycleBE.UpdatedByWkrId;
                newCycle.UserId = "1";

                NewCycleBE = newCycle;
            }

            if (TaskReviews.Count > 0 && !TaskReviews.Any(x => x.EstimatedRereviewTime > 1))
            {
                NewCycleBE.GateDt = null;
                NewCycleBE.PlansReadyOnDt = null;
            }
            else
            {
                NewCycleBE.GateDt = _Project.GateDt;
            }

            if (CurrentCycleBE != null)
            {
                CurrentCycleBE.CurrentCycleInd = false;
                CurrentCycleBE.IsActive = false;
                CurrentCycleBE.IsCompleteInd = true;
                _ProjectCycleBO.Update(CurrentCycleBE);
            }

            if (FutureCycleBE != null)
            {
                _ProjectCycleBO.Update(NewCycleBE);
            }
            else
            {
                NewCycleBE.ProjectCycleId = _ProjectCycleBO.Create(NewCycleBE);
                CurrentCycleBE = NewCycleBE;
            }

            ProjectCycleDetailBO projectCycleDetailBO = new ProjectCycleDetailBO();
            ProjectCycleDetailBE projectCycleDetailBE = new ProjectCycleDetailBE();
            AccelaDepartmentBO accelaDepartmentBO = new AccelaDepartmentBO();

            List<ProjectCycleDetailBE> projectCycleDetails = new List<ProjectCycleDetailBE>();

            // add project cycle detail - when there are revisions required task reviews will be set
            foreach (TaskReview taskReview in TaskReviews)
            {
                int businessRefId = accelaDepartmentBO.MapAccelaDepartment(taskReview.Department);

                projectCycleDetailBE = new ProjectCycleDetailBE()
                {
                    ProjectCycleId = NewCycleBE.ProjectCycleId,
                    BusinessRefId = businessRefId,
                    RereviewHoursNbr = taskReview.EstimatedRereviewTime,
                    UserId = NewCycleBE.CreatedByWkrId
                };

                projectCycleDetailBO.Create(projectCycleDetailBE);

                projectCycleDetails.Add(projectCycleDetailBE);
            }
        }

        public void GetCyclesForProject()
        {
            ProjectCycles = _ProjectCycleBO.GetListByProject(_Project.ID);
            
            if (ProjectCycles.Count > 0)
            {
                CurrentCycleBE = ProjectCycles.FirstOrDefault(x => x.CurrentCycleInd == true);

                FutureCycleBE = ProjectCycles.FirstOrDefault(x => x.FutureCycleInd == true);
            }
        }
    }
}