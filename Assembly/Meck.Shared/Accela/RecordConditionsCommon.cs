using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    [DataContract]
    public partial class RecordConditionModelActiveStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelActiveStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelActiveStatus(string text = default(string), string value = default(string))
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

    public partial class RecordConditionModelGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelGroup" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelGroup(string text = default(string), string value = default(string))
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
    public partial class RecordConditionModelInheritable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelInheritable" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelInheritable(string text = default(string), string value = default(string))
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

    public partial class RecordConditionModelAppliedbyDepartment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelAppliedbyDepartment" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelAppliedbyDepartment(string text = default(string), string value = default(string))
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
    public partial class RecordConditionModelAppliedbyUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelAppliedbyUser" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelAppliedbyUser(string text = default(string), string value = default(string))
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


    [DataContract]
    public partial class RecordConditionModelPriority
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelPriority" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelPriority(string text = default(string), string value = default(string))
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

    public partial class RecordConditionModelSeverity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelSeverity" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelSeverity(string text = default(string), string value = default(string))
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
   
    public partial class RecordConditionModelStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelStatus(string text = default(string), string value = default(string))
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

    public partial class RecordConditionModelType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelType(string text = default(string), string value = default(string))
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

    public partial class RecordConditionModelActionbyDepartment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelActionbyDepartment" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelActionbyDepartment(string text = default(string), string value = default(string))
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

    public partial class RecordConditionModelActionbyUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelActionbyUser" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelActionbyUser(string text = default(string), string value = default(string))
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
