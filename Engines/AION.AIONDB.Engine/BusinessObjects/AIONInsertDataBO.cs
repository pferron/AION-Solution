using AION.Base;
using Meck.Data;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AION.AIONDB.Engine.BusinessObjects
{
    public partial class AIONInsertDataBO : BaseBO
    {
        Logger _mLogger = new Logger();

        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }
        }

        private string ErrCode = string.Empty;

        private string GetConfigValue(string settingName)
        {
            string settingValue = string.Empty;
            try
            {
                settingValue = ConfigurationManager.AppSettings[settingName];
            }
            catch (ConfigurationErrorsException exConfig)
            {

                // config value not in Web.Config 
                settingValue = "false";
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    "Missing app setting during start :" + settingName + " in Config file.", ex: exConfig));
            }

            return settingValue;
        }


        private void LogRunTime(MethodBase source, string sqlcode, long TimeElasped)
        {
            string message = "SP Execution Time: " + TimeElasped +
                             " milliseconds for " + sqlcode + " to complete";
            Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.ExecutionTime, source, message, ex: null));
        }

        public AIONRecordQueueResponse TaskInsertNewAIONRecord(RecordNotification inComingRecordData)
        {
            StringBuilder sbError = new StringBuilder();

            AIONRecordQueueResponse mQueueResponse = new AIONRecordQueueResponse();

            AIONQueueRecordBE recInfo = new AIONQueueRecordBE();

            string sql = "usp_insert_aion_accela_received_records_getnewrow";

            var receivedrecord = inComingRecordData;

            bool mStopWatchTimerAIONInsert = false;

            Stopwatch mStopWatch = new Stopwatch();

            var timerEnable = GetConfigValue("InsertNewAIONRecordTimer");

            string currentRecord = @"InsertAionRecord -" + inComingRecordData.recordID + "-";

            try
            {
                if (timerEnable.ToUpper() == "TRUE")
                {
                    mStopWatchTimerAIONInsert = true;

                    mStopWatch.Start();
                }

                ErrCode = "DB1";

                System.Type t = inComingRecordData.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name == "EstimatedRereviewHours")
                    {
                        ErrCode = "DB2";
                        if (inComingRecordData.EstimatedRereviewHours != null)
                        {
                            decimal decimalResult;

                            if (!Decimal.TryParse(inComingRecordData.EstimatedRereviewHours.ToString(),
                                out decimalResult))
                            {
                                sbError.Append("EstimatedRereviewHours is not a Decimal.");
                            }

                            ErrCode = "DB2.1";
                        }
                    }
                }

                ErrCode = "DB3";
                string reEstimateTime;
                if (receivedrecord.EstimatedRereviewHours == null)
                {
                    reEstimateTime = "0";
                }
                else
                {
                    reEstimateTime = receivedrecord.EstimatedRereviewHours;
                }

                SqlParameter[] sqlParm = new SqlParameter[11];

                sqlParm[0] = new SqlParameter("@REC_ID_NUM", receivedrecord.recordID);
                sqlParm[1] = new SqlParameter("@REC_TYP_DESC", receivedrecord.recordtype);
                sqlParm[2] = new SqlParameter("@STATUS_DESC", receivedrecord.status);
                sqlParm[3] = new SqlParameter("@WORKSTEP_ID_NUM", receivedrecord.WorkFlowStepId);
                sqlParm[4] = new SqlParameter("@WORKFLOW_TASK_NM", receivedrecord.WorkFlowTaskName);
                sqlParm[5] = new SqlParameter("@WORKFLOW_TASK_STATUS", receivedrecord.WorkFlowStatus);
                sqlParm[6] = new SqlParameter("@ESTIMATED_REREVIEW_HOURS_NBR", reEstimateTime);
                sqlParm[7] = new SqlParameter("@PROCESS_STATUS_DESC", "Received");
                sqlParm[8] = new SqlParameter("@WKR_ID_CREATED_TXT", "API");
                sqlParm[9] = new SqlParameter("@WKR_ID_UPDATED_TXT", "API");
                sqlParm[10] = new SqlParameter("@ReturnValue", 0);

                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref sqlParm);

                while (result.Read())
                {
                    ErrCode = "Db6";
                    recInfo.ACCELA_RECEIVED_REC_QUEUE_ID =
                        result.GetInt32(result.GetOrdinal("ACCELA_RECEIVED_REC_QUEUE_ID"));
                    recInfo.REC_ID_NUM = result.GetString(result.GetOrdinal("REC_ID_NUM"));
                    recInfo.REC_TYP_DESC = result.GetString(result.GetOrdinal("REC_TYP_DESC"));
                    recInfo.STATUS_DESC = result.GetString(result.GetOrdinal("STATUS_DESC"));

                    if (result.IsDBNull(result.GetOrdinal("WORKSTEP_ID_NUM")))
                    {
                        recInfo.WORKSTEP_ID_NUM = null;
                    }
                    else
                    {
                        recInfo.WORKSTEP_ID_NUM = result.GetString(result.GetOrdinal("WORKSTEP_ID_NUM"));
                    }

                    if (result.IsDBNull(result.GetOrdinal("WORKFLOW_TASK_NM")))
                    {
                        recInfo.WORKFLOW_TASK_NM = null;
                    }
                    else
                    {
                        recInfo.WORKFLOW_TASK_NM = result.GetString(result.GetOrdinal("WORKFLOW_TASK_NM"));
                    }

                    if (result.IsDBNull(result.GetOrdinal("WORKFLOW_TASK_STATUS")))
                    {
                        recInfo.WORKFLOW_TASK_STATUS = null;
                    }
                    else
                    {
                        recInfo.WORKFLOW_TASK_STATUS = result.GetString(result.GetOrdinal("WORKFLOW_TASK_STATUS"));
                    }

                    recInfo.RECEIVED_DT = result.GetDateTime(result.GetOrdinal("RECEIVED_DT"));
                    recInfo.LAST_PROCESSING_DT = result.GetDateTime(result.GetOrdinal("LAST_PROCESSING_DT"));
                    recInfo.PROCESS_STATUS_DESC = result.GetString(result.GetOrdinal("PROCESS_STATUS_DESC"));
                    recInfo.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    recInfo.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    recInfo.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    recInfo.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));
                    recInfo.EstimatedRereviewHours =
                        result.GetDecimal(result.GetOrdinal("ESTIMATED_REREVIEW_HOURS_NBR"));
                }

                ErrCode = "Db7";
                mQueueResponse.errors = sbError.ToString();
                mQueueResponse.newRecordSource = receivedrecord;
                mQueueResponse.CurrentRecord = recInfo;

                ErrCode = "Db8";
                if (mStopWatchTimerAIONInsert)
                {
                    mStopWatch.Stop();
                    var mStopWatchDuration = mStopWatch.ElapsedMilliseconds;
                    LogRunTime(MethodBase.GetCurrentMethod(), sql, mStopWatch.ElapsedMilliseconds);
                }

                return mQueueResponse;

            }
            catch (Exception ex)
            {
                if (mStopWatchTimerAIONInsert)
                {
                    mStopWatch.Stop();
                }

                sbError.Append(ex.Message);

                string errmsg = ex.Message + "method section: " + ErrCode;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errmsg, ex: ex));
            }
            mQueueResponse.errors = sbError.ToString();
            mQueueResponse.newRecordSource = receivedrecord;
            mQueueResponse.CurrentRecord = recInfo;
            return mQueueResponse;


        }

        public List<AIONQueueRecordBE> TaskGetNewAIONRecordsToProcess(string status = "", int offsetMinutes = 0)
        {
            // return object 
            List<AIONQueueRecordBE> mQueRecs = new List<AIONQueueRecordBE>();

            // create data extract
            try
            {
                string sql = "[AION].[usp_select_aion_accela_received_records_queue_for_processing]";
                SqlParameter[] sqlParm = new SqlParameter[2];

                sqlParm[0] = new SqlParameter("@PROCESS_STATUS_DESC", status);
                sqlParm[1] = new SqlParameter("@OFFSET_MINS", offsetMinutes);

                var result = SqlWrapper.RunSPReturnDS(sql, Globals.AIONConnectionString, ref sqlParm);

                foreach (DataRow dataRow in result.Tables[0].Rows)
                {
                    mQueRecs.Add(this.ConvertDataRowToBE(dataRow));
                }

                return mQueRecs;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }

        public DataTable TaskSelectAccelaAionMapDataTableByAccerlaRecordType(string recordType)
        {
            try
            {
                string sql = "usp_select_aion_accela_aion_map_by_accela_rec_type_nm";

                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();

                // get a db connection 

                var table = new DataTable();

                SqlParameter[] sqlParm = { new SqlParameter("@recType", recordType) };

                dataSet = SqlWrapper.RunSPReturnDS(sql, Globals.AIONConnectionString, ref sqlParm);

                dataTable = dataSet.Tables[0];

                return dataTable;
            }

            catch (Exception ex)
            {
                string errmsg = ex.Message;
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inComingRecordData"></param>
        /// <returns></returns>
        public AIONPlanReviewHistoryResponse TaskInsertNewAIONPlanReviewHistoryRecord(PlanReviewHistory inComingRecordData)
        {
            var receivedrecord = inComingRecordData;

            AIONPlanReviewHistoryResponse mPlanReviewHistoryResponse = new AIONPlanReviewHistoryResponse();
            PlanReviewHistoryAllFields mPlanReviewHistory = new PlanReviewHistoryAllFields();


            bool mStopWatchTimerAIONInsert = false;

            Stopwatch mStopWatch = new Stopwatch();

            var timerEnable = GetConfigValue("InsertNewAIONPlanRecordHistoryTimer");
            if (timerEnable.ToUpper() == "TRUE")
            {
                mStopWatchTimerAIONInsert = true;

                mStopWatch.Start();
            }

            StringBuilder sbError = new StringBuilder();

            string currentRecord = "InsertNewAIONPlanReviewHistoryRecord -" + receivedrecord.REC_ID_NUM;


            try
            {
                var inputrecord = JsonConvert.SerializeObject(inComingRecordData);

                ErrCode = "Db1";

                if (receivedrecord.REC_ID_NUM is null)
                {
                    sbError.Append("REC_ID_NUM is null");
                    throw new Exception("REC_ID_NUM is null");
                }

                if (String.IsNullOrWhiteSpace(receivedrecord.PLAN_REVIEW_START_DT.ToString()))
                {
                    sbError.Append("PLAN_REVIEW_START_DT is null");
                    throw new Exception("PLAN_REVIEW_START_DT is null");
                }

                if (string.IsNullOrWhiteSpace(receivedrecord.PLAN_REVIEW_END_DT.ToString()))
                {
                    sbError.Append("PLAN_REVIEW_END_DT is null");

                    throw new Exception("PLAN_REVIEW_END_DT is null");
                }

                string sql = "usp_insert_aion_plan_review_history_getnewrow";

                ErrCode = "Db2";

                SqlParameter[] sqlParm = new SqlParameter[9];


                sqlParm[0] = new SqlParameter("@REC_ID_NUM", receivedrecord.REC_ID_NUM);
                sqlParm[1] = new SqlParameter("@ACCELA_CREATED_DT", receivedrecord.ACCELA_CREATED_DT.ToString());
                sqlParm[2] = new SqlParameter("@PLAN_REVIEW_START_DT", receivedrecord.PLAN_REVIEW_START_DT.ToString());
                sqlParm[3] = new SqlParameter("@PLAN_REVIEW_END_DT", receivedrecord.PLAN_REVIEW_END_DT.ToString());
                sqlParm[4] = new SqlParameter("@RECEIVED_DT", DateTime.Now.ToString());
                sqlParm[5] = new SqlParameter("@WKR_ID_CREATED_TXT", "API");
                sqlParm[6] = new SqlParameter("@WKR_ID_UPDATED_TXT", "API");
                sqlParm[7] = new SqlParameter("@ReturnValue", 0);

                ErrCode = "Db2.1";
                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref sqlParm);

                while (result.Read())
                {
                    mPlanReviewHistory.PLAN_REVIEW_HISTORY_ID =
                        result.GetInt32(result.GetOrdinal("PLAN_REVIEW_HISTORY_ID"));
                    mPlanReviewHistory.REC_ID_NUM = result.GetString(result.GetOrdinal("REC_ID_NUM"));
                    mPlanReviewHistory.ACCELA_CREATED_DT = result.GetDateTime(result.GetOrdinal("ACCELA_CREATED_DT"));
                    mPlanReviewHistory.PLAN_REVIEW_START_DT =
                        result.GetDateTime(result.GetOrdinal("PLAN_REVIEW_START_DT"));
                    mPlanReviewHistory.PLAN_REVIEW_END_DT = result.GetDateTime(result.GetOrdinal("PLAN_REVIEW_END_DT"));
                    mPlanReviewHistory.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mPlanReviewHistory.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mPlanReviewHistory.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mPlanReviewHistory.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));
                }

                if (mStopWatchTimerAIONInsert)
                {
                    mStopWatch.Stop();
                    var mStopWatchDuration = mStopWatch.ElapsedMilliseconds;
                    LogRunTime(MethodBase.GetCurrentMethod(), sql, mStopWatch.ElapsedMilliseconds);
                }

                mPlanReviewHistoryResponse.errors = sbError.ToString();
                mPlanReviewHistoryResponse.PlanReviewRecordSource = receivedrecord;
                mPlanReviewHistoryResponse.CurrentRecord = mPlanReviewHistory;

            }
            catch (Exception ex)
            {
                if (mStopWatchTimerAIONInsert)
                {
                    mStopWatch.Stop();
                }

                string errmsg = ex.Message;

                sbError.Append(errmsg);

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));
                throw new Exception(ex.Message);
            }

            mPlanReviewHistoryResponse.errors = sbError.ToString();
            mPlanReviewHistoryResponse.PlanReviewRecordSource = receivedrecord;
            mPlanReviewHistoryResponse.CurrentRecord = mPlanReviewHistory;

            return mPlanReviewHistoryResponse;
        }

        /// <summary>
        /// InsertNewAEData
        /// </summary>
        /// <param name="receivedRecord"></param>
        /// <returns></returns>
        public AIONAEDataResponse TaskInsertNewAEData(AccelaAIONAEData receivedRecord)
        {
            AIONAEDataResponse mAIONAEDataResponse = new AIONAEDataResponse();
            AccelaAIONAEData mAccelaAionAEData = new AccelaAIONAEData();

            Stopwatch mStopWatch = new Stopwatch();
            StringBuilder sbError = new StringBuilder();
            bool mStopWatchTimerAIONInsert = false;

            string currentRecord = "InsertNewAEData -" + receivedRecord.REC_ID_NUM;

            try
            {

                ErrCode = "Ddb1";

                var timerEnable = GetConfigValue("InsertNewAIONAEDataTimer");
                if (timerEnable.ToUpper() == "TRUE")
                {
                    mStopWatchTimerAIONInsert = true;

                    mStopWatch.Start();
                }

                string sql = "usp_insert_accela_ae_data_getnewrow"; //  "usp_insert_accela_ae_data";
                ErrCode = "DB1.1";

                System.Type t = receivedRecord.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetValue(receivedRecord) == null)
                    {
                        sbError.Append("Value for " + prop.Name + " is null, ");
                        throw new Exception("Value for " + prop.Name + " is null ");
                    }
                }

                if (sbError.Length == 0)
                {
                    // save the record and report it back. 

                    ErrCode = "DB2";

                    //  int returnitem = 0;

                    SqlParameter[] sqlParms = new SqlParameter[12];

                    sqlParms[0] = new SqlParameter("@SYSTEM_USER_NM", receivedRecord.SYSTEM_USER_NM);
                    sqlParms[1] = new SqlParameter("@REC_ID_NUM", receivedRecord.REC_ID_NUM);
                    sqlParms[2] = new SqlParameter("@PLAN_REVIEW_TYP_DESC", receivedRecord.PLAN_REVIEW_TYP_DESC);
                    sqlParms[3] = new SqlParameter("@CYCLE_NBR", receivedRecord.CYCLE_NBR);
                    sqlParms[4] = new SqlParameter("@LICENSE_TYP_DESC", receivedRecord.LICENSE_TYP_DESC);
                    sqlParms[5] = new SqlParameter("@PROJECT_SCORE_DESC", receivedRecord.PROJECT_SCORE_DESC);
                    sqlParms[6] = new SqlParameter("@PROJECT_CREATED_DTTM", receivedRecord.PROJECT_CREATED_DTTM);
                    sqlParms[7] = new SqlParameter("@PASS_FAIL_IND", receivedRecord.PASS_FAIL_IND);
                    sqlParms[8] = new SqlParameter("@FAILURE_CAUSE_TXT", receivedRecord.FAILURE_CAUSE_TXT);
                    sqlParms[9] = new SqlParameter("@FAILURE_REASON_TXT", receivedRecord.FAILURE_REASON_TXT);
                    sqlParms[10] = new SqlParameter("@WKR_ID_CREATED_TXT", receivedRecord.WKR_ID_CREATED_TXT);
                    sqlParms[11] = new SqlParameter("@ReturnValue", 0)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var reader = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref sqlParms);

                    while (reader.Read())
                    {


                        ErrCode = "Db6";
                        mAccelaAionAEData.ACCELA_AE_DATA_ID = reader.GetInt32(reader.GetOrdinal("ACCELA_AE_DATA_ID"));
                        mAccelaAionAEData.SYSTEM_USER_NM = reader.GetString(reader.GetOrdinal("SYSTEM_USER_NM"));
                        mAccelaAionAEData.REC_ID_NUM = reader.GetString(reader.GetOrdinal("REC_ID_NUM"));
                        mAccelaAionAEData.PLAN_REVIEW_TYP_DESC =
                            reader.GetString(reader.GetOrdinal("PLAN_REVIEW_TYP_DESC"));
                        mAccelaAionAEData.CYCLE_NBR = reader.GetInt32(reader.GetOrdinal("CYCLE_NBR"));
                        mAccelaAionAEData.LICENSE_TYP_DESC = reader.GetString(reader.GetOrdinal("LICENSE_TYP_DESC"));
                        mAccelaAionAEData.PROJECT_SCORE_DESC =
                            reader.GetString(reader.GetOrdinal("PROJECT_SCORE_DESC"));
                        mAccelaAionAEData.PROJECT_CREATED_DTTM =
                            reader.GetDateTime(reader.GetOrdinal("PROJECT_CREATED_DTTM"));
                        mAccelaAionAEData.PASS_FAIL_IND = reader.GetBoolean(reader.GetOrdinal("PASS_FAIL_IND"));
                        mAccelaAionAEData.FAILURE_CAUSE_TXT = reader.GetString(reader.GetOrdinal("FAILURE_CAUSE_TXT"));
                        mAccelaAionAEData.FAILURE_REASON_TXT =
                            reader.GetString(reader.GetOrdinal("FAILURE_REASON_TXT"));
                        mAccelaAionAEData.WKR_ID_CREATED_TXT =
                            reader.GetString(reader.GetOrdinal("WKR_ID_CREATED_TXT"));
                        mAccelaAionAEData.CREATED_DTTM = reader.GetDateTime(reader.GetOrdinal("CREATED_DTTM"));
                        mAccelaAionAEData.WKR_ID_UPDATED_TXT =
                            reader.GetString(reader.GetOrdinal("WKR_ID_UPDATED_TXT"));
                        mAccelaAionAEData.UPDATED_DTTM = reader.GetDateTime(reader.GetOrdinal("UPDATED_DTTM"));
                    }

                    if (mStopWatchTimerAIONInsert)
                    {
                        mStopWatch.Stop();
                        var mStopWatchDuration = mStopWatch.ElapsedMilliseconds;
                        LogRunTime(MethodBase.GetCurrentMethod(), sql, mStopWatch.ElapsedMilliseconds);
                    }

                    mAIONAEDataResponse.errors = sbError.ToString();
                    mAIONAEDataResponse.AionAERecord = receivedRecord;
                    mAIONAEDataResponse.CurrentRecord = mAccelaAionAEData;
                }
                return mAIONAEDataResponse;
            }
            catch (Exception ex)
            {
                if (mStopWatchTimerAIONInsert)
                {
                    mStopWatch.Stop();
                }

                sbError.Append(ex.Message);

                string detail = JsonConvert.SerializeObject(receivedRecord);
                string errmsg = ex.Message + "Data:" + detail;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errmsg, ex: ex));
            }
            mAIONAEDataResponse.errors = sbError.ToString();
            mAIONAEDataResponse.AionAERecord = receivedRecord;
            mAIONAEDataResponse.CurrentRecord = mAccelaAionAEData;
            return mAIONAEDataResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReceivedQueueId"></param>
        /// <returns></returns>
        public AIONQueueRecordBE TaskGetSpecificAIONQueueRecord(int ReceivedQueueId)
        {
            AIONQueueRecordBE mQueRec = new AIONQueueRecordBE();

            try
            {
                string sql = "[AION].[usp_select_aion_accela_received_records_queue_by_id]";

                SqlParameter[] SqlParm = { new SqlParameter("@QUEUE_ID", ReceivedQueueId) };

                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref SqlParm);

                while (result.Read())
                {
                    // Get the Record 
                    mQueRec.ACCELA_RECEIVED_REC_QUEUE_ID = result.GetInt32(result.GetOrdinal("ACCELA_RECEIVED_REC_QUEUE_ID"));
                    mQueRec.REC_ID_NUM = result.GetString(result.GetOrdinal("REC_ID_NUM"));
                    mQueRec.REC_TYP_DESC = result.GetString(result.GetOrdinal("REC_TYP_DESC"));
                    mQueRec.STATUS_DESC = result.GetString(result.GetOrdinal("STATUS_DESC"));
                    mQueRec.WORKSTEP_ID_NUM = result.GetString(result.GetOrdinal("WORKSTEP_ID_NUM"));
                    mQueRec.WORKFLOW_TASK_NM = result.GetString(result.GetOrdinal("WORKFLOW_TASK_NM"));
                    mQueRec.WORKFLOW_TASK_STATUS = result.GetString(result.GetOrdinal("WORKFLOW_TASK_STATUS"));
                    mQueRec.EstimatedRereviewHours = result.GetDecimal(result.GetOrdinal("ESTIMATED_REREVIEW_HOURS_NBR"));
                    mQueRec.RECEIVED_DT = result.GetDateTime(result.GetOrdinal("RECEIVED_DT"));
                    mQueRec.LAST_PROCESSING_DT = result.GetDateTime(result.GetOrdinal("LAST_PROCESSING_DT"));
                    mQueRec.PROCESS_STATUS_DESC = result.GetString(result.GetOrdinal("PROCESS_STATUS_DESC"));
                    mQueRec.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mQueRec.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mQueRec.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mQueRec.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));
                }

                return mQueRec;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReceivedQueueId"></param>
        /// <param name="NewProcessStatus"></param>
        /// <returns></returns>
        public int TaskUpDateQueueRecordStatus(int ReceivedQueueId, string NewProcessStatus)
        {
            try
            {
                string sql = "[AION].[usp_update_aion_accela_received_record_queue_processing_status]";

                SqlParameter[] sqlParm = new SqlParameter[3];

                sqlParm[0] = new SqlParameter("@ACCELA_RECEIVED_REC_QUEUE_ID", ReceivedQueueId);
                sqlParm[1] = new SqlParameter("@PROCESS_STATUS_DESC", NewProcessStatus);
                sqlParm[2] = new SqlParameter("@ReturnValue", 0);
                sqlParm[2].Direction = ParameterDirection.Output;

                var result = SqlWrapper.RunSPReturnInteger(sql, Globals.AIONConnectionString, ref sqlParm);

                return result;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));
                throw (ex);
            }
        }

        /// <summary>
        ///
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PlanReviewHistoryAllFields> TaskAuditPlanReviewRecords()
        {
            try
            {
                List<PlanReviewHistoryAllFields> mPlanReviewHistoryAllFields = new List<PlanReviewHistoryAllFields>();

                string sql = "Select top 5 * from Aion.Plan_Review_History  order by Created_dttm desc ";

                var result = SqlWrapper.RunSQLReturnReader(sql, Globals.AIONConnectionString);

                while (result.Read())
                {
                    PlanReviewHistoryAllFields mplanreviewhistory = new PlanReviewHistoryAllFields();

                    mplanreviewhistory.PLAN_REVIEW_HISTORY_ID =
                        result.GetInt32(result.GetOrdinal("PLAN_REVIEW_HISTORY_ID"));
                    mplanreviewhistory.REC_ID_NUM = result.GetString(result.GetOrdinal("REC_ID_NUM"));
                    mplanreviewhistory.ACCELA_CREATED_DT =
                        result.GetDateTime(result.GetOrdinal("ACCELA_CREATED_DT"));
                    mplanreviewhistory.PLAN_REVIEW_START_DT =
                        result.GetDateTime(result.GetOrdinal("PLAN_REVIEW_START_DT"));
                    mplanreviewhistory.PLAN_REVIEW_END_DT =
                        result.GetDateTime(result.GetOrdinal("PLAN_REVIEW_END_DT"));
                    mplanreviewhistory.ACCELA_CREATED_DT =
                        result.GetDateTime(result.GetOrdinal("ACCELA_CREATED_DT"));
                    mplanreviewhistory.WKR_ID_CREATED_TXT =
                        result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mplanreviewhistory.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mplanreviewhistory.WKR_ID_UPDATED_TXT =
                        result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mplanreviewhistory.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mPlanReviewHistoryAllFields.Add(mplanreviewhistory);
                }

                return mPlanReviewHistoryAllFields;
            }
            catch (Exception ex)
            {

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw new Exception(ex.Message);
            }
        }

        private AIONQueueRecordBE ConvertDataRowToBE(DataRow dataRow)
        {
            AIONQueueRecordBE AIONQueueRecordBE = new AIONQueueRecordBE();

            AIONQueueRecordBE.ACCELA_RECEIVED_REC_QUEUE_ID = TryToParse<int>(dataRow["ACCELA_RECEIVED_REC_QUEUE_ID"]);
            AIONQueueRecordBE.REC_ID_NUM = TryToParse<string>(dataRow["REC_ID_NUM"]);
            AIONQueueRecordBE.REC_TYP_DESC = TryToParse<string>(dataRow["REC_TYP_DESC"]);
            AIONQueueRecordBE.STATUS_DESC = TryToParse<string>(dataRow["STATUS_DESC"]);
            AIONQueueRecordBE.WORKSTEP_ID_NUM = TryToParse<string>(dataRow["WORKSTEP_ID_NUM"]);
            AIONQueueRecordBE.WORKFLOW_TASK_NM = TryToParse<string>(dataRow["WORKFLOW_TASK_NM"]);
            AIONQueueRecordBE.WORKFLOW_TASK_STATUS = TryToParse<string>(dataRow["WORKFLOW_TASK_STATUS"]);
            AIONQueueRecordBE.EstimatedRereviewHours =
                TryToParse<decimal?>(dataRow["ESTIMATED_REREVIEW_HOURS_NBR"]);
            AIONQueueRecordBE.RECEIVED_DT = TryToParse<DateTime?>(dataRow["RECEIVED_DT"]);
            AIONQueueRecordBE.LAST_PROCESSING_DT = TryToParse<DateTime?>(dataRow["LAST_PROCESSING_DT"]);
            AIONQueueRecordBE.PROCESS_STATUS_DESC = TryToParse<string>(dataRow["PROCESS_STATUS_DESC"]);
            AIONQueueRecordBE.WKR_ID_CREATED_TXT = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            AIONQueueRecordBE.CREATED_DTTM = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            AIONQueueRecordBE.WKR_ID_UPDATED_TXT = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            AIONQueueRecordBE.UPDATED_DTTM = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            return AIONQueueRecordBE;

        }

        public bool TaskInsertNewAIONAccelaOutBoundLog(AIONOutBoundLog logdata)
        {
            try
            {
                string sql = "[usp_insert_Aion_Accela_outbound_log]";

                SqlParameter[] sqlParm = new SqlParameter[6];

                sqlParm[0] = new SqlParameter("@ACCELA_REC_ID_NUM", logdata.ACCELA_REC_ID_NUM);
                sqlParm[1] = new SqlParameter("@ACTION_CAUSE_DESC", logdata.ACTION_CAUSE_DESC);
                sqlParm[2] = new SqlParameter("@ACTION_DETAIL_TXT", logdata.ACTION_DETAIL_TXT);
                sqlParm[3] = new SqlParameter("@PROCESS_DTTM", DateTime.Now);
                sqlParm[4] = new SqlParameter("@WKR_ID_CREATED_TXT", "Posse-Out");
                sqlParm[5] = new SqlParameter("@ReturnValue", 0);

                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref sqlParm);

                return true;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errmsg, ex: ex));
                return false;
            }
        }
    }
}


