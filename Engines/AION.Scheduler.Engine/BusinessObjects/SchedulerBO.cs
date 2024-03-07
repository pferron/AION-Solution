using AION.Base;
using AION.Scheduler.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AION.Scheduler.Engine.BusinessObjects
{
    public class SchedulerBO : BaseBO
    {
        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private SchedulerBE _schedulerBE;

        private int _id;

        #endregion

        #region Public Methods

        public SchedulerBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            SchedulerBE SchedulerBe = new SchedulerBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_scheduler_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                SchedulerBe = this.ConvertDataRowToBe(dataSet.Tables[0].Rows[0]);

            }

            return SchedulerBe;
        }


        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Create:
                    if (_schedulerBE == null)
                    {
                        _errorMsg = "Error On Scheduler Engine Create Method. Missing SchedulerBE";
                        return false;
                    }
                    break;

                case ActionType.Update:
                    if (_schedulerBE == null)
                    {
                        _errorMsg = "Error On Scheduler Engine Update Method. Missing SchedulerBE";
                        return false;
                    }
                    break;

                case ActionType.GetList:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Scheduler Engine GetList Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.GetById:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Scheduler Engine GetById Method. Missing Id";
                        return false;
                    }
                    break;

                case ActionType.Delete:
                    if (_id == 0)
                    {
                        _errorMsg = "Error On Scheduler Engine Delete Method. Missing Id";
                        return false;
                    }
                    break;

                default:
                    break;
            }

            return true;

        }


        private SchedulerBE ConvertDataRowToBe(DataRow dataRow)
        {
            SchedulerBE schedulerBE = new SchedulerBE();

            schedulerBE.BusinessName = dataRow["BUSINESS_NAME"].ToString();
            schedulerBE.ParentPropertyID = TryToParse<decimal?>(dataRow["PARENT_PHA_PK"].ToString());
            schedulerBE.ParcelID = TryToParse<decimal?>(dataRow["Parcel ID"].ToString());
            schedulerBE.BusinessName = dataRow["BUSINESS_NAME"].ToString();
            schedulerBE.PropertyAddress = dataRow["Property Address"].ToString();
            schedulerBE.Aassessment = TryToParse<decimal?>(dataRow["Assessment"].ToString());
            schedulerBE.TaxYear = TryToParse<decimal?>(dataRow["TAX_YEAR"].ToString());
            schedulerBE.AssessedDate = TryToParse<DateTime>(dataRow["Assessed Date"].ToString());
            schedulerBE.AbstractPK = TryToParse<decimal?>(dataRow["ABSTRACT_PK"].ToString());

            return schedulerBE;

        }
        #endregion

    }
}
