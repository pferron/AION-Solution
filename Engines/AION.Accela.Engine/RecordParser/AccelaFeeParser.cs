using AION.Accela.Engine.BusinessObjects;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using Meck.Shared.Accela;
using Meck.Shared.PosseToAccela;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace AION.Accela.Engine.RecordParser
{
    public class AccelaFeeParser
    {
        public RecordWorkFlowTaskForPaymentBE GetAllParsedFeePayments(string workFlowHistory)
        {
            RecordWorkFlowTaskForPaymentBE mWorkFlowFeePayments = new RecordWorkFlowTaskForPaymentBE();

            IAIONDBEngine theengine = new AIONEngineCrudApiBO();

            // get valid task name4s
            var MapDetails = theengine.SelectPosseAccelaMapforFees();

            //  added 
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, object>>(workFlowHistory);

            RecordWorkFlowTaskForPaymentBE mWorkFlowTasks = new RecordWorkFlowTaskForPaymentBE();

            foreach (var mtask in dict) // mDictionary)
            {
                if (mtask.Key != "status")
                {
                    object workFlowTaskObj;

                    dict.TryGetValue(mtask.Key, out workFlowTaskObj);

                    ArrayList mDataArray = (ArrayList)workFlowTaskObj;

                    ArrayList mDataElement0 = new ArrayList { mDataArray[0] };

                    Dictionary<string, object> mElements = (Dictionary<string, object>)mDataElement0[0];

                    foreach (var mElement in mDataArray)
                    {
                        Dictionary<String, Object> dicWorkTask = (Dictionary<string, object>)mElement;

                        object idval;
                        object isActiveObj;
                        object isCompletedObj;
                        object descriptionObj;

                        dicWorkTask.TryGetValue("description", out descriptionObj);
                        dicWorkTask.TryGetValue("id", out idval);
                        var maprecs = MapDetails.Find(x => x.ACCELA_LOOKUP_FIELD_NM == descriptionObj.ToString());

                        if (maprecs != null)
                        {
                            if (!mWorkFlowTasks.Results.Exists(x => x.id == idval.ToString()))
                            {
                                dicWorkTask.TryGetValue("isActive", out isActiveObj);
                                dicWorkTask.TryGetValue("isCompleted", out isCompletedObj);

                                ResultFull newResultFull = new ResultFull()
                                {
                                    id = idval.ToString(),
                                    isCompleted = isCompletedObj.ToString(),
                                    isActive = isActiveObj.ToString(),
                                    description = descriptionObj.ToString()
                                };
                                mWorkFlowTasks.Results.Add(newResultFull);
                                mWorkFlowTasks.Results.Add(newResultFull);
                            }
                        }
                    }
                }
            }
            return mWorkFlowTasks;
        }

        /// <summary>
        ///  parse the WorkFlow Custom fields.  
        /// </summary>
        /// <param name="workFlowHistory"></param>
        /// <returns></returns>
        public List<PlanReviewFee> ParseWorkFlowCustomForms(string projectnumber, string workTaskDescription, string workFeeDetail)
        {
            List<PlanReviewFee> mPlanReviewFees = new List<PlanReviewFee>();

            IAccelaEngine theengine = new AccelaApiBO();

            PlanReviewFee mPlanReviewFee = new PlanReviewFee();

            //  added 
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, object>>(workFeeDetail);

            foreach (var feeDetail in dict)
            {
                if (feeDetail.Key.ToUpper() != "STATUS")
                {
                    object _feeDetails;

                    dict.TryGetValue(feeDetail.Key, out _feeDetails);

                    object feeobj = 0m;
                    object feenote = "";

                    ArrayList mDataArray = (ArrayList)_feeDetails;
                    foreach (var arrFeeDetail in mDataArray)
                    {
                        Dictionary<string, object> mfeedlayer = (Dictionary<string, object>)arrFeeDetail;

                        if (mfeedlayer.ContainsKey("FeeAmount"))
                        {
                            mfeedlayer.TryGetValue("FeeAmount", out feeobj);
                            mfeedlayer.TryGetValue("FeeNotes", out feenote);

                            mPlanReviewFee.FeeName = workTaskDescription;
                            mPlanReviewFee.ProjectNumber = projectnumber;
                            mPlanReviewFee.TotalFee = Convert.ToDecimal(feeobj.ToString());
                            mPlanReviewFee.Remarks = Convert.ToString(feenote);
                            mPlanReviewFees.Add(mPlanReviewFee);
                        }
                    }
                }
            }
            return mPlanReviewFees;
        }
    }
}
