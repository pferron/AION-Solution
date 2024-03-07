using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Configuration;
using Meck.Logging;

namespace Meck.Azure
{
    public class KeyVaultUtility
    {

        #region Properties

        static Logger m_Logger = new Logger();
        public static Logger Logging
        {
            get { return m_Logger; }
            set { m_Logger = value; }
        }

        #endregion

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
                //Adding logging for execution time for every KeyVault call -wkb
                //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
                var secret = keyVaultClient.GetSecretAsync(WebConfigurationManager.AppSettings[keyVaultSecretIdentifier]);
                watch.Stop();
                string message = "KeyVault 'GetSecret' Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to get " + keyVaultSecretIdentifier + " out of KeyVault.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

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
