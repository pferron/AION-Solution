using AION.BL;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Scheduler.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Helpers
{
    public class SchedulingHelper
    {
        public static List<UserScheduleBE> FlattenTimeSlots(List<UserScheduleBE> users)
        {
            //merge all 15 minute timeslots based on continous projects.
            if (users == null || users.Count == 0) //NA or not picked up then just return
                return new List<UserScheduleBE>();
            List<UserScheduleBE> ret = new List<UserScheduleBE>();
            //sort the list first based on start time. each time is supposed to be 15 minutes apart.
            List<UserScheduleBE> allocatedTimeSlots = users.OrderBy(x => x.StartDateTime).ToList();

            UserScheduleBE pivotSlot = null;
            bool firstLeg = true;
            foreach (var currentSlot in allocatedTimeSlots)
            {
                if (firstLeg == true)
                {
                    pivotSlot = currentSlot;
                    firstLeg = false;
                    continue;
                }
                else if (pivotSlot.EndDateTime != currentSlot.StartDateTime)
                {
                    ret.Add(pivotSlot);
                    pivotSlot = currentSlot;
                }
                else
                {
                    pivotSlot.EndDateTime = currentSlot.EndDateTime;
                }
            }
            //add the last slot.
            ret.Add(pivotSlot);
            return ret;
        }

        public static List<TimeSlot> FlattenTimeSlots(List<TimeSlot> slots)
        {
            //merge all timeslots where contiguous.
            if (slots == null || slots.Count == 0) //NA or not picked up then just return
                return new List<TimeSlot>();
            List<TimeSlot> ret = new List<TimeSlot>();
            //sort the list first based on start time. each time is supposed to be 15 minutes apart.
            List<TimeSlot> allocatedTimeSlots = slots.OrderBy(x => x.StartTime).ToList();

            TimeSlot pivotSlot = null;
            bool firstLeg = true;
            foreach (var currentSlot in allocatedTimeSlots)
            {
                if (firstLeg == true)
                {
                    pivotSlot = currentSlot;
                    firstLeg = false;
                    continue;
                }
                else if (pivotSlot.EndTime != currentSlot.StartTime)
                {
                    ret.Add(pivotSlot);
                    pivotSlot = currentSlot;
                }
                else
                {
                    pivotSlot.StartTime = currentSlot.StartTime;
                }
            }
            //add the last slot.
            ret.Add(pivotSlot);
            return ret;
        }

        public static List<TimeSlot> SplitTimeSlotByTimeSlotIntervalMinsHalfTime(TimeSlot timeslot, int timeSlotIntervalByMinutes, int allowedStartTime, int allowedEndTime)
        {
            DateTime current = timeslot.StartTime.CurrentHalfTime();
            List<TimeSlot> ret = new List<TimeSlot>();
            if (current == timeslot.EndTime.CurrentHalfTime())//if less than 30 minutes then make that as one slot and then quit.
            {
                if (current.Hour >= allowedStartTime && current.Hour < allowedEndTime)//if out of time for the day then skip. 
                {
                    TimeSlot val = new TimeSlot();
                    val.StartTime = current;
                    val.EndTime = current.AddMinutes(timeSlotIntervalByMinutes);
                    val.AllocationType = timeslot.AllocationType;
                    val.ProjectScheduleID = timeslot.ProjectScheduleID;
                    val.ProjectID = timeslot.ProjectID;
                    val.UserID = timeslot.UserID;
                    val.UserScheduleID = timeslot.UserScheduleID;
                    ret.Add(val);
                }
            }
            else
            {
                while (current < timeslot.EndTime.CurrentHalfTime().AddMinutes(timeSlotIntervalByMinutes))
                {
                    if (current.Hour >= allowedStartTime && current.Hour < allowedEndTime)//if out of time for the day then skip. 
                    {
                        TimeSlot val = new TimeSlot();
                        val.StartTime = current;
                        val.EndTime = current.AddMinutes(timeSlotIntervalByMinutes);
                        val.AllocationType = timeslot.AllocationType;
                        val.ProjectScheduleID = timeslot.ProjectScheduleID;
                        val.ProjectID = timeslot.ProjectID;
                        val.UserID = timeslot.UserID;
                        val.UserScheduleID = timeslot.UserScheduleID;
                        ret.Add(val);
                        current = current.AddMinutes(timeSlotIntervalByMinutes);
                    }
                    else
                        current = current.AddMinutes(timeSlotIntervalByMinutes);
                }
            }
            return ret;
        }

        public static List<TimeSlot> SplitTimeSlotByTimeSlotIntervalMinsQuarterTime(TimeSlot timeslot, int timeSlotIntervalByMinutes, int allowedStartTime, int allowedEndTime)
        {
            if (timeslot == null)
                return new List<TimeSlot>();
            DateTime current = timeslot.StartTime.CurrentQuarterTime();
            List<TimeSlot> ret = new List<TimeSlot>();
            if (current == timeslot.EndTime.CurrentQuarterTime())//if less than 15 minutes then make that as one slot and then quit.
            {
                if (current.Hour >= allowedStartTime && current.Hour < allowedEndTime)//if out of time for the day then skip. 
                {
                    TimeSlot val = new TimeSlot();
                    val.StartTime = current;
                    val.EndTime = current.AddMinutes(timeSlotIntervalByMinutes);
                    val.AllocationType = timeslot.AllocationType;
                    val.ProjectScheduleID = timeslot.ProjectScheduleID;
                    val.ProjectID = timeslot.ProjectID;
                    val.UserID = timeslot.UserID;
                    val.UserScheduleID = timeslot.UserScheduleID;
                    val.ProjectScheduleTypeName = timeslot.ProjectScheduleTypeName;
                    val.ProjectCategory = timeslot.ProjectCategory;
                    val.TotalTimeOfDay = timeslot.EndTime - timeslot.StartTime;
                    ret.Add(val);
                }
            }
            else
            {
                while (current < timeslot.EndTime.CurrentQuarterTime())
                {
                    if (current.Hour >= allowedStartTime && current.Hour < allowedEndTime)//if out of time for the day then skip. 
                    {
                        TimeSlot val = new TimeSlot();
                        val.StartTime = current;
                        val.EndTime = current.AddMinutes(timeSlotIntervalByMinutes);
                        val.AllocationType = timeslot.AllocationType;
                        val.ProjectScheduleID = timeslot.ProjectScheduleID;
                        val.ProjectID = timeslot.ProjectID;
                        val.UserID = timeslot.UserID;
                        val.UserScheduleID = timeslot.UserScheduleID;
                        val.ProjectScheduleTypeName = timeslot.ProjectScheduleTypeName;
                        val.ProjectCategory = timeslot.ProjectCategory;
                        val.TotalTimeOfDay = timeslot.EndTime - timeslot.StartTime;
                        ret.Add(val);
                        current = current.AddMinutes(timeSlotIntervalByMinutes);
                    }
                    else
                        current = current.AddMinutes(timeSlotIntervalByMinutes);
                }
            }
            return ret;
        }
        public static decimal GetUserAllowedPlanReviewHours(UserIdentity user, PropertyTypeEnums projectType)
        {
            decimal allowedHours = 0M;
            if (user.PlanReviewOverrideHours != 0)
                allowedHours = user.PlanReviewOverrideHours;
            else
                allowedHours = GetGlobalAllowedPlanReviewHours(projectType);
            if (allowedHours == 0)
                allowedHours = 8;//set default as 8 hrs incase nothing found.
            return allowedHours;
        }

        public static decimal GetGlobalAllowedPlanReviewHours(PropertyTypeEnums projectType)
        {
            decimal ret = 0M;
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            List<PlanReviewerAvailableHour> hours = thisengine.GetAllPlanReviewerHours();

            PlanReviewerAvailableHour planReviewerAvailableHour = new PlanReviewerAvailableHour();

            switch (projectType)
            {
                case PropertyTypeEnums.Commercial:
                case PropertyTypeEnums.Special_Projects_Team:
                case PropertyTypeEnums.Townhomes:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursPlanReviewerMMF).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;

                case PropertyTypeEnums.Mega_Multi_Family:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursMMF).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;

                case PropertyTypeEnums.County_Fire_Shop_Drawings:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursCountyFire).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;

                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursFIFOAddRenSFH).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;

                case PropertyTypeEnums.FIFO_Master_Plans:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursFIFOMsPln).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;

                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursFIFOSingleFH).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;

                case PropertyTypeEnums.FIFO_Small_Commercial:
                    planReviewerAvailableHour = hours.Where(x => x.EnumMappingValNbr == PlanReviewHourTypes.HoursFIFOSmComm).FirstOrDefault();
                    if (planReviewerAvailableHour != null)
                        ret = planReviewerAvailableHour.AvailableHours;
                    break;
            }

            return ret;
        }

    }
}