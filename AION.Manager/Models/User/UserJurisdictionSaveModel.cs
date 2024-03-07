using System.Collections.Generic;

namespace AION.Manager.Models.User
{
    public class UserJurisdictionSaveModel
    {
        /// <summary>
        /// User 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// List of Jurisdiction enum vals
        /// </summary>
        public List<string> JurisdictionList { get; set; }

        /// <summary>
        /// Update user for audit
        /// </summary>
        public string WrkId { get; set; }
    }
}