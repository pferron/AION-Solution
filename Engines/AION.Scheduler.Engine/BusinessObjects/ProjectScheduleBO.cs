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

    #region BusinessObject - ProjectScheduleBO

    public class ProjectScheduleBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectScheduleBE _projectScheduleBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectScheduleBE projectScheduleBE)
        {
            int id;
            _projectScheduleBE = projectScheduleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PROJECT_SCHEDULE_TYP_DESC", projectScheduleBE.ProjectScheduleTypeRef);
                sqlParameters[1] = new SqlParameter("@APPT_ID", projectScheduleBE.AppoinmentID);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", projectScheduleBE.UserId);

                sqlParameters[3] = new SqlParameter("@RECURRING_APPT_DT", projectScheduleBE.RecurringApptDt);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_schedule", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_schedule", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectScheduleBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_schedule_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectScheduleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectScheduleBE;
        }
        public List<ProjectScheduleBE> GetByApptId(int id, string projectScheduleTypeDesc, List<int> scheduleIds = null)
        {
            _id = id;
            List<ProjectScheduleBE> ret = new List<ProjectScheduleBE>();

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            try
            {
                //get all the schedules.
                if (scheduleIds == null)
                {
                    ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();
                    DataSet dataSet;

                    SqlParameter[] sqlParameters = new SqlParameter[2];

                    sqlParameters[0] = new SqlParameter("@npaid", id);
                    sqlParameters[1] = new SqlParameter("@projectScheduleTypeDesc", projectScheduleTypeDesc);

                    dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_schedule_get_by_schedule_details", base.ConnectionString, ref sqlParameters);

                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in dataSet.Tables[0].Rows)
                        {
                            projectScheduleBE = this.ConvertDataRowToBE(item);
                            ret.Add(projectScheduleBE);
                        }
                    }
                }
                else
                {
                    foreach (var item in scheduleIds)
                    {
                        try
                        {
                            ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();
                            DataSet dataSet;

                            SqlParameter[] sqlParameters = new SqlParameter[3];

                            sqlParameters[0] = new SqlParameter("@npaid", id);
                            sqlParameters[1] = new SqlParameter("@projectScheduleTypeDesc", projectScheduleTypeDesc);
                            sqlParameters[2] = new SqlParameter("@scheduleid", item);

                            dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_schedule_get_by_schedule_details", base.ConnectionString, ref sqlParameters);

                            if (dataSet.Tables[0].Rows.Count > 0)
                            {
                                projectScheduleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
                            }
                            ret.Add(projectScheduleBE);
                        }
                        //try catch in for loop makes to continue even if one of them fail with returing the rest.
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return ret;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_schedule_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ProjectScheduleBE> GetList()
        {
           // _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectScheduleBE> projectScheduleBEList = new List<ProjectScheduleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                //sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_schedule_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectScheduleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectScheduleBEList;

        }

        public int Update(ProjectScheduleBE projectScheduleBE)
        {
            int rows;
            _projectScheduleBE = projectScheduleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@PROJECT_SCHEDULE_ID", projectScheduleBE.ProjectScheduleID);
                sqlParameters[1] = new SqlParameter("@PROJECT_SCHEDULE_TYP_DESC", projectScheduleBE.ProjectScheduleTypeRef);
                sqlParameters[2] = new SqlParameter("@APPT_ID", projectScheduleBE.AppoinmentID);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", projectScheduleBE.UpdatedDate);
                sqlParameters[4] = new SqlParameter("@RECURRING_APPT_DT", projectScheduleBE.RecurringApptDt);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", projectScheduleBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_schedule", base.ConnectionString, ref sqlParameters);

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

        private ProjectScheduleBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();

            projectScheduleBE.ProjectScheduleID = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);
            projectScheduleBE.ProjectScheduleTypeRef = TryToParse<string>(dataRow["PROJECT_SCHEDULE_TYP_DESC"]);
            projectScheduleBE.AppoinmentID = TryToParse<int?>(dataRow["APPT_ID"]);
            projectScheduleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectScheduleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectScheduleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectScheduleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectScheduleBE.RecurringApptDt = TryToParse<DateTime?>(dataRow["RECURRING_APPT_DT"]);

            return projectScheduleBE;

        }

        #endregion

    }

    #endregion

}