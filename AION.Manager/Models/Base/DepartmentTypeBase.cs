using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL
{
    public abstract class DepartmentTypeBase: ModelBase
    {

        public string DepartmentTypeCode { get; set; }

        public string DepartmentTypeName { get; set; }

        public string DepartmentTypeExternalRef { get; set; }

        public ExternalSystem ExternalSystem { get; set; }

        public int EnumMappingVal { get; set; }

    }

    public class DepartmentTypeInfo: DepartmentTypeBase
    {
        public DepartmentTypeEnum DepartmentType { get; set; }
    }


    public class DepartmentDivisionInfo : DepartmentTypeBase
    {
        public DepartmentDivisionEnum DepartmentDivision { get; set; }
    }

    public class DepartmentRegionInfo : DepartmentTypeBase
    {
        public DepartmentRegionEnum DepartmentRegion { get; set; }
    }
}

