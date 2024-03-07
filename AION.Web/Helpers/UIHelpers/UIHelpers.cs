using AION.BL;
using AION.BL.Models;
using AION.Web.BusinessEntities;
using AION.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AION.Web.Helpers
{
    public class UIHelpers
    {
        public bool SetPermissionsForEstimationUI(IEstimationViewModel model)
        {
            if (model.PermissionMapping.IsManager || model.PermissionMapping.IsTradeEstimator)
            {
                model.CanEditBEMP = true;
                model.CanEditFire = true;
                model.CanEditHealth = true;
                model.CanEditZoning = true;
                model.CanEditBackFlow = true;
                model.CanViewBEMP = true;
                model.CanViewFire = true;
                model.CanViewHealth = true;
                model.CanViewZoning = true;
                model.CanViewBackFlow = true;
            }
            else if (model.PermissionMapping.IsAgencyEstimator)
            {
                if (model.PermissionMapping.IsZoneAllowed == true)
                {
                    model.CanViewZoning = true;
                    model.CanEditZoning = true;
                }
                if (model.PermissionMapping.IsHealthAllowed == true)
                {
                    model.CanViewHealth = true;
                    model.CanEditHealth = true;
                }

                if (model.PermissionMapping.BackFlow == true)
                {
                    model.CanViewBackFlow = true;
                    model.CanEditBackFlow = true;
                }
            }
            return true;
        }
        internal static string ConvertUserUISettingsJsonToDashboardString(string jsonstring, string dashboardtype)
        {
            UiSettings uisettings = ConvertJsonStringToUiSettings(jsonstring);
            switch (dashboardtype)
            {
                case "estimation":
                    return uisettings.EstimationDashboard.ColumnsFilter;
                case "meeting":
                    return uisettings.MeetingDashboard.ColumnsFilter;
                case "scheduling":
                    return uisettings.SchedulingDashboard.ColumnsFilter;
                default:
                    return string.Empty;
            }
        }
        public List<DepartmentNameEnums> GetAgencyNamesForSystemRole(PermissionMapping pm)
        {
            List<DepartmentNameEnums> departments = new List<DepartmentNameEnums>();

            if (pm.Prlim_Estimat_Zoning || pm.Estimat_Zoning)
            {
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Davidson);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Cornelius);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Pineville);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Matthews);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Mint_Hill);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Huntersville);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_UMC);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_Cty_Chrlt);
                DepartmentsAdd(departments, DepartmentNameEnums.Zone_County);

            }

            if (pm.Prlim_Estimat_Fire || pm.Estimat_Fire)
            {
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Cornelius);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Davidson);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Huntersville);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Matthews);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Mint_Hill);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Pineville);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_UMC);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_Cty_Chrlt);
                DepartmentsAdd(departments, DepartmentNameEnums.Fire_County);

            }

            if (pm.Prlim_Estimat_EHS || pm.Estimat_EHS)
            {
                DepartmentsAdd(departments, DepartmentNameEnums.EH_Day_Care);
                DepartmentsAdd(departments, DepartmentNameEnums.EH_Food);
                DepartmentsAdd(departments, DepartmentNameEnums.EH_Pool);
                DepartmentsAdd(departments, DepartmentNameEnums.EH_Facilities);
            }
            if (pm.Prlim_Estimat_Bkflow || pm.Estimat_Bkflow)
                DepartmentsAdd(departments, DepartmentNameEnums.Backflow);

            return departments;
        }
        private void DepartmentsAdd(List<DepartmentNameEnums> departments, DepartmentNameEnums enumobj)
        {
            if (!departments.Contains(enumobj))
                departments.Add(enumobj);
        }

        internal static UiSettings ConvertJsonStringToUiSettings(string jsonstring)
        {
            UiSettings uisettings = new UiSettings();
            uisettings.EstimationDashboard = new DashboardUiSetting();
            uisettings.MeetingDashboard = new DashboardUiSetting();
            uisettings.SchedulingDashboard = new DashboardUiSetting();
            if (!string.IsNullOrWhiteSpace(jsonstring))
            {
                uisettings = JsonConvert.DeserializeObject<UiSettings>(jsonstring);
            }
            return uisettings;
        }
        internal static string ConvertUiSettingsToJsonString(UiSettings uisettings)
        {
            return JsonConvert.SerializeObject(uisettings);
        }
    }
}