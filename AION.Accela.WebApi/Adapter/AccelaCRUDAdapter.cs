
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using AION.Base;
using Meck.Logging;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


namespace AION.Accela.WebApi.Adapter
{


    public partial class AccelaCRUDAdapter : BaseBO, IAccelaWebApi
    {
        Logger _mLogger = new Logger();

        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }

        }

        // <summary>
        ///
        /// </summary>
        /// <param name="inComingRecord"></param>
        /// <returns></returns>
        public AIONRecordQueueResponse InsertNewRecordQueue(RecordNotification inComingRecord)
        {
            try
            {

                IAIONDBEngine theengine = new AIONEngineCrudApiBO();

                var sample = JsonConvert.SerializeObject(inComingRecord);

                string startUpDetails = "InsertAionNewRecord data :" + sample;

                var result = theengine.InsertNewAIONRecord(inComingRecord);

                return result;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message, ex: ex));

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inComingData"></param>
        /// <returns></returns>
        public AIONPlanReviewHistoryResponse InsertPlanReviewHistoryRecord(PlanReviewHistory inComingData)
        {
            try
            {
                IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

                var result = mAIONDBEngine.InsertNewAIONPlanReviewHistoryRecord(inComingData);

                return result;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message,
                    ex: ex));

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public List<PlanReviewHistoryAllFields> AuditPlanReviewHistory()
        {
            try
            {
                IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

                var result = Task.Run(() => mAIONDBEngine.AuditPlanReviewRecords());

                return result.Result;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                    ex.Message,
                    ex: ex));

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<AIONQueueRecordBE> GetReceivedRecordsToProcess()
        {
            try
            {
                IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

                var result = Task.Run(() => mAIONDBEngine.GetNewAIONRecordsToProcess());

                return result.Result;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message,
                    ex: ex));

                throw new Exception(ex.Message);
            }
        }

        public AIONAEDataResponse InsertAIONPlanReviewAEData(AccelaAIONAEData aeData)
        {
            try
            {
                IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();
                var resulttask = mAIONDBEngine.InsertNewAEData(aeData);
                //resulttask.Wait();


                return resulttask;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message,
                    ex: ex));

                throw new Exception(ex.Message);
            }
        }
        public List<AccelaAIONAEData> GetAERecordDetailByRecordId(string recordId)
        {
            try
            {
                IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

                var result = mAIONDBEngine.GetAEDataByRecordId(recordId);

                var response = result;

                return response;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;

                Task.Run(() => _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), ex.Message,
                     ex: ex));

                throw new Exception(ex.Message);
            }
        }
    }
}