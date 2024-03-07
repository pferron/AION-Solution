using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AION.Base.MSGraph
{
    public class GlobalGraphAPI
    {

        public static string GetGlobalGraphAPIAdminAuthToken(string appId, string appSecret, string tenantid)
        {
            TokenResponseData ret = new TokenResponseData();
            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", appId),
                new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
                new KeyValuePair<string, string>("client_secret", appSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };
            ret = PostFormUrlEncoded<TokenResponseData>(@"https://login.microsoftonline.com/" + tenantid + @"/oauth2/v2.0/token", args).Result;
            return ret.access_token;
        }

        public static async Task<Result> PostFormUrlEncoded<Result>(string url, IEnumerable<KeyValuePair<string, string>> postData, string authToken = "",
                IEnumerable<KeyValuePair<string, string>> headerData = null)
        {
            string resultjson = "";
            using (var httpClient = new HttpClient())
            {
                if (authToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    if (headerData != null)
                    {
                        foreach (var item in headerData)
                        {
                            content.Headers.Add(item.Key, item.Value);
                        }
                    }
                    HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                    resultjson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Result>(resultjson);
                }
            }
        }

        public static async Task<string> DeleteWithJsonBodyAndHeaders(string url, string authToken = "",
            IEnumerable<KeyValuePair<string, string>> headerData = null)
        {
            using (var httpClient = new HttpClient())
            {
                string resultjson = "";
                if (authToken != "") //if token is ther then add it. Else assume it is not required for this request.
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;

                resultjson = await response.Content.ReadAsStringAsync();
                return resultjson;
            }
        }

        public static async Task<string> PostWithJsonBodyAndHeaders(string url, string postDataJson, string authToken = "",
     IEnumerable<KeyValuePair<string, string>> headerData = null)
        {
            using (var httpClient = new HttpClient())
            {
                string resultjson = "";
                if (authToken != "") //if token is ther then add it. Else assume it is not required for this request.
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
                if (headerData != null)
                {
                    foreach (var item in headerData)
                    {
                        content.Headers.Add(item.Key, item.Value);
                    }
                }
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                resultjson = await response.Content.ReadAsStringAsync();
                return resultjson;
            }
        }

        public static async Task<string> GetWithJsonAndHeaders(string url, string authToken = "",
             IEnumerable<KeyValuePair<string, string>> headerData = null)
        {
            using (var httpClient = new HttpClient())
            {
                string resultjson = "";
                if (authToken != "") //if token is ther then add it. Else assume it is not required for this request.
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                resultjson = await response.Content.ReadAsStringAsync();
                return resultjson;
            }
        }

    }
}
