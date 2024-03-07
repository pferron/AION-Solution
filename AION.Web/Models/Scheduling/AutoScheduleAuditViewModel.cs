using System;

namespace AION.Web.Models.Scheduling
{
    public class AutoScheduleAuditViewModel
    {
        public bool auditAutoScheduleButton { get; set; }
        public int auditBuildingUserID { get; set; }
        public int auditElectricUserID { get; set; }
        public int auditMechUserID { get; set; }
        public int auditPlumbUserID { get; set; }
        public int auditZoneUserID { get; set; }
        public int auditFireUserID { get; set; }
        public int auditFoodServiceUserID { get; set; }
        public int auditPoolUserID { get; set; }
        public int auditFacilityUserID { get; set; }
        public int auditDayCareUserID { get; set; }
        public int auditBackFlowUserID { get; set; }

        public DateTime? auditScheduleStart { get; set; }
        public DateTime? auditScheduleEnd { get; set; }

        public DateTime? auditBuildingScheduleStart { get; set; }
        public DateTime? auditBuildingScheduleEnd { get; set; }
        public DateTime? auditElectricScheduleStart { get; set; }
        public DateTime? auditElectricScheduleEnd { get; set; }
        public DateTime? auditMechScheduleStart { get; set; }
        public DateTime? auditMechScheduleEnd { get; set; }
        public DateTime? auditPlumbScheduleStart { get; set; }
        public DateTime? auditPlumbScheduleEnd { get; set; }
        public DateTime? auditZoneScheduleStart { get; set; }
        public DateTime? auditZoneScheduleEnd { get; set; }
        public DateTime? auditFireScheduleStart { get; set; }
        public DateTime? auditFireScheduleEnd { get; set; }
        public DateTime? auditFoodScheduleStart { get; set; }
        public DateTime? auditFoodScheduleEnd { get; set; }
        public DateTime? auditPoolScheduleStart { get; set; }
        public DateTime? auditPoolScheduleEnd { get; set; }
        public DateTime? auditFacilityScheduleStart { get; set; }
        public DateTime? auditFacilityScheduleEnd { get; set; }
        public DateTime? auditDayCareScheduleStart { get; set; }
        public DateTime? auditDayCareScheduleEnd { get; set; }
        public DateTime? auditBackFlowScheduleStart { get; set; }
        public DateTime? auditBackFlowScheduleEnd { get; set; }

        public int auditMeetingRoomId { get; set; }
        public DateTime auditSelectedStartDateTime { get; set; }
        public DateTime auditSelectedEndDateTime { get; set; }

        public DateTime auditScheduleDate { get; set; }
        public bool auditZoneIsPool { get; set; }


    }
}