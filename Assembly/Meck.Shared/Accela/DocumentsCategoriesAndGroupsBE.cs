using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class DocumentsCategoriesAndGroupsBE
    {

        public DocumentsCategoriesAndGroupsBE(List<mDocumentTypeModelGroups> result = default(List<mDocumentTypeModelGroups>), int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public List<mDocumentTypeModelGroups> Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }


    }

    public partial class mDocumentTypeModelGroups
    {
        /// <param name="value">The data value..</param>
        public mDocumentTypeModelGroups(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

}