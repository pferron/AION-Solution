using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meck.Shared.Accela;

namespace AION.Accela.WebApi.Adapter
{
    public interface IAccelaWebApi
    {
        AIONRecordQueueResponse InsertNewRecordQueue(RecordNotification inComingRecord);

        AIONPlanReviewHistoryResponse InsertPlanReviewHistoryRecord(PlanReviewHistory inComingData);

        List<PlanReviewHistoryAllFields> AuditPlanReviewHistory();

        List<AIONQueueRecordBE> GetReceivedRecordsToProcess();

        AIONAEDataResponse InsertAIONPlanReviewAEData(AccelaAIONAEData aeData);

        List<AccelaAIONAEData> GetAERecordDetailByRecordId(string recordId);

    }
}
