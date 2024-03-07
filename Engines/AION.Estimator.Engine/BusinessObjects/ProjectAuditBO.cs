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

    #region BusinessObject - ProjectAuditBO

    public class ProjectAuditBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectAuditBE _projectAuditBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectAuditBE projectAuditBE)
        {
            int id;
            _projectAuditBE = projectAuditBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectAuditBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@AUDIT_ACTION_DETAILS_TXT", projectAuditBE.AuditActionDetailsTxt);
                sqlParameters[2] = new SqlParameter("@AUDIT_USER_NM", projectAuditBE.AuditUserId);
                sqlParameters[3] = new SqlParameter("@AUDIT_DT", projectAuditBE.AuditDt);
                sqlParameters[4] = new SqlParameter("@AUDIT_ACTION_REF_ID", projectAuditBE.AuditActionRefId);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", projectAuditBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_audit", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_audit", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectAuditBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectAuditBE projectAuditBE = new ProjectAuditBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_audit_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectAuditBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectAuditBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_audit_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectAuditBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectAuditBE> projectAuditBEList = new List<ProjectAuditBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_audit_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectAuditBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectAuditBEList;

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

        private ProjectAuditBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectAuditBE projectAuditBE = new ProjectAuditBE();

            projectAuditBE.ProjectAuditId = TryToParse<int>(dataRow["PROJECT_AUDIT_ID"]);
            projectAuditBE.ProjectId = TryToParse<int>(dataRow["PROJECT_ID"]);
            projectAuditBE.AuditActionDetailsTxt = TryToParse<string>(dataRow["AUDIT_ACTION_DETAILS_TXT"]);
            projectAuditBE.AuditUserId = TryToParse<string>(dataRow["AUDIT_USER_NM"]);
            projectAuditBE.AuditDt = TryToParse<DateTime?>(dataRow["AUDIT_DT"]);
            projectAuditBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectAuditBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectAuditBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectAuditBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectAuditBE.AuditActionRefId = TryToParse<int>(dataRow["AUDIT_ACTION_REF_ID"]);
            projectAuditBE.ProjectCycleId = TryToParse<int>(dataRow["PROJECT_CYCLE_ID"]);
            projectAuditBE.CycleNbr = TryToParse<int?>(dataRow["CYCLE_NBR"]);

            return projectAuditBE;

        }

        #endregion

    }

    #endregion

}