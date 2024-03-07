using System.Collections.Generic;

namespace AION.Manager.Models
{
    public interface INPAAttendees
    {
        List<AttendeeInfo> AttendeeIds { get; set; }
        int NpaId { get; set; }
        int WkrId { get; set; }
    }

}