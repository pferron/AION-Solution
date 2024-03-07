using AION.Base;
using System.Web.Mvc;

namespace AION.Manager.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}