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

    #region BusinessObject - ProjectStatusRefBO

    public class ProjectStatusRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectStatusRefBE _projectStatusRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectStatusRefBE projectStatusRefBE)
        {
            int id;
            _projectStatusRefBE = projectStatusRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];

                sqlParameters[0] = new SqlParameter("@PROJECT_STATUS_REF_NM", projectStatusRefBE.ProjectStatusRefNm);
                sqlParameters[1] = new SqlParameter("@PROJECT_STATUS_REF_DESC", projectStatusRefBE.ProjectStatusRefNm);
                sqlParameters[2] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", projectStatusRefBE.ExternalSystemRefId);
                sqlParameters[3] = new SqlParameter("@SRC_SYSTEM_VALUE_TXT", projectStatusRefBE.SrcSystemValueTxt);
                sqlParameters[4] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", projectStatusRefBE.EnumMappingValNbr);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", projectStatusRefBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_status_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_status_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectStatusRefBE GetByExternalstatusRef(string projectStatusExternalRef, int externalSystemEnum)
        {
            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectStatusRefBE projectStatusRefBE = new ProjectStatusRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectStatusExternalRef", projectStatusExternalRef);
                sqlParameters[1] = new SqlParameter("@externalSystemEnum", externalSystemEnum);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_status_ref_get_by_projectStatusEnum", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectStatusRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectStatusRefBE;
        }

        public ProjectStatusRefBE GetByEnum(int projectStatusEnum)
        {
            _id = projectStatusEnum;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectStatusRefBE projectStatusRefBE = new ProjectStatusRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectStatusEnum", projectStatusEnum);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_status_ref_get_by_projectStatusEnum", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectStatusRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectStatusRefBE;
        }

        public ProjectStatusRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectStatusRefBE projectStatusRefBE = new ProjectStatusRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_status_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectStatusRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectStatusRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_status_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectStatusRefBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectStatusRefBE> projectStatusRefBEList = new List<ProjectStatusRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_status_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectStatusRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectStatusRefBEList;

        }

        public List<ProjectStatusRefBE> GetAllList()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectStatusRefBE> projectStatusRefBEList = new List<ProjectStatusRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_status_ref_get_Alllist", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectStatusRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectStatusRefBEList;

        }

        public int Update(ProjectStatusRefBE projectStatusRefBE)
        {
            int rows;
            _projectStatusRefBE = projectStatusRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@PROJECT_STATUS_REF_NM", projectStatusRefBE.ProjectStatusRefNm);
                sqlParameters[1] = new SqlParameter("@PROJECT_STATUS_REF_DESC", projectStatusRefBE.ProjectStatusRefNm);
                sqlParameters[2] = new SqlParameter("@PROJECT_STATUS_REF_ID", projectStatusRefBE.ProjectStatusRefId);
                sqlParameters[3] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", projectStatusRefBE.ExternalSystemRefId);
                sqlParameters[4] = new SqlParameter("@SRC_SYSTEM_VALUE_TXT", projectStatusRefBE.SrcSystemValueTxt);
                sqlParameters[5] = new SqlParameter("@UPDATED_DTTM", projectStatusRefBE.UpdatedDate);
                sqlParameters[6] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", projectStatusRefBE.EnumMappingValNbr);

                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", projectStatusRefBE.UserId);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_status_ref", base.ConnectionString, ref sqlParameters);

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

        private ProjectStatusRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectStatusRefBE projectStatusRefBE = new ProjectStatusRefBE();

            projectStatusRefBE.ProjectStatusRefNm = TryToParse<string>(dataRow["PROJECT_STATUS_REF_NM"]);
            projectStatusRefBE.ProjectStatusRefNm = TryToParse<string>(dataRow["PROJECT_STATUS_REF_DESC"]);
            projectStatusRefBE.ProjectStatusRefId = TryToParse<int?>(dataRow["PROJECT_STATUS_REF_ID"]);
            projectStatusRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            projectStatusRefBE.SrcSystemValueTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            projectStatusRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectStatusRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectStatusRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);

            return projectStatusRefBE;

        }

        #endregion

    }

    #endregion

}