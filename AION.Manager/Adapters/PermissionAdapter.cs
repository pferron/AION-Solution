using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AION.Manager.Models;
using AION.Web.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.BL.Adapters
{
    public class PermissionAdapter : BaseManagerAdapter, IPermissionAdapter
    {
        public PermissionMapping GetPermissionMappingByUserId(int userid)
        {
            PermissionMappingModelBO mappingModelBO = new PermissionMappingModelBO();
            PermissionMapping pm = mappingModelBO.GetInstance(userid);

            return pm;
        }
        public bool InsertRolePermission(int systemroleid, int permissionid, string wkrid)
        {
            try
            {
                //insert the permission for the role
                int xrefid = new PermissionModelBO().InsertSystemRoleXref(permissionid, systemroleid, wkrid);
                return (xrefid > 0);

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in InsertRolePermission - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool InsertRolePermissions(int systemroleid, List<int> perms, string wrkrid)
        {
            bool success = false;
            try
            {
                foreach (int permid in perms)
                {
                    //find out if exists already, return true;
                    success = InsertRolePermission(systemroleid, permid, wrkrid);
                }

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in InsertRolePermissions - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }
        public bool InsertDefaultRolePermissions()
        {
            try
            {
                //check xref, if empty, add these as requested, 
                if (new PermissionBO().GetSystemRoleXrefList() > 0) return true;
                List<SystemRole> roles = new SystemRoleModelBO().BaseList;
                List<PermissionEnum> perms = new List<PermissionEnum>();
                foreach (SystemRole role in roles)
                {
                    switch (role.SystemRoleEnum)
                    {
                        case SystemRoleEnum.Estimator:
                            perms = new List<PermissionEnum>();
                            perms.Add(PermissionEnum.Add_Project_Files);
                            perms.Add(PermissionEnum.Reqst_Mtng);

                            perms.Add(PermissionEnum.Prlim_Estimat_Trads);
                            perms.Add(PermissionEnum.Prlim_Estimat_Zoning);
                            perms.Add(PermissionEnum.Prlim_Estimat_Fire);
                            perms.Add(PermissionEnum.Prlim_Estimat_Bkflow);
                            perms.Add(PermissionEnum.Prlim_Estimat_EHS);
                            perms.Add(PermissionEnum.Estimat_Trads);
                            perms.Add(PermissionEnum.Estimat_Zoning);
                            perms.Add(PermissionEnum.Estimat_Fire);
                            perms.Add(PermissionEnum.Estimat_Bkflow);
                            perms.Add(PermissionEnum.Estimat_EHS);
                            perms.Add(PermissionEnum.Vw_Fclttor_Wrkload);
                            perms.Add(PermissionEnum.E_Fclttor);
                            perms.Add(PermissionEnum.Resend_Notif);
                            perms.Add(PermissionEnum.E_Plns_Rdy_Dt);
                            perms.Add(PermissionEnum.Vw_Mngmnt_Rprts);
                            perms.Add(PermissionEnum.Reqst_Mtng);
                            perms.Add(PermissionEnum.Use_Express);
                            break;
                        case SystemRoleEnum.Plan_Reviewer:
                            perms = new List<PermissionEnum>();
                            perms.Add(PermissionEnum.Add_Project_Files);
                            perms.Add(PermissionEnum.Reqst_Mtng);

                            perms.Add(PermissionEnum.Apprv_Mtng_Minuts);
                            perms.Add(PermissionEnum.Resend_Notif);
                            perms.Add(PermissionEnum.Assign_To_Me);
                            perms.Add(PermissionEnum.Exit_Mtng_Notes_For_Cstmr);
                            break;
                        case SystemRoleEnum.Facilitator:
                            perms = new List<PermissionEnum>();
                            perms.Add(PermissionEnum.Add_Project_Files);
                            perms.Add(PermissionEnum.Reqst_Mtng);

                            perms.Add(PermissionEnum.Schdul_Prlim_Mtng_Auto);
                            perms.Add(PermissionEnum.Schdul_Prlim_Mtng_Man);
                            perms.Add(PermissionEnum.Add_Prlim_Mtng_Prtcpnt);
                            perms.Add(PermissionEnum.Cancel_Prlim_Mtng);
                            perms.Add(PermissionEnum.Upload_Minuts);
                            perms.Add(PermissionEnum.Vw_Fclttor_Wrkload);
                            perms.Add(PermissionEnum.E_Fclttor);
                            perms.Add(PermissionEnum.Resend_Notif);
                            perms.Add(PermissionEnum.E_Plns_Rdy_Dt);
                            perms.Add(PermissionEnum.Accpt_Rjct_Rview_Dt);
                            perms.Add(PermissionEnum.Schdul_Rview_Pln_Rview_Auto);
                            perms.Add(PermissionEnum.Schdul_Rview_Pln_Rview_Man);
                            perms.Add(PermissionEnum.Activt_NA_Rview);
                            perms.Add(PermissionEnum.Vw_Schdul_Cpcty);
                            perms.Add(PermissionEnum.Schdul_Nxt_Cycl);
                            perms.Add(PermissionEnum.Schdul_Notes_Sel);
                            perms.Add(PermissionEnum.Vw_Mngmnt_Rprts);
                            perms.Add(PermissionEnum.Close_Mtng);
                            perms.Add(PermissionEnum.Cancel_Mtng);
                            perms.Add(PermissionEnum.Enter_Mtng_Prtcpnt);
                            perms.Add(PermissionEnum.Configure_Express);
                            perms.Add(PermissionEnum.E_Express_Rsrvtions);
                            perms.Add(PermissionEnum.E_Schdul_Express);
                            perms.Add(PermissionEnum.Man_Reserve_Express_Time);
                            perms.Add(PermissionEnum.Schdul_Express_Auto);
                            perms.Add(PermissionEnum.Schdul_Express_Man);
                            perms.Add(PermissionEnum.Use_Express);
                            perms.Add(PermissionEnum.Schdul_Collab_Mtng);
                            perms.Add(PermissionEnum.Schdul_PrePermitting_Mtng);
                            perms.Add(PermissionEnum.Schdul_Phasing_Mtng);
                            perms.Add(PermissionEnum.Schdul_Exit_Mtng);
                            perms.Add(PermissionEnum.Create_NPA);
                            perms.Add(PermissionEnum.Modify_NPA);
                            break;
                        case SystemRoleEnum.Manager:
                            perms = new List<PermissionEnum>();
                            perms.Add(PermissionEnum.Add_Project_Files);
                            perms.Add(PermissionEnum.Reqst_Mtng);

                            perms.Add(PermissionEnum.Prlim_Estimat_Trads);
                            perms.Add(PermissionEnum.Prlim_Estimat_Zoning);
                            perms.Add(PermissionEnum.Prlim_Estimat_Fire);
                            perms.Add(PermissionEnum.Prlim_Estimat_Bkflow);
                            perms.Add(PermissionEnum.Prlim_Estimat_EHS);
                            perms.Add(PermissionEnum.Estimat_Trads);
                            perms.Add(PermissionEnum.Estimat_Zoning);
                            perms.Add(PermissionEnum.Estimat_Fire);
                            perms.Add(PermissionEnum.Estimat_Bkflow);
                            perms.Add(PermissionEnum.Estimat_EHS);

                            perms.Add(PermissionEnum.Schdul_Prlim_Mtng_Auto);
                            perms.Add(PermissionEnum.Schdul_Prlim_Mtng_Man);
                            perms.Add(PermissionEnum.Add_Prlim_Mtng_Prtcpnt);
                            perms.Add(PermissionEnum.Cancel_Prlim_Mtng);
                            perms.Add(PermissionEnum.Upload_Minuts);
                            perms.Add(PermissionEnum.Vw_Fclttor_Wrkload);
                            perms.Add(PermissionEnum.E_Fclttor);
                            perms.Add(PermissionEnum.Resend_Notif);
                            perms.Add(PermissionEnum.E_Plns_Rdy_Dt);
                            perms.Add(PermissionEnum.Accpt_Rjct_Rview_Dt);
                            perms.Add(PermissionEnum.Schdul_Rview_Pln_Rview_Auto);
                            perms.Add(PermissionEnum.Schdul_Rview_Pln_Rview_Man);
                            perms.Add(PermissionEnum.Activt_NA_Rview);
                            perms.Add(PermissionEnum.Vw_Schdul_Cpcty);
                            perms.Add(PermissionEnum.Schdul_Nxt_Cycl);
                            perms.Add(PermissionEnum.Schdul_Notes_Sel);
                            perms.Add(PermissionEnum.Vw_Mngmnt_Rprts);
                            perms.Add(PermissionEnum.Close_Mtng);
                            perms.Add(PermissionEnum.Cancel_Mtng);
                            perms.Add(PermissionEnum.Enter_Mtng_Prtcpnt);
                            perms.Add(PermissionEnum.Configure_Express);
                            perms.Add(PermissionEnum.E_Express_Rsrvtions);
                            perms.Add(PermissionEnum.E_Schdul_Express);
                            perms.Add(PermissionEnum.Man_Reserve_Express_Time);
                            perms.Add(PermissionEnum.Schdul_Express_Auto);
                            perms.Add(PermissionEnum.Schdul_Express_Man);
                            perms.Add(PermissionEnum.Use_Express);
                            perms.Add(PermissionEnum.Schdul_Collab_Mtng);
                            perms.Add(PermissionEnum.Schdul_PrePermitting_Mtng);
                            perms.Add(PermissionEnum.Schdul_Phasing_Mtng);
                            perms.Add(PermissionEnum.Schdul_Exit_Mtng);
                            perms.Add(PermissionEnum.Create_NPA);
                            perms.Add(PermissionEnum.Modify_NPA);
                            perms.Add(PermissionEnum.Apprv_Mtng_Minuts);
                            perms.Add(PermissionEnum.Assign_To_Me);
                            perms.Add(PermissionEnum.Reopen_Mtng);
                            perms.Add(PermissionEnum.Exit_Mtng_Notes_For_Cstmr);

                            //LES-305 3/30/22 add management reports
                            perms.Add(PermissionEnum.Management_Report_1);
                            perms.Add(PermissionEnum.Management_Report_2);
                            perms.Add(PermissionEnum.Management_Report_3);
                            perms.Add(PermissionEnum.Management_Report_4);

                            break;

                        case SystemRoleEnum.Sys_Admin:
                            perms = new List<PermissionEnum>();
                            perms.Add(PermissionEnum.Add_Project_Files);
                            perms.Add(PermissionEnum.Reqst_Mtng);

                            perms.Add(PermissionEnum.Prlim_Estimat_Trads);
                            perms.Add(PermissionEnum.Prlim_Estimat_Zoning);
                            perms.Add(PermissionEnum.Prlim_Estimat_Fire);
                            perms.Add(PermissionEnum.Prlim_Estimat_Bkflow);
                            perms.Add(PermissionEnum.Prlim_Estimat_EHS);
                            perms.Add(PermissionEnum.Estimat_Trads);
                            perms.Add(PermissionEnum.Estimat_Zoning);
                            perms.Add(PermissionEnum.Estimat_Fire);
                            perms.Add(PermissionEnum.Estimat_Bkflow);
                            perms.Add(PermissionEnum.Estimat_EHS);

                            perms.Add(PermissionEnum.Schdul_Prlim_Mtng_Auto);
                            perms.Add(PermissionEnum.Schdul_Prlim_Mtng_Man);
                            perms.Add(PermissionEnum.Add_Prlim_Mtng_Prtcpnt);
                            perms.Add(PermissionEnum.Cancel_Prlim_Mtng);
                            perms.Add(PermissionEnum.Upload_Minuts);
                            perms.Add(PermissionEnum.Vw_Fclttor_Wrkload);
                            perms.Add(PermissionEnum.E_Fclttor);
                            perms.Add(PermissionEnum.Resend_Notif);
                            perms.Add(PermissionEnum.E_Plns_Rdy_Dt);
                            perms.Add(PermissionEnum.Accpt_Rjct_Rview_Dt);
                            perms.Add(PermissionEnum.Schdul_Rview_Pln_Rview_Auto);
                            perms.Add(PermissionEnum.Schdul_Rview_Pln_Rview_Man);
                            perms.Add(PermissionEnum.Activt_NA_Rview);
                            perms.Add(PermissionEnum.Vw_Schdul_Cpcty);
                            perms.Add(PermissionEnum.Schdul_Nxt_Cycl);
                            perms.Add(PermissionEnum.Schdul_Notes_Sel);
                            perms.Add(PermissionEnum.Vw_Mngmnt_Rprts);
                            perms.Add(PermissionEnum.Close_Mtng);
                            perms.Add(PermissionEnum.Cancel_Mtng);
                            perms.Add(PermissionEnum.Enter_Mtng_Prtcpnt);
                            perms.Add(PermissionEnum.Configure_Express);
                            perms.Add(PermissionEnum.E_Express_Rsrvtions);
                            perms.Add(PermissionEnum.E_Schdul_Express);
                            perms.Add(PermissionEnum.Man_Reserve_Express_Time);
                            perms.Add(PermissionEnum.Schdul_Express_Auto);
                            perms.Add(PermissionEnum.Schdul_Express_Man);
                            perms.Add(PermissionEnum.Use_Express);
                            perms.Add(PermissionEnum.Schdul_Collab_Mtng);
                            perms.Add(PermissionEnum.Schdul_PrePermitting_Mtng);
                            perms.Add(PermissionEnum.Schdul_Phasing_Mtng);
                            perms.Add(PermissionEnum.Schdul_Exit_Mtng);
                            perms.Add(PermissionEnum.Create_NPA);
                            perms.Add(PermissionEnum.Modify_NPA);
                            perms.Add(PermissionEnum.Apprv_Mtng_Minuts);
                            perms.Add(PermissionEnum.Assign_To_Me);
                            perms.Add(PermissionEnum.Reopen_Mtng);
                            perms.Add(PermissionEnum.Exit_Mtng_Notes_For_Cstmr);

                            //LES-305 3/30/22 add management reports
                            perms.Add(PermissionEnum.Management_Report_1);
                            perms.Add(PermissionEnum.Management_Report_2);
                            perms.Add(PermissionEnum.Management_Report_3);
                            perms.Add(PermissionEnum.Management_Report_4);

                            break;
                        default:
                            break;
                    }
                    //for each system role, add each permission
                    //get the permission list for the enums
                    List<Permission> permslist = new List<Permission>();
                    foreach (PermissionEnum en in perms)
                    {
                        permslist.Add(new PermissionModelBO().GetInstance((int)en));
                    }
                    foreach (Permission permission in permslist)
                    {
                        //insert the permission for the role
                        int xrefid = new PermissionModelBO().InsertSystemRoleXref(permission.ID, role.ID, "1");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in InsertDefaultRolePermissions - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;

            }
        }

        public List<Permission> GetPermissionList()
        {
            try
            {
                //future filtering?
                return new PermissionModelBO().CreateInstance();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetPermissions - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool SaveSystemRole(SystemRole systemRole)
        {
            try
            {
                //SystemRole systemRole = new SystemRole
                //{
                //    ParentSystemRoleId = vm.ParentSystemRoleId,
                //    RoleName = vm.SystemRoleName,
                //    Permissions = GetPermissionList(vm),
                //    UpdatedUser = new UserIdentity { ID = int.Parse(vm.WrkrId) }
                //};

                int systemroleid = systemRole.ID;
                string wrkid = systemRole.UpdatedUser.ID.ToString();
                SystemRoleBE systemRoleBE = new SystemRoleBE
                {
                    EnabledInd = true,
                    ExternalSystemRefId = 1,
                    ParentSystemRoleId = systemRole.ParentSystemRoleId,
                    SystemRoleNm = systemRole.RoleName,
                    SystemRoleId = systemroleid,
                    UserId = wrkid
                };
                SystemRoleBO systemRoleBO = new SystemRoleBO();
                if (systemroleid == 0)
                {
                    //insert
                    //make sure system role name is not blank
                    if (string.IsNullOrEmpty(systemRole.RoleName))
                        return false;
                    systemroleid = systemRoleBO.Create(systemRoleBE);
                    if (systemroleid > 0)
                    {
                        //save permissions for system role
                        PermissionModelBO permModelBO = new PermissionModelBO();
                        PermissionBO permissionBO = new PermissionBO();
                        foreach (Permission permission in systemRole.Permissions)
                        {
                            //get the permission id
                            //the list sent in only contains the enum mapping val nbr
                            Permission insert = permModelBO.GetInstance((PermissionEnum)permission.EnumMappingNumber);
                            permissionBO.InsertPermissionSystemRoleXref(insert.PermissionId.Value, systemroleid, systemRoleBE.UserId);

                        }
                    }
                }
                else
                {
                    UpdateSystemRolePermissions(systemRole);
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveSystemRole - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        public bool UpdateSystemRolePermissions(SystemRole systemRole)
        {
            try
            {
                int systemroleid = systemRole.ID;
                string wrkid = systemRole.UpdatedUser.ID.ToString();

                //update the xref
                PermissionModelBO permModelBO = new PermissionModelBO();
                PermissionBO permissionBO = new PermissionBO();
                //delete permissions
                permissionBO.DeleteBySystemRoleId(systemroleid, wrkid);
                //insert permissions
                foreach (Permission permission in systemRole.Permissions)
                {
                    //get the permission id
                    //the list sent in only contains the enum mapping val nbr
                    Permission insert = permModelBO.GetInstance((PermissionEnum)permission.EnumMappingNumber);
                    if (insert != null && insert.PermissionId.HasValue)
                        permissionBO.InsertPermissionSystemRoleXref(insert.PermissionId.Value, systemroleid, wrkid);

                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateSystemRolePermissions - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return true;
        }

        public bool SaveUserPermissions(UserPermissionsSaveModel model)
        {
            try
            {
                PermissionBO permissionBO = new PermissionBO();
                PermissionModelBO permModelBO = new PermissionModelBO();

                //delete user permissions
                permissionBO.DeleteByUserId(model.UserId, model.WrkrId);
                //insert user permissions
                foreach (Permission permission in model.Permissions)
                {
                    Permission insert = permModelBO.GetInstance((PermissionEnum)permission.EnumMappingNumber);
                    if (insert != null && insert.PermissionId.HasValue)
                        permissionBO.InsertPermissionUserXref(insert.PermissionId.Value, model.UserId, model.WrkrId);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SaveUserPermissions - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return true;
        }
    }

    public interface IPermissionAdapter
    {
        /// <summary>
        /// Get PermissionMapping object by user id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        PermissionMapping GetPermissionMappingByUserId(int userid);

        /// <summary>
        /// Insert Role Permissions by role id
        /// </summary>
        /// <param name="systemroleid"></param>
        /// <param name="perms"></param>
        /// <param name="wrkrid"></param>
        /// <returns></returns>
        bool InsertRolePermissions(int systemroleid, List<int> perms, string wrkrid);

        /// <summary>
        /// Used for the default initialization only
        /// </summary>
        bool InsertDefaultRolePermissions();
        /// <summary>
        /// insert System Role Permission
        /// </summary>
        /// <param name="systemroleid"></param>
        /// <param name="permissionid"></param>
        /// <param name="wkrid"></param>
        /// <returns></returns>
        bool InsertRolePermission(int systemroleid, int permissionid, string wkrid);

        /// <summary>
        /// Gets list of permissions
        /// Used in Admin for Create Role and Modify Permissions
        /// </summary>
        /// <returns></returns>
        List<Permission> GetPermissionList();

        /// <summary>
        /// Saves user entered system role
        /// Used in Admin for Create Role and Modify Permissions
        /// Saves a new system role if system role id = 0
        /// Updates the permissions for system role if id > 0
        /// </summary>
        /// <param name="systemRole"></param>
        /// <returns></returns>
        bool SaveSystemRole(SystemRole systemRole);

        /// <summary>
        /// Update permissions for system role
        /// uses Permissions property
        /// </summary>
        /// <param name="systemRole"></param>
        /// <returns></returns>
        bool UpdateSystemRolePermissions(SystemRole systemRole);
        bool SaveUserPermissions(UserPermissionsSaveModel model);
    }
}
