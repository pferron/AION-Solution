using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AION.Accela.Engine.Models
{
    public class AccelaAgenciesBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaAgenciesBE.Model.ResponseAgencyArray" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>

        public AccelaAgenciesBE()
        {
            Result = new List<AccelaAgency>();
        }

        public AccelaAgenciesBE(List<AccelaAgency> result = default(List<AccelaAgency>), int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }



        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public List<AccelaAgency> Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }
    }

    public partial class AccelaAgency
    {

        public AccelaAgency()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaAgenciesBE.Model.Agency" /> class.
        /// </summary>
        /// <param name="displayName">The display name of the agency..</param>
        /// <param name="enabled">Indicates whether or not the agency is enabled on the [Construct Admin Portal](https://admin.accela.com)..</param>
        /// <param name="hostedACA">Indicates whether or not the agency has deployed Citizen Access for the public user..</param>
        /// <param name="iconName">The icon filename..</param>
        /// <param name="isForDemo">Indicates whether or not the agency is used for demo purposes only..</param>
        /// <param name="name">The agency name..</param>
        /// <param name="serviceProviderCode">The unique agency identifier, the system assigns, on the Accela Civic Platform..</param>
        /// <param name="state">The two-character code for the state that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; Agency Info..</param>
        /// <param name="country">The two-character code for the country that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; {Agency} &gt; Agency Info..</param>
        public AccelaAgency(string displayName = default(string), bool? enabled = default(bool?),
            bool? hostedACA = default(bool?), string iconName = default(string), bool? isForDemo = default(bool?),
            string name = default(string), string serviceProviderCode = default(string),
            string state = default(string), string country = default(string))
        {
            this.DisplayName = displayName;
            this.Enabled = enabled;
            this.HostedACA = hostedACA;
            this.IconName = iconName;
            this.IsForDemo = isForDemo;
            this.Name = name;
            this.ServiceProviderCode = serviceProviderCode;
            this.State = state;
            this.Country = country;
        }

        /// <summary>
        /// The display name of the agency.
        /// </summary>
        /// <value>The display name of the agency.</value>
        [DataMember(Name = "displayName", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Indicates whether or not the agency is enabled on the [Construct Admin Portal](https://admin.accela.com).
        /// </summary>
        /// <value>Indicates whether or not the agency is enabled on the [Construct Admin Portal](https://admin.accela.com).</value>
        [DataMember(Name = "enabled", EmitDefaultValue = false)]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Indicates whether or not the agency has deployed Citizen Access for the public user.
        /// </summary>
        /// <value>Indicates whether or not the agency has deployed Citizen Access for the public user.</value>
        [DataMember(Name = "hostedACA", EmitDefaultValue = false)]
        public bool? HostedACA { get; set; }

        /// <summary>
        /// The icon filename.
        /// </summary>
        /// <value>The icon filename.</value>
        [DataMember(Name = "iconName", EmitDefaultValue = false)]
        public string IconName { get; set; }

        /// <summary>
        /// Indicates whether or not the agency is used for demo purposes only.
        /// </summary>
        /// <value>Indicates whether or not the agency is used for demo purposes only.</value>
        [DataMember(Name = "isForDemo", EmitDefaultValue = false)]
        public bool? IsForDemo { get; set; }

        /// <summary>
        /// The agency name.
        /// </summary>
        /// <value>The agency name.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The unique agency identifier, the system assigns, on the Accela Civic Platform.
        /// </summary>
        /// <value>The unique agency identifier, the system assigns, on the Accela Civic Platform.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The two-character code for the state that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; Agency Info.
        /// </summary>
        /// <value>The two-character code for the state that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; Agency Info.</value>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// The two-character code for the country that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; {Agency} &gt; Agency Info.
        /// </summary>
        /// <value>The two-character code for the country that the agency belongs to, as configured on [Construct Admin Portal](https://admin.accela.com) &gt; Agencies &gt; {Agency} &gt; Agency Info.</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }
    }
}


