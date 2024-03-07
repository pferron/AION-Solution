using AION.BL;
using AION.Web.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    public class HomeController : BaseControllerWeb
    {
        /// <summary>
        /// AION Home page -- Search page
        /// Logic here to update users and permissions objects
        /// Logic to update status message
        /// 
        /// </summary>
        /// <param name="LoggedInUserEmail"></param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        public ActionResult Landing()
        {
            ViewModelBase vm = new ViewModelBase();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();

            if (string.IsNullOrWhiteSpace(vm.LoggedInUserEmail))
            {
                TempData["UIStatusMessage"] = UIStatusMessage.Insufficient_Permission;
                vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                LogOutUser(vm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UpdateUserAndPermissions(vm);

                if (vm.LoggedInUser.IsActive == false)
                {
                    TempData["UIStatusMessage"] = UIStatusMessage.Insufficient_Permission;
                    vm.UIStatusMessage = UIStatusMessage.Insufficient_Permission;
                    LogOutUser(vm);
                    return RedirectToAction("Index", "Home");

                }

                //if this is a customer, send to projects dashboard
                if (vm.PermissionMapping.IsCustomer)
                    return RedirectToAction("ProjectsDashboard", "Customer", new { LoggedInUserEmail = vm.LoggedInUserEmail });
            }

            return RedirectToAction("SearchDashboard", "Search");
        }

        /// <summary>
        /// Public Landing Page
        /// 
        /// If user is logged in, send to Landing action to get the right home page 
        /// and send the customer to the right page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewModelBase vm = new ViewModelBase();
            //if TempData isn't null, check for ui status message
            string statusmsg = string.Empty;
            if (TempData["UIStatusMessage"] != null)
            {
                statusmsg = TempData["UIStatusMessage"].ToString();
            }
            if (User.Identity.IsAuthenticated && string.IsNullOrWhiteSpace(statusmsg))
            {
                return RedirectToAction("Landing");
            }
            else
            {
                if (string.IsNullOrEmpty(statusmsg))
                {
                    vm.UIStatusMessage = UIStatusMessage.NA;
                }
                else
                {
                    vm.UIStatusMessage = (UIStatusMessage)System.Enum.Parse(typeof(UIStatusMessage), statusmsg);

                }
                return View(vm);

            }
        }

        private void LogOutUser(ViewModelBase vm)
        {
            vm.LoggedInUser = new UserIdentity { ID = 0 };
            vm.LoggedInUserEmail = string.Empty;
        }
    }
}