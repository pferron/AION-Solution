using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using System.Collections.Generic;
using System.Data;

namespace AION.AIONDB.Engine
{
    public interface IAIONDBEngine
    {

        #region Posse to AION db Crud

        void InsertNewAIONAccelaOutBoundLog(AIONOutBoundLog logdata);

        List<MeckAccelaDataMap> SelectPosseAccelaMapforFees();

        // List<MeckAccelaDataMap> SelectPosseAccelaMap();

        //  List<MeckAccelaDataMap> SelectPosseAccelaMapByRecordType(string recordType);
        //  bool DeletePosseAccelaMap(string recorddata);

        //  bool InsertAccelaPosseDataLog(AccelaPosseLoging accela_posse_log);

         

        #endregion

        #region AION to AION DB CRUD  API

 bool DeleteRecordsAndResetTable(string sourceSystem);

        AIONRecordQueueResponse InsertNewAIONRecord(RecordNotification inComingRecordData);

        AIONPlanReviewHistoryResponse InsertNewAIONPlanReviewHistoryRecord(PlanReviewHistory inComingRecordData);

        AIONAEDataResponse InsertNewAEData(AccelaAIONAEData receivedRecord);

        List<AIONQueueRecordBE> GetNewAIONRecordsToProcess(string status = "", int offsetMinutes = 0);

        AIONQueueRecordBE GetSpecificAIONQueueRecord(int ReceivedQueueId);

        int UpDateQueueRecordStatus(int ReceivedQueueId, string NewProcessStatus);

        List<PlanReviewHistoryAllFields> AuditPlanReviewRecords();

        bool DeleteAccelaToAionMap(string datavalue);

        List<MeckAccelaDataMap> SelectAccelaAionMap();

        bool InsertAccelaAIONMapRecord(LoadAccelaAIONMap newRecord);

        DataTable SelectAccelaAionMapDataTable();

        List<MeckAccelaDataMap> SelectAccelaAionMapByRecordType(string recordType);

        DataTable SelectAccelaAionMapDataTableByAccerlaRecordType(string recordType);


        List<AccelaAIONAEData> GetAEDataByRecordId(string recordId);

        bool DeleteAERecordsByRecordId(string recordId);

        #endregion
    }

}
