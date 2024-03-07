using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AION.Accela.Engine
{
    public interface IAccelaEngine
    {

        #region Records



        #region Conditions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        Task<string> GetAccelaWorkFlowHistoryforRecord(string recordId);


        /// <summary>
        /// CreateRecordConditions(string recordId, RecordConditionsInsertBE mRecConditions)
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="mRecConditions"></param>
        /// <returns></returns>
        Task<ResponseResultModelArrayBE> CreateRecordConditions(string recordId, RecordConditionsInsertBE mRecConditions);

        /// <summary>
        ///  UpDateRecordCondition(string recordId, RecordConditionModelBE recConditions)
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="recConditions"></param>
        /// <returns></returns>
        Task<RecordConditionModelBE> UpDateRecordCondition(string recordId, RecordConditionModelBE recConditions);

        /// <summary>
        /// TaskDeleteRecordConditions(string recordId, string mRecConditonIds) 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="mRecConditonIds"></param>
        /// <returns></returns>
        Task<ResponseResultModelArrayBE> DeleteRecordConditions(string recordId, string mRecConditonIds);

        /// <summary>
        ///  GetRecordConditions(string recordId) 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        Task<ResponseRecordConditionModelArrayBE> GetRecordConditions(string recordId);

        /// <summary>
        /// Gets Contacts for a Record 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        Task<List<RecordContactSimpleModelBO>> GetAccelaRecordContacts(string recordId);

        /// <summary>
        ///  Used to obtain the Custom Form for a ContactId. 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        Task<string> GetContactCustomForm(string recordId, long contactId);


        #endregion

        /// <summary>
        /// GetAccelaDocument  returns back unfiltered record from Accela for the documentId
        /// "/v4/records/{ids}"
        /// "/v4/records/{recordId}/addresses"
        /// "/v4/records/{recordId}/contacts"
        /// "/v4/records/{recordId}/professionals"
        /// "includes all assigned and completed addresses,parcels,professionals,contacts,owners,customForms,customTables,assets"
        /// ****  This is for reporting and review  only. 
        /// </summary>
        /// <param name="docuementID"></param>
        /// <returns>json results string</returns>
        Task<AccelaRecordModel> GetAccelaRecord(string recordId, bool traceData = false);

        /// <summary>
        /// GetAccelaDocument  returns back unfiltered record from Accela for the documentId
        /// "/v4/records/{ids}"
        /// "includes addresses,parcels,professionals,contacts,owners,customForms,customTables,assets"
        /// "/V4/records/{recordId}/WorkFlowTasks gets all possible WorkFlowTasks for the RecordId by recordType."
        ///  *****    This is to be used for AION Estimation and processing. 
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns>json results string</returns>
        Task<AccelaTableResult> GetAccelaRecordWorkFlowTask(string recordId);

        /// <summary>
        ///   GetRecordRecordCustomTableMeta Gets the Customtable that is required for Task updating. 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="CustomTableName"></param>
        /// <returns></returns>
        Task<ResponseCustomFormSubgroupModelArrayBE> GetRecordRecordCustomTableMeta(string recordId,
            string CustomTableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recorId"></param>
        /// <returns></returns>
        Task<string> GetAccelaWorkFlowHistoryObjectForRecord(string recordId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="relationShip">  Must be one of {child, parent, renewal}  </param>
        /// <returns></returns>
        Task<ResponseRecordRelatedModelArrayBE> GetRelatedRecordInfo(string recordId, RecordRelatedModelBE.RelationshipEnum relationShip);

        /// <summary>
        /// UpDateRecord
        /// "/v4/Records/{id}"
        /// </summary>
        /// <param name="mRecordId"></param>
        /// <param name="mUpdateRecordDetail"></param>
        /// <returns></returns>
        Task<SimpleRecordModelBE> UpDateRecord(string mRecordId, RecordBE mUpdateRecordDetail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustForms(string recordId, string projectId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="WorkFlowTaskid"></param>
        /// <returns></returns>
        Task<WorkTaskCustForms> GetRecordWorkFlowTaskCustomFormUsingWorkFlowTaskID(string recordId, string WorkFlowTaskid);

        /// <summary>
        /// UpDateDRecordWorkFLowItem 
        /// </summary>
        /// <param name="mWorkFlowTaskUpdate"></param>
        /// <returns></returns>
        // Task<ResponseTaskItemModelBE> UpDateRecordWorkFlowItem(AccelaRecordCustomTableTaskInsertBE mWorkFlowTaskUpdate);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="mWorkFlowTaskUpdate"></param>
        ///// <returns></returns>
        Task<List<string>> UpDateRecordCustomTables(RequestCustomTablesTasksBE taskUpdatedDta);

        /// <summary>
        /// GetRecordsSearch
        /// </summary>
        /// <param name="mParms"></param>
        /// <returns></returns>
        Task<RecordWrapperBE> GetRecordsSearch(RecordSearchParametersBE mParms);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasksUpDateDetails"></param>
        /// <returns></returns>
        Task<List<string>> UpDateAccelaTasksRecords(AccelaCustomTableTaskUpDateModelBE tasksUpDateDetails);

        #endregion

        #region Utility methods

        /// <summary>
        /// Gets a TestToken.  Can be used to verify communications
        /// /oauth2/token
        /// </summary>
        /// <returns>AccelaTokenBE)</returns>
        Task<AccelaTokenBE> TestGetToken();


        /// <summary>
        /// GetAccelaAddress
        /// </summary>
        /// <param name="projectnumber"> this must be the Accela address Id of the address to find from the record object.</param>
        /// <returns></returns>
        Task<string> GetAccelaAddress(string projectnumber);

        //
        //   Real Object Methods
        // 

        /// <summary>
        ///  GetAllAgencyList
        ///   "/v4/settings/departments"
        /// </summary>
        /// <returns></returns>
        Task<DepartmentWrapperBE> GetAllAgencyList();


        /// <summary>
        /// GetUsersByType
        /// "/v4/settings/departments"
        /// </summary>
        /// <param name="UserType"> Value seen in Accela Department types interface.</param>
        /// <returns>UserWrapperBE- List of UseerTypeBE </returns>
        Task<DepartmentWrapperBE> GetUsersByType(string UserType);

        /// <summary>
        /// GetAllUsersWithDepartment
        ///  "/v4/settings/departments"
        ///  /v4/settings/departments/{id}/staffs"
        /// </summary>
        /// <returns></returns>
        Task<DepartmentWrapperBE> GetAllUsersWithDepartment();

        /// <summary>
        /// GetAllTradesNameList
        /// "/v4/settings/contacts/types"
        /// </summary>
        /// <returns></returns>
        Task<SettingsWrapperBE> GetAllTradesNameList();

        /// <summary>
        /// GetAllDepartments
        /// "/v4/settings/departments"
        /// </summary>
        Task<DepartmentWrapperBE> GetAllDepartments();

        /// <summary>
        /// GetAllProfessionalLicenseTypes
        /// "/v4/settings/professionals/types"
        /// </summary>
        /// <returns></returns>
        Task<SettingsWrapperBE> GetAllProfessionalLicenseTypes();

        #endregion

        #region Settings

        Task<ResponseCustomFormMetadataModelArrayBE> GetSettingsCustomForms(string recordTypeName);
        Task<ResponseCustomTableMetadataModelArrayBE> GetRecordSettingsCustomTables(string recordTypeName);

        #endregion


    }
}