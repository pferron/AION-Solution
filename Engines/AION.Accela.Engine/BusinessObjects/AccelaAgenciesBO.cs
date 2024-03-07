
using AccelaAgencies.Api;
using AION.Accela.Engine.BusinessObjects;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AION.Accela.Engine.Models
{
    public class AccelaAgencyApiBO : AccelaBase
    {

        public async Task<AgencyBE> TaskGetAllAgenciesInfo(string AgencyName)
        {
            // This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            AgenciesApi mAgencies = new AgenciesApi();

            var result = await mAgencies.V4GetAgenciesAsync(AccelaContentHeaderEncoding, _mAccelaTokenBE.access_token, _mAccelaClientId, AgencyName);

            var mAgencyList = JsonConvert.SerializeObject(result.Result);

            AgencyBE mAgencyBE = JsonConvert.DeserializeObject<AgencyBE>(mAgencyList);

            return mAgencyBE;

        }
    }
}
