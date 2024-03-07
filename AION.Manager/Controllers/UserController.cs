using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AION.Manager.Models.User;
using AION.Web.BusinessEntities;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Manager.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// GetUserIdentityByEmailExtSystem
        /// </summary>
        /// <param name="email"> string </param>
        /// <param name="externalSystemEnumID"> int</param>
        /// <returns>UserIdentity  model</returns>
        [HttpGet]
        [ResponseType(typeof(UserIdentity))]
        [Route("api/User/GetUserIdentityByEmailExtSystem")]
        public IHttpActionResult GetUserIdentityByEmailExtSystem(string email, int externalSystemEnumID = (int)ExternalSystemEnum.AION)
        {
            IUserAdapter thisengine = new UserAdapter();

            // UserIdentity  return
            var result = thisengine.GetUserIdentityByEmailWPermissions(email, externalSystemEnumID);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(UserIdentity))]
        [Route("api/User/GetUserIdentityByUserName")]
        public IHttpActionResult GetUserIdentityByUserName(string userName)
        {
            IUserAdapter thisengine = new UserAdapter();

            // UserIdentity  return
            var result = thisengine.GetUserIdentityByUserName(userName);

            return Ok(result);
        }

        /// <summary>
        ///  GetUserIdentityByID
        /// </summary>
        /// <param name="ID"> integer Id</param>
        /// <returns>UserIdentiy model</returns>
        [HttpGet]
        [ResponseType(typeof(UserIdentity))]
        [Route("api/User/GetUserIdentityByID")]
        public IHttpActionResult GetUserIdentityByID(int ID)
        {
            UserAdapter thisengine = new UserAdapter();

            // UserIdentity return

            var result = thisengine.GetUserIdentityByID(ID);

            return Ok(result);
        }

        /// <summary>
        /// GetPermissionMapByUserID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>PermissionMapping</returns>
        [HttpGet]
        [ResponseType(typeof(PermissionMapping))]
        [Route("api/User/GetPermissionMapByUserID")]
        public IHttpActionResult GetPermissionMapByUserID(int ID)
        {
            IPermissionAdapter thisengine = new PermissionAdapter();
            PermissionMapping pm = thisengine.GetPermissionMappingByUserId(ID);

            var result = pm;

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/GetUserIdentityByUserBE")]
        public IHttpActionResult GetUserIdentityByUserBE(UserIdentity item)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.GetUserIdentityByUserBE(item);

            return Ok(result);
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/UpdateUser")]
        public IHttpActionResult UpdateUser(UserIdentity item)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.UpdateUser(item);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/User/CreateUser")]
        public IHttpActionResult CreateUser(UserIdentity item)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.CreateUser(item);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/UpdateUserProjectTypeXref")]
        public IHttpActionResult UpdateUserProjectTypeXref(UserProjectTypeXref item)
        {
            UserAdapter thisengine = new UserAdapter();
            var result = thisengine.UpdateUserProjectTypeXref(item);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(UserProjectTypeXref))]
        [Route("api/User/GetSelectedProjectTypeXrefList")]
        public IHttpActionResult GetSelectedProjectTypeXrefList(int UserID)
        {
            UserAdapter thisengine = new UserAdapter();
            var result = thisengine.GetSelectedProjectTypesByUser(UserID);
            UserProjectTypeXref ret = new UserProjectTypeXref();
            ret.UserID = UserID;
            ret.ProjectTypeIDList = new List<int>();
            if (result != null && result.Count > 0)
            {
                ret.TimeStamp = result[0].CreatedDate.Value;
                ret.UpdatedUserId = int.Parse(result[0].UpdatedByWkrId);
                foreach (var item in result)
                {
                    ret.ProjectTypeIDList.Add(item.ProjectTypeRefID.Value);
                }
            }
            return Ok(ret);
        }



        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/UpdateUserDepartmentXref")]
        public IHttpActionResult UpdateUserDepartmentXref(UserDepartmentXref item)
        {
            UserAdapter thisengine = new UserAdapter();
            var result = thisengine.UpdateUserDepartmentXref(item);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(UserDepartmentXref))]
        [Route("api/User/GetSelectedDepartmentXrefList")]
        public IHttpActionResult GetSelectedDepartmentXrefList(int UserID)
        {
            UserAdapter thisengine = new UserAdapter();
            var result = thisengine.GetUserBusinessRelationshipByUser(UserID);
            UserDepartmentXref ret = new UserDepartmentXref();
            ret.UserID = UserID;
            ret.UserDepartmentIDList = new List<int>();
            if (result != null && result.Count > 0)
            {
                ret.TimeStamp = result[0].CreatedDate.Value;
                ret.UpdatedUserId = int.Parse(result[0].UpdatedByWkrId);
                foreach (var item in result)
                {
                    ret.UserDepartmentIDList.Add(item.BusinessRefId.Value);
                }
            }
            return Ok(ret);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/UpdateUserIsSchedulable")]
        public IHttpActionResult UpdateUserIsSchedulable(UserIdentity item)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.UpdateUserIdentityIsSchedulable(item.ID, item.IsSchedulable);

            return Ok(result);
        }

        //bool CreateRoleMappings(int userID, List<int> roleMappings)
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/CreateRoleMappings")]
        public IHttpActionResult CreateRoleMappings(RoleMappingManagerModel item)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.CreateRoleMappings(item.UserId, item.RoleMappings);

            return Ok(result);
        }

        //bool DeleteRoleMappings(int userID, List<int> roleMappings)
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/DeleteRoleMappings")]
        public IHttpActionResult DeleteRoleMappings(RoleMappingManagerModel item)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.DeleteRoleMappings(item.UserId, item.RoleMappings);

            return Ok(result);
        }


        //public List<SystemRole> GetSystemRolesByUserId(int userID)
        [HttpGet]
        [ResponseType(typeof(List<SystemRole>))]
        [Route("api/User/GetSystemRolesByUserId")]
        public IHttpActionResult GetSystemRolesByUserId(int userid)
        {
            IUserAdapter thisengine = new UserAdapter();
            var result = thisengine.GetSystemRolesByUserId(userid);
            return Ok(result);
        }



        [HttpGet]
        [ResponseType(typeof(List<Department>))]
        [Route("api/User/GetDesignatedDepartmentsByUserId")]
        public IHttpActionResult GetDesignatedDepartmentsByUserId(int userid)
        {
            IUserAdapter thisengine = new UserAdapter();
            var result = thisengine.GetAllDepartmentsByUserIdWSOI(userid);
            return Ok(result);
        }

        //public List<SystemRole> GetDesignatedDepartmentsByUserId(int userID)
        [HttpGet]
        [ResponseType(typeof(Department))]
        [Route("api/User/GetGetDepartmentByEnum")]
        public IHttpActionResult GetGetDepartmentByEnum(int deptEnumID)
        {

            IUserAdapter thisengine = new UserAdapter();
            var result = thisengine.GetDepartmentByEnum((DepartmentNameEnums)deptEnumID);
            return Ok(result);
        }


        /// <summary>

        /// </summary>
        /// <returns>strong of List<OccupancySquareFootage> </returns>
        [HttpGet]
        [ResponseType(typeof(List<OccupancySquareFootage>))]
        [Route("api/User/GetSquareFootageList")]
        public IHttpActionResult GetSquareFootageList()
        {
            IUserAdapter thisengine = new UserAdapter();

            var mAllSqFtList = thisengine.GetAllSquareFootageList();

            return Ok(mAllSqFtList);

        }

        /// <summary>
        /// Used by User Admin to get user's occupancy and square footage for reviewer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<UserMgmtOccupancy>))]
        [Route("api/User/GetSquareFootageListbyUserOccupancy")]
        public IHttpActionResult GetSquareFootageListbyUserOccupancy(int userId)
        {
            UserAdapter thisengine = new UserAdapter();

            var mUserMgmtOccu = thisengine.GetSquareFootageListbyUserOccupancy(userId);

            return Ok(mUserMgmtOccu);

        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/CreateOccupancy")]
        public IHttpActionResult CreateOccupancy(List<OccupancyOutput> occupancy)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.CreateOccupancy(occupancy);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/DeleteOccupancy")]
        public IHttpActionResult DeleteOccupancy(int userId)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.DeleteOccupancy(userId);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<UserIdentity>))]
        [Route("api/User/Search")]
        public IHttpActionResult Search(string firstname, string lastname)
        {
            IUserAdapter thisengine = new UserAdapter();

            var result = thisengine.Search(firstname, lastname);

            return Ok(result);
        }

        /// <summary>
        /// Get Users that have Manager perms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<UserIdentity>))]
        [Route("api/User/GetAllManagers")]
        public IHttpActionResult GetAllManagers()
        {
            IUserAdapter thisAdapter = new UserAdapter();
            var mManagers = thisAdapter.GetAllManagers();

            return Ok(mManagers);
        }

        /// <summary>
        /// Get Users that have Estimation perms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<EstimatorUIModel>))]
        [Route("api/User/GetAllEstimators")]
        public IHttpActionResult GetAllEstimators()
        {
            IUserAdapter thisAdapter = new UserAdapter();
            var mEstimators = thisAdapter.GetAllEstimators();

            return Ok(mEstimators);
        }

        /// <summary>
        ///  Get Users that have Facilitator perms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Facilitator>))]
        [Route("api/User/GetAllFacilitators")]
        public IHttpActionResult GetAllFacilitators()
        {
            IUserAdapter thisAdapter = new UserAdapter();
            var mFacilitators = thisAdapter.GetAllFacilitators();

            return Ok(mFacilitators);
        }

        /// <summary>
        /// Get Users that have Reviewer perms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Reviewer>))]
        [Route("api/User/GetAllReviewers")]
        public IHttpActionResult GetAllReviewers(bool isExpressSched = false, bool isSchedulable = true)
        {
            IUserAdapter thisAdapter = new UserAdapter();
            var mReviewers = thisAdapter.GetAllReviewers(isExpressSched, isSchedulable);

            return Ok(mReviewers);
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/User/SaveUserJurisdiction")]
        public IHttpActionResult SaveUserJurisdiction(UserJurisdictionSaveModel item)
        {
            UserAdapter thisengine = new UserAdapter();
            var result = thisengine.SaveUserJurisdiction(item);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [Route("api/User/GetJurisdictionListByUser")]
        public IHttpActionResult GetJurisdictionListByUser(int id)
        {
            IUserAdapter thisAdapter = new UserAdapter();
            var mReviewers = thisAdapter.GetJurisdictionListByUser(id);

            return Ok(mReviewers);
        }

        [HttpGet]
        [ResponseType(typeof(List<Reviewer>))]
        [Route("api/User/GetReviewers")]
        public IHttpActionResult GetReviewers(int propertyTypeEnum, int deptNameEnum, bool isExpressSchedulable = false)
        {
            IUserAdapter thisAdapter = new UserAdapter();
            var mReviewers = thisAdapter.GetReviewers(propertyTypeEnum, deptNameEnum, isExpressSchedulable);

            return Ok(mReviewers);
        }
    }
}