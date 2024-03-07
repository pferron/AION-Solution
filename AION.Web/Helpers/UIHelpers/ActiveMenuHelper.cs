using System;
using System.Web.Mvc;

namespace AION.Web.Helpers
{
    public static class ActiveMenuHelper
    {
        public static string MenuLinkWithAction(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            string className = string.Empty;

            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase)
                && String.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase))
                className = "active";

            return className;
        }

        public static string MenuLink(this HtmlHelper htmlHelper, string controllerName)
        {
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            string className = string.Empty;

            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
                className = "active";

            return className;
        }
    }
}