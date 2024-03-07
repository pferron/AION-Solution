#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - DefaultEstimationHoursBE

    [DataContract]
    public class DefaultEstimationHoursBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? DefaultEstimationHoursId { get; set; }

        [DataMember]
        public decimal? DefaultHoursNbr { get; set; }

        [DataMember]
        public int? BusinessRefId { get; set; }

        [DataMember]
        public int? ProjectTypeRefId { get; set; }
        
        [DataMember]
        public int? Enabled { get; set; }

        [DataMember]
        public string EstimationHrsTxt { get; set; }
        
        #endregion

    }

    #endregion

}