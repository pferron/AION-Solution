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

    #region BusinessObject - UserPermissionBO

    public class UserPermissionBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private UserPermissionBE _userPermissionBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(UserPermissionBE userPermissionBE)
        {
            int id;
            _userPermissionBE = userPermissionBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@USER_ID", userPermissionBE.UserId);
                sqlParameters[1] = new SqlParameter("@PERMISSION_REF_ID", userPermissionBE.PermissionId);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", userPermissionBE.UserId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_permission", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_permission", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public UserPermissionBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            UserPermissionBE userPermissionBE = new UserPermissionBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_permission_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userPermissionBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userPermissionBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_permission_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<UserPermissionBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserPermissionBE> userPermissionBEList = new List<UserPermissionBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_permission_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userPermissionBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userPermissionBEList;

        }

        public int Update(UserPermissionBE userPermissionBE)
        {
            int rows;
            _userPermissionBE = userPermissionBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@USER_ID", userPermissionBE.UserId);
                sqlParameters[1] = new SqlParameter("@PERMISSION_REF_ID", userPermissionBE.PermissionId);
                sqlParameters[2] = new SqlParameter("@USER_PERMISSION_CROSS_REF_ID", userPermissionBE.UserPermissionId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", userPermissionBE.UpdatedDate);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", userPermissionBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_permission", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }
        public List<UserPermissionBE> GetListByUserId(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserPermissionBE> userPermissionBEList = new List<UserPermissionBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@userid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_permission_get_byuserid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userPermissionBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userPermissionBEList;

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

        private UserPermissionBE ConvertDataRowToBE(DataRow dataRow)
        {
            UserPermissionBE userPermissionBE = new UserPermissionBE();

            userPermissionBE.UserId = TryToParse<int?>(dataRow["USER_ID"]);
            userPermissionBE.PermissionId = TryToParse<int?>(dataRow["PERMISSION_REF_ID"]);
            userPermissionBE.UserPermissionId = TryToParse<int?>(dataRow["USER_PERMISSION_CROSS_REF_ID"]);
            userPermissionBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            userPermissionBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            userPermissionBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            userPermissionBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return userPermissionBE;

        }

        #endregion

    }

    #endregion

}