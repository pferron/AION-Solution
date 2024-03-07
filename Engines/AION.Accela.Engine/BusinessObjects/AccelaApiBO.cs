using AION.Accela.Engine.Helpers;
using AION.Accela.Engine.RecordParser;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AION.AIONDB.Engine.BusinessObjects;
using Meck.Shared.MeckDataMapping;


namespace AION.Accela.Engine.BusinessObjects
{
    public partial class AccelaApiBO : AccelaBase, IAccelaEngine
    {

        public AccelaApiBO()
        {
            //  AccelaUserId = username;
            //  AccelaPassword = password;

        }
        //public AccelaApiBO(string Username, string Password)
        //{
        //    //   AccelaUserId = Username;
        //    //   AccelaPassword = Password;

        //}


        #region Token & users
        /// <summary>
        /// TestGetToken   return values based on the contents of the Web config and the AzureKeyVault.
        /// </summary>
        /// <returns>AccelaTokenBE object</returns>
        public async Task<AccelaTokenBE> TestGetToken()
        {

            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            return tokendata;

        }

        /// <summary>
        /// GetAllUsersWithDepartment
        /// </summary>
        /// <returns></returns>
        public async Task<DepartmentWrapperBE> GetAllUsersWithDepartment()
        {
            AccelaSettingsBO mAccelaSettingsBO = new AccelaSettingsBO();
            var mDeptUsersResult = await mAccelaSettingsBO.TaskGetUsersForAllDepartments();

            return mDeptUsersResult;
        }
        #endregion

        #region Utility
        /// <summary>
        /// GetUsersByType
        /// </summary>
        /// <param name="UserType">Department type text value </param>
        /// <returns></returns>
        public async Task<DepartmentWrapperBE> GetUsersByType(string UserType)
        {
            try
            {
                AccelaSettingsBO mSettingsBO = new AccelaSettingsBO();

                var result = await mSettingsBO.TaskGetUsersForType(UserType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion


        #region Documents

        /// <summary>
        /// GetRecordsDocuments(string recordid) 
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns>Object for record documents</returns>
        public async Task<RecordWrapperBE> GetRecordsDocuments(string RecordId)
        {
            try
            {
                AccelaDocumentsBO mDocBE = new AccelaDocumentsBO();

                var result = await mDocBE.TaskGetRecordDocuments(RecordId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        ///  GetDocumentInfo
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DocumentUpDateModelWrapperBE> GetDocumentInfo(string documentId)
        {
            AccelaDocumentsBO mAccelaDocumentsBO = new AccelaDocumentsBO();

            try
            {
                var docInfoResult = await mAccelaDocumentsBO.TaskGetDocumentInfo(documentId);

                return docInfoResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// GetDocumentById
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<DocumentStatusWrapperBE> GetDocumentDownLoadById(string documentId)
        {

            try
            {
                AccelaDocumentsBO mAccelaDocumentsBO = new AccelaDocumentsBO();
                var result = await mAccelaDocumentsBO.TaskGetDocumentDownLoad(documentId);

                DocumentStatusWrapperBE mdocBE = new DocumentStatusWrapperBE(result.Status, result.FilePath)
                {
                    DocumentID = documentId
                };
                return mdocBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        ///  UpLoadDocument
        /// </summary>
        /// <param name="RecordId"></param>
        /// <param name="FilePath"></param>
        /// <param name="Category"></param> 
        /// <param name="Group"></param>
        /// <returns></returns>
        public async Task<DocumentChangeResultModelBE> UpLoadDocument(string RecordId, string FilePath, string Description, string Category, string Group)
        {
            AccelaRecordBO mAccelaDocumentsBO = new AccelaRecordBO();

            var result = await mAccelaDocumentsBO.TaskUpLoadDocument(RecordId, FilePath, Description, Category, Group);

            DocumentChangeResultModelBE mResult =
                JsonConvert.DeserializeObject<DocumentChangeResultModelBE>(JsonConvert.SerializeObject(result.Result));

            return mResult;


        }

        /// <summary>
        ///  UpDateDocument
        /// </summary>
        /// <param name="DocumentId"></param>
        /// <param name="FilePath"></param>
        /// <param name="upDateParms"></param>
        /// <returns></returns>
        public async Task<DocumentWrapperBE> UpDateDocument(string DocumentId, string FilePath, DocumentModelBE upDateParms)
        {

            AccelaDocumentsBO mAccelaDocumentsBo = new AccelaDocumentsBO();

            var mResult = await mAccelaDocumentsBo.TaskUpDateDocumentByDocumentId(DocumentId, FilePath, upDateParms);

            return mResult;
        }

        /// <summary>
        /// DeleteRecordDocumentsByIDS
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="documentIds"></param>
        /// <returns></returns>
        public async Task<List<ResponseResultModelArrayBE>> DeleteRecordDocumentsByIds(string recordId, string documentIds)
        {
            /// get a list ofd documents for the RecordId
            AccelaDocumentsBO mAccelaDocumentsBo = new AccelaDocumentsBO();

            var mvarresults = await mAccelaDocumentsBo.TaskGetRecordDocuments(recordId);

            DocumentUpDateModelWrapperBE mDeletedResult = new DocumentUpDateModelWrapperBE();

            List<ResponseResultModelArrayBE> mResponseResultModelArrayBE = new List<ResponseResultModelArrayBE>();


            foreach (var mTestResult in mvarresults.RecordDocuments)
            {

                string[] founddocids = mTestResult.EntityId.Split(',');

                var mDocIdDelete = JsonConvert.SerializeObject(founddocids);

                var mresult = await mAccelaDocumentsBo.TaskDeleteRecordDocumentsByIDS(recordId, mDocIdDelete);

                mResponseResultModelArrayBE.Add(mresult);

            }

            return mResponseResultModelArrayBE;
        }

        #endregion

        #region Conditons
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="mRecConditions"></param>
        /// <returns></returns>
        public async Task<ResponseResultModelArrayBE> CreateRecordConditions(string recordId, RecordConditionsInsertBE mRecConditions)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            new AIONEngineCrudApiBO().InsertNewAIONAccelaOutBoundLog(new AIONOutBoundLog(recordId, "CreateRecordConditions(string recordId, RecordConditionsInsertBE mRecConditions)", JsonConvert.SerializeObject(mRecConditions)));




            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var result = await mAccelaRecordBO.TaskCreateRecordCondition(recordId, mRecConditions);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="recConditions"></param>
        /// <returns></returns>
        public async Task<RecordConditionModelBE> UpDateRecordCondition(string recordId,
            RecordConditionModelBE recConditions)
        {

            var methodBase = MethodBase.GetCurrentMethod();
            new AIONEngineCrudApiBO().InsertNewAIONAccelaOutBoundLog(new AIONOutBoundLog(recordId, "UpDateRecordCondition(string recordId,RecordConditionModelBE recConditions)", JsonConvert.SerializeObject(recConditions)));


            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var result = await mAccelaRecordBO.TaskUpDateRecordCondition(recordId, recConditions);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="mRecConditonIds"></param>
        /// <returns></returns>
        public async Task<ResponseResultModelArrayBE> DeleteRecordConditions(string recordId, string mRecConditonIds)
        {
            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var result = await mAccelaRecordBO.TaskDeleteRecordConditions(recordId, mRecConditonIds);

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<ResponseRecordConditionModelArrayBE> GetRecordConditions(string recordId)
        {
            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var result = await mAccelaRecordBO.TaskGetRecordConditions(recordId);

            return result;
        }
        #endregion

        #region Records

        public async Task<ResponseSimpleRecordModelBE> CreateNewAccelaRecordInit(RequestRecordModelBE newRecordBody)
        {
            // Task<ResponseSimpleRecordModelBE> TaskCreatePartialAccelaRecord(RequestRecordModelBE newRecordBody);
            // 

            AccelaRecordCreationBO mAccelaRecordBO = new AccelaRecordCreationBO();

            var result = mAccelaRecordBO.TaskCreatePartialAccelaRecord(newRecordBody);
            result.Wait();
            var mNewRecordResult = result.Result;

            return mNewRecordResult;



        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mParms"></param>
        /// <returns></returns>
        public async Task<RecordWrapperBE> GetRecordsSearch(RecordSearchParametersBE mParms)
        {
            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var mRecordsList = await mAccelaRecordBO.TaskGetRecordsSearch(mParms);

            return mRecordsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="CustomTableName"></param>
        /// <returns></returns>
        public async Task<ResponseCustomFormSubgroupModelArrayBE> GetRecordRecordCustomTableMeta(string recordId,
            string CustomTableName)
        {
            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var result = await mAccelaRecordBO.TaskGetRecordRecordCustomTableMeta(recordId, CustomTableName);
            return result;
        }

        /// <summary>
        /// GetSettingsCustomForms(string recordTypeName)
        /// </summary>
        /// <param name="recordTypeName"></param>
        /// <returns></returns>
        public async Task<ResponseCustomFormMetadataModelArrayBE> GetSettingsCustomForms(string recordTypeName)
        {
            AccelaSettingsBO mAccelaSettingsBO = new AccelaSettingsBO();

            //  Task<ResponseCustomFormMetadataModelArrayBE> TaskGetCustomFormsByRecordType(string RecordTypeName)

            var result = await mAccelaSettingsBO.TaskGetCustomFormsByRecordType(recordTypeName);

            return result;
        }

        /// <summary>
        /// GetRecordSettingsCustomForms(string RecordTypeName)
        /// </summary>
        /// <param name="RecordTypeName"></param>
        /// <returns></returns>
        public async Task<ResponseCustomTableMetadataModelArrayBE> GetRecordSettingsCustomTables(string recordTypeName)
        {
            AccelaSettingsBO mAccelaSettingsBO = new AccelaSettingsBO();

            //Task<ResponseCustomTableMetadataModelArrayBE> TaskGetCustomTablesByRecordType(string RecordTypeName)

            var result = await mAccelaSettingsBO.TaskGetCustomTablesByRecordType(recordTypeName);

            return result;
        }

        /// <summary>
        /// GetAccelaRecord data  - ****  This is for reporting and review  only. ***
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns>AccelaRecordDocumentBE</returns>
        public async Task<AccelaRecordModel> GetAccelaRecord(string recordId, bool traceData = false)
        {
            try
            {
                // This does everything needed to get a token

                AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

                var mAccelaRecordDetail = Task.Run(() => mAccelaRecordBO.TaskGetAccelaRecordAll(recordId, traceData));

                return mAccelaRecordDetail.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// GetAccelaRecordWorkFlowTask ***** This is to be used for AION Estimation and processing. 
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns>AccelaRecordDocumentBE</returns>
        public async Task<AccelaTableResult> GetAccelaRecordWorkFlowTask(string recordId)
        {
            try
            {
                // This does everything needed to get a token

                AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

                AccelaTableResult mAccelaRecordModel = new AccelaTableResult();


                mAccelaRecordModel = await mAccelaRecordBO.TaskGetAccelaRecordWorkTasks(recordId);

                return mAccelaRecordModel;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<string> GetAccelaRecordWorkFlowTaskJson(string recordId)
        {
            try
            {
                // This does everything needed to get a token

                AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

                var result = await mAccelaRecordBO.TaskGetAccelaRecordWorkTasksJson(recordId);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<TaskReview> GetRecordWorkFlowTasksWorkFlowTaskInfo(string recordId, TaskInfo taskInfo)
        {
            AccelaRecordBO thisengine = new AccelaRecordBO();
            AccelaWorkTaskCustomFormParser mTaskParser = new AccelaWorkTaskCustomFormParser();

            var workFlowTask = Task.Run(() => thisengine.TasksGetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(recordId, taskInfo.TaskId));

            var workFlowTaskDetail = workFlowTask.Result;

            TaskReview taskReview = new TaskReview();

            taskReview = mTaskParser.ParseWorkFlowTask(recordId, taskInfo, workFlowTaskDetail);

            return taskReview;
        }

        public async Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(string recordId,
            string WorkFlowTaskid)
        {
            AccelaRecordBO thisengine = new AccelaRecordBO();
            WorkTaskCustForms mWorkTaskCustForms = new WorkTaskCustForms();
            AccelaWorkTaskCustomFormParser mFeeParser = new AccelaWorkTaskCustomFormParser();

            var workFlowFeeDetailResult = Task.Run(() => thisengine.TasksGetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(recordId, WorkFlowTaskid));

            var workFlowFeeDetail = workFlowFeeDetailResult.Result;

            mWorkTaskCustForms = mFeeParser.ParseWorkFlowCustomForms(mWorkTaskCustForms, recordId, workFlowFeeDetail);

            return mWorkTaskCustForms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustForms(string recordId, string projectId)
        {

            int Line = 0;
            try
            {
                AccelaRecordBO thisengine = new AccelaRecordBO();

                // Get Work flow history 
                Line = 1;
                var resultWorkFlow = thisengine.TaskGetRecordsRecordIdWorkflowTasksHistories(recordId);
                resultWorkFlow.Wait();
                Line = 2;
                // call parseing to reduce to required values 

                AccelaWorkTaskCustomFormParser mFeeParser = new AccelaWorkTaskCustomFormParser();
                Line = 3;
                var workFlowTaskDetailResult = mFeeParser.GetWorkTaskCustomFormsList(resultWorkFlow.Result);

                var workFlowTaskDetail = workFlowTaskDetailResult;
                Line = 4;
                List<FieldsOfFeeDetail> workFlowFeeDetails = new List<FieldsOfFeeDetail>();
                Line = 5;
                WorkTaskCustForms mWorkTaskCustForms = new WorkTaskCustForms();
                Line = 6;

                //if (workFlowTaskDetail.AANHolds.Count > 0)
                //{
                //    Line = 7;
                //    mWorkTaskCustForms.AANHolds = workFlowTaskDetail.AANHolds;
                //}

                // Added for AION to pull Comments for meeting attendees 

                if (resultWorkFlow.Result.Contains("Comments"))
                {
                    // list of RecordWorkFlowHistoryBE

                    var tWorkFlowHistoryForComments = JsonConvert.DeserializeObject<List<RecordWorkFlowHistoryBE>>(resultWorkFlow.Result);

                    foreach (var twfdetail in tWorkFlowHistoryForComments)
                    {

                        Console.WriteLine(" comment's");
                    }

                }

                mWorkTaskCustForms.stageMeetingsDetails = workFlowTaskDetail.MeetingTasks;


                Line = 8;
                if (workFlowTaskDetail.Results.Count > 0)
                {
                    Line = 9;
                    foreach (var tWorkFlowTask in workFlowTaskDetail.Results)
                    {

                        Line = 10;
                        // for each record then call to WorkFlow Custom Form 
                        Line = 11;
                        string workFlowTaskId = string.Empty;
                        Line = 12;
                        var workFlowFeeDetailResult = thisengine.TasksGetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(recordId, tWorkFlowTask.id);
                        workFlowFeeDetailResult.Wait();
                        Line = 13;
                        var workFlowFeeDetail = workFlowFeeDetailResult.Result;
                        Line = 14;
                        mWorkTaskCustForms = mFeeParser.ParseWorkFlowCustomForms(mWorkTaskCustForms, recordId, workFlowFeeDetail, tWorkFlowTask.description, projectId);
                    }
                }

                Line = 15;
                return mWorkTaskCustForms;
            }
            catch (Exception ex)
            {
                LoggingWrapper mLoggingControl = new LoggingWrapper();

                mLoggingControl.BLLogMessage(MethodBase.GetCurrentMethod(), " Line = " + Line.ToString() + "-" + ex.Message, ex);

                throw new Exception(MethodBase.GetCurrentMethod() + "- Line = " + Line.ToString() + "-" + ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// GetAccelaWorkFlowREcords used to facitate billing. 
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns>AccelaRecordDocumentBE</returns>
        public async Task<string> GetAccelaWorkFlowHistoryforRecord(string recordId)
        {
            try
            {
                // This does everything needed to get a token

                AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

                return await mAccelaRecordBO.TaskGetAccelaWorkFlowHistoryforRecord(recordId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<string> GetAccelaWorkFlowHistoryObjectForRecord(string recordId)
        {
            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

            var result = await mAccelaRecordBO.TaskGetRecordsRecordIdWorkflowTasksHistories(recordId);

            return result;
        }

        public async Task<ResponseRecordRelatedModelArrayBE> GetRelatedRecordInfo(string recordId,
            RecordRelatedModelBE.RelationshipEnum relationShip)
        {
            try
            {
                AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();

                var result = Task.Run(() => mAccelaRecordBO.TaskGetRelatedRecordInfo(recordId, relationShip));

                return result.Result;
            }
            catch (Exception ex)
            {
                throw new Exception(" Unable to access Accela Record Relationships " + ex.Message, ex.InnerException);

            }
        }


        /// <summary>
        /// UpDateRecord
        /// </summary>
        /// <param name="mRecordId"></param>
        /// <param name="mUpdateRecordDetail"></param>
        /// <returns></returns>
        public async Task<SimpleRecordModelBE> UpDateRecord(string mRecordId, RecordBE mUpdateRecordDetail)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            new AIONEngineCrudApiBO().InsertNewAIONAccelaOutBoundLog(new AIONOutBoundLog(mRecordId, "UpDateRecord(string mRecordId, RecordBE mUpdateRecordDetail)", JsonConvert.SerializeObject(mUpdateRecordDetail)));


            AccelaRecordBO mRecBE = new AccelaRecordBO();

            var UpdateResult = await mRecBE.TaskUpDateRecord(mRecordId, mUpdateRecordDetail);

            var mSimpleRecordModel = JsonConvert.DeserializeObject<SimpleRecordModelBE>(UpdateResult.ToJson());

            return mSimpleRecordModel;
        }

        /// <summary>
        ///  UpDateRecordWorkFlowItem(AccelaWorkFlowTaskUpdate mWorkFlowTaskUpdate)
        /// </summary>
        /// <param name="taskUpdatedDaModelData">RequestCustomTablesTasksBE</param>
        /// <returns></returns>
        public async Task<List<string>> UpDateRecordCustomTables(RequestCustomTablesTasksBE taskUpdatedDaModelData)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            new AIONEngineCrudApiBO().InsertNewAIONAccelaOutBoundLog(new AIONOutBoundLog(taskUpdatedDaModelData.recordId, "UpDateRecordCustomTables(RequestCustomTablesTasksBE taskUpdatedDaModelData)", JsonConvert.SerializeObject(taskUpdatedDaModelData)));


            AccelaRecordBO mRecBO = new AccelaRecordBO();

            var result = Task.Run(() => mRecBO.TaskUpDateRecordCustomTables(taskUpdatedDaModelData));

            return result.Result;
        }

        public async Task<List<string>> UpDateAccelaTasksRecords(AccelaCustomTableTaskUpDateModelBE tasksUpDateDetails)
        {
            var methodBase = MethodBase.GetCurrentMethod();

            new AIONEngineCrudApiBO().InsertNewAIONAccelaOutBoundLog(new AIONOutBoundLog(tasksUpDateDetails.recordId, "UpDateAccelaTasksRecords(AccelaCustomTableTaskUpDateModelBE tasksUpDateDetails)", JsonConvert.SerializeObject(tasksUpDateDetails)));


            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

            foreach (var taskdetail in tasksUpDateDetails.custTableTaskUpdateModel)
            {
                //    ResponseTaskItemModelBE UpDateRecordWorkFlowItem(AccelaWorkFlowTaskUpdate mWorkFlowTaskUpdate);

                List<TableFieldBE> mFieldBE = new List<TableFieldBE>();
                mFieldBE.Add(new TableFieldBE(null, "id", taskdetail.id));
                mFieldBE.Add(new TableFieldBE(null, "Task Type", taskdetail.TaskType));
                mFieldBE.Add(new TableFieldBE(null, "Task Name", taskdetail.TaskName));
                mFieldBE.Add(new TableFieldBE(null, "Pool Review", taskdetail.PoolReview));
                mFieldBE.Add(new TableFieldBE(null, "Assignee", taskdetail.Assignee));
                mFieldBE.Add(new TableFieldBE(null, "Due Date", taskdetail.DueDate));
                mFieldBE.Add(new TableFieldBE(null, "Cycle #", taskdetail.CycleCount));
                mFieldBE.Add(new TableFieldBE(null, "StartDate", taskdetail.StartDate));
                mFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", taskdetail.EstimatedReviewTime));
                mFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", taskdetail.DateTimeStamp));
                mFieldBE.Add(new TableFieldBE(null, "Processing Status", taskdetail.ProcessingStatus));
                mFieldBE.Add(new TableFieldBE(null, "Comments", taskdetail.Comments));
                TableRowsBE mTableRowsBE =
                    new TableRowsBE(taskdetail.id, TableRowBE.ActionEnum.Add, mFieldBE);
                // Add next set of fields  
                // finalize all fields into the array of Custom tables object. 
                mRequestCustomTablesTasksBe.array.Add(mTableRowsBE);
            }
            mRequestCustomTablesTasksBe.recordId = tasksUpDateDetails.recordId;

            // TestDataRecordUpdate mTestDataRecordUpdate = new TestDataRecordUpdate();

            var result = UpDateRecordCustomTables(mRequestCustomTablesTasksBe);
            result.Wait();
            var response = result.Result;
            return response;
        }


        public async Task<List<RecordContactSimpleModelBO>> GetAccelaRecordContacts(string recordId)
        {

            AccelaRecordBO mRecBO = new AccelaRecordBO();

            // Task<List<ResponseRecordContactSimpleModelArrayBO>> TaskGetAccalRecorcdContacts(string recordId)
            try
            {

                var result = await mRecBO.TaskGetAccelaRecordContacts(recordId);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" Unable to access Accela Record Contacts " + ex.Message, ex.InnerException);

            }
        }


        public async Task<string> GetContactCustomForm(string recordId,
            long contactId)
        {
            //  ask<ResponseCustomAttributeModelArrayBE> TaskGetContactCustomForm(string recordId, long contactId)
            AccelaRecordBO mRecBO = new AccelaRecordBO();

            var result = await mRecBO.TaskGetContactCustomForm(recordId, contactId);

            return result;
        }


        #endregion

        #region Settings 
        /// <summary>
        /// GetDocumentFolders
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> GetDocumentGroups()
        {
            AccelaSettingsBO mSettingsBO = new AccelaSettingsBO();

            return await mSettingsBO.TaskGetAllDocumentGroups();
        }

        /// <summary>
        ///  GetDocumentCategories
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> GetDocumentCategories()
        {
            AccelaSettingsBO mSettingsBO = new AccelaSettingsBO();

            return await mSettingsBO.TaskGetAllCategories();
        }
        /// <summary>
        ///  GetAAgencyList
        /// </summary>
        /// <returns></returns>
        public async Task<DepartmentWrapperBE> GetAllAgencyList()
        {
            try
            {
                // This does everything needed to get a token.  

                AccelaSettingsBO mSettings = new AccelaSettingsBO();

                var SettingsDepartmentResult = await mSettings.TaskGetAllDepartmentsList();

                return SettingsDepartmentResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// GetAllTradesNameList
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> GetAllTradesNameList()
        {
            AccelaSettingsBO mSettings = new AccelaSettingsBO();

            var result = await mSettings.TaskGetAllTradesNameList();

            return result;
        }

        /// <summary>
        /// GetDepartments 
        /// </summary>
        /// <returns>DepartmentWrapperBE</returns>
        public async Task<DepartmentWrapperBE> GetAllDepartments()
        {
            AccelaSettingsBO mAccelaSettingsBO = new AccelaSettingsBO();
            var result = await mAccelaSettingsBO.TaskGetAllDepartmentsList();
            return result;
        }


        /// <summary>
        /// GetAllProfessionalLicenseTypes
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> GetAllProfessionalLicenseTypes()
        {
            AccelaSettingsBO mAccelaSettingsBO = new AccelaSettingsBO();

            var SettingsResult = await mAccelaSettingsBO.TaskGetAllProfessionalLicenseTypes();

            return SettingsResult;
        }

        #endregion


        public async Task<string> GetAccelaAddress(string projectnumber)
        {
            AccelaAddressBO mAddressBO = new AccelaAddressBO();

            var result = await mAddressBO.TaskGetAccelaAddress(projectnumber);

            return result;

        }


        #region utility functions 


        private String ObjectToString(object obj)
        {
            try
            {
                Dictionary<string, object> recDictionary;

                recDictionary = DictionaryFromType(obj);

                StringBuilder sb = new StringBuilder();

                //object accountidkey = null;

                //Object DocfieldValue = null;

                int intcnt = 0;

                foreach (KeyValuePair<string, object> okey in recDictionary)
                {
                    intcnt++;
                    // recDictionary.TryGetValue(okey.Key, out DocfieldValue);


                    if (okey.Value != null)
                    {
                        sb.Append(okey.Key.ToLower() + "=" + okey.Value);
                    }

                    if (intcnt < recDictionary.Count && okey.Value != null)
                    {
                        sb.Append("&");
                    }

                }

                return sb.ToString().Substring(0, (sb.ToString().Length - 1));



                //   return obj != null ? JsonConvert.SerializeObject(obj) : null;


            }
            catch (Exception e)
            {
                throw new ApiExceptionBO.ApiException(500, e.Message);
            }
        }


        private static Dictionary<string, object> DictionaryFromType(object atype)
        {
            if (atype == null) return new Dictionary<string, object>();
            System.Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(atype, new object[] { });
                dict.Add(prp.Name, value);
            }

            return dict;
        }


        #endregion


    }
}
