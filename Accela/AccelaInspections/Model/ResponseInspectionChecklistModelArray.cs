/* 
 * Inspections
 *
 * Use the Inspections API to manage inspection records during their complete lifecycle from application submittal to permit issuance or license issuance. Your agency may need to complete inspections on new property developments, homes, or complaints.
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
using SwaggerDateConverter = AccelaInspections.Client.SwaggerDateConverter;

namespace AccelaInspections.Model
{
    /// <summary>
    /// ResponseInspectionChecklistModelArray
    /// </summary>
    [DataContract]
    public partial class ResponseInspectionChecklistModelArray :  IEquatable<ResponseInspectionChecklistModelArray>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseInspectionChecklistModelArray" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseInspectionChecklistModelArray(List<InspectionChecklistModel> result = default(List<InspectionChecklistModel>), int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }
        
        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name="result", EmitDefaultValue=false)]
        public List<InspectionChecklistModel> Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public int? Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ResponseInspectionChecklistModelArray {\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
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
            return this.Equals(input as ResponseInspectionChecklistModelArray);
        }

        /// <summary>
        /// Returns true if ResponseInspectionChecklistModelArray instances are equal
        /// </summary>
        /// <param name="input">Instance of ResponseInspectionChecklistModelArray to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ResponseInspectionChecklistModelArray input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Result == input.Result ||
                    this.Result != null &&
                    this.Result.SequenceEqual(input.Result)
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
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
                if (this.Result != null)
                    hashCode = hashCode * 59 + this.Result.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
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
