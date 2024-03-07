using AION.BL;

namespace AION.Manager.Helpers
{
    public class DashboardHelper
    {

        public static string GetJurisdictionByDepartmentNameEnum(DepartmentNameEnums agencyEnum)
        {
            switch (agencyEnum)
            {
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Zone_Davidson:
                    return JurisdictionEnum.Davidson.ToStringValue();
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Zone_Cornelius:
                    return JurisdictionEnum.Cornelius.ToStringValue();
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Zone_Pineville:
                    return JurisdictionEnum.Pineville.ToStringValue();
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Zone_Matthews:
                    return JurisdictionEnum.Matthews.ToStringValue();
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Zone_Mint_Hill:
                    return JurisdictionEnum.Mint_Hill.ToStringValue();
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Zone_Huntersville:
                    return JurisdictionEnum.Huntersville.ToStringValue();
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Zone_UMC:
                    return JurisdictionEnum.County.ToStringValue();
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                    return JurisdictionEnum.Charlotte.ToStringValue();
                case DepartmentNameEnums.Fire_County:
                case DepartmentNameEnums.Zone_County:
                    return JurisdictionEnum.County.ToStringValue();
                default:
                    break;
            }
            return string.Empty;
        }
    }
}