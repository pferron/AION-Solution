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

    #region BusinessObject - PlanReviewerAvailableTimeBO

    public class PlanReviewerAvailableTimeBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private PlanReviewerAvailableTimeBE _planReviewerAvailableTimeBE;

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

        public List<PlanReviewerAvailableTimeBE> GetAllPlanReviewerAvailableTimes()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewerAvailableTimeBE> planReviewerAvailableTimeBEList = new List<PlanReviewerAvailableTimeBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_reviewer_available_time_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewerAvailableTimeBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewerAvailableTimeBEList;

        }


        public int Update(PlanReviewerAvailableTimeBE planReviewerAvailableTimeBE)
        {
            int rows;
            _planReviewerAvailableTimeBE = planReviewerAvailableTimeBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@PROJECT_TYP_REF_ID", planReviewerAvailableTimeBE.ProjectTypeRefID);
                sqlParameters[1] = new SqlParameter("@AVAILABLE_START_TM", planReviewerAvailableTimeBE.AvailableStartTime);
                sqlParameters[2] = new SqlParameter("@AVAILABLE_END_TM", planReviewerAvailableTimeBE.AvailableEndTime);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", planReviewerAvailableTimeBE.UpdatedDate);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", planReviewerAvailableTimeBE.UpdatedByWkrId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_reviewer_available_time", base.ConnectionString, ref sqlParameters);

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

        private PlanReviewerAvailableTimeBE ConvertDataRowToBE(DataRow dataRow)
        {
            PlanReviewerAvailableTimeBE planReviewerAvailableTimeBE = new PlanReviewerAvailableTimeBE();

            planReviewerAvailableTimeBE.PlanReviewerAvailableTimeID = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            planReviewerAvailableTimeBE.AvailableStartTime = TryToParse<DateTime?>(dataRow["AVAILABLE_START_TM"]);
            planReviewerAvailableTimeBE.AvailableEndTime = TryToParse<DateTime?>(dataRow["AVAILABLE_END_TM"]);
            planReviewerAvailableTimeBE.ProjectTypeDesc = TryToParse<string>(dataRow["PROJECT_TYP_REF_DISPLAY_NM"]);
            planReviewerAvailableTimeBE.ProjectTypeRefID = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            planReviewerAvailableTimeBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewerAvailableTimeBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewerAvailableTimeBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewerAvailableTimeBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return planReviewerAvailableTimeBE;

        }

        #endregion

    }

    #endregion

}