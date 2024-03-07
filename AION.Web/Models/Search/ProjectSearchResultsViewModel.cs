using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Web.Models.Search
{
    public class ProjectSearchResultsViewModel : ViewModelBase
    {
        public ProjectSearchResultsViewModel()
        {
            ProjectList = new List<ProjectSearchResult>();
        }

        public List<ProjectSearchResult> _projectList;

        public List<ProjectSearchResult> ProjectList
        {
            get { return _projectList; }
            set { _projectList = value; }
        }
    }
}