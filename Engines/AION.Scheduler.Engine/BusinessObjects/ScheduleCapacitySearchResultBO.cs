using AION.Base;
using AION.Scheduler.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AION.Scheduler.Engine.BusinessObjects
{
    public class ScheduleCapacitySearchResultBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        #endregion

        #region Public Methods

        /// <summary>
        /// brings back the list for the dates and reviewers cross join
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="reviewerscsv"></param>
        /// <returns></returns>
        public List<ScheduleCapacitySearchResultBE> GetReviewersDateList(DateTime? startdate, DateTime? enddate, string reviewerscsv)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ScheduleCapacitySearchResultBE> scheduleCapacitySearchBE = new List<ScheduleCapacitySearchResultBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@startdate", startdate);
                sqlParameters[1] = new SqlParameter("@enddate", enddate);
                sqlParameters[2] = new SqlParameter("@reviewerscsv", reviewerscsv);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_schedule_capacity_search_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    scheduleCapacitySearchBE.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return scheduleCapacitySearchBE;

        }

        /// <summary>
        /// Gets  Meetings for date range and reviewer id
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="reviewerscsv"></param>
        /// <returns></returns>
        public List<ScheduleCapacitySearchResultBE> GetMeetingsForDateList(DateTime? startdate, DateTime? enddate, string reviewerscsv)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ScheduleCapacitySearchResultBE> scheduleCapacitySearchBE = new List<ScheduleCapacitySearchResultBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@startdate", startdate);
                sqlParameters[1] = new SqlParameter("@enddate", enddate);
                sqlParameters[2] = new SqlParameter("@reviewerscsv", reviewerscsv);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_schedule_capacity_search_meetings_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    scheduleCapacitySearchBE.Add(this.ConvertDataRowToMeetingBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return scheduleCapacitySearchBE;

        }

        public List<ScheduleCapacitySearchResultBE> GetPlanReviewsForDateList(DateTime? startdate, DateTime? enddate, string reviewerscsv)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ScheduleCapacitySearchResultBE> scheduleCapacitySearchBE = new List<ScheduleCapacitySearchResultBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@startdate", startdate);
                sqlParameters[1] = new SqlParameter("@enddate", enddate);
                sqlParameters[2] = new SqlParameter("@reviewerscsv", reviewerscsv);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_schedule_capacity_search_plan_reviews", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    scheduleCapacitySearchBE.Add(this.ConvertDataRowToMeetingBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return scheduleCapacitySearchBE;

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

        private ScheduleCapacitySearchResultBE ConvertDataRowToBE(DataRow dataRow)
        {
            ScheduleCapacitySearchResultBE scheduleCapacitySearchResultBE = new ScheduleCapacitySearchResultBE();

            scheduleCapacitySearchResultBE.UserId = TryToParse<int>(dataRow["USER_ID"]);
            scheduleCapacitySearchResultBE.FirstName = TryToParse<string>(dataRow["FIRST_NM"]);
            scheduleCapacitySearchResultBE.LastName = TryToParse<string>(dataRow["LAST_NM"]);
            scheduleCapacitySearchResultBE.ScheduleDate = TryToParse<DateTime?>(dataRow["SCHEDULE_DATE"]);
            return scheduleCapacitySearchResultBE;

        }
        private ScheduleCapacitySearchResultBE ConvertDataRowToMeetingBE(DataRow dataRow)
        {

            ScheduleCapacitySearchResultBE scheduleCapacitySearchResultBE = new ScheduleCapacitySearchResultBE();

            scheduleCapacitySearchResultBE.UserId = TryToParse<int>(dataRow["USER_ID"]);
            scheduleCapacitySearchResultBE.ApptId = TryToParse<int>(dataRow["APPT_ID"]);
            scheduleCapacitySearchResultBE.MeetingType = TryToParse<string>(dataRow["MEETING_TYPE"]);
            scheduleCapacitySearchResultBE.MeetingName = TryToParse<string>(dataRow["APPT_NM"]);
            scheduleCapacitySearchResultBE.StartDttm = TryToParse<DateTime?>(dataRow["START_DTTM"]);
            scheduleCapacitySearchResultBE.EndDttm = TryToParse<DateTime?>(dataRow["END_DTTM"]);
            return scheduleCapacitySearchResultBE;

        }
        #endregion

    }
}
