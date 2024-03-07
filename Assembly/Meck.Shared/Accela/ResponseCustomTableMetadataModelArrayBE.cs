using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class ResponseCustomTableMetadataModelArrayBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCustomTableMetadataModelArray" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseCustomTableMetadataModelArrayBE(
            List<CustomTableMetadataModelBE> result = default(List<CustomTableMetadataModelBE>),
            int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public List<CustomTableMetadataModelBE> Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }
    }

    public partial class CustomTableMetadataModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTableMetadataModel" /> class.
        /// </summary>
        /// <param name="displayOrder">The custom table&#39;s display order..</param>
        /// <param name="fields">fields.</param>
        /// <param name="id">The custom table id..</param>
        /// <param name="text">The custom table name..</param>
        public CustomTableMetadataModelBE(long? displayOrder = default(long?),
            List<CustomFormFieldBE> fields = default(List<CustomFormFieldBE>), string id = default(string),
            string text = default(string))
        {
            this.DisplayOrder = displayOrder;
            this.Fields = fields;
            this.Id = id;
            this.Text = text;
        }

        /// <summary>
        /// The custom table&#39;s display order.
        /// </summary>
        /// <value>The custom table&#39;s display order.</value>
        [DataMember(Name = "displayOrder", EmitDefaultValue = false)]
        public long? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or Sets Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public List<CustomFormFieldBE> Fields { get; set; }

        /// <summary>
        /// The custom table id.
        /// </summary>
        /// <value>The custom table id.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The custom table name.
        /// </summary>
        /// <value>The custom table name.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }
    }
}