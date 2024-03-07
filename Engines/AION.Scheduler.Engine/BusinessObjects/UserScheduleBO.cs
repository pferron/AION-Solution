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

    #region BusinessObject - UserScheduleBO

    public class UserScheduleBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private UserScheduleBE _userScheduleBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(UserScheduleBE userScheduleBE)
        {
            int id;
            _userScheduleBE = userScheduleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@START_DTTM", userScheduleBE.StartDateTime);
                sqlParameters[1] = new SqlParameter("@END_DTTM", userScheduleBE.EndDateTime);
                sqlParameters[2] = new SqlParameter("@PROJECT_SCHEDULE_ID", userScheduleBE.ProjectScheduleID);
                sqlParameters[3] = new SqlParameter("@BUSINESS_REF_ID", userScheduleBE.BusinessRefID);
                sqlParameters[4] = new SqlParameter("@USER_ID", userScheduleBE.UserID);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", userScheduleBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_schedule", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_schedule", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public int DeleteByUserScheduleIds(string userScheduleIds)
        {
            int rows;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("userScheduleIds", userScheduleIds);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user_schedules_by_user_schedule_ids", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }
        /// <summary>
        /// Deletes user schedules for Scheduled that are greater than input date
        /// csvRecIdTxt should be rec_id_txt seperated by ,
        /// input date
        ///     checks if the user schedules start date is greater than input date
        /// </summary>
        /// <param name="csvRecIdTxt"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public int DeleteUserSchedulesByRecIDAfterDate(string csvRecIdTxt, string inputDate)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@csvRecIdTxt", csvRecIdTxt);

                sqlParameters[1] = new SqlParameter("@inputDate", inputDate);
                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_remove_future_user_schedules", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }
        public List<UserScheduleBE> GetUsedTimeSlotsByUserID(int userId)
        {
            _id = userId;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserScheduleBE> userScheduleBEList = new List<UserScheduleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", userId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_used_schedules_by_user_id", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    UserScheduleBE userScheduleBE = new UserScheduleBE();

                    userScheduleBE.UserScheduleID = TryToParse<int?>(dataRow["USER_SCHEDULE_ID"]);
                    userScheduleBE.StartDateTime = TryToParse<DateTime?>(dataRow["START_DTTM"]);
                    userScheduleBE.EndDateTime = TryToParse<DateTime?>(dataRow["END_DTTM"]);
                    userScheduleBE.ProjectScheduleID = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);
                    userScheduleBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);

                    userScheduleBEList.Add(userScheduleBE);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userScheduleBEList;
        }

        public List<UserScheduleExBE> GetUsedTimeSlotsWithExtrasByUserID(int userId = -1, DateTime? startDt = null, DateTime? endDt = null)
        {
            _id = userId;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserScheduleExBE> userScheduleBEList = new List<UserScheduleExBE>();

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@IDENTITY", userId == -1 ? (object)DBNull.Value : userId);
                sqlParameters[1] = new SqlParameter("@STDT", startDt == null ? (object)DBNull.Value : startDt);
                sqlParameters[2] = new SqlParameter("@ENTDT", endDt == null ? (object)DBNull.Value : endDt);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_used_schedules_extras_by_user_id_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    UserScheduleExBE userScheduleBE = new UserScheduleExBE();

                    userScheduleBE.UserScheduleID = TryToParse<int?>(dataRow["USER_SCHEDULE_ID"]);
                    userScheduleBE.StartDateTime = TryToParse<DateTime?>(dataRow["START_DTTM"]);
                    userScheduleBE.EndDateTime = TryToParse<DateTime?>(dataRow["END_DTTM"]);
                    userScheduleBE.ProjectScheduleID = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);
                    userScheduleBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
                    userScheduleBE.ProjectScheduleTypeName = TryToParse<string>(dataRow["PROJECT_SCHEDULE_TYP_DESC"]);
                    userScheduleBE.ProjectID = TryToParse<int?>(dataRow["PROJECT_ID"]).HasValue ? TryToParse<int?>(dataRow["PROJECT_ID"]) : 0;
                    userScheduleBE.BusinessRefID = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]).HasValue ? TryToParse<int?>(dataRow["BUSINESS_REF_ID"]) : -1;
                    userScheduleBE.ProjectCategory = TryToParse<string>(dataRow["PROJ_CATEGORY"]);
                    userScheduleBEList.Add(userScheduleBE);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userScheduleBEList;
        }

        public UserScheduleBE GetById(int id)
        {
            throw new Exception("Error check sproc");
            //_id = id;

            //if (!this.Validate(ActionType.GetById))
            //    throw (new Exception(_errorMsg));

            //UserScheduleBE userScheduleBE = new UserScheduleBE();
            //DataSet dataSet;

            //try
            //{
            //    SqlParameter[] sqlParameters = new SqlParameter[1];

            //    sqlParameters[0] = new SqlParameter("@identity", id);

            //    dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_get_by_id", base.ConnectionString, ref sqlParameters);

            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    userScheduleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            //}

            //return userScheduleBE;
        }

        public DataSet GetDataSet(int id)
        {
            throw new Exception("Error check sproc");
            //_id = id;

            //if (!this.Validate(ActionType.GetDataSet))
            //    throw (new Exception(_errorMsg));

            //DataSet dataSet;

            //try
            //{
            //    SqlParameter[] sqlParameters = new SqlParameter[1];

            //    sqlParameters[0] = new SqlParameter("@identity", id);

            //    dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_get_list", base.ConnectionString, ref sqlParameters);

            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

            //return dataSet;
        }

        public List<UserScheduleBE> GetList(int id)
        {
            throw new Exception("Error check sproc");
            //_id = id;

            //if (!this.Validate(ActionType.GetList))
            //    throw (new Exception(_errorMsg));

            //DataSet dataSet;
            //List<UserScheduleBE> userScheduleBEList = new List<UserScheduleBE>();

            //try
            //{
            //    SqlParameter[] sqlParameters = new SqlParameter[1];

            //    sqlParameters[0] = new SqlParameter("@identity", id);

            //    dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_get_list", base.ConnectionString, ref sqlParameters);

            //    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            //    {
            //        userScheduleBEList.Add(this.ConvertDataRowToBE(dataRow));
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

            //return userScheduleBEList;

        }
        public List<UserScheduleBE> GetListByScheduleID(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserScheduleBE> userScheduleBEList = new List<UserScheduleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectscheduleid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_get_list_byprojectscheduleid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userScheduleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userScheduleBEList;

        }

        public List<UserScheduleBE> GetListByProjectScheduleType(string projectScheduleType, DateTime startDate, DateTime endDate, int userId = 0)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserScheduleBE> userScheduleBEList = new List<UserScheduleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                if (userId == 0)
                {
                    sqlParameters[0] = new SqlParameter("@identity", DBNull.Value);
                }
                else
                {
                    sqlParameters[0] = new SqlParameter("@identity", userId);
                }

                sqlParameters[1] = new SqlParameter("@projectscheduletype", projectScheduleType);
                sqlParameters[2] = new SqlParameter("@startdate", startDate);
                sqlParameters[3] = new SqlParameter("@enddate", endDate);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_schedule_get_list_by_project_schedule_type", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userScheduleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userScheduleBEList;

        }

        public int Update(UserScheduleBE userScheduleBE)
        {
            int rows;
            _userScheduleBE = userScheduleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@USER_SCHEDULE_ID", userScheduleBE.UserScheduleID);
                sqlParameters[1] = new SqlParameter("@START_DTTM", userScheduleBE.StartDateTime);
                sqlParameters[2] = new SqlParameter("@END_DTTM", userScheduleBE.EndDateTime);
                sqlParameters[3] = new SqlParameter("@PROJECT_SCHEDULE_ID", userScheduleBE.ProjectScheduleID);
                sqlParameters[4] = new SqlParameter("@USER_ID", userScheduleBE.UserID);
                sqlParameters[5] = new SqlParameter("@UPDATED_DTTM", userScheduleBE.UpdatedDate);
                sqlParameters[6] = new SqlParameter("@BUSINESS_REF_ID", userScheduleBE.BusinessRefID);

                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", userScheduleBE.UserId);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_schedule", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<UserScheduleBE> GetDisablePlanReviewerAllocations(int userId, string projectScheduleTypeDesc)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));
            DataSet dataSet;
            List<UserScheduleBE> userScheduleBEList = new List<UserScheduleBE>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@userId", userId);
                sqlParameters[1] = new SqlParameter("@projectScheduleTypeDesc", projectScheduleTypeDesc);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_disabled_plan_reviewer_allocations", base.ConnectionString, ref sqlParameters);
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userScheduleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return userScheduleBEList;
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

        private UserScheduleBE ConvertDataRowToBE(DataRow dataRow)
        {
            UserScheduleBE userScheduleBE = new UserScheduleBE();

            userScheduleBE.UserScheduleID = TryToParse<int?>(dataRow["USER_SCHEDULE_ID"]);
            userScheduleBE.StartDateTime = TryToParse<DateTime?>(dataRow["START_DTTM"]);
            userScheduleBE.EndDateTime = TryToParse<DateTime?>(dataRow["END_DTTM"]);
            userScheduleBE.ProjectScheduleID = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);
            userScheduleBE.BusinessRefID = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            userScheduleBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
            userScheduleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            userScheduleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            userScheduleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            userScheduleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return userScheduleBE;

        }


        #endregion

    }

    #endregion

}