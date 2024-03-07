#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectOccupancyTypRelBE

    [DataContract]
    public class ProjectOccupancyTypRelBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ProjectOccupancyTypeRelationshipId { get; set; }

        [DataMember]
        public int? OccupancyTypRefId { get; set; }

        [DataMember]
        public int? ProjectOccupancyTypMapRefId { get; set; }

        #endregion

    }

    #endregion

}