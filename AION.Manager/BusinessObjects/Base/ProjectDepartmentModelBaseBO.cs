using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class ProjectDepartmentModelBaseBO : ModelBaseModelBO
    {
        private enum ActionType { GetInstance, GetInstanceByEnum, GetDataSet, Delete, GetById, GetList, Update };


        private ProjectBusinessRelationshipBO _projectBRBO;
        private ProjectBusinessRelationshipBE _projectBRBE;
        private ProjectDepartment _projectdepartment;
        private UserIdentity _naUserIdentity;
        private int _rowcount;

        protected ProjectDepartment InjectBaseObjects(ProjectDepartment inheritedObject,
        Project projectInfo, DepartmentTypeEnum departmentType, DepartmentDivisionEnum division, DepartmentRegionEnum region, Meck.Shared.MeckDataMapping.DepartmentInfo deptInfo, ProjectBusinessRelationshipBE previousProjectDept)
        {
            _naUserIdentity = new UserIdentityModelBO().GetInstance(-1);
            ProjectDepartment ret = MapProjectDepartment(inheritedObject, projectInfo, departmentType, division, region, deptInfo, previousProjectDept);
            return ret;
        }

        /// <summary>
        /// updates
        /// </summary>
        /// <param name="projectDepartment"></param>
        /// <returns>number of rows updated</returns>
        public int UpdateProjectDepartment(ProjectDepartment projectDepartment)
        {
            try
            {
                _projectBRBO = new ProjectBusinessRelationshipBO();
                MapPrjctDptmntToBusinessRefBE(projectDepartment);
                _rowcount = _projectBRBO.Update(_projectBRBE);
                return _rowcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Map returned objects to ProjectDepartment
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="projectInfo"></param>
        /// <param name="departmentType"></param>
        /// <param name="division"></param>
        /// <param name="region"></param>
        /// <returns>ProjectDepartment</returns>
        private ProjectDepartment MapProjectDepartment(ProjectDepartment ret, Project projectInfo, DepartmentTypeEnum departmentType,
            DepartmentDivisionEnum division, DepartmentRegionEnum region, Meck.Shared.MeckDataMapping.DepartmentInfo accelaDeptInfo = null, ProjectBusinessRelationshipBE previousProjectDept = null)
        {
            ret.DepartmentDivision = division;
            ret.DepartmentTypeEnum = departmentType;
            ret.ProjectId = projectInfo.ID;
            ret.ProjectStatus = projectInfo.AIONProjectStatus;
            ret.DepartmentInfo = new DepartmentModelBO().GetInstance(departmentType, division, region).DepartmentEnum;
            //=======================================
            ret.EstimationHours = 0.00M;
            ret.DepartmentStatusRef = new ProjectStatusModelBO().GetInstance(-1);
            //=========================================
            //incase the project dept info is not part of application and project is not RTAP or prelim then create a new instance.
            if (accelaDeptInfo == null && previousProjectDept == null)
            {
                ret.AssignedPlanReviewer = _naUserIdentity;
                ret.ExcludedPlanReviewers = new List<int>();
                ret.PrimaryPlanReviewer = _naUserIdentity;
                ret.ProposedPlanReviewer = _naUserIdentity;
                ret.SecondaryPlanReviewer = _naUserIdentity;
            }
            else
            {
                //if project is rtap then pick from what is there in last project.
                if ((projectInfo.IsProjectRTAP == true || projectInfo.IsPreliminaryMeetingCompleted == true) && previousProjectDept != null)
                {
                    ret.AssignedPlanReviewer = new UserIdentityModelBO().GetInstance(previousProjectDept.AssignedPlanReviewerId.HasValue ? previousProjectDept.AssignedPlanReviewerId.Value : -1);
                    ret.PrimaryPlanReviewer = new UserIdentityModelBO().GetInstance(previousProjectDept.PrimaryPlanReviewerId.HasValue ? previousProjectDept.PrimaryPlanReviewerId.Value : -1);
                    ret.SecondaryPlanReviewer = new UserIdentityModelBO().GetInstance(previousProjectDept.SecondaryPlanReviewerId.HasValue ? previousProjectDept.SecondaryPlanReviewerId.Value : -1);
                    ret.ExcludedPlanReviewers = new List<int>();
                }
                //if this had a previous preliminary review
                else if (!string.IsNullOrWhiteSpace(projectInfo.DisplayOnlyInformation.ProjectNumberPrevPrelimReview))
                {
                    if (previousProjectDept != null)
                    {
                        ret.PrimaryPlanReviewer = new UserIdentityModelBO().GetInstance(previousProjectDept.AssignedPlanReviewerId.HasValue ? previousProjectDept.AssignedPlanReviewerId.Value : -1);

                    }
                    else
                    {
                        ret.PrimaryPlanReviewer = _naUserIdentity;

                    }
                    ret.AssignedPlanReviewer = _naUserIdentity;
                    ret.ExcludedPlanReviewers = new List<int>();
                    ret.SecondaryPlanReviewer = _naUserIdentity;
                }
                else
                {
                    ret.AssignedPlanReviewer = _naUserIdentity;
                    ret.ExcludedPlanReviewers = new List<int>();
                    ret.PrimaryPlanReviewer = _naUserIdentity;
                    ret.SecondaryPlanReviewer = _naUserIdentity;
                }
                //reagrdless of the project type is RTAP or prelim the customer suggested plan reviewer must be displayed.
                if (accelaDeptInfo != null && string.IsNullOrEmpty(accelaDeptInfo.RequestedReviewerName) == false)
                {
                    ret.ProposedPlanReviewer = new UserIdentityModelBO().GetInstance(accelaDeptInfo.RequestedReviewerName, "lastname,firstname").FirstOrDefault();
                    if (ret.ProposedPlanReviewer == null) ret.ProposedPlanReviewer = _naUserIdentity;
                }
                else
                    ret.ProposedPlanReviewer = _naUserIdentity;
            }
            if (accelaDeptInfo != null)
                ret.IsDeptRequested = accelaDeptInfo.IsDeptRequested;
            //==============================================
            ret.CreatedDate = new DateTime();
            ret.CreatedUser = new UserIdentity();
            ret.UpdatedDate = new DateTime();
            ret.UpdatedUser = new UserIdentity();
            return ret;
        }

        private ProjectBusinessRelationshipBE MapPrjctDptmntToBusinessRefBE(ProjectDepartment dept)
        {
            _projectdepartment = dept;
            _projectBRBE = new ProjectBusinessRelationshipBE
            {
                ProjectBusinessRelationshipId = dept.ID,
                BusinessRefId = new DepartmentModelBO().GetInstance(dept.DepartmentInfo).ID,
                EstimationHoursNbr = dept.EstimationHours,
                ProjectId = dept.ProjectId,
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
                UserId = dept.UpdatedUser.ID.ToString()
            };
            return _projectBRBE;
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

    }
}
