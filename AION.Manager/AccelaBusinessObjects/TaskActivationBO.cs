using Meck.Shared.MeckDataMapping;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AION.Manager.AccelaBusinessObjects
{
    public class TaskActivationBO : AccelaBusinessObjectBase
    {
        public List<TaskActivation> MakeAionTaskActivationsList(List<KeyValuePair<string, object>> newValue)
        {
            List<TaskActivation> mNewTaskActivation = new List<TaskActivation>();
            string Errloc = "MakeTaskList-0";

            try
            {
                for (int indx = 0; indx < newValue.Count; indx++)
                {
                    Errloc = "MakeTaskList-1";
                    if (newValue[indx].Value.GetType().Name.Contains("ArrayList"))
                    {
                        ArrayList mDataArray = (ArrayList)newValue[indx].Value;
                        for (int indxarray = 0; indxarray < mDataArray.Count; indxarray++)
                        {
                            Errloc = "MakeTaskList-2";
                            ArrayList mArrayDataElement = new ArrayList { mDataArray[indxarray] };

                            Dictionary<string, object> mNewElement = (Dictionary<string, object>)mArrayDataElement[0];
                            TaskActivation mTaskActivation = new TaskActivation();

                            foreach (var tElementTopLayer in mNewElement)
                            {
                                Errloc = "MakeTaskList-3";


                                if (!tElementTopLayer.Value.GetType().Name.Contains("Dictionary"))
                                {
                                    mTaskActivation.id = Convert.ToInt32(tElementTopLayer.Value.ToString());
                                }
                                else
                                {



                                    Dictionary<string, object> tElement =
                                        (Dictionary<string, object>)tElementTopLayer.Value;

                                    foreach (var telement in tElement)
                                    {
                                        Errloc = "MakeTaskList-4";
                                        object saveObject = null;

                                        switch (telement.Key.ToUpper())
                                        {
                                            case "STARTDATE":
                                                {
                                                    tElement.TryGetValue("StartDate", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.StartDate =
                                                            Convert.ToDateTime(saveObject.ToString());
                                                    }
                                                }
                                                break;
                                            case "ASSIGNEE":
                                                {
                                                    tElement.TryGetValue("Assignee", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.Assignee = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "TASK TYPE":
                                                {
                                                    tElement.TryGetValue("Task Type", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.TaskType = saveObject.ToString();
                                                    }
                                                }
                                                break;

                                            case "TASK NAME":
                                                {
                                                    tElement.TryGetValue("Task Name", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.TaskName = saveObject.ToString();
                                                    }

                                                }
                                                break;
                                            case "DATE-TIME STAMP":
                                                {
                                                    tElement.TryGetValue("Date-Time Stamp", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        DateTime d = new DateTime();
                                                        if (DateTime.TryParse(saveObject.ToString(), out d))
                                                            mTaskActivation.DateTimeStamp = d;
                                                        else mTaskActivation.DateTimeStamp = DateTime.Now;
                                                    }
                                                }
                                                break;
                                            case "COMMENTS":
                                                {
                                                    tElement.TryGetValue("Comments", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.Comments = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "CYCLE #":
                                                {

                                                    tElement.TryGetValue("Cycle #", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.Cycle = Convert.ToInt32(saveObject.ToString());
                                                    }
                                                }
                                                break;
                                            case "PROCESSING STATUS":
                                                {
                                                    tElement.TryGetValue("Processing Status", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.ProcessingStatus = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "POOL REVIEW":
                                                {

                                                    tElement.TryGetValue("Pool Review", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.PoolReview =
                                                            Convert.ToBoolean(saveObject.ToString() == "Yes" ? "True" : "False");
                                                    }
                                                }
                                                break;
                                            case "ESTIMATEDREVIEWTIME":
                                                {
                                                    tElement.TryGetValue("EstimatedReviewTime", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.EstimatedReviewTime =
                                                            Convert.ToDouble(saveObject.ToString());
                                                    }
                                                }
                                                break;
                                            case "DUE DATE":
                                                {
                                                    tElement.TryGetValue("Due Date", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mTaskActivation.DueDate = Convert.ToDateTime(saveObject.ToString());
                                                    }
                                                }
                                                break;
                                            default:
                                                {
                                                    tElement.TryGetValue(telement.Key, out saveObject);
                                                    string badvalue = string.Empty;
                                                    if (saveObject != null)
                                                    {
                                                        badvalue = saveObject.ToString();
                                                    }

                                                    sbAionMapTrap.Append(
                                                        "TaskActivation field from Accela error took default path:" +
                                                        telement.Key.ToUpper() + " Value:" + badvalue);
                                                }
                                                break;
                                        }
                                    }


                                }
                            }
                            Errloc = "MakeTaskList-9";
                            mNewTaskActivation.Add(mTaskActivation);
                        }
                    }
                }
                Errloc = "MakeTaskList-10";
                return mNewTaskActivation;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in MakeTaskList fromRecord : " + Errloc + "-" + ex.InnerException);
            }
        }

    }
}