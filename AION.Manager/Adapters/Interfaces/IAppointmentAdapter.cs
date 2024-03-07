using AION.BL.Adapters;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IAppointmentAdapter
    {
        int SaveInternalNotes();
        void InsertProjectAudit();
        bool InsertAttendees(List<AttendeeInfo> attendeeIds, int wkrId);
        bool InsertAttendees(List<AttendeeInfo> attendeeIds, int wkrId, int projectScheduleId);
        bool InsertAttendees(List<AttendeeDetails> attendeeDetails, int projectScheduleId);
        bool RemoveAttendees(List<AttendeeInfo> attendeeIds, int? projectScheduleId = null);
        bool RemoveAttendees(int projectScheduleId);
        bool UpdateAttendees(List<AttendeeInfo> attendeeIds, int wkrId, int projectScheduleId);
        bool UpdateAttendeeList(int wkrId);
        bool UpdateAttendeeList(List<AttendeeInfo> attendeeIds, int WkrId, int projectScheduleId, bool processInsertRemoveOnly);
        bool CancelAppointment();
        int UpdateProjectDept();
        void SendAppointmentNotifications(bool isCancellation);
        List<AttendeeInfo> GetAttendeesByApptId(int appointmentId);
        List<ProjectScheduleBE> GetProjectScheduleByAppointmentId(int id);
    }
}