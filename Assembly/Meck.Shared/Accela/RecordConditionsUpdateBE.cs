using System;

namespace Meck.Shared.Accela
{
    public  class RecordConditionsUpdateBE
    {
        public RecordConditionsUpdateBEActionbydepartment actionbyDepartment { get; set; }
        public RecordConditionsUpdateBEActionbyuser actionbyUser { get; set; }
        public RecordConditionsUpdateBEActivestatus activeStatus { get; set; }
        public string additionalInformation { get; set; }
        public string additionalInformationPlainText { get; set; }
        public DateTime appliedDate { get; set; }
        public RecordConditionsUpdateBEAppliedbydepartment appliedbyDepartment { get; set; }
        public RecordConditionsUpdateBEAppliedbyuser appliedbyUser { get; set; }
        public string dispAdditionalInformationPlainText { get; set; }
        public bool displayNoticeInAgency { get; set; }
        public bool displayNoticeInCitizens { get; set; }
        public bool displayNoticeInCitizensFee { get; set; }
        public int displayOrder { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime expirationDate { get; set; }
        public RecordConditionsUpdateBEGroup group { get; set; }
        public int id { get; set; }
        public RecordConditionsUpdateBEInheritable inheritable { get; set; }
        public bool isIncludeNameInNotice { get; set; }
        public bool isIncludeShortCommentsInNotice { get; set; }
        public string longComments { get; set; }
        public string name { get; set; }
        public RecordConditionsUpdateBEPriority priority { get; set; }
        public string publicDisplayMessage { get; set; }
        public string resAdditionalInformationPlainText { get; set; }
        public string resolutionAction { get; set; }
        public string serviceProviderCode { get; set; }
        public RecordConditionsUpdateBESeverity severity { get; set; }
        public string shortComments { get; set; }
        public RecordConditionsUpdateBEStatus status { get; set; }
        public DateTime statusDate { get; set; }
        public string statusType { get; set; }
        public RecordConditionsUpdateBEStatusType type { get; set; }
    }

    
    public class RecordConditionsUpdateBEActionbydepartment
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEActionbyuser
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEActivestatus
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEAppliedbydepartment
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEAppliedbyuser
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEGroup
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEInheritable
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEPriority
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBESeverity
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class RecordConditionsUpdateBEStatus
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public  class RecordConditionsUpdateBEStatusType
    {
        public string text { get; set; }
        public string value { get; set; }
    }

}
