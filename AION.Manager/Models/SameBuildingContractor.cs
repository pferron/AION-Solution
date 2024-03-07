using AION.BL;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class SameBuildingContractor
    {
        public List<ProjectBE> ProjectList { get; set; } = new List<ProjectBE>();
        public bool IsSameBuildingContractor { get; set; } = false;
        public List<PlanReviewScheduleDetail> FifoSchedules { get; set; } = new List<PlanReviewScheduleDetail>();
    }
}