using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.TagHelpers.LabeledControl
{
    public class LabeledControlContext
    {
        public TagHelperContent Label { get; set; }
        public TagHelperContent Control { get; set; }
    }
}