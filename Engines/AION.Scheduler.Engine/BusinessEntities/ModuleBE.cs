#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - ModuleBE

    [DataContract]
    public class ModuleBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ModuleId { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public int? EnumMappingNumber { get; set; }

        [DataMember]
        public string ModuleDisplayName { get; set; }
        #endregion

    }

    #endregion

}