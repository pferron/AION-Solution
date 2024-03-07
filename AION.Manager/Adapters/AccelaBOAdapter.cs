using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Helpers;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace AION.Manager.Adapters
{
    public class AccelaBOAdapter : BaseManagerAdapter
    {
        // This class will call the AccelaBO objects and return the data.

        /// <summary>
        /// GetPaisdStatus for m Condition , gets th erecorc contidtions , schecksthe estatus value for the Paystatus and
        /// returns true if status Value  = ""Condition Met(Not Applied)" 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public bool GetRecordPaidStatusFromConditons(string recordId)
        {
            string ConditionPaidStatus = "Condition Met";

            bool isPaid = false;

            try
            {
                IAccelaEngine thisengine = new AccelaApiBO();
                var result = Task.Run(() => thisengine.GetRecordConditions(recordId));

                var ConditionsList = result.Result;

                if (ConditionsList.Result != null)
                {

                    foreach (var conditionObject in ConditionsList.Result)
                    {
                        if (conditionObject.Name == "Awaiting Plan Review Fees Payment")
                        {
                            if (conditionObject.Status != null)
                            {
                                if (conditionObject.Status != null)
                                {
                                    if (conditionObject.Status.Text.Contains(ConditionPaidStatus))
                                    {
                                        isPaid = true;
                                    }
                                }
                            }
                        }
                    }
                }

                return isPaid;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ScheduleFMA(int meetingId)
        {
            FacilitatorMeetingAppointment fma = new FacilitatorMeetingApptModelBO().GetInstance(meetingId);

            string processingStatus = "Scheduled";
            if (fma.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Cancelled)
            {
                processingStatus = "Cancelled";
            }

            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserIdentity user = fma.UpdatedUser;
            BusinessRefBO businessRefBO = new BusinessRefBO();
            BusinessRefBE businessRefBE = new BusinessRefBE();
            AccelaMappingHelper aPIHelper = new AccelaMappingHelper();
            AccelaApiBO accelaApiBO = new AccelaApiBO();
            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();

            //get dates
            string startDate, startTime;
            startDate = fma.FromDt.Value.Date.ToString();
            startTime = fma.FromDt.Value.TimeOfDay.ToString();

            string attendeeList = "";
            foreach (AttendeeInfo attendee in fma.Attendees)
            {
                attendeeList += attendee.FirstName + " " + attendee.LastName + ";";
            }



            int count = 0;
            string taskId = string.Empty;
            try
            {
                Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(fma.ProjectID.Value);
                int currentCycle = project.CycleNbr.Value;

                if (project != null)
                {
                    taskId = aPIHelper.AccelaPropertyTypeToMeetingID(project.AccelaPropertyType, project.IsProjectRTAP);

                    if (string.IsNullOrEmpty(taskId))
                    {
                        string errorMessage = "Error in AccelaBOAdapter ScheduleFMA - Task ID is null";

                        var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);

                        throw new Exception();
                    }

                    mAccelaCustomTableTaskUpDateModelBe.recordId = project.RecIdTxt;
                    AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
                    mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

                    mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);
                    RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

                    int id = 0;

                    List<TableFieldBE> mPRFieldBE = new List<TableFieldBE>();

                    mPRFieldBE.Add(new TableFieldBE(null, "id", id));
                    mPRFieldBE.Add(new TableFieldBE(null, "Meeting Type", fma.MeetingTypeEnum.ToStringValue()));
                    mPRFieldBE.Add(new TableFieldBE(null, "Requester", user.FirstName + " " + user.LastName));
                    mPRFieldBE.Add(new TableFieldBE(null, "Cycle #", currentCycle));
                    mPRFieldBE.Add(new TableFieldBE(null, "Meeting Date", startDate));
                    mPRFieldBE.Add(new TableFieldBE(null, "Meeting Time", startTime));
                    mPRFieldBE.Add(new TableFieldBE(null, "Attendees List", attendeeList));
                    mPRFieldBE.Add(new TableFieldBE(null, "Notes", fma.InternalNotes));
                    mPRFieldBE.Add(new TableFieldBE(null, "Status", processingStatus));

                    TableRowsBE mPRTableRowsBE =
                                new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mPRFieldBE);


                    mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);

                    mRequestCustomTablesTasksBe.recordId = project.RecIdTxt;

                    var result = accelaApiBO.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);
                    result.Wait();
                    var response = result.Result;

                    count = response.Count;
                    if (count > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AccelaBOAdapter ScheduleFMA - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        /// <summary>
        /// Send scheduling information to Accela for Preliminary Meeting 
        /// Convert PMA to Plan Review
        /// </summary>
        /// <param name="meetingId">PreliminaryMeetingAppointmentId</param>
        /// <param name="isReschedule"></param>
        /// <returns></returns>
        public bool SchedulePrelim(int meetingId)
        {
            PlanReview planReview = new PlanReview();

            PreliminaryMeetingAppointment pma = new PreliminaryMeetingApptModelBO().GetInstance(meetingId);

            List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEs =
                new ProjectBusinessRelationshipBO().GetListByProjectId(pma.ProjectID.Value);

            List<ProjectCycle> projectCycles = new PlanReviewAdapter().GetProjectCyclesByProjectId(pma.ProjectID.Value);

            planReview.ProjectCycle = projectCycles.FirstOrDefault(x => x.CurrentCycleInd == true);

            planReview.IsReschedule = pma.IsReschedule;

            //jcl pool is always false since this is a preliminary meeting
            //set the start and end date for every relevant business ref
            foreach (ProjectBusinessRelationshipBE item in projectBusinessRelationshipBEs)
            {
                if (item.IsEstimationNotApplicable == false && item.AssignedPlanReviewerId != null && item.AssignedPlanReviewerId.HasValue && item.AssignedPlanReviewerId > 0)
                {
                    DepartmentNameEnums department = (DepartmentNameEnums)item.BusinessRefId;
                    BuildPlanReviewDatesForPreliminary(department, planReview, pma.StartDate.Value, pma.EndDate.Value);
                }
            }

            return SchedulePlanReview(planReview);
        }
        private void BuildPlanReviewDatesForPreliminary(DepartmentNameEnums department, PlanReview planReview, DateTime startdt, DateTime enddt)
        {

            switch (department)
            {
                case DepartmentNameEnums.Building:
                    planReview.BuildStartDate = startdt;
                    planReview.BuildEndDate = enddt;
                    break;
                case DepartmentNameEnums.Electrical:
                    planReview.ElectStartDate = startdt;
                    planReview.ElectEndDate = enddt;
                    break;
                case DepartmentNameEnums.Mechanical:
                    planReview.MechaStartDate = startdt;
                    planReview.MechaEndDate = enddt;
                    break;
                case DepartmentNameEnums.Plumbing:
                    planReview.PlumbStartDate = startdt;
                    planReview.PlumbEndDate = enddt;
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
                    planReview.ZoneStartDate = startdt;
                    planReview.ZoneEndDate = enddt;
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
                    planReview.FireStartDate = startdt;
                    planReview.FireEndDate = enddt;
                    break;
                case DepartmentNameEnums.EH_Day_Care:
                    planReview.DaycStartDate = startdt;
                    planReview.DaycEndDate = enddt;
                    break;
                case DepartmentNameEnums.EH_Food:
                    planReview.FoodStartDate = startdt;
                    planReview.FoodEndDate = enddt;
                    break;
                case DepartmentNameEnums.EH_Pool:
                    planReview.PoolStartDate = startdt;
                    planReview.PoolEndDate = enddt;
                    break;
                case DepartmentNameEnums.EH_Facilities:
                    planReview.FacilStartDate = startdt;
                    planReview.FacilEndDate = enddt;
                    break;
                case DepartmentNameEnums.Backflow:
                    planReview.BackfStartDate = startdt;
                    planReview.BackfEndDate = enddt;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Send scheduling info to Accela for PlanReview
        /// Paramater PlanReview requires ProjectCycle
        /// </summary>
        /// <param name="planReview">PlanReview</param>
        /// <returns></returns>
        public bool SchedulePlanReview(PlanReview pr)
        {
            //generate task objects for plan review scheduling
            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserBE userBE = new UserBE();
            BusinessRefBO businessRefBO = new BusinessRefBO();
            BusinessRefBE businessRefBE = new BusinessRefBE();
            AccelaMappingHelper aPIHelper = new AccelaMappingHelper();
            AccelaApiBO accelaApiBO = new AccelaApiBO();
            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();
            int currentCycle = pr.ProjectCycle.CycleNbr.Value;
            string startDate, gateDate, dueDate;

            gateDate = pr.ProjectCycle.GateDt?.ToString();
            startDate = string.Empty;
            dueDate = string.Empty;

            string processingStatus = pr.IsReschedule ? "Change by AION" : "Added by AION";

            int count = 0;
            string taskId = string.Empty;
            int id = 0;

            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

            try
            {
                Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(pr.ProjectCycle.ProjectId.Value);

                if (project != null)
                {
                    bool IsPreliminary = project.IsProjectPreliminary == true ? project.IsProjectPreliminary : false;
                    //taskId = aPIHelper.AccelaPropertyTypeToTaskId(project.AccelaPropertyType, project.IsProjectRTAP);
                    taskId = aPIHelper.AccelaPropertyTypeToTaskId(project.AccelaPropertyType, project.IsProjectRTAP, IsPreliminary);


                    if (gateDate != null)
                    {
                        var recordValues = GenerateGateAndFacilitatorRecords(currentCycle, project, taskId, gateDate, processingStatus);

                        mRequestCustomTablesTasksBe = recordValues.Item1;
                        id = recordValues.Item2;
                    }
                    else
                    {
                        var facilitatorRecord = GenerateFacilitatorRecord(currentCycle, project, taskId, gateDate, processingStatus);

                        mRequestCustomTablesTasksBe = facilitatorRecord.Item1;
                        id = facilitatorRecord.Item2;

                    }

                    mAccelaCustomTableTaskUpDateModelBe.recordId = project.RecIdTxt;
                    AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
                    mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

                    mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

                    if (project.AssignedFacilitator != null)
                    {
                        userBE = new UserBO().GetById(project.AssignedFacilitator.Value);
                    }

                    //TODO - consider ScheduleBusinessRelationship table for subsequent cycle hours
                    List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEs = new ProjectBusinessRelationshipBO().GetListByProjectId(project.ID);

                    if (pr.ProjectCycle.CycleNbr == 1)
                    {
                        foreach (ProjectBusinessRelationshipBE projectBusinessRelationshipBE in projectBusinessRelationshipBEs)
                        {
                            if (projectBusinessRelationshipBE.AssignedPlanReviewerId != null && projectBusinessRelationshipBE.AssignedPlanReviewerId != -1)
                            {
                                userBE = new UserBO().GetById(projectBusinessRelationshipBE.AssignedPlanReviewerId.Value);  //what values is this for each task type?
                            }
                            else continue;

                            businessRefBE = new BusinessRefBO().GetById(projectBusinessRelationshipBE.BusinessRefId.Value);

                            var tableValues = TableValueTasksMapAionToAccela(pr, project, businessRefBE.BusinessName);

                            TableRowsBE mPRTableRowsBE = GeneratePlanReviewRecord(
                            id++,
                            tableValues.Item4,
                            tableValues.Item1,
                            userBE,
                            tableValues.Item3,
                            currentCycle,
                            tableValues.Item2,
                            projectBusinessRelationshipBE.EstimationHoursNbr.GetValueOrDefault(0m),
                            processingStatus,
                            taskId);

                            mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);
                        }
                    }
                    else
                    {
                        // new way
                        PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
                        List<PlanReviewSchedule> planReviewSchedules = planReviewAdapter.GetPlanReviewSchedulesByProjectCycle(pr.ProjectCycle.ID);
                        PlanReviewSchedule planReviewSchedule = planReviewSchedules.FirstOrDefault(x => x.IsRescheduleInd == false);
                        List<PlanReviewScheduleDetail> planReviewScheduleDetails = planReviewAdapter.GetPlanReviewScheduleDetailsByPlanReviewSchedule(planReviewSchedule.ID);

                        //get collection of bus ref ids for fire and zoning
                        List<int> zoningBusinessRefIds = new List<int>();

                        Helper helper = new Helper();
                        DepartmentModelBO departmentModelBO = new DepartmentModelBO();

                        List<DepartmentNameEnums> fireDepartments = helper.FireDepartmentNames;
                        List<int> fireBusinessRefIds = new List<int>();

                        foreach (DepartmentNameEnums fireDepartment in fireDepartments)
                        {
                            fireBusinessRefIds.Add(departmentModelBO.GetInstance(fireDepartment).ID);
                        }

                        List<DepartmentNameEnums> zoneDepartments = helper.ZoneDepartmentNames;
                        List<int> zoneBusinessRefIds = new List<int>();

                        foreach (DepartmentNameEnums zoneDepartment in zoneDepartments)
                        {
                            zoneBusinessRefIds.Add(departmentModelBO.GetInstance(zoneDepartment).ID);
                        }

                        foreach (PlanReviewScheduleDetail prsd in planReviewScheduleDetails)
                        {
                            ProjectBusinessRelationshipBE pbr = new ProjectBusinessRelationshipBE();

                            if (fireBusinessRefIds.Contains(prsd.BusinessRefId.Value))
                            {
                                pbr = projectBusinessRelationshipBEs.Where(x => fireBusinessRefIds.Contains(prsd.BusinessRefId.Value)).FirstOrDefault();
                            }
                            else if (zoneBusinessRefIds.Contains(prsd.BusinessRefId.Value))
                            {
                                pbr = projectBusinessRelationshipBEs.Where(x => zoneBusinessRefIds.Contains(prsd.BusinessRefId.Value)).FirstOrDefault();
                            }
                            else
                            {
                                pbr = projectBusinessRelationshipBEs.Where(x => x.BusinessRefId == (int)prsd.BusinessRefId).FirstOrDefault();
                            }

                            userBE = new UserBO().GetById(pbr.AssignedPlanReviewerId.Value);

                            businessRefBE = new BusinessRefBO().GetById(prsd.BusinessRefId.Value);

                            var tableValues = TableValueTasksMapAionToAccela(pr, project, businessRefBE.BusinessName);

                            TableRowsBE mPRTableRowsBE = GeneratePlanReviewRecord(
                            id++,
                            tableValues.Item4,
                            tableValues.Item1,
                            userBE,
                            tableValues.Item3,
                            currentCycle,
                            tableValues.Item2,
                            prsd.AssignedHoursNbr.GetValueOrDefault(0m),
                            processingStatus,
                            taskId);

                            mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);
                        }
                    }

                    mRequestCustomTablesTasksBe.recordId = project.RecIdTxt;

                    var result = accelaApiBO.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);
                    result.Wait();
                    var response = result.Result;

                    count = response.Count;
                    if (count > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AccelaBOAdapter SchedulePlanReview - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public bool ChangeAssignedFacilitator(ProjectEstimation project)
        {
            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserBE userBE = new UserBE();
            BusinessRefBO businessRefBO = new BusinessRefBO();
            BusinessRefBE businessRefBE = new BusinessRefBE();
            AccelaMappingHelper aPIHelper = new AccelaMappingHelper();
            AccelaApiBO accelaApiBO = new AccelaApiBO();
            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();
            int currentCycle = project.CycleNbr.Value;
            string gateDate = project.GateDt?.ToString();

            string processingStatus = "Change by AION";

            int count = 0;
            string taskId = string.Empty;
            int id = 0;

            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

            try
            {
                if (project != null)
                {
                    bool IsPreliminary = project.IsProjectPreliminary == true ? project.IsProjectPreliminary : false;
                    taskId = aPIHelper.AccelaPropertyTypeToTaskId(project.AccelaPropertyType, project.IsProjectRTAP, IsPreliminary);
                    var recordValues = GenerateFacilitatorRecord(currentCycle, project, taskId, gateDate, processingStatus);

                    mRequestCustomTablesTasksBe = recordValues.Item1;
                    id = recordValues.Item2;

                    mAccelaCustomTableTaskUpDateModelBe.recordId = project.RecIdTxt;
                    AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
                    mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

                    mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

                    mRequestCustomTablesTasksBe.recordId = project.RecIdTxt;

                    var result = accelaApiBO.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);
                    result.Wait();
                    var response = result.Result;

                    count = response.Count;
                    if (count > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AccelaBOAdapter ChangeAssignedFacilitator - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return true;
        }

        public Tuple<RequestCustomTablesTasksBE, int> GenerateFacilitatorRecord(int cycle, Project project, string taskId, string gateDate, string processingStatus)
        {
            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserBE userBE = new UserBE();
            AccelaMappingHelper aPIHelper = new AccelaMappingHelper();
            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();

            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();
            int id = 0;

            try
            {
                if (project != null)
                {
                    if (string.IsNullOrEmpty(taskId))
                    {
                        string errorMessage = "Error in AccelaBOAdapter GenerateFacilitatorRecord - Task ID is null";

                        var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);

                        throw new Exception();
                    }

                    mAccelaCustomTableTaskUpDateModelBe.recordId = project.RecIdTxt;
                    AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
                    mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

                    mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

                    if (project.AssignedFacilitator != null)
                    {
                        userBE = new UserBO().GetById(project.AssignedFacilitator.Value);
                    }

                    if (userBE != null)
                    {
                        if (userBE.UserName != null)
                        {
                            TableRowsBE mFCTableRowsBE = GenerateFacilitatorRecord(id, userBE, cycle, processingStatus, taskId);
                            mRequestCustomTablesTasksBe.array.Add(mFCTableRowsBE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AccelaBOAdapter GenerateGateAndFacilitatorRecords - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            var tableValues = Tuple.Create(mRequestCustomTablesTasksBe, id);

            return tableValues;
        }
        public Tuple<RequestCustomTablesTasksBE, int> GenerateGateAndFacilitatorRecords(int cycle, Project project, string taskId, string gateDate, string processingStatus)
        {
            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserBE userBE = new UserBE();
            AccelaMappingHelper aPIHelper = new AccelaMappingHelper();
            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();

            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();
            int id = 0;

            try
            {
                if (project != null)
                {
                    if (string.IsNullOrEmpty(taskId))
                    {
                        string errorMessage = "Error in AccelaBOAdapter ScheduleReview - Task ID is null";

                        var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                            string.Empty, string.Empty, string.Empty);

                        throw new Exception();
                    }

                    mAccelaCustomTableTaskUpDateModelBe.recordId = project.RecIdTxt;
                    AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
                    mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

                    mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

                    if (project.AssignedFacilitator != null)
                    {
                        userBE = new UserBO().GetById(project.AssignedFacilitator.Value);
                    }

                    if (userBE != null)
                    {
                        if (userBE.UserName != null)
                        {
                            TableRowsBE mGateTableRowsBE = GenerateGateRecord(id, cycle, processingStatus, gateDate, taskId);
                            mRequestCustomTablesTasksBe.array.Add(mGateTableRowsBE);

                            id++;

                            TableRowsBE mFCTableRowsBE = GenerateFacilitatorRecord(id, userBE, cycle, processingStatus, taskId);
                            mRequestCustomTablesTasksBe.array.Add(mFCTableRowsBE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AccelaBOAdapter GenerateGateAndFacilitatorRecords - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            var tableValues = Tuple.Create(mRequestCustomTablesTasksBe, id);

            return tableValues;
        }

        public Tuple<string, string, string, string> TableValueTasksMapAionToAccela(PlanReview pr, Project project, string BusinessName)
        {

            string pool = "No";
            string taskName = string.Empty;
            string startDate = string.Empty;
            string dueDate = string.Empty;

            switch (BusinessName)
            {
                case "NA":
                case "Building":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial Building";
                    }

                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Residential Building";
                    }
                    pool = pr.BuildPool.HasValue ? (pr.BuildPool.Value ? "Yes" : "No") : "No";
                    if (pr.BuildStartDate.HasValue) startDate = pr.BuildStartDate.Value.ToString();
                    if (pr.BuildEndDate.HasValue) dueDate = pr.BuildEndDate.Value.ToString();
                    break;
                case "Electrical":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial Electrical";
                    }
                    pool = pr.ElectPool.HasValue ? (pr.ElectPool.Value ? "Yes" : "No") : "No";
                    if (pr.ElectStartDate.HasValue) startDate = pr.ElectStartDate.Value.ToString();
                    if (pr.ElectEndDate.HasValue) dueDate = pr.ElectEndDate.Value.ToString();
                    break;
                case "Mechanical":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial Mechanical";
                    }
                    pool = pr.MechaPool.HasValue ? (pr.MechaPool.Value ? "Yes" : "No") : "No";
                    if (pr.MechaStartDate.HasValue) startDate = pr.MechaStartDate.Value.ToString();
                    if (pr.MechaEndDate.HasValue) dueDate = pr.MechaEndDate.Value.ToString();
                    break;
                case "Plumbing":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial Plumbing";
                    }
                    if (pr.PlumbStartDate.HasValue) startDate = pr.PlumbStartDate.Value.ToString();
                    if (pr.PlumbEndDate.HasValue) dueDate = pr.PlumbEndDate.Value.ToString();
                    break;
                case "Zone_Davidson":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_County":
                case "Zone_Cornelius":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Pineville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Matthews":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Huntersville":
                    taskName = "Huntersville Zoning";
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Mint_Hill":
                    taskName = "Mint Hill Zoning";
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone Unincorporated Mecklenburg County":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_City Of Charlotte":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial City Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential Charlotte Zoning";
                    }
                    pool = pr.ZonePool.HasValue ? (pr.ZonePool.Value ? "Yes" : "No") : "No";
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Fire_County":
                case "Fire_Davidson":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Cornelius":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Pineville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Matthews":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Huntersville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire Unincorporated Mecklenburg County":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_City Of Charlotte":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.Express
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial City Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                            || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                            || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                            || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential City Fire";
                    }

                    pool = pr.FirePool.HasValue ? (pr.FirePool.Value ? "Yes" : "No") : "No";
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Environmental Health: Day Care":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    { taskName = "Commercial EHS Day Care"; }
                    pool = pr.DaycPool.HasValue ? (pr.DaycPool.Value ? "Yes" : "No") : "No";
                    if (pr.DaycStartDate.HasValue) startDate = pr.DaycStartDate.Value.ToString();
                    if (pr.DaycEndDate.HasValue) dueDate = pr.DaycEndDate.Value.ToString();
                    break;
                case "Environmental Health: Food Service":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Food Service"; }

                    pool = pr.FoodPool.HasValue ? (pr.FoodPool.Value ? "Yes" : "No") : "No";
                    if (pr.FoodStartDate.HasValue) startDate = pr.FoodStartDate.Value.ToString();
                    if (pr.FoodEndDate.HasValue) dueDate = pr.FoodEndDate.Value.ToString();
                    break;
                case "Environmental Health: Public Pool":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                          || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                          || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                          || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                          || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    { taskName = "Commercial EHS Public Pool"; }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "EHS Residential Pools";
                    }
                    pool = pr.PoolPool.HasValue ? (pr.PoolPool.Value ? "Yes" : "No") : "No";
                    if (pr.PoolStartDate.HasValue) startDate = pr.PoolStartDate.Value.ToString();
                    if (pr.PoolEndDate.HasValue) dueDate = pr.PoolEndDate.Value.ToString();
                    break;
                case "Environmental Health: Facilities/Lodging":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                    || project.AccelaPropertyType == PropertyTypeEnums.Express)
                    { taskName = "Commercial EHS Facility Lodging"; }

                    pool = pr.FacilPool.HasValue ? (pr.FacilPool.Value ? "Yes" : "No") : "No";
                    if (pr.FacilStartDate.HasValue) startDate = pr.FacilStartDate.Value.ToString();
                    if (pr.FacilEndDate.HasValue) dueDate = pr.FacilEndDate.Value.ToString();
                    break;
                case "Charlotte Water Backflow":
                    taskName = "CLTWTR Backflow Prevention";
                    pool = pr.BackfPool.HasValue ? (pr.BackfPool.Value ? "Yes" : "No") : "No";
                    if (pr.BackfStartDate.HasValue) startDate = pr.BackfStartDate.Value.ToString();
                    if (pr.BackfEndDate.HasValue) dueDate = pr.BackfEndDate.Value.ToString();
                    break;
                default:
                    pool = "No";
                    taskName = string.Empty;
                    startDate = string.Empty;
                    dueDate = string.Empty;
                    break;
            }

            if (project.IsFifo)
            {
                dueDate = string.Empty;
            }

            var tableValues = Tuple.Create(pool, startDate, dueDate, taskName);
            return tableValues;
        }

        public Tuple<string, string, string, string> TableValueTasksMapAionToAccela(FIFOSchedule fs, Project project, string BusinessName)
        {
            string pool = "No";
            string taskName = string.Empty;
            string startDate = string.Empty;
            string dueDate = string.Empty;

            switch (BusinessName)
            {
                case "NA":
                case "Building":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Building";
                    }

                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential Building";
                    }

                    if (fs.BuildStartDate.HasValue) startDate = fs.BuildStartDate.Value.ToString();
                    if (fs.BuildEndDate.HasValue) dueDate = fs.BuildEndDate.Value.ToString();
                    break;
                case "Electrical":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Electrical";
                    }

                    if (fs.ElectStartDate.HasValue) startDate = fs.ElectStartDate.Value.ToString();
                    if (fs.ElectEndDate.HasValue) dueDate = fs.ElectEndDate.Value.ToString();
                    break;
                case "Mechanical":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Mechanical";
                    }

                    if (fs.MechaStartDate.HasValue) startDate = fs.MechaStartDate.Value.ToString();
                    if (fs.MechaEndDate.HasValue) dueDate = fs.MechaEndDate.Value.ToString();
                    break;
                case "Plumbing":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Plumbing";
                    }
                    if (fs.PlumbStartDate.HasValue) startDate = fs.PlumbStartDate.Value.ToString();
                    if (fs.PlumbEndDate.HasValue) dueDate = fs.PlumbEndDate.Value.ToString();
                    break;
                case "Zone_Davidson":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_County":
                case "Zone_Cornelius":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;

                case "Zone_Pineville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Matthews":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Huntersville":
                    taskName = "Huntersville Zoning";
                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;

                case "Zone Unincorporated Mecklenburg County":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_City Of Charlotte":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial City Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential Charlotte Zoning";
                    }

                    if (fs.ZoneStartDate.HasValue) startDate = fs.ZoneStartDate.Value.ToString();
                    if (fs.ZoneEndDate.HasValue) dueDate = fs.ZoneEndDate.Value.ToString();
                    break;
                case "Fire_Davidson":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;
                case "Fire_County":
                case "Fire_Cornelius":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;
                case "Fire_Pineville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;
                case "Fire_Matthews":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;
                case "Fire_Huntersville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;

                case "Fire Unincorporated Mecklenburg County":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;
                case "Fire_City Of Charlotte":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial City Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential City Fire";
                    }

                    if (fs.FireStartDate.HasValue) startDate = fs.FireStartDate.Value.ToString();
                    if (fs.FireEndDate.HasValue) dueDate = fs.FireEndDate.Value.ToString();
                    break;
                case "Environmental Health: Day Care":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Day Care"; }
                    if (fs.DaycStartDate.HasValue) startDate = fs.DaycStartDate.Value.ToString();
                    if (fs.DaycEndDate.HasValue) dueDate = fs.DaycEndDate.Value.ToString();
                    break;
                case "Environmental Health: Food Service":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Facility Food Service"; }

                    if (fs.FoodStartDate.HasValue) startDate = fs.FoodStartDate.Value.ToString();
                    if (fs.FoodEndDate.HasValue) dueDate = fs.FoodEndDate.Value.ToString();
                    break;
                case "Environmental Health: Public Pool":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                          || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                          || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                          || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Pool Review"; }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "EHS Residential Pools";
                    }

                    if (fs.PoolStartDate.HasValue) startDate = fs.PoolStartDate.Value.ToString();
                    if (fs.PoolEndDate.HasValue) dueDate = fs.PoolEndDate.Value.ToString();
                    break;
                case "Environmental Health: Facilities/Lodging":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Facilities Lodging Review"; }

                    if (fs.FacilStartDate.HasValue) startDate = fs.FacilStartDate.Value.ToString();
                    if (fs.FacilEndDate.HasValue) dueDate = fs.FacilEndDate.Value.ToString();
                    break;
                case "Charlotte Water Backflow":
                    taskName = "CLTWTR Backflow Prevention";

                    if (fs.BackfStartDate.HasValue) startDate = fs.BackfStartDate.Value.ToString();
                    if (fs.BackfEndDate.HasValue) dueDate = fs.BackfEndDate.Value.ToString();
                    break;
                default:
                    pool = "No";
                    taskName = string.Empty;
                    startDate = string.Empty;
                    dueDate = string.Empty;
                    break;
            }

            var tableValues = Tuple.Create(pool, startDate, dueDate, taskName);
            return tableValues;
        }

        private TableRowsBE GenerateGateRecord(int id, int cycle, string processingStatus, string gateDate, string taskId)
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var todayFormat = today.ToString("ddd MMM dd HH:mm:ss EST yyyy");
            //var assigndHoursDec = assignedHours.ToString("0.0");
            List<TableFieldBE> mGateFieldBE = new List<TableFieldBE>();

            mGateFieldBE.Add(new TableFieldBE(null, "id", id));
            mGateFieldBE.Add(new TableFieldBE(null, "Task Type", "Control"));
            mGateFieldBE.Add(new TableFieldBE(null, "Task Name", "Gate"));
            mGateFieldBE.Add(new TableFieldBE(null, "Pool Review", "No"));
            mGateFieldBE.Add(new TableFieldBE(null, "Assignee", ""));
            mGateFieldBE.Add(new TableFieldBE(null, "Due Date", gateDate));
            mGateFieldBE.Add(new TableFieldBE(null, "Cycle #", cycle));
            mGateFieldBE.Add(new TableFieldBE(null, "StartDate", string.Empty));
            mGateFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", "0"));
            mGateFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", todayFormat));
            mGateFieldBE.Add(new TableFieldBE(null, "Processing Status", processingStatus));
            mGateFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mGateTableRowsBE =
                   new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mGateFieldBE);

            return mGateTableRowsBE;
        }

        private TableRowsBE GenerateFacilitatorRecord(int id, UserBE userBE, int cycle, string processingStatus, string taskId)
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var todayFormat = today.ToString("ddd MMM dd HH:mm:ss EST yyyy");
            var UserAD = userBE.ADName;
            List<TableFieldBE> mFCFieldBE = new List<TableFieldBE>();

            mFCFieldBE.Add(new TableFieldBE(null, "id", id++));
            mFCFieldBE.Add(new TableFieldBE(null, "Task Type", "Control"));
            mFCFieldBE.Add(new TableFieldBE(null, "Task Name", "Facilitator Coordination"));
            mFCFieldBE.Add(new TableFieldBE(null, "Pool Review", "No"));
            mFCFieldBE.Add(new TableFieldBE(null, "Assignee", UserAD));
            //mFCFieldBE.Add(new TableFieldBE(null, "Assignee", userBE.FirstNm + " " + userBE.LastNm));
            mFCFieldBE.Add(new TableFieldBE(null, "Due Date", string.Empty));
            mFCFieldBE.Add(new TableFieldBE(null, "Cycle #", cycle));
            mFCFieldBE.Add(new TableFieldBE(null, "StartDate", string.Empty));
            mFCFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", "0"));
            mFCFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", todayFormat));
            mFCFieldBE.Add(new TableFieldBE(null, "Processing Status", processingStatus));
            mFCFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mFCTableRowsBE =
                   new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mFCFieldBE);

            return mFCTableRowsBE;
        }

        private TableRowsBE GeneratePlanReviewRecord(int id, string taskName, string poolReview, UserBE userBE, string dueDate, int cycle, string startDate, decimal assignedHours, string processingStatus, string taskId)
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var todayFormat = today.ToString("ddd MMM dd HH:mm:ss EST yyyy");
            var UserAD = userBE.ADName;
            var assigndHoursDec = assignedHours.ToString("0.0");

            List<TableFieldBE> mPRFieldBE = new List<TableFieldBE>();

            mPRFieldBE.Add(new TableFieldBE(null, "id", id++));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Type", "Plan Review"));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Name", taskName));
            mPRFieldBE.Add(new TableFieldBE(null, "Pool Review", poolReview));
            mPRFieldBE.Add(new TableFieldBE(null, "Assignee", UserAD));
            mPRFieldBE.Add(new TableFieldBE(null, "Due Date", dueDate));
            mPRFieldBE.Add(new TableFieldBE(null, "Cycle #", cycle));
            mPRFieldBE.Add(new TableFieldBE(null, "StartDate", startDate));
            mPRFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", assigndHoursDec));
            mPRFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", todayFormat));
            mPRFieldBE.Add(new TableFieldBE(null, "Processing Status", processingStatus));
            mPRFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mPRTableRowsBE =
                       new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mPRFieldBE);

            return mPRTableRowsBE;
        }
    }
}



