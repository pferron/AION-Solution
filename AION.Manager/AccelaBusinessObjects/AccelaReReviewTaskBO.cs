using AION.Accela.Engine.BusinessObjects;
using AION.Manager.Helpers;
using Meck.Shared;
using Meck.Shared.Accela.ParserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaReReviewTaskBO : AccelaBusinessObjectBase
    {
        private const string taskStatus = "Revisions Required";

        public List<TaskReview> GetReReviews(string recordId, string cycleNumber)
        {
            AccelaTableResult workFlowHistory = AccelaTaskHelper.GetAccelaWorkFlowTaskHistory(recordId);
            List<TaskInfo> tasks = GetTaskInfoForHistoryRecords(workFlowHistory, taskStatus);
            return GetIndividualTaskRecords(recordId, tasks, cycleNumber);
        }
    }
}