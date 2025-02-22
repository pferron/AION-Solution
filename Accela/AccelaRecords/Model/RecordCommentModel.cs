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
    /// RecordCommentModel
    /// </summary>
    [DataContract]
    public partial class RecordCommentModel :  IEquatable<RecordCommentModel>, IValidatableObject
    {
        /// <summary>
        /// Indicates whether or not the comment is displayed on inspection.
        /// </summary>
        /// <value>Indicates whether or not the comment is displayed on inspection.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DisplayOnInspectionEnum
        {
            
            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")]
            Y = 1,
            
            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")]
            N = 2
        }

        /// <summary>
        /// Indicates whether or not the comment is displayed on inspection.
        /// </summary>
        /// <value>Indicates whether or not the comment is displayed on inspection.</value>
        [DataMember(Name="displayOnInspection", EmitDefaultValue=false)]
        public DisplayOnInspectionEnum? DisplayOnInspection { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordCommentModel" /> class.
        /// </summary>
        /// <param name="createdBy">The user who added the record comment..</param>
        /// <param name="createdDate">The date when the record comment was added..</param>
        /// <param name="displayOnInspection">Indicates whether or not the comment is displayed on inspection..</param>
        /// <param name="id">The comment system id assigned by the Civic Platform server..</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="text">The comment text..</param>
        public RecordCommentModel(string createdBy = default(string), DateTime? createdDate = default(DateTime?), DisplayOnInspectionEnum? displayOnInspection = default(DisplayOnInspectionEnum?), long? id = default(long?), RecordIdModel recordId = default(RecordIdModel), string text = default(string))
        {
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.DisplayOnInspection = displayOnInspection;
            this.Id = id;
            this.RecordId = recordId;
            this.Text = text;
        }
        
        /// <summary>
        /// The user who added the record comment.
        /// </summary>
        /// <value>The user who added the record comment.</value>
        [DataMember(Name="createdBy", EmitDefaultValue=false)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date when the record comment was added.
        /// </summary>
        /// <value>The date when the record comment was added.</value>
        [DataMember(Name="createdDate", EmitDefaultValue=false)]
        public DateTime? CreatedDate { get; set; }


        /// <summary>
        /// The comment system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The comment system id assigned by the Civic Platform server.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name="recordId", EmitDefaultValue=false)]
        public RecordIdModel RecordId { get; set; }

        /// <summary>
        /// The comment text.
        /// </summary>
        /// <value>The comment text.</value>
        [DataMember(Name="text", EmitDefaultValue=false)]
        public string Text { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordCommentModel {\n");
            sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
            sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
            sb.Append("  DisplayOnInspection: ").Append(DisplayOnInspection).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  RecordId: ").Append(RecordId).Append("\n");
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
            return this.Equals(input as RecordCommentModel);
        }

        /// <summary>
        /// Returns true if RecordCommentModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RecordCommentModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecordCommentModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CreatedBy == input.CreatedBy ||
                    (this.CreatedBy != null &&
                    this.CreatedBy.Equals(input.CreatedBy))
                ) && 
                (
                    this.CreatedDate == input.CreatedDate ||
                    (this.CreatedDate != null &&
                    this.CreatedDate.Equals(input.CreatedDate))
                ) && 
                (
                    this.DisplayOnInspection == input.DisplayOnInspection ||
                    (this.DisplayOnInspection != null &&
                    this.DisplayOnInspection.Equals(input.DisplayOnInspection))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.RecordId == input.RecordId ||
                    (this.RecordId != null &&
                    this.RecordId.Equals(input.RecordId))
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
                if (this.CreatedBy != null)
                    hashCode = hashCode * 59 + this.CreatedBy.GetHashCode();
                if (this.CreatedDate != null)
                    hashCode = hashCode * 59 + this.CreatedDate.GetHashCode();
                if (this.DisplayOnInspection != null)
                    hashCode = hashCode * 59 + this.DisplayOnInspection.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.RecordId != null)
                    hashCode = hashCode * 59 + this.RecordId.GetHashCode();
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
