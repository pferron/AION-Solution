using AION.Accela.Engine;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AION.BL.Test.MockRepositories
{
    public class MockAccelaEngine : IAccelaEngine
    {
        public Task<ResponseResultModelArrayBE> CreateRecordConditions(string recordId, RecordConditionsInsertBE mRecConditions)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResultModelArrayBE> DeleteRecordConditions(string recordId, string mRecConditonIds)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAccelaAddress(string projectnumber)
        {
            throw new NotImplementedException();
        }

        public Task<AccelaRecordModel> GetAccelaRecord(string recordId, bool traceData = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<RecordContactSimpleModelBO>> GetAccelaRecordContacts(string recordId)
        {
            throw new NotImplementedException();
        }

        public Task<AccelaTableResult> GetAccelaRecordWorkFlowTask(string recordId)
        {
            throw new NotImplementedException();
        }


        public Task<string> GetAccelaWorkFlowHistoryforRecord(string recordId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAccelaWorkFlowHistoryObjectForRecord(string recordId)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentWrapperBE> GetAllAgencyList()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentWrapperBE> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public Task<SettingsWrapperBE> GetAllProfessionalLicenseTypes()
        {
            throw new NotImplementedException();
        }

        public Task<SettingsWrapperBE> GetAllTradesNameList()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentWrapperBE> GetAllUsersWithDepartment()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetContactCustomForm(string recordId, long contactId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseRecordConditionModelArrayBE> GetRecordConditions(string recordId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCustomFormSubgroupModelArrayBE> GetRecordRecordCustomTableMeta(string recordId, string CustomTableName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCustomTableMetadataModelArrayBE> GetRecordSettingsCustomTables(string recordTypeName)
        {
            throw new NotImplementedException();
        }

        public Task<RecordWrapperBE> GetRecordsSearch(RecordSearchParametersBE mParms)
        {
            throw new NotImplementedException();
        }

        public Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustForms(string recordId, string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(string recordId, string WorkFlowTaskid)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseRecordRelatedModelArrayBE> GetRelatedRecordInfo(string recordId, RecordRelatedModelBE.RelationshipEnum relationShip)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCustomFormMetadataModelArrayBE> GetSettingsCustomForms(string recordTypeName)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentWrapperBE> GetUsersByType(string UserType)
        {
            throw new NotImplementedException();
        }

        public Task<AccelaTokenBE> TestGetToken()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> UpDateAccelaTasksRecords(AccelaCustomTableTaskUpDateModelBE tasksUpDateDetails)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleRecordModelBE> UpDateRecord(string mRecordId, RecordBE mUpdateRecordDetail)
        {
            throw new NotImplementedException();
        }

        public Task<RecordConditionModelBE> UpDateRecordCondition(string recordId, RecordConditionModelBE recConditions)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> UpDateRecordCustomTables(RequestCustomTablesTasksBE taskUpdatedDta)
        {
            throw new NotImplementedException();
        }
    }
}
