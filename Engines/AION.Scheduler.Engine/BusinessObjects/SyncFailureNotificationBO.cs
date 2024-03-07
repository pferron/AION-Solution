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

    #region BusinessObject - SyncFailureNotificationBO

    public class SyncFailureNotificationBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private SyncFailureNotificationBE _syncFailureNotificationBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(SyncFailureNotificationBE syncFailureNotificationBE)
		{
			int id;
			_syncFailureNotificationBE = syncFailureNotificationBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[2];

				sqlParameters[0] = new SqlParameter("@LAST_FAILURE_NOTIFICATION_DT", syncFailureNotificationBE.LastFailureNotificationDt);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_sync_failure_notification", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return id;
		}

		public List<SyncFailureNotificationBE> GetList()
		{
			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<SyncFailureNotificationBE> syncFailureNotificationBEList = new List<SyncFailureNotificationBE>();

			try
			{
				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_sync_failure_notification_get_list", base.ConnectionString);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					syncFailureNotificationBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return syncFailureNotificationBEList;

		}

		public int Update(SyncFailureNotificationBE syncFailureNotificationBE)
		{
			int rows;
			_syncFailureNotificationBE = syncFailureNotificationBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[3];

				sqlParameters[0] = new SqlParameter("@LAST_FAILURE_NOTIFICATION_DT", syncFailureNotificationBE.LastFailureNotificationDt);

				sqlParameters[1] = new SqlParameter("@WKR_ID_TXT",syncFailureNotificationBE.UserId);

				sqlParameters[2] = new SqlParameter("@ReturnValue",0);
				sqlParameters[2].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_sync_failure_notification", base.ConnectionString, ref sqlParameters);

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

		private SyncFailureNotificationBE ConvertDataRowToBE(DataRow dataRow)
		{
			SyncFailureNotificationBE syncFailureNotificationBE = new SyncFailureNotificationBE();

			syncFailureNotificationBE.LastFailureNotificationDt = TryToParse<DateTime?>(dataRow["LAST_FAILURE_NOTIFICATION_DT"]);

			return syncFailureNotificationBE;

		}

		#endregion

	}

	#endregion

}