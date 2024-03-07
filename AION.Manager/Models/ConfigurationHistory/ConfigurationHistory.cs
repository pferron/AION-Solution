using AION.BL;

namespace AION.Manager.Models.ConfigurationHistory
{
    public class ConfigurationHistory
    {
        public string SearchType { get; set; }
        public string SearchRange { get; set; }

        public ConfigurationHistoryTable SearchTypeEnum { get; set; }

        public SearchRange SearchRangeEnum { get; set; }
    }
}