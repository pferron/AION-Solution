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

	#region BusinessObject - AttachmentLinkBO

	public class AttachmentLinkBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private AttachmentLinkBE _attachmentLinkBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(AttachmentLinkBE attachmentLinkBE)
		{
			int id;
			_attachmentLinkBE = attachmentLinkBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[9];

				sqlParameters[0] = new SqlParameter("@LINK_TXT", attachmentLinkBE.LinkText);
				sqlParameters[1] = new SqlParameter("@NOTES_ID", attachmentLinkBE.NotesId);
				sqlParameters[2] = new SqlParameter("@TAG_CREATED_ID_TXT", attachmentLinkBE.TagCreatedIdTxt);
				sqlParameters[3] = new SqlParameter("@TAG_CREATED_DTTM", attachmentLinkBE.TagCreatedDt);
				sqlParameters[4] = new SqlParameter("@TAG_UPDATED_DTTM", attachmentLinkBE.TagUpdatedDt);
				sqlParameters[5] = new SqlParameter("@TAG_UPDATED_ID_TXT", attachmentLinkBE.TagUpdatedIdTxt);
				sqlParameters[6] = new SqlParameter("@ATTACHMENT_TYP_CD", attachmentLinkBE.AttachmentTypeCd);

				sqlParameters[7] = new SqlParameter("@WKR_ID_TXT",attachmentLinkBE.UserId);

				sqlParameters[8] = new SqlParameter("@ReturnValue",0);
				sqlParameters[8].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_attachment_link", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_attachment_link", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public AttachmentLinkBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			AttachmentLinkBE attachmentLinkBE = new AttachmentLinkBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_attachment_link_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				attachmentLinkBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return attachmentLinkBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_attachment_link_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<AttachmentLinkBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<AttachmentLinkBE> attachmentLinkBEList = new List<AttachmentLinkBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_attachment_link_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					attachmentLinkBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return attachmentLinkBEList;

		}

		public int Update(AttachmentLinkBE attachmentLinkBE)
		{
			int rows;
			_attachmentLinkBE = attachmentLinkBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[11];

				sqlParameters[0] = new SqlParameter("@ATTACHMENT_LINK_ID", attachmentLinkBE.AttachmentLinkId);
				sqlParameters[1] = new SqlParameter("@LINK_TXT", attachmentLinkBE.LinkText);
				sqlParameters[2] = new SqlParameter("@NOTES_ID", attachmentLinkBE.NotesId);
				sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", attachmentLinkBE.UpdatedDate);
				sqlParameters[4] = new SqlParameter("@TAG_CREATED_ID_TXT", attachmentLinkBE.TagCreatedIdTxt);
				sqlParameters[5] = new SqlParameter("@TAG_CREATED_DTTM", attachmentLinkBE.TagCreatedDt);
				sqlParameters[6] = new SqlParameter("@TAG_UPDATED_DTTM", attachmentLinkBE.TagUpdatedDt);
				sqlParameters[7] = new SqlParameter("@TAG_UPDATED_ID_TXT", attachmentLinkBE.TagUpdatedIdTxt);
				sqlParameters[8] = new SqlParameter("@ATTACHMENT_TYP_CD", attachmentLinkBE.AttachmentTypeCd);

				sqlParameters[9] = new SqlParameter("@WKR_ID_TXT",attachmentLinkBE.UserId);

				sqlParameters[10] = new SqlParameter("@ReturnValue",0);
				sqlParameters[10].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_attachment_link", base.ConnectionString, ref sqlParameters);

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

		private AttachmentLinkBE ConvertDataRowToBE(DataRow dataRow)
		{
			AttachmentLinkBE attachmentLinkBE = new AttachmentLinkBE();

			attachmentLinkBE.AttachmentLinkId = TryToParse<int?>(dataRow["ATTACHMENT_LINK_ID"]);
			attachmentLinkBE.LinkText = TryToParse<string>(dataRow["LINK_TXT"]);
			attachmentLinkBE.NotesId = TryToParse<int?>(dataRow["NOTES_ID"]);
			attachmentLinkBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			attachmentLinkBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			attachmentLinkBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			attachmentLinkBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			attachmentLinkBE.TagCreatedIdTxt = TryToParse<string>(dataRow["TAG_CREATED_ID_TXT"]);
			attachmentLinkBE.TagCreatedDt = TryToParse<DateTime?>(dataRow["TAG_CREATED_DTTM"]);
			attachmentLinkBE.TagUpdatedDt = TryToParse<DateTime?>(dataRow["TAG_UPDATED_DTTM"]);
			attachmentLinkBE.TagUpdatedIdTxt = TryToParse<string>(dataRow["TAG_UPDATED_ID_TXT"]);
			attachmentLinkBE.AttachmentTypeCd = TryToParse<string>(dataRow["ATTACHMENT_TYP_CD"]);

			return attachmentLinkBE;

		}

		#endregion

	}

	#endregion

}