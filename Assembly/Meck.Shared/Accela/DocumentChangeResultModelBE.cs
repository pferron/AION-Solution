using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class DocumentChangeResultModelBE
    {  /// <summary>
        /// Initializes a new instance of the <see cref="ResultModel" /> class.
        /// </summary>
        /// <param name="code">The error code, if an error is encountered..</param>
        /// <param name="id">The system id of the object in this operation..</param>
        /// <param name="isSuccess">Indicates whether or not the operation on the object is successful..</param>
        /// <param name="message">The error message, if an error is encountered.</param>
        public DocumentChangeResultModelBE(string code = default(string), long? id = default(long?), bool? isSuccess = default(bool?), string message = default(string))
        {
            this.Code = code;
            this.Id = id;
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        /// <summary>
        /// The error code, if an error is encountered.
        /// </summary>
        /// <value>The error code, if an error is encountered.</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// The system id of the object in this operation.
        /// </summary>
        /// <value>The system id of the object in this operation.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// Indicates whether or not the operation on the object is successful.
        /// </summary>
        /// <value>Indicates whether or not the operation on the object is successful.</value>
        [DataMember(Name = "isSuccess", EmitDefaultValue = false)]
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// The error message, if an error is encountered
        /// </summary>
        /// <value>The error message, if an error is encountered</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}
