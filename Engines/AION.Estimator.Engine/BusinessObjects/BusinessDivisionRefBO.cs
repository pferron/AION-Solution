#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - BusinessDivisionRefBO

    public class BusinessDivisionRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private BusinessDivisionRefBE _businessDivisionRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(BusinessDivisionRefBE businessDivisionRefBE)
        {
            int id;
            _businessDivisionRefBE = businessDivisionRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@BUSINESS_DIVISION_NM", businessDivisionRefBE.BusinessDivisionName);
                sqlParameters[1] = new SqlParameter("@BUSINESS_DIVISION_DESC", businessDivisionRefBE.BusinessDivisionDesc);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", businessDivisionRefBE.EnumMappingValNbr);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", businessDivisionRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_business_division_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_business_division_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public BusinessDivisionRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            BusinessDivisionRefBE businessDivisionRefBE = new BusinessDivisionRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_division_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                businessDivisionRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return businessDivisionRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_division_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<BusinessDivisionRefBE> GetList()
        {
            DataSet dataSet;
            List<BusinessDivisionRefBE> businessDivisionRefBEList = new List<BusinessDivisionRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_division_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    businessDivisionRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return businessDivisionRefBEList;

        }

        public BusinessDivisionRefBE GetDivisionByBusinessRefId(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            BusinessDivisionRefBE businessDivisionRefBE = new BusinessDivisionRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_division_ref_get_by_businessrefid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                businessDivisionRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return businessDivisionRefBE;
        }
        public List<BusinessDivisionXRefBE> GetXRefList()
        {
            DataSet dataSet;
            List<BusinessDivisionXRefBE> businessDivisionRefBEList = new List<BusinessDivisionXRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_division_xref", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    businessDivisionRefBEList.Add(this.ConvertDataRowToBEXRef(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return businessDivisionRefBEList;

        }
        public int Update(BusinessDivisionRefBE businessDivisionRefBE)
        {
            int rows;
            _businessDivisionRefBE = businessDivisionRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@BUSINESS_DIVISION_REF_ID", businessDivisionRefBE.BusinessDivisionRefId);
                sqlParameters[1] = new SqlParameter("@BUSINESS_DIVISION_NM", businessDivisionRefBE.BusinessDivisionName);
                sqlParameters[2] = new SqlParameter("@BUSINESS_DIVISION_DESC", businessDivisionRefBE.BusinessDivisionDesc);
                sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", businessDivisionRefBE.EnumMappingValNbr);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", businessDivisionRefBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", businessDivisionRefBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_business_division_ref", base.ConnectionString, ref sqlParameters);

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

        private BusinessDivisionRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            BusinessDivisionRefBE businessDivisionRefBE = new BusinessDivisionRefBE();

            businessDivisionRefBE.BusinessDivisionRefId = TryToParse<int?>(dataRow["BUSINESS_DIVISION_REF_ID"]);
            businessDivisionRefBE.BusinessDivisionName = TryToParse<string>(dataRow["BUSINESS_DIVISION_NM"]);
            businessDivisionRefBE.BusinessDivisionDesc = TryToParse<string>(dataRow["BUSINESS_DIVISION_DESC"]);
            businessDivisionRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            businessDivisionRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            businessDivisionRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            businessDivisionRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            businessDivisionRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return businessDivisionRefBE;

        }
        private BusinessDivisionXRefBE ConvertDataRowToBEXRef(DataRow dataRow)
        {
            BusinessDivisionXRefBE businessDivisionRefBE = new BusinessDivisionXRefBE();

            businessDivisionRefBE.BusinessDivisionRefId = TryToParse<int?>(dataRow["BUSINESS_DIVISION_REF_ID"]);
            businessDivisionRefBE.BusinessDivisionName = TryToParse<string>(dataRow["BUSINESS_DIVISION_NM"]);
            businessDivisionRefBE.BusinessDivisionDesc = TryToParse<string>(dataRow["BUSINESS_DIVISION_DESC"]);
            businessDivisionRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            businessDivisionRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            businessDivisionRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            businessDivisionRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            businessDivisionRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            businessDivisionRefBE.BusinessDivisionRefId = TryToParse<int?>(dataRow["BUSINESS_DIVISION_REF_ID"]);
            businessDivisionRefBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);

            return businessDivisionRefBE;

        }
        #endregion

    }

    #endregion

}