#region Using

using AION.Base;
using AION.BL;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - UserBO

    public class UserBO : BaseBO
    {

        #region Properties

        Logger m_Logger = new Logger();
        public Logger Logging
        {
            get { return m_Logger; }
            set { m_Logger = value; }
        }

        #endregion

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private UserBE _userBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(UserBE userBE)
        {
            int id;
            _userBE = userBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[23];

                sqlParameters[0] = new SqlParameter("@FIRST_NM", userBE.FirstNm);
                sqlParameters[1] = new SqlParameter("@LAST_NM", userBE.LastNm);
                sqlParameters[2] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", 1);
                sqlParameters[3] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", userBE.SrcSystemValueTxt);
                sqlParameters[4] = new SqlParameter("@ACTIVE_IND", userBE.IsActive);
                sqlParameters[5] = new SqlParameter("@USER_INTERFACE_SETTING_TXT", userBE.UiSettings);
                sqlParameters[6] = new SqlParameter("@IS_EXPRESS_SCHEDULED_IND", userBE.IsExpressSched);
                sqlParameters[7] = new SqlParameter("@USER_NM", userBE.UserName);
                sqlParameters[8] = new SqlParameter("@LAN_ID_TXT", userBE.ADName);
                sqlParameters[9] = new SqlParameter("@PHONE_NUM", userBE.Phone);
                sqlParameters[10] = new SqlParameter("@EMAIL_ADDR_TXT", userBE.Email);
                sqlParameters[11] = new SqlParameter("@NOTES_TXT", userBE.Notes);
                sqlParameters[12] = new SqlParameter("@IS_SCHEDULABLE_IND", userBE.IsSchedulable);
                sqlParameters[13] = new SqlParameter("@PLAN_REVIEW_OVERRIDE_HOURS_NBR", userBE.PlanReviewOverrideHours);
                sqlParameters[14] = new SqlParameter("@HOURS_ESTIMATED_DESC", userBE.HoursEstimated);
                sqlParameters[15] = new SqlParameter("@JURISDICTION_TYP_ID", userBE.Jurisdiction);
                sqlParameters[16] = new SqlParameter("@SCHEDULABLE_LVL_DESC", userBE.SchedulableLevel);

                sqlParameters[17] = new SqlParameter("@WKR_ID_TXT", userBE.UserId);
                sqlParameters[18] = new SqlParameter("@IS_PRELIM_MEETING_ALLOWED_IND", (userBE.IsPrelimMeetingAllowed.HasValue ? userBE.IsPrelimMeetingAllowed.Value : false));
                sqlParameters[19] = new SqlParameter("@USER_PRINCIPAL_NM", userBE.UserPrincipalName);
                sqlParameters[20] = new SqlParameter("@CALENDAR_ID", userBE.CalendarId);
                sqlParameters[21] = new SqlParameter("@CITY_IND", userBE.IsCity);

                sqlParameters[22] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[22].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_user_v2", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_user", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public UserBE GetById(int id)
        {
            //To Do remove after we fix -wkb
            var message = "[GetById User Id] We are hitting this method a lot. Adding  GUID to help define differences:" + Guid.NewGuid() + ".";
            var logging = Logging.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message,
                string.Empty, string.Empty, string.Empty);

            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            UserBE userBE = new UserBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userBE;
        }


        public UserBE GetByExternalRefInfo(string external_Ref_info, int externalSystemID)
        {

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            UserBE userBE = new UserBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@externalRefInfo", external_Ref_info);
                sqlParameters[1] = new SqlParameter("@externalSystemID", externalSystemID);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_GetByExternalRefInfo", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userBE;
        }

        public UserBE GetByEmail(string email)
        {

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            UserBE userBE = new UserBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@email", email);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_by_email", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return userBE;
        }


        public bool GetUserIdentityByUserBE(UserBE userBE)
        {


            bool exists = false;
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@FIRST_NM", userBE.FirstNm);
                sqlParameters[1] = new SqlParameter("@LAST_NM", userBE.LastNm);
                sqlParameters[2] = new SqlParameter("@USER_NM", userBE.UserName);
                sqlParameters[3] = new SqlParameter("@LAN_ID_TXT", userBE.ADName);
                sqlParameters[4] = new SqlParameter("@EMAIL_ADDR_TXT", userBE.Email);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_by_userBE", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                exists = true;
            }

            return exists;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<UserBE> GetList()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserBE> userBEList = new List<UserBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBEList;

        }


        public List<UserBE> GetUserManagementSearchList(string name)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserBE> userBEList = new List<UserBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@name", name);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_admin_search_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBEList;

        }

        /// <summary>
        /// Searches all users, inactive as well.
        /// </summary>
        /// <param name="businessrefenumcsv">List of Enum Values for the business refs to search for</param>
        /// <returns></returns>
        public List<UserBE> GetUserManagementBusinessRefSearchList(string businessrefenumcsv)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserBE> userBEList = new List<UserBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@businessenumcsv", businessrefenumcsv);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_list_bybusinessrefid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBEList;

        }
        public int Update(UserBE userBE)
        {
            int rows;
            _userBE = userBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[25];

                sqlParameters[0] = new SqlParameter("@USER_ID", userBE.UserID);
                sqlParameters[1] = new SqlParameter("@FIRST_NM", userBE.FirstNm);
                sqlParameters[2] = new SqlParameter("@LAST_NM", userBE.LastNm);
                sqlParameters[3] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", userBE.ExternalSystemRefId);
                sqlParameters[4] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", userBE.SrcSystemValueTxt);
                sqlParameters[5] = new SqlParameter("@UPDATED_DTTM", userBE.UpdatedDate);
                sqlParameters[6] = new SqlParameter("@ACTIVE_IND", userBE.IsActive);
                sqlParameters[7] = new SqlParameter("@USER_INTERFACE_SETTING_TXT", userBE.UiSettings);
                sqlParameters[8] = new SqlParameter("@IS_EXPRESS_SCHEDULED_IND", userBE.IsExpressSched);
                sqlParameters[9] = new SqlParameter("@USER_NM", userBE.UserName);
                sqlParameters[10] = new SqlParameter("@LAN_ID_TXT", userBE.ADName);
                sqlParameters[11] = new SqlParameter("@PHONE_NUM", userBE.Phone);
                sqlParameters[12] = new SqlParameter("@EMAIL_ADDR_TXT", userBE.Email);
                sqlParameters[13] = new SqlParameter("@NOTES_TXT", userBE.Notes);
                sqlParameters[14] = new SqlParameter("@IS_SCHEDULABLE_IND", userBE.IsSchedulable);
                sqlParameters[15] = new SqlParameter("@PLAN_REVIEW_OVERRIDE_HOURS_NBR", userBE.PlanReviewOverrideHours);
                sqlParameters[16] = new SqlParameter("@HOURS_ESTIMATED_DESC", userBE.HoursEstimated);
                sqlParameters[17] = new SqlParameter("@JURISDICTION_TYP_ID", userBE.Jurisdiction);
                sqlParameters[18] = new SqlParameter("@SCHEDULABLE_LVL_DESC", userBE.SchedulableLevel);

                sqlParameters[19] = new SqlParameter("@WKR_ID_TXT", userBE.UserId);
                sqlParameters[20] = new SqlParameter("@IS_PRELIM_MEETING_ALLOWED_IND", userBE.IsPrelimMeetingAllowed.HasValue ? userBE.IsPrelimMeetingAllowed.Value : false);
                sqlParameters[21] = new SqlParameter("@USER_PRINCIPAL_NM", userBE.UserPrincipalName);
                sqlParameters[22] = new SqlParameter("@CALENDAR_ID", userBE.CalendarId);
                sqlParameters[23] = new SqlParameter("@CITY_IND", userBE.IsCity);

                sqlParameters[24] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[24].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_v2", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdateIsSchedulable(UserBE userBE)
        {
            int rows;
            _userBE = userBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@USER_ID", userBE.UserID);
                sqlParameters[1] = new SqlParameter("@UPDATED_DTTM", userBE.UpdatedDate);
                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", userBE.UpdatedByWkrId);
                sqlParameters[3] = new SqlParameter("@IS_SCHEDULABLE_IND", userBE.IsSchedulable);
                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_isschedulable", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdateIsExpressSched(UserBE userBE)
        {
            int rows;
            _userBE = userBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@USER_ID", userBE.UserID);
                sqlParameters[1] = new SqlParameter("@UPDATED_DTTM", userBE.UpdatedDate);
                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", userBE.UpdatedByWkrId);
                sqlParameters[3] = new SqlParameter("@IS_EXPRESS_SCHEDULED_IND", userBE.IsExpressSched);
                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_user_isexpresssched", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        /*Assign Facilitators */
        public List<UserBE> GetListBySystemRole(String SystemRoleName, bool getInactiveUsers = false)
        {
            //_id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));
            //transform name to enum
            SystemRoleEnum roleenum;
            Enum.TryParse<SystemRoleEnum>(SystemRoleName, out roleenum);

            DataSet dataSet;
            List<UserBE> userBEList = new List<UserBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@parentsystemroleenum", (int)roleenum);
                sqlParameters[1] = new SqlParameter("@getall", getInactiveUsers);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_byparentsystemrole", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBEList;
        }

        public List<UserBE> GetListBySystemRoleID(int systemRoleId)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserBE> userBEList = new List<UserBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@systemroleid", systemRoleId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_bysystemroleid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBEList;
        }

        public int InsertUsersWithSystemRole(string FirstName, string LastName, int ExternalSystemId, string SrcSystemValueTxt, string roleName, string operationType,
            string userNm, string lanIdTxt, string phoneNum, string emailAddrTxt, string notesTxt, bool isSchedulableInd, decimal planReviewOverrideHoursNbr,
            string hoursEstimatedDesc, int jurisdictionTypId, string schedulableLvlDesc)
        {
            int id;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[17];

                sqlParameters[0] = new SqlParameter("@firstName", FirstName);
                sqlParameters[1] = new SqlParameter("@lastName", LastName);
                sqlParameters[2] = new SqlParameter("@externalSystemRefId", ExternalSystemId);
                sqlParameters[3] = new SqlParameter("@srcSystemValueTxt", SrcSystemValueTxt);
                sqlParameters[4] = new SqlParameter("@roleName", roleName);
                sqlParameters[5] = new SqlParameter("@USER_NM", userNm);
                sqlParameters[6] = new SqlParameter("@LAN_ID_TXT", lanIdTxt);
                sqlParameters[7] = new SqlParameter("@PHONE_NUM", phoneNum);
                sqlParameters[8] = new SqlParameter("@EMAIL_ADDR_TXT", emailAddrTxt);
                sqlParameters[9] = new SqlParameter("@NOTES_TXT", notesTxt);
                sqlParameters[10] = new SqlParameter("@IS_SCHEDULABLE_IND", isSchedulableInd);
                sqlParameters[11] = new SqlParameter("@PLAN_REVIEW_OVERRIDE_HOURS_NBR", planReviewOverrideHoursNbr);
                sqlParameters[12] = new SqlParameter("@HOURS_ESTIMATED_DESC", hoursEstimatedDesc);
                sqlParameters[13] = new SqlParameter("@JURISDICTION_TYP_ID", jurisdictionTypId);
                sqlParameters[14] = new SqlParameter("@SCHEDULABLE_LVL_DESC", schedulableLvlDesc);
                sqlParameters[15] = new SqlParameter("@operation", operationType);
                sqlParameters[16] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[16].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_sync_aion_user_from_accela", base.ConnectionString, ref sqlParameters);


            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }



        public int DeleteUserWithSystemRole(int? userId, string roleName, string operationType)
        {
            int rows;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@userId", userId);
                sqlParameters[1] = new SqlParameter("@roleName", roleName);
                sqlParameters[2] = new SqlParameter("@operation", operationType);
                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_sync_aion_user_from_accela", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public int UpdateUsersWithSystemRole(int? userId, string FirstName, string LastName, string SrcSystemValueTxt, int ExternalSystemId, string roleName, string operationType,
            string userNm, string lanIdTxt, string phoneNum, string emailAddrTxt, string notesTxt, bool isSchedulableInd, decimal planReviewOverrideHoursNbr,
            string hoursEstimatedDesc, int jurisdictionTypId, string schedulableLvlDesc, bool isPrelimAllowed)
        {
            int rows;


            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[19];

                sqlParameters[0] = new SqlParameter("@firstName", FirstName);
                sqlParameters[1] = new SqlParameter("@lastName", LastName);
                sqlParameters[2] = new SqlParameter("@externalSystemRefId", ExternalSystemId);
                sqlParameters[3] = new SqlParameter("@srcSystemValueTxt", SrcSystemValueTxt);
                sqlParameters[4] = new SqlParameter("@userId", userId);

                sqlParameters[5] = new SqlParameter("@USER_NM", userNm);
                sqlParameters[6] = new SqlParameter("@LAN_ID_TXT", lanIdTxt);
                sqlParameters[7] = new SqlParameter("@PHONE_NUM", phoneNum);
                sqlParameters[8] = new SqlParameter("@EMAIL_ADDR_TXT", emailAddrTxt);
                sqlParameters[9] = new SqlParameter("@NOTES_TXT", notesTxt);
                sqlParameters[10] = new SqlParameter("@IS_SCHEDULABLE_IND", isSchedulableInd);
                sqlParameters[11] = new SqlParameter("@PLAN_REVIEW_OVERRIDE_HOURS_NBR", planReviewOverrideHoursNbr);
                sqlParameters[12] = new SqlParameter("@HOURS_ESTIMATED_DESC", hoursEstimatedDesc);
                sqlParameters[13] = new SqlParameter("@JURISDICTION_TYP_ID", jurisdictionTypId);
                sqlParameters[14] = new SqlParameter("@SCHEDULABLE_LVL_DESC", schedulableLvlDesc);

                sqlParameters[15] = new SqlParameter("@roleName", roleName);
                sqlParameters[16] = new SqlParameter("@operation", operationType);
                sqlParameters[17] = new SqlParameter("@IS_PRELIM_MEETING_ALLOWED_IND", isPrelimAllowed);
                sqlParameters[18] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[18].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_sync_aion_user_from_accela", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        /// <summary>
        /// Gets the user list by project type id (MMF, etc)
        /// Active=true, schedulable= true
        /// </summary>
        /// <param name="projecttypeid"></param>
        /// <param name="businessrefenumcsv"></param>
        /// <returns></returns>
        public List<UserBE> GetListByProjectTypeId(int projecttypeid, string businessrefenumcsv)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<UserBE> userBEList = new List<UserBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@PROJECT_TYP_REF_ID", projecttypeid);
                sqlParameters[1] = new SqlParameter("@businessenumcsv", businessrefenumcsv);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_user_get_reviewers_bypropertytype", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    userBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return userBEList;

        }

        public List<DateTime> GetDisablePlanReviewerAllocations(int userId, string projectScheduleTypeDesc)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<DateTime> dateList = new List<DateTime>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@userId", userId);
                sqlParameters[1] = new SqlParameter("@projectScheduleTypeDesc", projectScheduleTypeDesc);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_disabled_plan_reviewer_allocations", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {


                    DateTime date = TryToParse<DateTime>(dataRow["START_DTTM"]);

                    dateList.Add(date);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dateList;
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

        private UserBE ConvertDataRowToBE(DataRow dataRow)
        {
            UserBE userBE = new UserBE();

            userBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
            userBE.FirstNm = TryToParse<string>(dataRow["FIRST_NM"]);
            userBE.LastNm = TryToParse<string>(dataRow["LAST_NM"]);
            userBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            userBE.SrcSystemValueTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            userBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            userBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            userBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            userBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            userBE.IsActive = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            userBE.UiSettings = TryToParse<string>(dataRow["USER_INTERFACE_SETTING_TXT"]);
            userBE.IsExpressSched = TryToParse<bool?>(dataRow["IS_EXPRESS_SCHEDULED_IND"]);
            userBE.UserName = TryToParse<string>(dataRow["USER_NM"]);
            userBE.ADName = TryToParse<string>(dataRow["LAN_ID_TXT"]);
            userBE.Phone = TryToParse<string>(dataRow["PHONE_NUM"]);
            userBE.Email = TryToParse<string>(dataRow["EMAIL_ADDR_TXT"]);
            userBE.Notes = TryToParse<string>(dataRow["NOTES_TXT"]);
            userBE.IsSchedulable = TryToParse<bool?>(dataRow["IS_SCHEDULABLE_IND"]);
            userBE.PlanReviewOverrideHours = TryToParse<decimal?>(dataRow["PLAN_REVIEW_OVERRIDE_HOURS_NBR"]);
            userBE.HoursEstimated = TryToParse<string>(dataRow["HOURS_ESTIMATED_DESC"]);
            userBE.Jurisdiction = TryToParse<int?>(dataRow["JURISDICTION_TYP_ID"]);
            userBE.SchedulableLevel = TryToParse<string>(dataRow["SCHEDULABLE_LVL_DESC"]);
            userBE.IsPrelimMeetingAllowed = TryToParse<bool?>(dataRow["IS_PRELIM_MEETING_ALLOWED_IND"]);
            userBE.UserPrincipalName = TryToParse<string>(dataRow["USER_PRINCIPAL_NM"]);
            userBE.CalendarId = TryToParse<string>(dataRow["CALENDAR_ID"]);
            userBE.IsCity = TryToParse<bool?>(dataRow["CITY_IND"]);
            return userBE;

        }

        #endregion

    }

    #endregion

}