using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Meck.Shared.Accela
{
    public partial class ResponseRecordRelatedModelArrayBE
    {
       /// <summary>
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseRecordRelatedModelArrayBE(
            List<RecordRelatedModelBE> result = default(List<RecordRelatedModelBE>), int? status = default(int?))
        {
       
            this.Result = result;
            this.Status = status;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        public List<RecordRelatedModelBE> Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>

        public int? Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ResponseRecordRelatedModelArray {\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
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



    }

    public partial class RecordRelatedModelBE
    {
        /// <summary>
        /// The type of relationship of a related record.
        /// </summary>
        /// <value>The type of relationship of a related record.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RelationshipEnum
        {

            /// <summary>
            /// Enum Parent for value: parent
            /// </summary>
            [EnumMember(Value = "parent")] Parent = 1,

            /// <summary>
            /// Enum Child for value: child
            /// </summary>
            [EnumMember(Value = "child")] Child = 2,

            /// <summary>
            /// Enum Renewal for value: renewal
            /// </summary>
            [EnumMember(Value = "renewal")] Renewal = 3
        }

        /// <summary>
        /// The type of relationship of a related record.
        /// </summary>
        /// <value>The type of relationship of a related record.</value>
        public RelationshipEnum? Relationship { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordRelatedModel" /> class.
        /// </summary>
        /// <param name="customId">An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application..</param>
        /// <param name="id">The record system id assigned by the Civic Platform server..</param>
        /// <param name="relationship">The type of relationship of a related record..</param>
        /// <param name="serviceProveCode">The unique agency id..</param>
        /// <param name="trackingId">The application tracking number (IVR tracking number)..</param>
        /// <param name="type">type.</param>
        public RecordRelatedModelBE(string customId = default(string), string id = default(string),
            RelationshipEnum? relationship = default(RelationshipEnum?), string serviceProveCode = default(string),
            long? trackingId = default(long?), RecordTypeNoAliasModelBE type = default(RecordTypeNoAliasModelBE))
        {
            this.CustomId = customId;
            this.Id = id;
            this.Relationship = relationship;
            this.ServiceProveCode = serviceProveCode;
            this.TrackingId = trackingId;
            this.Type = type;
        }

        /// <summary>
        /// An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.
        /// </summary>
        /// <value>An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.</value>
        public string CustomId { get; set; }

        /// <summary>
        /// The record system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The record system id assigned by the Civic Platform server.</value>
        public string Id { get; set; }


        /// <summary>
        /// The unique agency id.
        /// </summary>
        /// <value>The unique agency id.</value>
        public string ServiceProveCode { get; set; }

        /// <summary>
        /// The application tracking number (IVR tracking number).
        /// </summary>
        /// <value>The application tracking number (IVR tracking number).</value>
        public long? TrackingId { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        public RecordTypeNoAliasModelBE Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordRelatedModel {\n");
            sb.Append("  CustomId: ").Append(CustomId).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Relationship: ").Append(Relationship).Append("\n");
            sb.Append("  ServiceProveCode: ").Append(ServiceProveCode).Append("\n");
            sb.Append("  TrackingId: ").Append(TrackingId).Append("\n");
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

    }

    public partial class RecordTypeNoAliasModelBE
    { 
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordTypeNoAliasModel" /> class.
        /// </summary>
        /// <param name="category">The 4th level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="filterName">The name of the record type filter which defines the record types to be displayed for the citizen user..</param>
        /// <param name="group">The 1st level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="id">The record type system id assigned by the Civic Platform server..</param>
        /// <param name="module">Use to filter by the module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules)..</param>
        /// <param name="subType">The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="text">The record type display text.</param>
        /// <param name="type">The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="value">The record type value..</param>
        public RecordTypeNoAliasModelBE(string category = default(string), string filterName = default(string),
            string group = default(string), string id = default(string), string module = default(string),
            string subType = default(string), string text = default(string), string type = default(string),
            string value = default(string))
        {
            this.Category = category;
            this.FilterName = filterName;
            this.Group = group;
            this.Id = id;
            this.Module = module;
            this.SubType = subType;
            this.Text = text;
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// The 4th level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 4th level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
       
        public string Category { get; set; }

        /// <summary>
        /// The name of the record type filter which defines the record types to be displayed for the citizen user.
        /// </summary>
        /// <value>The name of the record type filter which defines the record types to be displayed for the citizen user.</value>
      
        public string FilterName { get; set; }

        /// <summary>
        /// The 1st level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 1st level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        
        public string Group { get; set; }

        /// <summary>
        /// The record type system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The record type system id assigned by the Civic Platform server.</value>
      
        public string Id { get; set; }

        /// <summary>
        /// Use to filter by the module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules).
        /// </summary>
        /// <value>Use to filter by the module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules).</value>
     
        public string Module { get; set; }

        /// <summary>
        /// The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
       
        public string SubType { get; set; }

        /// <summary>
        /// The record type display text
        /// </summary>
        /// <value>The record type display text</value>
       public string Text { get; set; }

        /// <summary>
        /// The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
      
        public string Type { get; set; }

        /// <summary>
        /// The record type value.
        /// </summary>
        /// <value>The record type value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordTypeNoAliasModel {\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  FilterName: ").Append(FilterName).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Module: ").Append(Module).Append("\n");
            sb.Append("  SubType: ").Append(SubType).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
    }
}
 
