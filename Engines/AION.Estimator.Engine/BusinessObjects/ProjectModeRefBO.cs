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

    #region BusinessObject - ProjectModeRefBO

    public class ProjectModeRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectModeRefBE _projectModeRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectModeRefBE projectModeRefBE)
        {
            int id;
            _projectModeRefBE = projectModeRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];

                sqlParameters[0] = new SqlParameter("@PROJECT_MODE_REF_NM", projectModeRefBE.ProjectModeRefNm);
                sqlParameters[1] = new SqlParameter("@PROJECT_MODE_REF_DISPLAY_NM", projectModeRefBE.ProjectModeRefDisplayNm);
                sqlParameters[6] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", projectModeRefBE.ExternalSystemRefId);
                sqlParameters[7] = new SqlParameter("@SRC_SYSTEM_VALUE_TXT", projectModeRefBE.SrcSystemValueTxt);

                sqlParameters[8] = new SqlParameter("@WKR_ID_TXT", projectModeRefBE.UserId);

                sqlParameters[9] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[9].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_mode_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_mode_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectModeRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectModeRefBE projectModeRefBE = new ProjectModeRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_mode_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectModeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectModeRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_mode_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectModeRefBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectModeRefBE> projectModeRefBEList = new List<ProjectModeRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_mode_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectModeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectModeRefBEList;

        }

        public int Update(ProjectModeRefBE projectModeRefBE)
        {
            int rows;
            _projectModeRefBE = projectModeRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@PROJECT_MODE_REF_ID", projectModeRefBE.ProjectModeRefId);
                sqlParameters[1] = new SqlParameter("@PROJECT_MODE_REF_NM", projectModeRefBE.ProjectModeRefNm);
                sqlParameters[2] = new SqlParameter("@PROJECT_MODE_REF_DISPLAY_NM", projectModeRefBE.ProjectModeRefDisplayNm);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", projectModeRefBE.UpdatedDate);
                sqlParameters[4] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", projectModeRefBE.ExternalSystemRefId);
                sqlParameters[5] = new SqlParameter("@SRC_SYSTEM_VALUE_TXT", projectModeRefBE.SrcSystemValueTxt);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", projectModeRefBE.UserId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_mode_ref", base.ConnectionString, ref sqlParameters);

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

        private ProjectModeRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectModeRefBE projectModeRefBE = new ProjectModeRefBE();

            projectModeRefBE.ProjectModeRefId = TryToParse<int?>(dataRow["PROJECT_MODE_REF_ID"]);
            projectModeRefBE.ProjectModeRefNm = TryToParse<string>(dataRow["PROJECT_MODE_REF_NM"]);
            projectModeRefBE.ProjectModeRefDisplayNm = TryToParse<string>(dataRow["PROJECT_MODE_REF_DISPLAY_NM"]);
            projectModeRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            projectModeRefBE.SrcSystemValueTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VALUE_TXT"]);
            projectModeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectModeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return projectModeRefBE;

        }

        #endregion

    }

    #endregion

}