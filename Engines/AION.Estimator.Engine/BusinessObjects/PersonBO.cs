#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Base;
using AION.Estimator.Engine.BusinessEntities;

#endregion

namespace AION.Engine.BusinessObjects
{

	#region BusinessObject - PersonBO

	public class PersonBO : BaseBO
	{

		#region Private Members

		private enum ActionType {Create, Delete, GetById, GetDataSet, GetList, Update};

		private string _errorMsg;

		private PersonBE _personBE;

		private int _id;

		#endregion

		#region Public Methods

		public int Create(PersonBE personBE)
		{
			int id;
			_personBE = personBE;

			if (!this.Validate(ActionType.Create))
				throw(new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[11];

				sqlParameters[0] = new SqlParameter("@LAST_NM", personBE.LastName);
				sqlParameters[1] = new SqlParameter("@MIDDLE_NM", personBE.MiddleName);
				sqlParameters[2] = new SqlParameter("@FIRST_NM", personBE.FirstName);
				sqlParameters[3] = new SqlParameter("@GENDER_REF_ID", personBE.GenderRefId);
				sqlParameters[4] = new SqlParameter("@BIRTH_DT", personBE.Birthdate);
				sqlParameters[5] = new SqlParameter("@AGE_NUM", personBE.Age);
				sqlParameters[6] = new SqlParameter("@ETHNICITY_REF_ID", personBE.EthnicityRefId);
				sqlParameters[7] = new SqlParameter("@RACE_OTHER_DESC_TXT", personBE.RaceOtherDescription);
				sqlParameters[8] = new SqlParameter("@RACE_REF_ID", personBE.RaceRefId);

				sqlParameters[9] = new SqlParameter("@WKR_ID_TXT",personBE.UserId);

				sqlParameters[10] = new SqlParameter("@ReturnValue",0);
				sqlParameters[10].Direction  = ParameterDirection.Output;

				id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_person", base.ConnectionString, ref sqlParameters);

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

				rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_person", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return rows;

		}

		public PersonBE GetById(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetById))
				throw (new Exception(_errorMsg));

			PersonBE personBE = new PersonBE();
			DataSet dataSet;

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_person_get_by_id", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			if (dataSet.Tables[0].Rows.Count > 0)
			{
				personBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
			}

			return personBE;
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

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_person_get_list", base.ConnectionString, ref sqlParameters);

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return dataSet;
		}

		public List<PersonBE> GetList(int id)
		{
			_id = id;

			if (!this.Validate(ActionType.GetList))
				throw (new Exception(_errorMsg));

			DataSet dataSet;
			List<PersonBE> personBEList = new List<PersonBE>();

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[1];

				sqlParameters[0] = new SqlParameter("@identity", id);

				dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_person_get_list", base.ConnectionString, ref sqlParameters);

				foreach (DataRow dataRow in dataSet.Tables[0].Rows)
				{
					personBEList.Add(this.ConvertDataRowToBE(dataRow));
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}

			return personBEList;

		}

		public int Update(PersonBE personBE)
		{
			int rows;
			_personBE = personBE;

			if (!this.Validate(ActionType.Update))
			throw (new Exception(_errorMsg));

			try
			{
				SqlParameter[] sqlParameters = new SqlParameter[13];

				sqlParameters[0] = new SqlParameter("@PERSON_ID", personBE.PersonId);
				sqlParameters[1] = new SqlParameter("@LAST_NM", personBE.LastName);
				sqlParameters[2] = new SqlParameter("@MIDDLE_NM", personBE.MiddleName);
				sqlParameters[3] = new SqlParameter("@FIRST_NM", personBE.FirstName);
				sqlParameters[4] = new SqlParameter("@GENDER_REF_ID", personBE.GenderRefId);
				sqlParameters[5] = new SqlParameter("@BIRTH_DT", personBE.Birthdate);
				sqlParameters[6] = new SqlParameter("@AGE_NUM", personBE.Age);
				sqlParameters[7] = new SqlParameter("@ETHNICITY_REF_ID", personBE.EthnicityRefId);
				sqlParameters[8] = new SqlParameter("@RACE_OTHER_DESC_TXT", personBE.RaceOtherDescription);
				sqlParameters[9] = new SqlParameter("@RACE_REF_ID", personBE.RaceRefId);
				sqlParameters[10] = new SqlParameter("@UPDATED_DTTM", personBE.UpdatedDate);

				sqlParameters[11] = new SqlParameter("@WKR_ID_TXT",personBE.UserId);

				sqlParameters[12] = new SqlParameter("@ReturnValue",0);
				sqlParameters[12].Direction  = ParameterDirection.Output;

				rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_person", base.ConnectionString, ref sqlParameters);

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

		private PersonBE ConvertDataRowToBE(DataRow dataRow)
		{
			PersonBE personBE = new PersonBE();

			personBE.PersonId = TryToParse<int?>(dataRow["PERSON_ID"]);
			personBE.LastName = TryToParse<string>(dataRow["LAST_NM"]);
			personBE.MiddleName = TryToParse<string>(dataRow["MIDDLE_NM"]);
			personBE.FirstName = TryToParse<string>(dataRow["FIRST_NM"]);
			personBE.GenderRefId = TryToParse<int?>(dataRow["GENDER_REF_ID"]);
			personBE.Birthdate = TryToParse<DateTime?>(dataRow["BIRTH_DT"]);
			personBE.Age = TryToParse<int?>(dataRow["AGE_NUM"]);
			personBE.EthnicityRefId = TryToParse<int?>(dataRow["ETHNICITY_REF_ID"]);
			personBE.RaceOtherDescription = TryToParse<string>(dataRow["RACE_OTHER_DESC_TXT"]);
			personBE.RaceRefId = TryToParse<int?>(dataRow["RACE_REF_ID"]);
			personBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
			personBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
			personBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
			personBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);

			return personBE;

		}

		#endregion

	}

	#endregion

}