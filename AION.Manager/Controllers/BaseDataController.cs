using AION.Base;
using AION.BL;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class BaseDataController : BaseApiController
    {
        // GET: User
        public BaseDataController()
        {

        }

        /// <summary>
        /// GetUserIdentityByEmailExtSystem
        /// </summary>
        /// <param name="email"> string </param>
        /// <param name="externalSystemEnumID"> int</param>
        /// <returns>UserIdentity  model</returns>
        [HttpGet]
        [ResponseType(typeof(List<ExternalSystemRefBE>))]
        [ActionName("GetAllExternalSystemRefBOList")]
        [Route("api/BaseData/GetAllExternalSystemRefBOList")]
        public IHttpActionResult GetAllExternalSystemRefBOList()
        {
            List<ExternalSystem> ret = new List<ExternalSystem>();
            ExternalSystemRefBO bo = new ExternalSystemRefBO();
            List<ExternalSystemRefBE> be = bo.GetAllList();

            return Ok(be);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<SystemRole>))]
        [ActionName("GetSystemRoleBaseList")]
        [Route("api/BaseData/GetSystemRoleBaseList")]
        public IHttpActionResult GetSystemRoleBaseList()
        {

            ExternalSystemRefBO bo = new ExternalSystemRefBO();
            List<ExternalSystemRefBE> be = bo.GetAllList();

            return Ok(be);
        }
    }
}