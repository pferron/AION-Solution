#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{

    #region BusinessObject - PermissionBO

    public class PermissionBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, DeleteBySystemRoleId };

        private string _errorMsg;

        private PermissionBE _permissionBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(PermissionBE permissionBE)
        {
            int id;
            _permissionBE = permissionBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PERMISSION_NM", permissionBE.PermissionName);
                sqlParameters[1] = new SqlParameter("@MODULE_REF_ID", permissionBE.ModuleId);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", permissionBE.EnumMappingNumber);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", permissionBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_permission", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Delete(int id)
        {
            int rows;
            _id = id;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("identity", id);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_permission", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public PermissionBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PermissionBE permissionBE = new PermissionBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                permissionBE = this.ConvertDataRowToBENoModuleEnum(dataSet.Tables[0].Rows[0]);
            }

            return permissionBE;
        }

        public DataSet GetDataSet(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetDataSet))
                throw (new Exception(_errorMsg));

            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<PermissionBE> GetList()
        {

            DataSet dataSet;
            List<PermissionBE> permissionBEList = new List<PermissionBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    permissionBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return permissionBEList;

        }
        public List<PermissionBE> GetListBySystemRoleId(int systemroledid)
        {

            DataSet dataSet;
            List<PermissionBE> permissionBEList = new List<PermissionBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@systemroleid", systemroledid);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_get_list_bysystemroleid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    permissionBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return permissionBEList;

        }
        public int InsertPermissionSystemRoleXref(int id, int systemroleid, string wrkrid)
        {
            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PERMISSION_REF_ID", id);
                sqlParameters[1] = new SqlParameter("@SYSTEM_ROLE_ID", systemroleid);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", wrkrid);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_permission_system_role", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;

        }
        public int GetSystemRoleXrefList()
        {

            DataSet dataSet;
            List<PermissionBE> permissionBEList = new List<PermissionBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_system_role_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    permissionBEList.Add(new PermissionBE { PermissionId = TryToParse<int?>(dataRow["PERMISSION_REF_ID"]) });
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return permissionBEList.Count;

        }
        public int Update(PermissionBE permissionBE)
        {
            int rows;
            _permissionBE = permissionBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@PERMISSION_REF_ID", permissionBE.PermissionId);
                sqlParameters[1] = new SqlParameter("@PERMISSION_NM", permissionBE.PermissionName);
                sqlParameters[2] = new SqlParameter("@MODULE_REF_ID", permissionBE.ModuleId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", permissionBE.UpdatedDate);
                sqlParameters[4] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", permissionBE.EnumMappingNumber);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", permissionBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_permission", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }
        public int InsertPermissionUserXref(int id, int userid, string wrkrid)
        {

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PERMISSION_REF_ID", id);
                sqlParameters[1] = new SqlParameter("@USER_ID", userid);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", wrkrid);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_permission", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;

        }
        public List<PermissionBE> GetListByUserId(int userid)
        {

            DataSet dataSet;
            List<PermissionBE> permissionBEList = new List<PermissionBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@userid", userid);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_get_list_byuserid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    permissionBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return permissionBEList;

        }

        /// <summary>
        /// Deletes all permissions in PERMISSION_SYSTEM_ROLE_XREF for this system role
        /// </summary>
        /// <param name="systemroleid"></param>
        /// <param name="wkrid"></param>
        /// <returns></returns>
        public int DeleteBySystemRoleId(int systemroleid, string wkrid)
        {
            int rows;
            _id = systemroleid;

            if (!this.Validate(ActionType.DeleteBySystemRoleId))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@SYSTEM_ROLE_ID", systemroleid);

                sqlParameters[1] = new SqlParameter("@WKR_ID_TXT", wkrid);

                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_permission_bysystemroleid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }
        public int DeleteByUserId(int userid, string wkrid)
        {
            int rows;
            _id = userid;

            if (!this.Validate(ActionType.DeleteBySystemRoleId))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@USER_ID", userid);

                sqlParameters[1] = new SqlParameter("@WKR_ID_TXT", wkrid);

                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_permission_byuserid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        #endregion

        #region Private Methods

        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Create:
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    return (_errorMsg == String.Empty);

                case ActionType.DeleteBySystemRoleId:
                    return (_errorMsg == String.Empty);
                default:
                    break;
            }

            return true;

        }

        private PermissionBE ConvertDataRowToBE(DataRow dataRow)
        {
            PermissionBE permissionBE = new PermissionBE();

            permissionBE.PermissionId = TryToParse<int?>(dataRow["PERMISSION_REF_ID"]);
            permissionBE.PermissionName = TryToParse<string>(dataRow["PERMISSION_NM"]);
            permissionBE.ModuleId = TryToParse<int?>(dataRow["MODULE_REF_ID"]);
            permissionBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            permissionBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            permissionBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            permissionBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            permissionBE.EnumMappingNumber = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            permissionBE.ModuleEnumMappingNumber = TryToParse<int?>(dataRow["MODULE_ENUM_MAPPING_VAL_NBR"]);
            return permissionBE;

        }

        private PermissionBE ConvertDataRowToBENoModuleEnum(DataRow dataRow)
        {
            PermissionBE permissionBE = new PermissionBE();

            permissionBE.PermissionId = TryToParse<int?>(dataRow["PERMISSION_REF_ID"]);
            permissionBE.PermissionName = TryToParse<string>(dataRow["PERMISSION_NM"]);
            permissionBE.ModuleId = TryToParse<int?>(dataRow["MODULE_REF_ID"]);
            permissionBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            permissionBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            permissionBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            permissionBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            permissionBE.EnumMappingNumber = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            return permissionBE;

        }


        #endregion

    }

    #endregion

}