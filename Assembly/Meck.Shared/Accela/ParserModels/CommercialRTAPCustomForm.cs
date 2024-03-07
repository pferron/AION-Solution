using Newtonsoft.Json;

namespace Meck.Shared.Accela.ParserModels
{
    public class CommercialRTAPCustomForm
    {
        [JsonProperty("RTAPScopeOfWork")] public object RTAPScopeOfWork { get; set; }

        [JsonProperty("BuildingCode")] public object BuildingCode { get; set; }

        [JsonProperty("PropertyType")] public object PropertyType { get; set; }

        [JsonProperty("ReviewType")] public object ReviewType { get; set; }

        [JsonProperty("EquipmentCost")] public object EquipmentCost { get; set; }

        [JsonProperty("ConstructionCost")] public object ConstructionCost { get; set; }

        [JsonProperty("ProjectCostTotal")] public object ProjectCostTotal { get; set; }

        [JsonProperty("RTAPAffordableUnitsRemove")]
        public object RTAPAffordableUnitsRemove { get; set; }

        [JsonProperty("RTAPAffordableWorkforceUnitsAdd")]
        public object RTAPAffordableWorkforceUnitsAdd { get; set; }

        [JsonProperty("RTAPAffordableUnitChange")]
        public object RTAPAffordableUnitChange { get; set; }

        [JsonProperty("RTAPWorkforceRemove")] public object RTAPWorkforceRemove { get; set; }

        [JsonProperty("RTAPWorkforceAdd")] public object RTAPWorkforceAdd { get; set; }

        [JsonProperty("Current Review Cycle")] public object CurrentReviewCycle { get; set; }

        [JsonProperty("ClonedFromProjectNumber")]
        public object ClonedFromProjectNumber { get; set; }

        [JsonProperty("IsRTAP")] public object IsRTAP { get; set; }

        [JsonProperty("TaxJurisdiction")] public object TaxJurisdiction { get; set; }

        [JsonProperty("Record Location")] public object RecordLocation { get; set; }

        [JsonProperty("TotalEstimatedFees")] public decimal TotalEstimatedFees { get; set; }

        [JsonProperty("PrepaidFeePaymentType")]
        public string PrepaidFeePaymentType { get; set; }

        [JsonProperty("AccountNumber")] public string AccountNumber { get; set; }

        [JsonProperty("TotalNumOfSheets")] public object TotalNumOfSheets { get; set; }

        [JsonProperty("ProjectName")] public object ProjectName { get; set; }

        [JsonProperty("IncludesAfforableOrWorkforceHousing")]
        public object IncludesAfforableOrWorkforceHousing { get; set; }

        [JsonProperty("OriginalPermitNumber")] public object OriginalPermitNumber { get; set; }

        [JsonProperty("OriginalProjectNumber")]
        public object OriginalProjectNumber { get; set; }

        [JsonProperty("DrawingsReadyDate")] public object DrawingsReadyDate { get; set; }

        [JsonProperty("GateKeeperChecklistAdded")]
        public object GateKeeperChecklistAdded { get; set; }
        // ---------------------------------------------------------------

    }
}
