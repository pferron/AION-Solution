using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Meck.Shared.Accela
{
    public class SettingsWrapperBE
    {

        public List<SettingValueModelBE> SettingTypes { get; set; }
        public List<SettingsFolderGroupModel> FolderGroups { get; set; }

        public List<CatalogDocumentTypeModelGroups> CatalogTypes { get; set; }


        public SettingsWrapperBE()
        {
            SettingTypes = new List<SettingValueModelBE>();
        }
    }

    public class SettingValueModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingValueModelBE" /> class.
        /// </summary>
        /// <param name="text">The setting label..</param>
        /// <param name="value">The setting value..</param>
        public SettingValueModelBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The setting label.
        /// </summary>
        /// <value>The setting label.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The setting value.
        /// </summary>
        /// <value>The setting value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

    }

    public partial class SettingsFolderGroupModel
    {
        /// <summary>
        /// Indicates whether or not the folder group is active.
        /// </summary>
        /// <value>Indicates whether or not the folder group is active.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsActiveEnum
        {

            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")] Y = 1,

            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")] N = 2
        }

        /// <summary>
        /// Indicates whether or not the folder group is active.
        /// </summary>
        /// <value>Indicates whether or not the folder group is active.</value>
        [DataMember(Name = "isActive", EmitDefaultValue = false)]
        public IsActiveEnum? IsActive { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderGroupModel" /> class.
        /// </summary>
        /// <param name="description">The folder group description..</param>
        /// <param name="id">The ID of the folder group assigned by the Civic Platform server..</param>
        /// <param name="isActive">Indicates whether or not the folder group is active..</param>
        /// <param name="name">The folder group name..</param>
        /// <param name="type">The folder group type..</param>
        public SettingsFolderGroupModel(string description = default(string), string id = default(string),
            IsActiveEnum? isActive = default(IsActiveEnum?), string name = default(string),
            string type = default(string))
        {
            this.Description = description;
            this.Id = id;
            this.IsActive = isActive;
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// The folder group description.
        /// </summary>
        /// <value>The folder group description.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The ID of the folder group assigned by the Civic Platform server.
        /// </summary>
        /// <value>The ID of the folder group assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }


        /// <summary>
        /// The folder group name.
        /// </summary>
        /// <value>The folder group name.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The folder group type.
        /// </summary>
        /// <value>The folder group type.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }
    }

    public partial class SettingsCategoriesDocumentTypeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeModel" /> class.
        /// </summary>
        /// <param name="groups">groups.</param>
        /// <param name="id">The ID of the document type assigned by the Civic Platform server..</param>
        /// <param name="text">The document type display name..</param>
        /// <param name="value">The document type value..</param>
        public SettingsCategoriesDocumentTypeModel(
            List<CatalogDocumentTypeModelGroups> groups = default(List<CatalogDocumentTypeModelGroups>), string id = default(string),
            string text = default(string), string value = default(string))
        {
            this.Groups = groups;
            this.Id = id;
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// Gets or Sets Groups
        /// </summary>
        [DataMember(Name = "groups", EmitDefaultValue = false)]
        public List<CatalogDocumentTypeModelGroups> Groups { get; set; }

        /// <summary>
        /// The ID of the document type assigned by the Civic Platform server.
        /// </summary>
        /// <value>The ID of the document type assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The document type display name.
        /// </summary>
        /// <value>The document type display name.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The document type value.
        /// </summary>
        /// <value>The document type value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    /// <summary>
    /// DocumentTypeModelGroups
    /// </summary>
    [DataContract]
    public partial class CatalogDocumentTypeModelGroups
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaSettings.Model.DocumentTypeModelGroups" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public CatalogDocumentTypeModelGroups(string text = default(string), string value = default(string))
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
