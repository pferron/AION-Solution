using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.BL.Adapters
{
    public interface INPAAdapter
    {
        NPAModel GetNPAModel();
        int Upsert();
        bool RearrangeTimeslots(int userid, DateTime fromDt, DateTime toDt);
    }
}
