/* 
 * Agencies-b
 *
 * The Agencies API provides agency information as configured in the [Construct Admin Portal](https://admin.accela.com).
 *
 * OpenAPI spec version: v4-oas3
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = AccelaAgencies.Client.SwaggerDateConverter;

namespace AccelaAgencies.Model
{
    /// <summary>
    /// Agency
    /// </summary>
    [DataContract]
        public partial class Agency :  IEquatable<Agency>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Agency" /> class.
        /// </summary>
        /// <param name="displayName">The display name of the agency..</param>
        /// <param name="enabled">Indicates whether or not the agency is enabled on the [Construct Admin Portal](https://admin.accela.com)..</param>
        /// <param name="hostedACA">Indicates whether or not the agency has deployed Citizen Access for the public user..</param>
        /// <param name="iconName">The icon filename..</param>
        /// <param name="isForDemo">Indicates whether or not the agency is used for demo purposes only..</param>
        /// <param name="name">The agency name..</param>
        /// <param name="serviceProviderCode">The unique agency identifier, the system assigns, on the Accela Civic Platform..</param>
        /// <param name="state">The two-character code for the state that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; Agency Info..</param>
        /// <param name="country">The two-character code for the country that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; {Agency} &gt; Agency Info..</param>
        public Agency(string displayName = default(string), bool? enabled = default(bool?), bool? hostedACA = default(bool?), string iconName = default(string), bool? isForDemo = default(bool?), string name = default(string), string serviceProviderCode = default(string), string state = default(string), string country = default(string))
        {
            this.DisplayName = displayName;
            this.Enabled = enabled;
            this.HostedACA = hostedACA;
            this.IconName = iconName;
            this.IsForDemo = isForDemo;
            this.Name = name;
            this.ServiceProviderCode = serviceProviderCode;
            this.State = state;
            this.Country = country;
        }
        
        /// <summary>
        /// The display name of the agency.
        /// </summary>
        /// <value>The display name of the agency.</value>
        [DataMember(Name="displayName", EmitDefaultValue=false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Indicates whether or not the agency is enabled on the [Construct Admin Portal](https://admin.accela.com).
        /// </summary>
        /// <value>Indicates whether or not the agency is enabled on the [Construct Admin Portal](https://admin.accela.com).</value>
        [DataMember(Name="enabled", EmitDefaultValue=false)]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Indicates whether or not the agency has deployed Citizen Access for the public user.
        /// </summary>
        /// <value>Indicates whether or not the agency has deployed Citizen Access for the public user.</value>
        [DataMember(Name="hostedACA", EmitDefaultValue=false)]
        public bool? HostedACA { get; set; }

        /// <summary>
        /// The icon filename.
        /// </summary>
        /// <value>The icon filename.</value>
        [DataMember(Name="iconName", EmitDefaultValue=false)]
        public string IconName { get; set; }

        /// <summary>
        /// Indicates whether or not the agency is used for demo purposes only.
        /// </summary>
        /// <value>Indicates whether or not the agency is used for demo purposes only.</value>
        [DataMember(Name="isForDemo", EmitDefaultValue=false)]
        public bool? IsForDemo { get; set; }

        /// <summary>
        /// The agency name.
        /// </summary>
        /// <value>The agency name.</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// The unique agency identifier, the system assigns, on the Accela Civic Platform.
        /// </summary>
        /// <value>The unique agency identifier, the system assigns, on the Accela Civic Platform.</value>
        [DataMember(Name="serviceProviderCode", EmitDefaultValue=false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The two-character code for the state that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; Agency Info.
        /// </summary>
        /// <value>The two-character code for the state that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; Agency Info.</value>
        [DataMember(Name="state", EmitDefaultValue=false)]
        public string State { get; set; }

        /// <summary>
        /// The two-character code for the country that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; {Agency} &gt; Agency Info.
        /// </summary>
        /// <value>The two-character code for the country that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; {Agency} &gt; Agency Info.</value>
        [DataMember(Name="country", EmitDefaultValue=false)]
        public string Country { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Agency {\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Enabled: ").Append(Enabled).Append("\n");
            sb.Append("  HostedACA: ").Append(HostedACA).Append("\n");
            sb.Append("  IconName: ").Append(IconName).Append("\n");
            sb.Append("  IsForDemo: ").Append(IsForDemo).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  ServiceProviderCode: ").Append(ServiceProviderCode).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
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

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Agency);
        }

        /// <summary>
        /// Returns true if Agency instances are equal
        /// </summary>
        /// <param name="input">Instance of Agency to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Agency input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DisplayName == input.DisplayName ||
                    (this.DisplayName != null &&
                    this.DisplayName.Equals(input.DisplayName))
                ) && 
                (
                    this.Enabled == input.Enabled ||
                    (this.Enabled != null &&
                    this.Enabled.Equals(input.Enabled))
                ) && 
                (
                    this.HostedACA == input.HostedACA ||
                    (this.HostedACA != null &&
                    this.HostedACA.Equals(input.HostedACA))
                ) && 
                (
                    this.IconName == input.IconName ||
                    (this.IconName != null &&
                    this.IconName.Equals(input.IconName))
                ) && 
                (
                    this.IsForDemo == input.IsForDemo ||
                    (this.IsForDemo != null &&
                    this.IsForDemo.Equals(input.IsForDemo))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.ServiceProviderCode == input.ServiceProviderCode ||
                    (this.ServiceProviderCode != null &&
                    this.ServiceProviderCode.Equals(input.ServiceProviderCode))
                ) && 
                (
                    this.State == input.State ||
                    (this.State != null &&
                    this.State.Equals(input.State))
                ) && 
                (
                    this.Country == input.Country ||
                    (this.Country != null &&
                    this.Country.Equals(input.Country))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.DisplayName != null)
                    hashCode = hashCode * 59 + this.DisplayName.GetHashCode();
                if (this.Enabled != null)
                    hashCode = hashCode * 59 + this.Enabled.GetHashCode();
                if (this.HostedACA != null)
                    hashCode = hashCode * 59 + this.HostedACA.GetHashCode();
                if (this.IconName != null)
                    hashCode = hashCode * 59 + this.IconName.GetHashCode();
                if (this.IsForDemo != null)
                    hashCode = hashCode * 59 + this.IsForDemo.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.ServiceProviderCode != null)
                    hashCode = hashCode * 59 + this.ServiceProviderCode.GetHashCode();
                if (this.State != null)
                    hashCode = hashCode * 59 + this.State.GetHashCode();
                if (this.Country != null)
                    hashCode = hashCode * 59 + this.Country.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}