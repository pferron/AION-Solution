#region Using

using AION.Base;
using AION.Estimator.Engine.BusinessObjects;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessObject - AutoEstimationRefBO

	public class AutoEstimationRefBO : BaseBO, IDataContextAutoEstimationRefBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private AutoEstimationRefBE _autoEstimationRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(AutoEstimationRefBE autoEstimationRefBE)
		{
			int id;
			_autoEstimationRefBE = autoEstimationRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[8];

				sqlParameters[0] = new SqlParameter("@ACTIVE_IND", autoEstimationRefBE.ActiveInd);
				sqlParameters[1] = new SqlParameter("@ACTIVE_DT", autoEstimationRefBE.ActiveDate);
				sqlParameters[2] = new SqlParameter("@MONTH_NBR", autoEstimationRefBE.Months);
				sqlParameters[3] = new SqlParameter("@WEIGHT_SQFT_NBR", autoEstimationRefBE.WeightSqftNbr);
				sqlParameters[4] = new SqlParameter("@WEIGHT_COC_NBR", autoEstimationRefBE.WeightCocNbr);
				sqlParameters[5] = new SqlParameter("@WEIGHT_SHEETS_NBR", autoEstimationRefBE.WeightSheetsNbr);

				sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",autoEstimationRefBE.UserId);

				sqlParameters[7] = new SqlParameter("@ReturnValue",0);
				sqlParameters[7].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_auto_estimation_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_auto_estimation_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public AutoEstimationRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			AutoEstimationRefBE autoEstimationRefBE = new AutoEstimationRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_auto_estimation_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				autoEstimationRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return autoEstimationRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_auto_estimation_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<AutoEstimationRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<AutoEstimationRefBE> autoEstimationRefBEList = new List<AutoEstimationRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_auto_estimation_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					autoEstimationRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return autoEstimationRefBEList;

		}

        public AutoEstimationRefBE GetActive()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;

            AutoEstimationRefBE autoEstimationRefBE = new AutoEstimationRefBE();

            try
            {
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_auto_estimation_ref_get_active", base.ConnectionString);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    autoEstimationRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return autoEstimationRefBE;

        }

        public int Update(AutoEstimationRefBE autoEstimationRefBE)
		{
			int rows;
			_autoEstimationRefBE = autoEstimationRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[10];

				sqlParameters[0] = new SqlParameter("@ACTIVE_IND", autoEstimationRefBE.ActiveInd);
				sqlParameters[1] = new SqlParameter("@ACTIVE_DT", autoEstimationRefBE.ActiveDate);
				sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", autoEstimationRefBE.UpdatedDate);
				sqlParameters[3] = new SqlParameter("@AUTO_ESTIMATION_REF_ID", autoEstimationRefBE.AutoEstimationRefId);
				sqlParameters[4] = new SqlParameter("@MONTH_NBR", autoEstimationRefBE.Months);
				sqlParameters[5] = new SqlParameter("@WEIGHT_SQFT_NBR", autoEstimationRefBE.WeightSqftNbr);
				sqlParameters[6] = new SqlParameter("@WEIGHT_COC_NBR", autoEstimationRefBE.WeightCocNbr);
				sqlParameters[7] = new SqlParameter("@WEIGHT_SHEETS_NBR", autoEstimationRefBE.WeightSheetsNbr);

				sqlParameters[8] = new SqlParameter("@WKR_ID_TXT",autoEstimationRefBE.UserId);

				sqlParameters[9] = new SqlParameter("@ReturnValue",0);
				sqlParameters[9].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_auto_estimation_ref", base.ConnectionString, ref sqlParameters);

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

		private AutoEstimationRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			AutoEstimationRefBE autoEstimationRefBE = new AutoEstimationRefBE();

			autoEstimationRefBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
			autoEstimationRefBE.ActiveDate = TryToParse<DateTime?>(dataRow["ACTIVE_DT"]);
			autoEstimationRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			autoEstimationRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			autoEstimationRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			autoEstimationRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			autoEstimationRefBE.AutoEstimationRefId = TryToParse<int?>(dataRow["AUTO_ESTIMATION_REF_ID"]);
			autoEstimationRefBE.Months = TryToParse<int?>(dataRow["MONTH_NBR"]);
			autoEstimationRefBE.WeightSqftNbr = TryToParse<decimal?>(dataRow["WEIGHT_SQFT_NBR"]);
			autoEstimationRefBE.WeightCocNbr = TryToParse<decimal?>(dataRow["WEIGHT_COC_NBR"]);
			autoEstimationRefBE.WeightSheetsNbr = TryToParse<decimal?>(dataRow["WEIGHT_SHEETS_NBR"]);

			return autoEstimationRefBE;

		}

		#endregion

	}

	#endregion

}