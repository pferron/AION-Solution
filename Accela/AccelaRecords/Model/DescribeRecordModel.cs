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
    /// DescribeRecordModel
    /// </summary>
    [DataContract]
    public partial class DescribeRecordModel :  IEquatable<DescribeRecordModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeRecordModel" /> class.
        /// </summary>
        /// <param name="elements">elements.</param>
        /// <param name="fields">fields.</param>
        public DescribeRecordModel(List<ElementModel> elements = default(List<ElementModel>), List<FieldModel> fields = default(List<FieldModel>))
        {
            this.Elements = elements;
            this.Fields = fields;
        }
        
        /// <summary>
        /// Gets or Sets Elements
        /// </summary>
        [DataMember(Name="elements", EmitDefaultValue=false)]
        public List<ElementModel> Elements { get; set; }

        /// <summary>
        /// Gets or Sets Fields
        /// </summary>
        [DataMember(Name="fields", EmitDefaultValue=false)]
        public List<FieldModel> Fields { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DescribeRecordModel {\n");
            sb.Append("  Elements: ").Append(Elements).Append("\n");
            sb.Append("  Fields: ").Append(Fields).Append("\n");
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
            return this.Equals(input as DescribeRecordModel);
        }

        /// <summary>
        /// Returns true if DescribeRecordModel instances are equal
        /// </summary>
        /// <param name="input">Instance of DescribeRecordModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DescribeRecordModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Elements == input.Elements ||
                    this.Elements != null &&
                    this.Elements.SequenceEqual(input.Elements)
                ) && 
                (
                    this.Fields == input.Fields ||
                    this.Fields != null &&
                    this.Fields.SequenceEqual(input.Fields)
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
                if (this.Elements != null)
                    hashCode = hashCode * 59 + this.Elements.GetHashCode();
                if (this.Fields != null)
                    hashCode = hashCode * 59 + this.Fields.GetHashCode();
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