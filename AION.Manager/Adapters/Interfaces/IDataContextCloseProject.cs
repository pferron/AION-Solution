using AION.BL;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Shared;
using System.Collections.Generic;

namespace AION.MAION.Manager.Adapters
{
    public interface IDataContextCloseProject
    {
        bool Process(ProjectEstimation project);
        bool CloseProjectCycles(ProjectEstimation project);
        bool UpdateProjectActualHours(List<ProjectBusinessRelationshipBE> projectBusinessRelationships, List<TaskReview> taskReviews);
    }
}
