using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Meck.Azure
{
    public class KeyVaultUtility
    {
        public static string EncryptedSecret { get; set; }

        private static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(ConfigurationManager.AppSettings["KeyVaultApplicationId"],
                        ConfigurationManager.AppSettings["KeyVaultApplicationPassword"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        public static string GetSecret(string keyVaultSecretIdentifier)
        {
            try
            {
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
                var secret = keyVaultClient.GetSecretAsync(ConfigurationManager.AppSettings[keyVaultSecretIdentifier]);

                EncryptedSecret = secret.Result.Value;

                return EncryptedSecret;
            }
            catch
            {

            //if key is not found method throws exception instead of returning null or "". So catches that explicitly. 
            // TBD: find alternative for this. As of now not able to find a property to check status.
                return "";
            }
        }
    }
}
