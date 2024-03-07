using AION.Base;
using AION.Web.Models;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    public class DemoController : BaseController
    {
        // GET: Demo
        public ActionResult Index(DemoViewModel dvm)
        {

            return View(dvm);
        }
    }
}