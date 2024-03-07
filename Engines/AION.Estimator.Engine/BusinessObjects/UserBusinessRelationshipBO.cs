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

    #region BusinessObject - UserBusinessRelationshipBO

    public class UserBusinessRelationshipBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private UserBusinessRelationshipBE _userBusinessRelationshipBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(UserBusinessRelationshipBE userBusinessRelationshipBE)
        {
            int id;
            _userBusinessRelationshipBE = userBusinessRelationshipBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@USER_ID", userBusinessRelationshipBE.UserID);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", userBusinessRelationshipBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@PROCESS_NPA_IND", userBusinessRelationshipBE.ProcessNpaInd);
                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", userBusinessRelationshipBE.UpdatedByWkrId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_business_relationship", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_business_relationship", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public UserBusinessRelationshipBE GetByID(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            UserBusinessRelationshipBE userBusinessRelationshipBE = new UserBusinessRelationshipBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_business_relationship_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userBusinessRelationshipBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userBusinessRelationshipBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_business_relationship_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<UserBusinessRelationshipBE> GetAllListByUserID(int userID)
        {
            _id = userID;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserBusinessRelationshipBE> userBusinessRelationshipBEList = new List<UserBusinessRelationshipBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", userID);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_business_relationship_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBusinessRelationshipBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBusinessRelationshipBEList;

        }
        public List<UserBusinessRelationshipBE> GetProcessNpaInd()
        {

            DataSet dataSet;
            List<UserBusinessRelationshipBE> userBusinessRelationshipBEList = new List<UserBusinessRelationshipBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_business_relationship_processnpas", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBusinessRelationshipBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBusinessRelationshipBEList;

        }

        /// <summary>
        /// Update all process_npa_ind to false after function runs
        /// </summary>
        /// <returns></returns>
        public int UpdateProcessNpaToFalse()
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[0].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_business_relationship_processnpaind", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }
        public int DeleteAllByUser(int userID)
        {
            int rows;
            _id = userID;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("identity", userID);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_all_aion_user_business_relationship_byuser", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int Update(UserBusinessRelationshipBE userBusinessRelationshipBE)
        {
            int rows;
            _userBusinessRelationshipBE = userBusinessRelationshipBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@USER_BUSINESS_RELATIONSHIP_ID", userBusinessRelationshipBE.UserBusinessRelationshipId);
                sqlParameters[1] = new SqlParameter("@USER_ID", userBusinessRelationshipBE.UserID);
                sqlParameters[2] = new SqlParameter("@BUSINESS_REF_ID", userBusinessRelationshipBE.BusinessRefId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", userBusinessRelationshipBE.UpdatedDate);

                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", userBusinessRelationshipBE.UserId);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_business_relationship", base.ConnectionString, ref sqlParameters);

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

        private UserBusinessRelationshipBE ConvertDataRowToBE(DataRow dataRow)
        {
            UserBusinessRelationshipBE userBusinessRelationshipBE = new UserBusinessRelationshipBE();

            userBusinessRelationshipBE.UserBusinessRelationshipId = TryToParse<int?>(dataRow["USER_BUSINESS_RELATIONSHIP_ID"]);
            userBusinessRelationshipBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
            userBusinessRelationshipBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            userBusinessRelationshipBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            userBusinessRelationshipBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            userBusinessRelationshipBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            userBusinessRelationshipBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            userBusinessRelationshipBE.ProcessNpaInd = TryToParse<bool?>(dataRow["PROCESS_NPA_IND"]);
            return userBusinessRelationshipBE;

        }

        #endregion

    }

    #endregion

}