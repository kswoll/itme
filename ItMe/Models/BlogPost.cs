using System;
using ItMe.Utils;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace ItMe.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int CommentCount { get; set; }

        [JsonIgnore]
        public string Url => $"/blog/{Slug}";

        public IHtmlContent GetExcerpt()
        {
            return (Excerpt ?? Body.Summarize()).ToMarkDown(removeLinks: true);
        }
    }
}