using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using Meck.Logging;
using System;
using System.Reflection;

namespace AION.BL.Adapters
{
    public class ProjectFacilitatorAdapter : BaseManagerAdapter, IProjectFacilitatorAdapter
    {
        private IProjectFacilitatorAdapter _projectAdapter;
        public ProjectFacilitatorAdapter()
        {

        }

        public ProjectFacilitatorAdapter(IProjectFacilitatorAdapter projectAdapter)
        {
            _projectAdapter = projectAdapter;
        }

        public bool GetAssignedFacilitator(ProjectEstimation model)
        {
            if (model.AccelaProjectRefId == null && model.AccelaPreliminaryProjectRefId == null && model.AccelaRTAPProjectRefId == null) return false;

            if (GetFacilitator(model) == false) return false;

            return true;
        }

        public bool GetFacilitator(Project model)
        {
            try
            {
                ProjectBO projectBO = new ProjectBO();
                if (model.IsProjectRTAP == true)
                {
                    if (model.AccelaRTAPProjectRefId != null)
                    {
                        ProjectBE projectBE = projectBO.GetByExternalRefInfo(model.AccelaRTAPProjectRefId);
                        model.AssignedFacilitator = projectBE.AssignedFacilitatorId;
                    }
                }
                else if (model.IsProjectPreliminary == true)
                {
                    if (model.AccelaPreliminaryProjectRefId != null)
                    {
                        ProjectBE projectBE = projectBO.GetByExternalRefInfo(model.AccelaPreliminaryProjectRefId);
                        model.AssignedFacilitator = projectBE.AssignedFacilitatorId == null ? -1 : projectBE.AssignedFacilitatorId;
                    }
                }
                else
                {
                    int assignedFacilitator = projectBO.GetAssignedFacilitator();
                    model.AssignedFacilitator = assignedFacilitator;
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetFacilitator - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// This updates the facilitator by project id
        /// Required: project.ID, project.UpdatedUser.ID, project.AssignedFacilitator.Value, current UpdatedDate
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool UpdateAssignedFacilitator(Project project)
        {
            try
            {
                ProjectBO projectBO = new ProjectBO();

                ProjectBE projectBE = projectBO.GetById(project.ID);
                projectBE.UserId = project.UpdatedUser.ID.ToString();
                projectBE.AssignedFacilitatorId = project.AssignedFacilitator.Value;
     
                int rows = projectBO.UpdateProjectFacilitator(projectBE);
                if (rows > 0)
                {
                    //audit the change
                    string facilitatorname = string.Empty;
                    UserIdentity facilitator = new UserIdentityModelBO().GetInstance(project.AssignedFacilitator.Value);
                    facilitatorname += facilitator.FirstName + " " + facilitator.LastName;
                    new ProjectAuditModelBO().InsertProjectAudit(project.ID, facilitatorname, project.UpdatedUser.ID.ToString(), AuditActionEnum.Facilitator_Assigned);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error UpdateAssignedFacilitator - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
