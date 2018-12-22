using System.Web;
using System.Web.Mvc;

namespace Buddy.WebApp.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString JsString<TModel>(this HtmlHelper<TModel> html, string s)
        {
            return html.Raw(HttpUtility.JavaScriptStringEncode(s));
        }
    }
}