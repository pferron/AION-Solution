using AccelaRecords.Api;
using AccelaRecords.Model;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaRecordCreationBO : AccelaBase
    {

        public async Task<ResponseSimpleRecordModelBE> TaskCreatePartialAccelaRecord(RequestRecordModelBE newRecordBody)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            var tokenresult = mAuth.TaskGetAuthToken();
            tokenresult.Wait();

            var token = tokenresult.Result;

            IRecordsApi mAccelaRecordApi = new RecordsApi();

            RequestRecordModel newbody = new RequestRecordModel();
            // do not send Custom Forms and Custom table yet. 
            //  MapNewBody from NewRecord 


            //  ResponseSimpleRecordModel V4PostRecordsInitialize (string contentType, tring authorization, RequestRecordModel body, bool? isFeeEstimate = null, string fields = null, string lang = null);

            var result =
                mAccelaRecordApi.V4PostRecordsInitializeAsync(AccelaContentHeaderEncoding, token.access_token, newbody, false, null, null);
            result.Wait();
            var mInitRecResult = result.Result;

            var mNewRecInit = mInitRecResult.Result;

            var newRecStatus = mNewRecInit.Status.Value;

            if (newRecStatus == "OK")
            {
                // Send CustomForms
                // Send CustomTables
                // Finalize with record parameters only. 

                // public async System.Threading.Tasks.Task<ResponseSimpleRecordModel> V4PostRecordsRecordIdFinalizeAsync (string contentType, string authorization, string recordId, RequestRecordModel body, string fields = null, string lang = null)

                //        var result =
                //          mAccelaRecordApi.V4PostRecordsRecordIdFinalizeAsync(AccelaContentHeaderEncoding, token.access_token, mNewRecInit.Id, newbody,null,null );

            }


            var mAddRecord = JsonConvert.SerializeObject(mInitRecResult);

            ResponseSimpleRecordModelBE mResponseSimpleRecordModelBe =
                JsonConvert.DeserializeObject<ResponseSimpleRecordModelBE>(mAddRecord);

            return mResponseSimpleRecordModelBe;
        }
    }
}
