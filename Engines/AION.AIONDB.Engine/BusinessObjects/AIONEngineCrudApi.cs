using AION.Base;
using Meck.Data;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AION.AIONDB.Engine.BusinessObjects
{
    public partial class AIONEngineCrudApiBO : BaseBO, IAIONDBEngine
    {

        public void InsertNewAIONAccelaOutBoundLog(AIONOutBoundLog logdata)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();
            mAccelaDBApi.TaskInsertNewAIONAccelaOutBoundLog(logdata);

            return;
        }


        public AIONRecordQueueResponse InsertNewAIONRecord(RecordNotification inComingRecordData)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var result = mAccelaDBApi.TaskInsertNewAIONRecord(inComingRecordData);

            return result;
        }

        public AIONPlanReviewHistoryResponse InsertNewAIONPlanReviewHistoryRecord(PlanReviewHistory inComingRecordData)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var result = mAccelaDBApi.TaskInsertNewAIONPlanReviewHistoryRecord(inComingRecordData);

            return result;
        }

        public bool DeleteAccelaToAionMap(string datavalue)
        {
            AIONMapBO mAIONEngine = new AIONMapBO();

            var result = Task.Run(() => mAIONEngine.TaskDeleteAccelaToAionMap(datavalue));
            result.Wait();

            return true;
        }

        public List<MeckAccelaDataMap> SelectPosseAccelaMapforFees()
        {
            PosseMapBO mPosseMapBO = new PosseMapBO();

            return mPosseMapBO.TaskSelectPosseAccelaMapforFees();
        }



        public bool DeleteRecordsAndResetTable(string sourceSystem)
        {
            AIONMapBO mAIONEngine = new AIONMapBO();

            var result = mAIONEngine.TaskDeleteRecordsAndResetTable(sourceSystem);

            return true;
        }

        public bool InsertAccelaAIONMapRecord(LoadAccelaAIONMap newRecord)
        {
            AIONMapBO mAIONEngine = new AIONMapBO();

            var result = mAIONEngine.TaskInsertAccelaAIONMapRecord(newRecord);

            return true;
        }

        public List<MeckAccelaDataMap> SelectAccelaAionMap()
        {
            AIONMapBO mAIONEngine = new AIONMapBO();

            var result = mAIONEngine.TaskSelectAccelaAionMap();

            return result;
        }

        public DataTable SelectAccelaAionMapDataTable()
        {
            AIONMapBO mAIONEngine = new AIONMapBO();

            var result = mAIONEngine.TaskSelectAccelaAionMapDataTable();

            return result;
        }

        public List<MeckAccelaDataMap> SelectAccelaAionMapByRecordType(string recordType)
        {
            AIONMapBO mAIONEngine = new AIONMapBO();

            var result = mAIONEngine.TaskSelectAccelaAionMapByRecordType(recordType);

            return result;

        }

        public DataTable SelectAccelaAionMapDataTableByAccerlaRecordType(string recordType)
        {
            string sql = "usp_select_aion_accela_aion_map_by_accela_rec_type_nm";

            DataTable dataTable = new DataTable();

            SqlParameter[] sqlParm = { new SqlParameter("@recType", recordType) };

            DataSet dataSet = SqlWrapper.RunSPReturnDS(sql, Globals.AIONConnectionString, ref sqlParm);

            dataTable = dataSet.Tables[0];

            return dataTable;

        }

        public AIONAEDataResponse InsertNewAEData(AccelaAIONAEData receivedRecord)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var result = mAccelaDBApi.TaskInsertNewAEData(receivedRecord);

            return result;
        }

        public List<AIONQueueRecordBE> GetNewAIONRecordsToProcess(string status = "", int offsetMinutes = 0)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var results = mAccelaDBApi.TaskGetNewAIONRecordsToProcess(status, offsetMinutes);

            return results;
        }

        public AIONQueueRecordBE GetSpecificAIONQueueRecord(int ReceivedQueueId)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var result = mAccelaDBApi.TaskGetSpecificAIONQueueRecord(ReceivedQueueId);

            return result;
        }

        public int UpDateQueueRecordStatus(int ReceivedQueueId, string NewProcessStatus)
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var result = mAccelaDBApi.TaskUpDateQueueRecordStatus(ReceivedQueueId, NewProcessStatus);

            return result;
        }

        public List<PlanReviewHistoryAllFields> AuditPlanReviewRecords()
        {
            AIONInsertDataBO mAccelaDBApi = new AIONInsertDataBO();

            var result = mAccelaDBApi.TaskAuditPlanReviewRecords();

            return result;
        }



        public bool UpDateAccelaToAionMap(AccelaAIONMap newRecord)
        {
            AIONMapBO mAccelaDBApi = new AIONMapBO();

            var result = mAccelaDBApi.TaskUpDateAccelaToAionMap(newRecord);

            return true;
        }

        public List<AIONQueueRecordBE> GetQueueRecordsByRecordId(string recordId)
        {
            List<AIONQueueRecordBE> mAccelaAIONQueueDataByRecordId = new List<AIONQueueRecordBE>();

            string sql = "[AION].[usp_select_aion_accela_received_records_queue_by_recordid]";

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@REC_ID_NUM", recordId);

            try
            {
                var result = SqlWrapper.RunSPReturnReader(sql, base.ConnectionString, ref sqlParameters);

                while (result.Read())
                {
                    AIONQueueRecordBE mAccelaAionQueueData = new AIONQueueRecordBE();

                    mAccelaAionQueueData.ACCELA_RECEIVED_REC_QUEUE_ID = result.GetInt32(result.GetOrdinal("ACCELA_RECEIVED_REC_QUEUE_ID"));
                    mAccelaAionQueueData.REC_ID_NUM = result.GetString(result.GetOrdinal("REC_ID_NUM"));
                    mAccelaAionQueueData.REC_TYP_DESC = result.GetString(result.GetOrdinal("REC_TYP_DESC"));
                    mAccelaAionQueueData.STATUS_DESC = result.GetString(result.GetOrdinal("STATUS_DESC"));
                    mAccelaAionQueueData.WORKSTEP_ID_NUM = result.GetString(result.GetOrdinal("WORKSTEP_ID_NUM"));
                    mAccelaAionQueueData.WORKFLOW_TASK_NM = result.GetString(result.GetOrdinal("WORKFLOW_TASK_NM"));
                    mAccelaAionQueueData.WORKFLOW_TASK_STATUS = result.GetString(result.GetOrdinal("WORKFLOW_TASK_STATUS"));
                    mAccelaAionQueueData.EstimatedRereviewHours = result.GetDecimal(result.GetOrdinal("ESTIMATED_REREVIEW_HOURS_NBR"));
                    mAccelaAionQueueData.RECEIVED_DT = result.GetDateTime(result.GetOrdinal("RECEIVED_DT"));
                    mAccelaAionQueueData.LAST_PROCESSING_DT = result.GetDateTime(result.GetOrdinal("LAST_PROCESSING_DT"));
                    mAccelaAionQueueData.PROCESS_STATUS_DESC = result.GetString(result.GetOrdinal("PROCESS_STATUS_DESC"));
                    mAccelaAionQueueData.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mAccelaAionQueueData.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mAccelaAionQueueData.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mAccelaAionQueueData.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mAccelaAIONQueueDataByRecordId.Add(mAccelaAionQueueData);
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;

                throw new Exception(er);
            }
            return mAccelaAIONQueueDataByRecordId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public List<AccelaAIONAEData> GetAEDataByRecordId(string recordId)
        {
            List<AccelaAIONAEData> mAccelaAIONAEData = new List<AccelaAIONAEData>();

            string sql = "[AION].[usp_select_accela_ae_data_by_recordid]";

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@REC_ID_NUM", recordId);

            try
            {
                var result = SqlWrapper.RunSPReturnReader(sql, base.ConnectionString, ref sqlParameters);


                while (result.Read())
                {
                    AccelaAIONAEData mAccelaAionAEData = new AccelaAIONAEData();

                    mAccelaAionAEData.ACCELA_AE_DATA_ID = result.GetInt32(result.GetOrdinal("ACCELA_AE_DATA_ID"));
                    mAccelaAionAEData.SYSTEM_USER_NM = result.GetString(result.GetOrdinal("SYSTEM_USER_NM"));
                    mAccelaAionAEData.REC_ID_NUM = result.GetString(result.GetOrdinal("REC_ID_NUM"));
                    mAccelaAionAEData.PLAN_REVIEW_TYP_DESC = result.GetString(result.GetOrdinal("PLAN_REVIEW_TYP_DESC"));
                    mAccelaAionAEData.CYCLE_NBR = result.GetInt32(result.GetOrdinal("CYCLE_NBR"));
                    mAccelaAionAEData.LICENSE_TYP_DESC = result.GetString(result.GetOrdinal("LICENSE_TYP_DESC"));
                    mAccelaAionAEData.PROJECT_SCORE_DESC = result.GetString(result.GetOrdinal("PROJECT_SCORE_DESC"));
                    mAccelaAionAEData.PROJECT_CREATED_DTTM = result.GetDateTime(result.GetOrdinal("PROJECT_CREATED_DTTM"));
                    mAccelaAionAEData.PASS_FAIL_IND = result.GetBoolean(result.GetOrdinal("PASS_FAIL_IND"));
                    mAccelaAionAEData.FAILURE_CAUSE_TXT = result.GetString(result.GetOrdinal("FAILURE_CAUSE_TXT"));
                    mAccelaAionAEData.FAILURE_REASON_TXT = result.GetString(result.GetOrdinal("FAILURE_REASON_TXT"));
                    mAccelaAionAEData.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mAccelaAionAEData.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mAccelaAionAEData.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mAccelaAionAEData.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mAccelaAIONAEData.Add(mAccelaAionAEData);
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;

                throw new Exception(er);
            }
            return mAccelaAIONAEData;
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public bool DeleteAERecordsByRecordId(string recordId)
        {
            string sql = "[AION].[usp_delete_accela_ae_data_by_recordid]";

            SqlParameter[] sqlParameters = new SqlParameter[10];

            sqlParameters[0] = new SqlParameter("@REC_ID_NUM", recordId);

            SqlWrapper.RunSP(sql, base.ConnectionString, ref sqlParameters);

            return true;
        }
    }
}
