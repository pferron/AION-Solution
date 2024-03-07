namespace AION.Manager.Models
{
    public class MMFHoursGroupingFlattened
    {
        public int OccupancyTypeId { get; set; }
        public string ConstructionTypeShortDesc { get; set; }
        public int TotalProjects { get; set; }
        public decimal BuildingHours { get; set; }
        public decimal ElectricalHours { get; set; }
        public decimal MechanicalHours { get; set; }
        public decimal PlumbingHours { get; set; }
        public decimal CostOfConstruction { get; set; }
        public int SheetsCount { get; set; }
        public int SquareFootage { get; set; }
    }
}