/* 
 * Assets and Assessments
 *
 * The Assets and Assessments APIs enable apps to manage assets and their related condition assessments.
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
using SwaggerDateConverter = AccelaAssetsAndAssessments.Client.SwaggerDateConverter;

namespace AccelaAssetsAndAssessments.Model
{
    /// <summary>
    /// RecordTypeModel
    /// </summary>
    [DataContract]
        public partial class RecordTypeModel :  IEquatable<RecordTypeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordTypeModel" /> class.
        /// </summary>
        /// <param name="alias">The record type alias..</param>
        /// <param name="category">The 4th level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="filterName">The name of the record type filter which defines the record types to be displayed for the citizen user..</param>
        /// <param name="group">The 1st level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="id">The record type id..</param>
        /// <param name="module">The module the record type belongs to..</param>
        /// <param name="subType">The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="text">The localized display text..</param>
        /// <param name="type">The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="value">The stored value..</param>
        public RecordTypeModel(string alias = default(string), string category = default(string), string filterName = default(string), string group = default(string), string id = default(string), string module = default(string), string subType = default(string), string text = default(string), string type = default(string), string value = default(string))
        {
            this.Alias = alias;
            this.Category = category;
            this.FilterName = filterName;
            this.Group = group;
            this.Id = id;
            this.Module = module;
            this.SubType = subType;
            this.Text = text;
            this.Type = type;
            this.Value = value;
        }
        
        /// <summary>
        /// The record type alias.
        /// </summary>
        /// <value>The record type alias.</value>
        [DataMember(Name="alias", EmitDefaultValue=false)]
        public string Alias { get; set; }

        /// <summary>
        /// The 4th level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 4th level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name="category", EmitDefaultValue=false)]
        public string Category { get; set; }

        /// <summary>
        /// The name of the record type filter which defines the record types to be displayed for the citizen user.
        /// </summary>
        /// <value>The name of the record type filter which defines the record types to be displayed for the citizen user.</value>
        [DataMember(Name="filterName", EmitDefaultValue=false)]
        public string FilterName { get; set; }

        /// <summary>
        /// The 1st level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 1st level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public string Group { get; set; }

        /// <summary>
        /// The record type id.
        /// </summary>
        /// <value>The record type id.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// The module the record type belongs to.
        /// </summary>
        /// <value>The module the record type belongs to.</value>
        [DataMember(Name="module", EmitDefaultValue=false)]
        public string Module { get; set; }

        /// <summary>
        /// The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name="subType", EmitDefaultValue=false)]
        public string SubType { get; set; }

        /// <summary>
        /// The localized display text.
        /// </summary>
        /// <value>The localized display text.</value>
        [DataMember(Name="text", EmitDefaultValue=false)]
        public string Text { get; set; }

        /// <summary>
        /// The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public string Type { get; set; }

        /// <summary>
        /// The stored value.
        /// </summary>
        /// <value>The stored value.</value>
        [DataMember(Name="value", EmitDefaultValue=false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordTypeModel {\n");
            sb.Append("  Alias: ").Append(Alias).Append("\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  FilterName: ").Append(FilterName).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Module: ").Append(Module).Append("\n");
            sb.Append("  SubType: ").Append(SubType).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return this.Equals(input as RecordTypeModel);
        }

        /// <summary>
        /// Returns true if RecordTypeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RecordTypeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecordTypeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Alias == input.Alias ||
                    (this.Alias != null &&
                    this.Alias.Equals(input.Alias))
                ) && 
                (
                    this.Category == input.Category ||
                    (this.Category != null &&
                    this.Category.Equals(input.Category))
                ) && 
                (
                    this.FilterName == input.FilterName ||
                    (this.FilterName != null &&
                    this.FilterName.Equals(input.FilterName))
                ) && 
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
                    this.Module == input.Module ||
                    (this.Module != null &&
                    this.Module.Equals(input.Module))
                ) && 
                (
                    this.SubType == input.SubType ||
                    (this.SubType != null &&
                    this.SubType.Equals(input.SubType))
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
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
                if (this.Alias != null)
                    hashCode = hashCode * 59 + this.Alias.GetHashCode();
                if (this.Category != null)
                    hashCode = hashCode * 59 + this.Category.GetHashCode();
                if (this.FilterName != null)
                    hashCode = hashCode * 59 + this.FilterName.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Module != null)
                    hashCode = hashCode * 59 + this.Module.GetHashCode();
                if (this.SubType != null)
                    hashCode = hashCode * 59 + this.SubType.GetHashCode();
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
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