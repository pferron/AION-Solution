/* 
 * Records
 *
 * Construct APIs for transactional records and related record resources
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
using SwaggerDateConverter = AccelaRecords.Client.SwaggerDateConverter;

namespace AccelaRecords.Model
{
    /// <summary>
    /// Contains metadata description of a custom form, including the custom fields metadata.  Added in Civic Platform version: 9.2.0
    /// </summary>
    [DataContract]
    public partial class ApoCustomFormsMetadata :  IEquatable<ApoCustomFormsMetadata>, IValidatableObject
    {
        /// <summary>
        /// Indicates whether the custom form is for an address, parcel, or owner.
        /// </summary>
        /// <value>Indicates whether the custom form is for an address, parcel, or owner.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CustomFormTypeEnum
        {
            
            /// <summary>
            /// Enum Address for value: address
            /// </summary>
            [EnumMember(Value = "address")]
            Address = 1,
            
            /// <summary>
            /// Enum Parcel for value: parcel
            /// </summary>
            [EnumMember(Value = "parcel")]
            Parcel = 2,
            
            /// <summary>
            /// Enum Owner for value: owner
            /// </summary>
            [EnumMember(Value = "owner")]
            Owner = 3
        }

        /// <summary>
        /// Indicates whether the custom form is for an address, parcel, or owner.
        /// </summary>
        /// <value>Indicates whether the custom form is for an address, parcel, or owner.</value>
        [DataMember(Name="customFormType", EmitDefaultValue=false)]
        public CustomFormTypeEnum? CustomFormType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ApoCustomFormsMetadata" /> class.
        /// </summary>
        /// <param name="name">The name of the custom form.</param>
        /// <param name="description">Describes the usage or puporse of the custom form..</param>
        /// <param name="fields">Contains the field metadata..</param>
        /// <param name="id">The unique string identifier of the custom form..</param>
        /// <param name="customFormType">Indicates whether the custom form is for an address, parcel, or owner..</param>
        public ApoCustomFormsMetadata(string name = default(string), string description = default(string), List<ApoCustomFormsMetadataFields> fields = default(List<ApoCustomFormsMetadataFields>), string id = default(string), CustomFormTypeEnum? customFormType = default(CustomFormTypeEnum?))
        {
            this.Name = name;
            this.Description = description;
            this.Fields = fields;
            this.Id = id;
            this.CustomFormType = customFormType;
        }
        
        /// <summary>
        /// The name of the custom form
        /// </summary>
        /// <value>The name of the custom form</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Describes the usage or puporse of the custom form.
        /// </summary>
        /// <value>Describes the usage or puporse of the custom form.</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Contains the field metadata.
        /// </summary>
        /// <value>Contains the field metadata.</value>
        [DataMember(Name="fields", EmitDefaultValue=false)]
        public List<ApoCustomFormsMetadataFields> Fields { get; set; }

        /// <summary>
        /// The unique string identifier of the custom form.
        /// </summary>
        /// <value>The unique string identifier of the custom form.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApoCustomFormsMetadata {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Fields: ").Append(Fields).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CustomFormType: ").Append(CustomFormType).Append("\n");
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
            return this.Equals(input as ApoCustomFormsMetadata);
        }

        /// <summary>
        /// Returns true if ApoCustomFormsMetadata instances are equal
        /// </summary>
        /// <param name="input">Instance of ApoCustomFormsMetadata to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ApoCustomFormsMetadata input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Fields == input.Fields ||
                    this.Fields != null &&
                    this.Fields.SequenceEqual(input.Fields)
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CustomFormType == input.CustomFormType ||
                    (this.CustomFormType != null &&
                    this.CustomFormType.Equals(input.CustomFormType))
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
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.Fields != null)
                    hashCode = hashCode * 59 + this.Fields.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.CustomFormType != null)
                    hashCode = hashCode * 59 + this.CustomFormType.GetHashCode();
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