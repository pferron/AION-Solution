/* 
 * Citizens
 *
 * The Citizens API include endpoints for citizen users to manage their own accounts, authorized users to manage other accounts, and manage citizen delegates, announcements, and invitations. 
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
using SwaggerDateConverter = AccelaCitizens.Client.SwaggerDateConverter;

namespace AccelaCitizens.Model
{
    /// <summary>
    /// PermissionLevelModel
    /// </summary>
    [DataContract]
    public partial class PermissionLevelModel :  IEquatable<PermissionLevelModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionLevelModel" /> class.
        /// </summary>
        /// <param name="mODULE">The Civic Platform module the delegate has access to..</param>
        /// <param name="aGENCY">The agency the delegate has access to, for a multi-agency environment..</param>
        public PermissionLevelModel(string mODULE = default(string), string aGENCY = default(string))
        {
            this.MODULE = mODULE;
            this.AGENCY = aGENCY;
        }
        
        /// <summary>
        /// The Civic Platform module the delegate has access to.
        /// </summary>
        /// <value>The Civic Platform module the delegate has access to.</value>
        [DataMember(Name="MODULE", EmitDefaultValue=false)]
        public string MODULE { get; set; }

        /// <summary>
        /// The agency the delegate has access to, for a multi-agency environment.
        /// </summary>
        /// <value>The agency the delegate has access to, for a multi-agency environment.</value>
        [DataMember(Name="AGENCY", EmitDefaultValue=false)]
        public string AGENCY { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PermissionLevelModel {\n");
            sb.Append("  MODULE: ").Append(MODULE).Append("\n");
            sb.Append("  AGENCY: ").Append(AGENCY).Append("\n");
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
            return this.Equals(input as PermissionLevelModel);
        }

        /// <summary>
        /// Returns true if PermissionLevelModel instances are equal
        /// </summary>
        /// <param name="input">Instance of PermissionLevelModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PermissionLevelModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.MODULE == input.MODULE ||
                    (this.MODULE != null &&
                    this.MODULE.Equals(input.MODULE))
                ) && 
                (
                    this.AGENCY == input.AGENCY ||
                    (this.AGENCY != null &&
                    this.AGENCY.Equals(input.AGENCY))
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
                if (this.MODULE != null)
                    hashCode = hashCode * 59 + this.MODULE.GetHashCode();
                if (this.AGENCY != null)
                    hashCode = hashCode * 59 + this.AGENCY.GetHashCode();
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
