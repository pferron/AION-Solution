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

    #region BusinessObject - NonProjectAppoinmentBO

    public class NonProjectAppoinmentBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, GetNonExistingNPAs };

        private string _errorMsg;

        private NonProjectAppoinmentBE _nonProjectAppoinmentBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(NonProjectAppoinmentBE nonProjectAppoinmentBE)
        {
            int id;
            _nonProjectAppoinmentBE = nonProjectAppoinmentBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[21];

                sqlParameters[0] = new SqlParameter("@APPT_NM", nonProjectAppoinmentBE.AppoinmentName);
                sqlParameters[1] = new SqlParameter("@ALL_PLAN_REVIEWERS_IND", nonProjectAppoinmentBE.IsAllPlanReviewers);
                sqlParameters[2] = new SqlParameter("@ALL_DAY_IND", nonProjectAppoinmentBE.IsAllDay);
                sqlParameters[3] = new SqlParameter("@APPT_FROM_DTTM", nonProjectAppoinmentBE.AppointmentFrom);
                sqlParameters[4] = new SqlParameter("@APPT_TO_DTTM", nonProjectAppoinmentBE.AppointmentTo);
                sqlParameters[5] = new SqlParameter("@NON_PROJECT_APPT_TYP_REF_ID", nonProjectAppoinmentBE.NPATypeRefID);
                sqlParameters[6] = new SqlParameter("@MEETING_ROOM_REF_ID", nonProjectAppoinmentBE.MeetingRoomRefID);
                sqlParameters[7] = new SqlParameter("@APPT_RECURRENCE_REF_ID", nonProjectAppoinmentBE.AppoinmentRecurrenceRefID);
                sqlParameters[8] = new SqlParameter("@ALL_BUILD_IND", nonProjectAppoinmentBE.IsAllBuild);
                sqlParameters[9] = new SqlParameter("@ALL_ELCTR_IND", nonProjectAppoinmentBE.IsAllElectric);
                sqlParameters[10] = new SqlParameter("@ALL_MECH_IND", nonProjectAppoinmentBE.IsAllMech);
                sqlParameters[11] = new SqlParameter("@ALL_PLUMB_IND", nonProjectAppoinmentBE.IsAllPlumb);
                sqlParameters[12] = new SqlParameter("@ALL_ZONING_IND", nonProjectAppoinmentBE.IsAllZoning);
                sqlParameters[13] = new SqlParameter("@ALL_FIRE_IND", nonProjectAppoinmentBE.IsAllFire);
                sqlParameters[14] = new SqlParameter("@ALL_BACKFLOW_IND", nonProjectAppoinmentBE.IsAllBackFlow);
                sqlParameters[15] = new SqlParameter("@ALL_EHS_FOOD_IND", nonProjectAppoinmentBE.IsAllEhsFood);
                sqlParameters[16] = new SqlParameter("@ALL_EHS_POOL_IND", nonProjectAppoinmentBE.IsAllEhsPool);
                sqlParameters[17] = new SqlParameter("@ALL_EHS_LODGE_IND", nonProjectAppoinmentBE.IsAllEhsLodge);
                sqlParameters[18] = new SqlParameter("@ALL_EHS_DAYCARE_IND", nonProjectAppoinmentBE.IsAllEhsDayCare);

                sqlParameters[19] = new SqlParameter("@WKR_ID_TXT", nonProjectAppoinmentBE.UserId);

                sqlParameters[20] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[20].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_non_project_appoinment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Delete(int scheduleId, bool flag)
        {
            int rows;
            _id = scheduleId;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@scheduleId", scheduleId);
                sqlParameters[1] = new SqlParameter("@flag", flag);
                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_non_project_appoinment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public NonProjectAppoinmentBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            NonProjectAppoinmentBE nonProjectAppoinmentBE = new NonProjectAppoinmentBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_non_project_appoinment_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                nonProjectAppoinmentBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return nonProjectAppoinmentBE;
        }

        public NonProjectAppoinmentBE GetNpaByProjectScheduleId(int projectScheduleId)
        {
            _id = projectScheduleId;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            NonProjectAppoinmentBE nonProjectAppoinmentBE = new NonProjectAppoinmentBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectScheduleID", projectScheduleId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_non_project_appoinment_by_prj_sch_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                nonProjectAppoinmentBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return nonProjectAppoinmentBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_non_project_appoinment_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<NonProjectAppoinmentBE> GetList()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<NonProjectAppoinmentBE> nonProjectAppoinmentBEList = new List<NonProjectAppoinmentBE>();

            try
            {


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_non_project_appoinment_get_list", base.ConnectionString);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    nonProjectAppoinmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return nonProjectAppoinmentBEList;

        }
        public List<NonProjectAppoinmentBE> Search(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<NonProjectAppoinmentBE> nonProjectAppoinmentBEList = new List<NonProjectAppoinmentBE>();

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@APPT_FROM_DTTM", startdate);
                sqlParameters[1] = new SqlParameter("@APPT_TO_DTTM", enddate);
                sqlParameters[2] = new SqlParameter("@NON_PROJECT_APPT_TYP_REF_ID", type);
                sqlParameters[3] = new SqlParameter("@SEARCH_TXT", searchtxt);
                sqlParameters[4] = new SqlParameter("@REVIEWER_ID", reviewerId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_non_project_appoinment_search", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    nonProjectAppoinmentBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return nonProjectAppoinmentBEList;

        }
        /// <summary>
        /// Used to get the NPA --> Project schedules for the search in NPA module
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reviewerId"></param>
        /// <param name="searchtxt"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<NPASearchResultBE> Searchv2(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<NPASearchResultBE> nonProjectAppoinmentBEList = new List<NPASearchResultBE>();

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@APPT_FROM_DTTM", startdate);
                sqlParameters[1] = new SqlParameter("@APPT_TO_DTTM", enddate);
                sqlParameters[2] = new SqlParameter("@NON_PROJECT_APPT_TYP_REF_ID", type);
                sqlParameters[3] = new SqlParameter("@SEARCH_TXT", searchtxt);
                sqlParameters[4] = new SqlParameter("@REVIEWER_ID", reviewerId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_non_project_appoinment_search_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    nonProjectAppoinmentBEList.Add(this.ConvertDataRowToNPASearchResultBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return nonProjectAppoinmentBEList;

        }
        public int Update(NonProjectAppoinmentBE nonProjectAppoinmentBE)
        {
            int rows;
            _nonProjectAppoinmentBE = nonProjectAppoinmentBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[23];

                sqlParameters[0] = new SqlParameter("@NON_PROJECT_APPT_ID", nonProjectAppoinmentBE.NonProjectAppointmentID);
                sqlParameters[1] = new SqlParameter("@APPT_NM", nonProjectAppoinmentBE.AppoinmentName);
                sqlParameters[2] = new SqlParameter("@ALL_PLAN_REVIEWERS_IND", nonProjectAppoinmentBE.IsAllPlanReviewers);
                sqlParameters[3] = new SqlParameter("@ALL_DAY_IND", nonProjectAppoinmentBE.IsAllDay);
                sqlParameters[4] = new SqlParameter("@APPT_FROM_DTTM", nonProjectAppoinmentBE.AppointmentFrom);
                sqlParameters[5] = new SqlParameter("@APPT_TO_DTTM", nonProjectAppoinmentBE.AppointmentTo);
                sqlParameters[6] = new SqlParameter("@NON_PROJECT_APPT_TYP_REF_ID", nonProjectAppoinmentBE.NPATypeRefID);
                sqlParameters[7] = new SqlParameter("@MEETING_ROOM_REF_ID", nonProjectAppoinmentBE.MeetingRoomRefID);
                sqlParameters[8] = new SqlParameter("@UPDATED_DTTM", nonProjectAppoinmentBE.UpdatedDate);
                sqlParameters[9] = new SqlParameter("@APPT_RECURRENCE_REF_ID", nonProjectAppoinmentBE.AppoinmentRecurrenceRefID);
                sqlParameters[10] = new SqlParameter("@ALL_BUILD_IND", nonProjectAppoinmentBE.IsAllBuild);
                sqlParameters[11] = new SqlParameter("@ALL_ELCTR_IND", nonProjectAppoinmentBE.IsAllElectric);
                sqlParameters[12] = new SqlParameter("@ALL_MECH_IND", nonProjectAppoinmentBE.IsAllMech);
                sqlParameters[13] = new SqlParameter("@ALL_PLUMB_IND", nonProjectAppoinmentBE.IsAllPlumb);
                sqlParameters[14] = new SqlParameter("@ALL_ZONING_IND", nonProjectAppoinmentBE.IsAllZoning);
                sqlParameters[15] = new SqlParameter("@ALL_FIRE_IND", nonProjectAppoinmentBE.IsAllFire);
                sqlParameters[16] = new SqlParameter("@ALL_BACKFLOW_IND", nonProjectAppoinmentBE.IsAllBackFlow);
                sqlParameters[17] = new SqlParameter("@ALL_EHS_FOOD_IND", nonProjectAppoinmentBE.IsAllEhsFood);
                sqlParameters[18] = new SqlParameter("@ALL_EHS_POOL_IND", nonProjectAppoinmentBE.IsAllEhsPool);
                sqlParameters[19] = new SqlParameter("@ALL_EHS_LODGE_IND", nonProjectAppoinmentBE.IsAllEhsLodge);
                sqlParameters[20] = new SqlParameter("@ALL_EHS_DAYCARE_IND", nonProjectAppoinmentBE.IsAllEhsDayCare);

                sqlParameters[21] = new SqlParameter("@WKR_ID_TXT", nonProjectAppoinmentBE.UserId);

                sqlParameters[22] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[22].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_non_project_appoinment", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        /// <summary>
        /// Gets the NPAs marked as 'all building', 'all reviewers' etc
        /// that the user doesn't have schedules for
        /// USED BY NPAAdapter.SendExistingNPACalendarApptsSaveUser
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <param name="allbuildind"></param>
        /// <param name="allelctrind"></param>
        /// <param name="allmechind"></param>
        /// <param name="allplumbind"></param>
        /// <param name="allzoningind"></param>
        /// <param name="allfireind"></param>
        /// <param name="allbackflowind"></param>
        /// <param name="allehsfoodind"></param>
        /// <param name="allehspoolind"></param>
        /// <param name="allehslodgeind"></param>
        /// <param name="allehsdaycareind"></param>
        /// <returns></returns>
        public List<int[]> GetNonExistingNPAsByUser(
            DateTime? startdate,
            DateTime? enddate,
            int userid,
            bool allbuildind = false,
            bool allelctrind = false,
            bool allmechind = false,
            bool allplumbind = false,
            bool allzoningind = false,
            bool allfireind = false,
            bool allbackflowind = false,
            bool allehsfoodind = false,
            bool allehspoolind = false,
            bool allehslodgeind = false,
            bool allehsdaycareind = false)
        {

            if (!this.Validate(ActionType.GetNonExistingNPAs))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<int[]> nonProjectAppoinmentBEList = new List<int[]>();

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[14];

                sqlParameters[0] = new SqlParameter("@startdate", startdate);
                sqlParameters[1] = new SqlParameter("@enddate", enddate);
                sqlParameters[2] = new SqlParameter("@reviewerscsv", userid);
                sqlParameters[3] = new SqlParameter("@all_build_ind", allbuildind);
                sqlParameters[4] = new SqlParameter("@all_elctr_ind", allelctrind);
                sqlParameters[5] = new SqlParameter("@all_mech_ind", allmechind);
                sqlParameters[6] = new SqlParameter("@all_plumb_ind", allplumbind);
                sqlParameters[7] = new SqlParameter("@all_zoning_ind", allzoningind);
                sqlParameters[8] = new SqlParameter("@all_fire_ind", allfireind);
                sqlParameters[9] = new SqlParameter("@all_backflow_ind", allbackflowind);
                sqlParameters[10] = new SqlParameter("@all_ehs_food_ind", allehsfoodind);
                sqlParameters[11] = new SqlParameter("@all_ehs_pool_ind", allehspoolind);
                sqlParameters[12] = new SqlParameter("@all_ehs_lodge_ind", allehslodgeind);
                sqlParameters[13] = new SqlParameter("@all_ehs_daycare_ind", allehsdaycareind);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_npa_AllIndByUserIds", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    //return object is user_id, appt_id, business_ref_id
                    int user_id = TryToParse<int>(dataRow["USER_ID"]);
                    int appt_id = TryToParse<int>(dataRow["NON_PROJECT_APPT_ID"]);
                    int business_ref_id = TryToParse<int>(dataRow["BUSINESS_REF_ID"]);
                    int[] row = { user_id, appt_id, business_ref_id };

                    nonProjectAppoinmentBEList.Add(row);

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return nonProjectAppoinmentBEList;

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
                case ActionType.GetNonExistingNPAs:
                    return (_errorMsg == String.Empty);
                default:
                    break;
            }

            return true;

        }

        private NonProjectAppoinmentBE ConvertDataRowToBE(DataRow dataRow)
        {
            NonProjectAppoinmentBE nonProjectAppoinmentBE = new NonProjectAppoinmentBE();

            nonProjectAppoinmentBE.NonProjectAppointmentID = TryToParse<int?>(dataRow["NON_PROJECT_APPT_ID"]);
            nonProjectAppoinmentBE.AppoinmentName = TryToParse<string>(dataRow["APPT_NM"]);
            nonProjectAppoinmentBE.IsAllPlanReviewers = TryToParse<bool?>(dataRow["ALL_PLAN_REVIEWERS_IND"]);
            nonProjectAppoinmentBE.IsAllDay = TryToParse<bool?>(dataRow["ALL_DAY_IND"]);
            nonProjectAppoinmentBE.AppointmentFrom = TryToParse<DateTime?>(dataRow["APPT_FROM_DTTM"]);
            nonProjectAppoinmentBE.AppointmentTo = TryToParse<DateTime?>(dataRow["APPT_TO_DTTM"]);
            nonProjectAppoinmentBE.NPATypeRefID = TryToParse<int?>(dataRow["NON_PROJECT_APPT_TYP_REF_ID"]);
            nonProjectAppoinmentBE.MeetingRoomRefID = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            nonProjectAppoinmentBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            nonProjectAppoinmentBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            nonProjectAppoinmentBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            nonProjectAppoinmentBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            nonProjectAppoinmentBE.AppoinmentRecurrenceRefID = TryToParse<int?>(dataRow["APPT_RECURRENCE_REF_ID"]);
            nonProjectAppoinmentBE.IsAllBuild = TryToParse<bool?>(dataRow["ALL_BUILD_IND"]);
            nonProjectAppoinmentBE.IsAllElectric = TryToParse<bool?>(dataRow["ALL_ELCTR_IND"]);
            nonProjectAppoinmentBE.IsAllMech = TryToParse<bool?>(dataRow["ALL_MECH_IND"]);
            nonProjectAppoinmentBE.IsAllPlumb = TryToParse<bool?>(dataRow["ALL_PLUMB_IND"]);
            nonProjectAppoinmentBE.IsAllZoning = TryToParse<bool?>(dataRow["ALL_ZONING_IND"]);
            nonProjectAppoinmentBE.IsAllFire = TryToParse<bool?>(dataRow["ALL_FIRE_IND"]);
            nonProjectAppoinmentBE.IsAllBackFlow = TryToParse<bool?>(dataRow["ALL_BACKFLOW_IND"]);
            nonProjectAppoinmentBE.IsAllEhsFood = TryToParse<bool?>(dataRow["ALL_EHS_FOOD_IND"]);
            nonProjectAppoinmentBE.IsAllEhsPool = TryToParse<bool?>(dataRow["ALL_EHS_POOL_IND"]);
            nonProjectAppoinmentBE.IsAllEhsLodge = TryToParse<bool?>(dataRow["ALL_EHS_LODGE_IND"]);
            nonProjectAppoinmentBE.IsAllEhsDayCare = TryToParse<bool?>(dataRow["ALL_EHS_DAYCARE_IND"]);

            return nonProjectAppoinmentBE;

        }
        private NPASearchResultBE ConvertDataRowToNPASearchResultBE(DataRow dataRow)
        {
            NPASearchResultBE nonProjectAppoinmentBE = new NPASearchResultBE();

            nonProjectAppoinmentBE.NonProjectAppointmentID = TryToParse<int?>(dataRow["NON_PROJECT_APPT_ID"]);
            nonProjectAppoinmentBE.AppoinmentName = TryToParse<string>(dataRow["APPT_NM"]);
            nonProjectAppoinmentBE.IsAllPlanReviewers = TryToParse<bool?>(dataRow["ALL_PLAN_REVIEWERS_IND"]);
            nonProjectAppoinmentBE.IsAllDay = TryToParse<bool?>(dataRow["ALL_DAY_IND"]);
            nonProjectAppoinmentBE.AppointmentFrom = TryToParse<DateTime?>(dataRow["APPT_FROM_DTTM"]);
            nonProjectAppoinmentBE.AppointmentTo = TryToParse<DateTime?>(dataRow["APPT_TO_DTTM"]);
            nonProjectAppoinmentBE.NPATypeRefID = TryToParse<int?>(dataRow["NON_PROJECT_APPT_TYP_REF_ID"]);
            nonProjectAppoinmentBE.MeetingRoomRefID = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            nonProjectAppoinmentBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            nonProjectAppoinmentBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            nonProjectAppoinmentBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            nonProjectAppoinmentBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            nonProjectAppoinmentBE.AppoinmentRecurrenceRefID = TryToParse<int?>(dataRow["APPT_RECURRENCE_REF_ID"]);
            nonProjectAppoinmentBE.IsAllBuild = TryToParse<bool?>(dataRow["ALL_BUILD_IND"]);
            nonProjectAppoinmentBE.IsAllElectric = TryToParse<bool?>(dataRow["ALL_ELCTR_IND"]);
            nonProjectAppoinmentBE.IsAllMech = TryToParse<bool?>(dataRow["ALL_MECH_IND"]);
            nonProjectAppoinmentBE.IsAllPlumb = TryToParse<bool?>(dataRow["ALL_PLUMB_IND"]);
            nonProjectAppoinmentBE.IsAllZoning = TryToParse<bool?>(dataRow["ALL_ZONING_IND"]);
            nonProjectAppoinmentBE.IsAllFire = TryToParse<bool?>(dataRow["ALL_FIRE_IND"]);
            nonProjectAppoinmentBE.IsAllBackFlow = TryToParse<bool?>(dataRow["ALL_BACKFLOW_IND"]);
            nonProjectAppoinmentBE.IsAllEhsFood = TryToParse<bool?>(dataRow["ALL_EHS_FOOD_IND"]);
            nonProjectAppoinmentBE.IsAllEhsPool = TryToParse<bool?>(dataRow["ALL_EHS_POOL_IND"]);
            nonProjectAppoinmentBE.IsAllEhsLodge = TryToParse<bool?>(dataRow["ALL_EHS_LODGE_IND"]);
            nonProjectAppoinmentBE.IsAllEhsDayCare = TryToParse<bool?>(dataRow["ALL_EHS_DAYCARE_IND"]);
            nonProjectAppoinmentBE.ProjectScheduleID = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);
            nonProjectAppoinmentBE.RecurringApptDt = TryToParse<DateTime?>(dataRow["RECURRING_APPT_DT"]);

            return nonProjectAppoinmentBE;

        }
        #endregion

    }

    #endregion

}