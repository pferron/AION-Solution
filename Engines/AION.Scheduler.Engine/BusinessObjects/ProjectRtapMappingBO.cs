#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Base;
using AION.Engine.BusinessEntities;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{

	#region BusinessObject - ProjectRtapMappingBO

	public class ProjectRtapMappingBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private ProjectRtapMappingBE _projectRtapMappingBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(ProjectRtapMappingBE projectRtapMappingBE)
		{
			int id;
			_projectRtapMappingBE = projectRtapMappingBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[4];

				sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectRtapMappingBE.ProjectId);
				sqlParameters[1] = new SqlParameter("@ORIGINAL_PROJECT_ID", projectRtapMappingBE.OriginalProjectId);

				sqlParameters[2] = new SqlParameter("@WKR_ID_TXT",projectRtapMappingBE.CreatedByWkrId);

				sqlParameters[3] = new SqlParameter("@ReturnValue",0);
				sqlParameters[3].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_rtap_mapping", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_rtap_mapping", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public ProjectRtapMappingBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			ProjectRtapMappingBE projectRtapMappingBE = new ProjectRtapMappingBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_rtap_mapping_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				projectRtapMappingBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return projectRtapMappingBE;
		}

		public ProjectRtapMappingBE GetByProjectId(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			ProjectRtapMappingBE projectRtapMappingBE = new ProjectRtapMappingBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_rtap_mapping_get_by_project_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				projectRtapMappingBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return projectRtapMappingBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_rtap_mapping_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<ProjectRtapMappingBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ProjectRtapMappingBE> projectRtapMappingBEList = new List<ProjectRtapMappingBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_rtap_mapping_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					projectRtapMappingBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return projectRtapMappingBEList;

		}

		public int Update(ProjectRtapMappingBE projectRtapMappingBE)
		{
			int rows;
			_projectRtapMappingBE = projectRtapMappingBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@PROJECT_RTAP_MAPPING_ID", projectRtapMappingBE.ProjectRtapMappingId);
				sqlParameters[1] = new SqlParameter("@PROJECT_ID", projectRtapMappingBE.ProjectId);
				sqlParameters[2] = new SqlParameter("@ORIGINAL_PROJECT_ID", projectRtapMappingBE.OriginalProjectId);
				sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", projectRtapMappingBE.UpdatedDate);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",projectRtapMappingBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_rtap_mapping", base.ConnectionString, ref sqlParameters);

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
					return(_errorMsg == String.Empty);

				case ActionType.Delete:
					return(_errorMsg == String.Empty);

				case ActionType.GetById:
					return(_errorMsg == String.Empty);

				case ActionType.GetDataSet:
					return(_errorMsg == String.Empty);

				case ActionType.GetList:
					return(_errorMsg == String.Empty);

				case ActionType.Update:
					return(_errorMsg == String.Empty);

				default:
					break;
			}

			return true;

		}

		private ProjectRtapMappingBE ConvertDataRowToBE(DataRow dataRow)
		{
			ProjectRtapMappingBE projectRtapMappingBE = new ProjectRtapMappingBE();

			projectRtapMappingBE.ProjectRtapMappingId = TryToParse<int?>(dataRow["PROJECT_RTAP_MAPPING_ID"]);
			projectRtapMappingBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
			projectRtapMappingBE.OriginalProjectId = TryToParse<int?>(dataRow["ORIGINAL_PROJECT_ID"]);
			projectRtapMappingBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			projectRtapMappingBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			projectRtapMappingBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			projectRtapMappingBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			projectRtapMappingBE.OriginalProjectNumber = TryToParse<string>(dataRow["ORIGINAL_PROJECT_NUMBER"]);

			return projectRtapMappingBE;

		}

		#endregion

	}

	#endregion

}