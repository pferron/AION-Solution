using AION.BL;

namespace AION.Manager.Accessors
{
    public interface IPMAAccessor
    {
        PreliminaryMeetingAppointment GetByProjectId(int id);
        bool CancelByProjectId(int id);
    }
}