using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AION.BL.Adapters
{

    /// <summary>
    /// This class works as an adapter between AION and Accela API interfaces. All the calls from AION to Accelal should be done in this class.
    /// </summary>
    public class EstimationAccelaAdapter : BaseManagerAdapter, IEstimationAccelaAdapter
    {
        private IAccelaEngine _acceladataconn;
        private List<SystemRole> _usersystemroles;
        private enum ListType { Reviewer, Estimator, Facilitator };
        private ListType _listtype;

        public EstimationAccelaAdapter()
        {
            _acceladataconn = new AccelaApiBO();
        }

        /// <summary>
        /// Used by Estimation Dashboard
        /// Project Statuses: Ready_For_Estimator, Estimation_In_Progress, Auto_Estimation_In_Progress, Auto_Estimation_Pending,
        /// LES-3769 - add Pending Cancellation
        /// Get project list from Accela and return as ProjectEstimation list
        /// Saves Accela project
        /// </summary>`
        /// <returns></returns>
        public List<ProjectEstimation> GetProjectEstimationList()
        {
            List<ProjectEstimation> projects = new List<ProjectEstimation>();

            List<int> statusenums = new List<int>();
            statusenums.Add((int)ProjectStatusEnum.Ready_For_Estimator);
            statusenums.Add((int)ProjectStatusEnum.Estimation_In_Progress);
            statusenums.Add((int)ProjectStatusEnum.Auto_Estimation_In_Progress);
            statusenums.Add((int)ProjectStatusEnum.Auto_Estimation_Pending);
            statusenums.Add((int)ProjectStatusEnum.Pending_Cancellation_Awaiting_Cancellation_Fees);

            return GetProjectsFromStatusIds(statusenums, true);
        }

        /// <summary>
        /// Used by Scheduling Dashboard 
        /// Project Statuses: Not_Scheduled, PROD_Not_Known,
        /// LES-3769 - add Pending Cancellation
        /// Get project list from Accela and return as ProjectEstimation list
        /// Saves Accela project
        /// </summary>
        /// <returns></returns>
        public List<ProjectEstimation> GetProjectSchedulingList()
        {
            List<int> statusenums = new List<int>();
            statusenums.Add((int)ProjectStatusEnum.Not_Scheduled);
            //Per Nathan Olson, PROD NOT KNOWN should NOT show on the Dashboard jcl 10/20/2021
            //statusenums.Add((int)ProjectStatusEnum.PROD_Not_Known);
            statusenums.Add((int)ProjectStatusEnum.Pending_Cancellation_Awaiting_Cancellation_Fees);

            return GetProjectsFromStatusIds(statusenums, false);
        }

        /// <summary>
        /// Used by Scheduling Dashboard
        /// Get all projects for facilitator meeting list
        /// Uses AION Project table project_id list
        /// </summary>
        /// <param name="projectIds"></param>
        /// <returns></returns>
        public List<ProjectEstimation> GetFMAProjectScheduleList(List<int> projectIds)
        {
            string prjids = string.Join(",", projectIds);
            ProjectBO projectBO = new ProjectBO();
            List<ProjectBE> estimationProjects = new List<ProjectBE>();
            estimationProjects = projectBO.GetListByProjectIds(prjids);

            List<ProjectEstimation> projects = new List<ProjectEstimation>();

            foreach (ProjectBE projectBe in estimationProjects)
            {
                projects.Add(ConvertProjectBEToProjectForDashboard(projectBe, false));
            }
            return projects;

        }
        private List<ProjectEstimation> GetProjectsFromStatusIds(List<int> statusEnums, bool includeTradesAgencies = false)
        {
            string prjStatusEnums = string.Join(",", statusEnums);

            ProjectBO projectBO = new ProjectBO();
            List<ProjectBE> estimationProjects = new List<ProjectBE>();
            estimationProjects = projectBO.GetListByStatusIds(prjStatusEnums);

            List<ProjectEstimation> projects = new List<ProjectEstimation>();

            foreach (ProjectBE projectBe in estimationProjects)
            {
                // only select FIFO: Small Commercial Poor Performers if 24 hours since application submittal
                if (IsOnHoldForOneDay(projectBe))
                {
                    continue;
                }
                projects.Add(ConvertProjectBEToProjectForDashboard(projectBe, includeTradesAgencies));
            }
            return projects;
        }

        public bool IsOnHoldForOneDay(ProjectBE project)
        {
            PropertyTypeEnums propertyType = project.ProjectTypRefId.HasValue ? (PropertyTypeEnums)project.ProjectTypRefId.Value : PropertyTypeEnums.NA;

            bool onHold = project.FifoInd == true
                && propertyType == PropertyTypeEnums.FIFO_Small_Commercial
                && project.TeamGradeTxt == "Poor"
                &&
                (project.TagUpdatedByTs.HasValue
                    && project.TagUpdatedByTs.Value != DateTime.MinValue
                    && project.TagUpdatedByTs.Value.AddHours(24) > DateTime.Now);

            return onHold;
        }

        /// <summary>
        /// Convert PRojectBE to ProjectEstimation for the Scheduling Dashboard 
        /// Only displayed information
        /// </summary>
        /// <param name="projectBe"></param>
        /// <returns></returns>
        private ProjectEstimation ConvertProjectBEToProjectForDashboard(ProjectBE projectBe, bool includeTradesAgencies = false)
        {
            int resultInt = 0;
            ProjectEstimation projectEstimation = new ProjectEstimation
            {
                ID = projectBe.ProjectId.Value,
                AccelaProjectRefId = projectBe.SrcSystemValTxt,
                AccelaPropertyType = projectBe.ProjectTypRefId.HasValue ? (PropertyTypeEnums)projectBe.ProjectTypRefId.Value : PropertyTypeEnums.NA,
                IsProjectRTAP = projectBe.RtapInd.Value,
                AccelaCostOfConstruction = 0,
                UpdatedDate = projectBe.UpdatedDate.Value,
                ProjectName = projectBe.ProjectNm,
                IsProjectPreliminary = projectBe.PreliminaryInd.HasValue ? projectBe.PreliminaryInd.Value : false,
                AssignedFacilitator = projectBe.AssignedFacilitatorId,
                DisplayOnlyInformation = new Meck.Shared.MeckDataMapping.AccelaProjectDisplayInfo(),
                AccelaProjectCreatedDate = projectBe.TagCreatedByTs,
                PlansReadyOnDate = projectBe.PlansReadyOnDt,
                AIONProjectStatus = new ProjectStatusModelBO().GetInstance(projectBe.ProjectStatusRefId.Value),
                IsPreliminaryMeetingRequested = projectBe.PreliminaryInd.HasValue ? projectBe.PreliminaryInd.Value : false,
                AccelaNumberofSheets = int.TryParse(projectBe.SheetsCntDesc, out resultInt) ? resultInt : 0,
                RecIdTxt = projectBe.RecIdTxt,
                TeamGradeTxt = projectBe.TeamGradeTxt
            };

            projectEstimation.DisplayOnlyInformation.ScopeOfWorkCivil = projectBe.CivilWorkScopeDesc;
            projectEstimation.DisplayOnlyInformation.ScopeOfWorkElectrical = projectBe.ElctrWorkScopeDesc;
            projectEstimation.DisplayOnlyInformation.ScopeOfWorkMechanical = projectBe.MechWorkScopeDesc;
            projectEstimation.DisplayOnlyInformation.ScopeOfWorkOverall = projectBe.OverallWorkScopeDesc;
            projectEstimation.DisplayOnlyInformation.ScopeOfWorkPlumbing = projectBe.PlumbWorkScopeDesc;
            projectEstimation.DisplayOnlyInformation.TotalFee = projectBe.TotalFeeAmt;
            projectEstimation.ProjectCostTotal = projectBe.TotalJobCostAmt;

            if (projectBe.ProjectManagerId.HasValue)
            {
                UserIdentity projectmanager = new UserIdentityModelBO().GetInstance(projectBe.ProjectManagerId.Value);
                projectEstimation.PMEmail = projectmanager.Email;
                projectEstimation.PMName = projectmanager.FirstName + " " + projectmanager.LastName;
                projectEstimation.PMFirstName = projectmanager.FirstName;
                projectEstimation.PMLastName = projectmanager.LastName;
                projectEstimation.PMPhone = projectmanager.Phone;
            }
            //jcl : we don't need trades and agencies for most dashboards, this slows the dashboard down
            if (includeTradesAgencies)
            {
                //estimation needs the department status for each department
                List<ProjectBusinessRelationshipBE> projectBRBElist = new ProjectBusinessRelationshipBO().GetListByProjectId(projectEstimation.ID);

                foreach (ProjectBusinessRelationshipBE dept in projectBRBElist)
                {
                    DepartmentTypeEnum departmentType = new DepartmentTypeEnum().CreateInstance(dept.BusinessRefId.ToString());

                    switch (departmentType)
                    {
                        case DepartmentTypeEnum.Agency:
                            projectEstimation.Agencies.Add(new ProjectAgency
                            {
                                DepartmentInfo = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), dept.BusinessRefId.ToString()),
                                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                                DepartmentStatus = dept.ProjectBusinessRelationshipStatusDesc,

                            });
                            break;
                        case DepartmentTypeEnum.Trade:
                            projectEstimation.Trades.Add(new ProjectTrade
                            {
                                DepartmentInfo = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), dept.BusinessRefId.ToString()),
                                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                                DepartmentStatus = dept.ProjectBusinessRelationshipStatusDesc,
                            });
                            break;
                        case DepartmentTypeEnum.NA:
                            break;
                        default:
                            break;
                    }
                }

            }

            projectEstimation.IsPaidStatus = projectBe.IsPaidStatus;

            return projectEstimation;
        }

        public string GetAccelaWorkflowTaskStatusFromQueueTable(string recIdTxt)
        {
            AIONEngineCrudApiBO bo = new AIONEngineCrudApiBO();
            List<AIONQueueRecordBE> queueRecords = bo.GetQueueRecordsByRecordId(recIdTxt); // returns in descending order by last processed date

            AIONQueueRecordBE lastProcessedQueueRecord = queueRecords.FirstOrDefault();
            return lastProcessedQueueRecord.WORKFLOW_TASK_STATUS;
        }

        public List<string> GetAllAgencies(ProjectParms parms)
        {
            List<string> agencies = new List<string>();
            var task = GetAllAgenciesAsync(parms);
            task.Wait();
            var result = task.Result;
            AgencyWrapperBE agencywrapper = JsonConvert.DeserializeObject<AgencyWrapperBE>(result);
            List<Meck.Shared.Accela.AgencyBE> agencyBEs = agencywrapper.AgencyList;
            //AutoMapper to Department
            //do foreach for now to get the agencies
            foreach (AgencyBE agencybe in agencyBEs)
            {
                agencies.Add(agencybe.AgencyName);
            }
            return agencies;
        }
        private async Task<string> GetAllAgenciesAsync(ProjectParms parms)
        {
            var jsonresult = await _acceladataconn.GetAllAgencyList();

            return JsonConvert.SerializeObject(jsonresult);
        }

        /// <summary>
        /// Gets Estimators from Accela
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<EstimatorUIModel> GetAllEstimators()
        {
            _listtype = ListType.Estimator;
            //this leaves out adding non-existent users
            //this only returns estimators that exist in the AION db
            List<EstimatorUIModel> estimators = new List<EstimatorUIModel>();
            var task = GetAllEstimatorsAsync();
            task.Wait();
            var result = task.Result;
            UserWrapperBE userwrapper = JsonConvert.DeserializeObject<UserWrapperBE>(result);
            List<AccelaUserBE> users = userwrapper.UserList;
            List<UserIdentity> userIdentities = users.Select(x => new UserIdentityModelBO().GetInstance(x.SrcSystemValueTxt, ExternalSystemEnum.Accela)).Where(y => y.ID != 0).ToList();

            estimators = users
                .Join(userIdentities,
                usr => usr.SrcSystemValueTxt,
                uid => uid.SrcSystemValueText,
                (usr, uid) => new EstimatorUIModel
                {
                    ID = uid.ID,
                    FirstName = uid.FirstName,
                    LastName = uid.LastName,
                    ExternalSystemID = uid.ExternalSystemID,
                    SrcSystemValueText = uid.SrcSystemValueText,
                    CreatedDate = uid.CreatedDate,
                    CreatedUser = uid.CreatedUser,
                    UpdatedDate = uid.UpdatedDate,
                    UpdatedUser = uid.UpdatedUser,
                    AssignedProjectCount = usr.AssignedProjectCount,
                    AssignedProjectsHours = usr.AssignedProjectHours,
                    DesignatedRoles = UpsertUserSystemRoles(usr.AgencyList, usr.TradesList, uid.ID)
                })
                .ToList();

            return estimators;
        }
        private async Task<string> GetAllEstimatorsAsync()
        {
            AccelaOldBO mold = new AccelaOldBO();

            var jsonresult = await mold.GetallEstimators();



            return jsonresult;
        }
        /// <summary>
        /// used in internal methods, made public for unit tests
        /// </summary>
        /// <param name="incomingagencies"></param>
        /// <param name="incomingtrades"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<SystemRole> UpsertUserSystemRoles(List<AgencyInfo> incomingagencies, List<TradeInfo> incomingtrades, int userid)
        {
            _usersystemroles = new List<SystemRole>();
            try
            {
                if (userid <= 0)
                    throw (new Exception("userid is invalid"));
                if (incomingagencies == null) incomingagencies = new List<AgencyInfo>();
                if (incomingtrades == null) incomingtrades = new List<TradeInfo>();
                UserIdentityModelBO model = new UserIdentityModelBO();
                List<SystemRole> systemroles = model.GetUserSystemRolesByUserId(userid).Where(x => x != null).ToList();
                List<SystemRole> aionroles = new List<SystemRole>();

                switch (_listtype)
                {
                    case ListType.Reviewer:
                        //agency_reviewer
                        SystemRole agencyreviewer = GetAIONSystemRole(SystemRoleExternalRef.Agency_Reviewer);
                        //plan_reviewer
                        SystemRole planreviewer = GetAIONSystemRole(SystemRoleExternalRef.Plan_Reviewer);
                        if (incomingagencies.Count() > 0)
                        {
                            aionroles.Add(agencyreviewer);
                        }
                        aionroles.Add(planreviewer);
                        break;
                    case ListType.Estimator:
                        //trade_estimator
                        SystemRole tradeestimator = GetAIONSystemRole(SystemRoleExternalRef.Trade_Estimator);
                        //agency_estimator
                        SystemRole agencyestimator = GetAIONSystemRole(SystemRoleExternalRef.Agency_Estimator);
                        if (incomingagencies.Count() > 0)
                        {
                            aionroles.Add(agencyestimator);
                        }
                        if (incomingtrades.Count() > 0)
                        {
                            aionroles.Add(tradeestimator);
                        }
                        break;
                    case ListType.Facilitator:
                        //just one role Facilitator
                        SystemRole facilitator = GetAIONSystemRole(_listtype.ToString());
                        aionroles.Add(facilitator);
                        break;
                    default:
                        break;
                }
                _usersystemroles = systemroles;
                List<SystemRole> incomingroles = GetSystemRoles(incomingagencies, incomingtrades).Where(x => x != null).ToList();

                List<SystemRole> deletedroles = new List<SystemRole>();
                List<SystemRole> insertedroles = new List<SystemRole>();
                foreach (SystemRole role in systemroles)
                {
                    //if not in incoming Accela or AION, add to delete
                    if (!incomingroles.Union(aionroles).Where(x => x.ID == role.ID).Any())
                    {
                        deletedroles.Add(role);
                        int identity = model.GetUserSystemRoleRelationship(userid, role.ID);
                        int rows = model.DeleteUserSystemRoleByIdentity(identity);
                    }
                }
                _usersystemroles = systemroles.Except(deletedroles).ToList();
                List<SystemRole> unionroles = incomingroles.Union(aionroles).Where(x => x != null).ToList();
                foreach (SystemRole role in unionroles)
                {
                    //if not in system, add to insertroles
                    if (!systemroles.Where(x => x.ID == role.ID).Any())
                    {
                        insertedroles.Add(role);
                        _usersystemroles.Add(role);
                        int rows = model.InsertUserSystemRole(userid, role.ID);
                    }
                }

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in UpsertUserSystemRoles - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (ex);
            }
            return _usersystemroles;
        }

        private List<SystemRole> GetSystemRoles(List<AgencyInfo> agencies, List<TradeInfo> trades)
        {
            List<SystemRole> roles = new List<SystemRole>();
            //roles for agencies
            if (agencies != null)
            {
                foreach (AgencyInfo agency in agencies)
                {
                    Department department = new Department();
                    SystemRole role = new SystemRole();

                    department = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, agency.AccelaDepartmentDivisionRef, agency.AccelaDepartmentRegionRef);
                    role = new SystemRoleModelBO().GetInstance((int)ExternalSystemEnum.Accela, department.DepartmentName);
                    roles.Add(role);
                }
            }
            //roles for trades
            if (trades != null)
            {
                foreach (TradeInfo trade in trades)
                {
                    Department department = new Department();
                    SystemRole role = new SystemRole();

                    department = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Trade, trade.AccelaDepartmentDivisionRef, trade.AccelaDepartmentRegionRef);
                    role = new SystemRoleModelBO().GetInstance((int)ExternalSystemEnum.Accela, department.ExternalRefInfo);
                    roles.Add(role);
                }
            }
            return roles;
        }
        public List<Facilitator> GetAllFacilitators()
        {
            _listtype = ListType.Facilitator;
            List<Facilitator> facilitators = new List<Facilitator>();
            var task = GetAllFacilitatorsAsync();
            task.Wait();
            var result = task.Result;
            UserWrapperBE userwrapper = JsonConvert.DeserializeObject<UserWrapperBE>(result);

            List<AccelaUserBE> users = userwrapper.UserList;

            if (new UserIdentityModelBO().SyncUserstoAIONDB(users, SystemRoleExternalRef.Facilitator) == false)
                return null;
            List<UserIdentity> userIdentities = users.Select(x => new UserIdentityModelBO().GetInstance(x.SrcSystemValueTxt, ExternalSystemEnum.Accela)).Where(y => y.ID != 0).ToList();
            facilitators = users
                .Join(userIdentities,
                usr => usr.SrcSystemValueTxt,
                uid => uid.SrcSystemValueText,
                (usr, uid) => new Facilitator
                {
                    ID = uid.ID,
                    FirstName = uid.FirstName,
                    LastName = uid.LastName,
                    ExternalSystemID = uid.ExternalSystemID,
                    SrcSystemValueText = uid.SrcSystemValueText,
                    CreatedDate = uid.CreatedDate,
                    CreatedUser = uid.CreatedUser,
                    UpdatedDate = uid.UpdatedDate,
                    UpdatedUser = uid.UpdatedUser,
                    AssignedProjectCount = usr.AssignedProjectCount,
                    AssignedProjectsHours = usr.AssignedProjectHours,
                    DesignatedRoles = UpsertUserSystemRoles(usr.AgencyList, usr.TradesList, uid.ID)
                })
                .ToList();
            return facilitators;

        }
        private async Task<string> GetAllFacilitatorsAsync()
        {
            // var jsonresult = await _acceladataconn.GetAllFacilitators();

            AccelaOldBO mold = new AccelaOldBO();

            var jsonresult = await mold.GetAllFacilitators();


            return jsonresult;
        }
        public List<Reviewer> GetAllReviewers(bool isExpressSched = false, bool IsSchedulable = false)
        {
            _listtype = ListType.Reviewer;
            List<Reviewer> reviewers = new List<Reviewer>();
            var task = GetAllReviewersAsync();
            task.Wait();
            var result = task.Result;
            UserWrapperBE userwrapper = JsonConvert.DeserializeObject<UserWrapperBE>(result);
            List<AccelaUserBE> users = userwrapper.UserList;
            if (new UserIdentityModelBO().SyncUserstoAIONDB(users, "Plan_Reviewer") == false)
                return null;
            List<UserIdentity> userIdentities = users.Select(x => new UserIdentityModelBO().GetInstance(x.SrcSystemValueTxt, ExternalSystemEnum.Accela)).Where(y => y.ID != 0).ToList();
            reviewers = users
               .Join(userIdentities,
               usr => usr.SrcSystemValueTxt,
               uid => uid.SrcSystemValueText,
               (usr, uid) => new Reviewer
               {
                   ID = uid.ID,
                   FirstName = uid.FirstName,
                   LastName = uid.LastName,
                   ExternalSystemID = uid.ExternalSystemID,
                   SrcSystemValueText = uid.SrcSystemValueText,
                   CreatedDate = uid.CreatedDate,
                   CreatedUser = uid.CreatedUser,
                   UpdatedDate = uid.UpdatedDate,
                   UpdatedUser = uid.UpdatedUser,
                   IsExpressSched = uid.IsExpressSched,
                   IsSchedulable = uid.IsSchedulable,
                   AssignedProjectCount = usr.AssignedProjectCount,
                   AssignedProjectsHours = usr.AssignedProjectHours,
                   DesignatedRoles = UpsertUserSystemRoles(usr.AgencyList, usr.TradesList, uid.ID)
               })
               .ToList();
            if (isExpressSched)
            {
                return reviewers.Where(x => x.IsExpressSched == true).ToList();
            }
            if (IsSchedulable)
            {
                return reviewers.Where(x => x.IsSchedulable == true).ToList();
            }
            return reviewers;
        }
        private async Task<string> GetAllReviewersAsync()
        {
            // var jsonresult = await _acceladataconn.GetAllPlanReviewers();

            AccelaOldBO mold = new AccelaOldBO();

            var jsonresult = await mold.GetAllPlanReviewers();

            return jsonresult;
        }

        public List<AIONQueueRecordBE> GetPlanReviewStatusList()
        {

            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            List<Meck.Shared.MeckDataMapping.AccelaProjectModel> projects = new List<Meck.Shared.MeckDataMapping.AccelaProjectModel>();
            //TODO:  Once Accela API is ready, point this method to the non-staging method to receive updated queue records
            var task = _mAccelaApiBO.GetPlanReviewStatusListJsonTestFile();
            task.Wait();
            var result = task.Result;


            var recordwrapper = JsonConvert.DeserializeObject<AIONQueueRecordBE>(result);
            List<AIONQueueRecordBE> records = recordwrapper.PlanReviewStatusRecords;
            return records;
        }

        private SystemRole GetAIONSystemRole(string rolename)
        {
            SystemRole role = new SystemRoleModelBO().GetInstance((int)ExternalSystemEnum.Accela, rolename);
            return role;
        }

        public string GetProjectDetailsJsonString(ProjectParms parms)
        {
            Meck.Shared.MeckDataMapping.AccelaProjectModel mAccelaProjectModel = new Meck.Shared.MeckDataMapping.AccelaProjectModel();
            try
            {

                var res = GetProjectDetails(parms);
                var result = JsonConvert.SerializeObject(res);
                return result;
            }
            catch (Exception ex)
            {
                mAccelaProjectModel = null;
                string ErrMsg = DateTime.Now + "Exception trapped while running AccelaMapping RecordId " + parms.ProjectId + "check Azure Logging " + ex.Message;

                //     BLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);

                //  throw new Exception(ErrMsg);
            }
            return "";
        }
        public Meck.Shared.MeckDataMapping.AccelaProjectModel GetProjectDetailsLoad(ProjectParms parms)
        {

            Meck.Shared.MeckDataMapping.AccelaProjectModel mAccelaProjectModel = new Meck.Shared.MeckDataMapping.AccelaProjectModel();
            try
            {

                var result = GetProjectDetails(parms);
                var resultjson = JsonConvert.SerializeObject(result);

                var AccelaRec = result;
                var mapresult = GetAccelaAIONMapByAccelaRecordType(AccelaRec.result[0].ParseRecType);
                mapresult.Wait();
                var mAccelaAONMap = mapresult.Result;
                mAccelaProjectModel = new AccelaProjectModelBO().ConvertAccelaToAionMappingAccelaProjectModel(result, mAccelaAONMap);
                
                // remove items we can't process , undefined in Mapping
            }
            catch (Exception ex)
            {
                mAccelaProjectModel = null;
                string ErrMsg = DateTime.Now + "Exception trapped while running AccelaMapping RecordId " + parms.ProjectId + "check Azure Logging " + ex.Message;

                //     BLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);

                //  throw new Exception(ErrMsg);
            }
            return mAccelaProjectModel;
        }

        /// <summary>
        /// GetAccelaAIONMap
        /// </summary>
        /// <returns>List<AccelaAionMap> </AAccelAionMap></returns>
        private async Task<List<MeckAccelaDataMap>> GetAccelaAIONMap()
        {
            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();
            var result = thisengine.SelectAccelaAionMap();

            List<MeckAccelaDataMap> mAccelaAIONMap = result;

            return mAccelaAIONMap;
        }

        /// <summary>
        /// GetAccelaAIONMap
        /// </summary>
        /// <returns>List<AccelaAionMap> </AAccelAionMap></returns>
        public async Task<List<MeckAccelaDataMap>> GetAccelaAIONMapByAccelaRecordType(string AccelaRecordType)
        {
            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();
            var result = thisengine.SelectAccelaAionMapByRecordType(AccelaRecordType);

            List<MeckAccelaDataMap> mAccelaAIONMap = result;

            // remove records we can't process
            List<MeckAccelaDataMap> mMeckAccelaDataMaps = new List<MeckAccelaDataMap>();

            mMeckAccelaDataMaps = mAccelaAIONMap.FindAll(X => X.ACCELA_OBJ_TYP_DESC.ToUpper() != "VOID").ToList();

            return mMeckAccelaDataMaps;
        }

        public AccelaRecordModel GetProjectDetails(ProjectParms parms)
        {
            _acceladataconn = new AccelaApiBO();

            var task = _acceladataconn.GetAccelaRecord(parms.RecIdTxt);
            task.Wait();
            var accelaRecordModel = task.Result;

            return accelaRecordModel;
        }

        public RecordRelatedModelBE GetRelatedRecord(string recordId, RecordRelatedModelBE.RelationshipEnum relationship)
        {
            _acceladataconn = new AccelaApiBO();

            var task = _acceladataconn.GetRelatedRecordInfo(recordId, relationship);
            task.Wait();
            var relatedRecordModel = task.Result.Result[0];

            return relatedRecordModel;
        }

        public object ParsePreLimDetails(List<object> sourceData, object targetData)
        {
            Object mTargetData = targetData;

            System.Type t = mTargetData.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int indx = 0; indx < sourceData.Count; indx++)
            {
                //PropertyInfo piFieldInstance = t.GetProperty(sourceData[indx].Key);

                //object tDetail = new object();

                //targetData = sourceData[indx].Value; // sourceData[indx].Key, out tDetail);

                //if (sourceData[indx].Value.GetType().Name.ToUpper().Contains("String"))
                //{
                //    Object mNewDetail = new Object();
                //    mNewDetail = sourceData[indx].Value;
                //    PropertyInfo piInstance = t.GetProperty(sourceData[indx].Key);

                //    if (piInstance != null)
                //    {
                //        piInstance.SetValue(mTargetData, mNewDetail);
                //    }
                //}

                //if (sourceData[indx].Value.GetType().Name.ToUpper().Contains("INTEG"))
                //{
                //    Object mNewDetail = new Object();
                //    mNewDetail = sourceData[indx].Value;
                //    PropertyInfo piInstance = t.GetProperty(sourceData[indx].Key);

                //    if (piInstance != null)
                //    {
                //        int tIntValue = Convert.ToInt32(tDetail.ToString());

                //        piInstance.SetValue(mTargetData, mNewDetail);
                //    }
                //}

                //if (sourceData[indx].Value.GetType().Name.ToUpper().Contains("BOOL"))
                //{
                //    Object mNewDetail = new Object();
                //    mNewDetail = sourceData[indx].Value;
                //    PropertyInfo piInstance = t.GetProperty(sourceData[indx].Key);

                //    if (piInstance != null)
                //    {
                //        string testValue = (string)mNewDetail;

                //        if (testValue.ToUpper() == "YES")
                //        {
                //            piInstance.SetValue(mTargetData, true);
                //        }
                //        else
                //        {
                //            piInstance.SetValue(mTargetData, false);
                //        }

                //        if (testValue.ToUpper() == "CHECKED")
                //        {
                //            piInstance.SetValue(mTargetData, true);
                //        }
                //    }
                //}
            }

            return mTargetData;

        }



    }
}