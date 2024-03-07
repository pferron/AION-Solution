using AION.Estimator.Engine.BusinessEntities;

namespace AION.BL.BusinessObjects
{
    public class ProjectAgencyModelBO : ProjectDepartmentModelBaseBO, IProjectAgencyBO
    {

        public ProjectAgency GetInstance(Project project, DepartmentDivisionEnum division, DepartmentRegionEnum region, Meck.Shared.MeckDataMapping.AgencyInfo accelaAgencyInfo, ProjectBusinessRelationshipBE previousProjectDept)
        {
            ProjectAgency ret = new ProjectAgency();

            base.InjectBaseObjects(ret, project, DepartmentTypeEnum.Agency, division, region, accelaAgencyInfo, previousProjectDept);
            return ret;
        }

    }

    public interface IProjectAgencyBO
    {
        ProjectAgency GetInstance(Project project, DepartmentDivisionEnum division, DepartmentRegionEnum region, Meck.Shared.MeckDataMapping.AgencyInfo accelaAgencyInfo, ProjectBusinessRelationshipBE previousProjectDept);

    }
}
