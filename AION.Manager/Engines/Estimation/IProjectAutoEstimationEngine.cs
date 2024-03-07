using AION.BL.Models;

namespace AION.BL.Controller
{
    public interface IProjectAutoEstimationEngine
    {
        ProjectEstimation ExecuteProjectEstimation(ProjectParms projInfo, bool forceAutoCalc = false);

        /// <summary>
        /// get the actual hours from the projects and calculate the rolling 24 month factors
        /// </summary>
        /// <returns></returns>
        bool CalculateAverageEstimationHoursFactors();
    }
}