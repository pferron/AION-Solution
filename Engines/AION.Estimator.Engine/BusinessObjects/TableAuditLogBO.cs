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

    #region BusinessObject - TableAuditLogBO

    public class TableAuditLogBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private TableAuditLogBE _tableAuditLogBE;

        private int _id;
        private DateTime _startdate;
        private DateTime _enddate;
        #endregion

        #region Public Methods

        public int Create(TableAuditLogBE tableAuditLogBE)
        {
            int id;
            _tableAuditLogBE = tableAuditLogBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@AUDIT_TYP_CD", tableAuditLogBE.AuditTypeCd);
                sqlParameters[1] = new SqlParameter("@AUDIT_TABLE_NM", tableAuditLogBE.AuditTableName);
                sqlParameters[2] = new SqlParameter("@AUDIT_TABLE_PK_ID", tableAuditLogBE.AuditTablePkId);
                sqlParameters[3] = new SqlParameter("@AUDIT_FIELD_NM", tableAuditLogBE.AuditFieldNm);
                sqlParameters[4] = new SqlParameter("@OLD_VAL_TXT", tableAuditLogBE.OldValTxt);
                sqlParameters[5] = new SqlParameter("@NEW_VAL_TXT", tableAuditLogBE.NewValTxt);

                sqlParameters[6] = new SqlParameter("@WKR_ID_TXT", tableAuditLogBE.UserId);

                sqlParameters[7] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[7].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_table_audit_log", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_table_audit_log", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public TableAuditLogBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            TableAuditLogBE tableAuditLogBE = new TableAuditLogBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_table_audit_log_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                tableAuditLogBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return tableAuditLogBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_table_audit_log_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<TableAuditLogBE> GetList(DateTime startdate, DateTime enddate, string tablenamescsv)
        {
            _startdate = startdate;
            _enddate = enddate;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<TableAuditLogBE> tableAuditLogBEList = new List<TableAuditLogBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@StartDate", _startdate);
                sqlParameters[1] = new SqlParameter("@EndDate", _enddate);
                sqlParameters[2] = new SqlParameter("@TableNamesCsv", tablenamescsv);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_table_audit_log_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    tableAuditLogBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return tableAuditLogBEList;

        }

        public int Update(TableAuditLogBE tableAuditLogBE)
        {
            int rows;
            _tableAuditLogBE = tableAuditLogBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];

                sqlParameters[0] = new SqlParameter("@TABLE_AUDIT_LOG_ID", tableAuditLogBE.TableAuditLogId);
                sqlParameters[1] = new SqlParameter("@AUDIT_TYP_CD", tableAuditLogBE.AuditTypeCd);
                sqlParameters[2] = new SqlParameter("@AUDIT_TABLE_NM", tableAuditLogBE.AuditTableName);
                sqlParameters[3] = new SqlParameter("@AUDIT_TABLE_PK_ID", tableAuditLogBE.AuditTablePkId);
                sqlParameters[4] = new SqlParameter("@AUDIT_FIELD_NM", tableAuditLogBE.AuditFieldNm);
                sqlParameters[5] = new SqlParameter("@OLD_VAL_TXT", tableAuditLogBE.OldValTxt);
                sqlParameters[6] = new SqlParameter("@NEW_VAL_TXT", tableAuditLogBE.NewValTxt);
                sqlParameters[7] = new SqlParameter("@UPDATED_DTTM", tableAuditLogBE.UpdatedDate);

                sqlParameters[8] = new SqlParameter("@WKR_ID_TXT", tableAuditLogBE.UserId);

                sqlParameters[9] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[9].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_table_audit_log", base.ConnectionString, ref sqlParameters);

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
                    int result = DateTime.Compare(_startdate, _enddate);
                    string relationship;

                    if (result < 0)
                        relationship = " is earlier than ";
                    else if (result == 0)
                        relationship = " is the same time as ";
                    else
                        relationship = " is later than ";

                    if (result > 0) _errorMsg = _startdate.ToShortDateString() + relationship.ToString() + _enddate.ToShortDateString();
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }

            return true;

        }

        private TableAuditLogBE ConvertDataRowToBE(DataRow dataRow)
        {
            TableAuditLogBE tableAuditLogBE = new TableAuditLogBE();

            tableAuditLogBE.TableAuditLogId = TryToParse<int?>(dataRow["TABLE_AUDIT_LOG_ID"]);
            tableAuditLogBE.AuditTypeCd = TryToParse<string>(dataRow["AUDIT_TYP_CD"]);
            tableAuditLogBE.AuditTableName = TryToParse<string>(dataRow["AUDIT_TABLE_NM"]);
            tableAuditLogBE.AuditTablePkId = TryToParse<int?>(dataRow["AUDIT_TABLE_PK_ID"]);
            tableAuditLogBE.AuditFieldNm = TryToParse<string>(dataRow["AUDIT_FIELD_NM"]);
            tableAuditLogBE.OldValTxt = TryToParse<string>(dataRow["OLD_VAL_TXT"]);
            tableAuditLogBE.NewValTxt = TryToParse<string>(dataRow["NEW_VAL_TXT"]);
            tableAuditLogBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            tableAuditLogBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return tableAuditLogBE;

        }

        #endregion

    }

    #endregion

}