using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.Models
{
    public class CustmrMeetingAcceptance
    {
        [JsonProperty("id")]
        public int MeetingId { get; set; }
        [JsonProperty("type")]
        public string MeetingType { get; set; }
    }
}