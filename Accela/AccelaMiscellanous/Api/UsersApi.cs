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
    public interface IUsersApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get My User Account
        /// </summary>
        /// <remarks>
        /// Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseUserModel</returns>
        ResponseUserModel V4GetUsersMe (string contentType, string authorization, string fields = null, string lang = null);

        /// <summary>
        /// Get My User Account
        /// </summary>
        /// <remarks>
        /// Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseUserModel</returns>
        ApiResponse<ResponseUserModel> V4GetUsersMeWithHttpInfo (string contentType, string authorization, string fields = null, string lang = null);
        /// <summary>
        /// Get All User Groups
        /// </summary>
        /// <remarks>
        /// Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseUserGroupModel</returns>
        ResponseUserGroupModel V4GetUsersUserIdGroups (string contentType, string userId, string authorization, string fields = null, string lang = null);

        /// <summary>
        /// Get All User Groups
        /// </summary>
        /// <remarks>
        /// Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseUserGroupModel</returns>
        ApiResponse<ResponseUserGroupModel> V4GetUsersUserIdGroupsWithHttpInfo (string contentType, string userId, string authorization, string fields = null, string lang = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get My User Account
        /// </summary>
        /// <remarks>
        /// Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseUserModel</returns>
        System.Threading.Tasks.Task<ResponseUserModel> V4GetUsersMeAsync (string contentType, string authorization, string fields = null, string lang = null);

        /// <summary>
        /// Get My User Account
        /// </summary>
        /// <remarks>
        /// Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseUserModel)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResponseUserModel>> V4GetUsersMeAsyncWithHttpInfo (string contentType, string authorization, string fields = null, string lang = null);
        /// <summary>
        /// Get All User Groups
        /// </summary>
        /// <remarks>
        /// Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseUserGroupModel</returns>
        System.Threading.Tasks.Task<ResponseUserGroupModel> V4GetUsersUserIdGroupsAsync (string contentType, string userId, string authorization, string fields = null, string lang = null);

        /// <summary>
        /// Get All User Groups
        /// </summary>
        /// <remarks>
        /// Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseUserGroupModel)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResponseUserGroupModel>> V4GetUsersUserIdGroupsAsyncWithHttpInfo (string contentType, string userId, string authorization, string fields = null, string lang = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class UsersApi : IUsersApi
    {
        private AccelaMiscellanous.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersApi"/> class.
        /// </summary>
        /// <returns></returns>
        public UsersApi(String basePath)
        {
            this.Configuration = new AccelaMiscellanous.Client.Configuration { BasePath = basePath };

            ExceptionFactory = AccelaMiscellanous.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public UsersApi(AccelaMiscellanous.Client.Configuration configuration = null)
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
        /// Get My User Account Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseUserModel</returns>
        public ResponseUserModel V4GetUsersMe (string contentType, string authorization, string fields = null, string lang = null)
        {
             ApiResponse<ResponseUserModel> localVarResponse = V4GetUsersMeWithHttpInfo(contentType, authorization, fields, lang);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get My User Account Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseUserModel</returns>
        public ApiResponse< ResponseUserModel > V4GetUsersMeWithHttpInfo (string contentType, string authorization, string fields = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling UsersApi->V4GetUsersMe");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling UsersApi->V4GetUsersMe");

            var localVarPath = "/v4/users/me";
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
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetUsersMe", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseUserModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseUserModel) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseUserModel)));
        }

        /// <summary>
        /// Get My User Account Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseUserModel</returns>
        public async System.Threading.Tasks.Task<ResponseUserModel> V4GetUsersMeAsync (string contentType, string authorization, string fields = null, string lang = null)
        {
             ApiResponse<ResponseUserModel> localVarResponse = await V4GetUsersMeAsyncWithHttpInfo(contentType, authorization, fields, lang);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get My User Account Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseUserModel)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResponseUserModel>> V4GetUsersMeAsyncWithHttpInfo (string contentType, string authorization, string fields = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling UsersApi->V4GetUsersMe");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling UsersApi->V4GetUsersMe");

            var localVarPath = "/v4/users/me";
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
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4GetUsersMe", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseUserModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseUserModel) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseUserModel)));
        }

        /// <summary>
        /// Get All User Groups Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseUserGroupModel</returns>
        public ResponseUserGroupModel V4GetUsersUserIdGroups (string contentType, string userId, string authorization, string fields = null, string lang = null)
        {
             ApiResponse<ResponseUserGroupModel> localVarResponse = V4GetUsersUserIdGroupsWithHttpInfo(contentType, userId, authorization, fields, lang);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get All User Groups Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseUserGroupModel</returns>
        public ApiResponse< ResponseUserGroupModel > V4GetUsersUserIdGroupsWithHttpInfo (string contentType, string userId, string authorization, string fields = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling UsersApi->V4GetUsersUserIdGroups");
            // verify the required parameter 'userId' is set
            if (userId == null)
                throw new ApiException(400, "Missing required parameter 'userId' when calling UsersApi->V4GetUsersUserIdGroups");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling UsersApi->V4GetUsersUserIdGroups");

            var localVarPath = "/v4/users/{user_id}/groups";
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

            if (userId != null) localVarPathParams.Add("user_id", this.Configuration.ApiClient.ParameterToString(userId)); // path parameter
            if (fields != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "fields", fields)); // query parameter
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
                Exception exception = ExceptionFactory("V4GetUsersUserIdGroups", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseUserGroupModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseUserGroupModel) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseUserGroupModel)));
        }

        /// <summary>
        /// Get All User Groups Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseUserGroupModel</returns>
        public async System.Threading.Tasks.Task<ResponseUserGroupModel> V4GetUsersUserIdGroupsAsync (string contentType, string userId, string authorization, string fields = null, string lang = null)
        {
             ApiResponse<ResponseUserGroupModel> localVarResponse = await V4GetUsersUserIdGroupsAsyncWithHttpInfo(contentType, userId, authorization, fields, lang);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get All User Groups Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="userId">The ID of the user to be fetched.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseUserGroupModel)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResponseUserGroupModel>> V4GetUsersUserIdGroupsAsyncWithHttpInfo (string contentType, string userId, string authorization, string fields = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling UsersApi->V4GetUsersUserIdGroups");
            // verify the required parameter 'userId' is set
            if (userId == null)
                throw new ApiException(400, "Missing required parameter 'userId' when calling UsersApi->V4GetUsersUserIdGroups");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling UsersApi->V4GetUsersUserIdGroups");

            var localVarPath = "/v4/users/{user_id}/groups";
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

            if (userId != null) localVarPathParams.Add("user_id", this.Configuration.ApiClient.ParameterToString(userId)); // path parameter
            if (fields != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "fields", fields)); // query parameter
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
                Exception exception = ExceptionFactory("V4GetUsersUserIdGroups", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseUserGroupModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseUserGroupModel) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseUserGroupModel)));
        }

    }
}