using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Manager.Models.User;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IUserAdapter
    {
        UserIdentity GetUserIdentityByEmailExtSystem(string email, int externalSystemEnumID);
        UserIdentity GetUserIdentityByUserName(string userName);
        UserIdentity GetUserIdentityByID(int ID);
        bool UpdateUserIdentityIsExpressSched(int ID, bool isExpressSched);
        bool UpdateUser(UserIdentity user);
        int CreateUser(UserIdentity user);
        bool UpdateUserProjectTypeXref(UserProjectTypeXref item);
        bool UpdateUserIdentityIsSchedulable(int ID, bool isSchedulable);
        bool CreateRoleMappings(int userID, List<int> roleMappings);
        bool DeleteRoleMappings(int userID, List<int> roleMappings);
        bool GetUserIdentityByUserBE(UserIdentity user);
        List<SystemRole> GetSystemRolesByUserId(int userID);
        Department GetDepartmentByEnum(DepartmentNameEnums deptEnum);
        List<Occupancy> GetOccupancyList();
        List<OccupancySquareFootage> GetAllSquareFootageList();

        List<UserMgmtOccupancy> GetSquareFootageListbyUserOccupancy(int userId);

        bool CreateOccupancy(List<OccupancyOutput> occupancy);

        bool DeleteOccupancy(int userID);
        /// <summary>
        /// Search all users in AION db by first name, or last name
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        List<UserIdentity> Search(string firstname, string lastname);

        List<UserIdentity> GetAllManagers();
        List<EstimatorUIModel> GetAllEstimators();
        List<Facilitator> GetAllFacilitators();
        List<Reviewer> GetAllReviewers(bool isExpressSched = false, bool IsSchedulable = false);
        List<UserIdentity> GetUserIdentityListBySystemRoleEnum(SystemRoleEnum systemRoleEnum);

        /// <summary>
        /// Save user jurisdiction xref
        /// Admin User Managerment
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool SaveUserJurisdiction(UserJurisdictionSaveModel item);

        /// <summary>
        /// Get the list of jurisdiction for the user
        /// Admin User Manaegment
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<string> GetJurisdictionListByUser(int userId);

        /// <summary>
        /// Get active Schedulable Reviewers by Project Type (MMF, Express, etc)
        /// Used on Schedule Plan Review pages (prelim, express, etc)
        /// </summary>
        /// <param name="propertyTypeEnum"></param>
        /// <returns></returns>
        List<Reviewer> GetReviewers(int propertyTypeEnum, int departmentNameEnum, bool isExpressSchedulable = false);

        List<Department> GetAllDepartmentsByUserIdWSOI(int userID);
        UserIdentity GetUserIdentityByEmailWPermissions(string email, int externalSystemEnumID);

    }

}
