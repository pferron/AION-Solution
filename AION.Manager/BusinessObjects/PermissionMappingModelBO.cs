using AION.BL;
using AION.BL.BusinessObjects;
using AION.Manager.Models;
using AION.Web.BusinessEntities;
using System.Collections.Generic;

namespace AION.Manager.BusinessObjects
{
    public class PermissionMappingModelBO : IPermissionMappingModelBO
    {
        public PermissionMapping GetInstance(List<SystemRole> roles)
        {
            PermissionMapping ret = new PermissionMapping();
            //incase of isseus with user or roles not assigned just return an empty object.
            if (roles == null)
                return ret;
            foreach (SystemRole role in roles)
            {
                switch (role.SystemRoleEnum)
                {
                    case SystemRoleEnum.NA:
                        break;
                    case SystemRoleEnum.Estimator:
                        ret.IsTradeEstimator = true;
                        ret.IsAgencyEstimator = true;
                        ret.ISMMFEstimator = true;
                        break;
                    case SystemRoleEnum.Plan_Reviewer:
                        ret.IsTradePlanReviewer = true;
                        ret.IsAgencyPlanReviewer = true;
                        break;
                    case SystemRoleEnum.Facilitator:
                        ret.IsFacilitator = true;
                        break;
                    case SystemRoleEnum.External_Project_Manager:
                        ret.IsCustomer = true;
                        break;
                    case SystemRoleEnum.Manager:
                        ret.IsManager = true;
                        ret.Building = true;
                        ret.IsBEMPAllowed = true;
                        ret.Electrical = true;
                        ret.IsBEMPAllowed = true;
                        ret.Mechanical = true;
                        ret.IsBEMPAllowed = true;
                        ret.Plumbing = true;
                        ret.IsBEMPAllowed = true;
                        ret.ZoneDavidson = true;
                        ret.ZoneCornelius = true;
                        ret.ZonePineVille = true;
                        ret.ZoneMintHill = true;
                        ret.ZoneMatthews = true;
                        ret.ZoneHuntersVille = true;
                        ret.ZoneUNC = true;
                        ret.ZoneCityCharlotte = true;
                        ret.IsZoneAllowed = true;
                        ret.FireDavidson = true;
                        ret.FireCornelius = true;
                        ret.FirePineVille = true;
                        ret.FireMatthews = true;
                        ret.FireMintHill = true;
                        ret.FireHuntersVille = true;
                        ret.FireUNC = true;
                        ret.FireCityCharlotte = true;
                        ret.IsFireAllowed = true;
                        ret.EHDayCare = true;
                        ret.IsHealthAllowed = true;
                        ret.EHFood = true;
                        ret.EHPool = true;
                        ret.EHFacilities = true;
                        ret.BackFlow = true;
                        ret.IsBackFlowAllowed = true;
                        break;
                    case SystemRoleEnum.View_Only:
                        ret.IsViewOnly = true;
                        break;
                    case SystemRoleEnum.Sys_Admin:
                        ret.IsManager = true;
                        ret.Building = true;
                        ret.IsBEMPAllowed = true;
                        ret.Electrical = true;
                        ret.IsBEMPAllowed = true;
                        ret.Mechanical = true;
                        ret.IsBEMPAllowed = true;
                        ret.Plumbing = true;
                        ret.IsBEMPAllowed = true;
                        ret.ZoneDavidson = true;
                        ret.ZoneCornelius = true;
                        ret.ZonePineVille = true;
                        ret.ZoneMintHill = true;
                        ret.ZoneMatthews = true;
                        ret.ZoneHuntersVille = true;
                        ret.ZoneUNC = true;
                        ret.ZoneCityCharlotte = true;
                        ret.IsZoneAllowed = true;
                        ret.FireDavidson = true;
                        ret.FireCornelius = true;
                        ret.FirePineVille = true;
                        ret.FireMatthews = true;
                        ret.FireMintHill = true;
                        ret.FireHuntersVille = true;
                        ret.FireUNC = true;
                        ret.FireCityCharlotte = true;
                        ret.IsFireAllowed = true;
                        ret.EHDayCare = true;
                        ret.IsHealthAllowed = true;
                        ret.EHFood = true;
                        ret.EHPool = true;
                        ret.EHFacilities = true;
                        ret.BackFlow = true;
                        ret.IsBackFlowAllowed = true;
                        break;
                    default:
                        break;
                }

            }

            return ret;
        }
        public PermissionMapping GetMappingByPerms(List<Permission> permissions, PermissionMapping pm)
        {
            foreach (Permission permission in permissions)
            {
                switch (permission.PermissionEnum)
                {
                    case PermissionEnum.Add_Project_Files:
                        pm.Add_Project_Files = true;
                        break;
                    case PermissionEnum.Prlim_Estimat_Trads:
                        pm.Prlim_Estimat_Trads = true;
                        pm.Building = true;
                        pm.Electrical = true;
                        pm.Mechanical = true;
                        pm.Plumbing = true;
                        pm.IsBEMPAllowed = true;
                        pm.IsTradeEstimator = true;
                        break;
                    case PermissionEnum.Prlim_Estimat_Zoning:
                        pm.Prlim_Estimat_Zoning = true;
                        pm.ZoneCityCharlotte = true;
                        pm.ZoneCornelius = true;
                        pm.ZoneDavidson = true;
                        pm.ZoneHuntersVille = true;
                        pm.ZoneMatthews = true;
                        pm.ZoneMintHill = true;
                        pm.ZonePineVille = true;
                        pm.ZoneUNC = true;
                        pm.IsZoneAllowed = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Prlim_Estimat_Fire:
                        pm.FireDavidson = true;
                        pm.FireCornelius = true;
                        pm.FirePineVille = true;
                        pm.FireMatthews = true;
                        pm.FireMintHill = true;
                        pm.FireHuntersVille = true;
                        pm.FireUNC = true;
                        pm.FireCityCharlotte = true;
                        pm.IsFireAllowed = true;
                        pm.Prlim_Estimat_Fire = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Prlim_Estimat_Bkflow:
                        pm.IsBackFlowAllowed = true;
                        pm.Prlim_Estimat_Bkflow = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Prlim_Estimat_EHS:
                        pm.IsHealthAllowed = true;
                        pm.EHDayCare = true;
                        pm.EHFacilities = true;
                        pm.EHFood = true;
                        pm.EHPool = true;
                        pm.Prlim_Estimat_EHS = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Schdul_Prlim_Mtng_Auto:
                        pm.Schdul_Prlim_Mtng_Auto = true;
                        break;
                    case PermissionEnum.Schdul_Prlim_Mtng_Man:
                        pm.Schdul_Prlim_Mtng_Man = true;
                        break;
                    case PermissionEnum.Add_Prlim_Mtng_Prtcpnt:
                        pm.Add_Prlim_Mtng_Prtcpnt = true;
                        break;
                    case PermissionEnum.Cancel_Prlim_Mtng:
                        pm.Cancel_Prlim_Mtng = true;
                        break;
                    case PermissionEnum.Apprv_Mtng_Minuts:
                        pm.Apprv_Mtng_Minuts = true;
                        break;
                    case PermissionEnum.Upload_Minuts:
                        pm.Upload_Minuts = true;
                        break;
                    case PermissionEnum.Estimat_Trads:
                        pm.Building = true;
                        pm.Electrical = true;
                        pm.Mechanical = true;
                        pm.Plumbing = true;
                        pm.IsBEMPAllowed = true;
                        pm.Estimat_Trads = true;
                        pm.IsTradeEstimator = true;
                        break;
                    case PermissionEnum.Estimat_Zoning:
                        pm.ZoneCityCharlotte = true;
                        pm.ZoneCornelius = true;
                        pm.ZoneDavidson = true;
                        pm.ZoneHuntersVille = true;
                        pm.ZoneMatthews = true;
                        pm.ZoneMintHill = true;
                        pm.ZonePineVille = true;
                        pm.ZoneUNC = true;
                        pm.IsZoneAllowed = true;
                        pm.Estimat_Zoning = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Estimat_Fire:
                        pm.FireDavidson = true;
                        pm.FireCornelius = true;
                        pm.FirePineVille = true;
                        pm.FireMatthews = true;
                        pm.FireMintHill = true;
                        pm.FireHuntersVille = true;
                        pm.FireUNC = true;
                        pm.FireCityCharlotte = true;
                        pm.IsFireAllowed = true;
                        pm.Estimat_Fire = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Estimat_Bkflow:
                        pm.IsBackFlowAllowed = true;
                        pm.Estimat_Bkflow = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Estimat_EHS:
                        pm.IsHealthAllowed = true;
                        pm.EHDayCare = true;
                        pm.EHFacilities = true;
                        pm.EHFood = true;
                        pm.EHPool = true;
                        pm.Estimat_EHS = true;
                        pm.IsAgencyEstimator = true;
                        break;
                    case PermissionEnum.Vw_Fclttor_Wrkload:
                        pm.Vw_Fclttor_Wrkload = true;
                        break;
                    case PermissionEnum.E_Fclttor:
                        pm.E_Fclttor = true;
                        break;
                    case PermissionEnum.Resend_Notif:
                        pm.Resend_Notif = true;
                        break;
                    case PermissionEnum.E_Plns_Rdy_Dt:
                        pm.E_Plns_Rdy_Dt = true;
                        break;
                    case PermissionEnum.Accpt_Rjct_Rview_Dt:
                        pm.Accpt_Rjct_Rview_Dt = true;
                        break;
                    case PermissionEnum.Schdul_Rview_Pln_Rview_Auto:
                        pm.Schdul_Rview_Pln_Rview_Auto = true;
                        break;
                    case PermissionEnum.Schdul_Rview_Pln_Rview_Man:
                        pm.Schdul_Rview_Pln_Rview_Man = true;
                        break;
                    case PermissionEnum.Activt_NA_Rview:
                        pm.Activt_NA_Rview = true;
                        break;
                    case PermissionEnum.Vw_Schdul_Cpcty:
                        pm.Vw_Schdul_Cpcty = true;
                        break;
                    case PermissionEnum.Schdul_Nxt_Cycl:
                        pm.Schdul_Nxt_Cycl = true;
                        break;
                    case PermissionEnum.Assign_To_Me:
                        pm.Assign_To_Me = true;
                        break;
                    case PermissionEnum.Schdul_Notes_Sel:
                        pm.Schdul_Notes_Sel = true;
                        break;
                    case PermissionEnum.Vw_Mngmnt_Rprts:
                        pm.Vw_Mngmnt_Rprts = true;
                        break;
                    case PermissionEnum.Reqst_Mtng:
                        pm.Reqst_Mtng = true;
                        break;
                    case PermissionEnum.Reopen_Mtng:
                        pm.Reopen_Mtng = true;
                        break;
                    case PermissionEnum.Close_Mtng:
                        pm.Close_Mtng = true;
                        break;
                    case PermissionEnum.Cancel_Mtng:
                        pm.Cancel_Mtng = true;
                        break;
                    case PermissionEnum.Exit_Mtng_Notes_For_Cstmr:
                        pm.Exit_Mtng_Notes_For_Cstmr = true;
                        break;
                    case PermissionEnum.Enter_Mtng_Prtcpnt:
                        pm.Enter_Mtng_Prtcpnt = true;
                        break;
                    case PermissionEnum.Configure_Express:
                        pm.Configure_Express = true;
                        break;
                    case PermissionEnum.E_Express_Rsrvtions:
                        pm.E_Express_Rsrvtions = true;
                        break;
                    case PermissionEnum.E_Schdul_Express:
                        pm.E_Schdul_Express = true;
                        break;
                    case PermissionEnum.Man_Reserve_Express_Time:
                        pm.Man_Reserve_Express_Time = true;
                        break;
                    case PermissionEnum.Schdul_Express_Auto:
                        pm.Schdul_Express_Auto = true;
                        break;
                    case PermissionEnum.Schdul_Express_Man:
                        pm.Schdul_Express_Man = true;
                        break;
                    case PermissionEnum.Use_Express:
                        pm.Use_Express = true;
                        break;
                    case PermissionEnum.Schdul_Collab_Mtng:
                        pm.Schdul_Collab_Mtng = true;
                        break;
                    case PermissionEnum.Schdul_PrePermitting_Mtng:
                        pm.Schdul_PrePermitting_Mtng = true;
                        break;
                    case PermissionEnum.Schdul_Phasing_Mtng:
                        pm.Schdul_Phasing_Mtng = true;
                        break;
                    case PermissionEnum.Schdul_Exit_Mtng:
                        pm.Schdul_Exit_Mtng = true;
                        break;
                    case PermissionEnum.Create_NPA:
                        pm.Create_NPA = true;
                        break;
                    case PermissionEnum.Modify_NPA:
                        pm.Modify_NPA = true;
                        break;
                    case PermissionEnum.Holday_Config:
                        pm.Holday_Config = true;
                        break;
                    case PermissionEnum.Schdul_Mtng:
                        pm.Schdul_Mtng = true;
                        break;
                    //LES-305 add management reports
                    case PermissionEnum.Management_Report_1:
                        pm.Management_Report_1 = true;
                        break;
                    case PermissionEnum.Management_Report_2:
                        pm.Management_Report_2 = true;
                        break;
                    case PermissionEnum.Management_Report_3:
                        pm.Management_Report_3 = true;
                        break;
                    case PermissionEnum.Management_Report_4:
                        pm.Management_Report_4 = true;
                        break;
                    default:
                        break;
                }
            }

            //check for access pm props
            pm.Access_Estimation = pm.Estimat_Bkflow
                || pm.Estimat_EHS
                || pm.Estimat_Fire
                || pm.Estimat_Trads
                || pm.Estimat_Zoning
                || pm.IsViewOnly;
            pm.Access_PrelimEstimation = pm.Prlim_Estimat_Bkflow
                || pm.Prlim_Estimat_EHS
                || pm.Prlim_Estimat_Fire
                || pm.Prlim_Estimat_Trads
                || pm.Prlim_Estimat_Zoning
                || pm.IsViewOnly;
            pm.Access_ReserveExpress = pm.Configure_Express
                || pm.E_Express_Rsrvtions
                || pm.Man_Reserve_Express_Time
                || pm.IsViewOnly;
            pm.Access_ScheduleExpress = pm.Schdul_Express_Auto
                || pm.Schdul_Express_Man
                || pm.IsViewOnly;
            pm.Access_ScheduleMeeting = pm.Schdul_Mtng
                || pm.IsViewOnly;
            pm.Access_SchedulePR = pm.Schdul_Rview_Pln_Rview_Auto
                || pm.Schdul_Rview_Pln_Rview_Man
                || pm.Schdul_Nxt_Cycl
                || pm.IsViewOnly;
            pm.Access_SchedulePrelim = pm.Schdul_Prlim_Mtng_Auto
                || pm.Schdul_Prlim_Mtng_Man
                || pm.IsViewOnly;


            return pm;
        }
        public PermissionMapping GetInstance(int userid)
        {
            PermissionModelBO permissionModelBO = new PermissionModelBO();
            List<Permission> allpermissions = new List<Permission>();
            //get roles
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
            List<SystemRole> roles = userIdentityModelBO.GetUserSystemRolesEnumsByUserId(userid);
            PermissionMapping pm = GetInstance(roles);

            //get permissions by user id
            //this includes any system permissions
            List<Permission> userpermissions = new PermissionModelBO().GetByUserID(userid);
            allpermissions.AddRange(userpermissions);

            //convert to permission mapping
            pm = GetMappingByPerms(allpermissions, pm);

            return pm;
        }

    }

    public interface IPermissionMappingModelBO
    {
        PermissionMapping GetInstance(List<SystemRole> roles);
    }
}
