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

	#region BusinessObject - CatalogRefBO

	public class CatalogRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private CatalogRefBE _catalogRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(CatalogRefBE catalogRefBE)
		{
			int id;
			_catalogRefBE = catalogRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@VALUE", catalogRefBE.Value);
				sqlParameters[1] = new SqlParameter("@CATALOG_KEY_TXT", catalogRefBE.Key);
				sqlParameters[2] = new SqlParameter("@CATALOG_SUBKEY_TXT", catalogRefBE.SubKey);
				sqlParameters[3] = new SqlParameter("@CATALOG_GRP_REF_ID", catalogRefBE.CatalogGroupRefID);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",catalogRefBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_catalog_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_catalog_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public CatalogRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			CatalogRefBE catalogRefBE = new CatalogRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				catalogRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return catalogRefBE;
		}

        public List<CatalogRefBE> GetByExternalRef(string externalRefID)
        {

            List<CatalogRefBE> ret = new List<CatalogRefBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@externalRefID", externalRefID);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_ref_get_by_externalRefID", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (System.Data.DataRow item in dataSet.Tables[0].Rows)
                {
                    ret.Add(this.ConvertDataRowToBE(item));
                }
            }
            return ret;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<CatalogRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<CatalogRefBE> catalogRefBEList = new List<CatalogRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_catalog_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					catalogRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return catalogRefBEList;

		}

		public int Update(CatalogRefBE catalogRefBE)
		{
			int rows;
			_catalogRefBE = catalogRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[8];

				sqlParameters[0] = new SqlParameter("@CATALOG_REF_ID", catalogRefBE.ID);
				sqlParameters[1] = new SqlParameter("@CATALOG_VAL_TXT", catalogRefBE.Value);
				sqlParameters[2] = new SqlParameter("@CATALOG_KEY_TXT", catalogRefBE.Key);
				sqlParameters[3] = new SqlParameter("@CATALOG_SUBKEY_TXT", catalogRefBE.SubKey);
				sqlParameters[4] = new SqlParameter("@CATALOG_GRP_REF_ID", catalogRefBE.CatalogGroupRefID);
				sqlParameters[5] = new SqlParameter("@UPDATED_DTTM", catalogRefBE.UpdatedDate);

				sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",catalogRefBE.UserId);

				sqlParameters[7] = new SqlParameter("@ReturnValue",0);
				sqlParameters[7].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_catalog_ref", base.ConnectionString, ref sqlParameters);

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

		private CatalogRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			CatalogRefBE catalogRefBE = new CatalogRefBE();

			catalogRefBE.ID = TryToParse<int?>(dataRow["CATALOG_REF_ID"]);
			catalogRefBE.Value = TryToParse<string>(dataRow["CATALOG_VAL_TXT"]);
			catalogRefBE.Key = TryToParse<string>(dataRow["CATALOG_KEY_TXT"]);
			catalogRefBE.SubKey = TryToParse<string>(dataRow["CATALOG_SUBKEY_TXT"]);
			catalogRefBE.CatalogGroupRefID = TryToParse<string>(dataRow["CATALOG_GRP_REF_ID"]);
			catalogRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			catalogRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			catalogRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			catalogRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return catalogRefBE;

		}

		#endregion

	}

	#endregion

}