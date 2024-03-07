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
    public class CustomerScheduledMeetingBO : BaseBO
    {
        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;


        #endregion

        public List<CustomerScheduledMeetingBE> GetById(string projectId)
        {


            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            List<CustomerScheduledMeetingBE> customerScheduledMeetingBEs = new List<CustomerScheduledMeetingBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@projectId", projectId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_get_custproject_scheduled_meeting_details_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                customerScheduledMeetingBEs.Add(this.ConvertMeetingAppointmentDataRowToBE(dataRow));
            }

            return customerScheduledMeetingBEs;
        }


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

        private CustomerScheduledMeetingBE ConvertMeetingAppointmentDataRowToBE(DataRow dataRow)
        {
            CustomerScheduledMeetingBE be = new CustomerScheduledMeetingBE();
            be.MeetingId = TryToParse<int>(dataRow["MEETING_ID"]);
            be.MeetingType = TryToParse<int?>(dataRow["MEETING_TYPE"]).HasValue ? TryToParse<int?>(dataRow["MEETING_TYPE"]).Value : 0;
            be.FromDt = TryToParse<DateTime?>(dataRow["FROM_DT"]).HasValue ? TryToParse<DateTime?>(dataRow["FROM_DT"]).Value : DateTime.Now;
            be.ToDt = TryToParse<DateTime?>(dataRow["TO_DT"]).HasValue ? TryToParse<DateTime?>(dataRow["TO_DT"]).Value : DateTime.Now;

            if (TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).HasValue)
            {
                be.AppBAgendaDue = TryToParse<DateTime?>(dataRow["AGENDA_DUE"]).Value;
            }
            else
            {
                be.AppBAgendaDue = null;
            }

            if (TryToParse<DateTime?>(dataRow["RESPONSE_DUE"]).HasValue)
            {
                be.ResponseDue = TryToParse<DateTime?>(dataRow["RESPONSE_DUE"]).Value;
            }
            else
            {
                be.ResponseDue = null;
            }

            be.ApptResponseStatusRefId = TryToParse<int>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            be.ApptCancellationRefId = TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]).HasValue ? TryToParse<int?>(dataRow["APPT_CANCELLATION_REF_ID"]).Value : 0;
            be.Attendees = TryToParse<string>(dataRow["ATTENDEES"]);
            return be;
        }
        #endregion
    }
}
