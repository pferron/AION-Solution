#region Using

using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - UserSystemRoleRelationshipBO

    public class UserSystemRoleRelationshipBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetByUserRoleId, GetDataSet, GetList, GetListByUserId, Update };

        private string _errorMsg;

        private UserSystemRoleRelationshipBE _userSystemRoleRelationshipBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(int userID, List<int> roleIDList, int updatingUserID = 1)
        {
            int id;
            try
            {
                string rolelst = "";
                foreach (var item in roleIDList)
                {
                    if (rolelst == "")
                        rolelst = item.ToString();
                    else
                        rolelst = rolelst + "," + item.ToString();
                }
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@USER_ID", userID);
                sqlParameters[1] = new SqlParameter("@USER_SYS_ROLE_ID_LST", rolelst);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", updatingUserID);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_system_role_relationship", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Delete(int userID, List<int> roleIDList = null)
        {
            int rows;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                string rolelst = "";
                if (roleIDList != null)
                {
                    foreach (var item in roleIDList)
                    {
                        if (rolelst == "")
                            rolelst = item.ToString();
                        else
                            rolelst = rolelst + "," + item.ToString();
                    }
                }
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@USER_ID", userID);
                sqlParameters[1] = new SqlParameter("@USER_SYS_ROLE_ID_LST", rolelst);

                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_system_role_relationship", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public UserSystemRoleRelationshipBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            UserSystemRoleRelationshipBE userSystemRoleRelationshipBE = new UserSystemRoleRelationshipBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_system_role_relationship_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userSystemRoleRelationshipBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userSystemRoleRelationshipBE;
        }
        public UserSystemRoleRelationshipBE GetByUserRoleId(int userid, int roleid)
        {
            //_id = id;

            if (!this.Validate(ActionType.GetByUserRoleId))
                throw (new Exception(_errorMsg));

            UserSystemRoleRelationshipBE userSystemRoleRelationshipBE = new UserSystemRoleRelationshipBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@userid", userid);
                sqlParameters[1] = new SqlParameter("@roleid", roleid);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_system_role_relationship_get_by_userroleid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userSystemRoleRelationshipBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userSystemRoleRelationshipBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_system_role_relationship_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<UserSystemRoleRelationshipBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserSystemRoleRelationshipBE> userSystemRoleRelationshipBEList = new List<UserSystemRoleRelationshipBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_system_role_relationship_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userSystemRoleRelationshipBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userSystemRoleRelationshipBEList;

        }
        public List<UserSystemRoleRelationshipBE> GetListByUserId(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetListByUserId))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserSystemRoleRelationshipBE> userSystemRoleRelationshipBEList = new List<UserSystemRoleRelationshipBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@userid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_system_role_relationship_get_list_byuserid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userSystemRoleRelationshipBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userSystemRoleRelationshipBEList;

        }
        public int Update(UserSystemRoleRelationshipBE userSystemRoleRelationshipBE)
        {
            int rows;
            _userSystemRoleRelationshipBE = userSystemRoleRelationshipBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@USER_SYSTEM_ROLE_RELATIONSHIP_ID", userSystemRoleRelationshipBE.UserSystemRoleRelationshipId);
                sqlParameters[1] = new SqlParameter("@USER_ID", userSystemRoleRelationshipBE.UserID);
                sqlParameters[2] = new SqlParameter("@SYSTEM_ROLE_ID", userSystemRoleRelationshipBE.SystemRoleId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", userSystemRoleRelationshipBE.UpdatedDate);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", userSystemRoleRelationshipBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_system_role_relationship", base.ConnectionString, ref sqlParameters);

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
                    if (_userSystemRoleRelationshipBE.UserID == null || _userSystemRoleRelationshipBE.SystemRoleId == null)
                    { _errorMsg = "UserID or SystemRoleId is null. Role cannot be added."; }
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);
                case ActionType.GetByUserRoleId:
                    return (_errorMsg == String.Empty);
                case ActionType.Update:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }

            return true;

        }

        private UserSystemRoleRelationshipBE ConvertDataRowToBE(DataRow dataRow)
        {
            UserSystemRoleRelationshipBE userSystemRoleRelationshipBE = new UserSystemRoleRelationshipBE();

            userSystemRoleRelationshipBE.UserSystemRoleRelationshipId = TryToParse<int?>(dataRow["USER_SYSTEM_ROLE_RELATIONSHIP_ID"]);
            userSystemRoleRelationshipBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
            userSystemRoleRelationshipBE.SystemRoleId = TryToParse<int?>(dataRow["SYSTEM_ROLE_ID"]);
            userSystemRoleRelationshipBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            userSystemRoleRelationshipBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            userSystemRoleRelationshipBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            userSystemRoleRelationshipBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            userSystemRoleRelationshipBE.SystemRoleEnumMappingNumber = TryToParse<int?>(dataRow["SYSTEM_ROLE_ENUM_MAPPING_VAL_NBR"]);

            return userSystemRoleRelationshipBE;

        }

        #endregion

    }

    #endregion

}