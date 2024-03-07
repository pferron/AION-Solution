using AION.BL;
using AION.BL.Models;
using AION.Web.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class SearchViewModel : ViewModelBase
    {
        public SearchViewModel()
        {
            Facilitators = new List<Facilitator>();
            Estimators = new List<EstimatorUIModel>();
            Managers = new List<UserIdentity>();
            ProjectSearchResultsViewModel = new ProjectSearchResultsViewModel();
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatusEnum ProjectStatus { get; set; }

        private string _planReviewer;
        public string PlanReviewer
        {
            get { return _planReviewer; }
            set { _planReviewer = string.IsNullOrEmpty(value) ? value : value.Trim(); }
        }
        public int FacilitatorId { get; set; }
        public int EstimatorId { get; set; }

        private string _projectNumber;
        public string ProjectNumber
        {
            get { return _projectNumber; }
            set { _projectNumber = string.IsNullOrEmpty(value) ? value : value.Trim(); }
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = string.IsNullOrEmpty(value) ? value : value.Trim(); }
        }

        private string _projectAddress;
        public string ProjectAddress
        {
            get { return _projectAddress; }
            set { _projectAddress = string.IsNullOrEmpty(value) ? value : value.Trim(); }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = string.IsNullOrEmpty(value) ? value : value.Trim(); }
        }
        public MeetingTypeEnum MeetingType { get; set; }

        public List<Facilitator> Facilitators { get; set; }

        public SelectList FacilitatorSelectList
        {
            get
            {
                if (Facilitators.Any())
                {
                    var facilitators = Facilitators
                    .Select(x =>
                            new System.Web.Mvc.SelectListItem
                            {
                                Value = x.ID.ToString(),
                                Text = $"{x.FirstName} {x.LastName}"
                            });
                    return new SelectList(facilitators, "Value", "Text");
                }
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public List<UserIdentity> Managers { get; set; }
        public List<EstimatorUIModel> Estimators { get; set; }

        public SelectList EstimatorSelectList
        {
            get
            {
                List<SelectListItem> estimators = new List<SelectListItem>();
                if (Estimators.Any())
                {

                    foreach (EstimatorUIModel estimator in Estimators)
                    {
                        estimators.Add(new SelectListItem
                        {
                            Value = estimator.ID.ToString(),
                            Text = $"{estimator.FirstName} {estimator.LastName}"
                        });
                    }
                }

                if (Managers.Any())
                {
                    foreach (UserIdentity manager in Managers)
                    {
                        estimators.Add(new SelectListItem
                        {
                            Value = manager.ID.ToString(),
                            Text = $"{manager.FirstName} {manager.LastName}"
                        });
                    }
                }

                if (estimators.Count() > 0)
                {
                    estimators = estimators.OrderBy(x => x.Text).GroupBy(x => x.Text).Select(x => x.First()).ToList();
                    return new SelectList(estimators, "Value", "Text");
                }

                return new SelectList(Enumerable.Empty<System.Web.Mvc.SelectListItem>());
            }
        }

        public List<SelectListItem> ProjectStatusSelectList
        {
            get
            {
                return BuildProjectStatusSelectList();
            }
        }

        public List<SelectListItem> MeetingTypeSelectList
        {
            get
            {
                return BuildMeetingTypeSelectList();
            }
        }

        public ProjectSearchResultsViewModel ProjectSearchResultsViewModel { get; set; }

        private static List<SelectListItem> BuildProjectStatusSelectList()
        {
            var projectStatusList = new List<SelectListItem>();
            foreach (ProjectStatusEnum item in Enum.GetValues(typeof(ProjectStatusEnum)))
            {
                projectStatusList.Add(new SelectListItem { Text = item.ToStringValue(), Value = ((int)item).ToString() });
            }
            return projectStatusList.OrderBy(x => x.Text).ToList();
        }

        private static List<SelectListItem> BuildMeetingTypeSelectList()
        {
            var meetingTypeList = new List<SelectListItem>();
            foreach (MeetingTypeEnum item in Enum.GetValues(typeof(MeetingTypeEnum)))
            {
                if (item != MeetingTypeEnum.NA && item != MeetingTypeEnum.Express)
                    meetingTypeList.Add(new SelectListItem { Text = item.ToStringValue(), Value = ((int)item).ToString() });
            }
            return meetingTypeList.OrderBy(x => x.Text).ToList();
        }
    }
}