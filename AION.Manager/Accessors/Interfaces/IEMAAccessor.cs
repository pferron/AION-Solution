using AION.BL;
using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Accessors
{
    public interface IEMAAccessor
    {
        List<ExpressSearchResult> GetScheduledByDate(DateTime fromdate, DateTime todate);
    }
}