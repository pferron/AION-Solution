namespace AION.BL
{

    public class Department : ModelBase
    {

        public string DepartmentName { get; set; }

        public string DepartmentCd { get; set; }

        public string ExternalRefInfo { get; set; }

        public int ExternalSystemID { get; set; }

        public DepartmentNameEnums DepartmentEnum { get; set; }

        public DepartmentTypeInfo DepartmentType { get; set; }

        public DepartmentDivisionInfo DepartmentDivision { get; set; }

        public DepartmentRegionInfo DepartmentRegion { get; set; }
        public string DepartmentStatus { get; set; }
    }
}
