#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Base;
using AION.Estimator.Engine.BusinessEntities;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

	#region BusinessObject - UserProjectTypeRefBO

	public class UserProjectTypeRefBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private UserProjectTypeRefBE _userProjectTypeRefBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(UserProjectTypeRefBE userProjectTypeRefBE)
		{
			int id;
			_userProjectTypeRefBE = userProjectTypeRefBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[4];

				sqlParameters[0] = new SqlParameter("@PROJECT_TYP_REF_ID", userProjectTypeRefBE.ProjectTypeRefID);
				sqlParameters[1] = new SqlParameter("@USER_ID", userProjectTypeRefBE.UserID);

				sqlParameters[2] = new SqlParameter("@WKR_ID_TXT",userProjectTypeRefBE.CreatedByWkrId);

				sqlParameters[3] = new SqlParameter("@ReturnValue",0);
				sqlParameters[3].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_project_type_ref", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_project_type_ref", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

        public int DeleteAllByUser(int userID)
        {
            int rows;
            _id = userID;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("identity", userID);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_project_type_ref_byuser", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public UserProjectTypeRefBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			UserProjectTypeRefBE userProjectTypeRefBE = new UserProjectTypeRefBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_project_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				userProjectTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return userProjectTypeRefBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_project_type_ref_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<UserProjectTypeRefBE> GetListByUserID(int UserID)
		{
			_id = UserID;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<UserProjectTypeRefBE> userProjectTypeRefBEList = new List<UserProjectTypeRefBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", UserID);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_project_type_ref_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					userProjectTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return userProjectTypeRefBEList;

		}

		public int Update(UserProjectTypeRefBE userProjectTypeRefBE)
		{
			int rows;
			_userProjectTypeRefBE = userProjectTypeRefBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[6];

				sqlParameters[0] = new SqlParameter("@USER_PROJECT_TYP_CROSS_REF_ID", userProjectTypeRefBE.UserProjectTypeCrossRefID);
				sqlParameters[1] = new SqlParameter("@PROJECT_TYP_REF_ID", userProjectTypeRefBE.ProjectTypeRefID);
				sqlParameters[2] = new SqlParameter("@USER_ID", userProjectTypeRefBE.UserID);
				sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", userProjectTypeRefBE.UpdatedDate);

				sqlParameters[4] = new SqlParameter("@WKR_ID_TXT",userProjectTypeRefBE.UserId);

				sqlParameters[5] = new SqlParameter("@ReturnValue",0);
				sqlParameters[5].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_project_type_ref", base.ConnectionString, ref sqlParameters);

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

		private UserProjectTypeRefBE ConvertDataRowToBE(DataRow dataRow)
		{
			UserProjectTypeRefBE userProjectTypeRefBE = new UserProjectTypeRefBE();

			userProjectTypeRefBE.UserProjectTypeCrossRefID = TryToParse<int?>(dataRow["USER_PROJECT_TYP_CROSS_REF_ID"]);
			userProjectTypeRefBE.ProjectTypeRefID = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
			userProjectTypeRefBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
			userProjectTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			userProjectTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
			userProjectTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			userProjectTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

			return userProjectTypeRefBE;

		}

		#endregion

	}

	#endregion

}