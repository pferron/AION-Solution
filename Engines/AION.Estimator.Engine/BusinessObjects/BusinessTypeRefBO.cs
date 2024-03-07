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

	#region BusinessObject - BusinessTypeRefBO

	public class BusinessTypeRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, GetAllList, Update };

		private string _errorMsg;

		private BusinessTypeRefBE _businessTypeRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(BusinessTypeRefBE businessTypeRefBE)
		{
			int id;
			_businessTypeRefBE = businessTypeRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[7];

				sqlParameters[0] = new SqlParameter("@BUSINESS_REF_TYP_SHORT_DESC", businessTypeRefBE.BusinessRefTypeShortDesc);
				sqlParameters[1] = new SqlParameter("@BUSINESS_REF_DISPLAY_NM", businessTypeRefBE.BusinessRefDisplayName);
				sqlParameters[2] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", businessTypeRefBE.ExternalSystemRefId);
				sqlParameters[3] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", businessTypeRefBE.BusinessRef_SrcSystemValueText);
				sqlParameters[4] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", businessTypeRefBE.BusinessRef_EnumMappingValNbr);

				sqlParameters[5] = new SqlParameter("@WKR_ID_TXT",businessTypeRefBE.UserId);

				sqlParameters[6] = new SqlParameter("@ReturnValue",0);
				sqlParameters[6].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_business_type_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_business_type_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public BusinessTypeRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			BusinessTypeRefBE businessTypeRefBE = new BusinessTypeRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				businessTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return businessTypeRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_type_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<BusinessTypeRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<BusinessTypeRefBE> businessTypeRefBEList = new List<BusinessTypeRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_type_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					businessTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return businessTypeRefBEList;

		}

        public List<BusinessTypeRefBE> GetAllList()
        {

            if (!this.Validate(ActionType.GetAllList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<BusinessTypeRefBE> businessTypeRefBEList = new List<BusinessTypeRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_type_ref_get_Alllist", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    businessTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return businessTypeRefBEList;

        }

        public int Update(BusinessTypeRefBE businessTypeRefBE)
		{
			int rows;
			_businessTypeRefBE = businessTypeRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[9];

				sqlParameters[0] = new SqlParameter("@BUSINESS_TYP_REF_ID", businessTypeRefBE.BusinessTypeRefId);
				sqlParameters[1] = new SqlParameter("@BUSINESS_REF_TYP_SHORT_DESC", businessTypeRefBE.BusinessRefTypeShortDesc);
				sqlParameters[2] = new SqlParameter("@BUSINESS_REF_DISPLAY_NM", businessTypeRefBE.BusinessRefDisplayName);
				sqlParameters[3] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", businessTypeRefBE.ExternalSystemRefId);
				sqlParameters[4] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", businessTypeRefBE.BusinessRef_SrcSystemValueText);
				sqlParameters[5] = new SqlParameter("@UPDATED_DTTM", businessTypeRefBE.UpdatedDate);
				sqlParameters[6] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", businessTypeRefBE.BusinessRef_EnumMappingValNbr);

				sqlParameters[7] = new SqlParameter("@WKR_ID_TXT",businessTypeRefBE.UserId);

				sqlParameters[8] = new SqlParameter("@ReturnValue",0);
				sqlParameters[8].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_business_type_ref", base.ConnectionString, ref sqlParameters);

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

                case ActionType.GetAllList:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
					return(_errorMsg == String.Empty);

				default:
					break;
			}

			return true;

		}

		private BusinessTypeRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			BusinessTypeRefBE businessTypeRefBE = new BusinessTypeRefBE();

			businessTypeRefBE.BusinessTypeRefId = TryToParse<int?>(dataRow["BUSINESS_TYP_REF_ID"]);
			businessTypeRefBE.BusinessRefTypeShortDesc = TryToParse<string>(dataRow["BUSINESS_REF_TYP_SHORT_DESC"]);
			businessTypeRefBE.BusinessRefDisplayName = TryToParse<string>(dataRow["BUSINESS_REF_DISPLAY_NM"]);
			businessTypeRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
			businessTypeRefBE.BusinessRef_SrcSystemValueText = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
			businessTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			businessTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			businessTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			businessTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			businessTypeRefBE.BusinessRef_EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);

			return businessTypeRefBE;

		}

		#endregion

	}

	#endregion

}