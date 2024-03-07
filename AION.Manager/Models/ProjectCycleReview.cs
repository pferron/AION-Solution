using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.BL
{
    public class ProjectCycleReview
    {
        public ProjectCycle ProjectCycle { get; set; }
        public List<PlanReview> PlanReviews { get; set; }
    }
}