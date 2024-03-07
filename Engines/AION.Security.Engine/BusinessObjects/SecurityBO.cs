using AION.Base;
using AION.Security.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AION.Security.Engine.BusinessObjects
{
    public class SecurityBO : BaseBO
    {
        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private SecurityBE _SecurityBE;

        private int _id;

        #endregion

        #region Public Methods

        public SecurityBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            SecurityBE SecurityBe = new SecurityBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_Security_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                SecurityBe = this.ConvertDataRowToBe(dataSet.Tables[0].Rows[0]);

            }

            return SecurityBe;
        }


        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Create:
                    if (_SecurityBE == null)
                    {
                        _errorMsg = "Error On Security Engine Create Method. Missing SecurityBE";
                        return false;
                    }
                    break;

                case ActionType.Update:
                    if (_SecurityBE == null)
                    {
                        _errorMsg = "Error On Security Engine Update Method. Missing SecurityBE";
                        return false;
                    }
                    break;

                case ActionType.GetList:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Security Engine GetList Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.GetById:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Security Engine GetById Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.Delete:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Security Engine Delete Method. Missing Id";
                        return false;
                    }
                    break;

                default:
                    break;
            }

            return true;

        }


        private SecurityBE ConvertDataRowToBe(DataRow dataRow)
        {
            SecurityBE SecurityBE = new SecurityBE();

            SecurityBE.BusinessName = dataRow["BUSINESS_NAME"].ToString();
            SecurityBE.ParentPropertyID = TryToParse<decimal?>(dataRow["PARENT_PHA_PK"].ToString());
            SecurityBE.ParcelID = TryToParse<decimal?>(dataRow["Parcel ID"].ToString());
            SecurityBE.BusinessName = dataRow["BUSINESS_NAME"].ToString();
            SecurityBE.PropertyAddress = dataRow["Property Address"].ToString();
            SecurityBE.Aassessment = TryToParse<decimal?>(dataRow["Assessment"].ToString());
            SecurityBE.TaxYear = TryToParse<decimal?>(dataRow["TAX_YEAR"].ToString());
            SecurityBE.AssessedDate = TryToParse<DateTime>(dataRow["Assessed Date"].ToString());
            SecurityBE.AbstractPK = TryToParse<decimal?>(dataRow["ABSTRACT_PK"].ToString());
            _SecurityBE = SecurityBE;
            return SecurityBE;

        }
        #endregion

    }
}
