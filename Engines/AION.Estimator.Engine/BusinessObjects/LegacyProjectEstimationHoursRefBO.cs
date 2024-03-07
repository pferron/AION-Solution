#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Estimator.Engine.BusinessEntities;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

	#region BusinessObject - LegacyProjectEstimationHoursRefBO

	public class LegacyProjectEstimationHoursRefBO : BaseBO, IDataContextLegacyProjectDataBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private LegacyProjectEstimationHoursRefBE _legacyProjectEstimationHoursRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(LegacyProjectEstimationHoursRefBE legacyProjectEstimationHoursRefBE)
		{
			int id;
			_legacyProjectEstimationHoursRefBE = legacyProjectEstimationHoursRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[13];

				sqlParameters[0] = new SqlParameter("@LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID", legacyProjectEstimationHoursRefBE.LegacyProjectEstimationHoursRefId);
				sqlParameters[1] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", legacyProjectEstimationHoursRefBE.OccupancyTypRefId);
				sqlParameters[2] = new SqlParameter("@CONSTR_TYP_TXT", legacyProjectEstimationHoursRefBE.ConstrTypTxt);
				sqlParameters[3] = new SqlParameter("@TOTAL_PROJECTS_CNT", legacyProjectEstimationHoursRefBE.TotalProjectsCnt);
				sqlParameters[4] = new SqlParameter("@BUILD_HOURS_NBR", legacyProjectEstimationHoursRefBE.BuildHoursNbr);
				sqlParameters[5] = new SqlParameter("@ELCTR_HOURS_NBR", legacyProjectEstimationHoursRefBE.ElectHoursNbr);
				sqlParameters[6] = new SqlParameter("@MECH_HOURS_NBR", legacyProjectEstimationHoursRefBE.MechHoursNbr);
				sqlParameters[7] = new SqlParameter("@PLUMB_HOURS_NBR", legacyProjectEstimationHoursRefBE.PlumbHoursNbr);
				sqlParameters[8] = new SqlParameter("@TOTAL_SQUARE_FOOTAGE_CNT", legacyProjectEstimationHoursRefBE.TotalSquareFootageCnt);
				sqlParameters[9] = new SqlParameter("@TOTAL_CONSTR_COST_AMT", legacyProjectEstimationHoursRefBE.TotalConstrCostAmt);
				sqlParameters[10] = new SqlParameter("@TOTAL_SHEETS_CNT", legacyProjectEstimationHoursRefBE.TotalSheetsCnt);

				sqlParameters[11] = new SqlParameter("@WKR_ID_TXT",legacyProjectEstimationHoursRefBE.UserId);

				sqlParameters[12] = new SqlParameter("@ReturnValue",0);
				sqlParameters[12].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_legacy_project_estimation_hours_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_legacy_project_estimation_hours_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public LegacyProjectEstimationHoursRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			LegacyProjectEstimationHoursRefBE legacyProjectEstimationHoursRefBE = new LegacyProjectEstimationHoursRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_legacy_project_estimation_hours_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				legacyProjectEstimationHoursRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return legacyProjectEstimationHoursRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_legacy_project_estimation_hours_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<LegacyProjectEstimationHoursRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<LegacyProjectEstimationHoursRefBE> legacyProjectEstimationHoursRefBEList = new List<LegacyProjectEstimationHoursRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_legacy_project_estimation_hours_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					legacyProjectEstimationHoursRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return legacyProjectEstimationHoursRefBEList;

		}

		public int Update(LegacyProjectEstimationHoursRefBE legacyProjectEstimationHoursRefBE)
		{
			int rows;
			_legacyProjectEstimationHoursRefBE = legacyProjectEstimationHoursRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[14];

				sqlParameters[0] = new SqlParameter("@LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID", legacyProjectEstimationHoursRefBE.LegacyProjectEstimationHoursRefId);
				sqlParameters[1] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", legacyProjectEstimationHoursRefBE.OccupancyTypRefId);
				sqlParameters[2] = new SqlParameter("@CONSTR_TYP_TXT", legacyProjectEstimationHoursRefBE.ConstrTypTxt);
				sqlParameters[3] = new SqlParameter("@TOTAL_PROJECTS_CNT", legacyProjectEstimationHoursRefBE.TotalProjectsCnt);
				sqlParameters[4] = new SqlParameter("@BUILD_HOURS_NBR", legacyProjectEstimationHoursRefBE.BuildHoursNbr);
				sqlParameters[5] = new SqlParameter("@ELCTR_HOURS_NBR", legacyProjectEstimationHoursRefBE.ElectHoursNbr);
				sqlParameters[6] = new SqlParameter("@MECH_HOURS_NBR", legacyProjectEstimationHoursRefBE.MechHoursNbr);
				sqlParameters[7] = new SqlParameter("@PLUMB_HOURS_NBR", legacyProjectEstimationHoursRefBE.PlumbHoursNbr);
				sqlParameters[8] = new SqlParameter("@UPDATED_DTTM", legacyProjectEstimationHoursRefBE.UpdatedDate);
				sqlParameters[9] = new SqlParameter("@TOTAL_SQUARE_FOOTAGE_CNT", legacyProjectEstimationHoursRefBE.TotalSquareFootageCnt);
				sqlParameters[10] = new SqlParameter("@TOTAL_CONSTR_COST_AMT", legacyProjectEstimationHoursRefBE.TotalConstrCostAmt);
				sqlParameters[11] = new SqlParameter("@TOTAL_SHEETS_CNT", legacyProjectEstimationHoursRefBE.TotalSheetsCnt);

				sqlParameters[12] = new SqlParameter("@WKR_ID_TXT",legacyProjectEstimationHoursRefBE.UserId);

				sqlParameters[13] = new SqlParameter("@ReturnValue",0);
				sqlParameters[13].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_legacy_project_estimation_hours_ref", base.ConnectionString, ref sqlParameters);

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

		private LegacyProjectEstimationHoursRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			LegacyProjectEstimationHoursRefBE legacyProjectEstimationHoursRefBE = new LegacyProjectEstimationHoursRefBE();

			legacyProjectEstimationHoursRefBE.LegacyProjectEstimationHoursRefId = TryToParse<int?>(dataRow["LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID"]);
			legacyProjectEstimationHoursRefBE.OccupancyTypRefId = TryToParse<int?>(dataRow["OCCUPANCY_TYP_REF_ID"]);
			legacyProjectEstimationHoursRefBE.ConstrTypTxt = TryToParse<string>(dataRow["CONSTR_TYP_TXT"]);
			legacyProjectEstimationHoursRefBE.TotalProjectsCnt = TryToParse<decimal?>(dataRow["TOTAL_PROJECTS_CNT"]);
			legacyProjectEstimationHoursRefBE.BuildHoursNbr = TryToParse<decimal?>(dataRow["BUILD_HOURS_NBR"]);
			legacyProjectEstimationHoursRefBE.ElectHoursNbr = TryToParse<decimal?>(dataRow["ELCTR_HOURS_NBR"]);
			legacyProjectEstimationHoursRefBE.MechHoursNbr = TryToParse<decimal?>(dataRow["MECH_HOURS_NBR"]);
			legacyProjectEstimationHoursRefBE.PlumbHoursNbr = TryToParse<decimal?>(dataRow["PLUMB_HOURS_NBR"]);
			legacyProjectEstimationHoursRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			legacyProjectEstimationHoursRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			legacyProjectEstimationHoursRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			legacyProjectEstimationHoursRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			legacyProjectEstimationHoursRefBE.TotalSquareFootageCnt = TryToParse<decimal?>(dataRow["TOTAL_SQUARE_FOOTAGE_CNT"]);
			legacyProjectEstimationHoursRefBE.TotalConstrCostAmt = TryToParse<decimal?>(dataRow["TOTAL_CONSTR_COST_AMT"]);
			legacyProjectEstimationHoursRefBE.TotalSheetsCnt = TryToParse<decimal?>(dataRow["TOTAL_SHEETS_CNT"]);

			return legacyProjectEstimationHoursRefBE;

		}

		public List<LegacyProjectEstimationHoursRefBE> GetList()
		{
			throw new NotImplementedException();
		}

		#endregion

	}

	#endregion

}