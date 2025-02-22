/* 
 * Authentication
 *
 * Construct's OAuth2 APIs for generating API access tokens. For an overview, see [Construct API Authentication](../construct-apiAuth.html).
 *
 * OpenAPI spec version: v4-oas3
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
using SwaggerDateConverter = AccelaAuthorization.Client.SwaggerDateConverter;

namespace AccelaAuthorization.Model
{
    /// <summary>
    /// ResponseStatus
    /// </summary>
    [DataContract]
        public partial class ResponseStatus :  IEquatable<ResponseStatus>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseStatus" /> class.
        /// </summary>
        /// <param name="status">The HTTP error code..</param>
        /// <param name="code">The error code..</param>
        /// <param name="message">The error message..</param>
        /// <param name="traceId">The traceid for debugging purposes. .</param>
        public ResponseStatus(string status = default(string), string code = default(string), string message = default(string), string traceId = default(string))
        {
            this.Status = status;
            this.Code = code;
            this.Message = message;
            this.TraceId = traceId;
        }
        
        /// <summary>
        /// The HTTP error code.
        /// </summary>
        /// <value>The HTTP error code.</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public string Status { get; set; }

        /// <summary>
        /// The error code.
        /// </summary>
        /// <value>The error code.</value>
        [DataMember(Name="code", EmitDefaultValue=false)]
        public string Code { get; set; }

        /// <summary>
        /// The error message.
        /// </summary>
        /// <value>The error message.</value>
        [DataMember(Name="message", EmitDefaultValue=false)]
        public string Message { get; set; }

        /// <summary>
        /// The traceid for debugging purposes. 
        /// </summary>
        /// <value>The traceid for debugging purposes. </value>
        [DataMember(Name="traceId", EmitDefaultValue=false)]
        public string TraceId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ResponseStatus {\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  TraceId: ").Append(TraceId).Append("\n");
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
            return this.Equals(input as ResponseStatus);
        }

        /// <summary>
        /// Returns true if ResponseStatus instances are equal
        /// </summary>
        /// <param name="input">Instance of ResponseStatus to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ResponseStatus input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.Code == input.Code ||
                    (this.Code != null &&
                    this.Code.Equals(input.Code))
                ) && 
                (
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
                ) && 
                (
                    this.TraceId == input.TraceId ||
                    (this.TraceId != null &&
                    this.TraceId.Equals(input.TraceId))
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
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.Code != null)
                    hashCode = hashCode * 59 + this.Code.GetHashCode();
                if (this.Message != null)
                    hashCode = hashCode * 59 + this.Message.GetHashCode();
                if (this.TraceId != null)
                    hashCode = hashCode * 59 + this.TraceId.GetHashCode();
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
