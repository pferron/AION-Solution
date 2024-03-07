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

    #region BusinessObject - JurisdictionRefBO

    public class JurisdictionRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private JurisdictionRefBE _jurisdictionRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(JurisdictionRefBE jurisdictionRefBE)
        {
            int id;
            _jurisdictionRefBE = jurisdictionRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@JURISDICTION_DESC", jurisdictionRefBE.JurisdictionDesc);
                sqlParameters[1] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", jurisdictionRefBE.EnumMappingValNbr);
                sqlParameters[2] = new SqlParameter("@ACTIVE_IND", jurisdictionRefBE.ActiveInd);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", jurisdictionRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_jurisdiction_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_jurisdiction_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public JurisdictionRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            JurisdictionRefBE jurisdictionRefBE = new JurisdictionRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_jurisdiction_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                jurisdictionRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return jurisdictionRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_jurisdiction_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<JurisdictionRefBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<JurisdictionRefBE> jurisdictionRefBEList = new List<JurisdictionRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_jurisdiction_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    jurisdictionRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return jurisdictionRefBEList;

        }

        public int Update(JurisdictionRefBE jurisdictionRefBE)
        {
            int rows;
            _jurisdictionRefBE = jurisdictionRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@JURISDICTION_REF_ID", jurisdictionRefBE.JurisdictionRefId);
                sqlParameters[1] = new SqlParameter("@JURISDICTION_DESC", jurisdictionRefBE.JurisdictionDesc);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", jurisdictionRefBE.EnumMappingValNbr);
                sqlParameters[3] = new SqlParameter("@ACTIVE_IND", jurisdictionRefBE.ActiveInd);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", jurisdictionRefBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", jurisdictionRefBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_jurisdiction_ref", base.ConnectionString, ref sqlParameters);

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

        private JurisdictionRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            JurisdictionRefBE jurisdictionRefBE = new JurisdictionRefBE();

            jurisdictionRefBE.JurisdictionRefId = TryToParse<int?>(dataRow["JURISDICTION_REF_ID"]);
            jurisdictionRefBE.JurisdictionDesc = TryToParse<string>(dataRow["JURISDICTION_DESC"]);
            jurisdictionRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            jurisdictionRefBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            jurisdictionRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            jurisdictionRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            jurisdictionRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            jurisdictionRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return jurisdictionRefBE;

        }

        #endregion

    }

    #endregion

}