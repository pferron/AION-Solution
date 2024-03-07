using AccelaMiscellanous.ScriptApi;
using AccelaRecords.Api;
using AccelaRecords.Model;
using AION.Accela.Engine.Models;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using Newtonsoft.Json;
using Posse.Accela.Engine.RecordParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ResponseResultModelArray = AccelaRecords.Model.ResponseResultModelArray;


namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaRecordBO : AccelaBase
    {
        public async Task<RecordWrapperBE> TaskGetRecordsSearch(RecordSearchParametersBE mParms)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsApi mDocuments = new RecordsApi();

            var mRecordsResult = await mDocuments.V4GetRecordsAsync(AccelaContentHeaderEncoding, tokendata.access_token,
                mParms.type, mParms.openedDateFrom, mParms.openedDateTo, mParms.customId, mParms.module, mParms.status,
                mParms.assignedToDepartment, mParms.assignedUser,
                mParms.assignedDateFrom, mParms.assignedDateTo, mParms.completedDateFrom, mParms.completedDateTo,
                mParms.statusDateFrom,
                mParms.statusDateTo, mParms.completedByDepartment, mParms.completedByUser, mParms.closedDateFrom,
                mParms.closedDateTo,
                mParms.closedByDepartment, mParms.closedByUser, mParms.recordClass, mParms.limit, mParms.offset,
                mParms.fields, mParms.lang);

            //  null, null, null, null, ModuleName, StatusValue);

            RecordWrapperBE mRecWrapper = new RecordWrapperBE();

            foreach (var mresult in mRecordsResult.Result)
            {
                var mTResult = JsonConvert.SerializeObject(mresult);

                var mDocModel = JsonConvert.DeserializeObject<SimpleRecordModelBE>(mTResult);

                mRecWrapper.SimpleRecModels.Add(mDocModel);
                // for drawing sealed info
                IScriptApi mAccelaMiscellanous = new ScriptApi();






            }





            return mRecWrapper;
        }



        #region Record Details

        /// <summary>
        /// GetAccelaRecordAll
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<AccelaRecordModel> TaskGetAccelaRecordAll(string recordId, bool traceDetail = false)
        {
            try
            {
                // This does everything needed to get a token. 
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                // Get REcord detail

                IRecordsApi mRecordApi = new RecordsApi();

                //   ResponseRecordModelArray
                string mExpandValues =
                    "addresses,parcels,professionals,contacts,conditions,owners,customForms,customTables,assets,run_emse_script";

                //  var recordresult = await mRecordApi.V4GetRecordsIdsAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId, mExpandValues);

                var recordresulttask = mRecordApi.V4GetRecordsIdsStringAsync(AccelaContentHeaderEncoding,
                    tokendata.access_token, recordId, mExpandValues);
                recordresulttask.Wait();

                var recordresult = recordresulttask.Result;

                if (traceDetail)
                {
                    WriteTraceFile("GetRecords", recordId, recordresult);
                }

                AccelaRecordParser mAccelaRecordParser = new AccelaRecordParser();

                AccelaRecordModel mAccelaRecordResult = new AccelaRecordModel();

                IScriptApi mAccelaMiscellanous = new ScriptApi();

                mAccelaRecordResult.result.Add(mAccelaRecordParser.ParseAccelaJson(recordresult));

                foreach (var professional in mAccelaRecordResult.result[0].professionals)
                {
                    // Load up a valie the makes sense to check   the professional record Id which has the type 

                    object objlicense = string.Empty;

                    professional.TryGetValue("id", out objlicense);

                    if (objlicense.ToString().Contains("Architect"))
                    {

                        professional.TryGetValue("licenseNumber", out objlicense);
                        if (objlicense == null)
                        {
                            Dictionary<string, object> maddDocStatus = new Dictionary<string, object>();

                            professional.Add("DrawingSealed", objlicense);
                            break;
                        }

                        professional.TryGetValue("licenseNumber", out objlicense);

                        AccelaRecordModel msignedobject = new AccelaRecordModel();

                        var drawingStatus = mAccelaMiscellanous.V4PostGetDrawingsSealedEms(AccelaContentHeaderEncoding,
                            tokendata.access_token, recordId, objlicense.ToString());


                        if (drawingStatus.Contains("\"DrawingsSealed\":\"Yes\""))
                        {
                            professional.Add("DrawingSealed", "YES");
                        }
                        else
                        {
                            professional.Add("DrawingSealed", "NO");
                        }
                    }
                }

                return mAccelaRecordResult;

            }
            catch (Exception ex)
            {
                throw new Exception(" Unable to access Accela Record" + ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<string> TaskGetAccelaWorkFlowHistoryforRecord(string recordId)
        {
            // This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record WorkFlowHistory detail

            IRecordsWorkflowsApi mRecordWorkflowsApi = new RecordsWorkflowsApi();

            var recordResult = await mRecordWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksHistoriesStringAsync(
                AccelaContentHeaderEncoding,
                tokendata.access_token, recordId);

            RecordWorkFlowHistoryBE recordWorkFlowHistory =
                JsonConvert.DeserializeObject<RecordWorkFlowHistoryBE>(recordResult);

            return recordResult;
        }


        public async Task<string> TaskGetRecordsRecordIdWorkflowTasksHistories(string recordId)
        {
            // This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();


            // Get Record WorkFlowHistory detail

            IRecordsWorkflowsApi mRecordWorkflowsApi = new RecordsWorkflowsApi();

            var recordResult = await mRecordWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksHistoriesAsync(
                AccelaContentHeaderEncoding,
                tokendata.access_token, recordId);

            var recordWorkFlowHistory = JsonConvert.SerializeObject(recordResult);

            return recordWorkFlowHistory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="workFlowTaskId"></param>
        /// <returns></returns>
        public async Task<string> TasksGetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(string recordId,
            string workFlowTaskId)
        {
            // This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record WorkFlowHistory detail

            IRecordsWorkflowsApi mRecordWorkFlowApi = new RecordsWorkflowsApi();

            var recordWorkFlowResult =
                await mRecordWorkFlowApi.V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsStringAsync(
                    AccelaContentHeaderEncoding, tokendata.access_token, recordId, workFlowTaskId);

            // returning a accela object that needs to be parsed. 

            return recordWorkFlowResult;
        }

        /// <summary>
        /// GetAccelaRecordWorkTasks
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<AccelaTableResult> TaskGetAccelaRecordWorkTasks(string recordId)
        {
            try
            {
                // This does everything needed to get a token. 
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                // Get REcord detail

                IRecordsWorkflowsApi mRecordWorkflowsApi = new RecordsWorkflowsApi();

                //   ResponseRecordModelArray
                string mExpandValues = string.Empty;
                // "addresses,parcels,professionals,contacts,owners,customForms,customTables,assets";

                var recordresult = await mRecordWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksAsync(
                    AccelaContentHeaderEncoding,
                    tokendata.access_token, recordId, mExpandValues);

                AccelaRecordParser mAccelaRecordParser = new AccelaRecordParser();

                AccelaTableResult mActiveTables = mAccelaRecordParser.CustomFormsLoadAndProcess(recordresult);

                return mActiveTables;
            }
            catch (Exception ex)
            {
                throw new Exception(" Unable to access Accela Record" + ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// GetAccelaRecordWorkTasksJson
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<string> TaskGetAccelaRecordWorkTasksJson(string recordId)
        {
            try
            {
                // This does everything needed to get a token. 
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                // Get REcord detail

                IRecordsWorkflowsApi mRecordWorkflowsApi = new RecordsWorkflowsApi();

                //   ResponseRecordModelArray
                string mExpandValues = string.Empty;
                // "addresses,parcels,professionals,contacts,owners,customForms,customTables,assets";

                var recordresult = await mRecordWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksAsync(
                    AccelaContentHeaderEncoding,
                    tokendata.access_token, recordId, mExpandValues);

                return recordresult;
            }
            catch (Exception ex)
            {
                throw new Exception(" Unable to access Accela Record" + ex.Message, ex.InnerException);
            }
        }

        public async Task<ResponseRecordRelatedModelArrayBE> TaskGetRelatedRecordInfo(string recordId, RecordRelatedModelBE.RelationshipEnum relationShip)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsApi mRecordsApi = new RecordsApi();

            RecordRelatedModelBE.RelationshipEnum cInEnumValu = new RecordRelatedModelBE.RelationshipEnum();

            RecordRelatedModel.RelationshipEnum.TryParse(relationShip.ToString(), out cInEnumValu);

            if (cInEnumValu == null)
            {
                throw new Exception("RealtionShip must be one of the following: parent or child or renewal");
            }
            try
            {

                //
                var result = await mRecordsApi.V4GetRecordsRecordIdRelatedAsync(AccelaContentHeaderEncoding, tokendata.access_token,
                    recordId, cInEnumValu.ToString().ToLower());

                ResponseRecordRelatedModelArrayBE mResponseRecordRelatedModelArrayBE = new ResponseRecordRelatedModelArrayBE();

                mResponseRecordRelatedModelArrayBE.Result = new List<RecordRelatedModelBE>();

                foreach (var tRelationShip in result.Result)
                {
                    RecordRelatedModelBE mRecordrelatedModelBE = new RecordRelatedModelBE();

                    RecordTypeNoAliasModelBE mRecordTypeNoAliasModelBE = new RecordTypeNoAliasModelBE();

                    mRecordTypeNoAliasModelBE.Id = tRelationShip.Type.Id;
                    mRecordTypeNoAliasModelBE.Category = tRelationShip.Type.Category;
                    mRecordTypeNoAliasModelBE.FilterName = tRelationShip.Type.FilterName;
                    mRecordTypeNoAliasModelBE.Group = tRelationShip.Type.Group;
                    mRecordTypeNoAliasModelBE.Module = tRelationShip.Type.Module;
                    mRecordTypeNoAliasModelBE.SubType = tRelationShip.Type.SubType;
                    mRecordTypeNoAliasModelBE.Text = tRelationShip.Type.Text;
                    mRecordTypeNoAliasModelBE.Value = tRelationShip.Type.Value;


                    mRecordrelatedModelBE.CustomId = tRelationShip.CustomId;
                    mRecordrelatedModelBE.Id = tRelationShip.Id.Replace("MECKLENBURG-", "");

                    RecordRelatedModelBE.RelationshipEnum cEnumValu = new RecordRelatedModelBE.RelationshipEnum();

                    RecordRelatedModel.RelationshipEnum.TryParse(tRelationShip.Relationship.ToString(), out cEnumValu);

                    mRecordrelatedModelBE.Relationship = cEnumValu;
                    mRecordrelatedModelBE.ServiceProveCode = tRelationShip.ServiceProveCode;
                    mRecordrelatedModelBE.TrackingId = tRelationShip.TrackingId;
                    mRecordrelatedModelBE.Type = mRecordTypeNoAliasModelBE;


                    mResponseRecordRelatedModelArrayBE.Result.Add(mRecordrelatedModelBE);
                }

                return mResponseRecordRelatedModelArrayBE;
            }
            catch (Exception)
            {
                throw;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <param name="fields"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public async Task<ResponseTaskItemModelBE> TaskUpDateRecordWorkFlowStep(string recordId, string id,
            RequestTaskItemModelBE body, string fields = null, string lang = null)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsWorkflowsApi mAccelaRecordWorkFLows = new RecordsWorkflowsApi();

            var mupdatreReqwuestTaskItem = JsonConvert.SerializeObject(body);

            var mbody = JsonConvert.DeserializeObject<RequestTaskItemModel>(mupdatreReqwuestTaskItem);

            var docresult = await mAccelaRecordWorkFLows.V4PutRecordsRecordIdWorkflowTasksIdAsync(
                AccelaContentHeaderEncoding, tokendata.access_token, recordId, id, mbody, null, null);


            var mResult = JsonConvert.SerializeObject(docresult);

            ResponseTaskItemModelBE mREsultTaskItemModel =
                JsonConvert.DeserializeObject<ResponseTaskItemModelBE>(mResult);

            return mREsultTaskItemModel;
        }

        /// <summary>
        /// TaskUpDateRecordWorkFlowItem
        /// </summary>
        /// <param name="RecordId"></param>
        /// <param name="WorkFlowItemID"></param>
        /// <param name="TaskItemBody"></param>
        /// <returns></returns>
        public async Task<List<string>> TaskUpDateRecordCustomTables(RequestCustomTablesTasksBE mWorkFlowupdateInfo)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE mTokenData = await mAuth.TaskGetAuthToken();

            IRecordsCustomTablesApi mRecordsApi = new RecordsCustomTablesApi();

            AccelaRecordParser mAccelaRecordParser = new AccelaRecordParser();

            string RecordId = mWorkFlowupdateInfo.recordId;

            List<string> TaskProcessingResults = new List<string>();
            foreach (var WorkFlowupdateInfo in mWorkFlowupdateInfo.array)
            {
                var taskforupdate = WorkFlowupdateInfo.ToJasonString(WorkFlowupdateInfo);

                var result = await mRecordsApi.V4PutRecordsRecordIdCustomTablesListAsync(AccelaContentHeaderEncoding,
                    mTokenData.access_token, RecordId, taskforupdate, null, null);

                var workflowupdate = JsonConvert.SerializeObject(result.Result);
                TaskProcessingResults.Add(workflowupdate);
            }

            return TaskProcessingResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="mUpdateRecordDetail"></param>
        /// <returns></returns>
        public async Task<SimpleRecordModel> TaskUpDateRecord(string recordId, RecordBE mUpdateRecordDetail)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsApi mRecordsApi = new RecordsApi();

            var keyStoreValues = mAuth.LoadupParmsAndAzureKeyVaultData();

            RequestSimpleRecordModel mUpDateDetail =
                JsonConvert.DeserializeObject<RequestSimpleRecordModel>(
                    JsonConvert.SerializeObject(mUpdateRecordDetail));

            var UpDateResult =
                await mRecordsApi.V4PutRecordsIdAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId,
                    mUpDateDetail);

            return UpDateResult;
        }


        #endregion

        #region Record supporting data



        /// <summary>
        /// GetRecordAddress
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Address WrapperBE</returns>
        public async Task<AddressWrapperBE> TaskGetRecordAddress(string recordId)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsAddressesApi mDocuments = new RecordsAddressesApi();

            var recordAddressResult = await mDocuments.V4GetRecordsRecordIdAddressesAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId);

            AddressWrapperBE mAddressResult = new AddressWrapperBE();


            if (recordAddressResult != null)
            {
                foreach (var mdetail in recordAddressResult)
                {
                    var mTAddressResults = JsonConvert.SerializeObject(mdetail);

                    var mResult = JsonConvert.DeserializeObject<AddressBE>(mTAddressResults);

                    mAddressResult.Addresses.Add(mResult);
                }
            }

            return mAddressResult;

        }

        /// <summary>
        /// GetRecordContacts
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>ContactWrapperBE</returns>
        public async Task<ContactWrapperBE> TaskGetRecordContacts(string recordId)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record Contacts

            IRecordsContactsApi mRecordContacts = new RecordsContactsApi();

            var recordContactResult = await mRecordContacts.V4GetRecordsRecordIdContactsAsync(tokendata.access_token, recordId);

            ContactWrapperBE mContactWrapper = new ContactWrapperBE();

            if (recordContactResult != null)
            {
                foreach (var mdetail in recordContactResult.Result)
                {
                    var mTAddressResults = JsonConvert.SerializeObject(mdetail);

                    var mResult = JsonConvert.DeserializeObject<ContactBE>(mTAddressResults);

                    mContactWrapper.Contacts.Add(mResult);
                }
            }

            return mContactWrapper;
        }


        /// <summary>
        /// GetRecordProfessional
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<List<LicenseProfessionalModelBE>> TaskGetRecordProfessionals(string recordId)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record Contacts

            IRecordsProfessionalsApi mRecordContacts = new RecordsProfessionalsApi();

            var recordProfessionalResult = await mRecordContacts.V4GetRecordsRecordIdProfessionalsAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId);

            List<LicenseProfessionalModelBE> mProfWrapper = new List<LicenseProfessionalModelBE>();


            foreach (var mdata in recordProfessionalResult.Result)
            {
                var mtdata = JsonConvert.SerializeObject(mdata);
                var mresultdata = JsonConvert.DeserializeObject<LicenseProfessionalModelBE>(mtdata);
                mProfWrapper.Add(mresultdata);
            }

            return mProfWrapper;
        }


        public async Task<ResponseCustomFormSubgroupModelArrayBE> TaskGetRecordRecordCustomTableMeta(string recordId, string CustomTableName)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();
            /*
                    //   IRecordsCustomTablesApi mRecordCustomTables = new RecordsCustomTablesApi();

                      //   var result =
                       //       await mRecordCustomTables.V4GetRecordsRecordIdCustomTablesTableIdMetaAsync(AccelaContentHeaderEncoding,
                       //           tokendata.access_token, _mAccelaClientId, recordId, CustomTableName);

                       //var jsonresult = JsonConvert.SerializeObject(result.Result);

                       //   ResponseCustomFormSubgroupModelArrayBE mResponseCustomFormSubgroupModelArrayBE = JsonConvert.DeserializeObject<ResponseCustomFormSubgroupModelArrayBE>(jsonresult);
                        */

            IRecordsCustomTablesApi mRecordCustomTables = new RecordsCustomTablesApi();

            var result =
                await mRecordCustomTables.V4GetRecordsRecordIdCustomTablesTableIdMetaAsync(AccelaContentHeaderEncoding,
                    tokendata.access_token, _mAccelaClientId, recordId, CustomTableName);

            //    var jsonresult = JsonConvert.SerializeObject(result.Result);

            ResponseCustomFormSubgroupModelArrayBE mResponseCustomFormSubgroupModelArrayBE = JsonConvert.DeserializeObject<ResponseCustomFormSubgroupModelArrayBE>(result.ToJson());


            return mResponseCustomFormSubgroupModelArrayBE;
        }
        #endregion

        #region Records Documents

        /// <summary>
        ///  GetRecordDocuments
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Object</returns>
        public async Task<DocumentWrapperBE> TaskGetRecordDocuments(string recordId)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsDocumentsApi mDocuments = new RecordsDocumentsApi();

            var docresult = await mDocuments.V4GetRecordsRecordIdDocumentsAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId);

            DocumentWrapperBE result = JsonConvert.DeserializeObject<DocumentWrapperBE>(JsonConvert.SerializeObject(docresult));

            return result;
        }

        public async Task<ResponseResultModelArray> TaskUpLoadDocument(string RecordId, string filepath, string Description, string Category, string group)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsDocumentsApi mRecordsApi = new RecordsDocumentsApi();

            var keyStoreValues = mAuth.LoadupParmsAndAzureKeyVaultData();

            if (!File.Exists(filepath))
            {
                throw new Exception("File create operation error : File not found or accessable");

            }

            FileInfo mNewFileInfo = new FileInfo(filepath);

            var mFileMimeType = HeyRed.Mime.MimeTypesMap.GetMimeType(mNewFileInfo.Extension);

            AccelaFileInfo mFileInfoParms = new AccelaFileInfo("Mecklenburg", mNewFileInfo.Name,
                mFileMimeType, Description);

            string mFileInfo = "[" + JsonConvert.SerializeObject(mFileInfoParms) + "]";

            FileStream mNewReadStream = new FileStream(filepath, FileMode.Open);

            var uploadresult = await mRecordsApi.V4PostRecordsRecordIdDocumentsAsync("multipart/form-data",
                tokendata.access_token, RecordId, mNewReadStream, mFileInfo);

            return uploadresult;
        }

        /// <summary>
        /// DeleteRecordDocumentsByIDS
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="documentIDS"></param>
        /// <returns> ResponseREsultModelArray</returns>
        public async Task<ResponseResultModelArray> TaskDeleteRecordDocumentsByIDS(string recordId, string documentIds)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsDocumentsApi mrecordsApi = new RecordsDocumentsApi();

            //    // Get speific values needed to delete

            var keyStoreValues = mAuth.LoadupParmsAndAzureKeyVaultData();

            //  //   V4DeleteRecordsRecordIdDocumentsDocumentIdsAsync(string contentType, string authorization, string recordId, string documentIds, string userId, string password, string lang = null);


            var deleteresult = await mrecordsApi.V4DeleteRecordsRecordIdDocumentsDocumentIdsAsync(
                AccelaContentHeaderEncoding, tokendata.access_token, recordId, documentIds, keyStoreValues.UserName,
                keyStoreValues.password);

            return deleteresult;
        }

        #endregion

        #region Holds/Condition

        //Create Record Conditions

        public async Task<ResponseResultModelArrayBE> TaskCreateRecordCondition(string recordId, RecordConditionsInsertBE mRecConditions)
        {

            //  System.Threading.Tasks.Task<ResponseResultModelArray> V4PostRecordsRecordIdConditionsAsync(string contentType, string authorization, string recordId, List<RecordConditionModel> body, string lang = null);
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record Contacts

            List<RecordConditionModel> body = new List<RecordConditionModel>();

            foreach (var reccondition in mRecConditions.Property1)
            {
                RecordConditionModel newRecCondititon = new RecordConditionModel();

                newRecCondititon = JsonConvert.DeserializeObject<RecordConditionModel>(JsonConvert.SerializeObject(reccondition));

                body.Add(newRecCondititon);
            }

            IRecordsConditionsApi mRecordConditions = new RecordsConditionsApi();

            var result = await mRecordConditions.V4PostRecordsRecordIdConditionsAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId, body, null);

            ResponseResultModelArrayBE mResultModelArrayBE =
                JsonConvert.DeserializeObject<ResponseResultModelArrayBE>(JsonConvert.SerializeObject(result.Result));

            return mResultModelArrayBE;


        }

        //Update Record Condition

        public async Task<RecordConditionModelBE> TaskUpDateRecordCondition(string recordId, RecordConditionModelBE recConditions)
        {

            //  System.Threading.Tasks.Task<RecordConditionModel> V4PutRecordsRecordIdConditionsIdAsync(string contentType, string authorization, RecordConditionModel body, string recordId, string id, string lang = null);

            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Record Conditions

            IRecordsConditionsApi mRecordConditions = new RecordsConditionsApi();

            RecordConditionModel body = new RecordConditionModel();

            body = JsonConvert.DeserializeObject<RecordConditionModel>(JsonConvert.SerializeObject(recConditions));

            var result = await mRecordConditions.V4PutRecordsRecordIdConditionsIdAsync(AccelaContentHeaderEncoding,
                 tokendata.access_token, body, recordId, null, null);

            RecordConditionModelBE mRecCondititionModelBE =
                JsonConvert.DeserializeObject<RecordConditionModelBE>(JsonConvert.SerializeObject(result));

            return mRecCondititionModelBE;
        }


        //Delete Record Conditions

        public async Task<ResponseResultModelArrayBE> TaskDeleteRecordConditions(string recordId, string mRecConditonIds)
        {
            //      System.Threading.Tasks.Task<ResponseResultModelArray> V4DeleteRecordsRecordIdConditionsIdsAsync (string contentType, string authorization, string recordId, string ids, string lang = null);

            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record Contacts

            IRecordsConditionsApi mRecordConditions = new RecordsConditionsApi();
            var result = mRecordConditions.V4DeleteRecordsRecordIdConditionsIdsAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId, mRecConditonIds, null);

            ResponseResultModelArrayBE mResult =
                JsonConvert.DeserializeObject<ResponseResultModelArrayBE>(
                    JsonConvert.SerializeObject(result.Result));
            return mResult;
        }


        //Get All Conditions for Record


        public async Task<ResponseRecordConditionModelArrayBE> TaskGetRecordConditions(string recordId)
        {
            //   System.Threading.Tasks.Task<ResponseRecordConditionModelArray> V4GetRecordsRecordIdConditionsAsync (string contentType, string authorization, string recordId, string fields = null, string lang = null);


            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // Get Record Contacts

            IRecordsConditionsApi mRecordConditions = new RecordsConditionsApi();

            var result = mRecordConditions.V4GetRecordsRecordIdConditionsAsync(AccelaContentHeaderEncoding, tokendata.access_token, recordId, null, null);

            ResponseRecordConditionModelArrayBE mResult =
                JsonConvert.DeserializeObject<ResponseRecordConditionModelArrayBE>(
                    JsonConvert.SerializeObject(result.Result));


            return mResult;

        }

        /// <summary>
        /// Get Record Contacts 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<List<RecordContactSimpleModelBO>> TaskGetAccelaRecordContacts(string recordId)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsContactsApi mRecordContacts = new RecordsContactsApi();


            ResponseRecordContactSimpleModelArrayBO mResponseRecordContactSimpleModelArrayBO =
                new ResponseRecordContactSimpleModelArrayBO();

            RecordContactSimpleModelBO mRecordContactSimpleModelBO = new RecordContactSimpleModelBO();


            List<RecordContactSimpleModelBO> mAccelaContacts = new List<RecordContactSimpleModelBO>();


            var result =
                 mRecordContacts.V4GetRecordsRecordIdContacts(AccelaContentHeaderEncoding, tokendata.access_token,
                     recordId);

            foreach (var tresult in result.Result)
            {
                mRecordContactSimpleModelBO =
                    JsonConvert.DeserializeObject<RecordContactSimpleModelBO>(
                        JsonConvert.SerializeObject(tresult));

                mAccelaContacts.Add(mRecordContactSimpleModelBO);
            }

            return mAccelaContacts;

        }


        /// <summary>
        ///  GetsContactCustom Form 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<string> TaskGetContactCustomForm(string recordId, long contactId)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsContactsCustomFormsApi mRecordContacts = new RecordsContactsCustomFormsApi();

            //  Task<ResponseCustomAttributeModelArray> mRecordContacts.V4GetRecordsRecordIdContactsContactIdCustomFormsAsync(string contentType, string authorization, string recordId, long? contactId, string lang = null);

            var custformresult = mRecordContacts.V4GetRecordsRecordIdContactsContactIdCustomFormsMeck(AccelaContentHeaderEncoding, tokendata.access_token, recordId, contactId);

            //  ResponseCustomAttributeModelArrayBE mResponseCustomAttributeModelArrayBE =
            //    JsonConvert.DeserializeObject<ResponseCustomAttributeModelArrayBE>(result);

            var result = JsonConvert.SerializeObject(custformresult);


            return result;

        }

        #endregion
        #region  Private utility

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordType"></param>
        /// <param name="recordId"></param>
        /// <param name="outdata"></param>
        private void WriteTraceFile(string recordType, string recordId, string outdata)
        {
            string recTaskfilePath = Path.GetTempPath() + "AION\\Tracedata\\";

            Directory.CreateDirectory(recTaskfilePath);

            string recTaskfileName = recTaskfilePath + recordId + " - " + recordType + ".Json";
            if (File.Exists(recTaskfileName))
            {
                File.Delete(recTaskfileName);
            }

            StreamWriter rectaskfsw = new StreamWriter(recTaskfileName, false);
            rectaskfsw.Write(outdata);
            rectaskfsw.Flush();
            rectaskfsw.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mRecord"></param>
        /// <returns></returns>
        private RecordApoCustomFormsModelBE CreateNewObject(ResponseRecordModelArray mRecord)
        {
            RecordApoCustomFormsModelBE mRecordWrapperBE = new RecordApoCustomFormsModelBE();

            foreach (var mresult in mRecord.Result)
            {
                mRecordWrapperBE = new RecordApoCustomFormsModelBE()
                {
                    ActualProductionUnit = mresult.ActualProductionUnit,
                    AppearanceDate = mresult.AppearanceDate,
                    AppearanceDayOfWeek = mresult.AppearanceDayOfWeek,
                    AssignedDate = mresult.AssignedDate,
                    AssignedToDepartment = mresult.AssignedToDepartment,
                    AssignedUser = mresult.AssignedUser,
                    Balance = mresult.Balance,
                    Booking = mresult.Booking,
                    ClosedByDepartment = mresult.ClosedByDepartment,
                    ClosedByUser = mresult.ClosedByUser,
                    ClosedDate = mresult.ClosedDate,
                    CompleteDate = mresult.CompleteDate,
                    CompletedByDepartment = mresult.CompletedByDepartment,
                    CompletedByUser = mresult.CompletedByUser,
                    CostPerUnit = mresult.CostPerUnit,
                    CreatedBy = mresult.CreatedBy,
                    CustomId = mresult.CustomId,
                    DefendantSignature = mresult.DefendantSignature,
                    Description = mresult.Description,
                    EnforceDepartment = mresult.EnforceDepartment,
                    EnforceUser = mresult.EnforceUser,
                    EnforceUserId = mresult.EnforceUserId,
                    EstimatedCostPerUnit = mresult.EstimatedCostPerUnit,
                    EstimatedDueDate = mresult.EstimatedDueDate,
                    EstimatedProductionUnit = mresult.EstimatedProductionUnit,
                    EstimatedTotalJobCost = mresult.EstimatedTotalJobCost,
                    FirstIssuedDate = mresult.FirstIssuedDate,
                    HousingUnits = mresult.HousingUnits,
                    Id = mresult.Id,
                    InPossessionTime = mresult.InPossessionTime,
                    Infraction = mresult.Infraction,
                    InitiatedProduct = mresult.InitiatedProduct,
                    InspectorDepartment = mresult.InspectorDepartment,
                    InspectorId = mresult.InspectorId,
                    InspectorName = mresult.InspectorName,
                    JobValue = mresult.JobValue,
                    Misdemeanor = mresult.Misdemeanor,
                    Module = mresult.Module,
                    Name = mresult.Name,
                    NumberOfBuildings = mresult.NumberOfBuildings,
                    OffenseWitnessed = mresult.OffenseWitnessed,
                    OpenedDate = mresult.OpenedDate,
                    OverallApplicationTime = mresult.OverallApplicationTime,
                    PublicOwned = mresult.PublicOwned,
                    RecordClass = mresult.RecordClass,
                    ReportedDate = mresult.ReportedDate,
                    ScheduledDate = mresult.ScheduledDate,
                    ShortNotes = mresult.ShortNotes,
                    StatusDate = mresult.StatusDate,
                    TotalFee = mresult.TotalFee,
                    TotalJobCost = mresult.TotalJobCost,
                    TotalPay = mresult.TotalPay,
                    TrackingId = mresult.TrackingId,
                    UndistributedCost = mresult.UndistributedCost,
                    UpdateDate = mresult.UpdateDate,
                    Value = mresult.Value
                };

                // distinct objects 
                //  ConstructionType = default(RecordAPOCustomFormsModelConstructionTypeBE),

                if ((mresult.ConstructionType) != null)
                {
                    var mtConstdata = JsonConvert.SerializeObject(mresult.ConstructionType);
                    mRecordWrapperBE.ConstructionType =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelConstructionTypeBE>(mtConstdata);
                }

                if ((mresult.ConstructionType) != null)
                {
                    //   CreatedByCloning = default(RecordApoCustomFormsModelBE.CreatedByCloningEnumBE?),
                    var mtClonedata = JsonConvert.SerializeObject(mresult.ConstructionType);
                    mRecordWrapperBE.CreatedByCloning =
                        JsonConvert.DeserializeObject<RecordApoCustomFormsModelBE.CreatedByCloningEnumBE>(mtClonedata);
                }

                if ((mresult.Priority) != null)
                {
                    //Priority = default(RecordAPOCustomFormsModelPriorityBE),
                    var mtPriordata = JsonConvert.SerializeObject(mresult.Priority);
                    mRecordWrapperBE.Priority =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelPriorityBE>(mtPriordata);
                }

                if ((mresult.RenewalInfo) != null)
                {
                    //  RenewalInfo = mresult.RenewalInfo,
                    var mtRenewData = JsonConvert.SerializeObject(mresult.RenewalInfo);
                    mRecordWrapperBE.RenewalInfo = JsonConvert.DeserializeObject<RecordExpirationModelBE>(mtRenewData);
                }

                if ((mresult.ReportedChannel) != null)
                {
                    //ReportedChannel = mresult.ReportedChannel,
                    var mtReportData = JsonConvert.SerializeObject(mresult.ReportedChannel);
                    mRecordWrapperBE.ReportedChannel =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelReportedChannelBE>(mtReportData);
                }

                if ((mresult.ReportedType) != null)
                {
                    //ReportedType = (RecordAPOCustomFormsModelReportedTypeBE)mresult,
                    var mtReptTypeData = JsonConvert.SerializeObject(mresult.ReportedType);
                    mRecordWrapperBE.ReportedType =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelReportedTypeBE>(mtReptTypeData);
                }

                if ((mresult.Severity) != null)
                {
                    //Severity = (RecordAPOCustomFormsModelSeverityBE)mresult,
                    var mtSevrData = JsonConvert.SerializeObject(mresult.Severity);
                    mRecordWrapperBE.Severity =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelSeverityBE>(mtSevrData);
                }

                if ((mresult.Status) != null)
                {
                    //Status = (RecordAPOCustomFormsModelStatusBE)mresult,
                    var mtStatusData = JsonConvert.SerializeObject(mresult.Status);
                    mRecordWrapperBE.Status =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelStatusBE>(mtStatusData);
                }

                if ((mresult.StatusReason) != null)
                {
                    // StatusReason = (RecordAPOCustomFormsModelStatusReasonBE)mresult,
                    var mtStatusReasonData = JsonConvert.SerializeObject(mresult.StatusReason);
                    mRecordWrapperBE.StatusReason =
                        JsonConvert.DeserializeObject<RecordAPOCustomFormsModelStatusReasonBE>(mtStatusReasonData);
                }

                if ((mresult.Type) != null)
                {
                    // Type = (RecordTypeModelBE)mresult.Type,
                    var mtTypeData = JsonConvert.SerializeObject(mresult.Type);
                    mRecordWrapperBE.Type = JsonConvert.DeserializeObject<RecordTypeModelBE>(mtTypeData);
                }

                // List objects 
                if (mresult.Addresses != null)
                {
                    foreach (var mAddr in mresult.Addresses)
                    {
                        var mtaddr = JsonConvert.SerializeObject(mAddr);
                        var mAddress = JsonConvert.DeserializeObject<RecordAddressCustomFormsModelBE>(mtaddr);

                        mRecordWrapperBE.Addresses.Add(mAddress);
                    }
                }

                if (mresult.Assets != null)
                {
                    foreach (var mdata in mresult.Assets)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<AssetMasterModelBE>(mtdata);
                        mRecordWrapperBE.Assets.Add(mresultdata);
                    }
                }

                if (mresult.ConditionOfApprovals != null)
                {
                    // ConditionOfApprovals = default(List<CapConditionModel2BE>),
                    foreach (var mdata in mresult.ConditionOfApprovals)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<CapConditionModel2BE>(mtdata);
                        mRecordWrapperBE.ConditionOfApprovals.Add(mresultdata);
                    }
                }

                if (mresult.Conditions != null)
                {
                    // Conditions = default(List<NoticeConditionModelBE>),
                    foreach (var mdata in mresult.Conditions)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<NoticeConditionModelBE>(mtdata);
                        mRecordWrapperBE.Conditions.Add(mresultdata);
                    }
                }

                if (mresult.Contact != null)
                {
                    //  Contact = default(List<RecordContactSimpleModelBE>),
                    foreach (var mdata in mresult.Contact)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<RecordContactSimpleModelBE>(mtdata);
                        mRecordWrapperBE.Contact.Add(mresultdata);
                    }
                }

                if (mresult.CustomForms != null)
                {
                    //  CustomForms = default(List<CustomAttributeModelBE>),
                    foreach (var mdata in mresult.CustomForms)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<CustomAttributeModelBE>(mtdata);
                        mRecordWrapperBE.CustomForms.Add(mresultdata);
                    }
                }

                if (mresult.CustomTables != null)
                {
                    // CustomTables = default(List<TableModelBE>),
                    foreach (var mdata in mresult.CustomTables)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<TableModelBE>(mtdata);
                        mRecordWrapperBE.CustomTables.Add(mresultdata);
                    }
                }

                if (mresult.Owner != null)
                {
                    //  Owner = default(List<RefOwnerModelBE>),
                    foreach (var mdata in mresult.Owner)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<RefOwnerModelBE>(mtdata);
                        mRecordWrapperBE.Owner.Add(mresultdata);
                    }
                }

                if (mresult.Parcel != null)
                {
                    // Parcel = default(List<ParcelModel1BE>),
                    foreach (var mdata in mresult.Parcel)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<ParcelModel1BE>(mtdata);
                        mRecordWrapperBE.Parcel.Add(mresultdata);
                    }
                }

                if (mresult.Professional != null)
                {
                    // Professional = default(List<LicenseProfessionalModelBE>),
                    foreach (var mdata in mresult.Professional)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<LicenseProfessionalModelBE>(mtdata);
                        mRecordWrapperBE.Professional.Add(mresultdata);
                    }
                }

                if (mresult.StatusType != null)
                {
                    //  StatusType = default(List<string>),
                    foreach (var mdata in mresult.StatusType)
                    {
                        var mtdata = JsonConvert.SerializeObject(mdata);
                        var mresultdata = JsonConvert.DeserializeObject<string>(mtdata);
                        mRecordWrapperBE.StatusType.Add(mresultdata);
                    }
                }
            }

            return mRecordWrapperBE;
        }

        #endregion



    }
}
