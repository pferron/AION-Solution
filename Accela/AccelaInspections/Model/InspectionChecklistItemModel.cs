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
    /// InspectionChecklistItemModel
    /// </summary>
    [DataContract]
    public partial class InspectionChecklistItemModel :  IEquatable<InspectionChecklistItemModel>, IValidatableObject
    {
        /// <summary>
        /// Indicates whether or not the checklist item is critical.
        /// </summary>
        /// <value>Indicates whether or not the checklist item is critical.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsCriticalEnum
        {
            
            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")]
            Y = 1,
            
            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")]
            N = 2
        }

        /// <summary>
        /// Indicates whether or not the checklist item is critical.
        /// </summary>
        /// <value>Indicates whether or not the checklist item is critical.</value>
        [DataMember(Name="isCritical", EmitDefaultValue=false)]
        public IsCriticalEnum? IsCritical { get; set; }
        /// <summary>
        /// Indicates whether or not the checklist item is a major violation.
        /// </summary>
        /// <value>Indicates whether or not the checklist item is a major violation.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsMajorViolationEnum
        {
            
            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")]
            Y = 1,
            
            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")]
            N = 2
        }

        /// <summary>
        /// Indicates whether or not the checklist item is a major violation.
        /// </summary>
        /// <value>Indicates whether or not the checklist item is a major violation.</value>
        [DataMember(Name="isMajorViolation", EmitDefaultValue=false)]
        public IsMajorViolationEnum? IsMajorViolation { get; set; }
        /// <summary>
        /// Indicates whether or not the field is required.
        /// </summary>
        /// <value>Indicates whether or not the field is required.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsRequiredEnum
        {
            
            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")]
            Y = 1,
            
            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")]
            N = 2
        }

        /// <summary>
        /// Indicates whether or not the field is required.
        /// </summary>
        /// <value>Indicates whether or not the field is required.</value>
        [DataMember(Name="isRequired", EmitDefaultValue=false)]
        public IsRequiredEnum? IsRequired { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="InspectionChecklistItemModel" /> class.
        /// </summary>
        /// <param name="checklist">The name of a checklist..</param>
        /// <param name="checklistId">The unique ID associated with the checklist..</param>
        /// <param name="checklistItem">checklistItem.</param>
        /// <param name="comment">comment.</param>
        /// <param name="customId">The checklist item &#39;s custom ID..</param>
        /// <param name="displayOrder">The item&#39; s display order..</param>
        /// <param name="id">The checklist item system id assigned by the Civic Platform server..</param>
        /// <param name="isCritical">Indicates whether or not the checklist item is critical..</param>
        /// <param name="isMajorViolation">Indicates whether or not the checklist item is a major violation..</param>
        /// <param name="isRequired">Indicates whether or not the field is required..</param>
        /// <param name="score">The inspection score for a checklist or checklist item..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="status">status.</param>
        public InspectionChecklistItemModel(string checklist = default(string), long? checklistId = default(long?), InspectionChecklistItemModelChecklistItem checklistItem = default(InspectionChecklistItemModelChecklistItem), InspectionChecklistItemModelComment comment = default(InspectionChecklistItemModelComment), string customId = default(string), long? displayOrder = default(long?), long? id = default(long?), IsCriticalEnum? isCritical = default(IsCriticalEnum?), IsMajorViolationEnum? isMajorViolation = default(IsMajorViolationEnum?), IsRequiredEnum? isRequired = default(IsRequiredEnum?), long? score = default(long?), string serviceProviderCode = default(string), InspectionChecklistItemModelStatus status = default(InspectionChecklistItemModelStatus))
        {
            this.Checklist = checklist;
            this.ChecklistId = checklistId;
            this.ChecklistItem = checklistItem;
            this.Comment = comment;
            this.CustomId = customId;
            this.DisplayOrder = displayOrder;
            this.Id = id;
            this.IsCritical = isCritical;
            this.IsMajorViolation = isMajorViolation;
            this.IsRequired = isRequired;
            this.Score = score;
            this.ServiceProviderCode = serviceProviderCode;
            this.Status = status;
        }
        
        /// <summary>
        /// The name of a checklist.
        /// </summary>
        /// <value>The name of a checklist.</value>
        [DataMember(Name="checklist", EmitDefaultValue=false)]
        public string Checklist { get; set; }

        /// <summary>
        /// The unique ID associated with the checklist.
        /// </summary>
        /// <value>The unique ID associated with the checklist.</value>
        [DataMember(Name="checklistId", EmitDefaultValue=false)]
        public long? ChecklistId { get; set; }

        /// <summary>
        /// Gets or Sets ChecklistItem
        /// </summary>
        [DataMember(Name="checklistItem", EmitDefaultValue=false)]
        public InspectionChecklistItemModelChecklistItem ChecklistItem { get; set; }

        /// <summary>
        /// Gets or Sets Comment
        /// </summary>
        [DataMember(Name="comment", EmitDefaultValue=false)]
        public InspectionChecklistItemModelComment Comment { get; set; }

        /// <summary>
        /// The checklist item &#39;s custom ID.
        /// </summary>
        /// <value>The checklist item &#39;s custom ID.</value>
        [DataMember(Name="customId", EmitDefaultValue=false)]
        public string CustomId { get; set; }

        /// <summary>
        /// The item&#39; s display order.
        /// </summary>
        /// <value>The item&#39; s display order.</value>
        [DataMember(Name="displayOrder", EmitDefaultValue=false)]
        public long? DisplayOrder { get; set; }

        /// <summary>
        /// The checklist item system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The checklist item system id assigned by the Civic Platform server.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }




        /// <summary>
        /// The inspection score for a checklist or checklist item.
        /// </summary>
        /// <value>The inspection score for a checklist or checklist item.</value>
        [DataMember(Name="score", EmitDefaultValue=false)]
        public long? Score { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name="serviceProviderCode", EmitDefaultValue=false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public InspectionChecklistItemModelStatus Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InspectionChecklistItemModel {\n");
            sb.Append("  Checklist: ").Append(Checklist).Append("\n");
            sb.Append("  ChecklistId: ").Append(ChecklistId).Append("\n");
            sb.Append("  ChecklistItem: ").Append(ChecklistItem).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  CustomId: ").Append(CustomId).Append("\n");
            sb.Append("  DisplayOrder: ").Append(DisplayOrder).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IsCritical: ").Append(IsCritical).Append("\n");
            sb.Append("  IsMajorViolation: ").Append(IsMajorViolation).Append("\n");
            sb.Append("  IsRequired: ").Append(IsRequired).Append("\n");
            sb.Append("  Score: ").Append(Score).Append("\n");
            sb.Append("  ServiceProviderCode: ").Append(ServiceProviderCode).Append("\n");
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
            return this.Equals(input as InspectionChecklistItemModel);
        }

        /// <summary>
        /// Returns true if InspectionChecklistItemModel instances are equal
        /// </summary>
        /// <param name="input">Instance of InspectionChecklistItemModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InspectionChecklistItemModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Checklist == input.Checklist ||
                    (this.Checklist != null &&
                    this.Checklist.Equals(input.Checklist))
                ) && 
                (
                    this.ChecklistId == input.ChecklistId ||
                    (this.ChecklistId != null &&
                    this.ChecklistId.Equals(input.ChecklistId))
                ) && 
                (
                    this.ChecklistItem == input.ChecklistItem ||
                    (this.ChecklistItem != null &&
                    this.ChecklistItem.Equals(input.ChecklistItem))
                ) && 
                (
                    this.Comment == input.Comment ||
                    (this.Comment != null &&
                    this.Comment.Equals(input.Comment))
                ) && 
                (
                    this.CustomId == input.CustomId ||
                    (this.CustomId != null &&
                    this.CustomId.Equals(input.CustomId))
                ) && 
                (
                    this.DisplayOrder == input.DisplayOrder ||
                    (this.DisplayOrder != null &&
                    this.DisplayOrder.Equals(input.DisplayOrder))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.IsCritical == input.IsCritical ||
                    (this.IsCritical != null &&
                    this.IsCritical.Equals(input.IsCritical))
                ) && 
                (
                    this.IsMajorViolation == input.IsMajorViolation ||
                    (this.IsMajorViolation != null &&
                    this.IsMajorViolation.Equals(input.IsMajorViolation))
                ) && 
                (
                    this.IsRequired == input.IsRequired ||
                    (this.IsRequired != null &&
                    this.IsRequired.Equals(input.IsRequired))
                ) && 
                (
                    this.Score == input.Score ||
                    (this.Score != null &&
                    this.Score.Equals(input.Score))
                ) && 
                (
                    this.ServiceProviderCode == input.ServiceProviderCode ||
                    (this.ServiceProviderCode != null &&
                    this.ServiceProviderCode.Equals(input.ServiceProviderCode))
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
                if (this.Checklist != null)
                    hashCode = hashCode * 59 + this.Checklist.GetHashCode();
                if (this.ChecklistId != null)
                    hashCode = hashCode * 59 + this.ChecklistId.GetHashCode();
                if (this.ChecklistItem != null)
                    hashCode = hashCode * 59 + this.ChecklistItem.GetHashCode();
                if (this.Comment != null)
                    hashCode = hashCode * 59 + this.Comment.GetHashCode();
                if (this.CustomId != null)
                    hashCode = hashCode * 59 + this.CustomId.GetHashCode();
                if (this.DisplayOrder != null)
                    hashCode = hashCode * 59 + this.DisplayOrder.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.IsCritical != null)
                    hashCode = hashCode * 59 + this.IsCritical.GetHashCode();
                if (this.IsMajorViolation != null)
                    hashCode = hashCode * 59 + this.IsMajorViolation.GetHashCode();
                if (this.IsRequired != null)
                    hashCode = hashCode * 59 + this.IsRequired.GetHashCode();
                if (this.Score != null)
                    hashCode = hashCode * 59 + this.Score.GetHashCode();
                if (this.ServiceProviderCode != null)
                    hashCode = hashCode * 59 + this.ServiceProviderCode.GetHashCode();
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