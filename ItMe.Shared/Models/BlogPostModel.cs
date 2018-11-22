using System;
using Newtonsoft.Json;

namespace ItMe.Shared.Models
{
    public class BlogPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        
        [JsonIgnore]
        public string Url => $"/blog/{Slug}";
    }
}