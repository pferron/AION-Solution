using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class NPAModel
    {
        public List<Reviewer> Reviewers { get; set; } = new List<Reviewer>();
        public List<NpaType> NpaTypes { get; set; } = new List<NpaType>();
        public List<NPASearchResult> NPASearchResults { get; set; } = new List<NPASearchResult>();
        public List<NPASearchResult> EndingSoonResults { get; set; } = new List<NPASearchResult>();
        public List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();
        public List<HolidayConfig> Holidays { get; set; } = new List<HolidayConfig>();
    }
}