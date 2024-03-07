using Newtonsoft.Json;
using System.Collections.Generic;


namespace Meck.Shared.Accela
{
    public class TradeWrapperBE
    {
        [JsonProperty("result")]
        public List<TradeBE> TradeList { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }
    }

    public class LicenseType
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class State
    {
        [JsonProperty("State__value")]
        public string Value { get; set; }

        [JsonProperty("State__text")]
        public string Text { get; set; }
    }

   

    public class TradeBE
    {
        [JsonProperty("id")]
        public long TradeID { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("licenseNumber")]
        public string LicenseNumber { get; set; }

        [JsonProperty("licenseState")]
        public string LicenseState { get; set; }

        [JsonProperty("serviceProviderCode")]
        public string ServiceProviderCode { get; set; }

        [JsonProperty("licenseType")]
        public LicenseType LicenseType { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("businessName")]
        public string BusinessName { get; set; }

        [JsonProperty("licenseIssueDate")]
        public string LicenseIssueDate { get; set; }

        [JsonProperty("licenseExpirationDate")]
        public string LicenseExpirationDate { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("businessLicense")]
        public string BusinessLicense { get; set; }

        [JsonProperty("phone3")]
        public string Phone3 { get; set; }

        [JsonProperty("phone1")]
        public string Phone1 { get; set; }

        [JsonProperty("phone2")]
        public string Phone2 { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("workerCompensationExemption")]
        public string WorkerCompensationExemption { get; set; }

        [JsonProperty("workerCompensationExpirationDate")]
        public string WorkerCompensationExpirationDate { get; set; }

        [JsonProperty("workerCompensationPolicyNumber")]
        public string WorkerCompensationPolicyNumber { get; set; }

        [JsonProperty("insuranceCompany")]
        public string InsuranceCompany { get; set; }

        [JsonProperty("insuranceAmount")]
        public double? InsuranceAmount { get; set; }

        [JsonProperty("insuranceExpirationDate")]
        public string InsuranceExpirationDate { get; set; }
    }

    public class Page
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("hasmore")]
        public bool Hasmore { get; set; }
    }


}
