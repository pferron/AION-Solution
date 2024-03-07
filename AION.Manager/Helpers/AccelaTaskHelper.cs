using AION.Accela.Engine.BusinessObjects;
using Meck.Shared;
using Meck.Shared.Accela.ParserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AION.Manager.Helpers
{
    public class AccelaTaskHelper
    {

        public static AccelaTableResult GetAccelaWorkFlowTaskHistory(string recordId)
        {
            AccelaApiBO accelaApiBO = new AccelaApiBO();

            var workFlowHistoryTask = Task.Run(() => accelaApiBO.GetAccelaRecordWorkFlowTask(recordId));

            workFlowHistoryTask.Wait();

            if (workFlowHistoryTask.IsCompleted && !workFlowHistoryTask.IsCanceled && !workFlowHistoryTask.IsFaulted)
            {
                AccelaTableResult workFlowHistory = workFlowHistoryTask.Result;
                return workFlowHistory;
            }

            return null;
        }

        public static string GetAccelaWorkFlowTaskHistoryJson(string recordId)
        {
            AccelaApiBO accelaApiBO = new AccelaApiBO();

            var workFlowHistoryTask = Task.Run(() => accelaApiBO.GetAccelaRecordWorkFlowTaskJson(recordId));

            workFlowHistoryTask.Wait();

            if (workFlowHistoryTask.IsCompleted && !workFlowHistoryTask.IsCanceled && !workFlowHistoryTask.IsFaulted)
            {
                return workFlowHistoryTask.Result;
            }

            return null;
        }

        public static List<TaskReview> GetApplicableTaskInfoForHistoryRecords(AccelaTableResult workFlowHistory)
        {
            string naStatus = "Not Applicable";

            Rows historyItems = (Rows)workFlowHistory.rows[0];
            List<TaskReview> tasks = new List<TaskReview>();

            for (int i = 0; i < historyItems.fields.Count; i++)
            {
                Dictionary<string, object> tableFields = (Dictionary<string, object>)historyItems.fields[i];

                string descriptionName = string.Empty;
                string hoursSpent = string.Empty;
                string isCompleted = string.Empty;

                bool hasStatusField = tableFields.TryGetValue("status", out object statusObj);
                bool hasDescription = tableFields.TryGetValue("description", out object descriptionObj);
                bool hasHoursSpent = tableFields.TryGetValue("hoursSpent", out object hoursSpentObj);
                bool hasIsCompleted = tableFields.TryGetValue("isCompleted", out object isCompletedObj);
                if (hasIsCompleted)
                {
                    isCompleted = Convert.ToString(isCompletedObj);
                }
                if (hasHoursSpent)
                {
                    hoursSpent = Convert.ToString(hoursSpentObj);
                }
                if (hasDescription)
                {
                    descriptionName = Convert.ToString(descriptionObj);
                }

                if (hasStatusField)
                {
                    Dictionary<string, object> status = (Dictionary<string, object>)statusObj;
                    status.TryGetValue("text", out object statusText);
                    if (Convert.ToString(statusText) != naStatus)
                    {
                        string taskId = tableFields["id"].ToString();

                        tasks.Add(new TaskReview()
                        {
                            Department = descriptionName,
                            TaskId = taskId,
                            HoursSpent = hoursSpent,
                            IsCompleted = isCompleted

                        });
                    }
                }
            }

            return tasks;
        }

        public static TaskReview GetIndividualTaskRecord(string recordId, string taskId, string cycleNumber)
        {
            AccelaApiBO accelaApiBO = new AccelaApiBO();

            TaskInfo taskInfo = new TaskInfo
            {
                TaskId = taskId,

            };
            var workTaskReviewTask = Task.Run(() => accelaApiBO.GetRecordWorkFlowTasksWorkFlowTaskInfo(recordId, taskInfo));

            workTaskReviewTask.Wait();

            if (workTaskReviewTask.IsCompleted && !workTaskReviewTask.IsCanceled && !workTaskReviewTask.IsFaulted)
            {
                TaskReview taskReview = workTaskReviewTask.Result;
                if (taskReview.CycleNumber == cycleNumber)
                {
                    return taskReview;
                }
            }

            return null;
        }

        public static WorkTaskCustForms GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(string recordId, string workflowTaskId)
        {
            AccelaApiBO accelaApiBO = new AccelaApiBO();

            var workflowTask = Task.Run(() => accelaApiBO.GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(recordId, workflowTaskId));

            workflowTask.Wait();

            if (workflowTask.IsCompleted && !workflowTask.IsCanceled && !workflowTask.IsFaulted)
            {
                WorkTaskCustForms workTaskCustForms = workflowTask.Result;

                return workTaskCustForms;
            }

            return null;
        }
    }
}