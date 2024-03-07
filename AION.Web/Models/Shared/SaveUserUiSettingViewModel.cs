namespace AION.Web.Models
{
    public class SaveUserUiSettingViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string SaveFilterList { get; set; }
        public string DashboardType { get; set; }
    }
}