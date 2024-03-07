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

	#region BusinessObject - NotesTypeRefBO

	public class NotesTypeRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private NotesTypeRefBE _notesTypeRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(NotesTypeRefBE notesTypeRefBE)
		{
			int id;
			_notesTypeRefBE = notesTypeRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@NOTES_TYP_REF_NM", notesTypeRefBE.NotesTypeRefName);
				sqlParameters[1] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", notesTypeRefBE.ExternalSystemRefId);
				sqlParameters[2] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", notesTypeRefBE.SrcSystemValTxt);
				sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", notesTypeRefBE.EnumMapping);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",notesTypeRefBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_notes_type_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_notes_type_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public NotesTypeRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			NotesTypeRefBE notesTypeRefBE = new NotesTypeRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notes_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				notesTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return notesTypeRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notes_type_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<NotesTypeRefBE> GetAllList()
		{
			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<NotesTypeRefBE> notesTypeRefBEList = new List<NotesTypeRefBE>();

			try
			{
				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notes_type_ref_get_list", base.ConnectionString);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					notesTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return notesTypeRefBEList;

		}

		public int Update(NotesTypeRefBE notesTypeRefBE)
		{
			int rows;
			_notesTypeRefBE = notesTypeRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[8];

				sqlParameters[0] = new SqlParameter("@NOTES_TYP_REF_ID", notesTypeRefBE.NotesTypeRefId);
				sqlParameters[1] = new SqlParameter("@NOTES_TYP_REF_NM", notesTypeRefBE.NotesTypeRefName);
				sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", notesTypeRefBE.UpdatedDate);
				sqlParameters[3] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", notesTypeRefBE.ExternalSystemRefId);
				sqlParameters[4] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", notesTypeRefBE.SrcSystemValTxt);
				sqlParameters[5] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", notesTypeRefBE.EnumMapping);

				sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",notesTypeRefBE.UserId);

				sqlParameters[7] = new SqlParameter("@ReturnValue",0);
				sqlParameters[7].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_notes_type_ref", base.ConnectionString, ref sqlParameters);

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

		private NotesTypeRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			NotesTypeRefBE notesTypeRefBE = new NotesTypeRefBE();

			notesTypeRefBE.NotesTypeRefId = TryToParse<int?>(dataRow["NOTES_TYP_REF_ID"]);
			notesTypeRefBE.NotesTypeRefName = TryToParse<string>(dataRow["NOTES_TYP_REF_NM"]);
			notesTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			notesTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			notesTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			notesTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			notesTypeRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
			notesTypeRefBE.SrcSystemValTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
			notesTypeRefBE.EnumMapping = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);

			return notesTypeRefBE;

		}

		#endregion

	}

	#endregion

}