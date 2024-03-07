using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.BusinessObjects
{
    public class PermissionModuleModelBO : ModelBaseModelBO
    {
        public PermissionModule GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ModuleId == ID).FirstOrDefault();
            return t;
        }

        public PermissionModule GetInstance(PermissionModuleEnum moduleEnum)
        {
            var t = BaseList.Where(x => x.ModuleEnum == moduleEnum).FirstOrDefault();
            return t;
        }

        public List<PermissionModule> BaseList
        {
            get
            {
                List<PermissionModule> ret = LocalStorage<List<PermissionModule>>.GetValue("Module_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<PermissionModule>>.Add("Module_List", ret);
                }
                return ret;
            }
        }
        public List<PermissionModule> CreateInstance()
        {
            List<PermissionModule> ret = new List<PermissionModule>();
            ModuleBO bo = new ModuleBO();
            List<ModuleBE> be = bo.GetList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }
        public PermissionModule ConvertData(ModuleBE be)
        {
            PermissionModule ret = new PermissionModule();
            InjectBaseObjects(ret, be.ModuleId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.ModuleName = be.ModuleName;
            ret.ModuleId = be.ModuleId;
            ret.ModuleEnum = (PermissionModuleEnum)be.EnumMappingNumber.Value;
            ret.ModuleDisplayName = be.ModuleDisplayName;
            return ret;
        }

        public bool RefreshList()
        {
            LocalStorage<List<PermissionModule>>.Delete("Module_List");
            return true;
        }

    }
}
