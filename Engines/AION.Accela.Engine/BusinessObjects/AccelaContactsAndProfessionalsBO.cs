using AccelaContactsAndProfessionals.Api;
using AccelaSettings.Api;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaContactsAndProfessionalsBO : AccelaBase
    {

        /// <summary>
        ///  GetAllTradesNames
        /// </summary>
        public async Task<SettingsWrapperBE> TaskGetAllTradesNames()
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            //
            //  v4/settings/professionals/types
            // 

            ISettingsProfesssionalsApi mSettingsEngine = new SettingsProfesssionalsApi();

            var TradesResult = await mSettingsEngine.V4GetSettingsProfessionalsTypesAsync(AccelaContentHeaderEncoding, _mAccelaToken);

            SettingsWrapperBE ProfTitles = JsonConvert.DeserializeObject<SettingsWrapperBE>(JsonConvert.SerializeObject(TradesResult));

            return ProfTitles;
        }


        /// <summary>
        ///  GetAllTradesList ()
        /// </summary>
        /// <returns></returns>
        public async Task<TradeWrapperBE> TaskGetAllTradesList()
        {
            IProfessionalsApi mProfessionalApi = new ProfessionalsApi();

            try
            {
                // This does everything needed to get a token.  
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();


                var ProfessionalsResult = await mProfessionalApi.V4GetProfessionalsAsync(ApplicationWwwFormUrlEncoded, tokendata.access_token);

                TradeWrapperBE result =
                   JsonConvert.DeserializeObject<TradeWrapperBE>(JsonConvert.SerializeObject(ProfessionalsResult));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Accela failure getting Professionals records.:" + ex.Message, ex.InnerException);
            }
        }


    }
}
