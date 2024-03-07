using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class EstimationCRUDController : BaseApiController
    {
        public EstimationCRUDController()
        {
        }

        [HttpPost]
        [ResponseType(typeof(ProjectDetailModel))]
        [Route("api/EstimationCRUD/GetProjectDetailModel")]
        public IHttpActionResult GetProjectDetailModel(ProjectParms projectParms)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            var result = thisAdapter.GetProjectDetailModel(projectParms);

            return Ok(result);
        }

        /// <summary>
        /// Gets project details from Accela 
        /// </summary>
        /// <param name="accelaModelProjectId"></param>
        /// <param name="recidtxt"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ProjectEstimation))]
        [Route("api/EstimationCRUD/GetProjectDetailsForEstimationByAccelaId")]
        public IHttpActionResult GetProjectDetailsForEstimationByAccelaId(string accelaModelProjectId, string recidtxt)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            var mProjectEstimation = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(accelaModelProjectId);

            return Ok(mProjectEstimation);
        }


        [HttpGet]
        [ResponseType(typeof(ProjectEstimation))]
        [Route("api/EstimationCRUD/GetProjectDetailsByProjectSrcSourceTxt")]
        public IHttpActionResult GetProjectDetailsByProjectSrcSourceTxt(string accelaModelProjectId)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            var mProjectEstimation = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(accelaModelProjectId);

            return Ok(mProjectEstimation);
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/EstimationCRUD/SaveProjectEstimationDetails")]
        public IHttpActionResult SaveProjectEstimationDetails(ProjectEstimation model)
        {
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            return Ok(thisAdapter.SaveProjectEstimationDetails(model));
        }

        /// <summary>
        /// Used to save Estimation from EstimationMain
        /// Uses SaveTypeEnum to send express emails
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/EstimationCRUD/SaveEstimation")]
        public IHttpActionResult SaveEstimation(ProjectEstimation model)
        {
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            return Ok(thisAdapter.SaveEstimation(model));
        }

        [HttpGet]
        [ResponseType(typeof(List<Facilitator>))]
        [Route("api/EstimationCRUD/GetFacilitatorWorkloadSummary")]
        public IHttpActionResult GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            var mFacilatators = thisAdapter.GetFacilitatorWorkloadSummary(startdate, enddate);

            return Ok(mFacilatators);

        }

        /// <summary>
        /// GetAllPendingReasons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<CatalogItem>))]
        [Route("api/EstimationCRUD/GetAllPendingReasons")]
        public IHttpActionResult GetAllPendingReasons()
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            var mCatalogItems = thisAdapter.GetAllPendingReasons();

            return Ok(mCatalogItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectTrade"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/EstimationCRUD/SaveProjectTrade")]
        public IHttpActionResult SaveProjectTrade(ProjectTrade projectTrade)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            return Ok(thisAdapter.SaveProjectTrade(projectTrade));
        }

        /// <summary>
        /// Save ProjectAgency
        /// </summary>
        /// <param name="projectAgency"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/EstimationCRUD/SaveProjectAgency")]
        public IHttpActionResult SaveProjectAgency(ProjectAgency projectAgency)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            return Ok(thisAdapter.SaveProjectAgency(projectAgency));
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectAudit>))]
        [Route("api/EstimationCRUD/GetProjectAudits")]
        public IHttpActionResult GetProjectAudits(int projectid)
        {
            IProjectAuditAdapter thisAdapter = new ProjectAuditAdapter();
            var result = thisAdapter.GetProjectAudits(projectid);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectStatus>))]
        [Route("api/EstimationCRUD/GetProjectStatusBaseList")]
        public IHttpActionResult GetProjectStatusBaseList()
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            var result = thisAdapter.GetProjectStatusBaseList();

            return Ok(result);
        }



        [HttpGet]
        [ResponseType(typeof(ProjectEstimation))]
        [Route("api/EstimationCRUD/GetProjectDetailsByProjectId")]
        public IHttpActionResult GetProjectDetailsByProjectId(int projectId)
        {
            IEstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            var result = thisAdapter.GetProjectDetailsByProjectId(projectId);

            return Ok(result);
        }
    }
}