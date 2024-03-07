using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;

namespace AION.BL.Controller
{

	public class ProjectAutoEstimationEngine : IProjectAutoEstimationEngine
    {
        /* These should be the one being used but for IDE debugging intellisense putting objects.*/
        //IEstimationAccelaAdapter _api;
        //IEstimationCRUDAdapter _crud;
        //IProjectEstimationAdapter _estimation;

        EstimationAccelaAdapter _api = new EstimationAccelaAdapter();
        EstimationCRUDAdapter _crud = new EstimationCRUDAdapter();
        ProjectEstimationAdapter _estimation = new ProjectEstimationAdapter();
        ProjectFacilitatorAdapter _facilitator = new ProjectFacilitatorAdapter();

        public ProjectAutoEstimationEngine()
        {

        }

        public ProjectAutoEstimationEngine(IEstimationAccelaAdapter api, IEstimationCRUDAdapter crud, IProjectEstimationAdapter estimation)
        {
            /* These should be the one being used but for IDE debugging intellisense navigation to functions, putting the actual objects.*/
            //_api = api;
            //_crud = crud;
            //_estimation = estimation;

            _api = (EstimationAccelaAdapter)api;
            _crud = (EstimationCRUDAdapter)crud;
            _estimation = (ProjectEstimationAdapter)estimation;
        }

        public ProjectEstimation ExecuteProjectEstimation(ProjectParms projInfo, bool forceAutoCalc = false)
        {
            //get details from AION which is required for estimation.

            ProjectEstimation aionProjectModel = _crud.GetProjectDetailsByProjectSrcSourceTxt(projInfo.ProjectId);

            /* This object will always be non null. the way to check for estimation is
             * done previusly for this account is to check there is a project record in database. So do that now.
             * If the record  doesn't exists then do the auto calc and then save the record.
             * Also perform the auto calc even if the record is returned if it is specifed to force to autot calc again.
             * in this case the records will be updated instead of add new. */

            if (aionProjectModel.ID == 0 || forceAutoCalc == true || aionProjectModel.AIONProjectStatus.ProjectStatusEnum == ProjectStatusEnum.NA)
            {
                //get system user
                aionProjectModel.UpdatedUser = new UserIdentityModelBO().GetInstance(1);
                //perform the auto estimation
                if (_estimation.PerformAutoEstimation(aionProjectModel) == false) return null;

                //Get Auto assigned Facilitator
                bool projectGetsAutoAssignedFacilitator = new ProjectTypeAdapter().CheckConfigurationForAutoAssignedFacilitator(aionProjectModel.AccelaPropertyType);
                if (projectGetsAutoAssignedFacilitator)
                {
                    if (_facilitator.GetAssignedFacilitator(aionProjectModel) == false) return null;
                }

                //change project status

                bool isEstimationComplete = _estimation.CheckIfAllDepartmentsComplete(aionProjectModel.ID);

                ProjectStatus projectstatus = new ProjectStatus();

                if (isEstimationComplete)
                {
                    projectstatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Not_Scheduled);
                }
                else
                {
                    projectstatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Ready_For_Estimator);
                }

                aionProjectModel.AIONProjectStatus = projectstatus;

                //saves the details to datbase.
                if (_crud.SaveProjectEstimationDetails(aionProjectModel) == false) return null;
            }
            return aionProjectModel;
        }

        public bool CalculateAverageEstimationHoursFactors()
        {
            IDataContextLegacyProjectDataBO dataContextLegacyProjectData = new LegacyProjectEstimationHoursRefBO();

            IDataContextAutoEstimationRefBO dataContextAutoEstimation = new AutoEstimationRefBO();
            
            IDataContextProjectBusinessRelationshipBO dataContextProjectBusinessRelationship = new ProjectBusinessRelationshipBO();

            IDataContextAverageEstimationHoursFactorBO dataContextAverageEstimationHoursFactorBO = new AverageEstimationHoursFactorBO();

            IDataContextProjectBO dataContextProjectBO = new ProjectBO();

            ICalcAvgEstimationFactorAdapter thisadapter =
                new CalcAvgEstimationFactorAdapter(
                dataContextLegacyProjectData,    
                dataContextAutoEstimation,
                dataContextProjectBusinessRelationship,
                dataContextAverageEstimationHoursFactorBO,
                dataContextProjectBO);

            var success = thisadapter.ProcessData();
            return success;
        }
    }
}
