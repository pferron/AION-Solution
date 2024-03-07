#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using AION.Manager.Adapters;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{

	#region BusinessObject - ProjectCycleBO

	public class ProjectCycleBO : BaseBO, IDataContextProjectCycleBO
    {

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private ProjectCycleBE _projectCycleBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(ProjectCycleBE projectCycleBE)
		{
			int id;
			_projectCycleBE = projectCycleBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[13];

				sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectCycleBE.ProjectId);
				sqlParameters[1] = new SqlParameter("@CURRENT_CYCLE_IND", projectCycleBE.CurrentCycleInd);
				sqlParameters[2] = new SqlParameter("@FUTURE_CYCLE_IND", projectCycleBE.FutureCycleInd);
				sqlParameters[3] = new SqlParameter("@CYCLE_NBR", projectCycleBE.CycleNbr);
				sqlParameters[4] = new SqlParameter("@PLANS_READY_ON_DT", projectCycleBE.PlansReadyOnDt);
				sqlParameters[5] = new SqlParameter("@IS_COMPLETE_IND", projectCycleBE.IsCompleteInd);
				sqlParameters[6] = new SqlParameter("@GATE_DT", projectCycleBE.GateDt);
				sqlParameters[7] = new SqlParameter("@SCHEDULE_AFTER_DT", projectCycleBE.ScheduleAfterDt);
				sqlParameters[8] = new SqlParameter("@RESPONDER_USER_ID", projectCycleBE.ResponderUserId);
				sqlParameters[9] = new SqlParameter("@IS_APRV_IND", projectCycleBE.IsAprvInd);
				sqlParameters[10] = new SqlParameter("@RESPONSE_DT", projectCycleBE.ResponseDt);

				sqlParameters[11] = new SqlParameter("@WKR_ID_TXT",projectCycleBE.UserId);

				sqlParameters[12] = new SqlParameter("@ReturnValue",0);
				sqlParameters[12].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_cycle", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_cycle", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public ProjectCycleBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			ProjectCycleBE projectCycleBE = new ProjectCycleBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				projectCycleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return projectCycleBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<ProjectCycleBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ProjectCycleBE> projectCycleBEList = new List<ProjectCycleBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					projectCycleBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return projectCycleBEList;

		}

		public List<ProjectCycleBE> GetListByProject(int projectId)
		{
			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ProjectCycleBE> projectCycleBEList = new List<ProjectCycleBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@projectId", projectId);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_cycle_get_list_by_project_id", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					projectCycleBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return projectCycleBEList;

		}

		public int Update(ProjectCycleBE projectCycleBE)
		{
			int rows;
			_projectCycleBE = projectCycleBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[16];

				sqlParameters[0] = new SqlParameter("@PROJECT_CYCLE_ID", projectCycleBE.ProjectCycleId);
				sqlParameters[1] = new SqlParameter("@PROJECT_ID", projectCycleBE.ProjectId);
				sqlParameters[2] = new SqlParameter("@CURRENT_CYCLE_IND", projectCycleBE.CurrentCycleInd);
				sqlParameters[3] = new SqlParameter("@FUTURE_CYCLE_IND", projectCycleBE.FutureCycleInd);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", projectCycleBE.UpdatedDate);
				sqlParameters[5] = new SqlParameter("@CYCLE_NBR", projectCycleBE.CycleNbr);
				sqlParameters[6] = new SqlParameter("@PLANS_READY_ON_DT", projectCycleBE.PlansReadyOnDt);
				sqlParameters[7] = new SqlParameter("@IS_COMPLETE_IND", projectCycleBE.IsCompleteInd);
				sqlParameters[8] = new SqlParameter("@GATE_DT", projectCycleBE.GateDt);
				sqlParameters[9] = new SqlParameter("@SCHEDULE_AFTER_DT", projectCycleBE.ScheduleAfterDt);
				sqlParameters[10] = new SqlParameter("@RESPONDER_USER_ID", projectCycleBE.ResponderUserId);
				sqlParameters[11] = new SqlParameter("@IS_APRV_IND", projectCycleBE.IsAprvInd);
				sqlParameters[12] = new SqlParameter("@RESPONSE_DT", projectCycleBE.ResponseDt);

				sqlParameters[13] = new SqlParameter("@WKR_ID_TXT",projectCycleBE.UserId);
                sqlParameters[14] = new SqlParameter("@INCREMENT_ON_PLANS_RECEIVED_IND", projectCycleBE.IncrementOnPlansReceivedInd);

                sqlParameters[15] = new SqlParameter("@ReturnValue",0);
				sqlParameters[15].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_cycle", base.ConnectionString, ref sqlParameters);

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

		private ProjectCycleBE ConvertDataRowToBE(DataRow dataRow)
		{
			ProjectCycleBE projectCycleBE = new ProjectCycleBE();

			projectCycleBE.ProjectCycleId = TryToParse<int?>(dataRow["PROJECT_CYCLE_ID"]);
			projectCycleBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
			projectCycleBE.CurrentCycleInd = TryToParse<bool?>(dataRow["CURRENT_CYCLE_IND"]);
			projectCycleBE.FutureCycleInd = TryToParse<bool?>(dataRow["FUTURE_CYCLE_IND"]);
			projectCycleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			projectCycleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			projectCycleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			projectCycleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			projectCycleBE.CycleNbr = TryToParse<int?>(dataRow["CYCLE_NBR"]);
			projectCycleBE.PlansReadyOnDt = TryToParse<DateTime?>(dataRow["PLANS_READY_ON_DT"]);
			projectCycleBE.IsCompleteInd = TryToParse<bool?>(dataRow["IS_COMPLETE_IND"]);
			projectCycleBE.GateDt = TryToParse<DateTime?>(dataRow["GATE_DT"]);
			projectCycleBE.ScheduleAfterDt = TryToParse<DateTime?>(dataRow["SCHEDULE_AFTER_DT"]);
			projectCycleBE.ResponderUserId = TryToParse<int?>(dataRow["RESPONDER_USER_ID"]);
			projectCycleBE.IsAprvInd = TryToParse<bool?>(dataRow["IS_APRV_IND"]);
			projectCycleBE.ResponseDt = TryToParse<DateTime?>(dataRow["RESPONSE_DT"]);
			projectCycleBE.IncrementOnPlansReceivedInd = TryToParse<bool?>(dataRow["INCREMENT_ON_PLANS_RECEIVED_IND"]);

			return projectCycleBE;

		}

		

		#endregion

	}

	#endregion

}