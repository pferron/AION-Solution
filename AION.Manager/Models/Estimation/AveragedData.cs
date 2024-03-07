namespace AION.Manager.Models.Estimation
{
    public class AveragedData
    {
        public int OccupancyTypeRefId { get; set; }
        public string OccupancyType { get; set; }
        public string ConstructionType { get; set; }
        public decimal AvgBuildingHours { get; set; }
        public decimal AvgElectricHours { get; set; }
        public decimal AvgMechanicalHours { get; set; }
        public decimal AvgPlumbingHours { get; set; }
        public decimal AvgSquareFeet { get; set; }
        public decimal AvgCostOfConstruction { get; set; }
        public decimal AvgSheets { get; set; }
    }
}