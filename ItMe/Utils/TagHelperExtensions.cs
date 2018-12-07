using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.Utils
{
    public static class TagHelperExtensions
    {
        public static Task<TagHelperOutput> GetOutput(this TagHelper tagHelper, TagHelperContext context, string tagName, string childContent)
        {
            var content = new DefaultTagHelperContent();
            content.Append(childContent);
            return tagHelper.GetOutput(context, tagName, (_, __) => content);
        }

        public static Task<TagHelperOutput> GetOutput(this TagHelper tagHelper, TagHelperContext context, string tagName, params IHtmlContent[] childContent)
        {
            var content = new DefaultTagHelperContent();
            foreach (var item in childContent)
                content.AppendHtml(item);
            return tagHelper.GetOutput(context, tagName, (_, __) => content);
        }

        public static Task<TagHelperOutput> GetOutput(this TagHelper tagHelper, TagHelperContext context, string tagName)
        {
            return tagHelper.GetOutput(context, tagName, (_, __) => new DefaultTagHelperContent());
        }

        public static Task<TagHelperOutput> GetOutput(this TagHelper tagHelper, TagHelperContext context, string tagName, Func<bool, HtmlEncoder, TagHelperContent> getChildContent)
        {
            return tagHelper.GetOutput(context, tagName, (useCachedResult, encoder) => Task.FromResult(getChildContent(useCachedResult, encoder)));
        }

        public static async Task<TagHelperOutput> GetOutput(this TagHelper tagHelper, TagHelperContext context, string tagName, Func<bool, HtmlEncoder, Task<TagHelperContent>> getChildContent)
        {
            var textAreaOutput = new TagHelperOutput(tagName, new TagHelperAttributeList(), getChildContent);
            var textAreaContext = new TagHelperContext(new TagHelperAttributeList(), 
                context.Items, Guid.NewGuid().ToString());
            await tagHelper.ProcessAsync(textAreaContext, textAreaOutput);
            return textAreaOutput;
        }

        public static TagHelperContent ComposeWith(this TagHelperOutput item1, params TagHelperOutput[] rest)
        {
            var content = new DefaultTagHelperContent();
            content.AppendHtml(item1);
            foreach (var item in rest)
            {
                content.AppendHtml(item);
            }
            return content;
        }
    }
}