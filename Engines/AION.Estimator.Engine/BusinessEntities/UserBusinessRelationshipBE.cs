#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - UserBusinessRelationshipBE

    [DataContract]
    public class UserBusinessRelationshipBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? UserBusinessRelationshipId { get; set; }

        [DataMember]
        public int? UserID { get; set; }

        [DataMember]
        public int? BusinessRefId { get; set; }

        /// <summary>
        /// Used by a function process to add a user newly added to 
        /// any NPAs that are indicated for all of a specific business ref (department)
        /// New user, set to true
        /// Update user, added to this department, set to true
        /// otherwise false
        /// </summary>
        [DataMember]
        public bool? ProcessNpaInd { get; set; }
        #endregion

    }

    #endregion

}