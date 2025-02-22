/* 
 * Inspections
 *
 * Use the Inspections API to manage inspection records during their complete lifecycle from application submittal to permit issuance or license issuance. Your agency may need to complete inspections on new property developments, homes, or complaints.
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
using SwaggerDateConverter = AccelaInspections.Client.SwaggerDateConverter;

namespace AccelaInspections.Model
{
    /// <summary>
    /// InspectionTypeModel
    /// </summary>
    [DataContract]
    public partial class InspectionTypeModel :  IEquatable<InspectionTypeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InspectionTypeModel" /> class.
        /// </summary>
        /// <param name="group">The name of a group of inspection types..</param>
        /// <param name="id">The inspection type system id assigned by the Civic Platform server..</param>
        /// <param name="text">The inspection type display text..</param>
        /// <param name="value">The inspection type value..</param>
        public InspectionTypeModel(string group = default(string), long? id = default(long?), string text = default(string), string value = default(string))
        {
            this.Group = group;
            this.Id = id;
            this.Text = text;
            this.Value = value;
        }
        
        /// <summary>
        /// The name of a group of inspection types.
        /// </summary>
        /// <value>The name of a group of inspection types.</value>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public string Group { get; set; }

        /// <summary>
        /// The inspection type system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The inspection type system id assigned by the Civic Platform server.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }

        /// <summary>
        /// The inspection type display text.
        /// </summary>
        /// <value>The inspection type display text.</value>
        [DataMember(Name="text", EmitDefaultValue=false)]
        public string Text { get; set; }

        /// <summary>
        /// The inspection type value.
        /// </summary>
        /// <value>The inspection type value.</value>
        [DataMember(Name="value", EmitDefaultValue=false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InspectionTypeModel {\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return this.Equals(input as InspectionTypeModel);
        }

        /// <summary>
        /// Returns true if InspectionTypeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of InspectionTypeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InspectionTypeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Group == input.Group ||
                    (this.Group != null &&
                    this.Group.Equals(input.Group))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
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
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.Value != null)
                    hashCode = hashCode * 59 + this.Value.GetHashCode();
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
