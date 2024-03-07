namespace AION.Manager.Models.Estimation
{
    public class ActualsData
    {
        public int OccupancyTypeRefId { get; set; }
        public string OccupancyType { get; set; }
        public string ConstructionType { get; set; }
        public decimal ActualBuildingHours { get; set; }
        public decimal ActualElectricHours { get; set; }
        public decimal ActualMechanicalHours { get; set; }
        public decimal ActualPlumbingHours { get; set; }
        public decimal ActualSquareFeet { get; set; }
        public decimal ActualCostOfConstruction { get; set; }
        public decimal ActualSheets { get; set; }
    }
}