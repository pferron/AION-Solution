#region Using

using AION.Base;
using AION.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Engine.BusinessObjects
{

    #region BusinessObject - SchedulingLeadTimeReportBO

    public class SchedulingLeadTimeReportBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private SchedulingLeadTimeReportBE _schedulingLeadTimeReportBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(SchedulingLeadTimeReportBE schedulingLeadTimeReportBE)
        {
            int id;
            _schedulingLeadTimeReportBE = schedulingLeadTimeReportBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];

                sqlParameters[0] = new SqlParameter("@RPT_DTTM", schedulingLeadTimeReportBE.ReportGeneratedOn);
                sqlParameters[1] = new SqlParameter("@PROJECT_TYP_REF_ID", schedulingLeadTimeReportBE.ProjectTypeRefId);
                sqlParameters[2] = new SqlParameter("@BUSINESS_DIVISION_REF_ID", schedulingLeadTimeReportBE.BusinessDivisionRefId);
                sqlParameters[3] = new SqlParameter("@REQUIRED_PROJECT_HOURS_NBR", schedulingLeadTimeReportBE.RequiredProjectHours);
                sqlParameters[4] = new SqlParameter("@LEAD_TM_NBR", schedulingLeadTimeReportBE.LeadTimeDays);

                sqlParameters[5] = new SqlParameter("@DATE_RANGE_START_DT", schedulingLeadTimeReportBE.DateRangeStartDate);
                sqlParameters[6] = new SqlParameter("@DATE_RANGE_END_DT", schedulingLeadTimeReportBE.DateRangeEndDate);
                sqlParameters[7] = new SqlParameter("@PROJECT_LVL_TXT", schedulingLeadTimeReportBE.ProjectLevelTxt);

                sqlParameters[8] = new SqlParameter("@WKR_ID_TXT", schedulingLeadTimeReportBE.UserId);

                sqlParameters[9] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[9].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_scheduling_lead_time_report", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public bool DeleteAll()
        {

            try
            {
                SqlWrapper.RunSP("usp_delete_aion_scheduling_lead_time_report_all", base.ConnectionString);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return true;

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

        private SchedulingLeadTimeReportBE ConvertDataRowToBE(DataRow dataRow)
        {
            SchedulingLeadTimeReportBE schedulingLeadTimeReportBE = new SchedulingLeadTimeReportBE();

            schedulingLeadTimeReportBE.SchedulingLeadTimeReportId = TryToParse<int?>(dataRow["SCHEDULING_LEAD_TM_RPT_ID"]);
            schedulingLeadTimeReportBE.ReportGeneratedOn = TryToParse<DateTime?>(dataRow["RPT_DTTM"]);
            schedulingLeadTimeReportBE.ProjectTypeRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            schedulingLeadTimeReportBE.BusinessDivisionRefId = TryToParse<int?>(dataRow["BUSINESS_DIVISION_REF_ID"]);
            schedulingLeadTimeReportBE.RequiredProjectHours = TryToParse<int?>(dataRow["REQUIRED_PROJECT_HOURS_NBR"]);
            schedulingLeadTimeReportBE.LeadTimeDays = TryToParse<int?>(dataRow["LEAD_TM_NBR"]);
            schedulingLeadTimeReportBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            schedulingLeadTimeReportBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            schedulingLeadTimeReportBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            schedulingLeadTimeReportBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            schedulingLeadTimeReportBE.DateRangeStartDate = TryToParse<DateTime?>(dataRow["DATE_RANGE_START_DT"]);
            schedulingLeadTimeReportBE.DateRangeEndDate = TryToParse<DateTime?>(dataRow["DATE_RANGE_END_DT"]);
            schedulingLeadTimeReportBE.ProjectLevelTxt = TryToParse<string>(dataRow["PROJECT_LEVEL_TXT"]);

            return schedulingLeadTimeReportBE;

        }

        #endregion

    }

    #endregion

}