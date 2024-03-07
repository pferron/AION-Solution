using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Manager.Common
{
    /// <summary> Enum Extension Methods </summary>
    /// <typeparam name="T"> type of Enum </typeparam>
    public class Enum<T> where T : struct, IConvertible
    {
        public static int Count
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetNames(typeof(T)).Length;
            }
        }
        public static List<SelectListItem> ToSelectList
        {
            get
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                foreach (Enum i in Enum.GetValues(typeof(T)))
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

        public static List<T> ToList()
        {
            List<T> enums = new List<T>();
            foreach (T i in Enum.GetValues(typeof(T)))
            {
                enums.Add(i);
            }
            return enums;
        }
    }
}