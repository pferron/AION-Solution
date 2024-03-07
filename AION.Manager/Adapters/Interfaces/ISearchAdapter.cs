using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface ISearchAdapter
    {
        List<ProjectSearchResultBE> SearchProjects(DateTime? startDate, DateTime? endDate, string projectNumber, string projectName, string projectAddress,
       string customerName, string planReviewer, int? projectStatus = null, int? estimatorId = null, int? facilitatorId = null, int? meetingType = null);
    }
}
