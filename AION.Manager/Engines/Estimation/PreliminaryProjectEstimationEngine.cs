using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using Meck.Shared;

namespace AION.BL.Controller
{
    public class PreliminaryProjectEstimationEngine : IPreliminaryProjectEstimationEngine
    {
        EstimationAccelaAdapter _api = new EstimationAccelaAdapter();
        EstimationCRUDAdapter _crud = new EstimationCRUDAdapter();
        ProjectEstimationAdapter _estimation = new ProjectEstimationAdapter();
        ProjectFacilitatorAdapter _facilitator = new ProjectFacilitatorAdapter();
        public PreliminaryProjectEstimationEngine()
        {

        }

        public PreliminaryProjectEstimationEngine(IEstimationAccelaAdapter api, IEstimationCRUDAdapter crud, IProjectEstimationAdapter estimation)
        {
            /* These should be the one being used but for IDE debugging intellisense navigation to functions, putting the actual objects.*/
            //_api = api;
            //_crud = crud;
            //_estimation = estimation;

            _api = (EstimationAccelaAdapter)api;
            _crud = (EstimationCRUDAdapter)crud;
            _estimation = (ProjectEstimationAdapter)estimation;
        }

        public ProjectEstimation ExecutePreliminaryProjectEstimation(ProjectParms projInfo)
        {
            //get details from AION which is required for estimation.
            ProjectEstimation aionProjectModel = _crud.GetProjectDetailsByProjectSrcSourceTxt(projInfo.ProjectId);

            //if project is a new one then set project status
            if (aionProjectModel.ID == 0)
            {
                //get system user
                aionProjectModel.UpdatedUser = new UserIdentityModelBO().GetInstance(1);

                ProjectStatus projectstatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Ready_For_Estimator);

                foreach (var item in aionProjectModel.Trades)
                {
                    item.DepartmentStatus = ProjectDisplayStatus.None;
                }
                foreach (var item in aionProjectModel.Agencies)
                {
                    item.DepartmentStatus = ProjectDisplayStatus.None;
                }
                aionProjectModel.AIONProjectStatus = projectstatus;

                //saves the details to datbase.
                if (_crud.SaveProjectEstimationDetails(aionProjectModel) == false) return null;
            }
            return aionProjectModel;
        }
    }

    public interface IPreliminaryProjectEstimationEngine
    {
        ProjectEstimation ExecutePreliminaryProjectEstimation(ProjectParms projInfo);
    }
}
