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

    #region BusinessObject - MeetingRoomRefBO

    public class MeetingRoomRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private MeetingRoomRefBE _meetingRoomRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(MeetingRoomRefBE meetingRoomRefBE)
        {
            int id;
            _meetingRoomRefBE = meetingRoomRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@MEETING_ROOM_REF_ID", meetingRoomRefBE.MeetingRoomRefID);
                sqlParameters[1] = new SqlParameter("@MEETING_ROOM_NM", meetingRoomRefBE.MeetingRoomName);
                sqlParameters[2] = new SqlParameter("@ACTIVE_IND", meetingRoomRefBE.IsActive);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", meetingRoomRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_meeting_room_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_meeting_room_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public MeetingRoomRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            MeetingRoomRefBE meetingRoomRefBE = new MeetingRoomRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_meeting_room_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                meetingRoomRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return meetingRoomRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_meeting_room_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<MeetingRoomRefBE> GetList(string meetingType = null)
        {
            if (string.IsNullOrEmpty(meetingType)) meetingType = null;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<MeetingRoomRefBE> meetingRoomRefBEList = new List<MeetingRoomRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@MEETING_TYPE", meetingType);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_meeting_room_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    meetingRoomRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return meetingRoomRefBEList;

        }

        public int Update(MeetingRoomRefBE meetingRoomRefBE)
        {
            int rows;
            _meetingRoomRefBE = meetingRoomRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@MEETING_ROOM_REF_ID", meetingRoomRefBE.MeetingRoomRefID);
                sqlParameters[1] = new SqlParameter("@MEETING_ROOM_NM", meetingRoomRefBE.MeetingRoomName);
                sqlParameters[2] = new SqlParameter("@USER_PRINCIPAL_NM", meetingRoomRefBE.UserPrincipalName);
                sqlParameters[3] = new SqlParameter("@CALENDAR_ID", meetingRoomRefBE.CalendarId);
                sqlParameters[4] = new SqlParameter("@ACTIVE_IND", meetingRoomRefBE.IsActive);
                sqlParameters[5] = new SqlParameter("@UPDATED_DTTM", meetingRoomRefBE.UpdatedDate);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", meetingRoomRefBE.UserId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_meeting_room_ref", base.ConnectionString, ref sqlParameters);

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

        private MeetingRoomRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            MeetingRoomRefBE meetingRoomRefBE = new MeetingRoomRefBE();

            meetingRoomRefBE.MeetingRoomRefID = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            meetingRoomRefBE.MeetingRoomName = TryToParse<string>(dataRow["MEETING_ROOM_NM"]);
            meetingRoomRefBE.MeetingRoomEmail = TryToParse<string>(dataRow["MEETING_ROOM_EMAIL_ADDR_TXT"]);
            meetingRoomRefBE.IsActive = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            meetingRoomRefBE.UserPrincipalName = TryToParse<string>(dataRow["USER_PRINCIPAL_NM"]);
            meetingRoomRefBE.CalendarId = TryToParse<string>(dataRow["CALENDAR_ID"]);
            meetingRoomRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            meetingRoomRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            meetingRoomRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            meetingRoomRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return meetingRoomRefBE;

        }

        #endregion

    }

    #endregion

}