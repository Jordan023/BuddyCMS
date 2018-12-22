using System;
using System.Web.Mvc;

namespace Buddy.WebApp.Helpers
{    
    public class HtmlContainers : IDisposable
    {
        protected HtmlHelper _helper;

        public HtmlContainers(HtmlHelper helper, object htmlAttributes)
        {
            _helper = helper;

            var tag = new TagBuilder($"div");

            tag.AddCssClass("card card-body");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tag.MergeAttributes(attributes);
            }
            
            _helper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.StartTag));
        }

        public void Dispose()
        {
            _helper.ViewContext.Writer.Write("</div>");
        }
    }

    public static class CustomDisposableHelpers
    {
        public static HtmlContainers FullStackDiv(this HtmlHelper self, object htmlAttributes)
        {
            return new HtmlContainers(self, htmlAttributes);
        }
    }
}