using Meck.Shared.MeckDataMapping;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class UserWrapperBE
    {
        [JsonProperty("result")]
        public List<AccelaUserBE> UserList { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("page")]
        public PageBE Page { get; set; }


        public UserWrapperBE()
        {
            UserList = new List<AccelaUserBE>();
        }


    }

    public class AccelaUserBE
    {
        [JsonProperty("firstNm")]
        public string FirstNm { get; set; }

        [JsonProperty("lastNm")]
        public string LastNm { get; set; }

        [JsonProperty("externalSystemRefId")]
        public int ExternalSystemRefId { get; set; }

        [JsonProperty("srcSystemValueTxt")]
        public string SrcSystemValueTxt { get; set; }

        [JsonProperty("createdByWkrId")]
        public int CreatedByWkrId { get; set; }

        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("updatedByWkrId")]
        public int UpdatedByWkrId { get; set; }

        [JsonProperty("updatedDate")]
        public string UpdatedDate { get; set; }

        [JsonProperty("assignedProjectCount")]
        public int AssignedProjectCount { get; set; }

        [JsonProperty("assignedProjectHours")]
        public double AssignedProjectHours { get; set; }



        //@USER_NM in DB table
        [JsonProperty("externalAppUserNm")]
        public string ExternalAppUserNm { get; set; }

        //@LAN_ID_TXT  in DB table
        [JsonProperty("ADUserIDTxt")]
        public string LanIdTxt { get; set; }

        //@PHONE_NUM in DB table
        [JsonProperty("phoneNum")]
        public string PhoneNum { get; set; }

        //@EMAIL_ADDR_TXT in DB table
        [JsonProperty("emailAddrTxt")]
        public string EmailAddrTxt { get; set; }

        //@NOTES_TXT in DB table
        [JsonProperty("notesTxt")]
        public string NotesTxt { get; set; }

        //[JsonProperty("agencyList")]
        //public List<AgencyInfo> AgencyList { get; set; }
        [JsonProperty("tradesList")]
        public List<TradeInfo> TradesList { get; set; }

        [JsonProperty("agenciesList")]
        public List<AgencyInfo> AgencyList { get; set; }

    }

    public class PageBE
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("hasmore")]
        public bool Hasmore { get; set; }
    }

    /// <summary>
    /// UserGroupModel
    /// </summary>
    [DataContract]
    public partial class AccelaUserGroupModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserGroupModel" /> class.
        /// </summary>
        /// <param name="id">The ID of the user group..</param>
        /// <param name="moduleName">The module the user group belongs to..</param>
        /// <param name="name">The name of the user group..</param>
        public AccelaUserGroupModelBE(long? id = default(long?), string moduleName = default(string),
            string name = default(string))
        {
            this.Id = id;
            this.ModuleName = moduleName;
            this.Name = name;
        }

        /// <summary>
        /// The ID of the user group.
        /// </summary>
        /// <value>The ID of the user group.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// The module the user group belongs to.
        /// </summary>
        /// <value>The module the user group belongs to.</value>
        [DataMember(Name = "moduleName", EmitDefaultValue = false)]
        public string ModuleName { get; set; }

        /// <summary>
        /// The name of the user group.
        /// </summary>
        /// <value>The name of the user group.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
    }


}
