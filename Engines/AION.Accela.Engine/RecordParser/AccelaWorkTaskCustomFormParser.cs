using AION.Accela.Engine.BusinessObjects;
using AION.Accela.Engine.Helpers;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.PosseToAccela;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace AION.Accela.Engine.RecordParser
{
    public class AccelaWorkTaskCustomFormParser
    {
        public RecordWorkFlowTaskForPaymentBE GetWorkTaskCustomFormsList(string workFlowHistory)
        {
            try
            {
                RecordWorkFlowTaskForPaymentBE mWorkFlowTasks = new RecordWorkFlowTaskForPaymentBE();

                IAIONDBEngine theengine = new AIONEngineCrudApiBO();

                // get valid task name4s
                var mapDetailsResult = theengine.SelectPosseAccelaMapforFees();

                var MapDetails = mapDetailsResult;

                //  added 
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, object>>(workFlowHistory);

                foreach (var mtask in dict) // mDictionary)
                {
                    if (mtask.Key != "status")
                    {
                        object workFlowTaskObj;

                        dict.TryGetValue(mtask.Key, out workFlowTaskObj);

                        ArrayList mDataArray = (ArrayList)workFlowTaskObj;

                        for (int indx = 0; indx < mDataArray.Count; indx++)
                        {
                            ArrayList mDataElement0 = new ArrayList { mDataArray[indx] };

                            Dictionary<string, object> mElements = (Dictionary<string, object>)mDataElement0[0];

                            //foreach (var mElement in mElements)
                            //{
                            object idval = null;
                            object isActiveObj = null;
                            object isCompletedObj = null;
                            object descriptionObj = null;
                            object objDescription = null;
                            object objHoldNote = null;
                            object objComment = null;
                            object objDueDate = null;
                            object objStatus = null;


                            mElements.TryGetValue("description", out descriptionObj);
                            mElements.TryGetValue("id", out idval);
                            mElements.TryGetValue("description", out objDescription);
                            mElements.TryGetValue("isCompleted", out objHoldNote);
                            mElements.TryGetValue("isActive", out isActiveObj);
                            mElements.TryGetValue("isCompleted", out isCompletedObj);
                            mElements.TryGetValue("comment", out objComment);
                            mElements.TryGetValue("status", out objStatus);
                            mElements.TryGetValue("dueDate", out objDueDate);


                            var maprecs =
                                MapDetails.Find(x => x.ACCELA_LOOKUP_FIELD_NM == descriptionObj.ToString());

                            //if (maprecs != null)
                            //{
                            //    if (!mWorkFlowTasks.Results.Exists(x => x.id == idval.ToString()))
                            //    {
                            //        AANHold mAANHold = new AANHold();
                            //        mAANHold.AccessGroup = objDescriptionAAN.ToString();
                            //        mAANHold.HoldNote = objHoldNote.ToString();
                            //        mWorkFlowTasks.AANHolds.Add(mAANHold);
                            //    }
                            //}

                            ResultFull newResultFull = new ResultFull();
                            if (idval != null)
                            {
                                newResultFull.id = idval.ToString();
                            }

                            if (isCompletedObj != null)
                            {
                                newResultFull.isCompleted = isCompletedObj.ToString();
                            }

                            if (isActiveObj != null)
                            {
                                newResultFull.isActive = isActiveObj.ToString();
                            }

                            if (descriptionObj != null)
                            {
                                newResultFull.description = descriptionObj.ToString();
                            }

                            if (objStatus.GetType().Name.Contains("Dictionary"))
                            {
                                Dictionary<string, object> mStatus = (Dictionary<String, object>)objStatus;

                                object testMeeting;
                                mStatus.TryGetValue("value", out testMeeting);

                                if (testMeeting.ToString().Contains("Meeting"))
                                {
                                    WorkTaskCustForms.WorkTaskMeetingDetail nMeeting =
                                        new WorkTaskCustForms.WorkTaskMeetingDetail();

                                    DateTime nDueDate = Convert.ToDateTime(objDueDate.ToString());

                                    nMeeting.meetingDate = nDueDate.Date.ToShortDateString();

                                    nMeeting.meetingAttendeesList = objComment?.ToString();

                                    mWorkFlowTasks.MeetingTasks.Add(nMeeting);
                                }
                            }

                            mWorkFlowTasks.Results.Add(newResultFull);
                            // }
                        }
                    }
                }

                return mWorkFlowTasks;
            }
            catch (Exception ex)
            {
                LoggingWrapper mLoggingControl = new LoggingWrapper();

                mLoggingControl.BLLogMessage(MethodBase.GetCurrentMethod(), ex.Message, ex);

                throw new Exception(ex.Message);

            }
        }

        /// <summary>
        ///  parse the WorkFlow Custom fields.  
        /// </summary>
        /// <param name="mWorkTaskCustForms"></param>
        /// <param name="projectnumber"></param>
        /// <param name="workFeeDetail"></param>
        /// <param name="workTaskDescription"></param>

        /// <returns></returns>
        public WorkTaskCustForms ParseWorkFlowCustomForms(WorkTaskCustForms mWorkTaskCustForms, string projectnumber, string workFeeDetail, string workTaskDescription = default(string), string projectId = default(string))
        {
            List<PlanReviewFee> mPlanReviewFees = new List<PlanReviewFee>();

            IAccelaEngine theengine = new AccelaApiBO();

            //  added 
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, object>>(workFeeDetail);

            foreach (var feeDetail in dict)
            {
                if (feeDetail.Key.ToUpper() != "STATUS")
                {
                    object _taskCFormInfo;
                    dict.TryGetValue(feeDetail.Key, out _taskCFormInfo);

                    string taskcustomform = JsonConvert.SerializeObject(_taskCFormInfo);

                    ArrayList mDataArray = (ArrayList)_taskCFormInfo;
                    foreach (var arrFeeDetail in mDataArray)
                    {
                        object dataTypeId;

                        Dictionary<string, object> mfeedlayer = (Dictionary<string, object>)arrFeeDetail;
                        mfeedlayer.TryGetValue("id", out dataTypeId);
                        switch (dataTypeId.ToString())
                        {
                            case "CE_TSI_RVFEE-PLAN.cREVIEW.cFEES":
                                object feeobj = 0m;
                                object feenote = "";

                                mfeedlayer.TryGetValue("FeeAmount", out feeobj);
                                mfeedlayer.TryGetValue("FeeNotes", out feenote);

                                PlanReviewFee mPlanReviewFee = new PlanReviewFee();

                                mPlanReviewFee.FeeName = workTaskDescription;
                                mPlanReviewFee.ProjectNumber = projectId;
                                mPlanReviewFee.TotalFee = Convert.ToDecimal(feeobj.ToString());
                                mPlanReviewFee.Remarks = Convert.ToString(feenote);

                                mWorkTaskCustForms.stagePlanReviewFees.Add(mPlanReviewFee);

                                break;
                            case "CE_TSI_RVFEE-MEETINGS":
                            case "CE_TSI_RVCBG-MEETINGS":
                            case "CE_TSI_RVCEL-MEETINGS":
                            case "CE_TSI_RVCMC-MEETINGS":
                            case "CE_TSI_RVCPB-MEETINGS":
                            case "CE_TSI_PLCRD-MEETINGS":
                            case "CE_TSI_FAC-MEETINGS":
                            case "CE_TSI_CLOSE-MEETINGS":
                            case "CE_TSI_RV-MEETINGS":
                                object objWFMeeting;
                                if (mfeedlayer.TryGetValue("MeetingType", out objWFMeeting))
                                {
                                    if (objWFMeeting is null)
                                    {
                                    }
                                    else
                                    {
                                        WorkTaskCustForms.WorkTaskMeeting mWfMeeting = new WorkTaskCustForms.WorkTaskMeeting();
                                        mWfMeeting.MeetingType = objWFMeeting.ToString();
                                        mWorkTaskCustForms.stageMeetings.Add(mWfMeeting);
                                    }
                                }

                                break;
                            case "CE_TSI_PLCRD-NUMBER.cOF.cPERMITS":
                                object objNumBuildingPermits;
                                object objNumElectricalPermits;
                                object objNumMechanicalPermits;
                                object objNumPlumbingPermits;

                                if (mfeedlayer.TryGetValue("NumBuildingPermits", out objNumBuildingPermits))
                                {
                                    mfeedlayer.TryGetValue("NumElectricalPermits", out objNumElectricalPermits);
                                    mfeedlayer.TryGetValue("NumMechanicalPermits", out objNumMechanicalPermits);
                                    mfeedlayer.TryGetValue("NumPlumbingPermits", out objNumPlumbingPermits);

                                    mWorkTaskCustForms.stageTradePermits.BuildingPermitsRequired = Convert.ToInt32(objNumBuildingPermits);
                                    mWorkTaskCustForms.stageTradePermits.ElectricalPermitsRequired = Convert.ToInt32(objNumElectricalPermits);
                                    mWorkTaskCustForms.stageTradePermits.MechanicalPermitsRequired = Convert.ToInt32(objNumMechanicalPermits);
                                    mWorkTaskCustForms.stageTradePermits.PlumbingPermitsRequired = Convert.ToInt32(objNumPlumbingPermits);
                                }
                                break;
                            case "CE_TSI_RV-REVIEWS":
                            case "CE_TSI_RVFEE-REVIEWS":
                            case "CE_TSI_RVCBG-REVIEWS":
                            case "CE_TSI_RVCEL-REVIEWS":
                            case "CE_TSI_RVCMC-REVIEWS":
                            case "CE_TSI_RVCPB-REVIEWS":

                                object objStartDate;
                                object objCycleNumber;
                                object objPoolReview;
                                object objId;
                                object objEstimatedRereviewTime;
                                object objEstimatedReviewTime;

                                TaskReview mTaskReview = new TaskReview();
                                mfeedlayer.TryGetValue("StartDate", out objStartDate);
                                mfeedlayer.TryGetValue("Cycle #", out objCycleNumber);
                                mfeedlayer.TryGetValue("id", out objId);
                                mfeedlayer.TryGetValue("Pool Review", out objPoolReview);
                                mfeedlayer.TryGetValue("EstimatedRereviewTime", out objEstimatedRereviewTime);
                                mfeedlayer.TryGetValue("EstimatedReviewTime", out objEstimatedReviewTime);

                                if (!String.IsNullOrEmpty(objStartDate?.ToString())
                                    && !String.IsNullOrEmpty(objCycleNumber?.ToString())
                                    && !String.IsNullOrEmpty(objPoolReview?.ToString())
                                    && !String.IsNullOrEmpty(objEstimatedReviewTime.ToString()))
                                {
                                    mTaskReview.StartDate = objStartDate.ToString();
                                    mTaskReview.CycleNumber = objCycleNumber.ToString();
                                    mTaskReview.PoolReview = objPoolReview.ToString() == "No" ? false : true;

                                    if (objEstimatedRereviewTime != null)
                                    {
                                        mTaskReview.EstimatedRereviewTime = decimal.Parse(objEstimatedRereviewTime.ToString());
                                    }
                                    else
                                    {
                                        mTaskReview.EstimatedRereviewTime = 0;
                                    }

                                    if (objEstimatedReviewTime != null)
                                    {
                                        mTaskReview.EstimatedReviewTime = decimal.Parse(objEstimatedReviewTime.ToString());
                                    }
                                    else
                                    {
                                        mTaskReview.EstimatedReviewTime = 0;
                                    }

                                    mWorkTaskCustForms.TaskReviews.Add(mTaskReview);
                                }

                                break;
                        }
                    }
                }
            }

            return mWorkTaskCustForms;
        }

        public TaskReview ParseWorkFlowTask(string recordId, TaskInfo taskInfo, string workFlowTaskDetail)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, object>>(workFlowTaskDetail);

            TaskReview taskReview = new TaskReview();

            foreach (var taskDetail in dict)
            {
                if (taskDetail.Key.ToUpper() != "STATUS")
                {
                    object _taskCFormInfo;
                    dict.TryGetValue(taskDetail.Key, out _taskCFormInfo);

                    string taskcustomform = JsonConvert.SerializeObject(_taskCFormInfo);

                    ArrayList mDataArray = (ArrayList)_taskCFormInfo;
                    foreach (var arrTaskDetail in mDataArray)
                    {
                        object dataTypeId;

                        Dictionary<string, object> mfeedlayer = (Dictionary<string, object>)arrTaskDetail;
                        mfeedlayer.TryGetValue("id", out dataTypeId);

                        if (dataTypeId.ToString().Contains("REVIEWS"))
                        {

                            mfeedlayer.TryGetValue("StartDate", out object objStartDate);
                            mfeedlayer.TryGetValue("Cycle #", out object objCycleNumber);
                            mfeedlayer.TryGetValue("id", out object objId);
                            mfeedlayer.TryGetValue("Pool Review", out object objPoolReview);
                            mfeedlayer.TryGetValue("EstimatedRereviewTime", out object objEstimatedRereviewTime);
                            mfeedlayer.TryGetValue("EstimatedReviewTime", out object objEstimatedReviewTime);
                            mfeedlayer.TryGetValue("Hours Spent", out object objHoursSpent);
                            mfeedlayer.TryGetValue("Department", out object objDepartment);
                            mfeedlayer.TryGetValue("IsCompleted", out object objIsCompleted);

                            if (objCycleNumber != null
                                && objPoolReview != null
                                && objEstimatedReviewTime != null)
                            {
                                taskReview.StartDate = objStartDate != null ? objStartDate.ToString() : string.Empty;
                                taskReview.CycleNumber = objCycleNumber.ToString();
                                taskReview.PoolReview = objPoolReview.ToString() == "No" ? false : true;
                                taskReview.Id = objId != null ? objId.ToString() : string.Empty;
                                taskReview.HoursSpent = objHoursSpent != null ? objHoursSpent.ToString() : "0";
                                taskReview.Department = objDepartment != null ? objDepartment.ToString() : string.Empty;
                                taskReview.IsCompleted = objIsCompleted != null ? objIsCompleted.ToString() : string.Empty;

                                if (objEstimatedRereviewTime != null)
                                {
                                    taskReview.EstimatedRereviewTime = decimal.Parse(objEstimatedRereviewTime.ToString());
                                }
                                else
                                {
                                    taskReview.EstimatedRereviewTime = 0;
                                }

                                if (objEstimatedReviewTime != null)
                                {
                                    taskReview.EstimatedReviewTime = decimal.Parse(objEstimatedReviewTime.ToString());
                                }
                                else
                                {
                                    taskReview.EstimatedReviewTime = 0;
                                }

                                taskReview.Department = taskInfo.Department;
                            }
                        }
                    }
                }
            }

            return taskReview;
        }
    }
}
