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
    /// ConditionHistoryModel
    /// </summary>
    [DataContract]
    public partial class ConditionHistoryModel :  IEquatable<ConditionHistoryModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionHistoryModel" /> class.
        /// </summary>
        /// <param name="actionbyDepartment">actionbyDepartment.</param>
        /// <param name="actionbyUser">actionbyUser.</param>
        /// <param name="activeStatus">activeStatus.</param>
        /// <param name="additionalInformation">additionalInformation.</param>
        /// <param name="additionalInformationPlainText">additionalInformationPlainText.</param>
        /// <param name="appliedDate">appliedDate.</param>
        /// <param name="appliedbyDepartment">appliedbyDepartment.</param>
        /// <param name="appliedbyUser">appliedbyUser.</param>
        /// <param name="dispAdditionalInformationPlainText">dispAdditionalInformationPlainText.</param>
        /// <param name="displayNoticeInAgency">displayNoticeInAgency.</param>
        /// <param name="displayNoticeInCitizens">displayNoticeInCitizens.</param>
        /// <param name="displayNoticeInCitizensFee">displayNoticeInCitizensFee.</param>
        /// <param name="effectiveDate">effectiveDate.</param>
        /// <param name="expirationDate">expirationDate.</param>
        /// <param name="group">group.</param>
        /// <param name="id">id.</param>
        /// <param name="inheritable">inheritable.</param>
        /// <param name="isIncludeNameInNotice">isIncludeNameInNotice.</param>
        /// <param name="isIncludeShortCommentsInNotice">isIncludeShortCommentsInNotice.</param>
        /// <param name="longComments">longComments.</param>
        /// <param name="manipulationDate">manipulationDate.</param>
        /// <param name="manipulationType">manipulationType.</param>
        /// <param name="name">name.</param>
        /// <param name="priority">priority.</param>
        /// <param name="publicDisplayMessage">publicDisplayMessage.</param>
        /// <param name="resAdditionalInformationPlainText">resAdditionalInformationPlainText.</param>
        /// <param name="resolutionAction">resolutionAction.</param>
        /// <param name="serviceProviderCode">serviceProviderCode.</param>
        /// <param name="severity">severity.</param>
        /// <param name="shortComments">shortComments.</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">statusDate.</param>
        /// <param name="statusType">statusType.</param>
        /// <param name="type">type.</param>
        public ConditionHistoryModel(ConditionHistoryModelActionbyDepartment actionbyDepartment = default(ConditionHistoryModelActionbyDepartment), ConditionHistoryModelActionbyDepartment actionbyUser = default(ConditionHistoryModelActionbyDepartment), ConditionHistoryModelActionbyDepartment activeStatus = default(ConditionHistoryModelActionbyDepartment), string additionalInformation = default(string), string additionalInformationPlainText = default(string), DateTime? appliedDate = default(DateTime?), ConditionHistoryModelActionbyDepartment appliedbyDepartment = default(ConditionHistoryModelActionbyDepartment), ConditionHistoryModelActionbyDepartment appliedbyUser = default(ConditionHistoryModelActionbyDepartment), string dispAdditionalInformationPlainText = default(string), bool? displayNoticeInAgency = default(bool?), bool? displayNoticeInCitizens = default(bool?), bool? displayNoticeInCitizensFee = default(bool?), DateTime? effectiveDate = default(DateTime?), DateTime? expirationDate = default(DateTime?), ConditionHistoryModelActionbyDepartment group = default(ConditionHistoryModelActionbyDepartment), long? id = default(long?), ConditionHistoryModelActionbyDepartment inheritable = default(ConditionHistoryModelActionbyDepartment), bool? isIncludeNameInNotice = default(bool?), bool? isIncludeShortCommentsInNotice = default(bool?), string longComments = default(string), DateTime? manipulationDate = default(DateTime?), string manipulationType = default(string), string name = default(string), ConditionHistoryModelActionbyDepartment priority = default(ConditionHistoryModelActionbyDepartment), string publicDisplayMessage = default(string), string resAdditionalInformationPlainText = default(string), string resolutionAction = default(string), string serviceProviderCode = default(string), ConditionHistoryModelActionbyDepartment severity = default(ConditionHistoryModelActionbyDepartment), string shortComments = default(string), ConditionHistoryModelActionbyDepartment status = default(ConditionHistoryModelActionbyDepartment), DateTime? statusDate = default(DateTime?), string statusType = default(string), ConditionHistoryModelActionbyDepartment type = default(ConditionHistoryModelActionbyDepartment))
        {
            this.ActionbyDepartment = actionbyDepartment;
            this.ActionbyUser = actionbyUser;
            this.ActiveStatus = activeStatus;
            this.AdditionalInformation = additionalInformation;
            this.AdditionalInformationPlainText = additionalInformationPlainText;
            this.AppliedDate = appliedDate;
            this.AppliedbyDepartment = appliedbyDepartment;
            this.AppliedbyUser = appliedbyUser;
            this.DispAdditionalInformationPlainText = dispAdditionalInformationPlainText;
            this.DisplayNoticeInAgency = displayNoticeInAgency;
            this.DisplayNoticeInCitizens = displayNoticeInCitizens;
            this.DisplayNoticeInCitizensFee = displayNoticeInCitizensFee;
            this.EffectiveDate = effectiveDate;
            this.ExpirationDate = expirationDate;
            this.Group = group;
            this.Id = id;
            this.Inheritable = inheritable;
            this.IsIncludeNameInNotice = isIncludeNameInNotice;
            this.IsIncludeShortCommentsInNotice = isIncludeShortCommentsInNotice;
            this.LongComments = longComments;
            this.ManipulationDate = manipulationDate;
            this.ManipulationType = manipulationType;
            this.Name = name;
            this.Priority = priority;
            this.PublicDisplayMessage = publicDisplayMessage;
            this.ResAdditionalInformationPlainText = resAdditionalInformationPlainText;
            this.ResolutionAction = resolutionAction;
            this.ServiceProviderCode = serviceProviderCode;
            this.Severity = severity;
            this.ShortComments = shortComments;
            this.Status = status;
            this.StatusDate = statusDate;
            this.StatusType = statusType;
            this.Type = type;
        }
        
        /// <summary>
        /// Gets or Sets ActionbyDepartment
        /// </summary>
        [DataMember(Name="actionbyDepartment", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment ActionbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets ActionbyUser
        /// </summary>
        [DataMember(Name="actionbyUser", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment ActionbyUser { get; set; }

        /// <summary>
        /// Gets or Sets ActiveStatus
        /// </summary>
        [DataMember(Name="activeStatus", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment ActiveStatus { get; set; }

        /// <summary>
        /// Gets or Sets AdditionalInformation
        /// </summary>
        [DataMember(Name="additionalInformation", EmitDefaultValue=false)]
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Gets or Sets AdditionalInformationPlainText
        /// </summary>
        [DataMember(Name="additionalInformationPlainText", EmitDefaultValue=false)]
        public string AdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets AppliedDate
        /// </summary>
        [DataMember(Name="appliedDate", EmitDefaultValue=false)]
        public DateTime? AppliedDate { get; set; }

        /// <summary>
        /// Gets or Sets AppliedbyDepartment
        /// </summary>
        [DataMember(Name="appliedbyDepartment", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment AppliedbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets AppliedbyUser
        /// </summary>
        [DataMember(Name="appliedbyUser", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment AppliedbyUser { get; set; }

        /// <summary>
        /// Gets or Sets DispAdditionalInformationPlainText
        /// </summary>
        [DataMember(Name="dispAdditionalInformationPlainText", EmitDefaultValue=false)]
        public string DispAdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInAgency
        /// </summary>
        [DataMember(Name="displayNoticeInAgency", EmitDefaultValue=false)]
        public bool? DisplayNoticeInAgency { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInCitizens
        /// </summary>
        [DataMember(Name="displayNoticeInCitizens", EmitDefaultValue=false)]
        public bool? DisplayNoticeInCitizens { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInCitizensFee
        /// </summary>
        [DataMember(Name="displayNoticeInCitizensFee", EmitDefaultValue=false)]
        public bool? DisplayNoticeInCitizensFee { get; set; }

        /// <summary>
        /// Gets or Sets EffectiveDate
        /// </summary>
        [DataMember(Name="effectiveDate", EmitDefaultValue=false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Gets or Sets ExpirationDate
        /// </summary>
        [DataMember(Name="expirationDate", EmitDefaultValue=false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment Group { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Inheritable
        /// </summary>
        [DataMember(Name="inheritable", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment Inheritable { get; set; }

        /// <summary>
        /// Gets or Sets IsIncludeNameInNotice
        /// </summary>
        [DataMember(Name="isIncludeNameInNotice", EmitDefaultValue=false)]
        public bool? IsIncludeNameInNotice { get; set; }

        /// <summary>
        /// Gets or Sets IsIncludeShortCommentsInNotice
        /// </summary>
        [DataMember(Name="isIncludeShortCommentsInNotice", EmitDefaultValue=false)]
        public bool? IsIncludeShortCommentsInNotice { get; set; }

        /// <summary>
        /// Gets or Sets LongComments
        /// </summary>
        [DataMember(Name="longComments", EmitDefaultValue=false)]
        public string LongComments { get; set; }

        /// <summary>
        /// Gets or Sets ManipulationDate
        /// </summary>
        [DataMember(Name="manipulationDate", EmitDefaultValue=false)]
        public DateTime? ManipulationDate { get; set; }

        /// <summary>
        /// Gets or Sets ManipulationType
        /// </summary>
        [DataMember(Name="manipulationType", EmitDefaultValue=false)]
        public string ManipulationType { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name="priority", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment Priority { get; set; }

        /// <summary>
        /// Gets or Sets PublicDisplayMessage
        /// </summary>
        [DataMember(Name="publicDisplayMessage", EmitDefaultValue=false)]
        public string PublicDisplayMessage { get; set; }

        /// <summary>
        /// Gets or Sets ResAdditionalInformationPlainText
        /// </summary>
        [DataMember(Name="resAdditionalInformationPlainText", EmitDefaultValue=false)]
        public string ResAdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets ResolutionAction
        /// </summary>
        [DataMember(Name="resolutionAction", EmitDefaultValue=false)]
        public string ResolutionAction { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderCode
        /// </summary>
        [DataMember(Name="serviceProviderCode", EmitDefaultValue=false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets Severity
        /// </summary>
        [DataMember(Name="severity", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment Severity { get; set; }

        /// <summary>
        /// Gets or Sets ShortComments
        /// </summary>
        [DataMember(Name="shortComments", EmitDefaultValue=false)]
        public string ShortComments { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment Status { get; set; }

        /// <summary>
        /// Gets or Sets StatusDate
        /// </summary>
        [DataMember(Name="statusDate", EmitDefaultValue=false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets StatusType
        /// </summary>
        [DataMember(Name="statusType", EmitDefaultValue=false)]
        public string StatusType { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public ConditionHistoryModelActionbyDepartment Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ConditionHistoryModel {\n");
            sb.Append("  ActionbyDepartment: ").Append(ActionbyDepartment).Append("\n");
            sb.Append("  ActionbyUser: ").Append(ActionbyUser).Append("\n");
            sb.Append("  ActiveStatus: ").Append(ActiveStatus).Append("\n");
            sb.Append("  AdditionalInformation: ").Append(AdditionalInformation).Append("\n");
            sb.Append("  AdditionalInformationPlainText: ").Append(AdditionalInformationPlainText).Append("\n");
            sb.Append("  AppliedDate: ").Append(AppliedDate).Append("\n");
            sb.Append("  AppliedbyDepartment: ").Append(AppliedbyDepartment).Append("\n");
            sb.Append("  AppliedbyUser: ").Append(AppliedbyUser).Append("\n");
            sb.Append("  DispAdditionalInformationPlainText: ").Append(DispAdditionalInformationPlainText).Append("\n");
            sb.Append("  DisplayNoticeInAgency: ").Append(DisplayNoticeInAgency).Append("\n");
            sb.Append("  DisplayNoticeInCitizens: ").Append(DisplayNoticeInCitizens).Append("\n");
            sb.Append("  DisplayNoticeInCitizensFee: ").Append(DisplayNoticeInCitizensFee).Append("\n");
            sb.Append("  EffectiveDate: ").Append(EffectiveDate).Append("\n");
            sb.Append("  ExpirationDate: ").Append(ExpirationDate).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Inheritable: ").Append(Inheritable).Append("\n");
            sb.Append("  IsIncludeNameInNotice: ").Append(IsIncludeNameInNotice).Append("\n");
            sb.Append("  IsIncludeShortCommentsInNotice: ").Append(IsIncludeShortCommentsInNotice).Append("\n");
            sb.Append("  LongComments: ").Append(LongComments).Append("\n");
            sb.Append("  ManipulationDate: ").Append(ManipulationDate).Append("\n");
            sb.Append("  ManipulationType: ").Append(ManipulationType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Priority: ").Append(Priority).Append("\n");
            sb.Append("  PublicDisplayMessage: ").Append(PublicDisplayMessage).Append("\n");
            sb.Append("  ResAdditionalInformationPlainText: ").Append(ResAdditionalInformationPlainText).Append("\n");
            sb.Append("  ResolutionAction: ").Append(ResolutionAction).Append("\n");
            sb.Append("  ServiceProviderCode: ").Append(ServiceProviderCode).Append("\n");
            sb.Append("  Severity: ").Append(Severity).Append("\n");
            sb.Append("  ShortComments: ").Append(ShortComments).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StatusDate: ").Append(StatusDate).Append("\n");
            sb.Append("  StatusType: ").Append(StatusType).Append("\n");
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
            return this.Equals(input as ConditionHistoryModel);
        }

        /// <summary>
        /// Returns true if ConditionHistoryModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ConditionHistoryModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ConditionHistoryModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ActionbyDepartment == input.ActionbyDepartment ||
                    (this.ActionbyDepartment != null &&
                    this.ActionbyDepartment.Equals(input.ActionbyDepartment))
                ) && 
                (
                    this.ActionbyUser == input.ActionbyUser ||
                    (this.ActionbyUser != null &&
                    this.ActionbyUser.Equals(input.ActionbyUser))
                ) && 
                (
                    this.ActiveStatus == input.ActiveStatus ||
                    (this.ActiveStatus != null &&
                    this.ActiveStatus.Equals(input.ActiveStatus))
                ) && 
                (
                    this.AdditionalInformation == input.AdditionalInformation ||
                    (this.AdditionalInformation != null &&
                    this.AdditionalInformation.Equals(input.AdditionalInformation))
                ) && 
                (
                    this.AdditionalInformationPlainText == input.AdditionalInformationPlainText ||
                    (this.AdditionalInformationPlainText != null &&
                    this.AdditionalInformationPlainText.Equals(input.AdditionalInformationPlainText))
                ) && 
                (
                    this.AppliedDate == input.AppliedDate ||
                    (this.AppliedDate != null &&
                    this.AppliedDate.Equals(input.AppliedDate))
                ) && 
                (
                    this.AppliedbyDepartment == input.AppliedbyDepartment ||
                    (this.AppliedbyDepartment != null &&
                    this.AppliedbyDepartment.Equals(input.AppliedbyDepartment))
                ) && 
                (
                    this.AppliedbyUser == input.AppliedbyUser ||
                    (this.AppliedbyUser != null &&
                    this.AppliedbyUser.Equals(input.AppliedbyUser))
                ) && 
                (
                    this.DispAdditionalInformationPlainText == input.DispAdditionalInformationPlainText ||
                    (this.DispAdditionalInformationPlainText != null &&
                    this.DispAdditionalInformationPlainText.Equals(input.DispAdditionalInformationPlainText))
                ) && 
                (
                    this.DisplayNoticeInAgency == input.DisplayNoticeInAgency ||
                    (this.DisplayNoticeInAgency != null &&
                    this.DisplayNoticeInAgency.Equals(input.DisplayNoticeInAgency))
                ) && 
                (
                    this.DisplayNoticeInCitizens == input.DisplayNoticeInCitizens ||
                    (this.DisplayNoticeInCitizens != null &&
                    this.DisplayNoticeInCitizens.Equals(input.DisplayNoticeInCitizens))
                ) && 
                (
                    this.DisplayNoticeInCitizensFee == input.DisplayNoticeInCitizensFee ||
                    (this.DisplayNoticeInCitizensFee != null &&
                    this.DisplayNoticeInCitizensFee.Equals(input.DisplayNoticeInCitizensFee))
                ) && 
                (
                    this.EffectiveDate == input.EffectiveDate ||
                    (this.EffectiveDate != null &&
                    this.EffectiveDate.Equals(input.EffectiveDate))
                ) && 
                (
                    this.ExpirationDate == input.ExpirationDate ||
                    (this.ExpirationDate != null &&
                    this.ExpirationDate.Equals(input.ExpirationDate))
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
                    this.Inheritable == input.Inheritable ||
                    (this.Inheritable != null &&
                    this.Inheritable.Equals(input.Inheritable))
                ) && 
                (
                    this.IsIncludeNameInNotice == input.IsIncludeNameInNotice ||
                    (this.IsIncludeNameInNotice != null &&
                    this.IsIncludeNameInNotice.Equals(input.IsIncludeNameInNotice))
                ) && 
                (
                    this.IsIncludeShortCommentsInNotice == input.IsIncludeShortCommentsInNotice ||
                    (this.IsIncludeShortCommentsInNotice != null &&
                    this.IsIncludeShortCommentsInNotice.Equals(input.IsIncludeShortCommentsInNotice))
                ) && 
                (
                    this.LongComments == input.LongComments ||
                    (this.LongComments != null &&
                    this.LongComments.Equals(input.LongComments))
                ) && 
                (
                    this.ManipulationDate == input.ManipulationDate ||
                    (this.ManipulationDate != null &&
                    this.ManipulationDate.Equals(input.ManipulationDate))
                ) && 
                (
                    this.ManipulationType == input.ManipulationType ||
                    (this.ManipulationType != null &&
                    this.ManipulationType.Equals(input.ManipulationType))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Priority == input.Priority ||
                    (this.Priority != null &&
                    this.Priority.Equals(input.Priority))
                ) && 
                (
                    this.PublicDisplayMessage == input.PublicDisplayMessage ||
                    (this.PublicDisplayMessage != null &&
                    this.PublicDisplayMessage.Equals(input.PublicDisplayMessage))
                ) && 
                (
                    this.ResAdditionalInformationPlainText == input.ResAdditionalInformationPlainText ||
                    (this.ResAdditionalInformationPlainText != null &&
                    this.ResAdditionalInformationPlainText.Equals(input.ResAdditionalInformationPlainText))
                ) && 
                (
                    this.ResolutionAction == input.ResolutionAction ||
                    (this.ResolutionAction != null &&
                    this.ResolutionAction.Equals(input.ResolutionAction))
                ) && 
                (
                    this.ServiceProviderCode == input.ServiceProviderCode ||
                    (this.ServiceProviderCode != null &&
                    this.ServiceProviderCode.Equals(input.ServiceProviderCode))
                ) && 
                (
                    this.Severity == input.Severity ||
                    (this.Severity != null &&
                    this.Severity.Equals(input.Severity))
                ) && 
                (
                    this.ShortComments == input.ShortComments ||
                    (this.ShortComments != null &&
                    this.ShortComments.Equals(input.ShortComments))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.StatusDate == input.StatusDate ||
                    (this.StatusDate != null &&
                    this.StatusDate.Equals(input.StatusDate))
                ) && 
                (
                    this.StatusType == input.StatusType ||
                    (this.StatusType != null &&
                    this.StatusType.Equals(input.StatusType))
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
                if (this.ActionbyDepartment != null)
                    hashCode = hashCode * 59 + this.ActionbyDepartment.GetHashCode();
                if (this.ActionbyUser != null)
                    hashCode = hashCode * 59 + this.ActionbyUser.GetHashCode();
                if (this.ActiveStatus != null)
                    hashCode = hashCode * 59 + this.ActiveStatus.GetHashCode();
                if (this.AdditionalInformation != null)
                    hashCode = hashCode * 59 + this.AdditionalInformation.GetHashCode();
                if (this.AdditionalInformationPlainText != null)
                    hashCode = hashCode * 59 + this.AdditionalInformationPlainText.GetHashCode();
                if (this.AppliedDate != null)
                    hashCode = hashCode * 59 + this.AppliedDate.GetHashCode();
                if (this.AppliedbyDepartment != null)
                    hashCode = hashCode * 59 + this.AppliedbyDepartment.GetHashCode();
                if (this.AppliedbyUser != null)
                    hashCode = hashCode * 59 + this.AppliedbyUser.GetHashCode();
                if (this.DispAdditionalInformationPlainText != null)
                    hashCode = hashCode * 59 + this.DispAdditionalInformationPlainText.GetHashCode();
                if (this.DisplayNoticeInAgency != null)
                    hashCode = hashCode * 59 + this.DisplayNoticeInAgency.GetHashCode();
                if (this.DisplayNoticeInCitizens != null)
                    hashCode = hashCode * 59 + this.DisplayNoticeInCitizens.GetHashCode();
                if (this.DisplayNoticeInCitizensFee != null)
                    hashCode = hashCode * 59 + this.DisplayNoticeInCitizensFee.GetHashCode();
                if (this.EffectiveDate != null)
                    hashCode = hashCode * 59 + this.EffectiveDate.GetHashCode();
                if (this.ExpirationDate != null)
                    hashCode = hashCode * 59 + this.ExpirationDate.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Inheritable != null)
                    hashCode = hashCode * 59 + this.Inheritable.GetHashCode();
                if (this.IsIncludeNameInNotice != null)
                    hashCode = hashCode * 59 + this.IsIncludeNameInNotice.GetHashCode();
                if (this.IsIncludeShortCommentsInNotice != null)
                    hashCode = hashCode * 59 + this.IsIncludeShortCommentsInNotice.GetHashCode();
                if (this.LongComments != null)
                    hashCode = hashCode * 59 + this.LongComments.GetHashCode();
                if (this.ManipulationDate != null)
                    hashCode = hashCode * 59 + this.ManipulationDate.GetHashCode();
                if (this.ManipulationType != null)
                    hashCode = hashCode * 59 + this.ManipulationType.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Priority != null)
                    hashCode = hashCode * 59 + this.Priority.GetHashCode();
                if (this.PublicDisplayMessage != null)
                    hashCode = hashCode * 59 + this.PublicDisplayMessage.GetHashCode();
                if (this.ResAdditionalInformationPlainText != null)
                    hashCode = hashCode * 59 + this.ResAdditionalInformationPlainText.GetHashCode();
                if (this.ResolutionAction != null)
                    hashCode = hashCode * 59 + this.ResolutionAction.GetHashCode();
                if (this.ServiceProviderCode != null)
                    hashCode = hashCode * 59 + this.ServiceProviderCode.GetHashCode();
                if (this.Severity != null)
                    hashCode = hashCode * 59 + this.Severity.GetHashCode();
                if (this.ShortComments != null)
                    hashCode = hashCode * 59 + this.ShortComments.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.StatusDate != null)
                    hashCode = hashCode * 59 + this.StatusDate.GetHashCode();
                if (this.StatusType != null)
                    hashCode = hashCode * 59 + this.StatusType.GetHashCode();
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