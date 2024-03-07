namespace AION.Web.Models.Shared
{
    public class DetailsListViewModel : ViewModelBase
    {
        public DetailsListViewModel()
        {
            DefaultRows = false;
        }
        public string DetailsList { get; set; }

        public bool DefaultRows { get; set; }

        public string ListType { get; set; }
    }
}