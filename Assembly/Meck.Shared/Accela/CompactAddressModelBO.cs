using Newtonsoft.Json;
using System.Text;

namespace Meck.Shared.Accela
{
    public partial class CompactAddressModelBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactAddressModel" /> class.
        /// </summary>
        /// <param name="addressLine1">The first line of the address..</param>
        /// <param name="addressLine2">The second line of the address..</param>
        /// <param name="addressLine3">The third line of the address..</param>
        /// <param name="city">The name of the city..</param>
        /// <param name="country">country.</param>
        /// <param name="postalCode">The postal ZIP code for the address..</param>
        /// <param name="state">state.</param>
        public CompactAddressModelBO(string addressLine1 = default(string), string addressLine2 = default(string), string addressLine3 = default(string), string city = default(string), CompactAddressModelCountryBO country = default(CompactAddressModelCountryBO), string postalCode = default(string), CompactAddressModelStateBO state = default(CompactAddressModelStateBO))
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.AddressLine3 = addressLine3;
            this.City = city;
            this.Country = country;
            this.PostalCode = postalCode;
            this.State = state;
        }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        /// <value>The first line of the address.</value>

        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        /// <value>The second line of the address.</value>

        public string AddressLine2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        /// <value>The third line of the address.</value>

        public string AddressLine3 { get; set; }

        /// <summary>
        /// The name of the city.
        /// </summary>
        /// <value>The name of the city.</value>

        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>

        public CompactAddressModelCountryBO Country { get; set; }

        /// <summary>
        /// The postal ZIP code for the address.
        /// </summary>
        /// <value>The postal ZIP code for the address.</value>

        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>

        public CompactAddressModelStateBO State { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CompactAddressModel {\n");
            sb.Append("  AddressLine1: ").Append(AddressLine1).Append("\n");
            sb.Append("  AddressLine2: ").Append(AddressLine2).Append("\n");
            sb.Append("  AddressLine3: ").Append(AddressLine3).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }

    public partial class CompactAddressModelStateBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactAddressModelState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public CompactAddressModelStateBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }

    }

    public partial class CompactAddressModelCountryBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactAddressModelCountry" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public CompactAddressModelCountryBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
       public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }
}
