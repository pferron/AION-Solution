using AccelaAddressParcelsOwners.Api;
using Meck.Shared.Accela;
using System;
using System.Threading.Tasks;

namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaAddressBO : AccelaBase
    {
        private AddressesApi mAccelEngine = new AddressesApi();

        public AccelaAddressBO()
        {

        }


        public async Task<string> TaskGetAccelaAddress(string projectnumber)
        {
            string returnvalue = string.Empty;
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            var result = await mAccelEngine.V4GetAddressesAsync(ApplicationWwwFormUrlEncoded, _mAccelaToken, Convert.ToInt64(projectnumber));

            returnvalue = result.ToJson();

            return returnvalue;
        }



    }
}
