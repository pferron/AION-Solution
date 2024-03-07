using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Manager.Adapters.Interfaces;
using AION.Manager.Models.ConfigurationHistory;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class ConfigurationHistoryAdapter : BaseManagerAdapter, IConfigurationHistoryAdapter
    {
        public List<TableAuditLog> GetTableAuditLogListWDetails(ConfigurationHistory history)
        {
            try
            {
                TableAuditLogModelBO bo = new TableAuditLogModelBO();
                //ConfigurationHistoryTable table;
                Enum.TryParse(history.SearchType, out ConfigurationHistoryTable table);
                //ConfigurationHistoryTable table = (ConfigurationHistoryTable)int.Parse(history.SearchType);
                //SearchRange range = (SearchRange)int.Parse(history.SearchRange);
                Enum.TryParse(history.SearchRange, out SearchRange range);
                DateTime startDate = DateTime.Now.Date.AddDays(-1);
                DateTime endDate = DateTime.Now.Date.AddDays(1);
                string tablenamescsv = "";

                switch (range)
                {
                    case SearchRange.Hours24:
                        startDate = DateTime.Now.Date.AddDays(-1);
                        endDate = DateTime.Now.Date.AddDays(1);
                        break;
                    case SearchRange.Days3:
                        startDate = DateTime.Now.Date.AddDays(-3);
                        endDate = DateTime.Now.Date.AddDays(1);
                        break;
                    case SearchRange.Days14:
                        startDate = DateTime.Now.Date.AddDays(-14);
                        endDate = DateTime.Now.Date.AddDays(1);
                        break;
                    default:
                        break;
                }

                switch (table)
                {
                    case ConfigurationHistoryTable.UserManagement:
                        tablenamescsv = "USER,USER_SYSTEM_ROLE_RELATIONSHIP,USER_BUSINESS_RELATIONSHIP";
                        break;
                    case ConfigurationHistoryTable.NPAConfiguration:
                        tablenamescsv = "NON_PROJECT_APPOINTMENT_TYPE_REF";
                        break;
                    case ConfigurationHistoryTable.HolidayConfiguration:
                        tablenamescsv = "HOLIDAY_CONFIGURATION";
                        break;
                    case ConfigurationHistoryTable.DefaultHoursConfiguration:
                        tablenamescsv = "CATALOG_REF";
                        break;
                    case ConfigurationHistoryTable.MiscConfiguration:
                        tablenamescsv = "CATALOG_REF,PLAN_REVIEWER_AVAILABLE_HOURS";
                        break;
                    case ConfigurationHistoryTable.MessageConfiguration:
                        tablenamescsv = "TEMPLATE";
                        break;
                    case ConfigurationHistoryTable.CreateRole:
                        tablenamescsv = "SYSTEM_ROLE,PERMISSION_SYSTEM_ROLE_XREF,USER_PERMISSION_XREF";

                        break;
                    case ConfigurationHistoryTable.ModifyRole:
                        tablenamescsv = "SYSTEM_ROLE,PERMISSION_SYSTEM_ROLE_XREF,USER_PERMISSION_XREF";

                        break;
                    default:
                        break;
                }

                return bo.GetListWDetails(startDate, endDate, tablenamescsv);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetTableAuditLogListWDetails - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
    }
}