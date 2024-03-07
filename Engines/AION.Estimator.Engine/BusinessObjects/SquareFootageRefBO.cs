#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

	#region BusinessObject - SquareFootageRefBO

	public class SquareFootageRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private SquareFootageRefBE _squareFootageRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(SquareFootageRefBE squareFootageRefBE)
		{
			int id;
			_squareFootageRefBE = squareFootageRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@SQUARE_FOOTAGE_REF_ID", squareFootageRefBE.SquareFootageRefID);
				sqlParameters[1] = new SqlParameter("@SQUARE_FOOTAGE_DESC", squareFootageRefBE.SquareFootageDesc);
				sqlParameters[2] = new SqlParameter("@SQUARE_FOOTAGE_VAL_TXT", squareFootageRefBE.SquareFootageValue);
				sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", squareFootageRefBE.EnumMappingVal);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",squareFootageRefBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_square_footage_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_square_footage_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public SquareFootageRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			SquareFootageRefBE squareFootageRefBE = new SquareFootageRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_square_footage_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				squareFootageRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return squareFootageRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_square_footage_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<SquareFootageRefBE> GetList()
		{			

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<SquareFootageRefBE> squareFootageRefBEList = new List<SquareFootageRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];
				

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_square_footage_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					squareFootageRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return squareFootageRefBEList;

		}

		public int Update(SquareFootageRefBE squareFootageRefBE)
		{
			int rows;
			_squareFootageRefBE = squareFootageRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[7];

				sqlParameters[0] = new SqlParameter("@SQUARE_FOOTAGE_REF_ID", squareFootageRefBE.SquareFootageRefID);
				sqlParameters[1] = new SqlParameter("@SQUARE_FOOTAGE_DESC", squareFootageRefBE.SquareFootageDesc);
				sqlParameters[2] = new SqlParameter("@SQUARE_FOOTAGE_VAL_TXT", squareFootageRefBE.SquareFootageValue);
				sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", squareFootageRefBE.EnumMappingVal);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", squareFootageRefBE.UpdatedDate);

				sqlParameters[5] = new SqlParameter("@WKR_ID_TXT",squareFootageRefBE.UserId);

				sqlParameters[6] = new SqlParameter("@ReturnValue",0);
				sqlParameters[6].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_square_footage_ref", base.ConnectionString, ref sqlParameters);

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

		private SquareFootageRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			SquareFootageRefBE squareFootageRefBE = new SquareFootageRefBE();

			squareFootageRefBE.SquareFootageRefID = TryToParse<int?>(dataRow["SQUARE_FOOTAGE_REF_ID"]);
			squareFootageRefBE.SquareFootageDesc = TryToParse<string>(dataRow["SQUARE_FOOTAGE_DESC"]);
			squareFootageRefBE.SquareFootageValue = TryToParse<string>(dataRow["SQUARE_FOOTAGE_VAL_TXT"]);
			squareFootageRefBE.EnumMappingVal = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
			squareFootageRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			squareFootageRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			squareFootageRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			squareFootageRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return squareFootageRefBE;

		}

		#endregion

	}

	#endregion

}