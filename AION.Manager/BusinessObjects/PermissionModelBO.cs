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
    public class PermissionModelBO : ModelBaseModelBO
    {
        public Permission GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }

        public Permission GetInstance(PermissionEnum permissionEnum)
        {
            var t = BaseList.Where(x => x.PermissionEnum == permissionEnum).FirstOrDefault();
            return t;
        }

        public List<Permission> BaseList
        {
            get
            {
                List<Permission> ret = LocalStorage<List<Permission>>.GetValue("Permission_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<Permission>>.Add("Permission_List", ret);
                }
                return ret;
            }
        }
        public List<Permission> CreateInstance()
        {
            List<Permission> ret = new List<Permission>();
            PermissionBO bo = new PermissionBO();
            List<PermissionBE> be = bo.GetList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }
        public Permission ConvertData(PermissionBE be)
        {
            //PermissionModule permissionModule = new PermissionModuleModelBO().GetInstance(be.ModuleId.Value);

            Permission ret = new Permission();
            ret.ID = be.PermissionId.Value;
            ret.CreatedDate = be.CreatedDate.Value;
            ret.UpdatedDate = be.UpdatedDate.Value;
            ret.UpdatedUser = new UserIdentity { ID = int.Parse(be.UpdatedByWkrId) };
            ret.CreatedUser = new UserIdentity() { ID = int.Parse(be.CreatedByWkrId) };

            //InjectBaseObjects(ret, be.PermissionId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.PermissionName = be.PermissionName;
            ret.PermissionModuleId = be.ModuleId;
            ret.PermissionEnum = (PermissionEnum)be.EnumMappingNumber.Value;
            ret.PermissionDisplayName = be.PermissionDisplayName;
            ret.PermissionModuleEnum = (PermissionModuleEnum)be.ModuleEnumMappingNumber.Value;
            ret.PermissionId = be.PermissionId;
            return ret;
        }

        public bool RefreshList()
        {
            LocalStorage<List<Permission>>.Delete("Permission_List");
            return true;
        }

        public List<Permission> GetBySystemRoleID(int systemroleid)
        {
            PermissionBO bo = new PermissionBO();
            List<Permission> permissions = new List<Permission>();
            List<PermissionBE> permissionBEs = bo.GetListBySystemRoleId(systemroleid);
            foreach (PermissionBE permissionBE in permissionBEs)
            {
                permissions.Add(ConvertData(permissionBE));
            }
            return permissions;
        }
        public List<Permission> GetByUserID(int userid)
        {
            PermissionBO bo = new PermissionBO();
            List<Permission> permissions = new List<Permission>();
            List<PermissionBE> permissionBEs = bo.GetListByUserId(userid);
            foreach (PermissionBE permissionBE in permissionBEs)
            {
                permissions.Add(ConvertData(permissionBE));
            }
            return permissions;
        }
        public int InsertSystemRoleXref(int permissionId, int systemroleid, string wrkrid)
        {
            PermissionBO bo = new PermissionBO();
            return bo.InsertPermissionSystemRoleXref(permissionId, systemroleid, wrkrid);

        }
        public int InsertUserXref(int permissionid, int userid, string wrkrid)
        {
            PermissionBO bo = new PermissionBO();
            return bo.InsertPermissionUserXref(permissionid, userid, wrkrid);
        }
    }
}
