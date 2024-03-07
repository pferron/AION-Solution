using AION.Web.Models;
using System;

namespace AION.Web.Helpers
{
    public class ProjectAuditHelper
    {
        public static bool SetAutoScheduleProjectAuditForPlanReview(ScheduleSaveViewModel vm)
        {
            bool isAutoSchedule = true;
            //determine if the user clicked the autoschedule button
            if (!vm.auditAutoScheduleButton)
            {
                return false;
            }

            //compare each reviewer
            bool bReviewer = CompareAllReviewers(vm);

            if (bReviewer == false)
            {
                return false;
            }
            //compare dates
            //if startDates <> 0 then return false for isAutoSchedule
            int startDates = CompareAllStartDates(vm);

            if (startDates != 0)
            {
                return false;
            }
            //if endDates <> 0 then return false for isAutoSchedule
            int endDates = CompareAllEndDates(vm);
            if (endDates != 0)
            {
                return false;
            }
            bool bZonePool = vm.ZonePool == vm.auditZoneIsPool;

            if (bZonePool == false)
            {
                return false;
            }

            return isAutoSchedule;
        }

        public static bool SetAutoScheduleProjectAuditForMeeting(ScheduleSaveViewModel vm)
        {
            bool isAutoSchedule = true;
            //determine if the user clicked the autoschedule button
            if (!vm.auditAutoScheduleButton)
            {
                return false;
            }

            //compare each reviewer
            bool bReviewer = CompareAllReviewers(vm);

            if (bReviewer == false)
            {
                return false;
            }
            //compare dates
            //if startDates <> 0 then return false for isAutoSchedule
            int dateTimes = CompareMeetingDateTime(vm);

            if (dateTimes != 0)
            {
                return false;
            }

            return isAutoSchedule;
        }

        public static bool SetAutoScheduleByButtonStatus(ScheduleSaveViewModel vm)
        {
            bool isAutoSchedule = true;
            //determine if the user clicked the autoschedule button
            if (!vm.auditAutoScheduleButton)
            {
                return false;
            }

            return isAutoSchedule;
        }

        private static int CompareMeetingDateTime(ScheduleSaveViewModel vm)
        {
            return DateTime.Compare(SetNullAuditDateTime(vm.auditSelectedStartDateTime), vm.StartTime)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditSelectedEndDateTime), vm.EndTime)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditScheduleDate), vm.ScheduleDate);
        }
        private static int CompareAllEndDates(ScheduleSaveViewModel vm)
        {
            return DateTime.Compare(SetNullAuditDateTime(vm.auditBuildingScheduleEnd), vm.BuildEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditElectricScheduleEnd), vm.ElectEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditMechScheduleEnd), vm.MechaEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditPlumbScheduleEnd), vm.PlumbEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditFireScheduleEnd), vm.FireEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditZoneScheduleEnd), vm.ZoneEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditBackFlowScheduleEnd), vm.BackfEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditFacilityScheduleEnd), vm.FacilEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditPoolScheduleEnd), vm.PoolEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditFoodScheduleEnd), vm.FoodEndDate)
                 + DateTime.Compare(SetNullAuditDateTime(vm.auditDayCareScheduleEnd), vm.DaycEndDate);
        }
        private static int CompareAllStartDates(ScheduleSaveViewModel vm)
        {
            return DateTime.Compare(SetNullAuditDateTime(vm.auditBuildingScheduleStart), vm.BuildStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditElectricScheduleStart), vm.ElectStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditMechScheduleStart), vm.MechaStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditPlumbScheduleStart), vm.PlumbStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditFireScheduleStart), vm.FireStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditZoneScheduleStart), vm.ZoneStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditBackFlowScheduleStart), vm.BackfStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditFacilityScheduleStart), vm.FacilStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditPoolScheduleStart), vm.PoolStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditFoodScheduleStart), vm.FoodStartDate)
                + DateTime.Compare(SetNullAuditDateTime(vm.auditDayCareScheduleStart), vm.DaycStartDate);
        }
        private static bool CompareAllReviewers(ScheduleSaveViewModel vm)
        {
            //compare each reviewer
            return CompareReviewer(vm.ScheduledReviewerBuilding, vm.auditBuildingUserID)
                && CompareReviewer(vm.ScheduledReviewerElectrical, vm.auditElectricUserID)
                && CompareReviewer(vm.ScheduledReviewerMechanical, vm.auditMechUserID)
                && CompareReviewer(vm.ScheduledReviewerPlumbing, vm.auditPlumbUserID)
                && CompareReviewer(vm.ScheduledReviewerZone, vm.auditZoneUserID)
                && CompareReviewer(vm.ScheduledReviewerFire, vm.auditFireUserID)
                && CompareReviewer(vm.ScheduledReviewerBackFlow, vm.auditBackFlowUserID)
                && CompareReviewer(vm.ScheduledReviewerFacilities, vm.auditFacilityUserID)
                && CompareReviewer(vm.ScheduledReviewerDayCare, vm.auditDayCareUserID)
                && CompareReviewer(vm.ScheduledReviewerPool, vm.auditPoolUserID)
                && CompareReviewer(vm.ScheduledReviewerFood, vm.auditFoodServiceUserID);
        }

        private static bool CompareReviewer(string reviewerId, int auditReviewerId)
        {
            int scheduledReviewerId = 0;
            if (!string.IsNullOrWhiteSpace(reviewerId))
            {
                scheduledReviewerId = int.Parse(reviewerId);
            }
            else
            {
                //string empty or null will never equal int which defaults to 0
                return false;
            }
            return auditReviewerId == scheduledReviewerId;
        }
        private static DateTime SetNullAuditDateTime(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}