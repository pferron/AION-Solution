using AION.Base;
using System;
using System.Runtime.Serialization;

namespace AION.Estimator.Engine.BusinessEntities
{
    [DataContract]
    public class FacilitatorBE 
    {
        [DataMember]
        public int? UserID { get; set; }

        [DataMember]
        public string FirstNm { get; set; }

        [DataMember]
        public string LastNm { get; set; }

        [DataMember]
        public double AssignedProjectsHours { get; set; }

     
    }
}
