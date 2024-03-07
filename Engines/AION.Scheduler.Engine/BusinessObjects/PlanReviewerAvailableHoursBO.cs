#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{

    #region BusinessObject - PlanReviewerAvailableHoursBO

    public class PlanReviewerAvailableHoursBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private PlanReviewerAvailableHoursBE _planReviewerAvailableHoursBE;

        private int _id;

        #endregion

        #region Public Methods
        /*
         * jcl - 12/14/2022
         * This has been moved out of it's own table to the Project_type_ref table
         * These procedures have been updated to get the data from that table
         * The update occurs in the project type ref table as well
         * 
         * 
         * */

        public List<PlanReviewerAvailableHoursBE> GetAllPlanReviewerAvailableHours()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewerAvailableHoursBE> planReviewerAvailableHoursBEList = new List<PlanReviewerAvailableHoursBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_reviewer_available_hours_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewerAvailableHoursBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewerAvailableHoursBEList;

        }

        public int Update(PlanReviewerAvailableHoursBE planReviewerAvailableHoursBE)
        {
            int rows;
            _planReviewerAvailableHoursBE = planReviewerAvailableHoursBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PROJECT_TYP_REF_ID", planReviewerAvailableHoursBE.ProjectTypeRefId);
                sqlParameters[1] = new SqlParameter("@AVAILABLE_HOURS_NBR", planReviewerAvailableHoursBE.AvailableHours);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", planReviewerAvailableHoursBE.UpdatedDate);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", planReviewerAvailableHoursBE.UpdatedByWkrId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_reviewer_available_hours", base.ConnectionString, ref sqlParameters);

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
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }

            return true;

        }

        private PlanReviewerAvailableHoursBE ConvertDataRowToBE(DataRow dataRow)
        {
            PlanReviewerAvailableHoursBE planReviewerAvailableHoursBE = new PlanReviewerAvailableHoursBE();

            planReviewerAvailableHoursBE.ID = TryToParse<int>(dataRow["PROJECT_TYP_REF_ID"]);
            planReviewerAvailableHoursBE.AvailableHours = TryToParse<decimal?>(dataRow["AVAILABLE_HOURS_NBR"]);
            planReviewerAvailableHoursBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewerAvailableHoursBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewerAvailableHoursBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewerAvailableHoursBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            planReviewerAvailableHoursBE.EnumMappingValNbr = TryToParse<int?>(dataRow["AVAILABLE_HOURS_ENUM_MAPPING_VAL_NBR"]);
            planReviewerAvailableHoursBE.PlanReviewTypeCd = TryToParse<string>(dataRow["AVAILABLE_HOURS_PLAN_REVIEWER_TYP_DESC"]);
            planReviewerAvailableHoursBE.ProjectTypeRefId = TryToParse<int>(dataRow["PROJECT_TYP_REF_ID"]);
            return planReviewerAvailableHoursBE;

        }

        #endregion

    }

    #endregion

}