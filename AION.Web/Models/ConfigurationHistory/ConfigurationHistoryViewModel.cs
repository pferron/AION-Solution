using AION.BL;
using AION.BL.Models;
using AION.Manager.Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models.ConfigurationHistory
{
    public class ConfigurationHistoryViewModel : ViewModelBase
    {
        public ConfigurationHistoryViewModel()
        {
            AuditLogs = new List<TableAuditLogViewModel>();
            TableAuditLogs = new List<TableAuditLog>();
        }
        public List<TableAuditLogViewModel> AuditLogs { get; set; }
        public List<TableAuditLog> TableAuditLogs { get; set; }

        public List<SelectListItem> TableTypeList
        {
            get
            {
                return Enum<ConfigurationHistoryTable>.ToSelectList;
            }
        }

        public List<SelectListItem> SearchRangeList
        {
            get
            {
                return Enum<SearchRange>.ToSelectList;
            }
        }

        public string SearchType { get; set; }
        public string SearchRange { get; set; }
    }

}