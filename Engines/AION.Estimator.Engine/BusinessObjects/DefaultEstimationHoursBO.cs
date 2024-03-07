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

    #region BusinessObject - DefaultEstimationHoursBO

    public class DefaultEstimationHoursBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetByDepartmentProjectId, GetDataSet, GetList, GetAllList, Update };

        private string _errorMsg;

        private DefaultEstimationHoursBE _defaultEstimationHoursBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(DefaultEstimationHoursBE defaultEstimationHoursBE)
        {
            int id;
            _defaultEstimationHoursBE = defaultEstimationHoursBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@DEFAULT_HOURS_NBR", defaultEstimationHoursBE.DefaultHoursNbr);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", defaultEstimationHoursBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@PROJECT_TYP_REF_ID", defaultEstimationHoursBE.ProjectTypeRefId);
                sqlParameters[3] = new SqlParameter("@ENABLED_IND", defaultEstimationHoursBE.Enabled);
                sqlParameters[4] = new SqlParameter("@ESTIMATION_HOURS_TXT", defaultEstimationHoursBE.EstimationHrsTxt);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", defaultEstimationHoursBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_default_estimation_hours", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_default_estimation_hours", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        //public DefaultEstimationHoursBE GetById(int id)
        //{
        //    _id = id;

        //    if (!this.Validate(ActionType.GetById))
        //        throw (new Exception(_errorMsg));

        //    DefaultEstimationHoursBE defaultEstimationHoursBE = new DefaultEstimationHoursBE();
        //    DataSet dataSet;

        //    try
        //    {
        //        SqlParameter[] sqlParameters = new SqlParameter[1];

        //        sqlParameters[0] = new SqlParameter("@identity", id);

        //        dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_default_estimation_hours_get_by_id", base.ConnectionString, ref sqlParameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    if (dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        defaultEstimationHoursBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
        //    }

        //    return defaultEstimationHoursBE;
        //}

        //public DefaultEstimationHoursBE GetByDepartmentProjectId(int departmentId, int projectTypeId)
        //{
        //    if (!this.Validate(ActionType.GetByDepartmentProjectId))
        //        throw (new Exception(_errorMsg));

        //    DefaultEstimationHoursBE defaultEstimationHoursBE = new DefaultEstimationHoursBE();
        //    DataSet dataSet;

        //    try
        //    {
        //        SqlParameter[] sqlParameters = new SqlParameter[2];

        //        sqlParameters[0] = new SqlParameter("@departmentID", departmentId);
        //        sqlParameters[1] = new SqlParameter("@projectTypeID", projectTypeId);

        //        dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_getDefaultHoursByDepartmentProjectType", base.ConnectionString, ref sqlParameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    if (dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        defaultEstimationHoursBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
        //    }

        //    return defaultEstimationHoursBE;
        //}

        //public DataSet GetDataSet(int id)
        //{
        //    _id = id;

        //    if (!this.Validate(ActionType.GetDataSet))
        //        throw (new Exception(_errorMsg));

        //    DataSet dataSet;

        //    try
        //    {
        //        SqlParameter[] sqlParameters = new SqlParameter[1];

        //        sqlParameters[0] = new SqlParameter("@identity", id);

        //        dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_default_estimation_hours_get_list", base.ConnectionString, ref sqlParameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    return dataSet;
        //}

        //public List<DefaultEstimationHoursBE> GetList(int id)
        //{
        //    _id = id;

        //    if (!this.Validate(ActionType.GetList))
        //        throw (new Exception(_errorMsg));

        //    DataSet dataSet;
        //    List<DefaultEstimationHoursBE> defaultEstimationHoursBEList = new List<DefaultEstimationHoursBE>();

        //    try
        //    {
        //        SqlParameter[] sqlParameters = new SqlParameter[1];

        //        sqlParameters[0] = new SqlParameter("@identity", id);

        //        dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_default_estimation_hours_get_list", base.ConnectionString, ref sqlParameters);

        //        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        //        {
        //            defaultEstimationHoursBEList.Add(this.ConvertDataRowToBE(dataRow));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    return defaultEstimationHoursBEList;

        //}

        public List<DefaultEstimationHoursBE> GetAllList(int? projectTypeRefID = null)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<DefaultEstimationHoursBE> defaultEstimationHoursBEList = new List<DefaultEstimationHoursBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@PROJECT_TYP_REF_ID", projectTypeRefID);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_default_estimation_hours_get_AllList", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    defaultEstimationHoursBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return defaultEstimationHoursBEList;

        }

        public int Update(DefaultEstimationHoursBE defaultEstimationHoursBE)
        {
            int rows;
            _defaultEstimationHoursBE = defaultEstimationHoursBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@DEFAULT_ESTIMATION_HOURS_ID", defaultEstimationHoursBE.DefaultEstimationHoursId);
                sqlParameters[1] = new SqlParameter("@DEFAULT_HOURS_NBR", defaultEstimationHoursBE.DefaultHoursNbr);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", defaultEstimationHoursBE.UpdatedDate);
                sqlParameters[3] = new SqlParameter("@BUSINESS_REF_ID", defaultEstimationHoursBE.BusinessRefId);
                sqlParameters[4] = new SqlParameter("@PROJECT_TYP_REF_ID", defaultEstimationHoursBE.ProjectTypeRefId);
                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", defaultEstimationHoursBE.UpdatedByWkrId);
                sqlParameters[6] = new SqlParameter("@ENABLED_IND", defaultEstimationHoursBE.Enabled);
                sqlParameters[7] = new SqlParameter("@ESTIMATION_HOURS_TXT", defaultEstimationHoursBE.EstimationHrsTxt);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_default_estimation_hours", base.ConnectionString, ref sqlParameters);

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

                case ActionType.GetByDepartmentProjectId:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                case ActionType.GetAllList:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }

            return true;

        }

        private DefaultEstimationHoursBE ConvertDataRowToBE(DataRow dataRow)
        {
            DefaultEstimationHoursBE defaultEstimationHoursBE = new DefaultEstimationHoursBE();

            defaultEstimationHoursBE.DefaultEstimationHoursId = TryToParse<int?>(dataRow["DEFAULT_ESTIMATION_HOURS_ID"]);
            defaultEstimationHoursBE.DefaultHoursNbr = TryToParse<decimal?>(dataRow["DEFAULT_HOURS_NBR"]);
            defaultEstimationHoursBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            defaultEstimationHoursBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            defaultEstimationHoursBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            defaultEstimationHoursBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            defaultEstimationHoursBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            defaultEstimationHoursBE.ProjectTypeRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            defaultEstimationHoursBE.Enabled = TryToParse<int?>(dataRow["ENABLED_IND"]);
            defaultEstimationHoursBE.EstimationHrsTxt = TryToParse<string>(dataRow["ESTIMATION_HOURS_TXT"]);
            return defaultEstimationHoursBE;

        }

        #endregion

    }

    #endregion

}