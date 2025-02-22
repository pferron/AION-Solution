/* 
 * Settings
 *
 * The Settings API provides configuration values that have been defined in Civic Platform Administration, typically as standard choice values. The Settings APIs are helpful when you need reference or custom-configured values in your API calls.
 *
 * OpenAPI spec version: v4
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
using SwaggerDateConverter = AccelaSettings.Client.SwaggerDateConverter;

namespace AccelaSettings.Model
{
    /// <summary>
    /// ChecklistModel
    /// </summary>
    [DataContract]
    public partial class ChecklistModel :  IEquatable<ChecklistModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistModel" /> class.
        /// </summary>
        /// <param name="group">group.</param>
        /// <param name="id">The checklist id..</param>
        /// <param name="items">items.</param>
        /// <param name="serviceProviderCode">The agency unique identifier..</param>
        /// <param name="text">The checklist name..</param>
        public ChecklistModel(List<string> group = default(List<string>), string id = default(string), List<ChecklistItemModel> items = default(List<ChecklistItemModel>), string serviceProviderCode = default(string), string text = default(string))
        {
            this.Group = group;
            this.Id = id;
            this.Items = items;
            this.ServiceProviderCode = serviceProviderCode;
            this.Text = text;
        }
        
        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public List<string> Group { get; set; }

        /// <summary>
        /// The checklist id.
        /// </summary>
        /// <value>The checklist id.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>
        [DataMember(Name="items", EmitDefaultValue=false)]
        public List<ChecklistItemModel> Items { get; set; }

        /// <summary>
        /// The agency unique identifier.
        /// </summary>
        /// <value>The agency unique identifier.</value>
        [DataMember(Name="serviceProviderCode", EmitDefaultValue=false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The checklist name.
        /// </summary>
        /// <value>The checklist name.</value>
        [DataMember(Name="text", EmitDefaultValue=false)]
        public string Text { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ChecklistModel {\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
            sb.Append("  ServiceProviderCode: ").Append(ServiceProviderCode).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
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
            return this.Equals(input as ChecklistModel);
        }

        /// <summary>
        /// Returns true if ChecklistModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ChecklistModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ChecklistModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Group == input.Group ||
                    this.Group != null &&
                    this.Group.SequenceEqual(input.Group)
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Items == input.Items ||
                    this.Items != null &&
                    this.Items.SequenceEqual(input.Items)
                ) && 
                (
                    this.ServiceProviderCode == input.ServiceProviderCode ||
                    (this.ServiceProviderCode != null &&
                    this.ServiceProviderCode.Equals(input.ServiceProviderCode))
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
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
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Items != null)
                    hashCode = hashCode * 59 + this.Items.GetHashCode();
                if (this.ServiceProviderCode != null)
                    hashCode = hashCode * 59 + this.ServiceProviderCode.GetHashCode();
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
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
