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

    #region BusinessObject - ProjectOccupancyTypMapRefBO

    public class ProjectOccupancyTypMapRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectOccupancyTypMapRefBE _projectOccupancyTypMapRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectOccupancyTypMapRefBE projectOccupancyTypMapRefBE)
        {
            int id;
            _projectOccupancyTypMapRefBE = projectOccupancyTypMapRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_NM", projectOccupancyTypMapRefBE.ProjectOccupancyTypMapName);

                sqlParameters[1] = new SqlParameter("@WKR_ID_TXT", projectOccupancyTypMapRefBE.UserId);

                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_occupancy_typ_map_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_occupancy_typ_map_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectOccupancyTypMapRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectOccupancyTypMapRefBE projectOccupancyTypMapRefBE = new ProjectOccupancyTypMapRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_occupancy_typ_map_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectOccupancyTypMapRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectOccupancyTypMapRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_occupancy_typ_map_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectOccupancyTypMapRefBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectOccupancyTypMapRefBE> projectOccupancyTypMapRefBEList = new List<ProjectOccupancyTypMapRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_occupancy_typ_map_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectOccupancyTypMapRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectOccupancyTypMapRefBEList;

        }

        public int Update(ProjectOccupancyTypMapRefBE projectOccupancyTypMapRefBE)
        {
            int rows;
            _projectOccupancyTypMapRefBE = projectOccupancyTypMapRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_REF_ID", projectOccupancyTypMapRefBE.ProjectOccupancyTypMapRefId);
                sqlParameters[1] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_NM", projectOccupancyTypMapRefBE.ProjectOccupancyTypMapName);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", projectOccupancyTypMapRefBE.UpdatedDate);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", projectOccupancyTypMapRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_occupancy_typ_map_ref", base.ConnectionString, ref sqlParameters);

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

        private ProjectOccupancyTypMapRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectOccupancyTypMapRefBE projectOccupancyTypMapRefBE = new ProjectOccupancyTypMapRefBE();

            projectOccupancyTypMapRefBE.ProjectOccupancyTypMapRefId = TryToParse<int?>(dataRow["PROJECT_OCCUPANCY_TYP_MAP_REF_ID"]);
            projectOccupancyTypMapRefBE.ProjectOccupancyTypMapName = TryToParse<string>(dataRow["PROJECT_OCCUPANCY_TYP_MAP_NM"]);
            projectOccupancyTypMapRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectOccupancyTypMapRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectOccupancyTypMapRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectOccupancyTypMapRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return projectOccupancyTypMapRefBE;

        }

        #endregion

    }

    #endregion

}