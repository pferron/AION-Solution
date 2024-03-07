using System;

namespace Meck.Shared.MeckDataMapping
{
    public class ExternalBase
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedTS { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTS { get; set; }
    }
}
