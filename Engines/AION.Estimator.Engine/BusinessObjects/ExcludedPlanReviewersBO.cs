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

    #region BusinessObject - ExcludedPlanReviewersBO

    public class ExcludedPlanReviewersBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ExcludedPlanReviewersBE _excludedPlanReviewersBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ExcludedPlanReviewersBE excludedPlanReviewersBE)
        {
            int id;
            _excludedPlanReviewersBE = excludedPlanReviewersBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@PLAN_REVIEWER_ID", excludedPlanReviewersBE.PlanReviewerId);
                sqlParameters[1] = new SqlParameter("@PROJECT_BUSINESS_RELATIONSHIP_ID", excludedPlanReviewersBE.ProjectBusinessRelationshipId);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", excludedPlanReviewersBE.UserId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_excluded_plan_reviewers", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_excluded_plan_reviewers", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public List<ExcludedPlanReviewersBE> GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));
            List<ExcludedPlanReviewersBE> excludedPlanReviewersBEList = new List<ExcludedPlanReviewersBE>();

            ExcludedPlanReviewersBE excludedPlanReviewersBE = new ExcludedPlanReviewersBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_excluded_plan_reviewers_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                excludedPlanReviewersBEList.Add(this.ConvertDataRowToBE(dataRow));
            }

            return excludedPlanReviewersBEList;
        }

        public List<ExcludedPlanReviewersBE> GetListByProjectDepartmentID(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ExcludedPlanReviewersBE> excludedPlanReviewersBEList = new List<ExcludedPlanReviewersBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@prjdeptID", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_excluded_plan_reviewers_get_list_by_PrjDeptID", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    excludedPlanReviewersBEList.Add(this.ConvertDataRowToBE(dataRow));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return excludedPlanReviewersBEList;

        }

        public int SyncExcludedPlanReviewers(int ProjectBusinessRelationshipId, List<int> excludedPlanreviewersList, int updatingUserID)
        {
            if (excludedPlanreviewersList.Count == 0)
                return 0;
            int rows = 0;
            try
            {
                string excludedPlanReviewers = "";
                foreach (var item in excludedPlanreviewersList)
                {
                    if (excludedPlanReviewers == "")
                        excludedPlanReviewers = item.ToString();
                    else
                        excludedPlanReviewers = excludedPlanReviewers + "," + item.ToString();
                }
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@PLAN_REVIEWER_ID_LIST", excludedPlanReviewers);
                sqlParameters[1] = new SqlParameter("@PROJECT_BUSINESS_RELATIONSHIP_ID", ProjectBusinessRelationshipId);
                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", updatingUserID.ToString());

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_sync_aion_excluded_plan_reviewers", base.ConnectionString, ref sqlParameters);

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

        private ExcludedPlanReviewersBE ConvertDataRowToBE(DataRow dataRow)
        {
            ExcludedPlanReviewersBE excludedPlanReviewersBE = new ExcludedPlanReviewersBE();

            excludedPlanReviewersBE.ExcludedPlanReviewerId = TryToParse<int?>(dataRow["EXCLUDED_PLAN_REVIEWERS_ID"]);
            excludedPlanReviewersBE.PlanReviewerId = TryToParse<int?>(dataRow["PLAN_REVIEWER_ID"]);
            excludedPlanReviewersBE.ProjectBusinessRelationshipId = TryToParse<int?>(dataRow["PROJECT_BUSINESS_RELATIONSHIP_ID"]);
            excludedPlanReviewersBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            excludedPlanReviewersBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            excludedPlanReviewersBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            excludedPlanReviewersBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            excludedPlanReviewersBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            return excludedPlanReviewersBE;

        }

        #endregion

    }

    #endregion

}