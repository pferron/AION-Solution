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

    #region BusinessObject - NpaTypeRefBO

    public class NpaTypeRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private NpaTypeRefBE _npaTypeRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(NpaTypeRefBE npaTypeRefBE)
        {
            int id;
            _npaTypeRefBE = npaTypeRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@APPT_TYP_DESC", npaTypeRefBE.AppointmentTypeName);
                sqlParameters[1] = new SqlParameter("@ACTIVE_IND", npaTypeRefBE.IsActive);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", npaTypeRefBE.CreatedByWkrId);
                sqlParameters[3] = new SqlParameter("@TIME_ALLOCATION_TYP_REF_ID", npaTypeRefBE.TimeAllocationTypeRefId);
                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_npa_type_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_npa_type_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public NpaTypeRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            NpaTypeRefBE npaTypeRefBE = new NpaTypeRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_npa_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                npaTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return npaTypeRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_npa_type_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<NpaTypeRefBE> GetAllNPaTypes(bool includeOnlyActive = false)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<NpaTypeRefBE> npaTypeRefBEList = new List<NpaTypeRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@getActiveOnly", includeOnlyActive == true ? 1 : 0);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_npa_type_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    npaTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return npaTypeRefBEList;

        }
        /// <summary>
        /// Updates Active Indicator Only
        /// </summary>
        /// <param name="npaTypeRefBE"></param>
        /// <returns></returns>
        public int Update(NpaTypeRefBE npaTypeRefBE)
        {
            int rows;
            _npaTypeRefBE = npaTypeRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@NON_PROJECT_APPT_TYP_REF_ID", npaTypeRefBE.NpaTypeRefID);
                sqlParameters[1] = new SqlParameter("@ACTIVE_IND", npaTypeRefBE.IsActive);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", npaTypeRefBE.UpdatedDate);
                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", npaTypeRefBE.UpdatedByWkrId);
                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_npa_type_ref", base.ConnectionString, ref sqlParameters);

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

        private NpaTypeRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            NpaTypeRefBE npaTypeRefBE = new NpaTypeRefBE();

            npaTypeRefBE.NpaTypeRefID = TryToParse<int?>(dataRow["NON_PROJECT_APPT_TYP_REF_ID"]);
            npaTypeRefBE.AppointmentTypeName = TryToParse<string>(dataRow["APPT_TYP_DESC"]);
            npaTypeRefBE.IsActive = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            npaTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            npaTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            npaTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            npaTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            npaTypeRefBE.TimeAllocationTypeRefId = TryToParse<int?>(dataRow["TIME_ALLOCATION_TYP_REF_ID"]);
            return npaTypeRefBE;

        }

        #endregion

    }

    #endregion

}