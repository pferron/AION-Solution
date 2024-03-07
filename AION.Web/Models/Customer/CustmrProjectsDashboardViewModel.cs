using AION.Manager.Models;
using System;
using System.Collections.Generic;
using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Web.BusinessEntities;
using System.Linq;

namespace AION.Web.Models
{
    public class CustmrProjectsDashboardViewModel : ViewModelBase
    {
        public CustmrProjectsDashboardViewModel()
        {
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
        }
        public List<ProjectsList> ProjectsList { get; set; }

        public string StatusMessage { get; set; }
        private List<ProjectEstimation> _projects;
     
        private List<Facilitator> _facilitators;
        
        public List<ProjectEstimation> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }
        
        public List<Facilitator> Facilitators
        {
            get
            {
                return _facilitators;
            }
            set
            {
                _facilitators = value;
            }
        }
        
       

    }
}