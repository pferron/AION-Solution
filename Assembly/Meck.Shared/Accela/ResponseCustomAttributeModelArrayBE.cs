using System.Collections.Generic;
using System.Text;

namespace Meck.Shared.Accela
{
    public partial class ResponseCustomAttributeModelArrayBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCustomAttributeModelArray" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseCustomAttributeModelArrayBE(List<CustomAttributeModelBE> result = default(List<CustomAttributeModelBE>), int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        public List<CustomAttributeModelBE> Result { get; set; }

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
            sb.Append("class ResponseCustomAttributeModelArray {\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}

