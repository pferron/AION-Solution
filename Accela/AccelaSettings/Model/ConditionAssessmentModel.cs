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
    /// ConditionAssessmentModel
    /// </summary>
    [DataContract]
    public partial class ConditionAssessmentModel :  IEquatable<ConditionAssessmentModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionAssessmentModel" /> class.
        /// </summary>
        /// <param name="conditionAssessment">The condition assessment id, as defined on Civic Platform Administration..</param>
        /// <param name="description">The condition assessment description. If the description is null, this field is not returned..</param>
        public ConditionAssessmentModel(string conditionAssessment = default(string), string description = default(string))
        {
            this.ConditionAssessment = conditionAssessment;
            this.Description = description;
        }
        
        /// <summary>
        /// The condition assessment id, as defined on Civic Platform Administration.
        /// </summary>
        /// <value>The condition assessment id, as defined on Civic Platform Administration.</value>
        [DataMember(Name="conditionAssessment", EmitDefaultValue=false)]
        public string ConditionAssessment { get; set; }

        /// <summary>
        /// The condition assessment description. If the description is null, this field is not returned.
        /// </summary>
        /// <value>The condition assessment description. If the description is null, this field is not returned.</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ConditionAssessmentModel {\n");
            sb.Append("  ConditionAssessment: ").Append(ConditionAssessment).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
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
            return this.Equals(input as ConditionAssessmentModel);
        }

        /// <summary>
        /// Returns true if ConditionAssessmentModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ConditionAssessmentModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ConditionAssessmentModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ConditionAssessment == input.ConditionAssessment ||
                    (this.ConditionAssessment != null &&
                    this.ConditionAssessment.Equals(input.ConditionAssessment))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
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
                if (this.ConditionAssessment != null)
                    hashCode = hashCode * 59 + this.ConditionAssessment.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
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