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

    #region BusinessObject - PreliminaryMeetingAppointmentBO

    public class PreliminaryMeetingAppointmentBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, Search };

        private string _errorMsg;

        private PreliminaryMeetingAppointmentBE _preliminaryMeetingAppointmentBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE)
        {
            int id;
            _preliminaryMeetingAppointmentBE = preliminaryMeetingAppointmentBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@FROM_DT", preliminaryMeetingAppointmentBE.FromDT);
                sqlParameters[1] = new SqlParameter("@TO_DT", preliminaryMeetingAppointmentBE.ToDT);
                sqlParameters[2] = new SqlParameter("@MEETING_ROOM_REF_ID", preliminaryMeetingAppointmentBE.MeetingRoomRefID);
                sqlParameters[3] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", preliminaryMeetingAppointmentBE.ApptResponseStatusRefId);
                sqlParameters[4] = new SqlParameter("@PROJECT_ID", preliminaryMeetingAppointmentBE.ProjectID);
                sqlParameters[5] = new SqlParameter("@APPENDIX_AGENDA_DUE_DT", preliminaryMeetingAppointmentBE.AppendixAgendaDueDt);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", preliminaryMeetingAppointmentBE.UserId);
                sqlParameters[7] = new SqlParameter("@CANCEL_AFTER_DT", preliminaryMeetingAppointmentBE.CancelAfterDt);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_preliminary_meeting_appointment_v2", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_preliminary_meeting_appointment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public PreliminaryMeetingAppointmentBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE = new PreliminaryMeetingAppointmentBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_preliminary_meeting_appointment_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                preliminaryMeetingAppointmentBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return preliminaryMeetingAppointmentBE;
        }
        public PreliminaryMeetingAppointmentBE GetByProjectId(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE = new PreliminaryMeetingAppointmentBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_preliminary_meeting_appointment_get_by_projectid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                preliminaryMeetingAppointmentBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return preliminaryMeetingAppointmentBE;
        }
        public List<PreliminaryMeetingAppointmentBE> Search(DateTime? startDate, DateTime? endDate, int? apptResponseStatusRefId)
        {

            if (!this.Validate(ActionType.Search))
                throw (new Exception(_errorMsg));

            List<PreliminaryMeetingAppointmentBE> preliminaryMeetingAppointmentBEList = new List<PreliminaryMeetingAppointmentBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@startdate", startDate);
                sqlParameters[1] = new SqlParameter("@enddate", endDate);
                sqlParameters[2] = new SqlParameter("@apptresponsestatusrefid", apptResponseStatusRefId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_preliminary_meeting_appointment_get_list_search", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    preliminaryMeetingAppointmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return preliminaryMeetingAppointmentBEList;
        }

        public List<CustmrMeetingsBE> GetMeetingsListByUserID(int userId)
        {

            DataSet dataSet;
            List<CustmrMeetingsBE> preliminaryMeetingAppointmentBEList = new List<CustmrMeetingsBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", userId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_get_meeting_appointment_list_by_user_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    preliminaryMeetingAppointmentBEList.Add(this.ConvertMeetingAppointmentDataRowToBE(dataRow));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return preliminaryMeetingAppointmentBEList;

        }

        /// <summary>
        /// Gets all the scheduled meetings
        /// Used on Internal Meetings dashboard
        /// For all meetings, send 0 as wrkId, otherwise, send user id
        /// TODO: add daterange
        /// </summary>
        /// <param name="wrkId"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<InternalMeetingsBE> GetInternalMeetingsList(int? wrkId = null, DateTime? startdate = null, DateTime? enddate = null)
        {

            DataSet dataSet;
            List<InternalMeetingsBE> preliminaryMeetingAppointmentBEList = new List<InternalMeetingsBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", wrkId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_get_internal_meeting_appointment_list_for_facilitators_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    preliminaryMeetingAppointmentBEList.Add(this.ConvertInternalMeetingAppointmentDataRowToBE(dataRow));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return preliminaryMeetingAppointmentBEList;

        }

        /// <summary>
        /// Get PMA list of meeting appointments by project id
        /// can switch on scheduled or all
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="scheduled"></param>
        /// <returns></returns>
        public List<MeetingBE> GetInternalMeetingsListByProjectID(int projectid)
        {

            DataSet dataSet;
            List<MeetingBE> preliminaryMeetingAppointmentBEList = new List<MeetingBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectid", projectid);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_pma_internalmeetings_byprojectid_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    preliminaryMeetingAppointmentBEList.Add(this.ConvertInternalMeetingAppointmentDataRowToMeetingBE(dataRow));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return preliminaryMeetingAppointmentBEList;

        }

        public int CancelPMAByProjectId(int id)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@projectID", id);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_cancel_preliminary_meeting_appointment_by_projectid", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        private CustmrMeetingsBE ConvertMeetingAppointmentDataRowToBE(DataRow dataRow)
        {
            CustmrMeetingsBE be = new CustmrMeetingsBE();
            be.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPOINTMENT_RESPONSE_STATUS"]).HasValue ? TryToParse<int?>(dataRow["APPOINTMENT_RESPONSE_STATUS"]).Value : -1;
            be.MeetingDate = TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).HasValue ? TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).Value : DateTime.Now;
            be.MeetingTime = TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).HasValue ? TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).Value : DateTime.Now;
            be.MeetingType = TryToParse<int?>(dataRow["MEETING_TYPE"]).HasValue ? TryToParse<int?>(dataRow["MEETING_TYPE"]).Value : 0;
            be.ProjectType = TryToParse<int?>(dataRow["PROJECT_TYPE_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_TYPE_ID"]).Value : 0;
            be.ProjectExternalRefID = TryToParse<string>(dataRow["PROJECT_EXTREF_ID"]);
            be.ProjectName = TryToParse<string>(dataRow["PROJECT_NM"]);
            be.ProjectStatus = TryToParse<int?>(dataRow["PROJECT_STATUS"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_STATUS"]).Value : -1;
            be.AppendixAgendaDue = TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).HasValue ? TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).Value : DateTime.Now;
            be.MinutesDue = TryToParse<DateTime?>(dataRow["MINUTES_DUE"]).HasValue ? TryToParse<DateTime?>(dataRow["MINUTES_DUE"]).Value : DateTime.Now;
            be.ProjectID = TryToParse<int?>(dataRow["PROJECT_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_ID"]).Value : 0;
            be.UserID = TryToParse<int?>(dataRow["USER_ID"]).HasValue ? TryToParse<int?>(dataRow["USER_ID"]).Value : 0;
            be.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            be.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            be.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            be.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            be.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]);
            be.RecIdTxt = TryToParse<string>(dataRow["REC_ID_TXT"]);
            return be;

        }

        private InternalMeetingsBE ConvertInternalMeetingAppointmentDataRowToBE(DataRow dataRow)
        {
            InternalMeetingsBE be = new InternalMeetingsBE();
            be.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPOINTMENT_RESPONSE_STATUS"]).HasValue ? TryToParse<int?>(dataRow["APPOINTMENT_RESPONSE_STATUS"]).Value : -1;
            be.MeetingDate = TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).HasValue ? TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).Value : DateTime.Now;
            be.MeetingTime = TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).HasValue ? TryToParse<DateTime?>(dataRow["MEETING_DATE_TIME"]).Value : DateTime.Now;
            be.MeetingType = TryToParse<int?>(dataRow["MEETING_TYPE"]).HasValue ? TryToParse<int?>(dataRow["MEETING_TYPE"]).Value : 0;
            be.ProjectType = TryToParse<int?>(dataRow["PROJECT_TYPE_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_TYPE_ID"]).Value : 0;
            be.ProjectExternalRefID = TryToParse<string>(dataRow["PROJECT_EXTREF_ID"]);
            be.ProjectName = TryToParse<string>(dataRow["PROJECT_NM"]);
            be.ProjectStatus = TryToParse<int?>(dataRow["PROJECT_STATUS"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_STATUS"]).Value : -1;
            be.AppendixAgendaDue = TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).HasValue ? TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).Value : (DateTime?)null;
            be.MinutesDue = TryToParse<DateTime?>(dataRow["MINUTES_DUE"]).HasValue ? TryToParse<DateTime?>(dataRow["MINUTES_DUE"]).Value : DateTime.Now;
            be.ProjectID = TryToParse<int?>(dataRow["PROJECT_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_ID"]).Value : 0;
            be.UserID = TryToParse<int?>(dataRow["USER_ID"]).HasValue ? TryToParse<int?>(dataRow["USER_ID"]).Value : 0;
            be.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            be.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            be.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            be.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            be.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]);
            be.IsProjectRTAP = TryToParse<bool?>(dataRow["RTAP_IND"]);
            be.FacilitatorId = TryToParse<int?>(dataRow["ASSIGNED_FACILITATOR_ID"]);
            be.ProjectManagerId = TryToParse<int?>(dataRow["PROJECT_MANAGER_ID"]);
            be.TeamGradeTxt = TryToParse<string>(dataRow["TEAM_GRADE_TXT"]);
            be.BuildingCodeVersion = TryToParse<string>(dataRow["BUILD_CODE_VERSION_DESC"]);

            return be;

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
            be.AppendixAgendaDue = TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).HasValue ? TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).Value : DateTime.Now;
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
        public int Update(PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE)
        {
            int rows;
            _preliminaryMeetingAppointmentBE = preliminaryMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[13];

                sqlParameters[0] = new SqlParameter("@PRELIMINARY_MEETING_APPT_ID", preliminaryMeetingAppointmentBE.PreliminaryMeetingApptID);
                sqlParameters[1] = new SqlParameter("@FROM_DT", preliminaryMeetingAppointmentBE.FromDT);
                sqlParameters[2] = new SqlParameter("@TO_DT", preliminaryMeetingAppointmentBE.ToDT);
                sqlParameters[3] = new SqlParameter("@MEETING_ROOM_REF_ID", preliminaryMeetingAppointmentBE.MeetingRoomRefID);
                sqlParameters[4] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", preliminaryMeetingAppointmentBE.ApptResponseStatusRefId);
                sqlParameters[5] = new SqlParameter("@APPT_CANCELLATION_REF_ID", preliminaryMeetingAppointmentBE.ApptCancellationRefId);
                sqlParameters[6] = new SqlParameter("@UPDATED_DTTM", preliminaryMeetingAppointmentBE.UpdatedDate);
                sqlParameters[7] = new SqlParameter("@PROJECT_ID", preliminaryMeetingAppointmentBE.ProjectID);
                sqlParameters[8] = new SqlParameter("@APPENDIX_AGENDA_DUE_DT", preliminaryMeetingAppointmentBE.AppendixAgendaDueDt);

                sqlParameters[9] = new SqlParameter("@WKR_ID_TXT", preliminaryMeetingAppointmentBE.UserId);
                sqlParameters[10] = new SqlParameter("@CANCEL_AFTER_DT", preliminaryMeetingAppointmentBE.CancelAfterDt);
                sqlParameters[11] = new SqlParameter("@RESCHEDULE_IND", preliminaryMeetingAppointmentBE.IsReschedule);
                sqlParameters[12] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[12].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_preliminary_meeting_appointment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdatePrelimDateRequest(PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE)
        {
            int rows;
            _preliminaryMeetingAppointmentBE = preliminaryMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PRELIMINARY_MEETING_APPT_ID", preliminaryMeetingAppointmentBE.PreliminaryMeetingApptID);
                sqlParameters[1] = new SqlParameter("@PROPOSED_DT_1", preliminaryMeetingAppointmentBE.RequestedDate1);
                sqlParameters[2] = new SqlParameter("@PROPOSED_DT_2", preliminaryMeetingAppointmentBE.RequestedDate2);
                sqlParameters[3] = new SqlParameter("@PROPOSED_DT_3", preliminaryMeetingAppointmentBE.RequestedDate3);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_prelim_meeting_proposed_dates", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }
        /// <summary>
        /// Changes PMA status and changes PROJECT status if this appointment was REJECTED/ACCEPTED
        /// </summary>
        /// <param name="preliminaryMeetingAppointmentBE"></param>
        /// <returns></returns>
        public int UpdatePrelimStatus(PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE)
        {
            int rows;
            _preliminaryMeetingAppointmentBE = preliminaryMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@PRELIMINARY_MEETING_APPT_ID", preliminaryMeetingAppointmentBE.PreliminaryMeetingApptID);
                sqlParameters[1] = new SqlParameter("@APPT_RESPONSE_STATUS", preliminaryMeetingAppointmentBE.ApptResponseStatusEnumId);
                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_prelim_response_status", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdateMeetingAction(PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE)
        {
            int rows;
            _preliminaryMeetingAppointmentBE = preliminaryMeetingAppointmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", preliminaryMeetingAppointmentBE.ProjectID);
                sqlParameters[1] = new SqlParameter("@APPT_RESPONSE_STATUS", preliminaryMeetingAppointmentBE.ApptResponseStatusRefId);
                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_prelim_Meeting_Action", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<int> CancelPrelimMeeting()
        {
            DataSet dataSet;
            List<int> cancelledAppointmentIds = new List<int>();

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_prelim_meeting_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    cancelledAppointmentIds.Add(TryToParse<int>(dataRow["PRELIMINARY_MEETING_APPT_ID"]));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return cancelledAppointmentIds;
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
                case ActionType.Search:
                    return (_errorMsg == String.Empty);
                default:
                    break;
            }

            return true;

        }

        private PreliminaryMeetingAppointmentBE ConvertDataRowToBE(DataRow dataRow)
        {
            PreliminaryMeetingAppointmentBE preliminaryMeetingAppointmentBE = new PreliminaryMeetingAppointmentBE();

            preliminaryMeetingAppointmentBE.PreliminaryMeetingApptID = TryToParse<int?>(dataRow["PRELIMINARY_MEETING_APPT_ID"]);
            preliminaryMeetingAppointmentBE.FromDT = TryToParse<DateTime?>(dataRow["FROM_DT"]);
            preliminaryMeetingAppointmentBE.ToDT = TryToParse<DateTime?>(dataRow["TO_DT"]);
            preliminaryMeetingAppointmentBE.MeetingRoomRefID = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            preliminaryMeetingAppointmentBE.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            preliminaryMeetingAppointmentBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            preliminaryMeetingAppointmentBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            preliminaryMeetingAppointmentBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            preliminaryMeetingAppointmentBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            preliminaryMeetingAppointmentBE.ProjectID = TryToParse<int?>(dataRow["PROJECT_ID"]);
            preliminaryMeetingAppointmentBE.AppendixAgendaDueDt = TryToParse<DateTime?>(dataRow["APPENDIX_AGENDA_DUE_DT"]);
            preliminaryMeetingAppointmentBE.RequestedDate1 = TryToParse<DateTime?>(dataRow["PROPOSED_1_DT"]);
            preliminaryMeetingAppointmentBE.RequestedDate2 = TryToParse<DateTime?>(dataRow["PROPOSED_2_DT"]);
            preliminaryMeetingAppointmentBE.RequestedDate3 = TryToParse<DateTime?>(dataRow["PROPOSED_3_DT"]);
            preliminaryMeetingAppointmentBE.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]);
            preliminaryMeetingAppointmentBE.CancelAfterDt = TryToParse<DateTime?>(dataRow["CANCEL_AFTER_DT"]);
            preliminaryMeetingAppointmentBE.IsReschedule = TryToParse<bool?>(dataRow["RESCHEDULE_IND"]);

            return preliminaryMeetingAppointmentBE;

        }

        #endregion

    }

    #endregion

}