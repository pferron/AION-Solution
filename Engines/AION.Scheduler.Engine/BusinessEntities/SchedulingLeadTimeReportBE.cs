#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - SchedulingLeadTimeReportBE

    [DataContract]
    public class SchedulingLeadTimeReportBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? SchedulingLeadTimeReportId { get; set; }

        [DataMember]
        public DateTime? ReportGeneratedOn { get; set; }

        [DataMember]
        public int? ProjectTypeRefId { get; set; }

        [DataMember]
        public int? BusinessDivisionRefId { get; set; }

        [DataMember]
        public int? RequiredProjectHours { get; set; }

        [DataMember]
        public int? LeadTimeDays { get; set; }

        [DataMember]
        public DateTime? DateRangeStartDate { get; set; }

        [DataMember]
        public DateTime? DateRangeEndDate { get; set; }

        [DataMember]
        public string ProjectLevelTxt { get; set; }

        #endregion

    }

    #endregion

}