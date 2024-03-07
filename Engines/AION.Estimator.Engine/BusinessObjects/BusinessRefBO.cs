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

	#region BusinessObject - BusinessRefBO

	public class BusinessRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, GetAllList, Update };

		private string _errorMsg;

		private BusinessRefBE _businessRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(BusinessRefBE businessRefBE)
		{
			int id;
			_businessRefBE = businessRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[10];

				sqlParameters[0] = new SqlParameter("@BUSINESS_NM", businessRefBE.BusinessName);
				sqlParameters[1] = new SqlParameter("@BUSINESS_SHORT_DESC", businessRefBE.BusinessShortDesc);
				sqlParameters[2] = new SqlParameter("@BUSINESS_TYP_REF_ID", businessRefBE.BusinessTypeRefId);
				sqlParameters[3] = new SqlParameter("@DIVISION_REF_ID", businessRefBE.DisionRefId);
				sqlParameters[4] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", businessRefBE.EnumMappingNumber);
				sqlParameters[5] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", businessRefBE.ExternalSystemRefId);
				sqlParameters[6] = new SqlParameter("@REGION_REF_ID", businessRefBE.RegionRefId);
				sqlParameters[7] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", businessRefBE.SourceSystemValueText);

				sqlParameters[8] = new SqlParameter("@WKR_ID_TXT",businessRefBE.UserId);

				sqlParameters[9] = new SqlParameter("@ReturnValue",0);
				sqlParameters[9].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_business_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_business_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public BusinessRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			BusinessRefBE businessRefBE = new BusinessRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				businessRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return businessRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<BusinessRefBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<BusinessRefBE> businessRefBEList = new List<BusinessRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					businessRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return businessRefBEList;

		}

        public List<BusinessRefBE> GetAllList()
        {

            if (!this.Validate(ActionType.GetAllList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<BusinessRefBE> businessRefBEList = new List<BusinessRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_business_ref_get_Alllist", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    businessRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return businessRefBEList;

        }


        public int Update(BusinessRefBE businessRefBE)
		{
			int rows;
			_businessRefBE = businessRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[12];

				sqlParameters[0] = new SqlParameter("@BUSINESS_NM", businessRefBE.BusinessName);
				sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", businessRefBE.BusinessRefId);
				sqlParameters[2] = new SqlParameter("@BUSINESS_SHORT_DESC", businessRefBE.BusinessShortDesc);
				sqlParameters[3] = new SqlParameter("@BUSINESS_TYP_REF_ID", businessRefBE.BusinessTypeRefId);
				sqlParameters[4] = new SqlParameter("@DIVISION_REF_ID", businessRefBE.DisionRefId);
				sqlParameters[5] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", businessRefBE.EnumMappingNumber);
				sqlParameters[6] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", businessRefBE.ExternalSystemRefId);
				sqlParameters[7] = new SqlParameter("@REGION_REF_ID", businessRefBE.RegionRefId);
				sqlParameters[8] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", businessRefBE.SourceSystemValueText);
				sqlParameters[9] = new SqlParameter("@UPDATED_DTTM", businessRefBE.UpdatedDate);

				sqlParameters[10] = new SqlParameter("@WKR_ID_TXT",businessRefBE.UserId);

				sqlParameters[11] = new SqlParameter("@ReturnValue",0);
				sqlParameters[11].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_business_ref", base.ConnectionString, ref sqlParameters);

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

		private BusinessRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			BusinessRefBE businessRefBE = new BusinessRefBE();

			businessRefBE.BusinessName = TryToParse<string>(dataRow["BUSINESS_NM"]);
			businessRefBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
			businessRefBE.BusinessShortDesc = TryToParse<string>(dataRow["BUSINESS_SHORT_DESC"]);
			businessRefBE.BusinessTypeRefId = TryToParse<int?>(dataRow["BUSINESS_TYP_REF_ID"]);
			businessRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			businessRefBE.DisionRefId = TryToParse<int?>(dataRow["DIVISION_REF_ID"]);
			businessRefBE.EnumMappingNumber = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
			businessRefBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
			businessRefBE.RegionRefId = TryToParse<int?>(dataRow["REGION_REF_ID"]);
			businessRefBE.SourceSystemValueText = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
			businessRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			businessRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			businessRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);

			return businessRefBE;

		}

		#endregion

	}

	#endregion

}