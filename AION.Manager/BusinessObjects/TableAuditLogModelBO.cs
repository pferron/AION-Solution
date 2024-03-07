using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AION.BL.BusinessObjects
{
    public class TableAuditLogModelBO
    {
        private enum ActionType { GetList };


        private TableAuditLog _auditLog;
        private List<TableAuditLog> _auditLogs;
        private TableAuditLogBO _tableAuditLogBO;
        public List<TableAuditLog> GetList()
        {
            _auditLogs = new List<TableAuditLog>();
            _tableAuditLogBO = new TableAuditLogBO();
            DateTime startdate = DateTime.Now.AddMonths(-1);
            DateTime enddate = DateTime.Parse("12/31/2050");
            _auditLogs = _tableAuditLogBO.GetList(startdate, enddate, string.Empty).Select(x => ConvertTableAuditLogBEToAuditLog(x)).ToList();
            return _auditLogs;
        }
        public List<TableAuditLog> GetListWDetails(DateTime startDate, DateTime endDate, string tableNameCsv)
        {
            _auditLogs = new List<TableAuditLog>();
            _tableAuditLogBO = new TableAuditLogBO();
            _auditLogs = _tableAuditLogBO.GetList(startDate, endDate, tableNameCsv).Select(x => ConvertTableAuditLogBEToAuditLogWDetails(x)).ToList();

            return _auditLogs;
        }
        public TableAuditLog ConvertTableAuditLogBEToAuditLog(TableAuditLogBE tableAuditLogBE)
        {
            _auditLog = new TableAuditLog
            {
                AuditFieldNm = tableAuditLogBE.AuditFieldNm,
                AuditTableName = tableAuditLogBE.AuditTableName,
                AuditTablePkId = tableAuditLogBE.AuditTablePkId,
                AuditTypeCd = tableAuditLogBE.AuditTypeCd,
                NewValTxt = tableAuditLogBE.NewValTxt,
                OldValTxt = tableAuditLogBE.OldValTxt,
                TableAuditLogId = tableAuditLogBE.TableAuditLogId,
                UpdatedDate = (DateTime)tableAuditLogBE.UpdatedDate,
                UpdatedUser = (tableAuditLogBE.UpdatedByWkrId != null)
                ? new UserIdentityModelBO().GetInstance(int.Parse(tableAuditLogBE.UpdatedByWkrId))
                : new UserIdentity(),
            };
            _auditLog.AuditCodeName = GetAuditCodeName(_auditLog.AuditTypeCd);
            _auditLog.TypeName = ConvertTableNameToTypeName(_auditLog.AuditTableName);
            return _auditLog;
        }
        public TableAuditLog ConvertTableAuditLogBEToAuditLogWDetails(TableAuditLogBE tableAuditLogBE)
        {
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            _auditLog = new TableAuditLog
            {
                AuditFieldNm = tableAuditLogBE.AuditFieldNm,
                AuditTableName = tableAuditLogBE.AuditTableName,
                AuditTablePkId = tableAuditLogBE.AuditTablePkId,
                AuditTypeCd = tableAuditLogBE.AuditTypeCd,
                NewValTxt = tableAuditLogBE.NewValTxt,
                OldValTxt = tableAuditLogBE.OldValTxt,
                TableAuditLogId = tableAuditLogBE.TableAuditLogId,
                UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)tableAuditLogBE.UpdatedDate, easternZone),
                UpdatedUser = (tableAuditLogBE.UpdatedByWkrId != null)
                ? new UserIdentityModelBO().GetInstance(int.Parse(tableAuditLogBE.UpdatedByWkrId))
                : new UserIdentity(),
            };
            _auditLog.TypeName = ConvertTableNameToTypeName(_auditLog.AuditTableName);
            _auditLog.AuditCodeName = GetAuditCodeName(_auditLog.AuditTypeCd);
            AuditDetailRow auditDetailRow = new AuditDetailRow();
            auditDetailRow = ConvertJsonStringToAuditDetail(_auditLog.NewValTxt);
            switch (_auditLog.AuditTableName)
            {
                case "DEFAULT_ESTIMATION_HOURS":
                    _auditLog.ValTxt = ConvertAuditDetailToValTxt_DefaultEstimationHours(auditDetailRow.DataChanges.ToList());
                    break;
                case "PLAN_REVIEWER_AVAILABLE_HOURS":
                    _auditLog.ValTxt = ConvertAuditDetailToValTxt_PlanReviewerAvailableHours(auditDetailRow.DataChanges.ToList());
                    break;
                case "CATALOG_REF":
                    _auditLog.ValTxt = ConvertAuditDetailToValTxt_CatalogRef(auditDetailRow.DataChanges.ToList());
                    break;
                default:
                    _auditLog.ValTxt = ConvertAuditDetailToValTxt(auditDetailRow.DataChanges.Where(x => x.OldValue != x.NewValue).ToList());

                    break;
            }
            return _auditLog;
        }
        public string ConvertTableNameToTypeName(string tblName)
        {
            List<TableName> tableNames = new List<TableName>();
            tableNames.Add(new TableName { DBTableName = "CATALOG_REF", TableDesc = "Misc Configuration" });
            tableNames.Add(new TableName { DBTableName = "DEFAULT_ESTIMATION_HOURS", TableDesc = "Default Hours" });
            tableNames.Add(new TableName { DBTableName = "HOLIDAY_CONFIGURATION", TableDesc = "Holiday Configuration" });
            tableNames.Add(new TableName { DBTableName = "SYSTEM_ROLE", TableDesc = "Create Role" });
            tableNames.Add(new TableName { DBTableName = "USER_BUSINESS_RELATIONSHIP", TableDesc = "User Management" });
            tableNames.Add(new TableName { DBTableName = "USER", TableDesc = "User Management" });
            tableNames.Add(new TableName { DBTableName = "USER_SYSTEM_ROLE_RELATIONSHIP", TableDesc = "User Management" });
            tableNames.Add(new TableName { DBTableName = "PLAN_REVIEWER_AVAILABLE_HOURS", TableDesc = "Misc Configuration - Plan Reviewer Available Hours" });
            tableNames.Add(new TableName { DBTableName = "NON_PROJECT_APPOINTMENT_TYPE_REF", TableDesc = "NPA Appointment Type" });
            tableNames.Add(new TableName { DBTableName = "TEMPLATE", TableDesc = "Message Configuration" });
            tableNames.Add(new TableName { DBTableName = "USER_PERMISSION_XREF", TableDesc = "User Permission" });
            tableNames.Add(new TableName { DBTableName = "PERMISSION_SYSTEM_ROLE_XREF", TableDesc = "Role Permission" });
            string typename = tableNames.Where(x => x.DBTableName == tblName).FirstOrDefault().TableDesc;

            return typename;
        }
        public string ConvertAuditDetailToValTxt(List<AuditDetail> auditDetails)
        {
            StringBuilder sb = new StringBuilder();
            foreach (AuditDetail det in auditDetails)
            {
                string oldvalue = GetObjectValTxt(det.OldValue, det.FieldName);
                string newvalue = GetObjectValTxt(det.NewValue, det.FieldName);
                sb.Append(Environment.NewLine);
                sb.Append(det.FieldNameDesc);
                sb.Append(" - ");
                sb.Append(Environment.NewLine);
                sb.Append("From: ");
                sb.Append(oldvalue);
                sb.Append(Environment.NewLine);
                sb.Append("To: ");
                sb.Append(newvalue);
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public string ConvertAuditDetailToValTxt_DefaultEstimationHours(List<AuditDetail> auditDetails)
        {
            StringBuilder sb = new StringBuilder();
            foreach (AuditDetail det in auditDetails)
            {
                string oldvalue = GetObjectValTxt(det.OldValue, det.FieldName);
                string newvalue = GetObjectValTxt(det.NewValue, det.FieldName);
                switch (det.FieldName)
                {
                    case "DEFAULT_HOURS_NBR":
                        sb.Append(Environment.NewLine);
                        sb.Append(det.FieldNameDesc);
                        sb.Append(" - ");
                        sb.Append(Environment.NewLine);
                        sb.Append("From: ");
                        sb.Append(oldvalue);
                        sb.Append(Environment.NewLine);
                        sb.Append("To: ");
                        sb.Append(newvalue);
                        sb.Append(Environment.NewLine);
                        break;
                    case "BUSINESS_REF_ID":
                    case "PROJECT_TYP_REF_ID":
                        sb.Append(Environment.NewLine);
                        sb.Append(det.FieldNameDesc);
                        sb.Append(" - ");
                        sb.Append(Environment.NewLine);
                        sb.Append(oldvalue);
                        sb.Append(Environment.NewLine);
                        break;
                }
            }
            return sb.ToString();

        }
        public string ConvertAuditDetailToValTxt_PlanReviewerAvailableHours(List<AuditDetail> auditDetails)
        {
            StringBuilder sb = new StringBuilder();
            foreach (AuditDetail det in auditDetails)
            {
                string oldvalue = GetObjectValTxt(det.OldValue, det.FieldName);
                string newvalue = GetObjectValTxt(det.NewValue, det.FieldName);
                switch (det.FieldName)
                {
                    case "AVAILABLE_HOURS_NBR":
                        sb.Append(Environment.NewLine);
                        sb.Append(det.FieldNameDesc);
                        sb.Append(" - ");
                        sb.Append(Environment.NewLine);
                        sb.Append("From: ");
                        sb.Append(oldvalue);
                        sb.Append(Environment.NewLine);
                        sb.Append("To: ");
                        sb.Append(newvalue);
                        sb.Append(Environment.NewLine);
                        break;
                    case "ENUM_MAPPING_VAL_NBR":
                        string enumtypename = ((PlanReviewHourTypes)int.Parse(oldvalue)).ToStringValue();
                        sb.Append(Environment.NewLine);
                        sb.Append(" - ");
                        sb.Append(enumtypename);
                        sb.Append(Environment.NewLine);
                        break;
                }
            }
            return sb.ToString();

        }

        public string ConvertAuditDetailToValTxt_CatalogRef(List<AuditDetail> auditDetails)
        {
            StringBuilder sb = new StringBuilder();
            //old rows in the audit table wont have the necessary information
            if (auditDetails.Where(x => x.FieldName == "CATALOG_KEY_TXT").Any())
            {
                string keytxt = auditDetails.Where(x => x.FieldName == "CATALOG_KEY_TXT").FirstOrDefault().OldValue;
                string subkeytxt = auditDetails.Where(x => x.FieldName == "CATALOG_SUBKEY_TXT").FirstOrDefault().OldValue;
                string keydesc = GetCatalogRefKeyDesc(keytxt);
                string subkeydesc = GetCatalogRefSubKeyDesc(subkeytxt);

                foreach (AuditDetail det in auditDetails)
                {
                    string oldvalue = GetObjectValTxt(det.OldValue, det.FieldName);
                    string newvalue = GetObjectValTxt(det.NewValue, det.FieldName);
                    string fieldNameDesc = string.Empty;

                    switch (det.FieldName)
                    {
                        case "CATALOG_VAL_TXT":
                            sb.Append(Environment.NewLine);
                            sb.Append(keydesc);
                            sb.Append(" - ");
                            sb.Append(subkeydesc);
                            sb.Append(" - ");
                            sb.Append(Environment.NewLine);
                            sb.Append("From: ");
                            sb.Append(oldvalue);
                            sb.Append(Environment.NewLine);
                            sb.Append("To: ");
                            sb.Append(newvalue);
                            sb.Append(Environment.NewLine);
                            break;
                        case "CATALOG_KEY_TXT":
                            break;
                        case "CATALOG_SUBKEY_TXT":
                            break;
                    }


                }
            }
            return sb.ToString();

        }
        private string GetCatalogRefSubKeyDesc(string catalogSubKeyTxt)
        {
            string desc = "";
            switch (catalogSubKeyTxt)
            {
                case "SCHEDULE_DATE_CONFIGURATION":
                case "SAME_CONTRACTOR_DAYS":
                    desc = "Number of Days";
                    break;
                default:
                    desc = catalogSubKeyTxt;
                    break;
            }
            return desc;
        }
        private string GetCatalogRefKeyDesc(string catalogKeyTxt)
        {
            string desc = catalogKeyTxt;
            switch (catalogKeyTxt)
            {
                case "EMAILENGINE.BASEURL":
                    desc = "Email Engine Base URL";
                    break;
                case "COUNTY.ZONING.DEFAULT.SELECTION":
                    desc = "County Zoning Default Selection";
                    break;
                case "COUNTY.ZONING.DEFAULT.HRS_ENABLED":
                    desc = "County Zoning Default Hours Enabled";
                    break;
                case "COUNTY.ZONING.DEFAULT.HOUR":
                    desc = "County Zoning Default Hour";
                    break;
                case "AION.PROJECT_STATUS_REF.ENUM_MAPPING_VAL_NBR":
                    desc = "Project Status Ref Mapping Nbr";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.USE":
                    desc = "Miscellaneous Config - Use Scheduling Multiplier";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.START_DATE":
                    desc = "Miscellaneous Config - Scheduling Multiplier Start Date";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE":
                    desc = "Miscellaneous Config - Scheduling Multiplier Project Type";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.NAME":
                    desc = "Miscellaneous Config - Scheduling Multiplier Name";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR":
                    desc = "Miscellaneous Config - Scheduling Multiplier Factor";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.END_DATE":
                    desc = "Miscellaneous Config - Scheduling Multiplier End Date";
                    break;
                case "ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION":
                    desc = "Miscellaneous Config - Schedule Gate Date";
                    break;
                case "ADMIN.MISC_CONFIG.SAME_CONTRACTOR_DAYS":
                    desc = "Miscellaneous Config - Same Contractor Days";
                    break;
            }
            return desc;
        }
        public AuditDetailRow ConvertJsonStringToAuditDetail(string jsonstring)
        {
            AuditDetailRow auditDetailRow = new AuditDetailRow();
            auditDetailRow.DataChanges = new List<AuditDetail>();
            if (!string.IsNullOrWhiteSpace(jsonstring))
            {
                auditDetailRow = JsonConvert.DeserializeObject<AuditDetailRow>(jsonstring);
            }
            return auditDetailRow;
        }
        public string GetAuditCodeName(string auditcd)
        {
            string cdname = string.Empty;

            switch (auditcd)
            {
                case "I":
                    return "Insert";
                case "U":
                    return "Update";
                case "D":
                    return "Delete";
                default:
                    break;
            }
            return cdname;
        }
        public string GetObjectValTxt(string objval, string fieldname)
        {
            if (!string.IsNullOrWhiteSpace(objval))
            {
                switch (fieldname)
                {
                    case "BUSINESS_REF_ID":
                        return ((DepartmentNameEnums)int.Parse(objval)).ToStringValue();
                    case "PROJECT_TYP_REF_ID":
                        return ((PropertyTypeEnums)int.Parse(objval)).ToStringValue();
                    case "SYSTEM_ROLE_ID":
                        var systemRole = new SystemRoleBO().GetById(int.Parse(objval));
                        return systemRole.SystemRoleNm;
                    case "USER_ID":
                        UserIdentity user = new UserIdentityModelBO().GetInstance(int.Parse(objval));
                        return user.FirstName + " " + user.LastName;
                    case "TEMPLATE_TYP_ID":
                        MessageTemplateTypeBE templatetype = new MessageTemplateTypeBO().GetById(int.Parse(objval));
                        MessageTemplateTypeEnum messageTemplateTypeEnum = (MessageTemplateTypeEnum)templatetype.EnumMappingValNbr;
                        return messageTemplateTypeEnum.ToStringValue();
                    case "PERMISSION_REF_ID":
                        PermissionBE permissionBE = new PermissionBO().GetById(int.Parse(objval));
                        PermissionEnum permissionEnum = (PermissionEnum)permissionBE.EnumMappingNumber;
                        return permissionEnum.ToStringValue();
                    case "ENABLED_IND":
                        return int.Parse(objval) == 1 ? "Enabled" : "Disabled";
                    case "ACTIVE_IND":
                        return int.Parse(objval) == 1 ? "Active" : "Inactive";
                    case "HOLIDAY_ANNUAL_RECUR_IND":
                        return int.Parse(objval) == 1 ? "Recurring Annually" : "One Time Only";
                }
            }
            return objval;
        }
    }

    public class AuditDetailRow
    {
        [JsonProperty("rowdatachanges")]
        public List<AuditDetail> DataChanges { get; set; }
    }
    public class AuditDetail
    {
        [JsonProperty("fieldname")]
        public string FieldName { get; set; }
        [JsonProperty("oldvalue")]
        public string OldValue { get; set; }
        [JsonProperty("newvalue")]
        public string NewValue { get; set; }
        [JsonProperty("fieldnamedesc")]
        public string FieldNameDesc { get; set; }
    }
    public class TableName
    {
        public string DBTableName { get; set; }
        public string TableDesc { get; set; }
    }
}
