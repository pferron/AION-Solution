using AION.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace AION.Base
{
    public static class ObjectMapper
    {
        public static List<T> DataTableToList<T>(this DataTable table, string codeColName = "_CD", string descColName = "_DESC", bool concatCodeInText = true, string valueColName = "") where T : class, new()
        {
            try
            {
                string dataMemberName;
                List<T> list = new List<T>();

                var columnNames = table.Columns.Cast<DataColumn>()
                                    .Select(c => c.ColumnName)
                                    .ToList();

                var codeColumnName = (from DataColumn x in table.Columns
                                      where x.ColumnName.Contains(codeColName)
                                      select x.ColumnName).FirstOrDefault();

                var descriptionColumnName = (from DataColumn x in table.Columns
                                             where x.ColumnName.Contains(descColName)
                                             select x.ColumnName).FirstOrDefault();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            //if object with custom attribute is used, use attribute property to map
                            if (Attribute.IsDefined(prop, typeof(CustomTableAttribute)))
                            {
                                dataMemberName = prop.GetCustomAttributes(typeof(CustomTableAttribute), false).OfType<CustomTableAttribute>().FirstOrDefault().ColumnName;
                                if (columnNames.Contains(dataMemberName))
                                {
                                    prop.SetValue(obj, ChangeType(row[dataMemberName], prop.PropertyType));
                                }
                            }
                            else
                            {
                                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);

                                switch (prop.Name)
                                {
                                    case "Id":
                                        propertyInfo.SetValue(obj, ChangeType(row[0], propertyInfo.PropertyType), null);
                                        break;
                                    case "Code":
                                        propertyInfo.SetValue(obj, ChangeType(row[codeColumnName], propertyInfo.PropertyType), null);
                                        break;
                                    case "Description":
                                        propertyInfo.SetValue(obj, ChangeType(row[descriptionColumnName], propertyInfo.PropertyType), null);
                                        break;
                                    case "Text":
                                        string text;
                                      
                                            text = $"{ChangeType(row[descriptionColumnName], propertyInfo.PropertyType)}";
                                        
                                        propertyInfo.SetValue(obj, text, null);
                                        break;
                                    case "Value":
                                        if (string.IsNullOrWhiteSpace(valueColName))
                                        {
                                            propertyInfo.SetValue(obj, ChangeType(row[0], propertyInfo.PropertyType), null);
                                        }
                                        else
                                        {
                                            propertyInfo.SetValue(obj, ChangeType(row[valueColName], propertyInfo.PropertyType), null);

                                        }
                                        break;
                                    case "Active":
                                        propertyInfo.SetValue(obj, ChangeType(row["ACTIVE_IND"], propertyInfo.PropertyType), null);
                                        break;
                                    default:
                                        if (columnNames.Contains(prop.Name))
                                        {
                                            propertyInfo.SetValue(obj, ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                                        }
                                        break;
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == DBNull.Value)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        public static List<T> DataTableToListCustom<T>(this DataTable table, string codeColName = "_ID", string descColName = "_NM") where T : class, new()
        {
            try
            {
                string dataMemberName;
                List<T> list = new List<T>();

                var columnNames = table.Columns.Cast<DataColumn>()
                                    .Select(c => c.ColumnName)
                                    .ToList();

                var codeColumnName = (from DataColumn x in table.Columns
                                      where x.ColumnName.Contains(codeColName)
                                      select x.ColumnName).FirstOrDefault();

                var descriptionColumnName = (from DataColumn x in table.Columns
                                             where x.ColumnName.Contains(descColName)
                                             select x.ColumnName).FirstOrDefault();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            //if object with custom attribute is used, use attribute property to map
                            if (Attribute.IsDefined(prop, typeof(CustomTableAttribute)))
                            {
                                dataMemberName = prop.GetCustomAttributes(typeof(CustomTableAttribute), false).OfType<CustomTableAttribute>().FirstOrDefault().ColumnName;
                                if (columnNames.Contains(dataMemberName))
                                {
                                    prop.SetValue(obj, ChangeType(row[dataMemberName], prop.PropertyType));
                                }
                            }
                            else
                            {
                                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);

                                switch (prop.Name)
                                {
                                    case "Id":
                                        propertyInfo.SetValue(obj, ChangeType(row[0], propertyInfo.PropertyType), null);
                                        break;
                                    case "Code":
                                        propertyInfo.SetValue(obj, ChangeType(row[codeColumnName], propertyInfo.PropertyType), null);
                                        break;
                                    case "Description":
                                        propertyInfo.SetValue(obj, ChangeType(row[descriptionColumnName], propertyInfo.PropertyType), null);
                                        break;
                                    case "Text":
                                        // var text = $"[{ChangeType(row[codeColumnName], propertyInfo.PropertyType)}] {ChangeType(row[descriptionColumnName], propertyInfo.PropertyType)}";
                                        propertyInfo.SetValue(obj, ChangeType(row[descriptionColumnName], propertyInfo.PropertyType), null);
                                        break;
                                    case "Value":
                                        propertyInfo.SetValue(obj, ChangeType(row[codeColumnName], propertyInfo.PropertyType), null);
                                        break;
                                    case "Active":
                                        propertyInfo.SetValue(obj, ChangeType(row["ACTIVE_IND"], propertyInfo.PropertyType), null);
                                        break;
                                    default:
                                        if (columnNames.Contains(prop.Name))
                                        {
                                            propertyInfo.SetValue(obj, ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                                        }
                                        break;
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

    }
}
