using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Base
{
    public class BaseController : Controller
    {
        #region Protected Methods
        protected static T TryToParse<T>(object objectValue)
        {
            //Integer
            if (typeof(T) == typeof(int))
            {
                int intValue;
                return Int32.TryParse(System.Convert.ToString(objectValue), out intValue) ? (T)Convert.ChangeType(intValue, typeof(object)) : default(T);

            }

            //Nullable Integer
            if (typeof(T) == typeof(int?))
            {
                int intValue;
                return Int32.TryParse(System.Convert.ToString(objectValue), out intValue) ? (T)Convert.ChangeType(intValue, typeof(object)) : default(T);

            }

            //Nullable Boolean
            else if (typeof(T) == typeof(bool?))
            {
                bool booleanValue;
                return Boolean.TryParse(System.Convert.ToString(objectValue), out booleanValue) ? (T)Convert.ChangeType(booleanValue, typeof(object)) : default(T);
            }

            //Nullable Decimal
            else if (typeof(T) == typeof(decimal?))
            {
                decimal decimalValue;
                return Decimal.TryParse(System.Convert.ToString(objectValue), out decimalValue) ? (T)Convert.ChangeType(decimalValue, typeof(object)) : default(T);
            }

            //Nullable DateTime
            else if (typeof(T) == typeof(DateTime?))
            {
                DateTime dateTimeValue;
                return DateTime.TryParse(System.Convert.ToString(objectValue), out dateTimeValue) ? (T)Convert.ChangeType(dateTimeValue, typeof(object)) : default(T);
            }

            //String
            else if (typeof(T) == typeof(string))
            {
                return !(objectValue == DBNull.Value) ? (T)Convert.ChangeType(objectValue, typeof(T)) : default(T);
            }

            //Default
            else
                return default(T);
        }

        //NLIU Added 20200527: handle httpclient curruption
        public class HttpClientWrapper
        {
            public HttpClientWrapper()
            {

                ExpiresOn = DateTimeOffset.UtcNow;
            }
            public HttpClient HttpClient;
            public DateTimeOffset ExpiresOn;
        }

        // MS#120042424005119 NLIU 2020-05-20
        // https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/
        // share httpclient to avoid socket exhaustion issue happened April 2020. 
        private static HttpClientWrapper clientWrapper = new HttpClientWrapper();

        /// <summary>
        /// Gets an instance of HttpClient. DO NOT USE THIS METHOD along with a using statement since using will dispose the object.
        /// </summary>
        /// <returns></returns>
        protected static async Task<HttpClient> GetManagerHttpClient()
        {
            try
            {
                lock (clientWrapper)
                {
                    if (clientWrapper.HttpClient == null || clientWrapper.ExpiresOn.Subtract(DateTimeOffset.UtcNow).Minutes < 2) // give 2 minues buffer in case there is delay in network or sleep code
                    {
                        var client = new HttpClient();

                        var authContext = new AuthenticationContext(ConfigurationManager.AppSettings["ida:ManagerAADInstance"] + ConfigurationManager.AppSettings["ida:ManagerTenantId"]);
                        var credentials = new ClientCredential(ConfigurationManager.AppSettings["ida:ManagerClientId"], ConfigurationManager.AppSettings["ida:ManagerClientSecret"]);
                        var authResult = authContext.AcquireTokenAsync(ConfigurationManager.AppSettings["ManagerWebApiAppIdUri"], credentials);
                        authResult.Wait();

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        //api gateway configuraiton
                        string gatewayKeyHeader = ConfigurationManager.AppSettings["GatewayKeyHeader"];
                        string gatewayKey = "";
                        if (System.Web.HttpContext.Current != null)
                            gatewayKey = System.Web.HttpContext.Current.Application["KeyVaultGatewayKey"].ToString();
                        else //Failsafe code incase System.Web.HttpContext.Current get null. Cost additional for calling keyvault.
                            gatewayKey = Meck.Azure.KeyVaultUtility.GetSecret("KeyVaultGatewayKey");
                        client.DefaultRequestHeaders.Add(gatewayKeyHeader, gatewayKey);

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Result.AccessToken);
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ManagerWebApiUri"]);

                        clientWrapper.HttpClient = client;
                        clientWrapper.ExpiresOn = authResult.Result.ExpiresOn;
                    }
                }

                // make this async to keep original signature
                return await Task.Run(() =>
                {
                    return clientWrapper.HttpClient;
                });
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw;
            }
        }

        protected ActionResult ErrorAction(String message)
        {
            return new RedirectResult("/Error?message=" + message);

        }

        #endregion

    }

}
