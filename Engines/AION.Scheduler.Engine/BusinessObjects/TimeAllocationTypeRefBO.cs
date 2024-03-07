#region Using

using AION.Base;
using AION.Scheduler.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Scheduler.Engine.BusinessObjects
{

    #region BusinessObject - TimeAllocationTypeRefBO

    public class TimeAllocationTypeRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private TimeAllocationTypeRefBE _timeAllocationTypeRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(TimeAllocationTypeRefBE timeAllocationTypeRefBE)
        {
            int id;
            _timeAllocationTypeRefBE = timeAllocationTypeRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@TIME_ALLOCATION_TYP_REF_DESC", timeAllocationTypeRefBE.TimeAllocationTypeRefDesc);
                sqlParameters[1] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", timeAllocationTypeRefBE.EnumMappingValNbr);
                sqlParameters[2] = new SqlParameter("@ACTIVE_IND", timeAllocationTypeRefBE.ActiveInd);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", timeAllocationTypeRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_time_allocation_type_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_time_allocation_type_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public TimeAllocationTypeRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            TimeAllocationTypeRefBE timeAllocationTypeRefBE = new TimeAllocationTypeRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_time_allocation_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                timeAllocationTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return timeAllocationTypeRefBE;
        }

        public DataSet GetDataSet()
        {

            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_time_allocation_type_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<TimeAllocationTypeRefBE> GetList()
        {

            DataSet dataSet;
            List<TimeAllocationTypeRefBE> timeAllocationTypeRefBEList = new List<TimeAllocationTypeRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_time_allocation_type_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    timeAllocationTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return timeAllocationTypeRefBEList;

        }

        public int Update(TimeAllocationTypeRefBE timeAllocationTypeRefBE)
        {
            int rows;
            _timeAllocationTypeRefBE = timeAllocationTypeRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@TIME_ALLOCATION_TYP_REF_ID", timeAllocationTypeRefBE.TimeAllocationTypeRefId);
                sqlParameters[1] = new SqlParameter("@TIME_ALLOCATION_TYP_REF_DESC", timeAllocationTypeRefBE.TimeAllocationTypeRefDesc);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", timeAllocationTypeRefBE.EnumMappingValNbr);
                sqlParameters[3] = new SqlParameter("@ACTIVE_IND", timeAllocationTypeRefBE.ActiveInd);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", timeAllocationTypeRefBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", timeAllocationTypeRefBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_time_allocation_type_ref", base.ConnectionString, ref sqlParameters);

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

        private TimeAllocationTypeRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            TimeAllocationTypeRefBE timeAllocationTypeRefBE = new TimeAllocationTypeRefBE();

            timeAllocationTypeRefBE.TimeAllocationTypeRefId = TryToParse<int?>(dataRow["TIME_ALLOCATION_TYP_REF_ID"]);
            timeAllocationTypeRefBE.TimeAllocationTypeRefDesc = TryToParse<string>(dataRow["TIME_ALLOCATION_TYP_REF_DESC"]);
            timeAllocationTypeRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            timeAllocationTypeRefBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            timeAllocationTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            timeAllocationTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            timeAllocationTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            timeAllocationTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return timeAllocationTypeRefBE;

        }

        #endregion

    }

    #endregion

}