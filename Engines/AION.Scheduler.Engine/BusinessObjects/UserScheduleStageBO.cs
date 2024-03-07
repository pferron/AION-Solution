#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessObject - UserScheduleStageBO

	public class UserScheduleStageBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private UserScheduleStageBE _userScheduleStageBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(UserScheduleStageBE userScheduleStageBE)
		{
			int id;
			_userScheduleStageBE = userScheduleStageBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[8];

				sqlParameters[0] = new SqlParameter("@START_DTTM", userScheduleStageBE.StartDate);
				sqlParameters[1] = new SqlParameter("@END_DTTM", userScheduleStageBE.EndDate);
				sqlParameters[2] = new SqlParameter("@PROJECT_SCHEDULE_ID", DBNull.Value);
				sqlParameters[3] = new SqlParameter("@USER_ID", userScheduleStageBE.UserID);
				sqlParameters[4] = new SqlParameter("@BUSINESS_REF_ID", userScheduleStageBE.BusinessRefID);
				sqlParameters[5] = new SqlParameter("@PROJECT_ID", userScheduleStageBE.ProjectID);

				sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",userScheduleStageBE.UserId);

				sqlParameters[7] = new SqlParameter("@ReturnValue",0);
				sqlParameters[7].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_schedule_stage", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return id;
		}

		public int Delete(int project_id)
		{
			int rows;
			_id = project_id;

			if (!this.Validate(ActionType.Delete))
				throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[2];

				sqlParameters[0] = new SqlParameter("@project_id", project_id);

				sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
				sqlParameters[1].Direction = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_schedule_stage", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public UserScheduleStageBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			UserScheduleStageBE userScheduleStageBE = new UserScheduleStageBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_stage_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				userScheduleStageBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return userScheduleStageBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_stage_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<UserScheduleStageBE> GetListByProjectID(int project_id)
		{
			_id = project_id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<UserScheduleStageBE> userScheduleStageBEList = new List<UserScheduleStageBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@project_id", project_id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_stage_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					userScheduleStageBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return userScheduleStageBEList;

		}

		public int Update(UserScheduleStageBE userScheduleStageBE)
		{
			int rows;
			_userScheduleStageBE = userScheduleStageBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[10];

				sqlParameters[0] = new SqlParameter("@USER_SCHEDULE_STAGE_IDENTIFIER", userScheduleStageBE.UserScheduleStageIdentifer);
				sqlParameters[1] = new SqlParameter("@START_DTTM", userScheduleStageBE.StartDate);
				sqlParameters[2] = new SqlParameter("@END_DTTM", userScheduleStageBE.EndDate);
				sqlParameters[3] = new SqlParameter("@PROJECT_SCHEDULE_ID", DBNull.Value);
				sqlParameters[4] = new SqlParameter("@USER_ID", userScheduleStageBE.UserID);
				sqlParameters[5] = new SqlParameter("@BUSINESS_REF_ID", userScheduleStageBE.BusinessRefID);
				sqlParameters[6] = new SqlParameter("@UPDATED_DTTM", userScheduleStageBE.UpdatedDate);
				sqlParameters[7] = new SqlParameter("@PROJECT_ID", userScheduleStageBE.ProjectID);

				sqlParameters[8] = new SqlParameter("@WKR_ID_TXT",userScheduleStageBE.UserId);

				sqlParameters[9] = new SqlParameter("@ReturnValue",0);
				sqlParameters[9].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_schedule_stage", base.ConnectionString, ref sqlParameters);

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

		private UserScheduleStageBE ConvertDataRowToBE(DataRow dataRow)
		{
			UserScheduleStageBE userScheduleStageBE = new UserScheduleStageBE();

			userScheduleStageBE.UserScheduleStageIdentifer = TryToParse<int?>(dataRow["USER_SCHEDULE_STAGE_IDENTIFIER"]);
			userScheduleStageBE.StartDate = TryToParse<DateTime?>(dataRow["START_DTTM"]);
			userScheduleStageBE.EndDate = TryToParse<DateTime?>(dataRow["END_DTTM"]);
			userScheduleStageBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
			userScheduleStageBE.BusinessRefID = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
			userScheduleStageBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			userScheduleStageBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			userScheduleStageBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			userScheduleStageBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			userScheduleStageBE.ProjectID = TryToParse<int?>(dataRow["PROJECT_ID"]);

			return userScheduleStageBE;

		}

		#endregion

	}

	#endregion

}