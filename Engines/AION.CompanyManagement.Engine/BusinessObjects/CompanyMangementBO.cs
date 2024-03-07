using AION.Base;
using AION.CompanyMangement.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace AION.CompanyMangement.Engine.BusinessObjects
{
    public class CompanyMangementBO : BaseBO
    {
        #region Private Members
        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private CompanyMangementBE _templateBE;

        private int _id = 0;


        #endregion


        #region Public Methods
        public List<CompanyMangementBE> GetList()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<CompanyMangementBE> TemplateBeList = new List<CompanyMangementBE>();

            try
            {
                //SqlParameter[] sqlParameters = new SqlParameter[1];
                //sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_company_management_get_list", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    TemplateBeList.Add(this.ConvertDataRowToBe(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return TemplateBeList;

        }


        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Create:
                    if (_templateBE == null)
                    {
                        _errorMsg = "Error On CompanyManagement Engine Create Method. Missing CompanyManagementBE";
                        return false;
                    }
                    break;

                case ActionType.Update:
                    if (_templateBE == null)
                    {
                        _errorMsg = "Error On CompanyManagement Engine Update Method. Missing CompanyManagementBE";
                        return false;
                    }
                    break;

                case ActionType.GetList:
                    if (_id != 0)
                    {
                        _errorMsg = "Error On CompanyManagement Engine GetList Method. GetList Should Not have Id";
                        return false;
                    }
                    break;

                case ActionType.GetById:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On CompanyManagement Engine GetById Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.Delete:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On CompanyManagement Engine Delete Method. Missing Id";
                        return false;
                    }
                    break;

                default:
                    break;
            }

            return true;

        }


        private CompanyMangementBE ConvertDataRowToBe(DataRow dataRow)
        {
            CompanyMangementBE templateBE = new CompanyMangementBE();

            templateBE.EstOrFinal = dataRow["EST_OR_FINAL"].ToString();
            templateBE.TaxYear = TryToParse<decimal?>(dataRow["TAX_YEAR"].ToString());
            templateBE.TaxDistrictType = dataRow["TAX_DISTRICT_TYPE"].ToString();
            templateBE.TaxRate = TryToParse<decimal?>(dataRow["TAX_RATE"].ToString());
            templateBE.VerifiedDate = TryToParse<DateTime>(dataRow["VERIFIED_DATE"].ToString());
            _templateBE = templateBE;
            return templateBE;

        }


        #endregion

    }
}
