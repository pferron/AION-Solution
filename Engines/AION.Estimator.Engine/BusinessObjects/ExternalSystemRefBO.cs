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

	#region BusinessObject - ExternalSystemRefBO

	public class ExternalSystemRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetBySystemName, GetDataSet, GetList, GetAllList, Update};

		private string _errorMsg;

		private ExternalSystemRefBE _externalSystemRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(ExternalSystemRefBE externalSystemRefBE)
		{
			int id;
			_externalSystemRefBE = externalSystemRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@EXTERNAL_SYSTEM_NM", externalSystemRefBE.ExternalSystemName);
				sqlParameters[1] = new SqlParameter("@EXTERNAL_SYSTEM_DESC", externalSystemRefBE.ExternalSystemDesc);
				sqlParameters[2] = new SqlParameter("@ADDL_INFORMATION_TXT", externalSystemRefBE.AddlInformationText);
				sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", externalSystemRefBE.EnumMappingValNbr);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",externalSystemRefBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_external_system_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_external_system_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public ExternalSystemRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			ExternalSystemRefBE externalSystemRefBE = new ExternalSystemRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_external_system_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				externalSystemRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return externalSystemRefBE;
		}

        public ExternalSystemRefBE GetBySystemName(string systemName)
        {


            if (!this.Validate(ActionType.GetBySystemName))
                throw (new Exception(_errorMsg));

            ExternalSystemRefBE externalSystemRefBE = new ExternalSystemRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@systemName", systemName);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_external_system_ref_get_by_system_name", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                externalSystemRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return externalSystemRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_external_system_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<ExternalSystemRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<ExternalSystemRefBE> externalSystemRefBEList = new List<ExternalSystemRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_external_system_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					externalSystemRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return externalSystemRefBEList;

		}

        public List<ExternalSystemRefBE> GetAllList()
        {
            if (!this.Validate(ActionType.GetAllList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ExternalSystemRefBE> externalSystemRefBEList = new List<ExternalSystemRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_external_system_ref_get_Alllist", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    externalSystemRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return externalSystemRefBEList;

        }

        public int Update(ExternalSystemRefBE externalSystemRefBE)
		{
			int rows;
			_externalSystemRefBE = externalSystemRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[8];

				sqlParameters[0] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", externalSystemRefBE.ExternalSystemRefId);
				sqlParameters[1] = new SqlParameter("@EXTERNAL_SYSTEM_NM", externalSystemRefBE.ExternalSystemName);
				sqlParameters[2] = new SqlParameter("@EXTERNAL_SYSTEM_DESC", externalSystemRefBE.ExternalSystemDesc);
				sqlParameters[3] = new SqlParameter("@ADDL_INFORMATION_TXT", externalSystemRefBE.AddlInformationText);
				sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", externalSystemRefBE.UpdatedDate);
				sqlParameters[5] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", externalSystemRefBE.EnumMappingValNbr);

				sqlParameters[6] = new SqlParameter("@WKR_ID_TXT",externalSystemRefBE.UserId);

				sqlParameters[7] = new SqlParameter("@ReturnValue",0);
				sqlParameters[7].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_external_system_ref", base.ConnectionString, ref sqlParameters);

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
                    return (_errorMsg == String.Empty);

                case ActionType.GetAllList:
					return(_errorMsg == String.Empty);

                case ActionType.GetBySystemName:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
					return(_errorMsg == String.Empty);

				default:
					break;
			}

			return true;

		}

		private ExternalSystemRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			ExternalSystemRefBE externalSystemRefBE = new ExternalSystemRefBE();

			externalSystemRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
			externalSystemRefBE.ExternalSystemName = TryToParse<string>(dataRow["EXTERNAL_SYSTEM_NM"]);
			externalSystemRefBE.ExternalSystemDesc = TryToParse<string>(dataRow["EXTERNAL_SYSTEM_DESC"]);
			externalSystemRefBE.AddlInformationText = TryToParse<string>(dataRow["ADDL_INFORMATION_TXT"]);
			externalSystemRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			externalSystemRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			externalSystemRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			externalSystemRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			externalSystemRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);

			return externalSystemRefBE;

		}

		#endregion

	}

	#endregion

}