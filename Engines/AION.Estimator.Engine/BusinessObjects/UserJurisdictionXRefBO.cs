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

    #region BusinessObject - UserJurisdictionXRefBO

    public class UserJurisdictionXRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private int _id;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the list of jurisdictions per user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserJurisdictionXRefBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserJurisdictionXRefBE> userJurisdictionXRefBEList = new List<UserJurisdictionXRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_jurisdiction_x_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userJurisdictionXRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userJurisdictionXRefBEList;

        }

        /// <summary>
        /// update a user's jurisdictions
        /// proc deletes records then inserts 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="jurisdictioncsv"></param>
        /// <param name="wrkid"></param>
        /// <returns></returns>
        public int Update(int userId, string jurisdictioncsv, string wrkid)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@userid", userId);
                sqlParameters[1] = new SqlParameter("@jurisdictionlscsv", jurisdictioncsv);
                sqlParameters[2] = new SqlParameter("@wrkid", wrkid);
                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_jurisdiction_x_ref", base.ConnectionString, ref sqlParameters);

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

        private UserJurisdictionXRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            UserJurisdictionXRefBE userJurisdictionXRefBE = new UserJurisdictionXRefBE();

            userJurisdictionXRefBE.UserJurisdictionXRefId = TryToParse<int?>(dataRow["USER_JURISDICTION_XREF_ID"]);
            userJurisdictionXRefBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
            userJurisdictionXRefBE.JurisdictionRefId = TryToParse<int?>(dataRow["JURISDICTION_REF_ID"]);
            userJurisdictionXRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            userJurisdictionXRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            userJurisdictionXRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            userJurisdictionXRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            userJurisdictionXRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return userJurisdictionXRefBE;

        }

        #endregion

    }

    #endregion

}