#region Using

using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - MessageTemplateDataElementBO

    public class MessageTemplateDataElementBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private MessageTemplateDataElementBE _messageTemplateDataElementBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(MessageTemplateDataElementBE messageTemplateDataElementBE)
        {
            int id;
            _messageTemplateDataElementBE = messageTemplateDataElementBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@DATA_ELEMENT_NM", messageTemplateDataElementBE.DataElementName);
                sqlParameters[1] = new SqlParameter("@DATA_ELEMENT_DESC", messageTemplateDataElementBE.DataElementDesc);
                sqlParameters[2] = new SqlParameter("@VAL_TXT", messageTemplateDataElementBE.DataElementValTxt);
                sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", messageTemplateDataElementBE.EnumMappingValNbr);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", messageTemplateDataElementBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_data_element", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_data_element", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public MessageTemplateDataElementBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            MessageTemplateDataElementBE messageTemplateDataElementBE = new MessageTemplateDataElementBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_message_template_data_element_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                messageTemplateDataElementBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return messageTemplateDataElementBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_message_template_data_element_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<MessageTemplateDataElementBE> GetList()
        {
            DataSet dataSet;
            List<MessageTemplateDataElementBE> messageTemplateDataElementBEList = new List<MessageTemplateDataElementBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];


                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_message_template_data_element_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    messageTemplateDataElementBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return messageTemplateDataElementBEList;

        }

        public int Update(MessageTemplateDataElementBE messageTemplateDataElementBE)
        {
            int rows;
            _messageTemplateDataElementBE = messageTemplateDataElementBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@DATA_ELEMENT_ID", messageTemplateDataElementBE.DataElementId);
                sqlParameters[1] = new SqlParameter("@DATA_ELEMENT_NM", messageTemplateDataElementBE.DataElementName);
                sqlParameters[2] = new SqlParameter("@DATA_ELEMENT_DESC", messageTemplateDataElementBE.DataElementDesc);
                sqlParameters[3] = new SqlParameter("@VAL_TXT", messageTemplateDataElementBE.DataElementValTxt);
                sqlParameters[4] = new SqlParameter("@UPDATED_DTTM", messageTemplateDataElementBE.UpdatedDate);
                sqlParameters[5] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", messageTemplateDataElementBE.EnumMappingValNbr);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", messageTemplateDataElementBE.UserId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_data_element", base.ConnectionString, ref sqlParameters);

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

        private MessageTemplateDataElementBE ConvertDataRowToBE(DataRow dataRow)
        {
            MessageTemplateDataElementBE messageTemplateDataElementBE = new MessageTemplateDataElementBE();

            messageTemplateDataElementBE.DataElementId = TryToParse<int?>(dataRow["DATA_ELEMENT_ID"]);
            messageTemplateDataElementBE.DataElementName = TryToParse<string>(dataRow["DATA_ELEMENT_NM"]);
            messageTemplateDataElementBE.DataElementDesc = TryToParse<string>(dataRow["DATA_ELEMENT_DESC"]);
            messageTemplateDataElementBE.DataElementValTxt = TryToParse<string>(dataRow["VAL_TXT"]);
            messageTemplateDataElementBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            messageTemplateDataElementBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            messageTemplateDataElementBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            messageTemplateDataElementBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            messageTemplateDataElementBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);

            return messageTemplateDataElementBE;

        }

        #endregion

    }

    #endregion

}