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
    /// TableModel
    /// </summary>
    [DataContract]
    public partial class TableModel :  IEquatable<TableModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableModel" /> class.
        /// </summary>
        /// <param name="id">The custom table id..</param>
        /// <param name="rows">A table row containing custom fields..</param>
        public TableModel(string id = default(string), List<RowModel> rows = default(List<RowModel>))
        {
            this.Id = id;
            this.Rows = rows;
        }
        
        /// <summary>
        /// The custom table id.
        /// </summary>
        /// <value>The custom table id.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// A table row containing custom fields.
        /// </summary>
        /// <value>A table row containing custom fields.</value>
        [DataMember(Name="rows", EmitDefaultValue=false)]
        public List<RowModel> Rows { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TableModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Rows: ").Append(Rows).Append("\n");
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
            return this.Equals(input as TableModel);
        }

        /// <summary>
        /// Returns true if TableModel instances are equal
        /// </summary>
        /// <param name="input">Instance of TableModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TableModel input)
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
                    this.Rows == input.Rows ||
                    this.Rows != null &&
                    this.Rows.SequenceEqual(input.Rows)
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
                if (this.Rows != null)
                    hashCode = hashCode * 59 + this.Rows.GetHashCode();
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
