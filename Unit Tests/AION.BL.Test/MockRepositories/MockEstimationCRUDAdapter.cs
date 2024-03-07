using AION.Base.Services;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;
using System.Data;

namespace AION.BL.Test.MockRepositories
{
    public class MockEstimationCRUDAdapter : IEstimationCRUDAdapter
    {
        public List<CatalogItem> GetAllPendingReasons()
        {
            throw new NotImplementedException();
        }

        public List<Facilitator> GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate)
        {
            throw new NotImplementedException();
        }

        public ProjectDetailModel GetProjectDetailModel(ProjectParms parms)
        {
            throw new NotImplementedException();
        }

        public ProjectEstimation GetProjectDetailsByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }

        public ProjectEstimation GetProjectDetailsByProjectSrcSourceTxt(string accelaModelProjectId, bool includeReviewers = true)
        {
            throw new NotImplementedException();
        }

        public ProjectEstimation GetProjectDetailsForEstimation(AccelaProjectModel p)
        {
            throw new NotImplementedException();
        }

        public Tuple<ProjectBE, ProjectEstimation> GetProjectDetailsTupleByProjectSrcSourceTxt(string srcSystemValTxt, bool includeReviewers = true)
        {
            throw new NotImplementedException();
        }

        public List<ProjectStatus> GetProjectStatusBaseList()
        {
            throw new NotImplementedException();
        }

        public bool SaveProjectAgency(ProjectAgency projectAgency)
        {
            throw new NotImplementedException();
        }

        public bool SaveProjectEstimationDetails(ProjectEstimation model)
        {
            throw new NotImplementedException();
        }

        public bool SaveProjectTrade(ProjectTrade projectTrade)
        {
            throw new NotImplementedException();
        }
    }
}
