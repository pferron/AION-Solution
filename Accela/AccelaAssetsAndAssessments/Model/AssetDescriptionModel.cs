/* 
 * Assets and Assessments
 *
 * The Assets and Assessments APIs enable apps to manage assets and their related condition assessments.
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
using SwaggerDateConverter = AccelaAssetsAndAssessments.Client.SwaggerDateConverter;

namespace AccelaAssetsAndAssessments.Model
{
    /// <summary>
    /// AssetDescriptionModel
    /// </summary>
    [DataContract]
        public partial class AssetDescriptionModel :  IEquatable<AssetDescriptionModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetDescriptionModel" /> class.
        /// </summary>
        /// <param name="description">The asset description..</param>
        /// <param name="group">The group the asset belongs to..</param>
        /// <param name="id">The asset system id assigned by the Civic Platform server..</param>
        /// <param name="status">status.</param>
        /// <param name="type">The asset type..</param>
        public AssetDescriptionModel(string description = default(string), string group = default(string), string id = default(string), AssetDescriptionModelStatus status = default(AssetDescriptionModelStatus), string type = default(string))
        {
            this.Description = description;
            this.Group = group;
            this.Id = id;
            this.Status = status;
            this.Type = type;
        }
        
        /// <summary>
        /// The asset description.
        /// </summary>
        /// <value>The asset description.</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// The group the asset belongs to.
        /// </summary>
        /// <value>The group the asset belongs to.</value>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public string Group { get; set; }

        /// <summary>
        /// The asset system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The asset system id assigned by the Civic Platform server.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public AssetDescriptionModelStatus Status { get; set; }

        /// <summary>
        /// The asset type.
        /// </summary>
        /// <value>The asset type.</value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public string Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AssetDescriptionModel {\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return this.Equals(input as AssetDescriptionModel);
        }

        /// <summary>
        /// Returns true if AssetDescriptionModel instances are equal
        /// </summary>
        /// <param name="input">Instance of AssetDescriptionModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AssetDescriptionModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Group == input.Group ||
                    (this.Group != null &&
                    this.Group.Equals(input.Group))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
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
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
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
