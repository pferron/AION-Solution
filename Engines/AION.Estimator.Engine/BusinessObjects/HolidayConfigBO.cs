#region Using

using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{
    #region BusinessObject - AdminBO
    public class HolidayConfigBO:BaseBO
    {
        #region Private Members

        private enum ActionType { Create, Delete, GetList };

        private string _errorMsg;

        private HolidayConfigBE _holidayConfigBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(HolidayConfigBE holidayConfigBE)
        {
            int id;
            _holidayConfigBE = holidayConfigBE;

            //if (!this.Validate(ActionType.Create))
            //    throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@HOLIDAY_NM", holidayConfigBE.HolidayNm);
                sqlParameters[1] = new SqlParameter("@HOLIDAY_DT", holidayConfigBE.HolidayDate);
                sqlParameters[2] = new SqlParameter("@HOLIDAY_ANNUAL_RECUR_IND", holidayConfigBE.HolidayAnnualRecurInd);
                sqlParameters[3] = new SqlParameter("@ACTIVE_IND", holidayConfigBE.IsActive);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_holidayconfig", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Inactivate (int id)
        {
            int rows;
            _id = id;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@identity", id);
                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_holidayconfig_inactivate", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return rows;
        }

        public List<HolidayConfigBE> GetList()
        {
         

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<HolidayConfigBE> holidayBEList = new List<HolidayConfigBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_holidayconfig_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    holidayBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return holidayBEList;

        }

        public List<DateTime> GetDateList()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<DateTime> holidayBEList = new List<DateTime>();

            try
            {
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_holiday_config_get_dates", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    holidayBEList.Add(this.ConvertDataRowToDateTime(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return holidayBEList;

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
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }
            return true;
        }

        private HolidayConfigBE ConvertDataRowToBE(DataRow dataRow)
        {
         
         //   var data = Convert.ToDateTime(dataRow["HOLIDAY_DT"].ToString()).ToString("mmddyyyy").ToString();
            HolidayConfigBE holidayConfigBE = new HolidayConfigBE();
            holidayConfigBE.HolidayConfigId = TryToParse<int>(dataRow["HOLIDAY_CONFIG_ID"]);
            holidayConfigBE.HolidayNm = TryToParse<string>(dataRow["HOLIDAY_NM"]);
            holidayConfigBE.HolidayDate = Convert.ToDateTime(dataRow["HOLIDAY_DT"]);
            holidayConfigBE.HolidayAnnualRecurInd = TryToParse<bool>(dataRow["HOLIDAY_ANNUAL_RECUR_IND"]);
            holidayConfigBE.IsActive = TryToParse<bool>(dataRow["ACTIVE_IND"]);
            return holidayConfigBE;
        }

        private DateTime ConvertDataRowToDateTime(DataRow dataRow)
        {
            return Convert.ToDateTime(dataRow["HOLIDAY_DT"]);
        }
        #endregion
    }
    #endregion
}
