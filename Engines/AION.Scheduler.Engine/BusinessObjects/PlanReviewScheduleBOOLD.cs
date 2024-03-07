#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


#endregion

namespace AION.Engine.BusinessObjects
{

    #region BusinessObject - PlanReviewScheduleBO

    public class PlanReviewScheduleBOOLD : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private PlanReviewScheduleBEOLD _planReviewScheduleBE;

        private int _id;


        #endregion

        #region Public Methods

        public int Create(PlanReviewScheduleBEOLD planReviewScheduleBE)
        {
            int id;
            _planReviewScheduleBE = planReviewScheduleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[21];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", planReviewScheduleBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", planReviewScheduleBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@START_DT", planReviewScheduleBE.StartDt);
                sqlParameters[3] = new SqlParameter("@END_DT", planReviewScheduleBE.EndDt);
                sqlParameters[4] = new SqlParameter("@POOL_REQUEST_IND", planReviewScheduleBE.PoolRequestInd);
                sqlParameters[5] = new SqlParameter("@FIFO_REQUEST_IND", planReviewScheduleBE.FifoRequestInd);
                sqlParameters[6] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", planReviewScheduleBE.ApptResponseStatusRefId);
                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", planReviewScheduleBE.UserId);
                sqlParameters[8] = new SqlParameter("@PLAN_REVIEW_PROJECT_DETAILS_ID", planReviewScheduleBE.PlanReviewProjectDetailsId);
                sqlParameters[9] = new SqlParameter("@CYCLE_NBR", planReviewScheduleBE.Cycle);
                sqlParameters[10] = new SqlParameter("@PLANS_READY_ON_DT", planReviewScheduleBE.ProdDate);
                sqlParameters[11] = new SqlParameter("@REQUEST_EXPRESS_NEXT_CYCLE_IND", planReviewScheduleBE.RequestExpressNextCycle);
                sqlParameters[12] = new SqlParameter("@IS_FUTURE_CYCLE_IND", planReviewScheduleBE.IsFutureCycle);
                sqlParameters[13] = new SqlParameter("@SCHEDULE_AFTER_DT", planReviewScheduleBE.ScheduleAfterDate);
                sqlParameters[14] = new SqlParameter("@IS_RESCHEDULE_IND", planReviewScheduleBE.IsReschedule);
                sqlParameters[15] = new SqlParameter("@GATE_DT", planReviewScheduleBE.GateDate);
                sqlParameters[16] = new SqlParameter("@IS_CURRENT_CYCLE_IND", planReviewScheduleBE.IsReschedule);
                sqlParameters[17] = new SqlParameter("@REREVIEW_HOURS_NBR", planReviewScheduleBE.ReReviewHours);
                sqlParameters[18] = new SqlParameter("@PROPOSED_HOURS_NBR", planReviewScheduleBE.ProposedHours);
                sqlParameters[19] = new SqlParameter("@PROPOSED_PLAN_REVIEWER_ID", planReviewScheduleBE.ProposedPlanReviewerId);

                sqlParameters[20] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[20].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_plan_review_schedule_v2", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_plan_review_schedule", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public PlanReviewScheduleBEOLD GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PlanReviewScheduleBEOLD planReviewScheduleBE = new PlanReviewScheduleBEOLD();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                planReviewScheduleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return planReviewScheduleBE;
        }

        public List<string> GetByAccelaWorkFlowTaskUpdate(string srcSystemValTxt, string businessRefList, DateTime? accelaEndDate)
        {


            List<string> planReviewScheduleIDList = new List<string>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@srcSystemValTxt", srcSystemValTxt);
                sqlParameters[1] = new SqlParameter("@businessRefList", businessRefList);
                sqlParameters[2] = new SqlParameter("@accelaEndDate", accelaEndDate);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_by_accela_workflow_tasks", base.ConnectionString, ref sqlParameters);




                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    planReviewScheduleIDList = dataSet.Tables[0].AsEnumerable()
                                                 .Select(r => r.Field<int>(0).ToString())
                                                 .ToList();

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleIDList;



        }

        public List<PlanReviewScheduleBEOLD> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewScheduleBEOLD> planReviewScheduleBEList = new List<PlanReviewScheduleBEOLD>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@project_id", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_list_byprojectid_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewScheduleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleBEList;

        }

        public int Update(PlanReviewScheduleBEOLD planReviewScheduleBE)
        {
            int rows;
            _planReviewScheduleBE = planReviewScheduleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[22];

                sqlParameters[0] = new SqlParameter("@PLAN_REVIEW_SCHEDULE_ID", planReviewScheduleBE.PlanReviewScheduleId);
                sqlParameters[1] = new SqlParameter("@PROJECT_ID", planReviewScheduleBE.ProjectId);
                sqlParameters[2] = new SqlParameter("@BUSINESS_REF_ID", planReviewScheduleBE.BusinessRefId);
                sqlParameters[3] = new SqlParameter("@START_DT", planReviewScheduleBE.StartDt);
                sqlParameters[4] = new SqlParameter("@END_DT", planReviewScheduleBE.EndDt);
                sqlParameters[5] = new SqlParameter("@POOL_REQUEST_IND", planReviewScheduleBE.PoolRequestInd);
                sqlParameters[6] = new SqlParameter("@FIFO_REQUEST_IND", planReviewScheduleBE.FifoRequestInd);
                sqlParameters[7] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", planReviewScheduleBE.ApptResponseStatusRefId);
                sqlParameters[8] = new SqlParameter("@UPDATED_DTTM", planReviewScheduleBE.UpdatedDate);
                sqlParameters[9] = new SqlParameter("@WKR_ID_TXT", planReviewScheduleBE.UserId);
                sqlParameters[10] = new SqlParameter("@PLAN_REVIEW_PROJECT_DETAILS_ID", planReviewScheduleBE.PlanReviewProjectDetailsId);
                sqlParameters[11] = new SqlParameter("@CYCLE_NBR", planReviewScheduleBE.Cycle);
                sqlParameters[12] = new SqlParameter("@PROD_DT", planReviewScheduleBE.ProdDate);
                sqlParameters[13] = new SqlParameter("@IS_FUTURE_CYCLE_IND", planReviewScheduleBE.IsFutureCycle);
                sqlParameters[14] = new SqlParameter("@SCHEDULE_AFTER_DT", planReviewScheduleBE.ScheduleAfterDate);
                sqlParameters[15] = new SqlParameter("@IS_RESCHEDULE_IND", planReviewScheduleBE.IsReschedule);
                sqlParameters[16] = new SqlParameter("@GATE_DT", planReviewScheduleBE.GateDate);
                sqlParameters[17] = new SqlParameter("@IS_CURRENT_CYCLE_IND", planReviewScheduleBE.IsReschedule);
                sqlParameters[18] = new SqlParameter("@REREVIEW_HOURS_NBR", planReviewScheduleBE.ReReviewHours);
                sqlParameters[19] = new SqlParameter("@PROPOSED_HOURS_NBR", planReviewScheduleBE.ProposedHours);
                sqlParameters[20] = new SqlParameter("@PROPOSED_PLAN_REVIEWER_ID", planReviewScheduleBE.ProposedPlanReviewerId);

                sqlParameters[21] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[21].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_review_schedule_v2", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }


        public int UpdatePlanReviewStatus(string planReviewScheduleIDList)
        {
            int rows;



            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@projectIDList", planReviewScheduleIDList);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_review_schedule_by_projectidList", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdatePlanReviewCycleFlag(string planReviewScheduleIdList, bool isCurrentFlag)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@planReviewScheduleIds", planReviewScheduleIdList);
                sqlParameters[1] = new SqlParameter("@isCurrentFlag", isCurrentFlag);
                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_review_schedule_flags_by_ids", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        /// <summary>
        /// Get the top 1 project schedule for a project id
        /// This only returns the top 1, does not return a list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectListBE GetProjectScheduleAppt(int id)
        {
            _id = id;

            DataSet dataSet;
            ProjectListBE projectListBE = new ProjectListBE();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_all_appointments_byprojectid", base.ConnectionString, ref sqlParameters);



            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectListBE = this.ConvertDataRowToProjectListBE(dataSet.Tables[0].Rows[0]);
            }


            return projectListBE;

        }

        /// <summary>
        /// Get the List of project schedule appts by AION project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ProjectListBE> GetProjectList(int id)
        {
            _id = id;

            DataSet dataSet;
            List<ProjectListBE> projectListBE = new List<ProjectListBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_all_appointments_byprojectid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                projectListBE.Add(this.ConvertDataRowToProjectListBE(dataRow));
            }

            return projectListBE;

        }

        /// <summary>
        /// Used on Customer Projects Dashboard - gets top 1 for acceptance deadline by start dt asc
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ProjectListBE GetAcceptanceDeadlineStartDtByProjectId(int id)
        {
            _id = id;

            DataSet dataSet;
            ProjectListBE projectListBE = new ProjectListBE();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_start_dt_byprojectid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectListBE = this.ConvertDataRowToProjectListBE(dataSet.Tables[0].Rows[0]);
            }

            return projectListBE;

        }
        public List<int> CancelSchedulePlanReview()
        {

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<int> projectIdlist = new List<int>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                //sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_schedule_plan_review", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    int projectId = TryToParse<int>(dataRow["projectId"]);
                    projectIdlist.Add(projectId);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return projectIdlist;
        }

        public List<int> CancelScheduledExpressPlanReview()
        {
            DataSet dataSet;
            List<int> cancelledAppointmentIds = new List<int>();

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_express_meeting", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    cancelledAppointmentIds.Add(TryToParse<int>(dataRow["EXPRESS_MEETING_APPT_ID"]));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return cancelledAppointmentIds;
        }

        public int CancelPlanReviewScheduleByCycle(int id, int cycle)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@projectID", id);
                sqlParameters[1] = new SqlParameter("@cycle", cycle);

                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_cancel_plan_review_schedule_by_projectid", base.ConnectionString, ref sqlParameters);

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

        private PlanReviewScheduleBEOLD ConvertDataRowToBE(DataRow dataRow)
        {
            PlanReviewScheduleBEOLD planReviewScheduleBE = new PlanReviewScheduleBEOLD();

            planReviewScheduleBE.PlanReviewScheduleId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]);
            planReviewScheduleBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            planReviewScheduleBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            planReviewScheduleBE.StartDt = TryToParse<DateTime?>(dataRow["START_DT"]);
            planReviewScheduleBE.EndDt = TryToParse<DateTime?>(dataRow["END_DT"]);
            planReviewScheduleBE.PoolRequestInd = TryToParse<bool?>(dataRow["POOL_REQUEST_IND"]);
            planReviewScheduleBE.FifoRequestInd = TryToParse<bool?>(dataRow["FIFO_REQUEST_IND"]);
            planReviewScheduleBE.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            planReviewScheduleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewScheduleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewScheduleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewScheduleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            planReviewScheduleBE.PlanReviewProjectDetailsId = TryToParse<int?>(dataRow["PLAN_REVIEW_PROJECT_DETAILS_ID"]);
            planReviewScheduleBE.Cycle = TryToParse<int>(dataRow["CYCLE_NBR"]);
            planReviewScheduleBE.ProdDate = TryToParse<DateTime?>(dataRow["PLANS_READY_ON_DT"]);
            planReviewScheduleBE.RequestExpressNextCycle = TryToParse<bool>(dataRow["REQUEST_EXPRESS_NEXT_CYCLE_IND"]);
            planReviewScheduleBE.IsFutureCycle = TryToParse<bool>(dataRow["IS_FUTURE_CYCLE_IND"]);
            planReviewScheduleBE.ScheduleAfterDate = TryToParse<DateTime?>(dataRow["SCHEDULE_AFTER_DT"]);
            planReviewScheduleBE.IsReschedule = TryToParse<bool>(dataRow["IS_RESCHEDULE_IND"]);
            planReviewScheduleBE.GateDate = TryToParse<DateTime?>(dataRow["GATE_DT"]);
            planReviewScheduleBE.IsCurrentCycle = TryToParse<bool>(dataRow["IS_CURRENT_CYCLE_IND"]);
            planReviewScheduleBE.ReReviewHours = TryToParse<decimal?>(dataRow["REREVIEW_HOURS_NBR"]);
            planReviewScheduleBE.ProposedHours = TryToParse<decimal?>(dataRow["PROPOSED_HOURS_NBR"]);
            planReviewScheduleBE.ProposedPlanReviewerId = TryToParse<int?>(dataRow["PROPOSED_PLAN_REVIEWER_ID"]);
            return planReviewScheduleBE;

        }


        private ProjectListBE ConvertDataRowToProjectListBE(DataRow dataRow)
        {
            ProjectListBE projectListBE = new ProjectListBE();


            projectListBE.ProjectId = TryToParse<int>(dataRow["PROJECT_ID"]);
            projectListBE.ProjectName = TryToParse<string>(dataRow["PROJECT_NM"]);
            projectListBE.ProjectType = TryToParse<int>(dataRow["PROJECT_TYP_REF_ID"]);
            projectListBE.ProjectStatus = TryToParse<int>(dataRow["PROJECT_STATUS_REF_ID"]);
            projectListBE.TentativeStartDate = TryToParse<DateTime?>(dataRow["START_DT"]);
            projectListBE.AcceptanceDeadLine = TryToParse<DateTime?>(dataRow["ACCEPTANCE_DEADLINE"]);
            return projectListBE;

        }

        #endregion

    }

    #endregion

}