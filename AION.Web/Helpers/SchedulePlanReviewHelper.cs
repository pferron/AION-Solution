using AION.BL;
using AION.Manager.Models;
using AION.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AION.Web.Helpers
{
    public class SchedulePlanReviewHelper
    {


        /// <summary>
        /// Scheduling for plan review when Activate NA has been used
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static PlanReview GetActivateNAPlanReviewForSave(ScheduleSaveViewModel vm)
        {
            APIHelper apihelper = new APIHelper();
            PlanReview pr = new PlanReview();
            //LES-3809 - add project audit for auto schedule
            pr.AutoScheduled = vm.AutoScheduled;

            pr.UpdateProjectStatus = false;
            pr.SendEmail = false;
            pr.ResetApproval = false;

            // IMPORTANT: LES-4824 This scenario is not a reschedule and should be updating existing plan review
            pr.IsReschedule = false; 

            vm.IsRescheduleOverwrite = false;

            //ids for updates
            pr.BackfPlanReviewScheduleId = vm.BackfPlanReviewScheduleId;
            pr.BuildPlanReviewScheduleId = vm.BuildPlanReviewScheduleId;
            pr.ElectPlanReviewScheduleId = vm.ElectPlanReviewScheduleId;
            pr.MechaPlanReviewScheduleId = vm.MechaPlanReviewScheduleId;
            pr.PlumbPlanReviewScheduleId = vm.PlumbPlanReviewScheduleId;
            pr.FirePlanReviewScheduleId = vm.FirePlanReviewScheduleId;
            pr.ZonePlanReviewScheduleId = vm.ZonePlanReviewScheduleId;
            pr.PoolPlanReviewScheduleId = vm.PoolPlanReviewScheduleId;
            pr.FoodPlanReviewScheduleId = vm.FoodPlanReviewScheduleId;
            pr.FacilPlanReviewScheduleId = vm.FacilPlanReviewScheduleId;
            pr.DaycPlanReviewScheduleId = vm.DaycPlanReviewScheduleId;

            //update dates
            pr.BackfPRSUpdateDate = vm.BackfPRSUpdateDate;
            pr.BuildPRSUpdateDate = vm.BuildPRSUpdateDate;
            pr.ElectPRSUpdateDate = vm.ElectPRSUpdateDate;
            pr.MechaPRSUpdateDate = vm.MechaPRSUpdateDate;
            pr.PlumbPRSUpdateDate = vm.PlumbPRSUpdateDate;
            pr.FirePRSUpdateDate = vm.FirePRSUpdateDate;
            pr.ZonePRSUpdateDate = vm.ZonePRSUpdateDate;
            pr.PoolPRSUpdateDate = vm.PoolPRSUpdateDate;
            pr.FoodPRSUpdateDate = vm.FoodPRSUpdateDate;
            pr.FacilPRSUpdateDate = vm.FacilPRSUpdateDate;
            pr.DaycPRSUpdateDate = vm.DaycPRSUpdateDate;

            //save hours
            pr.HoursBuilding = vm.HoursBuilding;
            pr.HoursElectic = vm.HoursElectic;
            pr.HoursMech = vm.HoursMech;
            pr.HoursPlumb = vm.HoursPlumb;
            pr.HoursZoning = vm.HoursZoning;
            pr.HoursFire = vm.HoursFire;
            pr.HoursBackFlow = vm.HoursBackFlow;
            pr.HoursFood = vm.HoursFood;
            pr.HoursPool = vm.HoursPool;
            pr.HoursLodge = vm.HoursLodge;
            pr.HoursDayCare = vm.HoursDayCare;

            //dates
            //if pool or fifo, dates should be null/blank
            pr.BackfStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BackfFifo, vm.BackfPool, vm.BackfStartDate);
            pr.BackfEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BackfFifo, vm.BackfPool, vm.BackfEndDate);

            pr.BuildStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BuildFifo, vm.BuildPool, vm.BuildStartDate);
            pr.BuildEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.BuildFifo, vm.BuildPool, vm.BuildEndDate);
            pr.ElectStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ElectFifo, vm.ElectPool, vm.ElectStartDate);
            pr.ElectEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ElectFifo, vm.ElectPool, vm.ElectEndDate);
            pr.MechaStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.MechaFifo, vm.MechaPool, vm.MechaStartDate);
            pr.MechaEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.MechaFifo, vm.MechaPool, vm.MechaEndDate);
            pr.PlumbStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PlumbFifo, vm.PlumbPool, vm.PlumbStartDate);
            pr.PlumbEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PlumbFifo, vm.PlumbPool, vm.PlumbEndDate);

            pr.FireStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FireFifo, vm.FirePool, vm.FireStartDate);
            pr.FireEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FireFifo, vm.FirePool, vm.FireEndDate);

            pr.ZoneStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ZoneFifo, vm.ZonePool, vm.ZoneStartDate);
            pr.ZoneEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.ZoneFifo, vm.ZonePool, vm.ZoneEndDate);

            pr.FoodStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FoodFifo, vm.FoodPool, vm.FoodStartDate);
            pr.FoodEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FoodFifo, vm.FoodPool, vm.FoodEndDate);
            pr.PoolStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PoolFifo, vm.PoolPool, vm.PoolStartDate);
            pr.PoolEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.PoolFifo, vm.PoolPool, vm.PoolEndDate);
            pr.FacilStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FacilFifo, vm.FacilPool, vm.FacilStartDate);
            pr.FacilEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.FacilFifo, vm.FacilPool, vm.FacilEndDate);
            pr.DaycStartDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.DaycFifo, vm.DaycPool, vm.DaycStartDate);
            pr.DaycEndDate = ScheduleHelpers.GetDateTimeByFifoPool(vm.DaycFifo, vm.DaycPool, vm.DaycEndDate);

            //pool
            //fifo
            pr.BackfPool = vm.BackfPool;
            pr.BackfFifo = vm.BackfFifo;

            pr.BuildPool = vm.BuildPool;
            pr.BuildFifo = vm.BuildFifo;
            pr.ElectPool = vm.ElectPool;
            pr.ElectFifo = vm.ElectFifo;
            pr.MechaPool = vm.MechaPool;
            pr.MechaFifo = vm.MechaFifo;
            pr.PlumbPool = vm.PlumbPool;
            pr.PlumbFifo = vm.PlumbFifo;

            pr.FirePool = vm.FirePool;
            pr.FireFifo = vm.FireFifo;

            pr.ZonePool = vm.ZonePool;
            pr.ZoneFifo = vm.ZoneFifo;

            pr.FoodPool = vm.FoodPool;
            pr.FoodFifo = vm.FoodFifo;
            pr.PoolPool = vm.PoolPool;
            pr.PoolFifo = vm.PoolFifo;
            pr.FacilPool = vm.FacilPool;
            pr.FacilFifo = vm.FacilFifo;
            pr.DaycPool = vm.DaycPool;
            pr.DaycFifo = vm.DaycFifo;


            //save schedule choices
            //save attendees
            //save any changes to primary/secondary/excluded
            pr.CreatedUser = vm.LoggedInUser;
            pr.UpdatedUser = vm.LoggedInUser;
            pr.ProjectId = vm.Project.ID;

            pr.UpdatedDate = vm.PMAUpdateDate;

            pr.ApptResponseStatusEnum = vm.ApptResponseStatusEnum;

            //get the attendees string
            //Get the Attendees who were added
            vm.AttendeeIds = string.IsNullOrWhiteSpace(vm.AttendeeIds) ? "" : vm.AttendeeIds;
            string[] attendeeids = vm.AttendeeIds.Split(',');

            List<AttendeeInfo> attendeeIds = new List<AttendeeInfo>();
            foreach (string item in attendeeids)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    int userId = int.Parse(item);
                    if (userId > 0)
                        attendeeIds.Add(new AttendeeInfo { AttendeeId = userId, DeptNameEnumId = -1, BusinessRefId = -1 });

                }
            }
            //add the reviewers to attendees
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
            attendeeIds.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

            //add the reviewers to assignedreviewers list for save model
            List<AttendeeInfo> assignedreviewers = new List<AttendeeInfo>();
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBuilding), DeptNameEnumId = (int)DepartmentNameEnums.Building, BusinessRefId = (int)DepartmentNameEnums.Building });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerBackFlow), DeptNameEnumId = (int)DepartmentNameEnums.Backflow, BusinessRefId = (int)DepartmentNameEnums.Backflow });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerDayCare), DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care, BusinessRefId = (int)DepartmentNameEnums.EH_Day_Care });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerElectrical), DeptNameEnumId = (int)DepartmentNameEnums.Electrical, BusinessRefId = (int)DepartmentNameEnums.Electrical });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFacilities), DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities, BusinessRefId = (int)DepartmentNameEnums.EH_Facilities });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFire), DeptNameEnumId = (int)vm.FireAgency, BusinessRefId = (int)vm.FireAgency });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerFood), DeptNameEnumId = (int)DepartmentNameEnums.EH_Food, BusinessRefId = (int)DepartmentNameEnums.EH_Food });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerMechanical), DeptNameEnumId = (int)DepartmentNameEnums.Mechanical, BusinessRefId = (int)DepartmentNameEnums.Mechanical });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPlumbing), DeptNameEnumId = (int)DepartmentNameEnums.Plumbing, BusinessRefId = (int)DepartmentNameEnums.Plumbing });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerPool), DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool, BusinessRefId = (int)DepartmentNameEnums.EH_Pool });
            assignedreviewers.Add(new AttendeeInfo { AttendeeId = int.Parse(vm.ScheduledReviewerZone), DeptNameEnumId = (int)vm.ZoneAgency, BusinessRefId = (int)vm.ZoneAgency });

            pr.AssignedReviewers = assignedreviewers;
            pr.InternalNotes = vm.InternalNotes;

            pr.AssignedFacilitator = vm.AssignedFacilitator;

            pr.PrimaryReviewers = ScheduleHelpers.GetReviewerListByDept(vm, ScheduleHelpers.ReviewerType.Primary);
            pr.SecondaryReviewers = ScheduleHelpers.GetReviewerListByDept(vm, ScheduleHelpers.ReviewerType.Secondary);

            pr.ExcludedPlanReviewersBuild = vm.ExcludedPlanReviewersBuild;

            pr.ExcludedPlanReviewersElectric = vm.ExcludedPlanReviewersElectric;

            pr.ExcludedPlanReviewersMech = vm.ExcludedPlanReviewersMech;

            pr.ExcludedPlanReviewersPlumb = vm.ExcludedPlanReviewersPlumb;

            pr.ExcludedPlanReviewersZone = vm.ExcludedPlanReviewersZone;

            pr.ExcludedPlanReviewersFire = vm.ExcludedPlanReviewersFire;

            pr.ExcludedPlanReviewersBackFlow = vm.ExcludedPlanReviewersBackFlow;

            pr.ExcludedPlanReviewersFood = vm.ExcludedPlanReviewersFood;

            pr.ExcludedPlanReviewersPool = vm.ExcludedPlanReviewersPool;

            pr.ExcludedPlanReviewersLodge = vm.ExcludedPlanReviewersLodge;

            pr.ExcludedPlanReviewersDayCare = vm.ExcludedPlanReviewersDayCare;

            List<AttendeeInfo> apptAttendees = attendeeIds.Where(x => x.AttendeeId > 0).ToList();

            pr.IsSubmit = vm.IsSubmit;
            pr.NewAttendees = apptAttendees.GroupBy(x => x.AttendeeId).Select(group => group.First()).ToList();

            //get the notes
            pr.InternalNotes = vm.InternalNotes;
            pr.AddSchedulingNotes = vm.AddSchedulingNotes;
            pr.MandatorySchedulingNotes = vm.MandatorySchedulingNotes;
            pr.SchedulingNotes = vm.SchedulingNotes;
            pr.SchedulingStandardNotes = vm.SchedulingStandardNotes;

            //save as current cycle
            pr.Cycle = vm.Cycle;

            if (vm.PlansReadyOnDate.HasValue) pr.ProdDate = vm.PlansReadyOnDate;

            pr.GateDate = vm.GateDate;

            pr.IsFutureCycle = false;

            return pr;
        }






    }
}