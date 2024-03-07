using AION.BL.Adapters;
using AION.BL.Common;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.BL.BusinessObjects
{
    public abstract class ProjectModelBaseBO : ModelBaseModelBO
    {

        private enum ActionType { Insert, Update, Delete, GetById, GetDataSet, GetList };
        private string _errorMsg;
        private Project _project;
        private ProjectEstimation _projectEstimation;
        private ProjectBE _projectbe;
        private ProjectBO _projectbo;
        private AccelaProjectModel _accelaprojectmodel;
        private ProjectBusinessRelationshipBO _projectBRBO;
        private ProjectBusinessRelationshipBE _projectBRBE;

        private ProjectDepartment _projectdepartment;
        private List<ProjectBusinessRelationshipBE> _projectBRBElist;
        private int _projectid;
        private int _rowcount;
        private ProjectAuditModelBO _projectAuditModelBO;
        private List<ExcludedPlanReviewersBE> _excludedPlanReviewers;

        protected Project InjectBaseObjects(Project retobj, AccelaProjectModel accelaProjectInfo)
        {
            _project = retobj;
            _accelaprojectmodel = accelaProjectInfo;
            _projectbo = new ProjectBO();
            _projectbe = _projectbo.GetByExternalRefInfo(accelaProjectInfo.ProjectNumber);
            _project = MapAccelaProject(_project, _projectbe, accelaProjectInfo);

            return _project;
        }

        /// <summary>
        /// Used by Accela Sync to insert a project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool InsertEstimationProjectDetails(ProjectEstimation project)
        {
            _project = project;
            _projectEstimation = project;
            _projectbo = new ProjectBO();
            _projectBRBO = new ProjectBusinessRelationshipBO();
            _projectbe = new ProjectBE();
            _projectAuditModelBO = new ProjectAuditModelBO();
            ExcludedPlanReviewersBO exbo = new ExcludedPlanReviewersBO();
            if (!this.Validate(ActionType.Insert))
                throw (new Exception(_errorMsg));

            try
            {
                //used by import from accela
                //add system user as created and updated
                if (_project.CreatedUser.ID == 0)
                {
                    _project.CreatedUser = new UserIdentityModelBO().GetInstance(1);
                }
                MapProjectToProjectBE();

                //upsert the project manager
                int userId = new UserAdapter().UpsertProjectManager(project);

                _projectbe.ProjectManagerId = (userId != 0) ? userId : (int?)null;
                _projectbe.TagUpdatedByTs = project.AccelaProjectLastUpdatedDate;

                _projectbe.CancellationFee = 0;
                //save
                _projectid = _projectbo.Create(_projectbe);

                if (project.IsProjectRTAP)
                {
                    UpsertProjectRtapMapping();
                    _projectAuditModelBO.InsertProjectAudit(_projectid, _project.AccelaRTAPProjectRefId, _project.CreatedUser.ID.ToString(), AuditActionEnum.Project_Linked);
                }

                if (_projectid != 0)
                {
                    if (_project.Agencies != null)
                    {
                        //save departments
                        foreach (var item in _project.Agencies)
                        {
                            MapPrjctDptmnt(item);
                            int deptid = _projectBRBO.Create(_projectBRBE);
                            foreach (var subitem in item.ExcludedPlanReviewers)
                            {
                                exbo.Create(new ExcludedPlanReviewersBE
                                {
                                    PlanReviewerId = subitem,
                                    ProjectBusinessRelationshipId = deptid,
                                    CreatedByWkrId = project.CreatedUser.ID.ToString()
                                });
                            }
                        }
                    }
                    if (_project.Trades != null)
                    {
                        foreach (var item in _project.Trades)
                        {
                            MapPrjctDptmnt(item);
                            int deptid = _projectBRBO.Create(_projectBRBE);
                            foreach (var subitem in item.ExcludedPlanReviewers)
                            {
                                exbo.Create(new ExcludedPlanReviewersBE
                                {
                                    PlanReviewerId = subitem,
                                    ProjectBusinessRelationshipId = deptid,
                                    CreatedByWkrId = project.CreatedUser.ID.ToString()
                                });
                            }
                        }
                    }

                    if (_project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Application_Submitted)
                        _projectAuditModelBO.InsertProjectAudit(_projectid, AuditActionEnum.Application_Submitted.ToStringValue(), project.CreatedUser.ID.ToString(), AuditActionEnum.Application_Submitted);
                   
                    project.ID = _projectid;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return true;
        }

        public bool UpsertProjectRtapMapping()
        {
            try
            {
                ProjectRtapMappingBO projectRtapMappingBO = new ProjectRtapMappingBO();

                ProjectRtapMappingBE projectRtapMappingBE = projectRtapMappingBO.GetByProjectId(_projectid);

                if (projectRtapMappingBE == null)
                {
                    ProjectBE originalProjectBE = _projectbo.GetByExternalRefInfo(_project.AccelaRTAPProjectRefId);
                    projectRtapMappingBE = new ProjectRtapMappingBE();
                    projectRtapMappingBE.ProjectId = _projectid;
                    projectRtapMappingBE.OriginalProjectId = originalProjectBE.ProjectId;
                    projectRtapMappingBO.Create(projectRtapMappingBE);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return true;
        }

        public bool UpdateProjectDetails(ProjectEstimation pe)
        {
            _projectAuditModelBO = new ProjectAuditModelBO();
            _project = pe;
            _projectEstimation = pe;
            _projectid = pe.ID;
            _projectbo = new ProjectBO();
            _projectbe = new ProjectBE();
            ProjectBE existingProjectBE = _projectbo.GetById(pe.ID);

            MapProjectToProjectBE();
            _projectbo.Update(_projectbe);
            if (existingProjectBE.ProjectStatusRefId != _projectbe.ProjectStatusRefId)
            {
                _projectAuditModelBO.InsertProjectAudit(_projectid, _project.AIONProjectStatus.ProjectStatusEnum.ToStringValue(), pe.UpdatedUser.ID.ToString(), AuditActionEnum.Status_Changed);
            }
            if (_project.AssignedFacilitator != null && _project.AssignedFacilitator != _projectbe.AssignedFacilitatorId)
            {
                UserIdentity facilitator = new UserIdentityModelBO().GetInstance((int)_projectbe.AssignedFacilitatorId);
                string facilitatorname = facilitator.FirstName + " " + facilitator.LastName;
                _projectAuditModelBO.InsertProjectAudit(_projectid, facilitatorname, _project.UpdatedUser.ID.ToString(), AuditActionEnum.Facilitator_Assigned);
            }

            return true;
        }
        /// <summary>
        /// This updates the project status by the project id
        /// Required: pe.ID, pe.UpdatedUser.ID, pe.AIONProjectStatus.ID, pe.UpdatedDate (should be existing update date)
        /// Updates the audit table with the project status change
        /// </summary>
        /// <param name="pe"></param>
        /// <returns></returns>
        public static bool UpdateProjectStatus(ProjectEstimation pe)
        {
            ProjectAuditModelBO projectAuditModelBO = new ProjectAuditModelBO();
            ProjectEstimation project = pe;
            int projectId = pe.ID;
            ProjectBO projectBO = new ProjectBO();
            ProjectBE existingProjectBE = projectBO.GetById(pe.ID);

            if (pe.UpdatedUser == null)
            {
                pe.UpdatedUser = new UserIdentityModelBO().GetInstance(1);
            }

            ProjectBE newProjectBE = new ProjectBE
            {
                ProjectStatusRefId = pe.AIONProjectStatus.ID,
                UpdatedDate = pe.UpdatedDate,
                ProjectId = pe.ID,
                UserId = pe.UpdatedUser.ID.ToString()
            };

            projectBO.UpdateProjectStatus(newProjectBE);

            if (existingProjectBE.ProjectStatusRefId != newProjectBE.ProjectStatusRefId)
            {
                projectAuditModelBO.InsertProjectAudit(projectId, project.AIONProjectStatus.ProjectStatusEnum.ToStringValue(), pe.UpdatedUser.ID.ToString(), AuditActionEnum.Status_Changed);
            }

            return true;
        }
        public bool UpdateEstimationProjectDetails(ProjectEstimation project)
        {
            _project = project;
            _projectEstimation = project;
            _projectid = project.ID;
            _projectbo = new ProjectBO();
            _projectBRBO = new ProjectBusinessRelationshipBO();
            _projectbe = new ProjectBE();
            _projectAuditModelBO = new ProjectAuditModelBO();
            ExcludedPlanReviewersBO exbo = new ExcludedPlanReviewersBO();
            ProjectBE existingProjectBE = _projectbo.GetById(_project.ID);

            NotesBO _notesBO = new NotesBO();

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));
            try
            {
                MapProjectToProjectBE();
                //jcl 9/3/21 LES-3508
                //Only update the project manager if this is from an accela call
                //otherwise we won't have the first last and email for comparison
                if (project.IsAccelaUpdate)
                {
                    //update the project manager
                    int userId = new UserAdapter().UpsertProjectManager(project);

                    _projectbe.ProjectManagerId = (userId != 0) ? userId : (int?)null;

                }
                _projectbe.TagUpdatedByTs = project.AccelaProjectLastUpdatedDate;

                _projectbe.CancellationFee = CalculateCancellationFee(project);

                if (_project.IsFifo)
                {
                    _projectbe.FifoDueDt = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now, 7);
                }

                //save
                _rowcount = _projectbo.Update(_projectbe);

                if (project.IsProjectRTAP)
                {
                    UpsertProjectRtapMapping();
                }

                if (_projectid != 0)
                {
                    foreach (var item in project.Agencies)
                    {
                        MapPrjctDptmnt(item);

                        int saveval = _projectBRBO.Update(_projectBRBE);

                        //only do this if this was a change from estimation
                        if (item.AuditAction == AuditActionEnum.Estimation_Change)
                        { ProjectAuditModelBO.InsertProjectDeptAudit(_projectid, item, project.UpdatedUser.ID.ToString()); }

                        exbo.SyncExcludedPlanReviewers(item.ID, item.ExcludedPlanReviewers.Select(x => x).ToList(), project.UpdatedUser.ID);

                    }
                    foreach (var item in project.Trades)
                    {
                        MapPrjctDptmnt(item);
                        int saveval = _projectBRBO.Update(_projectBRBE);
                        //only do this if this was a change from estimation
                        if (item.AuditAction == AuditActionEnum.Estimation_Change)
                        { ProjectAuditModelBO.InsertProjectDeptAudit(_projectid, item, project.UpdatedUser.ID.ToString()); }

                        exbo.SyncExcludedPlanReviewers(item.ID, item.ExcludedPlanReviewers.Select(x => x).ToList(), project.UpdatedUser.ID);
                    }
                    //notes are never udpated as per business requirement. It is always add new.
                    if (project.Notes.Any()) InsertProjectNotes(project);

                    string facilitatorname = string.Empty;

                    if (_project.AssignedFacilitator != null && _project.AssignedFacilitator.Value > 0 && (_project.AssignedFacilitator != existingProjectBE.AssignedFacilitatorId))
                    {
                        UserIdentity facilitator = new UserIdentityModelBO().GetInstance((int)_projectbe.AssignedFacilitatorId);
                        facilitatorname = facilitator.FirstName + " " + facilitator.LastName;
                        _projectAuditModelBO.InsertProjectAudit(_projectid, facilitatorname, _project.UpdatedUser.ID.ToString(), AuditActionEnum.Facilitator_Assigned);
                    }

                    if (_project.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Not_Scheduled)
                    {
                        _projectAuditModelBO.InsertProjectAudit_AllEstimationsCompleted(project);
                    }

                    //Add audit when status changes
                    if (_project.AIONProjectStatus.ID != existingProjectBE.ProjectStatusRefId)
                    {
                        _projectAuditModelBO.InsertProjectAudit(_projectid, _project.AIONProjectStatus.ProjectStatusEnum.ToStringValue(), project.UpdatedUser.ID.ToString(), AuditActionEnum.Status_Changed);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateEstimationProjectDetails - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return true;
        }

        private decimal? CalculateCancellationFee(Project project)
        {
            decimal? cancellationFeePerHour = CatalogItemModelBO.GetCancellationFeePerHour();
            decimal totalhrs = GetBEMPTotalHoursEstimated(project);

            if (totalhrs == 0 || cancellationFeePerHour == 0)
            {
                return 0;
            }
            else
            {
                return totalhrs * cancellationFeePerHour;
            }

        }

        public static decimal GetBEMPTotalHoursEstimated(Project project)
        {
            if (project == null || !project.CycleNbr.HasValue) return 0;
            //get the current cycle, return hours for current cycle
            List<PlanReviewScheduleDetailBE> list = new PlanReviewScheduleDetailBO().GetListByProjectCycle(project.ID, project.CycleNbr.Value);
            decimal totalhrs = 0;
            foreach (PlanReviewScheduleDetailBE item in list)
            {
                totalhrs += item.AssignedHoursNbr.Value;
            }
            return totalhrs;
        }

        /// <summary>
        /// This is only used when getting data from accela
        /// Called by SaveAccelaProject
        /// Do not use for regular updates
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool UpdateEstimationProjectDetailsFromAccela(ProjectEstimation project)
        {
            _project = project;
            _projectEstimation = project;
            _projectid = project.ID;
            _projectbo = new ProjectBO();
            _projectBRBO = new ProjectBusinessRelationshipBO();
            _projectbe = new ProjectBE();
            _projectAuditModelBO = new ProjectAuditModelBO();
            ExcludedPlanReviewersBO exbo = new ExcludedPlanReviewersBO();

            var existingProjectBRBOs = new List<ProjectBusinessRelationshipBE>();
            ProjectBE existingProjectBE = _projectbo.GetById(_project.ID);

            //IMPORTANT: KEEP EXISTING CYCLE NUMBER
            project.CycleNbr = existingProjectBE.CycleNbr;

            NotesBO _notesBO = new NotesBO();

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));
            try
            {
                MapProjectToProjectBE();
                //jcl 9/3/21 LES-3508
                //Only update the project manager if this is from an accela call
                //otherwise we won't have the first last and email for comparison
                if (project.IsAccelaUpdate)
                {
                    //update the project manager
                    int userId = new UserAdapter().UpsertProjectManager(project);

                    _projectbe.ProjectManagerId = (userId != 0) ? userId : (int?)null;

                }
                //save
                _rowcount = _projectbo.Update(_projectbe);

                if (_projectid != 0)
                {
                    if (_project.IsProjectRTAP)
                    {
                        UpsertProjectRtapMapping();
                    }

                    existingProjectBRBOs = _projectBRBO.GetListByProjectId(_projectid);
                    //TODO
                    if (project.Agencies != null)
                    {
                        int businessRefId = 0;
                        foreach (ProjectAgency item in project.Agencies)
                        {
                            //jcl 2/9/22 if businessrefid is the same, skip this iteration since this is likely an error
                            int holdBusinessRefId = businessRefId;
                            businessRefId = new DepartmentModelBO().GetInstance(item.DepartmentInfo).ID;
                            if (holdBusinessRefId == businessRefId)
                            {
                                continue;
                            }
                            MapPrjctDptmnt(item);

                            ProjectBusinessRelationshipBE matchingBRBO = existingProjectBRBOs.Where(x => x.BusinessRefId == businessRefId).FirstOrDefault();

                            if (matchingBRBO != null)
                            {
                                matchingBRBO.ProposedPlanReviewerId = item.ProposedPlanReviewer.ID == 0 ? -1 : item.ProposedPlanReviewer.ID;
                                int saveval = _projectBRBO.Update(matchingBRBO);
                            }

                        }
                    }
                    //TODO
                    if (project.Trades != null)
                    {
                        foreach (ProjectTrade item in project.Trades)
                        {
                            MapPrjctDptmnt(item);

                            var businessRefId = new DepartmentModelBO().GetInstance(item.DepartmentInfo).ID;
                            var matchingBRBO = existingProjectBRBOs.Where(x => x.BusinessRefId == businessRefId).FirstOrDefault();

                            if (matchingBRBO != null)
                            {
                                matchingBRBO.ProposedPlanReviewerId = item.ProposedPlanReviewer.ID == 0 ? -1 : item.ProposedPlanReviewer.ID;
                                int saveval = _projectBRBO.Update(matchingBRBO);
                                exbo.SyncExcludedPlanReviewers(item.ID, item.ExcludedPlanReviewers.Select(x => x).ToList(), project.UpdatedUser.ID);
                            }
                            else
                            {
                                int deptid = _projectBRBO.Create(_projectBRBE);
                                foreach (var subitem in item.ExcludedPlanReviewers)
                                {
                                    exbo.Create(new ExcludedPlanReviewersBE
                                    {
                                        PlanReviewerId = subitem,
                                        ProjectBusinessRelationshipId = deptid,
                                        CreatedByWkrId = project.UpdatedUser.ID.ToString()
                                    });
                                }
                            }

                        }
                    }
                    //notes are never udpated as per business requirement. It is always add new.
                    //   InsertProjectNotes(project.Notes, _projectid);

                    InsertProjectNotes(project);

                    if (_project.AssignedFacilitator != null && _project.AssignedFacilitator != _projectbe.AssignedFacilitatorId)
                    {
                        UserIdentity facilitator = new UserIdentityModelBO().GetInstance((int)_projectbe.AssignedFacilitatorId);
                        string facilitatorname = facilitator.FirstName + " " + facilitator.LastName;
                        _projectAuditModelBO.InsertProjectAudit(_projectid, facilitatorname, _project.UpdatedUser.ID.ToString(), AuditActionEnum.Facilitator_Assigned);
                    }
                    _projectAuditModelBO.InsertProjectAudit_AllEstimationsCompleted(project);
                    //Add audit when status changes
                    if (_project.AIONProjectStatus.ID != existingProjectBE.ProjectStatusRefId)
                    {
                        _projectAuditModelBO.InsertProjectAudit(_projectid, _project.AIONProjectStatus.ProjectStatusEnum.ToStringValue(), project.UpdatedUser.ID.ToString(), AuditActionEnum.Status_Changed);
                    }

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return true;
        }

        public bool CancelProject(Project project)
        {
            AbortProject(project);

            //Set project to cancelled
            new ProjectBO().Cancel(project.ID, project.UpdatedUser.ID);
            return true;
        }

        /// <summary>
        /// Cancel plan reviews and appointments by project id
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool AbortProject(Project project)
        {
            //Cancel Plan Reviews
            new PlanReviewScheduleBO().CancelPlanReviewByProjectId(project.ID, project.UpdatedUser.ID);

            //Cancel Express Meeting Appointments
            List<int> planReviewScheduleIds = new PlanReviewScheduleBO().CancelEMAByProjectId(project.ID, project.UpdatedUser.ID);

            foreach (int planReviewScheduleId in planReviewScheduleIds)
            {
                PlanReview planReview = new PlanReviewAdapter().GetPlanReviewByPlanReviewScheduleId(planReviewScheduleId);

                ExpressMeetingAppointment ema = new EMAAdapter().ConvertPlanReviewToEMA(planReview);

                new EMAAdapter(ema).CancelAppointment();
            }

            //Cancel Preliminary Meeting Appointments
            IPMAAccessor pmaAccessor = new PMAAccessor();
            pmaAccessor.CancelByProjectId(project.ID);

            //Cancel Facilitator Meeting Appointments
            IFMAAccessor fmaAccessor = new FMAAccessor();
            fmaAccessor.CancelByProjectId(project.ID);

            return true;
        }

        private void InsertProjectNotes(Project project)
        {
            NotesBO bo = new NotesBO();
            if (project.UpdatedUser != null)
            {
                if (project.UpdatedUser.ID == 0) project.UpdatedUser.ID = 1;
            }
            else
            {
                project.UpdatedUser = new UserIdentity();
                project.UpdatedUser.ID = project.UpdatedUser.ID;
            }
            foreach (var item in project.Notes)
            {
                if (String.IsNullOrWhiteSpace(item.NotesComments)) continue;
                if (item.NotesType == null)
                {
                    item.NotesType = new NoteTypeModelBO().GetInstance(NoteTypeEnum.NA);
                }
                NotesBE be = new NotesBE();
                be.CreatedByWkrId = project.UpdatedUser.ID.ToString();
                be.UpdatedByWkrId = project.UpdatedUser.ID.ToString();
                be.CreatedDate = DateTime.Now;
                be.UpdatedDate = DateTime.Now;
                be.NotesComment = item.NotesComments;
                be.NotesTypeRefId = item.NotesType.ID;
                be.ProjectId = item.ProjectID;
                be.BusinessRefID = (int)item.DeptNameEnum;
                be.ParentNoteID = item.ParentNoteID;
                bo.Create(be);
            }

        }


        /// <summary>
        /// decide if project needs update or insert
        /// based on ID != 0
        /// if the ID != 0 then the AION project already exists
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool UpsertEstimationProjectDetails(ProjectEstimation project)
        {
            string error = "";
            try
            {
                _project = project;
                _projectEstimation = project;
                //decide when to insert or update
                //does this project already have an id?
                //if it has an id, then it already exists in the AION db
                if (project.ID != 0)
                {
                    //need to use logged in user
                    //project.UpdatedUser = new Lazy<UserIdentity>();
                    UpdateEstimationProjectDetails(project);
                }
                else
                {
                    //need to use logged in user
                    //project.CreatedUser = new Lazy<UserIdentity>();
                    InsertEstimationProjectDetails(project);
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpsertEstimationProjectDetails - " + error;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (ex);
            }
        }

        private ProjectBE MapProjectToProjectBE()
        {
            ProjectStatus projectstatus = new ProjectStatusModelBO().GetInstance(_projectEstimation.AIONProjectStatus.ProjectStatusEnum);
            if (projectstatus == null) projectstatus = new ProjectStatus();
            _projectbe.ProjectId = _projectEstimation.ID;
            _projectbe.ProjectNm = _projectEstimation.ProjectName;
            _projectbe.ExternalSystemRefId = (int)ExternalSystemEnum.Accela;
            _projectbe.ProjectStatusRefId = projectstatus.ID;
            _projectbe.ProjectTypRefId = (int)_projectEstimation.AccelaPropertyType;
            _projectbe.SrcSystemValTxt = _projectEstimation.AccelaProjectRefId;
            _projectbe.TagCreatedByTs = _projectEstimation.AccelaProjectCreatedDate;
            _projectbe.TagCreatedIdNum = _projectEstimation.AccelaProjectCreatedByRefId;
            _projectbe.TagUpdatedByIdNum = _projectEstimation.AccelaProjectLastUpdatedByRefId;
            _projectbe.TagUpdatedByTs = _projectEstimation.AccelaProjectLastUpdatedDate;
            _projectbe.UpdatedDate = _projectEstimation.UpdatedDate;
            //_projectbe.UpdatedByWkrId = _project.UpdatedUser;
            _projectbe.CreatedDate = _projectEstimation.CreatedDate;
            //_projectbe.CreatedByWkrId = _project.CreatedUser;
            _projectbe.ProjectLvlTxt = _projectEstimation.ProjectLvlTxt;
            _projectbe.ProjectAddrTxt = _projectEstimation.ProjectAddress;
            _projectbe.ProjectManagerId = _projectEstimation.PMId;
            _projectbe.RtapInd = _projectEstimation.IsProjectRTAP;
            _projectbe.GateAcceptedInd = _projectEstimation.IsGateAccepted;
            _projectbe.GateDt = _projectEstimation.GateDt;
            _projectbe.HighRiseInd = _projectEstimation.IsHighRise;
            _projectbe.StoriesCnt = _projectEstimation.AccelaNumberOfStories;

            _projectbe.ProjectOccupancyTypMapNm = _projectEstimation.ProjectOccupancyTypMapNm;

            if (_projectEstimation.AssignedFacilitator != null)
                _projectbe.AssignedFacilitatorId = _projectEstimation.AssignedFacilitator;

            _projectbe.ProjectModeRefId = (int)_projectEstimation.ReviewType;

            if (_projectEstimation.AssignedEstimator > 0)
                _projectbe.AssignedEstimatorId = _projectEstimation.AssignedEstimator;

            _projectbe.PlansReadyOnDt = _projectEstimation.PlansReadyOnDate;

            _projectbe.FifoDueDt = _projectEstimation.FifoDueDt;
            _projectbe.FifoDueAccelaDt = _projectEstimation.FifoDueAccelaDt;
            _projectbe.FifoInd = _projectEstimation.IsFifo;
            _projectbe.BuildContrNm = _projectEstimation.BuildingContractorName;
            _projectbe.BuildContrAcctNum = _projectEstimation.BuildingContractorAcctNo;
            _projectbe.PreliminaryInd = _projectEstimation.IsPreliminaryMeetingRequested;

            //map display only fields
            _projectbe.ArchitectDesignerAutoEmailAddrTxt = _projectEstimation.DisplayOnlyInformation.ArchDesAutoEmail;
            //_projectbe.ProjectNm = _projectEstimation.DisplayOnlyInformation.ProjectName;
            _projectbe.ProjectAddrTxt = _projectEstimation.DisplayOnlyInformation.ProjectAddress;
            _projectbe.BuildCodeVersionDesc = _projectEstimation.DisplayOnlyInformation.BuildingCodeVersion;
            _projectbe.WorkTypDesc = _projectEstimation.DisplayOnlyInformation.TypeOfWork;
            _projectbe.ConstrTypDesc = _projectEstimation.DisplayOnlyInformation.TypeOfConstruction;
            _projectbe.OccupancyDesc = _projectEstimation.DisplayOnlyInformation.Occupancy;
            _projectbe.PriOccupancyDesc = _projectEstimation.DisplayOnlyInformation.PrimaryOccupancy;
            _projectbe.SecondaryOccupancyDesc = _projectEstimation.DisplayOnlyInformation.SecondaryOccupancy;
            _projectbe.SquareFootageDesc = _projectEstimation.DisplayOnlyInformation.SquareFootage;
            _projectbe.SheetsCntDesc = _projectEstimation.DisplayOnlyInformation.NumofSheets;
            _projectbe.SealHoldersDesc = _projectEstimation.DisplayOnlyInformation.SealHolders;
            _projectbe.DesignerDesc = _projectEstimation.DisplayOnlyInformation.Designers;
            _projectbe.FireDetailDesc = _projectEstimation.DisplayOnlyInformation.FireDetails;
            _projectbe.OverallWorkScopeDesc = _projectEstimation.DisplayOnlyInformation.ScopeOfWorkOverall;
            _projectbe.MechWorkScopeDesc = _projectEstimation.DisplayOnlyInformation.ScopeOfWorkMechanical;
            _projectbe.CivilWorkScopeDesc = _projectEstimation.DisplayOnlyInformation.ScopeOfWorkCivil;
            _projectbe.ElctrWorkScopeDesc = _projectEstimation.DisplayOnlyInformation.ScopeOfWorkElectrical;
            _projectbe.PlumbWorkScopeDesc = _projectEstimation.DisplayOnlyInformation.ScopeOfWorkPlumbing;
            _projectbe.ZoningOfSiteDesc = _projectEstimation.DisplayOnlyInformation.ZoningOfSite;
            _projectbe.ChgOfUseDesc = _projectEstimation.DisplayOnlyInformation.ChangeOfUse;
            _projectbe.ConditionalPermitApprovalDesc = _projectEstimation.DisplayOnlyInformation.IsConditionalPermitApproval;
            _projectbe.PreviousBusinessTypDesc = _projectEstimation.DisplayOnlyInformation.PreviousBusinessType;
            _projectbe.CityOfCharlotteDesc = _projectEstimation.DisplayOnlyInformation.CityOfC;
            _projectbe.ProposedBusinessTypDesc = _projectEstimation.DisplayOnlyInformation.ProposedBusinessType;
            _projectbe.CodeSummaryDesc = _projectEstimation.DisplayOnlyInformation.CodeSummary;
            _projectbe.BackflowApplicationDetailDesc = _projectEstimation.DisplayOnlyInformation.BackflowApplictnDet;
            _projectbe.WaterSewerDetailDesc = _projectEstimation.DisplayOnlyInformation.WaterSewerDetails;
            _projectbe.HealthDeptDetailDesc = _projectEstimation.DisplayOnlyInformation.HealthDeptDetails;
            _projectbe.DayCareDesc = _projectEstimation.DisplayOnlyInformation.DayCare;
            _projectbe.ProposedOutdoorUndergroundPipingDesc = _projectEstimation.DisplayOnlyInformation.ProposedOutdoorUndergroundPiping;
            _projectbe.ProposedFireSprinklerPipingDesc = _projectEstimation.DisplayOnlyInformation.ProposedFireSprinklerPiping;
            _projectbe.InstallCmudBackflowPreventerDesc = _projectEstimation.DisplayOnlyInformation.IsInstallingCMUDBackflowPreventer;
            _projectbe.ExtendingPublicWaterSewerDesc = _projectEstimation.DisplayOnlyInformation.ExtendingPublicWaterSewer;
            _projectbe.GradeModWaterSewerEasementDesc = _projectEstimation.DisplayOnlyInformation.GradeModificationWaterSewerEasement;
            _projectbe.ProposedEncroachmentWaterSewerEasementDesc = _projectEstimation.DisplayOnlyInformation.ProposedEncroachmentWaterSewerEasement;
            _projectbe.ParcelNum = _projectEstimation.DisplayOnlyInformation.ParcelNumber;
            _projectbe.AffordableHousingDesc = _projectEstimation.DisplayOnlyInformation.IsAffordableHousing;
            _projectbe.ExactAddrTxt = _projectEstimation.DisplayOnlyInformation.ExactAddress;
            _projectbe.DeliveryMthdDesc = _projectEstimation.DisplayOnlyInformation.DeliveryMethod;
            _projectbe.BimDesc = _projectEstimation.DisplayOnlyInformation.IsBIM;
            _projectbe.AttendeesCntDesc = _projectEstimation.DisplayOnlyInformation.NumOfAttendees;
            _projectbe.PreviousPrelimReviewDesc = _projectEstimation.DisplayOnlyInformation.PreviousPreliminaryReview;
            _projectbe.ProjectNumPreviousPrelimReviewDesc = _projectEstimation.DisplayOnlyInformation.ProjectNumberPrevPrelimReview;
            _projectbe.SameReviewTeamDesc = _projectEstimation.DisplayOnlyInformation.IsSameReviewTeam;
            _projectbe.PropertyOwnerNm = _projectEstimation.DisplayOnlyInformation.PropertyOwnerName;
            _projectbe.PropertyOwnerAddrTxt = _projectEstimation.DisplayOnlyInformation.PropertyOwnerAddress;
            _projectbe.PropertyOwnerEmailAddrTxt = _projectEstimation.DisplayOnlyInformation.PropertyOwnerEmail;
            _projectbe.PropertyOwnerPhoneNum = _projectEstimation.DisplayOnlyInformation.PropertyOwnerPhone;
            _projectbe.PropertyManagerNm = _projectEstimation.DisplayOnlyInformation.PropertyManagerName;
            _projectbe.PropertyManagerPhoneNum = _projectEstimation.DisplayOnlyInformation.PropertyManagerPhone;
            _projectbe.PropertyManagerEmailAddrTxt = _projectEstimation.DisplayOnlyInformation.PropertyManagerEmail;
            _projectbe.PropertyManagerEmailAddr2Txt = _projectEstimation.DisplayOnlyInformation.PropertyManagerEmail2;
            _projectbe.ArchitectDesignerCntctNm = _projectEstimation.DisplayOnlyInformation.ArchDesContactName;
            _projectbe.ArchitectDesignerCntctPhoneNum = _projectEstimation.DisplayOnlyInformation.ArchDesContactPhone;
            _projectbe.ArchitectDesignerCntctEmailAddrTxt = _projectEstimation.DisplayOnlyInformation.ArchDesContactEmail;
            _projectbe.ArchitectDesignerAutoEmailAddrTxt = _projectEstimation.DisplayOnlyInformation.ArchDesAutoEmail;
            _projectbe.ArchitectDrawingsSealedDesc = _projectEstimation.DisplayOnlyInformation.IsArchDrawingsSealed;
            _projectbe.ArchitectDesignerLicenseNum = _projectEstimation.DisplayOnlyInformation.ArchDesLicenseNum;
            _projectbe.ArchitectDesignerLicenseBoardDesc = _projectEstimation.DisplayOnlyInformation.ArchDesLicenseBoard;
            _projectbe.ArchitectDesignerEmployeeDesc = _projectEstimation.DisplayOnlyInformation.IsArchDesEmployee;
            _projectbe.PermitNum = _projectEstimation.DisplayOnlyInformation.PermitNumber;
            _projectbe.TotalFeeAmt = _projectEstimation.DisplayOnlyInformation.TotalFee.HasValue ? _projectEstimation.DisplayOnlyInformation.TotalFee : 0;
            _projectbe.CycleNbr = _projectEstimation.CycleNbr.HasValue ? _projectEstimation.CycleNbr : 0;
            _projectbe.RecIdTxt = _projectEstimation.RecIdTxt;
            _projectbe.TeamGradeTxt = _projectEstimation.TeamGradeTxt;
            _projectbe.ReviewTypRefDesc = _projectEstimation.ReviewTypRefDesc;
            _projectbe.TotalJobCostAmt = _projectEstimation.ProjectCostTotal;
            _projectbe.AccelaRtapProjectRefId = _projectEstimation.AccelaRTAPProjectRefId;

            //jcl 8-11-21 preliminary object details
            _projectbe.PrelimBIMProjectDeliveryObjDetails = _projectEstimation.PrelimBIMProjectDeliveryObjDetails;
            _projectbe.PrelimGeneralInfoObjDetails = _projectEstimation.PrelimGeneralInfoObjDetails;
            _projectbe.PrelimMeetingAgendaObjDetails = _projectEstimation.PrelimMeetingAgendaObjDetails;
            _projectbe.PrelimMeetingDetailObjDetails = _projectEstimation.PrelimMeetingDetailObjDetails;
            _projectbe.PrelimProjectSummaryObjDetails = _projectEstimation.PrelimProjectSummaryObjDetails;
            _projectbe.PrelimProposedWorkObjDetails = _projectEstimation.PrelimProposedWorkObjDetails;
            _projectbe.PrelimSystemInfoObjDetails = _projectEstimation.PrelimSystemInfoObjDetails;
            _projectbe.PrelimTypeOfWorkObjDetails = _projectEstimation.PrelimTypeOfWorkObjDetails;

            _projectbe.CancellationFee = _projectEstimation.CancellationFee;

            _projectbe.IsPaidStatus = _projectEstimation.IsPaidStatus;

            _projectbe.EstimatedFee = _projectEstimation.EstimatedFee;

            //LES-3407 RTAP
            _projectbe.RTAPAffordableUnitChange = _projectEstimation.RTAPAffordableUnitChange;
            _projectbe.RTAPAffordableUnitsRemove = _projectEstimation.RTAPAffordableUnitsRemove;
            _projectbe.RTAPAffordableWorkforceUnitsAdd = _projectEstimation.RTAPAffordableWorkforceUnitsAdd;
            _projectbe.RTAPWorkforceAdd = _projectEstimation.RTAPWorkforceAdd;
            _projectbe.RTAPWorkforceRemove = _projectEstimation.RTAPWorkforceRemove;
            _projectbe.Professionals = _projectEstimation.Professionals;
            _projectbe.AccountNumber = _projectEstimation.AccountNumber;
            _projectbe.EquipmentCost = _projectEstimation.EquipmentCost;
            _projectbe.PrepaidFeePaymentType = _projectEstimation.PrepaidFeePaymentType;
            _projectbe.SquareFootageOfOverallBuildNbr = _projectEstimation.AccelaSqrFtOfOverallBuilding;
            _projectbe.SquareFootageToBeReviewedNbr = _projectEstimation.AccelaSqrFtToBeReviewed;

            _projectbe.ConstrCostAmt = Convert.ToDecimal(_projectEstimation.AccelaCostOfConstruction);
            _projectbe.UserId = _projectEstimation.UpdatedUser == null ? "1" : _projectEstimation.UpdatedUser.ID.ToString();
            _projectbe.AccelaPrelimProjectRefId = _projectEstimation.AccelaPreliminaryProjectRefId;

            return _projectbe;
        }

        private ProjectBusinessRelationshipBE MapPrjctDptmnt(ProjectDepartment dept)
        {
            _projectdepartment = dept;
            _projectBRBE = new ProjectBusinessRelationshipBE
            {
                ProjectBusinessRelationshipId = dept.ID,
                BusinessRefId = new DepartmentModelBO().GetInstance(dept.DepartmentInfo).ID,
                EstimationHoursNbr = dept.EstimationHours,
                ProjectId = _projectid,
                UpdatedDate = dept.UpdatedDate,
                CreatedDate = dept.CreatedDate,
                AssignedPlanReviewerId = ConvertZeroToNull(dept.AssignedPlanReviewer.ID),
                PrimaryPlanReviewerId = ConvertZeroToNull(dept.PrimaryPlanReviewer.ID),
                ProposedPlanReviewerId = ConvertZeroToNull(dept.ProposedPlanReviewer.ID),
                SecondaryPlanReviewerId = ConvertZeroToNull(dept.SecondaryPlanReviewer.ID),
                IsEstimationNotApplicable = dept.EstimationNotApplicable,
                ProjectBusinessRelationshipStatusDesc = dept.DepartmentStatus,
                StatusRefId = dept.DepartmentStatusRef.ID,
                IsDeptRequested = dept.IsDeptRequested,
                UserId = dept.UpdatedUser == null ? "1" : dept.UpdatedUser.ID.ToString()
            };
            return _projectBRBE;
        }



        public Project MapAccelaProject(Project project, ProjectBE projectBE, AccelaProjectModel accelaProjectModel)
        {
            double resultDouble;
            int resultInt;
            DateTime dateTime;
            AccelaPropertyTypeBO accelaPropertyTypeBO = new AccelaPropertyTypeBO();
            AccelaDepartmentBO accelaDepartmentBO = new AccelaDepartmentBO();
            if (accelaProjectModel.DisplayOnlyInformation == null)
            {
                project.DisplayOnlyInformation = new AccelaProjectDisplayInfo();
            }
            else
            {
                project.DisplayOnlyInformation = new AccelaProjectDisplayInfoBO().GenerateDisplayOnlyInformation(accelaProjectModel);
            }
            //keep these 5 in top since it is used in below assignments for decision making.
            if (accelaProjectModel.RecordType == "Preliminary Meeting")
            {
                accelaProjectModel.IsPreliminaryMeetingRequested = true;
                if (accelaProjectModel.ProjectStatusCodeRef == "Cancelled") accelaProjectModel.IsPreliminaryMeetingCancelled = true;
                if (accelaProjectModel.ProjectStatusCodeRef == "Project Ended - Success") accelaProjectModel.IsPreliminaryMeetingCompleted = true;

                accelaProjectModel.ProjectTradesList = accelaDepartmentBO.GetAccelaTradeInfoList(accelaProjectModel.PrelimMeetingTradesAndReviewer);
                accelaProjectModel.ProjectAgencyList = accelaDepartmentBO.GetAccelaAgencyInfoList(accelaProjectModel.PrelimMeetingTradesAndReviewer);
            }
            project.IsPreliminaryMeetingRequested = accelaProjectModel.IsPreliminaryMeetingRequested;
            project.IsPreliminaryMeetingCompleted = accelaProjectModel.IsPreliminaryMeetingCompleted;

            if (accelaProjectModel.RecordType.Contains("RTAP")) accelaProjectModel.IsProjectRTAP = true;
            project.IsProjectRTAP = accelaProjectModel.IsProjectRTAP;
            if (accelaProjectModel.GateResponses.FirstOrDefault() != null)
            {
                if (accelaProjectModel.GateResponses.FirstOrDefault().Reason.Contains("Accept")) project.IsGateAccepted = true;
                else project.IsGateAccepted = false;
            }
            else project.IsGateAccepted = false;

            project.PMId = projectBE.ProjectManagerId;
            project.AssignedFacilitator = projectBE.AssignedFacilitatorId;

            foreach (var contact in accelaProjectModel.Contacts)
            {
                if (contact.ContactType == "Project Manager")
                {
                    project.PMName = contact.FirstName + " " + contact.LastName;
                    project.PMFirstName = contact.FirstName;
                    project.PMLastName = contact.LastName;
                    project.PMPhone = contact.PhoneNumber;
                    project.PMEmail = contact.EmailAddress;
                }
            }

            project.AccelaRTAPProjectRefId = accelaProjectModel.AccelaRTAPProjectRefId;
            project.AccelaPreliminaryProjectRefId = accelaProjectModel.AccelaPreliminaryProjectRefId;
            //keep above 5 in top.
            //trim occupancy string
            if (accelaProjectModel.OccupancyType != null) project.AccelaOccupancyType = accelaProjectModel.OccupancyType.Contains("*") ? accelaProjectModel.OccupancyType.Substring(0, accelaProjectModel.OccupancyType.IndexOf("*") - 1) : accelaProjectModel.OccupancyType;
            if (!string.IsNullOrEmpty(project.AccelaOccupancyType))
            {
                project.ProjectOccupancyTypMapNm = new AccelaProjectOccupancyTypeBO().GetProjectOccupancyTypeNameFromAccelaOccupancyType(project.AccelaOccupancyType);
            }

            project.AccelaConstructionType = accelaProjectModel.ConstructionType;
            project.AccelaCostOfConstruction = Double.TryParse(accelaProjectModel.CostOfConstruction, out resultDouble) ? resultDouble : 0.00;
            project.AccelaNumberofSheets = int.TryParse(accelaProjectModel.NumberofSheets, out resultInt) ? resultInt : 0;

            project.AccelaSqrFtToBeReviewed = int.TryParse(accelaProjectModel.SquareFootageToBeReviewed, out resultInt) ? resultInt : 0;
            project.AccelaSqrFtOfOverallBuilding = int.TryParse(accelaProjectModel.SquareFootageOfOverallBuilding, out resultInt) ? resultInt : 0;

            project.AccelaNumberOfStories = int.TryParse(accelaProjectModel.NumberOfStories, out resultInt) ? resultInt : 0;
            project.IsHighRise = accelaProjectModel.IsHighRise;
            project.PlansReadyOnDate = accelaProjectModel.PlansReadyOnDate;

            //requested express date is the same as plans ready on date
            if (accelaProjectModel.IsExpress && accelaProjectModel.PlansReadyOnDate.HasValue)
            {
                accelaProjectModel.RequestedExpressDates = new List<Meck.Shared.Accela.RequestedExpressDateBE>();
                accelaProjectModel.RequestedExpressDates.Add(new Meck.Shared.Accela.RequestedExpressDateBE { RequestedExpressDate = accelaProjectModel.PlansReadyOnDate.Value });
            }
            if (accelaProjectModel.RequestedExpressDates != null && accelaProjectModel.RequestedExpressDates.Any())
            {
                project.AccelaRequestedExpressDates = accelaProjectModel.RequestedExpressDates;
            }

            if (accelaProjectModel.DisplayOnlyInformation == null)
            {
                project.BuildingCodeVersion = string.Empty;
                project.ProjectAddress = string.Empty;
            }
            else
            {
                project.BuildingCodeVersion = accelaProjectModel.DisplayOnlyInformation.BuildingCodeVersion;
                project.ProjectAddress = accelaProjectModel.DisplayOnlyInformation.ProjectAddress;
            }
            //jcl change src system val txt to alt id, change recidtxt to accela project id
            project.AccelaProjectRefId = accelaProjectModel.ProjectNumber;
            project.RecIdTxt = accelaProjectModel.ProjectIDRef;
            project.TeamGradeTxt = accelaProjectModel.TeamGrade;

            project.ReviewTypRefDesc = accelaPropertyTypeBO.MapPropertyTypeForDisplayOnly(accelaProjectModel);

            //mmf, townhomes, fifo

            PropertyTypeEnums propertyType = accelaProjectModel.IsExpress ? PropertyTypeEnums.Express : accelaPropertyTypeBO.MapAccelaPropertyType(accelaProjectModel);

            accelaProjectModel.PropertyTypeRef = propertyType.ToStringValue();
            project.AccelaPropertyType = propertyType;
            project.AionPropertyType = propertyType;

            project.AccelaProjectCreatedDate = ConvertDefaultDTToNull(accelaProjectModel.CreatedTS);
            project.AccelaProjectLastUpdatedDate = ConvertDefaultDTToNull(accelaProjectModel.UpdatedTS);

            project.ProjectName = accelaProjectModel.ProjectName;

            if (project.IsPreliminaryMeetingRequested == true && project.IsPreliminaryMeetingCompleted == false)
            {
                project.IsProjectPreliminary = true;
            }

            project.IsFifo = project.IsProjectPreliminary ? false : DetermineIfFIFO(propertyType);

            project.FifoDueDt = null; // LES-4565 switch to estimation completion
            project.FifoDueAccelaDt = accelaProjectModel.FifoDueAccelaDt;


            project.ProjectLvlTxt = projectBE.ProjectLvlTxt;

            // project.AccelaCostOfConstruction = accelaProjectModel.TotalJobCost;

            //if record found from DB and no not null then consider this as edit new and perform all the edit mode thigns here.
            if (projectBE.ProjectId != null && projectBE.ProjectId > 0)
            {
                //TODO: ADAM, Verify PMId and AssignedFacilitator value from the top
                project.PMId = projectBE.ProjectManagerId;
                project.AssignedFacilitator = projectBE.AssignedFacilitatorId;
                if (projectBE.ProjectStatusRefId == null)
                    projectBE.ProjectStatusRefId = 0;

                _projectid = (int)projectBE.ProjectId;
                project.ID = (int)projectBE.ProjectId;
                project.CycleNbr = projectBE.CycleNbr;
                project.ReviewType = (ReviewTypeEnum)projectBE.ProjectModeRefId;
                project.CreatedDate = (DateTime)projectBE.CreatedDate;
                project.UpdatedDate = (DateTime)projectBE.UpdatedDate;
                project.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(projectBE.ProjectStatusRefId.Value);
                project.AccelaProjectStatus = accelaProjectModel.ProjectStatusCodeRef;
                project.PlansReadyOnDate = projectBE.PlansReadyOnDt;
                project.ProjectName = accelaProjectModel.ProjectName;

                //get the departments if the project is an existing one.
                GetExistingProjectDepartments(project, accelaProjectModel.ProjectAgencyList, accelaProjectModel.ProjectTradesList);
            }
            else //this is a new project and so need to set New status
            {
                project.CycleNbr = accelaProjectModel.CycleNumber;
                project.PlansReadyOnDate = accelaProjectModel.PlansReadyOnDate;
                /*TBD: Need to be added with a model object and then picked up to ModelBO. For now doing it directly */
                project.ReviewType = new ReviewTypeEnum().CreateInstance(accelaProjectModel.ReviewTypeRef);
                project.AIONProjectStatus = new ProjectStatusModelBO().GetInstance(accelaProjectModel.ProjectStatusCodeRef, ExternalSystemEnum.Accela);
                project.CreatedDate = accelaProjectModel.CreatedTS;
                project.CreatedUser = new UserIdentityModelBO().GetInstance(accelaProjectModel.CreatedBy, ExternalSystemEnum.Accela);
                if (project.IsProjectRTAP)
                {
                    //get the original project
                    ProjectBE rtapProject = new ProjectBO().GetByExternalRefInfo(accelaProjectModel.AccelaRTAPProjectRefId);

                    //set the project level
                    project.ProjectLvlTxt = rtapProject.ProjectLvlTxt;
                }

                // if the project is a new one generate list of departmetns based on department list recived from accela.
                CreateListsOfNewDepartments(project, accelaProjectModel);
            }

            if (DateTime.TryParse(accelaProjectModel.GateDate, out dateTime))
            {
                project.GateDt = ConvertDefaultDTToNull(dateTime);
            }
            else
            {
                project.GateDt = null;
            }
            project.AccelaProjectLastUpdatedDate = accelaProjectModel.UpdatedTS;

            project.BuildingContractorName = accelaProjectModel.BuildingContractorName;
            project.BuildingContractorAcctNo = accelaProjectModel.BuildingContractorAcctNo;
            project.ProjectCostTotal = string.IsNullOrEmpty(accelaProjectModel.TotalJobCost) ? 0 : decimal.Parse(accelaProjectModel.TotalJobCost);

            if (!project.IsProjectPreliminary && !project.IsProjectRTAP)
            {
                project.ProjectLvlTxt = SetProjectLevel(
                        project.AccelaPropertyType,
                        project.AccelaSqrFtOfOverallBuilding,
                        accelaProjectModel.IsHighRise,
                        project.AccelaNumberOfStories,
                        project.ProjectOccupancyTypMapNm);
            }

            //jcl 8-11-21 preliminary obj details 
            project.PrelimBIMProjectDeliveryObjDetails = accelaProjectModel.PrelimBIMProjectDelivery.To_String();
            project.PrelimGeneralInfoObjDetails = accelaProjectModel.PrelimGeneralInfo.To_String();
            project.PrelimMeetingAgendaObjDetails = accelaProjectModel.PrelimMeetingAgenda.To_String();
            project.PrelimMeetingDetailObjDetails = accelaProjectModel.PrelimMeetingDetail.To_String();
            project.PrelimProjectSummaryObjDetails = accelaProjectModel.PrelimProjectSummary.To_String();
            project.PrelimProposedWorkObjDetails = accelaProjectModel.PrelimProposedWork.To_String();
            project.PrelimSystemInfoObjDetails = accelaProjectModel.PrelimSystemInfo.To_String();
            project.PrelimTypeOfWorkObjDetails = accelaProjectModel.PrelimTypeOfWork.To_String();

            project.PrelimBIMProjectDelivery = accelaProjectModel.PrelimBIMProjectDelivery;
            project.PrelimGeneralInfo = accelaProjectModel.PrelimGeneralInfo;
            project.PrelimMeetingAgenda = accelaProjectModel.PrelimMeetingAgenda;
            project.PrelimMeetingDetail = accelaProjectModel.PrelimMeetingDetail;
            project.PrelimProjectSummary = accelaProjectModel.PrelimProjectSummary;
            project.PrelimProposedWork = accelaProjectModel.PrelimProposedWork;
            project.PrelimSystemInfo = accelaProjectModel.PrelimSystemInfo;
            project.PrelimTypeOfWork = accelaProjectModel.PrelimTypeOfWork;

            project.CancellationFee = projectBE.CancellationFee;
            project.IsPaidStatus = accelaProjectModel.IsPaidStatus;
            project.EstimatedFee = accelaProjectModel.EstimatedFee;

            //LES-3407 RTAP
            project.RTAPAffordableUnitChange = accelaProjectModel.DisplayOnlyInformation.RTAPAffordableUnitChange;
            project.RTAPAffordableUnitsRemove = accelaProjectModel.DisplayOnlyInformation.RTAPAffordableUnitsRemove;
            project.RTAPAffordableWorkforceUnitsAdd = accelaProjectModel.DisplayOnlyInformation.RTAPAffordableWorkforceUnitsAdd;
            project.RTAPWorkforceAdd = accelaProjectModel.DisplayOnlyInformation.RTAPWorkforceAdd;
            project.RTAPWorkforceRemove = accelaProjectModel.DisplayOnlyInformation.RTAPWorkforceRemove;
            project.Professionals = ProfessionalDetailBO.ConvertToCSV(accelaProjectModel.DisplayOnlyInformation.Professional, "~");
            project.ProfessionalsList = accelaProjectModel.DisplayOnlyInformation.Professional;
            project.AccountNumber = accelaProjectModel.DisplayOnlyInformation.AccountNumber;
            project.EquipmentCost = accelaProjectModel.DisplayOnlyInformation.EquipmentCost;
            project.PrepaidFeePaymentType = accelaProjectModel.DisplayOnlyInformation.PrepaidFeePaymentType;

            return project;
        }

        private bool DetermineIfFIFO(PropertyTypeEnums propertyType)
        {
            switch (propertyType)
            {
                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                case PropertyTypeEnums.FIFO_Master_Plans:
                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                case PropertyTypeEnums.FIFO_Small_Commercial:
                    return true;
                default:
                    return false;
            }
        }

        public string SetProjectLevel(PropertyTypeEnums propertyType, int? sqrFootage, bool? isHighRise, int? numberOfStories, string occupancyType)
        {
            string level = string.Empty;

            try
            {
                ProjectLevelCalculatorParms parms = new ProjectLevelCalculatorParms()
                {
                    PropertyType = propertyType,
                    SqrFootage = sqrFootage,
                    IsHighRise = isHighRise,
                    NumberOfStories = numberOfStories,
                    OccupancyType = occupancyType
                };

                ProjectLevelCalculatorBO projectLevelCalculator = new ProjectLevelCalculatorBO(parms);
                level = projectLevelCalculator.SetProjectLevel();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in SetProjectLevel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return level;
        }

        private string GetDepartmentDivisionInfoFromAccela(DepartmentTypeEnum departmentType, string departmentDivision, string departmentRegion)
        {
            Department department = new DepartmentModelBO().GetInstance(departmentType, departmentDivision, departmentRegion);
            return department.DepartmentDivision.DepartmentTypeCode;
        }
        protected List<ExcludedPlanReviewersBE> GetExcludedPlanReviewersByProjectId(int projectid)
        {
            return new ExcludedPlanReviewersBO().GetById(projectid);
        }

        private void GetExistingProjectDepartments(Project project, List<AgencyInfo> accelaAgencies, List<TradeInfo> accelaTrades)
        {
            project.Agencies = new List<ProjectAgency>();
            project.Trades = new List<ProjectTrade>();
            _excludedPlanReviewers = GetExcludedPlanReviewersByProjectId(project.ID);
            _projectBRBElist = new ProjectBusinessRelationshipBO().GetListByProjectId(project.ID);

            foreach (ProjectBusinessRelationshipBE be in _projectBRBElist)
            {
                DepartmentTypeEnum departmentType = new DepartmentTypeEnum().CreateInstance(be.BusinessRefId.ToString());
                DepartmentDivisionEnum departmentDivision = new DepartmentDivisionEnum().CreateInstance((int)be.BusinessRefId);

                AgencyInfo accelaAgency = accelaAgencies.Where(x => x.AccelaDepartmentDivisionRef == GetDepartmentDivisionInfoFromAccela(departmentType, departmentDivision.ToString(), x.AccelaDepartmentRegionRef)).FirstOrDefault();
                TradeInfo accelaTrade = accelaTrades.Where(x => x.AccelaDepartmentDivisionRef == GetDepartmentDivisionInfoFromAccela(departmentType, departmentDivision.ToString(), x.AccelaDepartmentRegionRef)).FirstOrDefault();

                switch (departmentType)
                {
                    case DepartmentTypeEnum.Agency:
                        project.Agencies.Add(MapExistingProjectAgencyToBEV2(be, project, accelaAgency));
                        break;
                    case DepartmentTypeEnum.Trade:
                        project.Trades.Add(MapExistingProjectTradeToBEV2(be, project, accelaTrade));
                        break;
                    case DepartmentTypeEnum.NA:
                        break;
                    default:
                        break;
                }
            }
        }

        private ProjectAgency MapExistingProjectAgencyToBEV2(ProjectBusinessRelationshipBE dept, Project project,
            AgencyInfo accelaAgency,
            bool isProjectRTAPorPrelim = false,
            ProjectBusinessRelationshipBE RTAPorPrelimProj = null)
        {
            ProjectStatusEnum projectStatus = project.AIONProjectStatus.ProjectStatusEnum;

            ProjectAgency _projectagency = new ProjectAgency
            {
                CreatedDate = (DateTime)dept.CreatedDate,
                DepartmentInfo = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), dept.BusinessRefId.ToString()),
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = dept.EstimationHoursNbr,
                ExcludedPlanReviewers = ConvertExcludedReviewersBEtoModel(_excludedPlanReviewers.Where(x => x.ProjectBusinessRelationshipId == dept.ProjectBusinessRelationshipId).ToList()),
                ID = (int)dept.ProjectBusinessRelationshipId,
                EstimationNotApplicable = dept.IsEstimationNotApplicable,
                ProjectId = project.ID,
                ProjectStatus = _project.AIONProjectStatus,
                UpdatedDate = (DateTime)dept.UpdatedDate,
                DepartmentStatus = dept.ProjectBusinessRelationshipStatusDesc,
                DepartmentStatusRef = new ProjectStatusModelBO().GetInstance(dept.StatusRefId)
            };
            _projectagency.AssignedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.AssignedPlanReviewerId.HasValue ? dept.AssignedPlanReviewerId.Value : -1);
            _projectagency.PrimaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.PrimaryPlanReviewerId.HasValue ? dept.PrimaryPlanReviewerId.Value : -1);

            if (projectStatus == ProjectStatusEnum.Auto_Estimation_Pending && accelaAgency != null)
            {
                _projectagency.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(accelaAgency.RequestedReviewerName, "lastname,firstname").FirstOrDefault();
            }
            else
            {
                _projectagency.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.ProposedPlanReviewerId.HasValue ? dept.ProposedPlanReviewerId.Value : -1);
            }

            _projectagency.SecondaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.SecondaryPlanReviewerId.HasValue ? dept.SecondaryPlanReviewerId.Value : -1);
            _projectagency.DepartmentDivision = new DepartmentDivisionEnum().CreateInstance((int)dept.BusinessRefId);
            _projectagency.IsDeptRequested = dept.IsDeptRequested;
            return _projectagency;
        }

        private ProjectTrade MapExistingProjectTradeToBEV2(ProjectBusinessRelationshipBE dept, Project project,
            TradeInfo accelaTrade,
            bool isProjectRTAPorPrelim = false,
            ProjectBusinessRelationshipBE RTAPorPrelimProj = null)
        {
            ProjectStatusEnum projectStatus = project.AIONProjectStatus.ProjectStatusEnum;

            ProjectTrade _projecttrade;
            _projecttrade = new ProjectTrade
            {
                CreatedDate = (DateTime)dept.CreatedDate,
                //CreatedUser = dept.CreatedByWkrId,
                DepartmentInfo = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), dept.BusinessRefId.ToString()),
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = dept.EstimationHoursNbr,
                //ExcludedPlanReviewers = ConvertExcludedReviewersBEtoModel(new ExcludedPlanReviewersBO().GetListByProjectDepartmentID(dept.ProjectBusinessRelationshipId.Value)),
                ExcludedPlanReviewers = ConvertExcludedReviewersBEtoModel(_excludedPlanReviewers.Where(x => x.ProjectBusinessRelationshipId == dept.ProjectBusinessRelationshipId).ToList()),
                ID = (int)dept.ProjectBusinessRelationshipId,
                EstimationNotApplicable = dept.IsEstimationNotApplicable,
                ProjectId = project.ID,
                ProjectStatus = _project.AIONProjectStatus,
                UpdatedDate = (DateTime)dept.UpdatedDate,
                //UpdatedUser = dept.UpdatedByWkrId
                DepartmentStatus = dept.ProjectBusinessRelationshipStatusDesc,
                DepartmentStatusRef = new ProjectStatusModelBO().GetInstance(dept.StatusRefId)
            };
            _projecttrade.AssignedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.AssignedPlanReviewerId.HasValue ? dept.AssignedPlanReviewerId.Value : -1);
            _projecttrade.PrimaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.PrimaryPlanReviewerId.HasValue ? dept.PrimaryPlanReviewerId.Value : -1);

            if (projectStatus == ProjectStatusEnum.Auto_Estimation_Pending && accelaTrade != null)
            {
                _projecttrade.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(accelaTrade.RequestedReviewerName, "lastname,firstname").FirstOrDefault();
            }
            else
            {
                _projecttrade.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.ProposedPlanReviewerId.HasValue ? dept.ProposedPlanReviewerId.Value : -1);
            }

            _projecttrade.SecondaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.SecondaryPlanReviewerId.HasValue ? dept.SecondaryPlanReviewerId.Value : -1);
            _projecttrade.DepartmentDivision = new DepartmentDivisionEnum().CreateInstance((int)dept.BusinessRefId);
            _projecttrade.IsDeptRequested = dept.IsDeptRequested;
            return _projecttrade;
        }

        private List<int> ConvertExcludedReviewersBEtoModel(List<ExcludedPlanReviewersBE> data)
        {
            List<int> ret = new List<int>();
            foreach (var item in data)
            {
                ret.Add(item.PlanReviewerId.Value);
            }
            return ret;
        }

        private void CreateListsOfNewDepartments(Project project, AccelaProjectModel accelaProjectModel)
        {

            _projectBRBO = new ProjectBusinessRelationshipBO();
            ProjectBE rtaporprlimproj = null;
            List<ProjectBusinessRelationshipBE> prelimOrRTAPlist = new List<ProjectBusinessRelationshipBE>();
            if (project.IsProjectRTAP)
            {
                rtaporprlimproj = _projectbo.GetByExternalRefInfo(project.AccelaRTAPProjectRefId);
                if (rtaporprlimproj.ProjectId.HasValue == true)
                    prelimOrRTAPlist = _projectBRBO.GetListByProjectId(rtaporprlimproj.ProjectId.Value);
            }
            else if (project.IsPreliminaryMeetingCompleted)
            {
                rtaporprlimproj = _projectbo.GetByExternalRefInfo(project.AccelaPreliminaryProjectRefId);
                if (rtaporprlimproj.ProjectId.HasValue == true)
                    prelimOrRTAPlist = _projectBRBO.GetListByProjectId(rtaporprlimproj.ProjectId.Value);
            }
            else if (!string.IsNullOrWhiteSpace(project.DisplayOnlyInformation.ProjectNumberPrevPrelimReview))
            {
                rtaporprlimproj = _projectbo.GetByExternalRefInfo(project.DisplayOnlyInformation.ProjectNumberPrevPrelimReview);
                if (rtaporprlimproj.ProjectId.HasValue == true)
                    prelimOrRTAPlist = _projectBRBO.GetListByProjectId(rtaporprlimproj.ProjectId.Value);
            }

            InitAgencies(project, accelaProjectModel, prelimOrRTAPlist);
            InitTrades(project, accelaProjectModel, prelimOrRTAPlist);
        }



        private bool InitAgencies(Project project, AccelaProjectModel accelaProjectModel, List<ProjectBusinessRelationshipBE> prelimOrRTAPlist)
        {
            //dept is used to pick respective departments.
            Department dept = null;
            ProjectBusinessRelationshipBE previousProjectDept = null;

            project.AgenciesAccela = new List<AgencyInfo>();
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();
            List<AgencyInfo> incomingagencies = accelaProjectModel.ProjectAgencyList;

            DepartmentRegionEnum deptenum = new DepartmentRegionEnum().CreateInstance(accelaProjectModel.ProjectRegionExternalRef);

            AgencyInfo agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Fire &&
            x.AccelaDepartmentRegionRef == DepartmentRegionExternalRef.NA).FirstOrDefault();

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Fire, deptenum);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Fire).FirstOrDefault();
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Fire, deptenum, agency, previousProjectDept));

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Zoning, deptenum);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Zoning).FirstOrDefault();
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Zoning, deptenum, agency, previousProjectDept));

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Day_Care);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Environmental
            && x.AccelaDepartmentRegionRef == DepartmentRegionExternalRef.Day_Care).FirstOrDefault();
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Day_Care,
                agency, previousProjectDept));

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Food_Service);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Environmental
            && x.AccelaDepartmentRegionRef == DepartmentRegionExternalRef.Food_Service).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Food_Service,
                agency, previousProjectDept));

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Public_Pool);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Environmental
            && x.AccelaDepartmentRegionRef == DepartmentRegionExternalRef.Public_Pool).FirstOrDefault();
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Public_Pool,
                agency, previousProjectDept));

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Facilities_Lodging);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Environmental
            && x.AccelaDepartmentRegionRef == DepartmentRegionExternalRef.Facilities_Lodging).FirstOrDefault();
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Environmental, DepartmentRegionEnum.Facilities_Lodging,
                agency, previousProjectDept));

            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Agency, DepartmentDivisionEnum.Backflow, DepartmentRegionEnum.NA);
            agency = incomingagencies.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Backflow).FirstOrDefault();
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Agencies.Add(new ProjectAgencyModelBO().GetInstance(project, DepartmentDivisionEnum.Backflow, DepartmentRegionEnum.NA,
                agency, previousProjectDept));
            foreach (var item in project.Agencies)
            {
                item.DepartmentStatus = ProjectDisplayStatus.NewApplication;
            }
            return true;
        }

        private bool InitTrades(Project project, AccelaProjectModel accelaProjectModel, List<ProjectBusinessRelationshipBE> prelimOrRTAPlist)
        {
            Department dept = null;
            project.TradesAccela = new List<TradeInfo>();
            ProjectBusinessRelationshipBE previousProjectDept = null;
            List<TradeInfo> incomingtrades = accelaProjectModel.ProjectTradesList;

            TradeInfo trade = incomingtrades.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Electrical).FirstOrDefault();
            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Trade, DepartmentDivisionEnum.Electrical, DepartmentRegionEnum.NA);
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Trades.Add(new ProjectTradeModelBO().GetInstance(project, DepartmentDivisionEnum.Electrical, trade, previousProjectDept));

            trade = incomingtrades.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Building).FirstOrDefault();
            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Trade, DepartmentDivisionEnum.Building, DepartmentRegionEnum.NA);
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Trades.Add(new ProjectTradeModelBO().GetInstance(project, DepartmentDivisionEnum.Building, trade, previousProjectDept));

            trade = incomingtrades.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Mechanical).FirstOrDefault();
            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Trade, DepartmentDivisionEnum.Mechanical, DepartmentRegionEnum.NA);
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Trades.Add(new ProjectTradeModelBO().GetInstance(project, DepartmentDivisionEnum.Mechanical, trade, previousProjectDept));

            trade = incomingtrades.Where(x => x.AccelaDepartmentDivisionRef == DepartmentDivisionExternalRef.Plumbing).FirstOrDefault();
            dept = new DepartmentModelBO().GetInstance(DepartmentTypeEnum.Trade, DepartmentDivisionEnum.Plumbing, DepartmentRegionEnum.NA);
            previousProjectDept = prelimOrRTAPlist.Where(x => x.BusinessRefId == dept.ID).FirstOrDefault();
            project.Trades.Add(new ProjectTradeModelBO().GetInstance(project, DepartmentDivisionEnum.Plumbing, trade, previousProjectDept));
            foreach (var item in project.Trades)
            {
                item.DepartmentStatus = ProjectDisplayStatus.NewApplication;
            }
            return true;
        }

        private int? ConvertZeroToNull(int i)
        {
            if (i == 0)
            {
                return null;
            }
            else
            {
                return i;
            }
        }
        private DateTime? ConvertDefaultDTToNull(DateTime i)
        {
            if (i.Year == 1)
                return null;
            else
                return i;
        }

        /// <summary>
        /// validation for processes in this object
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Insert:
                    //do insert validation
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    //do update validation
                    //for update project id must have a value
                    if (_project.ID == 0)
                    {
                        _errorMsg = "Not a valid AION project id";
                    }
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }
            return true;
        }

        #region AION only
        protected void GetExistingProjectDepartments(ProjectEstimation projectEstimation)
        {
            projectEstimation.Agencies = new List<ProjectAgency>();
            projectEstimation.Trades = new List<ProjectTrade>();
            _excludedPlanReviewers = GetExcludedPlanReviewersByProjectId(projectEstimation.ID);
            _projectBRBElist = new ProjectBusinessRelationshipBO().GetListByProjectId(projectEstimation.ID);

            foreach (ProjectBusinessRelationshipBE be in _projectBRBElist)
            {
                DepartmentTypeEnum departmentType = new DepartmentTypeEnum().CreateInstance(be.BusinessRefId.ToString());

                switch (departmentType)
                {
                    case DepartmentTypeEnum.Agency:
                        projectEstimation.Agencies.Add(MapExistingProjectAgency(be, projectEstimation));
                        break;
                    case DepartmentTypeEnum.Trade:
                        projectEstimation.Trades.Add(MapExistingProjectTrade(be, projectEstimation));
                        break;
                    case DepartmentTypeEnum.NA:
                        break;
                    default:
                        break;
                }
            }
        }

        private ProjectAgency MapExistingProjectAgency(ProjectBusinessRelationshipBE dept, ProjectEstimation projectEstimation)
        {
            DashboardAdapter dashboardAdapter = new DashboardAdapter();

            ProjectStatusEnum projectStatus = projectEstimation.AIONProjectStatus.ProjectStatusEnum;

            ProjectAgency _projectagency = new ProjectAgency
            {
                CreatedDate = (DateTime)dept.CreatedDate,
                DepartmentInfo = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), dept.BusinessRefId.ToString()),
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = dept.EstimationHoursNbr,
                ExcludedPlanReviewers = ConvertExcludedReviewersBEtoModel(_excludedPlanReviewers.Where(x => x.ProjectBusinessRelationshipId == dept.ProjectBusinessRelationshipId).ToList()),
                ID = (int)dept.ProjectBusinessRelationshipId,
                EstimationNotApplicable = dept.IsEstimationNotApplicable,
                ProjectId = projectEstimation.ID,
                ProjectStatus = projectEstimation.AIONProjectStatus,
                UpdatedDate = (DateTime)dept.UpdatedDate,
                DepartmentStatus = dashboardAdapter.SetDeptStatus(dept.ProjectBusinessRelationshipStatusDesc, projectEstimation.AccelaProjectCreatedDate.Value, dept.IsEstimationNotApplicable),
                DepartmentStatusRef = new ProjectStatusModelBO().GetInstance(dept.StatusRefId)
            };

            _projectagency.AssignedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.AssignedPlanReviewerId.HasValue ? dept.AssignedPlanReviewerId.Value : -1);
            _projectagency.PrimaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.PrimaryPlanReviewerId.HasValue ? dept.PrimaryPlanReviewerId.Value : -1);

            _projectagency.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.ProposedPlanReviewerId.HasValue ? dept.ProposedPlanReviewerId.Value : -1);

            _projectagency.SecondaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.SecondaryPlanReviewerId.HasValue ? dept.SecondaryPlanReviewerId.Value : -1);
            _projectagency.DepartmentDivision = new DepartmentDivisionEnum().CreateInstance((int)dept.BusinessRefId);
            _projectagency.IsDeptRequested = dept.IsDeptRequested;
            return _projectagency;
        }

        private ProjectTrade MapExistingProjectTrade(ProjectBusinessRelationshipBE dept, ProjectEstimation projectEstimation)
        {
            DashboardAdapter dashboardAdapter = new DashboardAdapter();

            ProjectStatusEnum projectStatus = projectEstimation.AIONProjectStatus.ProjectStatusEnum;

            ProjectTrade _projecttrade = new ProjectTrade
            {
                CreatedDate = (DateTime)dept.CreatedDate,
                DepartmentInfo = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), dept.BusinessRefId.ToString()),
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = dept.EstimationHoursNbr,
                ExcludedPlanReviewers = ConvertExcludedReviewersBEtoModel(_excludedPlanReviewers.Where(x => x.ProjectBusinessRelationshipId == dept.ProjectBusinessRelationshipId).ToList()),
                ID = (int)dept.ProjectBusinessRelationshipId,
                EstimationNotApplicable = dept.IsEstimationNotApplicable,
                ProjectId = projectEstimation.ID,
                ProjectStatus = projectEstimation.AIONProjectStatus,
                UpdatedDate = (DateTime)dept.UpdatedDate,
                DepartmentStatus = dashboardAdapter.SetDeptStatus(dept.ProjectBusinessRelationshipStatusDesc, projectEstimation.AccelaProjectCreatedDate.Value, dept.IsEstimationNotApplicable),
                DepartmentStatusRef = new ProjectStatusModelBO().GetInstance(dept.StatusRefId)
            };

            _projecttrade.AssignedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.AssignedPlanReviewerId.HasValue ? dept.AssignedPlanReviewerId.Value : -1);
            _projecttrade.PrimaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.PrimaryPlanReviewerId.HasValue ? dept.PrimaryPlanReviewerId.Value : -1);

            _projecttrade.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(dept.ProposedPlanReviewerId.HasValue ? dept.ProposedPlanReviewerId.Value : -1);

            _projecttrade.SecondaryPlanReviewer = new UserIdentityModelBO().GetInstance(dept.SecondaryPlanReviewerId.HasValue ? dept.SecondaryPlanReviewerId.Value : -1);
            _projecttrade.DepartmentDivision = new DepartmentDivisionEnum().CreateInstance((int)dept.BusinessRefId);
            _projecttrade.IsDeptRequested = dept.IsDeptRequested;
            return _projecttrade;
        }
        #endregion
    }
}
