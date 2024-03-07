using AION.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Common
{
    public class Helper
    {
        private List<DepartmentNameEnums> _firedepartmentnames;
        private List<DepartmentNameEnums> _countyfiredepartmentnames;

        private List<DepartmentNameEnums> _zonedepartmentnames;
        private List<DepartmentNameEnums> _ehsdepartmentnames;
        private List<DepartmentNameEnums> _bempdepartmentnames;
        private List<DepartmentNameEnums> _alldepartmentnames;
        private List<DepartmentNameEnums> _countyzonedepartmentnames;

        private List<DepartmentNameEnums> _cityJurisdiction;
        public List<DepartmentNameEnums> CityJurisdiction
        {
            get
            {
                _cityJurisdiction = new List<DepartmentNameEnums>();
                _cityJurisdiction.Add(DepartmentNameEnums.Fire_Cty_Chrlt);
                _cityJurisdiction.Add(DepartmentNameEnums.Zone_Cty_Chrlt);
                return _cityJurisdiction;
            }
        }

        private List<DepartmentNameEnums> _huntersvilledJurisdiction;
        public List<DepartmentNameEnums> HuntersvilleJurisdiction
        {
            get
            {
                _huntersvilledJurisdiction = new List<DepartmentNameEnums>();
                _huntersvilledJurisdiction.Add(DepartmentNameEnums.Fire_Huntersville);
                _huntersvilledJurisdiction.Add(DepartmentNameEnums.Zone_Huntersville);
                return _huntersvilledJurisdiction;
            }
        }
        private List<DepartmentNameEnums> _mintHillJurisdiction;
        public List<DepartmentNameEnums> MintHillJurisdiction
        {
            get
            {
                _mintHillJurisdiction = new List<DepartmentNameEnums>();
                _mintHillJurisdiction.Add(DepartmentNameEnums.Fire_Mint_Hill);
                _mintHillJurisdiction.Add(DepartmentNameEnums.Zone_Mint_Hill);
                return _mintHillJurisdiction;
            }
        }
        /// <summary>
        /// Gets list used in UserIdentityModelBO for search filter
        /// </summary>
        private List<SystemRoleEnum> _filterModeSearchEnums;
        public List<SystemRoleEnum> FilterModeSearchEnums
        {
            get
            {
                _filterModeSearchEnums = new List<SystemRoleEnum>();
                _filterModeSearchEnums.Add(SystemRoleEnum.Estimator);
                _filterModeSearchEnums.Add(SystemRoleEnum.Manager);
                _filterModeSearchEnums.Add(SystemRoleEnum.Facilitator);
                _filterModeSearchEnums.Add(SystemRoleEnum.View_Only);
                _filterModeSearchEnums.Add(SystemRoleEnum.Plan_Reviewer);
                return _filterModeSearchEnums;
            }
        }
        /// <summary>
        /// List of all department name enums (DepartmentNameEnums) except NA
        /// </summary>
        public List<DepartmentNameEnums> AllDepartmentNames
        {
            get
            {
                _alldepartmentnames = Enum<DepartmentNameEnums>.ToList().Where(x => (int)x > 0).ToList();
                return _alldepartmentnames;

            }
        }
        /// <summary>
        /// List of COUNTY Fire department name enums
        /// (excludes city of charlotte)
        /// </summary>
        public List<DepartmentNameEnums> CountyFireDepartmentNames
        {
            get
            {
                _countyfiredepartmentnames = new List<DepartmentNameEnums>();
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_Cornelius);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_Davidson);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_Huntersville);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_Matthews);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_Mint_Hill);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_Pineville);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_UMC);
                _countyfiredepartmentnames.Add(DepartmentNameEnums.Fire_County);

                return _countyfiredepartmentnames;
            }
        }

        /// <summary>
        /// List of Fire department name enums
        /// </summary>
        public List<DepartmentNameEnums> FireDepartmentNames
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
        /// <summary>
        /// List of Zone department enums
        /// </summary>
        public List<DepartmentNameEnums> ZoneDepartmentNames
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
        /// <summary>
        /// List of COUNTY Zone department enums
        /// excludes city of charlotte, mint hill and huntersville
        /// </summary>
        public List<DepartmentNameEnums> CountyZoneDepartmentNames
        {
            get
            {
                _countyzonedepartmentnames = new List<DepartmentNameEnums>();
                _countyzonedepartmentnames.Add(DepartmentNameEnums.Zone_Cornelius);
                _countyzonedepartmentnames.Add(DepartmentNameEnums.Zone_Davidson);
                _countyzonedepartmentnames.Add(DepartmentNameEnums.Zone_Matthews);
                _countyzonedepartmentnames.Add(DepartmentNameEnums.Zone_Pineville);
                _countyzonedepartmentnames.Add(DepartmentNameEnums.Zone_UMC);
                _countyzonedepartmentnames.Add(DepartmentNameEnums.Zone_County);

                return _countyzonedepartmentnames;
            }
        }

        /// <summary>
        /// List of EHS department name enums
        /// </summary>
        public List<DepartmentNameEnums> EhsDepartmentNames
        {
            get
            {
                _ehsdepartmentnames = new List<DepartmentNameEnums>();
                _ehsdepartmentnames.Add(DepartmentNameEnums.EH_Day_Care);
                _ehsdepartmentnames.Add(DepartmentNameEnums.EH_Facilities);
                _ehsdepartmentnames.Add(DepartmentNameEnums.EH_Food);
                _ehsdepartmentnames.Add(DepartmentNameEnums.EH_Pool);
                return _ehsdepartmentnames;
            }
        }
        /// <summary>
        /// List of BEMP department name enums
        /// </summary>
        public List<DepartmentNameEnums> BEMPDepartmentNames
        {
            get
            {
                _bempdepartmentnames = new List<DepartmentNameEnums>();
                _bempdepartmentnames.Add(DepartmentNameEnums.Building);
                _bempdepartmentnames.Add(DepartmentNameEnums.Electrical);
                _bempdepartmentnames.Add(DepartmentNameEnums.Mechanical);
                _bempdepartmentnames.Add(DepartmentNameEnums.Plumbing);
                return _bempdepartmentnames;
            }
        }
        /// <summary>
        /// Gets a list of enums based on the enum sent
        /// ex. Input is Zone_Davidson enum, the whole list of all zones is returned
        /// If NA is sent, the whole list of departments is returned
        /// This is used by the method to get the reviewers by dept
        /// </summary>
        /// <param name="deptNameEnum"></param>
        /// <returns></returns>
        public List<DepartmentNameEnums> DepartmentNamesEnums(DepartmentNameEnums deptNameEnum)
        {
            List<DepartmentNameEnums> departmentNameEnums = new List<DepartmentNameEnums>();
            switch (deptNameEnum)
            {
                case DepartmentNameEnums.Building:
                    departmentNameEnums.Add(DepartmentNameEnums.Building);
                    break;
                case DepartmentNameEnums.Electrical:
                    departmentNameEnums.Add(DepartmentNameEnums.Electrical);
                    break;
                case DepartmentNameEnums.Mechanical:
                    departmentNameEnums.Add(DepartmentNameEnums.Mechanical);
                    break;
                case DepartmentNameEnums.Plumbing:
                    departmentNameEnums.Add(DepartmentNameEnums.Plumbing);
                    break;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return ZoneDepartmentNames;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return FireDepartmentNames;
                case DepartmentNameEnums.EH_Day_Care:
                    departmentNameEnums.Add(DepartmentNameEnums.EH_Day_Care);
                    break;
                case DepartmentNameEnums.EH_Facilities:
                    departmentNameEnums.Add(DepartmentNameEnums.EH_Facilities);
                    break;
                case DepartmentNameEnums.EH_Food:
                    departmentNameEnums.Add(DepartmentNameEnums.EH_Food);
                    break;
                case DepartmentNameEnums.EH_Pool:
                    departmentNameEnums.Add(DepartmentNameEnums.EH_Pool);
                    break;
                case DepartmentNameEnums.Backflow:
                    departmentNameEnums.Add(DepartmentNameEnums.Backflow);
                    break;
                case DepartmentNameEnums.NA:
                    //return all depts except NA
                    departmentNameEnums.AddRange(AllDepartmentNames);

                    break;
                default:
                    return new List<DepartmentNameEnums>();
            }
            return departmentNameEnums;
        }

        private static List<TimeAllocationType> _timeAllocationTypes;
        public static List<TimeAllocationType> TimeAllocationTypes
        {
            get
            {
                _timeAllocationTypes = new List<TimeAllocationType>();
                _timeAllocationTypes = Enum.GetValues(typeof(TimeAllocationType)).Cast<TimeAllocationType>().ToList();
                return _timeAllocationTypes;
            }
        }
    }
}
