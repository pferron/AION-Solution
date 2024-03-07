using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class BulkEstimationModel
    {
        public List<Facilitator> Facilitators { get; set; } = new List<Facilitator>();
        public List<Reviewer> Reviewers { get; set; }
        public List<EstimatorUIModel> EstimatorUIModels { get; set; } = new List<EstimatorUIModel>();
    }
}