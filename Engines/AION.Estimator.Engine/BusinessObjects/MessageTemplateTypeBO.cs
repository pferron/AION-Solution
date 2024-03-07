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

    #region BusinessObject - TemplateTypeBO

    public class MessageTemplateTypeBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private MessageTemplateTypeBE _templateTypeBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(MessageTemplateTypeBE templateTypeBE)
        {
            int id;
            _templateTypeBE = templateTypeBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@TEMPLATE_TYP_NM", templateTypeBE.TemplateTypeName);
                sqlParameters[1] = new SqlParameter("@TEMPLATE_TYP_DESC", templateTypeBE.TemplateTypeDesc);
                sqlParameters[2] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", templateTypeBE.EnumMappingValNbr);
                sqlParameters[3] = new SqlParameter("@TEMPLATE_MODULE_ID", templateTypeBE.TemplateModuleId);
                sqlParameters[4] = new SqlParameter("@TEMPLATE_TYP_EDITABLE_IND", templateTypeBE.IsEditable);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", templateTypeBE.UserId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_template_type", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_template_type", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public MessageTemplateTypeBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            MessageTemplateTypeBE templateTypeBE = new MessageTemplateTypeBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_type_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                templateTypeBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return templateTypeBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_type_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        /// <summary>
        /// Get list of template types by module id - returns only editable templates
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MessageTemplateTypeBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<MessageTemplateTypeBE> templateTypeBEList = new List<MessageTemplateTypeBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@moduleid", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_template_type_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    templateTypeBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return templateTypeBEList;

        }

        public int Update(MessageTemplateTypeBE templateTypeBE)
        {
            int rows;
            _templateTypeBE = templateTypeBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@TEMPLATE_TYP_ID", templateTypeBE.TemplateTypeId);
                sqlParameters[1] = new SqlParameter("@TEMPLATE_TYP_NM", templateTypeBE.TemplateTypeName);
                sqlParameters[2] = new SqlParameter("@TEMPLATE_TYP_DESC", templateTypeBE.TemplateTypeDesc);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", templateTypeBE.UpdatedDate);
                sqlParameters[4] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", templateTypeBE.EnumMappingValNbr);
                sqlParameters[5] = new SqlParameter("@TEMPLATE_MODULE_ID", templateTypeBE.TemplateModuleId);
                sqlParameters[6] = new SqlParameter("@TEMPLATE_TYP_EDITABLE_IND", templateTypeBE.IsEditable);

                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", templateTypeBE.UserId);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_template_type", base.ConnectionString, ref sqlParameters);

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

        private MessageTemplateTypeBE ConvertDataRowToBE(DataRow dataRow)
        {
            MessageTemplateTypeBE templateTypeBE = new MessageTemplateTypeBE();

            templateTypeBE.TemplateTypeId = TryToParse<int?>(dataRow["TEMPLATE_TYP_ID"]);
            templateTypeBE.TemplateTypeName = TryToParse<string>(dataRow["TEMPLATE_TYP_NM"]);
            templateTypeBE.TemplateTypeDesc = TryToParse<string>(dataRow["TEMPLATE_TYP_DESC"]);
            templateTypeBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            templateTypeBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            templateTypeBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            templateTypeBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            templateTypeBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            templateTypeBE.TemplateModuleId = TryToParse<int?>(dataRow["TEMPLATE_MODULE_ID"]);
            templateTypeBE.IsEditable = TryToParse<bool?>(dataRow["TEMPLATE_TYP_EDITABLE_IND"]);

            return templateTypeBE;

        }

        #endregion

    }

    #endregion

}