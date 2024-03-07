using System.Collections.Generic;
namespace AION.Web.Models.Shared
{
    public class ScheduledMeetingsListViewModel
    {
        public List<ScheduledMeetingPartialViewModel> ScheduledMeetingPartialViewModels { get; set; }
        public string ProjectStatus { get; set; }
    }
}