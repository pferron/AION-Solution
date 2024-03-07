using AION.BL.Models;
using AION.Manager.Models.ConfigurationHistory;
using AION.Web.Helpers;
using AION.Web.Models.ConfigurationHistory;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class ConfigurationHistoryController : BaseControllerWeb
    {
        public ActionResult ConfigurationHistoryMain()
        {
            ConfigurationHistoryViewModel vm = new ConfigurationHistoryViewModel();
            
            SetUpViewModelBase<ConfigurationHistoryViewModel>(vm);

            //if logged in user is null then return home controller
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            if (vm.PermissionMapping.IsManager == false && vm.PermissionMapping.IsSysAdmin == false && vm.PermissionMapping.IsViewOnly == false)
            {
                return RedirectToAction("Index", "Home", new { StatusMessage = "Insufficient permission" });
            }

            return View(vm);
        }

        /// <summary>
        /// Perform search for table audit
        /// </summary>
        /// <param name="configurationHistory"></param>
        /// <returns></returns>
        public ActionResult GetTableAuditLogListWDetails(ConfigurationHistory configurationHistory)
        {
            List<TableAuditLogViewModel> items = GetTableAuditLogs(ConfigurationHistoryAPIHelper.GetAuditLogListWDetails(configurationHistory));

            return PartialView("_ConfigurationHistory", items);
        }

        private List<TableAuditLogViewModel> GetTableAuditLogs(List<TableAuditLog> logs)
        {
            List<TableAuditLogViewModel> list = new List<TableAuditLogViewModel>();
            foreach (TableAuditLog item in logs)
            {
                TableAuditLogViewModel model = new TableAuditLogViewModel
                {
                    UpdatedDate = item.UpdatedDate.ToShortDateString() + " " + item.UpdatedDate.ToShortTimeString(),
                    UpdatedUser = item.UpdatedUser.FirstName + " " + item.UpdatedUser.LastName,
                    TypeName = item.TypeName,
                    AuditCodeName = item.AuditCodeName + " : " + item.ValTxt
                };
                list.Add(model);
            }

            return list;
        }
    }
}