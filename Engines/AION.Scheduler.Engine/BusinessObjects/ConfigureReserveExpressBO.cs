#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Engine.BusinessEntities;
using AION.Base;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{

	#region BusinessObject - ConfigureReserveExpressBO

	public class ConfigureReserveExpressBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private ConfigureReserveExpressBE _configureReserveExpressBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(ConfigureReserveExpressBE configureReserveExpressBE)
		{
			int id;
			_configureReserveExpressBE = configureReserveExpressBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@RESERVE_EXPRESS_DAY_DESC", configureReserveExpressBE.ReserveExpressDay);
				sqlParameters[1] = new SqlParameter("@START_DTTM", configureReserveExpressBE.StartDate);
				sqlParameters[2] = new SqlParameter("@END_DTTM", configureReserveExpressBE.EndDate);
				sqlParameters[3] = new SqlParameter("@ACTIVE_IND", configureReserveExpressBE.ActiveInd);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",configureReserveExpressBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_configure_reserve_express", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_configure_reserve_express", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public ConfigureReserveExpressBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			ConfigureReserveExpressBE configureReserveExpressBE = new ConfigureReserveExpressBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_configure_reserve_express_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				configureReserveExpressBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return configureReserveExpressBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_configure_reserve_express_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<ConfigureReserveExpressBE> GetList()
		{
			//_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ConfigureReserveExpressBE> configureReserveExpressBEList = new List<ConfigureReserveExpressBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				//sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_configure_reserve_express_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					configureReserveExpressBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return configureReserveExpressBEList;

		}

		public int Update(ConfigureReserveExpressBE configureReserveExpressBE)
		{
			int rows;
			_configureReserveExpressBE = configureReserveExpressBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@CONFIGURE_RESERVE_EXPRESS_ID", configureReserveExpressBE.ConfigureReserveExpressId);
			//	sqlParameters[1] = new SqlParameter("@RESERVE_EXPRESS_DAY_DESC", configureReserveExpressBE.ReserveExpressDay);
				sqlParameters[1] = new SqlParameter("@START_DTTM", configureReserveExpressBE.StartDate);
				sqlParameters[2] = new SqlParameter("@END_DTTM", configureReserveExpressBE.EndDate);
				sqlParameters[3] = new SqlParameter("@ACTIVE_IND", configureReserveExpressBE.ActiveInd);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", configureReserveExpressBE.UpdatedDate);

				//sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",configureReserveExpressBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_configure_reserve_express", base.ConnectionString, ref sqlParameters);

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

		private ConfigureReserveExpressBE ConvertDataRowToBE(DataRow dataRow)
		{
			ConfigureReserveExpressBE configureReserveExpressBE = new ConfigureReserveExpressBE();

			configureReserveExpressBE.ConfigureReserveExpressId = TryToParse<int?>(dataRow["CONFIGURE_RESERVE_EXPRESS_ID"]);
			configureReserveExpressBE.ReserveExpressDay = TryToParse<string>(dataRow["RESERVE_EXPRESS_DAY_DESC"]);
			configureReserveExpressBE.StartDate = TryToParse<DateTime?>(dataRow["START_DTTM"]);
			configureReserveExpressBE.EndDate = TryToParse<DateTime?>(dataRow["END_DTTM"]);
			configureReserveExpressBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
			configureReserveExpressBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			configureReserveExpressBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			configureReserveExpressBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			configureReserveExpressBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return configureReserveExpressBE;

		}

		#endregion

	}

	#endregion

}