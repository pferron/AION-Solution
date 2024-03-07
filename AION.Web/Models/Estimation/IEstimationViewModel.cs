using AION.BL;
using AION.BL.Models;
using AION.Web.BusinessEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public interface IEstimationViewModel
    {
        string LoggedInUserEmail { get; set; }
        UserIdentity LoggedInUser { get; set; }
        PermissionMapping PermissionMapping { get; set; }
        bool IsReadOnly { get; set; }
        string DisabledCls { get; set; }
        string DisabledHtml { get; set; }
        string ReadonlyHtml { get; set; }

        bool CanEditBEMP { get; set; }
        bool CanEditFire { get; set; }
        bool CanEditHealth { get; set; }
        bool CanEditZoning { get; set; }
        bool CanEditBackFlow { get; set; }
        bool CanViewBEMP { get; set; }
        bool CanViewFire { get; set; }
        bool CanViewHealth { get; set; }
        bool CanViewZoning { get; set; }
        bool CanViewBackFlow { get; set; }

        List<Facilitator> FacilitatorList { get; set; }
        List<Reviewer> ReviewersList { get; set; }
        List<EstimatorUIModel> EstimatorList { get; set; }

        List<SelectListItem> GenerateFacilitatorListViewItems();

        List<SelectListItem> GenerateEstimatorListViewItems();

    }
}
