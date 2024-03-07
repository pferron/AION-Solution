using AION.BL.Models;
using Newtonsoft.Json;

namespace AION.BL.BusinessObjects
{
    public class UiSettingsModelBO
    {
        private UiSettings _uisettings;
        private string _jsonstring;
        public UiSettings UiSettings
        {
            get
            {
                return _uisettings;
            }
            set
            {
                _uisettings = value;

            }
        }
        public string JsonString
        {
            get
            {
                return _jsonstring;
            }
            set
            {
                _jsonstring = value;
                _uisettings = ConvertJsonStringToUiSettings(_jsonstring);
            }
        }
        public UiSettings ConvertJsonStringToUiSettings(string jsonstring)
        {
            UiSettings uisettings = new UiSettings();
            uisettings.EstimationDashboard = new DashboardUiSetting();
            uisettings.MeetingDashboard = new DashboardUiSetting();
            uisettings.SchedulingDashboard = new DashboardUiSetting();
            if (!string.IsNullOrWhiteSpace(jsonstring))
            {
                uisettings = JsonConvert.DeserializeObject<UiSettings>(jsonstring);
            }
            return uisettings;
        }
        public string ConvertUiSettingsToJsonString(UiSettings uisettings)
        {
            return JsonConvert.SerializeObject(uisettings);
        }
    }
}
