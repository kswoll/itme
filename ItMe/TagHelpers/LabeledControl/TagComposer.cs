using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ItMe.TagHelpers.LabeledControl
{
    public static class TagComposer
    {
        public static TagHelperOutput Compose(string tagName, object attributes = null)
        {
            var previewOutput = new TagHelperOutput(tagName, new TagHelperAttributeList(), (_, __) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            previewOutput.AddAttributes(attributes);
            return previewOutput;
        }

        public static void AddAttributes(this TagHelperOutput output, object attributes)
        {
            if (attributes != null)
            {
                foreach (var property in attributes.GetType().GetProperties())
                {
                    var propertyName = property.Name;
                    var attributeName = PascalCaseToKebabCase(propertyName);
                    var propertyValue = property.GetValue(attributes);
                    output.Attributes.Add(attributeName, propertyValue);
                }
            }
        }

        private static string PascalCaseToKebabCase(string s)
        {
            var builder = new StringBuilder();
            var lastIndex = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (char.IsUpper(c))
                {
                    if (i > lastIndex)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("-");
                        }

                        var part = s.Substring(lastIndex, i);
                        builder.Append(part.ToLower());
                        lastIndex = i;
                    }
                }
            }

            if (lastIndex < s.Length)
            {
                builder.Append(s.Substring(lastIndex).ToLower());
            }

            return builder.ToString();
        }
    }
}