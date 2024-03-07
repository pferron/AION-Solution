using AION.Base;
using Meck.Logging;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Meck.Data;
using Meck.Shared;


namespace AION.AIONDB.Engine.BusinessObjects
{
    public partial class AIONMapBO : BaseBO
    {
        Logger _mLogger = new Logger();

        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }

        }

        public AIONMapBO()
        {
            if (String.IsNullOrEmpty(Globals.AIONConnectionString))
            {
                
                Globals.AIONConnectionString = GetConfigValue("KeyVaultConnectionString");
            }
        }
        private string GetConfigValue(string settingName)
        {
            string settingValue = string.Empty;
            try
            {
                settingValue = ConfigurationManager.AppSettings[settingName];
            }
            catch (ConfigurationErrorsException exconfig)
            {

                // config value not in Web.Config 
                settingValue = "false";
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    "Missing app setting during start :" + settingName + " in Config file.", ex: exconfig));
            }

            return settingValue;
        }




        public bool TaskInsertAccelaAIONMapRecord(LoadAccelaAIONMap newRecord)
        {
            try
            {
                // This will load the mapping values for AIon only
                // 
                // var receivedrecord = JsonConvert.DeserializeObject<RecordNotification>(inComingRecordData);

                string sql = "usp_insert_aion_accela_Aion_Map";

                // get a db connection 

                SqlParameter[] sqlParms = new SqlParameter[13]; 

                sqlParms[0] = new SqlParameter( "@AION_CLS_NM", newRecord.AION_CLS_NM);
                sqlParms[1] = new SqlParameter("@AION_CLS_NM", newRecord.AION_CLS_NM);
                sqlParms[2] = new SqlParameter("@AION_FIELD_NM", newRecord.AION_FIELD_NM);
                sqlParms[3] = new SqlParameter("@AION_DATA_TYP_DESC", newRecord.AION_DATA_TYP_DESC);
                sqlParms[4] = new SqlParameter("@AION_USAGE_DESC", newRecord.AION_USAGE_DESC);
                sqlParms[5] = new SqlParameter("@ACCELA_REC_TYP_NM", newRecord.ACCELA_REC_TYP_NM);
                sqlParms[6] = new SqlParameter("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC);
                sqlParms[7] = new SqlParameter("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC);
                sqlParms[8] = new SqlParameter("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM);
                sqlParms[9] = new SqlParameter("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC);
                sqlParms[10] = new SqlParameter("@ACCELA_LOOKUP_FIELD_NM", newRecord.ACCELA_LOOKUP_FIELD_NM);
                sqlParms[11] = new SqlParameter("@ACCELA_LOOKUP_VAL_DESC", newRecord.ACCELA_LOOKUP_VAL_DESC);
                sqlParms[12] = new SqlParameter("@WKR_ID_CREATED_TXT", "AION APP");

                SqlWrapper.RunSP(sql,Globals.AIONConnectionString,ref sqlParms);

                return true;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }


        public bool  TaskUpDateAccelaToAionMap(AccelaAIONMap newRecord)
        {
            try
            {
                string sql = "usp_update_aion_accela_Aion_Map";

                SqlParameter[] sqlParms =
                {
                    new SqlParameter("@ACCELA_AION_MAP_ID", newRecord.ACCELA_AION_MAP_ID),
                    new SqlParameter("@AION_CLS_NM", newRecord.AION_CLS_NM),
                    new SqlParameter("@AION_FIELD_NM", newRecord.AION_FIELD_NM),
                    new SqlParameter("@AION_DATA_TYP_DESC", newRecord.AION_DATA_TYP_DESC),
                    new SqlParameter("@AION_USAGE_DESC", newRecord.AION_USAGE_DESC),
                    new SqlParameter("@ACCELA_REC_TYP_NM", newRecord.ACCELA_REC_TYP_NM),
                    new SqlParameter("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC),
                    new SqlParameter("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC),
                    new SqlParameter("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM),
                    new SqlParameter("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC),
                    new SqlParameter("@ACCELA_LOOKUP_FIELD_NM", newRecord.ACCELA_LOOKUP_FIELD_NM),
                    new SqlParameter("@ACCELA_LOOKUP_VAL_DESC", newRecord.ACCELA_LOOKUP_VAL_DESC),
                    new SqlParameter("@WKR_ID_CREATED_TXT", newRecord.WKR_ID_CREATED_TXT),
                    new SqlParameter("@WKR_ID_UPDATED_TXT", "AION APP"),
                    new SqlParameter(" @UPDATED_DTTM", DateTime.Now)
                };

                SqlWrapper.RunSP(sql, Globals.AIONConnectionString, ref sqlParms);

                return true;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }

        public List<MeckAccelaDataMap> TaskSelectAccelaAionMap()
        {
            List<MeckAccelaDataMap> mAccelaAionMaps = new List<MeckAccelaDataMap>();

            try
            {
                string sql = "usp_select_aion_accela_Aion_Map";
                
                // get a db connection 

                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString);

                while (result.Read())
                {
                    MeckAccelaDataMap mAccelaAionMap = new MeckAccelaDataMap();

                    //    mAccelaAionMap.ACCELA_AION_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_AION_MAP_ID"));
                    mAccelaAionMap.AION_CLS_NM = result.GetString(result.GetOrdinal("AION_CLS_NM"));
                    mAccelaAionMap.Meck_FIELD_NM = result.GetString(result.GetOrdinal("AION_FIELD_NM"));
                    mAccelaAionMap.Meck_DATA_TYP_DESC = result.GetString(result.GetOrdinal("AION_DATA_TYP_DESC"));
                    mAccelaAionMap.AION_USAGE_DESC = result.GetString(result.GetOrdinal("AION_USAGE_DESC"));
                    mAccelaAionMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM"));
                    mAccelaAionMap.ACCELA_OBJ_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC"));
                    mAccelaAionMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC"));
                    mAccelaAionMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM"));
                    mAccelaAionMap.ACCELA_DATA_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC"));
                    mAccelaAionMap.ACCELA_LOOKUP_FIELD_NM =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM"));
                    mAccelaAionMap.ACCELA_LOOKUP_VAL_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC"));
                    mAccelaAionMap.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mAccelaAionMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mAccelaAionMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mAccelaAionMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mAccelaAionMaps.Add(mAccelaAionMap);
                }

                return mAccelaAionMaps;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MeckAccelaDataMap> TaskSelectAccelaAionMapByRecordType(string recordType)
        {
            string sql = "usp_select_aion_accela_aion_map_by_accela_rec_type_nm";

            List<MeckAccelaDataMap> mAccelaAionMaps = new List<MeckAccelaDataMap>();
            try
            {

                // get a db connection 

                SqlParameter[] sqlParm = { new SqlParameter("@recType", recordType) };

                var result = SqlWrapper.RunSPReturnReader(sql, Globals.AIONConnectionString, ref sqlParm);

                while (result.Read())
                {

                    MeckAccelaDataMap mAccelaAionMap = new MeckAccelaDataMap();

                    //  mAccelaAionMap.ACCELA_AION_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_AION_MAP_ID"));
                    mAccelaAionMap.AION_CLS_NM = result.GetString(result.GetOrdinal("AION_CLS_NM")).Trim();
                    mAccelaAionMap.Meck_FIELD_NM = result.GetString(result.GetOrdinal("AION_FIELD_NM")).Trim();
                    mAccelaAionMap.Meck_DATA_TYP_DESC =
                        result.GetString(result.GetOrdinal("AION_DATA_TYP_DESC")).Trim();
                    mAccelaAionMap.AION_USAGE_DESC = result.GetString(result.GetOrdinal("AION_USAGE_DESC")).Trim();
                    mAccelaAionMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM")).Trim();
                    mAccelaAionMap.ACCELA_OBJ_TYP_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC")).Trim();
                    mAccelaAionMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC")).Trim();
                    mAccelaAionMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM")).Trim();
                    mAccelaAionMap.ACCELA_DATA_TYP_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC")).Trim();
                    mAccelaAionMap.ACCELA_LOOKUP_FIELD_NM =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM")).Trim();
                    mAccelaAionMap.ACCELA_LOOKUP_VAL_DESC =
                        result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC")).Trim();
                    mAccelaAionMap.WKR_ID_CREATED_TXT =
                        result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT")).Trim();
                    mAccelaAionMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mAccelaAionMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mAccelaAionMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mAccelaAionMaps.Add(mAccelaAionMap);

                }

                return mAccelaAionMaps;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable TaskSelectAccelaAionMapDataTable()
        {
            string sql = "usp_select_aion_accela_aion_map";

            DataTable dataTable = new DataTable();
            try
            {
                DataSet dataSet;

                dataSet = SqlWrapper.RunSPReturnDS(sql, Globals.AIONConnectionString);

                dataTable = dataSet.Tables[0];
                return dataTable;
            }

            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }

        public bool TaskDeleteAccelaToAionMap(string datavalue)
        {
            string sql = "usp_Delete_aion_accela_Aion_Map";
            try
            {
                List<AccelaAIONMap> mAccelaAionMaps = new List<AccelaAIONMap>();

                // get a db connection 

                SqlParameter[] sqlParm = { new SqlParameter("@Aion_Field_Name ", datavalue) };

                SqlWrapper.RunSP(sql, Globals.AIONConnectionString, ref sqlParm);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool TaskDeleteRecordsAndResetTable(string sourceSystem)
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

                if (sqldelete == string.Empty)
                {
                    return false;
                }

                SqlWrapper.RunSQLReturnDS(sqldelete, Globals.AIONConnectionString);  

                return true;
            }
            catch (Exception ex)
            {
                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw (ex);
            }
        }
    }
}



