using AION.BL;
using System.Collections.Generic;

namespace AION.Manager.Accessors
{
    public interface IFMAAccessor
    {
        List<FacilitatorMeetingAppointment> GetListByProjectId(int projectId);

        List<FacilitatorMeetingAppointment> GetByProjectIDAndMeetingType(string projectId, string meetingTypeDesc);
        bool UpdateStatus(FacilitatorMeetingAppointment fma, int apptResponseStatusRefId, int apptCancellationRefId);
        bool CancelByProjectId(int projectId);
    }
}