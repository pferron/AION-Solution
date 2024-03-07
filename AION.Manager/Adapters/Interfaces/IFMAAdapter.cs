using AION.BL;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IFMAAdapter
    {
        int Upsert();
        bool CancelMeetingById(CancelMeetingModel model);
    }
}