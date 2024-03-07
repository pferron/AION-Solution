#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Meck.Data;
using AION.Engine.BusinessEntities;
using AION.Base;

#endregion

namespace AIONEstimator.Engine.BusinessObjects
{
    public class ReserveExpressReservationBO: BaseBO
    {
        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ReserveExpressReservationBE _reserveExpressReservationBE;

        private int _id;

        #endregion

        public int Create(ReserveExpressReservationBE reserveExpressReservationBE)
        {
            int id;
            _reserveExpressReservationBE = reserveExpressReservationBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@RESERVE_EXPRESS_DT", reserveExpressReservationBE.ReserveExpressDt);
                sqlParameters[1] = new SqlParameter("@START_TM", reserveExpressReservationBE.StartTime);
                sqlParameters[2] = new SqlParameter("@END_TM", reserveExpressReservationBE.EndTime);
                sqlParameters[3] = new SqlParameter("@MEETING_ROOM_ID", reserveExpressReservationBE.MeetingRoomRefId);
                sqlParameters[4] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", reserveExpressReservationBE.ApptResponseStatusRefId);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", reserveExpressReservationBE.UserId);
                sqlParameters[6] = new SqlParameter("@CANCEL_AFTER_DT", reserveExpressReservationBE.CancelAfterDt);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_reserve_express_reservation_v2", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }


        public int GetExpressReservationCount()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            int reserveExpressReservationCount = 0;

            try
            {


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_reservation_get_list", base.ConnectionString);

                reserveExpressReservationCount = dataSet.Tables[0].Rows.Count;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return reserveExpressReservationCount;

        }

        public List<ReserveExpressReservationBE> GetList()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ReserveExpressReservationBE> reserveExpressReservationBEList = new List<ReserveExpressReservationBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];  

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_reservation_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    reserveExpressReservationBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return reserveExpressReservationBEList;

        }

        public ReserveExpressReservationBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ReserveExpressReservationBE reserveExpressReservationBE = new ReserveExpressReservationBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_reservation_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                reserveExpressReservationBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return reserveExpressReservationBE;
        }

        public List<ReserveExpressReservationBE> GetReservationByDate(DateTime fromDate, DateTime toDate)
        {
            DataSet dataSet;
            List<ReserveExpressReservationBE> ReserveExpressReservationBEList = new List<ReserveExpressReservationBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@FROM_DT", fromDate);
                sqlParameters[1] = new SqlParameter("@TO_DT", toDate);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_reservation_get_list_bydate", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReserveExpressReservationBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return ReserveExpressReservationBEList;
        }

        public List<ReserveExpressPlanReviewerSearchResultBE> GetReservationByDateV2(DateTime fromDate, DateTime toDate)
        {
            DataSet dataSet;
            List<ReserveExpressPlanReviewerSearchResultBE> ReserveExpressReservationBEList = new List<ReserveExpressPlanReviewerSearchResultBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@FROM_DT", fromDate);
                sqlParameters[1] = new SqlParameter("@TO_DT", toDate);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_reserve_express_reservation_get_list_bydate_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReserveExpressReservationBEList.Add(this.ConvertDataRowToSearchResultBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return ReserveExpressReservationBEList;
        }

        public int Update(ReserveExpressReservationBE reserveExpressReservationBE)
        {
            int rows;
            _reserveExpressReservationBE = reserveExpressReservationBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@RESERVE_EXPRESS_RESERVATION_ID", reserveExpressReservationBE.ReserveExpressReservationId);
                sqlParameters[1] = new SqlParameter("@MEETING_ROOM_REF_ID", reserveExpressReservationBE.MeetingRoomRefId);
              
                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_reserve_express_reservation", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int UpdateStatus(ReserveExpressReservationBE reserveExpressReservationBE)
        {
            int rows;
            _reserveExpressReservationBE = reserveExpressReservationBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@RESERVE_EXPRESS_RESERVATION_ID", reserveExpressReservationBE.ReserveExpressReservationId);
                sqlParameters[1] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", reserveExpressReservationBE.ApptResponseStatusRefId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_reserve_express_reservation_status", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_reserve_express_reservation", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public List<int> CancelReserveExpressReservation()
        {
            DataSet dataSet;
            List<int> cancelledAppointmentIds = new List<int>();

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                dataSet = SqlWrapper.RunSPReturnDS("usp_update_aion_cancel_reserve_express_reservation_v2", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    cancelledAppointmentIds.Add(TryToParse<int>(dataRow["RESERVE_EXPRESS_RESERVATION_ID"]));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return cancelledAppointmentIds;
        }

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

        private ReserveExpressReservationBE ConvertDataRowToBE(DataRow dataRow)
        {
            ReserveExpressReservationBE reserveExpressReservationBE = new ReserveExpressReservationBE();
            TimeSpan tstartTime = TimeSpan.Parse(dataRow["START_TM"].ToString());
            TimeSpan tendTime = TimeSpan.Parse(dataRow["END_TM"].ToString());
            reserveExpressReservationBE.ReserveExpressReservationId = TryToParse<int>(dataRow["RESERVE_EXPRESS_RESERVATION_ID"]);
            reserveExpressReservationBE.ReserveExpressDt = Convert.ToDateTime(dataRow["RESERVE_EXPRESS_DT"]);

            reserveExpressReservationBE.StartTime = Convert.ToDateTime(DateTime.Now.Date+ tstartTime);
            reserveExpressReservationBE.EndTime = Convert.ToDateTime(DateTime.Now.Date + tendTime);
            reserveExpressReservationBE.MeetingRoomRefId = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);

            reserveExpressReservationBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            reserveExpressReservationBE.CreatedDate = TryToParse<DateTime>(dataRow["CREATED_DTTM"]);
            reserveExpressReservationBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            reserveExpressReservationBE.UpdatedDate = TryToParse<DateTime>(dataRow["UPDATED_DTTM"]);
            reserveExpressReservationBE.CancelAfterDt = TryToParse<DateTime?>(dataRow["CANCEL_AFTER_DT"]);

            return reserveExpressReservationBE;
        }

        private ReserveExpressPlanReviewerSearchResultBE ConvertDataRowToSearchResultBE(DataRow dataRow)
        {
            ReserveExpressPlanReviewerSearchResultBE reserveExpressPlanReviewerSearchResultBE = new ReserveExpressPlanReviewerSearchResultBE();
            TimeSpan tstartTime = TimeSpan.Parse(dataRow["START_TM"].ToString());
            TimeSpan tendTime = TimeSpan.Parse(dataRow["END_TM"].ToString());
            reserveExpressPlanReviewerSearchResultBE.ReserveExpressReservationId = TryToParse<int>(dataRow["RESERVE_EXPRESS_RESERVATION_ID"]);
            reserveExpressPlanReviewerSearchResultBE.ReserveExpressDt = Convert.ToDateTime(dataRow["RESERVE_EXPRESS_DT"]);

            reserveExpressPlanReviewerSearchResultBE.AppointmentStartTime = Convert.ToDateTime(new DateTime() + tstartTime);
            reserveExpressPlanReviewerSearchResultBE.AppointmentEndTime = Convert.ToDateTime(new DateTime() + tendTime);
            reserveExpressPlanReviewerSearchResultBE.MeetingRoomRefId = TryToParse<int?>(dataRow["MEETING_ROOM_REF_ID"]);
            reserveExpressPlanReviewerSearchResultBE.ProjectScheduleId = TryToParse<int?>(dataRow["PROJECT_SCHEDULE_ID"]);


            return reserveExpressPlanReviewerSearchResultBE;
        }
    }
}
