// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function toMarkDown(s)
{
    const reader = new window.commonmark.Parser();
    const writer = new window.commonmark.HtmlRenderer();
    const parsed = reader.parse(s);
    const result = writer.render(parsed);
    return result;
}
