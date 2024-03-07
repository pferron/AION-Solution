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

    #region BusinessObject - SystemRoleBO

    public class SystemRoleBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetByExternalSystemRef, GetDataSet, GetList, Update };

        private string _errorMsg;

        private SystemRoleBE _systemRoleBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(SystemRoleBE systemRoleBE)
        {
            int id;
            _systemRoleBE = systemRoleBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];

                sqlParameters[0] = new SqlParameter("@SYSTEM_ROLE_NM", systemRoleBE.SystemRoleNm);
                sqlParameters[1] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", systemRoleBE.ExternalSystemRefId);
                sqlParameters[2] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", systemRoleBE.SrcSystemValTxt);
                sqlParameters[3] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", systemRoleBE.EnumMappingValNbr);
                sqlParameters[4] = new SqlParameter("@SYSTEM_ROLE_TXT", systemRoleBE.SystemRoleTxt);
                sqlParameters[5] = new SqlParameter("@ROLE_OPTIONS_TXT", systemRoleBE.RoleOptionsTxt);
                sqlParameters[6] = new SqlParameter("@ENABLED_IND", systemRoleBE.EnabledInd);
                sqlParameters[7] = new SqlParameter("@PARENT_SYSTEM_ROLE_ID", systemRoleBE.ParentSystemRoleId);

                sqlParameters[8] = new SqlParameter("@WKR_ID_TXT", systemRoleBE.UserId);

                sqlParameters[9] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[9].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_system_role", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_system_role", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public SystemRoleBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            SystemRoleBE systemRoleBE = new SystemRoleBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_system_role_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                systemRoleBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return systemRoleBE;
        }


        public List<SystemRoleBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<SystemRoleBE> systemRoleBEList = new List<SystemRoleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_system_role_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    systemRoleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return systemRoleBEList;

        }


        public List<SystemRoleBE> GetAllList()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<SystemRoleBE> systemRoleBEList = new List<SystemRoleBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_system_role_get_Alllist", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    systemRoleBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return systemRoleBEList;

        }

        public int Update(SystemRoleBE systemRoleBE)
        {
            int rows;
            _systemRoleBE = systemRoleBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[12];

                sqlParameters[0] = new SqlParameter("@SYSTEM_ROLE_ID", systemRoleBE.SystemRoleId);
                sqlParameters[1] = new SqlParameter("@SYSTEM_ROLE_NM", systemRoleBE.SystemRoleNm);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", systemRoleBE.UpdatedDate);
                sqlParameters[3] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", systemRoleBE.ExternalSystemRefId);
                sqlParameters[4] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", systemRoleBE.SrcSystemValTxt);
                sqlParameters[5] = new SqlParameter("@ENUM_MAPPING_VAL_NBR", systemRoleBE.EnumMappingValNbr);
                sqlParameters[6] = new SqlParameter("@SYSTEM_ROLE_TXT", systemRoleBE.SystemRoleTxt);
                sqlParameters[7] = new SqlParameter("@ROLE_OPTIONS_TXT", systemRoleBE.RoleOptionsTxt);
                sqlParameters[8] = new SqlParameter("@ENABLED_IND", systemRoleBE.EnabledInd);
                sqlParameters[9] = new SqlParameter("@PARENT_SYSTEM_ROLE_ID", systemRoleBE.ParentSystemRoleId);

                sqlParameters[10] = new SqlParameter("@WKR_ID_TXT", systemRoleBE.UserId);

                sqlParameters[11] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[11].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_system_role", base.ConnectionString, ref sqlParameters);

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

        private SystemRoleBE ConvertDataRowToBE(DataRow dataRow)
        {
            SystemRoleBE systemRoleBE = new SystemRoleBE();

            systemRoleBE.SystemRoleId = TryToParse<int?>(dataRow["SYSTEM_ROLE_ID"]);
            systemRoleBE.SystemRoleNm = TryToParse<string>(dataRow["SYSTEM_ROLE_NM"]);
            systemRoleBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            systemRoleBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            systemRoleBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            systemRoleBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            systemRoleBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            systemRoleBE.SrcSystemValTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            systemRoleBE.EnumMappingValNbr = TryToParse<int?>(dataRow["ENUM_MAPPING_VAL_NBR"]);
            systemRoleBE.EnabledInd = TryToParse<bool?>(dataRow["ENABLED_IND"]);
            systemRoleBE.RoleOptionsTxt = TryToParse<string>(dataRow["ROLE_OPTIONS_TXT"]) == null ? "" : TryToParse<string>(dataRow["ROLE_OPTIONS_TXT"]);
            systemRoleBE.ParentSystemRoleId = TryToParse<int?>(dataRow["PARENT_SYSTEM_ROLE_ID"]);
            return systemRoleBE;

        }

        #endregion

    }

    #endregion

}