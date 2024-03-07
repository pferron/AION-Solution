#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Engine.BusinessEntities;
using AION.Base;

#endregion

namespace AION.Engine.BusinessObjects
{

	#region BusinessObject - BO

	public class FifoScheduleBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		#endregion

		#region Public Methods

		public int GetLastAssignedCityZoningReviewer()
        {
			DataSet dataSet;
			int id = 0;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[0];

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_city_zoning_reviewer_assigned_latest_v2", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					id = TryToParse<int>(dataRow["ASSIGNED_PLAN_REVIEWER_ID"]);
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return id;
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

		#endregion

	}

	#endregion

}