using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.BusinessObjects
{
    public class SystemRoleModelBO : ModelBaseModelBO, ISystemRoleBO
    {

        public SystemRole GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }

        public SystemRole GetInstance(int externalSystemID, string systemRoleNameExtRef)
        {
            var t = BaseList.Where(x => x.ExternalSystemRefInfo == systemRoleNameExtRef && x.ExternalSystem.ID == externalSystemID).FirstOrDefault();
            return t;
        }

        public SystemRole GetInstance(SystemRoleEnum systemRoleEnum)
        {
            var t = BaseList.Where(x => x.SystemRoleEnum == systemRoleEnum).FirstOrDefault();
            return t;
        }


        public List<SystemRole> GetInstancesForUser(int userID)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            List<UserSystemRoleRelationshipBE> rolelist = uroles.GetListByUserId(userID);
            List<SystemRole> roles = new List<SystemRole>();
            foreach (UserSystemRoleRelationshipBE role in rolelist)
            {
                roles.Add(ConvertUserSystemRole(role));
            }
            return roles;
        }
        private SystemRole ConvertUserSystemRole(UserSystemRoleRelationshipBE role)
        {
            SystemRoleBE sr = new SystemRoleBO().GetById((int)role.SystemRoleId);

            return ConvertData(sr);
        }
        public List<SystemRole> BaseList
        {
            get
            {
                List<SystemRole> ret = LocalStorage<List<SystemRole>>.GetValue("SystemRole_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<SystemRole>>.Add("SystemRole_List", ret);
                }
                return ret;
            }
        }

        public List<SystemRole> CreateInstance()
        {
            List<SystemRole> ret = new List<SystemRole>();
            SystemRoleBO bo = new SystemRoleBO();
            List<SystemRoleBE> be = bo.GetAllList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }

        public bool UpdateRoleOptions(int roleID, string oldRoleOptionsString, string newRoleOptionsString)
        {
            SystemRoleBO bo = new SystemRoleBO();
            SystemRoleBE be = bo.GetById(roleID);
            be.RoleOptionsTxt = be.RoleOptionsTxt.Replace(oldRoleOptionsString, newRoleOptionsString);
            if (be.RoleOptionsTxt.Length > 1000)
                throw new Exception("Invalid arguments in assigning Role");
            bo.Update(be);
            return true;
        }

        public SystemRole ConvertData(SystemRoleBE be)
        {
            SystemRole ret = new SystemRole();
            InjectBaseObjects(ret, be.SystemRoleId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            /* Note the expression is evaluated lazily, so if you change the value of the 
             * variable before the constructor is called it might not do what you expect.*/
            ret.ExternalSystemRefInfo = be.SrcSystemValTxt;
            ret.ExternalSystem = new ExternalSystemModelBO().GetInstance((int)be.ExternalSystemRefId);
            ret.RoleName = be.SystemRoleNm;
            ret.SystemRoleEnum = be.EnumMappingValNbr.HasValue ? (SystemRoleEnum)be.EnumMappingValNbr.Value : SystemRoleEnum.NA;
            ret.EnumMappingValNbr = be.EnumMappingValNbr;
            ret.Enabled = be.EnabledInd.Value;
            ret.RoleOptions = be.RoleOptionsTxt;
            ret.ParentSystemRoleId = be.ParentSystemRoleId;
            ret.SrcSystemValTxt = be.SrcSystemValTxt;
            return ret;
        }

        public bool RefreshList()
        {
            LocalStorage<List<SystemRole>>.Delete("SystemRole_List");
            return true;
        }

    }


    public interface ISystemRoleBO : IModelCollectionCreater<SystemRole, SystemRoleBE>
    {
        SystemRole GetInstance(int ID);

        SystemRole GetInstance(int ExternalSystemID, string externalSystemRoleRef);

        SystemRole GetInstance(SystemRoleEnum systemRoleEnum);

        List<SystemRole> GetInstancesForUser(int userID);

    }
}
