using Meck.Data;
using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AION.Estimator.Engine.BusinessObjects
{
    public class EstimatorBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private EstimatorBE _estimatorBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(EstimatorBE estimatorBE)
        {
            int id;
            _estimatorBE = estimatorBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@LAST_NM", estimatorBE.ParcelId);
                sqlParameters[1] = new SqlParameter("@MIDDLE_NM", estimatorBE.ParentPropertyId);
                sqlParameters[2] = new SqlParameter("@FIRST_NM", estimatorBE.BusinessName);
                sqlParameters[3] = new SqlParameter("@GENDER_REF_ID", estimatorBE.PropertyAddress);
                sqlParameters[4] = new SqlParameter("@BIRTH_DT", estimatorBE.Aassessment);
                sqlParameters[5] = new SqlParameter("@AGE_NUM", estimatorBE.TaxYear);
                sqlParameters[6] = new SqlParameter("@ETHNICITY_REF_ID", estimatorBE.AssessedDate);
                sqlParameters[7] = new SqlParameter("RACE_OTHER_DESC_TXT", estimatorBE.TownTaxJurisdiction);
                sqlParameters[8] = new SqlParameter("@RACE_REF_ID", estimatorBE.AbstractPK);
                sqlParameters[9] = new SqlParameter("@WKR_ID_TXT", estimatorBE.UserId);

                sqlParameters[10] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[10].Direction = ParameterDirection.Output;

                id = Meck.Data.SqlWrapper.RunSPReturnInteger("usp_insert_aion_estimator", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Update(EstimatorBE estimatorBE)
        {
            int id;
            _estimatorBE = estimatorBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[12];

                sqlParameters[0] = new SqlParameter("@PERSON_ID ", estimatorBE.EstimatorId);
                sqlParameters[1] = new SqlParameter("@LAST_NM", estimatorBE.ParcelId);
                sqlParameters[2] = new SqlParameter("@MIDDLE_NM", estimatorBE.ParentPropertyId);
                sqlParameters[3] = new SqlParameter("@FIRST_NM", estimatorBE.BusinessName);
                sqlParameters[4] = new SqlParameter("@GENDER_REF_ID", estimatorBE.PropertyAddress);
                sqlParameters[5] = new SqlParameter("@BIRTH_DT", estimatorBE.Aassessment);
                sqlParameters[6] = new SqlParameter("@AGE_NUM", estimatorBE.TaxYear);
                sqlParameters[7] = new SqlParameter("@ETHNICITY_REF_ID", estimatorBE.AssessedDate);
                sqlParameters[8] = new SqlParameter("RACE_OTHER_DESC_TXT", estimatorBE.TownTaxJurisdiction);
                sqlParameters[9] = new SqlParameter("@RACE_REF_ID", estimatorBE.AbstractPK);
                sqlParameters[10] = new SqlParameter("@WKR_ID_TXT", estimatorBE.UserId);

                sqlParameters[11] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[11].Direction = ParameterDirection.Output;

                id = Meck.Data.SqlWrapper.RunSPReturnInteger("usp_update_aion_estimator", base.ConnectionString, ref sqlParameters);
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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_estimator", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public EstimatorBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            EstimatorBE estimatorBE = new EstimatorBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_estimator_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                estimatorBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);

            }

            return estimatorBE;
        }

        public List<EstimatorBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<EstimatorBE> estimatorBEList = new List<EstimatorBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_estimator_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    estimatorBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return estimatorBEList;
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

                sqlParameters[0] = new SqlParameter("@SCHOOL_CATEGORY_REF_ID", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_estimator_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }


            return dataSet;
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
                    if (_estimatorBE == null)
                    {
                        _errorMsg = "Error On Estimator Engine Create Method. Missing EstimatorBE";
                        return false;
                    }
                    break;

                case ActionType.Update:
                    if (_estimatorBE == null)
                    {
                        _errorMsg = "Error On Estimator Engine Update Method. Missing EstimatorBE";
                        return false;
                    }
                    break;

                case ActionType.Delete:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Estimator Engine Update Method. Missing id";
                        return false;
                    }
                    break;

                case ActionType.GetById:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Estimator Engine GetById Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.GetList:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Estimator Engine GetList Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.GetDataSet:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Estimator Engine GetDataSet Method. Missing Id";
                        return false;
                    }
                    break;

                default:
                    break;
            }

            return true;

        }

        private EstimatorBE ConvertDataRowToBE(DataRow dataRow)
        {
            EstimatorBE estimatorBE = new EstimatorBE();

            estimatorBE.EstimatorId = TryToParse<int>(dataRow["PERSON_ID"]);
            estimatorBE.ParcelId = TryToParse<int>(dataRow["PARCEL_ID"]);
            estimatorBE.ParentPropertyId = TryToParse<int>(dataRow["AGE_NUM"]);
            estimatorBE.BusinessName = TryToParse<string>(dataRow["FIRST_NM"]);
            estimatorBE.PropertyAddress = TryToParse<string>(dataRow["MIDDLE_NM"]);
            estimatorBE.Aassessment = TryToParse<decimal?>(dataRow["LAST_NM"]);
            estimatorBE.TaxYear = TryToParse<decimal?>(dataRow["GENDER_REF_ID"]);
            estimatorBE.AssessedDate = TryToParse<DateTime?>(dataRow["ETHNICITY_REF_ID"]);
            estimatorBE.TownTaxJurisdiction = TryToParse<string>(dataRow["RACE_REF_ID"]);
            estimatorBE.AbstractPK = TryToParse<decimal?>(dataRow["RACE_OTHER_DESC_TXT"]);

            return estimatorBE;
        }

        #endregion

    }


}
