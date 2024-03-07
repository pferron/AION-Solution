#region Using

using AION.Base;
using AION.Scheduler.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Scheduler.Engine.BusinessObjects
{
    public class MessageTemplateAppointmentBO : BaseBO
    {
        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        #endregion
        #region Public Methods


        public List<MessageTemplateAppointmentBE> GetList(int projectid, string projectscheduletypdesc, int? meetingtyprefid)
        {

            DataSet dataSet;
            List<MessageTemplateAppointmentBE> messageTemplateAppointmentBEs = new List<MessageTemplateAppointmentBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@projectid", projectid);
                sqlParameters[1] = new SqlParameter("@projecttypdesc", projectscheduletypdesc);
                sqlParameters[2] = new SqlParameter("@meetingtyprefid", meetingtyprefid);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appt_byproject_bytype", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    messageTemplateAppointmentBEs.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return messageTemplateAppointmentBEs;

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

        private MessageTemplateAppointmentBE ConvertDataRowToBE(DataRow dataRow)
        {
            MessageTemplateAppointmentBE messageTemplateAppointmentBE = new MessageTemplateAppointmentBE();

            messageTemplateAppointmentBE.ProjectId = TryToParse<int>(dataRow["PROJECT_ID"]);
            messageTemplateAppointmentBE.ProjectScheduleTypDesc = TryToParse<string>(dataRow["PROJECT_SCHEDULE_TYP_DESC"]);
            messageTemplateAppointmentBE.MeetingTypRefId = TryToParse<int?>(dataRow["MEETING_TYP_REF_ID"]);
            messageTemplateAppointmentBE.RowType = TryToParse<string>(dataRow["ROW_TYPE"]);
            messageTemplateAppointmentBE.StartDt = TryToParse<DateTime?>(dataRow["START_DT"]);
            messageTemplateAppointmentBE.CycleNbr = TryToParse<int?>(dataRow["CYCLE_NBR"]);
            messageTemplateAppointmentBE.ScheduledUserId = TryToParse<int?>(dataRow["SCHEDULED_USER_ID"]);
            messageTemplateAppointmentBE.MeetingRoomRefId = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            messageTemplateAppointmentBE.PendingNote = TryToParse<string>(dataRow["PENDING_NOTE"]);
            return messageTemplateAppointmentBE;

        }

        #endregion


    }
}
