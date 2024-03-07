namespace Meck.Shared.Accela.ParserModels
{
    public class ResidentialRTAPCustomForm
    {
       public string RTAPScopeOfWork { get; set; }
        public string id { get; set; }
        public string BuildingCode { get; set; }
        public string PropertyType { get; set; }
        public string ReviewType { get; set; }
        public string EquipmentCost { get; set; }
        public string ConstructionCost { get; set; }
        public string ProjectCostTotal { get; set; }
        public object RTAPAffordableUnitsRemove { get; set; }
        public object RTAPAffordableWorkforceUnitsAdd { get; set; }
        public string RTAPAffordableUnitChange { get; set; }
        public object RTAPWorkforceRemove { get; set; }
        public object RTAPWorkforceAdd { get; set; }
        public object currentReviewCycle { get; set; }
        public object ClonedFromProjectNumber { get; set; }
        public object IsRTAP { get; set; }
        public object TaxJurisdiction { get; set; }
        public object recordLocation { get; set; }
        public string TotalEstimatedFees { get; set; }
        public string PrepaidFeePaymentType { get; set; }
        public object AccountNumber { get; set; }
        public object PINNumber { get; set; }
        public string TotalNumOfSheets { get; set; }
        public string ProjectName { get; set; }
        public string IncludesAfforableOrWorkforceHousing { get; set; }
        public object OriginalPermitNumber { get; set; }
        public string OriginalProjectNumber { get; set; }
        public object DrawingsReadyDate { get; set; }
        public object GateKeeperChecklistAdded { get; set; }
    }

   
}
