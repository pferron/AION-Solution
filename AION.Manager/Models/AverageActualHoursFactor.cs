namespace AION.BL.Models
{
    public class AverageActualHoursFactor
    {
        public int? ProjectOccupancyTypMapRefId { get; set; }
        public string ProjectOccupancyTypMapName { get; set; }
        public int? OccupancyTypRefId { get; set; }
        public string OccupancyTypName { get; set; }
        public string ConstructionType { get; set; }
        public decimal? BuildingSqftFactor { get; set; }
        public decimal? ElectricalSqftFactor { get; set; }
        public decimal? MechanicalSqftFactor { get; set; }
        public decimal? PlumbingSqftFactor { get; set; }
        public decimal? BuildingCocFactor { get; set; }
        public decimal? ElectricalCocFactor { get; set; }
        public decimal? MechanicalCocFactor { get; set; }
        public decimal? PlumbingCocFactor { get; set; }
        public decimal? BuildingSheetsFactor { get; set; }
        public decimal? ElectricalSheetsFactor { get; set; }
        public decimal? MechanicalSheetsFactor { get; set; }
        public decimal? PlumbingSheetsFactor { get; set; }
    }
}
