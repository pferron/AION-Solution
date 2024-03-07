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

    #region BusinessObject - AuditActionRefBO

    public class AuditActionRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, GetByName };

        private string _errorMsg;

        private AuditActionRefBE _auditActionRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(AuditActionRefBE auditActionRefBE)
        {
            int id;
            _auditActionRefBE = auditActionRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@AUDIT_ACTION_NM", auditActionRefBE.AuditActionName);
                sqlParameters[1] = new SqlParameter("@AUDIT_ACTION_DESC", auditActionRefBE.AuditActionDesc);

                sqlParameters[2] = new SqlParameter("@WKR_ID_TXT", auditActionRefBE.UserId);

                sqlParameters[3] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[3].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_audit_action_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_audit_action_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public AuditActionRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            AuditActionRefBE auditActionRefBE = new AuditActionRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_audit_action_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                auditActionRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return auditActionRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_audit_action_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<AuditActionRefBE> GetList()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<AuditActionRefBE> auditActionRefBEList = new List<AuditActionRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_audit_action_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    auditActionRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return auditActionRefBEList;

        }

        public int Update(AuditActionRefBE auditActionRefBE)
        {
            int rows;
            _auditActionRefBE = auditActionRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@AUDIT_ACTION_REF_ID", auditActionRefBE.AuditActionRefId);
                sqlParameters[1] = new SqlParameter("@AUDIT_ACTION_NM", auditActionRefBE.AuditActionName);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", auditActionRefBE.UpdatedDate);
                sqlParameters[3] = new SqlParameter("@AUDIT_ACTION_DESC", auditActionRefBE.AuditActionDesc);

                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", auditActionRefBE.UserId);

                sqlParameters[5] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[5].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_audit_action_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }
        public AuditActionRefBE GetByName(string refname)
        {
            if (!this.Validate(ActionType.GetByName))
                throw (new Exception(_errorMsg));

            AuditActionRefBE auditActionRefBE = new AuditActionRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@AUDIT_ACTION_NM", refname);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_audit_action_ref_get_by_name", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                auditActionRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return auditActionRefBE;
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

                case ActionType.GetByName:
                    return (_errorMsg == String.Empty);
                default:
                    break;
            }

            return true;

        }

        private AuditActionRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            AuditActionRefBE auditActionRefBE = new AuditActionRefBE();

            auditActionRefBE.AuditActionRefId = TryToParse<int?>(dataRow["AUDIT_ACTION_REF_ID"]);
            auditActionRefBE.AuditActionName = TryToParse<string>(dataRow["AUDIT_ACTION_NM"]);
            auditActionRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            auditActionRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            auditActionRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            auditActionRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            auditActionRefBE.AuditActionDesc = TryToParse<string>(dataRow["AUDIT_ACTION_DESC"]);

            return auditActionRefBE;

        }

        #endregion

    }

    #endregion

}