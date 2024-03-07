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

    #region BusinessObject - PlanReviewScheduleBO

    public class PlanReviewScheduleBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private PlanReviewScheduleBE _planReviewScheduleBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(PlanReviewScheduleBE planReviewScheduleBE)
        {
            int id;
            _planReviewScheduleBE = planReviewScheduleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[13];

                sqlParameters[0] = new SqlParameter("@PROJECT_CYCLE_ID", planReviewScheduleBE.ProjectCycleId);
                sqlParameters[1] = new SqlParameter("@PROJECT_SCHEDULE_TYP_DESC", planReviewScheduleBE.ProjectScheduleTypDesc);
                sqlParameters[2] = new SqlParameter("@IS_RESCHEDULE_IND", planReviewScheduleBE.IsRescheduleInd);
                sqlParameters[3] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", planReviewScheduleBE.ApptResponseStatusRefId);
                sqlParameters[4] = new SqlParameter("@APPT_CANCELLATION_REF_ID", planReviewScheduleBE.ApptCancellationRefId);
                sqlParameters[5] = new SqlParameter("@VIRTUAL_MEETING_IND", planReviewScheduleBE.VirtualMeetingInd);
                sqlParameters[6] = new SqlParameter("@PROPOSED_1_DT", planReviewScheduleBE.Proposed1Dt);
                sqlParameters[7] = new SqlParameter("@PROPOSED_2_DT", planReviewScheduleBE.Proposed2Dt);
                sqlParameters[8] = new SqlParameter("@PROPOSED_3_DT", planReviewScheduleBE.Proposed3Dt);
                sqlParameters[9] = new SqlParameter("@CANCEL_AFTER_DT", planReviewScheduleBE.CancelAfterDt);
                sqlParameters[10] = new SqlParameter("@MEETING_ROOM_REF_ID", planReviewScheduleBE.MeetingRoomRefId);

                sqlParameters[11] = new SqlParameter("@WKR_ID_TXT", planReviewScheduleBE.UserId);

                sqlParameters[12] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[12].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_plan_review_schedule", base.ConnectionString, ref sqlParameters);

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

        public PlanReviewScheduleBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PlanReviewScheduleBE planReviewScheduleBE = new PlanReviewScheduleBE();
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

        public List<PlanReviewScheduleBE> GetByProjectCycleId(int projectCycleId)
        {

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            List<PlanReviewScheduleBE> planReviewScheduleBEs = new List<PlanReviewScheduleBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectCycleId", projectCycleId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_by_project_cycle_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                planReviewScheduleBEs.Add(this.ConvertDataRowToBE(dataRow));
            }

            return planReviewScheduleBEs;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<PlanReviewScheduleBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewScheduleBE> planReviewScheduleBEList = new List<PlanReviewScheduleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_list", base.ConnectionString, ref sqlParameters);

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

        public List<PlanReviewScheduleBE> GetListByDates(DateTime startDate, DateTime endDate, string projectScheduleTypDesc)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<PlanReviewScheduleBE> planReviewScheduleBEList = new List<PlanReviewScheduleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@START_DT", startDate);
                sqlParameters[1] = new SqlParameter("@END_DT", endDate);
                sqlParameters[2] = new SqlParameter("@PROJECT_SCHEDULE_TYP_DESC", projectScheduleTypDesc);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_plan_review_schedule_get_list_by_dates", base.ConnectionString, ref sqlParameters);

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

        public int Update(PlanReviewScheduleBE planReviewScheduleBE)
        {
            int rows;
            _planReviewScheduleBE = planReviewScheduleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[15];

                sqlParameters[0] = new SqlParameter("@PLAN_REVIEW_SCHEDULE_ID", planReviewScheduleBE.PlanReviewScheduleId);
                sqlParameters[1] = new SqlParameter("@PROJECT_CYCLE_ID", planReviewScheduleBE.ProjectCycleId);
                sqlParameters[2] = new SqlParameter("@PROJECT_SCHEDULE_TYP_DESC", planReviewScheduleBE.ProjectScheduleTypDesc);
                sqlParameters[3] = new SqlParameter("@IS_RESCHEDULE_IND", planReviewScheduleBE.IsRescheduleInd);
                sqlParameters[4] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", planReviewScheduleBE.ApptResponseStatusRefId);
                sqlParameters[5] = new SqlParameter("@APPT_CANCELLATION_REF_ID", planReviewScheduleBE.ApptCancellationRefId);
                sqlParameters[6] = new SqlParameter("@UPDATED_DTTM", planReviewScheduleBE.UpdatedDate);
                sqlParameters[7] = new SqlParameter("@VIRTUAL_MEETING_IND", planReviewScheduleBE.VirtualMeetingInd);
                sqlParameters[8] = new SqlParameter("@PROPOSED_1_DT", planReviewScheduleBE.Proposed1Dt == DateTime.MinValue ? null : planReviewScheduleBE.Proposed1Dt);
                sqlParameters[9] = new SqlParameter("@PROPOSED_2_DT", planReviewScheduleBE.Proposed2Dt == DateTime.MinValue ? null : planReviewScheduleBE.Proposed2Dt);
                sqlParameters[10] = new SqlParameter("@PROPOSED_3_DT", planReviewScheduleBE.Proposed3Dt==DateTime.MinValue?null: planReviewScheduleBE.Proposed3Dt);
                sqlParameters[11] = new SqlParameter("@CANCEL_AFTER_DT", planReviewScheduleBE.CancelAfterDt);
                sqlParameters[12] = new SqlParameter("@MEETING_ROOM_REF_ID", planReviewScheduleBE.MeetingRoomRefId);

                sqlParameters[13] = new SqlParameter("@WKR_ID_TXT", planReviewScheduleBE.UserId);

                sqlParameters[14] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[14].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_plan_review_schedule", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
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

        /// <summary>
        /// sets plan review schedule rows of type 'PR' to cancelled status
        /// used by abort processing
        /// </summary>
        /// <returns>list of plan_review_schedule_id that were affected</returns>
        public List<int> CancelPlanReviewByProjectId(int projectId, int wrkrid)
        {
            DataSet dataSet;
            List<int> planReviewScheduleIds = new List<int>();

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectId);
                sqlParameters[1] = new SqlParameter("@USER_ID", wrkrid);
                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_PR_by_project_id", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewScheduleIds.Add(TryToParse<int>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleIds;
        }

        /// <summary>
        /// sets plan review schedule rows of type 'EMA' to cancelled status
        /// used by abort processing
        /// </summary>
        /// <returns>list of plan_review_schedule_id that were affected</returns>
        public List<int> CancelEMAByProjectId(int projectId, int wrkrid)
        {
            DataSet dataSet;
            List<int> planReviewScheduleIds = new List<int>();

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectId);
                sqlParameters[1] = new SqlParameter("@USER_ID", wrkrid);

                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_EMA_by_project_id", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    planReviewScheduleIds.Add(TryToParse<int>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return planReviewScheduleIds;
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

        private PlanReviewScheduleBE ConvertDataRowToBE(DataRow dataRow)
        {
            PlanReviewScheduleBE planReviewScheduleBE = new PlanReviewScheduleBE();

            planReviewScheduleBE.PlanReviewScheduleId = TryToParse<int?>(dataRow["PLAN_REVIEW_SCHEDULE_ID"]);
            planReviewScheduleBE.ProjectCycleId = TryToParse<int?>(dataRow["PROJECT_CYCLE_ID"]);
            planReviewScheduleBE.ProjectScheduleTypDesc = TryToParse<string>(dataRow["PROJECT_SCHEDULE_TYP_DESC"]);
            planReviewScheduleBE.IsRescheduleInd = TryToParse<bool?>(dataRow["IS_RESCHEDULE_IND"]);
            planReviewScheduleBE.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            planReviewScheduleBE.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]);
            planReviewScheduleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            planReviewScheduleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            planReviewScheduleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            planReviewScheduleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            planReviewScheduleBE.VirtualMeetingInd = TryToParse<bool?>(dataRow["VIRTUAL_MEETING_IND"]);
            planReviewScheduleBE.Proposed1Dt = TryToParse<DateTime?>(dataRow["PROPOSED_1_DT"]);
            planReviewScheduleBE.Proposed2Dt = TryToParse<DateTime?>(dataRow["PROPOSED_2_DT"]);
            planReviewScheduleBE.Proposed3Dt = TryToParse<DateTime?>(dataRow["PROPOSED_3_DT"]);
            planReviewScheduleBE.CancelAfterDt = TryToParse<DateTime?>(dataRow["CANCEL_AFTER_DT"]);
            planReviewScheduleBE.MeetingRoomRefId = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);

            return planReviewScheduleBE;

        }

        #endregion

    }

    #endregion

}