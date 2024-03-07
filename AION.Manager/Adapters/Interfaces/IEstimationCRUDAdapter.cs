using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.BL.Adapters
{
    public interface IEstimationCRUDAdapter
    {
        ProjectDetailModel GetProjectDetailModel(ProjectParms parms);
        ProjectEstimation GetProjectDetailsForEstimation(Meck.Shared.MeckDataMapping.AccelaProjectModel p);
        /// <summary>
        /// Saves the ProjectEstimation object, includes Trades and Agencies
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveProjectEstimationDetails(ProjectEstimation model);

        List<Facilitator> GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate);
        List<CatalogItem> GetAllPendingReasons();
        bool SaveProjectTrade(ProjectTrade projectTrade);
        bool SaveProjectAgency(ProjectAgency projectAgency);
        List<ProjectStatus> GetProjectStatusBaseList();

        /// <summary>
        /// Get the ProjectEstimation by the AION Project ID (int)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectEstimation GetProjectDetailsByProjectId(int projectId);
        ProjectEstimation GetProjectDetailsByProjectSrcSourceTxt(string accelaModelProjectId, bool includeReviewers = true);
        Tuple<ProjectBE, ProjectEstimation> GetProjectDetailsTupleByProjectSrcSourceTxt(string srcSystemValTxt, bool includeReviewers = true);
    }
}