using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VarcalSys_System.Util.HtmlExtensions
{
    public static class MessagesExtensions
    {
        public static MvcHtmlString Messages(this HtmlHelper helper, string message, string cssClass = "errors")
        {
            if (string.IsNullOrWhiteSpace(message)) return null;

            return Messages(helper, new [] {message}, cssClass);
        }

        public static MvcHtmlString Messages(this HtmlHelper helper, IEnumerable<string> messages, string cssClass = "errors")
        {
            if (messages == null || !messages.Any()) return null;

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("<div class=\"{0}\">", cssClass);
            builder.Append(@"<ul>");

            foreach (var message in messages)
            {
                builder.AppendFormat("<li>{0}</li>", message);
            }

            builder.Append(@"</ul>");
            builder.Append(@"</div>");

            return new MvcHtmlString(builder.ToString());
        }
    }
}