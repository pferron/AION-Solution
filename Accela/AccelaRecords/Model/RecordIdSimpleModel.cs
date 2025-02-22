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
    /// RecordIdSimpleModel
    /// </summary>
    [DataContract]
    public partial class RecordIdSimpleModel :  IEquatable<RecordIdSimpleModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordIdSimpleModel" /> class.
        /// </summary>
        /// <param name="customId">An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application..</param>
        /// <param name="id">The record system id assigned by the Civic Platform server..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="trackingId">The application tracking number (IVR tracking number)..</param>
        public RecordIdSimpleModel(string customId = default(string), string id = default(string), string serviceProviderCode = default(string), long? trackingId = default(long?))
        {
            this.CustomId = customId;
            this.Id = id;
            this.ServiceProviderCode = serviceProviderCode;
            this.TrackingId = trackingId;
        }
        
        /// <summary>
        /// An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.
        /// </summary>
        /// <value>An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.</value>
        [DataMember(Name="customId", EmitDefaultValue=false)]
        public string CustomId { get; set; }

        /// <summary>
        /// The record system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The record system id assigned by the Civic Platform server.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name="serviceProviderCode", EmitDefaultValue=false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The application tracking number (IVR tracking number).
        /// </summary>
        /// <value>The application tracking number (IVR tracking number).</value>
        [DataMember(Name="trackingId", EmitDefaultValue=false)]
        public long? TrackingId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordIdSimpleModel {\n");
            sb.Append("  CustomId: ").Append(CustomId).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ServiceProviderCode: ").Append(ServiceProviderCode).Append("\n");
            sb.Append("  TrackingId: ").Append(TrackingId).Append("\n");
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
            return this.Equals(input as RecordIdSimpleModel);
        }

        /// <summary>
        /// Returns true if RecordIdSimpleModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RecordIdSimpleModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecordIdSimpleModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CustomId == input.CustomId ||
                    (this.CustomId != null &&
                    this.CustomId.Equals(input.CustomId))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.ServiceProviderCode == input.ServiceProviderCode ||
                    (this.ServiceProviderCode != null &&
                    this.ServiceProviderCode.Equals(input.ServiceProviderCode))
                ) && 
                (
                    this.TrackingId == input.TrackingId ||
                    (this.TrackingId != null &&
                    this.TrackingId.Equals(input.TrackingId))
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
                if (this.CustomId != null)
                    hashCode = hashCode * 59 + this.CustomId.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.ServiceProviderCode != null)
                    hashCode = hashCode * 59 + this.ServiceProviderCode.GetHashCode();
                if (this.TrackingId != null)
                    hashCode = hashCode * 59 + this.TrackingId.GetHashCode();
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
