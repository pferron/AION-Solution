using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AION.Base;
using Meck.Shared.MeckDataMapping;

namespace AION.Accela.Engine.BusinessObjects
{
    public partial class AccelaApiBO_Depercated :BaseBO
    {

        public async Task<bool> InsertAccelaAIONMapRecord(LoadAccelaAIONMap newRecord)
        {
            // var receivedrecord = JsonConvert.DeserializeObject<RecordNotification>(inComingRecordData);

            string sql = "usp_insert_aion_accela_Aion_Map";

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                /*
                cmd.Parameters.AddWithValue("@AION_CLS_NM", newRecord.AION_CLS_NM);
                cmd.Parameters.AddWithValue("@AION_FIELD_NM", newRecord.AION_FIELD_NM);
                cmd.Parameters.AddWithValue("@AION_DATA_TYP_DESC", newRecord.AION_DATA_TYP_DESC);
                cmd.Parameters.AddWithValue("@USAGE_DESC", newRecord.USAGE_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM);
                cmd.Parameters.AddWithValue("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC);
                cmd.Parameters.AddWithValue("@ALT_LOC_DESC", newRecord.ALT_LOC_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_DATA_ROW_DESC", newRecord.ACCELA_DATA_ROW_DESC);
                cmd.Parameters.AddWithValue("@DATA_FIELD_DESC", newRecord.DATA_FIELD_DESC);
                cmd.Parameters.AddWithValue("@WKR_ID_CREATED_TXT", "AION UTILITY");
                */

                cmd.Parameters.AddWithValue("@AION_CLS_NM", newRecord.AION_CLS_NM);
                cmd.Parameters.AddWithValue("@AION_FIELD_NM", newRecord.AION_FIELD_NM);
                cmd.Parameters.AddWithValue("@AION_DATA_TYP_DESC", newRecord.AION_DATA_TYP_DESC);
                cmd.Parameters.AddWithValue("@AION_USAGE_DESC", newRecord.AION_USAGE_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_REC_TYP_NM", newRecord.ACCELA_REC_TYP_NM);
                cmd.Parameters.AddWithValue("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM);
                cmd.Parameters.AddWithValue("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_LOOKUP_FIELD_NM", newRecord.ACCELA_LOOKUP_FIELD_NM);
                cmd.Parameters.AddWithValue("@ACCELA_LOOKUP_VAL_DESC", newRecord.ACCELA_LOOKUP_VAL_DESC);
                cmd.Parameters.AddWithValue("@WKR_ID_CREATED_TXT", "AION APP");

                //  Execute
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            return true;
        }


        public async Task<bool> UpDateAccelaToAionMap(AccelaAIONMap newRecord)
        {
            // var receivedrecord = JsonConvert.DeserializeObject<RecordNotification>(inComingRecordData);

            string sql = "usp_update_aion_accela_Aion_Map";

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACCELA_AION_MAP_ID", newRecord.ACCELA_AION_MAP_ID);
                cmd.Parameters.AddWithValue("@AION_CLS_NM", newRecord.AION_CLS_NM);
                cmd.Parameters.AddWithValue("@AION_FIELD_NM", newRecord.AION_FIELD_NM);
                cmd.Parameters.AddWithValue("@AION_DATA_TYP_DESC", newRecord.AION_DATA_TYP_DESC);
                cmd.Parameters.AddWithValue("@AION_USAGE_DESC", newRecord.AION_USAGE_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_REC_TYP_NM", newRecord.ACCELA_REC_TYP_NM);
                cmd.Parameters.AddWithValue("@ACCELA_OBJ_TYP_DESC", newRecord.ACCELA_OBJ_TYP_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_LOC_DESC", newRecord.ACCELA_LOC_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_FIELD_NM", newRecord.ACCELA_FIELD_NM);
                cmd.Parameters.AddWithValue("@ACCELA_DATA_TYP_DESC", newRecord.ACCELA_DATA_TYP_DESC);
                cmd.Parameters.AddWithValue("@ACCELA_LOOKUP_FIELD_NM", newRecord.ACCELA_LOOKUP_FIELD_NM);
                cmd.Parameters.AddWithValue("@ACCELA_LOOKUP_VAL_DESC", newRecord.ACCELA_LOOKUP_VAL_DESC);
                cmd.Parameters.AddWithValue("@WKR_ID_CREATED_TXT", newRecord.WKR_ID_CREATED_TXT);
                cmd.Parameters.AddWithValue("@WKR_ID_UPDATED_TXT", "AION APP");
                cmd.Parameters.AddWithValue(" @UPDATED_DTTM", DateTime.Now);

                //  Execute
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return true;

        }

        public async Task<List<AccelaAIONMap>> SelectAccelaAionMap()
        {
            string sql = "usp_select_aion_accela_Aion_Map";

            List<AccelaAIONMap> mAccelaAionMaps = new List<AccelaAIONMap>();

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader result = cmd.ExecuteReader();

                while (result.Read())
                {
                    AccelaAIONMap mAccelaAionMap = new AccelaAIONMap();

                    mAccelaAionMap.ACCELA_AION_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_AION_MAP_ID"));
                    mAccelaAionMap.AION_CLS_NM = result.GetString(result.GetOrdinal("AION_CLS_NM"));
                    mAccelaAionMap.AION_FIELD_NM = result.GetString(result.GetOrdinal("AION_FIELD_NM"));
                    mAccelaAionMap.AION_DATA_TYP_DESC = result.GetString(result.GetOrdinal("AION_DATA_TYP_DESC"));
                    mAccelaAionMap.AION_USAGE_DESC = result.GetString(result.GetOrdinal("AION_USAGE_DESC"));
                    mAccelaAionMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM"));
                    mAccelaAionMap.ACCELA_OBJ_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC"));
                    mAccelaAionMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC"));
                    mAccelaAionMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM"));
                    mAccelaAionMap.ACCELA_DATA_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC"));
                    mAccelaAionMap.ACCELA_LOOKUP_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM"));
                    mAccelaAionMap.ACCELA_LOOKUP_VAL_DESC = result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC"));
                    mAccelaAionMap.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mAccelaAionMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mAccelaAionMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mAccelaAionMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mAccelaAionMaps.Add(mAccelaAionMap);
                }
                conn.Close();
            }
            return mAccelaAionMaps;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccelaAIONMap>> SelectAccelaAionMapByRecordType(string recordType)
        {
            string sql = "usp_select_aion_accela_aion_map_by_accela_rec_type_nm";

            List<AccelaAIONMap> mAccelaAionMaps = new List<AccelaAIONMap>();

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@recType", recordType);

                conn.Open();

                SqlDataReader result = cmd.ExecuteReader();

                while (result.Read())
                {

                    AccelaAIONMap mAccelaAionMap = new AccelaAIONMap();

                    mAccelaAionMap.ACCELA_AION_MAP_ID = result.GetInt32(result.GetOrdinal("ACCELA_AION_MAP_ID"));
                    mAccelaAionMap.AION_CLS_NM = result.GetString(result.GetOrdinal("AION_CLS_NM"));
                    mAccelaAionMap.AION_FIELD_NM = result.GetString(result.GetOrdinal("AION_FIELD_NM"));
                    mAccelaAionMap.AION_DATA_TYP_DESC = result.GetString(result.GetOrdinal("AION_DATA_TYP_DESC"));
                    mAccelaAionMap.AION_USAGE_DESC = result.GetString(result.GetOrdinal("AION_USAGE_DESC"));
                    mAccelaAionMap.ACCELA_REC_TYP_NM = result.GetString(result.GetOrdinal("ACCELA_REC_TYP_NM"));
                    mAccelaAionMap.ACCELA_OBJ_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_OBJ_TYP_DESC"));
                    mAccelaAionMap.ACCELA_LOC_DESC = result.GetString(result.GetOrdinal("ACCELA_LOC_DESC"));
                    mAccelaAionMap.ACCELA_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_FIELD_NM"));
                    mAccelaAionMap.ACCELA_DATA_TYP_DESC = result.GetString(result.GetOrdinal("ACCELA_DATA_TYP_DESC"));
                    mAccelaAionMap.ACCELA_LOOKUP_FIELD_NM = result.GetString(result.GetOrdinal("ACCELA_LOOKUP_FIELD_NM"));
                    mAccelaAionMap.ACCELA_LOOKUP_VAL_DESC = result.GetString(result.GetOrdinal("ACCELA_LOOKUP_VAL_DESC"));
                    mAccelaAionMap.WKR_ID_CREATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_CREATED_TXT"));
                    mAccelaAionMap.CREATED_DTTM = result.GetDateTime(result.GetOrdinal("CREATED_DTTM"));
                    mAccelaAionMap.WKR_ID_UPDATED_TXT = result.GetString(result.GetOrdinal("WKR_ID_UPDATED_TXT"));
                    mAccelaAionMap.UPDATED_DTTM = result.GetDateTime(result.GetOrdinal("UPDATED_DTTM"));

                    mAccelaAionMaps.Add(mAccelaAionMap);

                }
                conn.Close();
            }
            return mAccelaAionMaps;
        }

        public async Task<DataTable> SelectAccelaAionMapDataTableByAccerlaRecordType(string recordType)
        {
            string sql = "usp_select_aion_accela_aion_map_by_accela_rec_type_nm";

            DataTable dataTable = new DataTable();

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@recType", recordType);

                conn.Open();

                var table = new DataTable();

                table.Load(cmd.ExecuteReader());

                cmd.Connection.Close();

                return table;
            }
        }



        public async Task<DataTable> SelectAccelaAionMapDataTable()
        {
            string sql = "usp_select_aion_accela_aion_map";

            DataTable dataTable = new DataTable();

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                //   cmd.Parameters.AddWithValue("@ACCELA_REC_TYP_NM", AccelaRecordType);

                conn.Open();

                var table = new DataTable();

                table.Load(cmd.ExecuteReader());

                cmd.Connection.Close();

                return table;
            }
        }


        public async Task<bool> DeleteAccelaToAionMap(int AccelaAionMapId)
        {
            string sql = "usp_select_aion_accela_Aion_Map";

            List<AccelaAIONMap> mAccelaAionMaps = new List<AccelaAIONMap>();

            // get a db connection 

            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@ACCELA_AION_MAP_ID ", AccelaAionMapId);

                cmd.ExecuteNonQuery();

                conn.Close();
            }

            return true;
        }

    }
}



