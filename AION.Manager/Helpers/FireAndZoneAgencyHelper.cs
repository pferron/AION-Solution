using AION.BL;
using AION.BL.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Helpers
{
    public class FireAndZoneAgencyHelper
    {
        private static List<DepartmentNameEnums> _firedepartmentnames;
        private static List<DepartmentNameEnums> _zonedepartmentnames;

        public static List<DepartmentNameEnums> FireDepartmentNames
        {
            get
            {
                _firedepartmentnames = new List<DepartmentNameEnums>();
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Cornelius);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Cty_Chrlt);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Davidson);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Huntersville);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Matthews);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Mint_Hill);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_Pineville);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_UMC);
                _firedepartmentnames.Add(DepartmentNameEnums.Fire_County);

                return _firedepartmentnames;
            }
        }

        public static List<DepartmentNameEnums> ZoneDepartmentNames
        {
            get
            {
                _zonedepartmentnames = new List<DepartmentNameEnums>();
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Cornelius);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Cty_Chrlt);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Davidson);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Huntersville);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Matthews);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Mint_Hill);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_Pineville);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_UMC);
                _zonedepartmentnames.Add(DepartmentNameEnums.Zone_County);

                return _zonedepartmentnames;
            }
        }

        public static bool IsFireAgency(int businessRefId)
        {
            DepartmentModelBO dept_bo = new DepartmentModelBO();

            Department dept = dept_bo.BaseList.FirstOrDefault(x => x.ID == businessRefId);

            if (FireDepartmentNames.Contains(dept.DepartmentEnum))
            {
                return true;
            }
            return false;
        }

        public static bool IsZoneAgency(int businessRefId)
        {
            DepartmentModelBO dept_bo = new DepartmentModelBO();

            Department dept = dept_bo.BaseList.FirstOrDefault(x => x.ID == businessRefId);

            if (ZoneDepartmentNames.Contains(dept.DepartmentEnum))
            {
                return true;
            }
            return false;
        }
    }
}