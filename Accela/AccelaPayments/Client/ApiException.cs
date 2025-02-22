/* 
 * Payments
 *
 * The Payments API enables apps to submit payment transactions on Civic Platform records. The Payments API provides two ways of accepting payments: 1: ***Using third-party payment vendors***  Payment API endpoints that save payment information from an external third-party payment vendor, to which the user is redirected for payment processing:   **Initialize Payment** - Initializes a Civic Platform payment transaction with a citizen's payment information such as record ID, third-party merchant account ID, and payment method. The Initialize Payment API returns a transaction ID which should be used as the payment ID when calling the Commit Payment API to save the payment details from the third-party merchant into Civic Platform.   **Commit Payment** - Triggers PaymentReceiveBefore and PaymentReceiveAfter events which can interact with third-party payment processing, and saves payment details in Civic Platform. Use the transaction ID returned by the Initialize Payment API to identify the payment transaction to be committed. Note that an app cannot use the Commit Payment API unless a Construct agency administrator enables the Payment Enabled setting on [Construct Admin Portal](https://admin.accela.com) > Agencies > {Agency} > Apps.  2: ***Using Civic Platform payment adapters***  Payment API endpoint that processes and saves payment transactions using a Civic Platform payment adapter:   **Create Payment** - Processes a payment using the default Civic Platform payment adapter.
 *
 * OpenAPI spec version: v4-oas3
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;

namespace AccelaPayments.Client
{
    /// <summary>
    /// API Exception
    /// </summary>
        public class ApiException : Exception
    {
        /// <summary>
        /// Gets or sets the error code (HTTP status code)
        /// </summary>
        /// <value>The error code (HTTP status code).</value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error content (body json object)
        /// </summary>
        /// <value>The error content (Http response body).</value>
        public dynamic ErrorContent { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        public ApiException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="errorContent">Error content.</param>
        public ApiException(int errorCode, string message, dynamic errorContent = null) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorContent = errorContent;
        }
    }

}
