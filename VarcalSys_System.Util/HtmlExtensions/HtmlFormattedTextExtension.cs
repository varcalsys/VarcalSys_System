using System.Web;
using System.Web.Mvc;

namespace VarcalSys_System.Util.HtmlExtensions
{
    public static class HtmlFormattedTextExtension
    {
         public static MvcHtmlString FormatedText(this HtmlHelper helper, string text)
         {
             text = HttpUtility.HtmlEncode(text);
             text = text.Replace("\n", "<br />");

             return new MvcHtmlString(text);
         }
    }
}