using AION.Base;
using System;
using System.Runtime.Serialization;

namespace AION.Engine.BusinessEntities
{
    [DataContract]
    public class ProjectListBE
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public int? ProjectType { get; set; }

        [DataMember]
        public string Facilitator { get; set; }

        [DataMember]
        public int ProjectStatus { get; set; }

        [DataMember]
        public DateTime? TentativeStartDate { get; set; }
        [DataMember]
        public DateTime? AcceptanceDeadLine { get; set; }
        [DataMember]
        public bool? Payment { get; set; }

    }
}
