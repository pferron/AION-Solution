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

    #region BusinessObject - AppoinmentReccuranceRefBO

    public class AppoinmentReccuranceRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private AppoinmentReccuranceRefBE _appoinmentReccuranceRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(AppoinmentReccuranceRefBE appoinmentReccuranceRefBE)
        {
            int id;
            _appoinmentReccuranceRefBE = appoinmentReccuranceRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@RECURRENCE_WEEK_DESC", appoinmentReccuranceRefBE.ReccuranceWeek);
                sqlParameters[1] = new SqlParameter("@RECURRENCE_DAY_DESC", appoinmentReccuranceRefBE.ReccuranceDay);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", appoinmentReccuranceRefBE.EnumMappingValNbr);
                sqlParameters[3] = new SqlParameter("@ACTIVE_IND", appoinmentReccuranceRefBE.IsActive);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", appoinmentReccuranceRefBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_appoinment_reccurance_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_appoinment_reccurance_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public AppoinmentReccuranceRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            AppoinmentReccuranceRefBE appoinmentReccuranceRefBE = new AppoinmentReccuranceRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appoinment_reccurance_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                appoinmentReccuranceRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return appoinmentReccuranceRefBE;
        }
        public AppoinmentReccuranceRefBE GetByEnumId(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            AppoinmentReccuranceRefBE appoinmentReccuranceRefBE = new AppoinmentReccuranceRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@enumid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appoinment_reccurance_ref_get_by_enumid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                appoinmentReccuranceRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return appoinmentReccuranceRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appoinment_reccurance_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<AppoinmentReccuranceRefBE> GetList(int? id = null)
        {
            _id = id.Value;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<AppoinmentReccuranceRefBE> appoinmentReccuranceRefBEList = new List<AppoinmentReccuranceRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appoinment_reccurance_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    appoinmentReccuranceRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return appoinmentReccuranceRefBEList;

        }

        public int Update(AppoinmentReccuranceRefBE appoinmentReccuranceRefBE)
        {
            int rows;
            _appoinmentReccuranceRefBE = appoinmentReccuranceRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@APPT_RECURRENCE_REF_ID", appoinmentReccuranceRefBE.AppoinmentReccuranceID);
                sqlParameters[1] = new SqlParameter("@RECURRENCE_WEEK_DESC", appoinmentReccuranceRefBE.ReccuranceWeek);
                sqlParameters[2] = new SqlParameter("@RECURRENCE_DAY_DESC", appoinmentReccuranceRefBE.ReccuranceDay);
                sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", appoinmentReccuranceRefBE.EnumMappingValNbr);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", appoinmentReccuranceRefBE.UpdatedDate);
                sqlParameters[5] = new SqlParameter("@ACTIVE_IND", appoinmentReccuranceRefBE.IsActive);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", appoinmentReccuranceRefBE.UserId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_appoinment_reccurance_ref", base.ConnectionString, ref sqlParameters);

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

        private AppoinmentReccuranceRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            AppoinmentReccuranceRefBE appoinmentReccuranceRefBE = new AppoinmentReccuranceRefBE();

            appoinmentReccuranceRefBE.AppoinmentReccuranceID = TryToParse<int?>(dataRow["APPT_RECURRENCE_REF_ID"]);
            appoinmentReccuranceRefBE.ReccuranceWeek = TryToParse<string>(dataRow["RECURRENCE_WEEK_DESC"]);
            appoinmentReccuranceRefBE.ReccuranceDay = TryToParse<string>(dataRow["RECURRENCE_DAY_DESC"]);
            appoinmentReccuranceRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            appoinmentReccuranceRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            appoinmentReccuranceRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            appoinmentReccuranceRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            appoinmentReccuranceRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            appoinmentReccuranceRefBE.IsActive = TryToParse<bool?>(dataRow["ACTIVE_IND"]);

            return appoinmentReccuranceRefBE;

        }

        #endregion

    }

    #endregion

}