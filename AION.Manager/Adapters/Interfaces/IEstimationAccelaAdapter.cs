using AION.BL.Models;
using AION.Manager.Models;
using Meck.Shared.MeckDataMapping;
using System.Collections.Generic;

namespace AION.BL.Adapters
{
    public interface IEstimationAccelaAdapter
    {
        /// <summary>
        ///  GetProjectDetails
        /// </summary>
        /// <param name="projInfo"></param>
        /// <returns></returns>
        Meck.Shared.MeckDataMapping.AccelaProjectModel GetProjectDetailsLoad(ProjectParms projInfo);

        List<ProjectEstimation> GetProjectEstimationList();
        List<string> GetAllAgencies(ProjectParms parms);
        List<SystemRole> UpsertUserSystemRoles(List<AgencyInfo> incomingagencies, List<TradeInfo> incomingtrades, int userid);
        List<EstimatorUIModel> GetAllEstimators();
        List<Facilitator> GetAllFacilitators();
        List<Reviewer> GetAllReviewers(bool isExpressSched = false, bool IsSchedulable = false);
    }
}
