using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meck.Shared.Accela
{

    public class ContactWrapperBE
    {

        public List<ContactBE> Contacts { get; set; }

        public ContactWrapperBE()
        {
            Contacts = new List<ContactBE>();
        }
    }


    public partial class RecordId
    {
        public string id { get; set; }
        public string serviceProviderCode { get; set; }
        public long? trackingId { get; set; }
        public string value { get; set; }
    }

    public partial class RecordStatus
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public partial class Type
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("Countryvalue")]
        public string Value { get; set; }

        [JsonProperty("Countrytext")]
        public string Text { get; set; }
    }

    public partial class ContactState
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public partial class Address
    {
        public string postalCode { get; set; }
        public string city { get; set; }
        public string addressLine1 { get; set; }
        public Country country { get; set; }
        public ContactState state { get; set; }
    }

    public class ContactBE
    {
        public string fullName { get; set; }
        public string lastName { get; set; }
        public string organizationName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string phone3 { get; set; }
        public string startDate { get; set; }
        public string id { get; set; }
        public string referenceContactId { get; set; }
        public RecordId recordId { get; set; }
        public RecordStatus status { get; set; }
        public Type type { get; set; }
        public Address address { get; set; }




    }
}


