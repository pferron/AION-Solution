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

    #region BusinessObject - AppointmentCancellationRefBO

    public class AppointmentCancellationRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private AppointmentCancellationRefBE _appointmentCancellationRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(AppointmentCancellationRefBE appointmentCancellationRefBE)
		{
			int id;
			_appointmentCancellationRefBE = appointmentCancellationRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[5];

				sqlParameters[0] = new SqlParameter("@CANCELLATION_DESC", appointmentCancellationRefBE.CancellationDesc);
				sqlParameters[1] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", appointmentCancellationRefBE.EnumMappingValNbr);
				sqlParameters[2] = new SqlParameter("@ACTIVE_IND", appointmentCancellationRefBE.ActiveInd);

				sqlParameters[3] = new SqlParameter("@WKR_ID_TXT",appointmentCancellationRefBE.UserId);

				sqlParameters[4] = new SqlParameter("@ReturnValue",0);
				sqlParameters[4].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_appointment_cancellation_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_appointment_cancellation_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public AppointmentCancellationRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			AppointmentCancellationRefBE appointmentCancellationRefBE = new AppointmentCancellationRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_cancellation_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				appointmentCancellationRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return appointmentCancellationRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_cancellation_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<AppointmentCancellationRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<AppointmentCancellationRefBE> appointmentCancellationRefBEList = new List<AppointmentCancellationRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_cancellation_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					appointmentCancellationRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return appointmentCancellationRefBEList;

		}

		public List<AppointmentCancellationRefBE> GetList()
		{

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<AppointmentCancellationRefBE> appointmentCancellationBEList = new List<AppointmentCancellationRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];


				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_cancellation_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					appointmentCancellationBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return appointmentCancellationBEList;

		}

		public int Update(AppointmentCancellationRefBE appointmentCancellationRefBE)
		{
			int rows;
			_appointmentCancellationRefBE = appointmentCancellationRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[7];

				sqlParameters[0] = new SqlParameter("@APPT_CANCELLATION_REF_ID", appointmentCancellationRefBE.ApptCancellationRefId);
				sqlParameters[1] = new SqlParameter("@CANCELLATION_DESC", appointmentCancellationRefBE.CancellationDesc);
				sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", appointmentCancellationRefBE.EnumMappingValNbr);
				sqlParameters[3] = new SqlParameter("@ACTIVE_IND", appointmentCancellationRefBE.ActiveInd);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", appointmentCancellationRefBE.UpdatedDate);

				sqlParameters[5] = new SqlParameter("@WKR_ID_TXT",appointmentCancellationRefBE.UserId);

				sqlParameters[6] = new SqlParameter("@ReturnValue",0);
				sqlParameters[6].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_appointment_cancellation_ref", base.ConnectionString, ref sqlParameters);

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

		private AppointmentCancellationRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			AppointmentCancellationRefBE appointmentCancellationRefBE = new AppointmentCancellationRefBE();

			appointmentCancellationRefBE.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]);
			appointmentCancellationRefBE.CancellationDesc = TryToParse<string>(dataRow["CANCELLATION_DESC"]);
			appointmentCancellationRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
			appointmentCancellationRefBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
			appointmentCancellationRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			appointmentCancellationRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			appointmentCancellationRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			appointmentCancellationRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return appointmentCancellationRefBE;

		}

		#endregion

	}

	#endregion

}