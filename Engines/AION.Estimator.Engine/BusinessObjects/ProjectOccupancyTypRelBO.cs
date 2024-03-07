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

    #region BusinessObject - ProjectOccupancyTypRelBO

    public class ProjectOccupancyTypRelBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectOccupancyTypRelBE _projectOccupancyTypRelBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectOccupancyTypRelBE projectOccupancyTypRelBE)
        {
            int id;
            _projectOccupancyTypRelBE = projectOccupancyTypRelBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", projectOccupancyTypRelBE.OccupancyTypRefId);
                sqlParameters[1] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_REF_ID", projectOccupancyTypRelBE.ProjectOccupancyTypMapRefId);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", projectOccupancyTypRelBE.UserId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_occupancy_typ_rel", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_occupancy_typ_rel", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectOccupancyTypRelBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectOccupancyTypRelBE projectOccupancyTypRelBE = new ProjectOccupancyTypRelBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_occupancy_typ_rel_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectOccupancyTypRelBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectOccupancyTypRelBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_occupancy_typ_rel_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectOccupancyTypRelBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectOccupancyTypRelBE> projectOccupancyTypRelBEList = new List<ProjectOccupancyTypRelBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_occupancy_typ_rel_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectOccupancyTypRelBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectOccupancyTypRelBEList;

        }

        public int Update(ProjectOccupancyTypRelBE projectOccupancyTypRelBE)
        {
            int rows;
            _projectOccupancyTypRelBE = projectOccupancyTypRelBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID", projectOccupancyTypRelBE.ProjectOccupancyTypeRelationshipId);
                sqlParameters[1] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", projectOccupancyTypRelBE.OccupancyTypRefId);
                sqlParameters[2] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_REF_ID", projectOccupancyTypRelBE.ProjectOccupancyTypMapRefId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", projectOccupancyTypRelBE.UpdatedDate);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", projectOccupancyTypRelBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_occupancy_typ_rel", base.ConnectionString, ref sqlParameters);

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

        private ProjectOccupancyTypRelBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectOccupancyTypRelBE projectOccupancyTypRelBE = new ProjectOccupancyTypRelBE();

            projectOccupancyTypRelBE.ProjectOccupancyTypeRelationshipId = TryToParse<int?>(dataRow["PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID"]);
            projectOccupancyTypRelBE.OccupancyTypRefId = TryToParse<int?>(dataRow["OCCUPANCY_TYP_REF_ID"]);
            projectOccupancyTypRelBE.ProjectOccupancyTypMapRefId = TryToParse<int?>(dataRow["PROJECT_OCCUPANCY_TYP_MAP_REF_ID"]);
            projectOccupancyTypRelBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectOccupancyTypRelBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectOccupancyTypRelBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectOccupancyTypRelBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return projectOccupancyTypRelBE;

        }

        #endregion

    }

    #endregion

}