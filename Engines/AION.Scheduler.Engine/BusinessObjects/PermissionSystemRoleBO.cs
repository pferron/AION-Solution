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

    #region BusinessObject - PermissionSystemRoleBO

    public class PermissionSystemRoleBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private PermissionSystemRoleBE _permissionSystemRoleBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(PermissionSystemRoleBE permissionSystemRoleBE)
        {
            int id;
            _permissionSystemRoleBE = permissionSystemRoleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@PERMISSION_REF_ID", permissionSystemRoleBE.PermissionId);
                sqlParameters[1] = new SqlParameter("@SYSTEM_ROLE_ID", permissionSystemRoleBE.SystemRoleId);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", permissionSystemRoleBE.UserId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_permission_system_role", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_permission_system_role", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public PermissionSystemRoleBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PermissionSystemRoleBE permissionSystemRoleBE = new PermissionSystemRoleBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_system_role_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                permissionSystemRoleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return permissionSystemRoleBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_system_role_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<PermissionSystemRoleBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PermissionSystemRoleBE> permissionSystemRoleBEList = new List<PermissionSystemRoleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_permission_system_role_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    permissionSystemRoleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return permissionSystemRoleBEList;

        }

        public int Update(PermissionSystemRoleBE permissionSystemRoleBE)
        {
            int rows;
            _permissionSystemRoleBE = permissionSystemRoleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@PERMISSION_REF_ID", permissionSystemRoleBE.PermissionId);
                sqlParameters[1] = new SqlParameter("@SYSTEM_ROLE_ID", permissionSystemRoleBE.SystemRoleId);
                sqlParameters[2] = new SqlParameter("@PERMISSION_SYSTEM_ROLE_CROSS_REF_ID", permissionSystemRoleBE.PermissionSystemRoleId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", permissionSystemRoleBE.UpdatedDate);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", permissionSystemRoleBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_permission_system_role", base.ConnectionString, ref sqlParameters);

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

                default:
                    break;
            }

            return true;

        }

        private PermissionSystemRoleBE ConvertDataRowToBE(DataRow dataRow)
        {
            PermissionSystemRoleBE permissionSystemRoleBE = new PermissionSystemRoleBE();

            permissionSystemRoleBE.PermissionId = TryToParse<int?>(dataRow["PERMISSION_REF_ID"]);
            permissionSystemRoleBE.SystemRoleId = TryToParse<int?>(dataRow["SYSTEM_ROLE_ID"]);
            permissionSystemRoleBE.PermissionSystemRoleId = TryToParse<int?>(dataRow["PERMISSION_SYSTEM_ROLE_CROSS_REF_ID"]);
            permissionSystemRoleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            permissionSystemRoleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            permissionSystemRoleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            permissionSystemRoleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return permissionSystemRoleBE;

        }

        #endregion

    }

    #endregion

}