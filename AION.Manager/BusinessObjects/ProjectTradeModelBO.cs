using AION.Estimator.Engine.BusinessEntities;

namespace AION.BL.BusinessObjects
{
    public class ProjectTradeModelBO : ProjectDepartmentModelBaseBO, IProjectTradeBO
    {
        public ProjectTrade GetInstance(Project project, DepartmentDivisionEnum division, Meck.Shared.MeckDataMapping.TradeInfo tradeInfo, ProjectBusinessRelationshipBE previousProjectDept)
        {
            ProjectTrade ret = new ProjectTrade();

            base.InjectBaseObjects(ret, project, DepartmentTypeEnum.Trade, division, DepartmentRegionEnum.NA, tradeInfo, previousProjectDept);
            return ret;
        }

    }

    public interface IProjectTradeBO
    {
        ProjectTrade GetInstance(Project project, DepartmentDivisionEnum division, Meck.Shared.MeckDataMapping.TradeInfo tradeInfo, ProjectBusinessRelationshipBE previousProjectDept);

    }
}
