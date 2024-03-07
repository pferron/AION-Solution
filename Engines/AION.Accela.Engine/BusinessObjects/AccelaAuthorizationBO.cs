using AccelaAuthorization.Api;
using AccelaAuthorization.Model;
using AION.Accela.Engine.Models;
using Meck.Shared;
using Meck.Shared.Accela;
using System;
using System.Threading.Tasks;

namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaAuthorizationBO : AccelaBase
    {


        /// <summary>
        /// This will load up all data needed to create the values needed to create a TokenValue. 
        /// </summary>
        /// <returns></returns>
        public AccelaParmsDetailBE LoadupParmsAndAzureKeyVaultData()
        {
            try
            {
                //  Loadup Key Vault values 
                //  baseUrl = "https://auth.accela.com";
                //  ClientId = "637032903017511136";
                //  ClientSecret = "d6008031844f41388a24f08e14909be6";
                //  Agency
                //  Environment
                //  UserName
                //   password 
                //   code  to load up details needed. 

                AccelaParmsDetailBE _keyDetail = new AccelaParmsDetailBE()
                {
                    baseUrl = Meck.Shared.Globals.AccelaAuthBaseUrl, // GetConfigValue("AccellaAuthbaseUrl"),
                    Agency = Meck.Shared.Globals.AccelaAgency,// GetConfigValue("AccelaAuthAgency"),
                    Environment = Meck.Shared.Globals.AccelaEnvironment, //  GetConfigValue("AccelaEnvironment"),
                    ClientId = Meck.Shared.Globals.AccelaClientId, //  GetConfigValue("AccelaClientID"),

                    ClientSecret = Meck.Shared.Globals.AccelaClientSecret,
                    UserName = Meck.Shared.Globals.AccelaUser,
                    password = Meck.Shared.Globals.AccelaPassword
                };

                //   < add key = "KeyVaultApplicationId" value = "f016504a-2a92-41f9-84b9-febbd1d27166" />
                //   < !--Azure Information: Azure Active Directory / App Registration / Keys / ClientSecret-- >
                //   < add key = "KeyVaultApplicationPassword" value = "EwDEz87mF7TjvRNFDuVpCYktdCpsn2h1W4ulwgr2S48=" />
                //   < !--Azure Information: KeyVault Needed / Secrets / Keys / Secret Needed / Version Needed / Secret Identifier-- >
                //   < add key = "KeyVaultConnectionString" value = "https://meck-sus-keyvault-dev.vault.azure.net/secrets/meck-sus-aion-connection-string/" />
                //   < add key = "AccelaUser" Value = "https://meck-sus-keyvault-dev.vault.azure.net/secrets/meck-sus-accela-client-secret/" />
                //   < add key = "AccelaPassword" Value = "https://meck-sus-keyvault-dev.vault.azure.net/secrets/meck-sus-accela-password/" />

                AccelaUserId = Globals.AccelaUserName;
                AccelaPassword = Globals.AccelaPassword;

                return _keyDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// GetAuthToken
        /// </summary>
        /// <returns>AccelaTokenBE</returns>
        public async Task<AccelaTokenBE> TaskGetAuthToken()
        {

            //  this is used to determine if existing mAccelaTokenBE needs to berefreshed.
            bool needed = false;

            IDefaultApi m_AccelaApi = new DefaultApi();
            try
            {
                // This exposes an object of type AzureKeyVaultDetailBE.
                var keyStoreValues = LoadupParmsAndAzureKeyVaultData();

                if (_mAccelaTokenBE == null)
                    needed = true;

                if (!needed && _mAccelaTokenBE.expiresAt > DateTime.Now)
                {
                    needed = false;
                }
                else
                {
                    needed = true;
                }

                // Thisis the contents being sent 
                /// <param name="client_Id">The app ID value from [Construct Developer Portal](https://developer.accela.com). (required).</param>
                /// <param name="client_Secret">The app secret value from [Construct Developer Portal](https://developer.accela.com). (required).</param>
                /// <param name="grant_Type">Specifies whether the request is for an authorization code, password credential access token, or refresh token. Valid values:   Values:   authorization_code - Request to exchange the given authorization code with an access token. Used with [Authorization Code Flow](../construct-authCodeFlow.html).    password - Request authentication via userid and password credential. See [Password Credential Login](../construct-passwordCredentialLogin.html).    refresh_token - Request to refresh the token.    **Note**: Make sure the grant_type value does not contain any space character. (required).</param>
                /// <param name="code">The authorization code obtained from the preceding [/oauth2/authorize](#operation/oauth2.authorize) request.   **Note**: code is required only when calling this API with grant_type&#x3D;autorhization_code for [Authorization Code Flow](../construct-authCodeFlow.html).    **Note**: The code should be URL-encoded, if you are using tools or libraries which will auto-encode the code, you need to pass the code under decoded.   **Note**: The code can be used no more than one time, the client should apply the rule during exchange access token. (required).</param>
                /// <param name="redirect_Uri">The URI that is used to redirect to the client with an access token.   **Note**: redirect_uri is required only when calling this API with grant_type&#x3D;autorhization_code for [Authorization Code Flow](../construct-authCodeFlow.html).    **Note**: The value of redirect_uri must match the redirect_uri used in the preceding [/oauth2/authorize](#operation/oauth2.authorize) request. (required).</param>
                /// <param name="username">For a **citizen app**, the user name is the Civic ID. For an **agency app**, the user name is the Civic Platform account.   **Note**: username is required only when calling this API with grant_type&#x3D;password for [Password Credential Login](../construct-passwordCredentialLogin.html).  (required).</param>
                /// <param name="password">For a **citizen app**, the user name is the Civic ID password. For an **agency app**, the user name is the Civic Platform password.   **Note**: username is required only when calling this API with grant_type&#x3D;password for [Password Credential Login](../construct-passwordCredentialLogin.html).  (required).</param>
                /// <param name="scope">The scope of the resources that the client requests. Enter a list of APIs scope names separated by spaces. Get the scope names from the [Construct API Reference](./api-index.html).   **Note**: scope is required only when calling this API with grant_type&#x3D;password for [Password Credential Login](../construct-passwordCredentialLogin.html). .</param>
                /// <param name="agency_Name">The agency name defined in [Construct Administrator Portal](https://admin.accela.com). APIs such as [Get All Agencies](./api-agencies.html#operation/v4.get.agencies), [Get Agency](./api-agencies.html#operation/v4.get.agencies.name), and [Search Agencies](./api-search.html#operation/v4.post.search.agencies) return valid agency names.    **Note**: agency_name is used only when calling this API with grant_type&#x3D;password for [Password Credential Login](../construct-passwordCredentialLogin.html). For an **agency app**, agency_name is required. For a **citizen app**, agency_name is optional. (required).</param>
                /// <param name="environment">The Construct environment name, such as \&quot;PROD\&quot; and \&quot;TEST\&quot;. The [Get All Agency Environments](./api-agencies.html#operation/v4.get.agencies.name.environments) API returns a list of configured environments available for a specific agency. The [Get Environment Status](./api-agencies.html#operation/v4.get.agencies.name.environments.env.status) checks connectivity with the Agency/Environment..   **Note**: scope is required only when calling this API with grant_type&#x3D;password for [Password Credential Login](../construct-passwordCredentialLogin.html).  (required).</param>
                /// <param name="refreshToken">The refresh token value obtained in the prior access token API request.   **Note**: refresh_token is required only when calling this API to refresh the token for both [Authorization Code Flow](../construct-authCodeFlow.html) and [Password Credential Login](../construct-passwordCredentialLogin.html). .</param>
                /// <param name="state">An opaque value that the client uses for maintaining the state between the request and callback. Enter a unique value. This can be used for [Cross-Site Request Forgery](http://en.wikipedia.org/wiki/Cross-site_request_forgery) (CSRF) protection.  This parameter is not used when refreshing a token.   **Note**: state is used and optional only when calling this API with grant_type&#x3D;authorization_code for [Authorization Code Flow](../construct-authCodeFlow.html). .</param>


                /// IF no needed then return existing Token. 
                if (needed)
                {
                    var authRequest = new RequestToken(keyStoreValues.ClientId, keyStoreValues.ClientSecret, RequestToken.GrantTypeEnum.Password, null, null, Globals.AccelaUserName,
                         Globals.AccelaPassword, mAccelaScope, Globals.AccelaAgency, Globals.AccelaEnvironment, null, null);

                    var result = await m_AccelaApi.Oauth2TokenAsync(authRequest, AccelaContentHeaderEncoding, authRequest.ClientId);

                    _mAccelaTokenBE = new AccelaTokenBE();

                    _mAccelaTokenBE.access_token = result.AccessToken;
                    _mAccelaTokenBE.appid = keyStoreValues.ClientId;
                    _mAccelaTokenBE.expires_in = Convert.ToInt32(result.ExpiresIn);
                    _mAccelaTokenBE.expiresAt = DateTime.Now.AddSeconds(Convert.ToInt32(result.ExpiresIn));
                    _mAccelaTokenBE.refresh_token = result.RefreshToken;
                    _mAccelaTokenBE.scope = result.Scope;

                    Meck.Shared.Globals.AccelaToken = _mAccelaTokenBE;

                    _mAccelaToken = Meck.Shared.Globals.AccelaToken.access_token;

                }

                return _mAccelaTokenBE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }


        }


    }
}
