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

    #region BusinessObject - PlanReviewScheduleDetailBO

    public class PlanReviewScheduleDetailBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private PlanReviewScheduleDetailBE _planReviewScheduleDetailBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(PlanReviewScheduleDetailBE planReviewScheduleDetailBE)
        {
            int id;
            _planReviewScheduleDetailBE = planReviewScheduleDetailBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@PLAN_REVIEW_SCHEDULE_ID", planReviewScheduleDetailBE.PlanReviewScheduleId);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", planReviewScheduleDetailBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@START_DT", planReviewScheduleDetailBE.StartDt);
                sqlParameters[3] = new SqlParameter("@END_DT", planReviewScheduleDetailBE.EndDt);
                sqlParameters[4] = new SqlParameter("@POOL_REQUEST_IND", planReviewScheduleDetailBE.PoolRequestInd);
                sqlParameters[5] = new SqlParameter("@SAME_BUILD_CONTR_IND", planReviewScheduleDetailBE.SameBuildContrInd);
                sqlParameters[6] = new SqlParameter("@MANUAL_ASSIGNMENT_IND", planReviewScheduleDetailBE.ManualAssignmentInd);
                sqlParameters[7] = new SqlParameter("@ASSIGNED_HOURS_NBR", planReviewScheduleDetailBE.AssignedHoursNbr);
                sqlParameters[8] = new SqlParameter("@ASSIGNED_PLAN_REVIEWER_ID", planReviewScheduleDetailBE.AssignedPlanReviewerId);

                sqlParameters[9] = new SqlParameter("@WKR_ID_TXT", planReviewScheduleDetailBE.UserId);

                sqlParameters[10] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[10].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_plan_review_schedule_detail", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_plan_review_schedule_detail", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public PlanReviewScheduleDetailBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PlanReviewScheduleDetailBE planReviewScheduleDetailBE = new PlanReviewScheduleDetailBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_detail_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                planReviewScheduleDetailBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return planReviewScheduleDetailBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_detail_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<PlanReviewScheduleDetailBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewScheduleDetailBE> planReviewScheduleDetailBEList = new List<PlanReviewScheduleDetailBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_detail_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewScheduleDetailBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleDetailBEList;

        }

        public List<PlanReviewScheduleDetailBE> GetListByPlanReviewScheduleId(int planReviewScheduleId)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewScheduleDetailBE> planReviewScheduleDetailBEList = new List<PlanReviewScheduleDetailBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@planReviewScheduleId", planReviewScheduleId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_detail_get_by_plan_review_schedule_id", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewScheduleDetailBEList.Add(this.ConvertDataRowToBEWReviewerName(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleDetailBEList;

        }

        public int Update(PlanReviewScheduleDetailBE planReviewScheduleDetailBE)
        {
            int rows;
            _planReviewScheduleDetailBE = planReviewScheduleDetailBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[13];

                sqlParameters[0] = new SqlParameter("@PLAN_REVIEW_SCHEDULE_DETAIL_ID", planReviewScheduleDetailBE.PlanReviewScheduleDetailId);
                sqlParameters[1] = new SqlParameter("@PLAN_REVIEW_SCHEDULE_ID", planReviewScheduleDetailBE.PlanReviewScheduleId);
                sqlParameters[2] = new SqlParameter("@BUSINESS_REF_ID", planReviewScheduleDetailBE.BusinessRefId);
                sqlParameters[3] = new SqlParameter("@START_DT", planReviewScheduleDetailBE.StartDt);
                sqlParameters[4] = new SqlParameter("@END_DT", planReviewScheduleDetailBE.EndDt);
                sqlParameters[5] = new SqlParameter("@POOL_REQUEST_IND", planReviewScheduleDetailBE.PoolRequestInd);
                sqlParameters[6] = new SqlParameter("@SAME_BUILD_CONTR_IND", planReviewScheduleDetailBE.SameBuildContrInd);
                sqlParameters[7] = new SqlParameter("@MANUAL_ASSIGNMENT_IND", planReviewScheduleDetailBE.ManualAssignmentInd);
                sqlParameters[8] = new SqlParameter("@ASSIGNED_HOURS_NBR", planReviewScheduleDetailBE.AssignedHoursNbr);
                sqlParameters[9] = new SqlParameter("@ASSIGNED_PLAN_REVIEWER_ID", planReviewScheduleDetailBE.AssignedPlanReviewerId);
                sqlParameters[10] = new SqlParameter("@UPDATED_DTTM", planReviewScheduleDetailBE.UpdatedDate);

                sqlParameters[11] = new SqlParameter("@WKR_ID_TXT", planReviewScheduleDetailBE.UserId);

                sqlParameters[12] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[12].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_review_schedule_detail", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<PlanReviewScheduleDetailBE> GetListByProjectCycle(int projectid, int cyclenbr)
        {


            DataSet dataSet;
            List<PlanReviewScheduleDetailBE> planReviewScheduleDetailBEList = new List<PlanReviewScheduleDetailBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@projectid", projectid);
                sqlParameters[1] = new SqlParameter("@cyclenbr", cyclenbr);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_detail_getByProjectCycle", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewScheduleDetailBEList.Add(this.ConvertDataRowToBEWBusinessRef(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleDetailBEList;

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
        private PlanReviewScheduleDetailBE ConvertDataRowToBEWReviewerName(DataRow dataRow)
        {
            PlanReviewScheduleDetailBE planReviewScheduleDetailBE = new PlanReviewScheduleDetailBE();

            planReviewScheduleDetailBE.PlanReviewScheduleDetailId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_DETAIL_ID"]);
            planReviewScheduleDetailBE.PlanReviewScheduleId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]);
            planReviewScheduleDetailBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            planReviewScheduleDetailBE.StartDt = TryToParse<DateTime?>(dataRow["START_DT"]);
            planReviewScheduleDetailBE.EndDt = TryToParse<DateTime?>(dataRow["END_DT"]);
            planReviewScheduleDetailBE.PoolRequestInd = TryToParse<bool?>(dataRow["POOL_REQUEST_IND"]);
            planReviewScheduleDetailBE.SameBuildContrInd = TryToParse<bool?>(dataRow["SAME_BUILD_CONTR_IND"]);
            planReviewScheduleDetailBE.ManualAssignmentInd = TryToParse<bool?>(dataRow["MANUAL_ASSIGNMENT_IND"]);
            planReviewScheduleDetailBE.AssignedHoursNbr = TryToParse<decimal?>(dataRow["ASSIGNED_HOURS_NBR"]);
            planReviewScheduleDetailBE.AssignedPlanReviewerId = TryToParse<int?>(dataRow["ASSIGNED_PLAN_REVIEWER_ID"]);
            planReviewScheduleDetailBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewScheduleDetailBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewScheduleDetailBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewScheduleDetailBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            //LES-4028
            planReviewScheduleDetailBE.AssignedReviewerFirstName = TryToParse<string>(dataRow["FIRST_NM"]);
            planReviewScheduleDetailBE.AssignedReviewerLastName = TryToParse<string>(dataRow["LAST_NM"]);
            return planReviewScheduleDetailBE;

        }
        private PlanReviewScheduleDetailBE ConvertDataRowToBE(DataRow dataRow)
        {
            PlanReviewScheduleDetailBE planReviewScheduleDetailBE = new PlanReviewScheduleDetailBE();

            planReviewScheduleDetailBE.PlanReviewScheduleDetailId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_DETAIL_ID"]);
            planReviewScheduleDetailBE.PlanReviewScheduleId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]);
            planReviewScheduleDetailBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            planReviewScheduleDetailBE.StartDt = TryToParse<DateTime?>(dataRow["START_DT"]);
            planReviewScheduleDetailBE.EndDt = TryToParse<DateTime?>(dataRow["END_DT"]);
            planReviewScheduleDetailBE.PoolRequestInd = TryToParse<bool?>(dataRow["POOL_REQUEST_IND"]);
            planReviewScheduleDetailBE.SameBuildContrInd = TryToParse<bool?>(dataRow["SAME_BUILD_CONTR_IND"]);
            planReviewScheduleDetailBE.ManualAssignmentInd = TryToParse<bool?>(dataRow["MANUAL_ASSIGNMENT_IND"]);
            planReviewScheduleDetailBE.AssignedHoursNbr = TryToParse<decimal?>(dataRow["ASSIGNED_HOURS_NBR"]);
            planReviewScheduleDetailBE.AssignedPlanReviewerId = TryToParse<int?>(dataRow["ASSIGNED_PLAN_REVIEWER_ID"]);
            planReviewScheduleDetailBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewScheduleDetailBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewScheduleDetailBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewScheduleDetailBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            return planReviewScheduleDetailBE;

        }
        private PlanReviewScheduleDetailBE ConvertDataRowToBEWBusinessRef(DataRow dataRow)
        {
            PlanReviewScheduleDetailBE planReviewScheduleDetailBE = new PlanReviewScheduleDetailBE();

            planReviewScheduleDetailBE.PlanReviewScheduleDetailId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_DETAIL_ID"]);
            planReviewScheduleDetailBE.PlanReviewScheduleId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]);
            planReviewScheduleDetailBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            planReviewScheduleDetailBE.StartDt = TryToParse<DateTime?>(dataRow["START_DT"]);
            planReviewScheduleDetailBE.EndDt = TryToParse<DateTime?>(dataRow["END_DT"]);
            planReviewScheduleDetailBE.PoolRequestInd = TryToParse<bool?>(dataRow["POOL_REQUEST_IND"]);
            planReviewScheduleDetailBE.SameBuildContrInd = TryToParse<bool?>(dataRow["SAME_BUILD_CONTR_IND"]);
            planReviewScheduleDetailBE.ManualAssignmentInd = TryToParse<bool?>(dataRow["MANUAL_ASSIGNMENT_IND"]);
            planReviewScheduleDetailBE.AssignedHoursNbr = TryToParse<decimal?>(dataRow["ASSIGNED_HOURS_NBR"]);
            planReviewScheduleDetailBE.AssignedPlanReviewerId = TryToParse<int?>(dataRow["ASSIGNED_PLAN_REVIEWER_ID"]);
            planReviewScheduleDetailBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewScheduleDetailBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewScheduleDetailBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewScheduleDetailBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return planReviewScheduleDetailBE;

        }
        #endregion

    }

    #endregion

}