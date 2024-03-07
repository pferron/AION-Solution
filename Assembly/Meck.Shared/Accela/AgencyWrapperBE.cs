using Newtonsoft.Json;
using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    public class AgencyWrapperBE
    {
        [JsonProperty("status")]
        public int StatusCode { get; set; }

        [JsonProperty("result")]
        public List<AgencyBE> AgencyList { get; set; }
    }
    public class AgencyBE
    {
        [JsonProperty("id")]
        public string AgencyId { get; set; }

        [JsonProperty("name")]
        public string AgencyName { get; set; }

        [JsonProperty("serviceProviderCode")]
        public string ServiceProviderCode { get; set; }

        [JsonProperty("hostId")]
        public string HostId { get; set; }

        [JsonProperty("logLevel")]
        public int LogLevel { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("hostedACA")]
        public bool HostedAca { get; set; }

        [JsonProperty("isForDemo")]
        public bool IsForDemo { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("defaultAppActive")]
        public bool DefaultAppActive { get; set; }

        [JsonProperty("databaseType")]
        public int DatabaseType { get; set; }

        [JsonProperty("iconName")]
        public string IconName { get; set; }


    }
}
