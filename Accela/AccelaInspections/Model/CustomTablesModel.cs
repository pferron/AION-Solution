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
    /// CustomTablesModel
    /// </summary>
    [DataContract]
    public partial class CustomTablesModel :  IEquatable<CustomTablesModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTablesModel" /> class.
        /// </summary>
        /// <param name="code">code.</param>
        /// <param name="message">message.</param>
        /// <param name="more">more.</param>
        /// <param name="page">page.</param>
        /// <param name="resultStatus">resultStatus.</param>
        /// <param name="result">result.</param>
        /// <param name="status">status.</param>
        /// <param name="traceId">traceId.</param>
        public CustomTablesModel(string code = default(string), string message = default(string), string more = default(string), PageModel page = default(PageModel), CustomTablesStatusModel resultStatus = default(CustomTablesStatusModel), string result = default(string), long? status = default(long?), string traceId = default(string))
        {
            this.Code = code;
            this.Message = message;
            this.More = more;
            this.Page = page;
            this.ResultStatus = resultStatus;
            this.Result = result;
            this.Status = status;
            this.TraceId = traceId;
        }
        
        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name="code", EmitDefaultValue=false)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name="message", EmitDefaultValue=false)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets More
        /// </summary>
        [DataMember(Name="more", EmitDefaultValue=false)]
        public string More { get; set; }

        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [DataMember(Name="page", EmitDefaultValue=false)]
        public PageModel Page { get; set; }

        /// <summary>
        /// Gets or Sets ResultStatus
        /// </summary>
        [DataMember(Name="resultStatus", EmitDefaultValue=false)]
        public CustomTablesStatusModel ResultStatus { get; set; }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name="result", EmitDefaultValue=false)]
        public string Result { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public long? Status { get; set; }

        /// <summary>
        /// Gets or Sets TraceId
        /// </summary>
        [DataMember(Name="traceId", EmitDefaultValue=false)]
        public string TraceId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomTablesModel {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  More: ").Append(More).Append("\n");
            sb.Append("  Page: ").Append(Page).Append("\n");
            sb.Append("  ResultStatus: ").Append(ResultStatus).Append("\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
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
            return this.Equals(input as CustomTablesModel);
        }

        /// <summary>
        /// Returns true if CustomTablesModel instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomTablesModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomTablesModel input)
        {
            if (input == null)
                return false;

            return 
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
                    this.More == input.More ||
                    (this.More != null &&
                    this.More.Equals(input.More))
                ) && 
                (
                    this.Page == input.Page ||
                    (this.Page != null &&
                    this.Page.Equals(input.Page))
                ) && 
                (
                    this.ResultStatus == input.ResultStatus ||
                    (this.ResultStatus != null &&
                    this.ResultStatus.Equals(input.ResultStatus))
                ) && 
                (
                    this.Result == input.Result ||
                    (this.Result != null &&
                    this.Result.Equals(input.Result))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
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
                if (this.Code != null)
                    hashCode = hashCode * 59 + this.Code.GetHashCode();
                if (this.Message != null)
                    hashCode = hashCode * 59 + this.Message.GetHashCode();
                if (this.More != null)
                    hashCode = hashCode * 59 + this.More.GetHashCode();
                if (this.Page != null)
                    hashCode = hashCode * 59 + this.Page.GetHashCode();
                if (this.ResultStatus != null)
                    hashCode = hashCode * 59 + this.ResultStatus.GetHashCode();
                if (this.Result != null)
                    hashCode = hashCode * 59 + this.Result.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
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