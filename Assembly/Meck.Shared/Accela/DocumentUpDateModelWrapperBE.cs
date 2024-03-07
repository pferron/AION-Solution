using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class DocumentUpDateModelWrapperBE
    {
        public List<DocumentModelBE> UpDatedModels { get; set; }


        public DocumentUpDateModelWrapperBE()
        {
            UpDatedModels = new List<DocumentModelBE>();
        }
    }


    public class DocumentModelBE
    {
        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        [DataMember(Name = "category", EmitDefaultValue = false)]
        public DocumentModelCategoryBE Category { get; set; }

        /// <summary>
        /// Gets or Sets Deletable
        /// </summary>
        [DataMember(Name = "deletable", EmitDefaultValue = false)]
        public UserRolePrivilegeModel Deletable { get; set; }

        /// <summary>
        /// The name of the department the document belongs to.
        /// </summary>
        /// <value>The name of the department the document belongs to.</value>
        [DataMember(Name = "department", EmitDefaultValue = false)]
        public string Department { get; set; }

        /// <summary>
        /// The document description.
        /// </summary>
        /// <value>The document description.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Downloadable
        /// </summary>
        [DataMember(Name = "downloadable", EmitDefaultValue = false)]
        public UserRolePrivilegeModel Downloadable { get; set; }

        /// <summary>
        /// The unique ID of the entity or record.
        /// </summary>
        /// <value>The unique ID of the entity or record.</value>
        [DataMember(Name = "entityId", EmitDefaultValue = false)]
        public string EntityId { get; set; }

        /// <summary>
        /// The type of entity.
        /// </summary>
        /// <value>The type of entity.</value>
        [DataMember(Name = "entityType", EmitDefaultValue = false)]
        public string EntityType { get; set; }

        /// <summary>
        /// The name of the file as it displays in the source location.
        /// </summary>
        /// <value>The name of the file as it displays in the source location.</value>
        [DataMember(Name = "fileName", EmitDefaultValue = false)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public DocumentModelGroup Group { get; set; }

        /// <summary>
        /// The document id.
        /// </summary>
        /// <value>The document id.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// The user account that last modified the document.
        /// </summary>
        /// <value>The user account that last modified the document.</value>
        [DataMember(Name = "modifiedBy", EmitDefaultValue = false)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// The date the document was last modified.
        /// </summary>
        /// <value>The date the document was last modified.</value>
        [DataMember(Name = "modifiedDate", EmitDefaultValue = false)]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The file size of the document.
        /// </summary>
        /// <value>The file size of the document.</value>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public double? Size { get; set; }

        /// <summary>
        /// The name for your agency&#x27;s electronic document management system.
        /// </summary>
        /// <value>The name for your agency&#x27;s electronic document management system.</value>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public DocumentModelStatus Status { get; set; }

        /// <summary>
        /// The date when the current status changed.
        /// </summary>
        /// <value>The date when the current status changed.</value>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets TitleViewable
        /// </summary>
        [DataMember(Name = "titleViewable", EmitDefaultValue = false)]
        public UserRolePrivilegeModel TitleViewable { get; set; }

        /// <summary>
        /// The document type.
        /// </summary>
        /// <value>The document type.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// The user who uploaded the document to the record.
        /// </summary>
        /// <value>The user who uploaded the document to the record.</value>
        [DataMember(Name = "uploadedBy", EmitDefaultValue = false)]
        public string UploadedBy { get; set; }

        /// <summary>
        /// The date when the document was uploaded.
        /// </summary>
        /// <value>The date when the document was uploaded.</value>
        [DataMember(Name = "uploadedDate", EmitDefaultValue = false)]
        public DateTime? UploadedDate { get; set; }

        /// <summary>
        /// This is the virtual folder for storing the attachment. With virtual folders you can organize uploaded attachments in groups
        /// </summary>
        /// <value>This is the virtual folder for storing the attachment. With virtual folders you can organize uploaded attachments in groups</value>
        [DataMember(Name = "virtualFolders", EmitDefaultValue = false)]
        public string VirtualFolders { get; set; }

        
    }

    /// <summary>
    /// The document group.
    /// </summary>
    [DataContract]
    public partial class DocumentModelGroup 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelGroup" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public DocumentModelGroup(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    /// <summary>
    /// The document status.
    /// </summary>
    [DataContract]
    public partial class DocumentModelStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaDocuments.Model.DocumentModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public DocumentModelStatus(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    /// <summary>
    /// UserRolePrivilegeModel
    /// </summary>
    [DataContract]
    public partial class UserRolePrivilegeModel
    {
        /// <summary>
        /// Defines CitizenRoles
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CitizenRolesEnum
        {
            /// <summary>
            /// Enum RegisteredUser for value: registeredUser
            /// </summary>
            [EnumMember(Value = "registeredUser")] RegisteredUser = 0,

            /// <summary>
            /// Enum LicensedProfessional for value: licensedProfessional
            /// </summary>
            [EnumMember(Value = "licensedProfessional")]
            LicensedProfessional = 1,

            /// <summary>
            /// Enum Owner for value: owner
            /// </summary>
            [EnumMember(Value = "owner")] Owner = 2,

            /// <summary>
            /// Enum Contact for value: contact
            /// </summary>
            [EnumMember(Value = "contact")] Contact = 3,

            /// <summary>
            /// Enum CitizenUser for value: citizenUser
            /// </summary>
            [EnumMember(Value = "citizenUser")] CitizenUser = 4,

            /// <summary>
            /// Enum RecordCreator for value: recordCreator
            /// </summary>
            [EnumMember(Value = "recordCreator")] RecordCreator = 5
        }

        /// <summary>
        /// Contains permission assignments for downloading documents, viewing document names, deleting documents.
        /// </summary>
        /// <value>Contains permission assignments for downloading documents, viewing document names, deleting documents.</value>
        [DataMember(Name = "citizenRoles", EmitDefaultValue = false)]
        public List<CitizenRolesEnum> CitizenRoles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaDocuments.Model.UserRolePrivilegeModel" /> class.
        /// </summary>
        /// <param name="citizenRoles">Contains permission assignments for downloading documents, viewing document names, deleting documents..</param>
        /// <param name="licenseTypeRules">A string array containing the types of licensed professionals who are given the permission. See [Get All Professional License Types](./api-settings.html#operation/v4.get.settings.professionals.types)..</param>
        public UserRolePrivilegeModel(
            List<CitizenRolesEnum> citizenRoles =
                default(List<CitizenRolesEnum>),
            List<string> licenseTypeRules = default(List<string>))
        {
            this.CitizenRoles = citizenRoles;
            this.LicenseTypeRules = licenseTypeRules;
        }


        /// <summary>
        /// A string array containing the types of licensed professionals who are given the permission. See [Get All Professional License Types](./api-settings.html#operation/v4.get.settings.professionals.types).
        /// </summary>
        /// <value>A string array containing the types of licensed professionals who are given the permission. See [Get All Professional License Types](./api-settings.html#operation/v4.get.settings.professionals.types).</value>
        [DataMember(Name = "licenseTypeRules", EmitDefaultValue = false)]
        public List<string> LicenseTypeRules { get; set; }
    }

    /// <summary>
    /// The document category. The list of category options varies depending on the document group.
    /// </summary>
    [DataContract]
    public partial class DocumentModelCategoryBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelCategory" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public DocumentModelCategoryBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
}
