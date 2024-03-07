using System;

namespace Meck.Shared.Accela
{
    public  class RecordConditionsInsertBE
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public RecordConditionModelActionbyDepartment actionbyDepartment { get; set; }
        public RecordConditionModelActionbyUser actionbyUser { get; set; }
        public RecordConditionModelActiveStatus activeStatus { get; set; }
        public string additionalInformation { get; set; }
        public string additionalInformationPlainText { get; set; }
        public DateTime appliedDate { get; set; }
        public RecordConditionModelAppliedbyDepartment appliedbyDepartment { get; set; }
        public RecordConditionModelAppliedbyUser appliedbyUser { get; set; }
        public string dispAdditionalInformationPlainText { get; set; }
        public bool displayNoticeInAgency { get; set; }
        public bool displayNoticeInCitizens { get; set; }
        public bool displayNoticeInCitizensFee { get; set; }
        public int displayOrder { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime expirationDate { get; set; }
        public RecordConditionModelGroup group { get; set; }
        public int id { get; set; }
        public RecordConditionModelInheritable inheritable { get; set; }
        public bool isIncludeNameInNotice { get; set; }
        public bool isIncludeShortCommentsInNotice { get; set; }
        public string longComments { get; set; }
        public string name { get; set; }
        public RecordConditionModelPriority priority { get; set; }
        public string publicDisplayMessage { get; set; }
        public string resAdditionalInformationPlainText { get; set; }
        public string resolutionAction { get; set; }
        public string serviceProviderCode { get; set; }
        public RecordConditionModelSeverity severity { get; set; }
        public string shortComments { get; set; }
        public RecordConditionModelStatus status { get; set; }
        public DateTime statusDate { get; set; }
        public string statusType { get; set; }
        public RecordConditionModelType type { get; set; }
    }

}
