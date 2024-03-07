using AION.Base;
using Meck.Data;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace AION.AIONDB.Engine.BusinessObjects
{
    public partial class PosseMapBO : BaseBO 
    {
        Logger _mLogger = new Logger();

        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }

        }


        private void LogRunTime(MethodBase source, string sqlcode, long TimeElasped)
        {
            string message = "SP Execution Time: " + TimeElasped +
                             " milliseconds for " + sqlcode + " to complete";
            Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.ExecutionTime, source, message, ex: null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inDetail"></param>
        /// <returns></returns>
        public bool TaskInsertAccelaPosseDataLog(AccelaPosseLoging accela_posse_log)
        {
            try
            {
                // string sql = @"usp_insert_accela_posse_log";

                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@ACCELA_REC_ID_NUM", accela_posse_log.ACCELA_REC_ID_NUM),
                    new SqlParameter("@POSSE_ACTION_CAUSE_DESC", accela_posse_log.POSSE_ACTION_CAUSE_DESC),
                    new SqlParameter("@POSSE_UPDATE_TXT", accela_posse_log.POSSE_UPDATE_TXT),
                    new SqlParameter("@POSSE_XML_TXT", accela_posse_log.POSSE_XML_TXT),
                    new SqlParameter("@PROCESS_DTTM", DateTime.Now),
                    new SqlParameter("@WKR_ID_CREATED_TXT", accela_posse_log.WKR_ID_CREATED_TXT)
                };

                SqlWrapper.RunSP("[AION].[usp_insert_accela_posse_log]", base.ConnectionString, ref sqlParameters);

                return true;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        public bool TaskInsertPosseAccelaMapRecord(LoadPosseAccelaMap newRecord)
        {
            // var receivedrecord = JsonConvert.DeserializeObject<RecordNotification>(inComingRecordData);

            string sql = "usp_insert_aion_accela_posse_map";

            // get a db connection 
            if (string.IsNullOrWhiteSpace(base.ConnectionString))
            {
                throw new Exception("AION connection not set when processing InsertPOSSEMAPRecord.");
            }

            try
            {
                // populate the values 
                SqlParameter[] sqlParm =
                {
                    new SqlParameter("@POSSE_FIELD_NM", newRecord.POSSE_FIELD_NM.Trim()),
                    new SqlParameter("@POSSE_DATA_TYP_DESC", newRecord.POSSE_DATA_TYP_DESC.Trim()),
                    new SqlParameter("@ACCELA_REC_TYP_NM", newRecord.ACCELA_REC_TYP_NM.Trim()),
                    new SqlParameter("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC.Trim()),
                    new SqlParameter("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC.Trim()),
                    new SqlParameter("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM.Trim()),
                    new SqlParameter("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC.Trim()),
                    new SqlParameter("@ACCELA_LOOKUP_FIELD_NM", newRecord.ACCELA_LOOKUP_FIELD_NM.Trim()),
                    new SqlParameter("@ACCELA_LOOKUP_VAL_DESC", newRecord.ACCELA_LOOKUP_VAL_DESC.Trim()),
                    new SqlParameter("@WKR_ID_CREATED_TXT", "AutoLoader")
                };
                //  Execute
                SqlWrapper.RunSP(sql, Globals.AIONConnectionString, ref sqlParm);

                return true;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        public bool TaskUpDatePosseToAccelaMap(PosseAccelaDBMap newRecord)
        {
            try
            {

                // var receivedrecord = JsonConvert.DeserializeObject<RecordNotification>(inComingRecordData);

                string sql = "usp_update_aion_accela_posse_map";

                // get a db connection 

                if (string.IsNullOrWhiteSpace(base.ConnectionString))
                {
                    throw new Exception("AION connection not set when processing UpdatingPOSSEMAPRecord.");
                }

                SqlParameter[] sqlParm =
                {
                    new SqlParameter("@ACCELA_POSSE_MAP_ID", newRecord.ACCELA_POSSE_MAP_ID),
                    new SqlParameter("@ACCELA_REC_TYP_NM", newRecord.ACCELA_REC_TYP_NM),
                    new SqlParameter("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC),
                    new SqlParameter("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC),
                    new SqlParameter("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM),
                    new SqlParameter("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC),
                    new SqlParameter("@ACCELA_LOOKUP_FIELD_NM", newRecord.ACCELA_LOOKUP_FIELD_NM),
                    new SqlParameter("@ACCELA_LOOKUP_VAL_DESC", newRecord.ACCELA_LOOKUP_VAL_DESC),
                    new SqlParameter("@WKR_ID_CREATED_TXT", newRecord.CREATED_DTTM),
                    new SqlParameter("@WKR_ID_UPDATED_TXT", newRecord.WKR_ID_UPDATED_TXT),
                    new SqlParameter("@POSSE_FIELD_NM", newRecord.POSSE_FIELD_NM),
                    new SqlParameter("@POSSE_DATA_TYP_DESC", newRecord.POSSE_DATA_TYP_DESC)
                };
                //  Execute
                SqlWrapper.RunSP(sql, Globals.AIONConnectionString, ref sqlParm);


                return true;
            }

            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MeckAccelaDataMap> TaskSelectPosseAccelaMap()
        {
            string sql = "usp_select_aion_accela_posse_map";

            List<MeckAccelaDataMap> mPosseAccelaDBMaps = new List<MeckAccelaDataMap>();

            // get a db connection 
            try
            {
                if (string.IsNullOrWhiteSpace(base.ConnectionString))
                {
                    throw new Exception("AION connection not set when processing SelectPOSSEMAPRecord.");
                }

                SqlDataReader result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString);

                while (result.Read())
                {
                    MeckAccelaDataMap mPosseAccelaDBMap = new MeckAccelaDataMap();

                    //   mPosseAccelaDBMap.ACCELA_POSSE_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_POSSE_MAP_ID"));
                    mPosseAccelaDBMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM"));
                    mPosseAccelaDBMap.ACCELA_OBJ_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC"));
                    mPosseAccelaDBMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC"));
                    mPosseAccelaDBMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM"));
                    mPosseAccelaDBMap.ACCELA_DATA_TYP_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC"));
                    mPosseAccelaDBMap.ACCELA_LOOKUP_FIELD_NM =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM"));
                    mPosseAccelaDBMap.ACCELA_LOOKUP_VAL_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC"));
                    mPosseAccelaDBMap.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mPosseAccelaDBMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mPosseAccelaDBMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mPosseAccelaDBMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));
                    mPosseAccelaDBMap.Meck_FIELD_NM = result.GetString(result.GetOrdinal("POSSE_FIELD_NM"));
                    mPosseAccelaDBMap.Meck_DATA_TYP_DESC = result.GetString(result.GetOrdinal("POSSE_DATA_TYP_DESC"));

                    mPosseAccelaDBMaps.Add(mPosseAccelaDBMap);
                }

                return mPosseAccelaDBMaps;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MeckAccelaDataMap> TaskSelectPosseAccelaMapByRecordType(string recordType)
        {
            try
            {
                string sql = "usp_select_aion_accela_posse_map_by_accela_rec_type_nm";

                List<MeckAccelaDataMap> mPosseAccelaDBMaps = new List<MeckAccelaDataMap>();

                // get a db connection 
                if (string.IsNullOrWhiteSpace(base.ConnectionString))
                {
                    throw new Exception("AION connection not set when processing SelectByRecordtype POSSEMAPRecord.");
                }

                SqlParameter[] sqlParm = { new SqlParameter("@recType", recordType) };

                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref sqlParm);

                while (result.Read())
                {

                    MeckAccelaDataMap mPosseAccelaDBMap = new MeckAccelaDataMap();

                    //  mPosseAccelaDBMap.ACCELA_POSSE_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_POSSE_MAP_ID"));
                    mPosseAccelaDBMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM"));
                    mPosseAccelaDBMap.ACCELA_OBJ_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC"));
                    mPosseAccelaDBMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC"));
                    mPosseAccelaDBMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM"));
                    mPosseAccelaDBMap.ACCELA_DATA_TYP_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC"));
                    mPosseAccelaDBMap.ACCELA_LOOKUP_FIELD_NM =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM"));
                    mPosseAccelaDBMap.ACCELA_LOOKUP_VAL_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC"));
                    mPosseAccelaDBMap.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mPosseAccelaDBMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mPosseAccelaDBMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mPosseAccelaDBMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));
                    mPosseAccelaDBMap.Meck_FIELD_NM = result.GetString(result.GetOrdinal("POSSE_FIELD_NM"));
                    mPosseAccelaDBMap.Meck_DATA_TYP_DESC = result.GetString(result.GetOrdinal("POSSE_DATA_TYP_DESC"));


                    mPosseAccelaDBMaps.Add(mPosseAccelaDBMap);

                }

                return mPosseAccelaDBMaps;

            }

            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MeckAccelaDataMap> TaskSelectPosseAccelaMapforFees()
        {
            try
            {
                string sql = "usp_select_aion_accela_posse_map_for_fees";

                List<MeckAccelaDataMap> mPosseAccelaDBMaps = new List<MeckAccelaDataMap>();

                // get a db connection 
                if (string.IsNullOrWhiteSpace(base.ConnectionString))
                {
                    throw new Exception("AION connection not set when processing SelectPosseAccelaMapforFees.");
                }

                SqlDataReader result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString);

                while (result.Read())
                {

                    MeckAccelaDataMap mPosseAccelaDBMap = new MeckAccelaDataMap();

                    //  mPosseAccelaDBMap.ACCELA_POSSE_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_POSSE_MAP_ID"));
                    mPosseAccelaDBMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM"));
                    mPosseAccelaDBMap.ACCELA_OBJ_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC"));
                    mPosseAccelaDBMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC"));
                    mPosseAccelaDBMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM"));
                    mPosseAccelaDBMap.ACCELA_DATA_TYP_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC"));
                    mPosseAccelaDBMap.ACCELA_LOOKUP_FIELD_NM =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM"));
                    mPosseAccelaDBMap.ACCELA_LOOKUP_VAL_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC"));
                    mPosseAccelaDBMap.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mPosseAccelaDBMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mPosseAccelaDBMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mPosseAccelaDBMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));
                    mPosseAccelaDBMap.Meck_FIELD_NM = result.GetString(result.GetOrdinal("POSSE_FIELD_NM"));
                    mPosseAccelaDBMap.Meck_DATA_TYP_DESC = result.GetString(result.GetOrdinal("POSSE_DATA_TYP_DESC"));


                    mPosseAccelaDBMaps.Add(mPosseAccelaDBMap);

                }

                return mPosseAccelaDBMaps;
            }

            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable TaskSelectPosseAccelaMapDataTable()
        {
            try
            {
                string sql = "usp_select_aion_accela_posse_map";

                DataTable dataTable = new DataTable();

                // get a db connection 
                if (string.IsNullOrWhiteSpace(base.ConnectionString))
                {
                    throw new Exception("AION connection not set when processing SelectDatasetPOSSEMAPRecord.");
                }

                DataSet dataSet = new DataSet();

                var table = new DataTable();

                dataSet = SqlWrapper.RunSPReturnDS(sql, Globals.AIONConnectionString);

                dataTable = dataSet.Tables[0];

                return dataTable;

            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ACCELA_POSSE_MAP_ID"></param>
        /// <returns></returns>
        public bool TaskDeletePosseAccelaMap(string datavalue)
        {
            try
            {
                string sql = "usp_delete_accela_posse_map";

                List<AccelaAIONMap> mAccelaAionMaps = new List<AccelaAIONMap>();

                // get a db connection 
                if (string.IsNullOrWhiteSpace(base.ConnectionString))
                {
                    throw new Exception("AION connection not set when processing DeletePOSSEMAPRecord.");
                }

                SqlParameter[] sqlParm = { new SqlParameter("@AION_Field_Name", datavalue) };

                SqlWrapper.RunSP(sql, Globals.AIONConnectionString, ref sqlParm);

                return true;
            }

            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message, ex: ex));
                throw (ex);
            }
        }

        public void TaskDeleteRecordsAndResetTable(string sourceSystem)
        {
            try
            {
                string sqldelete = string.Empty;
                string sqlResetTable = string.Empty;

                if (sourceSystem == "Posse")
                {
                    sqldelete = "truncate [AION].[ACCELA_Posse_MAP] ";
                    //  sqlResetTable = "DBCC CHECKIDENT('[AION].[ACCELA_Posse_MAP]', RESEED, 1)";
                }

                if (sourceSystem == "AION")
                {
                    sqldelete = "truncate [AION].[ACCELA_AION_MAP] ";
                    //  sqlResetTable = "DBCC CHECKIDENT('[AION].[ACCELA_Accela_MAP]', RESEED, 1)";
                }
                
                SqlWrapper.RunSQLReturnReader(sqldelete, Globals.AIONConnectionString);

            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));
                throw (ex);
            }
        }
    }
}


