using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Helpers;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Email.Engine.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.BusinessObjects;
using AION.Manager.Engines;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class PlanReviewAdapter : BaseManagerAdapter, IPlanReviewAdapter
    {
        private PlanReview _planreview;
        private ProjectEstimation _projectEstimation;

        ProjectCycleBO _ProjectCycleBO;
        ProjectCycleBE _ProjectCycleBE;
        List<ProjectCycleBE> _ProjectCycleBEs;

        ProjectCycleDetailBO _ProjectCycleDetailBO;
        ProjectCycleDetailBE _ProjectCycleDetailBE;
        List<ProjectCycleDetailBE> _ProjectCycleDetailBEs;

        PlanReviewScheduleBO _PlanReviewScheduleBO;
        PlanReviewScheduleBE _PlanReviewScheduleBE;

        PlanReviewScheduleDetailBO _PlanReviewScheduleDetailBO;
        PlanReviewScheduleDetailBE _PlanReviewScheduleDetailBE;
        List<PlanReviewScheduleDetailBE> _PlanReviewScheduleDetailBEs;

        ProjectScheduleBE _ProjectScheduleBE;
        ProjectScheduleBO _ProjectScheduleBO;

        List<AttendeeInfo> _RemovedAttendees = new List<AttendeeInfo>();
        List<AttendeeInfo> _InsertedAttendees = new List<AttendeeInfo>();

        public PlanReviewAdapter()
        {
            _ProjectCycleBO = new ProjectCycleBO();
            _ProjectCycleBE = new ProjectCycleBE();
            _ProjectCycleBEs = new List<ProjectCycleBE>();

            _ProjectCycleDetailBO = new ProjectCycleDetailBO();
            _ProjectCycleDetailBE = new ProjectCycleDetailBE();
            _ProjectCycleDetailBEs = new List<ProjectCycleDetailBE>();

            _PlanReviewScheduleBO = new PlanReviewScheduleBO();
            _PlanReviewScheduleBE = new PlanReviewScheduleBE();

            _PlanReviewScheduleDetailBO = new PlanReviewScheduleDetailBO();
            _PlanReviewScheduleDetailBE = new PlanReviewScheduleDetailBE();
            _PlanReviewScheduleDetailBEs = new List<PlanReviewScheduleDetailBE>();

            _ProjectScheduleBE = new ProjectScheduleBE();
            _ProjectScheduleBO = new ProjectScheduleBO();
        }

        public List<PlanReview> GetPlanReviewsByProjectId(string projectid)
        {
            ProjectBE project = new ProjectBO().GetByExternalRefInfo(projectid);

            return GetPlanReviewsByProject(project);
        }

        public List<ProjectCycleReview> GetProjectCycleReviews(int projectId)
        {
            List<ProjectCycleReview> projectCycleReviews = new List<ProjectCycleReview>();

            List<ProjectCycle> cycles = GetProjectCyclesByProjectId(projectId);
            foreach (ProjectCycle cycle in cycles)
            {
                projectCycleReviews.Add(new ProjectCycleReview()
                {
                    ProjectCycle = cycle,
                    PlanReviews = GetPlanReviewsByProjectCycle(cycle.ID)
                });
            }

            return projectCycleReviews;
        }

        public ProjectCycleSummary GetProjectCycleSummary(int projectId, ProjectBE projectBE = null)
        {
            ProjectCycleSummary summary = new ProjectCycleSummary();

            List<PlanReview> planReviews = projectBE != null ? GetPlanReviewsByProject(projectBE) : GetPlanReviewsByProjectId(projectId);

            summary.PlanReviews = planReviews;
            summary.PlanReviewCurrent = planReviews.FirstOrDefault(x => x.IsCurrentCycle == true);
            summary.PlanReviewFuture = planReviews.FirstOrDefault(x => x.IsFutureCycle == true);

            List<ProjectCycleReview> projectCycleReviews = GetProjectCycleReviews(projectId);
            summary.ProjectCycleReviews = projectCycleReviews;

            List<ProjectCycle> cycles = new List<ProjectCycle>();

            foreach (ProjectCycleReview cycleReview in projectCycleReviews)
            {
                cycles.Add(cycleReview.ProjectCycle);
            }

            summary.ProjectCycleCurrent = cycles.FirstOrDefault(x => x.CurrentCycleInd == true);
            summary.ProjectCycleFuture = cycles.FirstOrDefault(x => x.FutureCycleInd == true);

            int currentCycle = 0;

            if (summary.ProjectCycleCurrent != null)
            {
                currentCycle = summary.ProjectCycleCurrent.CycleNbr.Value;
            }

            int previousCycle = 1;

            bool hasPreviousCycle = false;

            if (currentCycle > 1)
            {
                hasPreviousCycle = true;
                previousCycle = currentCycle - 1;
                summary.ProjectCyclePrevious = cycles.FirstOrDefault(x => x.CycleNbr == previousCycle);
            }

            List<PlanReviewSchedule> planReviewSchedules = new List<PlanReviewSchedule>();

            if (summary.ProjectCycleCurrent != null)
            {
                summary.ProjectCycleDetailsCurrent = GetProjectCycleDetailsByProjectCycleId(summary.ProjectCycleCurrent.ID);
                planReviewSchedules = GetPlanReviewSchedulesByProjectCycle(summary.ProjectCycleCurrent.ID);
                summary.PlanReviewScheduleCurrent = planReviewSchedules.FirstOrDefault(x => x.IsRescheduleInd == false);

                if (summary.PlanReviewScheduleCurrent != null)
                {
                    if (summary.PlanReviewScheduleCurrent.ProjectScheduleTypDesc == ProjectScheduleRefEnum.EMA.ToString())
                    {
                        summary.ProjectCycleCurrent.HasEMAPlanReview = true;
                    }
                    summary.PlanReviewScheduleDetailsCurrent = GetPlanReviewScheduleDetailsByPlanReviewSchedule(summary.PlanReviewScheduleCurrent.ID);
                }
                cycles.Add(summary.ProjectCycleCurrent);
            }

            if (hasPreviousCycle)
            {
                summary.ProjectCycleDetailsPrevious = GetProjectCycleDetailsByProjectCycleId(summary.ProjectCyclePrevious.ID);
                planReviewSchedules = GetPlanReviewSchedulesByProjectCycle(summary.ProjectCyclePrevious.ID);
                summary.PlanReviewSchedulePrevious = planReviewSchedules.FirstOrDefault(x => x.IsRescheduleInd == false);
                if (summary.PlanReviewSchedulePrevious != null)
                {
                    summary.PlanReviewPrevious = planReviews.FirstOrDefault(x => x.ID == summary.PlanReviewSchedulePrevious.ID);
                    summary.PlanReviewScheduleDetailsPrevious = GetPlanReviewScheduleDetailsByPlanReviewSchedule(summary.PlanReviewSchedulePrevious.ID);
                }
                cycles.Add(summary.ProjectCyclePrevious);
            }

            if (summary.ProjectCycleFuture != null)
            {
                summary.ProjectCycleDetailsFuture = GetProjectCycleDetailsByProjectCycleId(summary.ProjectCycleFuture.ID);
                planReviewSchedules = GetPlanReviewSchedulesByProjectCycle(summary.ProjectCycleFuture.ID);
                summary.PlanReviewScheduleFuture = planReviewSchedules.FirstOrDefault(x => x.IsRescheduleInd == false);
                if (summary.PlanReviewScheduleFuture != null)
                {
                    if (summary.PlanReviewScheduleFuture.ProjectScheduleTypDesc == ProjectScheduleRefEnum.EMA.ToString())
                    {
                        summary.ProjectCycleFuture.HasEMAPlanReview = true;
                    }
                    summary.PlanReviewScheduleDetailsFuture = GetPlanReviewScheduleDetailsByPlanReviewSchedule(summary.PlanReviewScheduleFuture.ID);
                }
                cycles.Add(summary.ProjectCycleFuture);
            }

            return summary;
        }

        public ProjectCycle GetProjectCycleById(int projectCycleId)
        {
            ProjectCycleBE projectCycleBE = new ProjectCycleBO().GetById(projectCycleId);
            ProjectCycle projectCycle = new ProjectCycleModelBO().ConvertBEToModel(projectCycleBE);
            return projectCycle;
        }

        public List<PlanReviewSchedule> GetPlanReviewSchedulesByProjectCycle(int projectCycleId)
        {
            List<PlanReviewSchedule> planReviewSchedules = new List<PlanReviewSchedule>();

            List<PlanReviewScheduleBE> planReviewScheduleBEs = new List<PlanReviewScheduleBE>();
            PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();

            planReviewScheduleBEs = planReviewScheduleBO.GetByProjectCycleId(projectCycleId);

            foreach (PlanReviewScheduleBE planReviewScheduleBE in planReviewScheduleBEs)
            {
                if (planReviewScheduleBE.PlanReviewScheduleId.HasValue)
                {
                    PlanReviewSchedule planReviewSchedule = new PlanReviewSchedule();
                    PlanReviewScheduleModelBO planReviewScheduleModelBO = new PlanReviewScheduleModelBO();
                    planReviewSchedule = planReviewScheduleModelBO.ConvertBEToModel(planReviewScheduleBE);
                    planReviewSchedules.Add(planReviewSchedule);
                }
            }

            return planReviewSchedules;
        }

        public List<PlanReviewScheduleDetail> GetPlanReviewScheduleDetailsByPlanReviewSchedule(int planReviewScheduleId)
        {
            List<PlanReviewScheduleDetail> planReviewScheduleDetails = new List<PlanReviewScheduleDetail>();

            _PlanReviewScheduleDetailBEs = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(planReviewScheduleId);

            PlanReviewScheduleDetailModelBO planReviewScheduleDetailModelBO = new PlanReviewScheduleDetailModelBO();

            foreach (PlanReviewScheduleDetailBE item in _PlanReviewScheduleDetailBEs)
            {
                PlanReviewScheduleDetail detail = planReviewScheduleDetailModelBO.GetInstance(item.PlanReviewScheduleDetailId.Value);
                planReviewScheduleDetails.Add(detail);
            }

            return planReviewScheduleDetails;
        }

        private PlanReview UpdatePlanReviewWithPlanReviewSchedule(
            PlanReview planReview,
            PlanReviewScheduleBE planReviewScheduleBE,
            int projectCycleId)
        {
            PlanReviewSchedule planReviewSchedule = new PlanReviewScheduleModelBO().ConvertBEToModel(planReviewScheduleBE);
            _PlanReviewScheduleDetailBEs = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(planReviewScheduleBE.PlanReviewScheduleId.Value);
            planReview.ID = planReviewScheduleBE.PlanReviewScheduleId.Value;
            planReview.PlanReviewScheduleId = planReviewScheduleBE.PlanReviewScheduleId;
            planReview.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(planReviewScheduleBE.CreatedByWkrId));
            planReview.UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(planReviewScheduleBE.CreatedByWkrId));
            planReview.IsReschedule = planReviewScheduleBE.IsRescheduleInd.Value;

            planReview.ApptResponseStatusEnum =
                new AppointmentResponseStatusModelBO().GetInstance(planReviewScheduleBE.ApptResponseStatusRefId.Value).ApptResponseStatusEnum;

            planReview.ProjectScheduleRefEnum =
                (ProjectScheduleRefEnum)Enum.Parse(typeof(ProjectScheduleRefEnum), planReviewSchedule.ProjectScheduleTypDesc);

            if (planReviewSchedule.MeetingRoomRefId.HasValue)
            {
                planReview.MeetingRoom = new MeetingRoomBO().GetById(planReviewSchedule.MeetingRoomRefId.Value);
            }

            ProjectCycle projectCycle = new ProjectCycleModelBO().GetInstance(projectCycleId);
            planReview.ProjectCycle = projectCycle;

            planReview.ProjectId = projectCycle.ProjectId.Value;

            planReview.IsCurrentCycle = false;
            planReview.IsFutureCycle = false;

            if (projectCycle.FutureCycleInd == true)
            {
                planReview.IsFutureCycle = true;
            }

            if (projectCycle.CurrentCycleInd == true)
            {
                planReview.IsCurrentCycle = true;
            }

            planReview.CancelAfterDt = planReviewSchedule.CancelAfterDt;
            planReview.MeetingRoomRefId = planReviewSchedule.MeetingRoomRefId;
            planReview.ProposedDate1 = planReviewSchedule.Proposed1Dt;
            planReview.ProposedDate2 = planReviewSchedule.Proposed2Dt;
            planReview.ProposedDate3 = planReviewSchedule.Proposed3Dt;
            planReview.VirtualMeetingInd = planReviewSchedule.VirtualMeetingInd;

            DepartmentModelBO departmentModelBO = new DepartmentModelBO();
            planReview.AssignedReviewers = new List<AttendeeInfo>();

            if (_PlanReviewScheduleDetailBEs.Count > 0)
            {
                planReview.StartDate = _PlanReviewScheduleDetailBEs.First().StartDt;
                planReview.EndDate = _PlanReviewScheduleDetailBEs.First().EndDt;
            }

            if (planReview.StartDate != null)
            {
                planReview.ScheduleDate = planReview.StartDate;
                planReview.ResponseDate = DateTimeHelper.DetermineWorkDateAfterDateSpecified(planReview.CreatedDate, 2);
            }

            decimal totalBEMPhrs = 0;
            decimal? cancellationFeePerHour = CatalogItemModelBO.GetCancellationFeePerHour();

            foreach (PlanReviewScheduleDetailBE item in _PlanReviewScheduleDetailBEs)
            {
                DepartmentNameEnums department = (DepartmentNameEnums)item.BusinessRefId;

                switch (department)
                {
                    case DepartmentNameEnums.Building:
                        planReview.BuildEndDate = item.EndDt;
                        planReview.BuildPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.BuildPool = item.PoolRequestInd;
                        planReview.BuildPRSUpdateDate = item.UpdatedDate;
                        planReview.BuildStartDate = item.StartDt;
                        planReview.HoursBuilding = item.AssignedHoursNbr.Value;
                        planReview.BuildAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        totalBEMPhrs += item.AssignedHoursNbr.Value;
                        break;
                    case DepartmentNameEnums.Electrical:
                        planReview.ElectEndDate = item.EndDt;
                        planReview.ElectPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.ElectPool = item.PoolRequestInd;
                        planReview.ElectPRSUpdateDate = item.UpdatedDate;
                        planReview.ElectStartDate = item.StartDt;
                        planReview.HoursElectic = item.AssignedHoursNbr.Value;
                        planReview.ElectAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        totalBEMPhrs += item.AssignedHoursNbr.Value;
                        break;
                    case DepartmentNameEnums.Mechanical:
                        planReview.MechaEndDate = item.EndDt;
                        planReview.MechaPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.MechaPool = item.PoolRequestInd;
                        planReview.MechaPRSUpdateDate = item.UpdatedDate;
                        planReview.MechaStartDate = item.StartDt;
                        planReview.HoursMech = item.AssignedHoursNbr.Value;
                        planReview.MechaAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        totalBEMPhrs += item.AssignedHoursNbr.Value;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        planReview.PlumbEndDate = item.EndDt;
                        planReview.PlumbPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.PlumbPool = item.PoolRequestInd;
                        planReview.PlumbPRSUpdateDate = item.UpdatedDate;
                        planReview.PlumbStartDate = item.StartDt;
                        planReview.HoursPlumb = item.AssignedHoursNbr.Value;
                        planReview.PlumbAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        totalBEMPhrs += item.AssignedHoursNbr.Value;
                        break;
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        planReview.ZoneEndDate = item.EndDt;
                        planReview.ZonePlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.ZonePool = item.PoolRequestInd;
                        planReview.ZonePRSUpdateDate = item.UpdatedDate;
                        planReview.ZoneStartDate = item.StartDt;
                        planReview.HoursZoning = item.AssignedHoursNbr.Value;
                        planReview.ZoneAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    case DepartmentNameEnums.Fire_Davidson:
                    case DepartmentNameEnums.Fire_Cornelius:
                    case DepartmentNameEnums.Fire_Pineville:
                    case DepartmentNameEnums.Fire_Matthews:
                    case DepartmentNameEnums.Fire_Mint_Hill:
                    case DepartmentNameEnums.Fire_Huntersville:
                    case DepartmentNameEnums.Fire_UMC:
                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                    case DepartmentNameEnums.Fire_County:
                        planReview.FireEndDate = item.EndDt;
                        planReview.FirePlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.FirePool = item.PoolRequestInd;
                        planReview.FirePRSUpdateDate = item.UpdatedDate;
                        planReview.FireStartDate = item.StartDt;
                        planReview.HoursFire = item.AssignedHoursNbr.Value;
                        planReview.FireAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                        planReview.DaycEndDate = item.EndDt;
                        planReview.DaycPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.DaycPool = item.PoolRequestInd;
                        planReview.DaycPRSUpdateDate = item.UpdatedDate;
                        planReview.DaycStartDate = item.StartDt;
                        planReview.HoursDayCare = item.AssignedHoursNbr.Value;
                        planReview.DaycAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    case DepartmentNameEnums.EH_Food:
                        planReview.FoodEndDate = item.EndDt;
                        planReview.FoodPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.FoodPool = item.PoolRequestInd;
                        planReview.FoodPRSUpdateDate = item.UpdatedDate;
                        planReview.FoodStartDate = item.StartDt;
                        planReview.HoursFood = item.AssignedHoursNbr.Value;
                        planReview.FoodAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        planReview.PoolEndDate = item.EndDt;
                        planReview.PoolPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.PoolPool = item.PoolRequestInd;
                        planReview.PoolPRSUpdateDate = item.UpdatedDate;
                        planReview.PoolStartDate = item.StartDt;
                        planReview.HoursPool = item.AssignedHoursNbr.Value;
                        planReview.PoolAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        planReview.FacilEndDate = item.EndDt;
                        planReview.FacilPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.FacilPool = item.PoolRequestInd;
                        planReview.FacilPRSUpdateDate = item.UpdatedDate;
                        planReview.FacilStartDate = item.StartDt;
                        planReview.HoursLodge = item.AssignedHoursNbr.Value;
                        planReview.FacilAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    case DepartmentNameEnums.Backflow:
                        planReview.BackfEndDate = item.EndDt;
                        planReview.BackfPlanReviewScheduleId = item.PlanReviewScheduleId;
                        planReview.BackfPool = item.PoolRequestInd;
                        planReview.BackfPRSUpdateDate = item.UpdatedDate;
                        planReview.BackfStartDate = item.StartDt;
                        planReview.HoursBackFlow = item.AssignedHoursNbr.Value;
                        planReview.BackfAssignedReviewerName = item.AssignedReviewerFirstName + " " + item.AssignedReviewerLastName;
                        break;
                    default:
                        break;
                }

                planReview.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId));
                planReview.UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId));

                // assigned users
                if (item.AssignedPlanReviewerId.HasValue)
                {
                    planReview.AssignedReviewers.Add(new AttendeeInfo()
                    {
                        AttendeeId = item.AssignedPlanReviewerId.Value,
                        BusinessRefId = item.BusinessRefId.Value,
                        DeptNameEnumId = (int)departmentModelBO.GetInstance(item.BusinessRefId.Value).DepartmentEnum
                    });
                }
            }

            if (_PlanReviewScheduleDetailBEs.Any())
            {
                planReview.EarliestDate = _PlanReviewScheduleDetailBEs.Min(x => x.StartDt);
                planReview.MaxDate = _PlanReviewScheduleDetailBEs.Max(x => x.StartDt);

                if (planReview.ProjectScheduleRefEnum == ProjectScheduleRefEnum.FIFO)
                {
                    planReview.AllHoursOneOrLess = _PlanReviewScheduleDetailBEs.Count > 0 && !_PlanReviewScheduleDetailBEs.Any(x => x.AssignedHoursNbr > 1);
                }

                if (planReview.ProjectScheduleRefEnum != ProjectScheduleRefEnum.FIFO)
                {
                    planReview.AllPool = _PlanReviewScheduleDetailBEs.Count > 0 && !_PlanReviewScheduleDetailBEs.Any(x => x.PoolRequestInd == false);
                }
            }

            //jcl if there's no earliest date, this is all pooled and cancellation message should be blank
            if (planReview.EarliestDate.HasValue)
            {
                MessageTemplateEngine messageTemplateEngine = new MessageTemplateEngine();
                messageTemplateEngine.MessageTemplateTypeEnum = MessageTemplateTypeEnum.PR_Cancellation_Message;
                messageTemplateEngine.ProjectId = planReview.ProjectId;
                messageTemplateEngine.PlanReviewStartDt = planReview.EarliestDate.Value.ToShortDateString();
                messageTemplateEngine.PlanReviewStartDay = planReview.EarliestDate.Value.DayOfWeek.ToString();
                DateTime duedate = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(planReview.EarliestDate.Value, 5);
                messageTemplateEngine.CancellationDueDate = duedate.DayOfWeek.ToString() + " " + duedate.ToShortDateString();
                messageTemplateEngine.CancellationFee = "$0";
                if (cancellationFeePerHour.HasValue && totalBEMPhrs > 0)
                {
                    decimal? cancellationFee = totalBEMPhrs * cancellationFeePerHour.Value;
                    messageTemplateEngine.CancellationFee = "$" + cancellationFee.Value.ToString("0.00");
                    messageTemplateEngine.CancellationCalculation = "$" + cancellationFeePerHour.Value.ToString("0.00") + " * " + totalBEMPhrs.ToString("0.00") + "hrs";
                }
                planReview.CancellationMessage = messageTemplateEngine.BuildMessage();

            }

            return planReview;
        }

        public List<PlanReview> GetPlanReviewsByProjectCycle(int projectCycleId)
        {
            List<PlanReview> planReviews = new List<PlanReview>();

            List<PlanReviewScheduleBE> planReviewSchedules = _PlanReviewScheduleBO.GetByProjectCycleId(projectCycleId);

            foreach (PlanReviewScheduleBE planReviewSchedule in planReviewSchedules)
            {
                if (planReviewSchedule.PlanReviewScheduleId.HasValue && planReviewSchedule.PlanReviewScheduleId > 0)
                {
                    PlanReview planReview = new PlanReview();
                    planReview = UpdatePlanReviewWithPlanReviewSchedule(planReview, planReviewSchedule, projectCycleId);

                    planReviews.Add(planReview);
                }
            }

            return planReviews;
        }

        public List<PlanReview> GetPlanReviewsByProject(ProjectBE project)
        {
            int projectId = project.ProjectId.Value;
            ProjectStatusEnum projectStatusEnum = new ProjectStatusModelBO().GetInstance(project.ProjectStatusRefId.Value).ProjectStatusEnum;

            List<PlanReview> planReviews = new List<PlanReview>();

            List<ProjectCycleBE> projectCycleBEs = new List<ProjectCycleBE>();
            ProjectCycleModelBO projectCycleModelBO = new ProjectCycleModelBO();

            projectCycleBEs = _ProjectCycleBO.GetListByProject(projectId);

            bool hasFutureCycle = false;
            if (projectCycleBEs.Any(x => x.FutureCycleInd == true && x.IsActive == true))
            {
                hasFutureCycle = true;
            }

            foreach (ProjectCycleBE projectCycleBE in projectCycleBEs)
            {
                var planReviewsPerCycle = GetPlanReviewsByProjectCycle(projectCycleBE.ProjectCycleId.Value);

                foreach (PlanReview planReview in planReviewsPerCycle)
                {
                    ProjectCycle projectCycle = new ProjectCycleModelBO().ConvertBEToModel(projectCycleBE);

                    planReview.ProjectCycle = projectCycle;

                    planReview.HasFutureCycle = hasFutureCycle;

                    if (projectStatusEnum == ProjectStatusEnum.Suspended_Fees_Due)
                    {
                        planReview.EarliestDate = (DateTime?)null;
                    }

                    planReviews.Add(planReview);
                }
            }

            return planReviews;
        }

        public List<PlanReview> GetPlanReviewsByProjectId(int projectId)
        {
            ProjectBE project = new ProjectBO().GetById(projectId);
            return GetPlanReviewsByProject(project);
        }

        public PlanReview GetPlanReviewByPlanReviewScheduleId(int planReviewScheduleId)
        {
            PlanReview planReview = new PlanReview();

            _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(planReviewScheduleId);

            planReview = UpdatePlanReviewWithPlanReviewSchedule(planReview, _PlanReviewScheduleBE, _PlanReviewScheduleBE.ProjectCycleId.Value);

            return planReview;
        }

        public List<ProjectCycle> GetProjectCyclesByProjectId(int projectId)
        {
            List<ProjectCycle> projectCycles = new List<ProjectCycle>();

            ProjectCycleModelBO projectCycleModelBO = new ProjectCycleModelBO();

            _ProjectCycleBEs = _ProjectCycleBO.GetListByProject(projectId);

            foreach (ProjectCycleBE projectCycleBE in _ProjectCycleBEs)
            {
                ProjectCycle cycle = projectCycleModelBO.ConvertBEToModel(projectCycleBE);
                projectCycles.Add(cycle);
            }

            return projectCycles;
        }

        public List<ProjectCycleDetail> GetProjectCycleDetailsByProjectCycleId(int projectCycleId)
        {
            List<ProjectCycleDetail> projectCycleDetails = new List<ProjectCycleDetail>();

            ProjectCycleDetailModelBO projectCycleDetailModelBO = new ProjectCycleDetailModelBO();

            _ProjectCycleDetailBEs = _ProjectCycleDetailBO.GetListByProjectCycle(projectCycleId);

            foreach (ProjectCycleDetailBE projectCycleDetailBE in _ProjectCycleDetailBEs)
            {
                ProjectCycleDetail detail = projectCycleDetailModelBO.ConvertBEToModel(projectCycleDetailBE);
                projectCycleDetails.Add(detail);
            }

            return projectCycleDetails;
        }

        public PlanReview GetPlanReviewByProjectScheduleId(int projectScheduleId)
        {
            try
            {
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();

                projectScheduleBE = projectScheduleBO.GetById(projectScheduleId);

                PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();
                PlanReviewScheduleBE planReviewScheduleBE = new PlanReviewScheduleBE();
                planReviewScheduleBE = planReviewScheduleBO.GetById(projectScheduleBE.AppoinmentID.Value);

                PlanReview planReview = new PlanReview();

                if (planReviewScheduleBE.PlanReviewScheduleId.HasValue && planReviewScheduleBE.PlanReviewScheduleId > 0)
                {
                    planReview = UpdatePlanReviewWithPlanReviewSchedule(planReview, planReviewScheduleBE, planReviewScheduleBE.ProjectCycleId.Value);
                }

                return planReview;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter GetPlanReviewByProjectScheduleId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdateProjectCycle(ProjectCycle projectCycle)
        {
            try
            {
                ProjectCycleBO projectCycleBO = new ProjectCycleBO();
                ProjectCycleBE projectCycleBE = projectCycleBO.GetById(projectCycle.ID);
                projectCycleBE.PlansReadyOnDt = projectCycle.PlansReadyOnDt;
                projectCycleBE.IsAprvInd = projectCycle.IsAprvInd;
                projectCycleBE.UserId = projectCycle.UpdatedUser.ID.ToString();

                int rows = projectCycleBO.Update(projectCycleBE);

                AuditActionEnum auditActionEnum = AuditActionEnum.NA;

                if (projectCycleBE.IsAprvInd.HasValue)
                {
                    auditActionEnum = projectCycle.IsAprvInd.Value ? AuditActionEnum.Review_Date_Accepted : AuditActionEnum.Review_Date_Rejected;

                    new ProjectAuditModelBO().InsertProjectAudit(projectCycle.ProjectId.Value, projectCycle.AuditText, projectCycleBE.UserId, auditActionEnum);
                }

                return rows > 0;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdateProjectCycle - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdatePlanReviewStatus(PlanReview planReview, AppointmentResponseStatusEnum status)
        {
            try
            {
                return UpdatePlanReviewStatus(planReview, status, 0);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdatePlanReviewStatus - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdatePlanReviewStatus(PlanReview planReview, AppointmentResponseStatusEnum status, AppointmentCancellationEnum reason)
        {
            AppointmentCancellationRefModelBO appointmentCancellation = new AppointmentCancellationRefModelBO();
            AppointmentCancellationReason appointmentCancellationReason = appointmentCancellation.GetInstance(reason);
            int apptCancellationRefId = appointmentCancellationReason.ApptCancellationRefId.Value;

            try
            {
                return UpdatePlanReviewStatus(planReview, status, apptCancellationRefId);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdatePlanReviewStatus - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdatePlanReviewStatus(PlanReview planReview, AppointmentResponseStatusEnum status, int apptCancellationRefId = 0)
        {
            AppointmentResponseStatusModelBO appointmentResponse = new AppointmentResponseStatusModelBO();
            AppointmentResponseStatus appointmentResponseStatus = appointmentResponse.GetInstance(status);
            int apptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId.Value;

            //convert plan review to plan review schedules
            //if insert, this is id, if update, rows updated

            PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();
            PlanReviewScheduleBE planReviewSchedule = planReviewScheduleBO.GetById(planReview.PlanReviewScheduleId.Value);
            planReviewSchedule.UserId = planReview.UpdatedUser.ID.ToString();

            ProjectCycleBO projectCycleBO = new ProjectCycleBO();
            ProjectCycleBE projectCycleBE = new ProjectCycleBE();

            planReviewSchedule.ApptResponseStatusRefId = apptResponseStatusRefId;

            if (apptCancellationRefId > 0)
            {
                planReviewSchedule.ApptCancellationRefId = apptCancellationRefId;
            }

            bool updatePlanReview = UpdatePlanReview(planReviewSchedule, status);

            // update project cycle with approval info

            projectCycleBE = projectCycleBO.GetById(planReview.ProjectCycle.ID);
            projectCycleBE.PlansReadyOnDt = planReview.ProjectCycle.PlansReadyOnDt;
            projectCycleBE.IsAprvInd = planReview.ProjectCycle.IsAprvInd;
            projectCycleBE.ResponderUserId = planReview.ProjectCycle.ResponderUserId;
            projectCycleBE.ResponseDt = DateTime.Now;
            projectCycleBE.UserId = planReview.UpdatedUser.ID.ToString();

            projectCycleBO.Update(projectCycleBE);

            bool isCancellation = false;
            if (status == AppointmentResponseStatusEnum.Cancelled||status == AppointmentResponseStatusEnum.Not_Scheduled) 
            {
                isCancellation = true;
            }

            List<PlanReviewScheduleDetail> planReviewSchedules = GetPlanReviewScheduleDetailsByPlanReviewSchedule(planReviewSchedule.PlanReviewScheduleId.Value);

            if (planReview.SendEmail)
            {
                DateTime? earliestDate = planReviewSchedules.Min(x => x.StartDt);
                //jcl if there's no min date, this is all pooled
                if (earliestDate.HasValue == true && earliestDate.Value != DateTime.MinValue)
                {

                    SendEmailNotification(planReview, isCancellation, earliestDate.Value);
                }
            }

            if (status == AppointmentResponseStatusEnum.Not_Scheduled)
            {
                planReview.AssignedReviewers = new List<AttendeeInfo>();
                UpdateDepartments(planReview);

                _PlanReviewScheduleDetailBEs = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(planReview.PlanReviewScheduleId.Value);
                ResetPlanReviewSchedules(_PlanReviewScheduleDetailBEs, planReview.UpdatedUser.ID);
            }

            return updatePlanReview;
        }

        public bool UpdatePlanReview(PlanReviewScheduleBE planReviewScheduleBE, AppointmentResponseStatusEnum status)
        {
            try
            {
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();

                PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();

                List<PlanReview> planReviewsByCycle = GetPlanReviewsByProjectCycle(planReviewScheduleBE.ProjectCycleId.Value);
                
                int projectId = planReviewsByCycle.FirstOrDefault().ProjectId;

                ProjectEstimation projectEstimation = estimationCRUDAdapter.GetProjectDetailsByProjectId(projectId);

                PlanReview planReviewToBeRemoved = new PlanReview();

                if (planReviewScheduleBE.IsRescheduleInd.HasValue && planReviewScheduleBE.IsRescheduleInd.Value == true)
                {
                    switch (status)
                    {
                        case AppointmentResponseStatusEnum.Accept:
                        case AppointmentResponseStatusEnum.Scheduled:
                            planReviewScheduleBE.IsRescheduleInd = false;
                            
                            planReviewToBeRemoved = planReviewsByCycle.FirstOrDefault(x => x.IsReschedule == false);

                            int rows = planReviewScheduleBO.Update(planReviewScheduleBE);

                            break;
                        case AppointmentResponseStatusEnum.Not_Scheduled:
                            planReviewToBeRemoved = planReviewsByCycle.FirstOrDefault(x => x.IsReschedule == true);
                            break;
                    }

                    projectEstimation.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Scheduled);

                    estimationCRUDAdapter.SaveProjectEstimationDetails(projectEstimation);
                }
                else
                {
                    planReviewScheduleBO.Update(planReviewScheduleBE);
                }

                if (planReviewToBeRemoved != null && planReviewToBeRemoved.ID > 0)
                {
                    DeletePlanReview(planReviewToBeRemoved);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdatePlanReview - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool DeletePlanReview(PlanReview planReview)
        {
            bool success = false;

            try
            {
                PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();
                PlanReviewScheduleDetailBO planReviewScheduleDetailBO = new PlanReviewScheduleDetailBO();
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();
                UserScheduleBO userScheduleBO = new UserScheduleBO();

                // delete project and user schedules
                List<ProjectScheduleBE> projectScheduleBEs = projectScheduleBO.GetByApptId(planReview.PlanReviewScheduleId.Value, planReview.ProjectScheduleRefEnum.ToString());
                projectScheduleBE = projectScheduleBEs.FirstOrDefault();
                List<UserScheduleBE> userScheduleBEs = userScheduleBO.GetListByScheduleID(projectScheduleBE.ProjectScheduleID.Value);
                string userScheduleIds = string.Join(",", userScheduleBEs.Select(x => x.UserScheduleID).ToList());
                userScheduleBO.DeleteByUserScheduleIds(userScheduleIds);
                projectScheduleBO.Delete(projectScheduleBE.ProjectScheduleID.Value);

                List<PlanReviewScheduleDetailBE> planReviewScheduleDetailBEs = planReviewScheduleDetailBO.GetListByPlanReviewScheduleId(planReview.PlanReviewScheduleId.Value);
                foreach (var planReviewScheduleDetail in planReviewScheduleDetailBEs)
                {
                    planReviewScheduleDetailBO.Delete(planReviewScheduleDetail.PlanReviewScheduleDetailId.Value);
                }

                int rows = planReviewScheduleBO.Delete(planReview.PlanReviewScheduleId.Value);

                success = rows > 0;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter DeletePlanReview - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }

        public void SendEmailNotification(PlanReview planReview, bool isCancellation, DateTime earliestDate)
        {
            _planreview = planReview;
            ProjectEstimation projectEstimation = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(planReview.ProjectId);

            _projectEstimation = projectEstimation;

            int facilitatorId = projectEstimation.AssignedFacilitator.HasValue ? projectEstimation.AssignedFacilitator.Value : 0;
            Facilitator facilitator = new FacilitatorModelBO().GetInstance(facilitatorId);

            CreatePlanReviewScheduledEmail(projectEstimation.PMEmail, isCancellation, earliestDate, projectEstimation.EstimatedFee,
                                                           projectEstimation.AccelaProjectRefId, projectEstimation.ProjectName, projectEstimation.DisplayOnlyInformation.ProjectAddress,
                                                          facilitator.FirstName + " " + facilitator.LastName,
                                                          facilitator.Phone, facilitator.Email);
        }

        public bool ScheduleFuturePRCycle(PlanReview pr)
        {
            DepartmentModelBO dept_bo = new DepartmentModelBO();

            ProjectEstimation project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(pr.ProjectId);

            List<PlanReview> planReviews = GetPlanReviewsByProjectId(pr.ProjectId);

            int planReviewScheduleId = 0;

            if (planReviews.Any(x => x.IsFutureCycle == true))
            {
                var existingFuturePlanReview = planReviews.FirstOrDefault(x => x.IsFutureCycle == true);

                //schedules to be updated
                _PlanReviewScheduleDetailBEs = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(existingFuturePlanReview.PlanReviewScheduleId.Value);
                planReviewScheduleId = _PlanReviewScheduleDetailBEs.FirstOrDefault().PlanReviewScheduleId.Value;
            }
            else
            {
                // process a new cycle and previous schedules
                ProjectCycleProcessor cycleProcessor = new ProjectCycleProcessor(project);
                _PlanReviewScheduleDetailBEs = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(cycleProcessor.CurrentCycleBE.ProjectCycleId.Value);

                _ProjectCycleBE = cycleProcessor.NewCycleBE;
                _PlanReviewScheduleBE = new PlanReviewScheduleBE()
                {
                    ProjectCycleId = _ProjectCycleBE.ProjectCycleId.Value,
                    ApptResponseStatusRefId = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Not_Scheduled).ApptResponseStatusRefId,
                    ProjectScheduleTypDesc = ProjectScheduleRefEnum.PR.ToString(),
                    IsRescheduleInd = false,
                    ApptCancellationRefId = null,
                    IsActive = true,
                    UpdatedByWkrId = pr.UpdatedUser.ID.ToString(),
                    UserId = pr.UpdatedUser.ID.ToString()
                };

                planReviewScheduleId = _PlanReviewScheduleBO.Create(_PlanReviewScheduleBE);

                foreach (PlanReviewScheduleDetailBE scheduleDetail in _PlanReviewScheduleDetailBEs)
                {
                    scheduleDetail.PlanReviewScheduleId = planReviewScheduleId;
                    _PlanReviewScheduleDetailBO.Create(scheduleDetail);
                }
            }

            foreach (ProjectTrade trade in project.Trades)
            {
                int businessRefId = (int)trade.DepartmentInfo;

                decimal? hours = null;
                switch (trade.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:
                        if (pr.ProposedBuilding.HasValue && (pr.BuildPool.HasValue && pr.BuildPool.Value == true || pr.BuildStartDate != DateTime.MinValue && pr.BuildEndDate != DateTime.MinValue))
                            hours = pr.ProposedBuilding.Value;
                        break;
                    case DepartmentNameEnums.Electrical:
                        if (pr.ProposedElectric.HasValue && (pr.ElectPool.HasValue && pr.ElectPool.Value == true || pr.ElectStartDate != DateTime.MinValue && pr.ElectEndDate != DateTime.MinValue))
                            hours = pr.ProposedElectric.Value;
                        break;
                    case DepartmentNameEnums.Mechanical:
                        if (pr.ProposedMech.HasValue && (pr.MechaPool.HasValue && pr.MechaPool.Value == true || pr.MechaStartDate != DateTime.MinValue && pr.MechaEndDate != DateTime.MinValue))
                            hours = pr.ProposedMech.Value;
                        break;
                    case DepartmentNameEnums.Plumbing:
                        if (pr.ProposedPlumb.HasValue && (pr.PlumbPool.HasValue && pr.PlumbPool.Value == true || pr.PlumbStartDate != DateTime.MinValue && pr.PlumbEndDate != DateTime.MinValue))
                            hours = pr.ProposedPlumb.Value;
                        break;
                    default:
                        break;
                }
                if (hours.HasValue)
                {
                    if (_PlanReviewScheduleDetailBEs.Any(x => x.BusinessRefId == businessRefId))
                    {
                        // get the plan review schedule detail and update it with the hours and date
                        _PlanReviewScheduleDetailBE = _PlanReviewScheduleDetailBEs.FirstOrDefault(x => x.BusinessRefId == businessRefId);
                        _PlanReviewScheduleDetailBE.AssignedHoursNbr = hours == 0 ? null : hours;
                        _PlanReviewScheduleDetailBO.Update(_PlanReviewScheduleDetailBE);
                    }
                }
            }
            foreach (ProjectAgency agency in project.Agencies)
            {
                int businessRefId = (int)agency.DepartmentInfo;

                decimal? hours = null;
                switch (agency.DepartmentInfo)
                {
                    case DepartmentNameEnums.EH_Day_Care:
                        if (pr.ProposedDayCare.HasValue && (pr.DaycPool.HasValue && pr.DaycPool.Value == true || pr.DaycStartDate != DateTime.MinValue && pr.DaycEndDate != DateTime.MinValue))
                            hours = pr.ProposedDayCare.Value;
                        break;
                    case DepartmentNameEnums.EH_Food:
                        if (pr.ProposedFood.HasValue && (pr.FoodPool.HasValue && pr.FoodPool.Value == true || pr.FoodStartDate != DateTime.MinValue && pr.FoodEndDate != DateTime.MinValue))
                            hours = pr.ProposedFood.Value;
                        break;
                    case DepartmentNameEnums.EH_Pool:
                        if (pr.ProposedPool.HasValue && (pr.PoolPool.HasValue && pr.PoolPool.Value == true || pr.PoolStartDate != DateTime.MinValue && pr.PoolEndDate != DateTime.MinValue))
                            hours = pr.ProposedPool.Value;
                        break;
                    case DepartmentNameEnums.EH_Facilities:
                        if (pr.ProposedLodge.HasValue && (pr.FacilPool.HasValue && pr.FacilPool.Value == true || pr.FacilStartDate != DateTime.MinValue && pr.FacilEndDate != DateTime.MinValue))
                            hours = pr.ProposedLodge.Value;
                        break;
                    case DepartmentNameEnums.Backflow:
                        if (pr.ProposedBackFlow.HasValue && (pr.BackfPool.HasValue && pr.BackfPool.Value == true || pr.BackfStartDate != DateTime.MinValue && pr.BackfEndDate != DateTime.MinValue))
                            hours = pr.ProposedBackFlow.Value;
                        break;
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        if (pr.ProposedZoning.HasValue && (pr.ZonePool.HasValue && pr.ZonePool.Value == true || pr.ZoneStartDate != DateTime.MinValue && pr.ZoneEndDate != DateTime.MinValue))
                            hours = pr.ProposedZoning.Value;
                        break;
                    case DepartmentNameEnums.Fire_Davidson:
                    case DepartmentNameEnums.Fire_Cornelius:
                    case DepartmentNameEnums.Fire_Pineville:
                    case DepartmentNameEnums.Fire_Matthews:
                    case DepartmentNameEnums.Fire_Mint_Hill:
                    case DepartmentNameEnums.Fire_Huntersville:
                    case DepartmentNameEnums.Fire_UMC:
                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                    case DepartmentNameEnums.Fire_County:
                        if (pr.ProposedFire.HasValue && (pr.FirePool.HasValue && pr.FirePool.Value == true || pr.FireStartDate != DateTime.MinValue && pr.FireEndDate != DateTime.MinValue))
                            hours = pr.ProposedFire.Value;
                        break;
                    default:
                        break;
                }
                if (hours.HasValue)
                {
                    if (_PlanReviewScheduleDetailBEs.Any(x => x.BusinessRefId == businessRefId))
                    {
                        // get the plan review schedule detail and update it with the hours and date
                        _PlanReviewScheduleDetailBE = _PlanReviewScheduleDetailBEs.FirstOrDefault(x => x.BusinessRefId == businessRefId);
                        _PlanReviewScheduleDetailBE.AssignedHoursNbr = hours == 0 ? null : hours;
                        _PlanReviewScheduleDetailBO.Update(_PlanReviewScheduleDetailBE);
                    }
                }
            }
            return true;
        }

        public bool UpdatePlanReviewStatusByAccela()
        {
            List<string> listStrLineElements;

            EstimationAccelaAdapter _estimationAccelaAdapter = new EstimationAccelaAdapter();
            List<string> planReviewScheduleBEList = new List<string>();
            PlanReviewScheduleBOOLD planReviewScheduleBO = new PlanReviewScheduleBOOLD();
            List<AIONQueueRecordBE> records = _estimationAccelaAdapter.GetPlanReviewStatusList();
            AccelaMappingHelper aPIHelper = new AccelaMappingHelper();
            foreach (AIONQueueRecordBE record in records)
            {
                listStrLineElements = aPIHelper.AccelaWorkFlowTaskToDepartment(record.WORKFLOW_TASK_NM);
                var businessRefList = String.Join(",", listStrLineElements);

                planReviewScheduleBEList = planReviewScheduleBO.GetByAccelaWorkFlowTaskUpdate(record.REC_ID_NUM, businessRefList, record.UPDATED_DTTM);
                var planReviewScheduleIDList = String.Join(",", planReviewScheduleBEList);
                planReviewScheduleBO.UpdatePlanReviewStatus(planReviewScheduleIDList);
            }

            return true;
        }

        public List<PlanReviewSchedule> GetPreviousPlanReviewSchedules(ProjectCycle projectCycle)
        {
            if (projectCycle.CycleNbr.HasValue)
            {
                int previousCycleNbr = projectCycle.CycleNbr.Value - 1;

                ProjectCycle previousCycle = GetProjectCyclesByProjectId(projectCycle.ProjectId.Value).FirstOrDefault(x => x.CycleNbr.Value == previousCycleNbr);

                List<PlanReviewSchedule> planReviewSchedules = GetPlanReviewSchedulesByProjectCycle(previousCycle.ID);

                return planReviewSchedules;
            }

            return null;
        }

        public bool ProcessAllPoolSubsequentCycle(ProjectCycle projectCycle)
        {
            string errorMessage = "";

            try
            {
                AppointmentResponseStatusModelBO appointmentResponse = new AppointmentResponseStatusModelBO();

                int projectId = projectCycle.ProjectId.Value;
                ProjectBE projectBE = new ProjectBO().GetById(projectId);

                _PlanReviewScheduleBE = new PlanReviewScheduleBE();
                _PlanReviewScheduleBE.ProjectCycleId = projectCycle.ID;
                _PlanReviewScheduleBE.ApptResponseStatusRefId = appointmentResponse.GetInstance(AppointmentResponseStatusEnum.Scheduled).ApptResponseStatusRefId;
                _PlanReviewScheduleBE.IsActive = true;
                _PlanReviewScheduleBE.IsRescheduleInd = false;
                _PlanReviewScheduleBE.ProjectScheduleTypDesc = ProjectScheduleRefEnum.PR.ToString();
                _PlanReviewScheduleBE.CreatedByWkrId = projectCycle.CreatedUser.ID.ToString();
                _PlanReviewScheduleBE.UpdatedByWkrId = projectCycle.CreatedUser.ID.ToString();
                _PlanReviewScheduleBE.UserId = projectCycle.CreatedUser.ID.ToString();

                _PlanReviewScheduleBE.PlanReviewScheduleId = _PlanReviewScheduleBO.Create(_PlanReviewScheduleBE);

                List<PlanReview> planReviews = GetPlanReviewsByProjectCycle(projectCycle.ID);
                PlanReview planReview = planReviews.FirstOrDefault(x => x.IsReschedule == false);
                planReview.PlanReviewScheduleId = _PlanReviewScheduleBE.PlanReviewScheduleId;

                List<ProjectCycleDetail> projectCycleDetails = GetProjectCycleDetailsByProjectCycleId(projectCycle.ID);

                PlanReviewSchedule previousPlanReviewSchedule = GetPreviousPlanReviewSchedules(projectCycle).FirstOrDefault(x => x.IsRescheduleInd == false);

                List<PlanReviewScheduleDetail> previousSchedules = GetPlanReviewScheduleDetailsByPlanReviewSchedule(previousPlanReviewSchedule.ID);

                int planReviewScheduleId = 0;

                foreach (PlanReviewScheduleDetail planReviewScheduleDetailPrevious in previousSchedules)
                {
                    if (planReviewScheduleDetailPrevious.AssignedPlanReviewerId != null)
                    {
                        // get project cycle detail
                        ProjectCycleDetail cycleDetail = projectCycleDetails.FirstOrDefault(x => x.BusinessRefId == planReviewScheduleDetailPrevious.BusinessRefId);

                        if (cycleDetail != null)
                        {
                            planReview.AssignedReviewers.Add(new AttendeeInfo()
                            {
                                AttendeeId = planReviewScheduleDetailPrevious.AssignedPlanReviewerId.Value,
                                BusinessRefId = planReviewScheduleDetailPrevious.BusinessRefId.Value,
                                DeptNameEnumId = (int)new DepartmentModelBO().GetInstance(planReviewScheduleDetailPrevious.BusinessRefId.Value).DepartmentEnum
                            });

                            planReviewScheduleId = CreatePooledPlanReviewScheduleDetailRecord(cycleDetail, planReviewScheduleDetailPrevious.AssignedPlanReviewerId.Value);
                        }
                        else // no match on dept - check fire and zone agencies
                        {
                            bool previousIsFireAgency = FireAndZoneAgencyHelper.IsFireAgency(planReviewScheduleDetailPrevious.BusinessRefId.Value);
                            bool previousIsZoneAgency = FireAndZoneAgencyHelper.IsZoneAgency(planReviewScheduleDetailPrevious.BusinessRefId.Value);

                            if (previousIsFireAgency)
                            {
                                ProjectCycleDetail fireProjectCycleDetail = GetMatchingAgencyCycleDetail(projectCycleDetails, true);

                                if (fireProjectCycleDetail != null)
                                {
                                    planReview.AssignedReviewers.Add(new AttendeeInfo()
                                    {
                                        AttendeeId = planReviewScheduleDetailPrevious.AssignedPlanReviewerId.Value,
                                        BusinessRefId = planReviewScheduleDetailPrevious.BusinessRefId.Value,
                                        DeptNameEnumId = (int)new DepartmentModelBO().GetInstance(planReviewScheduleDetailPrevious.BusinessRefId.Value).DepartmentEnum
                                    });

                                    planReviewScheduleId = CreatePooledPlanReviewScheduleDetailRecord(fireProjectCycleDetail, planReviewScheduleDetailPrevious.AssignedPlanReviewerId.Value);
                                }
                            }

                            if (previousIsZoneAgency)
                            {
                                ProjectCycleDetail zoneProjectCycleDetail = GetMatchingAgencyCycleDetail(projectCycleDetails, false);

                                if (zoneProjectCycleDetail != null)
                                {
                                    planReview.AssignedReviewers.Add(new AttendeeInfo()
                                    {
                                        AttendeeId = planReviewScheduleDetailPrevious.AssignedPlanReviewerId.Value,
                                        BusinessRefId = planReviewScheduleDetailPrevious.BusinessRefId.Value,
                                        DeptNameEnumId = (int)new DepartmentModelBO().GetInstance(planReviewScheduleDetailPrevious.BusinessRefId.Value).DepartmentEnum
                                    });

                                    planReviewScheduleId = CreatePooledPlanReviewScheduleDetailRecord(zoneProjectCycleDetail, planReviewScheduleDetailPrevious.AssignedPlanReviewerId.Value);
                                }
                            }
                        }
                    }
                }

                PlanReview subCycleReview = new PlanReview();

                // build plan review object by planReviewScheduleId
                if (planReview.PlanReviewScheduleId > 0)
                {
                    subCycleReview = GetPlanReviewByPlanReviewScheduleId(planReview.PlanReviewScheduleId.Value);
                    if (subCycleReview != null && subCycleReview.ID > 0)
                    {
                        AccelaBOAdapter sendSubCycle = new AccelaBOAdapter();
                        sendSubCycle.SchedulePlanReview(subCycleReview);
                    }
                }

                bool projectUpdated = SaveScheduledProjectUpdate(projectId, _PlanReviewScheduleBE.UserId);
            }
            catch (Exception ex)
            {
                errorMessage = "Error in PlanReviewAdapter ProcessAllPoolSubsequentCycle - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool SaveScheduledProjectUpdate(int projectId, string userId)
        {
            string errorMessage = string.Empty;

            bool success = false;

            try
            {
                ProjectBO projectBO = new ProjectBO();
                ProjectBE projectBE = projectBO.GetById(projectId);
                int existingStatus = new ProjectStatusRefBO().GetById(projectBE.ProjectStatusRefId.Value).ProjectStatusRefId.Value;

                int scheduledStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Scheduled).ID;
                projectBE.ProjectStatusRefId = scheduledStatus;
                projectBE.UserId = userId;
                projectBE.PlansReadyOnDt = null;

                int rows = projectBO.Update(projectBE);

                new ProjectAuditModelBO().InsertProjectAudit(projectId, ProjectStatusEnum.Scheduled.ToStringValue(), "1", AuditActionEnum.Status_Changed);

                ProjectEstimation pe = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(projectId);
                int facilitatorId = pe.AssignedFacilitator.HasValue ? pe.AssignedFacilitator.Value : 0;

                PlanReviewEmailModel model = new PlanReviewEmailModel()
                {
                    AccelaProjectRefId = pe.AccelaProjectRefId,
                    FacilitatorId = facilitatorId,
                    ProjectAddress = pe.DisplayOnlyInformation.ProjectAddress,
                    ProjectName = pe.ProjectName,
                    RecIdTxt = pe.RecIdTxt
                };

                CreateNewCycleSchedulingNotRequiredEmail(model);

                success = rows > 0;
            }
            catch (Exception ex)
            {
                errorMessage = "Error in PlanReviewAdapter SaveScheduledProjectUpdate - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool UpdateAttendeeSchedules(int projectScheduleId, List<AttendeeInfo> attendees, int wkrId)
        {
            try
            {
                _ProjectScheduleBE = _ProjectScheduleBO.GetById(projectScheduleId);

                PlanReview planReview = GetPlanReviewByProjectScheduleId(projectScheduleId);
                planReview.AssignedReviewers = attendees;
                planReview.NewAttendees = attendees;
                planReview.UpdatedUser = new UserIdentityModelBO().GetInstance(wkrId);

                List<PlanReviewScheduleDetailBE> existingPlanReviewSchedules = new List<PlanReviewScheduleDetailBE>();
                List<PlanReviewScheduleDetailBE> newPlanReviewSchedules = new List<PlanReviewScheduleDetailBE>();

                existingPlanReviewSchedules = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(_PlanReviewScheduleBE.PlanReviewScheduleId.Value);
                planReview.HoursDefault = existingPlanReviewSchedules.First().AssignedHoursNbr.Value;
                newPlanReviewSchedules = BuildUpdatedPlanReviewSchedules(planReview, _PlanReviewScheduleBE.PlanReviewScheduleId.Value, existingPlanReviewSchedules);

                bool updateSchedules = UpsertSchedules(existingPlanReviewSchedules, newPlanReviewSchedules, attendees);
                List<AttendeeInfo> processedPRAttendees = ProcessPlanReviewAttendees(planReview, newPlanReviewSchedules, existingPlanReviewSchedules);
                bool processedApptAttendees = ProcessAppointmentAttendees(planReview);

                bool success = FinalizePlanReview(planReview, null, null);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdateAttendeeSchedules - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool UpsertPlanReview(PlanReview planReview)
        {
            string errorMessage = "";

            if (planReview == null)
            {
                errorMessage = "Plan review is null";

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw new Exception();
            }

            try
            {
                int projectId = planReview.ProjectId;
                //enter audit for auto schedule Y/N
                ProjectAuditModelBO.InsertAutoScheduledAudit(projectId, planReview.UpdatedUser.ID.ToString(), planReview.AutoScheduled ? "Y" : "N");

                if (planReview.ProjectScheduleRefEnum == ProjectScheduleRefEnum.EMA)
                {
                    planReview.CancelAfterDt = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 2);
                }

                ProjectBE projectBE = new ProjectBO().GetById(projectId);

                ProjectCycleSummary projectCycleSummary = GetProjectCycleSummary(projectBE.ProjectId.Value, projectBE);

                DateTime? prevGateDate = projectBE.GateDt;

                DateTime? gateDate = projectBE.FifoInd == true ? null : GenerateGateDateForCycle(planReview);

                SetProjectCycle(projectCycleSummary, planReview, gateDate);

                SetPlanReviewSchedule(projectCycleSummary, planReview);

                planReview.ProjectCycle = new ProjectCycleModelBO().ConvertBEToModel(_ProjectCycleBE);

                List<PlanReviewScheduleDetailBE> existingPlanReviewSchedules = new List<PlanReviewScheduleDetailBE>();
                List<PlanReviewScheduleDetailBE> newPlanReviewSchedules = new List<PlanReviewScheduleDetailBE>();

                existingPlanReviewSchedules = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(_PlanReviewScheduleBE.PlanReviewScheduleId.Value);
                newPlanReviewSchedules = BuildUpdatedPlanReviewSchedules(planReview, _PlanReviewScheduleBE.PlanReviewScheduleId.Value, existingPlanReviewSchedules);

                // determine new vs. update existing schedules
                //upsert
                bool updateSchedules = UpsertSchedules(existingPlanReviewSchedules, newPlanReviewSchedules, planReview.AssignedReviewers);

                _PlanReviewScheduleDetailBEs = _PlanReviewScheduleDetailBO.GetListByPlanReviewScheduleId(_PlanReviewScheduleBE.PlanReviewScheduleId.Value);

                planReview.EarliestDate = _PlanReviewScheduleDetailBEs
                    .Where(x => x.StartDt.HasValue && x.StartDt != null && x.StartDt != DateTime.MinValue)
                    .Min(x => x.StartDt);
                planReview.MaxDate = _PlanReviewScheduleDetailBEs.Max(x => x.StartDt);

                //update plan review schedule response status if all pool and sub cycle
                if ((planReview.ProjectScheduleRefEnum != ProjectScheduleRefEnum.FIFO && planReview.AllPool)
                    || (planReview.ProjectScheduleRefEnum == ProjectScheduleRefEnum.FIFO && planReview.AllHoursOneOrLess))
                {
                    // blank out PROD
                    _ProjectCycleBE = _ProjectCycleBO.GetById(_ProjectCycleBE.ProjectCycleId.Value);
                    _ProjectCycleBE.PlansReadyOnDt = null;
                    _ProjectCycleBO.Update(_ProjectCycleBE);

                    // set plan review status to accepted
                    int scheduledStatusRefId = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Scheduled).ApptResponseStatusRefId.Value;
                    _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(_PlanReviewScheduleBE.PlanReviewScheduleId.Value);
                    _PlanReviewScheduleBE.ApptResponseStatusRefId = scheduledStatusRefId;
                    _PlanReviewScheduleBO.Update(_PlanReviewScheduleBE);
                }

                //if any rows are not pool, then create a project schedule
                if (_PlanReviewScheduleDetailBEs.Any(x => x.PoolRequestInd == false))
                {
                    SetProjectSchedule(planReview);
                }

                List<AttendeeInfo> attendees = ProcessPlanReviewAttendees(planReview, _PlanReviewScheduleDetailBEs, existingPlanReviewSchedules);

                if (planReview.IsSubmit)
                    new ProjectAuditModelBO().InsertProjectAuditPR(attendees, planReview.ProjectId, "Schedule Plan Review", planReview.UpdatedUser.ID.ToString(), planReview.ApptResponseStatusEnum);


                bool success = FinalizePlanReview(planReview, gateDate, prevGateDate);

                if ((planReview.ProjectScheduleRefEnum != ProjectScheduleRefEnum.FIFO && planReview.AllPool)
                    || (planReview.ProjectScheduleRefEnum == ProjectScheduleRefEnum.FIFO))
                {
                    AccelaBOAdapter adapter = new AccelaBOAdapter();
                    adapter.SchedulePlanReview(planReview);
                }

                //remove the staging once saving is done.
                UserScheduleStageBO stgBo = new UserScheduleStageBO();
                stgBo.Delete(planReview.ProjectId);

                return success;
            }
            catch (System.Exception ex)
            {
                errorMessage = "Error in PlanReviewAdapter UpsertPlanReview - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public void SetProjectCycle(ProjectCycleSummary projectCycleSummary, PlanReview planReview, DateTime? gateDate)
        {
            ProjectCycleBO projectCycleBO = new ProjectCycleBO();

            if (planReview.IsFutureCycle)
            {
                if (planReview.IsFutureCycle && projectCycleSummary.ProjectCycleFuture != null)
                {
                    _ProjectCycleBE = projectCycleBO.GetById(projectCycleSummary.ProjectCycleFuture.ID);
                }
                else
                {
                    //create future cycle
                    int newCycleNbr = 0;

                    if (projectCycleSummary.ProjectCycleCurrent != null)
                    {
                        newCycleNbr = projectCycleSummary.ProjectCycleCurrent.CycleNbr.Value + 1;
                    }

                    ProjectCycleBE newCycle = new ProjectCycleBE();
                    newCycle.ProjectId = planReview.ProjectId;
                    newCycle.CurrentCycleInd = false;
                    newCycle.FutureCycleInd = true;
                    newCycle.CycleNbr = newCycleNbr;
                    newCycle.IsActive = true;
                    newCycle.IsAprvInd = null;
                    newCycle.IsCompleteInd = false;
                    newCycle.PlansReadyOnDt = planReview.ScheduleAfterDate;
                    newCycle.ResponderUserId = null;
                    newCycle.ResponseDt = null;
                    newCycle.UserId = planReview.CreatedUser.ID.ToString();

                    newCycle.ProjectCycleId = projectCycleBO.Create(newCycle);

                    _ProjectCycleBE = projectCycleBO.GetById(newCycle.ProjectCycleId.Value);
                }
            }

            if (!planReview.IsFutureCycle)
            {
                _ProjectCycleBE = projectCycleBO.GetById(projectCycleSummary.ProjectCycleCurrent.ID);
            }

            _ProjectCycleBE.ScheduleAfterDt = planReview.ScheduleAfterDate;

            if (!_ProjectCycleBE.ScheduleAfterDt.HasValue)
            {
                _ProjectCycleBE.ScheduleAfterDt = DateTime.Now;
            }

            if (planReview.ResetApproval == true)
            {
                _ProjectCycleBE.IsAprvInd = null;
                _ProjectCycleBE.ResponderUserId = null;
                _ProjectCycleBE.ResponseDt = null;
            }

            if (planReview.ProjectScheduleRefEnum == ProjectScheduleRefEnum.FIFO)
            {
                _ProjectCycleBE.IsAprvInd = true;
                _ProjectCycleBE.ResponderUserId = planReview.CreatedUser.ID;
                _ProjectCycleBE.ResponseDt = DateTime.Now;
            }

            _ProjectCycleBE.GateDt = gateDate;

            _ProjectCycleBO.Update(_ProjectCycleBE);
        }
        /// <summary>
        /// Creates Plan Review Schedule, newPlanReview must have apptResponseStatusEnum or apptResponseStatusRefId
        /// otherwise this gets set to tentatively scheduled
        /// </summary>
        /// <param name="projectCycleSummary"></param>
        /// <param name="newPlanReview"></param>
        public void SetPlanReviewSchedule(ProjectCycleSummary projectCycleSummary, PlanReview newPlanReview)
        {
            int planReviewScheduleId = 0;
            //jcl - get the status from the plan review
            int planReviewStatusRefId = (newPlanReview.ApptResponseStatusRefId == 0)
                ? new AppointmentResponseStatusModelBO().GetInstance(newPlanReview.ApptResponseStatusEnum).ApptResponseStatusRefId.Value
                : newPlanReview.ApptResponseStatusRefId;

            if (planReviewStatusRefId <= 0)
            {
                planReviewStatusRefId = new AppointmentResponseStatusModelBO().GetInstance(AppointmentResponseStatusEnum.Tentatively_Scheduled).ApptResponseStatusRefId.Value;
            }

            if (newPlanReview.IsReschedule)
            {
                //create new plan review schedule records with a status of tentatively scheduled
                _PlanReviewScheduleBE.ApptResponseStatusRefId = planReviewStatusRefId;
                _PlanReviewScheduleBE.UserId = newPlanReview.UpdatedUser.ID.ToString();
                _PlanReviewScheduleBE.ProjectCycleId = _ProjectCycleBE.ProjectCycleId.Value;
                _PlanReviewScheduleBE.ProjectScheduleTypDesc = newPlanReview.ProjectScheduleRefEnum.ToString();
                _PlanReviewScheduleBE.IsRescheduleInd = true;
                _PlanReviewScheduleBE.MeetingRoomRefId = newPlanReview.MeetingRoomRefId;
                _PlanReviewScheduleBE.Proposed1Dt = newPlanReview.ProposedDate1;
                _PlanReviewScheduleBE.Proposed2Dt = newPlanReview.ProposedDate2;
                _PlanReviewScheduleBE.Proposed3Dt = newPlanReview.ProposedDate3;
                _PlanReviewScheduleBE.CancelAfterDt = newPlanReview.CancelAfterDt;
                _PlanReviewScheduleBE.VirtualMeetingInd = newPlanReview.VirtualMeetingInd;

                planReviewScheduleId = _PlanReviewScheduleBO.Create(_PlanReviewScheduleBE);
                _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(planReviewScheduleId);
            }
            else
            {
                // future cycle with existing plan review record
                if (newPlanReview.IsFutureCycle && projectCycleSummary.PlanReviewFuture != null && projectCycleSummary.PlanReviewFuture.ID > 0)
                {
                    _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(projectCycleSummary.PlanReviewFuture.ID);
                }
                // future cycle with no existing plan review record
                if (newPlanReview.IsFutureCycle && projectCycleSummary.PlanReviewFuture == null)
                {
                    // create new schedule
                    _PlanReviewScheduleBE.ApptResponseStatusRefId = planReviewStatusRefId;
                    _PlanReviewScheduleBE.UserId = newPlanReview.UpdatedUser.ID.ToString();
                    _PlanReviewScheduleBE.ProjectCycleId = _ProjectCycleBE.ProjectCycleId.Value;
                    _PlanReviewScheduleBE.ProjectScheduleTypDesc = newPlanReview.ProjectScheduleRefEnum.ToString();
                    _PlanReviewScheduleBE.IsRescheduleInd = false;
                    _PlanReviewScheduleBE.MeetingRoomRefId = newPlanReview.MeetingRoomRefId;
                    _PlanReviewScheduleBE.Proposed1Dt = newPlanReview.ProposedDate1;
                    _PlanReviewScheduleBE.Proposed2Dt = newPlanReview.ProposedDate2;
                    _PlanReviewScheduleBE.Proposed3Dt = newPlanReview.ProposedDate3;
                    _PlanReviewScheduleBE.CancelAfterDt = newPlanReview.CancelAfterDt;
                    _PlanReviewScheduleBE.VirtualMeetingInd = newPlanReview.VirtualMeetingInd;

                    planReviewScheduleId = _PlanReviewScheduleBO.Create(_PlanReviewScheduleBE);
                    _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(planReviewScheduleId);
                }
                // current cycle with existing plan review record
                if (!newPlanReview.IsFutureCycle && projectCycleSummary.PlanReviewCurrent != null && projectCycleSummary.PlanReviewCurrent.ID > 0)
                {
                    _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(projectCycleSummary.PlanReviewCurrent.ID);

                    //update existing plan review
                    _PlanReviewScheduleBE.IsRescheduleInd = newPlanReview.IsReschedule;
                    _PlanReviewScheduleBE.ApptResponseStatusRefId = planReviewStatusRefId;
                    _PlanReviewScheduleBE.MeetingRoomRefId = newPlanReview.MeetingRoomRefId;
                    _PlanReviewScheduleBE.Proposed1Dt = newPlanReview.ProposedDate1;
                    _PlanReviewScheduleBE.Proposed2Dt = newPlanReview.ProposedDate2;
                    _PlanReviewScheduleBE.Proposed3Dt = newPlanReview.ProposedDate3;
                    _PlanReviewScheduleBE.CancelAfterDt = newPlanReview.CancelAfterDt;
                    _PlanReviewScheduleBE.VirtualMeetingInd = newPlanReview.VirtualMeetingInd;
                    _PlanReviewScheduleBE.UserId = newPlanReview.UpdatedUser.ID.ToString();

                    _PlanReviewScheduleBO.Update(_PlanReviewScheduleBE);
                }
                // current cycle with no existing plan review record
                if (!newPlanReview.IsFutureCycle && projectCycleSummary.PlanReviewCurrent == null)
                {
                    _PlanReviewScheduleBE.ApptResponseStatusRefId = planReviewStatusRefId;
                    _PlanReviewScheduleBE.UserId = newPlanReview.UpdatedUser.ID.ToString();
                    _PlanReviewScheduleBE.ProjectCycleId = _ProjectCycleBE.ProjectCycleId.Value;
                    _PlanReviewScheduleBE.ProjectScheduleTypDesc = newPlanReview.ProjectScheduleRefEnum.ToString();
                    _PlanReviewScheduleBE.IsRescheduleInd = false;
                    _PlanReviewScheduleBE.MeetingRoomRefId = newPlanReview.MeetingRoomRefId;
                    _PlanReviewScheduleBE.Proposed1Dt = newPlanReview.ProposedDate1;
                    _PlanReviewScheduleBE.Proposed2Dt = newPlanReview.ProposedDate2;
                    _PlanReviewScheduleBE.Proposed3Dt = newPlanReview.ProposedDate3;
                    _PlanReviewScheduleBE.CancelAfterDt = newPlanReview.CancelAfterDt;
                    _PlanReviewScheduleBE.VirtualMeetingInd = newPlanReview.VirtualMeetingInd;

                    planReviewScheduleId = _PlanReviewScheduleBO.Create(_PlanReviewScheduleBE);

                    _PlanReviewScheduleBE = _PlanReviewScheduleBO.GetById(planReviewScheduleId);
                }
            }
        }

        public bool UpsertSchedules(
            List<PlanReviewScheduleDetailBE> existingPlanReviewSchedules,
            List<PlanReviewScheduleDetailBE> newPlanReviewSchedules,
            List<AttendeeInfo> assignedReviewers)
        {
            if (existingPlanReviewSchedules.Count > 0)
            {
                foreach (PlanReviewScheduleDetailBE schedule in existingPlanReviewSchedules)
                {
                    if (!newPlanReviewSchedules.Any(x => x.BusinessRefId == schedule.BusinessRefId
                    && x.AssignedPlanReviewerId == schedule.AssignedPlanReviewerId))
                    {
                        _PlanReviewScheduleDetailBO.Delete(schedule.PlanReviewScheduleDetailId.Value);

                        _RemovedAttendees.Add(new AttendeeInfo()
                        {
                            AttendeeId = schedule.AssignedPlanReviewerId.Value,
                            BusinessRefId = schedule.BusinessRefId.Value,
                            DeptNameEnumId = (int)new DepartmentModelBO().GetInstance(schedule.BusinessRefId.Value).DepartmentEnum
                        });
                    }

                    if (newPlanReviewSchedules.Any(x => x.BusinessRefId == schedule.BusinessRefId
                    && x.AssignedPlanReviewerId == schedule.AssignedPlanReviewerId))
                    {
                        _PlanReviewScheduleDetailBE = newPlanReviewSchedules.FirstOrDefault(x => x.BusinessRefId == schedule.BusinessRefId && x.AssignedPlanReviewerId == schedule.AssignedPlanReviewerId);
                        _PlanReviewScheduleDetailBE.PlanReviewScheduleDetailId = schedule.PlanReviewScheduleDetailId;
                        _PlanReviewScheduleDetailBE.UpdatedDate = schedule.UpdatedDate;
                        _PlanReviewScheduleDetailBO.Update(_PlanReviewScheduleDetailBE);
                    }
                }

                foreach (PlanReviewScheduleDetailBE newSchedule in newPlanReviewSchedules)
                {
                    if (!existingPlanReviewSchedules.Any(x => x.BusinessRefId == newSchedule.BusinessRefId
                    && x.AssignedPlanReviewerId == newSchedule.AssignedPlanReviewerId))
                    {
                        _PlanReviewScheduleDetailBO.Create(newSchedule);

                        _InsertedAttendees.Add(assignedReviewers.FirstOrDefault(x => x.AttendeeId == newSchedule.AssignedPlanReviewerId && x.BusinessRefId == newSchedule.BusinessRefId));
                    }
                }
            }
            else
            {
                InsertSchedules(newPlanReviewSchedules);
            }
            return true;
        }

        public bool ResetPlanReviewSchedules(List<PlanReviewScheduleDetailBE> planReviewScheduleDetails, int wkrId)
        {
            foreach (PlanReviewScheduleDetailBE schedule in planReviewScheduleDetails)
            {
                schedule.AssignedPlanReviewerId = -1;
                schedule.StartDt = null;
                schedule.EndDt = null;
                schedule.UserId = wkrId.ToString();
                _PlanReviewScheduleDetailBO.Update(schedule);
            }
            
            return true;
        }

        public void SetProjectSchedule(PlanReview planReview)
        {
            _ProjectScheduleBE = _ProjectScheduleBO.GetByApptId(_PlanReviewScheduleBE.PlanReviewScheduleId.Value, planReview.ProjectScheduleRefEnum.ToString()).FirstOrDefault();

            if (_ProjectScheduleBE == null)
            {
                //TODO: create new project schedule!!!
                _ProjectScheduleBE = new ProjectScheduleBE();
                _ProjectScheduleBE.ProjectScheduleTypeRef = planReview.ProjectScheduleRefEnum.ToString();
                _ProjectScheduleBE.AppoinmentID = _PlanReviewScheduleBE.PlanReviewScheduleId.Value;
                _ProjectScheduleBE.RecurringApptDt = planReview.EarliestDate;
                _ProjectScheduleBE.UserId = planReview.UpdatedUser.ID.ToString();

                _ProjectScheduleBE.ProjectScheduleID = _ProjectScheduleBO.Create(_ProjectScheduleBE);
            }
            else // update project schedule with any possible date changes
            {
                _ProjectScheduleBE.RecurringApptDt = planReview.EarliestDate;
                _ProjectScheduleBE.UserId = planReview.UpdatedUser.ID.ToString();

                _ProjectScheduleBO.Update(_ProjectScheduleBE);
            }
        }

        public bool InsertSchedules(List<PlanReviewScheduleDetailBE> newPlanReviewSchedules)
        {
            foreach (PlanReviewScheduleDetailBE newSchedule in newPlanReviewSchedules)
            {
                _PlanReviewScheduleDetailBO.Create(newSchedule);
            }

            return true;
        }

        public DateTime? GenerateGateDateForCycle(PlanReview planReview)
        {
            DateTime? gateDate;

            //get earliest date
            DateTime mindate = DateTime.MaxValue;
            List<DateTime> dates = new List<DateTime>();
            if (planReview.BackfStartDate.HasValue && planReview.BackfStartDate != DateTime.MinValue) dates.Add(planReview.BackfStartDate.Value);
            if (planReview.BuildStartDate.HasValue && planReview.BuildStartDate != DateTime.MinValue) dates.Add(planReview.BuildStartDate.Value);
            if (planReview.DaycStartDate.HasValue && planReview.DaycStartDate != DateTime.MinValue) dates.Add(planReview.DaycStartDate.Value);
            if (planReview.ElectStartDate.HasValue && planReview.ElectStartDate != DateTime.MinValue) dates.Add(planReview.ElectStartDate.Value);
            if (planReview.FacilStartDate.HasValue && planReview.FacilStartDate != DateTime.MinValue) dates.Add(planReview.FacilStartDate.Value);
            if (planReview.FireStartDate.HasValue && planReview.FireStartDate != DateTime.MinValue) dates.Add(planReview.FireStartDate.Value);
            if (planReview.FoodStartDate.HasValue && planReview.FoodStartDate != DateTime.MinValue) dates.Add(planReview.FoodStartDate.Value);
            if (planReview.MechaStartDate.HasValue && planReview.MechaStartDate != DateTime.MinValue) dates.Add(planReview.MechaStartDate.Value);
            if (planReview.PlumbStartDate.HasValue && planReview.PlumbStartDate != DateTime.MinValue) dates.Add(planReview.PlumbStartDate.Value);
            if (planReview.PoolStartDate.HasValue && planReview.PoolStartDate != DateTime.MinValue) dates.Add(planReview.PoolStartDate.Value);
            if (planReview.ZoneStartDate.HasValue && planReview.ZoneStartDate != DateTime.MinValue) dates.Add(planReview.ZoneStartDate.Value);

            foreach (DateTime date in dates)
            {
                if (date < mindate)
                    mindate = date;
            }
            planReview.EarliestDate = mindate == DateTime.MaxValue ? (DateTime?)DateTime.MinValue : mindate;

            //Calculate new gate date
            int gateDateConfig = new AdminAdapter().GetGateDateConfig();

            if (planReview.EarliestDate != null && planReview.EarliestDate > DateTime.MinValue)
            {
                GateDateProcessor gateDateProcessor = new GateDateProcessor(gateDateConfig, planReview.EarliestDate.Value); //get earliest date

                //If all reviewers are assigned as pool then do not calculate a gate date. Gate date will be blank

                if (planReview.BuildPool.Value && planReview.ElectPool.Value && planReview.MechaPool.Value && planReview.PlumbPool.Value &&
                    planReview.FirePool.Value && planReview.ZonePool.Value && planReview.BackfPool.Value && planReview.PoolPool.Value &&
                    planReview.FoodPool.Value && planReview.FacilPool.Value && planReview.DaycPool.Value)
                {
                    gateDate = null;
                }
                else
                {
                    gateDate = gateDateProcessor.CalculateGateDate();
                }
            }
            else
            {
                gateDate = null;
            }

            return gateDate;
        }

        public List<PlanReviewScheduleDetailBE> BuildUpdatedPlanReviewSchedules(
            PlanReview planReview,
            int planReviewScheduleId,
            List<PlanReviewScheduleDetailBE> existingPlanReviewSchedules)
        {
            Helper helper = new Helper();

            List<PlanReviewScheduleDetailBE> planReviewSchedules = new List<PlanReviewScheduleDetailBE>();

            DepartmentModelBO departmentModelBO = new DepartmentModelBO();
            PlanReviewScheduleDetailBE planReviewSchedule = new PlanReviewScheduleDetailBE();

            int businessRefId = 0;
            AttendeeInfo attendeeInfo = new AttendeeInfo();
            int? assignedReviewerId;

            //building
            if ((planReview.BuildPool.HasValue && planReview.BuildPool.Value == true)
            || (planReview.BuildStartDate.HasValue && planReview.BuildStartDate != DateTime.MinValue && planReview.BuildEndDate != DateTime.MinValue)
            || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.Building && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.Building).ID;

                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    // check existing schedules
                    assignedReviewerId = planReview.BuildAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.BuildEndDate.HasValue ? planReview.BuildEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.BuildPool,
                    StartDt = planReview.BuildStartDate.HasValue ? planReview.BuildStartDate : planReview.StartDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursBuilding > 0 ? planReview.HoursBuilding : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //electrical
            if ((planReview.ElectPool.HasValue && planReview.ElectPool.Value == true)
            || (planReview.ElectStartDate.HasValue && planReview.ElectStartDate != DateTime.MinValue && planReview.ElectEndDate != DateTime.MinValue)
            || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.Electrical && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.Electrical).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.ElectricAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.ElectEndDate.HasValue ? planReview.ElectEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.ElectPool,
                    StartDt = planReview.ElectStartDate.HasValue ? planReview.ElectStartDate : planReview.StartDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursElectic > 0 ? planReview.HoursElectic : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //mechanical
            if ((planReview.MechaPool.HasValue && planReview.MechaPool.Value == true)
                || (planReview.MechaStartDate.HasValue && planReview.MechaStartDate != DateTime.MinValue && planReview.MechaEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.Mechanical && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.Mechanical).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.MechAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.MechaEndDate.HasValue ? planReview.MechaEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.MechaPool,
                    StartDt = planReview.MechaStartDate.HasValue ? planReview.MechaStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursMech > 0 ? planReview.HoursMech : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //plumbing
            if ((planReview.PlumbPool.HasValue && planReview.PlumbPool.Value == true)
                || (planReview.PlumbStartDate.HasValue && planReview.PlumbStartDate != DateTime.MinValue && planReview.PlumbEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.Plumbing && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.Plumbing).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.PlumbAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.PlumbEndDate.HasValue ? planReview.PlumbEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.PlumbPool,
                    StartDt = planReview.PlumbStartDate.HasValue ? planReview.PlumbStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursPlumb > 0 ? planReview.HoursPlumb : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            List<DepartmentNameEnums> fireDepartments = helper.FireDepartmentNames;
            List<int> fireBusinessRefIds = new List<int>();

            foreach (DepartmentNameEnums fireDepartment in fireDepartments)
            {
                fireBusinessRefIds.Add(departmentModelBO.GetInstance(fireDepartment).ID);
            }

            bool attendeesHaveFire = planReview.AssignedReviewers.FirstOrDefault(x => fireBusinessRefIds.Contains(x.DeptNameEnumId) && x.AttendeeId > 0) != null;

            //Fire

            if ((planReview.FirePool.HasValue && planReview.FirePool.Value == true)
                || (planReview.FireStartDate.HasValue && planReview.FireStartDate != DateTime.MinValue && planReview.FireEndDate != DateTime.MinValue)
                || (attendeesHaveFire))
            {
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => fireBusinessRefIds.Contains(x.DeptNameEnumId));
                PlanReviewScheduleDetailBE existingInfo = existingPlanReviewSchedules.FirstOrDefault(x => fireBusinessRefIds.Contains(x.BusinessRefId.Value));

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.FireAssignedReviewerId;
                    businessRefId = existingInfo.BusinessRefId.Value;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                    businessRefId = attendeeInfo.BusinessRefId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.FireEndDate.HasValue ? planReview.FireEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.FirePool,
                    StartDt = planReview.FireStartDate.HasValue ? planReview.FireStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursFire > 0 ? planReview.HoursFire : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            List<DepartmentNameEnums> zoneDepartments = helper.ZoneDepartmentNames;
            List<int> zoneBusinessRefIds = new List<int>();

            foreach (DepartmentNameEnums zoneDepartment in zoneDepartments)
            {
                zoneBusinessRefIds.Add(departmentModelBO.GetInstance(zoneDepartment).ID);
            }

            bool attendeesHaveZoning = planReview.AssignedReviewers.FirstOrDefault(x => zoneBusinessRefIds.Contains(x.DeptNameEnumId) && x.AttendeeId > 0) != null;

            //Zone
            if ((planReview.ZonePool.HasValue && planReview.ZonePool.Value == true)
            || (planReview.ZoneStartDate.HasValue && planReview.ZoneStartDate != DateTime.MinValue && planReview.ZoneEndDate != DateTime.MinValue)
            || (attendeesHaveZoning))
            {
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => zoneBusinessRefIds.Contains(x.DeptNameEnumId));
                PlanReviewScheduleDetailBE existingInfo = existingPlanReviewSchedules.FirstOrDefault(x => zoneBusinessRefIds.Contains(x.BusinessRefId.Value));

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.ZoningAssigedReviewerId;
                    businessRefId = existingInfo.BusinessRefId.Value;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                    businessRefId = attendeeInfo.BusinessRefId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.ZoneEndDate.HasValue ? planReview.ZoneEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.ZonePool,
                    StartDt = planReview.ZoneStartDate.HasValue ? planReview.ZoneStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursZoning > 0 ? planReview.HoursZoning : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //Backflow
            if ((planReview.BackfPool.HasValue && planReview.BackfPool.Value == true)
                || (planReview.BackfStartDate.HasValue && planReview.BackfStartDate != DateTime.MinValue && planReview.BackfEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.Backflow && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.Backflow).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.BackFlowAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.BackfEndDate.HasValue ? planReview.BackfEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.BackfPool,
                    StartDt = planReview.BackfStartDate.HasValue ? planReview.BackfStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursBackFlow > 0 ? planReview.HoursBackFlow : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //Pool
            if ((planReview.PoolPool.HasValue && planReview.PoolPool.Value == true)
                || (planReview.PoolStartDate.HasValue && planReview.PoolStartDate != DateTime.MinValue && planReview.PoolEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.EH_Pool && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.EH_Pool).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.PoolAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.PoolEndDate.HasValue ? planReview.PoolEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.PoolPool,
                    StartDt = planReview.PoolStartDate.HasValue ? planReview.PoolStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursPool > 0 ? planReview.HoursPool : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //Food
            if ((planReview.FoodPool.HasValue && planReview.FoodPool.Value == true)
                || (planReview.FoodStartDate.HasValue && planReview.FoodStartDate != DateTime.MinValue && planReview.FoodEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.EH_Food && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.EH_Food).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.FoodAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.FoodEndDate.HasValue ? planReview.FoodEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.FoodPool,
                    StartDt = planReview.FoodStartDate.HasValue ? planReview.FoodStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursFood > 0 ? planReview.HoursFood : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //Facilities
            if ((planReview.FacilPool.HasValue && planReview.FacilPool.Value == true)
                || (planReview.FacilStartDate.HasValue && planReview.FacilStartDate != DateTime.MinValue && planReview.FacilEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.EH_Facilities && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.EH_Facilities).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.FacilityAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.FacilEndDate.HasValue ? planReview.FacilEndDate : planReview.EndDate,
                    PoolRequestInd = planReview.FacilPool,
                    StartDt = planReview.FacilStartDate.HasValue ? planReview.FacilStartDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursLodge > 0 ? planReview.HoursLodge : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            //Daycare
            if ((planReview.DaycPool.HasValue && planReview.DaycPool.Value == true)
                || (planReview.DaycStartDate.HasValue && planReview.DaycStartDate != DateTime.MinValue && planReview.DaycEndDate != DateTime.MinValue)
                || (planReview.AssignedReviewers.Any(x => x.DeptNameEnumId == (int)BL.DepartmentNameEnums.EH_Day_Care && x.AttendeeId > 0)))
            {
                businessRefId = departmentModelBO.GetInstance(BL.DepartmentNameEnums.EH_Day_Care).ID;
                attendeeInfo = planReview.AssignedReviewers.FirstOrDefault(x => x.DeptNameEnumId == businessRefId);

                if (attendeeInfo == null)
                {
                    assignedReviewerId = planReview.DayCareAssignedReviewerId;
                }
                else
                {
                    assignedReviewerId = attendeeInfo.AttendeeId;
                }

                planReviewSchedules.Add(new PlanReviewScheduleDetailBE
                {
                    PlanReviewScheduleId = planReviewScheduleId,
                    BusinessRefId = businessRefId,
                    EndDt = planReview.DaycStartDate.HasValue ? planReview.DaycStartDate : planReview.EndDate,
                    PoolRequestInd = planReview.DaycPool,
                    StartDt = planReview.DaycEndDate.HasValue ? planReview.DaycEndDate : planReview.EndDate,
                    CreatedByWkrId = planReview.CreatedUser.ID.ToString(),
                    CreatedDate = planReview.CreatedDate,
                    UpdatedByWkrId = planReview.UpdatedUser.ID.ToString(),
                    AssignedPlanReviewerId = assignedReviewerId,
                    AssignedHoursNbr = planReview.HoursDayCare > 0 ? planReview.HoursDayCare : planReview.HoursDefault,
                    UserId = planReview.UpdatedUser.ID.ToString(),
                    IsActive = true,
                    ManualAssignmentInd = planReview.IsManualAssignment,
                    SameBuildContrInd = false
                });
            }

            return planReviewSchedules;
        }

        public List<AttendeeInfo> ProcessPlanReviewAttendees(PlanReview planReview,
            List<PlanReviewScheduleDetailBE> newPlanReviewSchedules,
            List<PlanReviewScheduleDetailBE> existingPlanReviewSchedules
            )
        {
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();

            foreach (PlanReviewScheduleDetailBE item in newPlanReviewSchedules)
            {
                if (item.AssignedPlanReviewerId.HasValue && item.AssignedPlanReviewerId.Value > 0)
                {
                    AttendeeInfo attendee = new AttendeeInfo()
                    {
                        AttendeeId = item.AssignedPlanReviewerId.Value,
                        BusinessRefId = item.BusinessRefId.Value,
                        DeptNameEnumId = (int)new DepartmentModelBO().GetInstance(item.BusinessRefId.Value).DepartmentEnum
                    };

                    attendees.Add(attendee);

                    if (!(item.PoolRequestInd.HasValue && item.PoolRequestInd.Value == true))
                    {
                        if (attendee.AttendeeId > 0)
                        {
                            UpsertPRAttendee(attendee, item.PlanReviewScheduleId.Value, item.UpdatedByWkrId, _ProjectScheduleBE.ProjectScheduleID.Value, planReview); //passing 0 for project schedule here creates new project schedule
                        }
                    }
                }
            }

            foreach (PlanReviewScheduleDetailBE item in existingPlanReviewSchedules)
            {
                if (_RemovedAttendees.Any(x => x.AttendeeId == item.AssignedPlanReviewerId && x.BusinessRefId == item.BusinessRefId))
                {
                    AttendeeInfo attendee = new AttendeeInfo()
                    {
                        AttendeeId = item.AssignedPlanReviewerId.Value,
                        BusinessRefId = item.BusinessRefId.Value,
                        DeptNameEnumId = (int)new DepartmentModelBO().GetInstance(item.BusinessRefId.Value).DepartmentEnum
                    };

                    RemovePRAttendee(attendee, item.PlanReviewScheduleId.Value, item.UpdatedByWkrId, planReview, _ProjectScheduleBE.ProjectScheduleID.Value);
                }
            }

            return attendees;
        }

        public bool ProcessAppointmentAttendees(PlanReview planReview)
        {
            ExpressMeetingAppointment ema = new EMAAdapter().ConvertPlanReviewToEMA(planReview);

            IEMAAdapter emaAdapter = new EMAAdapter(ema);

            List<AttendeeDetails> attendeeDetailsRemoved = GetAttendeeDetails(_RemovedAttendees);

            if (attendeeDetailsRemoved.Count > 0)
            {
                emaAdapter.SendAppointmentNotifications(attendeeDetailsRemoved, true);
            }

            List<AttendeeDetails> attendeeDetailsInserted = GetAttendeeDetails(_InsertedAttendees);

            if (attendeeDetailsInserted.Count > 0)
            {
                emaAdapter.SendAppointmentNotifications(attendeeDetailsInserted, false);
            }

            return true;
        }

        private List<AttendeeDetails> GetAttendeeDetails(List<AttendeeInfo> attendees)
        {
            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            UserIdentity identity;
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

            foreach (AttendeeInfo attendee in attendees)
            {
                identity = userIdentityModelBO.GetInstance(attendee.AttendeeId);

                attendeeDetails.Add(new AttendeeDetails
                {
                    EmailId = identity.SrcSystemValueText,
                    IsRequired = true,
                    FirstName = identity.FirstName,
                    LastName = identity.LastName,
                    UserId = identity.ID,
                    UserPrincipalName = identity.UserPrincipalName,
                    CalendarId = identity.CalendarId
                });
            }

            return attendeeDetails;
        }

        List<UserScheduleStageBE> userScheduleStageBE;
        public bool AllotUserSlots(PlanReview planReview, int planReviewProjectDetailsId, DepartmentNameEnums dept, PropertyTypeEnums property, List<UserScheduleBE> users)
        {
            UserScheduleStageBO userScheduleStageBO = new UserScheduleStageBO();
            PropertyTypeEnums projectType = (PropertyTypeEnums)(new ProjectBO().GetById(planReview.ProjectId).ProjectTypRefId.Value);
            if (userScheduleStageBE == null)//cache for all loop
                userScheduleStageBE = userScheduleStageBO.GetListByProjectID(planReview.ProjectId);
            List<UserScheduleStageBE> userScheduleStageBEList = userScheduleStageBE.Where(x => ((DepartmentNameEnums)x.BusinessRefID.Value == dept)).ToList();
            if (userScheduleStageBEList.Count > 0)
            {
                DateTime start = userScheduleStageBEList.Min(x => x.StartDate).Value;
                DateTime end = userScheduleStageBEList.Max(x => x.EndDate).Value;
                if (start.Date == GetPlandReviewStartDateTimeByDept(planReview, dept).Value
                    && end.Date == GetPlandReviewEndDateTimeByDept(planReview, dept).Value)
                { //use auto schedules values since auto exists and also user didn't change values after auto schedule button is clicked.
                    var rvr = planReview.AssignedReviewers.Where(x => ((DepartmentNameEnums)x.DeptNameEnumId) == dept).FirstOrDefault();
                    AllotUserSlotFromStage(rvr.AttendeeId, users, dept, planReview, planReviewProjectDetailsId);
                }
                else
                {// use manual date since user shifted to it is manual after auto for this dept
                    var rvr = planReview.AssignedReviewers.Where(x => ((DepartmentNameEnums)x.DeptNameEnumId) == dept).FirstOrDefault();
                    if (rvr != null)
                    {
                        DateTime PRstart = GetPlandReviewStartDateTimeByDept(planReview, dept).Value;
                        DateTime PRend = GetPlandReviewEndDateTimeByDept(planReview, dept).Value;
                        decimal hours = AddSchedulingMultiplier(property, GetPlanReviewHoursByDept(planReview, dept), PRstart, PRend);
                        AllotUserSlotManually(rvr.AttendeeId, PRstart, PRend, users, dept, hours, planReview.UpdatedUser.ID, planReviewProjectDetailsId, projectType);
                    }
                }
            }
            else
            { // use manual date since never clicked auto for this dept
                var rvr = planReview.AssignedReviewers.Where(x => ((DepartmentNameEnums)x.DeptNameEnumId) == dept).FirstOrDefault();
                if (rvr != null)
                {

                    DateTime? PRstart = GetPlandReviewStartDateTimeByDept(planReview, dept);
                    DateTime? PRend = GetPlandReviewEndDateTimeByDept(planReview, dept);
                    if (PRstart.HasValue && PRend.HasValue)
                    {
                        decimal hours = AddSchedulingMultiplier(property, GetPlanReviewHoursByDept(planReview, dept), PRstart.Value, PRend.Value);
                        AllotUserSlotManually(rvr.AttendeeId, PRstart.Value, PRend.Value, users, dept, hours, planReview.UpdatedUser.ID, planReviewProjectDetailsId, projectType);
                    }
                }
            }
            return true;
        }

        public decimal GetPlanReviewHoursByDept(PlanReview planReview, DepartmentNameEnums dept)
        {
            decimal ret = 0;
            switch (dept)
            {
                case DepartmentNameEnums.Building:
                    ret = planReview.ReReviewBuilding.HasValue ? planReview.ReReviewBuilding.Value : planReview.HoursBuilding;
                    break;
                case DepartmentNameEnums.Electrical:
                    ret = planReview.ReReviewElectric.HasValue ? planReview.ReReviewElectric.Value : planReview.HoursElectic;
                    break;
                case DepartmentNameEnums.Mechanical:
                    ret = planReview.ReReviewMech.HasValue ? planReview.ReReviewMech.Value : planReview.HoursMech;
                    break;
                case DepartmentNameEnums.Plumbing:
                    ret = planReview.ReReviewPlumb.HasValue ? planReview.ReReviewPlumb.Value : planReview.HoursPlumb;
                    break;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    ret = planReview.ReReviewZoning.HasValue ? planReview.ReReviewZoning.Value : planReview.HoursZoning;
                    break;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    ret = planReview.ReReviewFire.HasValue ? planReview.ReReviewFire.Value : planReview.HoursFire;
                    break;
                case DepartmentNameEnums.EH_Day_Care:
                    ret = planReview.ReReviewDayCare.HasValue ? planReview.ReReviewDayCare.Value : planReview.HoursDayCare;
                    break;
                case DepartmentNameEnums.EH_Food:
                    ret = planReview.ReReviewFood.HasValue ? planReview.ReReviewFood.Value : planReview.HoursFood;
                    break;
                case DepartmentNameEnums.EH_Pool:
                    ret = planReview.ReReviewPool.HasValue ? planReview.ReReviewPool.Value : planReview.HoursPool;
                    break;
                case DepartmentNameEnums.EH_Facilities:
                    ret = planReview.ReReviewLodge.HasValue ? planReview.ReReviewLodge.Value : planReview.HoursLodge;
                    break;
                case DepartmentNameEnums.Backflow:
                    ret = planReview.ReReviewBackFlow.HasValue ? planReview.ReReviewBackFlow.Value : planReview.HoursBackFlow;
                    break;
            }
            return ret;
        }

        public DateTime? GetPlandReviewStartDateTimeByDept(PlanReview planReview, DepartmentNameEnums dept)
        {
            switch (dept)
            {
                case DepartmentNameEnums.Building:
                    return planReview.BuildStartDate;
                case DepartmentNameEnums.Electrical:
                    return planReview.ElectStartDate;
                case DepartmentNameEnums.Mechanical:
                    return planReview.MechaStartDate;
                case DepartmentNameEnums.Plumbing:
                    return planReview.PlumbStartDate;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return planReview.ZoneStartDate;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return planReview.FireStartDate;
                case DepartmentNameEnums.EH_Day_Care:
                    return planReview.DaycStartDate;
                case DepartmentNameEnums.EH_Food:
                    return planReview.FoodStartDate;
                case DepartmentNameEnums.EH_Pool:
                    return planReview.PoolStartDate;
                case DepartmentNameEnums.EH_Facilities:
                    return planReview.FacilStartDate;
                case DepartmentNameEnums.Backflow:
                    return planReview.BackfStartDate;
            }
            return null;
        }

        public DateTime? GetPlandReviewEndDateTimeByDept(PlanReview planReview, DepartmentNameEnums dept)
        {
            switch (dept)
            {
                case DepartmentNameEnums.Building:
                    return planReview.BuildEndDate;
                case DepartmentNameEnums.Electrical:
                    return planReview.ElectEndDate;
                case DepartmentNameEnums.Mechanical:
                    return planReview.MechaEndDate;
                case DepartmentNameEnums.Plumbing:
                    return planReview.PlumbEndDate;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return planReview.ZoneEndDate;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return planReview.FireEndDate;
                case DepartmentNameEnums.EH_Day_Care:
                    return planReview.DaycEndDate;
                case DepartmentNameEnums.EH_Food:
                    return planReview.FoodEndDate;
                case DepartmentNameEnums.EH_Pool:
                    return planReview.PoolEndDate;
                case DepartmentNameEnums.EH_Facilities:
                    return planReview.FacilEndDate;
                case DepartmentNameEnums.Backflow:
                    return planReview.BackfEndDate;
            }
            return null;
        }

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

        public bool AllotUserSlotFromStage(int userID, List<UserScheduleBE> users
            , DepartmentNameEnums department, PlanReview planReview, int planReviewProjectDetailsId)
        {
            List<UserScheduleStageBE> curStg = userScheduleStageBE.Where(x => x.BusinessRefID.Value == (int)department && x.UserID == userID).ToList();
            foreach (var item in curStg)
            {
                users.Add(new UserScheduleBE
                {
                    ProjectScheduleID = planReviewProjectDetailsId,
                    StartDateTime = item.StartDate,
                    EndDateTime = item.EndDate,
                    BusinessRefID = new DepartmentModelBO().GetInstance(department).ID,
                    UserID = userID,
                    UserId = planReview.UpdatedUser.ID.ToString()
                });
            }
            return true;
        }

        public bool AllotUserSlotManually(int userID, DateTime start, DateTime end, List<UserScheduleBE> userSlots
        , DepartmentNameEnums department, decimal hrsTotal, int WkrId, int planReviewProjectDetailsId, PropertyTypeEnums projectType)
        {
            try
            {
                end = end.Date.AddDays(1);//add 24 so that it will calculate to end of that day.
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
                            //else if (slotStartHr.HasValue == true)
                            //{
                            //    userSlots.Add(new UserScheduleBE
                            //    {
                            //        ProjectScheduleID = planReviewProjectDetailsId,
                            //        StartDateTime = slotStartHr,
                            //        EndDateTime = currentHr.AddMinutes(15),
                            //        BusinessRefID = new DepartmentModelBO().GetInstance(department).ID,
                            //        UserID = userID,
                            //        UserId = planReview.UpdatedUser.ID.ToString()
                            //    });
                            //    slotStartHr = null;
                            //}
                            //else { } = skips the hr since it is allocated.
                            currentHr = currentHr.AddMinutes(15);
                        } while (GetTimeString(currentHr) != "1600" && leftoverMeetingSlots > 0 && usedSlots < allowedSlots);

                        //if (slotStartHr.HasValue == true) //if the loop came to 5 PM with free slot until this time then add that slot as end of day.
                        //{
                        //    userSlots.Add(new UserScheduleBE
                        //    {
                        //        ProjectScheduleID = planReviewProjectDetailsId,
                        //        StartDateTime = slotStartHr,
                        //        EndDateTime = currentHr.AddMinutes(15),
                        //        BusinessRefID = new DepartmentModelBO().GetInstance(department).ID,
                        //        UserID = userID,
                        //        UserId = planReview.UpdatedUser.ID.ToString()
                        //    });
                        //    slotStartHr = null;
                        //}
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
                    do
                    {
                        DateTime? slotStartHr = currentHr;
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
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter AllotUserSlotManually - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool UpsertPRAttendee(AttendeeInfo attendee, int prId, string WkrId, int projectScheduleId, PlanReview planReview)
        {
            bool success = false;
            try
            {
                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //get the project schedule id
                //if none are returned, this is a new apptmnt
                if (projectScheduleId == 0)
                {
                    List<ProjectScheduleBE> projectScheduleBEs = new List<ProjectScheduleBE>();
                    projectScheduleBEs = projectScheduleBO.GetByApptId(prId, planReview.ProjectScheduleRefEnum.ToString(), null);
                    if (projectScheduleBEs != null && projectScheduleBEs.Count > 0)
                    {
                        projectScheduleId = projectScheduleBEs.FirstOrDefault().ProjectScheduleID.Value;
                    }
                }

                //update attendees - send prils
                bool updatedsuccess = true;
                if (attendee != null)
                    updatedsuccess = UpdatePRAttendee(attendee, prId, WkrId, planReview, projectScheduleId);
                //these are initially set to true to capture failures
                if (/*!insertedsuccess || !removedsuccess ||*/ !updatedsuccess)
                {
                }
                else
                {

                    success = true;
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdatePRAttendeeList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool InsertPRAttendee(AttendeeInfo attendee, int prId, string WkrId, PlanReview planReview, int? projectScheduleId = null)
        {
            bool success = false;
            try
            {
                //insert into project schedule
                //get the project schedule
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(planReview.ProjectId);

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //from and to can be null
                DateTime? startdate = planReview.StartDate;
                DateTime? enddate = planReview.EndDate;

                //ScheduleTime apptdate = new ScheduleTime(startDate: pr.FromDT.Value, endDate: pr.ToDT.Value);
                List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

                //if no projectScheduleBEList passed in then it is considered as totaly new appoinment
                //and add all attendees to all dates.
                if (projectScheduleId == null || projectScheduleId == 0)
                {

                    ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE
                    {
                        AppoinmentID = prId,
                        ProjectScheduleTypeRef = planReview.ProjectScheduleRefEnum.ToString(),
                        UserId = WkrId.ToString(),
                        RecurringApptDt = startdate,
                    };
                    int projectscheduleid = projectScheduleBO.Create(projectScheduleBE);
                    //TODO: add autoschedule process based on staging table linked to _planreview.PlanReviewAutoScheduleId
                    if (_planreview.PlanReviewAutoScheduleId.HasValue && _planreview.PlanReviewAutoScheduleId > 0)
                    {
                        //do somethign here with the rows from the table
                    }
                    else
                    {

                        List<UserScheduleBE> users = new List<UserScheduleBE>();
                        AllotUserSlots(planReview, projectScheduleId.Value, (DepartmentNameEnums)attendee.BusinessRefId, project.AccelaPropertyType, users);
                        if (users.Count > 0)
                        {
                            var fltUsrs = SchedulingHelper.FlattenTimeSlots(users);
                            foreach (var item in fltUsrs)
                            {
                                //insert into user schedule
                                //TODO: connect manual schedule process to get times in date ranges
                                int userscheduleid = 0;
                                UserScheduleBE userScheduleBE = new UserScheduleBE
                                {
                                    ProjectScheduleID = projectscheduleid,
                                    StartDateTime = item.StartDateTime.Value,
                                    EndDateTime = item.EndDateTime.Value,
                                    BusinessRefID = attendee.BusinessRefId,
                                    UserID = attendee.AttendeeId,
                                    UserId = WkrId.ToString()
                                };
                                userscheduleid = userScheduleBO.Create(userScheduleBE);
                            }
                        }

                    }
                    success = true;
                }
                else
                {
                    ProjectScheduleBE projectScheduleBE = projectScheduleBO.GetById(projectScheduleId.Value);
                    //TODO: add autoschedule process based on staging table linked to _planreview.PlanReviewAutoScheduleId
                    if (planReview.PlanReviewAutoScheduleId.HasValue && planReview.PlanReviewAutoScheduleId > 0)
                    {
                        //do somethign here with the rows from the table
                    }
                    else
                    {
                        List<UserScheduleBE> users = new List<UserScheduleBE>();
                        //users.Select( x=> new {x.StartDateTime,x.EndDateTime}).OrderBy(x=> x.StartDateTime)
                        AllotUserSlots(planReview, projectScheduleId.Value, (DepartmentNameEnums)attendee.BusinessRefId, project.AccelaPropertyType, users);
                        if (users.Count > 0)
                        {
                            var fltUsrs = SchedulingHelper.FlattenTimeSlots(users);
                            foreach (var item in fltUsrs)
                            {
                                //insert into user schedule
                                int userscheduleid = 0;
                                UserScheduleBE userScheduleBE = new UserScheduleBE
                                {
                                    ProjectScheduleID = projectScheduleId.Value,
                                    StartDateTime = item.StartDateTime.Value,
                                    EndDateTime = item.EndDateTime.Value,
                                    BusinessRefID = attendee.BusinessRefId,
                                    UserID = attendee.AttendeeId,
                                    UserId = WkrId.ToString()
                                };
                                userscheduleid = userScheduleBO.Create(userScheduleBE);
                            }
                        }
                    }
                    success = true;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool InsertEMAAttendee(AttendeeInfo attendee, int prId, string WkrId, PlanReview planReview, int? projectScheduleId = null)
        {
            bool success = false;
            try
            {
                //insert into project schedule
                //get the project schedule
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(planReview.ProjectId);

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //from and to can be null
                DateTime? startdate = planReview.StartDate;
                DateTime? enddate = planReview.EndDate;

                List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

                if (projectScheduleId == null || projectScheduleId == 0)
                {
                    ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE
                    {
                        AppoinmentID = prId,
                        ProjectScheduleTypeRef = planReview.ProjectScheduleRefEnum.ToString(),
                        UserId = WkrId.ToString(),
                        RecurringApptDt = startdate,
                    };
                    int projectscheduleid = projectScheduleBO.Create(projectScheduleBE);
                    //TODO: add autoschedule process based on staging table linked to _planreview.PlanReviewAutoScheduleId
                    if (_planreview.PlanReviewAutoScheduleId.HasValue && _planreview.PlanReviewAutoScheduleId > 0)
                    {
                        //do somethign here with the rows from the table
                    }
                    else
                    {
                        int userscheduleid = 0;
                        UserScheduleBE userScheduleBE = new UserScheduleBE
                        {
                            ProjectScheduleID = projectscheduleid,
                            StartDateTime = planReview.StartDate,
                            EndDateTime = planReview.EndDate,
                            BusinessRefID = attendee.BusinessRefId,
                            UserID = attendee.AttendeeId,
                            UserId = WkrId.ToString()
                        };
                        userscheduleid = userScheduleBO.Create(userScheduleBE);
                    }
                    success = true;
                }
                else
                {
                    ProjectScheduleBE projectScheduleBE = projectScheduleBO.GetById(projectScheduleId.Value);
                    //TODO: add autoschedule process based on staging table linked to _planreview.PlanReviewAutoScheduleId
                    //if (planReview.PlanReviewAutoScheduleId.HasValue && planReview.PlanReviewAutoScheduleId > 0)
                    //{
                    //    //do somethign here with the rows from the table
                    //}
                    //else
                    //{
                    int userscheduleid = 0;
                    UserScheduleBE userScheduleBE = new UserScheduleBE
                    {
                        ProjectScheduleID = projectScheduleBE.ProjectScheduleID.Value,
                        StartDateTime = planReview.StartDate,
                        EndDateTime = planReview.EndDate,
                        BusinessRefID = attendee.BusinessRefId,
                        UserID = attendee.AttendeeId,
                        UserId = WkrId.ToString()
                    };
                    userscheduleid = userScheduleBO.Create(userScheduleBE);
                    //}
                    success = true;
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool UpdatePRAttendee(AttendeeInfo attendee, int prId, string WkrId, PlanReview planReview, int projectScheduleId)
        {
            bool success = false;
            try
            {
                //get the project schedule
                UserScheduleBO userScheduleBO = new UserScheduleBO();

                //get userschedulebe list by projectscheduleid
                List<UserScheduleBE> userschedules = new UserScheduleBO().GetListByScheduleID(projectScheduleId);

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //from and to can be null
                DateTime? startdate = planReview.StartDate;
                DateTime? enddate = planReview.EndDate;

                List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

                ProjectScheduleBE projectScheduleBE = projectScheduleBO.GetById(projectScheduleId);
                //TODO: add autoschedule process based on staging table linked to _planreview.PlanReviewAutoScheduleId
                if (planReview.PlanReviewAutoScheduleId != null && planReview.PlanReviewAutoScheduleId > 0)
                {
                    //do somethign here with the rows from the table
                }
                else
                {
                    //TODO: connect manual schedule process to get times in date ranges
                    //delete old user schedules, add new
                    foreach (UserScheduleBE schedule in userschedules.Where(x => x.BusinessRefID == attendee.BusinessRefId))
                    {
                        userScheduleBO.Delete(schedule.UserScheduleID.Value);
                    }

                    //add new

                    if (planReview.ProjectScheduleRefEnum == ProjectScheduleRefEnum.EMA)
                    {
                        InsertEMAAttendee(attendee, prId, WkrId, planReview, projectScheduleId);
                    }
                    else
                    {
                        InsertPRAttendee(attendee, prId, WkrId, planReview, projectScheduleId);
                    }

                    success = true;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return success;
        }

        public bool RemovePRAttendee(AttendeeInfo attendee, int prId, string WkrId, PlanReview planReview, int projectScheduleId)
        {
            bool success = false;
            try
            {
                //get the project schedule
                UserScheduleBO userScheduleBO = new UserScheduleBO();

                //get userschedulebe list by projectscheduleid
                List<UserScheduleBE> userschedules = new UserScheduleBO().GetListByScheduleID(projectScheduleId);

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                ProjectScheduleBE projectScheduleBE = projectScheduleBO.GetById(projectScheduleId);

                foreach (UserScheduleBE schedule in userschedules.Where(x => x.UserID == attendee.AttendeeId && x.BusinessRefID == attendee.BusinessRefId))
                {
                    userScheduleBO.Delete(schedule.UserScheduleID.Value);
                }

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter - RemovePRAttendee" + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return success;
        }

        public bool UpdateSinglePRAttendee(AttendeeInfo attendeeId, int prId, string WkrId, int projectScheduleId)
        {
            bool success = false;
            try
            {
                //get the project schedule
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                PlanReviewScheduleBE prSchedule = new PlanReviewScheduleBO().GetById(prId);
                ProjectCycle projectCycle = GetProjectCycleById(prSchedule.ProjectCycleId.Value);
                int projectId = projectCycle.ProjectId.Value;
                int projectCycleId = projectCycle.ID;
                List<PlanReviewScheduleDetail> prDetails = GetPlanReviewScheduleDetailsByPlanReviewSchedule(prSchedule.PlanReviewScheduleId.Value);
                PlanReviewScheduleDetail pr = prDetails.FirstOrDefault(x => x.AssignedPlanReviewerId == attendeeId.AttendeeId);

                //get userschedulebe list by projectscheduleid
                List<UserScheduleBE> userschedules = new UserScheduleBO().GetListByScheduleID(projectScheduleId);

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //from and to can be null

                DateTime? startdate = pr.StartDt;
                DateTime? enddate = pr.EndDt;
                attendeeId.BusinessRefId = pr.BusinessRefId.Value;

                List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

                ProjectScheduleBE projectScheduleBE = projectScheduleBO.GetById(projectScheduleId);
                //TODO: connect manual schedule process to get times in date ranges
                List<UserScheduleBE> attendeeschedules = userschedules.Where(x => x.UserID == attendeeId.AttendeeId).ToList();
                //delete old user schedules, add new
                foreach (UserScheduleBE schedule in attendeeschedules)
                {
                    userScheduleBO.Delete(schedule.UserScheduleID.Value);
                }
                //add new
                InsertSinglePRAttendee(projectId, projectCycleId, attendeeId, prId, WkrId, pr, projectScheduleId);

                success = true;

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdateSinglePRAttendee - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public bool InsertSinglePRAttendee(int projectId, int projectCycleId, AttendeeInfo attendeeId, int prId, string WkrId, PlanReviewScheduleDetail pr, int? projectScheduleId = null)
        {
            bool success = false;
            try
            {
                //insert into project schedule
                //get the project schedule
                UserScheduleBO userScheduleBO = new UserScheduleBO();

                ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                //from and to can be null
                DateTime? startdate = pr.StartDt;
                DateTime? enddate = pr.EndDt;

                //ScheduleTime apptdate = new ScheduleTime(startDate: pr.FromDT.Value, endDate: pr.ToDT.Value);
                List<AttendeeDetails> attendeesDetails = new List<AttendeeDetails>();

                ProjectScheduleBE projectScheduleBE = projectScheduleBO.GetById(projectScheduleId.Value);
                List<UserScheduleBE> users = new List<UserScheduleBE>();
                //users.Select( x=> new {x.StartDateTime,x.EndDateTime}).OrderBy(x=> x.StartDateTime)
                PropertyTypeEnums projectType = (PropertyTypeEnums)new ProjectBO().GetById(projectId).ProjectTypRefId;
                AllotUserSlotManually(attendeeId.AttendeeId, pr.StartDt.Value, pr.EndDt.Value, users, (DepartmentNameEnums)attendeeId.BusinessRefId,
                    GetPlanReviewScheduleHours(pr.PlanReviewScheduleId.Value, projectCycleId, attendeeId.BusinessRefId), pr.UpdatedUser.ID, projectScheduleId.Value, projectType);
                if (users.Count > 0)
                {
                    var fltUsrs = SchedulingHelper.FlattenTimeSlots(users);
                    foreach (var item in fltUsrs)
                    {
                        //insert into user schedule
                        int userscheduleid = 0;
                        UserScheduleBE userScheduleBE = new UserScheduleBE
                        {
                            ProjectScheduleID = projectScheduleId.Value,
                            StartDateTime = item.StartDateTime.Value,
                            EndDateTime = item.EndDateTime.Value,
                            BusinessRefID = attendeeId.BusinessRefId,
                            UserID = attendeeId.AttendeeId,
                            UserId = WkrId.ToString()
                        };
                        userscheduleid = userScheduleBO.Create(userScheduleBE);
                    }
                }
                success = true;

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter InsertSinglePRAttendee - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        #region Finalize Plan Review

        /// <summary>
        /// Updates the project, trades/agencies, saves notes, and sends plan review email
        /// </summary>
        /// <param name="item"></param>
        /// <param name="gateDt"></param>
        /// <param name="prevGateDt"></param>
        /// <returns></returns>
        public bool FinalizePlanReview(PlanReview item, DateTime? gateDt, DateTime? prevGateDt)
        {
            bool ret = false;
            try
            {
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                ProjectEstimation pe = estimationCRUDAdapter.GetProjectDetailsByProjectId(item.ProjectId);

                //get the facilitator
                int facilitatorId = pe.AssignedFacilitator.HasValue ? pe.AssignedFacilitator.Value : 0;
                Facilitator facilitator = new FacilitatorModelBO().GetInstance(facilitatorId);

                UpdateProject(item, pe, gateDt);
                UpdateDepartments(item);
                SaveNotes(item);
                SendPlanReviewEmail(item, pe, prevGateDt, facilitator);

                ret = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter FinalizePlanReview - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return ret;
        }

        public bool UpdateProject(PlanReview item, ProjectEstimation pe, DateTime? gateDt)
        {
            bool success = false;

            try
            {
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();

                pe.UpdatedUser = item.UpdatedUser;
                _projectEstimation = pe;

                //update project status if this PMA is scheduled
                //jcl 8/10/21 LES-3463 - do not change response status if this was Activate NA Review
                bool updatestatus = (item.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Scheduled
                    || item.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled)
                    && item.IsFutureCycle == false
                    && item.UpdateProjectStatus == true;

                if (updatestatus)
                {
                    //set the project status to scheduled
                    pe.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Tentatively_Scheduled);

                    if (item.ProjectScheduleRefEnum == ProjectScheduleRefEnum.FIFO || item.AllPool)
                    {
                        pe.PlansReadyOnDate = null;
                        pe.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Scheduled);
                    }

                    pe.GateDt = gateDt;
                }
                //update project facilitator
                bool updatefacilitator = pe.AssignedFacilitator.HasValue && item.AssignedFacilitator != null && (pe.AssignedFacilitator.Value != int.Parse(item.AssignedFacilitator));
                if (updatefacilitator)
                {
                    //save the facilitator
                    pe.AssignedFacilitator = int.Parse(item.AssignedFacilitator);
                }

                //if any project updates, save the project
                if (updatestatus || updatefacilitator)
                    estimationCRUDAdapter.SaveProjectEstimationDetails(pe);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdateProject - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool UpdateDepartments(PlanReview item)
        {
            bool success = false;

            try
            {
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                ExcludedPlanReviewersBO exbo = new ExcludedPlanReviewersBO();
                SchedulerAdapter schedulerAdapter = new SchedulerAdapter();

                ProjectEstimation pe = estimationCRUDAdapter.GetProjectDetailsByProjectId(item.ProjectId);
                foreach (ProjectTrade trade in pe.Trades)
                {
                    //assigned reviewer
                    AttendeeInfo scheduledReviewer = item.AssignedReviewers.Where(x => x.DeptNameEnumId == (int)trade.DepartmentInfo).FirstOrDefault();
                    if (scheduledReviewer != null)
                    {
                        UserIdentity assignedUser = userIdentityModelBO.GetInstance(scheduledReviewer.AttendeeId);
                        trade.AssignedPlanReviewer = assignedUser;
                    }
                    else
                    {
                        trade.AssignedPlanReviewer = userIdentityModelBO.GetInstance(-1);
                    }

                    // primary, secondary reviewer //
                    if (item.PrimaryReviewers != null)
                    {
                        AttendeeInfo primaryReviewer = item.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)trade.DepartmentInfo).FirstOrDefault();
                        if (primaryReviewer != null)
                        {
                            UserIdentity primaryUser = userIdentityModelBO.GetInstance(primaryReviewer.AttendeeId);
                            trade.PrimaryPlanReviewer = primaryUser;
                        }
                        else
                        {
                            trade.PrimaryPlanReviewer = userIdentityModelBO.GetInstance(-1);
                        }

                        AttendeeInfo secondaryReviewer = item.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)trade.DepartmentInfo).FirstOrDefault();
                        if (secondaryReviewer != null)
                        {
                            UserIdentity secondaryUser = userIdentityModelBO.GetInstance(secondaryReviewer.AttendeeId);
                            trade.SecondaryPlanReviewer = secondaryUser;
                        }
                        else
                        {
                            trade.SecondaryPlanReviewer = userIdentityModelBO.GetInstance(-1);
                        }
                    }

                    //jcl LES-186 - if this was previously NA, then save the hours sent and change NA
                    if (trade.EstimationNotApplicable)
                    {
                        decimal? estimationHours = trade.EstimationHours;
                        switch (trade.DepartmentInfo)
                        {
                            case DepartmentNameEnums.Building:
                                estimationHours = item.HoursBuilding;
                                break;
                            case DepartmentNameEnums.Electrical:
                                estimationHours = item.HoursElectic;
                                break;
                            case DepartmentNameEnums.Mechanical:
                                estimationHours = item.HoursMech;
                                break;
                            case DepartmentNameEnums.Plumbing:
                                estimationHours = item.HoursPlumb;
                                break;
                            default:
                                break;

                        }
                        if (estimationHours.HasValue && estimationHours > 0)
                        {
                            trade.EstimationHours = estimationHours;
                            trade.EstimationNotApplicable = false;
                        }
                    }

                    //save the trade
                    trade.UpdatedUser = item.UpdatedUser;
                    success = estimationCRUDAdapter.SaveProjectTrade(trade);

                    //excluded reviewers//
                    switch (trade.DepartmentInfo)
                    {
                        case DepartmentNameEnums.Building:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersBuild), item.UpdatedUser.ID);

                            break;
                        case DepartmentNameEnums.Electrical:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersElectric), item.UpdatedUser.ID);

                            break;
                        case DepartmentNameEnums.Mechanical:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersMech), item.UpdatedUser.ID);

                            break;
                        case DepartmentNameEnums.Plumbing:
                            exbo.SyncExcludedPlanReviewers(trade.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersPlumb), item.UpdatedUser.ID);
                            break;
                        default:
                            break;
                    }
                }
                foreach (ProjectAgency agency in pe.Agencies)
                {
                    //for fire and zoning, look for the division
                    //for everything else just get the enum
                    switch (agency.DepartmentInfo)
                    {
                        case DepartmentNameEnums.EH_Day_Care:
                        case DepartmentNameEnums.EH_Food:
                        case DepartmentNameEnums.EH_Pool:
                        case DepartmentNameEnums.EH_Facilities:
                        case DepartmentNameEnums.Backflow:
                            //assigned reviewer
                            AttendeeInfo scheduledReviewer = item.AssignedReviewers.Where(x => x.DeptNameEnumId == (int)agency.DepartmentInfo).FirstOrDefault();
                            if (scheduledReviewer != null)
                            {
                                UserIdentity assignedUser = userIdentityModelBO.GetInstance(scheduledReviewer.AttendeeId);
                                agency.AssignedPlanReviewer = assignedUser;
                            }
                            else
                            {
                                agency.AssignedPlanReviewer = userIdentityModelBO.GetInstance(-1);
                            }
                            //

                            // primary, secondary reviewer //
                            if (item.PrimaryReviewers != null)
                            {
                                AttendeeInfo primaryReviewer = item.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)agency.DepartmentInfo).FirstOrDefault();
                                if (primaryReviewer != null)
                                {
                                    UserIdentity primaryUser = userIdentityModelBO.GetInstance(primaryReviewer.AttendeeId);
                                    agency.PrimaryPlanReviewer = primaryUser;
                                }
                                else

                                {
                                    agency.PrimaryPlanReviewer = userIdentityModelBO.GetInstance(-1);
                                }

                                AttendeeInfo secondaryReviewer = item.PrimaryReviewers.Where(x => x.DeptNameEnumId == (int)agency.DepartmentInfo).FirstOrDefault();
                                if (secondaryReviewer != null)
                                {
                                    UserIdentity secondaryUser = userIdentityModelBO.GetInstance(secondaryReviewer.AttendeeId);
                                    agency.SecondaryPlanReviewer = secondaryUser;
                                }
                                else
                                {
                                    agency.SecondaryPlanReviewer = userIdentityModelBO.GetInstance(-1);
                                }
                            }

                            //jcl LES-186 - if this was previously NA, then save the hours sent and change NA
                            if (agency.EstimationNotApplicable)
                            {
                                decimal? estimationHours = agency.EstimationHours;
                                switch (agency.DepartmentInfo)
                                {
                                    case DepartmentNameEnums.EH_Day_Care:
                                        estimationHours = item.HoursDayCare;
                                        break;
                                    case DepartmentNameEnums.EH_Food:
                                        estimationHours = item.HoursFood;
                                        break;
                                    case DepartmentNameEnums.EH_Pool:
                                        estimationHours = item.HoursPool;
                                        break;
                                    case DepartmentNameEnums.EH_Facilities:
                                        estimationHours = item.HoursLodge;
                                        break;
                                    case DepartmentNameEnums.Backflow:
                                        estimationHours = item.HoursBackFlow;
                                        break;
                                    default:
                                        break;
                                }
                                if (estimationHours.HasValue && estimationHours > 0)
                                {
                                    agency.EstimationHours = estimationHours;
                                    agency.EstimationNotApplicable = false;
                                }
                            }
                            //save the agency
                            agency.UpdatedUser = item.UpdatedUser;
                            success = estimationCRUDAdapter.SaveProjectAgency(agency);

                            break;
                        case DepartmentNameEnums.Zone_Davidson:
                        case DepartmentNameEnums.Zone_Cornelius:
                        case DepartmentNameEnums.Zone_Pineville:
                        case DepartmentNameEnums.Zone_Matthews:
                        case DepartmentNameEnums.Zone_Mint_Hill:
                        case DepartmentNameEnums.Zone_Huntersville:
                        case DepartmentNameEnums.Zone_UMC:
                        case DepartmentNameEnums.Zone_Cty_Chrlt:
                        case DepartmentNameEnums.Fire_Davidson:
                        case DepartmentNameEnums.Fire_Cornelius:
                        case DepartmentNameEnums.Fire_Pineville:
                        case DepartmentNameEnums.Fire_Matthews:
                        case DepartmentNameEnums.Fire_Mint_Hill:
                        case DepartmentNameEnums.Fire_Huntersville:
                        case DepartmentNameEnums.Fire_UMC:
                        case DepartmentNameEnums.Fire_Cty_Chrlt:
                        case DepartmentNameEnums.Fire_County:
                        case DepartmentNameEnums.Zone_County:
                            //need to work out fire and zoning
                            //find out if this agency deparmtnet is in the same group
                            DepartmentDivisionEnum agencydepartmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)agency.DepartmentInfo);
                            //find the reviewer in the same division
                            AttendeeInfo attendeeInfo = item.AssignedReviewers.Where(x => new DepartmentDivisionEnum().CreateInstance(x.DeptNameEnumId) == agencydepartmentDivisionEnum).FirstOrDefault();
                            if (attendeeInfo != null)
                            {
                                UserIdentity userIdentity = userIdentityModelBO.GetInstance(attendeeInfo.AttendeeId);
                                agency.AssignedPlanReviewer = userIdentity;
                            }
                            else
                            {
                                agency.AssignedPlanReviewer = userIdentityModelBO.GetInstance(-1);
                            }

                            //primary, secondary//
                            //need to work out fire and zoning
                            //find out if this agency deparmtnet is in the same group

                            if (item.PrimaryReviewers != null)
                            {
                                DepartmentDivisionEnum primarydepartmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)agency.DepartmentInfo);
                                //find the reviewer in the same division
                                AttendeeInfo primary = item.PrimaryReviewers.Where(x => new DepartmentDivisionEnum().CreateInstance(x.DeptNameEnumId) == primarydepartmentDivisionEnum).FirstOrDefault();
                                if (primary != null)
                                {
                                    UserIdentity primaryIdentity = userIdentityModelBO.GetInstance(primary.AttendeeId);
                                    agency.PrimaryPlanReviewer = primaryIdentity;
                                }
                                else
                                {
                                    agency.PrimaryPlanReviewer = userIdentityModelBO.GetInstance(-1);
                                }
                            }

                            if (item.SecondaryReviewers != null)
                            {
                                //need to work out fire and zoning
                                //find out if this agency deparmtnet is in the same group
                                DepartmentDivisionEnum secondarydepartmentDivisionEnum = new DepartmentDivisionEnum().CreateInstance((int)agency.DepartmentInfo);
                                //find the reviewer in the same division
                                AttendeeInfo secondary = item.SecondaryReviewers.Where(x => new DepartmentDivisionEnum().CreateInstance(x.DeptNameEnumId) == secondarydepartmentDivisionEnum).FirstOrDefault();
                                if (secondary != null)
                                {
                                    UserIdentity secondaryIdentity = userIdentityModelBO.GetInstance(secondary.AttendeeId);
                                    agency.SecondaryPlanReviewer = secondaryIdentity;
                                }
                                else
                                {
                                    agency.SecondaryPlanReviewer = userIdentityModelBO.GetInstance(-1);
                                }
                            }

                            //jcl LES-186 - if this was previously NA, then save the hours sent and change NA
                            if (agency.EstimationNotApplicable)
                            {
                                decimal? estimationHours = agency.EstimationHours;
                                switch (agency.DepartmentInfo)
                                {
                                    case DepartmentNameEnums.Zone_Davidson:
                                    case DepartmentNameEnums.Zone_Cornelius:
                                    case DepartmentNameEnums.Zone_Pineville:
                                    case DepartmentNameEnums.Zone_Matthews:
                                    case DepartmentNameEnums.Zone_Mint_Hill:
                                    case DepartmentNameEnums.Zone_Huntersville:
                                    case DepartmentNameEnums.Zone_UMC:
                                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                                    case DepartmentNameEnums.Zone_County:
                                        estimationHours = item.HoursZoning;
                                        break;
                                    case DepartmentNameEnums.Fire_Davidson:
                                    case DepartmentNameEnums.Fire_Cornelius:
                                    case DepartmentNameEnums.Fire_Pineville:
                                    case DepartmentNameEnums.Fire_Matthews:
                                    case DepartmentNameEnums.Fire_Mint_Hill:
                                    case DepartmentNameEnums.Fire_Huntersville:
                                    case DepartmentNameEnums.Fire_UMC:
                                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                                    case DepartmentNameEnums.Fire_County:
                                        estimationHours = item.HoursFire;
                                        break;
                                    default:
                                        break;
                                }
                                if (estimationHours.HasValue && estimationHours > 0)
                                {
                                    agency.EstimationHours = estimationHours;
                                    agency.EstimationNotApplicable = false;
                                }
                            }
                            ////save the agency
                            agency.UpdatedUser = item.UpdatedUser;
                            success = estimationCRUDAdapter.SaveProjectAgency(agency);

                            break;
                        default:
                            break;
                    }
                    //sync all excluded
                    switch (agency.DepartmentInfo)
                    {
                        case DepartmentNameEnums.Zone_Davidson:
                        case DepartmentNameEnums.Zone_Cornelius:
                        case DepartmentNameEnums.Zone_Pineville:
                        case DepartmentNameEnums.Zone_Matthews:
                        case DepartmentNameEnums.Zone_Mint_Hill:
                        case DepartmentNameEnums.Zone_Huntersville:
                        case DepartmentNameEnums.Zone_UMC:
                        case DepartmentNameEnums.Zone_Cty_Chrlt:
                        case DepartmentNameEnums.Zone_County:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersZone), item.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.Fire_Davidson:
                        case DepartmentNameEnums.Fire_Cornelius:
                        case DepartmentNameEnums.Fire_Pineville:
                        case DepartmentNameEnums.Fire_Matthews:
                        case DepartmentNameEnums.Fire_Mint_Hill:
                        case DepartmentNameEnums.Fire_Huntersville:
                        case DepartmentNameEnums.Fire_UMC:
                        case DepartmentNameEnums.Fire_Cty_Chrlt:
                        case DepartmentNameEnums.Fire_County:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersFire), item.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Day_Care:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersDayCare), item.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Food:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersFood), item.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Pool:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersPool), item.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.EH_Facilities:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersLodge), item.UpdatedUser.ID);
                            break;
                        case DepartmentNameEnums.Backflow:
                            exbo.SyncExcludedPlanReviewers(agency.ID, schedulerAdapter.GetExcludedPlanReviewers(item.ExcludedPlanReviewersBackFlow), item.UpdatedUser.ID);
                            break;
                        default:
                            break;
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdateDepartments - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool SaveNotes(PlanReview item)
        {
            bool success = false;

            try
            {
                NotesBO notesBO = new NotesBO();
                NoteTypeModelBO noteTypeModelBO = new NoteTypeModelBO();
                NotesBE notesBe = new NotesBE();
                //default note
                notesBe = new NotesBE
                {
                    BusinessRefID = -1,
                    CreatedByWkrId = item.UpdatedUser.ID.ToString(),
                    UpdatedByWkrId = item.UpdatedUser.ID.ToString(),
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    NotesComment = item.InternalNotes,
                    NotesTypeRefId = noteTypeModelBO.GetInstance(NoteTypeEnum.InternalNotes).ID,
                    ParentNoteID = 0,
                    ProjectId = item.ProjectId,
                    UserId = item.UpdatedUser.ID.ToString()
                };
                //internal notes
                if (!string.IsNullOrWhiteSpace(item.InternalNotes))
                {
                    notesBe.NotesComment = item.InternalNotes;
                    notesBe.NotesTypeRefId = noteTypeModelBO.GetInstance(NoteTypeEnum.InternalNotes).ID;
                    notesBO.Create(notesBe);
                }
                //mandatory notes
                if (!string.IsNullOrWhiteSpace(item.MandatorySchedulingNotes))
                {
                    notesBe.NotesComment = item.MandatorySchedulingNotes;
                    notesBe.NotesTypeRefId = noteTypeModelBO.GetInstance(NoteTypeEnum.SchedulingMandatoryNotes).ID;
                    notesBO.Create(notesBe);
                }
                //standard notes
                if (!string.IsNullOrWhiteSpace(item.SchedulingStandardNotes))
                {
                    notesBe.NotesComment = item.SchedulingStandardNotes;
                    notesBe.NotesTypeRefId = noteTypeModelBO.GetInstance(NoteTypeEnum.SchedulingStandardNotes).ID;
                    notesBO.Create(notesBe);
                }
                //add scheduling notes
                if (!string.IsNullOrWhiteSpace(item.AddSchedulingNotes))
                {
                    notesBe.NotesComment = item.AddSchedulingNotes;
                    notesBe.NotesTypeRefId = noteTypeModelBO.GetInstance(NoteTypeEnum.SchedulingNotes).ID;
                    notesBO.Create(notesBe);
                }

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter SaveNotes - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool SendPlanReviewEmail(PlanReview item, ProjectEstimation pe, DateTime? prevGateDt, Facilitator facilitator)
        {
            bool success = false;

            try
            {
                //send scheduled plan review email
                //jcl 8/10/21 LES-3463 - do not send email if not requested
                if (item.SendEmail)
                {
                    //LES-1208 - if gate date is updated, and it's because the new dates are rescheduled to before the previous gate date, send a notification email
                    if (item.IsReschedule && prevGateDt.HasValue && DateTime.Now < prevGateDt.Value)
                    {
                        CreateRescheduleGateDateEmail(pe.PMEmail, item.EarliestDate.Value, item.GateDate.Value, 0,
                                                                pe.AccelaProjectRefId, pe.ProjectName, pe.DisplayOnlyInformation.ProjectAddress,
                                                            facilitator.FirstName + " " + facilitator.LastName,
                                                            facilitator.Phone, facilitator.Email);
                    }
                    else
                    {
                        if (item.ApptResponseStatusEnum != AppointmentResponseStatusEnum.Scheduled)
                        {
                            DateTime? planReviewStartDt = DateTime.Now.Date;
                            if (item.EarliestDate.HasValue && item.EarliestDate.Value != DateTime.MinValue)
                            {
                                planReviewStartDt = item.EarliestDate.Value;
                            }

                            if ((item.ProjectScheduleRefEnum == ProjectScheduleRefEnum.FIFO && item.AllHoursOneOrLess) || item.AllPool)
                            {
                                PlanReviewEmailModel model = new PlanReviewEmailModel()
                                {
                                    AccelaProjectRefId = pe.AccelaProjectRefId,
                                    FacilitatorId = facilitator.ID,
                                    ProjectAddress = pe.DisplayOnlyInformation.ProjectAddress,
                                    ProjectName = pe.ProjectName,
                                    RecIdTxt = pe.RecIdTxt
                                };

                                CreateNewCycleSchedulingNotRequiredEmail(model);
                            }
                            else
                            {
                                CreatePlanReviewScheduledEmail(pe.PMEmail, false, planReviewStartDt.Value, pe.EstimatedFee,
                                pe.AccelaProjectRefId, pe.ProjectName, pe.DisplayOnlyInformation.ProjectAddress,
                                facilitator.FirstName + " " + facilitator.LastName,
                                facilitator.Phone, facilitator.Email);
                            }
                        }
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter SendPlanReviewEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }
        #endregion

        public bool UpdateExpressDateRequest(RequestExpressDatesManagerModel model)
        {
            try
            {
                PlanReviewScheduleBE planReviewScheduleBE = new PlanReviewScheduleBE();
                PlanReviewScheduleBO planReviewScheduleBO = new PlanReviewScheduleBO();

                planReviewScheduleBE = planReviewScheduleBO.GetById(model.PlanReviewScheduleId);

                planReviewScheduleBE.Proposed1Dt = model.RequestDate1;
                planReviewScheduleBE.Proposed2Dt = model.RequestDate2;
                planReviewScheduleBE.Proposed3Dt = model.RequestDate3;
                planReviewScheduleBE.UserId = model.UserId;

                planReviewScheduleBO.Update(planReviewScheduleBE);

                var planReview = GetPlanReviewByPlanReviewScheduleId(model.PlanReviewScheduleId);

                bool prUpdateSuccess = UpdatePlanReviewStatus(planReview, AppointmentResponseStatusEnum.Cancelled, AppointmentCancellationEnum.Reject);

                //update project
                ProjectCycle projectCycle = GetProjectCycleById(planReviewScheduleBE.ProjectCycleId.Value);

                int projectId = projectCycle.ProjectId.Value;

                EstimationCRUDAdapter thisengine = new EstimationCRUDAdapter();
                ProjectEstimation pe = thisengine.GetProjectDetailsByProjectId(projectId);

                pe.UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(model.UserId));

                // insert customer audit
                ProjectAuditModelBO projectAuditModelBO = new ProjectAuditModelBO();
                projectAuditModelBO.InsertProjectAudit(projectId, AuditActionEnum.Review_Cancelled.ToStringValue(), pe.UpdatedUser.ID.ToString(), AuditActionEnum.Review_Cancelled);

                //pe.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.PROD_Not_Known);
                pe.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Not_Scheduled);
                pe.PlansReadyOnDate = planReview.ProdDate;

                bool projectUpdateSuccess = new ProjectEstimationModelBO().UpdateProjectDetails(pe);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter UpdateExpressDateRequest - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public decimal GetPlanReviewScheduleHours(int id, int projectCycleId, int businessRefId)
        {
            ProjectCycle projectCycle = GetProjectCycleById(projectCycleId);
            List<PlanReviewScheduleDetail> scheduleDetails = GetPlanReviewScheduleDetailsByPlanReviewSchedule(id);
            PlanReviewScheduleDetail scheduleDetail = scheduleDetails.FirstOrDefault(x => x.BusinessRefId == businessRefId);

            Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(projectCycle.ProjectId.Value);
            decimal defaulthours = 0;
            if (project != null)
            {
                ProjectDepartment dept = ProjectHelper.GetDepartment(project, (DepartmentNameEnums)businessRefId);

                if (dept != null && dept.EstimationHours.HasValue == true)
                {
                    defaulthours = dept.EstimationHours.Value;
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
                }
            }

            List<ProjectCycleDetail> projectCycleDetails = GetProjectCycleDetailsByProjectCycleId(projectCycleId);

            ProjectCycleDetail sbr = projectCycleDetails.FirstOrDefault(x => x.BusinessRefId == businessRefId);

            if (sbr != null) defaulthours = sbr.RereviewHoursNbr.Value;

            if (defaulthours > 0) defaulthours = AddSchedulingMultiplier(project.AccelaPropertyType, defaulthours, scheduleDetail.StartDt.HasValue ? scheduleDetail.StartDt.Value : DateTime.MinValue, scheduleDetail.EndDt.HasValue ? scheduleDetail.EndDt.Value : DateTime.MaxValue);

            return Math.Round(defaulthours, 1, MidpointRounding.AwayFromZero);
        }

        public decimal AddSchedulingMultiplier(PropertyTypeEnums propertyType, decimal defaultHrs, DateTime PRstart, DateTime PRend)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            decimal ret = defaultHrs;
            bool isPercentage = false;
            bool isHours = false;
            DateTime start = DateTime.Now, end = DateTime.Now;
            decimal factor = 0;
            List<int> propertyTypes = new List<int>();
            AdminAdapter thisengine = new AdminAdapter();
            List<CatalogItem> catlogs = thisengine.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER");
            if (catlogs.Count > 0)
            {
                var use = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.USE").FirstOrDefault();
                if (use != null)
                {
                    if (use.Value == "-1")
                        return defaultHrs; //if value is not set in Admin then return same value as default hrs.
                    if (use.Value == "Percentage")
                        isPercentage = true;
                    else if (use.Value == "Hours")
                        isHours = true;
                }
                else
                    return defaultHrs; //if value is not set in Admin then return same value as default hrs.
                start = DateTime.ParseExact(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.START_DATE").FirstOrDefault().Value, "MM/dd/yyyy", provider);
                end = DateTime.ParseExact(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.END_DATE").FirstOrDefault().Value, "MM/dd/yyyy", provider);
                factor = decimal.Parse(double.Parse(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR").FirstOrDefault().Value).ToString("0.##"));
                propertyTypes = GetselectedSMProjectTypeList(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE").FirstOrDefault().Value);
            }
            if (propertyTypes.Any(x => x == (int)propertyType) == false)
                return defaultHrs;
            if ((PRstart >= start && PRstart <= end) || (PRend >= start && PRend <= end))
            {
                if (isPercentage == true)
                {
                    ret = (ret / (decimal)100) * factor;
                }
                else if (isHours == true)
                {
                    ret = ret + factor;
                }
            }
            return ret;
        }
        private List<int> GetselectedSMProjectTypeList(string projectTypeList)
        {
            List<string> projectType = new List<string>(projectTypeList.Split(',').Select(s => s));
            return projectType.Select(x => int.Parse(x)).ToList();
        }



        public bool CreatePlanReviewScheduledEmail(string pmEmail, bool iscancellation,
                                                       DateTime planReviewStartDate, string planReviewFee,
                                                       string accelaProjectRefId, string projectName, string projectAddress,
                                                      string facilitatorName, string facilitatorPhone, string facilitatorEmail)
        {
            try
            {
                EmailAdapter emailAdapter = new EmailAdapter();
                EmailMessageBO emailMessageBO = new EmailMessageBO();

                string projectnumber = accelaProjectRefId;
                string projectname = projectName;
                string projectaddress = projectAddress;
                string planreviewfee = planReviewFee;
                string planreviewstartdate = planReviewStartDate.ToShortDateString();
                string projectcoordname = facilitatorName;
                string projectcoordphone = facilitatorPhone;
                string projectcoordemail = facilitatorEmail;

                MessageTemplateEngine mte = new MessageTemplateEngine();
                mte.ProjectNumber = accelaProjectRefId;
                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Plan_Review_AcceptReject_Email;
                mte.ProjectName = projectName;
                mte.ProjectAddress = projectAddress;
                mte.PlanReviewFee = planReviewFee;
                mte.EstimatorName = facilitatorName;
                mte.EstimatorEmail = facilitatorEmail;
                mte.EstimatorPhone = facilitatorPhone;
                mte.Project = _projectEstimation;
                mte.MeetingDate = planReviewStartDate.ToShortDateString();
                mte.PlanReviewStartDt = planReviewStartDate.ToShortDateString();

                string htmlMessageBody = mte.BuildMessage();

                string subject = "Review Tentatively Scheduled for Project #" + projectnumber + "(" + projectname + ")";

                //get mail message defaults
                MailMessage mailMessage = emailAdapter.GetMailMessage();

                if (!String.IsNullOrWhiteSpace(pmEmail) && pmEmail.Contains("@"))
                    mailMessage.To.Add(new MailAddress(pmEmail));

                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessageBody;

                if (iscancellation)
                {
                    //subject
                    mailMessage.Subject = string.Format("Plan review tentatively scheduled date cancelled for Project # {0} ({1})", projectnumber, projectname);
                    //message body
                    mailMessage.Body = emailMessageBO.CancelPlanReviewScheduledMessageBody(projectnumber, projectname, projectaddress, planreviewstartdate, projectcoordname, projectcoordphone, projectcoordemail);

                    emailAdapter.SendEmailMessage(mailMessage);
                }
                else
                {
                    emailAdapter.SendEmailMessage(mailMessage);
                    //save this notification
                    List<string> pmEmaillst = new List<string>();
                    pmEmaillst.Add(pmEmail);
                    SendProjectNotification sendProjectNotification = new SendProjectNotification
                    {
                        ProjectId = _projectEstimation.ID,
                        MailMessage = mailMessage,
                        SendDate = DateTime.Now,
                        WrkId = 1,
                        EmailNotif = BL.EmailNotifType.Plan_Review_Tentative_Scheduled,
                        EmailTxts = pmEmaillst
                    };
                    int notifId = emailAdapter.SaveNotificationEmail(sendProjectNotification);
                    if (notifId > 0)
                    {
                        //save the email list
                        sendProjectNotification.ProjectNotificationEmailId = notifId;
                        emailAdapter.SaveNotificationEmailSendList(sendProjectNotification);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter CreatePlanReviewScheduledEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return true;
        }

        public string CreatePlanReviewAcceptanceEmail(PlanReviewEmailModel model)
        {
            try
            {
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                ProjectParms projectParms = new ProjectParms
                {
                    RecIdTxt = model.RecIdTxt,
                    ProjectId = model.AccelaProjectRefId
                };
                ProjectEstimation pe = estimationCRUDAdapter.GetProjectDetailsByProjectSrcSourceTxt(model.AccelaProjectRefId);
                Facilitator facilitator = new FacilitatorModelBO().GetInstance(model.FacilitatorId);
                EmailAdapter emailAdapter = new EmailAdapter();
                EmailMessageBO emailMessageBO = new EmailMessageBO();
                string projectnumber = model.AccelaProjectRefId;
                string projectname = model.ProjectName;
                string projectaddress = model.ProjectAddress;
                string planreviewfee = pe.EstimatedFee;
                string planreviewstartdate = model.PlanReviewStartDate;
                string projectcoordname = facilitator.FirstName + " " + facilitator.LastName;
                string projectcoordphone = facilitator.Phone;
                string projectcoordemail = facilitator.Email;
                string subject = "Review Tentatively Scheduled for Project #" + projectnumber + "(" + projectname + ")";

                MessageTemplateEngine mte = new MessageTemplateEngine();
                mte.ProjectNumber = projectnumber;
                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Plan_Review_AcceptReject_Email;
                mte.ProjectName = projectname;
                mte.ProjectAddress = projectaddress;
                mte.PlanReviewFee = planreviewfee;
                mte.FacilitatorName = projectcoordname;
                mte.FacilitatorEmail = projectcoordemail;
                mte.FacilitatorPhone = projectcoordphone;
                mte.MeetingDate = planreviewstartdate;
                mte.Project = pe;
                string htmlMessageBody = mte.BuildMessage();

                return htmlMessageBody;

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter CreatePlanReviewAcceptanceEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool CreateRescheduleGateDateEmail(string pmEmail,
                                                         DateTime planReviewStartDate, DateTime GateDate, double planReviewFee,
                                                         string accelaProjectRefId, string projectName, string projectAddress,
                                                        string facilitatorName, string facilitatorPhone, string facilitatorEmail)
        {
            try
            {
                EmailAdapter emailAdapter = new EmailAdapter();
                EmailMessageBO emailMessageBO = new EmailMessageBO();

                string projectnumber = accelaProjectRefId;
                string projectname = projectName;
                string projectaddress = projectAddress;
                string planreviewstartdate = planReviewStartDate.ToShortDateString();
                string gatedate = GateDate.ToShortDateString();
                string planreviewfee = planReviewFee.ToString();
                string projectcoordname = facilitatorName;
                string projectcoordphone = facilitatorPhone;
                string projectcoordemail = facilitatorEmail;
                string subject = "Review Tentatively Rescheduled for Project #" + projectnumber + ":  " + projectname;
                string htmlMessageBody = emailMessageBO.CreateGateDateRescheduledMessageBody(projectnumber, projectname, projectaddress, planreviewstartdate, gatedate, planreviewfee, projectcoordname, projectcoordphone, projectcoordemail);

                //get mail message defaults
                MailMessage mailMessage = emailAdapter.GetMailMessage();

                //send email to customer
                if (!String.IsNullOrWhiteSpace(pmEmail) && pmEmail.Contains("@"))
                    mailMessage.To.Add(new MailAddress(pmEmail));

                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessageBody;

                emailAdapter.SendEmailMessage(mailMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter CreateRescheduleGateDateEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return true;
        }

        public bool CreateNewCycleSchedulingNotRequiredEmail(PlanReviewEmailModel model)
        {
            try
            {
                EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
                ProjectParms projectParms = new ProjectParms
                {
                    RecIdTxt = model.RecIdTxt,
                    ProjectId = model.AccelaProjectRefId
                };
                ProjectEstimation pe = estimationCRUDAdapter.GetProjectDetailsByProjectSrcSourceTxt(model.AccelaProjectRefId);
                Facilitator facilitator = new FacilitatorModelBO().GetInstance(model.FacilitatorId);
                EmailAdapter emailAdapter = new EmailAdapter();
                EmailMessageBO emailMessageBO = new EmailMessageBO();
                string projectnumber = model.AccelaProjectRefId;
                string projectname = model.ProjectName;
                string projectaddress = model.ProjectAddress;
                string planreviewfee = pe.EstimatedFee;
                string projectcoordname = facilitator.FirstName + " " + facilitator.LastName;
                string projectcoordphone = facilitator.Phone;
                string projectcoordemail = facilitator.Email;
                string subject = "Project #" + projectnumber + "(" + projectname + ") is a non-scheduled review cycle and can be submitted any time.";

                MessageTemplateEngine mte = new MessageTemplateEngine();
                mte.ProjectNumber = projectnumber;
                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.New_Cycle_Scheduling_Not_Required;
                mte.ProjectName = projectname;
                mte.ProjectAddress = projectaddress;
                mte.PlanReviewFee = planreviewfee;
                mte.FacilitatorName = projectcoordname;
                mte.FacilitatorEmail = projectcoordemail;
                mte.FacilitatorPhone = projectcoordphone;
                mte.Project = pe;
                string htmlMessageBody = mte.BuildMessage();

                MailMessage mailMessage = emailAdapter.GetMailMessage();

                //send email to facilitator
                if (!String.IsNullOrWhiteSpace(projectcoordemail) && projectcoordemail.Contains("@"))
                    mailMessage.To.Add(new MailAddress(projectcoordemail));

                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessageBody;

                emailAdapter.SendEmailMessage(mailMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PlanReviewAdapter CreateNewCycleSchedulingNotRequiredEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        #region Private Methods

        private ProjectCycleDetail GetMatchingAgencyCycleDetail(List<ProjectCycleDetail> projectCycleDetails, bool checkFire)
        {
            foreach (ProjectCycleDetail detail in projectCycleDetails)
            {
                if (checkFire)
                {
                    bool isFire = FireAndZoneAgencyHelper.IsFireAgency(detail.BusinessRefId.Value);
                    if (isFire)
                    {
                        return detail;
                    }
                }
                else
                {
                    bool isZone = FireAndZoneAgencyHelper.IsZoneAgency(detail.BusinessRefId.Value);
                    if (isZone)
                    {
                        return detail;
                    }
                }
            }
            return null;
        }

        private int CreatePooledPlanReviewScheduleDetailRecord(ProjectCycleDetail projectCycleDetail, int assignedReviewerId)
        {
            _PlanReviewScheduleDetailBE.AssignedHoursNbr = 0;
            _PlanReviewScheduleDetailBE.AssignedPlanReviewerId = assignedReviewerId;
            _PlanReviewScheduleDetailBE.BusinessRefId = projectCycleDetail.BusinessRefId;
            _PlanReviewScheduleDetailBE.CreatedByWkrId = _PlanReviewScheduleBE.CreatedByWkrId;
            _PlanReviewScheduleDetailBE.UpdatedByWkrId = _PlanReviewScheduleBE.UpdatedByWkrId;
            _PlanReviewScheduleDetailBE.StartDt = null;
            _PlanReviewScheduleDetailBE.EndDt = null;
            _PlanReviewScheduleDetailBE.IsActive = true;
            _PlanReviewScheduleDetailBE.ManualAssignmentInd = false;
            _PlanReviewScheduleDetailBE.PlanReviewScheduleId = _PlanReviewScheduleBE.PlanReviewScheduleId.Value;
            _PlanReviewScheduleDetailBE.PoolRequestInd = true;
            _PlanReviewScheduleDetailBE.SameBuildContrInd = false;
            _PlanReviewScheduleDetailBE.UserId = _PlanReviewScheduleBE.CreatedByWkrId;

            return _PlanReviewScheduleDetailBO.Create(_PlanReviewScheduleDetailBE);
        }
        #endregion
    }
}