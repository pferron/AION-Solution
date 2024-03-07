using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AION.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Adds readonly option to dropdownlistfor
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool enabled)
        {
            if (enabled)
            {
                return SelectExtensions.DropDownListFor<TModel, TProperty>(html, expression, selectList, htmlAttributes);
            }

            var selectMarkup = SelectExtensions.DropDownListFor<TModel, TProperty>(html, expression, selectList, htmlAttributes);
            //this replace allows the Selected items to remain selected while disabling the other options so submitting
            //  the form will get the correct values
            selectMarkup = new MvcHtmlString(selectMarkup.ToHtmlString().Replace("option value", "option disabled=\"disabled\" value"));
            var hdmk = InputExtensions.HiddenFor(html, expression);
            return selectMarkup;
        }
        /// <summary>
        /// Add readonly option to textareafor
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool enabled)
        {
            var htmlAttributesAsDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!enabled) htmlAttributesAsDict.Add("readonly", "readonly");
            return htmlHelper.TextAreaFor(expression, htmlAttributesAsDict);
        }
        /// <summary>
        /// Adds readonly option for TextBoxFor
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool enabled)
        {
            var htmlAttributesAsDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!enabled)
            {
                object val = new object();
                //TODO: clean this up after checking number type
                //this is an input[number]
                //type is only added as an attribute to the number input type
                if (htmlAttributesAsDict.TryGetValue("type", out val))
                {
                    if (val.ToString() == "number")
                    {
                        //htmlAttributesAsDict.Add("disabled", "disabled");
                        htmlAttributesAsDict.Add("readonly", "readonly");

                    }
                    else
                    {
                        //catch it if it has something other than number
                        htmlAttributesAsDict.Add("readonly", "readonly");
                    }
                }
                else
                {
                    //this is a input[text]
                    htmlAttributesAsDict.Add("readonly", "readonly");
                    //htmlAttributesAsDict.Add("disabled", "disabled");

                }
            }
            return htmlHelper.TextBoxFor(expression, htmlAttributesAsDict);
        }
        /// <summary>
        /// Adds Readonly option for ListBoxFor
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool enabled)
        {
            if (enabled)
            {
                return SelectExtensions.ListBoxFor<TModel, TProperty>(html, expression, selectList, htmlAttributes);
            }

            var selectMarkup = SelectExtensions.ListBoxFor<TModel, TProperty>(html, expression, selectList, htmlAttributes);
            //this replace allows the Selected items to remain selected while disabling the other options so submitting
            //  the form will get the correct values
            selectMarkup = new MvcHtmlString(selectMarkup.ToHtmlString().Replace("option value", "option disabled=\"disabled\" value"));

            return selectMarkup;
        }
        /// <summary>
        /// Adds Readonly option for CheckBoxFor
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes, bool enabled)
        {
            if (enabled)
                return InputExtensions.CheckBoxFor<TModel>(htmlHelper, expression, htmlAttributes);
            var htmlAttributesAsDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            //htmlAttributesAsDict.Add("disabled", "disabled");
            htmlAttributesAsDict.Add("onclick", "return false");
            return InputExtensions.CheckBoxFor<TModel>(htmlHelper, expression, htmlAttributesAsDict);
        }
        /// <summary>
        /// Add readonly option for CheckBox
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, object htmlAttributes, bool enabled)
        {
            if (enabled)
                return InputExtensions.CheckBox(htmlHelper, name, htmlAttributes);
            var htmlAttributesAsDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            //htmlAttributesAsDict.Add("disabled", "disabled");
            htmlAttributesAsDict.Add("onclick", "return false");
            return InputExtensions.CheckBox(htmlHelper, name, htmlAttributesAsDict);
        }

        public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes, bool enabled)
        {
            if (enabled)
                return TextAreaExtensions.TextArea(htmlHelper, name, value, htmlAttributes);
            var htmlAttributesAsDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            htmlAttributesAsDict.Add("readonly", "readonly");
            return TextAreaExtensions.TextArea(htmlHelper, name, value, htmlAttributesAsDict);
        }

        public static SelectList SelectListForBoolean(object selectedValue = null)
        {
            SelectListItem[] selectListItems = new SelectListItem[2];

            var itemFalse = new SelectListItem();
            itemFalse.Value = "false";
            itemFalse.Text = "No";
            selectListItems[0] = itemFalse;

            var itemTrue = new SelectListItem();
            itemTrue.Value = "true";
            itemTrue.Text = "Yes";
            selectListItems[1] = itemTrue;

            var selectList = new SelectList(selectListItems, "Value", "Text", selectedValue);

            return selectList;
        }
    }
}