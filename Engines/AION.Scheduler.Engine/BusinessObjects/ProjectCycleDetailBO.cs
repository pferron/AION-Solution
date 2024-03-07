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

    #region BusinessObject - ProjectCycleDetailBO

    public class ProjectCycleDetailBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private ProjectCycleDetailBE _projectCycleDetailBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(ProjectCycleDetailBE projectCycleDetailBE)
		{
			int id;
			_projectCycleDetailBE = projectCycleDetailBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[5];

				sqlParameters[0] = new SqlParameter("@PROJECT_CYCLE_ID", projectCycleDetailBE.ProjectCycleId);
				sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", projectCycleDetailBE.BusinessRefId);
				sqlParameters[2] = new SqlParameter("@REREVIEW_HOURS_NBR", projectCycleDetailBE.RereviewHoursNbr);

				sqlParameters[3] = new SqlParameter("@WKR_ID_TXT",projectCycleDetailBE.UserId);

				sqlParameters[4] = new SqlParameter("@ReturnValue",0);
				sqlParameters[4].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_cycle_detail", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_cycle_detail", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public ProjectCycleDetailBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			ProjectCycleDetailBE projectCycleDetailBE = new ProjectCycleDetailBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_detail_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				projectCycleDetailBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return projectCycleDetailBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_detail_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<ProjectCycleDetailBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ProjectCycleDetailBE> projectCycleDetailBEList = new List<ProjectCycleDetailBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_detail_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					projectCycleDetailBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return projectCycleDetailBEList;

		}

		public List<ProjectCycleDetailBE> GetListByProjectCycle(int projectCycleId)
		{
			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ProjectCycleDetailBE> projectCycleDetailBEList = new List<ProjectCycleDetailBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@projectCycleId", projectCycleId);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_detail_get_list_by_project_cycle_id", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					projectCycleDetailBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return projectCycleDetailBEList;

		}

		public int Update(ProjectCycleDetailBE projectCycleDetailBE)
		{
			int rows;
			_projectCycleDetailBE = projectCycleDetailBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[7];

				sqlParameters[0] = new SqlParameter("@PROJECT_CYCLE_DETAIL_ID", projectCycleDetailBE.ProjectCycleDetailId);
				sqlParameters[1] = new SqlParameter("@PROJECT_CYCLE_ID", projectCycleDetailBE.ProjectCycleId);
				sqlParameters[2] = new SqlParameter("@BUSINESS_REF_ID", projectCycleDetailBE.BusinessRefId);
				sqlParameters[3] = new SqlParameter("@REREVIEW_HOURS_NBR", projectCycleDetailBE.RereviewHoursNbr);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", projectCycleDetailBE.UpdatedDate);

				sqlParameters[5] = new SqlParameter("@WKR_ID_TXT",projectCycleDetailBE.UserId);

				sqlParameters[6] = new SqlParameter("@ReturnValue",0);
				sqlParameters[6].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_cycle_detail", base.ConnectionString, ref sqlParameters);

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

		private ProjectCycleDetailBE ConvertDataRowToBE(DataRow dataRow)
		{
			ProjectCycleDetailBE projectCycleDetailBE = new ProjectCycleDetailBE();

			projectCycleDetailBE.ProjectCycleDetailId = TryToParse<int?>(dataRow["PROJECT_CYCLE_DETAIL_ID"]);
			projectCycleDetailBE.ProjectCycleId = TryToParse<int?>(dataRow["PROJECT_CYCLE_ID"]);
			projectCycleDetailBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
			projectCycleDetailBE.RereviewHoursNbr = TryToParse<decimal?>(dataRow["REREVIEW_HOURS_NBR"]);
			projectCycleDetailBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			projectCycleDetailBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			projectCycleDetailBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			projectCycleDetailBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return projectCycleDetailBE;

		}

		#endregion

	}

	#endregion

}