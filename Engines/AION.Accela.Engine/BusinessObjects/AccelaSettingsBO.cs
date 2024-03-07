
using AccelaSettings.Api;
using AccelaSettings.Model;
using Meck.Shared.Accela;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaSettingsBO : AccelaBase
    {
        /// <summary>
        ///   GetAllTradesNameList ()
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> TaskGetAllTradesNameList()
        {
            // const string _cUriMethod = "/v4/settings/professionals/types ";
            //const string _cExpandValues =
            //    "expand=%22addresses,parcels,professionals,contacts,owners,customForms,customTables,assets,assessments,inspections,payments,%22%20";
            try
            {
                // This does everything needed to get a token.  
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                ISettingsContactsApi mSettingsApi = new SettingsContactsApi();

                var mTradeTypeNamesResult = await mSettingsApi.V4GetSettingsContactsTypesAsync(AccelaContentHeaderEncoding, tokendata.access_token);

                SettingsWrapperBE mSettingsResult = new SettingsWrapperBE();

                foreach (var mSettingValue in mTradeTypeNamesResult.Result)
                {
                    var mTradeNames = JsonConvert.SerializeObject(mSettingValue);
                    var mSettingValuedModelBEData = JsonConvert.DeserializeObject<SettingValueModelBE>(mTradeNames);

                    mSettingsResult.SettingTypes.Add(mSettingValuedModelBEData);
                }
                return mSettingsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        ///  GetAllDepartmentsList ()
        /// </summary>
        /// <returns></returns>
        public async Task<DepartmentWrapperBE> TaskGetAllDepartmentsList()
        {
            try
            {
                // This does everything needed to get a token. 
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                // now work with department details.
                ISettingsDepartmentsApi mSettings = new SettingsDepartmentsApi();

                var settingtypesresult = await mSettings.V4GetSettingsDepartmentsAsync(AccelaContentHeaderEncoding, tokendata.access_token);

                DepartmentWrapperBE mDepartments = new DepartmentWrapperBE();

                foreach (var mtdepartment in settingtypesresult.Result)
                {
                    DepartmentBE mtdepartmentbe = new DepartmentBE()
                    {
                        Agency = mtdepartment.Agency,
                        Bureau = mtdepartment.Bureau,
                        Division = mtdepartment.Division,
                        Group = mtdepartment.Group,
                        Id = mtdepartment.Id,
                        Office = mtdepartment.Office,
                        Section = mtdepartment.Section,
                        ServiceProviderCode = mtdepartment.ServiceProviderCode,
                        Text = mtdepartment.Text,
                        Value = mtdepartment.Value
                    };
                    mDepartments.Departmentlist.Add(mtdepartmentbe);
                }
                return mDepartments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// TaskGetUsersForType </summary>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public async Task<DepartmentWrapperBE> TaskGetUsersForType(string UserType)
        {
            try
            {
                // This does// everything needed to get a token. 
                //  AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                //   AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                // now work with department details.
                //   ISettingsDepartmentsApi mSettings = new SettingsDepartmentsApi();

                var mDeptList = await TaskGetUsersForAllDepartments();

                var departments = mDeptList.Departmentlist.FindAll(x => x.Text == UserType);

                return mDeptList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


        public async Task<DepartmentWrapperBE> TaskGetUsersForAllDepartments()
        {
            DepartmentWrapperBE mMasterUserList = new DepartmentWrapperBE();

            string curDept = String.Empty;

            try
            {
                // This does everything needed to get a token. 
                AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
                AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

                // now work with department details.
                ISettingsDepartmentsApi mSettings = new SettingsDepartmentsApi();

                var mDepartmentsResult = await mSettings.V4GetSettingsDepartmentsAsync(AccelaContentHeaderEncoding, tokendata.access_token);

                List<DepartmentBE> mDepartmentList = mMasterUserList.Departmentlist;

                foreach (DepartmentModel tResults in mDepartmentsResult.Result)
                {
                    var mDepartmap = JsonConvert.DeserializeObject<DepartmentBE>(JsonConvert.SerializeObject(tResults));

                    //   mMasterUserList.UserList.AgencyList = mAgencyInfoList;

                    // list is built for 
                    if (curDept != tResults.Id)
                    {
                        curDept = tResults.Id;

                        var settingtypesresult =
                            await mSettings.V4GetSettingsDepartmentsIdStaffsAsync(AccelaContentHeaderEncoding,
                                tokendata.access_token, curDept);
                        if (settingtypesresult.Result != null)
                        {
                            foreach (UserModel muser in settingtypesresult.Result)
                            {
                                DepartmentUserBE result = new DepartmentUserBE()
                                {
                                    CashierId = muser.CashierId,
                                    Email = muser.Email,
                                    EmployeeId = muser.EmployeeId,
                                    FirstName = muser.FirstName,
                                    FullName = muser.FullName,
                                    Id = muser.Id,
                                    Initial = muser.Initial,
                                    LastName = muser.LastName,
                                    MiddleName = muser.MiddleName,
                                    Namesuffix = muser.Namesuffix,
                                    Password = muser.Password,
                                    Phone = muser.Phone,
                                    ServiceProviderCode = muser.ServiceProviderCode,
                                    Title = muser.Title,
                                    //  UserGroups = muser.UserGroups,
                                    Value = muser.Value
                                };

                                mDepartmap.DepartmentUsers.Add(result);
                            }
                        }
                    }
                    mDepartmentList.Add(mDepartmap);

                }
                return mMasterUserList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// TaskGetAllCategories
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> TaskGetAllCategories()
        {
            // This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // now work with department details.
            ISettingsDocumentsApi mSettings = new SettingsDocumentsApi();

            var docuCatsResult = await mSettings.V4GetSettingsDocumentsCategoriesAsync(AccelaContentHeaderEncoding,
                tokendata.access_token);
            SettingsWrapperBE msettings = new SettingsWrapperBE();

            msettings.CatalogTypes = new List<CatalogDocumentTypeModelGroups>();

            foreach (AccelaSettings.Model.DocumentTypeModel mcatalog in docuCatsResult.Result)
            {
                //   ResponseDocumentTypeModelArray  
                CatalogDocumentTypeModelGroups mcataloggroups = new CatalogDocumentTypeModelGroups(mcatalog.Text, mcatalog.Value);

                msettings.CatalogTypes.Add(mcataloggroups);
            }

            return msettings;

        }

        /// <summary>
        /// TaskGetAllDocumentGroups
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> TaskGetAllDocumentGroups()
        {
            // This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // now work with department details.
            ISettingsDocumentsApi mSettings = new SettingsDocumentsApi();

            var docuCatsResult = await mSettings.V4GetSettingsDocumentsFolderGroupsAsync(AccelaContentHeaderEncoding, tokendata.access_token, "true");

            SettingsWrapperBE msettings = new SettingsWrapperBE();

            msettings.FolderGroups = new List<SettingsFolderGroupModel>();

            if (docuCatsResult.Result != null)
            {
                foreach (AccelaSettings.Model.FolderGroupModel mfolder in docuCatsResult.Result)
                {
                    if (mfolder.IsActive == AccelaSettings.Model.FolderGroupModel.IsActiveEnum.Y)
                    {
                        SettingsFolderGroupModel mgroup = new SettingsFolderGroupModel(mfolder.Description, mfolder.Id,
                            SettingsFolderGroupModel.IsActiveEnum.Y, mfolder.Name, mfolder.Type);
                        msettings.FolderGroups.Add(mgroup);
                    }
                    else
                    {
                        SettingsFolderGroupModel mgroup = new SettingsFolderGroupModel(mfolder.Description, mfolder.Id,
                            SettingsFolderGroupModel.IsActiveEnum.N, mfolder.Name, mfolder.Type);
                        msettings.FolderGroups.Add(mgroup);
                    }
                }
            }
            return msettings;
        }

        /// <summary>
        /// TaskGetAllProfessionalLicenseTypes()
        /// </summary>
        /// <returns></returns>
        public async Task<SettingsWrapperBE> TaskGetAllProfessionalLicenseTypes()
        {// This does everything needed to get a token. 
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // now work with department details.
            ISettingsProfesssionalsApi mSettings = new SettingsProfesssionalsApi();

            var result = await mSettings.V4GetSettingsProfessionalsTypesAsync(AccelaContentHeaderEncoding, tokendata.access_token);

            SettingsWrapperBE mSettingsResult = new SettingsWrapperBE();

            mSettingsResult.SettingTypes = new List<SettingValueModelBE>();

            foreach (var mSettingDetail in result.Result)
            {
                SettingValueModelBE mNewSettingDetail =
                    JsonConvert.DeserializeObject<SettingValueModelBE>(JsonConvert.SerializeObject(mSettingDetail));

                mSettingsResult.SettingTypes.Add(mNewSettingDetail);
            }
            return mSettingsResult;
        }

        /// <summary>
        /// TaskGetCustomFormsByRecordType
        /// </summary>
        /// <param name="RecordTypeName"></param>
        /// <returns></returns>
        public async Task<ResponseCustomFormMetadataModelArrayBE> TaskGetCustomFormsByRecordType(string RecordTypeName)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // now work with department details.
            ISettingsRecordsApi mSettings = new SettingsRecordsApi();

            // var result = await mSettings.V4GetSettingsRecordsTypesIdCustomFormsAsync(AccelaContentHeaderEncoding, tokendata.access_token, RecordTypeName,null,null);

            var result = await mSettings.V4GetSettingsRecordsTypesIdCustomFormsAsync(AccelaContentHeaderEncoding, tokendata.access_token, RecordTypeName, null, null);

            ResponseCustomFormMetadataModelArrayBE mResponseCustomFormMetadataModelArrayBE =
                        JsonConvert.DeserializeObject<ResponseCustomFormMetadataModelArrayBE>(result.ToJson());

            return mResponseCustomFormMetadataModelArrayBE;
        }

        /// <summary>
        ///  TaskGetCustomTablesByRecordType
        /// </summary>
        /// <param name="RecordTypeName"></param>
        /// <returns></returns>
        public async Task<ResponseCustomTableMetadataModelArrayBE> TaskGetCustomTablesByRecordType(string RecordTypeName)
        {
            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();
            AccelaTokenBE tokendata = await mAuth.TaskGetAuthToken();

            // now work with department details.
            ISettingsRecordsApi mSettings = new SettingsRecordsApi();

            // var result = await mSettings.V4GetSettingsRecordsTypesIdCustomFormsAsync(AccelaContentHeaderEncoding, tokendata.access_token, RecordTypeName,null,null);

            var result = await mSettings.V4GetSettingsRecordsTypesIdCustomTablesAsync(AccelaContentHeaderEncoding, tokendata.access_token, RecordTypeName, null, null);

            ResponseCustomTableMetadataModelArrayBE mResponseCustomFormMetadataModelArrayBE =
                JsonConvert.DeserializeObject<ResponseCustomTableMetadataModelArrayBE>(JsonConvert.SerializeObject(result));

            return mResponseCustomFormMetadataModelArrayBE;
        }
    }
}
