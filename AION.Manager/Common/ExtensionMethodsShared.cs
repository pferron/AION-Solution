using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

public static class ExtensionMethodsShared
{
    /// <summary>
    /// Returns the string specified in Description attribute of Enum.
    /// <para>Eg: [Description("Once in a week")]</para> 
    /// Once = -1
    /// </summary>
    /// <param name="value">Enum object</param>
    /// <returns>Returns the string specified in Description attribute of Enum. If nothing found returns default ToString</returns>
    public static string ToStringValue(this Enum value)
    {
        try
        {
            if (value.GetType().GetField(value.ToString()) == null) { throw new Exception(); }
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        catch
        {
            return "";
        }
    }
    public static bool BetweenInclusive(this DateTime instant, DateTime dtFrom, DateTime dtThru)
    {
        if (dtFrom > dtThru) throw new ArgumentException("dtFrom may not be after dtThru", "dtFrom");
        bool isBetween = (instant >= dtFrom && instant <= dtThru);
        return isBetween;
    }

    public static List<SelectListItem> ToSelectList<TEnum>()
    {
        List<SelectListItem> listItems = new List<SelectListItem>();
        foreach (Enum i in Enum.GetValues(typeof(TEnum)))
        {
            listItems.Add(new SelectListItem
            {
                Value = i.ToString(),
                Text = i.ToStringValue()
            });
        }
        return listItems;
    }

}
