#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectOccupancyTypMapRefBE

    [DataContract]
    public class ProjectOccupancyTypMapRefBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ProjectOccupancyTypMapRefId { get; set; }

        [DataMember]
        public string ProjectOccupancyTypMapName { get; set; }

        #endregion

    }

    #endregion

}