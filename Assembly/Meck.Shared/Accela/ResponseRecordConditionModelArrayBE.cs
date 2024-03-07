using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    public class ResponseRecordConditionModelArrayBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseRecordConditionModelArray" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseRecordConditionModelArrayBE(
            List<RecordConditionModelBE> result = default(List<RecordConditionModelBE>), int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        public List<RecordConditionModelBE> Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>
        public int? Status { get; set; }
    }
}