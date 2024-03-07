using Newtonsoft.Json;
using System.Collections.Generic;

namespace AION.Accela.Engine.Models
{

    public class CustomForm
    {

        [JsonProperty("Expiration Date")]
        public object ExpirationDate { get; set; }

        [JsonProperty("Extension Date")]
        public object ExtensionDate { get; set; }

        [JsonProperty("Approved Date")]
        public object ApprovedDate { get; set; }

        [JsonProperty("Appeal Period End Date")]
        public object AppealPeriodEndDate { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Proposed Use")]
        public string ProposedUse { get; set; }

        [JsonProperty("Total Project Area")]
        public string TotalProjectArea { get; set; }

        [JsonProperty("Type of Development")]
        public string TypeOfDevelopment { get; set; }

        [JsonProperty("Existing Use")]
        public string ExistingUse { get; set; }

        [JsonProperty("Include Demolition")]
        public object IncludeDemolition { get; set; }

        [JsonProperty("Include Tree Removal")]
        public object IncludeTreeRemoval { get; set; }

        [JsonProperty("Zoning - West")]
        public object ZoningWest { get; set; }

        [JsonProperty("Zoning - North")]
        public object ZoningNorth { get; set; }

        [JsonProperty("Zoning - South")]
        public object ZoningSouth { get; set; }

        [JsonProperty("Roads")]
        public object Roads { get; set; }

        [JsonProperty("Water Provider")]
        public object WaterProvider { get; set; }

        [JsonProperty("Adjacent Land Use")]
        public object AdjacentLandUse { get; set; }

        [JsonProperty("Historic Status Description")]
        public object HistoricStatusDescription { get; set; }

        [JsonProperty("Zoning")]
        public object Zoning { get; set; }

        [JsonProperty("Site Description")]
        public object SiteDescription { get; set; }

        [JsonProperty("Historic Status")]
        public object HistoricStatus { get; set; }

        [JsonProperty("Zoning - East")]
        public object ZoningEast { get; set; }

        [JsonProperty("Sewer Provider")]
        public object SewerProvider { get; set; }

        [JsonProperty("Number of Parking Spaces - Proposed")]
        public object NumberOfParkingSpacesProposed { get; set; }

        [JsonProperty("Rear Setback - Proposed")]
        public object RearSetbackProposed { get; set; }

        [JsonProperty("Rear Setback - Existing")]
        public object RearSetbackExisting { get; set; }

        [JsonProperty("Lot Frontage")]
        public object LotFrontage { get; set; }

        [JsonProperty("Building Height - Existing")]
        public object BuildingHeightExisting { get; set; }

        [JsonProperty("Number of Parking Spaces - Existing")]
        public object NumberOfParkingSpacesExisting { get; set; }

        [JsonProperty("Side1 Setback - Existing")]
        public object Side1SetbackExisting { get; set; }

        [JsonProperty("Side2 Setback - Existing")]
        public object Side2SetbackExisting { get; set; }

        [JsonProperty("Project Design Criteria Description")]
        public object ProjectDesignCriteriaDescription { get; set; }

        [JsonProperty("Lot Coverage/Structures - Proposed")]
        public object LotCoverageStructuresProposed { get; set; }

        [JsonProperty("Lot Coverage/Hardscape - Proposed")]
        public object LotCoverageHardscapeProposed { get; set; }

        [JsonProperty("Front Setback - Existing")]
        public object FrontSetbackExisting { get; set; }

        [JsonProperty("Lot Coverage/Hardscape - Existing")]
        public object LotCoverageHardscapeExisting { get; set; }

        [JsonProperty("Front Setback - Proposed")]
        public object FrontSetbackProposed { get; set; }

        [JsonProperty("Side1 Setback - Proposed")]
        public object Side1SetbackProposed { get; set; }

        [JsonProperty("Side2 Setback - Proposed")]
        public object Side2SetbackProposed { get; set; }

        [JsonProperty("Lot Coverage/Structures - Existing")]
        public object LotCoverageStructuresExisting { get; set; }

        [JsonProperty("Lot Area")]
        public object LotArea { get; set; }

        [JsonProperty("Building Height - Proposed")]
        public object BuildingHeightProposed { get; set; }
    }

    public partial class RecordId
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("trackingId")]
        public long? TrackingId { get; set; }

        [JsonProperty("serviceProviderCode")]
        public string ServiceProviderCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Status
    {

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Type
    {

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Relation
    {

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class State
    {

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Address
    {

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }
    }

    public class Contact
    {

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("isPrimary")]
        public string IsPrimary { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("individualOrOrganization")]
        public string IndividualOrOrganization { get; set; }

        [JsonProperty("phone2")]
        public string Phone2 { get; set; }

        [JsonProperty("phone3")]
        public string Phone3 { get; set; }

        [JsonProperty("phone1")]
        public string Phone1 { get; set; }

        [JsonProperty("recordId")]
        public RecordId RecordId { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("relation")]
        public Relation Relation { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }

    public class ReportedChannel
    {

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Result
    {

        [JsonProperty("customForms")]
        public IList<CustomForm> CustomForms { get; set; }

        [JsonProperty("contacts")]
        public IList<Contact> Contacts { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusDate")]
        public string StatusDate { get; set; }

        [JsonProperty("recordClass")]
        public string RecordClass { get; set; }

        [JsonProperty("initiatedProduct")]
        public string InitiatedProduct { get; set; }

        [JsonProperty("serviceProviderCode")]
        public string ServiceProviderCode { get; set; }

        [JsonProperty("undistributedCost")]
        public double UndistributedCost { get; set; }

        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }

        [JsonProperty("customId")]
        public string CustomId { get; set; }

        [JsonProperty("jobValue")]
        public double JobValue { get; set; }

        [JsonProperty("trackingId")]
        public long? TrackingId { get; set; }

        [JsonProperty("openedDate")]
        public string OpenedDate { get; set; }

        [JsonProperty("totalJobCost")]
        public double TotalJobCost { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("reportedDate")]
        public string ReportedDate { get; set; }

        [JsonProperty("reportedChannel")]
        public ReportedChannel ReportedChannel { get; set; }

        [JsonProperty("estimatedProductionUnit")]
        public double EstimatedProductionUnit { get; set; }

        [JsonProperty("actualProductionUnit")]
        public double ActualProductionUnit { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("totalFee")]
        public double TotalFee { get; set; }

        [JsonProperty("totalPay")]
        public double TotalPay { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("booking")]
        public bool Booking { get; set; }

        [JsonProperty("infraction")]
        public bool Infraction { get; set; }

        [JsonProperty("misdemeanor")]
        public bool Misdemeanor { get; set; }

        [JsonProperty("offenseWitnessed")]
        public bool OffenseWitnessed { get; set; }

        [JsonProperty("defendantSignature")]
        public bool DefendantSignature { get; set; }

        [JsonProperty("publicOwned")]
        public bool PublicOwned { get; set; }
    }

    public class AccelaDocuementDetailsBE
    {

        [JsonProperty("result")]
        public IList<Result> Result { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }

}



