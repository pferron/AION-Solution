using AION.BL.Models;

namespace AION.Manager.Models
{
    public class ProjectEstimationManagerModel
    {
        // ProjectParms projInfo, bool forceAutoCalc = false
        public ProjectParms ProjInfo { get; set; }
        public bool ForceAutoCalc { get; set; }
        public bool IsPreliminaryEstimation { get; set; }
    }
}