#if DEBUG 
// #define DEBUG
#endif


using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Helpers;
using AccelaEngineTests.UnitTestData;
using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using Meck.Azure;
using Meck.Shared.Accela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AccelaEngineTests
{

   
    [TestClass]
    public class TestGetRecords
    {

        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");

            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");

            //  _moqAccelaEngine = new Mock<IAccelaEngine>();
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];
        }



       [TestMethod]
       [Ignore]
        public async Task GetAccelaRecordStressTest()
        {
            //  uses TEstDataModel in UnitTestData.
            //  Calls Accela
            // GetrecordsByType  span 25, pages x 
            // on Json return, records are split from Json and loaded into List of Test Data
            // after load. each list item is read and Id is used to call to GetAccelData in Accela Engine.
            // Aseert taht we got a record, taht is has values and 

            Console.WriteLine("TEst for all a get on Records");

            Console.WriteLine(" Getting Listing of up to 200 available records");

            IAccelaEngine thisengine = new AccelaApiBO();

            RecordSearchParametersBE mParms = new RecordSearchParametersBE();

            //   bool NoTask = false;
            string ModuleName = string.Empty;

            String StatusValue = String.Empty;

            // List<TestDataModel> mTestRecords = new List<TestDataModel>();

            mParms.module = "CodeEnforcement";
            mParms.limit = 200;     // max number of records to get
            mParms.openedDateFrom = DateTime.Now.AddDays(-15).ToShortDateString(); ;

            var mRecShortInfo = await thisengine.GetRecordsSearch(mParms);

            // cycle through all of the returned REcords and build local Json outputs
            //  All File will appear in C:\TEmp\AionTest on local machine. 

            var mRecSearchInfo = JsonConvert.SerializeObject(mRecShortInfo);

            Assert.IsFalse(String.IsNullOrWhiteSpace(mRecSearchInfo));

            foreach (var mRecInfo in mRecShortInfo.SimpleRecModels)
            {

                var recordId = mRecInfo.Value;

                Assert.IsFalse(String.IsNullOrWhiteSpace(recordId));

                var rectestResult = await thisengine.GetAccelaRecord(recordId);

                // Did the record process? 

                if (string.IsNullOrWhiteSpace(rectestResult.result[0].TaskActivator))
                {
                    Console.WriteLine("Record has no Task Activator :" + recordId);
                }
                else
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(rectestResult.result[0].TaskActivator));

                    // Record CustomTable Meta data
                    var CustTableMetatask = thisengine.GetRecordRecordCustomTableMeta(recordId, rectestResult.result[0].TaskActivator);
                    CustTableMetatask.Wait();
                    var CustTableMetataskresult = CustTableMetatask.Result;

                    // WorkFlow task insert point 
                    var mTaskRecordInfo = JsonConvert.SerializeObject(CustTableMetatask.Result);

                    Assert.IsFalse(String.IsNullOrWhiteSpace(mTaskRecordInfo));

                    string recTaskFilePath = Path.GetTempPath() + "AION\\TaskActivator\\";
                    string recTaskFileName = recordId + "-TaskActivator" + rectestResult.result[0].TaskActivator + ".Json";
                    WriteFileWithDupeCheck(recTaskFilePath, recTaskFileName, mTaskRecordInfo, true);
                }

                Assert.IsTrue(String.IsNullOrWhiteSpace(rectestResult.result[0].parsingError));

                Assert.IsFalse(rectestResult.result[0].hasErrors);

                // Record WorkFlowTasks 
                var wftask = thisengine.GetAccelaRecordWorkFlowTask(recordId);
                wftask.Wait();
                var wfresult = wftask.Result;

                // AllCustForms
                var custformtask = thisengine.GetSettingsCustomForms(mRecInfo.Type.Id);
                custformtask.Wait();
                var custformtaskresult = custformtask.Result;

                // All CustTables 
                var custTablestask = thisengine.GetRecordSettingsCustomTables((mRecInfo.Type.Id));
                custTablestask.Wait();
                var custTablestaskresult = custTablestask.Result;
                // All Custom form   
                var mCustFormInfo = JsonConvert.SerializeObject(custformtaskresult);

#if DEBUG
                // searching details
                var mSearchRecordInfo = JsonConvert.SerializeObject(mRecInfo);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mSearchRecordInfo));

                string recSearchfilePath = Path.GetTempPath() + "AION\\SearchDetail\\";
                string recSearchFileName = recordId + "-" + mRecInfo.Type.Type + ".json";
                WriteFileWithDupeCheck(recSearchfilePath, recSearchFileName, mSearchRecordInfo, true);

                // records
                var mRecordInfo = JsonConvert.SerializeObject(rectestResult);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mRecordInfo));

                string recordFolderPath = Path.GetTempPath() + "AION\\RecordDetails\\";
                string reccordFileName = recordId + "-" + rectestResult.result[0].ParseRecType + ".json";
                WriteFileWithDupeCheck(recordFolderPath, reccordFileName, mRecordInfo, true);

                var mWFRecordInfo = JsonConvert.SerializeObject(wfresult);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mWFRecordInfo));



                string recwffilePath = Path.GetTempPath() + "AION\\WorkFlowTasks\\";
                string recwffileName = recordId + "-" + rectestResult.result[0].ParseRecType + ".json";
                WriteFileWithDupeCheck(recwffilePath, reccordFileName, mWFRecordInfo, true);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mCustFormInfo));

                string recCustFormfilePath = Path.GetTempPath() + "AION\\CustomForms\\";
                string recCustFormfileName = "CustomForms-" + mRecInfo.Type.Id + ".json";

                WriteFileWithDupeCheck(recCustFormfilePath, recCustFormfileName, mCustFormInfo, true);

                //All Custom Tables 
                var mCustTablesInfo = JsonConvert.SerializeObject(custTablestaskresult);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mCustTablesInfo));

                string reclCustTablesfilePath = Path.GetTempPath() + "AION\\CustomTables\\";
                string recCustomTablesfileName = "CustomTables-" + mRecInfo.Type.Id + ".json";

                WriteFileWithDupeCheck(reclCustTablesfilePath, recCustomTablesfileName, mWFRecordInfo, true);
#endif
            }
        }

        private void WriteFileWithDupeCheck(string folderPath, string customFileName, string fileDetails, bool checkDupes)
        {
            // now get the Custom tables data 
            ///   string recOutFolderPath = Path.GetTempPath() + folderPath;

            Directory.CreateDirectory(folderPath);

            string recOutFolderPathFileName = folderPath + customFileName;
            if (checkDupes)
            {
                if (File.Exists(recOutFolderPathFileName))
                {
                    File.Delete(recOutFolderPathFileName);
                }
            }

            StreamWriter recCustTablesfsw = new StreamWriter(recOutFolderPathFileName, false);
            recCustTablesfsw.Write(fileDetails);
            recCustTablesfsw.Flush();
            recCustTablesfsw.Close();

        }

    }
}

