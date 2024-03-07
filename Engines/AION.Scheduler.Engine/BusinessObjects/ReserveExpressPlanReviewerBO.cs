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

    #region BusinessObject - ReserveExpressPlanReviewerBO

    public class ReserveExpressPlanReviewerBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ReserveExpressPlanReviewerBE _reserveExpressPlanReviewerBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ReserveExpressPlanReviewerBE reserveExpressPlanReviewerBE)
        {
            int id;
            _reserveExpressPlanReviewerBE = reserveExpressPlanReviewerBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@BUSINESS_REF_ID", reserveExpressPlanReviewerBE.BusinessRefId);
                sqlParameters[1] = new SqlParameter("@PLAN_REVIEWER_ID", reserveExpressPlanReviewerBE.PlanReviewerId);
                sqlParameters[2] = new SqlParameter("@ROTATION_NBR", reserveExpressPlanReviewerBE.RotationNbr);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", reserveExpressPlanReviewerBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_reserve_express_plan_reviewer", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public bool DeleteExpressPlanReviewerRotation()
        {
            int rows;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[0].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_reserve_express_plan_reviewer_all", base.ConnectionString, ref sqlParameters);

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_reserve_express_plan_reviewer", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public ReserveExpressPlanReviewerBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ReserveExpressPlanReviewerBE reserveExpressPlanReviewerBE = new ReserveExpressPlanReviewerBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_plan_reviewer_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                reserveExpressPlanReviewerBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return reserveExpressPlanReviewerBE;
        }

        public List<ReserveExpressPlanReviewerBE> GetListAll()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ReserveExpressPlanReviewerBE> reserveExpressPlanReviewerBEList = new List<ReserveExpressPlanReviewerBE>();

            try
            {
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_plan_reviewer_get_list_all", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    reserveExpressPlanReviewerBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return reserveExpressPlanReviewerBEList;

        }

        public List<ReserveExpressPlanReviewerBE> GetListByBusinessRefCsv(string businessrefcsv)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ReserveExpressPlanReviewerBE> reserveExpressPlanReviewerBEList = new List<ReserveExpressPlanReviewerBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@businessrefcsv", businessrefcsv);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_plan_reviewer_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    reserveExpressPlanReviewerBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return reserveExpressPlanReviewerBEList;

        }

        public int Update(ReserveExpressPlanReviewerBE reserveExpressPlanReviewerBE)
        {
            int rows;
            _reserveExpressPlanReviewerBE = reserveExpressPlanReviewerBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@RESERVE_EXPRESS_PLAN_REVIEWER_ID", reserveExpressPlanReviewerBE.ReserveExpressPlanReviewerId);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", reserveExpressPlanReviewerBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@PLAN_REVIEWER_ID", reserveExpressPlanReviewerBE.PlanReviewerId);
                sqlParameters[3] = new SqlParameter("@ROTATION_NBR", reserveExpressPlanReviewerBE.RotationNbr);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", reserveExpressPlanReviewerBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", reserveExpressPlanReviewerBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_reserve_express_plan_reviewer", base.ConnectionString, ref sqlParameters);

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

        private ReserveExpressPlanReviewerBE ConvertDataRowToBE(DataRow dataRow)
        {
            ReserveExpressPlanReviewerBE reserveExpressPlanReviewerBE = new ReserveExpressPlanReviewerBE();

            reserveExpressPlanReviewerBE.ReserveExpressPlanReviewerId = TryToParse<int?>(dataRow["RESERVE_EXPRESS_PLAN_REVIEWER_ID"]);
            reserveExpressPlanReviewerBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            reserveExpressPlanReviewerBE.PlanReviewerId = TryToParse<int?>(dataRow["PLAN_REVIEWER_ID"]);
            reserveExpressPlanReviewerBE.RotationNbr = TryToParse<int?>(dataRow["ROTATION_NBR"]);
            reserveExpressPlanReviewerBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            reserveExpressPlanReviewerBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            reserveExpressPlanReviewerBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            reserveExpressPlanReviewerBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            reserveExpressPlanReviewerBE.FirstName = TryToParse<string>(dataRow["FIRST_NM"]);
            reserveExpressPlanReviewerBE.LastName = TryToParse<string>(dataRow["LAST_NM"]);

            return reserveExpressPlanReviewerBE;

        }

        #endregion

    }

    #endregion

}