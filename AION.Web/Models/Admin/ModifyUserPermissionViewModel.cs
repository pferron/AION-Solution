using AION.BL;
using AION.Manager.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models.Admin
{
    public class ModifyUserPermissionViewModel : PermissionViewModel
    {
        private List<SystemRole> _systemRoles;
        private List<SelectListItem> _parentSystemRoles;
        private List<SelectListItem> _allSystemRoles;
        public List<Permission> PermsList { get; set; }
        public List<Permission> PermsModulesList { get; set; }
        public int SystemRoleId { get; set; }
        public int ParentSystemRoleId { get; set; }
        public string SystemRoleName { get; set; }
        public List<SystemRole> SystemRoles
        {
            get
            {
                return _systemRoles;
            }
            set
            {
                _systemRoles = value;

            }
        }
        public List<SelectListItem> ParentSystemRoles
        {
            get
            {
                if (_systemRoles != null && _parentSystemRoles == null)
                    _parentSystemRoles = GetBaseRoleSelectList();
                return _parentSystemRoles;
            }
        }
        public List<SelectListItem> SystemRoleList
        {
            get
            {
                if (_systemRoles != null && _allSystemRoles == null)
                    _allSystemRoles = GetRoleSelectList();
                return _allSystemRoles;
            }
        }
        public string WrkrId { get; set; }



        public string ResultsMessage { get; set; }

        private List<SelectListItem> GetBaseRoleSelectList()
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            foreach (SystemRole item in _systemRoles)
            {
                //TODO: these match what's in user management but we need to change so the only base one are the ones
                // that have a null in parentsystemroleid
                if (item.SystemRoleEnum == SystemRoleEnum.Plan_Reviewer
                    || item.SystemRoleEnum == SystemRoleEnum.Estimator
                    || item.SystemRoleEnum == SystemRoleEnum.Manager
                    || item.SystemRoleEnum == SystemRoleEnum.Facilitator
                    || item.SystemRoleEnum == SystemRoleEnum.View_Only
                    || item.SystemRoleEnum == SystemRoleEnum.Sys_Admin)
                    ret.Add(new SelectListItem
                    {
                        Text = item.SystemRoleEnum.ToStringValue(),
                        Value = item.ID.ToString()
                    });
            }
            return ret;
        }
        private List<SelectListItem> GetRoleSelectList()
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(new SelectListItem
            {
                Text = "Select a Role",
                Value = "0"
            });

            foreach (SystemRole item in _systemRoles)
            {
                //exclude NA
                if (item.ID > 0)
                {
                    if (item.EnumMappingValNbr.HasValue && item.EnumMappingValNbr.Value > 0)
                    {
                        ret.Add(new SelectListItem
                        {
                            Text = item.SystemRoleEnum.ToStringValue(),
                            Value = item.ID.ToString()
                        });
                    }
                    else
                    {
                        ret.Add(new SelectListItem
                        {
                            Text = item.RoleName,
                            Value = item.ID.ToString()
                        });
                    }
                }

            }
            return ret;
        }

    }
}