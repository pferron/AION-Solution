
using AccelaDocuments.Api;
using AccelaDocuments.Model;
using AccelaRecords.Api;
using Meck.Azure;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;


namespace AION.Accela.Engine.BusinessObjects
{
    partial class AccelaDocumentsBO : AccelaBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentId"></param>
        /// <returns></returns>
        public async Task<DocumentUpDateModelWrapperBE> TaskGetDocumentInfo(string DocumentId)
        {
            try
            {
                // This does everything needed to get a token.  
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                IDocumentsApi mDocuments = new DocumentsApi();

                var result = await mDocuments.V4GetDocumentsDocumentIdsAsync(AccelaContentHeaderEncoding,
                    tokendata.access_token, DocumentId);

                if (result == null || result.Result == null)
                {
                    throw new Exception("Document not Found with Id ");
                }

                DocumentUpDateModelWrapperBE mDocumentData = new DocumentUpDateModelWrapperBE();

                foreach (var mtDocument in result.Result)
                {
                    var tDocDetail = mtDocument.ToJson();

                    DocumentModelBE mtDocDetail = JsonConvert.DeserializeObject<DocumentModelBE>(tDocDetail);

                    mDocumentData.UpDatedModels.Add(mtDocDetail);
                }

                return mDocumentData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// TaskGetDocumentDownLoad
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<InlineResponse200> TaskGetDocumentDownLoad(string documentId)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IDocumentsApi mDocuments = new DocumentsApi();

            var documentResult =
                await mDocuments.V4GetDocumentsDocumentIdDownloadAsync(AccelaContentHeaderEncoding, tokendata.access_token, documentId);


            return documentResult;
        }

        ///// <summary>
        ///// TaskUpDateDocumentByDocumentId
        ///// </summary>
        ///// <param name="DocumentId"></param>
        ///// <param name="FilePath"></param>
        ///// <param name="upDateParms"></param>
        ///// <returns></returns>
        public async Task<DocumentWrapperBE> TaskUpDateDocumentByDocumentId(string DocumentId, string FilePath, DocumentModelBE upDateParms)
        {
            // This does everything needed to get a token.  
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IDocumentsApi mDocumentsApi = new DocumentsApi();

            var keyStoreValues = mAuth.LoadupParmsAndAzureKeyVaultData();

            if (!File.Exists(FilePath))
            {
                throw new Exception("File create operation error : File not found or accessable");
            }

            FileInfo mNewFileInfo = new FileInfo(FilePath);

            FileStream mNewReadStream = new FileStream(FilePath, FileMode.Open);

            byte[] mFileRead = new byte[Convert.ToInt32(mNewFileInfo.Length)];

            await mNewReadStream.ReadAsync(mFileRead, 0, Convert.ToInt32(mNewFileInfo.Length));

            //  System.Threading.Tasks.Task<ResponseDocumentModel>
            // V4PutDocumentsDocumentIdAsync(string contentType, string authorization, string documentId, DocumentModelBE body = null, string lang = null);

            var mUpDateParams = JsonConvert.SerializeObject(upDateParms);

            AccelaDocuments.Model.DocumentModel mupdateDoc = JsonConvert.DeserializeObject<AccelaDocuments.Model.DocumentModel>(mUpDateParams);

            mupdateDoc.FileName = mNewFileInfo.Name;

            mupdateDoc.Id = Convert.ToInt64(DocumentId);
            mupdateDoc.Size = mNewFileInfo.Length;

            var mDocUpdateResult = await mDocumentsApi.V4PutDocumentsDocumentIdAsync("multipart/form-data", tokendata.access_token, DocumentId, mupdateDoc);
            var mUpdatedResult = mDocUpdateResult.Result.ToJson();

            DocumentBE mUpdateRecordDocument = JsonConvert.DeserializeObject<DocumentBE>(mUpdatedResult);

            DocumentWrapperBE nDocWrapper = new DocumentWrapperBE();

            nDocWrapper.Documents.Add(mUpdateRecordDocument);


            return nDocWrapper;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="recordId"></param>
        ///// <param name="documentIds"></param>
        ///// <returns></returns>
        public async Task<ResponseResultModelArrayBE> TaskDeleteRecordDocumentsByIDS(string recordId, string documentIds)
        {

            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsDocumentsApi mAccelaRecordDocuments = new RecordsDocumentsApi();

            var result = await mAccelaRecordDocuments.V4DeleteRecordsRecordIdDocumentsDocumentIdsAsync(AccelaContentHeaderEncoding,
                tokendata.access_token, recordId, documentIds, KeyVaultUtility.GetSecret("AccelaUserId"),
                KeyVaultUtility.GetSecret("AccelaPassword"));

            ResponseResultModelArrayBE mResult =
                JsonConvert.DeserializeObject<ResponseResultModelArrayBE>(JsonConvert.SerializeObject(result.Result));


            return mResult;
        }


        public async Task<RecordWrapperBE> TaskGetRecordDocuments(string RecordId)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            IRecordsDocumentsApi mAccelaRecordDocuments = new RecordsDocumentsApi();

            var docresult = await mAccelaRecordDocuments.V4GetRecordsRecordIdDocumentsAsync(AccelaContentHeaderEncoding, tokendata.access_token, RecordId);

            RecordWrapperBE mRecordDocs = new RecordWrapperBE();

            foreach (var mDocResult in docresult.Result)
            {
                string mdocresult = mDocResult.ToJson();

                var mRecordDoc = JsonConvert.DeserializeObject<RecordDocumentBE>(mdocresult);

                mRecordDocs.RecordDocuments.Add(mRecordDoc);
            }

            return mRecordDocs;

        }
    }
}
