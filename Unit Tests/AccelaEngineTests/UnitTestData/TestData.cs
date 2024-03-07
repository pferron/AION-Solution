using AION.Base;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Meck.Shared;
using Meck.Data;

namespace AccelaEngineTests.UnitTestData
{
    public class TestData : BaseBO
    {
        public List<PlanReviewHistory> mPlaneReviewHistoryTest = new List<PlanReviewHistory>();
        public List<RecordNotification> mRecordNotificationTest = new List<RecordNotification>();


        public string recordId =  "REC22-00000-000CJ"; // "MMF-PM-000101" ; // "REC21-090000-000F4"; 


        public TestData()
        {
            PrepPlanReviewHistory();
            PrepRecordNotification();
        }

        public void ClearDBTable(bool delRecords = false, bool delPlanReview = false, bool delAEData = false)
        {
            string sqlDelNewRecord =
               "DELETE FROM [AION].[ACCELA_RECEIVED_RECORD_QUEUE] WHERE [REC_ID_NUM] like 'Unit%' ";

            string sqlDelPlanReview =
                "DELETE FROM [AION].[Plan_Review_History] WHERE [REC_ID_NUM] like 'Unit%' ";

            string sqlDelAEData =
                "DELETE FROM [AION].[ACCELA_AE_DATA] WHERE [REC_ID_NUM] like 'Unit%' ";

            if (delRecords)
            {
                SqlWrapper.RunSQLReturnDS(sqlDelNewRecord, Globals.AIONConnectionString);

            }

            if (delPlanReview)
            {
                SqlWrapper.RunSQLReturnDS(sqlDelPlanReview, Globals.AIONConnectionString);
            }

            if (delAEData)
            {
                SqlWrapper.RunSQLReturnDS(sqlDelAEData, Globals.AIONConnectionString);
            }


        }


        /// <summary>
        /// make PlanReview History load
        /// </summary>
        /// <returns></returns>
        public void PrepPlanReviewHistory()
        {
            for (int indx = 0; indx < 4; indx++)
            {
                PlanReviewHistory mPRHistory = new PlanReviewHistory();

                mPRHistory.ACCELA_CREATED_DT = DateTime.Now.AddDays(-10.2);
                mPRHistory.PLAN_REVIEW_END_DT = DateTime.Now;
                mPRHistory.PLAN_REVIEW_START_DT = DateTime.Now.AddDays(-5.4);
                mPRHistory.REC_ID_NUM = "UnitTest-00" + Convert.ToString(indx) + "-Test 1";

                mPlaneReviewHistoryTest.Add(mPRHistory);
            }
        }

        /// <summary>
        /// Make REcordNotification Test data  
        /// </summary>
        public void PrepRecordNotification()
        {
            for (int indx = 0; indx < 10; indx++)
            {
                RecordNotification mRecordNortification = new RecordNotification()
                {
                    EstimatedRereviewHours = "2.5",
                    WorkFlowStepId  = " test step 1, test step 2, test step 3",
                    WorkFlowTaskName = "Similuated send to Aion",
                    WorkFlowStatus = "SendToAION",
                    recordID = "Unit_Test-00" + indx.ToString() + "-birds",
                    recordtype = "SomeRecordType",
                    status = " Ready for Estimate",
                    statusdescription = "should be replaced"
                };
                mRecordNotificationTest.Add(mRecordNortification);
            }

        }




    }

    public class TestDataModel
    {
        public string id { get; set; }
        public TestDataType type { get; set; }
        public string module { get; set; }
        public long trackingId { get; set; }
        public float jobValue { get; set; }
        public string createdBy { get; set; }
        public string reportedDate { get; set; }
        public string initiatedProduct { get; set; }
        public string statusDate { get; set; }
        public string recordClass { get; set; }
        public string updateDate { get; set; }
        public string serviceProviderCode { get; set; }
        public string customId { get; set; }
        public string openedDate { get; set; }
        public float undistributedCost { get; set; }
        public float totalJobCost { get; set; }
        public string value { get; set; }
        public float totalFee { get; set; }
        public float totalPay { get; set; }
        public float balance { get; set; }
        public bool booking { get; set; }
        public bool infraction { get; set; }
        public bool misdemeanor { get; set; }
        public bool offenseWitnessed { get; set; }
        public bool defendantSignature { get; set; }
        public bool publicOwned { get; set; }
    }

    public class TestDataType
    {
        public string module { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public string group { get; set; }
        public string text { get; set; }
        public string alias { get; set; }
        public string subType { get; set; }
        public string category { get; set; }
        public string id { get; set; }
    }



}

