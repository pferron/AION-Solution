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

    #region BusinessObject - ProjectTypeRefBO

    public class ProjectTypeRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectTypeRefBE _projectTypeRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public ProjectTypeRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectTypeRefBE projectTypeRefBE = new ProjectTypeRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectTypeRefBE;
        }

        public List<ProjectTypeRefBE> GetList()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectTypeRefBE> projectTypeRefBEList = new List<ProjectTypeRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_type_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectTypeRefBEList;

        }


        public int UpdateAutoAssignFacilitator(string projectTypeRefIdCsvList, bool autoAssignFacilitator, string wkrId)
        {
            int rows;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@PROJECT_TYPE_REF_ID_CSV", projectTypeRefIdCsvList);
                sqlParameters[1] = new SqlParameter("@AUTO_ASSIGN_FACILITATOR_IND", autoAssignFacilitator);
                sqlParameters[2] = new SqlParameter("@WKR_ID_UPDATED_TXT", wkrId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_type_ref_autoassign_csvlist", base.ConnectionString, ref sqlParameters);

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

        private ProjectTypeRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectTypeRefBE projectTypeRefBE = new ProjectTypeRefBE();

            projectTypeRefBE.ProjectTypRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            projectTypeRefBE.ProjectTypRefNm = TryToParse<string>(dataRow["PROJECT_TYP_REF_NM"]);
            projectTypeRefBE.ProjectTypRefDisplayNm = TryToParse<string>(dataRow["PROJECT_TYP_REF_DISPLAY_NM"]);
            projectTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectTypeRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            projectTypeRefBE.SrcSystemValueTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            projectTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectTypeRefBE.AutoAssignFacilitator = TryToParse<bool?>(dataRow["AUTO_ASSIGN_FACILITATOR_IND"]);
            projectTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);

            return projectTypeRefBE;

        }

        #endregion

    }

    #endregion

}