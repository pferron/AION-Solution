/* 
 * Payments
 *
 * The Payments API enables apps to submit payment transactions on Civic Platform records. The Payments API provides two ways of accepting payments: 1: ***Using third-party payment vendors***  Payment API endpoints that save payment information from an external third-party payment vendor, to which the user is redirected for payment processing:   **Initialize Payment** - Initializes a Civic Platform payment transaction with a citizen's payment information such as record ID, third-party merchant account ID, and payment method. The Initialize Payment API returns a transaction ID which should be used as the payment ID when calling the Commit Payment API to save the payment details from the third-party merchant into Civic Platform.   **Commit Payment** - Triggers PaymentReceiveBefore and PaymentReceiveAfter events which can interact with third-party payment processing, and saves payment details in Civic Platform. Use the transaction ID returned by the Initialize Payment API to identify the payment transaction to be committed. Note that an app cannot use the Commit Payment API unless a Construct agency administrator enables the Payment Enabled setting on [Construct Admin Portal](https://admin.accela.com) > Agencies > {Agency} > Apps.  2: ***Using Civic Platform payment adapters***  Payment API endpoint that processes and saves payment transactions using a Civic Platform payment adapter:   **Create Payment** - Processes a payment using the default Civic Platform payment adapter.
 *
 * OpenAPI spec version: v4-oas3
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using Newtonsoft.Json.Converters;

namespace AccelaPayments.Client
{
    /// <summary>
    /// Formatter for 'date' swagger formats ss defined by full-date - RFC3339
    /// see https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md#data-types
    /// </summary>
    public class SwaggerDateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerDateConverter" /> class.
        /// </summary>
        public SwaggerDateConverter()
        {
            // full-date   = date-fullyear "-" date-month "-" date-mday
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
