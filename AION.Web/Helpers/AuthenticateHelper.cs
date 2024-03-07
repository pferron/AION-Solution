using AION.Base;
using AION.BL;
using AION.Web.Models;

namespace AION.Web.Helpers
{
    public class AuthenticateHelper : BaseController
    {
        private string _loggedInUserEmail;
        private bool _isLoggedIn = false;
        public void Login(string loggedInUserEmail)
        {
            if (string.IsNullOrWhiteSpace(loggedInUserEmail))
            {
                TempData["StatusMessage"] = UIStatusMessage.Not_Logged_In;
                RedirectToLoginPage();
            }
        }

        public void RedirectToCustomerHomePage()
        {
            TempData["LoggedInUserEmail"] = _loggedInUserEmail;
            RedirectToAction("ProjectsDashboard", "Customer");
        }

        public void RedirectToInternalHomePage()
        {
            TempData["LoggedInUserEmail"] = _loggedInUserEmail;
            RedirectToAction("Index", "Home");
        }

        public void RedirectToLoginPage()
        {
            TempData["LoggedInUserEmail"] = _loggedInUserEmail;
            RedirectToAction("Index", "Home");
        }

        public void GetViewModelWPerms(ViewModelBase vm)
        {
            APIHelper apihelper = new APIHelper();

            vm.LoggedInUser = apihelper.GetUserIdentityByEmailSysRef(vm.LoggedInUserEmail, (int)ExternalSystemEnum.Accela);

            //get permissions
            vm.PermissionMapping = vm.LoggedInUser.PermissionMapping;

            if (vm.PermissionMapping.IsViewOnly)
            {
                vm.IsReadOnly = true;
                vm.DisabledCls = "disabled";
                vm.DisabledHtml = "disabled=\"disabled\"";
                vm.ReadonlyHtml = "readonly=\"readonly\"";
            }
        }
    }
}