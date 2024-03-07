using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Engines;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class FIFOAdapter : BaseManagerAdapter, IFIFOAdapter
    {
        public bool IsFifoScheduled { get; set; }

        public bool ProcessFIFO(ProjectEstimation model)
        {
            FIFOEngineParams data = new FIFOEngineParams();
            data.AccelaProjectIDRef = model.AccelaProjectRefId;
            data.Cycle = model.CycleNbr.Value;

            data.RecIdTxt = model.RecIdTxt;

            ProcessFIFOScheduling(data);

            return IsFifoScheduled;
        }

        public bool ProcessFIFOScheduling(FIFOEngineParams model)
        {
            try
            {
                FIFOSchedulingEngine thisengine = new FIFOSchedulingEngine(model);
                IsFifoScheduled = thisengine.IsFifoScheduled;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in FIFOAdapter ProcessFIFOScheduling - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return IsFifoScheduled;
        }

        public bool UpsertFIFO(PlanReview planReview)
        {
            bool success = false;

            try
            {
                IPlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                success = planReviewAdapter.UpsertPlanReview(planReview);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in FIFOAdapter UpsertFIFO - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public List<ProjectBE> GetProjectListByFIFODueDate()
        {
            List<ProjectBE> projects = new List<ProjectBE>();

            ProjectBO projectBO = new ProjectBO();
            projects = projectBO.GetListByFIFODueDate();

            return projects;
        }

        public bool InsertFIFOAttendees(
            List<AttendeeInfo> attendeeIds,
            int scheduleId,
            int WkrId,
            PlanReviewScheduleDetailBE scheduleDetail,
            int projectId,
            int? projectScheduleId = null)
        {
            bool success = false;
            try
            {
                UserScheduleBO userScheduleBO = new UserScheduleBO();

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //if no projectScheduleBEList passed in then it is considered as totaly new appoinment 
                //and add all attendees to all dates.
                List<ProjectScheduleBE> projectScheduleBEs = new List<ProjectScheduleBE>();
                int projectscheduleid = 0;
                projectScheduleBEs = projectScheduleBO.GetByApptId(scheduleId, ProjectScheduleRefEnum.FIFO.ToString(), null);
                if (projectScheduleBEs != null && projectScheduleBEs.Count > 0)
                {
                    projectScheduleBE = projectScheduleBEs.FirstOrDefault();
                    projectscheduleid = projectScheduleBE.ProjectScheduleID.Value;
                    projectScheduleBE.RecurringApptDt = scheduleDetail.StartDt;
                    projectScheduleBE.UserId = WkrId.ToString();
                    int schedret = projectScheduleBO.Update(projectScheduleBE);
                }
                else
                {
                    projectScheduleBE.AppoinmentID = scheduleId;
                    projectScheduleBE.ProjectScheduleTypeRef = ProjectScheduleRefEnum.FIFO.ToString();
                    projectScheduleBE.UserId = WkrId.ToString();
                    projectScheduleBE.RecurringApptDt = scheduleDetail.StartDt;
                    projectscheduleid = projectScheduleBO.Create(projectScheduleBE);
                }

                List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

                List<UserScheduleBE> users = new List<UserScheduleBE>();
                PropertyTypeEnums projectType = (PropertyTypeEnums)new ProjectBO().GetById(projectId).ProjectTypRefId;

                foreach (AttendeeInfo attendeeId in attendeeIds)
                {
                    if (attendeeId.AttendeeId != 0)
                    {
                        int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendeeId.DeptNameEnumId).ID;
                        DepartmentNameEnums dept = (DepartmentNameEnums)attendeeId.DeptNameEnumId;

                        //only adjust hours if the attendee was a previous attendee

                        decimal totalHours = GetPlanReviewScheduleHours(
                            projectId,
                            businessRefId,
                            scheduleDetail.SameBuildContrInd.Value,
                            attendeeId.AttendeeId);

                        AllotUserSlotManually(
                            attendeeId.AttendeeId,
                            scheduleDetail.StartDt.Value,
                            scheduleDetail.EndDt.Value,
                            users,
                            dept,
                            totalHours,
                            1,
                            projectscheduleid,
                            projectType);

                        var filteredusersbydept = users.Where(x => x.BusinessRefID == businessRefId).ToList();

                        if (filteredusersbydept.Count > 0)
                        {
                            var fltUsrs = SchedulingHelper.FlattenTimeSlots(filteredusersbydept);
                            foreach (var item in fltUsrs)
                            {
                                //insert into user schedule
                                int userscheduleid = 0;
                                UserScheduleBE userScheduleBE = new UserScheduleBE
                                {
                                    ProjectScheduleID = item.ProjectScheduleID,
                                    StartDateTime = item.StartDateTime.Value,
                                    EndDateTime = item.EndDateTime.Value,
                                    BusinessRefID = item.BusinessRefID,
                                    UserID = item.UserID,
                                    UserId = item.UserId
                                };
                                userscheduleid = userScheduleBO.Create(userScheduleBE);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in FIFOAdapter InsertFIFOAttendees - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public SameBuildingContractor GetSameBuildingContractor(Project project)
        {
            SameBuildingContractor model = new SameBuildingContractor();

            if (string.IsNullOrEmpty(project.BuildingContractorName)
                || string.IsNullOrEmpty(project.BuildingContractorAcctNo))
            {
                return model;
            }

            List<ProjectBE> projectList = new ProjectBO().GetProjectListByBuildContr(
                project.BuildingContractorName,
                project.BuildingContractorAcctNo,
                project.AccelaProjectCreatedDate.Value)
                .Where(x => x.ProjectId != project.ID).ToList();

            if (projectList.Count() > 0)
            {
                List<PlanReviewScheduleDetail> fifoSchedules = new List<PlanReviewScheduleDetail>();

                foreach (var prevProject in projectList)
                {
                    ProjectCycleSummary summary = new PlanReviewAdapter().GetProjectCycleSummary(prevProject.ProjectId.Value, prevProject);

                    if (summary.PlanReviewScheduleDetailsPrevious != null && summary.PlanReviewScheduleDetailsPrevious.Count > 0)
                    {
                        fifoSchedules.AddRange(summary.PlanReviewScheduleDetailsPrevious);
                    }
                }

                model.IsSameBuildingContractor = true;
                model.FifoSchedules = fifoSchedules;
            }

            return model;
        }

        public decimal GetPlanReviewScheduleHours(int projectId, int businessrefId, bool isSameBuilContrac, int attendeeId)
        {
            Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(projectId);
            Helper helper = new Helper();
            bool isPreviousReviewer = false;

            SameBuildingContractor sameBuildingContractor = GetSameBuildingContractor(project);
            List<PlanReviewScheduleDetail> reviewerSchedules = sameBuildingContractor.FifoSchedules.Where(x => x.AssignedPlanReviewerId == attendeeId).ToList();
            if (reviewerSchedules.Any())
            {
                isPreviousReviewer = true;
            }

            ProjectDepartment dept = ProjectHelper.GetDepartment(project, (DepartmentNameEnums)businessrefId);
            decimal defaulthours = 0;

            bool applicableFIFOTypes =
                project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes ||
                project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans;

            bool shouldTimeBeReduced = isSameBuilContrac && applicableFIFOTypes && isPreviousReviewer;

            // get project hours estimated
            List<ProjectBusinessRelationshipBE> businessRelationshipBEs = new ProjectBusinessRelationshipBO().GetListByProjectId(projectId);

            ProjectBusinessRelationshipBE businessRelationshipBE = new ProjectBusinessRelationshipBE();

            if (helper.ZoneDepartmentNames.Contains((DepartmentNameEnums)businessrefId))
            {
                businessRelationshipBE = businessRelationshipBEs.Where(x => helper.ZoneDepartmentNames.Contains((DepartmentNameEnums)x.BusinessRefId)).FirstOrDefault();
            }
            else if (helper.FireDepartmentNames.Contains((DepartmentNameEnums)businessrefId))
            {
                businessRelationshipBE = businessRelationshipBEs.Where(x => helper.FireDepartmentNames.Contains((DepartmentNameEnums)x.BusinessRefId)).FirstOrDefault();
            }
            else
            {
                businessRelationshipBE = businessRelationshipBEs.Where(x => x.BusinessRefId == businessrefId).FirstOrDefault();
            }

            defaulthours = businessRelationshipBE.EstimationHoursNbr.Value;

            if (defaulthours > 0)
            {
                if (shouldTimeBeReduced)
                {
                    defaulthours = defaulthours / 2;

                    decimal wholeestimationhours = Math.Truncate(defaulthours);
                    decimal partestimationhours = defaulthours - wholeestimationhours;
                    if (defaulthours % 0.5M != 0)
                    {
                        if (partestimationhours > 0.5M)
                        {
                            //add one to truncated value
                            defaulthours = wholeestimationhours + 1.0M;
                        }
                        if (partestimationhours < 0.5M)
                        {
                            //add .5 to truncated value
                            defaulthours = wholeestimationhours + 0.5M;
                        }
                    }

                    return Math.Round(defaulthours, 1);
                }
            }

            return defaulthours;
        }

        public UserIdentity GetLastAssignedCityZoningReviewer()
        {
            FifoScheduleBO fifoScheduleBO = new FifoScheduleBO();
            int reviewerId = fifoScheduleBO.GetLastAssignedCityZoningReviewer();

            UserIdentity cityZoningUser = new UserIdentityModelBO().GetInstance(reviewerId);
            return cityZoningUser;
        }

        public bool HasActiveSchedules(PlanReview fifoSchedule)
        {
            if (fifoSchedule.BuildStartDate.HasValue && fifoSchedule.BuildEndDate.HasValue) return true;
            if (fifoSchedule.ElectStartDate.HasValue && fifoSchedule.ElectEndDate.HasValue) return true;
            if (fifoSchedule.MechaStartDate.HasValue && fifoSchedule.MechaEndDate.HasValue) return true;
            if (fifoSchedule.PlumbStartDate.HasValue && fifoSchedule.PlumbEndDate.HasValue) return true;
            if (fifoSchedule.ZoneStartDate.HasValue && fifoSchedule.ZoneEndDate.HasValue) return true;
            if (fifoSchedule.FireStartDate.HasValue && fifoSchedule.FireEndDate.HasValue) return true;
            if (fifoSchedule.DaycStartDate.HasValue && fifoSchedule.DaycEndDate.HasValue) return true;
            if (fifoSchedule.MechaStartDate.HasValue && fifoSchedule.MechaEndDate.HasValue) return true;
            if (fifoSchedule.FoodStartDate.HasValue && fifoSchedule.FoodEndDate.HasValue) return true;
            if (fifoSchedule.PoolStartDate.HasValue && fifoSchedule.PoolEndDate.HasValue) return true;
            if (fifoSchedule.FacilStartDate.HasValue && fifoSchedule.FacilEndDate.HasValue) return true;
            if (fifoSchedule.BackfStartDate.HasValue && fifoSchedule.BackfEndDate.HasValue) return true;

            return false;
        }

        public bool AllotUserSlotManually(int userID, DateTime start, DateTime end, List<UserScheduleBE> userSlots
      , DepartmentNameEnums department, decimal hrsTotal, int WkrId, int planReviewProjectDetailsId, PropertyTypeEnums projectType)
        {
            try
            {
                decimal leftoverMeetingSlots = hrsTotal * 4; //convert to 15 minute slots
                if (leftoverMeetingSlots == 0) return false;
                List<TimeSlot> slotsby15Min = new List<TimeSlot>();
                UserAdapter usrBo = new UserAdapter();
                UserIdentity user = usrBo.GetUserIdentityByID(userID);
                int allowedSlots = System.Convert.ToInt32(SchedulingHelper.GetUserAllowedPlanReviewHours(user, projectType) * 4);
                List<TimeSlot> slots = usrBo.GetUsedTimeSlotsExtrasByUserID(userID, start, end);
                DateTime CurrentDate = start.Date;
                do
                {
                    //get all slots in current day
                    var cur = slots.Where(x => x.StartTime.Date == CurrentDate.Date).ToList();
                    if (cur != null && cur.Count > 0)
                    {
                        //splits each day slot in list to 15 minute slots
                        foreach (var item in cur)
                        {
                            slotsby15Min.AddRange(SchedulingHelper.SplitTimeSlotByTimeSlotIntervalMinsQuarterTime(item, 15, 8, 17));
                        }
                        //removes duplicates from 15 min slots.
                        slotsby15Min = DistinctBy(slotsby15Min, x => x.StartTime).ToList();
                        DateTime currentHr = CurrentDate.Date.AddHours(8);

                        int usedSlots = slotsby15Min.Count;
                        //DateTime? slotStartHr = currentHr;
                        do
                        {
                            if (slotsby15Min.Any(x => x.StartTime == currentHr) == false)
                            {   // nothing found at this time slot. So adding the plan review to this slot.

                                userSlots.Add(new UserScheduleBE
                                {
                                    ProjectScheduleID = planReviewProjectDetailsId,
                                    StartDateTime = currentHr,
                                    EndDateTime = currentHr.AddMinutes(15),
                                    BusinessRefID = new DepartmentModelBO().GetInstance(department).ID,
                                    UserID = userID,
                                    UserId = WkrId.ToString()
                                });
                                leftoverMeetingSlots--;
                                usedSlots++;
                            }
                            currentHr = currentHr.AddMinutes(15);
                        } while (GetTimeString(currentHr) != "1600" && leftoverMeetingSlots > 0 && usedSlots < allowedSlots);
                    }
                    else
                    {
                        DateTime currentHr = CurrentDate.Date.AddHours(8);
                        int usedSlots = 0;
                        do
                        {
                            userSlots.Add(new UserScheduleBE
                            {
                                ProjectScheduleID = planReviewProjectDetailsId,
                                StartDateTime = currentHr,
                                EndDateTime = currentHr.AddMinutes(15),
                                BusinessRefID = new DepartmentModelBO().GetInstance(department).ID,
                                UserID = userID,
                                UserId = WkrId.ToString()
                            });
                            leftoverMeetingSlots--;
                            usedSlots++;
                            currentHr = currentHr.AddMinutes(15);
                        } while (GetTimeString(currentHr) != "1600" && leftoverMeetingSlots > 0 && usedSlots < allowedSlots);
                    }
                    CurrentDate = NextWorkingDay(CurrentDate);
                } while (CurrentDate.Date < end.Date && leftoverMeetingSlots > 0);

                //Now it is the last day of manual allotment and as per rule if any of the hours are left, all the rest of the allotment need to be forced into last day and it is up to he user to complete the allocation. warnings are given already.
                if (leftoverMeetingSlots > 0)
                {
                    //if any of the hrs are left unallocated when it reached 5 PM of last day then double allocate the left over hours to last day.
                    DateTime currentHr = PrevWorkingDay(new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day)).AddHours(8); //go back one day since it will be past one day from last loop.
                    DateTime nextWorkingDay = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day).AddHours(8);
                    do
                    {
                        DateTime? slotStartHr = nextWorkingDay;
                        DateTime? slotEndHr = null;
                        if (leftoverMeetingSlots > 32)
                        {
                            //if left over is more than 8 hr then allocate an 8 hr. then move to next dulicate day.
                            slotEndHr = slotStartHr.Value.AddHours(8);
                            leftoverMeetingSlots -= 32;
                        }
                        else
                        {
                            //else allocate the left over hr.
                            slotEndHr = slotStartHr.Value.AddMinutes((double)leftoverMeetingSlots * 15);
                            leftoverMeetingSlots = 0;
                        }
                        if (slotStartHr.HasValue == true && slotEndHr.HasValue == true)
                        {
                            userSlots.Add(new UserScheduleBE
                            {
                                ProjectScheduleID = planReviewProjectDetailsId,
                                StartDateTime = slotStartHr,
                                EndDateTime = slotEndHr,
                                BusinessRefID = new DepartmentModelBO().GetInstance(department).ID,
                                UserID = userID,
                                UserId = WkrId.ToString()
                            });
                            slotEndHr = null;
                        }
                    } while (leftoverMeetingSlots > 0);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //bool IsDateHolidayOrWeekEnd(DateTime date)
        //{
        //    return HolidayList.Any(x => x.Date == date.Date)
        //       || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        //}

        //DateTime PrevWorkingDay(DateTime date)
        //{
        //    DateTime ret = date;
        //    do
        //    {
        //        ret = ret.AddDays(-1);
        //    }
        //    while (IsDateHolidayOrWeekEnd(ret));

        //    return ret;
        //}

        //DateTime NextWorkingDay(DateTime date)
        //{
        //    DateTime ret = date;
        //    do
        //    {
        //        ret = ret.AddDays(1);
        //    }
        //    while (IsDateHolidayOrWeekEnd(ret));

        //    return ret;
        //}
        private string GetTimeString(DateTime time)
        {
            return time.ToString("HHmm");
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(/*this*/ IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}