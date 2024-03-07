#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Base;
using AION.Engine.BusinessEntities;

#endregion

namespace AION.Engine.BusinessObjects
{

	#region BusinessObject - CatalogGroupRefBO

	public class CatalogGroupRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update,GetByExternalRef};

		private string _errorMsg;

		private CatalogGroupRefBE _catalogGroupRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(CatalogGroupRefBE catalogGroupRefBE)
		{
			int id;
			_catalogGroupRefBE = catalogGroupRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[3];

				sqlParameters[0] = new SqlParameter("@CATALOG_GRP_NM", catalogGroupRefBE.CatalogGroupName);

				sqlParameters[1] = new SqlParameter("@WKR_ID_TXT",catalogGroupRefBE.UserId);

				sqlParameters[2] = new SqlParameter("@ReturnValue",0);
				sqlParameters[2].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_catalog_group_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_catalog_group_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public CatalogGroupRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			CatalogGroupRefBE catalogGroupRefBE = new CatalogGroupRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_group_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				catalogGroupRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return catalogGroupRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_group_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<CatalogGroupRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<CatalogGroupRefBE> catalogGroupRefBEList = new List<CatalogGroupRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_group_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					catalogGroupRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return catalogGroupRefBEList;

		}

		public int Update(CatalogGroupRefBE catalogGroupRefBE)
		{
			int rows;
			_catalogGroupRefBE = catalogGroupRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[5];

				sqlParameters[0] = new SqlParameter("@CATALOG_GRP_REF_ID", catalogGroupRefBE.ID);
				sqlParameters[1] = new SqlParameter("@CATALOG_GRP_NM", catalogGroupRefBE.CatalogGroupName);
				sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", catalogGroupRefBE.UpdatedDate);

				sqlParameters[3] = new SqlParameter("@WKR_ID_TXT",catalogGroupRefBE.UserId);

				sqlParameters[4] = new SqlParameter("@ReturnValue",0);
				sqlParameters[4].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_catalog_group_ref", base.ConnectionString, ref sqlParameters);

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

                case ActionType.GetByExternalRef:
                    return (_errorMsg == String.Empty);

                default:
					break;
			}

			return true;

		}

		private CatalogGroupRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			CatalogGroupRefBE catalogGroupRefBE = new CatalogGroupRefBE();

			catalogGroupRefBE.ID = TryToParse<int?>(dataRow["CATALOG_GRP_REF_ID"]);
			catalogGroupRefBE.CatalogGroupName = TryToParse<string>(dataRow["CATALOG_GRP_NM"]);
			catalogGroupRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			catalogGroupRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			catalogGroupRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			catalogGroupRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return catalogGroupRefBE;

		}

       #endregion

    }

	#endregion

}