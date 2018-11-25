using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.TagHelpers.LabeledControl
{
    [HtmlTargetElement("lc-label", ParentTag = "labeled-control")]
    public class LabelTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var labeledControlContext = (LabeledControlContext)context.Items[typeof(LabeledControlTagHelper)];
            labeledControlContext.Label = childContent;

            output.SuppressOutput();
        }
    }
}