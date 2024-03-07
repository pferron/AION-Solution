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

    #region BusinessObject - ProjectEmailNotificationBO

    public class ProjectEmailNotificationBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectEmailNotificationBE _projectEmailNotificationBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectEmailNotificationBE projectEmailNotificationBE)
        {
            int id;
            _projectEmailNotificationBE = projectEmailNotificationBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectEmailNotificationBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@EMAIL_TYP_DESC", projectEmailNotificationBE.EmailTypeDesc);
                sqlParameters[2] = new SqlParameter("@EMAIL_SUBJECT_TXT", projectEmailNotificationBE.EmailSubjectText);
                sqlParameters[3] = new SqlParameter("@EMAIL_BODY_TXT", projectEmailNotificationBE.EmailBodyTxt);
                sqlParameters[4] = new SqlParameter("@EMAIL_SENT_DT", projectEmailNotificationBE.EmailSentDt);
                sqlParameters[5] = new SqlParameter("@SENDER_USER_ID", projectEmailNotificationBE.SenderUserId);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", projectEmailNotificationBE.UserId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_email_notification", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_email_notification", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        /// <summary>
        /// Get by ProjectNotificationEmailId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectEmailNotificationBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectEmailNotificationBE projectEmailNotificationBE = new ProjectEmailNotificationBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_email_notification_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectEmailNotificationBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectEmailNotificationBE;
        }

        /// <summary>
        /// Get by ProjectId
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_email_notification_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        /// <summary>
        /// Get by ProjectId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ProjectEmailNotificationBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectEmailNotificationBE> projectEmailNotificationBEList = new List<ProjectEmailNotificationBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_email_notification_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectEmailNotificationBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectEmailNotificationBEList;

        }

        public int Update(ProjectEmailNotificationBE projectEmailNotificationBE)
        {
            int rows;
            _projectEmailNotificationBE = projectEmailNotificationBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];

                sqlParameters[0] = new SqlParameter("@PROJECT_EMAIL_NOTIFICATION_ID", projectEmailNotificationBE.ProjectEmailNotificationId);
                sqlParameters[1] = new SqlParameter("@PROJECT_ID", projectEmailNotificationBE.ProjectId);
                sqlParameters[2] = new SqlParameter("@EMAIL_TYP_DESC", projectEmailNotificationBE.EmailTypeDesc);
                sqlParameters[3] = new SqlParameter("@EMAIL_SUBJECT_TXT", projectEmailNotificationBE.EmailSubjectText);
                sqlParameters[4] = new SqlParameter("@EMAIL_BODY_TXT", projectEmailNotificationBE.EmailBodyTxt);
                sqlParameters[5] = new SqlParameter("@EMAIL_SENT_DT", projectEmailNotificationBE.EmailSentDt);
                sqlParameters[6] = new SqlParameter("@SENDER_USER_ID", projectEmailNotificationBE.SenderUserId);
                sqlParameters[7] = new SqlParameter("@UPDATED_DTTM", projectEmailNotificationBE.UpdatedDate);

                sqlParameters[8] = new SqlParameter("@WKR_ID_TXT", projectEmailNotificationBE.UserId);

                sqlParameters[9] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[9].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_email_notification", base.ConnectionString, ref sqlParameters);

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

        private ProjectEmailNotificationBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectEmailNotificationBE projectEmailNotificationBE = new ProjectEmailNotificationBE();

            projectEmailNotificationBE.ProjectEmailNotificationId = TryToParse<int?>(dataRow["PROJECT_EMAIL_NOTIFICATION_ID"]);
            projectEmailNotificationBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            projectEmailNotificationBE.EmailTypeDesc = TryToParse<string>(dataRow["EMAIL_TYP_DESC"]);
            projectEmailNotificationBE.EmailSubjectText = TryToParse<string>(dataRow["EMAIL_SUBJECT_TXT"]);
            projectEmailNotificationBE.EmailBodyTxt = TryToParse<string>(dataRow["EMAIL_BODY_TXT"]);
            projectEmailNotificationBE.EmailSentDt = TryToParse<DateTime?>(dataRow["EMAIL_SENT_DT"]);
            projectEmailNotificationBE.SenderUserId = TryToParse<int?>(dataRow["SENDER_USER_ID"]);
            projectEmailNotificationBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectEmailNotificationBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectEmailNotificationBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectEmailNotificationBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return projectEmailNotificationBE;

        }

        #endregion

    }

    #endregion

}