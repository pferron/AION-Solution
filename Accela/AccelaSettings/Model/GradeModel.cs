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
    /// GradeModel
    /// </summary>
    [DataContract]
    public partial class GradeModel :  IEquatable<GradeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradeModel" /> class.
        /// </summary>
        /// <param name="grade">grade.</param>
        /// <param name="gradeImage">The link to an image file associated to the inspection grade..</param>
        /// <param name="group">The inspection grade group for the inspection grade value..</param>
        /// <param name="maximumMajorViolation"> Used with the maxMajorViolation field to define a range of the acceptable number of major violations for an inspection..</param>
        /// <param name="maximumScore"> Used with the minScore field to define a range of acceptable values for an inspection..</param>
        /// <param name="minimumMajorViolation"> Used with the maxMajorViolation field to define a range of the acceptable number of major violations for an inspection..</param>
        /// <param name="minimumScore"> Used with the maxScore field to define a range of acceptable values for an inspection..</param>
        public GradeModel(GradeModelGrade grade = default(GradeModelGrade), string gradeImage = default(string), string group = default(string), long? maximumMajorViolation = default(long?), long? maximumScore = default(long?), long? minimumMajorViolation = default(long?), long? minimumScore = default(long?))
        {
            this.Grade = grade;
            this.GradeImage = gradeImage;
            this.Group = group;
            this.MaximumMajorViolation = maximumMajorViolation;
            this.MaximumScore = maximumScore;
            this.MinimumMajorViolation = minimumMajorViolation;
            this.MinimumScore = minimumScore;
        }
        
        /// <summary>
        /// Gets or Sets Grade
        /// </summary>
        [DataMember(Name="grade", EmitDefaultValue=false)]
        public GradeModelGrade Grade { get; set; }

        /// <summary>
        /// The link to an image file associated to the inspection grade.
        /// </summary>
        /// <value>The link to an image file associated to the inspection grade.</value>
        [DataMember(Name="gradeImage", EmitDefaultValue=false)]
        public string GradeImage { get; set; }

        /// <summary>
        /// The inspection grade group for the inspection grade value.
        /// </summary>
        /// <value>The inspection grade group for the inspection grade value.</value>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public string Group { get; set; }

        /// <summary>
        ///  Used with the maxMajorViolation field to define a range of the acceptable number of major violations for an inspection.
        /// </summary>
        /// <value> Used with the maxMajorViolation field to define a range of the acceptable number of major violations for an inspection.</value>
        [DataMember(Name="maximumMajorViolation", EmitDefaultValue=false)]
        public long? MaximumMajorViolation { get; set; }

        /// <summary>
        ///  Used with the minScore field to define a range of acceptable values for an inspection.
        /// </summary>
        /// <value> Used with the minScore field to define a range of acceptable values for an inspection.</value>
        [DataMember(Name="maximumScore", EmitDefaultValue=false)]
        public long? MaximumScore { get; set; }

        /// <summary>
        ///  Used with the maxMajorViolation field to define a range of the acceptable number of major violations for an inspection.
        /// </summary>
        /// <value> Used with the maxMajorViolation field to define a range of the acceptable number of major violations for an inspection.</value>
        [DataMember(Name="minimumMajorViolation", EmitDefaultValue=false)]
        public long? MinimumMajorViolation { get; set; }

        /// <summary>
        ///  Used with the maxScore field to define a range of acceptable values for an inspection.
        /// </summary>
        /// <value> Used with the maxScore field to define a range of acceptable values for an inspection.</value>
        [DataMember(Name="minimumScore", EmitDefaultValue=false)]
        public long? MinimumScore { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GradeModel {\n");
            sb.Append("  Grade: ").Append(Grade).Append("\n");
            sb.Append("  GradeImage: ").Append(GradeImage).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  MaximumMajorViolation: ").Append(MaximumMajorViolation).Append("\n");
            sb.Append("  MaximumScore: ").Append(MaximumScore).Append("\n");
            sb.Append("  MinimumMajorViolation: ").Append(MinimumMajorViolation).Append("\n");
            sb.Append("  MinimumScore: ").Append(MinimumScore).Append("\n");
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
            return this.Equals(input as GradeModel);
        }

        /// <summary>
        /// Returns true if GradeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of GradeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GradeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Grade == input.Grade ||
                    (this.Grade != null &&
                    this.Grade.Equals(input.Grade))
                ) && 
                (
                    this.GradeImage == input.GradeImage ||
                    (this.GradeImage != null &&
                    this.GradeImage.Equals(input.GradeImage))
                ) && 
                (
                    this.Group == input.Group ||
                    (this.Group != null &&
                    this.Group.Equals(input.Group))
                ) && 
                (
                    this.MaximumMajorViolation == input.MaximumMajorViolation ||
                    (this.MaximumMajorViolation != null &&
                    this.MaximumMajorViolation.Equals(input.MaximumMajorViolation))
                ) && 
                (
                    this.MaximumScore == input.MaximumScore ||
                    (this.MaximumScore != null &&
                    this.MaximumScore.Equals(input.MaximumScore))
                ) && 
                (
                    this.MinimumMajorViolation == input.MinimumMajorViolation ||
                    (this.MinimumMajorViolation != null &&
                    this.MinimumMajorViolation.Equals(input.MinimumMajorViolation))
                ) && 
                (
                    this.MinimumScore == input.MinimumScore ||
                    (this.MinimumScore != null &&
                    this.MinimumScore.Equals(input.MinimumScore))
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
                if (this.Grade != null)
                    hashCode = hashCode * 59 + this.Grade.GetHashCode();
                if (this.GradeImage != null)
                    hashCode = hashCode * 59 + this.GradeImage.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.MaximumMajorViolation != null)
                    hashCode = hashCode * 59 + this.MaximumMajorViolation.GetHashCode();
                if (this.MaximumScore != null)
                    hashCode = hashCode * 59 + this.MaximumScore.GetHashCode();
                if (this.MinimumMajorViolation != null)
                    hashCode = hashCode * 59 + this.MinimumMajorViolation.GetHashCode();
                if (this.MinimumScore != null)
                    hashCode = hashCode * 59 + this.MinimumScore.GetHashCode();
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
