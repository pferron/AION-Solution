using AION.Accela.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaBusinessObjectBase
    {
        public Logger Logger { get; set; } = new Logger();

        public StringBuilder sbAionMapTrap { get; set; }

        /// <summary>
        ///  SetValueUpdate   used to update the primary object to complete mapping
        /// </summary>
        /// <param name="AIONObjectItem"></param>
        /// <param name="target"></param>
        /// <param name="AionDataFieldType"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public PropertyInfo SetValueUpdate(PropertyInfo AIONObjectItem, Object target, string AionDataFieldType, object newValue)
        {
            try
            {
                switch (AionDataFieldType.ToUpper())
                {
                    case "STRING":
                        List<string> sstring = new List<string>();

                        if (newValue.GetType().ToString().ToUpper().Contains("DICTIONARY"))
                        {

                            foreach (var newelement in (List<Dictionary<string, object>>)newValue)
                            {
                                foreach (var newitem in newelement)
                                {
                                    sstring.Add(newitem.Key + ":" + newitem.Value);
                                }
                            }

                            AIONObjectItem.SetValue(target, string.Join(";", sstring));
                        }
                        else
                            AIONObjectItem.SetValue(target, newValue.ToString());
                        break;
                    case "BOOL":
                        if (((string)newValue).ToUpper() == "YES" || ((string)newValue).ToUpper() == "Y")
                        {
                            AIONObjectItem.SetValue(target, true);
                        }
                        else
                        {
                            AIONObjectItem.SetValue(target, false);
                        }

                        break;
                    case "DATETIME":
                        if ((string)newValue == "") break;
                        AIONObjectItem.SetValue(target, DateTime.Parse((string)newValue));
                        break;
                    case "DOUBLE":
                        AIONObjectItem.SetValue(target, Convert.ToDouble(newValue));
                        break;
                    case "INT":
                        AIONObjectItem.SetValue(target, Convert.ToInt32(newValue));
                        break;
                    case "DECIMAL":
                        AIONObjectItem.SetValue(target, Convert.ToDecimal(newValue));
                        break;
                    case "LONG":
                        AIONObjectItem.SetValue(target, Convert.ToInt64(newValue));
                        break;
                    case "LIST<AGENCYINFO>":
                        AIONObjectItem.SetValue(target, (List<AgencyInfo>)newValue);
                        break;
                    case "LIST<TRADEINFO>":
                        AIONObjectItem.SetValue(target, (List<TradeInfo>)newValue);
                        break;
                    case "LIST<STRING>":

                        List<string> mstring = new List<string>();

                        if (newValue.GetType().Name.Contains("List"))
                        {
                            foreach (var newelement in (List<Dictionary<string, Object>>)newValue)
                            {
                                foreach (var newitem in newelement)
                                {
                                    mstring.Add(newitem.Key + ":" + newitem.Value);

                                }
                            }
                            AIONObjectItem.SetValue(target, (List<string>)mstring);
                        }
                        else
                        {
                            foreach (var newitem in (Dictionary<string, object>)newValue)
                            {
                                mstring.Add(newitem.Key + ":" + newitem.Value);
                            }

                            AIONObjectItem.SetValue(target, mstring);
                        }

                        break;

                    case "LIST<TASKACTIVATION>":
                        {
                            var mNeTaskActiavationList =
                                new TaskActivationBO().MakeAionTaskActivationsList((List<KeyValuePair<string, object>>)newValue);
                            AIONObjectItem.SetValue(target, mNeTaskActiavationList);
                        }

                        break;
                    case "LIST<ACCELAMEETING>":
                        {
                            var mNewAccelaMeetings = new AccelaMeetingBO().MakeAIONAccelaMeetingsList((List<KeyValuePair<string, object>>)newValue);
                            AIONObjectItem.SetValue(target, mNewAccelaMeetings);

                        }
                        break;
                    case "LIST<GATERESPONSE>":
                        {
                            var mNewGateRejectionReasons = new GateResponseBO().MakeAIONGateRejectionReasonsList((List<KeyValuePair<string, object>>)newValue);
                            AIONObjectItem.SetValue(target, mNewGateRejectionReasons);
                        }

                        break;
                    
                    case "PRELIMPROJECTSUMMARY":
                        {
                            PrelimProjectSummaryObj mPrelimProjectSummaries = new PrelimProjectSummaryObj();

                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimProjectSummaryObj>>(details);

                            mPrelimProjectSummaries = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimProjectSummaries);
                            break;
                        }
                    case "PRELIMGENERALINFO":
                        {
                            PrelimGeneralInfoObj mPrelimGeneralInfo = new PrelimGeneralInfoObj();

                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimGeneralInfoObj>>(details);

                            mPrelimGeneralInfo = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimGeneralInfo);
                            break;
                        }
                    case "PRELIMMEETINGAGENDA":
                        {
                            PrelimMeetingAgendaObj mPrelimMeetingAgenda = new PrelimMeetingAgendaObj();


                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimMeetingAgendaObj>>(details);

                            mPrelimMeetingAgenda = tdetails[0];


                            AIONObjectItem.SetValue(target, mPrelimMeetingAgenda);

                            break;
                        }
                    case "PRELIMPROPOSEDWORK":
                        {
                            PrelimProposedWorkObj mPrelimProposedWork = new PrelimProposedWorkObj();

                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimProposedWorkObj>>(details);

                            mPrelimProposedWork = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimProposedWork);

                            break;
                        }
                    case "PRELIMSYSTEMINFO":
                        {
                            PrelimSystemInfoObj mPrelimSystemInfo = new PrelimSystemInfoObj();
                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimSystemInfoObj>>(details);

                            mPrelimSystemInfo = tdetails[0];


                            AIONObjectItem.SetValue(target, mPrelimSystemInfo);
                            break;
                        }
                    case "PRELIMBIMPROJECTDELIVERY":
                        {
                            PrelimBIMProjectDeliveryObj mPrelimBIMProjectDelivery = new PrelimBIMProjectDeliveryObj();

                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimBIMProjectDeliveryObj>>(details);

                            mPrelimBIMProjectDelivery = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimBIMProjectDelivery);
                            break;
                        }
                    case "PRELIMTYPEOFWORK":
                        {
                            PrelimTypeOfWorkObj mPrelimTypeOfWork = new PrelimTypeOfWorkObj();

                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimTypeOfWorkObj>>(details);

                            mPrelimTypeOfWork = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimTypeOfWork);
                            break;
                        }
                    case "PRELIMMEETINGDETAIL":
                        {
                            PrelimMeetingDetailObj mPrelimMeetingDetail = new PrelimMeetingDetailObj();

                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimMeetingDetailObj>>(details);

                            mPrelimMeetingDetail = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimMeetingDetail);
                            break;
                        }
                    case "PRELIMMEETINGTRADESANDREVIEWER":
                        {
                            PrelimMeetingTradesAndReviewerObj mPrelimMeetingTradesAndReviewer = new PrelimMeetingTradesAndReviewerObj();
                            var details = JsonConvert.SerializeObject(newValue);

                            var tdetails = JsonConvert.DeserializeObject<List<PrelimMeetingTradesAndReviewerObj>>(details);

                            mPrelimMeetingTradesAndReviewer = tdetails[0];

                            AIONObjectItem.SetValue(target, mPrelimMeetingTradesAndReviewer);
                            break;
                        }

                }

                return AIONObjectItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Assign to Object Error: " + AIONObjectItem.Name + " - " + ex.Message, ex.InnerException);
            }
        }

        public bool IsChecked(string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && input.Equals("CHECKED", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public List<TaskInfo> GetTaskInfoForHistoryRecords(AccelaTableResult workFlowHistory, string taskStatus)
        {
            Rows historyItems = (Rows)workFlowHistory.rows[0];
            List<TaskInfo> tasks = new List<TaskInfo>();

            for (int i = 0; i < historyItems.fields.Count; i++)
            {
                Dictionary<string, object> tableFields = (Dictionary<string, object>)historyItems.fields[i];

                string descriptionName = string.Empty;

                bool hasStatusField = tableFields.TryGetValue("status", out object statusObj);
                bool hasDescription = tableFields.TryGetValue("description", out object descriptionObj);

                if (hasDescription)
                {
                    descriptionName = Convert.ToString(descriptionObj);
                }

                if (hasStatusField)
                {
                    Dictionary<string, object> status = (Dictionary<string, object>)statusObj;
                    bool hasRevisions = status.TryGetValue("text", out object statusText);
                    if (hasRevisions)
                    {
                        if (Convert.ToString(statusText) == taskStatus)
                        {
                            string taskId = tableFields["id"].ToString();

                            tasks.Add(new TaskInfo()
                            {
                                Department = descriptionName,
                                TaskId = taskId
                            });
                        }
                    }
                }
            }

            return tasks;
        }

        public List<TaskReview> GetIndividualTaskRecords(string recordId, List<TaskInfo> tasks, string cycleNumber)
        {
            AccelaApiBO accelaApiBO = new AccelaApiBO();
            List<TaskReview> taskReviews = new List<TaskReview>();

            foreach (TaskInfo task in tasks)
            {
                var workTaskReviewTask = Task.Run(() => accelaApiBO.GetRecordWorkFlowTasksWorkFlowTaskInfo(recordId, task));

                workTaskReviewTask.Wait();

                if (workTaskReviewTask.IsCompleted && !workTaskReviewTask.IsCanceled && !workTaskReviewTask.IsFaulted)
                {
                    TaskReview taskReview = workTaskReviewTask.Result;
                    if (taskReview.EstimatedRereviewTime > 0
                        && taskReview.CycleNumber == cycleNumber) // only care about rereviews for specific cycle
                    {
                        taskReviews.Add(workTaskReviewTask.Result);
                    }
                }
            }

            return taskReviews;
        }
    }
}