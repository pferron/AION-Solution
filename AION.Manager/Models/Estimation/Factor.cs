namespace AION.Manager.Models.Estimation
{
    public class Factor
    {
        public int OccupancyTypeRefId { get; set; }
        public string OccupancyType { get; set; }
        public string ConstructionType { get; set; }
        public decimal BldgSqFeet { get; set; }
        public decimal ElecSqFeet { get; set; }
        public decimal PlmgSqFeet { get; set; }
        public decimal MechSqFeet { get; set; }
        public decimal BldgCOC { get; set; }
        public decimal ElecCOC { get; set; }
        public decimal MechCOC { get; set; }
        public decimal PlmgCOC { get; set; }
        public decimal BldgSheets { get; set; }
        public decimal ElecSheets { get; set; }
        public decimal MechSheets { get; set; }
        public decimal PlmgSheets { get; set; }
    }
}