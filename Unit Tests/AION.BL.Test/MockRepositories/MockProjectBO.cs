using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.MockRepositories
{
    public class MockProjectBO : IDataContextProjectBO
    {
        public List<ProjectBE> MMFProjects = new List<ProjectBE>()
        {
            new ProjectBE()
            {
                ProjectId = 1,
                WorkTypDesc = "Upfit (Interior Completion)",
                ProjectOccupancyTypMapNm = "FACTORY INDUSTRIAL",
                ConstrCostAmt= 150000,
                SheetsCntDesc ="4",
                SquareFootageToBeReviewedNbr= 20000,
            },
            new ProjectBE()
            {
                ProjectId = 2,
                WorkTypDesc = "New Construction (Full)",
                ProjectOccupancyTypMapNm = "STORAGE",
                ConstrCostAmt= 660000,
                SheetsCntDesc ="7",
                SquareFootageToBeReviewedNbr= 30000,
            },
            new ProjectBE()
            {
                ProjectId = 3,
                WorkTypDesc = "Upfit (Interior Completion)",
                ProjectOccupancyTypMapNm = "ASSEMBLY",
                ConstrCostAmt= 660000,
                SheetsCntDesc ="15",
                SquareFootageToBeReviewedNbr= 40000,
            },
            new ProjectBE()
            {
                ProjectId = 4,
                WorkTypDesc = "New Construction (Full)",
                ProjectOccupancyTypMapNm = "RESIDENTIAL",
                ConstrCostAmt= 660000,
                SheetsCntDesc ="22",
                SquareFootageToBeReviewedNbr= 50000,
            },
            new ProjectBE()
            {
                ProjectId = 5,
                WorkTypDesc = "Upfit (Interior Completion)",
                ProjectOccupancyTypMapNm = "FACTORY INDUSTRIAL",
                ConstrCostAmt= 660000,
                SheetsCntDesc ="8",
                SquareFootageToBeReviewedNbr= 45000,
            },
            new ProjectBE()
            {
                ProjectId = 6,
                WorkTypDesc = "New Construction (Full)",
                ProjectOccupancyTypMapNm = "STORAGE",
                ConstrCostAmt= 660000,
                SheetsCntDesc ="33",
                SquareFootageToBeReviewedNbr= 35000,
            },
        };

        public List<ProjectBE> GetMMFProjectsComplete(DateTime startdate, DateTime enddate)
        {
            return MMFProjects;
        }
    }
}
