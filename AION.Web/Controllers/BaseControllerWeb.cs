using AION.Base;
using AION.BL;
using AION.Web.Helpers;
using AION.Web.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Claims;

namespace AION.Web.Controllers
{
    public class BaseControllerWeb : BaseController
    {

        public void SetUpViewModelBase<T>(T vm) where T : ViewModelBase, new()
        {
            string loggedInUser = GetLoggedInUserEmailAddress();

            vm.LoggedInUserEmail = loggedInUser;
            if (vm == null || string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm = new T()
                {
                    LoggedInUserEmail = loggedInUser
                };
            }
            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                vm.LoggedInUserEmail = loggedInUser;
            }
            else
            {
                vm.LoggedInUserEmail = vm.LoggedInUserEmail;
            }

            UpdateUserAndPermissions(vm);
        }

        public void UpdateUserAndPermissions(ViewModelBase vm)
        {
            if (Session["LoggedInUser"] == null)
            {
                new AuthenticateHelper().GetViewModelWPerms(vm);
                Session["LoggedInUser"] = JsonConvert.SerializeObject(vm.LoggedInUser);
                Session["PermissionMap"] = JsonConvert.SerializeObject(vm.PermissionMapping);
            }
            else
            {
                vm.PermissionMapping = JsonConvert.DeserializeObject<BusinessEntities.PermissionMapping>(Session["PermissionMap"].ToString());
                vm.LoggedInUser = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());
            }

            vm.IsUserInternal = !vm.PermissionMapping.IsCustomer;
        }

        public new string GetLoggedInUserEmailAddress()
        {
            //for external (guest) user, using 'User.Identity.Name' returns email address with 'live#' text i.e. 'live#john.doe@gmail.com
            //so need to look in schemas to pull emailaddress. For internal user, the emailAddress schema doesn't exist so if the loggedin user
            //is internal user, we will have to get the emailAddress from User.Identity.Name call
            string email;

            if (Session["LoggedInUserEmail"] != null)
                email = Session["LoggedInUserEmail"].ToString();
            else
            {
                email = ClaimsPrincipal.Current?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                if (string.IsNullOrWhiteSpace(email))
                {
                    email = User.Identity.Name;
                }
                Session["LoggedInUserEmail"] = email;
            }
            var log = Logger.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), $"LoggedInUserEmailAddress: {email}");
            return email;
        }
    }
}