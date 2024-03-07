using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.Adapters;
using AION.Manager.AccelaBusinessObjects;
using Meck.Azure;
using Meck.Shared.PosseToAccela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AION.AIONDB.Engine.Tests
{

    [TestClass]
    public class AccelaRecordMapTests
    {

        //  public Mock<AccelaRecordModel> mMockAccelTokenBe = new Mock<AccelaRecordModel>();


        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");

            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");

            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultConnectionString");
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void SelectAccelaAionMapTest()
        {
            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();


            var result = mAIONDBEngine.SelectAccelaAionMap();


            var mSelectedData = result;

            Assert.IsTrue(mSelectedData.Count > 1);
        }



        /// <summary>
        /// 
        /// </summary>
        //    [TestMethod]
        public void SelectAccelaAionMapDataSetTest()
        {
            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();

            var result = thisengine.SelectAccelaAionMapDataTable();

            var mSelectedData = result;


            Assert.IsTrue(mSelectedData.Rows.Count > 1);

        }

        /// <summary>
        /// 
        /// </summary>
        //    [TestMethod]
        public void SelectAccelaAionMapDataSetByAccelaRecTypeTest()
        {
            string mAccelRecType = "Commercial Project";

            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();

            var result = thisengine.SelectAccelaAionMapDataTableByAccerlaRecordType(mAccelRecType);

            var mSelectedData = result;

            Assert.IsTrue(mSelectedData.Rows.Count > 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //  [TestMethod]
        public void GetRecordTestMApByRecordType()
        {
            Console.WriteLine(
                "TEST GET Mecklenburg Document");

            IAccelaEngine thisengine = new AccelaApiBO();

            string recordid = "MECKLENBURG-REC20-00000-000I4"; // "REC20-00000-000HO";

            var rectestresult = thisengine.GetAccelaRecord(recordid);

            rectestresult.Wait();

            var testresult2 = rectestresult.Result;


            Console.WriteLine(JsonConvert.SerializeObject(testresult2));

            Assert.IsTrue(testresult2 != null);

            Assert.IsTrue(testresult2.result.Count > 0);

            Assert.IsFalse(testresult2.result[0].hasErrors);

            Assert.IsTrue(String.IsNullOrWhiteSpace(testresult2.result[0].parsingError));

            foreach (var trecord in testresult2.result)
            {

                IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

                var result = Task.Run(() => mAIONDBEngine.SelectAccelaAionMapByRecordType(trecord.ParseRecType));

                result.Wait();

                var mSelectedMap = result.Result;

                Assert.IsTrue(mSelectedMap.Count > 1);

                foreach (var testrow in mSelectedMap)
                {
                    Assert.AreEqual(testrow.ACCELA_REC_TYP_NM, trecord.ParseRecType);
                }
#if DEBUG
                //string filePath = Path.GetTempPath() + "AION\\MapDetails\\";

                //Directory.CreateDirectory(filePath);

                //string fileName = filePath + recordid + " RecordMapdetail.Json";

                //StreamWriter sw = new StreamWriter(fileName, false);
                //sw.Write(JsonConvert.SerializeObject(mSelectedMap));
                //sw.Flush();
                //sw.Close();
#endif
            }
        }

#if DEBUG


        [TestMethod]
        [Ignore]
        public void MapProjectToAIONProjectDisplayInfoTest()
        {
            string recordId = "REC21-00000-000G0"; // "REC21-00000-00007"; 

            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            var tAccelRecord = mAccelaEngine.GetAccelaRecord(recordId);


            var mAccelaRecordModel = tAccelRecord.Result;

            //  string jsonoutfie = JsonConvert.SerializeObject(mAccelaRecordModel);

            IEstimationAccelaAdapter mEstiamationAccelaMappingAdapter = new EstimationAccelaAdapter();

            IAIONDBEngine theengine = new AIONEngineCrudApiBO();

            Object mtrecordid;

            mAccelaRecordModel.result[0].CommonFields.TryGetValue("id", out mtrecordid);

            var recordid = mtrecordid.ToString();

            var result = Task.Run(() =>
                theengine.SelectAccelaAionMapByRecordType(mAccelaRecordModel.result[0].ParseRecType));
            result.Wait();

            var mAccelaAIONMap = result.Result;


            var partialmappingresult =
                new AccelaProjectDisplayInfoBO().ConvertAccelaToAIONProjectDisplayInfo(recordid,
                    mAccelaRecordModel.result[0], mAccelaAIONMap);

            Assert.IsFalse(partialmappingresult is null);


            var testresult = JsonConvert.SerializeObject(partialmappingresult);


            string recCustFormfilePath = Path.GetTempPath() + "AION MAPPING\\MappingResult\\";
            string recCustFormfileName = "AIONProjectDisplayInfo- " + recordid + " - " +
                                         mAccelaRecordModel.result[0].ParseRecType + ".json";

            WriteFileWithDupeCheck(recCustFormfilePath, recCustFormfileName, testresult, true);

        }

        //[TestMethod]
        public async Task GetAccelaContactsCustomForms()
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();

            string recordId = "REC21-00000-000I7";

            var mAccelaRecordModel = await mAccelaEngine.GetAccelaRecord(recordId);
            IAIONDBEngine theengine = new AIONEngineCrudApiBO();

            Object mtrecordid;

            mAccelaRecordModel.result[0].CommonFields.TryGetValue("id", out mtrecordid);

            var recordid = mtrecordid.ToString();

            var result = Task.Run(() =>
                theengine.SelectAccelaAionMapByRecordType(mAccelaRecordModel.result[0].ParseRecType));
            result.Wait();

            var mAccelaAIONMap = result.Result;


            foreach (var contact in mAccelaRecordModel.result[0].Contacts)
            {
                ContactCustomFormBO mCustFormBo = new ContactCustomFormBO();

                var mcontactDetail = new ContactDetailBO().ParserRecordContact(recordId, contact, mAccelaAIONMap);

                Console.WriteLine("Contact: " + JsonConvert.SerializeObject(mcontactDetail));

            }


            Assert.IsFalse(1 == 0);
        }



        /* --------------------------------     utility fuction -------------------------- */


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
#endif 
    }
}
