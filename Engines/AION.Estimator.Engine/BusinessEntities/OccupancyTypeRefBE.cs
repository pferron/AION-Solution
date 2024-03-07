#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - OccupancyTypeRefBE

    [DataContract]
    public class OccupancyTypeRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? OccupancyTypRefId { get; set; }

        [DataMember]
        public string OccupancyTypName { get; set; }

        #endregion

    }

    #endregion

}