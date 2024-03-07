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
    public class CustmrMeetingsDashboardViewModel : ViewModelBase
    {
         public CustmrMeetingsDashboardViewModel()
        {
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
        }
        public List<CustmrMeetings> MeetingList { get; set; }

        public string StatusMessage { get; set; }

    }

}