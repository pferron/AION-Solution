/* 
 * Settings
 *
 * The Settings API provides configuration values that have been defined in Civic Platform Administration, typically as standard choice values. The Settings APIs are helpful when you need reference or custom-configured values in your API calls.
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
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace AccelaSettings.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISettingsAssessmentsApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get All Condition Assessment Types
        /// </summary>
        /// <remarks>
        /// Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseConditionAssessmentModelArray</returns>
        ResponseConditionAssessmentModelArray V4GetSettingsAssessmentsTypes (string contentType, string fields = null, string lang = null);

        /// <summary>
        /// Get All Condition Assessment Types
        /// </summary>
        /// <remarks>
        /// Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseConditionAssessmentModelArray</returns>
        ApiResponse<ResponseConditionAssessmentModelArray> V4GetSettingsAssessmentsTypesWithHttpInfo (string contentType, string fields = null, string lang = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get All Condition Assessment Types
        /// </summary>
        /// <remarks>
        /// Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseConditionAssessmentModelArray</returns>
        System.Threading.Tasks.Task<ResponseConditionAssessmentModelArray> V4GetSettingsAssessmentsTypesAsync (string contentType, string fields = null, string lang = null);

        /// <summary>
        /// Get All Condition Assessment Types
        /// </summary>
        /// <remarks>
        /// Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseConditionAssessmentModelArray)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResponseConditionAssessmentModelArray>> V4GetSettingsAssessmentsTypesAsyncWithHttpInfo (string contentType, string fields = null, string lang = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class SettingsAssessmentsApi : ISettingsAssessmentsApi
    {
        private AccelaSettings.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsAssessmentsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SettingsAssessmentsApi(String basePath)
        {
            this.Configuration = new AccelaSettings.Client.Configuration { BasePath = basePath };

            ExceptionFactory = AccelaSettings.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsAssessmentsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public SettingsAssessmentsApi(AccelaSettings.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = AccelaSettings.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = AccelaSettings.Client.Configuration.DefaultExceptionFactory;
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
        public AccelaSettings.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public AccelaSettings.Client.ExceptionFactory ExceptionFactory
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
        /// Get All Condition Assessment Types Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseConditionAssessmentModelArray</returns>
        public ResponseConditionAssessmentModelArray V4GetSettingsAssessmentsTypes (string contentType, string fields = null, string lang = null)
        {
             ApiResponse<ResponseConditionAssessmentModelArray> localVarResponse = V4GetSettingsAssessmentsTypesWithHttpInfo(contentType, fields, lang);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get All Condition Assessment Types Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseConditionAssessmentModelArray</returns>
        public ApiResponse< ResponseConditionAssessmentModelArray > V4GetSettingsAssessmentsTypesWithHttpInfo (string contentType, string fields = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling SettingsAssessmentsApi->V4GetSettingsAssessmentsTypes");

            var localVarPath = "/v4/settings/assessments/types";
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

            if (fields != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "fields", fields)); // query parameter
            if (lang != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetSettingsAssessmentsTypes", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseConditionAssessmentModelArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseConditionAssessmentModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseConditionAssessmentModelArray)));
        }

        /// <summary>
        /// Get All Condition Assessment Types Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseConditionAssessmentModelArray</returns>
        public async System.Threading.Tasks.Task<ResponseConditionAssessmentModelArray> V4GetSettingsAssessmentsTypesAsync (string contentType, string fields = null, string lang = null)
        {
             ApiResponse<ResponseConditionAssessmentModelArray> localVarResponse = await V4GetSettingsAssessmentsTypesAsyncWithHttpInfo(contentType, fields, lang);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get All Condition Assessment Types Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaSettings.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseConditionAssessmentModelArray)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResponseConditionAssessmentModelArray>> V4GetSettingsAssessmentsTypesAsyncWithHttpInfo (string contentType, string fields = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling SettingsAssessmentsApi->V4GetSettingsAssessmentsTypes");

            var localVarPath = "/v4/settings/assessments/types";
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

            if (fields != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "fields", fields)); // query parameter
            if (lang != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetSettingsAssessmentsTypes", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseConditionAssessmentModelArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseConditionAssessmentModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseConditionAssessmentModelArray)));
        }

    }
}
