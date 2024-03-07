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

    #region BusinessObject - MeetingTypeRefBO

    public class MeetingTypeRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private MeetingTypeRefBE _meetingTypeRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(MeetingTypeRefBE meetingTypeRefBE)
		{
			int id;
			_meetingTypeRefBE = meetingTypeRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@MEETING_TYP_REF_ID", meetingTypeRefBE.MeetingTypRefId);
				sqlParameters[1] = new SqlParameter("@MEETING_TYP_DESC", meetingTypeRefBE.MeetingTypDesc);
				sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", meetingTypeRefBE.EnumMappingValNbr);
				sqlParameters[3] = new SqlParameter("@ACTIVE_IND", meetingTypeRefBE.ActiveInd);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",meetingTypeRefBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_meeting_type_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_meeting_type_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public MeetingTypeRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			MeetingTypeRefBE meetingTypeRefBE = new MeetingTypeRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_meeting_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				meetingTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return meetingTypeRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_meeting_type_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<MeetingTypeRefBE> GetList()
		{
			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<MeetingTypeRefBE> meetingTypeRefBEList = new List<MeetingTypeRefBE>();

			try
			{

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_meeting_type_ref_get_list", base.ConnectionString);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					meetingTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return meetingTypeRefBEList;

		}

		public int Update(MeetingTypeRefBE meetingTypeRefBE)
		{
			int rows;
			_meetingTypeRefBE = meetingTypeRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[7];

				sqlParameters[0] = new SqlParameter("@MEETING_TYP_REF_ID", meetingTypeRefBE.MeetingTypRefId);
				sqlParameters[1] = new SqlParameter("@MEETING_TYP_DESC", meetingTypeRefBE.MeetingTypDesc);
				sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", meetingTypeRefBE.EnumMappingValNbr);
				sqlParameters[3] = new SqlParameter("@ACTIVE_IND", meetingTypeRefBE.ActiveInd);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", meetingTypeRefBE.UpdatedDate);

				sqlParameters[5] = new SqlParameter("@WKR_ID_TXT",meetingTypeRefBE.UserId);

				sqlParameters[6] = new SqlParameter("@ReturnValue",0);
				sqlParameters[6].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_meeting_type_ref", base.ConnectionString, ref sqlParameters);

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

		private MeetingTypeRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			MeetingTypeRefBE meetingTypeRefBE = new MeetingTypeRefBE();

			meetingTypeRefBE.MeetingTypRefId = TryToParse<int?>(dataRow["MEETING_TYP_REF_ID"]);
			meetingTypeRefBE.MeetingTypDesc = TryToParse<string>(dataRow["MEETING_TYP_DESC"]);
			meetingTypeRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
			meetingTypeRefBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
			meetingTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			meetingTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			meetingTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			meetingTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return meetingTypeRefBE;

		}

		#endregion

	}

	#endregion

}