using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;

namespace ItMe.Utils
{
    public static class StringExtensions
    {
        public static string Summarize(this string s, int maxLength = 500)
        {
            if (s.Length < maxLength)
                return s;
            else
                return s.Substring(0, maxLength);
        }

        public static IHtmlContent ToMarkDown(this string s, bool removeLinks = false)
        {
            var markdown = CommonMark.CommonMarkConverter.Convert(s);
            if (removeLinks)
            {
                var document = new HtmlDocument();
                document.LoadHtml(markdown);
                foreach (var link in document.DocumentNode.Descendants("a"))
                {
                    link.Name = "span";
                    link.Attributes.Remove("href");
                }

                markdown = document.DocumentNode.OuterHtml;
            }
            return new HtmlString($"<div class=\"markdown\">{markdown}</div>");
        }

        public static IHtmlContent StripHtml(this IHtmlContent content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content.ToString());
            return new HtmlString(document.DocumentNode.InnerText.Trim());
        }
    }
}