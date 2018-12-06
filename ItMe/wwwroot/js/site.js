function toMarkDown(s)
{
    const reader = new window.commonmark.Parser();
    const writer = new window.commonmark.HtmlRenderer();
    const parsed = reader.parse(s);
    const result = writer.render(parsed);
    return "<div class='markdown'>" + result + "</div>";
}

function synchronizeScroll(editorId, previewId) {
    function syncScroll(from, to) {
        const scrollFrom = from.scrollHeight - from.clientHeight;
        const scrollTo = to.scrollHeight - to.clientHeight;

        if (scrollFrom < 1) {
            return;
        }

        const percentage = (from.scrollTop / scrollFrom) * 100;

        to.scrollTop = (scrollTo / 100) * percentage;
    }

    window.addEventListener("load", () => {
        var currentScrollEvent, timer;

        var editor = document.getElementById(editorId);
        var preview = document.getElementById(previewId);

        function bindScrollEvent(from, to) {
            from.addEventListener("scroll", () => {
                if (currentScrollEvent === to) {
                    return;
                }
                clearTimeout(timer);
                currentScrollEvent = from;
                syncScroll(from, to);
                timer = setTimeout(() => currentScrollEvent = null, 200);
            });
        }

        bindScrollEvent(editor, preview);
        bindScrollEvent(preview, editor);
    });
}

function updatePreview(preview, value) {
    document.getElementById(preview).innerHTML = toMarkDown(value);
}


