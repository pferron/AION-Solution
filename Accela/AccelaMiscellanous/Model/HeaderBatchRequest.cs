/* 
 * Miscellaneous
 *
 * Miscellaneous Construct APIs
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
using SwaggerDateConverter = AccelaMiscellanous.Client.SwaggerDateConverter;

namespace AccelaMiscellanous.Model
{
    /// <summary>
    /// HeaderBatchRequest
    /// </summary>
    [DataContract]
    public partial class HeaderBatchRequest :  IEquatable<HeaderBatchRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderBatchRequest" /> class.
        /// </summary>
        /// <param name="aHeaderKey">HTTP header key/value pairs. For example:  \&quot;headers\&quot; : { \&quot;header-key1\&quot;:\&quot;header-value1\&quot;, \&quot;header-key2\&quot;:\&quot;header-value2\&quot; }.  Provide the HTTP headers required by the operation&#39;s authorization type. If the required HTTP headers related to authorization are not provided, the operation will fail..</param>
        public HeaderBatchRequest(string aHeaderKey = default(string))
        {
            this.AHeaderKey = aHeaderKey;
        }
        
        /// <summary>
        /// HTTP header key/value pairs. For example:  \&quot;headers\&quot; : { \&quot;header-key1\&quot;:\&quot;header-value1\&quot;, \&quot;header-key2\&quot;:\&quot;header-value2\&quot; }.  Provide the HTTP headers required by the operation&#39;s authorization type. If the required HTTP headers related to authorization are not provided, the operation will fail.
        /// </summary>
        /// <value>HTTP header key/value pairs. For example:  \&quot;headers\&quot; : { \&quot;header-key1\&quot;:\&quot;header-value1\&quot;, \&quot;header-key2\&quot;:\&quot;header-value2\&quot; }.  Provide the HTTP headers required by the operation&#39;s authorization type. If the required HTTP headers related to authorization are not provided, the operation will fail.</value>
        [DataMember(Name="&lt;aHeaderKey&gt;", EmitDefaultValue=false)]
        public string AHeaderKey { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class HeaderBatchRequest {\n");
            sb.Append("  AHeaderKey: ").Append(AHeaderKey).Append("\n");
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
            return this.Equals(input as HeaderBatchRequest);
        }

        /// <summary>
        /// Returns true if HeaderBatchRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of HeaderBatchRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(HeaderBatchRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AHeaderKey == input.AHeaderKey ||
                    (this.AHeaderKey != null &&
                    this.AHeaderKey.Equals(input.AHeaderKey))
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
                if (this.AHeaderKey != null)
                    hashCode = hashCode * 59 + this.AHeaderKey.GetHashCode();
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