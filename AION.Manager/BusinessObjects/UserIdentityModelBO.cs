using AION.BL.Adapters;
using AION.BL.Common;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class UserIdentityModelBO : ModelBaseModelBO, IUserIdentityBO
    {
        private List<UserIdentity> BaseList
        {
            get
            {
                List<UserIdentity> ret = Base.LocalStorage<List<UserIdentity>>.GetValue("UserIdentity_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    Base.LocalStorage<List<UserIdentity>>.Add("UserIdentity_List", ret);
                }
                return ret;
            }
        }

        protected bool InjectBaseObjects(int? id, UserIdentity inheritedObject)
        {
            UserBO bo = new UserBO();
            UserBE be = bo.GetById(id.Value);

            ConvertBeToUserIdentity(be, inheritedObject);
            return true;
        }

        public bool RefreshList()
        {
            Base.LocalStorage<List<UserIdentity>>.Delete("UserIdentity_List");
            return true;
        }
        public UserIdentity GetInstance(int id)
        {
            UserIdentity ret = new UserIdentity();
            if (BaseList.Where(x => x.ID == id).Any() == false)
            {
                InjectBaseObjects(id, ret);
                //dont' add to the list if nothing returned
                if (ret.ID != 0)
                    BaseList.Add(ret);
            }
            else
                ret = BaseList.Where(x => x.ID == id).FirstOrDefault();
            return ret;

        }

        public List<UserIdentity> GetInstance(string filterString = "", string filterMode = "")
        {
            Helper helper = new Helper();
            //is called with no params. 
            if (string.IsNullOrEmpty(filterString) && string.IsNullOrEmpty(filterMode))
            {
                return BaseList;
            }
            else
            {
                List<UserIdentity> userIdentities = BaseList;

                DepartmentModelBO deptmodel = new DepartmentModelBO();
                bool isDepartmentNameEnum = helper.AllDepartmentNames.Any(x => x.ToString() == filterMode);
                bool isRoleEnum = helper.FilterModeSearchEnums.Any(x => x.ToString() == filterMode);

                if (isDepartmentNameEnum)
                {
                    DepartmentNameEnums deptenum;
                    Enum.TryParse<DepartmentNameEnums>(filterMode, out deptenum);
                    List<UserIdentity> userswdepts = userIdentities.ToList();
                    userswdepts.FillAllDesignatedDepartments();

                    bool isZone = helper.ZoneDepartmentNames.Any(x => x == deptenum);
                    bool isFire = helper.FireDepartmentNames.Any(x => x == deptenum);

                    if (isZone)
                    {
                        return userswdepts.Where(x => x.DesignatedDepartments.Any(y => helper.ZoneDepartmentNames.Contains(y.DepartmentEnum))
                            && (string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper()))).ToList();
                    }
                    else if (isFire)
                    {
                        return userswdepts.Where(x => x.DesignatedDepartments.Any(y => helper.FireDepartmentNames.Contains(y.DepartmentEnum))
                            && (string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper()))).ToList();
                    }
                    else
                    {
                        return userswdepts.Where(x => x.DesignatedDepartments.Any(y => y.DepartmentEnum == deptenum)
                            && (string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper()))).ToList();
                    }
                }
                else if (isRoleEnum)
                {
                    SystemRoleEnum roleenum;
                    Enum.TryParse<SystemRoleEnum>(filterMode, out roleenum);
                    List<UserIdentity> userswroles = new UserAdapter().GetUserIdentityListBySystemRoleEnum(roleenum);
                    return userswroles.Where(x => string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper())).ToList();
                }
                else if (filterMode == "lastname,firstname")
                {
                    if (!string.IsNullOrWhiteSpace(filterString) && filterString.IndexOf(",") > 0)
                    {
                        string firstname = filterString.Substring(filterString.IndexOf(",") + 1).Trim();
                        string lastname = filterString.Substring(0, filterString.IndexOf(",")).Trim();
                        return BaseList.Where(x => !string.IsNullOrWhiteSpace(x.FirstName) && !string.IsNullOrWhiteSpace(x.LastName)
                            && x.FirstName.Equals(firstname, StringComparison.OrdinalIgnoreCase)
                            && x.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else
                    {
                        return BaseList.Where(x => string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper())).ToList();

                    }
                }
                else //if (filterMode == "All")
                {
                    return BaseList.Where(x => string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper())).ToList();
                }

            }
        }

        public List<UserIdentity> UserManagementSearchList(string filterString = "", string filterMode = "")
        {
            Helper helper = new Helper();

            List<UserIdentity> userList = new List<UserIdentity>();
            UserBO bo = new UserBO();
            List<UserBE> be = bo.GetUserManagementSearchList(filterString);
            foreach (var item in be)
            {
                UserIdentity v = new UserIdentity();
                v = ConvertBeToUserIdentity(item, v);
                userList.Add(v);

            }
            if (filterMode == "All")
            {


                return userList;
            }

            else
            {
                bool isDepartmentNameEnum = helper.AllDepartmentNames.Any(x => x.ToString() == filterMode);
                bool isRoleEnum = helper.FilterModeSearchEnums.Any(x => x.ToString() == filterMode);


                DepartmentModelBO deptmodel = new DepartmentModelBO();
                if (isDepartmentNameEnum)
                {
                    DepartmentNameEnums deptenum;
                    Enum.TryParse<DepartmentNameEnums>(filterMode, out deptenum);
                    List<UserIdentity> userswdepts = new List<UserIdentity>();

                    List<DepartmentNameEnums> deptsenums = new Helper().DepartmentNamesEnums(deptenum);
                    string businessrefenumcsv = string.Join(",", deptsenums.Select(x => (int)x).ToList());
                    userswdepts = ConvertList(bo.GetUserManagementBusinessRefSearchList(businessrefenumcsv));

                    return userswdepts.Where(x => string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper())).ToList();
                }
                else if (isRoleEnum)
                {
                    SystemRoleEnum roleenum;
                    Enum.TryParse<SystemRoleEnum>(filterMode, out roleenum);
                    List<UserIdentity> userswroles = new UserAdapter().GetUserIdentityListBySystemRoleEnum(roleenum);
                    return userswroles.Where(x => string.IsNullOrEmpty(filterString) || x.FirstName.ToUpper().Contains(filterString.ToUpper()) || x.LastName.ToUpper().Contains(filterString.ToUpper())).ToList();
                }
                else //if (filterMode == "All")
                {
                    return userList;
                }


            }

        }


        protected bool InjectBaseObjects(string external_Ref_info, ExternalSystemEnum externalSystemEnum, UserIdentity inheritedObject)
        {
            UserBO bo = new UserBO();
            UserBE be = bo.GetByExternalRefInfo(external_Ref_info, (int)externalSystemEnum);
            ConvertBeToUserIdentity(be, inheritedObject);
            return true;
        }

        public UserIdentity GetInstance(string external_Ref_info, ExternalSystemEnum externalSystemEnum)
        {
            //jcl 04272021 externalSystemEnum is deprecated for searches, srssystemvaluetxt is required to be unique in the db
            //future cleanup for referencing methods
            if (external_Ref_info == null)
                external_Ref_info = "system"; //incase of null set it as default / SYSTEM
            UserIdentity ret = new UserIdentity();
            if (BaseList.Where(x => x.SrcSystemValueText == external_Ref_info).Any() == false)
            {
                InjectBaseObjects(external_Ref_info, externalSystemEnum, ret);
                if (ret.ID != 0)
                {
                    BaseList.Add(ret);
                }
                else // this will not be saved to db but wil save further calls to db.
                {
                    ret.SrcSystemValueText = external_Ref_info;
                    ret.ExternalSystemID = (int)externalSystemEnum;
                    BaseList.Add(ret);
                }
            }
            else
                ret = BaseList.Where(x => x.SrcSystemValueText == external_Ref_info).FirstOrDefault();
            return ret;
        }

        public UserIdentity GetInstanceByUserName(string userName)
        {
            return BaseList.FirstOrDefault(x => x.UserName == userName);
        }

        public List<SystemRole> GetUserSystemRolesByUserId(int UserId)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            List<UserSystemRoleRelationshipBE> rolelist = uroles.GetListByUserId(UserId);
            List<SystemRole> roles = new List<SystemRole>();
            foreach (UserSystemRoleRelationshipBE role in rolelist)
            {
                roles.Add(ConvertUserSystemRole(role));
            }
            return roles;
        }
        public List<SystemRole> GetUserSystemRolesEnumsByUserId(int UserId)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            List<UserSystemRoleRelationshipBE> rolelist = uroles.GetListByUserId(UserId);
            List<SystemRole> roles = new List<SystemRole>();
            foreach (UserSystemRoleRelationshipBE role in rolelist)
            {
                roles.Add(new SystemRole
                {
                    ID = role.SystemRoleId.Value,
                    EnumMappingValNbr = role.SystemRoleEnumMappingNumber,
                    SystemRoleEnum = role.SystemRoleEnumMappingNumber.HasValue ? (SystemRoleEnum)role.SystemRoleEnumMappingNumber.Value : SystemRoleEnum.NA
                }); ;
            }
            return roles;
        }
        public int GetUserSystemRoleRelationship(int userid, int roleid)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            UserSystemRoleRelationshipBE role = uroles.GetByUserRoleId(userid, roleid);
            return (int)role.UserSystemRoleRelationshipId;
        }
        public int DeleteUserSystemRoleByIdentity(int id)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            int rows = uroles.Delete(id);
            return rows;
        }
        public int InsertUserSystemRole(int userid, int roleid)
        {
            UserSystemRoleRelationshipBO uroles = new UserSystemRoleRelationshipBO();
            int rows = uroles.Create(userid, new List<int>() { roleid });
            return rows;
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
        private SystemRole ConvertUserSystemRole(UserSystemRoleRelationshipBE role)
        {
            SystemRoleBE sr = new SystemRoleBO().GetById((int)role.SystemRoleId);

            SystemRole ret = new SystemRole();

            InjectBaseObjects(ret, sr.SystemRoleId.Value, sr.CreatedDate.Value, sr.UpdatedDate.Value, sr.CreatedByWkrId, sr.UpdatedByWkrId);
            ret.ExternalSystem = new ExternalSystemModelBO().GetInstance(ret.ExternalSystemRefInfo);
            ret.ExternalSystemRefInfo = "Need to be mapped to Accela object/DB if exsits.";
            ret.RoleName = sr.SystemRoleNm;
            ret.SystemRoleEnum = sr.EnumMappingValNbr.HasValue ? (SystemRoleEnum)sr.EnumMappingValNbr.Value : SystemRoleEnum.NA;
            return ret;
        }

        public bool SyncUserstoAIONDB(List<AccelaUserBE> users, string systemRoleName)
        {
            UserBO bo = new UserBO();
            //remove duplicates if any by SrcSystemValueTxt
            List<UserBE> aionUsers = bo.GetListBySystemRole(systemRoleName).GroupBy(x => x.SrcSystemValueTxt).Select(x => x.FirstOrDefault()).ToList();
            //remove duplicates if any by SrcSystemValueTxt
            users = users.GroupBy(x => x.SrcSystemValueTxt).Select(x => x.FirstOrDefault()).ToList();
            //Users are in Accela but not exists in AION - Add these users to AION
            var notInAION = (from l1 in users
                             where !aionUsers.Any(l2 => l1.SrcSystemValueTxt == l2.SrcSystemValueTxt)
                             select l1).ToList();
            //Users are in Accela and in AION - Update these users to AION
            var InBoth = (from l1 in users
                          where aionUsers.Any(l2 => l1.SrcSystemValueTxt == l2.SrcSystemValueTxt &&
                          (l1.FirstNm != l2.FirstNm || l1.LastNm != l2.LastNm || l1.EmailAddrTxt != l2.Email || l1.ExternalAppUserNm != l2.UserName ||
                          l1.NotesTxt != l2.Notes || l1.PhoneNum != l2.Notes))
                          select l1).ToList();

            //Users are not in Accela and are in AION - Delete these users from AION
            var notInAccela = (from l1 in aionUsers
                               where !users.Any(l2 => l1.SrcSystemValueTxt == l2.SrcSystemValueTxt)
                               select l1).ToList();

            foreach (var item in notInAION)
            {
                if (item.ExternalSystemRefId != 1 && item.ExternalSystemRefId != 2)
                {
                    continue;
                }
                bo.InsertUsersWithSystemRole(item.FirstNm, item.LastNm, item.ExternalSystemRefId, item.SrcSystemValueTxt, systemRoleName, "add", item.ExternalAppUserNm, item.LanIdTxt, item.PhoneNum,
                    item.EmailAddrTxt, item.NotesTxt, false, 0, "0", 0, "");
            }
            //foreach (var item in notInAccela)
            //{

            //    bo.DeleteUserWithSystemRole(item.UserID, systemRoleName, "delete");
            //}
            foreach (var item in InBoth)
            {
                if (item.ExternalSystemRefId != 1 && item.ExternalSystemRefId != 2)
                {
                    continue;
                }
                var user = (from l1 in aionUsers
                            where l1.SrcSystemValueTxt == item.SrcSystemValueTxt
                            select l1).FirstOrDefault();
                //bo.UpdateUsersWithSystemRole(user.UserID, item.FirstNm, item.LastNm, item.SrcSystemValueTxt, item.ExternalSystemRefId, systemRoleName, "update", item.ExternalAppUserNm, item.LanIdTxt, item.PhoneNum,
                //    item.EmailAddrTxt, item.NotesTxt, false, 0, "0", 0, "",(user.IsPrelimMeetingAllowed.HasValue? user.IsPrelimMeetingAllowed.Value:false));
                bo.UpdateUsersWithSystemRole(user.UserID, item.FirstNm, item.LastNm, item.SrcSystemValueTxt, item.ExternalSystemRefId, systemRoleName, "update", item.ExternalAppUserNm, item.LanIdTxt, item.PhoneNum,
                    item.EmailAddrTxt, item.NotesTxt, user.IsSchedulable.HasValue ? user.IsSchedulable.Value : false,
                    user.PlanReviewOverrideHours.HasValue ? user.PlanReviewOverrideHours.Value : 0,
                    user.HoursEstimated, user.Jurisdiction.HasValue ? user.Jurisdiction.Value : 0,
                    user.SchedulableLevel, (user.IsPrelimMeetingAllowed.HasValue ? user.IsPrelimMeetingAllowed.Value : false));
            }
            return true;
        }


        public int CreateUser(UserIdentity user)
        {
            try
            {
                UserBO bo = new UserBO();
                UserBE be = new UserBE();
                be = ConvertUserIdentityToBE(user);
                int rows = bo.Create(be);
                //refresh base list
                RefreshList();
                return rows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(UserIdentity user)
        {
            try
            {
                UserBO bo = new UserBO();
                UserBE be = new UserBE();
                be = ConvertUserIdentityToBE(user);
                bo.Update(be);
                //refresh base list
                RefreshList();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GetUserIdentityByUserBE(UserIdentity user)
        {

            bool exists = false;
            try
            {
                UserBO userBO = new UserBO();
                UserBE be = new UserBE();
                be = ConvertUserIdentityToBE(user);
                exists = userBO.GetUserIdentityByUserBE(be);
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateExpressSchedulable(int userid, bool isexpresssched)
        {
            UserBO bo = new UserBO();
            UserBE be = bo.GetById(userid);
            be.IsExpressSched = isexpresssched;

            bool success = false;
            int rows = 0;
            rows = bo.UpdateIsExpressSched(be);
            success = (rows > 0);

            return true;
        }

        public bool UpdateIsSchedulable(int userid, bool isschedulable)
        {
            UserBO bo = new UserBO();
            UserBE be = bo.GetById(userid);
            be.IsSchedulable = isschedulable;

            bool success = false;
            int rows = 0;
            rows = bo.UpdateIsSchedulable(be);
            success = (rows > 0);

            return true;
        }
        public List<UserIdentity> Search(string firstname, string lastname)
        {
            //TODO: add search filters
            return BaseList;
        }

        private List<UserIdentity> CreateInstance()
        {
            List<UserIdentity> ret = new List<UserIdentity>();
            UserBO bo = new UserBO();
            List<UserBE> be = bo.GetList();
            foreach (var item in be)
            {
                UserIdentity v = new UserIdentity();
                v = ConvertBeToUserIdentity(item, v);
                ret.Add(v);
            }
            return ret;
        }

        private List<UserIdentity> ConvertList(List<UserBE> userBEs)
        {
            List<UserIdentity> userList = new List<UserIdentity>();
            foreach (var item in userBEs)
            {
                UserIdentity v = new UserIdentity();
                v = ConvertBeToUserIdentity(item, v);
                userList.Add(v);

            }
            return userList;
        }
        public UserIdentity ConvertBeToUserIdentity(UserBE be, UserIdentity inheritedObject = null)
        {
            UserIdentity ret = inheritedObject;
            if (ret == null)
                ret = new UserIdentity();
            if (be.UserID != null)
            {
                ret.ID = be.UserID.Value;
                ret.CreatedDate = be.CreatedDate.HasValue ? be.CreatedDate.Value : DateTime.Now;
                ret.UpdatedDate = be.UpdatedDate.HasValue ? be.UpdatedDate.Value : DateTime.Now;
                ret.FirstName = be.FirstNm;
                ret.LastName = be.LastNm;
                ret.SrcSystemValueText = be.SrcSystemValueTxt;
                ret.ExternalSystemID = be.ExternalSystemRefId.HasValue ? be.ExternalSystemRefId.Value : 1;
                ret.IsActive = be.IsActive.Value;
                ret.UiSetting = be.UiSettings;
                ret.IsExpressSched = be.IsExpressSched == null ? false : (bool)be.IsExpressSched;
                ret.UserName = be.UserName;
                ret.ADName = be.ADName;
                ret.Phone = be.Phone;
                ret.Email = be.Email;
                ret.Notes = be.Notes;
                ret.IsSchedulable = be.IsSchedulable.HasValue ? be.IsSchedulable.Value : false;
                ret.PlanReviewOverrideHours = be.PlanReviewOverrideHours.HasValue ? be.PlanReviewOverrideHours.Value : 0;
                ret.HoursEstimated = be.HoursEstimated;
                ret.Jurisdiction = be.Jurisdiction.HasValue ? be.Jurisdiction.Value : 0;
                ret.SchedulableLevel = be.SchedulableLevel;
                ret.IsPrelimMeetingAllowed = be.IsPrelimMeetingAllowed.HasValue ? be.IsPrelimMeetingAllowed.Value : false;
                ret.UserPrincipalName = be.UserPrincipalName;
                ret.CalendarId = be.CalendarId;
                ret.IsCity = be.IsCity.HasValue ? be.IsCity.Value : false;
            }
            ret.DesignatedRoles = new List<SystemRole>(); //when needed use extension method FillDesignatedRoles() which can be called from UserIdentity // be.UserID.HasValue ? new SystemRoleModelBO().GetInstancesForUser(be.UserID.Value) : new List<SystemRole>();
            ret.DesignatedDepartments = new List<Department>(); //when needed use extension method FillDesignatedDepartments() which can be called from UserIdentity  // be.UserID.HasValue ? new DepartmentModelBO().GetAllDepartmentsForUser(be.UserID.Value) : new List<Department>();
            ret.CreatedUser = string.IsNullOrWhiteSpace(be.CreatedByWkrId) ? new UserIdentity() : new UserIdentity { ID = int.Parse(be.CreatedByWkrId) };
            ret.UpdatedUser = string.IsNullOrWhiteSpace(be.UpdatedByWkrId) ? new UserIdentity() : new UserIdentity { ID = int.Parse(be.UpdatedByWkrId) };

            return ret;
        }

        #region Private Methods

        private UserBE ConvertUserIdentityToBE(UserIdentity user)
        {
            UserBE be = new UserBE();

            try
            {
                be.UserID = user.ID;
                be.FirstNm = user.FirstName;
                be.LastNm = user.LastName;
                be.SrcSystemValueTxt = user.SrcSystemValueText;
                be.UiSettings = user.UiSetting;
                be.ExternalSystemRefId = user.ExternalSystemID;
                be.IsActive = user.IsActive;
                be.UpdatedDate = user.UpdatedDate;
                be.UserName = user.UserName;
                be.ADName = user.ADName;
                be.Phone = user.Phone;
                be.Email = user.Email;
                be.Notes = user.Notes;
                be.IsSchedulable = user.IsSchedulable;
                be.PlanReviewOverrideHours = user.PlanReviewOverrideHours;
                be.HoursEstimated = user.HoursEstimated;
                be.Jurisdiction = user.Jurisdiction;
                be.SchedulableLevel = user.SchedulableLevel;
                be.IsExpressSched = user.IsExpressSched;
                be.IsPrelimMeetingAllowed = user.IsPrelimMeetingAllowed;
                be.UserPrincipalName = user.UserPrincipalName;
                be.CalendarId = user.CalendarId;
                be.IsCity = user.IsCity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return be;
        }

        #endregion Private Methods

    }
    public interface IUserIdentityBO
    {
        List<UserIdentity> GetInstance(string filterString, string FilterMode);
        UserIdentity GetInstance(int id);

        UserIdentity GetInstance(string external_Ref_info, ExternalSystemEnum externalSystemEnum);

        List<SystemRole> GetUserSystemRolesByUserId(int UserId);
        int DeleteUserSystemRoleByIdentity(int id);
        int GetUserSystemRoleRelationship(int userid, int roleid);
        int InsertUserSystemRole(int userid, int roleid);
        bool UpdateUser(UserIdentity user);
        bool UpdateExpressSchedulable(int userid, bool isexpresssched);
        /// <summary>
        /// REturns base list
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        List<UserIdentity> Search(string firstname, string lastname);
        /// <summary>
        /// converts Business Entity to Model object
        /// </summary>
        /// <param name="be"></param>
        /// <param name="inheritedObject"></param>
        /// <returns></returns>
        UserIdentity ConvertBeToUserIdentity(UserBE be, UserIdentity inheritedObject = null);
        /// <summary>
        /// Refresh the cached users list
        /// </summary>
        /// <returns></returns>
        bool RefreshList();
    }
}
