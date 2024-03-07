using AION.BL.Models;
using AION.Manager.Models.ConfigurationHistory;
using System.Collections.Generic;

namespace AION.Manager.Adapters.Interfaces
{
    public interface IConfigurationHistoryAdapter
    {
        List<TableAuditLog> GetTableAuditLogListWDetails(ConfigurationHistory history);
    }
}
