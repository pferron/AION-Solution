using AccelaAgencies.Model;
using AccelaAuthorization.Model;
using AccelaContactsAndProfessionals.Model;
using AccelaRecords.Model;
using AccelaSettings.Model;
using AION.Accela.Engine.Models;
using AutoMapper;
using Meck.Shared;
using Meck.Shared.Accela;
using System;
using ResponseDocumentModelArray = AccelaRecords.Model.ResponseDocumentModelArray;
using UserModel = AccelaSettings.Model.UserModel;

namespace AION.Accela.Engine.Helpers
{
    public class MapHelper
    {
        public static IMapper SetupMaps()
        {
            var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<ResponseAgencyArray, AgencyBE>();
                    cfg.CreateMap<AgencyBE, ResponseAgencyArray>();

                    cfg.CreateMap<ResponseRecordModelArray, RecordWrapperBE>();
                    cfg.CreateMap<RecordWrapperBE, ResponseRecordModelArray>();

                    cfg.CreateMap<ResponseDepartmentModelArray, DepartmentWrapperBE>();
                    cfg.CreateMap<DepartmentWrapperBE, ResponseDepartmentModelArray>();

                    cfg.CreateMap<ResponseDocumentModelArray, RecordWrapperBE>();
                    cfg.CreateMap<RecordWrapperBE, ResponseDocumentModelArray>();

                    cfg.CreateMap<RecordAddressModel, AddressBE>();
                    cfg.CreateMap<AddressBE, RecordAddressModel>();


                    cfg.CreateMap<ResponseLicenseModelArray, TradeWrapperBE>();
                    cfg.CreateMap<TradeWrapperBE, ResponseLicenseModelArray>();

                    cfg.CreateMap<ResponseSettingValueModelArray, SettingsWrapperBE>();
                    cfg.CreateMap<SettingsWrapperBE, ResponseSettingValueModelArray>();

                    cfg.CreateMap<ResponseUserModelArray, UserWrapperBE>();
                    cfg.CreateMap<DepartmentInfo, UserWrapperBE>();


                    cfg.CreateMap<ResponseRecordContactSimpleModelArray, ContactWrapperBE>();
                    cfg.CreateMap<ContactWrapperBE, ResponseRecordContactSimpleModelArray>();

                    cfg.CreateMap<ResponseLicenseProfessionalModelArray, ProfessionalWrapperBE>();
                    cfg.CreateMap<ProfessionalWrapperBE, ResponseLicenseProfessionalModelArray>();

                    cfg.CreateMap<AccelaRecords.Model.ResponseDocumentModelArray, DocumentWrapperBE>();
                    cfg.CreateMap<DocumentWrapperBE, AccelaRecords.Model.ResponseDocumentModelArray>();

                    cfg.CreateMap<AccelaRecords.Model.ResultModel, DocumentChangeResultModelBE>();
                    cfg.CreateMap<DocumentChangeResultModelBE, AccelaRecords.Model.ResultModel>();

                    cfg.CreateMap<AccelaDocuments.Model.DocumentModel, DocumentModel>();
                    cfg.CreateMap<DocumentModel, AccelaDocuments.Model.DocumentModel>();

                    cfg.CreateMap<AccelaSettings.Model.ResponseDocumentTypeModelArray, DocumentsCategoriesAndGroupsBE>();
                    cfg.CreateMap<DocumentsCategoriesAndGroupsBE, AccelaSettings.Model.ResponseDocumentTypeModelArray>();

                    cfg.CreateMap<AccelaDocuments.Model.ResponseDocumentModelArray, DocumentUpDateModelWrapperBE>();
                    cfg.CreateMap<DocumentUpDateModelWrapperBE, AccelaDocuments.Model.ResponseDocumentModelArray>();

                    cfg.CreateMap<UserModel, AccelaUserBE>();
                    cfg.CreateMap<AccelaUserBE, UserModel>();

                    cfg.CreateMap<AccelaSettings.Model.DepartmentModel, DepartmentBE>();
                    cfg.CreateMap<DepartmentBE,AccelaSettings.Model.DepartmentModel >();

                });

            return config.CreateMapper();
        }
    }
}
