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
    /// InspectionTypeAssociationsModel
    /// </summary>
    [DataContract]
    public partial class InspectionTypeAssociationsModel :  IEquatable<InspectionTypeAssociationsModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InspectionTypeAssociationsModel" /> class.
        /// </summary>
        /// <param name="standardCommentGroup">standardCommentGroup.</param>
        public InspectionTypeAssociationsModel(InspectionTypeAssociationsModelStandardCommentGroup standardCommentGroup = default(InspectionTypeAssociationsModelStandardCommentGroup))
        {
            this.StandardCommentGroup = standardCommentGroup;
        }
        
        /// <summary>
        /// Gets or Sets StandardCommentGroup
        /// </summary>
        [DataMember(Name="standardCommentGroup", EmitDefaultValue=false)]
        public InspectionTypeAssociationsModelStandardCommentGroup StandardCommentGroup { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InspectionTypeAssociationsModel {\n");
            sb.Append("  StandardCommentGroup: ").Append(StandardCommentGroup).Append("\n");
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
            return this.Equals(input as InspectionTypeAssociationsModel);
        }

        /// <summary>
        /// Returns true if InspectionTypeAssociationsModel instances are equal
        /// </summary>
        /// <param name="input">Instance of InspectionTypeAssociationsModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InspectionTypeAssociationsModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.StandardCommentGroup == input.StandardCommentGroup ||
                    (this.StandardCommentGroup != null &&
                    this.StandardCommentGroup.Equals(input.StandardCommentGroup))
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
                if (this.StandardCommentGroup != null)
                    hashCode = hashCode * 59 + this.StandardCommentGroup.GetHashCode();
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
