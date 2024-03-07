using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using AION.Scheduler.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.BusinessObjects
{
    public class TimeAllocationTypeRefModelBO : ModelBaseModelBO
    {

        public TimeAllocationTypeRef GetInstance(int id)
        {
            var t = BaseList.Where(x => x.ID == id).FirstOrDefault();
            return t;

        }

        public TimeAllocationTypeRef GetInstance(TimeAllocationType timeAllocationType)
        {
            var t = BaseList.Where(x => x.TimeAllocationType == timeAllocationType).FirstOrDefault();
            return t;

        }

        public List<TimeAllocationTypeRef> CreateInstance()
        {
            List<TimeAllocationTypeRef> ret = new List<TimeAllocationTypeRef>();
            TimeAllocationTypeRefBO bo = new TimeAllocationTypeRefBO();
            List<TimeAllocationTypeRefBE> be = bo.GetList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }

        public bool RefreshList()
        {
            LocalStorage<List<TimeAllocationTypeRef>>.Delete("TimeAllocationTypeRef_List");
            return true;
        }

        public List<TimeAllocationTypeRef> BaseList
        {
            get
            {
                List<TimeAllocationTypeRef> ret = LocalStorage<List<TimeAllocationTypeRef>>.GetValue("TimeAllocationTypeRef_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<TimeAllocationTypeRef>>.Add("TimeAllocationTypeRef_List", ret);
                }
                return ret;
            }
        }

        public TimeAllocationTypeRef ConvertData(TimeAllocationTypeRefBE be)
        {
            TimeAllocationTypeRef ret = new TimeAllocationTypeRef();
            InjectBaseObjects(ret, be.TimeAllocationTypeRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.TimeAllocationTypeRefDesc = be.TimeAllocationTypeRefDesc;
            ret.ActiveInd = be.ActiveInd;
            ret.TimeAllocationType = (TimeAllocationType)be.EnumMappingValNbr.Value;
            return ret;
        }

    }
}