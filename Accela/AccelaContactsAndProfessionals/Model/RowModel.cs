/* 
 * Contacts and Professionals
 *
 * The Contacts and Professionals APIs enable apps to manage reference contacts and licensed professionals.
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
using SwaggerDateConverter = AccelaContactsAndProfessionals.Client.SwaggerDateConverter;

namespace AccelaContactsAndProfessionals.Model
{
    /// <summary>
    /// RowModel
    /// </summary>
    [DataContract]
    public partial class RowModel :  IEquatable<RowModel>, IValidatableObject
    {
        /// <summary>
        /// The requested operation on the row.
        /// </summary>
        /// <value>The requested operation on the row.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ActionEnum
        {
            
            /// <summary>
            /// Enum Add for value: add
            /// </summary>
            [EnumMember(Value = "add")]
            Add = 1,
            
            /// <summary>
            /// Enum Update for value: update
            /// </summary>
            [EnumMember(Value = "update")]
            Update = 2,
            
            /// <summary>
            /// Enum Delete for value: delete
            /// </summary>
            [EnumMember(Value = "delete")]
            Delete = 3
        }

        /// <summary>
        /// The requested operation on the row.
        /// </summary>
        /// <value>The requested operation on the row.</value>
        [DataMember(Name="action", EmitDefaultValue=false)]
        public ActionEnum? Action { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RowModel" /> class.
        /// </summary>
        /// <param name="action">The requested operation on the row..</param>
        /// <param name="fields">fields.</param>
        /// <param name="id">The row id..</param>
        public RowModel(ActionEnum? action = default(ActionEnum?), CustomAttributeModel fields = default(CustomAttributeModel), string id = default(string))
        {
            this.Action = action;
            this.Fields = fields;
            this.Id = id;
        }
        

        /// <summary>
        /// Gets or Sets Fields
        /// </summary>
        [DataMember(Name="fields", EmitDefaultValue=false)]
        public CustomAttributeModel Fields { get; set; }

        /// <summary>
        /// The row id.
        /// </summary>
        /// <value>The row id.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RowModel {\n");
            sb.Append("  Action: ").Append(Action).Append("\n");
            sb.Append("  Fields: ").Append(Fields).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
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
            return this.Equals(input as RowModel);
        }

        /// <summary>
        /// Returns true if RowModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RowModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RowModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Action == input.Action ||
                    (this.Action != null &&
                    this.Action.Equals(input.Action))
                ) && 
                (
                    this.Fields == input.Fields ||
                    (this.Fields != null &&
                    this.Fields.Equals(input.Fields))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
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
                if (this.Action != null)
                    hashCode = hashCode * 59 + this.Action.GetHashCode();
                if (this.Fields != null)
                    hashCode = hashCode * 59 + this.Fields.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
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