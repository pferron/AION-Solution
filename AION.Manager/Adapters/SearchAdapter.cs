using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public class SearchAdapter : BaseManagerAdapter, ISearchAdapter
    {
        public List<ProjectSearchResultBE> SearchProjects(DateTime? startDate, DateTime? endDate, string projectNumber, string projectName, string projectAddress,
       string customerName, string planReviewer, int? projectStatus = null, int? estimatorId = null, int? facilitatorId = null, int? meetingType = null)
        {
            ProjectBO projectBO = new ProjectBO();
            List<ProjectSearchResultBE> projectSearchList = projectBO.Search(startDate, endDate, projectNumber, projectName, projectAddress,
            customerName, planReviewer, projectStatus, estimatorId, facilitatorId, meetingType);
            return projectSearchList;
        }
    }
}