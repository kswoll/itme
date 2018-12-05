using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.TagHelpers.LabeledControl
{
    [RestrictChildren("lc-label", "lc-control")]
    public class LabeledControlTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var labeledControlContext = new LabeledControlContext();
            context.Items.Add(typeof(LabeledControlTagHelper), labeledControlContext);

            await output.GetChildContentAsync();

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "labeled-control");

            output.Content.AppendHtmlLine(@"<label style=""width:100%;"">");
            output.Content.AppendHtml(@"<span class=""label"" width=""100%"">");
            output.Content.AppendHtml(labeledControlContext.Label);
            output.Content.AppendHtmlLine("</span><br />");
            output.Content.AppendHtml(@"<span class=""control"" width=""100%"">");
            output.Content.AppendHtml(labeledControlContext.Control);
            output.Content.AppendHtml("</span>");
            output.Content.AppendHtml("</label>");
        }
    }
}