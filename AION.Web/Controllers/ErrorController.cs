using System.Web.Mvc;
using AION.Base;
using AION.Web.Helpers;
using AION.Web.Models;

namespace AION.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Internal Error";
            ViewModelBase vm = GetViewModel();
            return View(vm);
        }

        public ActionResult NotFound()
        {
            ViewBag.Title = "Page Not Found";
            ViewModelBase vm = GetViewModel();
            return View(vm);
        }

        private ViewModelBase GetViewModel()
        {
            var vm = new ViewModelBase();
            vm.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            new AuthenticateHelper().GetViewModelWPerms(vm);
            if (ViewData != null & ViewData.Model != null)
            {
                var model = (HandleErrorInfo)ViewData.Model;
                vm.LoggingId = model.Exception.Message;
            }
            return vm;
        }
    }
}