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

    #region BusinessObject - FacilitatorMeetingApptDetailBO

    public class FacilitatorMeetingApptDetailBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private FacilitatorMeetingApptDetailBE _facilitatorMeetingApptDetailBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(FacilitatorMeetingApptDetailBE facilitatorMeetingApptDetailBE)
        {
            int id;
            _facilitatorMeetingApptDetailBE = facilitatorMeetingApptDetailBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@FACILITATOR_MEETING_APPT_IDENTIFIER", facilitatorMeetingApptDetailBE.FacilitatorMeetingApptId);
                sqlParameters[1] = new SqlParameter("@BUSINESS_REF_ID", facilitatorMeetingApptDetailBE.BusinessRefId);
                sqlParameters[2] = new SqlParameter("@ASSIGNED_PLAN_REVIEWER_ID", facilitatorMeetingApptDetailBE.AssignedPlanReviewerId);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", facilitatorMeetingApptDetailBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_facilitator_meeting_appt_detail", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_facilitator_meeting_appt_detail", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }
        public int DeleteByFMAId(int FMAId)
        {
            int rows;
            _id = FMAId;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@FACILITATOR_MEETING_APPT_IDENTIFIER", FMAId);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_facilitator_meeting_appt_detail_byFMAId", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }
        public FacilitatorMeetingApptDetailBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            FacilitatorMeetingApptDetailBE facilitatorMeetingApptDetailBE = new FacilitatorMeetingApptDetailBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appt_detail_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                facilitatorMeetingApptDetailBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return facilitatorMeetingApptDetailBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appt_detail_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<FacilitatorMeetingApptDetailBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<FacilitatorMeetingApptDetailBE> facilitatorMeetingApptDetailBEList = new List<FacilitatorMeetingApptDetailBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_facilitator_meeting_appt_detail_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    facilitatorMeetingApptDetailBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return facilitatorMeetingApptDetailBEList;

        }

        public int Update(FacilitatorMeetingApptDetailBE facilitatorMeetingApptDetailBE)
        {
            int rows;
            _facilitatorMeetingApptDetailBE = facilitatorMeetingApptDetailBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@FACILITATOR_MEETING_APPT_DETAIL_ID", facilitatorMeetingApptDetailBE.FacilitatorMeetingApptDetailId);
                sqlParameters[1] = new SqlParameter("@FACILITATOR_MEETING_APPT_IDENTIFIER", facilitatorMeetingApptDetailBE.FacilitatorMeetingApptId);
                sqlParameters[2] = new SqlParameter("@BUSINESS_REF_ID", facilitatorMeetingApptDetailBE.BusinessRefId);
                sqlParameters[3] = new SqlParameter("@ASSIGNED_PLAN_REVIEWER_ID", facilitatorMeetingApptDetailBE.AssignedPlanReviewerId);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", facilitatorMeetingApptDetailBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", facilitatorMeetingApptDetailBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_facilitator_meeting_appt_detail", base.ConnectionString, ref sqlParameters);

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

        private FacilitatorMeetingApptDetailBE ConvertDataRowToBE(DataRow dataRow)
        {
            FacilitatorMeetingApptDetailBE facilitatorMeetingApptDetailBE = new FacilitatorMeetingApptDetailBE();

            facilitatorMeetingApptDetailBE.FacilitatorMeetingApptDetailId = TryToParse<int?>(dataRow["FACILITATOR_MEETING_APPT_DETAIL_ID"]);
            facilitatorMeetingApptDetailBE.FacilitatorMeetingApptId = TryToParse<int?>(dataRow["FACILITATOR_MEETING_APPT_IDENTIFIER"]);
            facilitatorMeetingApptDetailBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
            facilitatorMeetingApptDetailBE.AssignedPlanReviewerId = TryToParse<int?>(dataRow["ASSIGNED_PLAN_REVIEWER_ID"]);
            facilitatorMeetingApptDetailBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            facilitatorMeetingApptDetailBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            facilitatorMeetingApptDetailBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            facilitatorMeetingApptDetailBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return facilitatorMeetingApptDetailBE;

        }

        #endregion

    }

    #endregion

}