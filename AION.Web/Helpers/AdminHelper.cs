using AION.BL;

namespace AION.Web.Helpers
{
    public class AdminHelper
    {

        public static int GetDepartmentNameEnumsByJurisdiction(JurisdictionEnum jurisdictionEnum, UserAdminViewModelDeptNameEnum deptNameEnum)
        {
            if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire || deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
            {

                switch (jurisdictionEnum)
                {
                    case JurisdictionEnum.County:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_County;

                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_County;
                        }
                        break;
                    case JurisdictionEnum.Charlotte:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Cty_Chrlt;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Cty_Chrlt;
                        }
                        break;
                    case JurisdictionEnum.Cornelius:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Cornelius;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Cornelius;
                        }
                        break;
                    case JurisdictionEnum.Davidson:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Davidson;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Davidson;
                        }
                        break;
                    case JurisdictionEnum.Huntersville:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Huntersville;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Huntersville;
                        }
                        break;
                    case JurisdictionEnum.Matthews:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Matthews;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Matthews;
                        }
                        break;
                    case JurisdictionEnum.Mint_Hill:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Mint_Hill;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Mint_Hill;
                        }
                        break;
                    case JurisdictionEnum.Pineville:
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Zoning)
                        {
                            return (int)DepartmentNameEnums.Zone_Pineville;
                        }
                        if (deptNameEnum == UserAdminViewModelDeptNameEnum.Fire)
                        {
                            return (int)DepartmentNameEnums.Fire_Pineville;
                        }
                        break;
                    default:
                        break;
                }
            }
            return 0;
        }
    }
}