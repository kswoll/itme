using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.TagHelpers.LabeledControl
{
    [HtmlTargetElement("lc-control", ParentTag = "labeled-control")]
    public class ControlTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var labeledControlContext = (LabeledControlContext)context.Items[typeof(LabeledControlTagHelper)];
            labeledControlContext.Control = childContent;

            output.SuppressOutput();
        }
    }
}