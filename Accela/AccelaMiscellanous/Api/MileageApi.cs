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
    public interface IMileageApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Mileage
        /// </summary>
        /// <remarks>
        /// Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseResultModelArray</returns>
        ResponseResultModelArray V4PostMileage (string contentType, string authorization, List<MileageModel> body = null, string lang = null);

        /// <summary>
        /// Create Mileage
        /// </summary>
        /// <remarks>
        /// Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseResultModelArray</returns>
        ApiResponse<ResponseResultModelArray> V4PostMileageWithHttpInfo (string contentType, string authorization, List<MileageModel> body = null, string lang = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Create Mileage
        /// </summary>
        /// <remarks>
        /// Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseResultModelArray</returns>
        System.Threading.Tasks.Task<ResponseResultModelArray> V4PostMileageAsync (string contentType, string authorization, List<MileageModel> body = null, string lang = null);

        /// <summary>
        /// Create Mileage
        /// </summary>
        /// <remarks>
        /// Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </remarks>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseResultModelArray)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResponseResultModelArray>> V4PostMileageAsyncWithHttpInfo (string contentType, string authorization, List<MileageModel> body = null, string lang = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class MileageApi : IMileageApi
    {
        private AccelaMiscellanous.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MileageApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MileageApi(String basePath)
        {
            this.Configuration = new AccelaMiscellanous.Client.Configuration { BasePath = basePath };

            ExceptionFactory = AccelaMiscellanous.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MileageApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public MileageApi(AccelaMiscellanous.Client.Configuration configuration = null)
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
        /// Create Mileage Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ResponseResultModelArray</returns>
        public ResponseResultModelArray V4PostMileage (string contentType, string authorization, List<MileageModel> body = null, string lang = null)
        {
             ApiResponse<ResponseResultModelArray> localVarResponse = V4PostMileageWithHttpInfo(contentType, authorization, body, lang);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create Mileage Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>ApiResponse of ResponseResultModelArray</returns>
        public ApiResponse< ResponseResultModelArray > V4PostMileageWithHttpInfo (string contentType, string authorization, List<MileageModel> body = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling MileageApi->V4PostMileage");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling MileageApi->V4PostMileage");

            var localVarPath = "/v4/mileage";
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

            if (lang != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter
            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body; // byte array
            }


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4PostMileage", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseResultModelArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseResultModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseResultModelArray)));
        }

        /// <summary>
        /// Create Mileage Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ResponseResultModelArray</returns>
        public async System.Threading.Tasks.Task<ResponseResultModelArray> V4PostMileageAsync (string contentType, string authorization, List<MileageModel> body = null, string lang = null)
        {
             ApiResponse<ResponseResultModelArray> localVarResponse = await V4PostMileageAsyncWithHttpInfo(contentType, authorization, body, lang);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Create Mileage Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 
        /// </summary>
        /// <exception cref="AccelaMiscellanous.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="body">Mileage request information (optional)</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>Task of ApiResponse (ResponseResultModelArray)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResponseResultModelArray>> V4PostMileageAsyncWithHttpInfo (string contentType, string authorization, List<MileageModel> body = null, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400, "Missing required parameter 'contentType' when calling MileageApi->V4PostMileage");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling MileageApi->V4PostMileage");

            var localVarPath = "/v4/mileage";
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

            if (lang != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null) localVarHeaderParams.Add("Content-Type", this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter
            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body; // byte array
            }


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4PostMileage", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResponseResultModelArray>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ResponseResultModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseResultModelArray)));
        }

    }
}