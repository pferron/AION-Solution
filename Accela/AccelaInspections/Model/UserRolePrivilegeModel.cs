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
    /// UserRolePrivilegeModel
    /// </summary>
    [DataContract]
    public partial class UserRolePrivilegeModel :  IEquatable<UserRolePrivilegeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRolePrivilegeModel" /> class.
        /// </summary>
        /// <param name="contactAllowed">Indicates whether or not the permission is given to a contact..</param>
        /// <param name="deleteAllowed">Indicates whether or not the permission to delete a document is allowed. .</param>
        /// <param name="downloadAllowed">Indicates whether or not the permission to download a document is allowed..</param>
        /// <param name="licenseTypeRules">licenseTypeRules.</param>
        /// <param name="licensendProfessionalAllowed">Indicates whether or not the permission is given to a licensed professional..</param>
        /// <param name="ownerAllowed">Indicates whether or not the permission is given to an owner..</param>
        /// <param name="recordCreatorAllowed">Indicates whether or not the permission is given to a record creator..</param>
        /// <param name="registeredUserAllowed">Indicates whether or not the permission is given to a registered public user..</param>
        /// <param name="titleViewAllowed">Indicates whether or not the permission to view a document name is allowed..</param>
        /// <param name="uploadAllowed">Indicates whether or not the permission to upload a document is allowed..</param>
        public UserRolePrivilegeModel(bool? contactAllowed = default(bool?), bool? deleteAllowed = default(bool?), bool? downloadAllowed = default(bool?), List<string> licenseTypeRules = default(List<string>), bool? licensendProfessionalAllowed = default(bool?), bool? ownerAllowed = default(bool?), bool? recordCreatorAllowed = default(bool?), bool? registeredUserAllowed = default(bool?), bool? titleViewAllowed = default(bool?), bool? uploadAllowed = default(bool?))
        {
            this.ContactAllowed = contactAllowed;
            this.DeleteAllowed = deleteAllowed;
            this.DownloadAllowed = downloadAllowed;
            this.LicenseTypeRules = licenseTypeRules;
            this.LicensendProfessionalAllowed = licensendProfessionalAllowed;
            this.OwnerAllowed = ownerAllowed;
            this.RecordCreatorAllowed = recordCreatorAllowed;
            this.RegisteredUserAllowed = registeredUserAllowed;
            this.TitleViewAllowed = titleViewAllowed;
            this.UploadAllowed = uploadAllowed;
        }
        
        /// <summary>
        /// Indicates whether or not the permission is given to a contact.
        /// </summary>
        /// <value>Indicates whether or not the permission is given to a contact.</value>
        [DataMember(Name="contactAllowed", EmitDefaultValue=false)]
        public bool? ContactAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission to delete a document is allowed. 
        /// </summary>
        /// <value>Indicates whether or not the permission to delete a document is allowed. </value>
        [DataMember(Name="deleteAllowed", EmitDefaultValue=false)]
        public bool? DeleteAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission to download a document is allowed.
        /// </summary>
        /// <value>Indicates whether or not the permission to download a document is allowed.</value>
        [DataMember(Name="downloadAllowed", EmitDefaultValue=false)]
        public bool? DownloadAllowed { get; set; }

        /// <summary>
        /// Gets or Sets LicenseTypeRules
        /// </summary>
        [DataMember(Name="licenseTypeRules", EmitDefaultValue=false)]
        public List<string> LicenseTypeRules { get; set; }

        /// <summary>
        /// Indicates whether or not the permission is given to a licensed professional.
        /// </summary>
        /// <value>Indicates whether or not the permission is given to a licensed professional.</value>
        [DataMember(Name="licensendProfessionalAllowed", EmitDefaultValue=false)]
        public bool? LicensendProfessionalAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission is given to an owner.
        /// </summary>
        /// <value>Indicates whether or not the permission is given to an owner.</value>
        [DataMember(Name="ownerAllowed", EmitDefaultValue=false)]
        public bool? OwnerAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission is given to a record creator.
        /// </summary>
        /// <value>Indicates whether or not the permission is given to a record creator.</value>
        [DataMember(Name="recordCreatorAllowed", EmitDefaultValue=false)]
        public bool? RecordCreatorAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission is given to a registered public user.
        /// </summary>
        /// <value>Indicates whether or not the permission is given to a registered public user.</value>
        [DataMember(Name="registeredUserAllowed", EmitDefaultValue=false)]
        public bool? RegisteredUserAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission to view a document name is allowed.
        /// </summary>
        /// <value>Indicates whether or not the permission to view a document name is allowed.</value>
        [DataMember(Name="titleViewAllowed", EmitDefaultValue=false)]
        public bool? TitleViewAllowed { get; set; }

        /// <summary>
        /// Indicates whether or not the permission to upload a document is allowed.
        /// </summary>
        /// <value>Indicates whether or not the permission to upload a document is allowed.</value>
        [DataMember(Name="uploadAllowed", EmitDefaultValue=false)]
        public bool? UploadAllowed { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserRolePrivilegeModel {\n");
            sb.Append("  ContactAllowed: ").Append(ContactAllowed).Append("\n");
            sb.Append("  DeleteAllowed: ").Append(DeleteAllowed).Append("\n");
            sb.Append("  DownloadAllowed: ").Append(DownloadAllowed).Append("\n");
            sb.Append("  LicenseTypeRules: ").Append(LicenseTypeRules).Append("\n");
            sb.Append("  LicensendProfessionalAllowed: ").Append(LicensendProfessionalAllowed).Append("\n");
            sb.Append("  OwnerAllowed: ").Append(OwnerAllowed).Append("\n");
            sb.Append("  RecordCreatorAllowed: ").Append(RecordCreatorAllowed).Append("\n");
            sb.Append("  RegisteredUserAllowed: ").Append(RegisteredUserAllowed).Append("\n");
            sb.Append("  TitleViewAllowed: ").Append(TitleViewAllowed).Append("\n");
            sb.Append("  UploadAllowed: ").Append(UploadAllowed).Append("\n");
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
            return this.Equals(input as UserRolePrivilegeModel);
        }

        /// <summary>
        /// Returns true if UserRolePrivilegeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of UserRolePrivilegeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UserRolePrivilegeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ContactAllowed == input.ContactAllowed ||
                    (this.ContactAllowed != null &&
                    this.ContactAllowed.Equals(input.ContactAllowed))
                ) && 
                (
                    this.DeleteAllowed == input.DeleteAllowed ||
                    (this.DeleteAllowed != null &&
                    this.DeleteAllowed.Equals(input.DeleteAllowed))
                ) && 
                (
                    this.DownloadAllowed == input.DownloadAllowed ||
                    (this.DownloadAllowed != null &&
                    this.DownloadAllowed.Equals(input.DownloadAllowed))
                ) && 
                (
                    this.LicenseTypeRules == input.LicenseTypeRules ||
                    this.LicenseTypeRules != null &&
                    this.LicenseTypeRules.SequenceEqual(input.LicenseTypeRules)
                ) && 
                (
                    this.LicensendProfessionalAllowed == input.LicensendProfessionalAllowed ||
                    (this.LicensendProfessionalAllowed != null &&
                    this.LicensendProfessionalAllowed.Equals(input.LicensendProfessionalAllowed))
                ) && 
                (
                    this.OwnerAllowed == input.OwnerAllowed ||
                    (this.OwnerAllowed != null &&
                    this.OwnerAllowed.Equals(input.OwnerAllowed))
                ) && 
                (
                    this.RecordCreatorAllowed == input.RecordCreatorAllowed ||
                    (this.RecordCreatorAllowed != null &&
                    this.RecordCreatorAllowed.Equals(input.RecordCreatorAllowed))
                ) && 
                (
                    this.RegisteredUserAllowed == input.RegisteredUserAllowed ||
                    (this.RegisteredUserAllowed != null &&
                    this.RegisteredUserAllowed.Equals(input.RegisteredUserAllowed))
                ) && 
                (
                    this.TitleViewAllowed == input.TitleViewAllowed ||
                    (this.TitleViewAllowed != null &&
                    this.TitleViewAllowed.Equals(input.TitleViewAllowed))
                ) && 
                (
                    this.UploadAllowed == input.UploadAllowed ||
                    (this.UploadAllowed != null &&
                    this.UploadAllowed.Equals(input.UploadAllowed))
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
                if (this.ContactAllowed != null)
                    hashCode = hashCode * 59 + this.ContactAllowed.GetHashCode();
                if (this.DeleteAllowed != null)
                    hashCode = hashCode * 59 + this.DeleteAllowed.GetHashCode();
                if (this.DownloadAllowed != null)
                    hashCode = hashCode * 59 + this.DownloadAllowed.GetHashCode();
                if (this.LicenseTypeRules != null)
                    hashCode = hashCode * 59 + this.LicenseTypeRules.GetHashCode();
                if (this.LicensendProfessionalAllowed != null)
                    hashCode = hashCode * 59 + this.LicensendProfessionalAllowed.GetHashCode();
                if (this.OwnerAllowed != null)
                    hashCode = hashCode * 59 + this.OwnerAllowed.GetHashCode();
                if (this.RecordCreatorAllowed != null)
                    hashCode = hashCode * 59 + this.RecordCreatorAllowed.GetHashCode();
                if (this.RegisteredUserAllowed != null)
                    hashCode = hashCode * 59 + this.RegisteredUserAllowed.GetHashCode();
                if (this.TitleViewAllowed != null)
                    hashCode = hashCode * 59 + this.TitleViewAllowed.GetHashCode();
                if (this.UploadAllowed != null)
                    hashCode = hashCode * 59 + this.UploadAllowed.GetHashCode();
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