using Microsoft.AspNetCore.Html;

namespace ItMe.Utils
{
    public class OpenGraphInfo
    {
        public string Title { get; set; }
        public IHtmlContent Description { get; set; }
        public string Image { get; set; }
    }
}