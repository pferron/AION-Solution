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
    /// RecordTypeAssetTypeModel
    /// </summary>
    [DataContract]
    public partial class RecordTypeAssetTypeModel :  IEquatable<RecordTypeAssetTypeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordTypeAssetTypeModel" /> class.
        /// </summary>
        /// <param name="gisIDForAssetID"> The name of the GIS ID field that is mapped to the asset ID..</param>
        /// <param name="gisLayer">gisLayer.</param>
        /// <param name="gisService"> The GIS service to be used with the asset. The GIS service should be a configured map service in Accela GIS Administration..</param>
        /// <param name="group">The name of the asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains..</param>
        /// <param name="type">The type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type..</param>
        public RecordTypeAssetTypeModel(string gisIDForAssetID = default(string), GISLayerIdModel gisLayer = default(GISLayerIdModel), string gisService = default(string), string group = default(string), string type = default(string))
        {
            this.GisIDForAssetID = gisIDForAssetID;
            this.GisLayer = gisLayer;
            this.GisService = gisService;
            this.Group = group;
            this.Type = type;
        }
        
        /// <summary>
        ///  The name of the GIS ID field that is mapped to the asset ID.
        /// </summary>
        /// <value> The name of the GIS ID field that is mapped to the asset ID.</value>
        [DataMember(Name="gisIDForAssetID", EmitDefaultValue=false)]
        public string GisIDForAssetID { get; set; }

        /// <summary>
        /// Gets or Sets GisLayer
        /// </summary>
        [DataMember(Name="gisLayer", EmitDefaultValue=false)]
        public GISLayerIdModel GisLayer { get; set; }

        /// <summary>
        ///  The GIS service to be used with the asset. The GIS service should be a configured map service in Accela GIS Administration.
        /// </summary>
        /// <value> The GIS service to be used with the asset. The GIS service should be a configured map service in Accela GIS Administration.</value>
        [DataMember(Name="gisService", EmitDefaultValue=false)]
        public string GisService { get; set; }

        /// <summary>
        /// The name of the asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains.
        /// </summary>
        /// <value>The name of the asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains.</value>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public string Group { get; set; }

        /// <summary>
        /// The type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type.
        /// </summary>
        /// <value>The type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type.</value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public string Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordTypeAssetTypeModel {\n");
            sb.Append("  GisIDForAssetID: ").Append(GisIDForAssetID).Append("\n");
            sb.Append("  GisLayer: ").Append(GisLayer).Append("\n");
            sb.Append("  GisService: ").Append(GisService).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
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
            return this.Equals(input as RecordTypeAssetTypeModel);
        }

        /// <summary>
        /// Returns true if RecordTypeAssetTypeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RecordTypeAssetTypeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecordTypeAssetTypeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.GisIDForAssetID == input.GisIDForAssetID ||
                    (this.GisIDForAssetID != null &&
                    this.GisIDForAssetID.Equals(input.GisIDForAssetID))
                ) && 
                (
                    this.GisLayer == input.GisLayer ||
                    (this.GisLayer != null &&
                    this.GisLayer.Equals(input.GisLayer))
                ) && 
                (
                    this.GisService == input.GisService ||
                    (this.GisService != null &&
                    this.GisService.Equals(input.GisService))
                ) && 
                (
                    this.Group == input.Group ||
                    (this.Group != null &&
                    this.Group.Equals(input.Group))
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
                if (this.GisIDForAssetID != null)
                    hashCode = hashCode * 59 + this.GisIDForAssetID.GetHashCode();
                if (this.GisLayer != null)
                    hashCode = hashCode * 59 + this.GisLayer.GetHashCode();
                if (this.GisService != null)
                    hashCode = hashCode * 59 + this.GisService.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
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