using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.BL.Models
{
    public class PlanReviewProjectDetails : ModelBase
    {
        public int PlanReviewProjectDetailsID { get; set; }
        public int ProjectID { get; set; }
        public int? ResponserUserID { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string IsApprovedText
        {
            get
            {
                string ret = "";
                if (IsApproved.HasValue)
                {
                    ret = IsApproved.Value == true ? "Accepted" : "Rejected";
                }
                else
                {
                    ret = "";
                }
                return ret;
            }
        }
        private List<SelectListItem> _planReviewResponseSelectList;
        public List<SelectListItem> PlanReviewResponseSelectList
        {
            get
            {
                if (_planReviewResponseSelectList == null) BuildPlanReviewResponseSelectList();
                return _planReviewResponseSelectList;
            }
        }

        private void BuildPlanReviewResponseSelectList()
        {
            _planReviewResponseSelectList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = "Accept", Value = ((int)PlanReviewResponseStatusEnum.Accept).ToString()},
                 new SelectListItem() { Text = "Reject", Value = ((int)PlanReviewResponseStatusEnum.Reject).ToString()}
            };
        }
    }
}