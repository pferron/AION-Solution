using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models;
using AION.Web.Helpers;
using AION.Web.Helpers.APIHelpers;
using AION.Web.Models;
using AION.Web.Models.Admin;
using Meck.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class AdminController : BaseControllerWeb
    {
        private string _loggedinUser;
        private AdminModel _model;
        private List<SystemRole> _systemRoleswPerms;
        private List<Permission> _permissionsList;
        private List<SystemRole> _systemRoles;
        #region ActionMethods

        // GET: Admin
        public ActionResult AdminMain(int selectedProjectType = 1)
        {
            AdminAPIHelper adminApiHelper = new AdminAPIHelper();

            AdminViewModel vm = new AdminViewModel();

            SetUpViewModelBase<AdminViewModel>(vm);

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            if (vm.PermissionMapping.IsManager == false && vm.PermissionMapping.IsSysAdmin == false && vm.PermissionMapping.IsViewOnly == false)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Insufficient permission" });
            }

            _model = adminApiHelper.GetAdminModel();

            //get lists used in roles tabs
            _systemRoleswPerms = _model.SystemRolesWithPermissions;
            _permissionsList = _model.PermissionsList;
            _systemRoles = _systemRoleswPerms;

            vm.HolidayConfigList = _model.HolidayConfigList;
            vm.DefaultHoursViewModel = GetDefaultHoursViewModel(selectedProjectType);
            //keep this line above user model.UserManagementViewModel creation. This will set the list of project types that can be auto assigned by each user which need to be listed for each user in user management tab.
            vm.MiscConfigurationViewModel = CreateMiscConfigurationViewModel();
            vm.UserManagementViewModel = CreateUserManagementViewModel(vm);
            vm.MiscConfigSchedulingMultiplierViewModel = CreateSchedulingMultiplierViewModel();
            vm.NPAConfigViewModel = CreateNPAViewModel();

            vm.CreateRoleViewModel = GetCreateRoleVM(null);
            vm.ModifyRoleViewModel = GetModifyRoleVM(null);
            vm.ModifyUserPermissionViewModel = GetModifyUserPermissionVM(null);
            vm.MessageConfigAdminViewModel = CreateMessageConfigurationVM();
            return View(vm);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult Update(AdminViewModel vm)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            AdminViewModel model = new AdminViewModel();
            AdminViewModel vmIdentity = new AdminViewModel();
            UserIdentity userIdentity = new UserIdentity();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;
            decimal result;
            int userID = 0;
            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                model.LoggedInUserEmail = _loggedinUser;
            }
            else
            {
                model.LoggedInUserEmail = vm.LoggedInUserEmail;
            }

            UpdateUserAndPermissions(vm);

            model.DefaultHoursViewModel = GetDefaultHoursViewModel(int.Parse(vm.DefaultHoursViewModel.SelectedProjectType));
            userIdentity.FirstName = vm.UserManagementViewModel.FirstName;
            userIdentity.LastName = vm.UserManagementViewModel.LastName;
            userIdentity.ADName = vm.UserManagementViewModel.AdAccount;
            userIdentity.Email = vm.UserManagementViewModel.Email;
            userIdentity.UserName = vm.UserManagementViewModel.UserName;

            if (
                (!string.IsNullOrEmpty(userIdentity.FirstName) &&
                (!string.IsNullOrEmpty(userIdentity.LastName) &&
                (!string.IsNullOrEmpty(userIdentity.ADName) &&
                (!string.IsNullOrEmpty(userIdentity.Email) &&
                (!string.IsNullOrEmpty(userIdentity.UserName)))))))

            {
                if (string.IsNullOrWhiteSpace(vm.UserManagementViewModel.SelectedUser)
                    || vm.UserManagementViewModel.SelectedUser.Equals("0"))
                {
                    userIdentity.Notes = vm.UserManagementViewModel.Notes;
                    userIdentity.FirstName = vm.UserManagementViewModel.FirstName;
                    userIdentity.LastName = vm.UserManagementViewModel.LastName;
                    userIdentity.ADName = vm.UserManagementViewModel.AdAccount;
                    userIdentity.Email = vm.UserManagementViewModel.Email;
                    userIdentity.SrcSystemValueText = vm.UserManagementViewModel.Email;
                    userIdentity.UserName = vm.UserManagementViewModel.UserName;
                    userIdentity.SchedulableLevel = LevelorOccupancy(vm.UserManagementViewModel.LevelSelected, vm.UserManagementViewModel.SelectedUser);
                    userIdentity.IsSchedulable = vm.UserManagementViewModel.IsSchedulableSelected == "Y" ? true : false;
                    userIdentity.IsExpressSched = vm.UserManagementViewModel.ExpressSelected == "Y" ? true : false;
                    userIdentity.IsPrelimMeetingAllowed = vm.UserManagementViewModel.PriliminaryMeetingSelected == "Y" ? true : false;
                    if (decimal.TryParse(vm.UserManagementViewModel.PlanReviewHoursOverride, out result))
                    {
                        userIdentity.PlanReviewOverrideHours = result;
                    }
                    userIdentity.CreatedUser = vm.UserManagementViewModel.LoggedInUser;
                    userIdentity.IsActive = vm.UserManagementViewModel.IsActiveSelected == "Y" ? true : false;
                    userIdentity.IsCity = vm.UserManagementViewModel.IsCityUserSelected == "Y" ? true : false;
                    userIdentity.Phone = vm.UserManagementViewModel.Phone;
                    userID = apihelper.CreateUser(userIdentity);
                    UserIdentity user = apihelper.GetUserIdentityByID(userID);
                    vm.UserManagementViewModel.SelectedUser = user.ID.ToString();
                }
            }
            SaveUserOptionsTabDetails(vm.UserManagementViewModel);
            SaveDefaultHoursViewModel(vm.DefaultHoursViewModel);
            SaveSchedulingMultiplierViewModel(vm.MiscConfigSchedulingMultiplierViewModel);

            //if there is a change in misc tab for the update auto assign project types then need to update user tab.

            if (SaveMiscConfigurationDetails(vm.MiscConfigurationViewModel) == true)
                CreateUserManagementViewModel(model);
            Task.Run(() =>
            {
                apihelper.DefaultEstimationHourModelRefreshList(); //refreh the cache to get latest from DB.
            });
            SaveHolidayConfiguration(vm.HolidayConfigAdminViewModel);
            SaveNPAConfiguration(vm.NPAConfigViewModel);
            SaveCreateRoleVM(vm.CreateRoleViewModel);
            SaveModifyRoleVM(vm.ModifyRoleViewModel);
            SaveMessageConfiguration(vm.MessageConfigAdminViewModel, vm.LoggedInUser);
            string ret = JsonConvert.SerializeObject(model.DefaultHoursViewModel);

            return RedirectToAction("AdminMain", new { selectedProjectType = int.Parse(vm.DefaultHoursViewModel.SelectedProjectType) });
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(List<int> HolidayIds, string LoggedInUserEmail)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            AdminViewModel vm = new AdminViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(vm);

            var rows = apihelper.DeleteHoliday(HolidayIds, vm.LoggedInUser.ID);
            if (rows != 0)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DefaultHoursSwitchProjectType(string propertyType, string LoggedInUserEmail)
        {
            AdminViewModel vm = new AdminViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = _loggedinUser;
            }
            else
            {
                vm.LoggedInUserEmail = LoggedInUserEmail;
            }

            UpdateUserAndPermissions(vm);

            vm.DefaultHoursViewModel = GetDefaultHoursViewModel(int.Parse(propertyType));
            return PartialView("_DefaultHours", vm);
            //return Json(ret, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult SaveUserAutoFacilitatorSelections(UserManagementViewModel m)
        {
            _loggedinUser = GetLoggedInUserEmailAddress();
            m.LoggedInUserEmail = _loggedinUser;

            SaveUserOptionsTabDetails(m);
            return View(); //no need to send back details since this will be the same as what came in.
        }


        [HttpPost]
        public ActionResult CheckIfUserExists(string FirstName, string LastName, string Email, string ADAccount, string UserName)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            UserIdentity userIdentity = new UserIdentity();
            userIdentity.FirstName = FirstName;
            userIdentity.LastName = LastName;
            userIdentity.Email = Email;
            userIdentity.ADName = ADAccount;
            userIdentity.UserName = UserName;

            bool exists = false;
            //see if this username already exists
            UserIdentity existingUser = apihelper.GetUserIdentityByEmailSysRef(UserName, (int)ExternalSystemEnum.Accela);
            if (existingUser.ID != 0)
            {
                exists = true;
            }
            else
            {
                exists = apihelper.GetUserIdentityByUserBE(userIdentity);

            }
            if (exists == true)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ValidateEmailAndUserName(string Email, string UserName, int UserId)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            //see if this email and/or username already exists

            UserIdentity existingUserName = apihelper.GetUserIdentityByUserName(UserName);
            if (existingUserName == null) existingUserName = new UserIdentity();

            UserIdentity existingUserEmail = apihelper.GetUserIdentityByEmailSysRef(Email, (int)ExternalSystemEnum.Accela);
            if (existingUserEmail == null) existingUserEmail = new UserIdentity();

            string response = string.Empty;

            if (existingUserName.ID != 0
                && existingUserName.ID != UserId
                && existingUserEmail.ID != 0
                && existingUserEmail.ID != UserId)
            {
                response = "User name is in use!";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSelectedUserConfigurationDetails(string userID, string loggedInUserEmail, string filterByType, string selectedUserSearchFilter)//string userID, string selProjectTypes)
        {
            AdminViewModel model = new AdminViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            model.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(loggedInUserEmail))
            {
                model.LoggedInUserEmail = _loggedinUser;
            }
            else
            {
                model.LoggedInUserEmail = loggedInUserEmail;
            }

            UpdateUserAndPermissions(model);

            model.UserManagementViewModel.SelectedUserTypeFilter = filterByType;
            model.UserManagementViewModel.SelectedUserSearchFilter = selectedUserSearchFilter;
            model.UserManagementViewModel.SelectedUser = userID;
            UserManagementViewModel usermodel = CreateUserManagementViewModel(model, int.Parse(userID));
            model.UserManagementViewModel = usermodel;
            return PartialView("_UserManagement", model);
        }

        [HttpPost]
        public ActionResult ApplyFilterToUsersUsers(string filterType, string searchText)
        {
            AdminViewModel model = new AdminViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            model.LoggedInUserEmail = _loggedinUser;

            var lst = UserAPIHelper.GetUsersByFilterModeUserManagement(searchText, filterType).OrderBy(x => x.LastName).ThenBy(y => y.FirstName).ToList();
            if (lst.Count > 0)
            {
                model.UserManagementViewModel.UserNameList.Add(new SelectListItem() { Text = "Select a user", Value = "0" });
            }
            else
            {
                model.UserManagementViewModel.UserNameList.Add(new SelectListItem() { Text = "Search for a user", Value = "0" });
            }
            model.UserManagementViewModel.UserNameList.AddRange(lst.Select(x => new SelectListItem() { Text = x.LastName + ", " + x.FirstName, Value = x.ID.ToString() }));

            return PartialView("_UserDropdown", model);
        }

        public ActionResult SaveDefaultHoursAndReload(string jsonModel)
        {
            AdminViewModel model = JsonConvert.DeserializeObject<AdminViewModel>(jsonModel);
            _loggedinUser = GetLoggedInUserEmailAddress();
            model.LoggedInUserEmail = _loggedinUser;

            if (string.IsNullOrWhiteSpace(model.LoggedInUserEmail))
            {
                model.LoggedInUserEmail = _loggedinUser;
            }

            UpdateUserAndPermissions(model);

            return PartialView("_DefaultHours", model.DefaultHoursViewModel);
        }

        public ActionResult GetTableAuditLogs()
        {
            return View();
        }

        public ActionResult GetMessageTemplateById(int id)
        {
            MessageTemplate messageTemplate = AdminAPIHelper.GetMessageTemplateById(id);
            return Json(messageTemplate);
        }
        public ActionResult GetMessageTemplatesByTypeId(int id)
        {
            List<MessageTemplate> messageTemplates = AdminAPIHelper.GetMessageTemplatesByTypeId(id);
            return Json(messageTemplates);
        }

        public ActionResult IsDuplicateTemplateName(string name)
        {
            bool isDuplicateTemplateName = true;
            if (!string.IsNullOrWhiteSpace(name))
            {
                List<MessageTemplate> messageTemplates = AdminAPIHelper.GetMessageTemplatesByTypeId(-1);
                isDuplicateTemplateName = messageTemplates.Where(x => x.TemplateName.Trim() == name.Trim()).Any();

            }
            return Json(new { isDuplicateTemplateName = isDuplicateTemplateName }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private Methods
        #endregion Private Methods


        #region User Management tab

        private bool SaveUserOptionsTabDetails(UserManagementViewModel vmUser)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vmUser.LoggedInUserEmail = _loggedinUser;

            bool dataupdated = false;

            bool delExpressRole = false;
            bool isSchedulableN = false;
            bool isExpressSchedulableN = false;
            bool isPlanreviewer = false;
            int selectedUserId = int.Parse(vmUser.SelectedUser);
            //incase no user is searched and updated and happens when only other tabs are updated.
            if (selectedUserId == 0)
            {


            }
            else
            {
                List<Reviewer> AllReviewers = _model != null ? _model.AllReviewers : UserAPIHelper.GetAllReviewers();
                foreach (var reviewer in AllReviewers)
                {
                    if (int.Parse(vmUser.SelectedUser) == reviewer.ID)
                    {
                        isPlanreviewer = true;
                    }
                }
                UserManagementViewModel usermodel = CreateUserManagementViewModel(new AdminViewModel(), selectedUserId);
                List<int> toDel = new List<int>(), toAdd = new List<int>();
                foreach (var item in vmUser.SelectedFacilitatorProjectTypes)
                {
                    if (!usermodel.SelectedFacilitatorProjectTypes.Where(x => x == item).Any())
                    {
                        toAdd.Add(int.Parse(item));
                    }
                }
                foreach (var item in usermodel.SelectedFacilitatorProjectTypes)
                {
                    if (!vmUser.SelectedFacilitatorProjectTypes.Where(x => x == item).Any())
                    {
                        toDel.Add(int.Parse(item));
                    }
                }

                //add new rolemappings if any
                if (toAdd.Count > 0)
                {
                    apihelper.CreateRoleMappings(selectedUserId, toAdd);
                    dataupdated = true;
                }
                //remove unchecked rolemappings if any
                if (toDel.Count > 0)
                {
                    apihelper.DeleteRoleMappings(selectedUserId, toDel);
                    dataupdated = true;
                }
                List<SystemRole> currentRoles = apihelper.GetSystemRolesByUserId(selectedUserId);
                List<SystemRole> allRoles = APIHelper.GetSystemRoles();

                List<int> delRoles = new List<int>();
                List<int> addRoles = new List<int>();
                foreach (var item in vmUser.SelectedRoles)
                {
                    var cur = currentRoles.Where(x => x.ID == int.Parse(item)).FirstOrDefault();
                    if (cur == null)
                    {
                        var adrole = allRoles.Where(x => x.ID == int.Parse(item)).FirstOrDefault();
                        if (adrole != null)
                            addRoles.Add(adrole.ID);
                    }
                }
                List<SelectListItem> rolelst = GetAllRolesList();
                foreach (var item in currentRoles)
                {
                    if (vmUser.SelectedRoles.Any(x => int.Parse(x) == item.ID) == false)
                    {
                        if (rolelst.Any(x => int.Parse(x.Value) == item.ID) == true)
                            delRoles.Add(item.ID);
                        if (item.RoleName == "Plan_Reviewer")
                            delExpressRole = true;
                    }
                }
                if (delRoles.Count > 0)
                    apihelper.DeleteRoleMappings(selectedUserId, delRoles);
                if (addRoles.Count > 0)
                    apihelper.CreateRoleMappings(selectedUserId, addRoles);
                UserProjectTypeXref projectTyplst = new UserProjectTypeXref();
                projectTyplst.UserID = int.Parse(vmUser.SelectedUser);
                projectTyplst.UpdatedUserId = apihelper.GetUserIdentityByEmailSysRef(_loggedinUser, (int)ExternalSystemEnum.Accela).ID;
                projectTyplst.TimeStamp = DateTime.Now;
                foreach (var item in vmUser.SelectedProjectTypes)
                {
                    projectTyplst.ProjectTypeIDList.Add(int.Parse(item));
                }

                //save the jurisdiction list
                //LES-3604 - jcl - altering this to only return one, leaving the ability to have more than one
                List<string> jurisdictionlist = new List<string>();
                jurisdictionlist.Add(vmUser.SelectedJurisdiction);

                UserDepartmentXref projectDeptlst = new UserDepartmentXref();
                projectDeptlst.UserID = int.Parse(vmUser.SelectedUser);
                projectDeptlst.UpdatedUserId = apihelper.GetUserIdentityByEmailSysRef(_loggedinUser, (int)ExternalSystemEnum.Accela).ID;
                projectDeptlst.TimeStamp = DateTime.Now;
                //LES-4564 jcl - these enums have the same enum value as DepartmentNameEnums except Zoning and Fire
                //  Zoning and Fire need to be mapped using the Jurisdiction
                foreach (var item in vmUser.SelectedTradeAgency)
                {
                    UserAdminViewModelDeptNameEnum enm = (UserAdminViewModelDeptNameEnum)int.Parse(item);
                    JurisdictionEnum jurisdictionEnum = (JurisdictionEnum)int.Parse(vmUser.SelectedJurisdiction);

                    if (enm != UserAdminViewModelDeptNameEnum.Zoning && enm != UserAdminViewModelDeptNameEnum.Fire)
                    {
                        //add the enum value to the department list
                        projectDeptlst.UserDepartmentIDList.Add(int.Parse(item));

                    }
                    else
                    {
                        //get the right jurisdictions 
                        projectDeptlst.UserDepartmentIDList.Add(AdminHelper.GetDepartmentNameEnumsByJurisdiction(jurisdictionEnum, enm));
                    }
                }

                //update express schedulable
                UserIdentity user = apihelper.GetUserIdentityByID(int.Parse(vmUser.SelectedUser));
                if (user.IsSchedulable && vmUser.IsSchedulableSelected == "N")
                {
                    isSchedulableN = true;
                }
                if (user.IsExpressSched && vmUser.ExpressSelected == "N")
                {
                    isExpressSchedulableN = true;
                }
                user.Notes = vmUser.Notes;
                user.FirstName = vmUser.FirstName;
                user.LastName = vmUser.LastName;
                user.ADName = vmUser.AdAccount;
                user.Email = vmUser.Email;
                user.UserName = vmUser.UserName;
                user.SchedulableLevel = LevelorOccupancy(vmUser.LevelSelected, vmUser.SelectedUser);

                user.HoursEstimated = vmUser.SelectedHoursEstimated;
                user.IsSchedulable = vmUser.IsSchedulableSelected == "Y" ? true : false;
                user.IsActive = vmUser.IsActiveSelected == "Y" ? true : false;
                user.IsExpressSched = vmUser.ExpressSelected == "Y" ? true : false;
                user.IsPrelimMeetingAllowed = vmUser.PriliminaryMeetingSelected == "Y" ? true : false;
                if (vmUser.PlanReviewHoursOverride != null)
                {
                    user.PlanReviewOverrideHours = decimal.Parse(vmUser.PlanReviewHoursOverride);
                }
                user.IsCity = vmUser.IsCityUserSelected == "Y" ? true : false;
                user.Phone = vmUser.Phone;
                apihelper.UpdateUser(user);
                apihelper.UpdateUserProjectTypeXRef(projectTyplst);

                apihelper.UpdateUserDepartmentXRef(projectDeptlst);
                AdminAPIHelper.SaveUserJurisdiction(userId: user.ID, jurisdictionList: jurisdictionlist, wrkId: projectTyplst.UpdatedUserId.ToString());

                if (isPlanreviewer && (delExpressRole || isSchedulableN || isExpressSchedulableN))
                {
                    apihelper.SendInactivePlanReviewerEmail(selectedUserId);
                }
                //refresh the role mappings again.

                //update express reserved reviewer config
                ExpressAPIHelper.UpdateExpressReviewerRotation();
            }
            return dataupdated;
        }

        private string LevelorOccupancy(string level, string userId)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            if (level == "Level1" || level == "Level2" || level == "Level3")
            {
                apihelper.DeleteOccupancy(Convert.ToInt32(userId));
                return level;
            }
            else
            {
                return " ";
            }
        }

        private UserManagementViewModel CreateUserManagementViewModel(AdminViewModel vm, int? userId = null)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedinUser;

            UserManagementViewModel ret = new UserManagementViewModel();
            MiscConfigurationViewModel miscmodel = new MiscConfigurationViewModel();
            //if this is an ajax call then need to init the misc configuration to get the list of auto projecttypes
            if (vm != null && vm.MiscConfigurationViewModel != null && vm.MiscConfigurationViewModel.FacilitatorAutoAssignableProjectTypes.Count > 0)
            { miscmodel = vm.MiscConfigurationViewModel; }
            else
            { miscmodel = CreateMiscConfigurationViewModel(); }
            ret.SelectedUserTypeFilter = vm.UserManagementViewModel.SelectedUserTypeFilter;
            ret.SelectedUserSearchFilter = vm.UserManagementViewModel.SelectedUserSearchFilter;
            ret.SelectedUser = vm.UserManagementViewModel.SelectedUser;

            //create a list of ids for system roles.
            List<int> systemroleIds = miscmodel.FacilitatorAutoAssignableProjectTypes.Select(x => int.Parse(x.Value)).ToList<int>();

            ret.ProjectTypeList = GetAllProjectTypesList();
            ret.TradeAgencyList = GetAllDepartmentList();
            ret.JurisdictionList = GetAllJurisdictionList();
            ret.UserTypeFilterList = GetAllUserTypeFilterList();
            ret.RoleList = GetAllRolesList();
            ret.PlanReviewHoursOverride = "0";

            if (userId.HasValue == true)
            {
                if (!string.IsNullOrEmpty(ret.SelectedUserSearchFilter) || !string.IsNullOrEmpty(ret.SelectedUserTypeFilter))
                {
                    List<UserIdentity> users = UserAPIHelper.GetUsersByFilterModeUserManagement(ret.SelectedUserSearchFilter, ret.SelectedUserTypeFilter);
                    //if nothing provided then sets the first user id as default selection.
                    if (userId.HasValue == false && users.Count > 0)
                        userId = users[0].ID;
                    else if (users.Any(x => x.ID == userId) == false && users.Count > 0)
                        userId = users[0].ID;
                    foreach (var item in users.OrderBy(x => x.LastName).ThenBy(y => y.FirstName).ToList())
                    {
                        ret.UserNameList.Add(new SelectListItem() { Text = item.LastName + ", " + item.FirstName, Value = item.ID.ToString(), Selected = (item.ID == userId) });
                    }
                }


                //get details for the selected user.
                UserIdentity currentusr = apihelper.GetUserIdentityByID(userId.Value);
                ret.FirstName = currentusr.FirstName;
                ret.LastName = currentusr.LastName;
                ret.UserName = currentusr.UserName;
                ret.Phone = currentusr.Phone;
                ret.Email = currentusr.Email;
                ret.AdAccount = currentusr.ADName;
                ret.Notes = currentusr.Notes;
                ret.LevelSelected = currentusr.SchedulableLevel;
                ret.IsActiveSelected = currentusr.IsActive == true ? "Y" : "N";
                ret.ExpressSelected = currentusr.IsExpressSched == true ? "Y" : "N";
                ret.PriliminaryMeetingSelected = currentusr.IsPrelimMeetingAllowed == true ? "Y" : "N";
                ret.IsSchedulableSelected = currentusr.IsSchedulable == true ? "Y" : "N";
                ret.SelectedHoursEstimated = currentusr.HoursEstimated;
                ret.PlanReviewHoursOverride = currentusr.PlanReviewOverrideHours.ToString("G29");
                List<SystemRole> currentRoles = apihelper.GetSystemRolesByUserId(userId.Value);
                ret.SelectedRoles = currentRoles.Select(x => x.ID.ToString()).ToList();

                //select user roles which are only related to auto facilitator assignment project type.
                List<SystemRole> autofaclst = currentRoles.Where(x => systemroleIds.Contains(x.ID)).ToList<SystemRole>();
                foreach (var item in miscmodel.FacilitatorAutoAssignableProjectTypes)
                {
                    if (autofaclst.Any(x => x.ID == int.Parse(item.Value)) == true)
                        ret.SelectedFacilitatorProjectTypes.Add(item.Value);
                }
                ret.SelectedProjectTypes = GetSelectedProjectTypeList(userId.Value);
                ret.SelectedTradeAgency = GetSelectedTradeAgencyList(userId.Value);
                //LES-3604 - jcl - altering this to only return one, leaving the ability to have more than one
                ret.SelectedJurisdiction = GetSelectedJurisdictionList(userId.Value).FirstOrDefault();
                ret.IsCityUserSelected = currentusr.IsCity == true ? "Y" : "N";
                ret.UserPrincipalName = currentusr.UserPrincipalName;
                ret.CalendarId = currentusr.CalendarId;
                return ret;

            }
            else
            {
                ret.UserNameList.Add(new SelectListItem() { Text = "Search for a User", Value = "0" });
                return ret;
            }

        }

        private List<string> GetSelectedTradeAgencyList(int userID)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            Helper helper = new Helper();
            string zoning = ((int)UserAdminViewModelDeptNameEnum.Zoning).ToString();
            string fire = ((int)UserAdminViewModelDeptNameEnum.Fire).ToString();
            // LES-4564 jcl get the right enums for the UserAdminViewModelDeptNameEnum
            //fire and zoning need to be added
            //when this is saved, the jurisdiction is used to map the correct business ref ids
            List<string> depts = apihelper.GetSelectedDepartmentXrefList(userID).UserDepartmentIDList.Select(x => x.ToString()).ToList();
            bool hasFire = depts.Any(x => helper.FireDepartmentNames.Contains((DepartmentNameEnums)int.Parse(x)));
            bool hasZoning = depts.Any(x => helper.ZoneDepartmentNames.Contains((DepartmentNameEnums)int.Parse(x)));

            if (hasZoning)
            {
                depts.Add(zoning);
            }
            if (hasFire)
            {
                depts.Add(fire);
            }

            return depts;
        }

        private List<string> GetSelectedProjectTypeList(int userID)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            return apihelper.GetSelectedProjectTypeXrefList(userID).ProjectTypeIDList.Select(x => x.ToString()).ToList();
        }

        private List<string> GetSelectedJurisdictionList(int userID)
        {
            return AdminAPIHelper.GetJurisdictionListByUser(userID);
        }
        List<SelectListItem> GetAllProjectTypesList(bool excludeNA = true)
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(GetProjectType(PropertyTypeEnums.Express));
            ret.Add(GetProjectType(PropertyTypeEnums.Commercial));
            ret.Add(GetProjectType(PropertyTypeEnums.Mega_Multi_Family));
            ret.Add(GetProjectType(PropertyTypeEnums.Special_Projects_Team));
            ret.Add(GetProjectType(PropertyTypeEnums.Townhomes));
            ret.Add(GetProjectType(PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home));
            ret.Add(GetProjectType(PropertyTypeEnums.FIFO_Master_Plans));
            ret.Add(GetProjectType(PropertyTypeEnums.FIFO_Single_Family_Homes));
            ret.Add(GetProjectType(PropertyTypeEnums.FIFO_Small_Commercial));
            ret.Add(GetProjectType(PropertyTypeEnums.County_Fire_Shop_Drawings));
            if (excludeNA == false)
                ret.Add(GetProjectType(PropertyTypeEnums.NA));
            return ret;
        }

        List<SelectListItem> GetAllRolesList()
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            if (_systemRoles == null) _systemRoles = APIHelper.GetSystemRoles();

            foreach (SystemRole item in _systemRoles)
            {
                //exclude NA
                if (item.ID > 0 && item.Enabled == true)
                {
                    if (item.EnumMappingValNbr.HasValue && item.EnumMappingValNbr.Value > 0)
                    {
                        ret.Add(new SelectListItem
                        {
                            Text = item.SystemRoleEnum.ToStringValue(),
                            Value = item.ID.ToString()
                        });
                    }
                    else
                    {
                        ret.Add(new SelectListItem
                        {
                            Text = item.RoleName,
                            Value = item.ID.ToString()
                        });
                    }
                }

            };
            return ret;
        }

        List<SelectListItem> GetAllUserTypeFilterList()
        {
            //keeping it string since there are a mix of Enums.
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(GetSelectedListItem("All", "All"));
            ret.Add(GetSelectedListItem("Building", DepartmentNameEnums.Building.ToString()));
            ret.Add(GetSelectedListItem("Electrical", DepartmentNameEnums.Electrical.ToString()));
            ret.Add(GetSelectedListItem("Mechanical", DepartmentNameEnums.Mechanical.ToString()));
            ret.Add(GetSelectedListItem("Plumbing", DepartmentNameEnums.Plumbing.ToString()));
            ret.Add(GetSelectedListItem("Zoning", DepartmentNameEnums.Zone_Cornelius.ToString()));
            ret.Add(GetSelectedListItem("Fire", DepartmentNameEnums.Fire_Cornelius.ToString()));
            ret.Add(GetSelectedListItem("Backflow", DepartmentNameEnums.Backflow.ToString()));
            ret.Add(GetSelectedListItem("Food Service", DepartmentNameEnums.EH_Food.ToString()));
            ret.Add(GetSelectedListItem("Public Pool", DepartmentNameEnums.EH_Pool.ToString()));
            ret.Add(GetSelectedListItem("Facility / Lodging", DepartmentNameEnums.EH_Facilities.ToString()));
            ret.Add(GetSelectedListItem("Day Care", DepartmentNameEnums.EH_Day_Care.ToString()));
            ret.Add(GetSelectedListItem("Estimator", SystemRoleEnum.Estimator.ToString()));
            ret.Add(GetSelectedListItem("Manager", SystemRoleEnum.Manager.ToString()));
            ret.Add(GetSelectedListItem("Facilitator", SystemRoleEnum.Facilitator.ToString()));
            ret.Add(GetSelectedListItem("View Only", SystemRoleEnum.View_Only.ToString()));
            return ret;
        }



        List<SelectListItem> GetAllJurisdictionList()
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(GetSelectedListItem(JurisdictionEnum.County.ToStringValue(), ((int)JurisdictionEnum.County).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Charlotte.ToStringValue(), ((int)JurisdictionEnum.Charlotte).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Cornelius.ToStringValue(), ((int)JurisdictionEnum.Cornelius).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Davidson.ToStringValue(), ((int)JurisdictionEnum.Davidson).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Huntersville.ToStringValue(), ((int)JurisdictionEnum.Huntersville).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Matthews.ToStringValue(), ((int)JurisdictionEnum.Matthews).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Mint_Hill.ToStringValue(), ((int)JurisdictionEnum.Mint_Hill).ToString()));
            ret.Add(GetSelectedListItem(JurisdictionEnum.Pineville.ToStringValue(), ((int)JurisdictionEnum.Pineville).ToString()));
            return ret;
        }
        List<SelectListItem> GetAllDepartmentList()
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Building.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Building).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Electrical.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Electrical).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Mechanical.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Mechanical).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Plumbing.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Plumbing).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Zoning.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Zoning).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Fire.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Fire).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.Backflow.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.Backflow).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.EH_Food.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.EH_Food).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.EH_Pool.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.EH_Pool).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.EH_Facilities.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.EH_Facilities).ToString()));
            ret.Add(GetSelectedListItem(UserAdminViewModelDeptNameEnum.EH_Day_Care.ToStringValue(), ((int)UserAdminViewModelDeptNameEnum.EH_Day_Care).ToString()));
            return ret;
        }

        SelectListItem GetSelectedListItem(string text, string value)
        {
            return new SelectListItem() { Text = text, Value = value };
        }

        SelectListItem GetSelectedListItem(string text, int value)
        {
            return new SelectListItem() { Text = text, Value = value.ToString() };
        }

        SelectListItem GetProjectType(PropertyTypeEnums propertyType)
        {
            SelectListItem ret = new SelectListItem();
            switch (propertyType)
            {
                case PropertyTypeEnums.Express:
                    return GetSelectedListItem(PropertyTypeEnums.Express.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.Commercial:
                    return GetSelectedListItem(PropertyTypeEnums.Commercial.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.Mega_Multi_Family:
                    return GetSelectedListItem(PropertyTypeEnums.Mega_Multi_Family.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.Special_Projects_Team:
                    return GetSelectedListItem(PropertyTypeEnums.Special_Projects_Team.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.Townhomes:
                    return GetSelectedListItem(PropertyTypeEnums.Townhomes.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.FIFO_Small_Commercial:
                    return GetSelectedListItem(PropertyTypeEnums.FIFO_Small_Commercial.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                    return GetSelectedListItem(PropertyTypeEnums.FIFO_Single_Family_Homes.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.FIFO_Master_Plans:
                    return GetSelectedListItem(PropertyTypeEnums.FIFO_Master_Plans.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    return GetSelectedListItem(PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home.ToStringValue(), ((int)propertyType).ToString());
                case PropertyTypeEnums.County_Fire_Shop_Drawings:
                    return GetSelectedListItem(PropertyTypeEnums.County_Fire_Shop_Drawings.ToStringValue(), ((int)propertyType).ToString());
                default:
                    return GetSelectedListItem(PropertyTypeEnums.NA.ToStringValue(), ((int)propertyType).ToString());
            }
        }

        [HttpGet]
        public ActionResult DebugFIFOOptimizer()
        {
            APIHelper apihelper = new Helpers.APIHelper();

            bool success = apihelper.OptimizeFIFOProjects();

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Misc Configuration

        private bool SaveMiscConfigurationDetails(MiscConfigurationViewModel model)
        {
            APIHelper apihelper = new APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();

            UserIdentity updateUser = apihelper.GetUserIdentityByEmailSysRef(_loggedinUser, (int)ExternalSystemEnum.Accela);
            bool dataUpdated = false;
            //Facilitator Auto Assign
            string toManual = "", toAuto = "";
            MiscConfigurationViewModel oldmodel = CreateMiscConfigurationViewModel();
            //get each project type ref id in a string csv
            foreach (var item in model.SelectedFacilitatorAutoAssignableProjectTypes)
            {
                if (!oldmodel.FacilitatorAutoAssignableProjectTypes.Where(x => x.Value == item).Any())
                {
                    toAuto += item + ",";
                }
            }
            //get each project type ref id in a string csv
            foreach (var item in model.SelectedFacilitatorManualProjectTypes)
            {
                if (!oldmodel.FacilitatorManualProjectTypes.Where(x => x.Value == item).Any())
                {
                    toManual += item + ",";
                }
            }
            //add new auto assign facilitator if any
            //send csv list of auto assigned 
            if (!string.IsNullOrWhiteSpace(toAuto))
            {
                AdminAPIHelper.UpdateAutoAssignFacilitator(toAuto, true, updateUser.ID.ToString());
                dataUpdated = true;
            }
            //remove unchecked auto assigned if any
            //send csv list of manual assigned
            if (!string.IsNullOrWhiteSpace(toManual))
            {
                AdminAPIHelper.UpdateAutoAssignFacilitator(toManual, false, updateUser.ID.ToString());
                dataUpdated = true;
            }
            List<PlanReviewerAvailableHour> hours = _model != null ? _model.PlanReviewerAvailableHours : apihelper.GetAllPlanReviewerHours();
            List<PlanReviewerAvailableHour> updatedhrs = new List<PlanReviewerAvailableHour>();
            foreach (var item in hours)
            {
                switch (item.EnumMappingValNbr)
                {
                    case PlanReviewHourTypes.HoursPlanReviewerMMF:
                        if (decimal.Parse(model.HoursPlanReviewerMMF.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursPlanReviewerMMF;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursMMF:
                        if (decimal.Parse(model.HoursMMF.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursMMF;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursCountyFire:
                        if (decimal.Parse(model.HoursCountyFire.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursCountyFire;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursExpress:
                        if (decimal.Parse(model.HoursExpress.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursExpress;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursFIFOSmComm:
                        if (decimal.Parse(model.HoursFIFOSmComm.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursFIFOSmComm;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursFIFOSingleFH:
                        if (decimal.Parse(model.HoursFIFOSingleFH.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursFIFOSingleFH;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursFIFOMsPln:
                        if (decimal.Parse(model.HoursFIFOMsPln.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursFIFOMsPln;
                            updatedhrs.Add(item);
                        }
                        break;
                    case PlanReviewHourTypes.HoursFIFOAddRenSFH:
                        if (decimal.Parse(model.HoursFIFOAddRenSFH.ToString("G29")) != decimal.Parse(item.AvailableHours.ToString("G29")))
                        {
                            item.AvailableHours = model.HoursFIFOAddRenSFH;
                            updatedhrs.Add(item);
                        }
                        break;
                    default:
                        break;
                }
            }
            foreach (var item in updatedhrs)
            {
                item.UpdatedUser = updateUser;
                apihelper.UpdatePlanReviewAvailableHours(item);
            }

            List<PlanReviewerAvailableTime> times = _model != null ? _model.PlanReviewerAvailableTimes : apihelper.GetAllPlanReviewerTimes();
            List<PlanReviewerAvailableTime> updatedtimes = new List<PlanReviewerAvailableTime>();
            foreach (var item in times)
            {
                switch (item.ProjectTypeRefID)
                {
                    case PropertyTypeEnums.Commercial:
                        item.AvailableStartTime = model.StartTimeCommercial;
                        item.AvailableEndTime = model.EndTimeCommercial;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.Mega_Multi_Family:
                        item.AvailableStartTime = model.StartTimeMMF;
                        item.AvailableEndTime = model.EndTimeMMF;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                        item.AvailableStartTime = model.StartTimeFIFO;
                        item.AvailableEndTime = model.EndTimeFIFO;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.FIFO_Master_Plans:
                        item.AvailableStartTime = model.StartTimeFIFO;
                        item.AvailableEndTime = model.EndTimeFIFO;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.FIFO_Single_Family_Homes:
                        item.AvailableStartTime = model.StartTimeFIFO;
                        item.AvailableEndTime = model.EndTimeFIFO;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.FIFO_Small_Commercial:
                        item.AvailableStartTime = model.StartTimeFIFO;
                        item.AvailableEndTime = model.EndTimeFIFO;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.Special_Projects_Team:
                        item.AvailableStartTime = model.StartTimeSpecialTeams;
                        item.AvailableEndTime = model.EndTimeSpecialTeams;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.Townhomes:
                        item.AvailableStartTime = model.StartTimeTownhomes;
                        item.AvailableEndTime = model.EndTimeTownhomes;
                        updatedtimes.Add(item);
                        break;
                    case PropertyTypeEnums.County_Fire_Shop_Drawings:
                        item.AvailableStartTime = model.StartTimeCSD;
                        item.AvailableEndTime = model.EndTimeCSD;
                        updatedtimes.Add(item);
                        break;
                    default:
                        break;
                }
            }
            foreach (var item in updatedtimes)
            {
                item.UpdatedUser = updateUser;
                apihelper.UpdatePlanReviewAvailableTimes(item);
            }

            if (oldmodel.ScheduleDateConfigurationCatalogItem.Value != model.ScheduleDateConfigurationCatalogItem.Value)
            {
                oldmodel.ScheduleDateConfigurationCatalogItem.Value = model.ScheduleDateConfigurationCatalogItem.Value;
                oldmodel.ScheduleDateConfigurationCatalogItem.UpdatedUser = updateUser;

                UpdateCatalogItem(oldmodel.ScheduleDateConfigurationCatalogItem, apihelper);
            };

            if (oldmodel.SameBuildingContractorReviewDaysCatalogItem.Value != model.SameBuildingContractorReviewDaysCatalogItem.Value)
            {
                oldmodel.SameBuildingContractorReviewDaysCatalogItem.Value = model.SameBuildingContractorReviewDaysCatalogItem.Value;
                oldmodel.SameBuildingContractorReviewDaysCatalogItem.UpdatedUser = updateUser;

                UpdateCatalogItem(oldmodel.SameBuildingContractorReviewDaysCatalogItem, apihelper);
            }
            if (oldmodel.CancellationFeePerHourCatalogItem.Value != model.CancellationFeePerHourCatalogItem.Value)
            {
                oldmodel.CancellationFeePerHourCatalogItem.Value = model.CancellationFeePerHourCatalogItem.Value;
                oldmodel.CancellationFeePerHourCatalogItem.UpdatedUser = updateUser;

                UpdateCatalogItem(oldmodel.CancellationFeePerHourCatalogItem, apihelper);
            }
            return dataUpdated;
        }

        private void UpdateCatalogItem(CatalogItem catalogItem, APIHelper apiHelper = null)
        {
            if (apiHelper == null)
            {
                apiHelper = new APIHelper();
            }

            apiHelper.UpdateCatalogItem(catalogItem);
        }

        private MiscConfigurationViewModel CreateMiscConfigurationViewModel()
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            MiscConfigurationViewModel model = new MiscConfigurationViewModel();
            _loggedinUser = GetLoggedInUserEmailAddress();
            //LES-4519
            //get the auto assign facilitator from the property types 
            List<ProjectType> projectTypes = _model != null ? _model.ProjectTypeList : AdminAPIHelper.GetProjectTypeList();

            foreach (ProjectType item in projectTypes.Where(x => x.ProjectTypRefId > 0).ToList())
            {
                //if the auto assign is true, put the item in the auto assignable list
                if (item.AutoAssignFacilitator.Value == true)
                {
                    model.FacilitatorAutoAssignableProjectTypes.Add(new SelectListItem() { Text = item.ProjectTypeEnum.ToStringValue(), Value = item.ProjectTypRefId.ToString() });
                }
                else
                {
                    //else put in the manual list
                    model.FacilitatorManualProjectTypes.Add(new SelectListItem() { Text = item.ProjectTypeEnum.ToStringValue(), Value = item.ProjectTypRefId.ToString() });
                }
            }
            List<PlanReviewerAvailableHour> hours = _model != null ? _model.PlanReviewerAvailableHours : apihelper.GetAllPlanReviewerHours();
            foreach (var item in hours)
            {
                switch (item.EnumMappingValNbr)
                {
                    case PlanReviewHourTypes.HoursPlanReviewerMMF:
                        model.HoursPlanReviewerMMF = decimal.Parse(item.AvailableHours.ToString("G29"));// 6;
                        break;
                    case PlanReviewHourTypes.HoursMMF:
                        model.HoursMMF = decimal.Parse(item.AvailableHours.ToString("G29"));// 4;
                        break;
                    case PlanReviewHourTypes.HoursCountyFire:
                        model.HoursCountyFire = decimal.Parse(item.AvailableHours.ToString("G29"));// 4;
                        break;
                    case PlanReviewHourTypes.HoursExpress:
                        model.HoursExpress = decimal.Parse(item.AvailableHours.ToString("G29"));// 8;
                        break;
                    case PlanReviewHourTypes.HoursFIFOSmComm:
                        model.HoursFIFOSmComm = decimal.Parse(item.AvailableHours.ToString("G29"));// 6;
                        break;
                    case PlanReviewHourTypes.HoursFIFOSingleFH:
                        model.HoursFIFOSingleFH = decimal.Parse(item.AvailableHours.ToString("G29"));// 6;
                        break;
                    case PlanReviewHourTypes.HoursFIFOMsPln:
                        model.HoursFIFOMsPln = decimal.Parse(item.AvailableHours.ToString("G29"));// 6;
                        break;
                    case PlanReviewHourTypes.HoursFIFOAddRenSFH:
                        model.HoursFIFOAddRenSFH = decimal.Parse(item.AvailableHours.ToString("G29"));// 6;
                        break;
                    default:
                        break;
                }
            }

            List<PlanReviewerAvailableTime> times = _model != null ? _model.PlanReviewerAvailableTimes : apihelper.GetAllPlanReviewerTimes();
            foreach (var item in times)
            {
                switch (item.ProjectTypeRefID)
                {
                    case PropertyTypeEnums.Commercial:
                        model.StartTimeCommercial = item.AvailableStartTime;
                        model.EndTimeCommercial = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.Mega_Multi_Family:
                        model.StartTimeMMF = item.AvailableStartTime;
                        model.EndTimeMMF = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                        model.StartTimeFIFO = item.AvailableStartTime;
                        model.EndTimeFIFO = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.FIFO_Master_Plans:
                        model.StartTimeFIFO = item.AvailableStartTime;
                        model.EndTimeFIFO = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.FIFO_Single_Family_Homes:
                        model.StartTimeFIFO = item.AvailableStartTime;
                        model.EndTimeFIFO = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.FIFO_Small_Commercial:
                        model.StartTimeFIFO = item.AvailableStartTime;
                        model.EndTimeFIFO = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.Special_Projects_Team:
                        model.StartTimeSpecialTeams = item.AvailableStartTime;
                        model.EndTimeSpecialTeams = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.Townhomes:
                        model.StartTimeTownhomes = item.AvailableStartTime;
                        model.EndTimeTownhomes = item.AvailableEndTime;
                        break;
                    case PropertyTypeEnums.County_Fire_Shop_Drawings:
                        model.StartTimeCSD = item.AvailableStartTime;
                        model.EndTimeCSD = item.AvailableEndTime;
                        break;
                    default:
                        break;
                }
            }

            model.ScheduleDateConfigurationCatalogItem = RetrieveScheduleDateConfigurationFromCatalogItems();
            model.SameBuildingContractorReviewDaysCatalogItem = RetrieveSameBuildingContractorReviewDaysCatalogItem();
            model.CancellationFeePerHourCatalogItem = RetrieveCancellationFeeCatalogItem();
            return model;
        }

        private CatalogItem RetrieveScheduleDateConfigurationFromCatalogItems()
        {
            List<CatalogItem> catalogItems;
            APIHelper apihelper = new APIHelper();

            if (_model != null)
            {
                catalogItems = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION").ToList();
            }
            else
            {
                catalogItems = apihelper.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION");
            }

            if (catalogItems.Any() && catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION").FirstOrDefault().Value != null)
            {
                return catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION").FirstOrDefault();
            }

            return null;
        }

        private CatalogItem RetrieveSameBuildingContractorReviewDaysCatalogItem()
        {
            List<CatalogItem> catalogItems;
            APIHelper apihelper = new APIHelper();

            if (_model != null)
            {
                catalogItems = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "ADMIN.MISC_CONFIG.SAME_CONTRACTOR_DAYS").ToList();
            }
            else
            {
                catalogItems = apihelper.GetCatalogItems("ADMIN.MISC_CONFIG.SAME_CONTRACTOR_DAYS");
            }

            if (catalogItems.Any() && catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.SAME_CONTRACTOR_DAYS").FirstOrDefault().Value != null)
            {
                return catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.SAME_CONTRACTOR_DAYS").FirstOrDefault();
            }

            return null;
        }

        private CatalogItem RetrieveCancellationFeeCatalogItem()
        {
            List<CatalogItem> catalogItems;
            APIHelper apihelper = new APIHelper();

            if (_model != null)
            {
                catalogItems = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR").ToList();
            }
            else
            {
                catalogItems = apihelper.GetCatalogItems("ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR");
            }

            if (catalogItems.Any() && catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR").FirstOrDefault().Value != null)
            {
                return catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR").FirstOrDefault();
            }

            return null;
        }

        #endregion

        #region Holiday Configuration

        private bool SaveHolidayConfiguration(HolidayConfigAdminViewModel vm)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            _loggedinUser = GetLoggedInUserEmailAddress();

            if (vm.AddHoliday.HolidayNm == null)
                return true;
            apihelper.InsertHolidayConfig(vm.AddHoliday);
            return true;
        }



        #endregion

        #region Default Hours

        public bool SaveDefaultHoursViewModel(DefaultHoursAdminViewModel model)
        {
            try
            {
                Helpers.APIHelper apihelper = new Helpers.APIHelper();
                _loggedinUser = GetLoggedInUserEmailAddress();

                PropertyTypeEnums propertyTypeEnum = (PropertyTypeEnums)int.Parse(model.SelectedProjectType);

                DefaultEstimationHour hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Building, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = model.BuildingHrs;
                    hr.IsEnabled = model.BuildingHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.BuildingSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Electrical, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = model.ElectricHrs;
                    hr.IsEnabled = model.ElectricHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.ElectricSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Mechanical, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = model.MechHrs;
                    hr.IsEnabled = model.MechHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.MechSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Plumbing, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = model.PlumbHrs;
                    hr.IsEnabled = model.PlumbHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.PlumbSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                // County zoning is required to be kept individual for each town but then again if the town says it need to be picked by county zoning settings then app need to use a common value.
                // This cannot be kept in hours table and so will be stored in  catalog table instead.

                List<CatalogItem> catlogs;

                if (_model != null)
                {
                    catlogs = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "COUNTY.ZONING.DEFAULTS").ToList();
                }
                else
                {
                    catlogs = apihelper.GetCatalogItems("COUNTY.ZONING.DEFAULTS");
                }

                if (catlogs.Any())
                {
                    CatalogItem hrsitm = catlogs.Where(x => x.Key == "COUNTY.ZONING.DEFAULT.HOUR" && x.SubKey == propertyTypeEnum.ToString()).FirstOrDefault();
                    if (model.CountyZoneSelection == "Default")
                        hrsitm.Value = model.CountyZoneHrs.ToString();
                    else
                        hrsitm.Value = "0";
                    apihelper.UpdateCatalogItem(hrsitm);
                    CatalogItem hrsEnbld = catlogs.Where(x => x.Key == "COUNTY.ZONING.DEFAULT.HRS_ENABLED" && x.SubKey == propertyTypeEnum.ToString()).FirstOrDefault();
                    hrsEnbld.Value = model.CountyZoneHrsAllowed;
                    apihelper.UpdateCatalogItem(hrsEnbld);
                    CatalogItem hrsSelect = catlogs.Where(x => x.Key == "COUNTY.ZONING.DEFAULT.SELECTION" && x.SubKey == propertyTypeEnum.ToString()).FirstOrDefault();
                    hrsSelect.Value = model.CountyZoneSelection;
                    apihelper.UpdateCatalogItem(hrsSelect);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Cty_Chrlt, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = model.CityZoneHrs;
                    hr.IsEnabled = model.CityZoneHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.CityZoneSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_County, propertyTypeEnum);
                if (hr != null) //need to update all the cities in this case.
                {
                    hr.DefaultHours = (model.CountyFireHrs);
                    hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.CountyFireSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Davidson, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Huntersville, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Matthews, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Mint_Hill, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Pineville, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_UMC, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                    hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Cornelius, propertyTypeEnum);
                    if (hr != null)
                    {
                        hr.DefaultHours = (model.CountyFireHrs);
                        hr.IsEnabled = model.CountyFireHrsAllowed == "1" ? true : false;
                        hr.EstimationHoursMode = model.CountyFireSelection;
                        apihelper.UpdateDefaultEstimationHour(hr);
                    }
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Cty_Chrlt, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = (model.CityFireHrs);
                    hr.IsEnabled = model.CityFireHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.CityFireSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Day_Care, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = (model.EHSDayCareHrs);
                    hr.IsEnabled = model.EHSDayCareHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.EHSDayCareSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Food, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = (model.EHSFoodServiceHrs);
                    hr.IsEnabled = model.EHSFoodServiceHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.EHSFoodServiceSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Pool, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = (model.EHSPoolHrs);
                    hr.IsEnabled = model.EHSPoolHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.EHSPoolSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Facilities, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = (model.EHSLodgingHrs);
                    hr.IsEnabled = model.EHSLodgingHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.EHSLodgingSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Backflow, propertyTypeEnum);
                if (hr != null)
                {
                    hr.DefaultHours = (model.BackFlowHrs);
                    hr.IsEnabled = model.BackFlowHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.BackFlowSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_County, propertyTypeEnum);
                if (hr != null)
                {
                    UpdateZoneDefaultHours(hr, model.CorneliusZoneHrs, model.CorneliusZoneSelection,
                    model.CountyZoneSelection, model.CountyZoneHrs);
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Cornelius, propertyTypeEnum);
                if (hr != null)
                {
                    UpdateZoneDefaultHours(hr, model.CorneliusZoneHrs, model.CorneliusZoneSelection,
                    model.CountyZoneSelection, model.CountyZoneHrs);
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Davidson, propertyTypeEnum);
                if (hr != null)
                {
                    UpdateZoneDefaultHours(hr, model.DavidsonZoneHrs, model.DavidsonZoneSelection,
                    model.CountyZoneSelection, model.CountyZoneHrs);
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Huntersville, propertyTypeEnum);
                //LES-3407 use the values for Huntersville and Mint Hill instead of choosing the county hours
                //  these two zones should always default hours to 1 hour
                if (hr != null)
                {
                    hr.DefaultHours = model.HuntersvilleZoneHrs;
                    hr.IsEnabled = model.HuntersvilleZoneHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.HuntersvilleZoneSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Matthews, propertyTypeEnum);
                if (hr != null)
                {
                    UpdateZoneDefaultHours(hr, model.MatthewsZoneHrs, model.MatthewsZoneSelection,
                    model.CountyZoneSelection, model.CountyZoneHrs);
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Mint_Hill, propertyTypeEnum);
                //LES-3407 use the values for Huntersville and Mint Hill instead of choosing the county hours
                //  these two zones should always default hours to 1 hour
                if (hr != null)
                {
                    hr.DefaultHours = model.MintHillZoneHrs;
                    hr.IsEnabled = model.MintHillZoneHrsAllowed == "1" ? true : false;
                    hr.EstimationHoursMode = model.MintHillZoneSelection;
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Pineville, propertyTypeEnum);
                if (hr != null)
                {
                    UpdateZoneDefaultHours(hr, model.PinevilleZoneHrs, model.PinevilleZoneSelection,
                        model.CountyZoneSelection, model.CountyZoneHrs);
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_UMC, propertyTypeEnum);
                if (hr != null)
                {
                    //update for UMC sets as default to what ever the county zone setting.
                    UpdateZoneDefaultHours(hr, 0, TradeSelectOptionConsts.County.Value, model.CountyZoneSelection, model.CountyZoneHrs);
                    apihelper.UpdateDefaultEstimationHour(hr);
                }
                apihelper.DefaultEstimationHourModelRefreshList();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        DefaultEstimationHour UpdateZoneDefaultHours(DefaultEstimationHour hr, decimal zoneHrs, string zoneSelection, string countyZoneSelection, decimal countyZoneHrs)
        {
            if (zoneSelection == TradeSelectOptionConsts.Town)
            {
                hr.DefaultHours = zoneHrs;
                hr.IsEnabled = true;
                hr.EstimationHoursMode = TradeSelectOptionConsts.Town.Value; //"Town"
            }
            else if (zoneSelection == TradeSelectOptionConsts.County)
            {
                if (countyZoneSelection == TradeSelectOptionConsts.NA)
                {
                    hr.DefaultHours = 0;
                    hr.IsEnabled = false;
                    hr.EstimationHoursMode = TradeSelectOptionConsts.NA.Value; //"NA"
                }
                else if (countyZoneSelection == TradeSelectOptionConsts.Manual)
                {
                    hr.DefaultHours = 0;
                    hr.IsEnabled = false;
                    hr.EstimationHoursMode = TradeSelectOptionConsts.Manual.Value; //"Manual"
                }
                else // if (countyZoneSelection == TradeSelectOptionConsts.Default)
                {
                    hr.DefaultHours = countyZoneHrs;
                    hr.IsEnabled = true;
                    hr.EstimationHoursMode = TradeSelectOptionConsts.Default.Value; //"Default"
                }
            }
            return hr;
        }

        private DefaultHoursAdminViewModel GetDefaultHoursViewModel(int propertyType)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            PropertyTypeEnums propertyTypeEnum = (PropertyTypeEnums)propertyType;
            DefaultHoursAdminViewModel ret = new DefaultHoursAdminViewModel();

            ret.ProjectType = new List<SelectListItem>()
            {
                new SelectListItem() { Text = PropertyTypeEnums.County_Fire_Shop_Drawings.ToStringValue(), Value = ((int)PropertyTypeEnums.County_Fire_Shop_Drawings).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.Express.ToStringValue(), Value = ((int)PropertyTypeEnums.Express).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home.ToStringValue(), Value = ((int)PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.FIFO_Master_Plans.ToStringValue(), Value = ((int)PropertyTypeEnums.FIFO_Master_Plans).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.FIFO_Single_Family_Homes.ToStringValue(), Value = ((int)PropertyTypeEnums.FIFO_Single_Family_Homes).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.FIFO_Small_Commercial.ToStringValue(), Value = ((int)PropertyTypeEnums.FIFO_Small_Commercial).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.Mega_Multi_Family.ToStringValue(), Value = ((int)PropertyTypeEnums.Mega_Multi_Family).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.Commercial.ToStringValue(), Value = ((int)PropertyTypeEnums.Commercial).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.Special_Projects_Team.ToStringValue(), Value = ((int)PropertyTypeEnums.Special_Projects_Team).ToString() },
                new SelectListItem() { Text = PropertyTypeEnums.Townhomes.ToStringValue(), Value = ((int)PropertyTypeEnums.Townhomes).ToString() }
            };

            ret.TradeSelectOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.NA, Value = (string)TradeSelectOptionConsts.NA},
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.Manual, Value = (string)TradeSelectOptionConsts.Manual},
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.Default, Value = (string)TradeSelectOptionConsts.Default},

            };
            if (propertyTypeEnum == PropertyTypeEnums.Mega_Multi_Family) //needed only in case of MMF.
                ret.TradeSelectOptions.Add(new SelectListItem() { Text = (string)TradeSelectOptionConsts.Auto, Value = (string)TradeSelectOptionConsts.Auto });
            ret.AgencySelectOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.NA, Value = (string)TradeSelectOptionConsts.NA},
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.Manual, Value = (string)TradeSelectOptionConsts.Manual},
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.Default, Value = (string)TradeSelectOptionConsts.Default},
            };
            ret.ZoneConfigSelectOptions = new List<SelectListItem>()
            {
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.County, Value = (string)TradeSelectOptionConsts.County},
                new SelectListItem() { Text = (string)TradeSelectOptionConsts.Town, Value = (string)TradeSelectOptionConsts.Town},
            };

            ret.SelectedProjectType = propertyType.ToString();

            DefaultEstimationHour hr;

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Building);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Building, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.BuildingHrs = hr.DefaultHours;
                ret.BuildingHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.BuildingSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Electrical);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Electrical, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.ElectricHrs = hr.DefaultHours;
                ret.ElectricHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.ElectricSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Mechanical);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Mechanical, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.MechHrs = hr.DefaultHours;
                ret.MechHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.MechSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Plumbing);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Plumbing, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.PlumbHrs = hr.DefaultHours;
                ret.PlumbHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.PlumbSelection = hr.EstimationHoursMode;
            }
            //jcl this isn't a departmentnameenums value for County Zoning, so this is saved in the catalog table
            List<CatalogItem> catlogs;

            if (_model != null)
            {
                catlogs = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "COUNTY.ZONING.DEFAULTS").ToList();
            }
            else
            {
                catlogs = apihelper.GetCatalogItems("COUNTY.ZONING.DEFAULTS");
            }

            if (catlogs.Any())
            {
                ret.CountyZoneHrs = decimal.Parse(decimal.Parse(catlogs.Where(x => x.Key == "COUNTY.ZONING.DEFAULT.HOUR" && x.SubKey == propertyTypeEnum.ToString()).FirstOrDefault().Value).ToString("0.###############"));
                ret.CountyZoneHrsAllowed = catlogs.Where(x => x.Key == "COUNTY.ZONING.DEFAULT.HRS_ENABLED" && x.SubKey == propertyTypeEnum.ToString()).FirstOrDefault().Value;
                ret.CountyZoneSelection = catlogs.Where(x => x.Key == "COUNTY.ZONING.DEFAULT.SELECTION" && x.SubKey == propertyTypeEnum.ToString()).FirstOrDefault().Value;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_Cty_Chrlt);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Cty_Chrlt, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.CityZoneHrs = hr.DefaultHours;
                ret.CityZoneHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.CityZoneSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Fire_County);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_County, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.CountyFireHrs = hr.DefaultHours;
                ret.CountyFireHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.CountyFireSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Fire_Cty_Chrlt);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Fire_Cty_Chrlt, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.CityFireHrs = hr.DefaultHours;
                ret.CityFireHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.CityFireSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.EH_Day_Care);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Day_Care, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.EHSDayCareHrs = hr.DefaultHours;
                ret.EHSDayCareHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.EHSDayCareSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.EH_Food);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Food, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.EHSFoodServiceHrs = hr.DefaultHours;
                ret.EHSFoodServiceHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.EHSFoodServiceSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.EH_Pool);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Pool, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.EHSPoolHrs = hr.DefaultHours;
                ret.EHSPoolHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.EHSPoolSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.EH_Facilities);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.EH_Facilities, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.EHSLodgingHrs = hr.DefaultHours;
                ret.EHSLodgingHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.EHSLodgingSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Backflow);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Backflow, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.BackFlowHrs = hr.DefaultHours;
                ret.BackFlowHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.BackFlowSelection = hr.EstimationHoursMode;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_County);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_County, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.CorneliusZoneHrs = GetZoneHrs(hr.DefaultHours, hr.EstimationHoursMode);
                ret.CorneliusZoneHrsAllowed = hr.EstimationHoursMode == TradeSelectOptionConsts.Town == true ? "1" : "0";
                ret.CorneliusZoneSelection = hr.EstimationHoursMode == TradeSelectOptionConsts.Town ? TradeSelectOptionConsts.Town.Value : TradeSelectOptionConsts.County.Value;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_Davidson);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Davidson, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.DavidsonZoneHrs = GetZoneHrs(hr.DefaultHours, hr.EstimationHoursMode);
                ret.DavidsonZoneHrsAllowed = hr.EstimationHoursMode == TradeSelectOptionConsts.Town == true ? "1" : "0";
                ret.DavidsonZoneSelection = hr.EstimationHoursMode == TradeSelectOptionConsts.Town ? TradeSelectOptionConsts.Town.Value : TradeSelectOptionConsts.County.Value;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_Huntersville);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Huntersville, propertyTypeEnum);
            }

            //LES-3407 use the values for Huntersville and Mint Hill instead of choosing the county hours
            //  these two zones should always default hours to 1 hour
            if (hr != null)
            {
                ret.HuntersvilleZoneHrs = hr.DefaultHours;
                ret.HuntersvilleZoneHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.HuntersvilleZoneSelection = hr.EstimationHoursMode == TradeSelectOptionConsts.Town ? TradeSelectOptionConsts.Town.Value : TradeSelectOptionConsts.County.Value;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_Matthews);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Matthews, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.MatthewsZoneHrs = GetZoneHrs(hr.DefaultHours, hr.EstimationHoursMode);
                ret.MatthewsZoneHrsAllowed = hr.EstimationHoursMode == TradeSelectOptionConsts.Town == true ? "1" : "0";
                ret.MatthewsZoneSelection = hr.EstimationHoursMode == TradeSelectOptionConsts.Town ? TradeSelectOptionConsts.Town.Value : TradeSelectOptionConsts.County.Value;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_Mint_Hill);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Mint_Hill, propertyTypeEnum);
            }

            //LES-3407 use the values for Huntersville and Mint Hill instead of choosing the county hours
            //  these two zones should always default hours to 1 hour
            if (hr != null)
            {
                ret.MintHillZoneHrs = hr.DefaultHours;
                ret.MintHillZoneHrsAllowed = hr.IsEnabled == true ? "1" : "0";
                ret.MintHillZoneSelection = hr.EstimationHoursMode == TradeSelectOptionConsts.Town ? TradeSelectOptionConsts.Town.Value : TradeSelectOptionConsts.County.Value;
            }

            if (propertyType == 1 && _model != null)
            {
                hr = _model.DefaultEstimationHours.FirstOrDefault(x => x.DepartmentID == (int)DepartmentNameEnums.Zone_Pineville);
            }
            else
            {
                hr = apihelper.GetDefaultEstimationHour(DepartmentNameEnums.Zone_Pineville, propertyTypeEnum);
            }

            if (hr != null)
            {
                ret.PinevilleZoneHrs = GetZoneHrs(hr.DefaultHours, hr.EstimationHoursMode);
                ret.PinevilleZoneHrsAllowed = hr.EstimationHoursMode == TradeSelectOptionConsts.Town == true ? "1" : "0";
                ret.PinevilleZoneSelection = hr.EstimationHoursMode == TradeSelectOptionConsts.Town ? TradeSelectOptionConsts.Town.Value : TradeSelectOptionConsts.County.Value;
            }
            return ret;
        }

        decimal GetZoneHrs(decimal zoneDefaultHours, string zoneEestimationMode)
        {
            decimal ret = 0;
            if (zoneEestimationMode == TradeSelectOptionConsts.Town)
                ret = zoneDefaultHours;
            return ret;
        }

        #endregion

        #region NPATypes

        private void SaveNPAConfiguration(NPAConfigViewModel nPAConfigViewModel)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            UserIdentity LoggedInUser = new UserIdentity { ID = 1 };
            if (Session["LoggedInUser"] != null)
            {
                LoggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
            }
            List<NpaType> vals = new List<NpaType>();
            List<NpaType> currentNpaTypes = _model != null ? _model.NpaTypes : apihelper.GetAllNpaTypes();
            List<NpaType> newNpa = new List<NpaType>(),
                toNpaActive = new List<NpaType>(),
                toNpaInactive = new List<NpaType>();
            foreach (var item in nPAConfigViewModel.SelectedActiveNPAList)
            {
                if (item.Contains("-1,") == true)
                {
                    //parse the name for allocation type
                    TimeAllocationType timeAllocationType = Helper.TimeAllocationTypes.Where(x => item.Contains(x.ToStringValue())).FirstOrDefault();
                    string apptTypeName = item.Substring(3, item.IndexOf("(") - 3).TrimEnd();
                    newNpa.Add(new NpaType()
                    {
                        AppointmentTypeName = apptTypeName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsActive = true,
                        UpdatedUser = LoggedInUser,
                        TimeAllocationType = timeAllocationType
                    });
                }
                else
                {
                    NpaType current = currentNpaTypes.Where(x => x.ID == int.Parse(item)).FirstOrDefault();
                    if (current.IsActive == false)
                    {
                        current.IsActive = true;
                        current.UpdatedUser = LoggedInUser;
                        toNpaActive.Add(current);
                    }
                }
            }
            foreach (var item in nPAConfigViewModel.SelectedInactiveNPAList)
            {
                if (item.Contains("-1,") == true)
                {
                    //parse the name for allocation type
                    TimeAllocationType timeAllocationType = Helper.TimeAllocationTypes.Where(x => item.Contains(x.ToStringValue())).FirstOrDefault();
                    string apptTypeName = item.Substring(3, item.IndexOf("(") - 3).TrimEnd();
                    newNpa.Add(new NpaType()
                    {
                        AppointmentTypeName = apptTypeName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsActive = false,
                        UpdatedUser = LoggedInUser,
                        TimeAllocationType = timeAllocationType
                    });
                }
                else
                {
                    NpaType current = currentNpaTypes.Where(x => x.ID == int.Parse(item)).FirstOrDefault();
                    if (current.IsActive == true)
                    {
                        current.IsActive = false;
                        current.UpdatedUser = LoggedInUser;
                        toNpaInactive.Add(current);
                    }
                }
            }
            foreach (var item in newNpa)
            {
                apihelper.InsertNpaType(item);
            }
            foreach (var item in toNpaActive)
            {
                apihelper.MakeNpaTypeActive(item);
            }
            foreach (var item in toNpaInactive)
            {
                apihelper.MakeNpaTypeInActive(item);
            }
        }

        private NPAConfigViewModel CreateNPAViewModel()
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            NPAConfigViewModel model = new NPAConfigViewModel();
            List<NpaType> currentNpaTypes = _model.NpaTypes;

            foreach (var item in currentNpaTypes)
            {
                string timeAllocationType = item.TimeAllocationType.ToStringValue();
                timeAllocationType = " ( " + timeAllocationType + " ) ";
                if (item.IsActive == true)
                    model.ActiveNPAList.Add(new SelectListItem() { Text = item.AppointmentTypeName + timeAllocationType, Value = item.ID.ToString() });
                else
                    model.InactiveNPAList.Add(new SelectListItem() { Text = item.AppointmentTypeName + timeAllocationType, Value = item.ID.ToString() });
            }
            return model;
        }
        #endregion

        #region Create Role Tab
        /// <summary>
        /// Initialize the Create Role tab
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private CreateRoleViewModel GetCreateRoleVM(CreateRoleViewModel vm)
        {
            if (vm == null) vm = new CreateRoleViewModel();

            vm.PermsList = _permissionsList;

            List<Permission> modules = new List<Permission>();
            int moduleid = 0;
            foreach (Permission permission in vm.PermsList.OrderBy(x => x.PermissionModuleId).ToList())
            {
                if (moduleid != permission.PermissionModuleId)
                {
                    modules.Add(permission);
                    moduleid = permission.PermissionModuleId.Value;
                }
            }
            vm.PermsModulesList = modules;

            List<SystemRole> systemRoles = _systemRoleswPerms;
            vm.SystemRoles = systemRoles.ToList();

            return vm;
        }

        /// <summary>
        /// used is save Admin method
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private bool SaveCreateRoleVM(CreateRoleViewModel vm)
        {
            bool saveRole = !string.IsNullOrWhiteSpace(vm.SystemRoleName);
            bool success = false;

            if (saveRole)
            {
                //send back systemrole with list of permissions
                //role name
                //parent role id
                SystemRole systemRole = new SystemRole
                {
                    ParentSystemRoleId = vm.ParentSystemRoleId,
                    RoleName = vm.SystemRoleName,
                    Permissions = GetPermissionList(vm),
                    UpdatedUser = new UserIdentity { ID = int.Parse(vm.WrkrId) }
                };
                success = APIHelper.SaveSystemRole(systemRole);
                //vm.ResultsMessage = "Saved Successfully";

            }

            return success;
        }
        /// <summary>
        /// Used in AJAX call to get the checkbox values
        /// in Create Role, Modify Role, and Modify User Permissions
        /// </summary>
        /// <param name="systemroleid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSystemRolePermissions(string systemroleid)
        {
            //get permissions by system role id
            List<Permission> lst = APIHelper.GetSystemRolePermissionList(systemroleid);
            //only send the ones that are true
            List<string> permsnames = new List<string>();
            foreach (Permission permission in lst)
            {
                permsnames.Add(permission.PermissionEnum.ToString());
            }
            return Json(permsnames, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used in AJAX call to get the checkbox values
        /// in Modify User Permissions
        /// </summary>
        /// <param name="systemroleid"></param>
        /// <returns></returns>
        public ActionResult GetUserRolePermissions(string userid)
        {
            //get permissions by user
            List<Permission> lst = APIHelper.GetUserPermissionList(userid);
            //only send the ones that are true
            List<string> permsnames = new List<string>();
            foreach (Permission permission in lst)
            {
                permsnames.Add(permission.PermissionEnum.ToString());
            }
            return Json(permsnames, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// creates list of int (enum val nbr) for permissions to be saved for a role
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private List<Permission> GetPermissionList(PermissionViewModel vm)
        {
            //get the perms list
            List<Permission> perms = new List<Permission>();
            //put in the flattened data as a list
            //sql script select 'if (vm.'+ permission_nm + ') perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.'+ permission_nm + '});' from permission_ref order by permission_nm

            if (vm.Accpt_Rjct_Rview_Dt) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Accpt_Rjct_Rview_Dt });
            if (vm.Activt_NA_Rview) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Activt_NA_Rview });
            if (vm.Add_Prlim_Mtng_Prtcpnt) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Add_Prlim_Mtng_Prtcpnt });
            if (vm.Add_Project_Files) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Add_Project_Files });
            if (vm.Apprv_Mtng_Minuts) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Apprv_Mtng_Minuts });
            if (vm.Assign_To_Me) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Assign_To_Me });
            if (vm.Cancel_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Cancel_Mtng });
            if (vm.Cancel_Prlim_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Cancel_Prlim_Mtng });
            if (vm.Close_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Close_Mtng });
            if (vm.Configure_Express) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Configure_Express });
            if (vm.Create_NPA) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Create_NPA });
            if (vm.E_Express_Rsrvtions) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.E_Express_Rsrvtions });
            if (vm.E_Fclttor) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.E_Fclttor });
            if (vm.E_Plns_Rdy_Dt) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.E_Plns_Rdy_Dt });
            if (vm.E_Schdul_Express) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.E_Schdul_Express });
            if (vm.Enter_Mtng_Prtcpnt) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Enter_Mtng_Prtcpnt });
            if (vm.Estimat_Bkflow) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Estimat_Bkflow });
            if (vm.Estimat_EHS) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Estimat_EHS });
            if (vm.Estimat_Fire) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Estimat_Fire });
            if (vm.Estimat_Trads) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Estimat_Trads });
            if (vm.Estimat_Zoning) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Estimat_Zoning });
            if (vm.Exit_Mtng_Notes_For_Cstmr) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Exit_Mtng_Notes_For_Cstmr });
            if (vm.Holday_Config) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Holday_Config });
            if (vm.Man_Reserve_Express_Time) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Man_Reserve_Express_Time });
            if (vm.Modify_NPA) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Modify_NPA });
            if (vm.Prlim_Estimat_Bkflow) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Prlim_Estimat_Bkflow });
            if (vm.Prlim_Estimat_EHS) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Prlim_Estimat_EHS });
            if (vm.Prlim_Estimat_Fire) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Prlim_Estimat_Fire });
            if (vm.Prlim_Estimat_Trads) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Prlim_Estimat_Trads });
            if (vm.Prlim_Estimat_Zoning) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Prlim_Estimat_Zoning });
            if (vm.Reopen_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Reopen_Mtng });
            if (vm.Reqst_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Reqst_Mtng });
            if (vm.Resend_Notif) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Resend_Notif });
            if (vm.Schdul_Collab_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Collab_Mtng });
            if (vm.Schdul_Exit_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Exit_Mtng });
            if (vm.Schdul_Express_Auto) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Express_Auto });
            if (vm.Schdul_Express_Man) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Express_Man });
            if (vm.Schdul_Notes_Sel) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Notes_Sel });
            if (vm.Schdul_Nxt_Cycl) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Nxt_Cycl });
            if (vm.Schdul_Phasing_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Phasing_Mtng });
            if (vm.Schdul_PrePermitting_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_PrePermitting_Mtng });
            if (vm.Schdul_Prlim_Mtng_Auto) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Prlim_Mtng_Auto });
            if (vm.Schdul_Prlim_Mtng_Man) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Prlim_Mtng_Man });
            if (vm.Schdul_Rview_Pln_Rview_Auto) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Rview_Pln_Rview_Auto });
            if (vm.Schdul_Rview_Pln_Rview_Man) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Rview_Pln_Rview_Man });
            if (vm.Upload_Minuts) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Upload_Minuts });
            if (vm.Use_Express) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Use_Express });
            if (vm.Vw_Fclttor_Wrkload) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Vw_Fclttor_Wrkload });
            if (vm.Vw_Mngmnt_Rprts) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Vw_Mngmnt_Rprts });
            if (vm.Vw_Schdul_Cpcty) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Vw_Schdul_Cpcty });
            if (vm.Schdul_Mtng) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Schdul_Mtng });

            //LES-305 add management reports
            if (vm.Management_Report_1) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Management_Report_1 });
            if (vm.Management_Report_2) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Management_Report_2 });
            if (vm.Management_Report_3) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Management_Report_3 });
            if (vm.Management_Report_4) perms.Add(new Permission { EnumMappingNumber = (int)PermissionEnum.Management_Report_4 });

            return perms;
        }
        #endregion Create Role Tab

        #region Modify Role Tab
        /// <summary>
        /// Initializes the modify role tab 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private ModifyRoleViewModel GetModifyRoleVM(ModifyRoleViewModel vm)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            if (vm == null) vm = new ModifyRoleViewModel();

            vm.PermsList = _permissionsList;

            List<Permission> modules = new List<Permission>();
            int moduleid = 0;
            foreach (Permission permission in vm.PermsList.OrderBy(x => x.PermissionModuleId).ToList())
            {
                if (moduleid != permission.PermissionModuleId)
                {
                    modules.Add(permission);
                    moduleid = permission.PermissionModuleId.Value;
                }
            }
            vm.PermsModulesList = modules;

            List<SystemRole> systemRoles = _systemRoleswPerms;
            vm.SystemRoles = systemRoles.ToList();

            return vm;
        }

        /// <summary>
        /// saves the modify role data when admin is saved.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private bool SaveModifyRoleVM(ModifyRoleViewModel vm)
        {
            //is this a user or a role
            //PermissionUserId will be > 0
            bool isUser = vm.PermissionUserId > 0;
            bool isRole = vm.SystemRoleId > 0;
            bool success = false;
            if (isRole)
            {
                SystemRole systemRole = new SystemRole
                {
                    ID = vm.SystemRoleId,
                    Permissions = GetPermissionList(vm),
                    UpdatedUser = new UserIdentity { ID = int.Parse(vm.WrkrId) }
                };
                success = APIHelper.SaveSystemRole(systemRole);

            }
            else if (isUser)
            {
                UserPermissionsSaveModel userPermissionsSaveModel = new UserPermissionsSaveModel
                {
                    UserId = vm.PermissionUserId,
                    Permissions = GetPermissionList(vm),
                    WrkrId = vm.WrkrId
                };
                success = APIHelper.SaveUserPermissions(userPermissionsSaveModel);
            }
            return success;
        }

        #endregion Modify Role Tab

        #region Modify User Permission Tab
        /// <summary>
        /// Initializes the modify user permission tab 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private ModifyUserPermissionViewModel GetModifyUserPermissionVM(ModifyUserPermissionViewModel vm)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            if (vm == null) vm = new ModifyUserPermissionViewModel();

            vm.PermsList = _permissionsList;

            List<Permission> modules = new List<Permission>();
            int moduleid = 0;
            foreach (Permission permission in vm.PermsList.OrderBy(x => x.PermissionModuleId).ToList())
            {
                if (moduleid != permission.PermissionModuleId)
                {
                    modules.Add(permission);
                    moduleid = permission.PermissionModuleId.Value;
                }
            }
            vm.PermsModulesList = modules;

            List<SystemRole> systemRoles = _systemRoleswPerms;
            vm.SystemRoles = systemRoles.ToList();

            return vm;
        }

        /// <summary>
        /// saves the user permissions data when admin is saved.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private bool SaveModifyUserPermissionVM(ModifyUserPermissionViewModel vm)
        {
            bool saveRole = vm.SystemRoleId > 0;
            bool success = false;
            //if (saveRole)
            //{
            //    //send back systemrole with list of permissions
            //    //role name
            //    //parent role id
            //    SystemRole systemRole = new SystemRole
            //    {
            //        ID = vm.SystemRoleId,
            //        Permissions = GetPermissionList(vm),
            //        UpdatedUser = new UserIdentity { ID = int.Parse(vm.WrkrId) }
            //    };
            //    success = APIHelper.SaveSystemRole(systemRole);
            //    //vm.ResultsMessage = "Saved Successfully";

            //}

            return success;
        }
        #endregion Modify User Permission Tab

        #region OccupancySquareFootage
        [ActionName("GetOccupancySquareFootage")]
        public ActionResult GetOccupancySquareFootage(string userId)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();

            OccupancyViewModel ret = new OccupancyViewModel();
            ret.SquareFootageList = GetSquareFootageList();
            ret.UserOccupancySquareFootageList = apihelper.GetSquareFootageListbyUserOccupancy(Convert.ToInt32(userId));
            return PartialView("_SquareFootageByOccupancy", ret);

        }
        private List<SelectListItem> GetSquareFootageList()
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            return apihelper.GetSquareFootageList().Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.SquareFootage }).ToList();
        }

        [HttpPost]
        [ActionName("CreateOccupancy")]
        public ActionResult CreateOccupancy(List<OccupancyOutput> occupancy)
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            string jsonmessage = string.Empty;
            int userId = Convert.ToInt32(occupancy[0].UserId);
            apihelper.DeleteOccupancy(userId);
            bool saved = apihelper.CreateOccupancy(occupancy);

            if (saved) jsonmessage = "OccupancyCreated.";
            return Json(jsonmessage, JsonRequestBehavior.AllowGet);



        }


        #endregion

        #region Scheduling Multiplier

        private MiscConfigSchedulingMultiplierViewModel CreateSchedulingMultiplierViewModel()
        {
            Helpers.APIHelper apihelper = new Helpers.APIHelper();
            CultureInfo provider = CultureInfo.InvariantCulture;
            MiscConfigSchedulingMultiplierViewModel ret = new MiscConfigSchedulingMultiplierViewModel();
            ret.SchedulingMultiplierProjectTypeList = GetAllSchedulingMultiplierProjectTypesList();

            List<CatalogItem> catlogs;

            if (_model != null)
            {
                catlogs = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER").ToList();
            }
            else
            {
                catlogs = apihelper.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER");
            }

            if (catlogs.Any() && (Convert.ToDateTime(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.END_DATE").FirstOrDefault().Value) > DateTime.Now))
            {
                ret.SchedulingMultiplierUseSelected = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.USE").FirstOrDefault().Value;
                ret.SchedulingMultiplierName = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.NAME").FirstOrDefault().Value;
                ret.SchedulingMultiplierStartDate = DateTime.ParseExact(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.START_DATE").FirstOrDefault().Value, "MM/dd/yyyy", provider);
                ret.SchedulingMultiplierEndDate = DateTime.ParseExact(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.END_DATE").FirstOrDefault().Value, "MM/dd/yyyy", provider);
                ret.SchedulingMultiplierFactor = decimal.Parse(decimal.Parse(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR").FirstOrDefault().Value).ToString("0.##"));
                ret.SchedulingMultiplierSelectedProjectTypes = GetselectedSMProjectTypeList(catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE").FirstOrDefault().Value);
            }

            if (ret.SchedulingMultiplierStartDate == DateTime.MinValue)
            {
                ret.SchedulingMultiplierStartDate = DateTime.Now;
            }

            if (ret.SchedulingMultiplierEndDate == DateTime.MinValue)
            {
                ret.SchedulingMultiplierEndDate = DateTime.Now;
            }

            return ret;
        }

        public bool SaveSchedulingMultiplierViewModel(MiscConfigSchedulingMultiplierViewModel model)
        {
            try
            {
                Helpers.APIHelper apihelper = new Helpers.APIHelper();

                List<CatalogItem> catlogs;

                if (_model != null)
                {
                    catlogs = _model.CatalogItems.Where(x => x.CatalogGroupRefName == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER").ToList();
                }
                else
                {
                    catlogs = apihelper.GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER");
                }

                if (catlogs.Any())
                {
                    CatalogItem smUse = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.USE").FirstOrDefault();
                    smUse.Value = model.SchedulingMultiplierUseSelected;
                    apihelper.UpdateCatalogItem(smUse);
                    CatalogItem smName = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.NAME").FirstOrDefault();
                    smName.Value = model.SchedulingMultiplierName;
                    apihelper.UpdateCatalogItem(smName);
                    CatalogItem smStartDate = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.START_DATE").FirstOrDefault();
                    smStartDate.Value = (model.SchedulingMultiplierStartDate).ToString("MM/dd/yyyy");
                    apihelper.UpdateCatalogItem(smStartDate);
                    CatalogItem smEndDate = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.END_DATE").FirstOrDefault();
                    smEndDate.Value = (model.SchedulingMultiplierEndDate).ToString("MM/dd/yyyy");
                    apihelper.UpdateCatalogItem(smEndDate);
                    CatalogItem smFactor = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.FACTOR").FirstOrDefault();
                    smFactor.Value = model.SchedulingMultiplierFactor.ToString("#.00");
                    apihelper.UpdateCatalogItem(smFactor);
                    CatalogItem smProjectType = catlogs.Where(x => x.Key == "ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER.PROJECT_TYPE").FirstOrDefault();
                    smProjectType.Value = String.Join(",", model.SchedulingMultiplierSelectedProjectTypes.Select(i => i.ToString()).ToArray());
                    apihelper.UpdateCatalogItem(smProjectType);
                }
                return true;
            }

            catch (System.Exception)
            {
                return false;
            }

        }

        List<SelectListItem> GetAllSchedulingMultiplierProjectTypesList()
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(GetProjectType(PropertyTypeEnums.Commercial));
            ret.Add(GetProjectType(PropertyTypeEnums.Mega_Multi_Family));
            ret.Add(GetProjectType(PropertyTypeEnums.Special_Projects_Team));
            ret.Add(GetProjectType(PropertyTypeEnums.Townhomes));
            ret.Add(GetProjectType(PropertyTypeEnums.County_Fire_Shop_Drawings));

            return ret;
        }

        private List<string> GetselectedSMProjectTypeList(string projectTypeList)
        {
            List<string> projectType = new List<string>(projectTypeList.Split(',').Select(s => s));
            return projectType;
        }

        #endregion

        #region "Message Configuration"

        private MessageConfigAdminViewModel CreateMessageConfigurationVM()
        {
            MessageConfigAdminViewModel vm = new MessageConfigAdminViewModel();
            List<MessageTemplateType> messageTemplateTypes = _model.MessageTemplateTypes;
            List<MessageTemplate> messageTemplates = AdminAPIHelper.GetMessageTemplatesByTypeId(-1);
            vm.MessageTemplateDataElements = _model.MessageTemplateDataElements;

            vm.MessageTemplates.Add(new SelectListItem { Text = "Select a Template: ", Value = "-1" });
            vm.MessageTemplateTypes.Add(new SelectListItem { Text = "Select a Template Type: ", Value = "-1" });
            vm.MessageTemplates.AddRange(messageTemplates.Select(x => new SelectListItem { Text = x.TemplateName, Value = x.TemplateId.ToString() }).ToList());
            vm.MessageTemplateTypes.AddRange(messageTemplateTypes.Select(x => new SelectListItem { Text = x.TemplateTypeName, Value = x.EnumMappingValNbr.ToString() }).ToList());

            return vm;
        }

        private void SaveMessageConfiguration(MessageConfigAdminViewModel vm, UserIdentity loggedInUser)
        {
            if (!string.IsNullOrEmpty(vm.MessageTemplateTypeId))
            {
                List<MessageTemplateType> messageTemplateTypes = AdminAPIHelper.GetMessageTemplateTypes();
                //put together date and time
                string dt = vm.ActiveDate.ToShortDateString();
                string tm = vm.ActiveDateTime.ToShortTimeString();
                DateTime activation = DateTime.Parse(dt + ' ' + tm);
                MessageTemplate messageTemplate = new MessageTemplate();
                messageTemplate.ActiveDt = activation;
                messageTemplate.ActiveInd = vm.IsActive;
                messageTemplate.TemplateName = vm.MessageTemplateName;
                messageTemplate.TemplateText = vm.MessageTemplateText;
                messageTemplate.TemplateTypeEnumValNbr = int.Parse(vm.MessageTemplateTypeId);
                messageTemplate.TemplateTypeId = messageTemplateTypes.Where(x => x.EnumMappingValNbr == int.Parse(vm.MessageTemplateTypeId)).FirstOrDefault().TemplateTypeId;

                if (vm.IsEdit != null && vm.IsEdit.Equals("true"))
                {
                    //edit exising template
                    messageTemplate.TemplateId = int.Parse(vm.MessageTemplateId);
                    messageTemplate.UpdatedUser = loggedInUser;
                    AdminAPIHelper.UpdateMessageTemplate(messageTemplate);
                }

                else if (int.Parse(vm.MessageTemplateId)==-1 && !string.IsNullOrEmpty(vm.MessageTemplateName))
                {
                    //save new message
                    messageTemplate.CreatedUser = loggedInUser;
                    AdminAPIHelper.InsertMessageTemplate(messageTemplate);
                }
            }
        }

        #endregion "Message Configuration"

    }
}