/* 
 * Contacts and Professionals
 *
 * The Contacts and Professionals APIs enable apps to manage reference contacts and licensed professionals.
 *
 * OpenAPI spec version: v4
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace AccelaContactsAndProfessionals.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IProfessionalsConditionsApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get All Professional Conditions
        /// </summary>
        /// <remarks>
        /// Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </remarks>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponsePeopleConditionModelArray</returns>
        ResponsePeopleConditionModelArray V4GetProfessionalsIdConditions (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null);

        /// <summary>
        /// Get All Professional Conditions
        /// </summary>
        /// <remarks>
        /// Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </remarks>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponsePeopleConditionModelArray</returns>
        ApiResponse<ResponsePeopleConditionModelArray> V4GetProfessionalsIdConditionsWithHttpInfo (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get All Professional Conditions
        /// </summary>
        /// <remarks>
        /// Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </remarks>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponsePeopleConditionModelArray</returns>
        System.Threading.Tasks.Task<ResponsePeopleConditionModelArray> V4GetProfessionalsIdConditionsAsync (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null);

        /// <summary>
        /// Get All Professional Conditions
        /// </summary>
        /// <remarks>
        /// Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </remarks>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponsePeopleConditionModelArray)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResponsePeopleConditionModelArray>> V4GetProfessionalsIdConditionsAsyncWithHttpInfo (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ProfessionalsConditionsApi : IProfessionalsConditionsApi
    {
        private AccelaContactsAndProfessionals.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfessionalsConditionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProfessionalsConditionsApi(String basePath)
        {
            this.Configuration = new AccelaContactsAndProfessionals.Client.Configuration { BasePath = basePath };

            ExceptionFactory = AccelaContactsAndProfessionals.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfessionalsConditionsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public ProfessionalsConditionsApi(AccelaContactsAndProfessionals.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = AccelaContactsAndProfessionals.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = AccelaContactsAndProfessionals.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public AccelaContactsAndProfessionals.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public AccelaContactsAndProfessionals.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public IDictionary<String, String> DefaultHeader()
        {
            return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Get All Professional Conditions Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </summary>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponsePeopleConditionModelArray</returns>
        public ResponsePeopleConditionModelArray V4GetProfessionalsIdConditions (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null)
        {
             ApiResponse<ResponsePeopleConditionModelArray> localVarResponse = V4GetProfessionalsIdConditionsWithHttpInfo(contentType, authorization, id, type, name, status, fields, offset, limit, lang);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get All Professional Conditions Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </summary>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponsePeopleConditionModelArray</returns>
        public ApiResponse< ResponsePeopleConditionModelArray > V4GetProfessionalsIdConditionsWithHttpInfo (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling ProfessionalsConditionsApi->V4GetProfessionalsIdConditions");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling ProfessionalsConditionsApi->V4GetProfessionalsIdConditions");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling ProfessionalsConditionsApi->V4GetProfessionalsIdConditions");

            var localVarPath = "/v4/professionals/{id}/conditions";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (id != null) localVarPathParams.Add("id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (type != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "type", type)); // query parameter
            if (name != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "name", name)); // query parameter
            if (status != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "status", status)); // query parameter
            if (fields != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "fields", fields)); // query parameter
            if (offset != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "offset", offset)); // query parameter
            if (limit != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "limit", limit)); // query parameter
            if (lang != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetProfessionalsIdConditions", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponsePeopleConditionModelArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponsePeopleConditionModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponsePeopleConditionModelArray)));
        }

        /// <summary>
        /// Get All Professional Conditions Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </summary>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponsePeopleConditionModelArray</returns>
        public async System.Threading.Tasks.Task<ResponsePeopleConditionModelArray> V4GetProfessionalsIdConditionsAsync (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null)
        {
             ApiResponse<ResponsePeopleConditionModelArray> localVarResponse = await V4GetProfessionalsIdConditionsAsyncWithHttpInfo(contentType, authorization, id, type, name, status, fields, offset, limit, lang);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get All Professional Conditions Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 
        /// </summary>
        /// <exception cref="AccelaContactsAndProfessionals.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="id">The ID of the licensed professional to fetch.</param>
        /// <param name="type">Condition type filter (optional)</param>
        /// <param name="name">Condition name filter (optional)</param>
        /// <param name="status">Condition status filter (optional)</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        /// <param name="limit">Search result size limit. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponsePeopleConditionModelArray)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResponsePeopleConditionModelArray>> V4GetProfessionalsIdConditionsAsyncWithHttpInfo (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling ProfessionalsConditionsApi->V4GetProfessionalsIdConditions");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling ProfessionalsConditionsApi->V4GetProfessionalsIdConditions");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling ProfessionalsConditionsApi->V4GetProfessionalsIdConditions");

            var localVarPath = "/v4/professionals/{id}/conditions";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (id != null) localVarPathParams.Add("id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (type != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "type", type)); // query parameter
            if (name != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "name", name)); // query parameter
            if (status != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "status", status)); // query parameter
            if (fields != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "fields", fields)); // query parameter
            if (offset != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "offset", offset)); // query parameter
            if (limit != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "limit", limit)); // query parameter
            if (lang != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetProfessionalsIdConditions", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponsePeopleConditionModelArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponsePeopleConditionModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponsePeopleConditionModelArray)));
        }

    }
}