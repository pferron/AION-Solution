/* 
 * Miscellaneous
 *
 * Miscellaneous Construct APIs
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
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace AccelaMiscellanous.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IAppSettingsApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get All App Settings
        /// </summary>
        /// <remarks>
        /// Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>ResponseAppSettingsArray</returns>
        ResponseAppSettingsArray V4GetAppsettings (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null);

        /// <summary>
        /// Get All App Settings
        /// </summary>
        /// <remarks>
        /// Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>ApiResponse of ResponseAppSettingsArray</returns>
        ApiResponse<ResponseAppSettingsArray> V4GetAppsettingsWithHttpInfo (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get All App Settings
        /// </summary>
        /// <remarks>
        /// Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>Task of ResponseAppSettingsArray</returns>
        System.Threading.Tasks.Task<ResponseAppSettingsArray> V4GetAppsettingsAsync (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null);

        /// <summary>
        /// Get All App Settings
        /// </summary>
        /// <remarks>
        /// Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseAppSettingsArray)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResponseAppSettingsArray>> V4GetAppsettingsAsyncWithHttpInfo (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class AppSettingsApi : IAppSettingsApi
    {
        private AccelaMiscellanous.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AppSettingsApi(String basePath)
        {
            this.Configuration = new AccelaMiscellanous.Client.Configuration { BasePath = basePath };

            ExceptionFactory = AccelaMiscellanous.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public AppSettingsApi(AccelaMiscellanous.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = AccelaMiscellanous.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = AccelaMiscellanous.Client.Configuration.DefaultExceptionFactory;
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
        public AccelaMiscellanous.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public AccelaMiscellanous.Client.ExceptionFactory ExceptionFactory
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
        /// Get All App Settings Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>ResponseAppSettingsArray</returns>
        public ResponseAppSettingsArray V4GetAppsettings (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null)
        {
             ApiResponse<ResponseAppSettingsArray> localVarResponse = V4GetAppsettingsWithHttpInfo(contentType, authorization, keys, agency, appId, appSecret);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get All App Settings Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>ApiResponse of ResponseAppSettingsArray</returns>
        public ApiResponse< ResponseAppSettingsArray > V4GetAppsettingsWithHttpInfo (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling AppSettingsApi->V4GetAppsettings");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling AppSettingsApi->V4GetAppsettings");

            var localVarPath = "/v4/appsettings";
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

            if (keys != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "keys", keys)); // query parameter
            if (agency != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "agency", agency)); // query parameter
            if (appId != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "appId", appId)); // query parameter
            if (appSecret != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "appSecret", appSecret)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetAppsettings", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseAppSettingsArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseAppSettingsArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseAppSettingsArray)));
        }

        /// <summary>
        /// Get All App Settings Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>Task of ResponseAppSettingsArray</returns>
        public async System.Threading.Tasks.Task<ResponseAppSettingsArray> V4GetAppsettingsAsync (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null)
        {
             ApiResponse<ResponseAppSettingsArray> localVarResponse = await V4GetAppsettingsAsyncWithHttpInfo(contentType, authorization, keys, agency, appId, appSecret);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get All App Settings Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="keys">Comma-delimited app setting keys filter (optional)</param>
        /// <param name="agency">The agency the app belongs to. (optional)</param>
        /// <param name="appId">The ID of the registered app on http://developer.accela.com. (optional)</param>
        /// <param name="appSecret">The secret key of the registered app on http://developer.accela.com. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseAppSettingsArray)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResponseAppSettingsArray>> V4GetAppsettingsAsyncWithHttpInfo (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling AppSettingsApi->V4GetAppsettings");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling AppSettingsApi->V4GetAppsettings");

            var localVarPath = "/v4/appsettings";
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

            if (keys != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "keys", keys)); // query parameter
            if (agency != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "agency", agency)); // query parameter
            if (appId != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "appId", appId)); // query parameter
            if (appSecret != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "appSecret", appSecret)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetAppsettings", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseAppSettingsArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseAppSettingsArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseAppSettingsArray)));
        }

    }
}