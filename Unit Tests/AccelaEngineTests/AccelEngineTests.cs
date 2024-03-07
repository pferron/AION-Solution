using AccelaEngineTests.UnitTestData;
using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.Accela.Engine.Models;
using AION.Base;
using Meck.Azure;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.PosseToAccela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
//using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Castle.Core.Internal;
using Newtonsoft.Json.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Match = System.Text.RegularExpressions.Match;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace AccelaEngineTests
{

    [TestClass]
    public class AccelaEngineTests : BaseBO
    {
        // Test for the Accela Engine.

        private Mock<IAccelaEngine> _moqAccelaEngine = new Mock<IAccelaEngine>();


        private TestData mTEstData = new TestData();

        //[ClassInitialize]
        //public static void AccelaBO(TestContext context)
        //{

        //}


        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccelaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");

            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");
            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultConnectionString");


 
            _moqAccelaEngine = new Mock<IAccelaEngine>();
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }


        [TestMethod]
        [Ignore]
        public void TestMethod1()
        {
            var result = KeyVaultUtility.GetSecret("AccelaUserId");

            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();

            AccelaParmsDetailBE mParams = mAuth.LoadupParmsAndAzureKeyVaultData();

            Assert.IsTrue(result != null);

            Console.WriteLine("Config Params Agency: {0}", mParams.Agency);
            Console.WriteLine("Config Params Environment: {0}", mParams.Environment);
            Console.WriteLine("Scope {0}", Meck.Shared.Globals.AccelaScope);
            Console.WriteLine("KeyVault AccelaUserID: {0}", mParams.UserName);
            Console.WriteLine("KeyValue Password: {0}", mParams.password);
            Console.WriteLine("KeyValue ClientId: {0}", mParams.ClientId);
            Console.WriteLine("KeyVault ClientSecret: {0}", mParams.ClientSecret);
        }

        /// <summary>
        ///  GetAuthTokenTest
        /// </summary>
        [TestMethod]
        [Ignore]
        public async Task TestGetTokenTest()
        {
            Console.WriteLine(
                "TEST change config changed to use Mecklenburg Accela agency");
            var result = new AccelaTokenBE();
            var Moqresult = new AccelaTokenBE();

            IAccelaEngine thisengine = new AccelaApiBO();

            _moqAccelaEngine.Setup(x => x.TestGetToken()).ReturnsAsync(Moqresult);

            result = await thisengine.TestGetToken();

            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsNotNull(result);

        }

      //  [TestMethod]
        public async Task GetAccelaContactsTest()
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            string recordId = "REC21-00000-000I7";



            var result = await mAccelaEngine.GetAccelaRecordContacts(recordId);

            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsFalse(result.Count == 0);


        }

      //  [TestMethod]
        public async Task GetContactCustFormTest()
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            string recordId = "REC21-00000-000I7";


            var result = await mAccelaEngine.GetAccelaRecordContacts(recordId);

            foreach (var contact in result)
            {

                var custform = await mAccelaEngine.GetContactCustomForm(recordId, Convert.ToInt32(contact.Id));

                ContactCustomFormBO myDeserializedClass = JsonConvert.DeserializeObject<ContactCustomFormBO>(custform);

                var mDictCustForm = JsonConvert.DeserializeObject<Dictionary<string, object>>(custform);

                Console.WriteLine(" ID :" + contact.Id + " " + myDeserializedClass.Result[0]);
            }

            Assert.IsFalse(1 == 0);
        }

       


        /// <summary>
        /// GetTokenStillActiveTest
        /// </summary>
        //  [TestMethod]
        public async Task GetTokenStillActiveTest()
        {
            var result = new AccelaTokenBE();
            var result2 = new AccelaTokenBE();

            IAccelaEngine thisengine = new AccelaApiBO();

            result = await thisengine.TestGetToken();

            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsNotNull(result);

            result2 = result = await thisengine.TestGetToken();

            Assert.AreEqual(result.expiresAt, result2.expiresAt);

            Console.WriteLine("DateTime test works.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [TestMethod]
       [Ignore]
        public async Task AccelaGetRecordTest()
        {
            Console.WriteLine(
                "TEST GET Mecklenburg Document");

            IAccelaEngine thisengine = new AccelaApiBO();

            string recordid = "REC23-00000-0000I";  // mTEstData.recordId; // "MECKLENBURG-REC21-00000-000SE"; // "REC20-00000-000HO";


            var testresult2 = await thisengine.GetAccelaRecord(recordid);

            string filePath = Path.GetTempPath() + "AION\\";


            Directory.CreateDirectory(filePath);

            string fileName = filePath + recordid + " Record detail.Json";

            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(JsonConvert.SerializeObject(testresult2));
            sw.Flush();
            sw.Close();
            Console.WriteLine(JsonConvert.SerializeObject(testresult2));

            Assert.IsTrue(testresult2 != null);

            Assert.IsTrue(testresult2.result.Count > 0);

            Assert.IsFalse(testresult2.result[0].hasErrors);

            Assert.IsTrue(String.IsNullOrWhiteSpace(testresult2.result[0].parsingError));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [Ignore]
        public async Task AccelaGetRecordWorkTasksTest()
        {
            Console.WriteLine(
                "TEST GET Mecklenburg Document");

            IAccelaEngine thisengine = new AccelaApiBO();

            string recordid = mTEstData.recordId; //"MECKLENBURG-REC21-00000-000SE";

            AccelaRecordModel mAccelaRecordModel = new AccelaRecordModel();

            // Task<ResponseCustomFormSubgroupModelArrayBE> GetRecordRecordCustomTableMeta(string recordId,string CustomTableName)


            var testresult2 = await thisengine.GetAccelaRecordWorkFlowTask(recordid);

            Assert.IsTrue(testresult2 != null);

            string filePath = Path.GetTempPath() + "AION\\";

            Directory.CreateDirectory(filePath);

            string fileName = filePath + recordid + "WorkTasks.json";

            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(JsonConvert.SerializeObject(testresult2));
            sw.Flush();
            sw.Close();
            Console.WriteLine(JsonConvert.SerializeObject(testresult2));




        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [Ignore]
        public async Task AccelaGetRecordRecordCustomTableMetaTest()
        {
            Console.WriteLine(
                "TEST GET Mecklenburg Document");

            IAccelaEngine thisengine = new AccelaApiBO();

            string recordId = mTEstData.recordId; //"MECKLENBURG-REC20-00000-000JW";   // known good parsed record 

            string customTableWorkTaskNameTable = "CE_COM-REVIEW.cTASK.cACTIVATION";


            AccelaRecordModel mAccelaRecordModel = new AccelaRecordModel();

            // Task<ResponseCustomFormSubgroupModelArrayBE> GetRecordRecordCustomTableMeta(string recordId,string CustomTableName)

            var testresult2 = await thisengine.GetRecordRecordCustomTableMeta(recordId, customTableWorkTaskNameTable);


            //   test script for the workflow tasks in the record.
            //      var testresult2 = await thisengine.GetAccelaRecordWorkFlowTask(recordid);


            string filePath = Path.GetTempPath() + "AION\\";

            Directory.CreateDirectory(filePath);

            string fileName = filePath + recordId + "WorkTasks Meta Data.json";

            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(JsonConvert.SerializeObject(testresult2));
            sw.Flush();
            sw.Close();
            Console.WriteLine(JsonConvert.SerializeObject(testresult2));

            Assert.IsTrue(testresult2 != null);


        }


        /// <summary>
        ///  UpdateRecordAddSingleTaskTest()
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [Ignore]
        public async Task UpdateRecordAddSingleTaskTest()
        {
            // ResponseTaskItemModelBE UpDateRecordWorkFlowItem(AccelaWorkFlowTaskUpdate mWorkFlowTaskUpdate);
            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

            List<TableFieldBE> mFieldBE = new List<TableFieldBE>();

            mFieldBE.Add(new TableFieldBE(null, "id", "CE_CRTAP-REVIEW.cTASK.cACTIVATION"));
            mFieldBE.Add(new TableFieldBE(null, "Task Type", "Application Submittal"));
            mFieldBE.Add(new TableFieldBE(null, "Task Name", "Agency Distribution"));
            mFieldBE.Add(new TableFieldBE(null, "Pool Review", "Yes"));
            mFieldBE.Add(new TableFieldBE(null, "Assignee", "TestUser"));
            mFieldBE.Add(new TableFieldBE(null, "Due Date", "2020-08-30"));
            mFieldBE.Add(new TableFieldBE(null, "Cycle #", ""));
            mFieldBE.Add(new TableFieldBE(null, "StartDate", "2020-09-01"));
            mFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", "10"));
            mFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", "10:30"));
            mFieldBE.Add(new TableFieldBE(null, "Processing Status", "Completed in Accela"));
            mFieldBE.Add(new TableFieldBE(null, "Comments", "Dave Unit test  Qa tyest 2 "));



            TableRowsBE mTableRowsBE =
                new TableRowsBE("CE_CRTAP-REVIEW.cTASK.cACTIVATION", TableRowBE.ActionEnum.Add, mFieldBE);


            // Add next set of fields  

            /// finalize all fields into the array of Custom tables object. 

            mRequestCustomTablesTasksBe.array.Add(mTableRowsBE);

            mRequestCustomTablesTasksBe.recordId = "REC20-00000-000IV";

            // TestDataRecordUpdate mTestDataRecordUpdate = new TestDataRecordUpdate();

            var fullobject = JsonConvert.SerializeObject(mRequestCustomTablesTasksBe);

            string filePath = Path.GetTempPath() + "AION\\AccelaTaskdataUpdater\\";

            Directory.CreateDirectory(filePath);

            string fileName = filePath + mRequestCustomTablesTasksBe.recordId + "- RequestCustomTablesTasksBE.Json";

            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(fullobject);
            sw.Flush();
            sw.Close();



            var testOut = mTableRowsBE.ToJasonString(mRequestCustomTablesTasksBe.array[0]);

            //    Console.WriteLine("-TestModel BE ???????????????????????????? ");

            Console.WriteLine(testOut);



            IAccelaEngine thisengine = new AccelaApiBO();

            var result = await thisengine.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);

            Assert.IsTrue(result.Count > 0);

        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Add2TaskStepsTest()
        {
            // populate the object  
            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe =
                new AccelaCustomTableTaskUpDateModelBE();
            // populate the object//
            mAccelaCustomTableTaskUpDateModelBe.recordId = "REC20-00000-000IV";

            AccelaTaskUpDateDetail mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail()
            {
                id = "CE_CRTAP-REVIEW.cTASK.cACTIVATION",
                TaskType = "Application submittal",
                TaskName = "Agency Distribution",
                PoolReview = "No",
                Assignee = "TestUser",
                DueDate = DateTime.Now.AddMonths(1).ToShortDateString(),
                CycleCount = "1",
                StartDate = DateTime.Now,
                EstimatedReviewTime = Convert.ToInt32("5"),
                DateTimeStamp = DateTime.Now,
                ProcessingStatus = "Unit Test",
                Comments = "Dave Unit test"
            };
            mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

            mAccelaTaskUpDateDetail.TaskName = "Agency Distribution";

            mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

            // make the call 

            IAccelaEngine thisengine = new AccelaApiBO();

            var result = thisengine.UpDateAccelaTasksRecords(mAccelaCustomTableTaskUpDateModelBe);
            result.Wait();
            var response = result.Result;

            Assert.IsTrue(response.Count == 2);

        }

        /// <summary>
        /// AccelaGetAllAgenciesTest
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task AccelaGetAllTradesTest()
        {
            Console.WriteLine(
                "TEST changed 12-18 -19 to use ddolson credentials , config changed to use Mecklenburg Accela agency");


            IAccelaEngine thisengine = new AccelaApiBO();


            var testresult2 = await thisengine.GetAllTradesNameList();

            Console.WriteLine(JsonConvert.SerializeObject(testresult2));

            Assert.IsTrue(testresult2 != null);
        }

        /// <summary>
        /// AccelaGetAllAgenciesTest
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task AccelaGetAllTradeNamesTest()
        {
            IAccelaEngine thisengine = new AccelaApiBO();


            var testresult2 = await thisengine.GetAllTradesNameList();

            Console.WriteLine(JsonConvert.SerializeObject(testresult2));

            Assert.IsTrue(testresult2 != null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [Ignore]
        public async Task TryDataMappingProjectJsonTestMethod1()
        {
            AccelaOldBO mold = new AccelaOldBO();

            var mtestresult = await mold.GetRecords();

            RecordWrapperBE recordwrapper = JsonConvert.DeserializeObject<RecordWrapperBE>(mtestresult);

            Console.WriteLine(" Done");

        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task GetUsersByTypeTest()
        {
            Console.WriteLine(" Get Users by Type ");

            IAccelaEngine thisengine = new AccelaApiBO();

            string mUserType;

            mUserType = "Estimator";

            var result = await thisengine.GetUsersByType(mUserType);

            Assert.IsTrue(result.Departmentlist[0].DepartmentUsers.Count != 0);

            Console.WriteLine(" TEst Completed. ");

        }

        /// <summary>
        /// GetAllUsersWithDepartmentTest
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task GetAllUsersWithDepartmentTest()
        {
            Console.WriteLine(" Get Users by Type ");

            IAccelaEngine thisengine = new AccelaApiBO();

            var result = await thisengine.GetAllUsersWithDepartment();


            // check for at least one contact in all of the d

            int TotaluserCnt = 0;

            foreach (var mTestDept in result.Departmentlist)
            {
                TotaluserCnt = TotaluserCnt + mTestDept.DepartmentUsers.Count;
            }

            Assert.IsTrue(TotaluserCnt > 0);

            foreach (var mDeptRec in result.Departmentlist)
            {
                Console.WriteLine("ID: {0}, count users {1} ", mDeptRec.Id, mDeptRec.DepartmentUsers.Count);
            }

            Console.WriteLine(" TEst Completed");
        }



        // Task<DepartmentWrapperBE> GetAllAgencyLis


        [TestMethod]
        [Ignore]
        public async Task GetAllAgencyListTest()
        {
            IAccelaEngine thisengine = new AccelaApiBO();

            var testresult = await thisengine.GetAllAgencyList();

            Assert.IsTrue(testresult.Departmentlist.Count != 0);

            Console.WriteLine(" Test Completed");

            var mTestResult = JsonConvert.SerializeObject(testresult.Departmentlist);

            Console.WriteLine("{0}", mTestResult);

        }



        /// <summary>
        /// GetAllDepartmentsListTest
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task GetAllDepartmentsListTest()
        {
            Console.WriteLine(" GetDepartments");

            IAccelaEngine thisengine = new AccelaApiBO();

            // <summa
            // GetAllDepartme
            // </summa
            var mDepartList = await thisengine.GetAllDepartments();

            Assert.IsTrue(mDepartList.Departmentlist.Count != 0);


            var mTestResult = JsonConvert.SerializeObject(mDepartList.Departmentlist);

            Console.WriteLine(" End GetDepartments");

            Console.WriteLine("{0}", mTestResult);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task GetAllProfessionalLicenseTypesTest()
        {
            Console.WriteLine("Deleting a document");

            IAccelaEngine thisengine = new AccelaApiBO();

            var mSettingResult = await thisengine.GetAllProfessionalLicenseTypes();


            Assert.IsTrue(mSettingResult.SettingTypes != null);

            Console.WriteLine("Test Completed");
            var testresult = JsonConvert.SerializeObject(mSettingResult);

            Console.WriteLine("{0}", testresult);
        }

        /// <summary>
        /// GetAllAcelaRecordsTest
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task GetAccelaRecordsSearchandDetailTest()
        {
            Console.WriteLine("TEst for all a get on Records");

            Console.WriteLine(" Getting Listing of available records, not using Module or Status ");


            string ModuleName = string.Empty;

            String StatusValue = String.Empty;

            IAccelaEngine thisengine = new AccelaApiBO();

            RecordSearchParametersBE mParms = new RecordSearchParametersBE();

            mParms.module = "CodeEnforcement";
            mParms.limit = 20; // max number of records to
            mParms.openedDateFrom = DateTime.Now.AddDays(-60).ToShortDateString();


            var mShortRecInfo = await thisengine.GetRecordsSearch(mParms);

            // cycle through all of the returned REcords and build local Json outp
            //  All File will appear in C:\TEmp\AionTest on local machin

            foreach (var mRecInfo in mShortRecInfo.SimpleRecModels)
            {
                var recordid = mRecInfo.Value;

                Console.WriteLine(
                    "TEST GET Mecklenburg Document for {0} ", recordid);

                var testResult = await thisengine.GetAccelaRecord(recordid);

                var mRecordInfo = JsonConvert.SerializeObject(testResult);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mRecordInfo));

                Assert.IsFalse(testResult.result[0].hasErrors);

                Assert.IsTrue(String.IsNullOrWhiteSpace(testResult.result[0].parsingError));



                //string filePath = Path.GetTempPath() + "AION\

                //Directory.CreateDirectory(filePat

                //string fileName = filePath + recordid + ".Jso

                ////StreamWriter sw = new StreamWriter(fileName, fals
                ////sw.Write(mRecordInf
                ////sw.Flush
                ////sw.Close
            }

            Console.WriteLine(" Test Completed");
        }

        /// <summary>
        /// GetAllAcelaRecordsTest
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [Ignore]
        public async Task GetCustFormsCusTablesForRecordTypes()
        {
            Console.WriteLine("TEst for all a get on Records");

            Console.WriteLine(" Getting Listing of available records, not using Module or Status ");


            string ModuleName = string.Empty;

            String StatusValue = String.Empty;

            IAccelaEngine thisengine = new AccelaApiBO();

            RecordSearchParametersBE mParms = new RecordSearchParametersBE();

            mParms.module = "CodeEnforcement";
            mParms.limit = 20; // max number of records to 

            var mShortRecInfo = await thisengine.GetRecordsSearch(mParms);

            // cycle through all of the returned REcords and build local Json outp
            //  All File will appear in C:\TEmp\AionTest on local machin

            foreach (var mRecInfo in mShortRecInfo.SimpleRecModels)
            {
                var custformtask = thisengine.GetSettingsCustomForms(mRecInfo.Type.Id);
                custformtask.Wait();
                var custformtaskresult = custformtask.Result;


                var custTablestask = thisengine.GetRecordSettingsCustomTables((mRecInfo.Type.Id));
                custTablestask.Wait();
                var custTablestaskresult = custTablestask.Result;



                var mCustFormInfo = JsonConvert.SerializeObject(custformtaskresult);
                var mCustTablesInfo = JsonConvert.SerializeObject(custTablestaskresult);

                Assert.IsFalse(String.IsNullOrWhiteSpace(mCustFormInfo));
                Assert.IsFalse(String.IsNullOrWhiteSpace(mCustTablesInfo));


                string recTaskfilePath = Path.GetTempPath() + "AION\\CustomForms\\";

                Directory.CreateDirectory(recTaskfilePath);

                string recTaskfileName = recTaskfilePath + "CustomForms -" + mRecInfo.Type.Id + ".Json";
                if (File.Exists(recTaskfileName))
                {
                    File.Delete(recTaskfileName);
                }

                StreamWriter rectaskfsw = new StreamWriter(recTaskfileName, false);
                rectaskfsw.Write(mCustFormInfo);
                rectaskfsw.Flush();
                rectaskfsw.Close();

                // now get the Custom tables data 
                string reclCuustTablesfilePath = Path.GetTempPath() + "AION\\CustomTables\\";

                Directory.CreateDirectory(reclCuustTablesfilePath);

                string recCustomTablesfileName =
                    reclCuustTablesfilePath + "CustomTables -" + mRecInfo.Type.Id + ".Json";
                if (File.Exists(recCustomTablesfileName))
                {
                    File.Delete(recCustomTablesfileName);
                }

                StreamWriter recCustTablesfsw = new StreamWriter(recCustomTablesfileName, false);
                recCustTablesfsw.Write(mCustTablesInfo);
                recCustTablesfsw.Flush();
                recCustTablesfsw.Close();

            }

            Console.WriteLine(" Test Completed");
        }

        /// <summary>
        /// Sanitize filename by removing the path
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>Filename</returns
        public static string SanitizeFilename(string filename)
        {
            Match match = Regex.Match(filename, @".*[/\\](.*)$");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return filename;
            }

        }

        [TestMethod]
        [Ignore]
        public async Task GetRelatedParentInfoTest()
        {

            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            //ResponseRecordRelatedModelArrayBE
            //

            // this must be a 
            //   Meeeting request or Commercial or Residential RTAP  

            string recordId = mTEstData.recordId; //"REC21-00000-000AG"; // parent 
            try
            {
                Console.WriteLine(" Sending for " + recordId + " and " +
                                  RecordRelatedModelBE.RelationshipEnum.Parent.ToString());
                var results =
                    await mAccelaEngine.GetRelatedRecordInfo(recordId, RecordRelatedModelBE.RelationshipEnum.Parent);

                Console.WriteLine(JsonConvert.SerializeObject(results));

                Assert.IsFalse(results is null);

            }

            catch (Exception ex)
            {
                string errmsg = "RecordID " + recordId + " May only have parent.";
                Console.WriteLine(errmsg);
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        [Ignore]
        public async Task GetRelatedChildInfoTest()
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            //ResponseRecordRelatedModelArrayBE
            //
            // this must be a 
            //   Meeeting request or Commercial or Residential RTAP

            string recordId = mTEstData.recordId; // "REC21-00000-000AG"; // parent 

            try
            {
                Console.WriteLine(" Sending for " + recordId + " and " +
                                  RecordRelatedModelBE.RelationshipEnum.Parent.ToString());
                var results =
                    await mAccelaEngine.GetRelatedRecordInfo(recordId, RecordRelatedModelBE.RelationshipEnum.Child);

                Console.WriteLine(JsonConvert.SerializeObject(results));

                Assert.IsFalse(results is null);

            }
            catch (Exception ex)
            {
                string errmsg = "RecordID " + recordId + " May only have parent.";
                Console.WriteLine(errmsg);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///  Returns WorkFlow CustomForms based on the recordId and WorkFlowTaskId
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskIDTest()
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            string recordId = mTEstData.recordId; //"REC21-00000-000SE";
            string workFlowTaskId = "62-19307";

            // returns  ask<WorkTaskCustForms> 

            var result = Task.Run(() =>
                mAccelaEngine.GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(recordId, workFlowTaskId));


            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.IsFalse(result.Result is null);

            //   Assert.IsNotNull(result.Result.stageMeetings[0].MeetingType); 


            Console.WriteLine(result.Result);

            string recTaskfilePath = Path.GetTempPath() + "AION\\CustomForms\\";

            Directory.CreateDirectory(recTaskfilePath);

            string recTaskfileName = recTaskfilePath + recordId + "-TaskID-" + workFlowTaskId +
                                     "-WorkFlowTaskCustomForm.Json";
            if (File.Exists(recTaskfileName))
            {
                File.Delete(recTaskfileName);
            }

            StreamWriter rectaskfsw = new StreamWriter(recTaskfileName, false);
            rectaskfsw.Write(result.Result);
            rectaskfsw.Flush();
            rectaskfsw.Close();

        }

        /// <summary>
        ///  Returns all work flow history for the recordId 
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        [Ignore]

        public async Task GetAccelaWorkFlowHistoryforRecordTest()
        {

            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            string recordId = mTEstData.recordId; // "REC21-00000-000SE";

            // returns  ask<WorkTaskCustForms> 

            var result = Task.Run(() => mAccelaEngine.GetAccelaWorkFlowHistoryforRecord(recordId));
            Assert.IsFalse(String.IsNullOrEmpty(result.Result));

            Console.WriteLine(result.Result);

            string recTaskfilePath = Path.GetTempPath() + "AION\\CustomForms\\";

            Directory.CreateDirectory(recTaskfilePath);

            string recTaskfileName = recTaskfilePath + recordId + " - WorkFlowHistory .Json";
            if (File.Exists(recTaskfileName))
            {
                File.Delete(recTaskfileName);
            }

            StreamWriter rectaskfsw = new StreamWriter(recTaskfileName, false);
            rectaskfsw.Write(result.Result);
            rectaskfsw.Flush();
            rectaskfsw.Close();

        }

        /// <summary>
        ///  This test will return the EstimatedReEstimate hours as part of the data. 
        /// </summary>
        [TestMethod]
        [Ignore]

        public void GetRecordWorkFlowTaskCustomTasksEstimateReEstimateHoursTest()
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            string recordId = mTEstData.recordId; // "REC21-00000-000SE";

            string projectId = "MMF-000259";


            var result = Task.Run(() => mAccelaEngine.GetRecordWorkFlowTaskCustForms(recordId, projectId));
            result.Wait();

            Console.WriteLine(result.Result);

            Assert.IsFalse(String.IsNullOrEmpty(result.Result.ToString()));

        }
    }
}
