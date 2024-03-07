using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Meck.Shared.Accela
{
    /// <summary>
    /// ResponseRecordContactSimpleModelArrayBO
    /// </summary>
    public partial class ResponseRecordContactSimpleModelArrayBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseRecordContactSimpleModelArray" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseRecordContactSimpleModelArrayBO(List<RecordContactSimpleModelBO> result = default(List<RecordContactSimpleModelBO>)) //, int? status = default(int?))
        {
            this.Result = result;
            this.Status = 200;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        public List<RecordContactSimpleModelBO> Result { get; set; }

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
            sb.Append("class ResponseRecordContactSimpleModelArrayBO {\n");
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
}
