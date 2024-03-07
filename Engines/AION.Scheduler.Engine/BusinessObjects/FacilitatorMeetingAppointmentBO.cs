#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using AION.Scheduler.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{

    #region BusinessObject - FacilitatorMeetingAppointmentBO

    public class FacilitatorMeetingAppointmentBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, Search };

        private string _errorMsg;

        private FacilitatorMeetingAppointmentBE _facilitatorMeetingAppointmentBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(FacilitatorMeetingAppointmentBE facilitatorMeetingAppointmentBE)
        {
            int id;
            _facilitatorMeetingAppointmentBE = facilitatorMeetingAppointmentBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[14];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", facilitatorMeetingAppointmentBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@MEETING_ROOM_REF_ID", facilitatorMeetingAppointmentBE.MeetingRoomRefId);
                sqlParameters[2] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", facilitatorMeetingAppointmentBE.ApptResponseStatusRefId);
                sqlParameters[3] = new SqlParameter("@FROM_DT", facilitatorMeetingAppointmentBE.FromDt);
                sqlParameters[4] = new SqlParameter("@TO_DT", facilitatorMeetingAppointmentBE.ToDt);
                sqlParameters[5] = new SqlParameter("@VIRTUAL_MEETING_IND", facilitatorMeetingAppointmentBE.VirtualMeetingInd);
                sqlParameters[6] = new SqlParameter("@MEETING_TYP_REF_ID", facilitatorMeetingAppointmentBE.MeetingTypRefId);
                sqlParameters[7] = new SqlParameter("@CANCEL_AFTER_DT", facilitatorMeetingAppointmentBE.CancelAfterDt);
                sqlParameters[8] = new SqlParameter("@WKR_ID_TXT", facilitatorMeetingAppointmentBE.UserId);
                sqlParameters[9] = new SqlParameter("@PROPOSED_1_DT", facilitatorMeetingAppointmentBE.RequestedDate1);
                sqlParameters[10] = new SqlParameter("@PROPOSED_2_DT", facilitatorMeetingAppointmentBE.RequestedDate2);
                sqlParameters[11] = new SqlParameter("@PROPOSED_3_DT", facilitatorMeetingAppointmentBE.RequestedDate3);
                sqlParameters[12] = new SqlParameter("@EXTERNAL_ATTENDEES_CNT", facilitatorMeetingAppointmentBE.ExternalAttendeesCnt);

                sqlParameters[13] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[13].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_facilitator_meeting_appointment_v4", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_facilitator_meeting_appointment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public FacilitatorMeetingAppointmentBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            FacilitatorMeetingAppointmentBE facilitatorMeetingAppointmentBE = new FacilitatorMeetingAppointmentBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                facilitatorMeetingAppointmentBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return facilitatorMeetingAppointmentBE;
        }

        public List<FacilitatorMeetingAppointmentBE> GetByProjectId(int projectId)
        {
            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            List<FacilitatorMeetingAppointmentBE> facilitatorMeetingAppointmentBEList = new List<FacilitatorMeetingAppointmentBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectId", projectId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_by_project_id", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingAppointmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingAppointmentBEList;
        }

        public List<FacilitatorMeetingAppointmentBE> GetByProjectIds(string projectIds)
        {
            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            List<FacilitatorMeetingAppointmentBE> facilitatorMeetingAppointmentBEList = new List<FacilitatorMeetingAppointmentBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectIds", projectIds);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_by_project_ids", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingAppointmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingAppointmentBEList;
        }

        /// <summary>
        /// Get PMA list of meeting appointments by project id
        /// can switch on scheduled or all
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="scheduled"></param>
        /// <returns></returns>
        public List<AION.Scheduler.Engine.BusinessEntities.MeetingBE> GetInternalMeetingsListByProjectID(int projectid)
        {

            DataSet dataSet;
            List<MeetingBE> facilitatorMeetingAppointmentBEList = new List<MeetingBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", projectid);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_fma_internalmeetings_byprojectid_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingAppointmentBEList.Add(this.ConvertInternalMeetingAppointmentDataRowToMeetingBE(dataRow));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingAppointmentBEList;

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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<FacilitatorMeetingAppointmentBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<FacilitatorMeetingAppointmentBE> facilitatorMeetingAppointmentBEList = new List<FacilitatorMeetingAppointmentBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingAppointmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingAppointmentBEList;

        }

        public List<FacilitatorMeetingAppointmentBE> GetListByProjectAndMeetingType(string projectId, string meetingTypeDesc)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<FacilitatorMeetingAppointmentBE> facilitatorMeetingAppointmentBEList = new List<FacilitatorMeetingAppointmentBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@SRC_SYSTEM_VAL_TEXT", projectId);
                sqlParameters[1] = new SqlParameter("@MEETING_TYP_DESC", meetingTypeDesc);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_list_by_project_and_meeting_type", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingAppointmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingAppointmentBEList;

        }

        public int Update(FacilitatorMeetingAppointmentBE facilitatorMeetingAppointmentBE)
        {
            int rows;
            _facilitatorMeetingAppointmentBE = facilitatorMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[14];

                sqlParameters[0] = new SqlParameter("@FACILITATOR_MEETING_APPT_IDENTIFIER", facilitatorMeetingAppointmentBE.FacilitatorMeetingApptId);
                sqlParameters[1] = new SqlParameter("@PROJECT_ID", facilitatorMeetingAppointmentBE.ProjectId);
                sqlParameters[2] = new SqlParameter("@MEETING_ROOM_REF_ID", facilitatorMeetingAppointmentBE.MeetingRoomRefId);
                sqlParameters[3] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", facilitatorMeetingAppointmentBE.ApptResponseStatusRefId);
                sqlParameters[4] = new SqlParameter("@APPT_CANCELLATION_REF_ID", facilitatorMeetingAppointmentBE.ApptCancellationRefId);
                sqlParameters[5] = new SqlParameter("@FROM_DT", facilitatorMeetingAppointmentBE.FromDt);
                sqlParameters[6] = new SqlParameter("@TO_DT", facilitatorMeetingAppointmentBE.ToDt);
                sqlParameters[7] = new SqlParameter("@UPDATED_DTTM", facilitatorMeetingAppointmentBE.UpdatedDate);
                sqlParameters[8] = new SqlParameter("@VIRTUAL_MEETING_IND", facilitatorMeetingAppointmentBE.VirtualMeetingInd);
                sqlParameters[9] = new SqlParameter("@MEETING_TYP_REF_ID", facilitatorMeetingAppointmentBE.MeetingTypRefId);

                sqlParameters[10] = new SqlParameter("@WKR_ID_TXT", facilitatorMeetingAppointmentBE.UserId);
                sqlParameters[11] = new SqlParameter("@CANCEL_AFTER_DT", facilitatorMeetingAppointmentBE.CancelAfterDt);
                sqlParameters[12] = new SqlParameter("@EXTERNAL_ATTENDEES_CNT", facilitatorMeetingAppointmentBE.ExternalAttendeesCnt);

                sqlParameters[13] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[13].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_facilitator_meeting_appointment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdateMeetingDateRequest(FacilitatorMeetingAppointmentBE facilitatorMeetingAppointmentBE)
        {
            int rows;
            _facilitatorMeetingAppointmentBE = facilitatorMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@FACILITATOR_MEETING_APPT_IDENTIFIER", facilitatorMeetingAppointmentBE.FacilitatorMeetingApptId);
                sqlParameters[1] = new SqlParameter("@PROPOSED_1_DT", facilitatorMeetingAppointmentBE.RequestedDate1);
                sqlParameters[2] = new SqlParameter("@PROPOSED_2_DT", facilitatorMeetingAppointmentBE.RequestedDate2);
                sqlParameters[3] = new SqlParameter("@PROPOSED_3_DT", facilitatorMeetingAppointmentBE.RequestedDate3);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_facilitator_meeting_proposed_dates", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdateMeetingStatus(FacilitatorMeetingAppointmentBE facilitatorMeetingAppointmentBE)
        {
            int rows;
            _facilitatorMeetingAppointmentBE = facilitatorMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@FACILITATOR_MEETING_APPT_IDENTIFIER", facilitatorMeetingAppointmentBE.FacilitatorMeetingApptId);
                sqlParameters[1] = new SqlParameter("@APPT_RESPONSE_STATUS", facilitatorMeetingAppointmentBE.ApptResponseStatusRefId);
                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_facilitator_meeting_response_status", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<FacilitatorMeetingAppointmentBE> Search(DateTime? startDate, DateTime? endDate, string fmaStatusIds)
        {
            if (!this.Validate(ActionType.Search))
                throw (new Exception(_errorMsg));

            List<FacilitatorMeetingAppointmentBE> facilitatorMeetingAppointmentBEList = new List<FacilitatorMeetingAppointmentBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@startdate", startDate);
                sqlParameters[1] = new SqlParameter("@enddate", endDate);
                sqlParameters[2] = new SqlParameter("@fmaStatusIds", fmaStatusIds);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appointment_get_list_by_status_ids", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingAppointmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingAppointmentBEList;
        }

        public int CancelMeetingSavedUserSchedules()
        {
            int rows;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[0].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_cancel_meeting_saved_user_schedules_v2", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<int> CancelFacilitatorMeetingAppointment()
        {
            DataSet dataSet;
            List<int> cancelledAppointmentIds = new List<int>();

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_facilitator_meeting_appointment_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    cancelledAppointmentIds.Add(TryToParse<int>(dataRow["FACILITATOR_MEETING_APPT_IDENTIFIER"]));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return cancelledAppointmentIds;
        }

        public int CancelFMAByProjectId(int id)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@projectID", id);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_cancel_facilitator_meeting_appointment_by_projectid", base.ConnectionString, ref sqlParameters);

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

        private FacilitatorMeetingAppointmentBE ConvertDataRowToBE(DataRow dataRow)
        {
            FacilitatorMeetingAppointmentBE facilitatorMeetingAppointmentBE = new FacilitatorMeetingAppointmentBE();

            facilitatorMeetingAppointmentBE.FacilitatorMeetingApptId = TryToParse<int?>(dataRow["FACILITATOR_MEETING_APPT_IDENTIFIER"]);
            facilitatorMeetingAppointmentBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            facilitatorMeetingAppointmentBE.MeetingRoomRefId = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            facilitatorMeetingAppointmentBE.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            facilitatorMeetingAppointmentBE.FromDt = TryToParse<DateTime?>(dataRow["FROM_DT"]);
            facilitatorMeetingAppointmentBE.ToDt = TryToParse<DateTime?>(dataRow["TO_DT"]);
            facilitatorMeetingAppointmentBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            facilitatorMeetingAppointmentBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            facilitatorMeetingAppointmentBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            facilitatorMeetingAppointmentBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            facilitatorMeetingAppointmentBE.VirtualMeetingInd = TryToParse<bool?>(dataRow["VIRTUAL_MEETING_IND"]) == null ? false : TryToParse<bool?>(dataRow["VIRTUAL_MEETING_IND"]);
            facilitatorMeetingAppointmentBE.MeetingTypRefId = TryToParse<int?>(dataRow["MEETING_TYP_REF_ID"]);
            facilitatorMeetingAppointmentBE.UserId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            facilitatorMeetingAppointmentBE.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]);
            facilitatorMeetingAppointmentBE.CancelAfterDt = TryToParse<DateTime?>(dataRow["CANCEL_AFTER_DT"]);
            facilitatorMeetingAppointmentBE.RequestedDate1 = TryToParse<DateTime?>(dataRow["PROPOSED_1_DT"]);
            facilitatorMeetingAppointmentBE.RequestedDate2 = TryToParse<DateTime?>(dataRow["PROPOSED_2_DT"]);
            facilitatorMeetingAppointmentBE.RequestedDate3 = TryToParse<DateTime?>(dataRow["PROPOSED_3_DT"]);
            facilitatorMeetingAppointmentBE.ExternalAttendeesCnt = TryToParse<int?>(dataRow["EXTERNAL_ATTENDEES_CNT"]);

            return facilitatorMeetingAppointmentBE;

        }

        private MeetingBE ConvertInternalMeetingAppointmentDataRowToMeetingBE(DataRow dataRow)
        {
            MeetingBE be = new MeetingBE();

            be.MeetingDate = TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).HasValue ? TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).Value : DateTime.Now;
            be.MeetingTime = TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).HasValue ? TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).Value : DateTime.Now;
            be.MeetingType = TryToParse<int?>(dataRow["MEETING_TYPE"]).HasValue ? TryToParse<int?>(dataRow["MEETING_TYPE"]).Value : 0;
            be.ProjectType = TryToParse<int?>(dataRow["PROJECT_TYPE_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_TYPE_ID"]).Value : 0;
            be.ProjectExternalRefID = TryToParse<string>(dataRow["PROJECT_EXTREF_ID"]);
            be.ProjectName = TryToParse<string>(dataRow["PROJECT_NM"]);
            be.ProjectStatus = TryToParse<int?>(dataRow["PROJECT_STATUS"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_STATUS"]).Value : -1;
            be.AppendixAgendaDue = TryToParse<DateTime?>(dataRow["AGENDA_DUE"]);
            be.MinutesDue = TryToParse<DateTime?>(dataRow["MINUTES_DUE"]).HasValue ? TryToParse<DateTime?>(dataRow["MINUTES_DUE"]).Value : DateTime.Now;
            be.ProjectID = TryToParse<int?>(dataRow["PROJECT_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_ID"]).Value : 0;
            be.UserID = TryToParse<int?>(dataRow["USER_ID"]).HasValue ? TryToParse<int?>(dataRow["USER_ID"]).Value : 0;
            be.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            be.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            be.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            be.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            be.MeetingRoomRefId = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            be.ProjectScheduleId = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);
            be.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            be.DateTimeFrom = TryToParse<DateTime?>(dataRow["FROM_DT"]);
            be.DateTimeTo = TryToParse<DateTime?>(dataRow["TO_DT"]);
            be.AppointmentId = TryToParse<int>(dataRow["APPT_ID"]);
            be.Attendees = TryToParse<string>(dataRow["ATTENDEES"]);
            return be;

        }

        #endregion
    }

    #endregion

}