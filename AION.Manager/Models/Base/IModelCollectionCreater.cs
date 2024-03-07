using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Models.Base
{
    public interface  IModelCollectionCreater<ModelObject,BEObject>
    {
        List<ModelObject> CreateInstance();

        ModelObject ConvertData(BEObject be);

        List<ModelObject> BaseList { get; }

        bool RefreshList();

    }
}
