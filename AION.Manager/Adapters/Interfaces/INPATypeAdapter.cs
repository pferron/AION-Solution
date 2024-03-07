using AION.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Adapters
{
    public interface INPATypeAdapter
    {
        bool UpdateNPAConfiguration(List<string> npaConfig);
        List<NpaType> GetAll(bool includeOnlyActive = false);
        bool Insert(NpaType data);
        bool MakeActive(NpaType data);
        bool MakeInActive(NpaType data);
    }
}