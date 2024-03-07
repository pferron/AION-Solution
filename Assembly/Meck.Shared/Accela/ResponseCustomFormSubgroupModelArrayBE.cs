using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public partial class ResponseCustomFormSubgroupModelArrayBE
    {
            /// <summary>
            /// Initializes a new instance of the <see cref="ResponseCustomFormSubgroupModelArrayBE" /> class.
            /// </summary>
            /// <param name="result">result.</param>
            /// <param name="status">The HTTP return status..</param>
            public ResponseCustomFormSubgroupModelArrayBE(List<CustomFormSubgroupModelBE> result = default(List<CustomFormSubgroupModelBE>), int? status = default(int?))
            {
                this.Result = result;
                this.Status = status;
            }

            /// <summary>
            /// Gets or Sets Result
            /// </summary>
            [DataMember(Name = "result", EmitDefaultValue = false)]
            public List<CustomFormSubgroupModelBE> Result { get; set; }

            /// <summary>
            /// The HTTP return status.
            /// </summary>
            /// <value>The HTTP return status.</value>
            [DataMember(Name = "status", EmitDefaultValue = false)]
            public int? Status { get; set; }
        }

    public partial class CustomFormSubgroupModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormSubgroupModel" /> class.
        /// </summary>
        /// <param name="displayOrder">The custom form subgroup display order..</param>
        /// <param name="fields">fields.</param>
        /// <param name="id">The custom form subgroup system id assigned by the Civic Platform server..</param>
        /// <param name="text">The custom form subgroup name..</param>
        public CustomFormSubgroupModelBE(long? displayOrder = default(long?),
            List<CustomFormFieldBE> fields = default(List<CustomFormFieldBE>), string id = default(string),
            string text = default(string))
        {
            this.DisplayOrder = displayOrder;
            this.Fields = fields;
            this.Id = id;
            this.Text = text;
        }

        /// <summary>
        /// The custom form subgroup display order.
        /// </summary>
        /// <value>The custom form subgroup display order.</value>
        [DataMember(Name = "displayOrder", EmitDefaultValue = false)]
        public long? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or Sets Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public List<CustomFormFieldBE> Fields { get; set; }

        /// <summary>
        /// The custom form subgroup system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The custom form subgroup system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The custom form subgroup name.
        /// </summary>
        /// <value>The custom form subgroup name.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }
      

    }

    public partial class CustomFormFieldBE
    {
        /// <summary>
        /// Indicates whether or not the custom field is read-only.
        /// </summary>
        /// <value>Indicates whether or not the custom field is read-only.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsReadonlyEnum
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
        /// Indicates whether or not the custom field is read-only.
        /// </summary>
        /// <value>Indicates whether or not the custom field is read-only.</value>
        [DataMember(Name = "isReadonly", EmitDefaultValue = false)]
        public IsReadonlyEnum? IsReadonly { get; set; }

        /// <summary>
        /// Indicates whether or not the custom field is required.
        /// </summary>
        /// <value>Indicates whether or not the custom field is required.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsRequiredEnum
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
        /// Indicates whether or not the custom field is required.
        /// </summary>
        /// <value>Indicates whether or not the custom field is required.</value>
        [DataMember(Name = "isRequired", EmitDefaultValue = false)]
        public IsRequiredEnum? IsRequired { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormField" /> class.
        /// </summary>
        /// <param name="displayOrder">displayOrder.</param>
        /// <param name="drillDown">drillDown.</param>
        /// <param name="fieldType">The custom field data type..</param>
        /// <param name="id">The custom field system id assigned by the Civic Platform server..</param>
        /// <param name="isReadonly">Indicates whether or not the custom field is read-only..</param>
        /// <param name="isRequired">Indicates whether or not the custom field is required..</param>
        /// <param name="maxLength">The custom field length.</param>
        /// <param name="options">options.</param>
        /// <param name="text">The custom field localized text..</param>
        /// <param name="value">The custom field stored value..</param>
        public CustomFormFieldBE(long? displayOrder = default(long?), ASITableDrillBE drillDown =
            default(ASITableDrillBE), string fieldType = default(string), string id =
            default(string), IsReadonlyEnum? isReadonly = default(IsReadonlyEnum?), IsRequiredEnum? isRequired =
            default(IsRequiredEnum?), long? maxLength = default(long?), List<CustomFormFieldOptionsBE> options =
            default(List<CustomFormFieldOptionsBE>), string text = default(string), string value = default(string))
        {
            this.DisplayOrder = displayOrder;
            this.DrillDown = drillDown;
            this.FieldType = fieldType;
            this.Id = id;
            this.IsReadonly = isReadonly;
            this.IsRequired = isRequired;
            this.MaxLength = maxLength;
            this.Options = options;
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// Gets or Sets DisplayOrder
        /// </summary>
        [DataMember(Name = "displayOrder", EmitDefaultValue = false)]
        public long? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or Sets DrillDown
        /// </summary>
        [DataMember(Name = "drillDown", EmitDefaultValue = false)]
        public ASITableDrillBE DrillDown { get; set; }

        /// <summary>
        /// The custom field data type.
        /// </summary>
        /// <value>The custom field data type.</value>
        [DataMember(Name = "fieldType", EmitDefaultValue = false)]
        public string FieldType { get; set; }

        /// <summary>
        /// The custom field system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The custom field system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }



        /// <summary>
        /// The custom field length
        /// </summary>
        /// <value>The custom field length</value>
        [DataMember(Name = "maxLength", EmitDefaultValue = false)]
        public long? MaxLength { get; set; }

        /// <summary>
        /// Gets or Sets Options
        /// </summary>
        [DataMember(Name = "options", EmitDefaultValue = false)]
        public List<CustomFormFieldOptionsBE> Options { get; set; }

        /// <summary>
        /// The custom field localized text.
        /// </summary>
        /// <value>The custom field localized text.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The custom field stored value.
        /// </summary>
        /// <value>The custom field stored value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    public partial class CustomFormFieldOptionsBE
    {


        public CustomFormFieldOptionsBE(string text = default(string), string value = default(string))
    {
    this.Text = text;
    this.Value = value;
    }

    /// <summary>
    /// The localized display value.
    /// </summary>
    /// <value>The localized display value.</value>
    [DataMember(Name = "text", EmitDefaultValue = false)]
    public string Text {
    get;
    set;
    }

    /// <summary>
    /// The data value.
    /// </summary>
    /// <value>The data value.</value>
    [DataMember(Name = "value", EmitDefaultValue = false)]
    public string Value {
    get;
    set;
    }
}

public partial class ASITableDrillBE
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ASITableDrillBE" /> class.
        /// </summary>
        /// <param name="children">children.</param>
        /// <param name="isRoot">isRoot.</param>
        public ASITableDrillBE(List<ChildDrillBE> children = default(List<ChildDrillBE>), bool? isRoot = default(bool?))
        {
            this.Children = children;
            this.IsRoot = isRoot;
        }

        /// <summary>
        /// Gets or Sets Children
        /// </summary>
        [DataMember(Name = "children", EmitDefaultValue = false)]
        public List<ChildDrillBE> Children { get; set; }

        /// <summary>
        /// Gets or Sets IsRoot
        /// </summary>
        [DataMember(Name = "isRoot", EmitDefaultValue = false)]
        public bool? IsRoot { get; set; }
    }

    public partial class ChildDrillBE 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChildDrill" /> class.
        /// </summary>
        /// <param name="drillId">drillId.</param>
        /// <param name="id">id.</param>
        public ChildDrillBE(long? drillId = default(long?), string id = default(string))
        {
            this.DrillId = drillId;
            this.Id = id;
        }

        /// <summary>
        /// Gets or Sets DrillId
        /// </summary>
        [DataMember(Name = "drillId", EmitDefaultValue = false)]
        public long? DrillId { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

    }
}