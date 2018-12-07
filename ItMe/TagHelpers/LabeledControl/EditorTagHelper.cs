using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItMe.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.TagHelpers.LabeledControl
{
    public class EditorTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Size { get; set; } = "200px";

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator generator;

        public EditorTagHelper(IHtmlGenerator generator)
        {
            this.generator = generator;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var id = Id ?? For.Name;
            var previewId = $"{id}-preview";

            var editorSideHelper = new LabeledControlTagHelper();
            var editorSideHelperOutput = await editorSideHelper.GetOutput("labeled-control", async (editorContext, useCachedResult, encoder) =>
            {
                var sideLabelHelper = new LabelTagHelper();
                var sideLabelOutput = await sideLabelHelper.GetOutput(editorContext, "lc-label", Label);

                var textAreaHelper = new TextAreaTagHelper(generator)
                {
                    For = For,
                    ViewContext = ViewContext
                };
                var textAreaOutput = await textAreaHelper.GetOutput(editorContext, "textarea");
                textAreaOutput.AddAttributes(new
                {
                    id, style = $"height:{Size}", oninput = $"updatePreview('{previewId}', this.value)"
                });
                var sideControlHelper = new ControlTagHelper();
                var sideControlOutput = await sideControlHelper.GetOutput(editorContext, "lc-control", textAreaOutput);

                return sideLabelOutput.ComposeWith(sideControlOutput);
            });
            var previewSideHelper = new LabeledControlTagHelper();
            var previewSideHelperOutput = await previewSideHelper.GetOutput("labeled-control", async (previewContext, useCachedResult, encoder) =>
            {
                var sideLabelHelper = new LabelTagHelper();
                var sideLabelOutput = await sideLabelHelper.GetOutput(previewContext, "lc-label", "Preview");

                var previewOutput = TagComposer.Compose("div", new
                {
                    id = previewId, style = $"height:{Size}", @class = "preview"
                });
                var sideControlHelper = new ControlTagHelper();
                var sideControlOutput = await sideControlHelper.GetOutput(previewContext, "lc-control", previewOutput);

                return sideLabelOutput.ComposeWith(sideControlOutput);
            });

            output.TagName = "div";
            output.AddAttributes(new { @class = "side-by-side" });
            output.Content.AppendHtml(editorSideHelperOutput);
            output.Content.AppendHtml(previewSideHelperOutput);

            var script = TagComposer.Compose("script");
            script.Content.AppendHtml($@"
    updatePreview('{previewId}', document.getElementById('{id}').value);
    synchronizeScroll('{id}', '{previewId}');
            ");
            output.Content.AppendHtml(script);
        } 
    }
}