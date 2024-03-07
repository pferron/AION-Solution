using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class EstimationModel
    {
        public List<Facilitator> Facilitators { get; set; } = new List<Facilitator>();
        public ProjectEstimation ProjectEstimation { get; set; }
        public List<EstimatorUIModel> EstimatorUIModels { get; set; } = new List<EstimatorUIModel>();
        public List<CatalogItem> PermissionMappingCatalogItems { get; set; } = new List<CatalogItem>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Facilitator> FacilitatorWorkloadSummary { get; set; } = new List<Facilitator>();
        public List<StandardNote> StandardNotes { get; set; } = new List<StandardNote>();
        public List<StandardNoteGroupEnums> StandardNoteGroupEnums { get; set; } = new List<StandardNoteGroupEnums>();
    }
}