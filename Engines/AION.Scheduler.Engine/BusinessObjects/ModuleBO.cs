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

    #region BusinessObject - ModuleBO

    public class ModuleBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ModuleBE _moduleBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ModuleBE moduleBE)
        {
            int id;
            _moduleBE = moduleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@MODULE_NM", moduleBE.ModuleName);
                sqlParameters[1] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", moduleBE.EnumMappingNumber);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", moduleBE.UserId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_module", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_module", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ModuleBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ModuleBE moduleBE = new ModuleBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_module_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                moduleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return moduleBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_module_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<ModuleBE> GetList()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ModuleBE> moduleBEList = new List<ModuleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_module_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    moduleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return moduleBEList;

        }

        public int Update(ModuleBE moduleBE)
        {
            int rows;
            _moduleBE = moduleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@MODULE_REF_ID", moduleBE.ModuleId);
                sqlParameters[1] = new SqlParameter("@MODULE_NM", moduleBE.ModuleName);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", moduleBE.UpdatedDate);
                sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", moduleBE.EnumMappingNumber);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", moduleBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_module", base.ConnectionString, ref sqlParameters);

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

        private ModuleBE ConvertDataRowToBE(DataRow dataRow)
        {
            ModuleBE moduleBE = new ModuleBE();

            moduleBE.ModuleId = TryToParse<int?>(dataRow["MODULE_REF_ID"]);
            moduleBE.ModuleName = TryToParse<string>(dataRow["MODULE_NM"]);
            moduleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            moduleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            moduleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            moduleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            moduleBE.EnumMappingNumber = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);

            return moduleBE;

        }

        #endregion

    }

    #endregion

}