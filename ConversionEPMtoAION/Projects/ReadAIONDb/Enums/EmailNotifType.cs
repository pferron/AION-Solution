using System.ComponentModel;

namespace ReadAIONDb.Enums
{
    public enum EmailNotifType
    {
        [Description("Meeting Accept/Reject email")]
        Meeting_Tentative_Scheduled,
        [Description("Plan Review Accept/Reject Email")]
        Plan_Review_Tentative_Scheduled,
        [Description("Preliminary Meeting Accept/Reject Email")]
        Preliminary_Tentative_Scheduled,
        [Description("Express Tentative Scheduled Email")]
        Express_Tentative_Scheduled,
        [Description("Pending Estimation")]
        Pending_Estimation,
        [Description("Pending Preliminary Estimation")]
        Pending_Preliminary_Estimation,
        [Description("Express Decision By Estimator")]
        Express_Decision_By_Estimator       
    }
}
