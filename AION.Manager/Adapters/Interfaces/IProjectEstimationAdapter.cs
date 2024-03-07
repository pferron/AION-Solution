using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.BL.Adapters
{
    public interface IProjectEstimationAdapter
    {
        EstimationModel GetEstimationModel(ProjectParms projectParms);
        BulkEstimationModel GetBulkEstimationModel();
        bool PerformAutoEstimation(ProjectEstimation model);

        bool EstimatorTool(List<ProjectTrade> trades, double coc, int sheets, int sqFt, string occupancyType, string constructionType);
        AverageEstimationHoursFactorBE GetAverageEstimationHoursFactors(string occupancyType, string constructionType);

        ProjectRtapMapping GetProjectRtapMapping(int projectId);
        int GetAssignedFacilitator(int projectId);

        /// <summary>
        /// Get the AION project id from the Accela string
        /// skips using the Model
        /// </summary>
        /// <param name="accelaProjectId"></param>
        /// <returns></returns>
        int GetAIONProjectId(string accelaProjectId);
    }
}
