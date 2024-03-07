using AION.Base.Services;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;

namespace AION.BL.Test.MockRepositories
{
    public class MockProjectBusinessRelationshipBO : IDataContextProjectBusinessRelationshipBO
    {
        public List<ProjectBusinessRelationshipBE> ProjectTrades { get; set; } =
            new List<ProjectBusinessRelationshipBE>() {
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 1, ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 16, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 2,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 8, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 3,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 21, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m},
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 4,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 22, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 5,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 23, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 6,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 24, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 7,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 25, IsEstimationNotApplicable = true, ActualHoursNbr= 0.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 8,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 2, IsEstimationNotApplicable = true, ActualHoursNbr= 3.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 9,ProjectId = 8165, EstimationHoursNbr = 2.0m, BusinessRefId = 1, IsEstimationNotApplicable = false, ActualHoursNbr= 4.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 10,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 3, IsEstimationNotApplicable = true, ActualHoursNbr= 2.0m },
            new ProjectBusinessRelationshipBE() { ProjectBusinessRelationshipId= 11,ProjectId = 8165, EstimationHoursNbr = 0.0m, BusinessRefId = 4, IsEstimationNotApplicable = true, ActualHoursNbr= 1.0m }
            };

        public List<ProjectBusinessRelationshipBE> MMFProjectHoursAION { get; set; } =
            new List<ProjectBusinessRelationshipBE>()
            {
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 1,
                    BusinessRefId = 1,
                    ActualHoursNbr = 10
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 1,
                    BusinessRefId = 2,
                    ActualHoursNbr = 20
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 1,
                    BusinessRefId = 3,
                    ActualHoursNbr = 30
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 1,
                    BusinessRefId = 4,
                    ActualHoursNbr = 40
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 2,
                    BusinessRefId = 3,
                    ActualHoursNbr = 50
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 2,
                    BusinessRefId = 4,
                    ActualHoursNbr = 60
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 3,
                    BusinessRefId = 1,
                    ActualHoursNbr = 58
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 3,
                    BusinessRefId = 2,
                    ActualHoursNbr = 64
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 4,
                    BusinessRefId = 3,
                    ActualHoursNbr = 21
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 4,
                    BusinessRefId = 1,
                    ActualHoursNbr = 16
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 4,
                    BusinessRefId = 3,
                    ActualHoursNbr = 12
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 5,
                    BusinessRefId = 1,
                    ActualHoursNbr = 10
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 5,
                    BusinessRefId = 2,
                    ActualHoursNbr = 20
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 5,
                    BusinessRefId = 3,
                    ActualHoursNbr = 30
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 5,
                    BusinessRefId = 4,
                    ActualHoursNbr = 40
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 6,
                    BusinessRefId = 3,
                    ActualHoursNbr = 50
                },
                new ProjectBusinessRelationshipBE()
                {
                    ProjectId = 6,
                    BusinessRefId = 4,
                    ActualHoursNbr = 60
                },
            };

        public int Create(ProjectBusinessRelationshipBE projectBusinessRelationshipBE)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public ProjectBusinessRelationshipBE GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public DataSet GetDataSet(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ProjectBusinessRelationshipBE> GetList(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ProjectBusinessRelationshipBE> GetListByProjectId(int id)
        {
            return MMFProjectHoursAION.Where(x => x.ProjectId == id).ToList();
        }

        public int Update(ProjectBusinessRelationshipBE projectBusinessRelationshipBE)
        {
            var trade = ProjectTrades.FirstOrDefault(x => x.ProjectBusinessRelationshipId == projectBusinessRelationshipBE.ProjectBusinessRelationshipId);
            trade.ActualHoursNbr = projectBusinessRelationshipBE.ActualHoursNbr;
            return 0;
        }
    }
}
