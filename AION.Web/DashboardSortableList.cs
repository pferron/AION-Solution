using Newtonsoft.Json;

namespace AION.Manager.Models.Dashboard
{
    public class DashboardSortableList
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("header")]
        public string Header { get; set; }
        [JsonProperty("list")]
        public string List { get; set; }
        [JsonProperty("selected")]
        public string Selected { get; set; }
        [JsonProperty("order")]
        public string Order { get; set; }
        [JsonProperty("required")]
        public string Required { get; set; }
    }


}