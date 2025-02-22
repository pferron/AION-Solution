/* 
 * Agencies-b
 *
 * The Agencies API provides agency information as configured in the [Construct Admin Portal](https://admin.accela.com).
 *
 * OpenAPI spec version: v4-oas3
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using Newtonsoft.Json.Converters;

namespace AccelaAgencies.Client
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
