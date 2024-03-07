#region Using

using AION.Base;
using AION.Base.Services;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - ProjectBusinessRelationshipBO

    public class ProjectBusinessRelationshipBO : BaseBO, IDataContextProjectBusinessRelationshipBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, GetListByProjectId };

        private string _errorMsg;

        private ProjectBusinessRelationshipBE _projectBusinessRelationshipBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectBusinessRelationshipBE projectBusinessRelationshipBE)
        {
            int id;
            _projectBusinessRelationshipBE = projectBusinessRelationshipBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[14];

                sqlParameters[0] = new SqlParameter("@ESTIMATION_HOURS_NBR", projectBusinessRelationshipBE.EstimationHoursNbr);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", projectBusinessRelationshipBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@PROJECT_ID", projectBusinessRelationshipBE.ProjectId);
                sqlParameters[3] = new SqlParameter("@ASSIGNED_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.AssignedPlanReviewerId);
                sqlParameters[4] = new SqlParameter("@PROPOSED_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.ProposedPlanReviewerId);
                sqlParameters[5] = new SqlParameter("@SECONDARY_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.SecondaryPlanReviewerId);
                sqlParameters[6] = new SqlParameter("@PRI_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.PrimaryPlanReviewerId);
                sqlParameters[7] = new SqlParameter("@PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC", projectBusinessRelationshipBE.ProjectBusinessRelationshipStatusDesc);
                sqlParameters[8] = new SqlParameter("@ESTIMATION_NOT_APPLICABLE_IND", projectBusinessRelationshipBE.IsEstimationNotApplicable);
                sqlParameters[9] = new SqlParameter("@WKR_ID_TXT", projectBusinessRelationshipBE.UserId);
                sqlParameters[10] = new SqlParameter("@PROJECT_STATUS_REF_ID", projectBusinessRelationshipBE.StatusRefId);
                sqlParameters[11] = new SqlParameter("@IS_DEPT_REQUESTED_IND", projectBusinessRelationshipBE.IsDeptRequested);
                sqlParameters[12] = new SqlParameter("@ACTUAL_HOURS_NBR", projectBusinessRelationshipBE.ActualHoursNbr);
                sqlParameters[13] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[13].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_business_relationship", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_business_relationship", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectBusinessRelationshipBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectBusinessRelationshipBE projectBusinessRelationshipBE = new ProjectBusinessRelationshipBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_business_relationship_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectBusinessRelationshipBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectBusinessRelationshipBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_business_relationship_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectBusinessRelationshipBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEList = new List<ProjectBusinessRelationshipBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_business_relationship_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBusinessRelationshipBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBusinessRelationshipBEList;

        }

        public int Update(ProjectBusinessRelationshipBE projectBusinessRelationshipBE)
        {
            int rows;
            _projectBusinessRelationshipBE = projectBusinessRelationshipBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[15];

                sqlParameters[0] = new SqlParameter("@PROJECT_BUSINESS_RELATIONSHIP_ID", projectBusinessRelationshipBE.ProjectBusinessRelationshipId);
                sqlParameters[1] = new SqlParameter("@ESTIMATION_HOURS_NBR", projectBusinessRelationshipBE.EstimationHoursNbr);
                sqlParameters[2] = new SqlParameter("@BUSINESS_REF_ID", projectBusinessRelationshipBE.BusinessRefId);
                sqlParameters[3] = new SqlParameter("@PROJECT_ID", projectBusinessRelationshipBE.ProjectId);
                sqlParameters[4] = new SqlParameter("@ASSIGNED_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.AssignedPlanReviewerId);
                sqlParameters[5] = new SqlParameter("@PROPOSED_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.ProposedPlanReviewerId);
                sqlParameters[6] = new SqlParameter("@SECONDARY_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.SecondaryPlanReviewerId);
                sqlParameters[7] = new SqlParameter("@PRI_PLAN_REVIEWER_ID", projectBusinessRelationshipBE.PrimaryPlanReviewerId);
                sqlParameters[8] = new SqlParameter("@PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC", projectBusinessRelationshipBE.ProjectBusinessRelationshipStatusDesc);
                sqlParameters[9] = new SqlParameter("@ESTIMATION_NOT_APPLICABLE_IND", projectBusinessRelationshipBE.IsEstimationNotApplicable);
                sqlParameters[10] = new SqlParameter("@UPDATED_DTTM", projectBusinessRelationshipBE.UpdatedDate);
                sqlParameters[11] = new SqlParameter("@WKR_ID_TXT", projectBusinessRelationshipBE.UserId);
                sqlParameters[12] = new SqlParameter("@PROJECT_STATUS_REF_ID", projectBusinessRelationshipBE.StatusRefId);
                sqlParameters[13] = new SqlParameter("@ACTUAL_HOURS_NBR", projectBusinessRelationshipBE.ActualHoursNbr);

                sqlParameters[14] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[14].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_business_relationship", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }
        public List<ProjectBusinessRelationshipBE> GetListByProjectId(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEList = new List<ProjectBusinessRelationshipBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_business_relationship_get_list_byprojectid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBusinessRelationshipBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBusinessRelationshipBEList;

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

        private ProjectBusinessRelationshipBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectBusinessRelationshipBE projectBusinessRelationshipBE = new ProjectBusinessRelationshipBE();

            projectBusinessRelationshipBE.ProjectBusinessRelationshipId = TryToParse<int?>(dataRow["PROJECT_BUSINESS_RELATIONSHIP_ID"]);
            projectBusinessRelationshipBE.EstimationHoursNbr = TryToParse<decimal?>(dataRow["ESTIMATION_HOURS_NBR"]);
            projectBusinessRelationshipBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            projectBusinessRelationshipBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            projectBusinessRelationshipBE.AssignedPlanReviewerId = TryToParse<int?>(dataRow["ASSIGNED_PLAN_REVIEWER_ID"]);
            projectBusinessRelationshipBE.ProposedPlanReviewerId = TryToParse<int?>(dataRow["PROPOSED_PLAN_REVIEWER_ID"]);
            projectBusinessRelationshipBE.SecondaryPlanReviewerId = TryToParse<int?>(dataRow["SECONDARY_PLAN_REVIEWER_ID"]);
            projectBusinessRelationshipBE.PrimaryPlanReviewerId = TryToParse<int?>(dataRow["PRI_PLAN_REVIEWER_ID"]);
            projectBusinessRelationshipBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectBusinessRelationshipBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectBusinessRelationshipBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectBusinessRelationshipBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectBusinessRelationshipBE.ProjectBusinessRelationshipStatusDesc = TryToParse<string>(dataRow["PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC"]);
            projectBusinessRelationshipBE.IsEstimationNotApplicable = TryToParse<bool>(dataRow["ESTIMATION_NOT_APPLICABLE_IND"]);
            projectBusinessRelationshipBE.StatusRefId = TryToParse<int>(dataRow["PROJECT_STATUS_REF_ID"]);
            projectBusinessRelationshipBE.IsDeptRequested = TryToParse<bool>(dataRow["IS_DEPT_REQUESTED_IND"]);
            projectBusinessRelationshipBE.ActualHoursNbr = TryToParse<decimal?>(dataRow["ACTUAL_HOURS_NBR"]);
            return projectBusinessRelationshipBE;

        }

        #endregion

    }

    #endregion

}