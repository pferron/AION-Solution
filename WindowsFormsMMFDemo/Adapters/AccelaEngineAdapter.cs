using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using Meck.Azure;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace MapLoaderInterface.Adapters
{
    public class AccelaEngineAdapter
    {
        public AccelaEngineAdapter()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccelaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            //    Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");
            //   Meck.Shared.Globals.AionConnectionString = KeyVaultUtility.GetSecret("KeyVaultAIONConnectionstring");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");

            //   string PosseDevTest = KeyVaultUtility.GetSecret("KeyVaultAIONDevConnectionString");


            //  _moqAccelaEngine = new Mock<IAccelaEngine>();
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }

        public List<string> MakeListofRecords(string customid)
        {

            IAccelaEngine thisengine = new AccelaApiBO();

            RecordSearchParametersBE mParms = new RecordSearchParametersBE();

            string ModuleName = string.Empty;

            String StatusValue = String.Empty;

            List<string> mTestRecords = new List<string>();

            mParms.module = "CodeEnforcement";
            mParms.limit = 200; // max number of records to get
            mParms.customId = customid;
            mParms.limit = 1;
            try
            {
                var task = Task.Run(() => thisengine.GetRecordsSearch(mParms));
                task.Wait();
                var result = task.Result;

                if (result.SimpleRecModels.Count > 0)
                {

                    foreach (var mRecInfo in result.SimpleRecModels)
                    {
                        var recordid = mRecInfo.Value;

                        //  mRecInfo.Id

                        mTestRecords.Add(recordid);

                    }
                }
            }
            catch (Exception)
            {
            }

            return mTestRecords;
        }

        public ResultModelBE LoadRecordFromCsvToAccelaAionMap(string theFileNameToLoad)
        {
            //  bool nodata = false;

            StringBuilder sbText = new StringBuilder();

            LoadAccelaAIONMap mAccelaAIONMap = new LoadAccelaAIONMap();

            List<LoadAccelaAIONMap> mAccelaAIONMaps = new List<LoadAccelaAIONMap>();

            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();

            //var mCsvConfig = new CsvHelper.Configuration.Configuration
            //{
            //    HasHeaderRecord = true,
            //    HeaderValidated = null,
            //    MissingFieldFound = null,
            //    IgnoreBlankLines = false,
            //};

            //using (var reader = new StreamReader(theFileNameToLoad))
            //using (var csv = new CsvReader(reader, mCsvConfig, false))
            //{
            //    csv.Read();
            //    csv.ReadHeader();
            //    while (csv.Read())
            //    {
            //        var record = csv.GetRecord<LoadAccelaAIONMap>();
            //        mAccelaAIONMaps.Add(record);
            //    }

            //}

            //List<LoadAccelaAIONMap> mMissingData = new List<LoadAccelaAIONMap>();
            //List<LoadAccelaAIONMap> mMissingDataB = new List<LoadAccelaAIONMap>();
            //List<LoadAccelaAIONMap> mMissingDataC = new List<LoadAccelaAIONMap>();



            //// Test data for obvious errors. 

            //// Test data for obvious errors. 
            //foreach (var tdata in mAccelaAIONMaps)
            //{
            //    mMissingData.Add(tdata);
            //}

            //string mFilterValue =
            //    @"Filter :FindAll(x => (x.ACCELA_OBJ_TYP_DESC == string.Empty && x.ACCELA_FIELD_NM != String.Empty));";

            //mMissingData = mMissingData.FindAll(x => (x.ACCELA_OBJ_TYP_DESC == string.Empty && x.ACCELA_FIELD_NM != String.Empty));

            //string mFilterValueb =
            //    @"Filter :FindAll(x => || (x.ACCELA_FIELD_NM != string.Empty  &&  x.AION_DATA_TYP_DESC == string.Empty));";

            //mMissingDataB = mMissingData.FindAll(x => (x.ACCELA_FIELD_NM != string.Empty && x.AION_DATA_TYP_DESC == string.Empty));



            ///// does mapping detail have a match in hte acelaobject?
            ///// 
            //string mFilterValueC = @"Field name not in Map result object: AionProjectModel ";

            //AccelaProjectModel mAIONProjectModel = new AccelaProjectModel();

            //var mAIONProjectModelJson = JsonConvert.SerializeObject(mAIONProjectModel);

            //foreach (var tdata in mAccelaAIONMaps)
            //{
            //    if (tdata.AION_CLS_NM == "AccelaProjectModel")
            //    {
            //        if (tdata.ACCELA_REC_TYP_NM.ToUpper() != "COMMONFIELDS")
            //        {
            //            // remove unmapped object for test
            //            if (tdata.ACCELA_OBJ_TYP_DESC.Trim().ToUpper() != "VOID")
            //            {
            //                if (!mAIONProjectModelJson.Contains(tdata.AION_FIELD_NM.Trim()))
            //                {
            //                    mMissingDataC.Add(tdata);
            //                }
            //            }
            //        }
            //    }
            //}

            //AccelaProjectDisplayInfo mAccelaProjectDisplayInfo = new AccelaProjectDisplayInfo();

            //var mAccelaProjectDisplayInfoJson = JsonConvert.SerializeObject(mAccelaProjectDisplayInfo);



            //foreach (var tdata in mAccelaAIONMaps)
            //{
            //    if (tdata.AION_CLS_NM == "AccelaProjectDisplayInfo")
            //    {
            //        if (tdata.ACCELA_REC_TYP_NM.ToUpper() != "COMMONFIELDS")
            //        {
            //            // remove unmapped object for test
            //            if (tdata.ACCELA_OBJ_TYP_DESC.Trim().ToUpper() != "VOID")
            //            {
            //                if (!mAccelaProjectDisplayInfoJson.Contains(tdata.AION_FIELD_NM))
            //                {
            //                    mMissingDataC.Add(tdata);
            //                }
            //            }
            //        }
            //    }
            //}


            //if (mMissingData.Count > 0 || mMissingDataB.Count > 0 || mMissingDataC.Count > 0)
            //{
            //    var asJson = JsonConvert.SerializeObject(mMissingData);
            //    var asJsonb = JsonConvert.SerializeObject(mMissingDataB);

            //    var asJsonc = JsonConvert.SerializeObject(mMissingDataC);

            //    sbText.AppendLine(mFilterValue + asJson + " \r\n" + mFilterValueb + asJsonb + "\r\n" + mFilterValueC + asJsonc);


            //    ResultModelBE mBadResult = new ResultModelBE("Data Error Detected", 0, false, sbText.ToString());

            //    return mBadResult;

            //}

            int indx = 0;

            foreach (var tdata in mAccelaAIONMaps)
            {
                thisengine.InsertAccelaAIONMapRecord(tdata);

                indx++;
            }

            ResultModelBE mGoodInsert = new ResultModelBE("OK", indx, true, "Inserted " + indx);


            return mGoodInsert;
        }


        public ResultModelBE TEstAIONRecordBeforeLoad(string theFileNameToLoad)
        {

            //  bool nodata = false;

            StringBuilder sbText = new StringBuilder();

            LoadAccelaAIONMap mAccelaAIONMap = new LoadAccelaAIONMap();

            List<LoadAccelaAIONMap> mAccelaAIONMaps = new List<LoadAccelaAIONMap>();

            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();

            //var mCsvConfig = new CsvHelper.Configuration.Configuration
            //{
            //    HasHeaderRecord = true,
            //    HeaderValidated = null,
            //    MissingFieldFound = null,
            //    IgnoreBlankLines = false,
            //};

            //using (var reader = new StreamReader(theFileNameToLoad))
            //using (var csv = new CsvReader(reader, mCsvConfig, false))
            //{
            //    csv.Read();
            //    csv.ReadHeader();
            //    while (csv.Read())
            //    {
            //        var record = csv.GetRecord<LoadAccelaAIONMap>();
            //        mAccelaAIONMaps.Add(record);
            //    }

            //}

            List<LoadAccelaAIONMap> mMissingData = new List<LoadAccelaAIONMap>();
            List<LoadAccelaAIONMap> mMissingDataB = new List<LoadAccelaAIONMap>();
            List<LoadAccelaAIONMap> mMissingDataC = new List<LoadAccelaAIONMap>();



            // Test data for obvious errors. 

            // Test data for obvious errors. 
            foreach (var tdata in mAccelaAIONMaps)
            {
                mMissingData.Add(tdata);
            }

            string mFilterValue =
                @"Filter :FindAll(x => (x.ACCELA_OBJ_TYP_DESC == string.Empty && x.ACCELA_FIELD_NM != String.Empty));";

            mMissingData = mMissingData.FindAll(x => (x.ACCELA_OBJ_TYP_DESC == string.Empty && x.ACCELA_FIELD_NM != String.Empty));

            string mFilterValueb =
                @"Filter :FindAll(x => || (x.ACCELA_FIELD_NM != string.Empty  &&  x.AION_DATA_TYP_DESC == string.Empty));";

            mMissingDataB = mMissingData.FindAll(x => (x.ACCELA_FIELD_NM != string.Empty && x.AION_DATA_TYP_DESC == string.Empty));



            /// does mapping detail have a match in hte acelaobject?
            /// 
            string mFilterValueC = @"Field name not in Map result object: AionProjectModel ";

            AccelaProjectModel mAIONProjectModel = new AccelaProjectModel();

            var mAIONProjectModelJson = JsonConvert.SerializeObject(mAIONProjectModel);

            foreach (var tdata in mAccelaAIONMaps)
            {
                if (tdata.AION_CLS_NM == "AccelaProjectModel")
                {
                    if (tdata.ACCELA_REC_TYP_NM.ToUpper() != "COMMONFIELDS")
                    {
                        // remove unmapped object for test
                        if (tdata.ACCELA_OBJ_TYP_DESC.Trim().ToUpper() != "VOID")
                        {
                            if (!mAIONProjectModelJson.Contains(tdata.AION_FIELD_NM.Trim()))
                            {
                                mMissingDataC.Add(tdata);
                            }
                        }
                    }
                }
            }

            AccelaProjectDisplayInfo mAccelaProjectDisplayInfo = new AccelaProjectDisplayInfo();

            var mAccelaProjectDisplayInfoJson = JsonConvert.SerializeObject(mAccelaProjectDisplayInfo);



            foreach (var tdata in mAccelaAIONMaps)
            {
                if (tdata.AION_CLS_NM == "AccelaProjectDisplayInfo")
                {
                    if (tdata.ACCELA_REC_TYP_NM.ToUpper() != "COMMONFIELDS")
                    {
                        // remove unmapped object for test
                        if (tdata.ACCELA_OBJ_TYP_DESC.Trim().ToUpper() != "VOID")
                        {
                            if (!mAccelaProjectDisplayInfoJson.Contains(tdata.AION_FIELD_NM))
                            {
                                mMissingDataC.Add(tdata);
                            }
                        }
                    }
                }
            }


            if (mMissingData.Count > 0 || mMissingDataB.Count > 0 || mMissingDataC.Count > 0)
            {
                var asJson = JsonConvert.SerializeObject(mMissingData);
                var asJsonb = JsonConvert.SerializeObject(mMissingDataB);

                var asJsonc = JsonConvert.SerializeObject(mMissingDataC);

                sbText.AppendLine(mFilterValue + asJson + " \r\n" + mFilterValueb + asJsonb + "\r\n" + mFilterValueC + asJsonc);


                ResultModelBE mBadResult = new ResultModelBE("Data Error Detected", 0, false, sbText.ToString());

                return mBadResult;

            }

            return new ResultModelBE("No errors Detected in output Object parameter matching", 0, true, sbText.ToString());
        }



        public List<MeckAccelaDataMap> GetAccelaIAonMapDetails()
        {
            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();

            var results = thisengine.SelectAccelaAionMap();

            return results;

        }

        public DataTable GetAccelaIAonMapListGrid()
        {
            IAIONDBEngine theengine = new AIONEngineCrudApiBO();

            var dgtable = theengine.SelectAccelaAionMapDataTable();

            var outTable = dgtable;

            return outTable;
        }

        public DataTable SelectAccelaAionMapByRecordType(string recordType)
        {
            IAIONDBEngine theengine = new AIONEngineCrudApiBO();

            var result = theengine.SelectAccelaAionMapDataTableByAccerlaRecordType(recordType);

            return result;
        }

    }
}





