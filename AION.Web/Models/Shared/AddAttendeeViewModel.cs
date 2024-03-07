using AION.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.Models
{
    public class AddAttendeeViewModel
    {
        public List<UserIdentity> UserIdentities { get; set; }
        public List<UserIdentity> CurrentAttendees { get; set; }
    }
}