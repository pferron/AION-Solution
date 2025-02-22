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
    /// A set of custom field name-value pairs on a custom form.  Added in Civic Platform version: 9.2.0
    /// </summary>
    [DataContract]
    public partial class ApoCustomForm :  IEquatable<ApoCustomForm>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApoCustomForm" /> class.
        /// </summary>
        /// <param name="id">The unique string id of the custom form template for the custom data..</param>
        /// <param name="aCustomFieldName">A custom field name. Note that this is the custom attribute name (not the attribute label). To get the attribute display label, use [Get Record Address Custom Forms Metadata](#operation/v4.get.records.recordId.addresses.addressId.customForms.meta)..</param>
        /// <param name="aCustomFieldValue">A custom field value.</param>
        public ApoCustomForm(string id = default(string), string aCustomFieldName = default(string), string aCustomFieldValue = default(string))
        {
            this.Id = id;
            this.ACustomFieldName = aCustomFieldName;
            this.ACustomFieldValue = aCustomFieldValue;
        }
        
        /// <summary>
        /// The unique string id of the custom form template for the custom data.
        /// </summary>
        /// <value>The unique string id of the custom form template for the custom data.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// A custom field name. Note that this is the custom attribute name (not the attribute label). To get the attribute display label, use [Get Record Address Custom Forms Metadata](#operation/v4.get.records.recordId.addresses.addressId.customForms.meta).
        /// </summary>
        /// <value>A custom field name. Note that this is the custom attribute name (not the attribute label). To get the attribute display label, use [Get Record Address Custom Forms Metadata](#operation/v4.get.records.recordId.addresses.addressId.customForms.meta).</value>
        [DataMember(Name="aCustomFieldName", EmitDefaultValue=false)]
        public string ACustomFieldName { get; set; }

        /// <summary>
        /// A custom field value
        /// </summary>
        /// <value>A custom field value</value>
        [DataMember(Name="aCustomFieldValue", EmitDefaultValue=false)]
        public string ACustomFieldValue { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApoCustomForm {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ACustomFieldName: ").Append(ACustomFieldName).Append("\n");
            sb.Append("  ACustomFieldValue: ").Append(ACustomFieldValue).Append("\n");
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
            return this.Equals(input as ApoCustomForm);
        }

        /// <summary>
        /// Returns true if ApoCustomForm instances are equal
        /// </summary>
        /// <param name="input">Instance of ApoCustomForm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ApoCustomForm input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.ACustomFieldName == input.ACustomFieldName ||
                    (this.ACustomFieldName != null &&
                    this.ACustomFieldName.Equals(input.ACustomFieldName))
                ) && 
                (
                    this.ACustomFieldValue == input.ACustomFieldValue ||
                    (this.ACustomFieldValue != null &&
                    this.ACustomFieldValue.Equals(input.ACustomFieldValue))
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
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.ACustomFieldName != null)
                    hashCode = hashCode * 59 + this.ACustomFieldName.GetHashCode();
                if (this.ACustomFieldValue != null)
                    hashCode = hashCode * 59 + this.ACustomFieldValue.GetHashCode();
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
