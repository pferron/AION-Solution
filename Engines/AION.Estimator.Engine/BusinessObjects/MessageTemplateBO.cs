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

    #region BusinessObject - TemplateBO

    public class MessageTemplateBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private MessageTemplateBE _templateBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(MessageTemplateBE templateBE)
        {
            int id;
            _templateBE = templateBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@TEMPLATE_NM", templateBE.TemplateName);
                sqlParameters[1] = new SqlParameter("@TEMPLATE_TYP_ID", templateBE.TemplateTypeId);
                sqlParameters[2] = new SqlParameter("@TEMPLATE_TXT", templateBE.TemplateText);
                sqlParameters[3] = new SqlParameter("@ACTIVE_IND", templateBE.ActiveInd);
                sqlParameters[4] = new SqlParameter("@ACTIVE_DT", templateBE.ActiveDt);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", templateBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_template", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_template", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public MessageTemplateBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            MessageTemplateBE templateBE = new MessageTemplateBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                templateBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return templateBE;
        }

        public MessageTemplateBE GetActiveByTypeId(int id, DateTime dateTime)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            MessageTemplateBE templateBE = new MessageTemplateBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@templatetypeenumid", id);
                sqlParameters[1] = new SqlParameter("@dtfilter", dateTime);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_activebytype", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                templateBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return templateBE;
        }

        public List<MessageTemplateBE> GetListByTypeId(int templateTypeId)
        {
            _id = templateTypeId;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<MessageTemplateBE> templateBEList = new List<MessageTemplateBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@templatetypeid", templateTypeId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_list_bytypeid", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    templateBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return templateBEList;

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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">This is the MessageTemplateTypeEnum int == enum_mapping_val_nbr in the db</param>
        /// <returns></returns>
        public List<MessageTemplateBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<MessageTemplateBE> templateBEList = new List<MessageTemplateBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    templateBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return templateBEList;

        }

        public int Update(MessageTemplateBE templateBE)
        {
            int rows;
            _templateBE = templateBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@TEMPLATE_ID", templateBE.TemplateId);
                sqlParameters[1] = new SqlParameter("@TEMPLATE_NM", templateBE.TemplateName);
                sqlParameters[2] = new SqlParameter("@TEMPLATE_TYP_ID", templateBE.TemplateTypeId);
                sqlParameters[3] = new SqlParameter("@TEMPLATE_TXT", templateBE.TemplateText);
                sqlParameters[4] = new SqlParameter("@ACTIVE_IND", templateBE.ActiveInd);
                sqlParameters[5] = new SqlParameter("@ACTIVE_DT", templateBE.ActiveDt);
                sqlParameters[6] = new SqlParameter("@UPDATED_DTTM", templateBE.UpdatedDate);

                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", templateBE.UserId);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_template", base.ConnectionString, ref sqlParameters);

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

        private MessageTemplateBE ConvertDataRowToBE(DataRow dataRow)
        {
            MessageTemplateBE templateBE = new MessageTemplateBE();

            templateBE.TemplateId = TryToParse<int?>(dataRow["TEMPLATE_ID"]);
            templateBE.TemplateName = TryToParse<string>(dataRow["TEMPLATE_NM"]);
            templateBE.TemplateTypeId = TryToParse<int?>(dataRow["TEMPLATE_TYP_ID"]);
            templateBE.TemplateText = TryToParse<string>(dataRow["TEMPLATE_TXT"]);
            templateBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            templateBE.ActiveDt = TryToParse<DateTime?>(dataRow["ACTIVE_DT"]);
            templateBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            templateBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            templateBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            templateBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return templateBE;

        }

        #endregion

    }

    #endregion

}