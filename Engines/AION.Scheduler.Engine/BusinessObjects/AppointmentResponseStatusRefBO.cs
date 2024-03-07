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

    #region BusinessObject - AppointmentResponseStatusRefBO

    public class AppointmentResponseStatusRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private AppointmentResponseStatusRefBE _appointmentResponseStatusRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(AppointmentResponseStatusRefBE appointmentResponseStatusRefBE)
        {
            int id;
            _appointmentResponseStatusRefBE = appointmentResponseStatusRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@APPT_RESPONSE_STATUS_DESC", appointmentResponseStatusRefBE.ApptResponseStatusDesc);
                sqlParameters[1] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", appointmentResponseStatusRefBE.EnumMappingValNbr);
                sqlParameters[2] = new SqlParameter("@ACTIVE_IND", appointmentResponseStatusRefBE.ActiveInd);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", appointmentResponseStatusRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_appointment_response_status_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_appointment_response_status_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public AppointmentResponseStatusRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            AppointmentResponseStatusRefBE appointmentResponseStatusRefBE = new AppointmentResponseStatusRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_response_status_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                appointmentResponseStatusRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return appointmentResponseStatusRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_response_status_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<AppointmentResponseStatusRefBE> GetList()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<AppointmentResponseStatusRefBE> appointmentResponseStatusRefBEList = new List<AppointmentResponseStatusRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_appointment_response_status_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    appointmentResponseStatusRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return appointmentResponseStatusRefBEList;

        }
        public int Update(AppointmentResponseStatusRefBE appointmentResponseStatusRefBE)
        {
            int rows;
            _appointmentResponseStatusRefBE = appointmentResponseStatusRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@APPT_RESPONSE_STATUS_REF_ID", appointmentResponseStatusRefBE.ApptResponseStatusRefId);
                sqlParameters[1] = new SqlParameter("@APPT_RESPONSE_STATUS_DESC", appointmentResponseStatusRefBE.ApptResponseStatusDesc);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", appointmentResponseStatusRefBE.EnumMappingValNbr);
                sqlParameters[3] = new SqlParameter("@ACTIVE_IND", appointmentResponseStatusRefBE.ActiveInd);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", appointmentResponseStatusRefBE.UpdatedDate);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", appointmentResponseStatusRefBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_appointment_response_status_ref", base.ConnectionString, ref sqlParameters);

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

        private AppointmentResponseStatusRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            AppointmentResponseStatusRefBE appointmentResponseStatusRefBE = new AppointmentResponseStatusRefBE();

            appointmentResponseStatusRefBE.ApptResponseStatusRefId = TryToParse<int?>(dataRow["APPT_RESPONSE_STATUS_REF_ID"]);
            appointmentResponseStatusRefBE.ApptResponseStatusDesc = TryToParse<string>(dataRow["APPT_RESPONSE_STATUS_DESC"]);
            appointmentResponseStatusRefBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            appointmentResponseStatusRefBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            appointmentResponseStatusRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            appointmentResponseStatusRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            appointmentResponseStatusRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            appointmentResponseStatusRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return appointmentResponseStatusRefBE;

        }

        #endregion

    }

    #endregion

}