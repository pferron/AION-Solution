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
    /// RatingTypeModel
    /// </summary>
    [DataContract]
    public partial class RatingTypeModel :  IEquatable<RatingTypeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingTypeModel" /> class.
        /// </summary>
        /// <param name="manualEntryRatingValue">If &#39;Y&#39;, a user can create a rating formula. If the user creates a formula, the user can enter manual rating values in addition to the calculated rating values. If the user does not create a formula, the user must enter rating values manually. If &#39;N&#39;, the user must create a rating formula..</param>
        /// <param name="recordStatus">The status of the rating type..</param>
        /// <param name="updatedBy">The person who last updated the rating type..</param>
        /// <param name="updatedDate">The date the rating type was last updated..</param>
        public RatingTypeModel(string manualEntryRatingValue = default(string), string recordStatus = default(string), string updatedBy = default(string), DateTime? updatedDate = default(DateTime?))
        {
            this.ManualEntryRatingValue = manualEntryRatingValue;
            this.RecordStatus = recordStatus;
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = updatedDate;
        }
        
        /// <summary>
        /// If &#39;Y&#39;, a user can create a rating formula. If the user creates a formula, the user can enter manual rating values in addition to the calculated rating values. If the user does not create a formula, the user must enter rating values manually. If &#39;N&#39;, the user must create a rating formula.
        /// </summary>
        /// <value>If &#39;Y&#39;, a user can create a rating formula. If the user creates a formula, the user can enter manual rating values in addition to the calculated rating values. If the user does not create a formula, the user must enter rating values manually. If &#39;N&#39;, the user must create a rating formula.</value>
        [DataMember(Name="manualEntryRatingValue", EmitDefaultValue=false)]
        public string ManualEntryRatingValue { get; set; }

        /// <summary>
        /// The status of the rating type.
        /// </summary>
        /// <value>The status of the rating type.</value>
        [DataMember(Name="recordStatus", EmitDefaultValue=false)]
        public string RecordStatus { get; set; }

        /// <summary>
        /// The person who last updated the rating type.
        /// </summary>
        /// <value>The person who last updated the rating type.</value>
        [DataMember(Name="updatedBy", EmitDefaultValue=false)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// The date the rating type was last updated.
        /// </summary>
        /// <value>The date the rating type was last updated.</value>
        [DataMember(Name="updatedDate", EmitDefaultValue=false)]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RatingTypeModel {\n");
            sb.Append("  ManualEntryRatingValue: ").Append(ManualEntryRatingValue).Append("\n");
            sb.Append("  RecordStatus: ").Append(RecordStatus).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
            sb.Append("  UpdatedDate: ").Append(UpdatedDate).Append("\n");
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
            return this.Equals(input as RatingTypeModel);
        }

        /// <summary>
        /// Returns true if RatingTypeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RatingTypeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RatingTypeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ManualEntryRatingValue == input.ManualEntryRatingValue ||
                    (this.ManualEntryRatingValue != null &&
                    this.ManualEntryRatingValue.Equals(input.ManualEntryRatingValue))
                ) && 
                (
                    this.RecordStatus == input.RecordStatus ||
                    (this.RecordStatus != null &&
                    this.RecordStatus.Equals(input.RecordStatus))
                ) && 
                (
                    this.UpdatedBy == input.UpdatedBy ||
                    (this.UpdatedBy != null &&
                    this.UpdatedBy.Equals(input.UpdatedBy))
                ) && 
                (
                    this.UpdatedDate == input.UpdatedDate ||
                    (this.UpdatedDate != null &&
                    this.UpdatedDate.Equals(input.UpdatedDate))
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
                if (this.ManualEntryRatingValue != null)
                    hashCode = hashCode * 59 + this.ManualEntryRatingValue.GetHashCode();
                if (this.RecordStatus != null)
                    hashCode = hashCode * 59 + this.RecordStatus.GetHashCode();
                if (this.UpdatedBy != null)
                    hashCode = hashCode * 59 + this.UpdatedBy.GetHashCode();
                if (this.UpdatedDate != null)
                    hashCode = hashCode * 59 + this.UpdatedDate.GetHashCode();
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
