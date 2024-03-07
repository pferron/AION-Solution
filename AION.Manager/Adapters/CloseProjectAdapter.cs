using AION.Accela.Engine;
using AION.Base;
using AION.Base.Services;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.MAION.Manager.Adapters;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Helpers;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.Accela.ParserModels;
using Posse.Accela.Engine.RecordParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AION.Manager.Adapters
{
    public class CloseProjectAdapter : BaseAdapter, IDataContextCloseProject
    {
        IAccelaEngine _accelaEngine;
        IDataContextProjectCycleBO _dataContextProjectCycleBE;
        IDataContextProjectBusinessRelationshipBO _dataContextProjectBusinessRelationshipBO;

        public CloseProjectAdapter(
            IAccelaEngine accelaEngine, 
            IDataContextProjectCycleBO dataContextProjectCycleBE,
            IDataContextProjectBusinessRelationshipBO dataContextProjectBusinessRelationshipBO) 
        {
            _accelaEngine = accelaEngine;
            _dataContextProjectCycleBE = dataContextProjectCycleBE;
            _dataContextProjectBusinessRelationshipBO = dataContextProjectBusinessRelationshipBO;
        }

        public bool Process(ProjectEstimation project)
        {
            bool closeCycles = CloseProjectCycles(project);

            List<ProjectBusinessRelationshipBE> projectBusinessRelationships = GetProjectBusinessRelationships(project.ID);

            List<TaskReview> taskReviews = GetAccelaTaskInfo(project.RecIdTxt);

            bool updateHours = UpdateProjectActualHours(projectBusinessRelationships, taskReviews);

            if (closeCycles && updateHours)
            {
                return true;
            }

            return false;
        }

        public bool CloseProjectCycles(ProjectEstimation project)
        {
            bool success = false;

            try
            {
                ProjectCycleBO projectCycleBO = new ProjectCycleBO();

                List<ProjectCycleBE> incompleteProjectCycles = _dataContextProjectCycleBE.GetListByProject(project.ID);

                foreach (ProjectCycleBE be in incompleteProjectCycles)
                {
                    be.IsActive = false;
                    be.IsCompleteInd = true;
                    be.CurrentCycleInd = false;
                    be.FutureCycleInd = false;

                    projectCycleBO.Update(be);
                }

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CloseProjectAdapter - CloseProjectCycles " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }

        public bool UpdateProjectActualHours(List<ProjectBusinessRelationshipBE> projectBusinessRelationships, List<TaskReview> taskReviews)
        {
            bool success = false;

            try
            {
                
                AccelaDepartmentBO accelaDepartmentBO = new AccelaDepartmentBO();

                foreach (TaskReview taskReview in taskReviews)
                {
                    int businessRefId = accelaDepartmentBO.MapAccelaDepartment(taskReview.Department);

                    if (businessRefId > -1)
                    {
                        if (taskReview.IsCompleted == "Y" && taskReview.CycleNumber == "1")
                        {
                            var projectBusinessRelationship = projectBusinessRelationships.FirstOrDefault(x => x.BusinessRefId == businessRefId);
                            if (projectBusinessRelationship != null)
                            {
                                projectBusinessRelationship.ActualHoursNbr = decimal.Parse(taskReview.HoursSpent);
                                _dataContextProjectBusinessRelationshipBO.Update(projectBusinessRelationship);
                            }
                        }
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CloseProjectAdapter - UpdateProjectActualHours " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }

        #region Private Methods
        private List<ProjectBusinessRelationshipBE> GetProjectBusinessRelationships(int projectId)
        {
            return _dataContextProjectBusinessRelationshipBO.GetListByProjectId(projectId);
        }

        private List<TaskReview> GetAccelaTaskInfo(string recordId)
        {
            Task<string> task = Task.Run<string>(async () =>
                    await _accelaEngine.GetAccelaWorkFlowHistoryforRecord(recordId));

            task.Wait();

            var tableresult = task.Result;

            AccelaRecordParser mAccelaRecordParser = new AccelaRecordParser();

            AccelaTableResult accelaTableResult = mAccelaRecordParser.CustomFormsLoadAndProcess(tableresult);

            return AccelaTaskHelper.GetApplicableTaskInfoForHistoryRecords(accelaTableResult);
        }
        #endregion
    }
}