using AION.BL;
using AION.Web.Helpers;
using System.Web.Mvc;

public static class ExtensionMethods
{
    public static UserIdentity FillDesignatedRoles(this UserIdentity userIdentity)
    {
        if (userIdentity.DesignatedRoles == null || userIdentity.DesignatedRoles.Count == 0)
        {
            userIdentity.DesignatedRoles = new APIHelper().GetSystemRolesByUserId(userIdentity.ID);
        }
        return userIdentity;
    }


    public static UserIdentity FillDesignatedDepartments(this UserIdentity userIdentity)
    {
        if (userIdentity.DesignatedDepartments == null || userIdentity.DesignatedDepartments.Count == 0)
        {
            userIdentity.DesignatedDepartments = new APIHelper().GetDesignatedDepartmentsByUserId(userIdentity.ID);
        }
        return userIdentity;
    }

    public static MvcHtmlString RawHtml(this string original)
    {
        return MvcHtmlString.Create(original);
    }
}
