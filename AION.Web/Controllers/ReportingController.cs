using AION.Base;
using AION.BL;
using AION.Web.Helpers;
using AION.Web.Models.Reporting;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]

    public class ReportingController : BaseControllerWeb
    {
        // GET: Reporting
        public ActionResult ReportingDashboard()
        {
            APIHelper apihelper = new APIHelper();
            ReportingViewModel reportingViewModel = new ReportingViewModel();
            reportingViewModel.LoggedInUserEmail = GetLoggedInUserEmailAddress();
            
            UpdateUserAndPermissions(reportingViewModel);

            //LES-305
            if (reportingViewModel.PermissionMapping.IsCustomer)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = UIStatusMessage.Insufficient_Permission });
            }
            reportingViewModel.FacilitatorWorkloadSummary = apihelper.GetFacilitatorWorkloadSummary(DateTime.Now.AddDays(-30), DateTime.Now);
            reportingViewModel.ReportUrl = ConfigurationManager.AppSettings["ReportUrl"].ToString();
            reportingViewModel.ManagementReportUrl = ConfigurationManager.AppSettings["ManagementReportUrl"].ToString();
            return View(reportingViewModel);
        }

        [HttpGet]
        public ActionResult GenerateSchedulingLeadTimeData()
        {
            UserIdentity user = JsonConvert.DeserializeObject<UserIdentity>(Session["LoggedInUser"].ToString());

            bool success = ReportingAPIHelper.GenerateSchedulingLeadTimeData(user.ID);

            return Json(success, JsonRequestBehavior.AllowGet);
        }
    }
}