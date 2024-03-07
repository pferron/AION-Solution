using AION.BL;
using AION.BL.Common;
using AION.BL.Models.Base;
using System.Linq;

namespace AION.Manager.Helpers
{
    public class ProjectHelper
    {
        public static ProjectDepartment GetDepartment(Project Project, DepartmentNameEnums deptType)
        {
            Helper helper = new Helper();
            if (deptType == DepartmentNameEnums.Electrical || deptType == DepartmentNameEnums.Mechanical || deptType == DepartmentNameEnums.Plumbing || deptType == DepartmentNameEnums.Building)
                return Project.Trades.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
            else
            {
                //in the property the first fire department is assigned. So assume it is only one fire at a time and then pick the related fire dept from list.
                if (helper.FireDepartmentNames.Contains(deptType))
                {
                    return Project.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                }
                //in the property the first Zone department is assigned. So assume it is only one fire at a time and then pick the related zone dept from list.
                else if (helper.ZoneDepartmentNames.Contains(deptType))
                {
                    return Project.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                }
                else
                {
                    return Project.Agencies.Where(x => x.DepartmentInfo == deptType).FirstOrDefault();
                }
            }
        }

    }
}