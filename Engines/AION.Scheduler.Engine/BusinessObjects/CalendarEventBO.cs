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

	#region BusinessObject - CalendarEventBO

	public class CalendarEventBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private CalendarEventBE _calendarEventBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(CalendarEventBE calendarEventBE)
		{
			int id;
			_calendarEventBE = calendarEventBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[9];

				sqlParameters[0] = new SqlParameter("@JSON_OBJECT_TXT", calendarEventBE.JsonObjectTxt);
				sqlParameters[1] = new SqlParameter("@ACTION_DESC", calendarEventBE.ActionDesc);
				sqlParameters[2] = new SqlParameter("@PROCESSED_IND", calendarEventBE.ProcessedInd);
				sqlParameters[3] = new SqlParameter("@PROCESSED_DTTM", calendarEventBE.ProcessedDate);
				sqlParameters[4] = new SqlParameter("@IN_PROCESS_IND", calendarEventBE.InProcessInd);
				sqlParameters[5] = new SqlParameter("@IN_PROCESS_DTTM", calendarEventBE.InProcessDate);

				sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",calendarEventBE.UserId);
				sqlParameters[7] = new SqlParameter("@RETRY_CNT", calendarEventBE.RetryCount);

				sqlParameters[8] = new SqlParameter("@ReturnValue",0);
				sqlParameters[8].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_calendar_event_queue_v2", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_calendar_event_queue", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public CalendarEventBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			CalendarEventBE calendarEventBE = new CalendarEventBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_calendar_event_queue_get_by_id_v2", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				calendarEventBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return calendarEventBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_calendar_event_queue_get_list_v2", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<CalendarEventBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<CalendarEventBE> calendarEventBEList = new List<CalendarEventBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_calendar_event_queue_get_list_v2", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					calendarEventBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return calendarEventBEList;

		}

		public List<CalendarEventBE> GetListByStatus(bool inProcess)
		{
			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<CalendarEventBE> calendarEventBEList = new List<CalendarEventBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[2];

				int processedInd = 0;
				int inProcessInd = 0;

				if (inProcess)
                {
					inProcessInd = 1;
				}

				sqlParameters[0] = new SqlParameter("@PROCESSED_IND", processedInd);
				sqlParameters[1] = new SqlParameter("@IN_PROCESS_IND", inProcessInd);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_calendar_event_queue_get_list_for_processing_v2", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					calendarEventBEList.Add(this.ConvertDataRowToBE(dataRow));
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return calendarEventBEList;

		}

		public int Update(CalendarEventBE calendarEventBE)
		{
			int rows;
			_calendarEventBE = calendarEventBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[11];

				sqlParameters[0] = new SqlParameter("@CALENDAR_EVENT_QUEUE_ID", calendarEventBE.CalendarEventQueueId);
				sqlParameters[1] = new SqlParameter("@JSON_OBJECT_TXT", calendarEventBE.JsonObjectTxt);
				sqlParameters[2] = new SqlParameter("@ACTION_DESC", calendarEventBE.ActionDesc);
				sqlParameters[3] = new SqlParameter("@PROCESSED_IND", calendarEventBE.ProcessedInd);
				sqlParameters[4] = new SqlParameter("@PROCESSED_DTTM", calendarEventBE.ProcessedDate);
				sqlParameters[5] = new SqlParameter("@IN_PROCESS_IND", calendarEventBE.InProcessInd);
				sqlParameters[6] = new SqlParameter("@IN_PROCESS_DTTM", calendarEventBE.InProcessDate);
				sqlParameters[7] = new SqlParameter("@UPDATED_DTTM", calendarEventBE.UpdatedDate);

				sqlParameters[8] = new SqlParameter("@WKR_ID_TXT",calendarEventBE.UserId);

				sqlParameters[9] = new SqlParameter("@RETRY_CNT", calendarEventBE.RetryCount);

				sqlParameters[10] = new SqlParameter("@ReturnValue",0);
				sqlParameters[10].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_calendar_event_queue_v2", base.ConnectionString, ref sqlParameters);

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

		private CalendarEventBE ConvertDataRowToBE(DataRow dataRow)
		{
			CalendarEventBE calendarEventBE = new CalendarEventBE();

			calendarEventBE.CalendarEventQueueId = TryToParse<int?>(dataRow["CALENDAR_EVENT_QUEUE_ID"]);
			calendarEventBE.JsonObjectTxt = TryToParse<string>(dataRow["JSON_OBJECT_TXT"]);
			calendarEventBE.ActionDesc = TryToParse<string>(dataRow["ACTION_DESC"]);
			calendarEventBE.ProcessedInd = TryToParse<bool?>(dataRow["PROCESSED_IND"]);
			calendarEventBE.ProcessedDate = TryToParse<DateTime?>(dataRow["PROCESSED_DTTM"]);
			calendarEventBE.InProcessInd = TryToParse<bool?>(dataRow["IN_PROCESS_IND"]);
			calendarEventBE.InProcessDate = TryToParse<DateTime?>(dataRow["IN_PROCESS_DTTM"]);
			calendarEventBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			calendarEventBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			calendarEventBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			calendarEventBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			calendarEventBE.RetryCount = TryToParse<int?>(dataRow["RETRY_CNT"]);

			return calendarEventBE;

		}

		#endregion

	}

	#endregion

}