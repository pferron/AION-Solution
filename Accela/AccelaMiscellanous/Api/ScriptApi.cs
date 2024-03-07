using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RestSharp;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;
using AccelaMiscellanous.Api;

namespace AccelaMiscellanous.ScriptApi
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IScriptApi : IApiAccessor
    {
        #region Synchronous Operations

        /// <summary>
        /// V4PostGetDrawingsSealedEms runs Script . **API Endpoint**:  POST /v4/scripts/GET_DRAWINGS_SEALED_EMSEs  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="recordId">The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).</param>
        /// <param name="licenseid">The licensed professional Id to be checked.</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>string</returns>
        string V4PostGetDrawingsSealedEms(string contentType, string authorization, string recordId, string licenseid,
            string lang = null);

        /// <summary>
        /// V4PostGetDrawingsSealedEmsWithHttpInfo **API Endpoint**:  POST /v4/scripts/GET_DRAWINGS_SEALED_EMSEs  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="recordId">The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).</param>
        /// <param name="licenseId">The licensed professional information to be added.</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns> Jason string </returns>
        string V4PostGetDrawingsSealedEmsWithHttpInfo(string contentType, string authorization, string recordId, string licenseId, string lang = null);


        #endregion Synchronous Operations

        #region Asynchronous Operations

        #endregion Asynchronous Operations
    }
    public partial class ScriptApi : IScriptApi
    {
        private AccelaMiscellanous.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ScriptApi(String basePath)
        {
            this.Configuration = new AccelaMiscellanous.Client.Configuration { BasePath = basePath };

            ExceptionFactory = AccelaMiscellanous.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeocodingApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public ScriptApi(AccelaMiscellanous.Client.Configuration configuration = null)
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
        public AccelaMiscellanous.Client.Configuration Configuration { get; set; }

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
        /// V4PostGetDrawingsSealedEms runs Script . **API Endpoint**:  POST /v4/scripts/GET_DRAWINGS_SEALED_EMSEs  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="recordId">The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).</param>
        /// <param name="licenseid">The licensed professional Id to be checked.</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns>string</returns>
        public string V4PostGetDrawingsSealedEms(string contentType, string authorization, string recordId,string licenseid, string lang = null)
        {
            string localVarResponse =
                V4PostGetDrawingsSealedEmsWithHttpInfo(contentType, authorization, recordId, licenseid, lang);
            return localVarResponse;
        }

        /// <summary>
        /// V4PostGetDrawingsSealedEmsWithHttpInfo **API Endpoint**:  POST /v4/scripts/GET_DRAWINGS_SEALED_EMSEs  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="contentType">Must be application/x-www-form-urlencoded.</param>
        /// <param name="authorization">Construct oAuth2 authentication token</param>
        /// <param name="recordId">The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).</param>
        /// <param name="licenseId">The licensed professional information to be added.</param>
        /// <param name="lang">Language parameter to support I18N. Default language is en_US. (optional)</param>
        /// <returns> Jason string </returns>
        public string V4PostGetDrawingsSealedEmsWithHttpInfo(string contentType, string authorization, string recordId, string licenseId, string lang = null)
        {
            // verify the required parameter 'contentType' is set
            if (contentType == null)
                throw new ApiException(400,
                    "Missing required parameter 'contentType' when calling RecordsProfessionalsApi->V4PostGetDrawingsSealedEms");
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400,
                    "Missing required parameter 'authorization' when calling RecordsProfessionalsApi->V4PostGetDrawingsSealedEms");
            // verify the required parameter 'recordId' is set
            if (recordId == null)
                throw new ApiException(400,
                    "Missing required parameter 'recordId' when calling RecordsProfessionalsApi->V4PostGetDrawingsSealedEms");
            // verify the required parameter 'body' is set
            if (licenseId == null)
                throw new ApiException(400,
                    "Missing required parameter 'licenseId' when calling RecordsProfessionalsApi->V4PostGetDrawingsSealedEms");

            /* -----------------------  special processing for input data to APi call --------------- */
            String[] recordIdPart = recordId.Split('-');

            /* build the body */ 
            ProfLicenseDocSignQuery body = new ProfLicenseDocSignQuery(recordIdPart[0], recordIdPart[1],
                recordIdPart[2], licenseId.ToString()); 
            


            var localVarPath = "/v4/scripts/GET_DRAWINGS_SEALED_EMSE";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[]
            {
            };
            String localVarHttpContentType =
                this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[]
            {
            };
            String localVarHttpHeaderAccept =
                this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (recordId != null)
                localVarPathParams.Add("recordId",
                    this.Configuration.ApiClient.ParameterToString(recordId)); // path parameter
            if (lang != null)
                localVarQueryParams.AddRange(
                    this.Configuration.ApiClient.ParameterToKeyValuePairs("", "lang", lang)); // query parameter
            if (contentType != null)
                localVarHeaderParams.Add("Content-Type",
                    this.Configuration.ApiClient.ParameterToString(contentType)); // header parameter
            if (authorization != null)
                localVarHeaderParams.Add("Authorization",
                    this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter
            
             /* get the prebuilt body */  
            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = body.ToJson(); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body;
            }


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams,
                localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("V4PostGetDrawingsSealedEms", localVarResponse);
                if (exception != null) throw exception;
            }

            // return new ApiResponse<ResponseResultModelArray>(localVarStatusCode,
            //    localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
            //    (ResponseResultModelArray) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResponseResultModelArray)));

            return localVarResponse.Content.ToString();

        }
    }
}