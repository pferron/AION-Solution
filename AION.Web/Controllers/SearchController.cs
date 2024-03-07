using AION.Web.Helpers;
using AION.Web.Models;
using AION.Web.Models.Search;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class SearchController : BaseControllerWeb
    {
        private string _loggedInUser;

        // GET: Search
        public ActionResult SearchDashboard()
        {
            Helpers.APIHelper apiHelper = new Helpers.APIHelper();
            SearchViewModel vm = new SearchViewModel();
            _loggedInUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedInUser;

            UpdateUserAndPermissions(vm);

            ////get permissions
            ////vm = GetAdminViewModelWPerms(vm);
            if (vm.LoggedInUser.ID == 0)
                return RedirectToAction("Index", "Home");
            if (vm.PermissionMapping.IsCustomer) { return RedirectToAction("ProjectsDashboard", "Customer"); }

            vm.Facilitators = apiHelper.GetAllFacilitators();
            vm.Estimators = apiHelper.GetAllEstimators();
            vm.Managers = apiHelper.GetAllManagers();

            return View(vm);
        }

        [ActionName("SearchProjects")]
        public ActionResult SearchProjects(SearchViewModel searchViewModel)
        {
            Helpers.APIHelper apiHelper = new Helpers.APIHelper();

            // fix up special characters before API call

            string projectNumber = searchViewModel.ProjectNumber == null ? string.Empty : Regex.Replace(searchViewModel.ProjectNumber, "[^a-zA-Z0-9]", String.Empty);
            string projectName = searchViewModel.ProjectName == null ? string.Empty : Regex.Replace(searchViewModel.ProjectName, "[^a-zA-Z0-9]", String.Empty);
            string projectAddress = searchViewModel.ProjectAddress == null ? string.Empty : Regex.Replace(searchViewModel.ProjectAddress, "[^a-zA-Z0-9]", String.Empty);
            string customerName = searchViewModel.CustomerName == null ? string.Empty : Regex.Replace(searchViewModel.CustomerName, "[^a-zA-Z0-9]", String.Empty);
            string planReviewer = searchViewModel.PlanReviewer == null ? string.Empty : Regex.Replace(searchViewModel.PlanReviewer, "[^a-zA-Z0-9]", String.Empty);

            var projects = SearchAPIHelper.SearchProjects(
                    searchViewModel.StartDate,
                    searchViewModel.EndDate,
                    projectNumber,
                    projectName,
                    projectAddress,
                    customerName,
                    planReviewer,
                    (int)searchViewModel.ProjectStatus,
                    searchViewModel.EstimatorId,
                    searchViewModel.FacilitatorId,
                    (int)searchViewModel.MeetingType
                );

            ProjectSearchResultsViewModel vm = new ProjectSearchResultsViewModel();
            _loggedInUser = GetLoggedInUserEmailAddress();
            vm.LoggedInUserEmail = _loggedInUser;

            vm.ProjectList = projects;

            return PartialView("_Projects", vm);
        }
    }
}