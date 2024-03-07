#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Engine.BusinessObjects
{

    #region BusinessObject - NotificationEmailListBO

    public class NotificationEmailListBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private NotificationEmailListBE _notificationEmailListBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(NotificationEmailListBE notificationEmailListBE)
        {
            int id;
            _notificationEmailListBE = notificationEmailListBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PROJECT_EMAIL_NOTIFICATION_ID", notificationEmailListBE.ProjectEmailNotificationId);
                sqlParameters[1] = new SqlParameter("@USER_ID", notificationEmailListBE.SendUserId);
                sqlParameters[2] = new SqlParameter("@EMAIL_ADDR_TXT", notificationEmailListBE.EmailAddressTxt);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", notificationEmailListBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_notification_email_list", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_notification_email_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        /// <summary>
        /// Email list by NotificationEmailListId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NotificationEmailListBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            NotificationEmailListBE notificationEmailListBE = new NotificationEmailListBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notification_email_list_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                notificationEmailListBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return notificationEmailListBE;
        }

        /// <summary>
        /// Email list by ProjectEmailNotificationId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notification_email_list_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        /// <summary>
        /// Email list by ProjectEmailNotificationId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<NotificationEmailListBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<NotificationEmailListBE> notificationEmailListBEList = new List<NotificationEmailListBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notification_email_list_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    notificationEmailListBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return notificationEmailListBEList;

        }

        public int Update(NotificationEmailListBE notificationEmailListBE)
        {
            int rows;
            _notificationEmailListBE = notificationEmailListBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@NOTIFICATION_EMAIL_LIST_ID", notificationEmailListBE.NotificationEmailListId);
                sqlParameters[1] = new SqlParameter("@PROJECT_EMAIL_NOTIFICATION_ID", notificationEmailListBE.ProjectEmailNotificationId);
                sqlParameters[2] = new SqlParameter("@USER_ID", notificationEmailListBE.UserId);
                sqlParameters[3] = new SqlParameter("@EMAIL_ADDR_TXT", notificationEmailListBE.EmailAddressTxt);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", notificationEmailListBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", notificationEmailListBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_notification_email_list", base.ConnectionString, ref sqlParameters);

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

        private NotificationEmailListBE ConvertDataRowToBE(DataRow dataRow)
        {
            NotificationEmailListBE notificationEmailListBE = new NotificationEmailListBE();

            notificationEmailListBE.NotificationEmailListId = TryToParse<int?>(dataRow["NOTIFICATION_EMAIL_LIST_ID"]);
            notificationEmailListBE.ProjectEmailNotificationId = TryToParse<int?>(dataRow["PROJECT_EMAIL_NOTIFICATION_ID"]);
            notificationEmailListBE.SendUserId = TryToParse<int?>(dataRow["USER_ID"]);
            notificationEmailListBE.EmailAddressTxt = TryToParse<string>(dataRow["EMAIL_ADDR_TXT"]);
            notificationEmailListBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            notificationEmailListBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            notificationEmailListBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            notificationEmailListBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return notificationEmailListBE;

        }

        #endregion

    }

    #endregion

}