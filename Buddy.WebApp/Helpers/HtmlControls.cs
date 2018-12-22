using System.Web.Mvc;

namespace Buddy.WebApp.Helpers
{
    public static class HtmlControls
    {
        public static MvcHtmlString FullStackCheckBox(this HtmlHelper html, string id, string text, string label, 
            bool? isChecked = false, object htmlAttributes = null)
        {
            var tag = new TagBuilder("input");
         
            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("id", id);

            if (isChecked.HasValue && isChecked.Value)
                tag.Attributes.Add("checked", "checked");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);                
                tag.MergeAttributes(attributes);
            }

            string output = tag.ToString(TagRenderMode.Normal);

            if (!string.IsNullOrEmpty(text))
            {
                var textTag = new TagBuilder("label");
                textTag.Attributes.Add("for", id);
                textTag.SetInnerText(text);
                output += textTag.ToString(TagRenderMode.Normal);
            }
            
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString FullStackSwitch(this HtmlHelper html, string id, string left, string right, string label,
            bool? isRight = false, object htmlAttributes = null)
        {

            var isChecked = "";

            if (isRight.HasValue && isRight.Value)
                isChecked = @"checked=""""";
            
            var output = $@"<div class=""switch"">
                <label>{left}
                    <input type=""checkbox"" {isChecked}><span class=""lever""></span>{right}</label>
                </div>";

            /*
            var tag = new TagBuilder("div");

            tag.AddCssClass("switch");

            var labelTag = new TagBuilder("label");
            
            var inputTag = new TagBuilder("input");

            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("id", id);

            
            var labelTag = new TagBuilder("span");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tag.MergeAttributes(attributes);
            }

            string output = tag.ToString(TagRenderMode.Normal);

            if (!string.IsNullOrEmpty(text))
            {
                var textTag = new TagBuilder("label");
                textTag.Attributes.Add("for", id);
                textTag.SetInnerText(text);
                output += textTag.ToString(TagRenderMode.Normal);
            }
            */
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString FullStackHeader(this HtmlHelper html, string text, sbyte headerSize, object htmlAttributes = null)
        {
            var tag = new TagBuilder($"h{headerSize}");

            //tag.AddCssClass("text-themecolor");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tag.MergeAttributes(attributes);
            }

            tag.InnerHtml = text; 

            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FullStackBlock_Obsolete(this HtmlHelper html, object htmlAttributes = null)
        {
            var tag = new TagBuilder($"div");

            tag.AddCssClass("card card-body");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tag.MergeAttributes(attributes);
            }
            
            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
        }


        /*
        public static MvcHtmlString DateTimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            object htmlAttributesBase = new { @class = "date-picker", type = "text" };
            if (htmlAttributes == null) htmlAttributes = htmlAttributesBase;

            var mvcHtmlString = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes);
            var xDoc = XDocument.Parse(mvcHtmlString.ToHtmlString());
            var xElement = xDoc.Element("input");
            if (xElement != null)
            {
                var valueAttribute = xElement.Attribute("value");
                if (valueAttribute != null)
                {
                    try
                    {
                        if (valueAttribute.Value.Trim() != "")
                        {
                            valueAttribute.Value = DateTime.Parse(valueAttribute.Value).ToShortDateString();
                            if (valueAttribute.Value == "1/1/0001")
                                valueAttribute.Value = string.Empty;
                        }
                        else
                        {
                            valueAttribute.Value = string.Empty;
                        }
                    }
                    catch
                    {
                        valueAttribute.Value = string.Empty;
                    }
                }
            }
            return new MvcHtmlString(xDoc.ToString());
        }

        public static MvcHtmlString Url<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);
            if (o == null) return MvcHtmlString.Empty;
            if (!o.ToString().StartsWith("http:")) o = "http://" + o;
            string output = "<a target=\"_blank\" href=\"" + o + "\">" + o + "</a>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString Email<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);
            if (o == null) return MvcHtmlString.Empty;
            string output = "<a href=\"mailto:" + o + "\">" + o + "</a>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString HyperLink<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);
            if (o == null) return MvcHtmlString.Empty;
            string output = "<a href=\"" + o + "\">" + o + "</a>";
            return new MvcHtmlString(output);
        }

        //public static MvcHtmlString Url(this HtmlHelper htmlHelper, object url,string name, string classname = "message")
        //{
        //    if (url == null ||name == null ) return MvcHtmlString.Empty;
        //    string output = "<a href=\""+url+"\">" + name + "</a>";
        //    return new MvcHtmlString(output);
        //}

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, object input, string classname = "message")
        {
            if (input == null) return MvcHtmlString.Empty;
            string output = "<p><div class=\"" + classname + "\">" + input + "</div></p>";
            return new MvcHtmlString(output);
        }
        public static MvcHtmlString ListItem(this HtmlHelper htmlHelper, string name, string controller, string action, object routeValues = null, IDictionary<string, object> htmlAttributes = null)
        {
            var href = System.Web.Mvc.Html.LinkExtensions.ActionLink(htmlHelper, name, action, controller,
                (routeValues == null ? null : new RouteValueDictionary(routeValues)), htmlAttributes);
            string output = "<li>" + href + "</li>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);
            if (o == null) return MvcHtmlString.Empty;
            if (o is bool)
            {
                if ((bool)o)
                    return new MvcHtmlString(Translations.Translation.Yes);
                return new MvcHtmlString(Translations.Translation.No);
            }
            if (o is DateTime)
            {
                return new MvcHtmlString(((DateTime)o).ToShortDateString());
            }
            return DisplayFor(html, expression);
        }

        public static MvcHtmlString CheckBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string field = ExpressionHelper.GetExpressionText(expression);

            bool value = false;
            if (o != null)
            {
                if (o is bool)
                {
                    if ((bool)o)
                        value = true;
                }
            }

            string output = "<input " + (value ? "checked=\"checked\"" : "") + " id=\"" + field + "\" name=\"" + field + "\" type=\"checkbox\" value=" + value.ToString().ToLower() + " />";

            return new MvcHtmlString(output);
        }

        public static MvcHtmlString BasicCheckBoxFor<T>(this HtmlHelper<T> html,
                                                Expression<Func<T, bool>> expression,
                                                object htmlAttributes = null)
        {
            var result = html.CheckBoxFor(expression).ToString();
            const string pattern = @"<input name=""[^""]+"" type=""hidden"" value=""false"" />";
            var single = Regex.Replace(result, pattern, "");
            return MvcHtmlString.Create(single);
        }

        public static MvcHtmlString CheckBoxCustom(this HtmlHelper html, string text, string href, IDictionary<string, object> htmlAttributes)
        {
            var tag = new TagBuilder("input");
            tag.MergeAttributes(htmlAttributes);

            if (!tag.Attributes.ContainsKey("class"))
                tag.Attributes.Add("class", "dialog");

            tag.Attributes.Add("type", "checkbox");

            tag.SetInnerText(text);

            string output = tag.ToString(TagRenderMode.Normal).Replace("_", "-");
            return new MvcHtmlString(output);
        }


        public static MvcHtmlString CheckBox1<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string field = ExpressionHelper.GetExpressionText(expression);

            bool value = false;
            if (o != null)
            {
                if (o is bool)
                {
                    if ((bool)o)
                        value = true;
                }
            }

            string output = "<input " + (value ? "checked=\"checked\"" : "") + " id=\"" + field + "\" name=\"" + field + "\" type=\"checkbox\" value=" + value.ToString().ToLower() + " />";

            return new MvcHtmlString(output);
        }


        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string name, string type, string value, string onClick = null, string classname = "button", bool javaScript = true)
        {

            if (type == "submit")
            {
                string output = "<input type=\"" + type + "\" id=\"" + name +
                    "\" name=\"" + name + "\" value=\"" + value + "\" class=\"" + classname + "\"";

                if (onClick != null)
                {
                    output += " onclick=\"" + onClick + "\"";
                }

                output += " /><text> </text>";

                return new MvcHtmlString(output);
            }
            else
            {
                string output = "<a id=\"" + name + "\" class=\"" + classname + "\"";

                if (onClick != null)
                {
                    if (javaScript)
                        output += " href=\"javascript:;\" onclick=\"" + onClick + "\"";
                    else
                        output += " href=\"" + onClick + "\"";
                }

                output += " >" + value + "</a>";

                return new MvcHtmlString(output);
            }
        }

        public static MvcHtmlString Dialog(this HtmlHelper html, string text, string href, object htmlAttributes)
        {
            return Dialog(html, text, href, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString Dialog(this HtmlHelper html, string text, string href, IDictionary<string, object> htmlAttributes)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttributes(htmlAttributes);

            if (!tag.Attributes.ContainsKey("class"))
                tag.Attributes.Add("class", "dialog");

            tag.Attributes.Add("href", href);

            tag.SetInnerText(text);

            string output = tag.ToString(TagRenderMode.Normal).Replace("_", "-");
            return new MvcHtmlString(output);
        }


        public static MvcHtmlString MobileNavigation(this HtmlHelper html, string text, string href)
        {
            string tag = "<li data-url=\"" + href + "\">";
            // tag += "<a href=\"" + href + "\" class=\"main-menu-item\">";
            tag += @"<div class=""icon-category sbm-right"" > </div>";
            tag += "<h3>" + text + "</h3>";
            //tag += "</a>";

            tag += "</li>";

            //string output = tag.ToString(TagRenderMode.Normal).Replace("_", "-");
            return new MvcHtmlString(tag);
        }

        public static MvcHtmlString MobileListItem(this HtmlHelper html, string text, string href)
        {
            string tag = "<li class=\"list-item\">";
            tag += "<a href=\"" + href + "\" class=\"list-item\">";
            tag += "<span>" + text + "</span>";
            tag += "</a>";
            tag += "</li>";

            //string output = tag.ToString(TagRenderMode.Normal).Replace("_", "-");
            return new MvcHtmlString(tag);
        }


        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string id = null, string alt = null, string source = null, string onError = null)
        {
            string output = "<img ";

            if (id != null) output += " id=\"" + id + "\"";
            if (source != null) output += " src=\"" + source + "\"";
            if (alt != null) output += " alt=\"" + alt + "\"";
            if (onError != null) output += " onerror=\"" + onError + "\"";

            output += " />";

            return new MvcHtmlString(output);
        }

        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }

        public static string DisplayName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
            return value; // MvcHtmlString.Create(value);
        }

        public static IHtmlString Webgrid(this HtmlHelper htmlHelper, IEnumerable<dynamic> source, List<WebGridColumn> columns, int rows = 20, int id = 1)
        {
            var grid = new WebGrid(source, canPage: true, rowsPerPage: rows, fieldNamePrefix: "g" + id, pageFieldName: "p" + id);
            //grid.Bind(source, autoSortAndPage: true);

            grid.Pager(WebGridPagerModes.All);

            return grid.GetHtml(tableStyle: "webgrid",
                    headerStyle: "header",
                    alternatingRowStyle: "alt",
                    footerStyle: "paging", columns: grid.Columns(columns.ToArray()));
        }
    }*/
        }


    }