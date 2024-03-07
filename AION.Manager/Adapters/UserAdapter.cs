using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AION.Manager.Common;
using AION.Manager.Models;
using AION.Manager.Models.User;
using AION.Scheduler.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.BL.Adapters
{
    public class UserAdapter : BaseManagerAdapter, IUserAdapter
    {
        public UserIdentity GetUserIdentityByEmailWPermissions(string email, int externalSystemEnumID)
        {
            UserIdentity userIdentity = new UserIdentityModelBO().GetInstance(email, (ExternalSystemEnum)externalSystemEnumID);
            userIdentity.PermissionMapping = new PermissionAdapter().GetPermissionMappingByUserId(userIdentity.ID);
            return userIdentity;
        }
        public UserIdentity GetUserIdentityByEmailExtSystem(string email, int externalSystemEnumID)
        {
            return new UserIdentityModelBO().GetInstance(email, (ExternalSystemEnum)externalSystemEnumID);
        }

        public UserIdentity GetUserIdentityByUserName(string userName)
        {
            return new UserIdentityModelBO().GetInstanceByUserName(userName);
        }

        public UserIdentity GetUserIdentityByID(int ID)
        {
            return new UserIdentityModelBO().GetInstance(ID);
        }


        public bool GetUserIdentityByUserBE(UserIdentity userbe)
        {
            try
            {
                return new UserIdentityModelBO().GetUserIdentityByUserBE(userbe);

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error GetUserIdentityByUserBE - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool UpdateUserIdentityIsExpressSched(int ID, bool isExpressSched)
        {
            try
            {
                return new UserIdentityModelBO().UpdateExpressSchedulable(ID, isExpressSched);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserIdentityIsExpressSched - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public int CreateUser(UserIdentity user)
        {
            try
            {
                UserOutlookDetail userOutlookDetail = GetOutlookDataForUser(user);
                user.UserPrincipalName = userOutlookDetail.UserPrincipalName;
                user.CalendarId = userOutlookDetail.CalendarId;

                return new UserIdentityModelBO().CreateUser(user);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error CreateUser - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }


        public bool UpdateUser(UserIdentity user)
        {
            try
            {
                UserOutlookDetail userOutlookDetail = GetOutlookDataForUser(user);
                user.UserPrincipalName = userOutlookDetail.UserPrincipalName;
                user.CalendarId = userOutlookDetail.CalendarId;

                return new UserIdentityModelBO().UpdateUser(user);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUser - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool UpdateUserProjectTypeXref(UserProjectTypeXref item)
        {
            try
            {
                UserProjectTypeRefBO bo = new UserProjectTypeRefBO();
                bo.DeleteAllByUser(item.UserID);
                foreach (var val in item.ProjectTypeIDList)
                {
                    UserProjectTypeRefBE data = new UserProjectTypeRefBE();
                    data.UserID = item.UserID;
                    data.ProjectTypeRefID = val;
                    data.CreatedByWkrId = item.UserID.ToString();
                    data.UpdatedByWkrId = item.UserID.ToString();
                    data.CreatedDate = item.TimeStamp;
                    data.UpdatedDate = item.TimeStamp;
                    bo.Create(data);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserProjectTypeRef - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public List<UserProjectTypeRefBE> GetSelectedProjectTypesByUser(int userID)
        {
            try
            {
                UserProjectTypeRefBO bo = new UserProjectTypeRefBO();
                var ret = bo.GetListByUserID(userID);
                return ret;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserProjectTypeRef - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public List<UserBusinessRelationshipBE> GetUserBusinessRelationshipByUser(int userID)
        {
            try
            {
                UserBusinessRelationshipBO bo = new UserBusinessRelationshipBO();
                var ret = bo.GetAllListByUserID(userID);
                return ret;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserProjectTypeRef - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool UpdateUserIdentityIsSchedulable(int ID, bool isSchedulable)
        {
            try
            {
                return new UserIdentityModelBO().UpdateIsSchedulable(ID, isSchedulable);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserIdentityIsSchedulable - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public object UpdateUserDepartmentXref(UserDepartmentXref item)
        {
            try
            {
                UserBusinessRelationshipBO bo = new UserBusinessRelationshipBO();
                //Check if user was added to a Trade/Agency
                List<UserBusinessRelationshipBE> GetAllListByUserID = bo.GetAllListByUserID(item.UserID);
                int ubrId = 0;

                bo.DeleteAllByUser(item.UserID);
                foreach (int businessRefId in item.UserDepartmentIDList)
                {
                    bool? processNpaInd = GetAllListByUserID.Where(x => x.BusinessRefId == businessRefId).Select(x => x.ProcessNpaInd).FirstOrDefault();

                    bool isNewlyAdded = GetAllListByUserID.Where(x => x.BusinessRefId == businessRefId).Any() == false;
                    //if this one is already processnpaind = true, then use that value, don't change it
                    if (processNpaInd.HasValue && processNpaInd == true)
                    {
                        isNewlyAdded = true;
                    }
                    UserBusinessRelationshipBE data = new UserBusinessRelationshipBE();
                    data.UserID = item.UserID;
                    data.BusinessRefId = businessRefId;
                    data.CreatedByWkrId = item.UpdatedUserId.ToString();
                    data.UpdatedByWkrId = item.UpdatedUserId.ToString();
                    data.CreatedDate = item.TimeStamp;
                    data.UpdatedDate = item.TimeStamp;
                    data.ProcessNpaInd = isNewlyAdded;
                    ubrId = bo.Create(data);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserDepartmentXref - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
        public bool CreateRoleMappings(int userID, List<int> roleMappings)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            int ret = uroles.Create(userID, roleMappings);
            return ret > 0 ? true : false;
        }
        public bool DeleteRoleMappings(int userID, List<int> roleMappings)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            int ret = uroles.Delete(userID, roleMappings);
            return ret > 0 ? true : false;
        }

        public List<SystemRole> GetSystemRolesByUserId(int userID)
        {
            try
            {
                UserIdentityModelBO bo = new UserIdentityModelBO();
                return bo.GetUserSystemRolesByUserId(userID);

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error GetSystemRolesByUserId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        /// <summary>
        /// Gets the department list for the user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Department> GetAllDepartmentsByUserIdWSOI(int userID)
        {
            try
            {
                return new DepartmentModelBO().GetAllDepartmentsForUser(userID);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error GetAllDepartmentsByUserIdWSOI - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
        public Department GetDepartmentByEnum(DepartmentNameEnums deptEnum)
        {
            try
            {
                DepartmentModelBO modelBo = new DepartmentModelBO();
                return modelBo.GetInstance(deptEnum);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error GetAllDepartmentsByUserId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public List<OccupancySquareFootage> GetAllSquareFootageList()
        {
            List<OccupancySquareFootage> ret = new List<OccupancySquareFootage>();
            SquareFootageRefBO bo = new SquareFootageRefBO();
            List<SquareFootageRefBE> belst = bo.GetList();
            foreach (var item in belst)
            {
                ret.Add(new OccupancySquareFootage()
                {
                    ID = item.SquareFootageRefID.Value,
                    SquareFootage = item.SquareFootageValue,

                    CreatedDate = item.CreatedDate.Value,
                    UpdatedDate = item.UpdatedDate.Value,
                    CreatedUser = new UserIdentity() { ID = int.Parse(item.CreatedByWkrId) },
                    UpdatedUser = new UserIdentity() { ID = int.Parse(item.UpdatedByWkrId) }
                });
            }
            return ret;
        }

        public List<Occupancy> GetOccupancyList()
        {
            List<Occupancy> ret = new List<Occupancy>();
            OccupancyTypeRefBO occbo = new OccupancyTypeRefBO();
            List<OccupancyTypeRefBE> occList = occbo.GetList();
            foreach (var item in occList)
            {
                Occupancy occ = new Occupancy();
                occ.OccupancyId = item.OccupancyTypRefId.Value;
                occ.OccupancyName = item.OccupancyTypName;
                ret.Add(occ);
            }
            return ret;
        }
        public List<UserMgmtOccupancy> GetSquareFootageListbyUserOccupancy(int userId)
        {
            List<UserMgmtOccupancy> returnValue = new List<UserMgmtOccupancy>();
            //Full Occupancy List
            OccupancyTypeRefBO occbo = new OccupancyTypeRefBO();
            List<OccupancyTypeRefBE> occList = occbo.GetList();

            //User OccupancyList
            OccupancySqrFootageUsrMapBO bo = new OccupancySqrFootageUsrMapBO();
            List<OccupancySqrFootageUsrMapBE> usrOccList = bo.GetList(userId);

            foreach (var item in occList)
            {
                UserMgmtOccupancy umOccupancy = new UserMgmtOccupancy();
                umOccupancy.OccupancyId = item.OccupancyTypRefId.Value;
                umOccupancy.OccupancyName = item.OccupancyTypName;
                umOccupancy.UserId = userId;

                if (usrOccList.Any(x => x.OccupancyTypeRefID == item.OccupancyTypRefId.Value))
                {

                    umOccupancy.OccuIdSelected = true;
                    umOccupancy.SquareFootageId = usrOccList
                                                  .Where(u => u.OccupancyTypeRefID == item.OccupancyTypRefId.Value)
                                                  .Select(s => s.SquareFootageRefID.Value).FirstOrDefault();
                }
                returnValue.Add(umOccupancy);
            }
            return returnValue;
        }


        internal List<TimeSlot> GetUsedTimeSlotsByUserID(int userID)
        {
            List<TimeSlot> ret = new List<TimeSlot>();
            try
            {
                UserScheduleBO bo = new UserScheduleBO();
                ret = bo.GetUsedTimeSlotsWithExtrasByUserID(userID).Select(x => x.StartDateTime == null ? null : new TimeSlot()
                {
                    AllocationType = TimeAllocationType.Project,
                    StartTime = x.StartDateTime.Value,
                    EndTime = x.EndDateTime.Value,
                    UserID = x.UserID.Value,
                    ProjectScheduleID = x.ProjectScheduleID.Value,
                    UserScheduleID = x.UserScheduleID.Value
                }).ToList();
                return ret.Where(x => x != null).ToList();
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error GetSystemRolesByUserId - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        internal List<TimeSlot> GetUsedTimeSlotsExtrasByUserID(int userID, DateTime? start = null, DateTime? end = null)
        {
            List<TimeSlot> ret = new List<TimeSlot>();
            try
            {
                UserScheduleBO bo = new UserScheduleBO();
                return bo.GetUsedTimeSlotsWithExtrasByUserID(userID, start, end).Select(x => x.StartDateTime == null || x.EndDateTime == null ? null : new TimeSlot()
                {
                    StartTime = x.StartDateTime.Value,
                    EndTime = x.EndDateTime.Value,
                    UserID = x.UserID.Value,
                    ProjectScheduleID = x.ProjectScheduleID.Value,
                    ProjectID = x.ProjectID.Value,
                    UserScheduleID = x.UserScheduleID.Value,
                    ProjectScheduleTypeName = x.ProjectScheduleTypeName,
                    DepartmentName = (DepartmentNameEnums)x.BusinessRefID.Value,
                    ProjectCategory = x.ProjectCategory,
                    AllocationType = GetAllocationType(x.ProjectScheduleTypeName, x.ProjectCategory),
                    TotalTimeOfDay = x.EndDateTime.Value - x.StartDateTime.Value
                }).Where(x => x != null).ToList();
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error GetUsedTimeSlotsExtrasByUserID - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        TimeAllocationType GetAllocationType(string projectScheduleTypeName, string projectCategory)
        {
            if (string.IsNullOrEmpty(projectScheduleTypeName))
                return TimeAllocationType.NA;
            if (projectScheduleTypeName.ToUpper() == "NPA")
            {
                if (!string.IsNullOrWhiteSpace(projectCategory) && int.TryParse(projectCategory, out int output))
                {
                    //get the time allocation
                    return new TimeAllocationTypeRefModelBO().GetInstance(output).TimeAllocationType;
                }
                else
                {
                    //return default of NPA
                    return TimeAllocationType.NPA;
                }
            }
            else if (projectScheduleTypeName.ToUpper() == "EXP")
                return TimeAllocationType.Project_Express_Blocked;
            else if (projectScheduleTypeName.ToUpper() == "EMA")
                return TimeAllocationType.Project_Express_Reserved;
            else if (
                projectScheduleTypeName.ToUpper() == "PMA" ||
                projectScheduleTypeName.ToUpper() == "PR" ||
                projectScheduleTypeName.ToUpper() == "FIFO")
                return TimeAllocationType.Project_PlanReview;
            else if (projectScheduleTypeName.ToUpper() == "FMA")
                return TimeAllocationType.Project_Facilitator_Meeting;
            else
                return TimeAllocationType.Project;
        }

        public List<ProjectOccupancyTypeNameModel> GetOccupancyTypeProjectMapListByUser(int userId)
        {
            List<ProjectOccupancyTypeNameModel> ret = new List<ProjectOccupancyTypeNameModel>();
            //Full Occupancy List
            OccupancySqrFootageUsrMapBO occbo = new OccupancySqrFootageUsrMapBO();
            return occbo.GetOccupancyTypeMapListByUser(userId).Select(x =>
            {
                return new ProjectOccupancyTypeNameModel()
                {
                    UserID = x.UserID,
                    ProjectOccupancyTypeName = x.ProjectOccupancyTypeName,
                    ProjectOccupancyTypeRefID = x.ProjectOccupancyTypeRefID,
                    OccupancyTypeName = x.OccupancyTypeName,
                    OccupancyTypeRefID = x.OccupancyTypeRefID
                };
            }).ToList();
        }

        public bool CreateOccupancy(List<OccupancyOutput> occupancy)
        {
            try
            {
                OccupancySqrFootageUsrMapBO bo = new OccupancySqrFootageUsrMapBO();

                foreach (var item in occupancy)
                {
                    OccupancySqrFootageUsrMapBE data = new OccupancySqrFootageUsrMapBE();

                    data.OccupancyTypeRefID = Convert.ToInt32(item.OccupancyIdSelected);
                    data.SquareFootageRefID = Convert.ToInt32(item.SquareFootageIdSelected);
                    data.UserID = Convert.ToInt32(item.UserId);
                    bo.Create(data);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error UpdateUserProjectTypeRef - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool DeleteOccupancy(int userId)
        {
            OccupancySqrFootageUsrMapBO bo = new OccupancySqrFootageUsrMapBO();
            int ret = bo.Delete(userId);
            return ret > 0 ? true : false;
        }

        public List<UserIdentity> Search(string firstname, string lastname)
        {
            UserIdentityModelBO bo = new UserIdentityModelBO();
            return bo.Search(string.Empty, string.Empty);
        }

        public List<UserIdentity> GetAllManagers()
        {
            try
            {
                return GetUserIdentityListBySystemRoleEnum(SystemRoleEnum.Manager);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetAllManagers - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
        public List<UserIdentity> GetUserIdentityListBySystemRoleEnum(SystemRoleEnum systemRoleEnum)
        {
            try
            {
                List<UserBE> userBEs = new UserBO().GetListBySystemRole(systemRoleEnum.ToString());
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                List<UserIdentity> userIdentities = userBEs != null ? userBEs.Select(x => userIdentityModelBO.ConvertBeToUserIdentity(x)).ToList() : new List<UserIdentity>();
                return userIdentities;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetUserIdentityListBySystemRoleEnum - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
        public List<EstimatorUIModel> GetAllEstimators()
        {
            try
            {
                List<UserIdentity> allusers = GetUserIdentityListBySystemRoleEnum(SystemRoleEnum.Estimator);
                List<EstimatorUIModel> estimators = new List<EstimatorUIModel>();
                foreach (UserIdentity user in allusers)
                {
                    estimators.Add(new EstimatorUIModel().ConvertUserIdentityToEstimator(user));
                }

                return estimators;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetAllEstimators - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public List<Facilitator> GetAllFacilitators()
        {
            try
            {
                List<UserIdentity> allusers = GetUserIdentityListBySystemRoleEnum(SystemRoleEnum.Facilitator);

                List<Facilitator> facilitators = new List<Facilitator>();
                foreach (UserIdentity user in allusers)
                {
                    facilitators.Add(new FacilitatorModelBO().ConvertUserIdentityToFacilitator(user));
                }
                return facilitators;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetAllFacilitators - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
        public List<Reviewer> GetAllReviewers(bool isExpressSched = false, bool IsSchedulable = false)
        {
            try
            {
                List<DepartmentNameEnums> departmentNameEnums = new List<DepartmentNameEnums>();

                if (IsSchedulable)
                {
                    return GetReviewers((int)PropertyTypeEnums.NA, (int)DepartmentNameEnums.NA, isExpressSched);
                }
                else
                {
                    List<UserIdentity> allusers = GetUserIdentityListBySystemRoleEnum(SystemRoleEnum.Plan_Reviewer);
                    List<Reviewer> reviewers = new List<Reviewer>();
                    foreach (UserIdentity user in allusers)
                    {
                        if (isExpressSched)
                        {
                            if (user.IsExpressSched)
                                reviewers.Add(new ReviewerModelBO().ConvertUserIdentityToReviewer(user));
                        }
                        else
                        {

                            reviewers.Add(new ReviewerModelBO().ConvertUserIdentityToReviewer(user));
                        }
                    }
                    return reviewers;

                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetAllReviewers - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool SaveUserJurisdiction(UserJurisdictionSaveModel item)
        {
            bool success = false;
            try
            {
                UserJurisdictionXRefBO bo = new UserJurisdictionXRefBO();
                int rows = bo.Update(item.UserId, string.Join(",", item.JurisdictionList), item.WrkId);
                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SaveUserJurisdiction - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public List<string> GetJurisdictionListByUser(int userId)
        {

            try
            {
                UserJurisdictionXRefBO bo = new UserJurisdictionXRefBO();
                List<UserJurisdictionXRefBE> items = bo.GetList(userId);
                List<string> enummappingvalnbrs = items.Select(x => x.EnumMappingValNbr.ToString()).ToList();
                return enummappingvalnbrs;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetJurisdictionListByUser - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        /// <summary>
        /// Get active Schedulable Reviewers by Project Type (MMF, Express, etc)
        /// Used on Schedule Plan Review pages (prelim, express, etc)
        /// </summary>
        /// <param name="propertyTypeEnum"></param>
        /// <returns></returns>
        public List<Reviewer> GetReviewers(int propertyTypeEnum, int departmentNameEnum, bool isExpressSchedulable = false)
        {
            List<Reviewer> reviewers = new List<Reviewer>();
            PropertyTypeEnums propertyType = (PropertyTypeEnums)propertyTypeEnum;
            DepartmentNameEnums deptEnum = (DepartmentNameEnums)departmentNameEnum;
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
            List<DepartmentNameEnums> deptsenums = new Helper().DepartmentNamesEnums(deptEnum);
            string csvs = string.Join(",", deptsenums.Select(x => (int)x).ToList());
            try
            {
                List<UserBE> userBEs = new UserBO().GetListByProjectTypeId(propertyTypeEnum, csvs);
                foreach (UserBE user in userBEs)
                {
                    UserIdentity userIdentity = userIdentityModelBO.ConvertBeToUserIdentity(user);
                    if (isExpressSchedulable)
                    {
                        if (user.IsExpressSched.Value == true)
                        {
                            userIdentity.FillDesignatedDepartments();
                            reviewers.Add(new ReviewerModelBO().ConvertUserIdentityToReviewer(userIdentity));

                        }
                    }
                    else
                    {
                        userIdentity.FillDesignatedDepartments();
                        reviewers.Add(new ReviewerModelBO().ConvertUserIdentityToReviewer(userIdentity));
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetReviewers - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return reviewers;
        }

        private UserOutlookDetail GetOutlookDataForUser(UserIdentity user)
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();
            UserOutlookDetail userOutlookDetail = new UserOutlookDetail();

            string userPrincipalName = string.Empty;
            string calendarId = string.Empty;

            if (user.IsCity)
            {
                userPrincipalName = "permitplanreview@mecklenburgcountync.gov";
                calendarId = outlookAdapter.GetCalendarIdForCityUser(user.Email);

                if (string.IsNullOrEmpty(calendarId))
                {
                    calendarId = outlookAdapter.CreateUserCalendarForCityUser(user.Email);
                }
            }
            else
            {
                userPrincipalName = outlookAdapter.GetUserPrincipalNameFromEmailAddress(user.Email);

                if (userPrincipalName != null)
                {
                    calendarId = outlookAdapter.GetUserDefaultCalendar(userPrincipalName);
                }
            }

            userOutlookDetail.UserPrincipalName = userPrincipalName;
            userOutlookDetail.CalendarId = calendarId;

            return userOutlookDetail;
        }

        public int UpsertProjectManager(Project project)
        {
            try
            {
                int userId = 0;
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                UserBO bo = new UserBO();
                string pmfirst = project.PMFirstName;
                string pmlast = project.PMLastName;
                string pmname = pmfirst + " " + pmlast;
                string pmemail = project.PMEmail;
                string pmphone = string.IsNullOrWhiteSpace(project.PMPhone) ? "" : project.PMPhone;
                string systemrolename = SystemRoleExternalRef.External_Project_Manager.ToString();

                //see if this manager exists
                //check if email is null and make sure it's the right format before saving
                if (String.IsNullOrWhiteSpace(pmemail) || !RegexUtilities.IsValidEmail(pmemail))
                    return 0;

                int? currentPM = new ProjectBO().GetById(project.ID).ProjectManagerId;
                bool isNewUser = false;

                UserBE exists = bo.GetByExternalRefInfo(project.PMEmail, (int)ExternalSystemEnum.Accela);
                //if manager doesn't exist, create, if exists, update
                //insert
                if (exists == null || exists.UserID.HasValue == false || exists.UserID.Value == 0)
                {
                    userId = bo.InsertUsersWithSystemRole(pmfirst, pmlast, (int)ExternalSystemEnum.Accela, pmemail, systemrolename,
                         "add", pmemail, pmemail, pmphone, pmemail, "", false, 0, "0", 0, "");
                    isNewUser = true;
                }
                else
                {
                    //if the name and phone are the same, don't update
                    if (pmfirst.Equals(exists.FirstNm) == false ||
                        pmlast.Equals(exists.LastNm) == false ||
                        pmphone.Equals(exists.Phone) == false)
                    {

                        //update the name and phone only
                        UserBE updateuser = new UserBE
                        {
                            ADName = exists.ADName,
                            Email = exists.Email,
                            ExternalSystemRefId = exists.ExternalSystemRefId,
                            FirstNm = pmfirst,
                            HoursEstimated = exists.HoursEstimated,
                            IsActive = exists.IsActive,
                            IsExpressSched = exists.IsExpressSched,
                            IsPrelimMeetingAllowed = exists.IsPrelimMeetingAllowed,
                            IsSchedulable = exists.IsSchedulable,
                            Jurisdiction = exists.Jurisdiction,
                            LastNm = pmlast,
                            Notes = exists.Notes,
                            Phone = pmphone,
                            PlanReviewOverrideHours = exists.PlanReviewOverrideHours,
                            SchedulableLevel = exists.SchedulableLevel,
                            SrcSystemValueTxt = exists.SrcSystemValueTxt,
                            UiSettings = exists.UiSettings,
                            UpdatedDate = exists.UpdatedDate,
                            UserID = exists.UserID,
                            UserId = "1",
                            UserName = exists.UserName,
                            CalendarId = string.Empty,
                            UserPrincipalName = string.Empty
                        };
                        bo.Update(updateuser);
                    }

                    userId = exists.UserID.Value;
                }

                //decide if the userId is 0 or null, then if the current pm isn't, use it so we don't have a blank pm id
                if (currentPM.HasValue && userId == 0)
                {
                    userId = currentPM.Value;
                }
                //send email, update email for PM updated on the project, or new email for new user
                if (currentPM.HasValue && currentPM.Value != 0 && userId != currentPM.Value)
                {
                    new EmailAdapter().SendUpdateProjectManagerToAdmin(pmname, pmemail, project.AccelaProjectRefId);
                }
                else
                {
                    if (isNewUser)
                        new EmailAdapter().SendNewProjectManagerToAdmin(pmname, pmemail, project.AccelaProjectRefId);
                }

                return userId;
            }
            catch (Exception ex)
            {
                string erroremail = !string.IsNullOrWhiteSpace(project.PMEmail) ? project.PMEmail : " email is null ";
                string errorMessage = "Error UpsertProjectManager(Project project) - " + erroremail + " : " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
    }
}
